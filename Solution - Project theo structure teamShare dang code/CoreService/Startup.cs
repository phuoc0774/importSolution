using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreService.service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ModelClassLibrary.connection;
using ModelClassLibrary.service;
using ModelClassLibrary.permission.permistionhandle;
using ModelClassLibrary.permission.services;

namespace CoreService
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
            var audienceConfig = Configuration.GetSection("Audience");
            var strSecret = audienceConfig["Secret"];
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(audienceConfig["Secret"]));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = audienceConfig["Iss"],
                ValidateAudience = true,
                ValidAudience = audienceConfig["Aud"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = true,
            };
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = "TestKey";
            })
            .AddJwtBearer("TestKey", x =>
            {
                x.RequireHttpsMetadata = false;
                x.TokenValidationParameters = tokenValidationParameters;
            });
            //connect database
            services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //connect db auth
            services.AddDbContext<PermissionContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("defaultconnection4")));
            //DI: service
            services.AddTransient<IHashPass, HashPassImpl>();

            //DT: (objService) PermissionDB.table
            services.AddTransient<IUser, UserImpl>();
            services.AddTransient<IRole, RoleImpl>();
            //DT: (objService) MainDB.table
            services.AddTransient<ISoTramBTS, SoTramBTSImpl>();
            services.AddTransient<IKhoaSoTramBTS, KhoaSoTramBTSImpl>();



            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //authorization
            List<string> temp = new List<string>() { "viewUser", "abc" };
            services.AddAuthorization(options =>
            {
                foreach (var permission in temp)
                {
                    options.AddPolicy(permission.ToString(),
                        policy => policy.Requirements.Add(new PermissionRequirement(permission.ToString())));
                }
            });
            services.AddScoped<IUserPermission, UserPermissionImpl>();
            services.AddScoped<IAuthorizationHandler, PermissionHandler>();
            services.AddScoped<ITranslate, TranslateImpl>();



            //*default*
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});

            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder
                                     .AllowAnyOrigin()
                                     .AllowAnyMethod()
                                     .AllowAnyHeader()
                                     .AllowCredentials());
            app.UseAuthentication();
            app.UseDeveloperExceptionPage();//filter user
            app.UseMvcWithDefaultRoute();///filter user
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseMvc();



            //*default*
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Error");
            //    app.UseHsts();
            //}

            //app.UseHttpsRedirection();
            //app.UseStaticFiles();
            //app.UseCookiePolicy();

            //app.UseMvc();
        }
    }
}
