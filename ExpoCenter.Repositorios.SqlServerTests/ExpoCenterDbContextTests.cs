using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpoCenter.Repositorios.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using ExpoCenter.Dominio.Entidades;

namespace ExpoCenter.Repositorios.SqlServer.Tests
{
    [TestClass()]
    public class ExpoCenterDbContextTests
    {
        private readonly ExpoCenterDbContext dbContext;
        private readonly DbContextOptions<ExpoCenterDbContext> options;

        public ExpoCenterDbContextTests()
        {
            options = new DbContextOptionsBuilder<ExpoCenterDbContext>()
                .UseSqlServer(new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ExpoCenter;Integrated Security=True"))
                .LogTo(ExibirQuery, LogLevel.Information)
                .Options;

            dbContext = new ExpoCenterDbContext(options);
        }

        private void ExibirQuery(string query)
        {
            Console.WriteLine(query);
        }

        [TestMethod()]
        public void InserirEventoTeste()
        {
            var evento = new Evento();
            evento.Data = DateTime.Now;
            evento.Descricao = "Comic Con 2024";
            evento.Local = "Los Angeles";
            evento.Preco = 100;

            dbContext.Eventos.Add(evento);
            //dbContext.Add(evento);
            dbContext.SaveChanges();
        }

        [TestMethod]
        public void InserirParticipanteTeste()
        {
            var participante = new Participante();
            participante.Cpf = "12345678900";
            participante.Email = "avelino.vitor@gmail.com";
            participante.DataNascimento = Convert.ToDateTime("01/01/2000");
            participante.Nome = "Vítor Avelino";

            participante.Eventos = new List<Evento>
            {
                dbContext.Eventos.Single(e => e.Descricao == "Comic Con 2023")
            };
            //participante.Eventos.Add(dbContext.Eventos.Single(e => e.Descricao == "Comic Con 2023"));

            dbContext.Participantes.Add(participante);
            dbContext.SaveChanges();

            foreach (var evento in participante.Eventos)
            {
                Console.WriteLine(evento.Descricao);
            }
        }

        [TestMethod]
        public void InserirPagamentoTeste()
        {
            var pagamento = new Pagamento();

            pagamento.IdCartao = Guid.NewGuid();
            pagamento.IdProduto = Guid.NewGuid();
            pagamento.Valor = 21;
            pagamento.Status = 4;

            dbContext.Add(pagamento);
            dbContext.SaveChanges();
        }
    }
}