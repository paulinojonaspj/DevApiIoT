using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IOTBack.Model.Consumo
{
    [Table("consumo")]
    public class Consumo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public required string Unidade { get; set; }
         
        [Required]
        public required double Quantidade { get; set; }
 

        [StringLength(100)]
        [Required]
        public required string Localizacao { get; set; }

        [Required]
        public required string Tipo { get; set; }


        [Required]
        public required DateTime Data { get; set; }

        [Required]
        public required int Hora { get; set; }
        public Consumo() { }
        public Consumo(string unidade, double quantidade, string localizacao, string tipo, DateTime data, int hora)
        {
            Unidade = unidade;
            Quantidade = quantidade;
            Localizacao = localizacao;
            Tipo = tipo;
            Data = data;
            Hora = hora;
        }
    }
 

}
