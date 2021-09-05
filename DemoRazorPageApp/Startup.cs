using DemoRazorPageApp.Interfaces.ICommon;
using DemoRazorPageApp.Interfaces.IRepos.IVehicle;
using DemoRazorPageApp.Interfaces.IServices.IVehicle;
using DemoRazorPageApp.Repos.Vehicle;
using DemoRazorPageApp.Services.Common;
using DemoRazorPageApp.Services.Vehicle;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DemoRazorPageApp
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
            //Registering DB, Services & Repositories

            //RegisterDatabase(services); // Not in use for now
            RegisterService(services); 
            RegisterRepository(services);

            //services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            //{
            //    builder.AllowAnyOrigin()
            //           .AllowAnyMethod()
            //           .AllowAnyHeader();
            //}));

           
             services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}

            //app.UseHttpsRedirection();
            //app.UseStaticFiles();

            //app.UseRouting();

            //app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapRazorPages();
            //});

            app.UseDeveloperExceptionPage();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
       
        #region Helpers

        private void RegisterService(IServiceCollection services)
        {
            services.AddSingleton<IAppSettings>(Configuration.Get<AppSettings>());
            services.AddScoped<IVehicleService, VehicleService>();

        }

        private void RegisterRepository(IServiceCollection services)
        {
          
            services.AddScoped<IVehicleRepo, VehicleRepo>();
        }

        //private void RegisterDatabase(IServiceCollection services)
        //{
        //    services.AddDbContext<LuboSoftContext>(options =>
        //            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        //}

        #endregion
    }
}
