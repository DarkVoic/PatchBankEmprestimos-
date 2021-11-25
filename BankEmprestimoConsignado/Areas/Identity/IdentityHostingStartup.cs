using System;
using BankEmprestimoConsignado.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

[assembly: HostingStartup(typeof(BankEmprestimoConsignado.Areas.Identity.IdentityHostingStartup))]
namespace BankEmprestimoConsignado.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {

            builder.ConfigureServices((context, services) =>
            {
                //string mySqlConnection = context.Configuration.GetConnectionString("DefaultConnection");
                //services.AddDbContextPool<BankContext2>(options =>
                //            options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));
                
                //services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                //    .AddRoles<ApplicationRole>()
                //    .AddEntityFrameworkStores<BankContext2>();
            });
        }
    }
}