using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.ApplicationInsights;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApp_Azure
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // CreateHostBuilder(args).Build().Run();
            // CreateHostBuilder_WithAppInsightsLogging(args).Build().Run();
            // CreateHostBuilder_WithAppInsightsLogging_UsingEnvironmentVariable(args).Build().Run();
            CreateHostBuilder_WithAppInsightsLogging_UsingAppSettings(args).Build().Run();
        }

        /// <summary>
        /// Creates a HostBuilder with default configuration settings
        /// </summary>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });



        /// <summary>
        /// Creates a HostBuilder with Azure Application Insights logging
        /// </summary>
        public static IHostBuilder CreateHostBuilder_WithAppInsightsLogging(string[] args) =>
            Host.CreateDefaultBuilder(args)

              .ConfigureLogging(
                    builder =>
                    {

                        // adds the Azure Application Insights Logging Provider
                        builder.AddApplicationInsights("<MY-OWN-APPLICATION-INSIGHTS-INSTRUMENTATION-KEY>");

                        // adds optional filters to configure LogLevel for specific categories
                        builder.AddFilter<ApplicationInsightsLoggerProvider>("Microsoft", LogLevel.Debug);
                        builder.AddFilter<ApplicationInsightsLoggerProvider>("Microsoft.Hosting.Lifetime", LogLevel.Information);
                    }
                )

                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });



        /// <summary>
        /// Creates a HostBuilder with Azure Application Insights logging (using Environment Variable)
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder_WithAppInsightsLogging_UsingEnvironmentVariable(string[] args) =>
            Host.CreateDefaultBuilder(args)

              .ConfigureLogging(
                    builder =>
                    {

                        // adds the Azure Application Insights Logging Provider
                        builder.AddApplicationInsights(Environment.GetEnvironmentVariable("APPINSIGHTS_INSTRUMENTATIONKEY"));
                    }
                )

                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });



        /// <summary>
        /// Creates a HostBuilder with Azure Application Insights logging (using appsettings)
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder_WithAppInsightsLogging_UsingAppSettings(string[] args)
        {
            return Host.CreateDefaultBuilder(args)

                .ConfigureLogging((hostingContext, builder) =>
                    {
                        // adds the Azure Application Insights Logging Provider
                        builder.AddApplicationInsights(hostingContext.Configuration["ApplicationInsights:InstrumentationKey"]);
                    }
                )
                
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        }
    }
}
