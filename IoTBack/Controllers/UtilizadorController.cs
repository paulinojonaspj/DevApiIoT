using AutoMapper;
using IOTBack.Model.Empregado;
using IOTBack.Model.Empregado.DTOS;
using IOTBack.Model.Utilizador;
using IOTBack.Model.Utilizador.DTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;

namespace IOTBack.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("api/v1/utilizador")]
    public class UtilizadorController : ControllerBase
    {
        private readonly IUtilizador _repository;
        private readonly IMapper _mapper;

        public UtilizadorController(IUtilizador repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("{id?}/{pageNumber?}/{pageQuantity?}")]
        public async Task<IActionResult> Get([FromRoute]  int id=0, [FromRoute] int pageNumber = 1, [FromRoute] int pageQuantity = 500)
        {
            if (id!=0)
            {
                var dado = await _repository.Get(id);
                if (dado != null)
                {
                    UtilizadorDTO dadoDTO = _mapper.Map<UtilizadorDTO>(dado);
                    return Ok(dadoDTO);
                }

                return NotFound("Não Encontrado");
            }

            var dadosPaginacao = await _repository.GetPaginacao(pageNumber-1, pageQuantity);
            List<UtilizadorDTO> dadosPaginacaoDTO = _mapper.Map<List<UtilizadorDTO>>(dadosPaginacao);
            return Ok(dadosPaginacaoDTO);            
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] UtilizadorForm view)
        {
            string filePath = "";
            if (view.Ficheiro != null)
            {
                filePath = Path.Combine("Storage\\Utilizador", view.Ficheiro.FileName);
                using Stream fileStream = new FileStream(filePath, FileMode.Create);
                view.Ficheiro.CopyTo(fileStream);  
            }
            view.Foto = filePath;
            Utilizador dado = _mapper.Map<Utilizador>(view);
            await _repository.Add(dado);
            return Ok(dado);
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
            return NotFound("Não removido");
        }

        [HttpPost]
        [Route("{id}/download")]
        public async Task<IActionResult> DownloadFotoAsync(int id)
        {
            var dado = await _repository.Get(id);
            if (dado != null && dado.Foto != null)
            {
                var dataBytes = System.IO.File.ReadAllBytes(dado.Foto);
                return File(dataBytes, "application/octer-stream", dado.Foto);
            }
            return NotFound("Ficheiro não encontrado");
        }


    }
}
