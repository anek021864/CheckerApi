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
[Route("api/postgre/checker-logs")]
[ApiKeyRequired]
public class CheckerLogsPostgreController : ControllerBase
{
    private readonly ProdCheckerPostgreSqlDqlDbContext _db;
    private readonly IConnection _connection;
    private readonly IConfiguration _conf;

    public CheckerLogsPostgreController(
        ProdCheckerPostgreSqlDqlDbContext db,
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
                queue: "iot_master_postgre_queue",
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
                routingKey: "iot_master_postgre_queue",
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
        var entity = await _db.Set<t_iot_master>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.id == id);

        if (entity is null)
            return NotFound();
        return Ok(TIotMasterMapper.ToDtoPostgre(entity));
    }

    [HttpGet("{ProdSn}/{CheckerName}")]
    public async Task<IActionResult> CheckerLink(string ProdSn, string CheckerName)
    {
        DateTime startDate = DateTime.Now.AddDays(-14); // ย้อนหลัง 7 วัน
        DateTime endDate = DateTime.Now;
        var entity = await _db
            .t_iot_masters.Where(i =>
                i.product_sn == ProdSn
                && i.cheker_name == CheckerName
                && i.productiontime >= startDate
                && i.productiontime <= endDate
            )
            .OrderByDescending(i => i.productiontime)
            .Select(t => new { t.cheker_name, t.result })
            .FirstOrDefaultAsync();

        if (entity is null)
            return NotFound();

        if (entity.result == 1)
        {
            return Ok(new { resutl = "PASS" });
        }
        else if (entity.result == 0)
        {
            return UnprocessableEntity(new { result = "FAIL" });
        }
        else
        {
            return NotFound();
        }
    }

    [HttpGet("{ProdSn}/{CheckerName}/{Date}")]
    public async Task<IActionResult> ListData(string ProdSn, string CheckerName, string Date)
    {
        if (!DateTime.TryParse(Date, out DateTime parsedDate))
        {
            return BadRequest("รูปแบบวันที่ไม่ถูกต้อง กรุณาใช้ d-M-yyyy");
        }

        DateTime startDate = parsedDate.AddDays(-14); // ย้อนหลัง 7 วัน
        DateTime endDate = DateTime.Now;
        var entity = await _db
            .t_iot_masters.Where(i =>
                i.product_sn == ProdSn
                && i.cheker_name == CheckerName
                && i.productiontime >= startDate
                && i.productiontime <= endDate
            )
            .OrderByDescending(i => i.productiontime)
            .FirstOrDefaultAsync();

        if (entity is null)
        {
            return NotFound();
        }
        return Ok(entity);
    }
}
