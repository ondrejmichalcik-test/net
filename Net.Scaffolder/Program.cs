using System.IO;
using System.Reflection;
using CodeNow.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Net.Scaffolder
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var configArg = args.Length > 0 ? args[0] : null;
                    
                    config.SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

                    if (configArg != null)
                    {
                        config.AddJsonFile(configArg, optional: false, reloadOnChange: true);
                    }
                    else
                    {
                        config
                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                            .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", 
                                optional: true, reloadOnChange: true);
                    }

                    config.AddEnvironmentVariables();
                    config.AddCommandLine(args);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseStartup<Startup>()
                        .ConfigureLogging(CodeNowLoggerExtensions.ConfigureCodeNowLogging);
                });
    }
}
