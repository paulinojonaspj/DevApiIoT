using IOTBack.Model.Empregado.DTOS;

namespace IOTBack.Model.Empregado
{
    public interface IEmpregado
    {
        void Add(Empregado empregado);

        List<Empregado> GetAll();

        List<EmpregadoDTO> GetDTO();

        Empregado? Get(int id);

        List<Empregado> GetPaginacao(int pageNumber, int pageQuantity);
    }
}
