using HomebankingMindHub.Repositories;
using HomebankingMindHub.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using HomeBankingMindHub.Repositories;
using HomeBankingMindHub.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace HomeBankingMindHub
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();

            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

            services.AddDbContext<HomeBankingContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("HomeBankingConexion")));

            services.AddScoped<IClientRepository, ClientRepository>();

            services.AddScoped<IAccountRepository, AccountRepository>();
            
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                options.LoginPath = new PathString("/index.html");
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ClientOnly", policy => policy.RequireClaim("Client"));
            });

        }

       
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=games}/{ action = Get}");

            });
        }


    }
}
