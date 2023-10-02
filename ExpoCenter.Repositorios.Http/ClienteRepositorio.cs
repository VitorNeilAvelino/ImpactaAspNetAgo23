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
    }
}