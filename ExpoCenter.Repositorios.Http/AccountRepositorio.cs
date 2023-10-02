using ExpoCenter.Dominio.Entidades;
using ExpoCenter.Dominio.Interfaces;
using System.Net.Http.Json;

namespace ExpoCenter.Repositorios.Http
{
    public class AccountRepositorio : IAccountRepositorio
    {
        private readonly HttpClient _httpClient;
        private const string caminho = "account/login";

        public AccountRepositorio(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserToken> Login(LoginModel request)
        {
            using var resposta = await _httpClient.PostAsJsonAsync(caminho, request);
            return await resposta.Content.ReadFromJsonAsync<UserToken>();
        }
    }
}