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

        public EmpregadoController(IEmpregado repository, IMapper mapper)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [Route("api/v1/teste")]
        [HttpGet]
        public async Task<IActionResult> Teste()
        {
            await Task.CompletedTask;

            return Ok("Esse é um teste Arduino HTTP");
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

        //[Authorize]
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
            var dados = await _repository.GetPaginacao(pageNumber-1, pageQuantity);
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

       

    }
}
