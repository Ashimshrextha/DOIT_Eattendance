using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.SystemSetting
{
    [Table("HRCalendar")]
    public class HRCalendarModel : EntityId<long>
    {
        [Required]
        [Display(Name = "नेपाली मिति")]
        [MaxLength(10)]
        public string NepDate { get; set; }

        [Required]
        [Display(Name = "नेपाली वर्ष")]
        public int NepYear { get; set; }

        [Required]
        [Display(Name = "नेपाली महिना")]
        public int NepMonth { get; set; }

        [Required]
        [Display(Name = "नेपाली दिन")]
        public int NepDay { get; set; }

        [Required]
        [Display(Name = "अंग्रेजी मिति")]
        public System.DateTime EngDate { get; set; }

        [Required]
        [Display(Name = "अंग्रेजी वर्ष")]
        public int EngYear { get; set; }

        [Required]
        [Display(Name = "अंग्रेजी महिना")]
        public int EngMonth { get; set; }

        [Required]
        [Display(Name = "अंग्रेजी दिन")]
        public int EngDay { get; set; }

        [Required]
        [Display(Name = "इभेन्ट नाम")]
        [MaxLength(250)]
        public string EventName { get; set; }

        [Required]
        [Display(Name = "महोत्सव नाम")]
        [MaxLength(250)]
        public string FestivalName { get; set; }

        [Required]
        [Display(Name = "सप्ताहांत बिदा छ/छैन")]
        public bool IsWeekendHoliday { get; set; }

        [Required]
        [Display(Name = "सार्वजनिक बिदा छ/छैन")]
        public bool IsPublicHoliday { get; set; }

    }
}
