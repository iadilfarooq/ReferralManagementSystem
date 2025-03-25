using Microsoft.AspNetCore.Mvc;
using ReferralManagementSystem.Repository.IRepository;

namespace ReferralManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IRolesRepository _rolesRepository;
        public AccountController(IUsersRepository usersRepository, IRolesRepository rolesRepository)
        {
            _usersRepository = usersRepository;
            _rolesRepository = rolesRepository;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _usersRepository.AuthenticateAsync(username, password);
            if (user != null)
            {
                // Store user details in session
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetString("Email", user.Email);
                HttpContext.Session.SetString("RoleId", user.RoleId.ToString());

                return RedirectToAction("Index", "Home"); // Redirect after login
            }

            ViewBag.Error = "Invalid credentials!";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Remove session data on logout
            return RedirectToAction("Login");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
