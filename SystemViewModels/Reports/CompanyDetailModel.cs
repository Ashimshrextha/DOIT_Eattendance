using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemStores.GlobalModels;
using SystemViewModels.Shared;

namespace SystemViewModels.Reports
{
    public class CompanyDetailViewModelList
    {
        public long? IdHrCompany { get; set; }
        public long? IdParCompany { get; set; }
        public long? CompanyNameEng { get; set; }
        public long? CompanyNameNep { get; set; }
    }
}
