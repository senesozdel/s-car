using Microsoft.Extensions.Configuration;
using System;

namespace VehicleRenting.DAL
{
    public abstract class BaseRepository
    {
        protected readonly string _connectionString;

        protected BaseRepository(IConfiguration configuration)
        {
            var envConnection = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");

            _connectionString = !string.IsNullOrEmpty(envConnection)
                ? envConnection
                : configuration.GetConnectionString("DefaultConnection");
        }
    }
}
