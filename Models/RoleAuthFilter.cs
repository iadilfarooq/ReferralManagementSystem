using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using ReferralManagementSystem.Repository.IRepository;
using ReferralManagementSystem.Models;

namespace ReferralManagementSystem.Filters
{
    public class RoleAuthFilter : IActionFilter
    {
        private readonly IUsersRepository _usersRepository;
        private readonly string _requiredRole;

        public RoleAuthFilter(IUsersRepository usersRepository, string requiredRole)
        {
            _usersRepository = usersRepository;
            _requiredRole = requiredRole;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var session = context.HttpContext.Session;
            var userIdString = session.GetString("UserId");

            // Check if the user is logged in
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
                return;
            }

            // Fetch user from database
            var user = _usersRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
                return;
            }
           
            // Get user role
            string userRole = user.RoleId == 1 ? "Admin" : "User"; // Example role mapping

            // If user role does not match the required role, deny access
            if (!_requiredRole.Split(',').Contains(userRole, StringComparer.OrdinalIgnoreCase))
            {
                context.Result = new RedirectToActionResult("AccessDenied", "Account", null);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // No action needed after execution
        }
    }
}
