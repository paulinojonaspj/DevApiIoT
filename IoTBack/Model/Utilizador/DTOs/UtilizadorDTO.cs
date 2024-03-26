using System.Text.Json.Serialization;

namespace IOTBack.Model.Utilizador.DTOS
{
    public class UtilizadorDTO :Utilizador
    {
        [JsonIgnore]  
        public new string? PalavraPasse { get; set; }
    }
}
