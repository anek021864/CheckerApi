// using Microsoft.Extensions.Diagnostics.HealthChecks;

// namespace JigNetApi;

// public class WorkerHealthCheck : IHealthCheck
// {
//     private readonly WorkerStateService _state;

//     public WorkerHealthCheck(WorkerStateService state) => _state = state;

//     public Task<HealthCheckResult> CheckHealthAsync(
//         HealthCheckContext context,
//         CancellationToken ct = default
//     )
//     {
//         var silenceTime = DateTime.Now - _state.LastRunTime;
//         if (silenceTime.TotalSeconds > 60)
//             return Task.FromResult(
//                 HealthCheckResult.Unhealthy($"Worker silent for {silenceTime.TotalSeconds:F0}s")
//             );

//         return Task.FromResult(HealthCheckResult.Healthy("Worker is pulsing"));
//     }
// }
