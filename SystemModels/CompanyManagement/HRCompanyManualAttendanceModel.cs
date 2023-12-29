using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemModels.Auditable;

namespace SystemModels.CompanyManagement
{
   public class HRCompanyManualAttendanceModel: AuditableEntity<long>
    {
        public long Id { get; set; }
        [Display(Name = "कार्यालय")]
        public long IdHrCompany { get; set; }
        [Display(Name = "स्विकृत गर्ने कर्मचारी")]
        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Range(1, double.PositiveInfinity, ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]

        public Nullable<long> IdApprovedBy { get; set; }
        public Nullable<System.DateTime> ApprovedOn { get; set; }
        public string ApprovedBy { get; set; }
        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "हाजिरी (Excel)")]
        public string Document { get; set; }
        public string FolderName { get; set; }

        [Display(Name = "अवस्था")]
        public Nullable<int> IdAttendanceStatus { get; set; }

        [Display(Name = "टिप्पणी")] 
        public string Remark { get; set; }

        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Display(Name = "प्रमाणित हाजिरी विवरण")]
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] ContentData { get; set; }

        [Display(Name = "कर्मचारी")]
        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Range(1, double.PositiveInfinity, ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]

        public Nullable<long> IdHREmployee { get; set; }

        [Display(Name = "हाजिरी मिति")]
        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        public string PunchDateNp { get; set; }



        [Display(Name = "हाजिरी मिति")]
        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        public DateTime PunchDate { get; set; }

        [Display(Name = "समय")]
        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        public TimeSpan PunchTime { get; set; }
    }
}
