using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using VehicleRenting.Entities;

namespace VehicleRenting.DAL.Vehicle
{
    public class VehicleRepo : BaseRepository, IVehicleRepo
    {
        public VehicleRepo(IConfiguration configuration) : base(configuration)
        {
        }

        public bool Create(VehicleModel vehicle)
        {
            string query = "INSERT INTO Vehicle (Name, PlateNumber, CreatedBy) VALUES (@Name, @PlateNumber, @CreatedBy,@CreatedDate,@IsDeleted)";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Name", vehicle.Name);
                cmd.Parameters.AddWithValue("@PlateNumber", vehicle.PlateNumber);
                cmd.Parameters.AddWithValue("@CreatedBy", vehicle.CreatedBy ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@IsDeleted", false);


                conn.Open();
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }

        public List<VehicleModel> GetAll()
        {
            List<VehicleModel> vehicles = new List<VehicleModel>();
            string query = "SELECT * FROM Vehicle WHERE IsDeleted = 0";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        vehicles.Add(new VehicleModel
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            PlateNumber = reader["PlateNumber"].ToString(),
                            CreatedBy = reader["CreatedBy"] as int?,
                            UpdatedBy = reader["UpdatedBy"] as int?,
                            DeletedBy = reader["DeletedBy"] as int?,
                            CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                            UpdatedDate = reader["UpdatedDate"] as DateTime?,
                            IsDeleted = Convert.ToBoolean(reader["IsDeleted"])
                        });
                    }
                }
            }

            return vehicles;
        }

        public VehicleModel GetById(int id)
        {
            VehicleModel vehicle = null;
            string query = "SELECT * FROM Vehicle WHERE Id = @Id AND IsDeleted = 0";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        vehicle = new VehicleModel
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            PlateNumber = reader["PlateNumber"].ToString(),
                            CreatedBy = reader["CreatedBy"] as int?,
                            UpdatedBy = reader["UpdatedBy"] as int?,
                            DeletedBy = reader["DeletedBy"] as int?,
                            CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                            UpdatedDate = reader["UpdatedDate"] as DateTime?,
                            IsDeleted = Convert.ToBoolean(reader["IsDeleted"])
                        };
                    }
                }
            }

            return vehicle;
        }

        public bool Update(VehicleModel vehicle)
        {
            string query = @"UPDATE Vehicle 
                             SET Name = @Name, PlateNumber = @PlateNumber, UpdatedBy = @UpdatedBy, UpdatedDate = GETDATE() 
                             WHERE Id = @Id";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Name", vehicle.Name);
                cmd.Parameters.AddWithValue("@PlateNumber", vehicle.PlateNumber);
                cmd.Parameters.AddWithValue("@UpdatedBy", vehicle.UpdatedBy ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Id", vehicle.Id);

                conn.Open();
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }

        public bool Delete(int id, int? deletedBy)
        {
            string query = "UPDATE Vehicle SET IsDeleted = 1, DeletedBy = @DeletedBy, UpdatedDate = GETDATE() WHERE Id = @Id";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@DeletedBy", deletedBy ?? (object)DBNull.Value);

                conn.Open();
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }
    }
}
