using System;
using System.ComponentModel.DataAnnotations;

namespace SystemModels.EmployeeManagement
{
    public class LeaveAppliedSummaryModel
    {

        [Display(Name = "कर्मचारी नाम")]
        public long IdHREmployee { get; set; }


        [Display(Name = "कर्मचारी नाम")]
        public string EmployeeName { get; set; }

        [Display(Name = "कर्मचारी संकेत न.")]
        public string PISNumber { get; set; }


        public Nullable<long> IdEnroll { get; set; }

        [Display(Name = "मोबाइल")]
        public string MobileNumber { get; set; }


        [Display(Name = "इमेल")]
        public string Email { get; set; }

        [Display(Name = "नियुक्ति मिति")]
        public string AppointmentDateNP { get; set; }

        public string ImageName { get; set; }

        [Display(Name = "जन्मस्थान")]
        public string BirthPlace { get; set; }

        public long IdLeavehistory { get; set; }

        [Display(Name = "बिदा शुरु")]
        public System.DateTime LeaveValidFrom { get; set; }

        [Display(Name = "बिदा समाप्त")]
        public System.DateTime LeaveValidTo { get; set; }


        [Display(Name = "बिदा शुरु")]
        public string LeaveValidFromNP { get; set; }

        [Display(Name = "बिदा समाप्त")]
        public string LeaveValidToNP { get; set; }


        [Display(Name = "बिदा कारण")]
        public string LeaveReason { get; set; }


        [Display(Name = "सिफारिस मिति")]
        public Nullable<System.DateTime> RecommandationDate { get; set; }


        [Display(Name = "सिफारिस टिप्पणी")]
        public string RecommandationRemark { get; set; }


        [Display(Name = "स्वीकृत मिति")]
        public Nullable<System.DateTime> ApprovedOn { get; set; }


        [Display(Name = "स्वीकृत टिप्पणी")]
        public string ApprovalRemark { get; set; }


        public System.DateTime CreatedOn { get; set; }


        [Display(Name = "आधा दिन")]
        public string IsHalfDayCount { get; set; }


        [Display(Name = "बिदा वर्ष")]
        public int LeaveYear { get; set; }


        [Display(Name = "बिदा वर्ष")]
        public Nullable<int> LeaveYearNP { get; set; }


        [Display(Name = "फारम")]
        public string Document { get; set; }


        [Display(Name = "कार्यालय")]
        public long IdHRCompany { get; set; }


        [Display(Name = "जागिर")]
        public long IdJob { get; set; }


        [Display(Name = "बिदाको प्रकार")]
        public string LeaveTitle { get; set; }


        [Display(Name = "कार्यालय")]
        public string HRCompanyName { get; set; }


        [Display(Name = "शाखा/सेक्सन")]
        public string DivisionName { get; set; }


        [Display(Name = "पद")]
        public string DesignationTitle { get; set; }


        [Display(Name = "सिफारिस गर्ने")]
        public Nullable<long> IdRecommandationBy { get; set; }


        [Display(Name = "स्वीकृत गर्ने")]
        public Nullable<long> IdApprovedBy { get; set; }


        [Display(Name = "अवस्था")]
        public int IdLeaveStatus { get; set; }


        [Display(Name = "अवस्था")]
        public string LeaveStatusType { get; set; }

        [Display(Name = "लिङ्ग")]
        public string Gender { get; set; }

        [Display(Name = "सिफारिस गर्ने")]
        public string RecommendedEmployeeName { get; set; }

        [Display(Name = "स्वीकृत गर्ने")]
        public string ApprovedEmployeeName { get; set; }

        [Display(Name = "जागिर किसिम")]
        public string PositionStatusTitle { get; set; }
    }
}
