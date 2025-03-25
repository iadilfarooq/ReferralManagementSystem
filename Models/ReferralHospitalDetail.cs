using System.ComponentModel.DataAnnotations;

namespace ReferralManagementSystem.Models
{
    public class ReferralHospitalDetail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Name cannot exceed 20 characters.")]
        public string Name { get; set; }

        [StringLength(20, ErrorMessage = "Head Name cannot exceed 20 characters.")]
        public string HeadName { get; set; }

        [StringLength(11, ErrorMessage = "Head Contact must be 11 characters.")]
        [Phone(ErrorMessage = "Please enter a valid contact number.")]
        public string HeadContact { get; set; }

        [StringLength(50, ErrorMessage = "Address cannot exceed 50 characters.")]
        public string Address { get; set; }
    }
}
