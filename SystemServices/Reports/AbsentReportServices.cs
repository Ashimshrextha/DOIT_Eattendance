using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using SystemDatabase;
using SystemInterfaces.Reports;
using SystemUnitOfWork.Interfaces;

namespace SystemServices.Reports
{
    public class AbsentReportServices : IAbsentReportServices<proc_EmployeeAbsentReport_Result>
    {
        public IUnitOfWork UnitOfWork { get; }
        public AbsentReportServices(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");
            UnitOfWork = unitOfWork;
        }

        public virtual async Task<ICollection<proc_EmployeeAbsentReport_Result>> GetResult(long? IdHRCompany, long? HRCompanyDivision, int? idJobStatus, DateTime date)
        {
            try
            {
                object[] obj =
           {
                new SqlParameter() {ParameterName = "@p1", SqlDbType = SqlDbType.BigInt, Value= IdHRCompany},
                new SqlParameter() {ParameterName = "@p2", SqlDbType = SqlDbType.BigInt, Value= HRCompanyDivision},
                new SqlParameter() {ParameterName = "@paramIdJobStatus", SqlDbType = SqlDbType.Int, Value= idJobStatus??(object)DBNull.Value},
                new SqlParameter() {ParameterName = "@p3", SqlDbType = SqlDbType.Date, Value= date},
                new SqlParameter() {ParameterName = "@paramSearch", SqlDbType = SqlDbType.NVarChar, Value= ""}
            };
                return await UnitOfWork.Db.Database.SqlQuery<proc_EmployeeAbsentReport_Result>("exec proc_EmployeeAbsentReport @p1,@p2,@paramIdJobStatus,@p3,@paramSearch", obj).ToListAsync();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public virtual async Task<IPagedList<proc_EmployeeAbsentReport_Result>> GetResult(long? IdHRCompany, long? HRCompanyDivision, int? idJobStatus, DateTime date, int pageNumber, int pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                object[] obj =
            {
                new SqlParameter() {ParameterName = "@p1", SqlDbType = SqlDbType.BigInt, Value= IdHRCompany},
                new SqlParameter() {ParameterName = "@p2", SqlDbType = SqlDbType.BigInt, Value= HRCompanyDivision},
                new SqlParameter() {ParameterName = "@paramIdJobStatus", SqlDbType = SqlDbType.Int, Value= idJobStatus??(object)DBNull.Value},
                new SqlParameter() {ParameterName = "@p3", SqlDbType = SqlDbType.Date, Value= date},
                new SqlParameter() {ParameterName = "@paramSearch", SqlDbType = SqlDbType.NVarChar, Value= searchKey}
            };

                var model = (await UnitOfWork.Db.Database.SqlQuery<proc_EmployeeAbsentReport_Result>("exec proc_EmployeeAbsentReport @p1,@p2,@paramIdJobStatus,@p3,@paramSearch", obj).ToListAsync());
                return model.OrderBy(orderingBy + " " + orderingDirection).ToPagedList(pageNumber, pageSize);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }
    }
}
