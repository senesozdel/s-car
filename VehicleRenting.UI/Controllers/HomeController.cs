using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VehicleRenting.BLL;
using VehicleRenting.UI.Models;
using VehicleRenting.UI.Models.DTOs;
using VehicleRenting.UI.Services;

namespace VehicleRenting.UI.Controllers;

public class HomeController : Controller
{
    private readonly LogManager _logManager;
    private readonly VehicleManager _vehicleManager ;
    private readonly UserManager _userManager;
    private readonly ExcelExportService _excelExportService;
    private readonly IMapper _mapper;

    public HomeController(LogManager logManager,IMapper mapper, VehicleManager vehicleManager, UserManager userManager,ExcelExportService excelExport)
    {
        _logManager = logManager;
        _mapper = mapper;
        _vehicleManager = vehicleManager;
        _userManager = userManager;
        _excelExportService = excelExport;
    }

    public IActionResult Index()
    {

        string role = HttpContext.Session.GetString("Role");

        if(role == "Admin")
        {
            return RedirectToAction("Dashboard");
        }

        return View();
    }

    public IActionResult Dashboard()
    {

        var users = _userManager.GetAllUsers();
        var userDTOs = _mapper.Map<List<UserDTO>>(users);

        var logs = _logManager.GetAllLogs();

        var logDtos =  _mapper.Map<List<LogDTO>>(logs).OrderByDescending(x => x.CreatedDate)
                     .ToList(); 
        var vehicles = _vehicleManager.GetAllVehicles();

        var vehicleDTOs = _mapper.Map<List<VehicleDTO>>(vehicles);

        HomeDTO homeDTO = new HomeDTO
        {
            Users = userDTOs,
            Logs = logDtos,
            Vehicles = vehicleDTOs
        };

            
        string activeUser = HttpContext.Session.GetString("Username");
        string role = HttpContext.Session.GetString("Role");

        if ( role == "Admin")
        {
            ViewBag.Layout = "_AdminLayout";
        }
        else
        {
            ViewBag.Layout = "_UserLayout";
        }

        ViewBag.UserName = activeUser;

        ViewBag.TotalVehicles = 128;
        ViewBag.DailyUsage = 68;
        ViewBag.PendingMaintenance = 12;
        ViewBag.ActiveHours = 1254;
        ViewBag.MaintenanceHours = 86;
        ViewBag.ActiveVehicles = 7;
        return View(homeDTO);
    }

    [HttpPost]
    public IActionResult ExportToExcel(string type)
    {
        switch (type?.ToLower())
        {
            case "logs":
                var logs = _logManager.GetAllLogs();
                var logDtos = _mapper.Map<List<LogDTO>>(logs);

                var logColumns = new Dictionary<string, Func<LogDTO, object>>()
                {
                    { "Kullanýcý", x => x.UserName },
                    { "Araç", x => x.VehicleName },
                    { "Açýklama", x => x.Description },
                    { "Tarih", x => x.CreatedDate.ToString("dd.MM.yyyy HH:mm") }
                };
                var logBytes = _excelExportService.ExportToExcel<LogDTO>(logDtos, "Loglar", logColumns);
                return File(logBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Logs_{DateTime.Now:yyyyMMddHHmm}.xlsx");

            case "vehicles":
                var vehicles = _vehicleManager.GetAllVehicles();
                var vehicleDTOs = _mapper.Map<List<VehicleDTO>>(vehicles);
                var vehicleColumns = new Dictionary<string, Func<VehicleDTO, object>>()
                {
                    { "Araç Adý", x => x.Name },
                    { "Plaka", x => x.PlateNumber },
                    { "Son Güncelleyen", x => x.LastUpdatedUser }
                };
                var vehicleBytes = _excelExportService.ExportToExcel<VehicleDTO>(vehicleDTOs, "Araçlar", vehicleColumns);
                return File(vehicleBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Vehicles_{DateTime.Now:yyyyMMddHHmm}.xlsx");

            case "users":
                var users = _userManager.GetAllUsers();
                var userDTOs = _mapper.Map<List<UserDTO>>(users);
                var userColumns = new Dictionary<string, Func<UserDTO, object>>()
                {
                    { "Kullanýcý Adý", x => x.Username },
                    { "E-posta", x => x.Email }
                };

                var userBytes = _excelExportService.ExportToExcel<UserDTO>(userDTOs, "Kullanýcýlar", userColumns);
                return File(userBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Users_{DateTime.Now:yyyyMMddHHmm}.xlsx");

            default:
                return BadRequest("Geçersiz tür parametresi. 'logs', 'vehicles' veya 'users' olmalý.");
        }
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
