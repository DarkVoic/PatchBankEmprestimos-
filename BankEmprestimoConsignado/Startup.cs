using BankEmprestimoConsignado.Configurations;
using BankEmprestimoConsignado.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankEmprestimoConsignado
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
            // Adicionando o GetConnection do meu MySql 
            string mySqlConnection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContextPool<BankContext>(options =>
                        options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));

            //Autenticação Facebook e Google
            services.AddAuthentication()
                .AddGoogle(googleOptions => {
                    googleOptions.ClientId = "582843770413-enaa2ma1jr8r1phmmc454514qdcdbk41.apps.googleusercontent.com";
                    googleOptions.ClientSecret = "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";
                })
                .AddFacebook(facebookOptions => {
                    facebookOptions.AppId = "1310991852665960";
                    facebookOptions.AppSecret = "e4c06a4dc67c0b80934d5e6627650229";
                });

            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<BankContext>();
         
            services.AddAuthorization(options => options.AddPolicy("Empregados", policy => policy.RequireRole("Gerente")));
            services.AddAuthorization(options => options.AddPolicy("cliente", policy => policy.RequireClaim("cliente")));
            //services.AddIdentityConfiguration(Configuration);
            services.AddRazorPages();
            services.AddControllersWithViews();
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
                endpoints.MapRazorPages();
            });
        }
    }
}
