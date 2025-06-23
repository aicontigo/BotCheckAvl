using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

// Local types (in project namespace-less files)

class Program
{
    public static async Task Main(string[] args)
    {
        using IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                services.Configure<TgBotSettings>(context.Configuration.GetSection("TgBot"));
                services.AddDbContext<BotDbContext>(options =>
                    options.UseSqlite(context.Configuration.GetConnectionString("Default")));
                services.AddTransient<MonitoringService>();
                services.AddHostedService<TgBotService>();
            })
            .Build();

        var config = host.Services.GetRequiredService<IConfiguration>();
        var logger = host.Services.GetRequiredService<ILogger<Program>>();
        var env = host.Services.GetRequiredService<IHostEnvironment>();

        logger.LogInformation($"Starting {Assembly.GetExecutingAssembly().GetName().Name} in {env.EnvironmentName} environment");

        await host.RunAsync();
    }
}
