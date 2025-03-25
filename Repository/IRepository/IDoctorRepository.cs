using ReferralManagementSystem.Models;

namespace ReferralManagementSystem.Repository.IRepository
{
    public interface IDoctorRepository
    {
        List<DoctorDetails> GetDoctorDetails();
        void CreateDoctorDetail(DoctorDetails doctorDetails);
        void UpdateDoctorDetail(DoctorDetails doctorDetails);
        void DeleteDoctorDetail(DoctorDetails doctorDetails);
    }
}