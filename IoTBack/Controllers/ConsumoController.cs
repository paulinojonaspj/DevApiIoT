using AutoMapper;
using IOTBack.Model.Consumo.DTOs;
using Microsoft.AspNetCore.Mvc;
using IOTBack.Model.Consumo;
using IOTBack.Model.Utilizador;
using IOTBack.Model.Utilizador.DTOS;
using IOTBack.Infraestrutura;
using Microsoft.EntityFrameworkCore;
using IOTBack.Model.Objetivo;

namespace IOTBack.Controllers
{
    [ApiController]
    [Route("api/v1/consumo")]
    public class ConsumoController : ControllerBase
    {
        private readonly IConsumo _repository;
        private readonly IUtilizador _repository_utilizador;
        private readonly IMapper _mapper;
        private readonly ConnectionContext _context = new ConnectionContext();

        public ConsumoController(IConsumo repository, IUtilizador repository_utilizador, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _repository_utilizador = repository_utilizador ?? throw new ArgumentNullException(nameof(repository_utilizador));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }       

       [HttpGet("{id?}/{pageNumber?}/{pageQuantity?}")]
        public async Task<IActionResult> Get([FromRoute] int id = 0, [FromRoute] int pageNumber = 1, [FromRoute] int pageQuantity = 500)
        {
            if (id != 0)
            {
                var dado = await _repository.Get(id);
                if (dado != null)
                {
                    ConsumoDTO dadoDTO = _mapper.Map<ConsumoDTO>(dado);
                    return Ok(dadoDTO);
                }

                return NotFound("Não Encontrado");
            }

            var dadosPaginacao = await _repository.GetPaginacao(pageNumber - 1, pageQuantity);
            List<ConsumoDTO> dadosPaginacaoDTO = _mapper.Map<List<ConsumoDTO>>(dadosPaginacao);
            return Ok(dadosPaginacaoDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] FConsumo view)
        {

            await Task.CompletedTask;
            var data = DateTime.Today;
            DateTime horaAtual = DateTime.Now;

            string horaFormatada = horaAtual.ToString("HH");
            var hora = int.Parse(horaFormatada);


            var dado =   _context.Consumo.FirstOrDefault(c => c.Hora == hora && c.Data == data && c.Tipo == view.Tipo && c.Localizacao == view.Localizacao);
                                   
            if (dado != null)
            {
                    dado.Quantidade += view.Quantidade;
                    _context.SaveChanges();
            }
            else
            {

               
                Consumo dados = new Consumo()
                {
                    Unidade = view.Tipo == "ENERGIA" ? "kWh" : "m3",
                    Quantidade = view.Quantidade,
                    Localizacao = view.Localizacao,
                    Tipo = view.Tipo,
                    Data = data,
                    Hora = hora
                };
                await _repository.Add(dados);
                return Ok("REGISTADO");

                return Ok(dados);
            }
            return Ok(view);
           /* Consumo dado = new Consumo();

            await _repository.Add(dado);
            return Ok(dado);
           */
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


    }
}
