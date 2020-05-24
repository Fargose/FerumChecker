using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FerumChecker.Repository.EF;
using FerumChecker.Repository.Interfaces;
using FerumChecker.Repository.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using FerumChecker.Service.Interfaces;
using FerumChecker.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using FerumChecker.DataAccess.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using FerumChecker.Repository.Repositories.UnitOfWork;
using FerumChecker.Service.Interfaces.Infrastructure;
using FerumChecker.Service.Interfaces.User;
using FerumChecker.Service.Services.Infrastructure;
using FerumChecker.Service.Services.user;
using FerumChecker.Service.Services.Hardware;
using FerumChecker.Service.Interfaces.Hardware;

namespace FerumChecker.Web
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
            services.AddControllersWithViews();


            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/NotPermission");
            });

            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<IUnitOfWork, EFUnitOfWork>();
            services.AddTransient<IUserService, UserService>();
            //Hardware services
            services.AddTransient<ICPUService, CPUService>();
            services.AddTransient<IHDDService, HDDService>();
            services.AddTransient<ISSDService, SSDService>();
            services.AddTransient<IMotherBoardService, MotherBoardService>();
            services.AddTransient<IVideoCardService, VideoCardService>();
            services.AddTransient<IPCCaseService, PCCaseService>();
            services.AddTransient<IRAMService, RAMService>();
            services.AddTransient<IPowerSupplyService, PowerSupplyService>();
            //Specification services
            services.AddTransient<ICPUSocketService, CPUSocketService>();
            services.AddTransient<IGPUService, GPUService>();
            services.AddTransient<IGraphicMemoryTypeService, GraphicMemoryTypeService>();
            services.AddTransient<IMotherBoardFormFactorService, MotherBoardFormFactorService>();
            services.AddTransient<IMotherBoardNorthBridgeService, MotherBoardNorthBridgeService>();
            services.AddTransient<IOuterMemoryFormFactorService, OuterMemoryFormFactorService>();
            services.AddTransient<IOuterMemoryInterfaceService, OuterMemoryInterfaceService>();
            services.AddTransient<IPowerSupplyCPUInterfaceService, PowerSupplyCPUInterfaceService>();
            services.AddTransient<IPowerSupplyMotherBoardInterfaceService, PowerSupplyMotherBoardInterfaceService>();
            services.AddTransient<IRAMTypeService, RAMTypeService>();
            services.AddTransient<IRequirementTypeService, RequirementTypeService>();
            services.AddTransient<IVideoCardInterfaceService, VideoCardInterfaceService>();
            //Infrasrtucture Services
            services.AddTransient<IComputerAssemblyService, ComputerAssemblyService>();
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IManufacturerService, ManufacturerService>();
            services.AddTransient<IPublisherService, PublisherService>();
            services.AddTransient<IDeveloperService, DeveloperService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
