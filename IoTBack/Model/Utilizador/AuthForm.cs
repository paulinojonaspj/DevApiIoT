
namespace IOTBack.Model.Utilizador
{
    public class AuthForm
    {
        public string Utilizador { get; set; }
        public string palavrapasse { get; set; }

        public AuthForm(string utilizador, string palavrapasse)
        {
            Utilizador = utilizador;
            this.palavrapasse = palavrapasse;
        }
    }
}
