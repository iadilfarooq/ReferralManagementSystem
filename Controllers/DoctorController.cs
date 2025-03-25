using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ReferralManagementSystem.Models;
using ReferralManagementSystem.Repository;
using ReferralManagementSystem.Repository.IRepository;

namespace ReferralManagementSystem.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorRepository _doctorRepository;
        public DoctorController(IDoctorRepository doctorRepository)
        {

            _doctorRepository = doctorRepository;
        }
        public IActionResult Index()
        {
            var doctorDetail = _doctorRepository.GetDoctorDetails();
            return View(doctorDetail);
        }
        [HttpGet]
        public IActionResult Create() => View();
        [HttpPost]
        public IActionResult Create(DoctorDetails doctorDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _doctorRepository.CreateDoctorDetail(doctorDetails);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var data = _doctorRepository.GetDoctorDetails().FirstOrDefault(where => where.Id == id);
            return View(data);
        }
        [HttpPost]
        public IActionResult Edit(DoctorDetails doctorDetails)
        {
            _doctorRepository.UpdateDoctorDetail(doctorDetails);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var data = _doctorRepository.GetDoctorDetails().FirstOrDefault(where => where.Id == id);
            return View(data);
        }
        [HttpPost]
        public IActionResult Delete(DoctorDetails doctorDetails)
        {
            _doctorRepository.DeleteDoctorDetail(doctorDetails);
            return RedirectToAction("Index");
        }
    }
}
