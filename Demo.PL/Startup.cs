using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Context;
using Demo.DAL.Models;
using Demo.PL.Mapping_Profiles;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Demo.PL
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
            services.AddControllersWithViews(); // MVC Services  
            services.AddDbContext<DataContext>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            } );




            // services.AddScoped<IDepartmentRepositories,DepartmentRepositories>();

            ////services.AddSingleton<IDepartmentRepositories,DepartmentRepositories>();
            ////services.AddTransient<IDepartmentRepositories,DepartmentRepositories>();

            //  services.AddScoped<IEmployeeRepository,EmployeeRepositories>();



            services.AddScoped<IUnitOfWork, UnitOfWork>();
                
            services.AddAutoMapper(M => M.AddProfile(new EmployeeProfile()));

            services.AddAutoMapper(M => M.AddProfile(new DepartmentProfile()));

            services.AddIdentity<ApplicationUser, IdentityRole>(option =>
            {
                option.Password.RequireDigit = true;
                option.Password.RequireLowercase = true;
                option.Password.RequireUppercase = true;
                option.Password.RequiredLength = 5;
                option.Password.RequireNonAlphanumeric = true;
            }).AddEntityFrameworkStores<DataContext>()
              .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                 .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, option =>
                 {
                     option.LoginPath = new PathString("/Account/Login");
                     option.AccessDeniedPath = new PathString("/Shared/Error");

                 });
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
                    pattern: "{controller=Account}/{action=Register}/{id?}");
            });
        }
    }
}
