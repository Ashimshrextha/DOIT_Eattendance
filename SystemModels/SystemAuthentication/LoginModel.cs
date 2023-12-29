using System.ComponentModel.DataAnnotations;

namespace SystemModels.SystemAuthentication
{
    public class LoginModel
    {
        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस")]
        [Display(Name = "प्रयोगकर्ता नाम/कर्मचारी सङ्केत नं")]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस")]
        [DataType(DataType.Password)]
        [Display(Name = "पासवर्ड")]
        public string Password { get; set; }

        [Display(Name = "युआरयल")]
        public string ReturnURL { get; set; }

        [Display(Name = "मलाई सम्झनुहोस्")]
        public bool IsRemember { get; set; }
    }
}
