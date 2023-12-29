using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.SystemSecurity
{
    [Table("HRCalendar")]
    public class HRCalendarModel : EntityId<long>
    {
        [Required]
        [Display(Name = "मिति (बीएस)")]
        [MaxLength(10)]
        public string NepDate { get; set; }

        [Display(Name = "वर्ष (बीएस)")]
        public int NepYear { get; set; }

        [Display(Name = "महिना (बीएस)")]
        public int NepMonth { get; set; }

        [Display(Name = "दिन (बीएस)")]
        public int NepDay { get; set; }

        [Required]
        [Display(Name = "मिति (एडी)")]
        [DataType(DataType.DateTime)]
        public System.DateTime EngDate { get; set; }

        [Display(Name = "वर्ष (एडी)")]
        public int EngYear { get; set; }

        [Display(Name = "महिना (एडी)")]
        public int EngMonth { get; set; }

        [Display(Name = "दिन (एडी)")]
        public int EngDay { get; set; }

        [Display(Name = "इभेन्ट नाम")]
        [MaxLength(250)]
        public string EventName { get; set; }

        [Display(Name = "महोत्सव नाम")]
        [MaxLength(250)]
        public string FestivalName { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "सप्ताहांत बिदा छ/छैन")]
        public bool IsWeekendHoliday { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "सार्वजनिक बिदा छ/छैन")]
        public bool IsPublicHoliday { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "पुरुष बिदा छ/छैन")]
        public bool IsHolidayForMale { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "महिला बिदा छ/छैन")]
        public bool IsHolidayForFemale { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "शारीरिक असक्षम व्यक्तिले मात्र पाउने")]
        public bool IsHolidayForDisabled { get; set; }

        [Display(Name = "धर्म")]
        public Nullable<int> IdReligion { get; set; }
    }
}
