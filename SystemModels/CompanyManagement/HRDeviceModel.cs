using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.CompanyManagement
{
    [Table("HRDevice")]
    public class HRDevicesModel : AuditableEntity<Guid>
    {
        [Display(Name = "कार्यालय")]
        public long IdHRCompany { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "उपकरण मोडेल")]
        public int IdHRDeviceModel { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस")]
        [Display(Name = "उपकरण नाम")]
        [MaxLength(250)]
        public string HRDeviceName { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस")]
        [Display(Name = "उपकरण नं.")]
        public int DeviceNumber { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस")]
        [Display(Name = "उपकरण आई.पी")]
        [MaxLength(50)]
        public string IPAddress { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस")]
        [Display(Name = "संचार पोर्ट")]
        public Nullable<int> CommunicationPort { get; set; }

        [Display(Name = "सिरियल पोर्ट")]
        public Nullable<int> SerialPort { get; set; }

        [Display(Name = "उपकरण सिरियल नं.")]
        [MaxLength(50)]
        public string SerialNumber { get; set; }
    }
}
