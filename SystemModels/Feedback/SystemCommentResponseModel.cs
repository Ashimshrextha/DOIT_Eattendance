using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.Feedback
{
    [Table("SystemCommentResponse")]
    public class SystemCommentResponseModel : AuditableEntity<long>
    {
        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "शीर्षक")]
        public long IdSystemComment { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "प्रेषकको नाम")]
        public long IdSender { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "प्राप्तकर्ताको नाम")]
        public long IdReceiver { get; set; }

        [Required]
        [Display(Name = "प्रेषकको टिप्पणी")]
        [MaxLength(250)]
        public string SenderComment { get; set; }

        [Required]
        [Display(Name = "प्राप्तकर्ताको टिप्पणी")]
        [MaxLength(1000)]
        public string ReceiverComment { get; set; }
    }
}
