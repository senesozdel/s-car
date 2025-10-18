using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRenting.DAL.Role;
using VehicleRenting.Entities;

namespace VehicleRenting.BLL
{
    public class RoleManager
    {
        private readonly IRoleRepo _roleRepo;

        public RoleManager(IRoleRepo roleRepo)
        {
            _roleRepo = roleRepo;
        }

        public bool AddRole(RoleModel role) => _roleRepo.Create(role);
        public List<RoleModel> GetAllRoles() => _roleRepo.GetAll();
        public RoleModel GetRoleById(int id) => _roleRepo.GetById(id);
        public bool UpdateRole(RoleModel role) => _roleRepo.Update(role);
        public bool DeleteRole(int id) => _roleRepo.Delete(id);
    }
}
