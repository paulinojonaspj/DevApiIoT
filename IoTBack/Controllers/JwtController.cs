using IOTBack.Configuracao;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace IOTBack.Controllers
{
    [ApiController]
    [Route("api/v1/jwt")]
    public class JwtController : Controller
    {
        private readonly IConfiguration _configuration;

        public JwtController(IConfiguration configuration)
        {
            this._configuration = new ConfigurationBuilder()
              .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .Build();
        }

        //Retorno com DTO, somente alguns campos, sem retornar a tabela
        [HttpGet]
        [Route("teste")]
        public IActionResult Teste()
        {
            string mensagemOriginal = "Server=94.46.180.24;Database=acessofa_iot;User Id=acessofa;Password=@K?1q7Q8vW2Ufo;TrustServerCertificate=true;";
            // Criptografa a mensagem
            string mensagemCriptografada = Key.Criptografar(mensagemOriginal);
            return Ok(mensagemCriptografada);
        }

        [HttpPost]
        [Route("encript")]
        public IActionResult Encript([FromForm] String texto)
        {
            string mensagemCriptografada = Key.Criptografar(texto);
            return Ok(mensagemCriptografada);
        }

        [HttpPost]
        [Route("decrypt")]
        public IActionResult Decrypt([FromForm] String texto)
        {
            if (texto == this._configuration["conexao:stringConnection"])
            {
               // return BadRequest("Não é possível expor os dados de conexão");
            }
            string mensagemDescriptografada = Key.Descriptografar(texto);
            Console.WriteLine("Mensagem Descriptografada:\n" + mensagemDescriptografada);
            return Ok(mensagemDescriptografada);
        }

        [HttpGet]
        [Route("gerarChaves")]
        public IActionResult gerarChaves()
        {

            using (var rsa = new RSACryptoServiceProvider(1024))
            {
                // Obter as chaves pública e privada em formato XML
                string chavePublicaXml = rsa.ToXmlString(false); // false indica chave pública
                string chavePrivadaXml = rsa.ToXmlString(true);  // true indica chave privada

                // Exibir as chaves geradas
                Console.WriteLine("Chave Pública:");
                Console.WriteLine(chavePublicaXml);

                Console.WriteLine("\nChave Privada:");
                Console.WriteLine(chavePrivadaXml);
                return Ok();
            }


        }
    }
}
