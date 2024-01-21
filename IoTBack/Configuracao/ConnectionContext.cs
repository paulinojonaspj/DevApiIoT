using IOTBack.Configuracao;
using IOTBack.Model.Empregado;
using Microsoft.EntityFrameworkCore;

namespace IOTBack.Infraestrutura
{
    public class ConnectionContext : DbContext
    {
        public DbSet<Empregado> Empregado { get; set; }

        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlServer(Key.Descriptografar(configuration["conexao:stringConnection"]));
    }
}
