using System.ComponentModel.DataAnnotations.Schema;

namespace IOTBack.Model.Empregado
{
    [Table("Empregado")]
    public class Empregado
    {
        public int Id { get;  private set; }
        public string? Nome { get; private set; }
        public int Idade { get; private set; }
        public string? Email { get; private set; }
        public string? Foto { get; private set; }

        public Empregado() { }
        public Empregado(string nome, int idade, string email, string? foto)
        {

            this.Nome = nome ?? throw new ArgumentNullException(nameof(nome));
            this.Idade = idade;
            this.Email = email;
            this.Foto = foto;
        }
    }
}
