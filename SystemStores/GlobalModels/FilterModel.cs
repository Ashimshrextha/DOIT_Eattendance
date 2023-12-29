using System;
using System.ComponentModel.DataAnnotations;
using static SystemStores.ENUMData.EnumGlobal;

namespace SystemStores.GlobalModels
{
    public class FilterModel
    {
        [Display(Name = "श्रेणी/तह")]
        public long? IdEmployeeCategory { get; set; }

        [Display(Name = "Parent Organization")]
        public string ParentHRCompany { get; set; }

        [Display(Name = "पद")]
        public long? IdHRCompanyDesignation { get; set; }

        [Display(Name = "पद")]
        public string Designation { get; set; }

        [Display(Name = "कार्यालय")]
        public long? IdHRCompany { get; set; }

        [Display(Name = "शाखा/सेक्सन")]
        public long? IdDivision { get; set; }

        [Display(Name = "भूमिका चयन गर्नुहोस्")]
        public int? IdRole { get; set; }

        [Display(Name = "कर्मचारीको नाम")]
        public long? IdHREmployee { get; set; }

        [Display(Name = "वर्ष")]
        public int? YearNp { get; set; }

        [Display(Name = "महिना")]
        public int? MonthNp { get; set; }

        [Display(Name = "Date(AD)")]
        public DateTime? Date { get; set; }

        [Display(Name = "From Date(AD)")]
        public DateTime? FromDate { get; set; }

        [Display(Name = "To Date(AD)")]
        [DataType(DataType.Date)]
        public DateTime? TodateDate { get; set; }

        [RegularExpression("((([0-9][0-9][0-9][1-9])|([1-9][0-9][0-9][0-9])|([0-9][1-9][0-9][0-9])|([0-9][0-9][1-9][0-9]))-((0[13578])|(1[02]))-((0[1-9])|([12][0-9])|(3[01])))|((([0-9][0-9][0-9][1-9])|([1-9][0-9][0-9][0-9])|([0-9][1-9][0-9][0-9])|([0-9][0-9][1-9][0-9]))-((0[469])|11)-((0[1-9])|([12][0-9])|(30)))|(((000[48])|([0-9]0-9)|([0-9][1-9][02468][048])|([1-9][0-9][02468][048]))-02-((0[1-9])|([12][0-9])))|((([0-9][0-9][0-9][1-9])|([1-9][0-9][0-9][0-9])|([0-9][1-9][0-9][0-9])|([0-9][0-9][1-9][0-9]))-02-((0[1-9])|([1][0-9])|([2][0-8])))", ErrorMessage = "{0} को ढाँचा मिलेन (YYYY-MM-DD) ")]
        [Display(Name = "मिति")]
        [MaxLength(10)]
        public string DateNp { get; set; }

        [Display(Name = "मिति देखि")]
        public string FromDateNp { get; set; }

        [Display(Name = "मिति सम्म")]
        public string TodateDateNp { get; set; }

        [Display(Name = "जागिर अवस्था")]
        public int? IdJobStatus { get; set; }

        [Display(Name = "शिफ्ट शीर्षक")]
        public long? idShiftTitle { get; set; }

        public ActiveDeActiveEmployee EmployeeStatus { get; set; }
        public int? IdActiveEmployee { get; set; }
    }
}
