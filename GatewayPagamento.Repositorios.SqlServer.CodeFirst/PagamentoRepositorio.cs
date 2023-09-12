using GatewayPagamento.Dominio.Entidades;
using GatewayPagamento.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;

namespace GatewayPagamento.Repositorios.SqlServer.CodeFirst
{
    public class PagamentoRepositorio : IPagamentoRepositorio
    {
        public void Inserir(Pagamento pagamento)
        {
            using (var contexto = new GatewayPagamentoContext())
            {
                pagamento.Cartao = contexto.Cartoes
                    .SingleOrDefault(c => c.Numero == pagamento.Cartao.Numero);

                contexto.Pagamentos.Add(pagamento);
                contexto.SaveChanges();
            }
        }

        public List<Pagamento> Selecionar(Guid guidCartao)
        {
            using (var contexto = new GatewayPagamentoContext())
            {
                return contexto.Pagamentos
                    //.Include("Cartao")
                    .Include(p => p.Cartao)
                    .Where(p => p.Cartao.Guid == guidCartao)
                    .ToList();
            }
        }

        public List<Pagamento> Selecionar(Expression<Func<Pagamento, bool>> condicao)
        {
            using (var contexto = new GatewayPagamentoContext())
            {
                return contexto.Pagamentos
                    .Include(p => p.Cartao)
                    .Where(condicao)
                    .ToList();
            }
        }
    }
}