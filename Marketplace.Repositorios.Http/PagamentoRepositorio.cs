using Marketplace.Repositorios.Http.Requests;
using Marketplace.Repositorios.Http.Responses;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Marketplace.Repositorios.Http
{
    public class PagamentoRepositorio
    {
        private readonly HttpClient httpClient = new HttpClient();
        private const string caminho = "pagamentos";

        public PagamentoRepositorio(string baseAddress)
        {
            httpClient.BaseAddress = new Uri(baseAddress.Trim('/') + '/');
        }

        /// <summary>
        /// Obtém a fatura do cartão.
        /// </summary>
        /// <param name="guidCartao">Guid do cartão.</param>
        public async Task<List<PagamentoResponse>> GetByCartao(Guid guidCartao)
        {
            using (var resposta = await httpClient.GetAsync($"{caminho}/cartao/{guidCartao}"))
            {
                return await resposta.Content.ReadAsAsync<List<PagamentoResponse>>();
            }
        }

        public async Task<PagamentoResponse> Post(PagamentoRequest pagamento)
        {
            using (var resposta = await httpClient.PostAsJsonAsync(caminho, pagamento))
            {
                return await resposta.Content.ReadAsAsync<PagamentoResponse>();
            }
        }
    }
}