using AutoMapper;
using IOTBack.Model.Consumo;
using IOTBack.Model.Consumo.DTOs;
using IOTBack.Model.Empregado;
using IOTBack.Model.Empregado.DTOS;
using IOTBack.Model.Utilizador;
using IOTBack.Model.Utilizador.DTOS;

namespace IOTBack.Configuracao
{
    public class DomainToDTOMapping : Profile
    {
        public DomainToDTOMapping()
        {
            CreateMap<Empregado, EmpregadoDTO>().ReverseMap(); //Reverse permite receber ou enviar nos dois lados
            CreateMap<Utilizador, UtilizadorDTO>().ReverseMap(); //Reverse permite receber ou enviar nos dois lados
            CreateMap<Consumo, ConsumoDTO>().ReverseMap(); //Reverse permite receber ou enviar nos dois lados


            //Se tiver atributo com nomes diferentes 
            //CreateMap<Empregado, EmpregadoDTO>().ForMember(dest => dest.NomeDTO, map => map.MapFrom(orig => orig.nomeEntidade));
        }
    }
}
