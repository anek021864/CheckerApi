using System.Text;
using System.Text.Json;
using JigNetApi.Data;
using JigNetApi.Dtos;
using JigNetApi.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace JigNetApi;

public class IotWorkerService2 : BackgroundService
{ // สร้างตัวแปร static เพื่อให้ Program.cs เข้าถึงได้
    public static DateTime LastRun { get; set; } = DateTime.Now;
    private readonly IServiceProvider _serviceProvider;
    private IConnection? _connection;
    private IConfiguration _conf;
    private IChannel? _channel; // เวอร์ชัน 7.x ใช้ IChannel แทน IModel

    private readonly ILogger<IotWorkerService2> _logger; // ประกาศตัวแปร logger

    public IotWorkerService2(
        IServiceProvider serviceProvider,
        IConfiguration conf,
        ILogger<IotWorkerService2> logger
    )
    {
        _serviceProvider = serviceProvider;
        _conf = conf;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            _logger.LogInformation("Worker Started at: {time}", DateTimeOffset.Now);
            var factory = new ConnectionFactory()
            {
                HostName = _conf["RabbitMq:HostName"].ToString(), // ใส่ IP ของ Server RabbitMQ
                UserName = _conf["RabbitMq:UserName"].ToString(), // บน Server ไม่ควรใช้ guest/guest
                Password = _conf["RabbitMq:Password"].ToString(),
                Port = Convert.ToInt32(_conf["RabbitMq:Port"]), // Port มาตรฐานของ RabbitMQ
            };
            _connection = await factory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();

            await _channel.QueueDeclareAsync(
                queue: "iot_master2_queue",
                durable: true,
                exclusive: false,
                autoDelete: false
            );

            // ใช้ AsyncEventingBasicConsumer สำหรับเวอร์ชัน 7.x
            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var dto = JsonSerializer.Deserialize<TJigNetCheckerCrateDto>(message);
                if (dto != null)
                {
                    using var scope = _serviceProvider.CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<ProdCheckerDbContext>(); // ใส่ชื่อ DbContext ของคุณ

                    var entity = new T_JIGNET_CHECKER();
                    TJigNetCheckerMapper.ApplyToEntity(dto, entity);

                    db.Add(entity);
                    try
                    {
                        await db.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning("Exception :" + ex);
                    }
                    // ยืนยันการรับข้อมูลแบบ Async
                    await _channel.BasicAckAsync(ea.DeliveryTag, false);
                }
            };

            await _channel.BasicConsumeAsync(
                queue: "iot_master2_queue",
                autoAck: false,
                consumer: consumer
            );

            // รันค้างไว้จนกว่าจะหยุดแอป
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    LastRun = DateTime.Now; // อัปเดตเวลาทุกครั้งที่ทำงาน
                    // รับค้างไว้จนกว่าจะหยุดแอป
                    await Task.Delay(1000, stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    // เมื่อมีการสั่งปิดแอป (stoppingToken ถูกเรียก) ให้จบการทำงานของ Loop อย่างสงบ
                    _logger.LogWarning("Worker Service2 is stopping...");
                    // --- ส่วนสำคัญ: ถ้า DB พัง ให้เขียนลงไฟล์ทันที ---
                    // var errorData = System.Text.Json.JsonSerializer.Serialize(req);
                    // Log.Error("DATABASE_ERROR: {Message} | DATA_TO_SAVE: {Data}", ex.Message, errorData);
                    break;
                }
                catch (Exception ex)
                {
                    // จัดการ Error อื่นๆ ที่อาจเกิดขึ้น
                    // var errorData = System.Text.Json.JsonSerializer.Serialize(req);
                    // Log.Error("DATABASE_ERROR: {Message} | DATA_TO_SAVE: {Data}", ex.Message, errorData);
                    _logger.LogError(ex, "Error occurred in IotWorkerService2");
                }
            }
        }
        catch (Exception ex) { }
    }
}
