using DomainLayer.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using RepositoryLayer;
using RepositoryLayer.Implement;
using System;

namespace Sum2BigNumNetCoreReactRedux
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
            //register your DatabaseContext
            services.AddDbContext<DatabaseContext>(options =>
            options.UseSqlServer(
                Configuration.GetConnectionString("DatebaseContext"),
                assembly => assembly.MigrationsAssembly(typeof(DatabaseContext).Assembly.FullName)
            ));

            services.AddMvc()
                .AddJsonOptions(options =>
                options.SerializerSettings.ContractResolver
                = new DefaultContractResolver())
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            //Generic Repository
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICalculationRepository, CalculationRepository>();
            services.AddScoped<IAddition, Addition>();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //Database Migration
            UpdateDatabase(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }

        /// <summary>
        /// Automatically migrate your Entity Framework Core managed database on application start
        /// </summary>
        /// <param name="app"></param>
        private static void UpdateDatabase(IApplicationBuilder app)
        {
            var logger = app.ApplicationServices.GetRequiredService<ILogger<Program>>();
            try
            {
                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    using (var context = serviceScope.ServiceProvider.GetService<DatabaseContext>())
                    {
                        if (!context.Database.EnsureCreated())
                        {
                            context.Database.Migrate();

                            logger.LogInformation("Database migrated successfully.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.GetBaseException().Message + "An error occured to migrate database.");
            }
        }
    }
}
