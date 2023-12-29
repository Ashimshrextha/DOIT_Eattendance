using System;
using System.ComponentModel.DataAnnotations;

namespace SystemModels.EmployeeManagement
{
    public class KaajAppliedSummaryModel
    {
        [Display(Name = "प्रकार")]
        public long IdKaaj { get; set; }

        [Display(Name = "कर्मचारी नाम")]
        public long IdHREmployee { get; set; }

        [Display(Name = "कर्मचारी नाम")]
        public string EmployeeName { get; set; }

        [Display(Name = "जन्मस्थान")]
        public string BirthPlace { get; set; }

        [Display(Name = "मोबाइल")]
        public string MobileNumber { get; set; }

        [Display(Name = "इमेल")]
        public string Email { get; set; }

        [Display(Name = "कर्मचारी संकेत न.")]
        public string PISNumber { get; set; }

        [Display(Name = "नियुक्ति मिति")]
        public string AppointmentDateNP { get; set; }

        public string ImageName { get; set; }

        [Display(Name = "कार्यालय")]
        public long IdHRCompany { get; set; }

        [Display(Name = "जागिर")]
        public long IdJob { get; set; }

        [Display(Name = "प्रकार")]
        public string KaajTypeTitle { get; set; }

        public string FiscalYear { get; set; }

        [Display(Name = "शुरुको मिति")]
        public string KaajFromNP { get; set; }

        [Display(Name = "अन्तिम मिति")]
        public string KaajToNP { get; set; }

        [Display(Name = "शुरुको मिति")]
        public System.DateTime KaajFromDate { get; set; }

        [Display(Name = "अन्तिम मिति")]
        public Nullable<System.DateTime> KaajToDate { get; set; }

        [Display(Name = "वर्ष")]
        public int KaajYearNP { get; set; }

        [Display(Name = "आउने समय")]
        public Nullable<System.TimeSpan> CheckInTime { get; set; }

        [Display(Name = "जाने समय")]
        public Nullable<System.TimeSpan> CheckOutTime { get; set; }

        [Display(Name = "फारम")]
        public string Document { get; set; }

        [Display(Name = "राष्ट्रिय हो/होइन ?")]
        public string IsNational { get; set; }

        public string KaajRegistrationNumber { get; set; }

        [Display(Name = "काज शीर्षक")]
        public string KaajTitle { get; set; }

        [Display(Name = "काज शीर्षक (संक्षिप्त)")]
        public string KaajShortName { get; set; }

        [Display(Name = "काज उद्देश्य")]
        public string kaajPurpose { get; set; }

        [Display(Name = "सवारी साधन")]
        public string VehicleUsedInKaaj { get; set; }

        [Display(Name = "काज खर्च")]
        public string KaajExpenses { get; set; }

        [Display(Name = "कार्यालय")]
        public string HRCompanyName { get; set; }

        [Display(Name = "पद")]
        public string DesignationTitle { get; set; }


        public int HRDesignationRank { get; set; }

        [Display(Name = "शाखा/सेक्सन")]
        public string DivisionName { get; set; }

        [Display(Name = "अवस्था")]
        public string KaajStatus { get; set; }

        [Display(Name = "अवस्था")]
        public Nullable<int> IdKaajStatus { get; set; }

        [Display(Name = "काज स्थान")]
        public string KaajLocation { get; set; }

        [Display(Name = "स्वीकृत गर्ने")]
        public string KaajApprovalEmployeeName { get; set; }

        [Display(Name = "स्वीकृत टिप्पणी")]
        public string KaajApprovalRemark { get; set; }

        [Display(Name = "सिफारिस टिप्पणी")]
        public string KaajRecommendationRemark { get; set; }

        [Display(Name = "देश")]
        public string KaajCountry { get; set; }

        [Display(Name = "स्वीकृत गर्ने")]
        public Nullable<long> KaajIdApprovedBy { get; set; }

        [Display(Name = "सिफारिस गर्ने")]
        public Nullable<long> KaajIdRecommendedBy { get; set; }

        [Display(Name = "स्वीकृत मिति")]
        public Nullable<System.DateTime> KaajApprovedOn { get; set; }

        [Display(Name = "सिफारिस मिति")]
        public Nullable<System.DateTime> KaajRecommendedOn { get; set; }

        public string Gender { get; set; }
        public Nullable<int> IdKaajType { get; set; }
    }
}
