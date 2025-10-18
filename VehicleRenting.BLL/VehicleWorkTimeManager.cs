using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRenting.DAL.Log;
using VehicleRenting.DAL.VehicleWorkTime;
using VehicleRenting.Entities;

namespace VehicleRenting.BLL
{
    public class VehicleWorkTimeManager
    {
        private readonly IVehicleWorkTimeRepo _workTimeRepo;
        private readonly ILogRepo _logRepo;

        public VehicleWorkTimeManager(IVehicleWorkTimeRepo workTimeRepo,ILogRepo logRepo)
        {
            _workTimeRepo = workTimeRepo;
            _logRepo = logRepo;
        }

        public bool AddWorkTime(VehicleWorkTimeModel workTime)
        {
            _logRepo.AddLog(new LogModel()
            {
                UserId = workTime.UserId,
                VehicleId = workTime.VehicleId,
                Description = $"Added new work time",
                CreatedDate = DateTime.Now
            });


            return _workTimeRepo.Create(workTime);
        }
        public List<VehicleWorkTimeModel> GetAllWorkTimes() => _workTimeRepo.GetAll();
        public VehicleWorkTimeModel GetWorkTimeById(int id) => _workTimeRepo.GetById(id);
        public bool UpdateWorkTime(VehicleWorkTimeModel workTime)
        {

            _logRepo.AddLog(new LogModel()
            {
                UserId = workTime.UserId,
                VehicleId = workTime.VehicleId,
                Description = $"Updated work time",
                CreatedDate = DateTime.Now
            });

            return _workTimeRepo.Update(workTime);
        }
        public bool DeleteWorkTime(int id)
        {
            var workTime = _workTimeRepo.GetById(id);
            if (workTime != null)
            {
                _logRepo.AddLog(new LogModel()
                {
                    UserId = workTime.UserId,
                    VehicleId = workTime.VehicleId,
                    Description = $"Deleted work time",
                    CreatedDate = DateTime.Now
                });
            }
            return _workTimeRepo.Delete(id);
        } 
        public List<VehicleWorkTimeModel> GetByVehicleId(int vehicleId) => _workTimeRepo.GetByVehicleId(vehicleId);
        public List<VehicleWorkTimeModel> GetByDateRange(DateTime start, DateTime end) => _workTimeRepo.GetByDateRange(start, end);
    }
}

