using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using VehicleRenting.Entities;

namespace VehicleRenting.DAL.VehicleWorkTime
{
    public class VehicleWorkTimeRepo : BaseRepository, IVehicleWorkTimeRepo
    {
        public VehicleWorkTimeRepo(IConfiguration configuration) : base(configuration)
        {
        }

        public bool Create(VehicleWorkTimeModel workTime)
        {
            string query = @"INSERT INTO VehicleWorkTime 
                             (VehicleId, UserId,  ActiveHours, MaintenanceHours,IdleHours,CreatedDate,IsDeleted) 
                             VALUES (@VehicleId, @UserId, @ActiveHours, @MaintenanceHours,@IdleHours,GETDATE(), 0)";

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@VehicleId", workTime.VehicleId);
            cmd.Parameters.AddWithValue("@UserId", workTime.UserId);
            cmd.Parameters.AddWithValue("@ActiveHours", workTime.ActiveHours);
            cmd.Parameters.AddWithValue("@MaintenanceHours", workTime.MaintenanceHours);
            cmd.Parameters.AddWithValue("@IdleHours", workTime.IdleHours);




            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public List<VehicleWorkTimeModel> GetAll()
        {
            List<VehicleWorkTimeModel> list = new List<VehicleWorkTimeModel>();
            string query = "SELECT * FROM VehicleWorkTime WHERE IsDeleted = 0";

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(query, conn);
            conn.Open();

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(ReadFromReader(reader));
            }
            return list;
        }

        public VehicleWorkTimeModel GetById(int id)
        {
            VehicleWorkTimeModel workTime = null;
            string query = "SELECT * FROM VehicleWorkTime WHERE Id=@Id AND IsDeleted=0";

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            conn.Open();

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                workTime = ReadFromReader(reader);
            }
            return workTime;
        }

        public bool Update(VehicleWorkTimeModel workTime)
        {
            string query = @"UPDATE VehicleWorkTime 
                             SET ActiveHours=@ActiveHours, MaintenanceHours=@MaintenanceHours, UpdatedDate=GETDATE() 
                             WHERE Id=@Id";

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@ActiveHours", workTime.ActiveHours);
            cmd.Parameters.AddWithValue("@MaintenanceHours", workTime.MaintenanceHours);
            cmd.Parameters.AddWithValue("@Id", workTime.Id);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Delete(int id)
        {
            string query = "UPDATE VehicleWorkTime SET IsDeleted=1, UpdatedDate=GETDATE() WHERE Id=@Id";
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public List<VehicleWorkTimeModel> GetByVehicleId(int vehicleId)
        {
            List<VehicleWorkTimeModel> list = new List<VehicleWorkTimeModel>();
            string query = "SELECT * FROM VehicleWorkTime WHERE VehicleId=@VehicleId AND IsDeleted=0";

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@VehicleId", vehicleId);
            conn.Open();

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(ReadFromReader(reader));
            }
            return list;
        }

        public List<VehicleWorkTimeModel> GetByDateRange(DateTime start, DateTime end)
        {
            List<VehicleWorkTimeModel> list = new List<VehicleWorkTimeModel>();
            string query = "SELECT * FROM VehicleWorkTime WHERE WorkDate BETWEEN @Start AND @End AND IsDeleted=0";

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Start", start);
            cmd.Parameters.AddWithValue("@End", end);
            conn.Open();

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(ReadFromReader(reader));
            }
            return list;
        }

        private VehicleWorkTimeModel ReadFromReader(SqlDataReader reader)
        {
            return new VehicleWorkTimeModel
            {
                Id = Convert.ToInt32(reader["Id"]),
                VehicleId = Convert.ToInt32(reader["VehicleId"]),
                UserId = Convert.ToInt32(reader["UserId"]),
                ActiveHours = Convert.ToDecimal(reader["ActiveHours"]),
                MaintenanceHours = Convert.ToDecimal(reader["MaintenanceHours"]),
                IdleHours = Convert.ToDecimal(reader["IdleHours"]),
                CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                UpdatedDate = reader["UpdatedDate"] as DateTime?,
                IsDeleted = Convert.ToBoolean(reader["IsDeleted"])
            };
        }
    }
}
