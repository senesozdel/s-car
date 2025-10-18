using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace VehicleRenting.UI.Filters
{
    public class AuthorizeSessionAttribute : ActionFilterAttribute
    {
        private readonly string? _role;

        public AuthorizeSessionAttribute(string? role = null)
        {
            _role = role;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var httpContext = context.HttpContext;
            var sessionRole = httpContext.Session.GetString("Role");

            if (string.IsNullOrEmpty(sessionRole))
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
                return;
            }

            if (!string.IsNullOrEmpty(_role) && sessionRole != _role)
            {
                context.Result = new RedirectToActionResult("AccessDenied", "Account", null);
                return;
            }

  
            base.OnActionExecuting(context);
        }
    }
}
