using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.SystemSetting;
using SystemStores.GlobalModels;

namespace SystemViewModels.SystemSetting
{
    public class HREmployeeRelationTypeViewModel:BreadCrumbModel
    {
        public HREmployeeRelationTypeModel DataModel { get; set; }
        public IPagedList<HREmployeeRelationTypeModel> DataModelList { get; set; }
        public ICollection<HREmployeeRelationType> DBModelList { get; set; }
    }
    public class HREmployeeRelationTypeViewModelList:BreadCrumbModel
    {
        public HREmployeeRelationTypeModel DBModel { get; set; }
        public IPagedList<HREmployeeRelationType> DBModelList { get; set; }
    }
}
