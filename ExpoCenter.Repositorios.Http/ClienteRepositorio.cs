using ExpoCenter.Dominio.Entidades;
using ExpoCenter.Dominio.Interfaces;

namespace ExpoCenter.Repositorios.Http
{
    public class ClienteRepositorio : CrudRepositorio<Cliente>, IClienteRepositorio
    {
        public ClienteRepositorio(HttpClient httpClient) : base(httpClient)
        {

        }

        public Task Upgrade(int id)
        {
            throw new NotImplementedException();
        }

        //private readonly HttpClient _httpClient;
        //private const string caminho = "clientes";

        //public string Caminho { get ; set ; }

        //public ClienteRepositorio(HttpClient httpClient)
        //{
        //    _httpClient = httpClient;
        //}

        //public async Task<List<Cliente>> Get()
        //{
        //    using var resposta = await _httpClient.GetAsync(caminho);
        //    return await resposta.Content.ReadFromJsonAsync<List<Cliente>>();
        //}

        //public async Task<Cliente> Get(int id)
        //{
        //    using var resposta = await _httpClient.GetAsync($"{caminho}/{id}");
        //    return await resposta.Content.ReadFromJsonAsync<Cliente>();
        //}

        //public async Task<Cliente> Post(Cliente cliente)
        //{
        //    using var resposta = await _httpClient.PostAsJsonAsync(caminho, cliente);
        //    return await resposta.Content.ReadFromJsonAsync<Cliente>();
        //}

        //public async Task Put(Cliente cliente, int id)
        //{
        //    using var resposta = await _httpClient.PutAsJsonAsync($"{caminho}/{id}", cliente);
        //    resposta.EnsureSuccessStatusCode();
        //}

        //public async Task Delete(int id)
        //{
        //    using var resposta = await _httpClient.DeleteAsync($"{caminho}/{id}");
        //    resposta.EnsureSuccessStatusCode();
        //}  
    }
}