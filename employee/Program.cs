using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Steeltoe.Extensions.Configuration;
using Steeltoe.Extensions.Configuration.CloudFoundry;
using Steeltoe.Extensions.Configuration.ConfigServer;
using Steeltoe.Extensions.Logging;

namespace employee
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseCloudFoundryHosting()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .AddConfigServer()
                .UseStartup<Startup>()
                .ConfigureAppConfiguration((builderContext, configBuilder) =>
                {
                    var env = builderContext.HostingEnvironment;
                    configBuilder.SetBasePath(env.ContentRootPath)
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                        .AddEnvironmentVariables()
                        .AddCloudFoundry();
                })
                .ConfigureLogging((builderContext, loggingBuilder) =>
                {
                    loggingBuilder.AddConfiguration(builderContext.Configuration.GetSection("Logging"));
                    loggingBuilder.AddDynamicConsole();
                    loggingBuilder.AddDebug();
                });
                
    }
}