using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using BankEmprestimoConsignado.Models;

namespace BankEmprestimoConsignado.Business {
    public class EmprestimoConsignado {

        /// <summary>
        /// Metodo responsavel por valida se a margem que o cliente possui, para o tipo de emprestimo solicitado, está dentro do permitido e é maior que 0;
        /// </summary>
        /// <param name="margemCliente">Valor da margem disponivel no salario do cliente para calculo</param>
        /// <param name="margemPermitida">Valor da margem que é permitida para cada tipo de emprestimo, sendo 30%(0.30) para emprestimo e 5%(0.05) para cartão.</param>
        /// <returns>Retorna se a margem que o cliente possui é valida ou não valida.</returns>
        public bool ValidarMargemCliente(double margemCliente, double margemPermitida) {
            if (margemCliente > 0 && margemCliente <= margemPermitida) return true;
            else return false;
        }

        /// <summary>
        /// Metodo responsavel por validar se valor pedido pelo cliente é valido.
        /// </summary>
        /// <param name="valorEmprestimo">Valor do emprestimo do cliente que será analisado.</param>
        /// <param name="valorLiberado">O valor maximo calculado que o emprestimo pode ser para a quantidade de parcelas escolhidas.</param>
        /// <returns>Retorna se o valor pedido pelo cliente pode ser liberado.</returns>
        public bool ValidarValorLiberacao(double valorEmprestimo, double valorLiberado)
        {
            if (valorEmprestimo <= valorLiberado) return true; // Valor pedido pelo cliente liberado
            else return false; // Valor pedido pelo clioente rejeitado.
        }

        /// <summary>
        /// Metodo responsavel por trocar o estado do emprestimo para liberacao ao cliente. No momento da aprovação, é gerado a parcela a ser paga e o valor liberado.
        /// </summary>
        /// <param name="cliente">O cliente que possui a lista de emprestimos.</param>
        /// <param name="idEmprestimo">Qual emprestimo do cliente será aprovado.</param>
        /// <returns>Retorna se emprestimo foi aprovado e liberado para o cliente.</returns>
        public bool AprovarEmprestimo(Cliente cliente, int idEmprestimo)
        {
            var emprestimo = cliente.Emprestimos.FirstOrDefault(ec => ec.IdEmprestimo == idEmprestimo);

            if (ValidarCliente(cliente))
            {
                if (GerarValorLiberacao(cliente, emprestimo))
                {
                    if (emprestimo.StatusEmprest == 2)
                    {
                        emprestimo.StatusEmprest = 1;
                        return true;
                    }
                    else return false;
                }
                else return false;
            }
            else return false;
        }

        /// <summary>
        /// Metodo responsavel por encerrar um emprestimo quando o valor total for quitado.
        /// </summary>
        /// <param name="cliente">O cliente que possui a lista de emprestimos.</param>
        /// <param name="idEmprestimo">Qual emprestimo do cliente será encerrado.</param>
        /// <returns>Retorna se o emprestimo foi encerrado ou não.</returns>
        public bool EncerrarEmprestimo(Cliente cliente, int idEmprestimo) {
            var emprestimo = cliente.Emprestimos.FirstOrDefault(ec => ec.IdEmprestimo == idEmprestimo);
            if (ValidarCliente(cliente))
            {
                if (emprestimo.ValorEmprestimo == 0)
                {
                    emprestimo.StatusEmprest = 0;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else return false;
        }

        /// <summary>
        /// Metodo responsavel por quitar um emprestimo antes do prazo definido com o cliente.
        /// </summary>
        /// <param name="cliente">Cliente que deseja quitar o emprestimo.</param>
        /// <param name="idEmprestimo">Emprestimo que o cliente deseja quitar.</param>
        /// <param name="valorPago">Valor dado pelo cliente para o pagamento do emprestimo.</param>
        /// <returns>Retorna se o emprestimo foi quitado ou não.</returns>
        public bool QuitarEmprestimo(Cliente cliente, int idEmprestimo, double valorPago)
        {
            var emprestimo = cliente.Emprestimos.FirstOrDefault(ec => ec.IdEmprestimo == idEmprestimo);
            if (ValidarCliente(cliente))
            {
                if (emprestimo.ValorEmprestimo == 0)
                {
                    return false; // Já encerrado.
                }
                else
                {
                    emprestimo.ValorLiberado -= valorPago;
                    EncerrarEmprestimo(cliente, idEmprestimo);
                    return true; // Emprestimo quitado.
                }
            }
            else return false;
        }

        /// <summary>
        /// Metodo responsavel por registrar uma nova proposta de emprestimo que poderá ou não ser aprovada.
        /// </summary>
        /// <param name="cliente">O cliente que possui a lista de emprestimos.</param>
        /// <param name="valorProposta">Valor que o cliente propoe precisar.</param>
        /// <param name="qtdParcelas">Quantas vezes o cliente deseja pagar o emprestimo.</param>
        /// <param name="taxaJuros">A taxa de juros estipulada no contrato.</param>
        /// <param name="tipoEmprestimo">O tipo de emprestimo desejado pelo cliente.</param>
        /// <param name="dataVencimento">A data de vencimento mensal escolhida para o pagamento da parcela.</param>
        /// <returns>Retorna se a proposta de pedido do cliente foi inserida ou não na lista dele para ser aprovada.</returns>
        public bool PropostaDeEmprestimo(Cliente cliente, double valorProposta, int qtdParcelas, double taxaJuros, int tipoEmprestimo, DateTime dataVencimento) {
            if (ValidarCliente(cliente))
            {
                try
                {
                    int id = cliente.Emprestimos.Count() + 1;

                    var emprestimo = new Emprestimo(
                        id,
                        valorProposta,
                        qtdParcelas,
                        taxaJuros,
                        tipoEmprestimo,
                        dataVencimento
                    );

                    //cliente.Emprestimos.Add(emprestimo);

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else return false;
        }

        /// <summary>
        /// Metodo responsavel por deduzir a parcela paga no valor que o cliente ainda deve.
        /// </summary>
        /// <param name="cliente">O cliente que possui a lista de emprestimos.</param>
        /// <param name="valorPago">Valor da parcela paga.</param>
        /// <param name="idEmprestimo">Emprestimo que o cliente deseja pagar sua parcela.</param>
        public bool PagarParcela(Cliente cliente, double valorPago, int idEmprestimo) {
            if (ValidarCliente(cliente))
            {
                var emprestimo = cliente.Emprestimos.FirstOrDefault(ec => ec.IdEmprestimo == idEmprestimo);

                if(emprestimo.ValorParcela == valorPago) {
                    emprestimo.ValorLiberado -= valorPago;
                    emprestimo.QtdParcelaRest -= 1;
                    return true;
                }
                else {
                    return false;
                }
            }
            else return false;           
        }

        //public void GerarParcela(ModelEmprestimoConsignado emprestimo)
        //{
        //    double aux = emprestimo.ValorLiberado / emprestimo.QtdParcelasEmprestimo;
        //    double parcela = aux * emprestimo.TaxaJurosEmprestimo;

        //    emprestimo.ValorParcelaEmprestimo = parcela;
        //}

        /// <summary>
        /// Metodo responsavel por gerar uma parcela que caiba dentro da margem disponivel para o cliente.
        /// </summary>
        /// <param name="taxaJuros"></param>
        /// <param name="cliente"></param>
        /// <param name="margem"></param>
        /// <returns>Retorna o valor da parcela maxima para a margem diponivel para o cliente.</returns>
        public double GerarParcela(double taxaJuros, Cliente cliente, double margem)
        {
            if (ValidarCliente(cliente))
            {
                double maxValorParcela = (cliente.Salario * margem);
                maxValorParcela -= CalcularJurosParcela(taxaJuros, maxValorParcela);
                return maxValorParcela;
            }
            else return 0;
        }

        /// <summary>
        /// Metodo responsavel por retornar a margem disponivel do cliente se estiver validar e dentro das margens permitidas.
        /// </summary>
        /// <param name="cliente">O cliente que possui a lista de emprestimos.</param>
        /// <param name="emprestimo">Qual emprestimo do cliente será analisado pelo tipo.</param>
        /// <returns>Retorna a margem disponivel do cliente</returns>
        public double GerarMargemTipoEmprestimo(Cliente cliente, Emprestimo emprestimo)
        {
            double margem = 0.00D;
            double margemPermitida = 0.00D;

            if (ValidarCliente(cliente) && ValidarEmprestimo(emprestimo))
            {
                switch (emprestimo.TipoEmprest)
                {
                    case 1:
                        margem = cliente.Margem;
                        margemPermitida = 0.3;
                        break;
                    case 2:
                        margem = cliente.MargemCartao;
                        margemPermitida = 0.05;
                        break;
                    default:
                        break;
                }

                if (ValidarMargemCliente(margem, margemPermitida)) return margem;
                else return 0;
            }
            else return 0;
        }

        /// <summary>
        /// Metodo responsavel pela liberacao do valor pedido pelo cliente ou o maximo disponivel para o mesmo.
        /// </summary>
        /// <param name="cliente"></param>
        /// <param name="emprestimo"></param>
        /// <returns>Retorno um valor de emprestimo que pode ser o maximo disponivel ou o afirmado com o cliente.</returns>
        public bool GerarValorLiberacao(Cliente cliente, Emprestimo emprestimo)
        {
            if (ValidarCliente(cliente) && ValidarEmprestimo(emprestimo))
            {
                double margem = GerarMargemTipoEmprestimo(cliente, emprestimo);

                if (margem > 0)
                {
                    double valorParcelaLiberado = GerarParcela(emprestimo.TaxaJuros, cliente, margem);
                    double valorLiberado = valorParcelaLiberado * emprestimo.QtdParcela;

                    if (ValidarValorLiberacao(emprestimo.ValorEmprestimo, valorLiberado))
                    {
                        return true; // Valor pedido pelo cliente foi aprovado.
                    }
                    else
                    {
                        emprestimo.ValorLiberado = valorLiberado;
                        emprestimo.ValorParcela = valorParcelaLiberado + CalcularJurosParcela(emprestimo.TaxaJuros, valorParcelaLiberado);
                        return true; // Outro valor foi liberado.
                    }
                }
                else
                {
                    Console.WriteLine("Emprestimo não aprovado por falta de margem.");
                    //cliente.Emprestimos.Remove(emprestimo);
                    return false;
                }
            }
            else return false;
        }

        /// <summary>
        /// Metodo responsavel por calcular o juros a ser cobrado na parcela.
        /// </summary>
        /// <param name="taxaJuros">Taxa de juros cobrada pelo banco</param>
        /// <param name="valorParcela">Valor da parcela do emprestimo</param>
        /// <returns>Juros a ser cobrado na parcela</returns>
        private double CalcularJurosParcela(double taxaJuros, double valorParcela)
        {
            return valorParcela * taxaJuros;
        }

        /// <summary>
        /// Metodo responsavel por validar cliente
        /// </summary>
        /// <param name="cliente">Cliente a ser validado</param>
        /// <returns>Se o cliente é valido ou não</returns>
        private bool ValidarCliente(Cliente cliente)
        {
            //var lista = new List<int>();
            //foreach (int item in Enum.GetValues(typeof(ModelProfissoes.ProfissoesValidas))) lista.Add(item);

            //if (lista.Contains(cliente.ProfissaoCliente) && cliente.EstadoCliente == 1) return true;
            if (cliente.Status == 1) return true;
            else return false;
        }

        /// <summary>
        /// Metodo responsavel por validar o estado do emprestimo: 1- Ativo ou 2- A ser aprovado
        /// </summary>
        /// <param name="emprestimo">Emprestimo a ser validado</param>
        /// <returns>Se o emprestimo está ativo ou não</returns>
        private bool ValidarEmprestimo(Emprestimo emprestimo)
        {
            if (emprestimo.StatusEmprest == 1 && emprestimo.StatusEmprest == 2) return true;
            else return false;
        }

        /// <summary>
        /// Metodo responsavel por listar emprestimo do cliente
        /// </summary>
        /// <param name="emprestimos">Lista de emprestimos do cliente</param>
        /// <returns>Lista os emprestimos</returns>
        public String ListarEmprestimos(List<Emprestimo> emprestimos)
        {
            string jsonString = JsonSerializer.Serialize(emprestimos);

            return jsonString;
        }
    }
}
