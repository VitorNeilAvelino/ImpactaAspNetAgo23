using GatewayPagamento.Dominio.Interfaces;
using GatewayPagamento.Dominio.Servicos;
using GatewayPagamento.Repositorios.SqlServer.CodeFirst;
using GatewayPagamento.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using static GatewayPagamento.WebApi.Models.PagamentoGetViewModel;

namespace GatewayPagamento.WebApi.Controllers
{
    public class PagamentosController : ApiController
    {
        private readonly IPagamentoRepositorio pagamentoRepositorio = new PagamentoRepositorio();
        private readonly ICartaoRepositorio cartaoRepositorio = new CartaoRepositorio();
        private readonly PagamentoServico pagamentoServico;

        public PagamentosController()
        {
            pagamentoServico = new PagamentoServico(cartaoRepositorio, pagamentoRepositorio);
        }

        [Route("api/pagamentos/cartao/{guidCartao}")]
        public IEnumerable<PagamentoGetViewModel> Get(Guid guidCartao)
        {
            return Mapear(pagamentoRepositorio.Selecionar(guidCartao));
        }

        public IHttpActionResult Post(PagamentoPostViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pagamento = PagamentoPostViewModel.Mapear(viewModel);

            pagamentoServico.Inserir(pagamento);

            var responseViewModel = Mapear(pagamento);

            switch (pagamento.Status)
            {
                case Dominio.Entidades.StatusPagamento.SaldoInsuficiente:
                case Dominio.Entidades.StatusPagamento.PedidoJaPago:
                case Dominio.Entidades.StatusPagamento.CartaoInexistente:
                    return Content(HttpStatusCode.BadRequest, responseViewModel);
                case Dominio.Entidades.StatusPagamento.PagamentoOK:
                    return Ok(responseViewModel);
            }

            return InternalServerError(new ArgumentOutOfRangeException(nameof(pagamento.Status)));
        }
    }
}