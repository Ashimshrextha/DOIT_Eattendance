using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.SystemSetting;
using SystemStores.GlobalModels;

namespace SystemViewModels.SystemSetting
{
    public class HREmployeeGenderViewModel:BreadCrumbModel
    {
        public HREmployeeGenderModel DataModel { get; set; }
        public IPagedList<HREmployeeGenderModel> DataModelList { get; set; }
        public ICollection<HREmployeeSex> DBModelList { get; set; }
    }
    public class HREmployeeGenderViewModelList : BreadCrumbModel
    {
        public HREmployeeSex DBModel { get; set; }
        public IPagedList<HREmployeeSex> DBModelList { get; set; }
    }
}
