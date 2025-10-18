using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRenting.Entities;

namespace VehicleRenting.DAL.Role
{
    public class RoleRepo : BaseRepository, IRoleRepo
    {
        public RoleRepo(IConfiguration configuration) : base(configuration)
        {
        }

        public bool Create(RoleModel role)
        {
            string query = "INSERT INTO Role (Name) VALUES (@Name)";
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Name", role.Name);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public List<RoleModel> GetAll()
        {
            List<RoleModel> roles = new List<RoleModel>();
            string query = "SELECT * FROM Role WHERE IsDeleted = 0";

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(query, conn);
            conn.Open();

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                roles.Add(new RoleModel
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                    UpdatedDate = reader["UpdatedDate"] as DateTime?,
                    IsDeleted = Convert.ToBoolean(reader["IsDeleted"])
                });
            }

            return roles;
        }

        public RoleModel GetById(int id)
        {
            RoleModel role = null;
            string query = "SELECT * FROM Role WHERE Id=@Id AND IsDeleted = 0";

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            conn.Open();

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                role = new RoleModel
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                    UpdatedDate = reader["UpdatedDate"] as DateTime?,
                    IsDeleted = Convert.ToBoolean(reader["IsDeleted"])
                };
            }
            return role;
        }

        public bool Update(RoleModel role)
        {
            string query = "UPDATE Role SET Name=@Name, UpdatedDate=GETDATE() WHERE Id=@Id";
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Name", role.Name);
            cmd.Parameters.AddWithValue("@Id", role.Id);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Delete(int id)
        {
            string query = "UPDATE Role SET IsDeleted=1, UpdatedDate=GETDATE() WHERE Id=@Id";
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}
