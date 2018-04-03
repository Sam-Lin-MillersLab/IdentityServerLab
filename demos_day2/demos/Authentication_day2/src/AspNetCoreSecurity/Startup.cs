// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreSecurity
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddIdentityServer()
                //.AddDeveloperSigningCredential();
                //.AddSigningCredential()
                .AddInMemoryClients(Config.Clients)
                .AddInMemoryIdentityResources(Config.IdentityResources)
                .AddInMemoryApiResources(Config.ApiResources)
                .AddTestUsers(TestUsers.Users)
                .AddSigningCredential("CN=sts");

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultScheme = "Cookies";
            //    //options.DefaultAuthenticateScheme = "Cookies";
            //    //options.DefaultChallengeScheme = "Cookies";
            //})

            services.AddAuthentication("Cookies")
                .AddCookie("Cookies", options =>
                {
                    options.Cookie.Name = "fred";
                    //options.Cookie.Domain;
                    options.SlidingExpiration = true;
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(10);

                    options.LoginPath = "/Account/Login";
                    options.AccessDeniedPath = "/Account/AccessDenied";
                })
                .AddCookie("Temp")
                .AddGoogle("Google", options =>
                {
                    // ~/signin-google
                    //options.CallbackPath = "/signin-fred"
                    options.SignInScheme = "Temp";
                    options.ClientId = "538301208639-l6c69j6b25te93v8p7tts2cira3e8hr2.apps.googleusercontent.com";
                    options.ClientSecret = "853xTcC_IwYlHwUmfKEfNcPx";
                });

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("ManageCustomers", policy =>
            //    {
            //        policy.RequireAuthenticatedUser();
            //        policy.RequireClaim("role", "Geek");
            //        //policy.RequireClaim("status", "senior");
            //    });
            //});
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseIdentityServer();
            //app.UseAuthentication();

            //app.Use(async (ctx, next) =>
            //{
            //    if (ctx.User.Identity.IsAuthenticated)
            //    {
            //        ((ClaimsIdentity)ctx.User.Identity).AddClaim(
            //            new Claim("now", DateTime.Now.ToString()));
            //    }

            //    await next();
            //});

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
