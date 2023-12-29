using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.EmployeeManagement
{
    [Table("HREmployeeAttendanceHistory")]
    public class HREmployeeAttendanceHistoryModel : EntityId<long>
    {
        [Display(Name = "जागिर")]
        public long IdJobHistory { get; set; }

        [Display(Name = "कर्मचारी")]
        public long IdHREmployee { get; set; }

        [Display(Name = "कार्यालय")]
        public long IdHRCompany { get; set; }

        [Display(Name = "उपस्थित मिति")]
        [DataType(DataType.DateTime)]
        public System.DateTime AttendanceDate { get; set; }

        [Display(Name = "चेक इन उपकरण न.")]
        public Nullable<int> CheckInDeviceNumber { get; set; }

        [Display(Name = "चेक आउट उपकरण न.")]
        public Nullable<int> CheckOutDeviceNumber { get; set; }

        [Display(Name = "खुल्ने समय")]
        public System.TimeSpan LogInTime { get; set; }

        [Display(Name = "बन्द हुने समय")]
        public System.TimeSpan LogOutTime { get; set; }

        [Display(Name = "आएको समय")]
        public System.TimeSpan CheckInTime { get; set; }

        [Display(Name = "गएको समय")]
        public Nullable<System.TimeSpan> CheckOutTime { get; set; }

        [Display(Name = "ढिलो आएको")]
        public Nullable<System.TimeSpan> LateBy { get; set; }

        [Display(Name = "छिटो आएको")]
        public Nullable<System.TimeSpan> CheckInEarly { get; set; }

        [Display(Name = "छिटो गएको")]
        public Nullable<System.TimeSpan> CheckOutEarly { get; set; }

        [Display(Name = "ढिलोको कारण")]
        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस")]
        [MaxLength(250)]
        public string LateReason { get; set; }

        [Display(Name = "छिटोको कारण")]
        [MaxLength(250)]
        public string CheckInEarlyReason { get; set; }

        [Display(Name = "छिटोजानुको कारण")]
        [MaxLength(250)]
        public string CheckOutEarlyReason { get; set; }

        [Display(Name = "ढिलोको कारण स्वीकृत छ/छैन")]
        public bool IsLateReasonApproved { get; set; }

        [Display(Name = "छिटो आएको कारण स्वीकृत छ/छैन")]
        public bool IsCheckInEarlyReasonApproved { get; set; }

        [Display(Name = "छिटो गएको कारण स्वीकृत छ/छैन")]
        public bool IsCheckOutEarlyReasonApproved { get; set; }

        [Display(Name = "टिप्पणी")]
        [MaxLength(500)]
        public string Remark { get; set; }

        [MaxLength(500)]
        public string ApprovedBy { get; set; }

        [Display(Name = "काम गर्ने(मिनेट)")]
        public int TotalWorkingInMin { get; set; }

        [Display(Name = "काम गरेको(मिनेट)")]
        public Nullable<int> TotalWorkedInMin { get; set; }

        [Display(Name = "कुल काम गरेको")]
        public Nullable<int> TotalWorked { get; set; }

        [Display(Name = "बढी काम गरेको(मिनेट)")]
        public Nullable<int> TotalOverTimeInMin { get; set; }

        [Display(Name = "शिफ्ट पुरा छ/छैन")]
        public bool IsShiftCompleted { get; set; }

        [Display(Name = "सिर्जना मिति")]
        [DataType(DataType.DateTime)]
        public System.DateTime CreatedOn { get; set; }

        [Display(Name = "अपडेट मिति")]
        [DataType(DataType.DateTime)]
        public Nullable<System.DateTime> UpdatedOn { get; set; }

        [Display(Name = "स्वीकृत गर्ने")]
        [Range(1, double.PositiveInfinity, ErrorMessage = "{0} चयन गर्नुहोस्")]
        public Nullable<long> IdApprovedBy { get; set; }

    }
}
