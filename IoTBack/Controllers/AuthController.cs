using IOTBack.Configuracao;
using IOTBack.Infraestrutura;
using IOTBack.Model.Empregado;
using IOTBack.Model.Utilizador;
using IOTBack.Model.Utilizador.DTOS;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IOTBack.Controllers
{

    
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : Controller
    {
        [HttpPost]
       
        public IActionResult Auth([FromBody] AuthForm user)
        {
            Dictionary<string, object> retorno = new Dictionary<string, object>();
            using (var contexto = new ConnectionContext())
            {
                var query = contexto.Utilizador.FromSqlRaw("SELECT * FROM utilizador WHERE Email = {0} or Telemovel = {0}", user.Utilizador).FirstOrDefault();
                if (query != null) { 
                Utilizador utilizadorEncontrado = query;
                    if (BCrypt.Net.BCrypt.EnhancedVerify(user.palavrapasse, utilizadorEncontrado.PalavraPasse)) {
                        var token = Token.GerarToken(utilizadorEncontrado);
                        retorno.Add("token", token);
                        retorno.Add("valido", "Sim");
                        retorno.Add("utilizador", utilizadorEncontrado);
                        return Ok(retorno);
                    }

                   
                }
                
                  
            }

            retorno.Add("token", "");
            retorno.Add("valido", "Não");
            return Ok(retorno);
        }

        [HttpGet]
        [Route("gerarpw")]
        public IActionResult GerarPw(string password)
        {
            var senhah = BCrypt.Net.BCrypt.EnhancedHashPassword(password);
            return Ok(senhah);
        }

        [HttpPost]
        [Route("Verificar")]
        public IActionResult Verificar(string senha, string password)
        {
            if (BCrypt.Net.BCrypt.EnhancedVerify(senha, password))
            {
                // var token = Token.GerarToken(new Empregado());
                //return Ok(token);
               
                return Ok("Correto");
            }

            return BadRequest("Utilizador inválido. Tente novamente");

        }
    }
}
