using Microsoft.AspNetCore.Identity;

namespace BankEmprestimoConsignado.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string TipoAcesso { get; set;}
    }
}