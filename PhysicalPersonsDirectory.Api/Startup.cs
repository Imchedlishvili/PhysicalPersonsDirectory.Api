using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using PhysicalPersonsDirectory.Api.Extensions;
using PhysicalPersonsDirectory.Api.Filters;
using PhysicalPersonsDirectory.Domain;
using PhysicalPersonsDirectory.Services.Services.Abstract;
using PhysicalPersonsDirectory.Services.Services.Concrete;
using System.Linq;
using System.Threading.Tasks;

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
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[] { "ka-Ge", "en-US" };
                options.AddSupportedCultures(supportedCultures);
                options.AddSupportedUICultures(supportedCultures);

                options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(context =>
                {
                    var userLangs = context.Request.Headers["Accept-Language"].ToString();
                    var firstLang = userLangs.Split(',').FirstOrDefault();
                    var defaultLang = string.IsNullOrEmpty(firstLang) ? "ka" : firstLang;
                    return Task.FromResult(new ProviderCultureResult(defaultLang, defaultLang));
                }));
            });

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
            app.UseSwagger();           
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Physical Persons Directory Api");
            });

            app.UseRequestLocalization();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandler("/Error"); //Built-In Middleware
            //app.ConfigureCustomExceptionMiddleware(); //CustomExceptionMiddleware

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
