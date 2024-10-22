using PMC.Application.Extensions;
using PMC.Infrastructure.Extensions;
using PMC.Infrastructure.Seeder;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApiServices();
builder.Services.AddApplication();
builder.Host.UseSerilog((context, configuration) =>
    configuration
        //.MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
        //.MinimumLevel.Override("Microsoft.EntityFrameworkCore", Serilog.Events.LogEventLevel.Information)
        //.WriteTo.File("Logs/PMC-API- .log", rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true)
        //.WriteTo.Console(outputTemplate:"[{Timestamp:dd-MM HH:mm:ss} {Level:u3}]|{SourceContext}|{NewLine} {Message:lj}{NewLine}{Exception}")
        .ReadFrom.Configuration(context.Configuration)
);

var app = builder.Build();


var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IDatabaseSeeder>();
await seeder.SeedAsync();

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
