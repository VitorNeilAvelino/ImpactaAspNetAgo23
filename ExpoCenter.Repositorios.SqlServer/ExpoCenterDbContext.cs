using ExpoCenter.Dominio.Entidades;
using ExpoCenter.Repositorios.SqlServer.ModelConfiguration;
using Microsoft.EntityFrameworkCore;

namespace ExpoCenter.Repositorios.SqlServer
{
    public class ExpoCenterDbContext : DbContext
    {
        public ExpoCenterDbContext(DbContextOptions<ExpoCenterDbContext> opcoes) : base(opcoes)
        {
            Database.EnsureCreated();
        }

        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Participante> Participantes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new EventoConfiguration());
            modelBuilder.ApplyConfiguration(new ParticipanteConfiguration());
        }
    }
}