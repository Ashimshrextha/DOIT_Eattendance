using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.EmployeeManagement
{
    [Table("HREmployeeKaajHistory")]
    public class HREmployeeKaajHistoryModel : AuditableEntity<long>
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

        [Required(ErrorMessage = "{0} लेख्नुहोस्")]
        [Display(Name = "काज/तालिम शीर्षक")]
        [MaxLength(250)]
        public string KaajTitle { get; set; } = "काज";

        [Required(ErrorMessage = "{0} लेख्नुहोस्")]
        [Display(Name = "काज/तालिम शीर्षक (संक्षिप्त)")]
        [MaxLength(10)]
        public string KaajShortName { get; set; } = "काज";

        [Required(ErrorMessage = "{0} लेख्नुहोस्")]
        [MaxLength(500)]
        [Display(Name = "काज/तालिम स्थान")]
        public string KaajLocation { get; set; }

        [Display(Name = "काउन्टर")]
        public int KaajTakenNumber { get; set; }

        [Required(ErrorMessage = "{0} लेख्नुहोस्")]
        [MaxLength(1000)]
        [Display(Name = "काज/तालिम उद्देश्य")]
        public string KaajPurpose { get; set; }

        [Display(Name = "सवारी साधन")]
        [MaxLength(500)]
        public string VehicleUsedInKaaj { get; set; }

        [Required(ErrorMessage = "कृपया {0} चयन गर्नुहोस्")]
        [Display(Name = "काज/तालिम खर्च")]
        public bool KaajExpenses { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "शुरुको मिति")]
        public Nullable<DateTime> KaajFromDate { get; set; }

        //[RegularExpression("((([0-9][0-9][0-9][1-9])|([1-9][0-9][0-9][0-9])|([0-9][1-9][0-9][0-9])|([0-9][0-9][1-9][0-9]))-((0[13578])|(1[02]))-((0[1-9])|([12][0-9])|(3[01])))|((([0-9][0-9][0-9][1-9])|([1-9][0-9][0-9][0-9])|([0-9][1-9][0-9][0-9])|([0-9][0-9][1-9][0-9]))-((0[469])|11)-((0[1-9])|([12][0-9])|(30)))|(((000[48])|([0-9]0-9)|([0-9][1-9][02468][048])|([1-9][0-9][02468][048]))-02-((0[1-9])|([12][0-9])))|((([0-9][0-9][0-9][1-9])|([1-9][0-9][0-9][0-9])|([0-9][1-9][0-9][0-9])|([0-9][0-9][1-9][0-9]))-02-((0[1-9])|([1][0-9])|([2][0-8])))", ErrorMessage = "{0} को ढाँचा मिलेन (YYYY-MM-DD) ")]
        [Required(ErrorMessage = "{0} चयन गर्नुहोस्")]
        [Display(Name = "शुरुको मिति")]
        [MaxLength(10)]
        public string KaajFromNP { get; set; }

        [Display(Name = "अन्तिम मिति")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> KaajToDate { get; set; }

        //[RegularExpression("((([0-9][0-9][0-9][1-9])|([1-9][0-9][0-9][0-9])|([0-9][1-9][0-9][0-9])|([0-9][0-9][1-9][0-9]))-((0[13578])|(1[02]))-((0[1-9])|([12][0-9])|(3[01])))|((([0-9][0-9][0-9][1-9])|([1-9][0-9][0-9][0-9])|([0-9][1-9][0-9][0-9])|([0-9][0-9][1-9][0-9]))-((0[469])|11)-((0[1-9])|([12][0-9])|(30)))|(((000[48])|([0-9]0-9)|([0-9][1-9][02468][048])|([1-9][0-9][02468][048]))-02-((0[1-9])|([12][0-9])))|((([0-9][0-9][0-9][1-9])|([1-9][0-9][0-9][0-9])|([0-9][1-9][0-9][0-9])|([0-9][0-9][1-9][0-9]))-02-((0[1-9])|([1][0-9])|([2][0-8])))", ErrorMessage = "{0} को ढाँचा मिलेन (YYYY-MM-DD) ")]
        [Required(ErrorMessage = "{0} चयन गर्नुहोस्")]
        [MaxLength(10)]
        [Display(Name = "अन्तिम मिति")]
        public string KaajToNP { get; set; }

        [Display(Name = "आउने समय")]
        [DataType(DataType.Time)]
        public Nullable<System.TimeSpan> CheckInTime { get; set; }

        [Display(Name = "जाने समय")]
        [DataType(DataType.Time)]
        public Nullable<System.TimeSpan> CheckOutTime { get; set; }

        [Display(Name = "अवस्था")]
        public Nullable<int> IdKaajStatus { get; set; }

        [Display(Name = "सिफारिस मिति")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> RecommendedOn { get; set; }

        [Display(Name = "सिफारिस गर्ने")]
        public string RecommendationBy { get; set; }

        [Display(Name = "सिफारिश टिप्पणी")]
        public string RemarkRecommended { get; set; }

        [Display(Name = "स्वीकृत मिति")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> ApprovedOn { get; set; }

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

        [Required(ErrorMessage = "{0} चयन गर्नुहोस्")]
        [Range(1, double.PositiveInfinity, ErrorMessage = "{0} चयन गर्नुहोस्")]
        [Display(Name = "वर्ष")]
        public int KaajYearNP { get; set; }

        [Required(ErrorMessage = "{0} चयन गर्नुहोस्")]
        [Range(1, double.PositiveInfinity, ErrorMessage = "{0} चयन गर्नुहोस्")]
        [Display(Name = "देश")]
        public int IdCountry { get; set; }

        [Required(ErrorMessage = "{0} चयन गर्नुहोस्")]
        [Range(1, double.PositiveInfinity, ErrorMessage = "{0} चयन गर्नुहोस्")]
        [Display(Name = "प्रकार")]
        public int IdKaajType { get; set; }
    }
}

