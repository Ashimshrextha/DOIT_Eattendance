using PagedList;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using SystemDatabase;
using SystemInterfaces.DeviceManagement;
using SystemModels.DeviceManagement;
using SystemUnitOfWork.Interfaces;
using SystemUnitOfWork.UOW;

namespace SystemServices.DeviceManagement
{
    public class DeviceLogsServices : BaseRepository<DeviceLog, DeviceLogsModel>, IDeviceLogsServices<DeviceLog>
    {
        public DeviceLogsServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }
        }

        public virtual async Task<IPagedList<DeviceLog>> GetsBySearchKey(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                var model = await FindAllAsync(x => x.PunchDate != null);
                return model.OrderBy(orderingBy + " " + orderingDirection)
                .ToPagedList((int)pageNumber, (int)pageSize);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public virtual async Task<ICollection<proc_GetDeviceLogsByPunchedDate_Result>> GetResultByPunchedDate(long? idEnroll, long idHRCompany, DateTime fromDate, DateTime toDate)
        {
            try
            {
                object[] myObjArray =
            {
                new SqlParameter() {ParameterName = "@paramIdEnroll", SqlDbType = SqlDbType.Int, Value= idEnroll},
                new SqlParameter() { ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.Int, Value = idHRCompany},
                new SqlParameter() {ParameterName = "@paramFromDate", SqlDbType = SqlDbType.Date, Value= fromDate},
                new SqlParameter() {ParameterName = "@paramToDate", SqlDbType = SqlDbType.Date, Value= toDate},
            };
                return (await UnitOfWork.Db.Database.SqlQuery<proc_GetDeviceLogsByPunchedDate_Result>("Exec proc_GetDeviceLogsByPunchedDate @paramIdEnroll,@paramIdHRCompany,@paramFromDate,@paramToDate", myObjArray).ToListAsync());
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }
    }
}
