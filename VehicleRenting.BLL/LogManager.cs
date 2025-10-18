using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRenting.DAL.Log;
using VehicleRenting.DAL.Role;
using VehicleRenting.Entities;

namespace VehicleRenting.BLL
{
    public class LogManager
    {
        private readonly ILogRepo _logRepo;

        public LogManager(ILogRepo logRepo)
        {
            _logRepo = logRepo;
        }

        public void AddLog(LogModel log) => _logRepo.AddLog(log);
        public List<LogModel> GetAllLogs() => _logRepo.GetAll();
        public bool DeleteLogs(int id) => _logRepo.Delete(id);
    }
}
