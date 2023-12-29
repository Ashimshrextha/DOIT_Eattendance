using PagedList;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SystemDatabase;

namespace SystemInterfaces.Home
{
    public interface IHomeServices
    {
        Task<IPagedList<proc_DaillyPresentAttendance_Result>> GetPresentEmployee(long? idHRCompany, int? idRoleType,long? IdHRCompanyDivision, DateTime date, int pageNumber, int pageSize, string orderingBy, string orderingDirection, string searchKey = "");
        Task<ICollection<proc_DaillyPresentAttendance_Result>> GetPresentEmployee(long? idHRCompany, int? idRoleType, long? IdHRCompanyDivision, DateTime date, string searchKey = "");
        Task<ICollection<proc_DaillyAbsentAttendance_Result>> GetAbsentEmployeeList(long? idHRCompany, int? idRoleType, long? IdHRCompanyDivision, DateTime date, string searchKey = "");


        

        Task<IPagedList<proc_DaillyEmployeeOnLeaveAttendance_Result>> GetEmployeeOnLeaveList(long? idHRCompany, int? idRoleType, long? IdHRCompanyDivision, DateTime date, int pageNumber, int pageSize, string orderingBy, string orderingDirection, string searchKey = "");
       
        
        
        
        Task<IPagedList<proc_EmployeeOnKaaj_Result>> GetEmployeeOnKaajList(long? idHRCompany, int? idRoleType, long? IdHRCompanyDivision, DateTime date, int pageNumber, int pageSize, string orderingBy, string orderingDirection, string searchKey = "");
        Task<ICollection<proc_EmployeeOnKaaj_Result>> GetEmployeeOnKaajList(long? idHRCompany, int? idRoleType,long? IdHRCompanyDivision, DateTime date);



        Task<IPagedList<proc_LateAttendance_Result>> GetLateAttendanceList(long? idHRCompany, long? idHRCompanyDivision, int? idJobStatus, long? idHREmployee, DateTime date, int pageNumber, int pageSize, string orderingBy, string orderingDirection, string searchKey = "");



        Task<IPagedList<proc_EarlyCheckoutReport_Result>> GetEarlyCheckoutEmpList(long? idHRCompany, long? idHRCompanyDivision, int? idJobStatus, long? idHREmployee, DateTime date, int pageNumber, int pageSize, string orderingBy, string orderingDirection, string searchKey = "");

    }
}
