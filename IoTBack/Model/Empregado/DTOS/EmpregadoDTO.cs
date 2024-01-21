namespace IOTBack.Model.Empregado.DTOS
{
    public class EmpregadoDTO 
    {
        //Alguns Campos
        public string? Nome { get; set; }
        public string? Email { get; set; }

        public EmpregadoDTO(string nome, string email)
        {
            Nome = nome;
            Email = email;
        }
    }
}
