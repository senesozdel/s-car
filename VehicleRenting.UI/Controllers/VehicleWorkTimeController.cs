using Microsoft.AspNetCore.Mvc;
using System.Data;
using VehicleRenting.BLL;
using VehicleRenting.Entities;
using VehicleRenting.UI.Filters;
using VehicleRenting.UI.Models;
using VehicleRenting.UI.Models.DTOs;

namespace VehicleRenting.UI.Controllers
{
    [AuthorizeSession]

    public class VehicleWorkTimeController : Controller
    {
        private readonly VehicleWorkTimeManager _workTimeManager;
        private readonly VehicleManager _vehicleManager;

        public VehicleWorkTimeController(VehicleWorkTimeManager workTimeManager, VehicleManager vehicleManager)
        {
            _workTimeManager = workTimeManager;
            _vehicleManager = vehicleManager;
        }

        public IActionResult Create()
        {
            var role = HttpContext.Session.GetString("Role");

            ViewBag.Vehicles = _vehicleManager.GetAllVehicles();
            ViewBag.Layout = role == "Admin" ? "_AdminLayout" : "_UserLayout";

            return View();
        }

        [HttpPost]
        public IActionResult Create([FromBody] VehicleWorkTimeModel model)
        {
            decimal totalWeekHours = 7 * 24; 
            model.IdleHours = totalWeekHours - (model.ActiveHours + model.MaintenanceHours);

            int? userId = HttpContext.Session.GetInt32("UserId");


            model.UserId = userId ?? 1;

            bool result = _workTimeManager.AddWorkTime(model);

            if (result)
            {
                return Json(new { success = true, message = "Çalışma süresi başarıyla kaydedildi!" });
            }
            else
            {
                return Json(new { success = false, message = "Kaydetme işlemi başarısız!" });
            }
        }

        public IActionResult List()
        {
            var workTimes = _workTimeManager.GetAllWorkTimes();
            var vehicles = _vehicleManager.GetAllVehicles();

            var chartData = vehicles.Select(v => new VehicleWorkTimeChartData
            {
                VehicleName = v.Name + " (" + v.PlateNumber + ")",
                ActiveHours = workTimes.Where(w => w.VehicleId == v.Id).Sum(w => w.ActiveHours),
                MaintenanceHours = workTimes.Where(w => w.VehicleId == v.Id).Sum(w => w.MaintenanceHours),
                IdleHours = workTimes.Where(w => w.VehicleId == v.Id).Sum(w => w.IdleHours)
            }).ToList();

            var role = HttpContext.Session.GetString("Role");

            if (role == "Admin")
            {
                ViewBag.Layout = "_AdminLayout";
            }
            else
            {
                ViewBag.Layout = "_UserLayout";
            }

            var model = new VehicleWorkTimeListDTO
            {
                WorkTimes = workTimes,
                Vehicles = vehicles,
                ChartData = chartData
            };

            return View(model);
        }


    }

}
