using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transporter.Models.Pages;
using Transporter.Services;
using Transporter.Services.HSL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Transporter
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IHslConnector, HslConnector>();
            services.AddTransient<IHslRouteSolver, HslRouteSolver>();
            services.AddTransient<ILayoutFactory, LayoutFactory>();

            services.AddMvc();

            services.AddMemoryCache();

            services.AddOptions();
            services.Configure<HslSettings>(Configuration);
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseStaticFiles();

            app.UseMvc(routes =>
           {
               routes.MapRoute(
                   name: "default",
                   template: "{controller=Home}/{action=Index}/{id?}");
           });


            //Domain specific initialization
            AddCoordinates();
            AddRouteOrigins();
        }

        protected void AddCoordinates()
        {
            HslCoordinateBank.AddCoordinatesFor(LocationEnum.Start, "2545370,6679959");
            HslCoordinateBank.AddCoordinatesFor(LocationEnum.School, "2547800,6679751");
            HslCoordinateBank.AddCoordinatesFor(LocationEnum.Solita, "2552077,6673617");
        }

        protected void AddRouteOrigins()
        {
            LocationBank.Add(LocationEnum.Start, "Vallikallio, Espoo");
            LocationBank.Add(LocationEnum.School, "Pitäjänmäki");
            LocationBank.Add(LocationEnum.Solita, "Solita");
        }
    }
}
