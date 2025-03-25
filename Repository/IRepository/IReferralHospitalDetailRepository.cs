using ReferralManagementSystem.Models;

namespace ReferralManagementSystem.Repository.IRepository
{
    public interface IReferralHospitalDetailRepository
    {
        Task<List<ReferralHospitalDetail>> GetHospitalAsync();
        Task CreateHospitalAsync(ReferralHospitalDetail referralHospitalDetail);
        Task DeleteHospitalAsync(ReferralHospitalDetail referralHospitalDetail);
        Task UpdateHospitalAsync(ReferralHospitalDetail referralHospitalDetail);
    }
}