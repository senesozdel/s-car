using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using VehicleRenting.BLL;
using VehicleRenting.Entities;
using VehicleRenting.UI.Filters;
using VehicleRenting.UI.Mappers;
using VehicleRenting.UI.Models.DTOs;

namespace VehicleRenting.UI.Controllers
{
    [AuthorizeSession]

    public class VehicleController : Controller
    {
        private readonly VehicleManager _vehicleManager;
        private readonly IMapper _mapper;


        public VehicleController(VehicleManager vehicleManager, IMapper mapper)
        {
            _vehicleManager = vehicleManager;
            _mapper = mapper;
        }
        [AuthorizeSession("Admin")]
        public IActionResult Create()
        {
            var role = HttpContext.Session.GetString("Role");

            ViewBag.Layout = role == "Admin" ? "_AdminLayout" : "_UserLayout";
            return View();
        }
        [AuthorizeSession("Admin")]

        [HttpPost]
        public IActionResult Create([FromBody] VehicleModel vehicle)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            vehicle.CreatedBy = userId;
    

            bool result = _vehicleManager.AddVehicle(vehicle);

            if (result)
                return Json(new { success = true, message = "Araç başarıyla eklendi!" });
            else
                return Json(new { success = false, message = "Araç eklenemedi!" });
        }


        public IActionResult List()
        {
            var role = HttpContext.Session.GetString("Role");

            ViewBag.Layout = role == "Admin" ? "_AdminLayout" : "_UserLayout";
            var vehicles = _vehicleManager.GetAllVehicles();

            var vehicleDTOs = _mapper.Map<List<VehicleDTO>>(vehicles);
            ViewBag.Role = role;


            return View(vehicleDTOs);
        }

        [AuthorizeSession("Admin")]
        public IActionResult Delete(int id)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            _vehicleManager.DeleteVehicle(id, userId); 
            return RedirectToAction("List");
        }

        [AuthorizeSession("Admin")]
        public IActionResult Edit(int id)
        {
            var vehicle = _vehicleManager.GetVehicleById(id);
            if (vehicle == null) return NotFound();
            return View(vehicle);
        }

        [AuthorizeSession("Admin")]

        [HttpPost]
        public IActionResult Edit(VehicleModel vehicle)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            vehicle.UpdatedBy = userId;
            if (_vehicleManager.UpdateVehicle(vehicle))
            {
                TempData["Success"] = "Araç başarıyla güncellendi!";
            }
            else
            {
                TempData["Error"] = "Güncelleme başarısız!";
            }
            return RedirectToAction("List");
        }
    }
}
