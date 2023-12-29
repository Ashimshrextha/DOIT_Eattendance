using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.EmployeeManagement
{
    [Table("HREmployeeBankAccount")]
    public class HREmployeeBankAccountModel : AuditableEntity<long>
    {
        [Display(Name = "कर्मचारी")]
        public long IdHREmployee { get; set; }

        [Required]
        [Display(Name = "बैंक नाम")]
        public long IdBank { get; set; }

        [Display(Name = "नोमिनी नाम")]
        [MaxLength(500)]
        public string NomineeName { get; set; }

        [Required]
        [Display(Name = "खाता नं.(ए/सी)")]
        [MaxLength(100)]
        public string BankAccountNumber { get; set; }

        [Required]
        [Display(Name = "खाता प्रकार")]
        [MaxLength(50)]
        public string AccountType { get; set; }

        [Display(Name = "पूर्वनिर्धारित छ/छैन")]
        public bool IsDefault { get; set; }
    }
}
