using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.CompanyManagement
{
    [Table("HRCompanyHistory")]
    public class HRCompanyHistoryModel : EntityId<long>
    {
        [Display(Name = "कार्यालय")]
        public long IdHRCompany { get; set; }

        [Display(Name = "पुरानो कार्यालय नाम")]
        public string OldHRCompanyName { get; set; }

        [Display(Name = "नँया कार्यालय नाम")]
        public string NewHRCompanyName { get; set; }

        [Display(Name = "पुरानो प्रमुख कार्यालय")]
        public Nullable<long> OldIdParent_HRCompany { get; set; }

        [Display(Name = "नँया प्रमुख कार्यालय")]
        public Nullable<long> NewIdParent_HRCompany { get; set; }

        [Display(Name = "पुरानो कार्यालयको प्रकार")]
        public Nullable<int> OldIdHRCompanyType { get; set; }

        [Display(Name = "नँया कार्यालयको प्रकार")]
        public Nullable<int> NewIdHRCompanyType { get; set; }

        [Display(Name = "सिर्जना मिति")]
        [DataType(DataType.DateTime)]
        public System.DateTime CreatedOn { get; set; }

        [Display(Name = "सिर्जना गर्ने")]
        [MaxLength(250)]
        public string CreatedBy { get; set; }

    }
}
