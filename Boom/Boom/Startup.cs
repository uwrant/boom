﻿using System;
using Microsoft.Framework.DependencyInjection;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Routing;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.SqlServer;

namespace Boom
{
    public class Startup
    {
        public void Configure(IBuilder app)
        {
            var configuration = new Configuration();
            configuration.AddJsonFile("config.json");
            configuration.AddEnvironmentVariables();

            app.UseStaticFiles();

            app.UseServices(services =>
            {
                services.AddMvc();

                var runningOnMono = Type.GetType("Mono.Runtime") != null;
                if (runningOnMono)
                {
                    services.AddEntityFramework().AddInMemoryStore();
                }
                else
                {
                    // Microsoft.Framework.DependencyInjection.SqlSer
                    services.AddEntityFramework().AddSqlServer();
                }

                services.AddScoped<BoomContext>();

                services.SetupOptions<DbContextOptions>(options =>
                {
                    if (runningOnMono)
                    {
                        options.UseInMemoryStore();
                    }
                    else
                    {
                        options.UseSqlServer(configuration.Get("Data:DefaultConnection:ConnectionString"));
                    }
                }
                );
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "Default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                //routes.MapRoute(
                //    name: "BacklogOptionsRoute",
                //    template: "backlogs/{backlogid}/options/{id?}",
                //    new { controller = "BacklogOptionsController" }),

                routes.MapRoute(
                    name:  "ApiRoute", 
                    template:  "{controller}/{id?}");


            });

            DbHelper.EnsureDbCreated(app);
        }
    }
}