using Castle.MicroKernel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReferralManagementSystem.Models;
using ReferralManagementSystem.Repository;
using ReferralManagementSystem.Repository.IRepository;
using ReferralManagementSystem.Utilities;

namespace ReferralManagementSystem.Controllers
{
    [AuthorizeRole("1")]
    public class PatientRFController : Controller
    {
        private readonly IPatientReferralFormRepository _patientReferralFormRepository;
        private readonly IReferralHospitalDetailRepository _referralHospitalDetailRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IUsersRepository _usersRepository;

        public PatientRFController(IPatientReferralFormRepository patientReferralFormRepository, IReferralHospitalDetailRepository referralHospitalDetailRepository, IDoctorRepository doctorRepository, IUsersRepository usersRepository)
        {

            _patientReferralFormRepository = patientReferralFormRepository;
            _referralHospitalDetailRepository = referralHospitalDetailRepository;
            _doctorRepository = doctorRepository;
            _usersRepository = usersRepository;
        }
        public async Task<IActionResult> Index()
        {
            var patientRFDetail = await _patientReferralFormRepository.GetPatientRFDetailsAsync();
            var activePatient = patientRFDetail.Where(x => x.IsActive == true).ToList();
            return View(activePatient);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var refferelHosptialDetail = await _referralHospitalDetailRepository.GetHospitalAsync();
            ViewBag.RefferelHospitalDetail = new SelectList(refferelHosptialDetail, "Id", "Name");

            var doctorDetail = _doctorRepository.GetDoctorDetails();
            ViewBag.DoctorDetail = new SelectList(doctorDetail, "Id", "Name");

            var userDetail = await _usersRepository.GetUsersDetailsAsync();
            ViewBag.UserDetail = new SelectList(userDetail, "Id", "Username");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(PatientReferralForm patientReferralForm)
        {
        //if Role is not mapped in model class then no need here to remove modelstate for role
            //ModelState.Remove("Role");
            
            if (!ModelState.IsValid)
            {
                var refferelHosptialDetail = await _referralHospitalDetailRepository.GetHospitalAsync();
                ViewBag.RefferelHospitalDetail = new SelectList(refferelHosptialDetail, "Id", "Name");
                
                var doctorDetail = _doctorRepository.GetDoctorDetails();
                ViewBag.DoctorDetail = new SelectList(doctorDetail, "Id", "Name");

                var userDetail = await _usersRepository.GetUsersDetailsAsync();
                ViewBag.UserDetail = new SelectList(userDetail, "Id", "Username");

                return View(patientReferralForm);
            }
            await _patientReferralFormRepository.CreatePatientRFAsync(patientReferralForm);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var refferelHosptialDetail = await _referralHospitalDetailRepository.GetHospitalAsync();
            ViewBag.RefferelHospitalDetail = new SelectList(refferelHosptialDetail, "Id", "Name");

            var doctorDetail = _doctorRepository.GetDoctorDetails();
            ViewBag.DoctorDetail = new SelectList(doctorDetail, "Id", "Name");

            var userDetail = await _usersRepository.GetUsersDetailsAsync();
            ViewBag.UserDetail = new SelectList(userDetail, "Id", "Username");

            var dataById = await _patientReferralFormRepository.GetPatientRFIdAsync(id);
            return View(dataById);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(PatientReferralForm patientReferralForm)
        {
            if (!ModelState.IsValid)
            {
                var refferelHosptialDetail = await _referralHospitalDetailRepository.GetHospitalAsync();
                ViewBag.RefferelHospitalDetail = new SelectList(refferelHosptialDetail, "Id", "Name");

                var doctorDetail = _doctorRepository.GetDoctorDetails();
                ViewBag.DoctorDetail = new SelectList(doctorDetail, "Id", "Name");

                var userDetail = await _usersRepository.GetUsersDetailsAsync();
                ViewBag.UserDetail = new SelectList(userDetail, "Id", "Username");

                return View(patientReferralForm);
            }

            await _patientReferralFormRepository.UpdatePatientRFAsync(patientReferralForm);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _patientReferralFormRepository.GetPatientRFIdAsync(id);
            
            return View(data);
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            await _patientReferralFormRepository.DeletePatientRFAsync(id);
            return RedirectToAction("Index");
        }
    }
}
