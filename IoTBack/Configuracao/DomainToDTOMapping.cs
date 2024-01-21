﻿using AutoMapper;
using IOTBack.Model.Empregado;
using IOTBack.Model.Empregado.DTOS;

namespace IOTBack.Configuracao
{
    public class DomainToDTOMapping : Profile
    {
        public DomainToDTOMapping()
        {
            CreateMap<Empregado, EmpregadoDTO>().ReverseMap(); //Reverse permite receber ou enviar nos dois lados

            //Se tiver atributo com nomes diferentes 
            //CreateMap<Empregado, EmpregadoDTO>().ForMember(dest => dest.NomeDTO, map => map.MapFrom(orig => orig.nomeEntidade));
        }
    }
}
