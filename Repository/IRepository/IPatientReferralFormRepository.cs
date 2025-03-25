using ReferralManagementSystem.Models;

namespace ReferralManagementSystem.Repository.IRepository
{
    public interface IPatientReferralFormRepository
    {
        Task<List<PatientReferralForm>> GetPatientRFDetailsAsync();
        Task CreatePatientRFAsync(PatientReferralForm patientReferralForm);
        Task<PatientReferralForm> GetPatientRFIdAsync(int id);
        Task UpdatePatientRFAsync(PatientReferralForm patientReferralForm);
        Task DeletePatientRFAsync(int id);
    }
}