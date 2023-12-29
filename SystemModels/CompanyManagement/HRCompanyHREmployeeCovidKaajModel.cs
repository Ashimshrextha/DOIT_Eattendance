using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.CompanyManagement
{
    [Table("HREmployeeKaajHistory")]
    public class HRCompanyHREmployeeCovidKaajModel : AuditableEntity<long>
    {

        [Display(Name = "कर्मचारी नाम")]
        [MaxLength(250)]
        public string EmployeeName { get; set; }

        [Display(Name = "कार्यालय")]
        public long IdHrCompany { get; set; }

        [Display(Name = "जागिर")]
        public long IdJob { get; set; }

        [Display(Name = "कर्मचारी नाम")]
        public long IdHREmployee { get; set; }

        [Display(Name = "सिफारिस गर्ने")]
        public Nullable<long> IdRecommendedBy { get; set; }

        [Required(ErrorMessage = "{0} चयन गर्नुहोस्")]
        [Range(1, double.PositiveInfinity, ErrorMessage = "{0} चयन गर्नुहोस्")]
        [Display(Name = "स्वीकृत गर्ने")]
        public Nullable<long> IdApprovedBy { get; set; }

        [Display(Name = "आर्थिक वर्ष")]
        public string FiscalYear { get; set; }

        [Display(Name = "दर्ता नं.")]
        public string KaajRegistrationNumber { get; set; }

        [Display(Name = "काज/तालिम शीर्षक")]
        [MaxLength(250)]
        public string KaajTitle { get; set; } = "कोभिड-काज";

        [Display(Name = "काज/तालिम शीर्षक (संक्षिप्त)")]
        [MaxLength(10)]
        public string KaajShortName { get; set; } = "कोभिड-काज";

        [MaxLength(500)]
        [Display(Name = "काज/तालिम स्थान")]
        public string KaajLocation { get; set; } = "कार्यालय";

        [Display(Name = "काउन्टर")]
        public int KaajTakenNumber { get; set; }

        [MaxLength(1000)]
        [Display(Name = "काज/तालिम उद्देश्य")]
        public string KaajPurpose { get; set; } = "कोभिड-काज";

        [Display(Name = "सवारी साधन")]
        [MaxLength(500)]
        public string VehicleUsedInKaaj { get; set; } = "None";

        [Display(Name = "काज/तालिम खर्च")]
        public bool KaajExpenses { get; set; } = true;

        [DataType(DataType.Date)]
        [Display(Name = "शुरुको मिति")]
        public Nullable<DateTime> KaajFromDate { get; set; }

        
        [Required(ErrorMessage = "{0} चयन गर्नुहोस्")]
        [Display(Name = "शुरुको मिति")]
        [MaxLength(10)]
        public string KaajFromNP { get; set; }

        [Display(Name = "अन्तिम मिति")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> KaajToDate { get; set; }


        [Display(Name = "अन्तिम मिति")]
        public string KaajToNP { get; set; } 

        [Display(Name = "आउने समय")]
        [DataType(DataType.Time)]
        public Nullable<System.TimeSpan> CheckInTime { get; set; }

        [Display(Name = "जाने समय")]
        [DataType(DataType.Time)]
        public Nullable<System.TimeSpan> CheckOutTime { get; set; }

        [Display(Name = "अवस्था")]
        public Nullable<int> IdKaajStatus { get; set; } = 3;

        [Display(Name = "सिफारिस मिति")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> RecommendedOn { get; set; }

        [Display(Name = "सिफारिस गर्ने")]
        public string RecommendationBy { get; set; }

        [Display(Name = "सिफारिश टिप्पणी")]
        public string RemarkRecommended { get; set; }

        [Display(Name = "स्वीकृत मिति")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> ApprovedOn { get; set; } = DateTime.Now;

        [Display(Name = "स्वीकृत गर्ने")]
        [MaxLength(250)]
        public string ApprovedBy { get; set; }

        [Display(Name = "स्वीकृत टिप्पणी")]
        [MaxLength(500)]
        public string RemarkApprovedBy { get; set; }

        [Display(Name = "अपलोड फारम")]
        public string Document { get; set; }

        [Display(Name = "अपलोड फारम")]
        public string Document1 { get; set; }

        [Display(Name = "अपलोड फारम")]
        public string Document2 { get; set; }

        [Required(ErrorMessage = "{0} चयन गर्नुहोस्")]
        [Display(Name = "राष्ट्रिय हो/होइन ?")]
        public bool IsNational { get; set; } = true;

        [Display(Name = "वर्ष")]
        public int KaajYearNP { get; set; } 

        [Display(Name = "देश")]
        public int IdCountry { get; set; } = 1;


        [Display(Name = "प्रकार")]
        public int IdKaajType { get; set; } 









    }
}
