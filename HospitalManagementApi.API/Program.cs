using HospitalManagementApi.Application;
using HospitalManagementApi.Infrastructure.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// ==================== Logging Configuration ====================
builder.Host.UseSerilog((context, configuration) =>
    configuration
        .MinimumLevel.Information()
        .WriteTo.Console()
        .WriteTo.File("logs/hospitalmanagement-.txt", rollingInterval: RollingInterval.Day)
);
 
// ==================== Services Configuration ====================

builder.Services.AddApplicationServices();

builder.Services.AddInfrastructureServices(builder.Configuration);

// ==================== CORS Configuration ====================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});


var app = builder.Build();

// ==================== Middleware Pipeline ====================
 
// Exception Handling Middleware
// app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.MapControllers();

// ==================== Health Check Endpoint ====================
app.MapGet("/health", () => Results.Ok(new { status = "Healthy", timestamp = DateTime.UtcNow }))
   .Produces(StatusCodes.Status200OK);

// ==================== Default Endpoint ====================
app.MapGet("/", () => Results.Ok(new { message = "Welcome to the Hospital Management API" }))
   .Produces(StatusCodes.Status200OK);

app.Run();
