using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.SystemSetting
{
    [Table("HRCompanyLeaveType")]
    public class HRCompanyLeaveTypeModel : AuditableEntity<long>
    {
        [Display(Name = "कार्यालय")]
        public long IdHRCompany { get; set; }

        [Display(Name = "बिदाको शीर्षक")]
        [ForeignKey("IdMasterLeaveTitle")]
        public int IdMasterLeaveTitle { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "साप्ताहिक बिदा गणना  गर्ने /नगर्ने ?")]
        public bool IsIncludeWeekend { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "सार्बजनिक बिदा गणना  गर्ने /नगर्ने ?")]
        public bool IsIncludePublicHoliday { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "करारले पाउने हो /होइन ?")]
        public bool EligibleTempStaff { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "बचत बिदा छ /छैन ?")]
        public bool IsSaving { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "जम्मा पाउने बिदा")]
        public int TotalLeaveDay { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "जम्मा बिदा बचत गर्न सकने")]
        public int SavingLimitDay { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "थप पाउने बिदा कति दिन?")]
        public int AdditionalDays { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "एकमुष्ट पाउने बिदा छ /छैन?")]
        public bool IsContinueLeave { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "पुरुषले मात्र पाउने बिदा हो /होइन ?")]
        public bool IsForMale { get; set; } = true;

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "महिलाले मात्र पाउने बिदा हो /होइन ?")]
        public bool IsForFemale { get; set; } = true;

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "आधा दिन पाउने बिदा हो /होइन ?")]
        public bool EnableHalfDay { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "दोहोरिने बिदा छ /छैन?")]
        public bool IsReoccuring { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "कति पटक पाउने बिदा")]
        public int OccuringTime { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "कार्यालय प्रमुख द्वारा स्वीकृत हो /होइन ?")]
        public bool IsApprovalByHRCompanyHead { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "बिदा सिफारिस गर्ने/नगर्ने")]
        public bool IsRecommended { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "कागजात अपलोड")]
        public bool IsDocumentUpload { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "कति दिनमा कागजात अपलोड गर्ने")]
        public int DocUploadDeadLineDays { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "कति दिनमा स्वीकृत गर्ने")]
        public int ApprovalDeadLineDays { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "अन्य बिदा बाट कट्टा हुने/नहुने ?")]
        public bool IsDeductFromOther { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "सटा बिदा  हो /होइन ?")]
        public bool IsSataLeave { get; set; }
    }
}
