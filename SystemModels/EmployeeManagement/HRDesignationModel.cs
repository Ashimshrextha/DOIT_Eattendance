using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.EmployeeManagement
{
    [Table("HRDesignation")]
    public class HRDesignationModel : AuditableEntity<long>
    {
        [Display(Name = "कार्यालय")]
        public long IdHRCompany { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "बिदा स्वीकृत गर्ने पद")]
        public Nullable<long> IdHRDesignationLeaveApproval { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "श्रेणी/तह")]
        public long IdHREmployeeCategory { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "पद शीर्षक")]
        [MaxLength(50)]
        public string HRDesignationTitle { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "पद शीर्षक (संक्षिप्त)")]
        [MaxLength(20)]
        public string HRDesignationShortName { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "पद क्रमश")]
        public int HRDesignationOrder { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "श्रेणी अनुसार")]
        public int HRDesignationRank { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "तलब")]
        public decimal BasicSalary { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "ग्रेड नं.")]
        public int SalaryIncrementNumber { get; set; }

        [Display(Name = "ग्रेड मूल्याङ्कन")]
        public decimal SalaryIncrementRate { get; set; }

        [Display(Name = "अल्लोवांस")]
        public Nullable<decimal> Allowance { get; set; }

        [Display(Name = "ओभरटाइम अल्लोवांस")]
        public Nullable<decimal> OverTimeAllowance { get; set; }

        [Display(Name = "ओभरटाइम/मिनेट")]
        public Nullable<decimal> RatePerMinute { get; set; }

        [Display(Name = "जम्मा तलब")]
        public decimal TotalSalary { get; set; }
    }
}
