using IOTBack.Infraestrutura;
using IOTBack.Model.Objetivo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IOTBack.Controllers
{
    [ApiController]
    [Route("api/v1/bsmart")]
    public class Bsmart : Controller
    {
        private readonly ConnectionContext _context = new ConnectionContext();
     
        [HttpGet("objetivos")]
        public async Task<IEnumerable<Objetivo>> GetObjetivos()
        {
            return await _context.Objetivo.ToListAsync();
        }

       [HttpPut("objetivos")]
       public async Task<bool> Alterar(Objetivo entidade)
        {
            _context.Objetivo.Entry(entidade).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

    }
}
