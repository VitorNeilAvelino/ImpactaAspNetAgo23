using GatewayPagamento.Dominio.Interfaces;
using GatewayPagamento.Repositorios.SqlServer.CodeFirst;
using GatewayPagamento.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GatewayPagamento.WebApi.Controllers
{
    public class PagamentosController : ApiController
    {
        private readonly IPagamentoRepositorio pagamentoRepositorio = new PagamentoRepositorio();

        public IEnumerable<PagamentoGetViewModel> Get(Guid guidCartao)
        {
            return PagamentoGetViewModel.Mapear(pagamentoRepositorio.Selecionar(guidCartao));
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}