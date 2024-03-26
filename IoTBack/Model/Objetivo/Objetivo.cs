using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOTBack.Model.Objetivo
{
    [Table("objetivo")]
    public class Objetivo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        required
        public string Tipo { get; set; }
        public int Ano { get; set; }
        public float Janeiro { get; set; }
        public float Fevereiro { get; set; }
        public float Marco { get; set; }
        public float Abril { get; set; }
        public float Maio { get; set; }
        public float Junho { get; set; }
        public float Julho { get; set; }
        public float Agosto { get; set; }
        public float Setembro { get; set; }
        public float Outubro { get; set; }
        public float Novembro { get; set; }
        public float Dezembro { get; set; }

        public Objetivo(int id, string tipo, int ano, float janeiro, float fevereiro, float marco, float abril,
                            float maio, float junho, float julho, float agosto, float setembro, float outubro,
                            float novembro, float dezembro)
        {
            Id = id;
            Tipo = tipo;
            Ano = ano;
            Janeiro = janeiro;
            Fevereiro = fevereiro;
            Marco = marco;
            Abril = abril;
            Maio = maio;
            Junho = junho;
            Julho = julho;
            Agosto = agosto;
            Setembro = setembro;
            Outubro = outubro;
            Novembro = novembro;
            Dezembro = dezembro;
        }
    }



}
