using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ReferralManagementSystem.Utilities
{
    public class AuthorizeRoleAttribute : ActionFilterAttribute
    {
        private readonly string[] _roles;

        public AuthorizeRoleAttribute(params string[] roles)
        {
            _roles = roles;
        }
//override method
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var role = context.HttpContext.Session.GetString("Role");

            // Check if the user is logged in and has the required role
            if (string.IsNullOrEmpty(role))
            {
                // Redirect to login if not authenticated
                context.Result = new RedirectToActionResult("Login", "Account", null);
                return;
            }

            if (!_roles.Any(r => r.Equals(role, StringComparison.OrdinalIgnoreCase)))
            {
                // Redirect to AccessDenied if the user lacks permission
                context.Result = new RedirectToActionResult("AccessDenied", "Account", new { message = "You do not have permission to access this page." });
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
