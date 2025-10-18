using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRenting.Entities;

namespace VehicleRenting.DAL.Vehicle
{
    public interface IVehicleRepo
    {
        bool Create(VehicleModel vehicle);
        List<VehicleModel> GetAll();
        VehicleModel GetById(int id);
        bool Update(VehicleModel vehicle);
        bool Delete(int id, int? deletedBy);
    }
}
