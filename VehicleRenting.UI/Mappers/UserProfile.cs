using AutoMapper;
using VehicleRenting.Entities;
using VehicleRenting.UI.Mappers.Resolvers;
using VehicleRenting.UI.Models.DTOs;

namespace VehicleRenting.UI.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserModel, UserDTO>()
             .ForMember(dest => dest.Role,
                        opt => opt.MapFrom<UserRoleResolver>());
        }
    }
}
