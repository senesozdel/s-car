using VehicleRenting.Entities;

namespace VehicleRenting.UI.Models.DTOs
{
    public class HomeDTO
    {
        public List<UserDTO> Users { get; set; }
        public List<VehicleDTO> Vehicles { get; set; }
        public List<LogDTO> Logs { get; set; } 
    }
}
