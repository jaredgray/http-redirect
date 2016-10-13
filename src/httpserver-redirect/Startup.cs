using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace httpserver
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.Run(async (context) =>
            {
                var host = context.Request.Host.Host;
                var fullpath = context.Request.Scheme + "://" + context.Request.Host.Host + context.Request.Path.Value + context.Request.QueryString.Value;
                if(host == "pdxcodebits.com" && !string.IsNullOrEmpty(context.Request.Path.Value.Replace("/", "")))
                {
                    context.Response.Redirect(fullpath.Replace("pdxcodebits.com", "127.0.0.1:1111"));
                }
                await context.Response.WriteAsync(
                    "Hello World! The Time is: " +
                    DateTime.Now.ToString("hh:mm:ss tt") + " " + fullpath);

            });
        }
    }
}
