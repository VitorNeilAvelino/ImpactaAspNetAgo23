using ExpoCenter.Dominio.Interfaces;
using System.Net.Http.Json;

namespace ExpoCenter.Repositorios.Http
{
    public class CrudRepositorio<T> : ICrudRepositorio<T> //IClienteRepositorio
    {
        private readonly HttpClient _httpClient;
        //private const string caminho = "clientes";

        public /*required*/ string Caminho { get; set; }
        public string Token { get; set; }

        public CrudRepositorio(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<T>> Get()
        {
            if (Token != null)
            {
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Token}");
            }

            using var resposta = await _httpClient.GetAsync(Caminho);

            if (resposta.IsSuccessStatusCode)
            {
                return await resposta.Content.ReadFromJsonAsync<List<T>>(); 
            }

            throw new HttpRequestException(resposta.StatusCode.ToString(), null, resposta.StatusCode);
        }

        public async Task<T> Get(int id)
        {
            using var resposta = await _httpClient.GetAsync($"{Caminho}/{id}");
            return await resposta.Content.ReadFromJsonAsync<T>();
        }

        public async Task<T> Post(T entidade)
        {
            using var resposta = await _httpClient.PostAsJsonAsync(Caminho, entidade);
            return await resposta.Content.ReadFromJsonAsync<T>();
        }

        public async Task Put(T entidade, int id)
        {
            using var resposta = await _httpClient.PutAsJsonAsync($"{Caminho}/{id}", entidade);
            resposta.EnsureSuccessStatusCode();
        }

        public async Task Delete(int id)
        {
            using var resposta = await _httpClient.DeleteAsync($"{Caminho}/{id}");
            resposta.EnsureSuccessStatusCode();
        }
    }
}