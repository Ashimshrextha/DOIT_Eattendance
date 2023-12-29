using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.SystemSetting;
using SystemStores.GlobalModels;

namespace SystemViewModels.SystemSetting
{
    public class HREmployeeNationalIdentityTypeViewModel:BreadCrumbModel
    {
        public HREmployeeNationalIdentityTypeModel DataModel { get; set; }

        public IPagedList<HREmployeeNationalIdentityTypeModel> DataModelList { get; set; }
        public ICollection<HREmployeeNationalIdentityType> DBModelList { get; set; }
    }
    public class HREmployeeNationalIdentityTypeViewModelList : BreadCrumbModel
    {
        public HREmployeeNationalIdentityType DBModel { get; set; }
        public IPagedList<HREmployeeNationalIdentityType> DBModelList { get; set; }
    }
}
