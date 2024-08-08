using SimonV839.LoggingHelpers;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SimonV839.ShipCrewsWebApiRestSwaggerEF.Models;

Log.Logger = new LoggerConfiguration()
    .ConfigureBasic()
    .ConfigureMinLoggingLevel()
    .ConfigureWriteToDefaultFile()
    .ConfigureWriteToConsole()
    .CreateLogger();
Log.Logger.Information("Starting");

try
{
    var builder = WebApplication.CreateBuilder(args);

    //builder.Services.AddSerilog(); // causes double outputs to console
    builder.Host.UseSerilog();

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddDbContext<ShipCrewsContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("ShipCrewsContext")));

    var app = builder.Build();

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
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    //Log.Logger.Information("Finished");   //  too late for any logging
    Log.CloseAndFlush();
}

