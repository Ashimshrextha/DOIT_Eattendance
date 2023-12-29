using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.EmployeeManagement
{
    [Table("HREmployee")]
    public class HREmployeeModel : AuditableEntity<long>
    {
        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "कार्यालय")]
        [Range(1, double.PositiveInfinity, ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        public long? IdHRCompany { get; set; }

        public long? IdParent_HREmployee { get; set; }

        [Display(Name = "मातृभाषा")]
        public Nullable<long> IdMotherTongue { get; set; }

        [Display(Name = "सम्बन्ध")]
        public Nullable<int> IdRelation { get; set; }

        //[Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "रक्त समूह")]
        // [Range(1, double.PositiveInfinity, ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        public Nullable<int> IdBloodGroup { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "लिङ्ग")]
        [Range(1, double.PositiveInfinity, ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        public long IdGender { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "वैवाहिक अवस्था")]
        [Range(1, double.PositiveInfinity, ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        public Nullable<long> IdMarital { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "धर्म")]
        [Range(1, double.PositiveInfinity, ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        public Nullable<int> IdHREmployeeReligion { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "कर्मचारी नाम (अंग्रेजीमा)")]
        [RegularExpression("^[a-zA-Z]'?([a-zA-Z]|/.| |-)+$", ErrorMessage = "कृपया सही  {0} लेख्नुहोस्")]
        [MaxLength(250)]
        public string HREmployeeName { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "कर्मचारी नाम (देवनागरीमा)")]
        [MaxLength(250)]
        public string HREmployeeNameNP { get; set; }

        [Display(Name = "व्यक्तिको रंग")]
        [MaxLength(250)]
        public string Color { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "उपकरणको आईडी")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "कृपया सही  {0} लेख्नुहोस्")]
        public long? IdEnroll { get; set; }

        [Display(Name = "कर्मचारी संकेत नं.")]
        [MaxLength(20, ErrorMessage = "कृपया  {0} {1} अंक को मात्र लेख्नुहोस्")]
        public string PISNumber { get; set; }

        // [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस")]
        [Display(Name = "मोबाइल नं.")]
        [RegularExpression("^([7-9]{3})?[0-9]*$", ErrorMessage = "कृपया सही  {0} लेख्नुहोस्")]
        [MinLength(10, ErrorMessage = "कृपया  {0} {1} अंक को मात्र लेख्नुहोस्")]
        [MaxLength(10, ErrorMessage = "कृपया  {0} {1} अंक को मात्र लेख्नुहोस्")]
        public string MobileNumber { get; set; }

        [Display(Name = "इमेल")]
        [RegularExpression("^[_A-Za-z0-9-\\+]+(\\.[_A-Za-z0-9-]+)*@[A-Za-z0-9-]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})$", ErrorMessage = "कृपया सही  {0} लेख्नुहोस")]
        [MaxLength(70)]
        public string Email { get; set; }

        //[Required(ErrorMessage = "कृपया  {0} लेख्नुहोस")]
        [Display(Name = "जन्म स्थान ")]
        [MaxLength(100)]
        public string BirthPlace { get; set; }

        [Display(Name = "जन्म मिति")]
        [DataType(DataType.Date)]
        public Nullable<DateTime> DOB { get; set; }

        [Display(Name = "शुरु नियुक्ती मिति")]
        [DataType(DataType.Date)]
        public Nullable<DateTime> AppointmentDate { get; set; }

        [Display(Name = "सेवानिवृत्त मिति")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> RetiredDate { get; set; }

        [Display(Name = "कागजात")]
        public string Document { get; set; }

        [Display(Name = "अन्य कागजात")]
        public string Document1 { get; set; }

        public string RightEye { get; set; }

        public string LeftEye { get; set; }

        [Display(Name = "दाँया थम्ब")]
        public string RightThumb { get; set; }

        [Display(Name = "बाँया थम्ब")]
        public string LeftThumb { get; set; }

        [Display(Name = "हस्ताक्षर")]
        [MaxLength(250)]
        public string Signature { get; set; }

        [Display(Name = "फोटो")]
        public string ImageName { get; set; }

        [Display(Name = "जम्मा सदस्य")]
        public Nullable<int> TotalFamilyNumber { get; set; }

        [Display(Name = "बच्चाको संख्या")]
        public Nullable<int> TotalChild { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "शारीरिक असक्षम")]
        public bool IsDisabled { get; set; }

        [Display(Name = "अवस्था")]
        public string Status { get; set; }

        [Display(Name = "अफिसर छ/छैन")]
        public bool IsOfficier { get; set; }

        [Display(Name = "आफ्नो बारेमा लेख्नुहोस्")]
        [MaxLength(500)]
        public string Remark { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "लग - इन आईडी")]
        [RegularExpression("^[-_./0-9a-zA-Z\\\\]*$", ErrorMessage = "कृपया सही  {0} लेख्नुहोस्")]
        [MaxLength(100)]
        [MinLength(4, ErrorMessage = "कृपया  {0} {1}अक्षर को अनिवार्य लेख्नुहोस्")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "प्रणाली भाषा")]
        [MaxLength(250)]
        public string SystemLanguage { get; set; }

        //[RegularExpression("((([0-9][0-9][0-9][1-9])|([1-9][0-9][0-9][0-9])|([0-9][1-9][0-9][0-9])|([0-9][0-9][1-9][0-9]))-((0[13578])|(1[02]))-((0[1-9])|([12][0-9])|(3[01])))|((([0-9][0-9][0-9][1-9])|([1-9][0-9][0-9][0-9])|([0-9][1-9][0-9][0-9])|([0-9][0-9][1-9][0-9]))-((0[469])|11)-((0[1-9])|([12][0-9])|(30)))|(((000[48])|([0-9]0-9)|([0-9][1-9][02468][048])|([1-9][0-9][02468][048]))-02-((0[1-9])|([12][0-9])))|((([0-9][0-9][0-9][1-9])|([1-9][0-9][0-9][0-9])|([0-9][1-9][0-9][0-9])|([0-9][0-9][1-9][0-9]))-02-((0[1-9])|([1][0-9])|([2][0-8])))", ErrorMessage = "{0} को ढाँचा मिलेन (YYYY-MM-DD) ")]
        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "शुरु नियुक्ती मिति")]
        [MinLength(10, ErrorMessage = "कृपया  {0} {1} अंक को मात्र लेख्नुहोस्")]
        [MaxLength(10, ErrorMessage = "कृपया  {0} {1} अंक को मात्र लेख्नुहोस्")]
        public string AppointmentDateNP { get; set; }

        [Display(Name = "सेवानिवृत्त मिति")]
        [MinLength(10, ErrorMessage = "कृपया  {0} {1} अंक को मात्र लेख्नुहोस्")]
        [MaxLength(10, ErrorMessage = "कृपया  {0} {1} अंक को मात्र लेख्नुहोस्")]
        public string RetiredDateNP { get; set; }

        [Display(Name = "जन्म मिति")]
        [MinLength(10, ErrorMessage = "कृपया  {0} {1} अंक को मात्र लेख्नुहोस्")]
        [MaxLength(10, ErrorMessage = "कृपया  {0} {1} अंक को मात्र लेख्नुहोस्")]
        public string DOBNP { get; set; }
    }
}
