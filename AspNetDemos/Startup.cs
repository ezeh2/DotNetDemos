using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AspNetDemos
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDirectoryBrowser();

            /*
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });            
            */
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {            
            // ### begin static files 
            //  files come wwwroot
            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/static-files?view=aspnetcore-6.0
            var options = new DefaultFilesOptions();
            options.DefaultFileNames.Clear();
            options.DefaultFileNames.Add("test.txt");
            app.UseDefaultFiles(options);
            app.UseDirectoryBrowser();

            app.UseStaticFiles();
            // ### end static files            

            /*
            app.UseSpaStaticFiles();
            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    // spa.UseAngularCliServer(npmScript: "start");
                }
            });           
            */ 

            /*
            StaticFileOptions options2 = new StaticFileOptions();
            app.UseSpaStaticFiles(options2);
            */
        }

        public void Configure1(IApplicationBuilder app, IWebHostEnvironment env)
        {
            string en = env.EnvironmentName;

            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync($"Getting Response from First Middleware {en}");
                // https://docs.microsoft.com/en-us/dotnet/core/compatibility/aspnet-core/6.0/middleware-new-use-overload
                await next(context);
            });
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Getting Response from Second Middleware");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure0(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
