using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.DeviceManagement
{
    [Table("HREmployeeBiometricTemplate")]
    public class HREmployeeBiometricTemplateModel : EntityId<long>
    {
        [Required]
        [Display(Name = "कर्मचारी")]
        public long IdHREmployee { get; set; }

        [Required]
        [Display(Name = "उपकरण मोडेल")]
        public int IdHRDeviceModel { get; set; }

        [Required]
        [Display(Name = "टेम्प्लेट प्रकार")]
        public int IdTemplateType { get; set; }

        [Required]
        [Display(Name = "कर्मचारी नं.")]
        public long IdEnroll { get; set; }

        [Display(Name = "टेम्प्लेट इनडेक्स")]
        public Nullable<int> TemplateIndex { get; set; }

        [Display(Name = "टेम्प्लेट डाटा लेन्थ")]
        public Nullable<long> TemplateDataLength { get; set; }

        [Display(Name = "टेम्प्लेट (स्ट्रिंग)")]
        public string TemplateDataStr { get; set; }

        [Display(Name = "टेम्प्लेट (बाइट)")]
        public byte[] TemplateDataBin { get; set; }

        [Display(Name = "सिर्जना मिति")]
        [DataType(DataType.DateTime)]
        public System.DateTime CreatedOn { get; set; }

        [Display(Name = "अपडेट मिति")]
        [DataType(DataType.DateTime)]
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }
}
