using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemModels.Auditable;

namespace SystemModels.CompanyManagement
{
   public class HRCompanyOfficeLogsModel: AuditableEntity<long>
    {

        public long Id { get; set; }



        [Display(Name = "कार्यालय")]
        public long IdHRCompany { get; set; }


        [Display(Name = "कर्मचारी")]
        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        [Range(1, double.PositiveInfinity, ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        public long IdHREmployee { get; set; }



        [Display(Name = "अफिस मिति")]
        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        public Nullable<System.DateTime> OfficeDate { get; set; }


        [Display(Name = "अफिस मिति")]
        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        public string OfficeDateNp { get; set; }


        [Display(Name = "समय")]
        [Required(ErrorMessage = "कृपया  {0} चयन गर्नुहोस्")]
        public Nullable<System.TimeSpan> OfficeTime { get; set; }
        [Display(Name = "टिप्पणी")]
        public string Remarks { get; set; }




    }
}
