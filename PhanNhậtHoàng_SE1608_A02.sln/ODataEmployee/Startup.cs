using BusinessObject;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OData.UriParser;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODataEmployee
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

   
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ODataEmployee", Version = "v1" });

            });

            var modelBuilder = new ODataConventionModelBuilder();

            modelBuilder.EntitySet<CompanyProject>("CompanyProjects");
            modelBuilder.EntitySet<Departmennt>("Departments");
            modelBuilder.EntitySet<Employee>("Employees");

            var participatingProjects = modelBuilder.EntitySet<ParticipatingProject>("ParticipatingProjects");

            participatingProjects.EntityType.HasKey(pp => new { pp.EmployeeID, pp.CompanyProjectID });

            modelBuilder.EntityType<Employee>().HasMany(e => e.ParticipatingProjects);
            modelBuilder.EntityType<CompanyProject>().HasMany(cp => cp.ParticipatingProjects);

            services.AddControllers().AddOData(options =>
            {
                options.Select().Filter().OrderBy().Expand().Count().SetMaxTop(null).AddRouteComponents(
                    "odata",
                    modelBuilder.GetEdmModel());


            });
          


        }

      
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ODataEmployee v1"));
            }
            app.UseODataBatching();
            app.UseRouting();

            app.UseAuthorization();
      
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

            });

  
        }

     
    }
}
