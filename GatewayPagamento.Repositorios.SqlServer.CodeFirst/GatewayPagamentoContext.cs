using GatewayPagamento.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GatewayPagamento.Repositorios.SqlServer.CodeFirst
{
    public class GatewayPagamentoContext : DbContext
    {
        public GatewayPagamentoContext() : base("GatewayPagamentoConnection")
        {
                
        }

        public DbSet<Cartao> Cartoes { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }
    }
}
