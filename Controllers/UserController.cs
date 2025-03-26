using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReferralManagementSystem.Models;
using ReferralManagementSystem.Repository;
using ReferralManagementSystem.Repository.IRepository;
using ReferralManagementSystem.Utilities;

namespace ReferralManagementSystem.Controllers
{
    [AuthorizeRole("2")]
    public class UserController : Controller
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IRolesRepository _rolesRepository;
      

        public UserController(IUsersRepository usersRepository, IRolesRepository rolesRepository)
        {
            _usersRepository = usersRepository;
            _rolesRepository = rolesRepository;
           
        }
        public async Task<IActionResult> Index()
        {
            var data= await _usersRepository.GetUsersDetailsAsync();
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var roles =  _rolesRepository.GetRoles();
            ViewBag.Roles = new SelectList(roles, "Id", "RoleName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Users users, IFormFile picture)
        {
            if (ModelState.IsValid)
            {
                await _usersRepository.CreateUserAsync(users, picture);
               return RedirectToAction("Index");
            }
            var roles = _rolesRepository.GetRoles();
            ViewBag.Roles = new SelectList(roles, "Id", "RoleName");
            return View(users);

           
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, IFormFile picture)
        {
          
          var userId = await _usersRepository.GetUserByIdAsync(id);
            var roles = _rolesRepository.GetRoles();
            ViewBag.Roles = new SelectList(roles, "Id", "RoleName",userId.RoleId);
            return View(userId);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Users users, IFormFile picture)
        {
            if (!ModelState.IsValid)
            {
                var roles =  _rolesRepository.GetRoles();
                ViewBag.Roles = new SelectList(roles, "Id", "RoleName", users.RoleId);
                return View(users);
            }

            await _usersRepository.UpdateUserAsync(users,picture);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {

            var userId = await _usersRepository.GetUserByIdAsync(id);
            var roles = _rolesRepository.GetRoles();
            ViewBag.Roles = new SelectList(roles, "Id", "RoleName", userId.RoleId);
            return View(userId);
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _usersRepository.DeleteUserAsync(id);
            return RedirectToAction("Index");
        }
    }
}
