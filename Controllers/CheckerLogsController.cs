using System.Configuration;
using System.Text;
using System.Text.Json;
using JigNetApi.Data;
using JigNetApi.Dtos;
using JigNetApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Expressions.Internal;
using RabbitMQ.Client;

namespace JigNetApi;

[ApiController]
[Route("api/checker-logs")]
public class CheckerLogsController : ControllerBase
{
    private readonly ProdCheckerDbContext _db;
    private readonly IConnection _connection;
    private readonly IConfiguration _conf;

    public CheckerLogsController(
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
    public async Task<IActionResult> Create([FromBody] TIotMasterCreateDto dto)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);
        try
        {
            using var channel = await _connection.CreateChannelAsync();
            await channel.QueueDeclareAsync(
                queue: "iot_master_queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            dto.ProductionTime = DateTime.Now;
            var json = JsonSerializer.Serialize(dto);
            var body = Encoding.UTF8.GetBytes(json);

            await channel.BasicPublishAsync(
                exchange: "",
                routingKey: "iot_master_queue",
                body: body
            );

            return Accepted(new { message = "ส่งข้อมูลเข้า Queue เรียบร้อยแล้ว" });
        }
        catch (Exception ex)
        {
            return Problem(title: "RabbitMQ Error", detail: ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var entity = await _db.Set<T_IOT_MASTER>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ID == id);

        if (entity is null)
            return NotFound();
        return Ok(TIotMasterMapper.ToDto(entity));
    }

    [HttpGet("{ProdSn}/{CheckerName}")]
    public async Task<IActionResult> CheckerLink(string ProdSn, string CheckerName)
    {
        DateTime startDate = DateTime.Now.AddDays(-14); // ย้อนหลัง 7 วัน
        DateTime endDate = DateTime.Now;
        var entity = await _db
            .T_IOT_MASTERs.Where(i =>
                i.PRODUCT_SN == ProdSn
                && i.CHEKER_NAME == CheckerName
                && i.PRODUCTIONTIME >= startDate
                && i.PRODUCTIONTIME <= endDate
            )
            .OrderByDescending(i => i.PRODUCTIONTIME)
            .Select(t => new { t.CHEKER_NAME, t.RESULT })
            .FirstOrDefaultAsync();

        if (entity is null)
            return NotFound();

        if (entity.RESULT == 1)
        {
            return Ok(new { resutl = "PASS" });
        }
        else if (entity.RESULT == 0)
        {
            return UnprocessableEntity(new { result = "FAIL" });
        }
        else
        {
            return NotFound();
        }
    }
}
