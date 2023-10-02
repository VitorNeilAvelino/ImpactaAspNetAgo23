using ExpoCenter.Dominio.Entidades;

namespace ExpoCenter.Dominio.Interfaces
{
    public interface IClienteRepositorio : ICrudRepositorio<Cliente>
    {
        Task Upgrade(int id);
        //Task Delete(int id);
        //Task<List<Cliente>> Get();
        //Task<Cliente> Get(int id);
        //Task<Cliente> Post(Cliente cliente);
        //Task Put(Cliente cliente);
    }
}