using ExpoCenter.Dominio.Entidades;
using ExpoCenter.Dominio.Interfaces;
using System.Net.Http.Json;

namespace ExpoCenter.Repositorios.Http
{
    public class PagamentoRepositorio : IPagamentoRepositorio
    {
        private readonly HttpClient _httpClient;
        private const string caminho = "pagamentos";

        public PagamentoRepositorio(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Pagamento>> Get()
        {
            using var resposta = await _httpClient.GetAsync(caminho);
            return await resposta.Content.ReadFromJsonAsync<List<Pagamento>>();
        }

        public async Task<Pagamento> Get(int id)
        {
            using var resposta = await _httpClient.GetAsync($"{caminho}/{id}");
            return await resposta.Content.ReadFromJsonAsync<Pagamento>();
        }

        public async Task<Pagamento> Post(Pagamento pagamento)
        {
            using var resposta = await _httpClient.PostAsJsonAsync(caminho, pagamento);
            return await resposta.Content.ReadFromJsonAsync<Pagamento>();
        }

        public async Task Put(Pagamento pagamento)
        {
            using var resposta = await _httpClient.PutAsJsonAsync($"{caminho}/{pagamento.Id}", pagamento);
            resposta.EnsureSuccessStatusCode();
        }

        public async Task Delete(int id)
        {
            using var resposta = await _httpClient.DeleteAsync($"{caminho}/{id}");
            resposta.EnsureSuccessStatusCode();
        }
    }
}