using AutoMapper;
using VehicleRenting.BLL;
using VehicleRenting.Entities;
using VehicleRenting.UI.Models.DTOs;

namespace VehicleRenting.UI.Mappers.Resolvers
{
    public class LogVehicleResolver : IValueResolver<LogModel, LogDTO, string>
    {
        private readonly VehicleManager _vehicleManager;

        public LogVehicleResolver( VehicleManager vehicleManager)
        {
            _vehicleManager = vehicleManager;
        }

        public string Resolve(LogModel source, LogDTO destination, string destMember, ResolutionContext context)
        {
            var vehicle = _vehicleManager.GetVehicleById(source.VehicleId);

            return vehicle?.Name ?? "Bilinmiyor";
        }
    }
}
