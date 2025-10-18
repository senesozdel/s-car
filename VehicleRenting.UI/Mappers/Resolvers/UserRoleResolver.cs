using AutoMapper;
using VehicleRenting.BLL;
using VehicleRenting.Entities;
using VehicleRenting.UI.Models.DTOs;

namespace VehicleRenting.UI.Mappers.Resolvers
{

    public class UserRoleResolver : IValueResolver<UserModel, UserDTO, string>
    {
        private readonly RoleManager _roleManager;

        public UserRoleResolver(RoleManager roleManager)
        {
            _roleManager = roleManager;
        }

        public string Resolve(UserModel source, UserDTO destination, string destMember, ResolutionContext context)
        {
            if (source.RoleId == null)
                return "Bilinmiyor";

            var role = _roleManager.GetRoleById(source.RoleId);
            return role?.Name ?? "Bilinmiyor";
        }
    }
}
