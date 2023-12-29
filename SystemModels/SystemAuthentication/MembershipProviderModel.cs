using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.SystemAuthentication
{
    [Table("MembershipProvider")]
    public class MembershipProviderModel : EntityId<int>
    {
        [Display(Name = "Id Login")]
        public Guid IdLogin { get; set; }

        [Display(Name = "र्कर्मचारी नाम")]
        public long IdHREmployee { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "प्रणाली भूमिका")]
        [Range(1, double.PositiveInfinity, ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        public int IdHREmployeeRole { get; set; }
    }
}
