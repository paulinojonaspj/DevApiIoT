using IOTBack.Infraestrutura;
using IOTBack.Model.Consumo;
using Microsoft.EntityFrameworkCore;

namespace IOTBack.Model.Consumo
{
    public class RConsumo : IConsumo
    {
        private readonly ConnectionContext _context = new ConnectionContext();
        async Task<bool> IConsumo.Add(Consumo entidade)
        {
            await _context.Consumo.AddAsync(entidade);
            return await _context.SaveChangesAsync() > 0;
        }

        async Task<bool> IConsumo.Alterar(Consumo entidade)
        {
            _context.Entry(entidade).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        async Task<bool> IConsumo.Remover(Consumo entidade)
        {
            _context.Consumo.Remove(entidade);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Consumo>> GetAll()
        {
            return await _context.Consumo.ToListAsync();
        }

        async Task<Consumo?> IConsumo.Get(int id)
        {
            return await _context.Consumo.FindAsync(id);
        }

        public async Task<IEnumerable<Consumo>> GetPaginacao(int pageNumber, int pageQuantity)
        {
            return await _context.Consumo.Skip(pageNumber * pageQuantity).Take(pageQuantity).ToListAsync();
        }
    }
}
