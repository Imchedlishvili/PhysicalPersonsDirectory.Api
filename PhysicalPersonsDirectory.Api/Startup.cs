using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhysicalPersonsDirectory.Api.CustomExceptionMiddleware;
using PhysicalPersonsDirectory.Api.Filters;
using PhysicalPersonsDirectory.Domain;
using PhysicalPersonsDirectory.Services.Services.Abstract;
using PhysicalPersonsDirectory.Services.Services.Concrete;
using System.Linq;

namespace PhysicalPersonsDirectory.Api
{
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
            services.AddControllers();

            services.AddMvc(options => { options.Filters.Add<ValidationFilter>(); })
                    .AddFluentValidation(t => t.RegisterValidatorsFromAssemblyContaining<Startup>())
                    .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddDbContext<PhysicalPersonsContext>(opt => opt.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"], x => x.UseNetTopologySuite()), ServiceLifetime.Transient);

            services.AddSwaggerGen();

            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<ILoggerService, LoggerService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Physical Persons Directory Api");
            });


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseExceptionHandler("/Error"); //Built-In Middleware
            app.UseMiddleware<ExceptionMiddleware>(); //CustomExceptionMiddleware

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
