using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserSysCore;
using UserSysCore.Service;
using UserSysCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using MySql.Data.EntityFrameworkCore.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace UserWeb
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
            services.AddDbContext<UserContext>(options=>options.UseMySQL(Configuration.GetConnectionString("DefaultConnection")));
            services.AddAuthentication(IdentityService.AuthenticationScheme).AddCookie(IdentityService.AuthenticationScheme,options=> {
                options.Cookie.Name = "UserIdentity";
            });
            services.AddAuthorization(options=> {
                options.AddPolicy("Permission", policy => { policy.Requirements.Add(new PermissionRequirement());  });
            });
            services.AddMvc();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUserInfoService, UserInfoService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IRoleInfoService, RoleInfoService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IdentityService>();
            services.AddScoped<IApplicationContext, ApplicationContext>();
            services.AddScoped<IAuthorizationHandler, PermissionHandler>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseAuthentication();
            
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
