using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.SystemSecurity
{
    [Table("HRCompanyCode")]
    public class HRCompanyCodeModel : EntityId<int>
    {
        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "कार्यालय कोड (अंग्रेजीमा)")]
        public string HRCompanyCodeTitle { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "कार्यालय कोड (देवनागरीमा)")]
        public string HRCompanyCodeNP { get; set; }
    }
}
