using HealthChecks.UI.Client;
using JigNetApi;
using JigNetApi.Data;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// --- 1. Database & Services ---
builder.Services.AddDbContext<ProdCheckerDbContext>(options =>
    options.UseOracle(builder.Configuration["Db:Oracle"])
);
builder.Services.AddDbContext<ProdCheckerPostgreSqlDqlDbContext>(options =>
    options.UseNpgsql(builder.Configuration["Db:Postgres"])
);

// builder.Services.AddSingleton<WorkerStateService>();
builder.Services.AddHostedService<IotWorkerService>();
builder.Services.AddHostedService<IotWorkerService2>();
builder.Services.AddHostedService<IotWorkerService3>();

// --- 2. RabbitMQ Setup with Error Handling ---
var factory = new ConnectionFactory()
{
    HostName = builder.Configuration["RabbitMq:HostName"]?.ToString(),
    UserName = builder.Configuration["RabbitMq:UserName"]?.ToString(),
    Password = builder.Configuration["RabbitMq:Password"]?.ToString(),
    Port = builder.Configuration.GetValue<int>("RabbitMq:Port", 5672),
};

IConnection? connection = null;

try
{
    // พยายามเชื่อมต่อ RabbitMQ ตอน Startup
    connection = await factory.CreateConnectionAsync();
}
catch (Exception ex)
{
    // ถ้าเชื่อมต่อไม่ได้ ให้ Log ไว้ (ถ้ามี Logger) และปล่อยให้แอปไปต่อได้
    Console.WriteLine($"[Warning] Could not connect to RabbitMQ on startup: {ex.Message}");
}

// Register Services (ส่งค่า connection เข้าไป แม้จะเป็น null)
builder.Services.AddSingleton(factory);
if (connection != null)
{
    builder.Services.AddSingleton<IConnection>(connection);
}

// --- 3. Monitoring (Health Checks) ---
var healthBuilder = builder.Services.AddHealthChecks();

// ตรวจสอบ RabbitMQ Health
healthBuilder.AddCheck(
    "RabbitMQ",
    () =>
    {
        if (connection != null && connection.IsOpen)
        {
            return HealthCheckResult.Healthy("RabbitMQ is connected.");
        }
        return HealthCheckResult.Unhealthy("RabbitMQ is disconnected or was never initialized.");
    },
    tags: new[] { "rabbitmq" }
);

// ตรวจสอบ Oracle DB Health
healthBuilder.AddOracle(builder.Configuration["Db:Oracle"]!, name: "Oracle-DB");
healthBuilder.AddCheck(
    "IotWorker1 : checker log to oracle",
    () =>
    {
        var isHealthy = (DateTime.Now - IotWorkerService.LastRun).TotalSeconds < 60;
        return isHealthy
            ? HealthCheckResult.Healthy()
            : HealthCheckResult.Unhealthy("Worker is lagging");
    }
);
healthBuilder.AddCheck(
    "IotWorker2 : checker process99 log to oracle",
    () =>
    {
        var isHealthy = (DateTime.Now - IotWorkerService2.LastRun).TotalSeconds < 60;
        return isHealthy
            ? HealthCheckResult.Healthy()
            : HealthCheckResult.Unhealthy("Worker is lagging");
    }
);
healthBuilder.AddCheck(
    "IotWorker3 : checker log to postgreSQL ",
    () =>
    {
        var isHealthy = (DateTime.Now - IotWorkerService3.LastRun).TotalSeconds < 60;
        return isHealthy
            ? HealthCheckResult.Healthy()
            : HealthCheckResult.Unhealthy("Worker is lagging");
    }
);
builder
    .Services.AddHealthChecksUI(setup =>
    {
        setup.AddHealthCheckEndpoint("Main Services", "/health");
        setup.SetEvaluationTimeInSeconds(10);
    })
    .AddInMemoryStorage();

// --- 4. API & JSON Setup ---
builder
    .Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateTimeConverterUsingDateTimeParse());
    });
builder.Services.AddEndpointsApiExplorer();

// builder.Services.AddSwaggerGen();

// builder.Services.AddSwaggerGen(c =>
// {
//     c.SwaggerDoc("v1", new OpenApiInfo { Title = "Checker API V1", Version = "v1" });

//     // เพิ่ม Bearer Auth สำหรับ Swagger
//     var securitySchema = new OpenApiSecurityScheme
//     {
//         Description =
//             "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
//         Name = "Authorization",
//         In = ParameterLocation.Header,
//         Type = SecuritySchemeType.Http,
//         Scheme = "bearer",
//         BearerFormat = "JWT",
//         Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" },
//     };
//     c.AddSecurityDefinition("Bearer", securitySchema);
//     c.AddSecurityRequirement(
//         new OpenApiSecurityRequirement { { securitySchema, new[] { "Bearer" } } }
//     );
// });

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Checker API V1", Version = "v1" });

    // 🔑 เพิ่ม API Key สำหรับ Swagger
    var apiKeyScheme = new OpenApiSecurityScheme
    {
        Description = "API Key via X-API-KEY header",
        Name = "X-API-KEY",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "ApiKeyScheme",
        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "ApiKey" },
    };

    c.AddSecurityDefinition("ApiKey", apiKeyScheme);

    c.AddSecurityRequirement(new OpenApiSecurityRequirement { { apiKeyScheme, new string[] { } } });
});

var app = builder.Build();

// --- 5. Pipeline Configuration ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Checker API V1");
        c.RoutePrefix = string.Empty;
    });
}
app.UseMiddleware<ApiKeyMiddleware>();
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

//app.UseAuthorization();

app.MapControllers();

// Dispose connection เฉพาะเมื่อมันถูกสร้างขึ้นมาจริงๆ
app.Lifetime.ApplicationStopping.Register(() =>
{
    if (connection != null && connection.IsOpen)
    {
        connection.Dispose();
    }
});

app.Run();
