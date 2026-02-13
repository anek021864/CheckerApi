using System.Text;
using System.Text.Json;
using JigNetApi.Data;
using JigNetApi.Dtos;
using JigNetApi.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace JigNetApi;

public class IotWorkerService : BackgroundService
{
    public static DateTime LastRun { get; set; } = DateTime.Now;
    private readonly IServiceProvider _serviceProvider;
    private readonly IConfiguration _conf;
    private readonly ILogger<IotWorkerService3> _logger; // แก้เป็น Service3 ให้ตรงกัน
    private IConnection? _connection;
    private IChannel? _channel;

    public IotWorkerService(
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
                queue: "iot_master_queue",
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
                        var db = scope.ServiceProvider.GetRequiredService<ProdCheckerDbContext>(); // ใส่ชื่อ DbContext ของคุณ

                        var entity = new T_IOT_MASTER();
                        TIotMasterMapper.ApplyToEntity(dto, entity);
                        db.Add(entity);
                        await db.SaveChangesAsync(stoppingToken);
                        await _channel.BasicAckAsync(ea.DeliveryTag, false);
                        //LastRun = DateTime.Now;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing message: {DeliveryTag}", ea.DeliveryTag);
                    // ส่ง Nack และไม่ให้ Requeue (เอาไปลง Dead Letter หรือทิ้งไป) เพื่อป้องกัน Loop
                    await _channel.BasicNackAsync(ea.DeliveryTag, false, requeue: false);
                }
            };

            await _channel.BasicConsumeAsync("iot_master_queue", false, consumer, stoppingToken);

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
                _logger.LogWarning("Worker Service is stopping...");

                // // // --- ส่วนสำคัญ: ถ้า DB พัง ให้เขียนลงไฟล์ทันที ---
                //  var errorData = System.Text.Json.JsonSerializer.Serialize(req);
                //  Log.Error("DATABASE_ERROR: {Message} | DATA_TO_SAVE: {Data}", ex.Message, errorData);

                break;
            }
            catch (Exception ex)
            {
                // // จัดการ Error อื่นๆ ที่อาจเกิดขึ้น
                //   var errorData = System.Text.Json.JsonSerializer.Serialize(req);
                //  Log.Error("DATABASE_ERROR: {Message} | DATA_TO_SAVE: {Data}", ex.Message, errorData);
                _logger.LogError(ex, "Error occurred in IotWorkerService");
            }
        }
    }
}


// public class IotWorkerService : BackgroundService
// { // สร้างตัวแปร static เพื่อให้ Program.cs เข้าถึงได้
//     public static DateTime LastRun { get; set; } = DateTime.Now;
//     private readonly IServiceProvider _serviceProvider;
//     private IConnection? _connection;
//     private IConfiguration _conf;
//     private IChannel? _channel; // เวอร์ชัน 7.x ใช้ IChannel แทน IModel

//     private readonly ILogger<IotWorkerService> _logger; // ประกาศตัวแปร logger

//     public IotWorkerService(
//         IServiceProvider serviceProvider,
//         IConfiguration conf,
//         ILogger<IotWorkerService> logger
//     )
//     {
//         _serviceProvider = serviceProvider;
//         _conf = conf;
//         _logger = logger;
//     }

//     protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//     {
//         try
//         {
//             _logger.LogInformation("Worker Started at: {time}", DateTimeOffset.Now);
//             var factory = new ConnectionFactory()
//             {
//                 HostName = _conf["RabbitMq:HostName"].ToString(), // ใส่ IP ของ Server RabbitMQ
//                 UserName = _conf["RabbitMq:UserName"].ToString(), // บน Server ไม่ควรใช้ guest/guest
//                 Password = _conf["RabbitMq:Password"].ToString(),
//                 Port = Convert.ToInt32(_conf["RabbitMq:Port"]), // Port มาตรฐานของ RabbitMQ
//             };
//             _connection = await factory.CreateConnectionAsync();
//             _channel = await _connection.CreateChannelAsync();

//             await _channel.QueueDeclareAsync(
//                 queue: "iot_master_queue",
//                 durable: true,
//                 exclusive: false,
//                 autoDelete: false
//             );

//             // ใช้ AsyncEventingBasicConsumer สำหรับเวอร์ชัน 7.x
//             var consumer = new AsyncEventingBasicConsumer(_channel);
//             consumer.ReceivedAsync += async (model, ea) =>
//             {
//                 var body = ea.Body.ToArray();
//                 var message = Encoding.UTF8.GetString(body);
//                 var dto = JsonSerializer.Deserialize<TIotMasterCreateDto>(message);

//                 if (dto != null)
//                 {
//                     using var scope = _serviceProvider.CreateScope();
//                     var db = scope.ServiceProvider.GetRequiredService<ProdCheckerDbContext>(); // ใส่ชื่อ DbContext ของคุณ

//                     var entity = new T_IOT_MASTER();
//                     TIotMasterMapper.ApplyToEntity(dto, entity);
//                     db.Add(entity);
//                     try
//                     {
//                         await db.SaveChangesAsync();
//                         await _channel.BasicAckAsync(ea.DeliveryTag, false);
//                     }
//                     catch (Exception ex)
//                     {
//                         _logger.LogWarning("Exception :" + ex);
//                         // ยืนยันการรับข้อมูลแบบ Async
//                     }
//                 }
//             };

//             await _channel.BasicConsumeAsync(
//                 queue: "iot_master_queue",
//                 autoAck: false,
//                 consumer: consumer
//             );

//             // รันค้างไว้จนกว่าจะหยุดแอป
//             while (!stoppingToken.IsCancellationRequested)
//             {
//                 try
//                 {
//                     LastRun = DateTime.Now; // อัปเดตเวลาทุกครั้งที่ทำงาน

//                     // รับค้างไว้จนกว่าจะหยุดแอป
//                     await Task.Delay(1000, stoppingToken);
//                 }
//                 catch (OperationCanceledException)
//                 {
//                     // เมื่อมีการสั่งปิดแอป (stoppingToken ถูกเรียก) ให้จบการทำงานของ Loop อย่างสงบ
//                     _logger.LogWarning("Worker Service is stopping...");

//                     // // // --- ส่วนสำคัญ: ถ้า DB พัง ให้เขียนลงไฟล์ทันที ---
//                     //  var errorData = System.Text.Json.JsonSerializer.Serialize(req);
//                     //  Log.Error("DATABASE_ERROR: {Message} | DATA_TO_SAVE: {Data}", ex.Message, errorData);

//                     break;
//                 }
//                 catch (Exception ex)
//                 {
//                     // // จัดการ Error อื่นๆ ที่อาจเกิดขึ้น
//                     //   var errorData = System.Text.Json.JsonSerializer.Serialize(req);
//                     //  Log.Error("DATABASE_ERROR: {Message} | DATA_TO_SAVE: {Data}", ex.Message, errorData);
//                     _logger.LogError(ex, "Error occurred in IotWorkerService");
//                 }
//             }
//         }
//         catch (Exception ex) { }
//     }
// }
