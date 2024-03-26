using System.ComponentModel.DataAnnotations;

namespace IOTBack.Model.Consumo
{
    public class FConsumo
    {

        [Required]  
        public required string Tipo { get; set; }
        [Required]
        public required string Localizacao { get; set; }
        [Required] 
        public required double Quantidade { get; set; }

    }
}
