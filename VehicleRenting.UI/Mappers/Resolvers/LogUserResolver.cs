using AutoMapper;
using VehicleRenting.BLL;
using VehicleRenting.Entities;
using VehicleRenting.UI.Models.DTOs;

namespace VehicleRenting.UI.Mappers.Resolvers
{
    public class LogUserResolver : IValueResolver<LogModel, LogDTO, string>
    {
        private readonly UserManager _userManager;

        public LogUserResolver(UserManager userManager)
        {
            _userManager = userManager;
        }

        public string Resolve(LogModel source, LogDTO destination, string destMember, ResolutionContext context)
        {
            var user = _userManager.GetUserById(source.UserId);
            return user?.Username ?? "Bilinmiyor";
        }
    }
}
