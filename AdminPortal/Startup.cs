using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Model.Interface;
using Repository;
using Repository.Interface;
using Service;
using Service.Interface;

namespace AdminPortal
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
            //Setup connection properties and url           
            ConfigureIOC(services);
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true,
                    ReactHotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }

        /// <summary>
        /// Setup IOC containter
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureIOC(IServiceCollection services)
        {
            services.AddScoped<IEmployeeServiceSolr<IEmployee>, EmployeeServiceSolr>();
            services.AddTransient<IEmployeeRepositorySolr<IEmployee>>(repository => new EmployeeRepositorySolr(Configuration.GetValue<string>("AppSettings:SolrUrl")));

            services.AddScoped<IEmpoyeeServiceSql<IEmployee>, EmployeeServiceSql>();
            services.AddTransient<IEmployeeRepositorySql<IEmployee>, EmployeeRepositorySql>(repository => new EmployeeRepositorySql(Configuration.GetValue<string>("AppSettings:SqlConnection")));
        }
    }
}
