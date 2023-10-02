using ExpoCenter.Dominio.Entidades;

namespace ExpoCenter.Dominio.Interfaces
{
    public interface IAccountRepositorio
    {
        Task<UserToken> Login(LoginModel request);
    }
}