using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BankEmprestimoConsignado.Areas.Identity;
using BankEmprestimoConsignado.Data;
using BankEmprestimoConsignado.Areas.Identity.Data;

namespace BankEmprestimoConsignado.Data
{
    public class BankContext2 : IdentityDbContext<ApplicationUser, ApplicationRole, string, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>

    {
        public BankContext2(DbContextOptions<BankContext2> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<IdentityUserLogin<string>>()
            //    .HasNoKey();
            //builder.Entity<IdentityUserRole<string>>()
            //    .HasNoKey();
            //builder.Entity<IdentityUserClaim<string>>()
            //    .HasNoKey();
            //builder.Entity<IdentityUserToken<string>>()
            //    .HasNoKey();
            //builder.Entity<IdentityUser<string>>()
            //    .HasNoKey();
            //builder.HasCharSet("utf8mb4")
            //    .UseCollation("utf8mb4_0900_ai_ci");
            base.OnModelCreating(builder);
        }
    }
}
