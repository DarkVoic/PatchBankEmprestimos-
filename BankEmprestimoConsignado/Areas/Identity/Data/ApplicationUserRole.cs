using Microsoft.AspNetCore.Identity;

namespace BankEmprestimoConsignado.Data
{
    public class ApplicationUserRole : IdentityUserRole<string>
    {
        public int Id { get; set; }

        public virtual ApplicationRole Role { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}