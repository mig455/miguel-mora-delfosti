using Aplicacion.DTO.SCode;
using AutoMapper;
using Persistencia.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Servicio.SCode
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>()
               .ForMember(dest => dest.Password, opt => opt.Ignore()) 
               .ForMember(dest => dest.Token, opt => opt.Ignore());
            CreateMap<UserDto, User>()
              .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()) 
              .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore());
            CreateMap<Project, ProjectDto>().ReverseMap();
            CreateMap<Tarea, TareaDto>().ReverseMap();
        }
    }
}
