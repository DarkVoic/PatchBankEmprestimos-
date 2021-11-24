using System;
using System.Collections.Generic;

#nullable disable

namespace BankEmprestimoConsignado.Models
{
    public partial class Usuario
    {
        public int IdUsuarios { get; set; }
        public int IdCliente { get; set; }
        public string User { get; set; }
        public byte[] Senha { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }

        public virtual Cliente IdClienteNavigation { get; set; }
    }
}
