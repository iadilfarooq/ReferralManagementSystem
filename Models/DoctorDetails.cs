using System.ComponentModel.DataAnnotations;

namespace ReferralManagementSystem.Models
{
    public class DoctorDetails
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Name cannot exceed 20 characters.")]
        public string Name { get; set; }
       
        [StringLength(20, ErrorMessage = "Department cannot exceed 20 characters.")]
        public string? Department { get; set; }
        
        [Required]
        [StringLength(20, ErrorMessage = "Designation cannot exceed 20 characters.")]
        public string Designation { get; set; }

 

        "public int number{get; set;}"
        [Required]
        [StringLength(11, ErrorMessage = "Phone Number must be 11 characters.")]
        [Phone(ErrorMessage = "Please enter a valid phone number.")]
        public string PhoneNumber { get; set; }
    }
}
