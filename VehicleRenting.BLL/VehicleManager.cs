using VehicleRenting.DAL.Log;
using VehicleRenting.DAL.Vehicle;
using VehicleRenting.Entities;

namespace VehicleRenting.BLL
{
    public class VehicleManager
    {
        private readonly IVehicleRepo _vehicleRepo;
        private readonly ILogRepo _logRepo;

        public VehicleManager(IVehicleRepo vehicleRepo, ILogRepo logRepo)
        {
            _vehicleRepo = vehicleRepo;
            _logRepo = logRepo;
        }

        public bool AddVehicle(VehicleModel vehicle)
        {

            return _vehicleRepo.Create(vehicle);
        } 
        public List<VehicleModel> GetAllVehicles() => _vehicleRepo.GetAll();
        public VehicleModel GetVehicleById(int id) => _vehicleRepo.GetById(id);
        public bool UpdateVehicle(VehicleModel vehicle)
        {
            return _vehicleRepo.Update(vehicle);
        } 
        public bool DeleteVehicle(int id, int? deletedBy)
        {
   
            return _vehicleRepo.Delete(id, deletedBy);
        }
    }
}
