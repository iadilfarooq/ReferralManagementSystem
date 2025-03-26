using Microsoft.AspNetCore.Mvc;
using ReferralManagementSystem.Models;
using ReferralManagementSystem.Repository;
using ReferralManagementSystem.Repository.IRepository;
using ReferralManagementSystem.Utilities;

namespace ReferralManagementSystem.Controllers
{
    [AuthorizeRole("2")]
    public class HospitalController : Controller
    {
        private readonly IReferralHospitalDetailRepository _referralHospitalDetailRepository;
        public HospitalController(IReferralHospitalDetailRepository referralHospitalDetailRepository)
        {

            _referralHospitalDetailRepository = referralHospitalDetailRepository;
        }
        public async Task<IActionResult> Index()
        {
            var hospitalDetail = await _referralHospitalDetailRepository.GetHospitalAsync();
            return View(hospitalDetail);
        }

        [HttpGet]
        public IActionResult Create() => View();
        [HttpPost]
        public async Task<IActionResult> Create(ReferralHospitalDetail referralHospitalDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _referralHospitalDetailRepository.CreateHospitalAsync(referralHospitalDetail);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var data = await _referralHospitalDetailRepository.GetHospitalAsync();
            var hospitalById = data.Find(x => x.Id == id);
            return View(hospitalById);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ReferralHospitalDetail referralHospitalDetail)
        {
            await _referralHospitalDetailRepository.UpdateHospitalAsync(referralHospitalDetail);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _referralHospitalDetailRepository.GetHospitalAsync();
            var hospitalById = data.Find(x => x.Id == id);
            return View(hospitalById);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(ReferralHospitalDetail referralHospitalDetail)
        {
            await _referralHospitalDetailRepository.DeleteHospitalAsync(referralHospitalDetail);
            return RedirectToAction("Index");
        }
    }
}
