using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.EmployeeManagement
{
    [Table("HREmployeeLeaveHistory")]
    public class HREmployeeLeaveHistoryModel : AuditableEntity<long>
    {
        [Display(Name = "कार्यालय")]
        public long IdHRCompany { get; set; }

        [Display(Name = "जागिर")]
        public long IdJob { get; set; }

        [Display(Name = "सिफारिस गर्ने")]
        public long? IdRecommandationBy { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Range(1, double.PositiveInfinity, ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "स्वीकृत गर्ने")]
        public long? IdApprovedBy { get; set; }

        [Display(Name = "कर्मचारी नाम")]
        public long? IdHREmployee { get; set; }

        [Display(Name = "कर्मचारी नाम")]
        public string EmployeeName { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "बिदाको प्रकार")]
        [Range(1, double.PositiveInfinity, ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        public long IdLeaveType { get; set; }

        [Display(Name = "बिदा वर्ष")]
        public Nullable<int> LeaveYearNP { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "बिदा शुरु")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime LeaveValidFrom { get; set; }

        //[RegularExpression("((([0-9][0-9][0-9][1-9])|([1-9][0-9][0-9][0-9])|([0-9][1-9][0-9][0-9])|([0-9][0-9][1-9][0-9]))-((0[13578])|(1[02]))-((0[1-9])|([12][0-9])|(3[01])))|((([0-9][0-9][0-9][1-9])|([1-9][0-9][0-9][0-9])|([0-9][1-9][0-9][0-9])|([0-9][0-9][1-9][0-9]))-((0[469])|11)-((0[1-9])|([12][0-9])|(30)))|(((000[48])|([0-9]0-9)|([0-9][1-9][02468][048])|([1-9][0-9][02468][048]))-02-((0[1-9])|([12][0-9])))|((([0-9][0-9][0-9][1-9])|([1-9][0-9][0-9][0-9])|([0-9][1-9][0-9][0-9])|([0-9][0-9][1-9][0-9]))-02-((0[1-9])|([1][0-9])|([2][0-8])))", ErrorMessage = "{0} को ढाँचा मिलेन (YYYY-MM-DD) ")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "बिदा शुरु")]
        [MaxLength(10)]
        public string LeaveValidFromNP { get; set; }

        //[RegularExpression("((([0-9][0-9][0-9][1-9])|([1-9][0-9][0-9][0-9])|([0-9][1-9][0-9][0-9])|([0-9][0-9][1-9][0-9]))-((0[13578])|(1[02]))-((0[1-9])|([12][0-9])|(3[01])))|((([0-9][0-9][0-9][1-9])|([1-9][0-9][0-9][0-9])|([0-9][1-9][0-9][0-9])|([0-9][0-9][1-9][0-9]))-((0[469])|11)-((0[1-9])|([12][0-9])|(30)))|(((000[48])|([0-9]0-9)|([0-9][1-9][02468][048])|([1-9][0-9][02468][048]))-02-((0[1-9])|([12][0-9])))|((([0-9][0-9][0-9][1-9])|([1-9][0-9][0-9][0-9])|([0-9][1-9][0-9][0-9])|([0-9][0-9][1-9][0-9]))-02-((0[1-9])|([1][0-9])|([2][0-8])))", ErrorMessage = "{0} को ढाँचा मिलेन (YYYY-MM-DD) ")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [MaxLength(10)]
        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "बिदा समाप्त")]
        public string LeaveValidToNP { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        [Display(Name = "बिदा समाप्त")]
        public DateTime LeaveValidTo { get; set; }

        [Display(Name = "उपलब्ध दिन")]
        public string CurrentAvailableLeaveDay { get; set; }

        [Display(Name = "पाउने बिदा दिन")]
        public decimal CurrentYearBalanceLeaveDay { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस")]
        [Display(Name = "बिदा कारण")]
        [MaxLength(500)]
        public string LeaveReason { get; set; }

        [Display(Name = "अवस्था")]
        public int IdLeaveStatus { get; set; }

        [Display(Name = "सिफारिस टिप्पणी")]
        [MaxLength(500)]
        public string RecommandationRemark { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "सिफारिस मिति")]
        [DataType(DataType.DateTime)]
        public DateTime? RecommandationDate { get; set; }

        [Display(Name = "स्वीकृत टिप्पणी")]
        [MaxLength(500)]
        public string ApprovalRemark { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "स्वीकृत मिति")]
        [DataType(DataType.DateTime)]
        public DateTime? ApprovedOn { get; set; }

        [Display(Name = "अपलोड फारम")]
        [MaxLength(250)]
        public string Document { get; set; }

        [Display(Name = "अपलोड डकुमेन्ट")]
        [MaxLength(250)]
        public string Document1 { get; set; }

        [Display(Name = "अपलोड डकुमेन्ट")]
        [MaxLength(250)]
        public string Document2 { get; set; }

        [Display(Name = "पद")]
        [MaxLength(50)]
        public string Designation { get; set; }

        [Display(Name = "शाखा/सेक्सन")]
        public string Division { get; set; }

        [Display(Name = "आधा दिन")]
        public bool IsHalfDayCount { get; set; }

        public int DesignationRank { get; set; }

        [Display(Name = "कर्मचारी संकेत न.")]
        public string PISNumber { get; set; }

        [Display(Name = "लिङ्ग")]
        public string Gender { get; set; }

        [Display(Name = "मोबाइल")]
        public string Mobile { get; set; }

        public int IdMasterLeaveTitle { get; set; }

        public string ImageName { get; set; }

        [Display(Name = "कार्यालय")]
        public string HRCompanyName { get; set; }

        [Display(Name = "जागिर अवस्था")]
        public string JobStatus { get; set; }

        public string AppoinmentDateNP { get; set; }
    }
}
