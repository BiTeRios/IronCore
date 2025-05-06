using AutoMapper;
using IronCore.Domain.Entities.User;
using IronCore.Models;

namespace IronCore.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // DB → ViewModel
            CreateMap<ULoginData, LoginViewModel>();

            // ViewModel → DB
            CreateMap<LoginViewModel, ULoginData>();
        }
    }
}
