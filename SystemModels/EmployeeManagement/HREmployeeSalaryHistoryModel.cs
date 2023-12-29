using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.EmployeeManagement
{
    [Table("HREmployeeSalaryHistory")]
    public class HREmployeeSalaryHistoryModel : AuditableEntity<long>
    {
        [Display(Name = "कर्मचारी")]
        public long IdHREmployee { get; set; }

        [Display(Name = "तलब मिति")]
        [DataType(DataType.DateTime)]
        public System.DateTime SalaryDate { get; set; }

        [Required]
        [Display(Name = "तलब")]
        public decimal BasicSalary { get; set; }

        [Display(Name = "अल्लोवांस")]
        public decimal Allowance { get; set; }

        [Display(Name = "OT Allowance")]
        public decimal OverTimeAllowance { get; set; }

        [Required]
        [Display(Name = "कुल बिदाको दिन")]
        public int TotalLeaveTakenDay { get; set; }

        [Required]
        [Display(Name = "भुक्तानी बिदा दिन")]
        public int TotalPaidLeaveDay { get; set; }

        [Required]
        [Display(Name = "बिदा रकम/दिन")]
        public decimal LeaveDayRatePerDay { get; set; }

        [Required]
        [Display(Name = "कुल काम गरिरहेको दिन")]
        public int TotalWorkingDay { get; set; }

        [Required]
        [Display(Name = "कुल काम गरेको दिन")]
        public int TotalWorkedDay { get; set; }

        [Required]
        [Display(Name = "काम गरिरहेको(मिनेट)")]
        public int TotalWorkingInMinute { get; set; }

        [Required]
        [Display(Name = "काम गरेको (मिनेट)")]
        public int TotalWorkedInMinute { get; set; }

        [Display(Name = "जम्मा ओटी (मिनेट)")]
        public int TotalOverTimeInMinute { get; set; }

        [Display(Name = "ओटी मूल्याङ्कन(/मिनेट)")]
        public decimal OverTimeRatePerMinute { get; set; }

        [Display(Name = "तीएडीए")]
        public decimal TADA { get; set; }

        [Display(Name = "जम्मा पेशकी")]
        public decimal TotalAdvance { get; set; }

        [Display(Name = "घटेको पेशकी")]
        public decimal DeducatedAdvance { get; set; }

        [Display(Name = "बाँकी पेशकी")]
        public decimal BalanceAdvance { get; set; }

        [Required]
        [Display(Name = "जम्मा कुल रकम")]
        public decimal GrandTotalSalary { get; set; }

        [Required]
        [Display(Name = "तलब पाएको")]
        public decimal ReceivalableSalary { get; set; }

        [Required]
        [Display(Name = "पाएको रकम")]
        public decimal ReceivedSalary { get; set; }

        [Display(Name = "तलब पाएको छ/छैन")]
        public bool IsSalaryReceived { get; set; }

        [Required]
        [Display(Name = "प्राप्त गरेको")]
        [MaxLength(250)]
        public string SalaryReceivedBy { get; set; }

        [Required]
        [Display(Name = "प्राप्त मिति")]
        [DataType(DataType.DateTime)]
        public Nullable<System.DateTime> SalaryReceivedDate { get; set; }

        [Required]
        [Display(Name = "भुक्तानी मोड")]
        [MaxLength(50)]
        public string SalaryPaidVia { get; set; }

        [Display(Name = "बैंक ए/सी")]
        [MaxLength(50)]
        public string BankAccountNumber { get; set; }

        [Display(Name = "सीआईटी फन्ड")]
        public Nullable<decimal> CITFund { get; set; }

        [Display(Name = "बीमा रकम")]
        public Nullable<decimal> InsuranceAmount { get; set; }

        [Display(Name = "ट्याक्स रकम")]
        public Nullable<decimal> TaxAmount { get; set; }

        [Display(Name = "भ्याट रकम")]
        public Nullable<decimal> VatAmount { get; set; }

        [Display(Name = "टिप्पणी")]
        [MaxLength(500)]
        public string Remark { get; set; }
    }
}
