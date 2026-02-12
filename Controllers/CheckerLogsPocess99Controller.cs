using System.Text;
using System.Text.Json;
using JigNetApi.Data;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;

namespace JigNetApi;

[ApiController]
[Route("api/checker99-logs")]
public class CheckerLogsPocess99Controller : ControllerBase
{
    private readonly ProdCheckerDbContext _db;
    private readonly IConnection _connection;
    private readonly IConfiguration _conf;

    public CheckerLogsPocess99Controller(
        ProdCheckerDbContext db,
        IConfiguration conf,
        IConnection connection
    )
    {
        _db = db;
        _conf = conf;
        _connection = connection;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TJigNetCheckerCrateDto dto)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);
        try
        {
            using var channel = await _connection.CreateChannelAsync();
            await channel.QueueDeclareAsync(
                queue: "iot_master2_queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            dto.DATECHECK = DateTime.Now.ToString("yyMMdd");
            var json = JsonSerializer.Serialize(dto);
            var body = Encoding.UTF8.GetBytes(json);

            await channel.BasicPublishAsync(
                exchange: "",
                routingKey: "iot_master2_queue",
                body: body
            );

            return Accepted(new { message = "ส่งข้อมูลเข้า Queue เรียบร้อยแล้ว" });
        }
        catch (Exception ex)
        {
            return Problem(title: "RabbitMQ Error", detail: ex.Message);
        }
    }
}
