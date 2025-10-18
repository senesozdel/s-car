using VehicleRenting.Entities;

namespace VehicleRenting.UI.Models.DTOs
{
    public class VehicleWorkTimeListDTO
    {
        public List<VehicleWorkTimeModel> WorkTimes { get; set; }
        public List<VehicleModel> Vehicles { get; set; }
        public List<VehicleWorkTimeChartData> ChartData { get; set; } 
    }
}
