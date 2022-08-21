using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Net.WebSockets;

namespace ContosoUniversity
{
    public class ConventionalMiddleware
    {
        private readonly RequestDelegate _next;

        public ConventionalMiddleware(RequestDelegate next)
            => _next = next;

        public async Task InvokeAsync(HttpContext context, SchoolContext dbContext)
        {
            var keyValue = context.Request.Query["key"];

            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                 

                await dbContext.SaveChangesAsync();
            }

            await _next(context);
        }
    }
    public class FactoryActivatedMiddleware : IMiddleware
    {
        private readonly SchoolContext _dbContext;

        public FactoryActivatedMiddleware(SchoolContext dbContext)
            => _dbContext = dbContext;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var keyValue = context.Request.Query["key"];

            if (!string.IsNullOrWhiteSpace(keyValue))
            {
              

                await _dbContext.SaveChangesAsync();
            }

            await next(context);
        }
    }
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseConventionalMiddleware(
            this IApplicationBuilder app)
            => app.UseMiddleware<ConventionalMiddleware>();

        public static IApplicationBuilder UseFactoryActivatedMiddleware(
            this IApplicationBuilder app)
            => app.UseMiddleware<FactoryActivatedMiddleware>();
    }
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
          
            services.AddTransient<FactoryActivatedMiddleware>();
            services.AddDbContext<SchoolContext>(options =>
                  options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddControllersWithViews();
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


          
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseConventionalMiddleware();
            app.UseFactoryActivatedMiddleware();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
