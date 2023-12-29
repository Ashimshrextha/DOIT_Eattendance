using PagedList;
using System.Collections.Generic;
using System.Web;
using SystemDatabase;
using SystemModels.EmployeeManagement;
using SystemStores.GlobalModels;

namespace SystemViewModels.EmployeeManagement
{
    public class HREmployeeEducationViewModel:BreadCrumbModel
    {
        public HREmployeeEducationModel DataModel { get; set; }

        public PagedList<HREmployeeEducationModel> DataModelList { get; set; }

		public ICollection<HREmployeeEducation> DBModelList { get; set; }
        public HttpPostedFileBase FileToUpload { get; set; }
    }
    public class HREmployeeEducationViewModelList : BreadCrumbModel
    {
        public HREmployeeEducation DBModel { get; set; }
		public IPagedList<HREmployeeEducation> DBModelList { get; set; }
    }
}
