using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRenting.Entities;

namespace VehicleRenting.DAL.Log
{
    public class LogRepo : BaseRepository, ILogRepo
    {

        public LogRepo(IConfiguration configuration) : base(configuration)
        {
        }

        public void AddLog(LogModel logModel)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"INSERT INTO VehicleLogs (UserId, VehicleId, [Description], CreatedDate, IsDeleted)
                                 VALUES (@UserId, @VehicleId, @Description, GETDATE(), 0)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", logModel.UserId);
                    cmd.Parameters.AddWithValue("@VehicleId", logModel.VehicleId);
                    cmd.Parameters.AddWithValue("@Description", logModel.Description ?? (object)DBNull.Value);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<LogModel> GetAll()
        {
            List<LogModel> roles = new List<LogModel>();
            string query = "SELECT * FROM VehicleLogs WHERE IsDeleted = 0";

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(query, conn);
            conn.Open();

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                roles.Add(new LogModel
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    UserId = Convert.ToInt32(reader["UserId"]),
                    VehicleId = Convert.ToInt32(reader["VehicleId"]),
                    Description = reader["Description"].ToString(),
                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                    IsDeleted = Convert.ToBoolean(reader["IsDeleted"])
                });
            }

            return roles;
        }

        public bool Delete(int id)
        {
            string query = "UPDATE VehicleLogs SET IsDeleted=1 WHERE Id=@Id";
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}
