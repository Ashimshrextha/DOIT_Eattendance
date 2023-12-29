using PagedList;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SystemDatabase;

namespace SystemInterfaces.CompanyManagement
{
    public interface IHRCompanyHREmployeeShiftRoasterServices<Tentity, Tmodel>
    {
        Task<bool> DeleteHRCompanyHREmployeeShiftRoasterAsync(ICollection<Tentity> EntityList);
        Task<List<Tmodel>> GetsWeekdayModel(long? idHREmployee);
        Task<ICollection<long>> GetsAssignEmployee(long? idHRCompany, DateTime fromDate, DateTime toDate);
        Task<IPagedList<proc_GetAssignHREmployeeOfShiftRoaster_Result>> Gets(long? idHRCompany, int pageNumber, int pageSize, string orderingBy, string orderingDirection, string searchKey);
        Task<List<proc_GetEmployeeShiftRoasterDetail_Result>> Gets(long? idHREmployee);
        Task<List<Tmodel>> GetsModel(long? idHREmployee);
    }
}
