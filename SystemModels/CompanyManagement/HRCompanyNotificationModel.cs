using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.CompanyManagement
{
    [Table("HRCompanyNotification")]
    public class HRCompanyNotificationModel : AuditableEntity<long>
    {
        [Display(Name = "कार्यालय")]
        public long IdHRCompany { get; set; }

        [Display(Name = "सूचना गर्ने")]
        public long IdNoticeBy { get; set; }

        [Required]
        [Display(Name = "सूचनाको शीर्षक")]
        [MaxLength(250)]
        public string NotificationTitle { get; set; }

        [Required]
        [Display(Name = "सन्देश")]
        [MaxLength(500)]
        public string NotificationMessage { get; set; }

        [Required]
        [Display(Name = "सामान्य छ/छैन")]
        public bool IsGeneral { get; set; }

        [Required]
        [Display(Name = "शुरुको मिति")]
        [DataType(DataType.DateTime)]
        public System.DateTime EffectiveFromDate { get; set; }

        [Display(Name = "अन्तिम मिति")]
        [DataType(DataType.DateTime)]
        public System.DateTime EffectiveToDate { get; set; }
    }
}
