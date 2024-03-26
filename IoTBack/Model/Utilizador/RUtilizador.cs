using Microsoft.EntityFrameworkCore;
using IOTBack.Infraestrutura;

namespace IOTBack.Model.Utilizador
{
    public class RUtilizador : IUtilizador
    {
        private readonly ConnectionContext _context = new ConnectionContext();
        async Task<bool> IUtilizador.Add(Utilizador entidade)
        {
            await _context.Utilizador.AddAsync(entidade);
            return await _context.SaveChangesAsync() > 0;
        }

        async Task<bool> IUtilizador.Alterar(Utilizador entidade)
        {
            _context.Entry(entidade).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        async Task<bool> IUtilizador.Remover(Utilizador entidade)
        {
            _context.Utilizador.Remove(entidade);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Utilizador>> GetAll()
        {
            return await _context.Utilizador.ToListAsync();
        }

        async Task<Utilizador?> IUtilizador.Get(int id)
        {
            return await _context.Utilizador.FindAsync(id);
        }

        public async Task<IEnumerable<Utilizador>> GetPaginacao(int pageNumber, int pageQuantity)
        {
            return await _context.Utilizador.Skip(pageNumber * pageQuantity).Take(pageQuantity).ToListAsync();
        }
 
    }
}
