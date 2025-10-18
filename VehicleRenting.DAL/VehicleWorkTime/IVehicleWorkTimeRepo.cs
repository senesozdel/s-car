using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRenting.Entities;

namespace VehicleRenting.DAL.VehicleWorkTime
{
    public interface IVehicleWorkTimeRepo
    {
        bool Create(VehicleWorkTimeModel workTime);
        List<VehicleWorkTimeModel> GetAll();
        VehicleWorkTimeModel GetById(int id);
        bool Update(VehicleWorkTimeModel workTime);
        bool Delete(int id);
        List<VehicleWorkTimeModel> GetByVehicleId(int vehicleId);
        List<VehicleWorkTimeModel> GetByDateRange(DateTime start, DateTime end);
    }
}
