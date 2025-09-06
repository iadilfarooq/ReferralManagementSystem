using ReferralManagementSystem.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReferralManagementSystem.Models
{
    public class PatientReferralForm
    {
        [Key]
        public int ReferralID { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Referred From cannot exceed 20 characters.")]
        public string ReferredFrom { get; set; }

        

        [Required]
        [StringLength(7, ErrorMessage = "Shift cannot exceed 7 characters.")]
        [RegularExpression("Morning|Evening|Night", ErrorMessage = "Shift must be either 'Morning', 'Evening', or 'Night'.")]
        public string Shift { get; set; }
        
        [Required]
        [StringLength(13, ErrorMessage = "Nature of Referral cannot exceed 13 characters.")]
        [RegularExpression("Emergency|Non Emergency", ErrorMessage = "Nature of Referral must be either 'Emergency' or 'Non Emergency'.")]
        public string NatureOfReferral { get; set; }

        
        [Required]
        [StringLength(20, ErrorMessage = "Patient Name cannot exceed 20 characters.")]
        public string PatientName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "SO_WO cannot exceed 20 characters.")]
        public string SO_WO { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Medical Record Number cannot exceed 20 characters.")]
        public string MedicalRecordNo { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        [StringLength(13, ErrorMessage = "CNIC cannot exceed 13 characters.")]
        public string CNIC { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        [StringLength(6, ErrorMessage = "Gender cannot exceed 6 characters.")]
        [RegularExpression("Male|Female|Other", ErrorMessage = "Gender must be 'Male', 'Female', or 'Other'.")]
        public string Gender { get; set; }

        [StringLength(11, ErrorMessage = "Phone Number cannot exceed 11 characters.")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Address cannot exceed 100 characters.")]
        public string Address { get; set; }

        [Required]
        [StringLength(13, ErrorMessage = "Triage cannot exceed 13 characters.")]
        [RegularExpression("Resuscitation|Emergency|Urgent|Non-Urgent|Semi-Urgent", ErrorMessage = "Triage must be one of the defined values.")]
        public string Triage { get; set; }

        [Required]
        [StringLength(12, ErrorMessage = "Response to Treatment cannot exceed 12 characters.")]
        [RegularExpression("Improved|Unchanged|Deteriorated", ErrorMessage = "Response to Treatment must be one of the defined values.")]
        public string ResponseToTreatment { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Patient Condition at Exit cannot exceed 25 characters.")]
        [RegularExpression("Alert|Respond to Verbal Command|Unconscious", ErrorMessage = "Patient Condition at Exit must be one of the defined values.")]
        public string PatientConditionAtExit { get; set; }

        public int? Pulse { get; set; }
        public int? BP { get; set; }
        public int? TEMP { get; set; }
        public int? RR { get; set; }
        public int? SPO2 { get; set; }

        [Required]
        [StringLength(8, ErrorMessage = "Stability cannot exceed 8 characters.")]
        [RegularExpression("Stable|Unstable", ErrorMessage = "Stability must be either 'Stable' or 'Unstable'.")]
        public string Stability { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Diagnosis cannot exceed 20 characters.")]
        public string Diagnosis { get; set; }

        [StringLength(50, ErrorMessage = "Complications cannot exceed 50 characters.")]
        public string Complications { get; set; }

        [StringLength(20, ErrorMessage = "Any Drug Allergy cannot exceed 20 characters.")]
        public string AnyDrugAllergy { get; set; }

        [StringLength(100, ErrorMessage = "Instruction cannot exceed 100 characters.")]
        public string Instruction { get; set; }

        [Required]
        [StringLength(26, ErrorMessage = "Referring Mode cannot exceed 26 characters.")]
        [RegularExpression("By Doctor|On Patient/Relative Request", ErrorMessage = "Referring Mode must be one of the defined values.")]
        public string ReferringMode { get; set; }

        [StringLength(100, ErrorMessage = "Reason of Referral cannot exceed 100 characters.")]
        public string ReasonOfRefer { get; set; }

        [Required]
        public DateTime DateIn { get; set; }

        [Required]
        public DateTime DateOut { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Referred From Department cannot exceed 20 characters.")]
        public string ReferredFromDepartment { get; set; }

        [Required]
        public int HospitalID { get; set; }
        [NotMapped]
        public string? HospitalName { get; set; }

        [Required]
        public int DoctorId { get; set; }
        [NotMapped]
        public string? DoctorName { get; set; }      

        [Required]
        public int UserId { get; set; }
        [NotMapped]
        public string? UserName { get; set; }


    }
}

