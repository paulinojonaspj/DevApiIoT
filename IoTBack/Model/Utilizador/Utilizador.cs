using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOTBack.Model.Utilizador
{
    [Table("utilizador")]
    public class Utilizador
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telemovel { get; set; }
        public string CodigoPostal { get; set; }
        public string Morada { get; set; }
        public string CasaNumero { get; set; }
        public string OperadoraAgua { get; set; }
        public float PrecoFixoAgua { get; set; }
        public float PrecoAgua { get; set; }
        public string OperadoraEnergia { get; set; }
        public string TarifaEnergia { get; set; }
        public float PotenciaEnergia { get; set; }
        public float PrecoFixoEnergia { get; set; }
        public float PrecoP1Energia { get; set; }
        public float PrecoP2Energia { get; set; }
        public float PrecoP3Energia { get; set; }
        public TimeSpan HorarioDeP1Energia { get; set; }
        public TimeSpan HorarioAteP1Energia { get; set; }
        public TimeSpan HorarioDeP2Energia { get; set; }
        public TimeSpan HorarioAteP2Energia { get; set; }
        public TimeSpan HorarioDeP3Energia { get; set; }
        public TimeSpan HorarioAteP3Energia { get; set; }
        public string? PalavraPasse { get; set; }

        public string? Foto { get; set; }

        public Utilizador() { }
        public Utilizador(int id, string nome, string email, string telemovel, string codigoPostal, string morada, string casaNumero, string operadoraAgua, float precoFixoAgua, float precoAgua, string operadoraEnergia, string tarifaEnergia, float potenciaEnergia, float precoFixoEnergia, float precoP1Energia, float precoP2Energia, float precoP3Energia, TimeSpan horarioDeP1Energia, TimeSpan horarioAteP1Energia, TimeSpan horarioDeP2Energia, TimeSpan horarioAteP2Energia, TimeSpan horarioDeP3Energia, TimeSpan horarioAteP3Energia, string? palavraPasse, string? foto)
        {
            Id = id;
            Nome = nome ?? throw new ArgumentNullException(nameof(nome));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Telemovel = telemovel ?? throw new ArgumentNullException(nameof(telemovel));
            CodigoPostal = codigoPostal ?? throw new ArgumentNullException(nameof(codigoPostal));
            Morada = morada ?? throw new ArgumentNullException(nameof(morada));
            CasaNumero = casaNumero ?? throw new ArgumentNullException(nameof(casaNumero));
            OperadoraAgua = operadoraAgua ?? throw new ArgumentNullException(nameof(operadoraAgua));
            PrecoFixoAgua = precoFixoAgua;
            PrecoAgua = precoAgua;
            OperadoraEnergia = operadoraEnergia ?? throw new ArgumentNullException(nameof(operadoraEnergia));
            TarifaEnergia = tarifaEnergia ?? throw new ArgumentNullException(nameof(tarifaEnergia));
            PotenciaEnergia = potenciaEnergia;
            PrecoFixoEnergia = precoFixoEnergia;
            PrecoP1Energia = precoP1Energia;
            PrecoP2Energia = precoP2Energia;
            PrecoP3Energia = precoP3Energia;
            HorarioDeP1Energia = horarioDeP1Energia;
            HorarioAteP1Energia = horarioAteP1Energia;
            HorarioDeP2Energia = horarioDeP2Energia;
            HorarioAteP2Energia = horarioAteP2Energia;
            HorarioDeP3Energia = horarioDeP3Energia;
            HorarioAteP3Energia = horarioAteP3Energia;
            PalavraPasse = palavraPasse;
            Foto = foto;
        }
    }
}
