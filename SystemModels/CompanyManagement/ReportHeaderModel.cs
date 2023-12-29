using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.CompanyManagement
{
    [Table("HRCompany")]
    public class CompanyInformationModel
    {
        public string CompanyName { get; set; }
        public string CompanyNameNP { get; set; }
        public string ParentCompanyName { get; set; }
        public string ParentCompanyNameNP { get; set; }
    }
}
