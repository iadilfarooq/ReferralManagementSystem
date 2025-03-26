using Microsoft.AspNetCore.Mvc;
using ReferralManagementSystem.Utilities;
using ReferralManagementSystem.Repository.IRepository;


namespace ReferralManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUsersRepository _userRepository;

        public AccountController(IUsersRepository usersRepository)
        {
            _userRepository = usersRepository;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _userRepository.ValidateUser(username, password);

            if (user == null)
            {
                ViewBag.Error = "Invalid username or password.";
                return View();
            }

            // Store User Information in Session
            HttpContext.Session.SetString("Username", user["Username"].ToString());
            HttpContext.Session.SetString("Role", user["RoleId"].ToString());

            return RedirectToAction("Index", "Home");
        }

        // Logout: Clear Session

        [AuthorizeRole("1", "2")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public IActionResult AccessDenied(string message)
        {
            ViewBag.Message = message;
            return View();
        }
    }

}
