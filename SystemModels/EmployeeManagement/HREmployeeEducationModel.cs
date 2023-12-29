using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.EmployeeManagement
{
    [Table("HREmployeeEducation")]
    public class HREmployeeEducationModel : AuditableEntity<long>
    {
        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "डिग्री नाम")]
        public int IdHREmployeeEducationType { get; set; }

        [Display(Name = "कर्मचारी नाम")]
        public long? IdHREmployee { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस")]
        [Display(Name = "कलेज नाम")]
        [MaxLength(500)]
        public string CollegeName { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस")]
        [Display(Name = "कलेज ठेगाना")]
        [MaxLength(500)]
        public string CollegeAddress { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस")]
        [Display(Name = "संकाय")]
        [MaxLength(100)]
        public string DegreeName { get; set; }

        [Display(Name = "स्कोर")]
        [MaxLength(50)]
        public string Score { get; set; }

        [Display(Name = "शुरुको मिति")]
        [DataType(DataType.DateTime)]
        public Nullable<System.DateTime> StartedDate { get; set; }

        [Display(Name = "शुरुको मिति")]
        [DataType(DataType.DateTime)]
        public String StartedDateNp { get; set; }

        [Display(Name = "अन्तिम मिति")]
        [DataType(DataType.DateTime)]
        public Nullable<System.DateTime> EndDate { get; set; }

        [Display(Name = "अन्तिम मिति")]
        [DataType(DataType.DateTime)]
        public string EndDateNp { get; set; }

        [Display(Name = "अपलोड ट्रान्सक्रिप्ट")]
        [MaxLength(250)]
        public string Document { get; set; }

        [Display(Name = "अपलोड क्यारेक्टर सर्टिफिकेट")]
        [MaxLength(250)]
        public string Document1 { get; set; }

        [Display(Name = "अपलोड डकुमेन्ट")]
        [MaxLength(250)]
        public string Document2 { get; set; }

        [Display(Name = "अपलोड डकुमेन्ट")]
        [MaxLength(250)]
        public string Document3 { get; set; }
    }
}
