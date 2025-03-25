using System.ComponentModel.DataAnnotations;

namespace ReferralManagementSystem.Models
{
    public class Roles
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "Role Name cannot exceed 10 characters.")]
        public string RoleName { get; set; }
    }
}
