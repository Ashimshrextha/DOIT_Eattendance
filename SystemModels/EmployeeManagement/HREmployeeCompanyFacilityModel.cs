using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.EmployeeManagement
{
    [Table("HREmployeeCompanyFacility")]
    public class HREmployeeCompanyFacilityModel : AuditableEntity<long>
    {
        [Display(Name = "कर्मचारी")]
        public long? IdHREmployee { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "सुविधा नाम")]
        [MaxLength(250)]
        public string FacilityName { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "टिप्पणी")]
        [MaxLength(500)]
        public string Remark { get; set; }

        [Required(ErrorMessage = "कृपया  {0} लेख्नुहोस्")]
        [Display(Name = "फिर्ता आयो/आछैन")]
        public bool IsReturnBack { get; set; }

        [Display(Name = "फिर्ता मिति")]
        [DataType(DataType.DateTime)]
        public DateTime? ReturnDate { get; set; }

        [Display(Name = "फिर्ता मिति")]
        [DataType(DataType.DateTime)]
        public string ReturnDateNp { get; set; }

    }
}
