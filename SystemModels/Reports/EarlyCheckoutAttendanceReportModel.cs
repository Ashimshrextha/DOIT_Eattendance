using System;
using System.ComponentModel.DataAnnotations;
using SystemModels.Auditable;

namespace SystemModels.Reports
{
    public class EarlyCheckoutAttendanceReportModel : EntityId<long>
    {
        [Display(Name = "कार्यालय")]
        public long IdHRCompany { get; set; }

        [Display(Name = "Job")]
        public long IdJobHistory { get; set; }

        [Display(Name = "कर्मचारीको नाम")]
        public long IdHREmployee { get; set; }

        [Display(Name = "क.संकेत नं.")]
        public Nullable<long> IdEnroll { get; set; }

        [Display(Name = "शाखा/सेक्सन")]
        public long IdHRCompanyDivision { get; set; }

        [Display(Name = "कर्मचारीको नाम")]
        public string EmployeeName { get; set; }

        [Display(Name = "पद")]
        public string Designation { get; set; }

        public int HRDesignationOrder { get; set; }

        public string AppointmentDateNP { get; set; }

        [Display(Name = "मिति")]
        [DataType(DataType.Date)]
        public System.DateTime AttendanceDate { get; set; }

        [Display(Name = "आउने समय")]
        public System.TimeSpan LogInTime { get; set; }

        [Display(Name = "जाने समय")]
        public System.TimeSpan LogOutTime { get; set; }

        [Display(Name = "आएको समय")]
        public System.TimeSpan CheckInTime { get; set; }

        [Display(Name = "गएको समय")]
        public Nullable<System.TimeSpan> CheckOutTime { get; set; }

        [Display(Name = "छिटो गएको")]
        public Nullable<System.TimeSpan> CheckOutEarly { get; set; }

        [Required(ErrorMessage = "कृप्या {0} लेख्नुहोस")]
        [Display(Name = "कारण")]
        [MaxLength(500)]
        public string CheckOutEarlyReason { get; set; }

        public bool IsCheckOutEarlyReasonApproved { get; set; }

        [Display(Name = "टिप्पणी")]
        [MaxLength(500)]
        public string Remark { get; set; }

        [Required]
        [Display(Name = "स्वीकृति गर्ने कर्मचारी")]
        public Nullable<long> IdApprovedBy { get; set; }

        [Display(Name = "स्वीकृति गर्ने कर्मचारी")]
        public string ApprovalEmployeeName { get; set; }
    }
}
