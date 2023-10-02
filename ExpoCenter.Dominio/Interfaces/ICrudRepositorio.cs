namespace ExpoCenter.Dominio.Interfaces
{
    public interface ICrudRepositorio<T>
    {
        string Caminho { get; set; }
        Task Delete(int id);
        Task<List<T>> Get();
        Task<T> Get(int id);
        Task<T> Post(T entidade);
        Task Put(T entidade, int id);
    }
}