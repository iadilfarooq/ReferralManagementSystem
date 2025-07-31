namespace ReferralManagementSystem.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }
public string ErrorName{get; set;}
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
