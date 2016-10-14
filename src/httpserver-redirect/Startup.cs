using httpserver.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace httpserver
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<ConfigurationOptions>(Configuration.GetSection("ConfigurationOptions"));
        }
        public IConfigurationRoot Configuration { get; }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IOptions<ConfigurationOptions> options)
        {
            app.Run(async (context) =>
            {
                var host = context.Request.Host.Host;
                var fullpath = context.Request.Scheme + "://" + context.Request.Host.Host + context.Request.Path.Value + context.Request.QueryString.Value;
                
                try
                {
                    foreach (var mapping in options.Value.Mappings)
                    {
                        if (host == mapping.From && !string.IsNullOrEmpty(context.Request.Path.Value.Replace("/", "")))
                        {
                            context.Response.Redirect(fullpath.Replace(mapping.From, mapping.To));
                        }
                    }
                }
                catch (Exception ex)
                {
                    await context.Response.WriteAsync(ex.ToString());
                    return;
                }

                await context.Response.WriteAsync(
                    "Hello World! The Time is: " +
                    DateTime.Now.ToString("hh:mm:ss tt") + " " + fullpath);

            });
        }
    }
}
