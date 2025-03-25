using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ReferralManagementSystem.Models
{
    public class Users
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Username cannot exceed 20 characters.")]
        public string Username { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Password cannot exceed 20 characters.")]
        public string PasswordHash { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Email cannot exceed 50 characters.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }
        public string? PicturePath { get; set; }

        [Required]
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        [NotMapped]
        public virtual Roles? Role { get; set; }
    }
}
