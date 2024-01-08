using IOTBack.Infraestrutura;
using IOTBack.Model.Empregado.DTOS;
using Microsoft.EntityFrameworkCore;

namespace IOTBack.Model.Empregado
{
    public class REmpregado : IEmpregado
    {
        private readonly ConnectionContext _context = new ConnectionContext();
        void IEmpregado.Add(Empregado empregado)
        {

            _context.Empregado.Add(empregado);

            _context.SaveChanges();

        }

        List<Empregado> IEmpregado.GetAll()
        {
            return _context.Empregado.ToList();
        }

        Empregado? IEmpregado.Get(int id)
        {
            return _context.Empregado.Find(id);
        }

        List<Empregado> IEmpregado.GetPaginacao(int pageNumber, int pageQuantity)
        {
            return _context.Empregado.Skip(pageNumber * pageQuantity).Take(pageQuantity).ToList();
        }

        //DTO sem Mapping
        List<EmpregadoDTO> IEmpregado.GetDTO()
        {
            return _context.Empregado.Select(b => new EmpregadoDTO(b.Nome ?? "", b.Email ?? "")).ToList();
        }
    }
}
