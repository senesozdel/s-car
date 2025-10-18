using Microsoft.Extensions.Configuration;
using System;

namespace VehicleRenting.DAL
{
    public abstract class BaseRepository
    {
        protected readonly string _connectionString;

        protected BaseRepository(IConfiguration configuration)
        {
            // 1️⃣ Önce environment variable'dan oku (Render gibi ortamlarda)
            var envConnection = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");

            // 2️⃣ Eğer env yoksa (örneğin localde) appsettings.json'dan al
            _connectionString = !string.IsNullOrEmpty(envConnection)
                ? envConnection
                : configuration.GetConnectionString("DefaultConnection");
        }
    }
}
