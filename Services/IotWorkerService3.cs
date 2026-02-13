using System.Text;
using System.Text.Json;
using JigNetApi.Data;
using JigNetApi.Dtos;
using JigNetApi.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace JigNetApi;

public class IotWorkerService3 : BackgroundService
{
    public static DateTime LastRun { get; set; } = DateTime.Now;
    private readonly IServiceProvider _serviceProvider;
    private readonly IConfiguration _conf;
    private readonly ILogger<IotWorkerService3> _logger; // แก้เป็น Service3 ให้ตรงกัน
    private IConnection? _connection;
    private IChannel? _channel;

    public IotWorkerService3(
        IServiceProvider serviceProvider,
        IConfiguration conf,
        ILogger<IotWorkerService3> logger
    )
    {
        _serviceProvider = serviceProvider;
        _conf = conf;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Task นี้จะรันขนานไปกับ Consumer ของ RabbitMQ
        _ = Task.Run(
            async () =>
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    LastRun = DateTime.Now; // บอกระบบว่า "ฉันยังตื่นอยู่"
                    await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
                }
            },
            stoppingToken
        );

        try
        {
            var factory = new ConnectionFactory()
            {
                HostName = _conf["RabbitMq:HostName"],
                UserName = _conf["RabbitMq:UserName"],
                Password = _conf["RabbitMq:Password"],
                Port = int.Parse(_conf["RabbitMq:Port"] ?? "5672"),
                // DispatchConsumersAsync = true // จำเป็นสำหรับ RabbitMQ.Client รุ่นเก่าบางตัว แต่ 7.x มักจะเป็น default
            };

            _connection = await factory.CreateConnectionAsync(stoppingToken);
            _channel = await _connection.CreateChannelAsync(cancellationToken: stoppingToken);

            await _channel.QueueDeclareAsync(
                queue: "iot_master_postgre_queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                cancellationToken: stoppingToken
            );

            // จำกัดการดึงข้อมูลมาทำทีละ 1 message เพื่อไม่ให้ Memory บวม
            await _channel.BasicQosAsync(0, 1, false, stoppingToken);

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var dto = JsonSerializer.Deserialize<TIotMasterCreateDto>(message);

                    if (dto != null)
                    {
                        using var scope = _serviceProvider.CreateScope();
                        var db =
                            scope.ServiceProvider.GetRequiredService<ProdCheckerPostgreSqlDqlDbContext>();

                        var entity = new t_iot_master();
                        TIotMasterMapper.ApplyToPostgreSqlEntity(dto, entity);

                        db.Add(entity);
                        await db.SaveChangesAsync(stoppingToken);

                        await _channel.BasicAckAsync(ea.DeliveryTag, false);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing message: {DeliveryTag}", ea.DeliveryTag);
                    // ส่ง Nack และไม่ให้ Requeue (เอาไปลง Dead Letter หรือทิ้งไป) เพื่อป้องกัน Loop
                    await _channel.BasicNackAsync(ea.DeliveryTag, false, requeue: false);
                }
            };

            await _channel.BasicConsumeAsync(
                "iot_master_postgre_queue",
                false,
                consumer,
                stoppingToken
            );

            // รอจนกว่าจะมีการ Cancel
            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Worker is stopping...");
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Fatal error in Worker Service");
        }
        finally
        {
            if (_channel != null)
                await _channel.CloseAsync();
            if (_connection != null)
                await _connection.CloseAsync();
        }
    }
}
