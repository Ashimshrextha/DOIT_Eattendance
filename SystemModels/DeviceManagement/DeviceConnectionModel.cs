using System;
using System.ComponentModel.DataAnnotations;
using static SystemStores.ENUMData.EnumGlobal;

namespace SystemModels.DeviceManagement
{
    public class DeviceConnectionModel
    {
        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "कार्यालय नाम")]
        public long IdHRCompany { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "उपकरण मोडेल")]
        public Guid IdHRDevice { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "प्रोटोकल")]
        public CommunicationProtocol Protocol { get; set; }
    }
}
