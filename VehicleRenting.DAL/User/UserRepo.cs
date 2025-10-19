using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRenting.Entities;

namespace VehicleRenting.DAL.User
{
    public class UserRepo : BaseRepository, IUserRepo
    {

        public UserRepo(IConfiguration configuration) : base(configuration)
        {
        }

        public bool AddUser(UserModel user)
        {
            string query = "INSERT INTO [User] (Username, PasswordHash, Email, RoleId) VALUES (@Username, @PasswordHash, @Email, @RoleId,@CreatedDate,@IsDeleted)";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Username", user.Username);
                cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@RoleId", user.RoleId);
                cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@IsDeleted", false);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public List<UserModel> GetAllUsers()
        {
            List<UserModel> users = new List<UserModel>();
            string query = "SELECT * FROM [User] WHERE IsDeleted = 0";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new UserModel
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Username = reader["Username"].ToString(),
                            PasswordHash = reader["PasswordHash"].ToString(),
                            Email = reader["Email"].ToString(),
                            RoleId = Convert.ToInt32(reader["RoleId"]),
                            CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                            UpdatedDate = reader["UpdatedDate"] as DateTime?,
                            IsDeleted = Convert.ToBoolean(reader["IsDeleted"])
                        });
                    }
                }
            }
            return users;
        }

        public UserModel GetUserById(int id)
        {
            UserModel user = null;
            string query = "SELECT * FROM [User] WHERE Id = @Id AND IsDeleted = 0";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new UserModel
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Username = reader["Username"].ToString(),
                            PasswordHash = reader["PasswordHash"].ToString(),
                            Email = reader["Email"].ToString(),
                            RoleId = Convert.ToInt32(reader["RoleId"]),
                            CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                            UpdatedDate = reader["UpdatedDate"] as DateTime?,
                            IsDeleted = Convert.ToBoolean(reader["IsDeleted"])
                        };
                    }
                }
            }
            return user;
        }

        public bool UpdateUser(UserModel user)
        {
            string query = @"UPDATE [User] 
                             SET Username=@Username, PasswordHash=@PasswordHash, Email=@Email, RoleId=@RoleId, UpdatedDate=GETDATE() 
                             WHERE Id=@Id";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Username", user.Username);
                cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@RoleId", user.RoleId);
                cmd.Parameters.AddWithValue("@Id", user.Id);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool DeleteUser(int id)
        {
            string query = "UPDATE [User] SET IsDeleted=1, UpdatedDate=GETDATE() WHERE Id=@Id";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
