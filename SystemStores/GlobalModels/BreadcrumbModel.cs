using static SystemStores.ENUMData.EnumGlobal;

namespace SystemStores.GlobalModels
{
	public abstract class BreadCrumbModel : PaginationModel
    {
        public string BreadCrumbTitle { get; set; }
        public string BreadCrumbArea { get; set; }
        public string BreadCrumbController { get; set; }
        public string BreadCrumbActionName { get; set; }
        public string BreadCrumbBaseURL { get; set; }
        public CRUDType CRUDAction { get; set; }
        public string HeaderTitle { get; set; }
        public string FormModelName { get; set; }
        public string SearchKey { get; set; }
        public int SearchKey1 { get; set; }
		public UserSessionModel SessionDetails { get; set; }
		public TreeModel<long,string> TreeCompany { get; set; }
	}
}
