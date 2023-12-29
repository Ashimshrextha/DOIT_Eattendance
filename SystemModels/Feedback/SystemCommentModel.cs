using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.Feedback
{
    [Table("SystemComment")]
    public class SystemCommentModel : AuditableEntity<long>
    {
        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "कार्यालय")]
        public long IdHRCompany { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "कर्मचारी")]
        public long IdHREmployee { get; set; }

        [Required]
        [Display(Name = "शीर्षक")]
        [MaxLength(250)]
        public string CommentTitle { get; set; }

        [Required]
        [Display(Name = "टिप्पणी")]
        [MaxLength(1000)]
        public string Comment { get; set; }

        [Display(Name = "हल भयो/भएको छैन")]
        public bool IsSolved { get; set; }
    }
}
