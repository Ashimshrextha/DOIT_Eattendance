using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.SystemSetting
{
    [Table("MonthNp")]
    public class MonthNpModel : EntityId<int>
    {
        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "महिना(बीएस)")]
        [MaxLength(10)]
        public string BS { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "महिना(एडी)")]
        public string AD { get; set; }
    }
}
