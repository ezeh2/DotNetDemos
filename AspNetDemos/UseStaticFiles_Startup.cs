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
    public class UseStaticFiles_Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDirectoryBrowser();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {            
            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/static-files?view=aspnetcore-6.0

            // ### begin static files 
            // files come wwwroot
            // http://localhost:5000/              --> Index of /
            // http://localhost:5000/test2.txt     --> wwwroot/test2.txt
            // http://localhost:5000/abc/test.txt  --> wwwroot/abc/test.txt
            // http://localhost:5000/abc/          --> wwwroot/abc/test.txt
            var options = new DefaultFilesOptions();
            options.DefaultFileNames.Clear();
            options.DefaultFileNames.Add("test.txt");
            app.UseDefaultFiles(options);
            app.UseDirectoryBrowser();

            app.UseStaticFiles();
            // ### end static files            
        }
    }
}
