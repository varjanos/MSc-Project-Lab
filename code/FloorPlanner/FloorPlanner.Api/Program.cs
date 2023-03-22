using Core.Translation.Service;
using Serilog;

namespace FloorPlanner.Api;

public static class Program
{
    public static void Main(string[] args)
    {
        ConfigurationSetup();

        if (args.Length > 0)
        {
            if (args[0].Equals("/SeedTranslations", StringComparison.OrdinalIgnoreCase))
            {
                var forceUpdate = args.Any(a => a.ToLower().Contains("--force"));
                var host = CreateHostBuilder(args).Build();
                using var scope = host.Services.CreateScope();
                var localizationService = scope.ServiceProvider.GetRequiredService<ITranslationService>();
                localizationService.SeedTranslationsAsync(true, forceUpdate).GetAwaiter().GetResult();
                return;
            }
        }

        try
        {
            Log.Information("Starting web host.");
            CreateHostBuilder(args).Build().Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application start-up failed.");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
       Host.CreateDefaultBuilder(args)
           .UseSerilog()
           .ConfigureWebHostDefaults(webBuilder =>
           {
               webBuilder.UseStartup<Startup>();
           });

    private static void ConfigurationSetup()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
            .Build();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
    }
}