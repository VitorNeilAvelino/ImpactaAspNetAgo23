using GatewayPagamento.Dominio.Entidades;
using GatewayPagamento.WebApi.Helpers;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Web.Services.Protocols;

namespace GatewayPagamento.WebApi.Models
{
    public class PagamentoGetViewModel
    {
        public int Id { get; set; }
        public string MascaraCartao { get; set; }
        public string NumeroPedido { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public int Status { get; set; }
        public string DescricaoStatus { get; set; }

        internal static IEnumerable<PagamentoGetViewModel> Mapear(List<Pagamento> pagamentos)
        {
            var viewModels = new List<PagamentoGetViewModel>();

            foreach (var pagamento in pagamentos)
            {
                viewModels.Add(Mapear(pagamento));
            }

            return viewModels;
        }

        private static PagamentoGetViewModel Mapear(Pagamento pagamento)
        {
            var viewModel = new PagamentoGetViewModel();

            viewModel.Id = pagamento.Id;
            viewModel.NumeroPedido = pagamento.NumeroPedido;
            viewModel.Data = pagamento.Data;
            viewModel.Valor = pagamento.Valor;

            var numeroCartao = pagamento.Cartao.Numero;

            viewModel.MascaraCartao = $"{numeroCartao.Substring(0, 6)}...{numeroCartao.Substring(numeroCartao.Length - 4)}";

            viewModel.Status = (int)pagamento.Status;
            viewModel.DescricaoStatus = pagamento.Status.ObterDescricao();
            
            return viewModel;
        }
    }
}