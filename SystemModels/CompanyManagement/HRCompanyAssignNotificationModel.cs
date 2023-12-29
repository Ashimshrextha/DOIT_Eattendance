using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.CompanyManagement
{
    [Table("HRCompanyAssignNotification")]
    public class HRCompanyAssignNotificationModel : EntityId<long>
    {
        [Display(Name = "अधिसूचना")]
        public long IdNotification { get; set; }

        [Display(Name = "कार्यालय")]
        public long IdCompany { get; set; }
    }
}
