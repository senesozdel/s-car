using Microsoft.AspNetCore.Mvc;
using VehicleRenting.BLL;
using VehicleRenting.Entities;
using VehicleRenting.UI.Models;
using VehicleRenting.UI.Models.DTOs;
using VehicleRenting.UI.Models.Enums;

namespace VehicleRenting.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager _userManager;

        public AccountController(UserManager userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.GetAllUsers()
                                        .FirstOrDefault(u => u.Username == model.Username && !u.IsDeleted);


                bool isPasswordValid = UserManager.VerifyPassword(model.Password, user.PasswordHash); 

                if (!isPasswordValid)
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                    return View(model);
                }


                HttpContext.Session.SetString("Role", ((RoleEnum)user.RoleId).ToString());
                HttpContext.Session.SetInt32("UserId", user.Id);
                HttpContext.Session.SetString("Username", user.Username);
                return RedirectToAction("Dashboard", "Home");
            }

            ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı.");
            return View(model);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterDTO model)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.GetAllUsers()
                                    .Any(u => u.Username == model.Username || u.Email == model.Email );

                if (user)
                {
                    ModelState.AddModelError("", "This username or email is already registered.");
                    return View(model);
                }

                _userManager.AddUser(new UserModel
                {
                    Username = model.Username,
                    PasswordHash = model.Password,
                    Email = model.Email,
                    RoleId = model.RoleId 
                });

                TempData["SuccessMessage"] = "Registration successful!";
                return RedirectToAction("Login");
            }

            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
