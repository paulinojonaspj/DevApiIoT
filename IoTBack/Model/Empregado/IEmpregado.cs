using IOTBack.Model.Empregado.DTOS;

namespace IOTBack.Model.Empregado
{
    public interface IEmpregado
    {
        Task<bool> Add(Empregado empregado);
        Task<bool> Remover(Empregado empregado);
        Task<bool> Alterar(Empregado empregado);


        //Tudo que retorna recomenda-se utilizar Task async
        Task<IEnumerable<Empregado>> GetAll();

        Task<IEnumerable<EmpregadoDTO>> GetDTO();

        Task<Empregado?> Get(int id);

        Task<IEnumerable<Empregado>> GetPaginacao(int pageNumber, int pageQuantity);
    }
}
