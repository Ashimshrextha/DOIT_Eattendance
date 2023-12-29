using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using SystemDatabase;
using SystemInterfaces.CompanyManagement;
using SystemModels.CompanyManagement;
using SystemUnitOfWork.Interfaces;
using SystemUnitOfWork.UOW;

namespace SystemServices.CompanyManagement
{
    public class HRCompanyHREmployeeCovidKaajServices : BaseRepository<HREmployeeKaajHistory, HRCompanyHREmployeeCovidKaajModel>, IHRCompanyHREmployeeCovidKaajServices<HREmployeeKaajHistory, HRCompanyHREmployeeCovidKaajModel>
    {
        public HRCompanyHREmployeeCovidKaajServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }
        }

        public async Task<List<EmployeeShiftRoasterSelectedModel>> GetsByCompany(long? idHRCompany, long? idHREmployee, int? idRoleType)
        {
            object[] obj =
                {
                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value= idHRCompany},
                new SqlParameter() {ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value = idHREmployee},
                new SqlParameter() {ParameterName = "@paramIdRoleType", SqlDbType = SqlDbType.Int, Value = idRoleType},
                new SqlParameter() {ParameterName = "@paramActive", SqlDbType = SqlDbType.Int, Value = 0},
                new SqlParameter() {ParameterName = "@paramSearch", SqlDbType = SqlDbType.NVarChar, Value = ""}
            };
            var model = (await ExecuteProcedure<proc_GetEmployeeByDesignation_Result>("EXEC proc_GetEmployeeByDesignation @paramIdHRCompany,@paramIdHREmployee,@paramIdRoleType,@paramActive,@paramSearch", obj)).ToList();
            return model.Select(x => new EmployeeShiftRoasterSelectedModel { IdHREmployee = x.Id, EmployeeName = x.HREmployeeNameNP, IsChecked = true, PISNumber = x.PISNumber, DesignationTitle = x.HRDesignationTitle, DivisionName = x.HRCompanyDivisionName }).ToList();
        }


        public string kaajRegistrationNumber(long IdHRCompany,string fiscalYear)
        {
            var kaajModel = (from c in _dbSet where c.IdHrCompany == IdHRCompany && c.FiscalYear == fiscalYear select c.Id);
            long kaajMaxId = kaajModel.Count() > 0 ? kaajModel.Max() : 0;
            var registrationNumber = (from c in _dbSet where c.Id == kaajMaxId select c.KaajRegistrationNumber).FirstOrDefault();
            int KaajCount = registrationNumber != null ? int.Parse(registrationNumber.Split('-')[1]) + 1 : 1;
            var KaajRegistrationNumber = $"{fiscalYear}-{KaajCount}"; //generate registration number

            return KaajRegistrationNumber;
        }

        public async Task<int> KaajTakenNumber(long IdHRCompany, string fiscalYear,long IdhrEmployee)
        {
            var KaajTakenNumber = await CountAsync(x => x.IdHrCompany == IdHRCompany && x.FiscalYear == fiscalYear && x.IdHREmployee == IdhrEmployee) + 1;
            return KaajTakenNumber;
        }


    }
}
