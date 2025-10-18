using AutoMapper;
using VehicleRenting.Entities;
using VehicleRenting.UI.Mappers.Resolvers;
using VehicleRenting.UI.Models.DTOs;

namespace VehicleRenting.UI.Mappers
{

    public class VehicleProfile : Profile
    {
        public VehicleProfile()
        {
            CreateMap<VehicleModel, VehicleDTO>()
             .ForMember(dest => dest.LastUpdatedUser,
                        opt => opt.MapFrom<LastUpdatedUserResolver>());
        }
    }
}
