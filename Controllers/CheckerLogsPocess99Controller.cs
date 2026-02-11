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
}
