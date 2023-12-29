using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using SystemDatabase;
using SystemUnitOfWork.Interfaces;

namespace SystemServices.Reports
{
    public class ShiftRoasterReportServices
    {
        public IUnitOfWork UnitOfWork { get; }
        public ShiftRoasterReportServices(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");
            UnitOfWork = unitOfWork;
        }

        public virtual async Task<ICollection<proc_GetEmployeeShiftRoasterReport_Result>> GetResult(long? IdHRCompany, long? HRCompanyDivision, long? idShiftTitle, DateTime date, string searchKey = "")
        {
            try
            {
                object[] obj =
           {
                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value= IdHRCompany},
                new SqlParameter() {ParameterName = "@paramIdHRCompanyDivision", SqlDbType = SqlDbType.BigInt, Value= HRCompanyDivision},
                new SqlParameter() {ParameterName = "@paramIdShiftTitle", SqlDbType = SqlDbType.BigInt, Value= idShiftTitle},
                new SqlParameter() {ParameterName = "@paramDate", SqlDbType = SqlDbType.Date, Value= date},
                new SqlParameter() {ParameterName = "@paramSearch", SqlDbType = SqlDbType.NVarChar, Value= searchKey}
            };
                return await UnitOfWork.Db.Database.SqlQuery<proc_GetEmployeeShiftRoasterReport_Result>("exec proc_GetEmployeeShiftRoasterReport @paramIdHRCompany,@paramIdHRCompanyDivision,@paramIdShiftTitle,@paramDate,@paramSearch", obj).ToListAsync();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }
    }
}
