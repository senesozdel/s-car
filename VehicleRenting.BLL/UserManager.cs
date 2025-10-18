using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using VehicleRenting.DAL.Log;
using VehicleRenting.DAL.User;
using VehicleRenting.Entities;

namespace VehicleRenting.BLL
{
    public class UserManager
    {
        private readonly IUserRepo _userRepo;
        private readonly ILogRepo _logRepo;

        public UserManager(IUserRepo userRepo,ILogRepo logRepo)
        {
            _userRepo = userRepo;
            _logRepo = logRepo;
        }

        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        public static bool VerifyPassword(string enteredPassword, string storedHash)
        {
            string hashedEnteredPassword = HashPassword(enteredPassword);
            return hashedEnteredPassword == storedHash;
        }

        public bool AddUser(UserModel user)
        {

            var userModel = new UserModel()
            {
                Username = user.Username,
                PasswordHash = HashPassword(user.PasswordHash),
                Email = user.Email,
                RoleId = user.RoleId,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false
            };

            return _userRepo.AddUser(userModel);
        } 
        public List<UserModel> GetAllUsers() => _userRepo.GetAllUsers();
        public UserModel GetUserById(int id) => _userRepo.GetUserById(id);

        public bool DeleteUser(int id)
        {

            return _userRepo.DeleteUser(id);
        } 
    }
}
