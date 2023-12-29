using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.SystemSetting
{
    [Table("HRPublicHoliday")]
    public class HRPublicHolidayModel : AuditableEntity<long>
    {
        [Display(Name = "कार्यालय")]
        public long IdHRCompany { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "सार्बजनिक बिदा")]
        [MaxLength(250)]
        public string HRPublicHolidayTitle { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "सार्बजनिक बिदा (संक्षिप्त)")]
        [MaxLength(10)]
        public string HRPublicHolidayShortName { get; set; }

        [Display(Name = "शुरुको मिति")]
        [DataType(DataType.DateTime)]
        public Nullable<DateTime> Fromdate { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "शुरुको मिति")]
        [MaxLength(10, ErrorMessage = "कृपया सही  {0} चयन गर्नुहोस्")]
        public string FromdateNp { get; set; }

        [Display(Name = "अन्तिम मिति")]
        [DataType(DataType.DateTime)]
        public Nullable<DateTime> ToDate { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "अन्तिम मिति")]
        [MaxLength(10, ErrorMessage = "कृपया सही  {0} चयन गर्नुहोस्")]
        public string ToDateNp { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "पुरुष बिदा छ/छैन")]
        public bool IsHolidayForMale { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "महिला बिदा छ/छैन")]
        public bool IsHolidayForFemale { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "शारीरिक असक्षम व्यक्तिले मात्र पाउने")]
        public bool IsHolidayForDisabled { get; set; } = false;

        [Display(Name = "धर्म")]
        public Nullable<int> IdReligion { get; set; }
    }
}
