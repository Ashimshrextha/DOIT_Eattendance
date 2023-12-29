using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using SystemDatabase;
using SystemInterfaces.Home;
using SystemUnitOfWork.Interfaces;
using SystemUnitOfWork.UOW;
using static SystemStores.ENUMData.EnumGlobal;

namespace SystemServices.Home
{
    public class HomeServices : IHomeServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeServices(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }
            _unitOfWork = unitOfWork;
        }

        #region PRESENT_ABSENT
        public virtual async Task<IPagedList<proc_DaillyPresentAttendance_Result>> GetPresentEmployee(long? idHRCompany, int? idRoleType, long? IdHRCompanyDivision, DateTime date, int pageNumber, int pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                object[] obj =
                {
                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value= idHRCompany},
                new SqlParameter() {ParameterName = "@paramToDate", SqlDbType = SqlDbType.Date, Value = date},
                new SqlParameter() {ParameterName = "@paramIdRoleType", SqlDbType = SqlDbType.Int, Value = idRoleType},
                new SqlParameter() {ParameterName = "@paramIdHRCompanyDivision", SqlDbType = SqlDbType.Int, Value = IdHRCompanyDivision},
                new SqlParameter() {ParameterName = "@paramSearch", SqlDbType = SqlDbType.NVarChar, Value = searchKey}
            };
                return (await _unitOfWork.Db.Database.SqlQuery<proc_DaillyPresentAttendance_Result>("EXEC proc_DaillyPresentAttendance @paramIdHRCompany,@paramIdRoleType,@paramIdHRCompanyDivision,@paramToDate,@paramSearch", obj).ToListAsync()).OrderBy(orderingBy + " " + orderingDirection)
                 .ToPagedList(pageNumber, pageSize);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }
        public virtual async Task<ICollection<proc_DaillyPresentAttendance_Result>> GetPresentEmployee(long? idHRCompany, int? idRoleType, long? IdHRCompanyDivision, DateTime date, string searchKey = "")
        {
            try
            {
                object[] obj =
                {
                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value= idHRCompany},
                new SqlParameter() {ParameterName = "@paramToDate", SqlDbType = SqlDbType.Date, Value = date},
                new SqlParameter() {ParameterName = "@paramIdRoleType", SqlDbType = SqlDbType.Int, Value = idRoleType},
                new SqlParameter() {ParameterName = "@paramIdHRCompanyDivision", SqlDbType = SqlDbType.Int, Value = IdHRCompanyDivision},
                new SqlParameter() {ParameterName = "@paramSearch", SqlDbType = SqlDbType.NVarChar, Value = searchKey}
            };
                return await _unitOfWork.Db.Database.SqlQuery<proc_DaillyPresentAttendance_Result>("EXEC proc_DaillyPresentAttendance @paramIdHRCompany,@paramIdRoleType,@paramIdHRCompanyDivision,@paramToDate,@paramSearch", obj).ToListAsync();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }


        public virtual async Task<IPagedList<proc_DaillyAbsentAttendance_Result>> GetAbsentEmployee(long? idHRCompany, int? idRoleType, long? IdHRCompanyDivision, DateTime date, int pageNumber, int pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                object[] obj =
                {
                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value= idHRCompany},
                new SqlParameter() {ParameterName = "@paramToDate", SqlDbType = SqlDbType.Date, Value = date},
                new SqlParameter() {ParameterName = "@paramIdRoleType", SqlDbType = SqlDbType.Int, Value = idRoleType},
                new SqlParameter() {ParameterName = "@paramIdHRCompanyDivision", SqlDbType = SqlDbType.Int, Value = IdHRCompanyDivision},
                new SqlParameter() {ParameterName = "@paramSearch", SqlDbType = SqlDbType.NVarChar, Value = searchKey}
            };
                return (await _unitOfWork.Db.Database.SqlQuery<proc_DaillyAbsentAttendance_Result>("EXEC proc_DaillyAbsentAttendance @paramIdHRCompany,@paramIdRoleType,@paramIdHRCompanyDivision,@paramToDate,@paramSearch", obj).ToListAsync()).OrderBy(orderingBy + " " + orderingDirection)
                 .ToPagedList(pageNumber, pageSize);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public virtual async Task<ICollection<proc_DaillyAbsentAttendance_Result>> GetAbsentEmployeeList(long? idHRCompany, int? idRoleType, long? IdHRCompanyDivision, DateTime date, string searchKey = "")
        {
            try
            {
                object[] obj =
                {
                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value= idHRCompany},
                new SqlParameter() {ParameterName = "@paramToDate", SqlDbType = SqlDbType.Date, Value = date},
                new SqlParameter() {ParameterName = "@paramIdRoleType", SqlDbType = SqlDbType.Int, Value = idRoleType},
                new SqlParameter() {ParameterName = "@paramIdHRCompanyDivision", SqlDbType = SqlDbType.Int, Value = IdHRCompanyDivision},
                new SqlParameter() {ParameterName = "@paramSearch", SqlDbType = SqlDbType.NVarChar, Value = searchKey}
            };
                return await _unitOfWork.Db.Database.SqlQuery<proc_DaillyAbsentAttendance_Result>("EXEC proc_DaillyAbsentAttendance @paramIdHRCompany,@paramIdRoleType,@paramIdHRCompanyDivision,@paramToDate,@paramSearch", obj).ToListAsync();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        #endregion

        #region LEAVE

        public virtual async Task<IPagedList<proc_DaillyEmployeeOnLeaveAttendance_Result>> GetEmployeeOnLeaveList(long? idHRCompany, int? idRoleType, long? IdHRCompanyDivision, DateTime date, int pageNumber, int pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                object[] obj =
                {
                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value= idHRCompany},
                new SqlParameter() {ParameterName = "@paramToDate", SqlDbType = SqlDbType.Date, Value = date},
                new SqlParameter() {ParameterName = "@paramIdRoleType", SqlDbType = SqlDbType.Int, Value = idRoleType},
                new SqlParameter() {ParameterName = "@paramIdHRCompanyDivision", SqlDbType = SqlDbType.Int, Value = IdHRCompanyDivision},
                new SqlParameter() {ParameterName = "@paramSearch", SqlDbType = SqlDbType.NVarChar, Value = searchKey}
            };
                return (await _unitOfWork.Db.Database.SqlQuery<proc_DaillyEmployeeOnLeaveAttendance_Result>("EXEC proc_DaillyEmployeeOnLeaveAttendance @paramIdHRCompany,@paramIdRoleType,@paramIdHRCompanyDivision,@paramToDate", obj).ToListAsync()).OrderBy(orderingBy + " " + orderingDirection)
                 .ToPagedList(pageNumber, pageSize);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public virtual async Task<ICollection<proc_DaillyEmployeeOnLeaveAttendance_Result>> GetTodayEmployeeOnLeaveList(long? idHRCompany, int? idRoleType, long? IdHRCompanyDivision, DateTime date, string searchKey = "")
        {
            try
            {
                object[] obj =
                {
                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value= idHRCompany},
                new SqlParameter() {ParameterName = "@paramToDate", SqlDbType = SqlDbType.Date, Value = date},
                new SqlParameter() {ParameterName = "@paramIdRoleType", SqlDbType = SqlDbType.Int, Value = idRoleType},
                new SqlParameter() {ParameterName = "@paramIdHRCompanyDivision", SqlDbType = SqlDbType.Int, Value = IdHRCompanyDivision},
                new SqlParameter() {ParameterName = "@paramSearch", SqlDbType = SqlDbType.NVarChar, Value = searchKey}
            };
                return (await _unitOfWork.Db.Database.SqlQuery<proc_DaillyEmployeeOnLeaveAttendance_Result>("EXEC proc_DaillyEmployeeOnLeaveAttendance @paramIdHRCompany,@paramIdRoleType,@paramIdHRCompanyDivision,@paramToDate", obj).ToListAsync());
                ;
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        #endregion

        #region KAAJ

        public virtual async Task<IPagedList<proc_EmployeeOnKaaj_Result>> GetEmployeeOnKaajList(long? idHRCompany, int? idRoleType, long? IdHRCompanyDivision, DateTime date, int pageNumber, int pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                object[] obj =
                 {
                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value= idHRCompany},
                new SqlParameter() {ParameterName = "@IdHRCompanyDivision", SqlDbType = SqlDbType.Int, Value = IdHRCompanyDivision},
                new SqlParameter() {ParameterName = "@paramToDate", SqlDbType = SqlDbType.Date, Value = date},
                new SqlParameter() {ParameterName = "@paramIdRoleType", SqlDbType = SqlDbType.Int, Value = idRoleType},
            };
                return (await _unitOfWork.Db.Database.SqlQuery<proc_EmployeeOnKaaj_Result>("EXEC proc_EmployeeOnKaaj  @paramIdHRCompany,@paramIdRoleType,@paramToDate,@IdHRCompanyDivision", obj).ToListAsync()).OrderBy(orderingBy + " " + orderingDirection)
                             .ToPagedList(pageNumber, pageSize);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }



        public virtual async Task<ICollection<proc_EmployeeOnKaaj_Result>> GetEmployeeOnKaajList(long? idHRCompany, int? idRoleType, long? idHRCompanyDivision, DateTime date)
        {
            try
            {
                object[] obj =
                {
                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value= idHRCompany},
                new SqlParameter() {ParameterName = "@IdHRCompanyDivision", SqlDbType = SqlDbType.Int, Value = idHRCompanyDivision},
                new SqlParameter() {ParameterName = "@paramToDate", SqlDbType = SqlDbType.Date, Value = date},
                new SqlParameter() {ParameterName = "@paramIdRoleType", SqlDbType = SqlDbType.Int, Value = idRoleType},
            };
                return await _unitOfWork.Db.Database.SqlQuery<proc_EmployeeOnKaaj_Result>("EXEC proc_EmployeeOnKaaj @paramIdHRCompany,@paramIdRoleType,@paramToDate,@IdHRCompanyDivision", obj).ToListAsync();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        #endregion

        #region LATEATTENDANCE

        public virtual async Task<IPagedList<proc_LateAttendance_Result>> GetLateAttendanceList( long? idHRCompany, long? idHRCompanyDivision, int? idJobStatus, long? idHREmployee, DateTime date, int pageNumber, int pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                object[] myObjArray =
            {
                new SqlParameter() {ParameterName = "@p1", SqlDbType = SqlDbType.BigInt, Value= idHRCompany},
                new SqlParameter() {ParameterName = "@p2", SqlDbType = SqlDbType.BigInt, Value= idHRCompanyDivision},
                new SqlParameter() {ParameterName = "@paramIdJobStatus", SqlDbType = SqlDbType.Int, Value= idJobStatus??(object)DBNull.Value},
                new SqlParameter() {ParameterName = "@p3", SqlDbType = SqlDbType.BigInt, Value= idHREmployee},
                new SqlParameter() {ParameterName = "@p4", SqlDbType = SqlDbType.DateTime, Value= date},
                new SqlParameter() {ParameterName = "@paramSearch", SqlDbType = SqlDbType.NVarChar, Value= searchKey}
            };
                var model = await _unitOfWork.Db.Database.SqlQuery<proc_LateAttendance_Result>("Exec proc_LateAttendance @p1,@p2,@paramIdJobStatus,@p3,@p4,@paramSearch", myObjArray).ToListAsync();
                return model.OrderBy("HRDesignationOrder ASC")
                    .ToPagedList((int)pageNumber, (int)pageSize);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }


        #endregion


        #region EARLYCHECKOUT

        public virtual async Task<IPagedList<proc_EarlyCheckoutReport_Result>> GetEarlyCheckoutEmpList(long? idHRCompany, long? idHRCompanyDivision, int? idJobStatus, long? idHREmployee, DateTime date, int pageNumber, int pageSize, string orderingBy, string orderingDirection,  string searchKey = "")
        {
            try
            {
                object[] myObjArray =
            {
                new SqlParameter() {ParameterName = "@p1", SqlDbType = SqlDbType.BigInt, Value= idHRCompany},
                new SqlParameter() {ParameterName = "@p2", SqlDbType = SqlDbType.BigInt, Value= idHRCompanyDivision??0},
                new SqlParameter() {ParameterName = "@paramIdJobStatus", SqlDbType = SqlDbType.Int, Value= idJobStatus??0},
                new SqlParameter() {ParameterName = "@p3", SqlDbType = SqlDbType.BigInt, Value= idHREmployee??0},
                new SqlParameter() {ParameterName = "@p4", SqlDbType = SqlDbType.DateTime, Value= date},
                new SqlParameter() {ParameterName = "@paramSearch", SqlDbType = SqlDbType.NVarChar, Value= searchKey}
            };
                var model = await _unitOfWork.Db.Database.SqlQuery<proc_EarlyCheckoutReport_Result>("Exec proc_EarlyCheckoutReport @p1,@p2,@paramIdJobStatus,@p3,@p4,@paramSearch", myObjArray).ToListAsync();
                return model.OrderBy("HRDesignationOrder ASC")
                  .ToPagedList((int)pageNumber, (int)pageSize);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }



        #endregion


        #region OFFICELOGS

        public virtual async Task<IPagedList<proc_OfficeLogsInfo_Result>> GetOfficeLogsEmpList(long? idHRCompany, long? idHRCompanyDivision, long? idHREmployee, DateTime date, int pageNumber, int pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                object[] myObjArray =
            {
                new SqlParameter() {ParameterName = "@p1", SqlDbType = SqlDbType.BigInt, Value= idHRCompany},
                new SqlParameter() {ParameterName = "@p2", SqlDbType = SqlDbType.BigInt, Value= idHRCompanyDivision??0},
                new SqlParameter() {ParameterName = "@p3", SqlDbType = SqlDbType.BigInt, Value= idHREmployee??0},
                new SqlParameter() {ParameterName = "@p4", SqlDbType = SqlDbType.DateTime, Value= date},
                new SqlParameter() {ParameterName = "@paramSearch", SqlDbType = SqlDbType.NVarChar, Value= searchKey}
            };
                var model = await _unitOfWork.Db.Database.SqlQuery<proc_OfficeLogsInfo_Result>("Exec proc_OfficeLogsInfo @p1,@p2,@p3,@p4,@paramSearch", myObjArray).ToListAsync();
                return model.OrderBy("HRDesignationOrder ASC")
                  .ToPagedList((int)pageNumber, (int)pageSize);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }



        #endregion

        #region A




        public virtual async Task<ICollection<proc_GetAssignedAndUnAssignedRoasterForEmployee_Result>> GetShiftRoaterAsync(long? idHRCompany, long? idHREmployee, DateTime date, string searchKey = "")
        {
            try
            {
                object[] obj =
               {
                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value= idHRCompany},
                new SqlParameter() {ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value= idHREmployee},
                new SqlParameter() {ParameterName = "@paramToDate", SqlDbType = SqlDbType.Date, Value = date},
                new SqlParameter() {ParameterName = "@paramSearch", SqlDbType = SqlDbType.NVarChar, Value = searchKey},
            };
                return await _unitOfWork.Db.Database.SqlQuery<proc_GetAssignedAndUnAssignedRoasterForEmployee_Result>("EXEC proc_GetAssignedAndUnAssignedRoasterForEmployee @paramIdHRCompany,@paramIdHREmployee,@paramToDate,@paramSearch", obj).ToListAsync();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }




        public virtual async Task<ICollection<proc_GetHREmployeeLeaveHistoryPerYearReport_Result>> GetEmployeeLeaveStatusList(long? idHREmployee, int date)
        {
            try
            {
                object[] obj =
                {
                new SqlParameter() {ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.Int, Value = idHREmployee},
                new SqlParameter() {ParameterName = "@paramToDate", SqlDbType = SqlDbType.Int, Value = date},
            };
                return await _unitOfWork.Db.Database.SqlQuery<proc_GetHREmployeeLeaveHistoryPerYearReport_Result>("EXEC proc_GetHREmployeeLeaveHistoryPerYearReport @paramIdHREmployee,@paramToDate", obj).ToListAsync();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }
        public virtual async Task<ICollection<ifc_GetShiftInfoForEmployee_Result>> GetShiftForEmployee(long? idHRCompany, long? idHREmployee)
        {
            try
            {
                object[] obj =
             {
                     new SqlParameter() {ParameterName = "@IdHRCompany", SqlDbType = SqlDbType.BigInt, Value = idHRCompany},
                     new SqlParameter() {ParameterName = "@IdHREmployee", SqlDbType = SqlDbType.BigInt, Value = idHREmployee}
              };
                return await _unitOfWork.Db.Database.SqlQuery<ifc_GetShiftInfoForEmployee_Result>("SELECT * FROM ifc_GetShiftInfoForEmployee (@IdHRCompany,@IdHREmployee)", obj).ToListAsync();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }
        public virtual async Task<ICollection<ifc_GetUpcomingHoliday_Result>> GetUpcomingHoliday(long? idHRCompany)
        {
            try
            {
                object[] obj =
             {
                     new SqlParameter() {ParameterName = "@IdHRCompany", SqlDbType = SqlDbType.BigInt, Value = idHRCompany}
              };
                return await _unitOfWork.Db.Database.SqlQuery<ifc_GetUpcomingHoliday_Result>("SELECT * FROM ifc_GetUpcomingHoliday (@IdHRCompany)", obj).ToListAsync();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }



        public virtual async Task<IList<proc_EmployeeAbsentReport_Result>> AdminAbsentEmployeeList(long? IdHRCompany, long? HRCompanyDivision, int? idJobStatus, DateTime date, string searchKey = "")
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

                var model = (await _unitOfWork.Db.Database.SqlQuery<proc_EmployeeAbsentReport_Result>("exec proc_EmployeeAbsentReport @p1,@p2,@paramIdJobStatus,@p3,@paramSearch", obj).ToListAsync());
                return model;
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }
        public async Task<IList<proc_GetEmployeeByDesignation_Result>> AdminTotalEmployeeList(long? idHRCompany, long? idHREmployee, int? idRoleType, int? active, string searchKey = "")
        {
            try
            {
                object[] obj =
                {
                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value= idHRCompany},
                new SqlParameter() {ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value = idHREmployee},
                new SqlParameter() {ParameterName = "@paramIdRoleType", SqlDbType = SqlDbType.Int, Value = idRoleType},
                new SqlParameter() {ParameterName = "@paramActive", SqlDbType = SqlDbType.Int, Value = active},
                new SqlParameter() {ParameterName = "@paramSearch", SqlDbType = SqlDbType.NVarChar, Value = searchKey}
            };
                return await _unitOfWork.Db.Database.SqlQuery<proc_GetEmployeeByDesignation_Result>("EXEC proc_GetEmployeeByDesignation @paramIdHRCompany,@paramIdHREmployee,@paramIdRoleType,@paramActive,@paramSearch", obj).ToListAsync();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }
       
        public virtual async Task<proc_GetMonthlyWorkingSummaryReport_Result> GetMonthlyDetailsByID(long? idHRCompany, long? idHRCompanyDivision, int? idJobStatus, long? idHREmployee, DateTime fromDate, DateTime toDate, string searchKey = "")
        {
            try
            {
                object[] myObjArray =
            {
                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value= idHRCompany},
                new SqlParameter() {ParameterName = "@paramIdHRCompanyDivision", SqlDbType = SqlDbType.BigInt, Value= idHRCompanyDivision},
                new SqlParameter() {ParameterName = "@paramIdJobStatus", SqlDbType = SqlDbType.Int, Value= idJobStatus??(object)DBNull.Value},
                new SqlParameter() {ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value= idHREmployee},
                new SqlParameter() {ParameterName = "@paramFromDate", SqlDbType = SqlDbType.Date, Value= fromDate},
                new SqlParameter() {ParameterName = "@paramToDate", SqlDbType = SqlDbType.Date, Value= toDate},
                new SqlParameter() {ParameterName = "@paramSearch", SqlDbType = SqlDbType.NVarChar, Value= searchKey}
            };
                var model = (await _unitOfWork.Db.Database.SqlQuery<proc_GetMonthlyWorkingSummaryReport_Result>("Exec proc_GetMonthlyWorkingSummaryReport @paramIdHRCompany,@paramIdHREmployee,@paramIdHRCompanyDivision,@paramIdJobStatus,@paramFromDate,@paramToDate,@paramSearch", myObjArray).FirstOrDefaultAsync());
                return model;
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }





        public virtual async Task<ICollection<proc_DaillyAbsentAttendance_Result>> GetSectionAbsentEmployeeList(long? idHRCompany, long? IdHRCompanyDivision, DateTime date, string searchKey = "")
        {
            try
            {
                object[] obj =
                {
                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value= idHRCompany},
                new SqlParameter() {ParameterName = "@paramToDate", SqlDbType = SqlDbType.Date, Value = date},
                new SqlParameter() {ParameterName = "@paramSearch", SqlDbType = SqlDbType.NVarChar, Value = searchKey},
                new SqlParameter() {ParameterName = "@paramIdHRCompanyDivision", SqlDbType = SqlDbType.Int, Value = IdHRCompanyDivision},
            };
                return await _unitOfWork.Db.Database.SqlQuery<proc_DaillyAbsentAttendance_Result>("SELECT * FROM ifc_DaillyAbsentAttendance(@paramIdHRCompany,@paramToDate,@paramSearch,@paramIdHRCompanyDivision)", obj).ToListAsync();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }




        public virtual async Task<ICollection<ifc_GetEmployeeKaajHistory_Result>> GetEmployeeKaajHistory(long? idHRemployee)
        {
            try
            {
                object[] obj =
             {
                     new SqlParameter() {ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value = idHRemployee}
              };
                return await _unitOfWork.Db.Database.SqlQuery<ifc_GetEmployeeKaajHistory_Result>("SELECT * FROM ifc_GetEmployeeKaajHistory (@paramIdHREmployee)", obj).ToListAsync();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

  
        #endregion

    }
}
