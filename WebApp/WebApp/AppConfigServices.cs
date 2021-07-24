using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace WebApp
{
    public class AppConfigServices
    {
        public static IConfiguration Configuration { get; }

        static AppConfigServices()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .Add(new JsonConfigurationSource {Path = "appsettings.json"})
                .Build();
        }
    }
}