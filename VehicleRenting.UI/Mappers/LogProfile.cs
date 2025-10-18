using AutoMapper;
using VehicleRenting.Entities;
using VehicleRenting.UI.Mappers.Resolvers;
using VehicleRenting.UI.Models.DTOs;

namespace VehicleRenting.UI.Mappers
{
    public class LogProfile : Profile
    {
        public LogProfile()
        {
            CreateMap<LogModel, LogDTO>()
             .ForMember(dest => dest.VehicleName,
                        opt => opt.MapFrom<LogVehicleResolver>())
             .ForMember(dest=>dest.UserName,
             opt => opt.MapFrom<LogUserResolver>());
        }
    }
}
