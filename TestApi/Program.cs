using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using TestApi.Utils.Settings;

namespace TestApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();

            ApplicationParams.Host = configuration?.GetSection("ApplicationParams").GetSection("Host").Value;
            ApplicationParams.Port = configuration?.GetSection("ApplicationParams").GetSection("Port").Value;
            ApplicationParams.HostSsl = configuration?.GetSection("ApplicationParams").GetSection("HostSsl").Value;
            ApplicationParams.PortSsl = configuration?.GetSection("ApplicationParams").GetSection("PortSsl").Value;

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .WriteTo.Console(
                    outputTemplate:
                    "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}",
                    theme: AnsiConsoleTheme.Code)
                .CreateLogger();
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //webBuilder.UseKestrel();
                    webBuilder.UseIISIntegration();
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls($"{ApplicationParams.Host}:{ApplicationParams.Port}",
                        $"{ApplicationParams.HostSsl}:{ApplicationParams.PortSsl}");
                });
    }
}