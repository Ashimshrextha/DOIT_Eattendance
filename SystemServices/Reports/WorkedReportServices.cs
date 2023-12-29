using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Dynamic;
using SystemDatabase;
using SystemInterfaces.Reports;
using SystemUnitOfWork.Interfaces;

namespace SystemServices.Reports
{
    public class WorkedReportServices : IWorkedReportServices<proc_GetTotalWorkedReport_Result>
    {
        public IUnitOfWork UnitOfWork { get; }
        public WorkedReportServices(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }
            UnitOfWork = unitOfWork;
        }

        public virtual IPagedList<proc_GetTotalWorkedReport_Result> GetPagedList(long? idHRCompany, long? idHRCompanyDivision, int? idJobStatus, long? idHREmployee, DateTime date, int pageNumber, int pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                object[] myObjArray =
            {
                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value= idHRCompany},
                new SqlParameter() {ParameterName = "@paramIdHRCompanyDivision", SqlDbType = SqlDbType.BigInt, Value= idHRCompanyDivision},
                new SqlParameter() {ParameterName = "@paramIdJobStatus", SqlDbType = SqlDbType.Int, Value= idJobStatus??(object)DBNull.Value},
                new SqlParameter() {ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value= idHREmployee},
                new SqlParameter() {ParameterName = "@paramDate", SqlDbType = SqlDbType.DateTime, Value= date},
                new SqlParameter() {ParameterName = "@paramSearch", SqlDbType = SqlDbType.NVarChar, Value= searchKey}   
            };
                var model = UnitOfWork.Db.Database.SqlQuery<proc_GetTotalWorkedReport_Result>("EXEC proc_GetTotalWorkedReport @paramIdHRCompany,@paramIdHRCompanyDivision,@paramIdJobStatus,@paramIdHREmployee,@paramDate,@paramSearch", myObjArray).ToList();
                return model.OrderBy("HRDesignationOrder ASC").ToPagedList(pageNumber, pageSize);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public virtual ICollection<proc_GetTotalWorkedReport_Result> Gets(long? idHRCompany, long? idHRCompanyDivision, int? idJobStatus, long? idHREmployee, DateTime date)
        {
            try
            {
                object[] myObjArray =
            {
                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value= idHRCompany},
                new SqlParameter() {ParameterName = "@paramIdHRCompanyDivision", SqlDbType = SqlDbType.BigInt, Value= idHRCompanyDivision},
                new SqlParameter() {ParameterName = "@paramIdJobStatus", SqlDbType = SqlDbType.Int, Value= idJobStatus??(object)DBNull.Value},
                new SqlParameter() {ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value= idHREmployee},
                new SqlParameter() {ParameterName = "@paramDate", SqlDbType = SqlDbType.DateTime, Value= date},
                new SqlParameter() {ParameterName = "@paramSearch", SqlDbType = SqlDbType.NVarChar, Value= ""}
            };
                return UnitOfWork.Db.Database.SqlQuery<proc_GetTotalWorkedReport_Result>("EXEC proc_GetTotalWorkedReport @paramIdHRCompany,@paramIdHRCompanyDivision,@paramIdJobStatus,@paramIdHREmployee,@paramDate,@paramSearch", myObjArray).ToList();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }
    }
}
