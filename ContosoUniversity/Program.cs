using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http;

namespace ContosoUniversity
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
            
            
            //        using (var host = WebHost.StartWith("http://localhost:8080", app =>
            //app.Use(next =>
            //{
            //    return async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    };
            //})))
            //        {
            //            Console.WriteLine("Use Ctrl-C to shut down the host...");
            //            host.WaitForShutdown();
            //        }



            CreateHostBuilder(args).Build().Run();
            var host = CreateHostBuilder(args).Build();
             
            CreateDbIfNotExists(host);

            host.Run();
        }
        private static void CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<SchoolContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
           
              

            Host.CreateDefaultBuilder(args)
            
                .ConfigureWebHostDefaults(webBuilder =>
                {
                     
                    webBuilder.UseStartup<Startup>();
                });
    }
}
