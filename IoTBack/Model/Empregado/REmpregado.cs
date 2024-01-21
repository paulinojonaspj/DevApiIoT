using IOTBack.Infraestrutura;
using IOTBack.Model.Empregado.DTOS;
using Microsoft.EntityFrameworkCore;

namespace IOTBack.Model.Empregado
{
    public class REmpregado : IEmpregado
    {
        private readonly ConnectionContext _context = new ConnectionContext();
      async Task<bool> IEmpregado.Add(Empregado empregado)
        {
          await _context.Empregado.AddAsync(empregado);
          return await _context.SaveChangesAsync()>0;
        }

       async Task<bool> IEmpregado.Alterar(Empregado empregado)
        {
            _context.Entry(empregado).State = EntityState.Modified;
            return await _context.SaveChangesAsync()>0;
        }

        async Task<bool> IEmpregado.Remover(Empregado empregado)
        {
             _context.Empregado.Remove(empregado);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Empregado>> GetAll()
        {
            return await _context.Empregado.ToListAsync();
        }

        async Task<Empregado?> IEmpregado.Get(int id)
        {
            return await _context.Empregado.FindAsync(id);
        }

       public async Task<IEnumerable<Empregado>> GetPaginacao(int pageNumber, int pageQuantity)
        {
            return await _context.Empregado.Skip(pageNumber * pageQuantity).Take(pageQuantity).ToListAsync();
        }

        //DTO sem Mapping
        async Task<IEnumerable<EmpregadoDTO>> IEmpregado.GetDTO()
        {
            return await _context.Empregado.Select(b => new EmpregadoDTO(b.Nome ?? "", b.Email ?? "")).ToListAsync();
        }

        
    }
}
