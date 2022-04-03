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
    public class UseSpaStaticFiles_UseSpa_Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {            
            // https://stackoverflow.com/questions/55988045/what-is-the-difference-between-usestaticfiles-usespastaticfiles-and-usespa-in

            // http://localhost:5000/test.txt         --> ClientApp/dist/test.txt
            // http://localhost:5000/index.html       --> ClientApp/dist/index.html
            // http://localhost:5000/assets/test.txt  --> ClientApp/dist/assets/test.txt
            app.UseSpaStaticFiles();

            // and everythng else:
            // http://localhost:5000/blabla           --> ClientApp/dist/index.html
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
        }
    }
}
