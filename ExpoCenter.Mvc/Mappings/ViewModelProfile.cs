using AutoMapper;
using ExpoCenter.Dominio.Entidades;
using ExpoCenter.Mvc.Models;

namespace ExpoCenter.Mvc.Mappings
{
    public class ViewModelProfile : Profile
    {
        public ViewModelProfile()
        {
            CreateMap<Participante, ParticipanteIndexViewModel>().ReverseMap();
        }
    }
}
