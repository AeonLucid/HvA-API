using System;
using HvA.API.NetStandard1;
using NLog.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HvA_API
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            var username = Configuration["HvA:Username"];
            var password = Configuration["HvA:Password"];

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                throw new Exception("Please fill in a HvA:Usernamd and HvA:Password in appsettings.json.");

            var hva = new HvAClient(username, password);

            if (!hva.SignInAsync().Result)
                throw new Exception("Wrong HvA credentials specified, couldn't authenticate.");

            services.AddMemoryCache();
            services.AddSingleton(hva);
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddNLog();

            app.UseMvc();
        }
    }
}
