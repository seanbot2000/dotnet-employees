using employee.Data;
using employee.Models;
using employee.Repositories;
using employee.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Steeltoe.CloudFoundry.Connector.MySql;
using Steeltoe.CloudFoundry.Connector.MySql.EFCore;
using Swashbuckle.AspNetCore.Swagger;
using Steeltoe.Management.CloudFoundry;
using Steeltoe.Management.Endpoint.Refresh;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace employee
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddMySqlConnection(Configuration);
            services.AddTransient<EmployeeService>();
            services.AddTransient<EmployeeRepository>();
            services.AddTransient<EmployeeDbSeed>();
            services.AddDbContext<EmployeeDbContext>(o => o.UseMySql(Configuration));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

            services.AddCloudFoundryActuators(Configuration);
            services.AddRefreshActuator(Configuration);
            services.AddOptions();
            services.Configure<SampleConfig>(Configuration.GetSection("sample"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, EmployeeDbSeed employeeDbSeed)
        {
            loggerFactory.AddConsole();
            var logger = loggerFactory.CreateLogger<ConsoleLogger>();
            logger.LogInformation("Executing Configure()");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseCloudFoundryActuators();
            app.UseRefreshActuator();
            EmployeeDbInitialize.init(app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider);
            employeeDbSeed.SeedEmployees().Wait();
        }
    }
}