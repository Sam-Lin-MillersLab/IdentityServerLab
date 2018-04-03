using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                //options.Filters.Add();
            });

        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();

            app.Use(async (ctx, next) =>
            {
                Console.WriteLine("Request path is: " + ctx.Request.Path);

                //ctx.Request.Path = "/hello";

                await next();

                Console.WriteLine("Response status: " + ctx.Response.StatusCode);
            });

            app.Map("/goodbye", g =>
            {
                g.Use(async (ctx, next) =>
                {
                    await ctx.Response.WriteAsync("<h1>this is goodbye!</h1>");
                });
            });

            app.Use(async (ctx, next) =>
            {
                Console.WriteLine("we're in our hello MW");

                if (ctx.Request.Path == "/hello")
                {
                    ctx.Response.StatusCode = 201;
                    await ctx.Response.WriteAsync("<h1>Hello back!</h1>");
                }
                else
                {
                    await next();
                }

            });

            app.UseMvcWithDefaultRoute();

            
        }
    }
}
