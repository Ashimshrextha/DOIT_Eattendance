using System.ComponentModel.DataAnnotations;
using SystemStores.GlobalModels;

namespace SystemViewModels.SystemAuthentication
{
    public class RegisterViewModel:BreadCrumbModel
    {
        [Required]
        [Display(Name = "User Name")]
        [MaxLength(250)]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(70)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [MaxLength(20)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Re-Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Sorry your {0} password didn't match")]
        [MaxLength(20)]
        public string RePassword { get; set; }

        [Display(Name = "Mobile")]
        [MaxLength(10)]
        public string Mobile { get; set; }

        [Display(Name ="Role")]
        public int IdHREemployeeRole { get; set; }
        public bool TwoFactorEnabled { get; set; } = false;
        public bool IsAgree { get; set; } = false;
    }
}
