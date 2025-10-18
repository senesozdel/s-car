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
                    { "Kullan�c�", x => x.UserName },
                    { "Ara�", x => x.VehicleName },
                    { "A��klama", x => x.Description },
                    { "Tarih", x => x.CreatedDate.ToString("dd.MM.yyyy HH:mm") }
                };
                var logBytes = _excelExportService.ExportToExcel<LogDTO>(logDtos, "Loglar", logColumns);
                return File(logBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Logs_{DateTime.Now:yyyyMMddHHmm}.xlsx");

            case "vehicles":
                var vehicles = _vehicleManager.GetAllVehicles();
                var vehicleDTOs = _mapper.Map<List<VehicleDTO>>(vehicles);
                var vehicleColumns = new Dictionary<string, Func<VehicleDTO, object>>()
                {
                    { "Ara� Ad�", x => x.Name },
                    { "Plaka", x => x.PlateNumber },
                    { "Son G�ncelleyen", x => x.LastUpdatedUser }
                };
                var vehicleBytes = _excelExportService.ExportToExcel<VehicleDTO>(vehicleDTOs, "Ara�lar", vehicleColumns);
                return File(vehicleBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Vehicles_{DateTime.Now:yyyyMMddHHmm}.xlsx");

            case "users":
                var users = _userManager.GetAllUsers();
                var userDTOs = _mapper.Map<List<UserDTO>>(users);
                var userColumns = new Dictionary<string, Func<UserDTO, object>>()
                {
                    { "Kullan�c� Ad�", x => x.Username },
                    { "E-posta", x => x.Email }
                };

                var userBytes = _excelExportService.ExportToExcel<UserDTO>(userDTOs, "Kullan�c�lar", userColumns);
                return File(userBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Users_{DateTime.Now:yyyyMMddHHmm}.xlsx");

            default:
                return BadRequest("Ge�ersiz t�r parametresi. 'logs', 'vehicles' veya 'users' olmal�.");
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
