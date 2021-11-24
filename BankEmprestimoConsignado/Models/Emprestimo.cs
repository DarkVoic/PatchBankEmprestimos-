using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BankEmprestimoConsignado.Models
{
    public partial class Emprestimo
    {
        public Emprestimo(int id, double valorProposta, int qtdParcelas, double taxaJuros, int tipoEmprestimo, DateTime dataVencimento)
        {
            IdEmprestimo = id;
            ValorEmprestimo = valorProposta;
            QtdParcela = qtdParcelas;
            TaxaJuros = taxaJuros;
            TipoEmprest = tipoEmprestimo;
            DataVenc = dataVencimento;
        }

        public Emprestimo()
        {

        }

        public int IdEmprestimo { get; set; }
        [DisplayName("Cliente")]
 
        public int IdCliente { get; set; }
        [DisplayName("Valor Liberado")]
        [Required(ErrorMessage = "O campo descrição é obrigatório")]
        public double ValorLiberado { get; set; }
        [DisplayName("Valor Pedido")]
        public double ValorEmprestimo { get; set; }
        [DisplayName("Vencimento")]
        [Required(ErrorMessage = "O campo descrição é obrigatório")]
        public DateTime DataVenc { get; set; }
        [DisplayName("Valor da Parcela")]
        [Required(ErrorMessage = "O campo descrição é obrigatório")]
        public double ValorParcela { get; set; }
        [DisplayName("Qtd. Parcelas")]
        [Required(ErrorMessage = "O campo descrição é obrigatório")]
        public int QtdParcela { get; set; }
        [DisplayName("Taxa Juros")]
        [Required(ErrorMessage = "O campo descrição é obrigatório")]
        public double TaxaJuros { get; set; }
        [DisplayName("Parcelas Restantes")]
        public int QtdParcelaRest { get; set; }
        [DisplayName("Status")]
        public int StatusEmprest { get; set; }
        [DisplayName("Tipo")]
        public int TipoEmprest { get; set; }

        [DisplayName("Cliente")]
        public virtual Cliente IdClienteNavigation { get; set; }
    }
}
