using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.SystemSetting
{
    [Table("SystemDatabaseBackup")]
    public class SystemDatabaseBackupModel:AuditableEntity<long>
    {
        [Display(Name ="कार्यालय")]
        public long IdHRCompany { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name ="शीर्षक")]
        [MaxLength(250)]
        public string Title { get; set; }

        [Display(Name ="ब्याकप")]
        [MaxLength(500)]
        public string BackupPath { get; set; }
    }
}
