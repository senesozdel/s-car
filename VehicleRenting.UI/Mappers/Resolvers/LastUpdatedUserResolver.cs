using AutoMapper;
using VehicleRenting.BLL;
using VehicleRenting.Entities;
using VehicleRenting.UI.Models.DTOs;

namespace VehicleRenting.UI.Mappers.Resolvers
{
    public class LastUpdatedUserResolver : IValueResolver<VehicleModel, VehicleDTO, string>
    {
        private readonly UserManager _userService;

        public LastUpdatedUserResolver(UserManager userService)
        {
            _userService = userService;
        }

        public string Resolve(VehicleModel source, VehicleDTO destination, string destMember, ResolutionContext context)
        {
            if (source.UpdatedBy == null)
                return "Bilinmiyor";

            var user = _userService.GetUserById(source.UpdatedBy.Value); 
            return user?.Username ?? "Bilinmiyor";
        }
    }
}
