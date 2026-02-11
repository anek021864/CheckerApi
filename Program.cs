using HealthChecks.UI.Client;
using JigNetApi;
using JigNetApi.Data;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using RabbitMQ.Client;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ProdCheckerDbContext>(options =>
    options.UseOracle(builder.Configuration["Db:Oracle"])
);

// // --- ตั้งค่า Serilog ---
// Log.Logger = new LoggerConfiguration()
//     .WriteTo.Console()
//     .WriteTo.File("Logs/db-fallback-.txt", rollingInterval: RollingInterval.Day)
//     .CreateLogger();

// builder.Host.UseSerilog(); // บอกให้ระบบใช้ Serilog แทนตัวเดิม

// --- 1. Register Services ---

var factory = new ConnectionFactory()
{
    HostName = builder.Configuration["RabbitMq:HostName"].ToString(), // ใส่ IP ของ Server RabbitMQ
    UserName = builder.Configuration["RabbitMq:UserName"].ToString(), // บน Server ไม่ควรใช้ guest/guest
    Password = builder.Configuration["RabbitMq:Password"].ToString(),
    Port = Convert.ToInt32(builder.Configuration["RabbitMq:Port"]), // Port มาตรฐานของ RabbitMQ
};

// เชื่อมต่อ RabbitMQ แบบ Async ตามมาตรฐานเวอร์ชัน 7.x
var connection = await factory.CreateConnectionAsync();
builder.Services.AddSingleton<IConnection>(connection);
builder.Services.AddSingleton<WorkerStateService>();
builder.Services.AddHostedService<IotWorkerService>();
builder
    .Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateTimeConverterUsingDateTimeParse());
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // ใช้แบบ Default ไปก่อนเพื่อเช็คว่าผ่านไหม

// --- 2. ตั้งค่า Monitoring (Health Checks) ---
builder
    .Services.AddHealthChecks()
    .AddCheck(
        "RabbitMQ",
        () => connection.IsOpen ? HealthCheckResult.Healthy() : HealthCheckResult.Unhealthy()
    )
    .AddOracle(connectionString: builder.Configuration["Db:Oracle"]!, name: "Oracle-DB")
    .AddCheck(
        "Iot-Worker",
        () =>
        {
            var state = builder
                .Services.BuildServiceProvider()
                .GetRequiredService<WorkerStateService>();
            var silenceTime = DateTime.Now - state.LastRunTime;

            // ถ้าเงียบไปนานเกิน 60 วินาที ถือว่า Unhealthy
            if (silenceTime.TotalSeconds > 60)
            {
                return HealthCheckResult.Unhealthy(
                    $"Worker silent for {silenceTime.TotalSeconds}s"
                );
            }

            return HealthCheckResult.Healthy("Worker is pulsing");
        },
        tags: new[] { "worker" }
    );

builder
    .Services.AddHealthChecksUI(setup =>
    {
        // ใส่ Full URL ของ API ของคุณ
        setup.AddHealthCheckEndpoint("Main Services", "http://localhost:5027/health");
        setup.SetEvaluationTimeInSeconds(10);
    })
    .AddInMemoryStorage();

var app = builder.Build();

// --- 2. Configure HTTP Request Pipeline ---
// ให้ Swagger แสดงเฉพาะตอน Development หรือจะเปิดตลอดก็ได้ถ้าใช้ใน Network ภายใน
if (app.Environment.IsDevelopment() || true)
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Checker API V1");
        // ตั้งให้หน้า Swagger เป็นหน้าแรกเมื่อรันโปรเจกต์ (optional)
        c.RoutePrefix = string.Empty;
    });
}

// --- 4. Endpoints ---
app.MapHealthChecks(
    "/health",
    new HealthCheckOptions
    {
        Predicate = _ => true,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
    }
);
app.UseHealthChecksUI(config =>
{
    config.UIPath = "/monitor";
});

app.UseAuthorization();
app.MapControllers();
app.Run();
