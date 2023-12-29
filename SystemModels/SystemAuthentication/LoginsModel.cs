using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.SystemAuthentication
{
    [Table("Logins")]
    public class LoginsModel : AuditableEntity<Guid>
    {
        [Required]
        [Display(Name = "प्रयोगकर्ता नाम")]
        [MaxLength(250)]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password_MD5 { get; set; }

        [Display(Name = "Password SHA")]
        [DataType(DataType.Password)]
        public string Password_SHA { get; set; }

        [Display(Name = "इमेल")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(70)]
        public string Email { get; set; }

        [Display(Name = "IsEmail Confirmed")]
        public bool IsEmailConfirmed { get; set; }

        [Display(Name = "मोबाइल")]
        [MaxLength(10)]
        public string Mobile { get; set; }

        [Display(Name = "2-चरण प्रमाणिकरण")]
        public bool TwoFactorEnabled { get; set; }

        [Display(Name = "लकआउट अन्त्य मिति")]
        [DataType(DataType.DateTime)]
        public DateTime? LockOutEndDate { get; set; }

        [Display(Name = "लकआउट सक्षम")]
        public bool LockOutEnabled { get; set; } = false;

        [Display(Name = "असफल गणना")]
        public int AccessFailedCount { get; set; }

        [Required]
        [Display(Name = "भाषा प्रणाली")]
        [MaxLength(250)]
        public string Language { get; set; }

        [Required]
        [Display(Name = "भूमिका")]
        public int IdHREmployeeRole { get; set; }
    }
}
