using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VehicleRenting.BLL;
using VehicleRenting.Entities;
using VehicleRenting.UI.Filters;
using VehicleRenting.UI.Models.DTOs;

namespace VehicleRenting.UI.Controllers
{
    [AuthorizeSession]

    public class UserController : Controller
    {
        private readonly UserManager _userManager;
        private readonly IMapper _mapper;

        public UserController(UserManager userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        [AuthorizeSession("Admin")]
        public IActionResult Create()
        {
            ViewBag.Layout = "_AdminLayout";
            return View();
        }

        [HttpPost]
        [AuthorizeSession("Admin")]
        public IActionResult Create(UserModel user)
        {

            ViewBag.Layout = "_AdminLayout";
            if (_userManager.AddUser(user))
            {
                TempData["Success"] = "Kullanıcı başarıyla eklendi!";
            }
            else
            {
                TempData["Error"] = "Kullanıcı eklenemedi!";
            }

            return RedirectToAction("Create");
        }

        public IActionResult List()
        {
            var role = HttpContext.Session.GetString("Role");

            if (role == "Admin")
                ViewBag.Layout = "_AdminLayout";
            else
                ViewBag.Layout = "_Layout";

            var users = _userManager.GetAllUsers();
            var userDTOs = _mapper.Map<List<UserDTO>>(users);
            ViewBag.Role = role;

            return View(userDTOs);
        }


        [AuthorizeSession("Admin")]

        public IActionResult Delete(int id)
        {

            _userManager.DeleteUser(id);
            return RedirectToAction("List");
        }
    }
}
