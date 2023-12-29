using System;
using System.ComponentModel.DataAnnotations;

namespace SystemInterfaces.Auditable
{
    public interface IAuditableEntity
    {
        string TerminalIP { get;}

        [Display(Name= "सक्रिय")]
        bool IsActive { get; set; }

        DateTime CreatedOn { get;}

        string CreatedBy { get;}

        Nullable<DateTime> UpdatedOn { get; }

        string UpdatedBy { get; }
    }
}
