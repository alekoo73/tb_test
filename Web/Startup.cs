using System.Linq;
using Core.Interfaces;
using Core.Services;
using Infrastructure.Data.Data;
using Infrastructure.Logging;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Web.Middleware;
using AutoMapper;
using Web.Classes;
using Web.Utilities;
using System.IO;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http;

namespace Web
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
            services.AddCors(allowsites =>
            {
                allowsites.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
            });
            services.AddLocalization(options =>
            {
                options.ResourcesPath = "Resources";
            }
        );
            services.AddSingleton<CultureLocalizer>();
            services.AddControllers();
            services.Configure<ApiBehaviorOptions>(apiBehaviorOptions => {
                apiBehaviorOptions.InvalidModelStateResponseFactory = actionContext => {                    
                    return new BadRequestObjectResult(actionContext.ModelState); 
                };
            });
            services.AddDbContext<Testapp2Context>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Connection")));
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IPersonService, PersonService>();
            services.AddTransient<IRelationService, RelationService>();
            services.AddTransient<IPersonRepository, PersonRepository>();
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            services.AddScoped<ValidationFilterAttribute>();
            services.AddAutoMapper(typeof(Startup).Assembly, typeof(Infrastructure.Data.Person).Assembly);




        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,IAppLogger<Program> logger)
        {
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Pictures")),
                RequestPath = new PathString("/Pictures")
            });


            var options = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(Statics.SupportedCultures.First().Name),
                SupportedCultures = Statics.SupportedCultures,
                SupportedUICultures = Statics.SupportedCultures
            };

       
            options.RequestCultureProviders.Clear();
            options.RequestCultureProviders.Add(new AcceptLanguageHeaderRequestCultureProvider());

            app.UseRequestLocalization(options);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(options => options.AllowAnyOrigin());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.ConfigureExceptionHandler<Program>(logger);
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }

    
    }
}
