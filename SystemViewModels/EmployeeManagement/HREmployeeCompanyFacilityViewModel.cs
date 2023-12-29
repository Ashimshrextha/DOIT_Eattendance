using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.EmployeeManagement;
using SystemStores.GlobalModels;

namespace SystemViewModels.EmployeeManagement
{
    public class HREmployeeCompanyFacilityViewModel:BreadCrumbModel
    {
        public HREmployeeCompanyFacilityModel DataModel { get; set; }
        public PagedList<HREmployeeCompanyFacilityModel> DataModelList { get; set; }
        public ICollection<HREmployeeCompanyFacility> DBModelList { get; set; }
	}
	public class HREmployeeCompanyFacilityViewModelList : BreadCrumbModel
    {
        public HREmployeeCompanyFacility DBModel { get; set; }
        public IPagedList<HREmployeeCompanyFacility> DBModelList { get; set; }
	}
}
