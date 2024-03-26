using IOTBack.Model.Empregado.DTOS;
using IOTBack.Model.Utilizador.DTOS;

namespace IOTBack.Model.Utilizador
{
    public interface IUtilizador
    {
        Task<bool> Add(Utilizador entidade);
        Task<bool> Remover(Utilizador entidade);
        Task<bool> Alterar(Utilizador entidade);

        //Tudo que retorna recomenda-se utilizar Task async
        Task<IEnumerable<Utilizador>> GetAll();

        Task<Utilizador?> Get(int id);

        Task<IEnumerable<Utilizador>> GetPaginacao(int pageNumber, int pageQuantity);
    }
}
