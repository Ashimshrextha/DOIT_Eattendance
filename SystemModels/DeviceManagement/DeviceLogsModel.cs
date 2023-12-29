using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;
using static SystemStores.GlobalMethod.SystemGlobalMethod;

namespace SystemModels.DeviceManagement
{
    [Table("DeviceLogs")]
    public class DeviceLogsModel : EntityId<Guid>
    {
        [Required]
        [Display(Name = "उपकरण नं.")]
        public int DeviceNumber { get; set; }

        [Required]
        [Display(Name = "कर्मचारी नं.")]
        public long IdEnroll { get; set; }

        [Required]
        [Display(Name = "पंच मिति")]
        [DataType(DataType.DateTime)]
        public System.DateTime PunchDate { get; set; }

        [Required]
        [Display(Name = "पंच मिति")]
        [DataType(DataType.DateTime)]
        public string PunchDateNp { get; set; }

        [Display(Name = "प्रमाणिकरण मोड")]
        [MaxLength(50)]
        public string VerificationMode { get; set; }

        [Display(Name = "इनआउट मोड")]
        [MaxLength(50)]
        public string InOutMode { get; set; }

        [Required]
        [Display(Name = "फाईल मिति")]
        [DataType(DataType.DateTime)]
        public System.DateTime FetchedDate { get; set; }

        [Display(Name = "टर्मिनल आई.पी")]
        [MaxLength(50)]
        public string TerminalIP { get { return RemoteTerminalId; } }

        [Display(Name = "प्रसोधन छ/छैन")]
        public bool IsProcessed { get; set; }

        [Display(Name = "उपकरण मोडेल")]
        public string DeviceModel { get; set; }

        [Display(Name = "कार्यालय नाम")]
        public string HRCompanyName { get; set; }
    }
}
