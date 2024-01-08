using IOTBack.Model.Empregado;
using Microsoft.EntityFrameworkCore;

namespace IOTBack.Infraestrutura
{
    public class ConnectionContext : DbContext
    {
        public DbSet<Empregado> Empregado { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlServer("Server=94.46.180.24;Database=acessofa_iot;User Id=acessofa;Password=@K?1q7Q8vW2Ufo;TrustServerCertificate=true;");
    }
}
