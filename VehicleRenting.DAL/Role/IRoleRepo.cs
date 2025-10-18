using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRenting.Entities;

namespace VehicleRenting.DAL.Role
{
    public interface IRoleRepo
    {
        bool Create(RoleModel role);
        List<RoleModel> GetAll();
        RoleModel GetById(int id);
        bool Update(RoleModel role);
        bool Delete(int id);
    }
}
