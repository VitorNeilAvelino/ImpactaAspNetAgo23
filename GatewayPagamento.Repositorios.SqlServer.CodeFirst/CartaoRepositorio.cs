using GatewayPagamento.Dominio.Entidades;
using GatewayPagamento.Dominio.Interfaces;
using System.Linq;

namespace GatewayPagamento.Repositorios.SqlServer.CodeFirst
{
    public class CartaoRepositorio : ICartaoRepositorio
    {
        public Cartao Selecionar(string numeroCartao)
        {
            using (var contexto = new GatewayPagamentoContext())
            {
                return contexto.Cartoes.SingleOrDefault(c => c.Numero == numeroCartao);
            }
        }
    }
}