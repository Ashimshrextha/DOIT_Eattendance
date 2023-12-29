using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.EmployeeManagement;
using SystemStores.GlobalModels;

namespace SystemViewModels.SystemSetting
{
    public class HRDesignationViewModel : BreadCrumbModel
    {
        public HRDesignationModel DataModel { get; set; }

        public IPagedList<HRDesignationModel> DataModelList { get; set; }
        public ICollection<HRDesignation> DBModelList { get; set; }
    }
    public class HRDesignationViewModelList : BreadCrumbModel
    {
		public HRDesignation DBModel { get; set; }

        public IPagedList<HRDesignation> DBModelList { get; set; }
    }
}
