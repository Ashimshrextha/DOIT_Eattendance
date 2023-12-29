using System.ComponentModel.DataAnnotations;
using SystemStores.GlobalModels;

namespace SystemViewModels.SystemAuthentication
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "वर्तमान पासवर्ड")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "नयाँ पासवर्ड")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "पासवर्ड कम से कम {0} हुनु पर्छ")]
        public string Password { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "पुन: नयाँ पासवर्ड")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "पासवर्ड {0} मेल खाएन")]
        public string RePassword { get; set; }

        public UserSessionModel UserModel { get; set; }
    }
}
