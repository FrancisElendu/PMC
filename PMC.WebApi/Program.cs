using PMC.Application.Extensions;
using PMC.Infrastructure.Extensions;
using PMC.Infrastructure.Seeder;
using PMC.WebApi.Middlewares;
using Serilog;
using System.Diagnostics;

//TODO: need to improve this 

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
    configuration
        .ReadFrom.Configuration(context.Configuration)
);

Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));

try
{
    Log.Information("Starting web host");

    // Add services to the container.
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddApiServices();
    builder.Services.AddApplication();

    builder.Host.UseSerilog();  // Ensure this is attached to the host

    builder.Services.AddScoped<ExceptionHandlingMiddleware>();

    var app = builder.Build();

    var scope = app.Services.CreateScope();
    var seeder = scope.ServiceProvider.GetRequiredService<IDatabaseSeeder>();
    await seeder.SeedAsync();

    // Register the Exception Handling Middleware
    app.UseMiddleware<ExceptionHandlingMiddleware>();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();  // Ensure Serilog logs are flushed on shutdown
}

