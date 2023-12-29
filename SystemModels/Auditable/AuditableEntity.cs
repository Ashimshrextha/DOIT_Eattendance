using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using SystemInterfaces.Auditable;
using static SystemStores.GlobalMethod.SystemGlobalMethod;

namespace SystemModels.Auditable
{
    public abstract class AuditableEntity<TKey> : EntityId<TKey>, IAuditableEntity
    {
        [Display(Name = "प्रयोगकर्ता आईपी")]
        [ScaffoldColumn(false)]
        public string TerminalIP { get { return RemoteTerminalId; } }

        [Display(Name = "सक्रिय छ")]
        [ScaffoldColumn(false)]
        public bool IsActive { get; set; } = true;

        [Display(Name = "सिर्जना मिति")]
        [ScaffoldColumn(false)]
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        [Display(Name = "द्वारा निर्मित")]
        [MaxLength(250)]
        [ScaffoldColumn(false)]
        public virtual string CreatedBy { get; } = HttpContext.Current.User.Identity.Name;

        [Display(Name = "सम्पादन मिति")]
        [ScaffoldColumn(false)]
        public DateTime? UpdatedOn { get; } = DateTime.Now;


        [Display(Name = "द्वारा सम्पादन")]
        [MaxLength(250)]
        [ScaffoldColumn(false)]
        public virtual string UpdatedBy { get; } = HttpContext.Current.User.Identity.Name;
    }
}
