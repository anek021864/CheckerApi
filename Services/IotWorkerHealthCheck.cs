using JigNetApi;
using JigNetApi.Data;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using RabbitMQ.Client;

public class IotWorkerHealthCheck : IHealthCheck
{
    private readonly IServiceProvider _sp;
    private readonly IConnection? _connection;
    private readonly IChannel? _channel;

    public IotWorkerHealthCheck(
        IServiceProvider sp,
        IConnection? connection = null,
        IChannel? channel = null
    )
    {
        _sp = sp;
        _connection = connection;
        _channel = channel;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken token = default
    )
    {
        var now = DateTime.UtcNow;
        var last = IotWorkerService.LastRun.ToUniversalTime();
        var since = now - last;

        var issues = new List<string>();

        // 1) Heartbeat/loop
        if (since > TimeSpan.FromSeconds(30))
            issues.Add($"LastRun too old: {since.TotalSeconds:F0}s");

        // 2) RabbitMQ
        if (_connection is null || !_connection.IsOpen)
            issues.Add("RabbitMQ connection is closed");
        if (_channel is null || !_channel.IsOpen)
            issues.Add("RabbitMQ channel is closed");

        // 3) Database
        try
        {
            using var scope = _sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ProdCheckerDbContext>();
            var canConnect = await db.Database.CanConnectAsync(token);
            if (!canConnect)
                issues.Add("Database cannot connect");
        }
        catch (Exception ex)
        {
            issues.Add($"Database check error: {ex.Message}");
        }

        if (issues.Count == 0)
            return HealthCheckResult.Healthy("IotWorkerService is healthy");

        return new HealthCheckResult(
            status: HealthStatus.Unhealthy,
            description: string.Join(" | ", issues)
        );
    }
}
