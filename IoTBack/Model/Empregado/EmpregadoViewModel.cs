namespace IOTBack.Model.Empregado
{
    public class EmpregadoViewModel
    {
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public int Idade { get; set; }
        public IFormFile? Foto { get; set; }
    }
}
