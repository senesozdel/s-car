using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRenting.Entities;

namespace VehicleRenting.DAL.User
{
    public interface IUserRepo
    {
        bool AddUser(UserModel user);
        List<UserModel> GetAllUsers();
        UserModel GetUserById(int id);
        bool DeleteUser(int id);
    }
}
