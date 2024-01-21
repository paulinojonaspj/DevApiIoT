using IOTBack.Configuracao;
using IOTBack.Model.Empregado;
using Microsoft.AspNetCore.Mvc;

namespace IOTBack.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : Controller
    {
        [HttpPost]
        public IActionResult Auth(string username, string password)
        {
            if (username == "paulino" && password == "1234") {
                var token = Token.GerarToken(new Empregado());
                return Ok(token);
            }
            
           return BadRequest("Utilizador inválido");

        }
    }
}
