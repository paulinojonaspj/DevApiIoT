using IOTBack.Configuracao;
using IOTBack.Model.Consumo;
using IOTBack.Model.Empregado;
using IOTBack.Model.Objetivo;
using IOTBack.Model.Utilizador;
using Microsoft.EntityFrameworkCore;

namespace IOTBack.Infraestrutura
{
    public class ConnectionContext : DbContext
    {
        public DbSet<Empregado> Empregado { get; set; }
        public DbSet<Utilizador> Utilizador { get; set; }
        public DbSet<Consumo> Consumo { get; set; }
        public DbSet<Objetivo> Objetivo { get; set; }


        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
             // optionsBuilder.UseMySQL(Key.Descriptografar(configuration["conexao:stringConnection"]??""));
            optionsBuilder.UseMySql((configuration["conexao:stringConnection"] ?? ""),ServerVersion.AutoDetect((configuration["conexao:stringConnection"] ?? "")));

       
    }
}
