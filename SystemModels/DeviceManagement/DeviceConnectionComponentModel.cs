using System.ComponentModel.DataAnnotations;

namespace SystemModels.DeviceManagement
{
    public class DeviceConnectionComponentModel
    {

        [Required]
        [Display(Name = "उपकरण नं.")]
        [Range(1, double.PositiveInfinity)]
        public int DeviceNumber { get; set; }

        [Required]
        [Display(Name = "आईपी ​​ठेगाना")]
        public string IPAddress { get; set; }

        [Required]
        [Display(Name = "पोर्ट नम्बर")]
        [Range(1, 65534)]
        public int PortNumber { get; set; }

        [Required]
        [Display(Name = "संचार कुञ्जी")]
        public int CommunicationKey { get; set; } = 0;

        public int IdDeviceModel { get; set; }
    }
}
