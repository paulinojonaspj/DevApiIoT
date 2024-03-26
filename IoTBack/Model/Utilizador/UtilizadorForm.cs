using AutoMapper.Configuration.Annotations;

namespace IOTBack.Model.Utilizador
{
    public class UtilizadorForm :Utilizador
    {
         
        private int Id { get; set; }
        private string Foto { get; set; }
        public IFormFile? Ficheiro { get; set; }
    }
}
