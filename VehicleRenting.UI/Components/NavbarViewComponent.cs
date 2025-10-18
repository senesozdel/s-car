using Microsoft.AspNetCore.Mvc;

namespace VehicleRenting.UI.Components
{
    public class NavbarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var role = HttpContext.Session.GetString("Role");
            return View("Default", role);
        }
    }
}
