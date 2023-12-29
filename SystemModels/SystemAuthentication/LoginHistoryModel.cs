using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.SystemAuthentication
{
    [Table("LoginsHistory")]
    public class LoginHistoryModel : AuditableEntity<long>
    {
        [Display(Name = "Old User Name")]
        [MaxLength(250)]
        public string OldUserName { get; set; }

        [Display(Name = "New User Name")]
        [MaxLength(250)]
        public string NewUserName { get; set; }

        [Display(Name = "Old MD5 Password")]
        [MaxLength(250)]
        public string OldPassword_MD5 { get; set; }

        [Display(Name = "New MD5 Password")]
        [MaxLength(250)]
        public string NewPassword_MD5 { get; set; }
    }
}
