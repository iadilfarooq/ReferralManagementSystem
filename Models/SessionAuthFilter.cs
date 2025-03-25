using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using ReferralManagementSystem.Repository.IRepository;

namespace ReferralManagementSystem.Filters
{
    public class SessionAuthFilter : IActionFilter
    {
        private readonly IUsersRepository _usersRepository;

        public SessionAuthFilter(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
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
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // No action needed after execution
        }
    }
}
