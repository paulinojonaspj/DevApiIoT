using AutoMapper;
using IOTBack.Model.Empregado;
using IOTBack.Model.Empregado.DTOS;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace IOTBack.Controllers
{
    [ApiController]
    [Route("api/v1/empregado")]
    public class EmpregadoController : ControllerBase
    {
      private readonly IEmpregado _empregadoRepository;
      private readonly IMapper _mapper;

        public EmpregadoController(IEmpregado empregadoRepository, IMapper mapper)
        {
            _empregadoRepository = empregadoRepository ?? throw new ArgumentNullException(nameof(empregadoRepository));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        public IActionResult Add([FromForm] EmpregadoViewModel empregadoView)
        {
            var filePath = Path.Combine("Storage", empregadoView.Foto.FileName);
            using Stream fileStream  = new FileStream(filePath, FileMode.Create);
            empregadoView.Foto.CopyTo(fileStream);     
                
            var empregado = new Empregado(empregadoView.Nome, empregadoView.Idade, empregadoView.Email, filePath);
            _empregadoRepository.Add(empregado);
            return Ok();
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var empregados = _empregadoRepository.GetAll();
            return Ok(empregados);
        }

        [HttpGet]
        [Route("dto")]
        public IActionResult GetDTO()
        {
            var empregados = _empregadoRepository.GetDTO();
            return Ok(empregados);
        }

        [HttpGet]
        [Route("paginacao")]
        public IActionResult GetPaginacao(int pageNumber, int pageQuantity)
        {
            var empregados = _empregadoRepository.GetPaginacao(pageNumber, pageQuantity);
            return Ok(empregados);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var empregado = _empregadoRepository.Get(id);
            if (empregado != null) { 
                return Ok(empregado);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("{id}/download")]
        public IActionResult DownloadFoto(int id)
        {
            var empregado = _empregadoRepository.Get(id);
            if(empregado != null && empregado.Foto != null) {
                    var dataBytes = System.IO.File.ReadAllBytes(empregado.Foto);
                    //return File(dataBytes, 'image/png', empregado.foto);
                    return File(dataBytes,"application/octer-stream",empregado.Foto);
            }
               return NotFound(); 
        }

        //Retorno com DTO, somente alguns campos, sem retornar a tabela
        [HttpGet]
        [Route("{id}/dto")]
        public IActionResult GetIdDTO(int id)
        {
            var empregado = _empregadoRepository.Get(id);
            if (empregado != null)
            {
                EmpregadoDTO empregadoDTO = _mapper.Map<EmpregadoDTO>(empregado);
                return Ok(empregadoDTO);
            }

            return NotFound();
        }

    }
}
