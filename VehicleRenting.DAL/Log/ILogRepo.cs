using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRenting.Entities;

namespace VehicleRenting.DAL.Log
{
    public interface ILogRepo
    {
        void AddLog(LogModel logModel);

        List<LogModel> GetAll();
        bool Delete(int id);
    }
}
