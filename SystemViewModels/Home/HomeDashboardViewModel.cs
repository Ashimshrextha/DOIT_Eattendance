using PagedList;
using System;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.EmployeeManagement;
using SystemStores.GlobalModels;
using SystemViewModels.Shared;

namespace SystemViewModels.Home
{
    public class HomeDashboardViewModel : BreadCrumbModel
    {

    }
    public class HomeDashboardViewModelList : BreadCrumbModel
    {
        public HREmployee DBModel { get; set; }
        public HREmployeeModel DataModel { get; set; }
        public ReportHeaderViewModel Header { get; set; }
        public IPagedList<HRCompany> HRCompanyList { get; set; }
        public IPagedList<HRDevice> HRDeviceList { get; set; }
        public IPagedList<proc_GetEmployeeByDesignation_Result> DBModelEmp { get; set; }
        public IPagedList<proc_DaillyPresentAttendance_Result> DBModelPresentEmp { get; set; }
        public IPagedList<proc_DaillyAbsentAttendance_Result> DBModelAbsentEmp { get; set; }
        public ICollection<proc_DaillyPresentAttendance_Result> DBModelPresentEmpList { get; set; }
        public ICollection<proc_DaillyAbsentAttendance_Result> DBModelAbsentEmpList { get; set; }
        public ICollection<proc_DaillyEmployeeOnLeaveAttendance_Result> DBModelLeaveEmpList { get; set; }
        public ICollection<proc_EmployeeOnKaaj_Result> DBModelKaajEmpList { get; set; }
        public IEnumerable<proc_GetEmployeeCountByGender_Result> DbGender { get; set; }
        public IEnumerable<proc_GetEmployeeTodayStatusForDashboard_Result> DbEmployeeTodayStat { get; set; }
        public IEnumerable<proc_MonthlyCalendar_Result> DBModelCalendar { get; set; }
        public ICollection<proc_GetAssignedAndUnAssignedRoasterForEmployee_Result> DBModelShift { get; set; }



        public IList<proc_GetAssignedAndUnAssignedRoasterForEmployee_Result> DBModelAssignShift { get; set; }
        public IList<proc_GetAssignedAndUnAssignedRoasterForEmployee_Result> DBModelUnAssignShift { get; set; }




        public ICollection<HREmployeeLeaveBalance> DBModelLeavBalance { get; set; }
        public ICollection<HREmployeeLeaveHistory> DBModelLeaveHistory { get; set; }

        public ICollection<HREmployeeKaajHistory> DBModelKaajHistory { get; set; }

        public ICollection<HREmployeeKaajHistory> DBModelKaajApprovalHistory { get; set; }

        public ICollection<HREmployeeLeaveHistory> DBModelLeaveApprovalHistory { get; set; }
        public ICollection<HREmployeeLeaveHistory> DBModelRequestedLeaveHistory { get; set; }
        public ICollection<HREmployeeKaajHistory> DBModelRequestedKaajHistory { get; set; }

        public IEnumerable<proc_getdailyindex_Result> Hierachy { get; set; }
        public IList<HomeIndexViewModel> HierachyModel { get; set; }
        public List<ParentChildViewModel> ParentChildViewModelList { get; set; }


        public ICollection<HREmployeeKaajHistory> DBModelAdminKaajHistory { get; set; }

        public ICollection<HREmployeeKaajHistory> DBModelKaajAdminApprovalHistory { get; set; }

        public ICollection<HREmployeeLeaveHistory> DBModelLeaveAdminApprovalHistory { get; set; }





        public Nullable<int> OfficeCount { get; set; }
        public Nullable<int> ActiveOfficeCount { get; set; }
        public ICollection<HRDevice> OfficeDeviceList { get; set; }
        public Nullable<int> EmployeeCount { get; set; }
        public Nullable<int> EmployeePresentCount { get; set; }
        public Nullable<int> EmployeeAbsentCount { get; set; }

        public  List<HomeIndexViewModel> OfficeStatus { get; set; }
        public int? idRoleType { get; set; }
        public string TodayDayNp { get; set; }
        public int? ActiveDevice { get; set; }
        public int? DeactiveDevice { get; set; }
    }




    public class NavMenuListModel
    {
        public string name { get; set; }
        public string icon { get; set; }
        public List<NaveMenuModel> menu { get; set; }
    }


    public class NaveMenuModel
    {
        public string name { get; set; }
        public bool hasChildren { get; set; }
        public string url { get; set; }
        public List<MenuChildModel> children { get; set; }

    }



    public class MenuChildModel
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class PaginationHomeModel
    {
        public int Count { get; set; } 
        public int FirstItemOnPage { get; set; } 
        public bool HasNextPage { get; set; } 
        public bool HasPreviousPage { get; set; } 
        public bool IsFirstPage { get; set; } 
        public bool IsLastPage { get; set; } 
        public int LastItemOnPage { get; set; } 
        public int PageCount { get; set; } 
        public int PageNumber { get; set; } 
        public int PageSize { get; set; } 
    }
}
