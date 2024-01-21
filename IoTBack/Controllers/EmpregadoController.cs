using AutoMapper;
using IOTBack.Configuracao;
using IOTBack.Model.Empregado;
using IOTBack.Model.Empregado.DTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace IOTBack.Controllers
{
    [ApiController]
    [Route("api/v1/empregado")]
    public class EmpregadoController : ControllerBase
    {
      private readonly IEmpregado _repository;
      private readonly IMapper _mapper;
      private readonly IConfiguration _configuration;

        public EmpregadoController(IEmpregado repository, IMapper mapper)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this._configuration = new ConfigurationBuilder()
              .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .Build();
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] EmpregadoViewModel view)
        {
            string filePath = "";
            if (view.Foto != null)
            {
                filePath = Path.Combine("Storage", view.Foto.FileName);
                using Stream fileStream  = new FileStream(filePath, FileMode.Create);
                view.Foto.CopyTo(fileStream);
            }

            var dado = new Empregado(view.Nome??"", view.Idade, view.Email ?? "", filePath);
            await _repository.Add(dado);
            return Ok();
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var dados = await _repository.GetAll();
            return Ok(dados);
        }

        [HttpDelete]
        public async Task<IActionResult> Remover(int id)
        {
            var dado = await _repository.Get(id);
            if (dado != null)
            {
                _ = _repository.Remover(dado);
                return Ok("Removido");
            }
            return BadRequest("Não removido");
        }

        [HttpGet]
        [Route("dto")]
        public async Task<IActionResult> GetDTO()
        {
            var dados = await _repository.GetDTO();
            return Ok(dados);
        }

        [HttpGet]
        [Route("paginacao")]
        public async Task<IActionResult> GetPaginacao(int pageNumber, int pageQuantity)
        {
            var dados = await _repository.GetPaginacao(pageNumber, pageQuantity);
            return Ok(dados);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var dado = await _repository.Get(id);
            if (dado != null) { 
                return Ok(dado);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("{id}/download")]
        public async Task<IActionResult> DownloadFotoAsync(int id)
        {
            var dado = await _repository.Get(id);
            if(dado != null && dado.Foto != null) {
                    var dataBytes = System.IO.File.ReadAllBytes(dado.Foto);
                    //return File(dataBytes, 'image/png', empregado.foto);
                    return File(dataBytes,"application/octer-stream", dado.Foto);
            }
               return NotFound(); 
        }

        //Retorno com DTO, somente alguns campos, sem retornar a tabela
        [HttpGet]
        [Route("{id}/dto")]
        public async Task<IActionResult> GetIdDTO(int id)
        {
            var dado = await _repository.Get(id);
            if (dado != null)
            {
                EmpregadoDTO dadoDTO = _mapper.Map<EmpregadoDTO>(dado);
                return Ok(dadoDTO);
            }

            return NotFound();
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


        //Retorno com DTO, somente alguns campos, sem retornar a tabela
        [HttpPost]
        [Route("decrypt")]
        public IActionResult Decrypt([FromForm] String texto)
        {
            if(texto== this._configuration["conexao:stringConnection"])
            {
                return BadRequest("Não é possível expor os dados de conexão");
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
