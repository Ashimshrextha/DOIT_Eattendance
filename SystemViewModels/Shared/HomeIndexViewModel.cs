using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemViewModels.Shared
{

public class ParentChildViewModel
    {
        public long? ParentID { get; set; }
        public List<HomeIndexViewModel> ChildList { get; set; }

        public HomeIndexViewModel parentDetails { get; set; }
    }
  public  class HomeIndexViewModel
    {
        public Nullable<long> IdHrcompany { get; set; }
        public string CompanyName { get; set; }
        public string CompanyNameNp { get; set; }
        public Nullable<long> IdParentCompany { get; set; }
        public Nullable<long> PresentCount { get; set; }
        public Nullable<long> AbsentCount { get; set; }
        public Nullable<long> LeaveCount { get; set; }
        public long? ParentID { get; set; }
        public List<HomeIndexViewModel> ChildList { get; set; }

    }
}
