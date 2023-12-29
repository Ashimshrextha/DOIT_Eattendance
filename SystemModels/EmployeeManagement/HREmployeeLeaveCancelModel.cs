using System;
using System.ComponentModel.DataAnnotations;

namespace SystemModels.EmployeeManagement
{
    public class HREmployeeLeaveCancelModel
    {

        [Display(Name = "आईडी")]
        public long Id { get; set; }

        [Display(Name = "कर्मचारी नाम")]
        public long? IdHREmployee { get; set; }

        [Display(Name = "बिदा रद्द गर्ने")]
        [DataType(DataType.DateTime)]
        public Nullable<System.DateTime> LeaveCancelFrom { get; set; }

        [Required]
        [Display(Name = "बिदा रद्द गर्ने")]
        [MaxLength(10)]
        public string LeaveCancelFromNP { get; set; }

        [Required]
        [Display(Name = "सिफारिस गर्ने")]
        public Nullable<long> IdRecommendationCancelBy { get; set; }

        [Required]
        [Display(Name = "स्वीकृत गर्ने")]
        public Nullable<long> IdApprovedCancelBy { get; set; }

        [Required]
        [Display(Name = "अवस्था")]
        public Nullable<int> IdLeaveCancelStatus { get; set; }

        [Required]
        [Display(Name = "सिफारिस टिप्पणी")]
        [MaxLength(500)]
        public string RecommendationCancelRemark { get; set; }

        [Display(Name = "सिफारिस मिति")]
        [DataType(DataType.DateTime)]
        public Nullable<System.DateTime> RecommendationCancelOn { get; set; }

        [Required]
        [Display(Name = "स्वीकृत टिप्पणी")]
        [MaxLength(500)]
        public string ApprovalCancelRemark { get; set; }

        [Display(Name = "स्वीकृत मिति")]
        [DataType(DataType.DateTime)]
        public Nullable<System.DateTime> ApprovedCancelOn { get; set; }
    }
}
