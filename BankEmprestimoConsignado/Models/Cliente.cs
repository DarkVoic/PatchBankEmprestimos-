using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BankEmprestimoConsignado.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Emprestimos = new HashSet<Emprestimo>();
            Usuarios = new HashSet<Usuario>();
        }

        public Cliente(int idCliente, string nome, double cpf, DateTime nasc, string profissao, double margem, double margemCartao, double salario, int status)
        {
            IdCliente = idCliente;
            Nome = nome;
            Cpf = cpf;
            Nascimento = nasc;
            Profissao = profissao;
            Margem = margem;
            MargemCartao = margemCartao;
            Salario = salario;
            Status = status;
        }

        [DisplayName("Cliente")]
        public int IdCliente { get; set; }
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo CPF é obrigatório")]
        public double Cpf { get; set; }
        [Required(ErrorMessage = "O campo Data é obrigatório")]
        public DateTime Nascimento { get; set; }

        [DisplayName("Profissão")]
        [Required(ErrorMessage = "O campo Profissão é obrigatório")]
        public string Profissao { get; set; }
        [Required(ErrorMessage = "O campo Margem é obrigatório")]
        public double Margem { get; set; }
        [DisplayName("Margem Cartão")]
        [Required(ErrorMessage = "O campo Margem do cartão é obrigatório")]
        public double MargemCartao { get; set; }
        [DisplayName("Salário")]
        [Required(ErrorMessage = "O campo Salário é obrigatório")]
        public double Salario { get; set; }
        public int Status { get; set; }

        public virtual IEnumerable<Emprestimo> Emprestimos { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
