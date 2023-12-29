using AttendanceManagementSystem.App_Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemDatabase;
using SystemStores.GlobalModels;
using SystemStores.PairModels;
using SystemUnitOfWork.Interfaces;
using SystemUnitOfWork.UOW;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;
using SystemModels.DeviceManagement;
using OfficeOpenXml;
using SystemModels.CompanyManagement;

namespace AttendanceManagementSystem.Controllers
{
    [AuthenticationFilter]
    public class BaseController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;

        public BaseController()
        {
            this._unitOfWork = new UnitOfWork();
        }
     public string GetApprovalEmployeeEmail(long? id)
        {
            try
            {
                return this._unitOfWork.Db.Set<HREmployee>().Where(x => x.Id == id).Select(x => x.Email).FirstOrDefault();
            }
            catch
            {
                return "info@pas.com.np";
            }
        }
       public enum PairModelTitle
        {
            HRLanguage,
            SystemLanguage,
            BloodGroup,
            Gender,
            Religion,
            Relation,
            MaritalStatus,
            SystemMenu,
            SystemStructure,
            SystemStructureBySystemMenu,
            HRCompanyType,
            HRCompany,
            HRCompanyWithCode,
            HRCompanyCode,
            State,
            Country,
            District,
            City,
            CityCategory,
            Division,
            DivisionType,
            Employee,
            EmployeeById,
            EmployeeRecommended,
            EmployeeApproved,
            EmployeeActive,
            EmployeeMannualAttendanceRecommended,
            EmployeeMannualAttendanceApproved,
            DeviceModel,
            Device,
            LeaveType,
            KaajType,
            SavingLeaveType,
            Designation,
            DesignationLeaveApproval,
            JobStatus,
            HREmployeePositionStatusType,
            HREmployeePositionStatusReportType,
            EmployeeCategory,
            Role,
            NationalIdentityType,
            ServiceType,
            SystemDetailCategory,
            ContactType,
            EducationType,
            LeaveApprovalStatus,
            LeaveRecommendationStatus,
            LeaveStatusType,
            EmployeeId,
            BiometricType,
            HRCompanyWithChildren,
            CompanyId,
            EmployeeAttendanceNP,
            EmployeeNP,
            RoleType,
            Year,
            CurrentYear,
            PreviousYear,
            Month,
            Day,
            VehicleType,
            HRCompanyOnly,
            HRCompanyShiftRoaster,
            HRCompanyHREmployeeShiftDate,
            ShiftTitle,
            MasterLeaveTitle,
            UnAssignedMasterLeaveTitle,
            HRParentChildCompany,
            Default
        }

        #region export to excel

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> ExportToExcel<T>(string baseFileName, IEnumerable<T> list)
        {
            var grid = new GridView();
            try
            {
                StringBuilder fileName = new StringBuilder();
                fileName.Append($"{baseFileName}-{DateTime.Now.ToShortDateString()}");
                grid.DataSource = ParseToDataTable(list);
                grid.DataBind();
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", $"attachment;fileName={fileName}.xls");
                //Response.ContentEncoding = System.Text.Encoding.UTF7;
                Response.ContentType = "application/vnd.ms-excel";
                Response.Charset = "";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                grid.RenderControl(htw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
                await Task.WhenAll();
                return PartialView("MyView");
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        protected DataTable ParseToDataTable<T>(IEnumerable<T> list)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in list)
            {
                for (int i = 0; i < values.Length; i++)
                    values[i] = props[i].GetValue(item) ?? DBNull.Value;
                table.Rows.Add(values);
            }
            return table;
        }
        #endregion

        #region Action Notification
        public async Task<JavaScriptResult> AlertNotification(string title, string message, AlertNotificationType type)
        {
            string script = string.Empty;
            script = "<script type='text/javascript'>$.notify({title:<strong>" + title + "</strong>" + ',' + "message:" + message + "},{type:" + type.ToString() + "})</script>";
            TempData["notification"] = script;
            await Task.WhenAll();
            return JavaScript(script);
        }

        public async Task<string> AlertNotifications(string title, string message, AlertNotificationType type)
        {
            string script = string.Empty;
            script = string.Format("<script language='javascript' type='text/javascript'>$.notify({title : <strong>{0}</strong>,message: {1}},{type:{2}});</script>", title, message, type.ToString());
            await Task.WhenAll();
            return script;
        }

        public UserSessionModel SessionDetail
        {
            get
            {
                return (UserSessionModel)HttpContext.Session["UserSession"];
            }
        }
        public Task<ifc_today_Result> Today
        {
            get
            {
                return _unitOfWork.Db.Database.SqlQuery<ifc_today_Result>("select * from ifc_today()").FirstOrDefaultAsync();
            }
        }
        public string FileRootPath
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["FileRootPath"];
            }
        }
        public string FileUrlPath
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["FileUrlPath"];
            }
        }
        public KeyValuePairModel<DateTime, DateTime> GetStartEndDate(int year, int month)
        {
            var day = _unitOfWork.Db.Set<HRCalendar>().Where(x => x.NepYear == year && x.NepMonth == month).OrderBy(x => x.NepDay).ToList();
            return new KeyValuePairModel<DateTime, DateTime> { Key = day.FirstOrDefault().EngDate, Value = day.LastOrDefault().EngDate };
        }
        public ifc_GetDateInfo_Result Date(DateTime date)
        {
            object[] myObjArray =
            {
                new SqlParameter() {ParameterName = "@p1", SqlDbType = SqlDbType.DateTime, Value= date}
            };
            return _unitOfWork.Db.Database.SqlQuery<ifc_GetDateInfo_Result>("select * from ifc_GetDateInfo(@p1)", myObjArray).FirstOrDefault();
        }

        public ifc_GetDateByNepaliDate_Result GetDateByDateNP(string date)
        {
            object[] myObjArray =
            {
                new SqlParameter() {ParameterName = "@p1", SqlDbType = SqlDbType.NVarChar, Value= date}
            };
            return _unitOfWork.Db.Database.SqlQuery<ifc_GetDateByNepaliDate_Result>("select * from ifc_GetDateByNepaliDate(@p1)", myObjArray).FirstOrDefault();
        }

        public virtual async Task<string> GetNpMonth(int id)
        {
            var model = await _unitOfWork.Db.Set<MonthNp>().FirstOrDefaultAsync(x => x.Id == id);
            return model.NepaliMonth;
        }
        public virtual async Task<string> GetNpMonth(string dateNP)
        {
            int id = int.Parse(dateNP.Split('-')[1]);
            var model = await _unitOfWork.Db.Set<MonthNp>().FirstOrDefaultAsync(x => x.Id == id);
            return model.NepaliMonth;
        }
        #endregion

        #region pair model

        public virtual async Task<ICollection<KeyValuePairModel<int, string>>> GetIntStringPairModel(PairModelTitle title, long? idHRCompany = 0)

        {
            try
            {
                var model = new List<KeyValuePairModel<int, string>>();
                switch (title)
                {
                    case PairModelTitle.KaajType:
                        return (from c in await _unitOfWork.Db.Set<KaajType>().ToListAsync()
                                where c.IsActive
                                select new KeyValuePairModel<int, string> { Key = c.Id, Value = c.TitleNP }).ToList();

                    case PairModelTitle.HREmployeePositionStatusType:
                        model= (from c in await _unitOfWork.Db.Set<HREmployeePositionStatusType>().ToListAsync()
                                where c.IsActive
                                select new KeyValuePairModel<int, string> { Key = c.Id, Value = c.Title }).ToList();
                        //model.Insert(0, new KeyValuePairModel<int, string> { Key = 0, Value = ConfigurationManager.AppSettings["Select"].ToString() });
                        return model;

                    case PairModelTitle.HREmployeePositionStatusReportType:
                        model = (from c in await _unitOfWork.Db.Set<HREmployeePositionStatusType>().ToListAsync()
                                 where c.IsActive
                                 select new KeyValuePairModel<int, string> { Key = c.Id, Value = c.Title }).ToList();
                        return model;
                    case PairModelTitle.UnAssignedMasterLeaveTitle:
                        object[] paramCompany =
                                            {
                                                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value= idHRCompany}
                                              };
                        return (from c in await _unitOfWork.Db.Database.SqlQuery<ifc_GetLeaveTypeForHRCompany_Result>("SELECT * FROM dbo.ifc_GetLeaveTypeForHRCompany(@paramIdHRCompany)", paramCompany).ToListAsync() select new KeyValuePairModel<int, string> { Key = c.Id, Value = c.LeaveTitleNP }).ToList();

                    case PairModelTitle.MasterLeaveTitle:
                        return (from c in await _unitOfWork.Db.Set<MasterLeaveTitle>().ToListAsync()
                                where c.IsActive
                                select new KeyValuePairModel<int, string> { Key = c.Id, Value = c.LeaveTitleNP }).ToList();

                    case PairModelTitle.SystemLanguage:
                        return (from c in await _unitOfWork.Db.Set<SystemLanguage>().ToListAsync() where c.IsActive select new KeyValuePairModel<int, string> { Key = c.Id, Value = c.LanguageTitle }).ToList();

                    case PairModelTitle.HRCompanyCode:
                        return (from c in await _unitOfWork.Db.Set<HRCompanyCode>().OrderByDescending(x=>x.Id).ToListAsync() select new KeyValuePairModel<int, string> { Key = c.Id, Value = c.HRCompanyCodeTitle }).ToList();

                    case PairModelTitle.Religion:
                        model = (from c in await _unitOfWork.Db.Set<HREmployeeReligion>().ToListAsync() where c.IsActive select new KeyValuePairModel<int, string> { Key = c.Id, Value = c.Religion }).ToList();
                        return model;

                    case PairModelTitle.Relation:
                        return (from c in await _unitOfWork.Db.Set<HREmployeeRelationType>().ToListAsync() select new KeyValuePairModel<int, string> { Key = c.Id, Value = c.RelationType }).ToList();

                    case PairModelTitle.BloodGroup:
                        model = (from c in await _unitOfWork.Db.Set<HREmployeeBloodGroup>().ToListAsync() select new KeyValuePairModel<int, string> { Key = c.Id, Value = c.HREmployeeBloodGroupTitle }).ToList();
                        model.Insert(0, new KeyValuePairModel<int, string> { Key = 0, Value = ConfigurationManager.AppSettings["Select"].ToString() });
                        return model;

                    case PairModelTitle.SystemMenu:
                        return (from c in await _unitOfWork.Db.Set<SystemMenu>().ToListAsync() select new KeyValuePairModel<int, string> { Key = c.Id, Value = c.Area }).ToList();

                    case PairModelTitle.SystemStructure:
                        var systemstruct = (from c in await _unitOfWork.Db.Set<SystemStructure>().ToListAsync() select new KeyValuePairModel<int, string> { Key = c.Id, Value = $"{c.Header}[{c.SystemMenu.Area}]" }).ToList();
                        systemstruct.Insert(0, new KeyValuePairModel<int, string> { Key = 0, Value = "None" });
                        return systemstruct;

                    case PairModelTitle.SystemStructureBySystemMenu:
                        var systemstrMenu = (from c in await _unitOfWork.Db.Set<SystemStructure>().ToListAsync() where c.IdSystemMenu == idHRCompany select new KeyValuePairModel<int, string> { Key = c.Id, Value = $"{c.Header}[{c.SystemMenu.Area}]" }).ToList();
                        systemstrMenu.Insert(0, new KeyValuePairModel<int, string> { Key = 0, Value = "None" });
                        return systemstrMenu;

                    case PairModelTitle.HRCompanyType:
                        return (from c in await _unitOfWork.Db.Set<HRCompanyType>().OrderBy(x => x.OrderBy).ToListAsync() select new KeyValuePairModel<int, string> { Key = c.Id, Value = c.CompanyType }).ToList();

                    case PairModelTitle.CityCategory:
                        return (from c in await _unitOfWork.Db.Set<CityCategory>().ToListAsync() select new KeyValuePairModel<int, string> { Key = c.Id, Value = c.CityCategoryName }).ToList();

                    case PairModelTitle.Country:
                        return (from c in await _unitOfWork.Db.Set<Country>().ToListAsync() select new KeyValuePairModel<int, string> { Key = c.Id, Value = c.CountryNp }).ToList();

                    case PairModelTitle.State:
                        return (from c in await _unitOfWork.Db.Set<CountryState>().ToListAsync() select new KeyValuePairModel<int, string> { Key = c.Id, Value = c.CountryStateName }).ToList();


                    case PairModelTitle.JobStatus:
                        model= (from c in await _unitOfWork.Db.Set<HREmployeeJobStatu>().ToListAsync() select new KeyValuePairModel<int, string> { Key = c.Id, Value = c.HREmployeeJobStatusTitle }).ToList();
                        model.Insert(0, new KeyValuePairModel<int, string> { Key = 0, Value = ConfigurationManager.AppSettings["Select"].ToString() });
                        return model;

                    case PairModelTitle.Role:
                        switch (this.SessionDetail.IdRoleType)
                        {
                            case (int)RoleType.SuperUser:
                                model = (from c in await _unitOfWork.Db.Set<HREmployeeRole>().Where(x=>x.IdRoleType!=0).ToListAsync() where c.IdHRCompany == idHRCompany select new KeyValuePairModel<int, string> { Key = c.Id, Value = c.HRRoleTitle }).ToList();
                                model.Insert(0, new KeyValuePairModel<int, string> { Key = 0, Value = ConfigurationManager.AppSettings["Select"].ToString() });
                                return model;
                            case (int)RoleType.Admin:
                                return (from c in await _unitOfWork.Db.Set<HREmployeeRole>().ToListAsync() where c.IdHRCompany == SessionDetail.IdHRCompany select new KeyValuePairModel<int, string> { Key = c.Id, Value = c.HRRoleTitle }).ToList();
                            case (int)RoleType.NormalUser:
                                return (from c in await _unitOfWork.Db.Set<HREmployeeRole>().ToListAsync() where c.IdHRCompany == SessionDetail.IdHRCompany && c.IdRoleType == SessionDetail.IdRoleType select new KeyValuePairModel<int, string> { Key = c.Id, Value = c.HRRoleTitle }).ToList();
                            case (int)RoleType.SectionAdmin:
                                return (from c in await _unitOfWork.Db.Set<HREmployeeRole>().Where(x => x.IdRoleType == 3 || x.IdRoleType == 4).ToListAsync() where c.IdHRCompany == SessionDetail.IdHRCompany select new KeyValuePairModel<int, string> { Key = c.Id, Value = c.HRRoleTitle }).ToList();

                            default:
                                return (from c in await _unitOfWork.Db.Set<HREmployeeRole>().ToListAsync() where c.IdHRCompany == this.SessionDetail.IdHRCompany select new KeyValuePairModel<int, string> { Key = c.Id, Value = c.HRRoleTitle }).ToList();
                        }

                    case PairModelTitle.NationalIdentityType:
                        model = (from c in await _unitOfWork.Db.Set<HREmployeeNationalIdentityType>().ToListAsync() select new KeyValuePairModel<int, string> { Key = c.Id, Value = c.NationalIdentityType }).ToList();
                        model.Insert(0, new KeyValuePairModel<int, string> { Key = 0, Value = ConfigurationManager.AppSettings["Select"].ToString() });
                        return model;

                    case PairModelTitle.DeviceModel:
                        return (from c in await _unitOfWork.Db.Set<HRDeviceModel>().ToListAsync() select new KeyValuePairModel<int, string> { Key = c.Id, Value = c.HRDeviceModel1 }).ToList();

                    case PairModelTitle.SystemDetailCategory:
                        return (from c in await _unitOfWork.Db.Set<SystemDetailCategory>().ToListAsync() select new KeyValuePairModel<int, string> { Key = c.Id, Value = c.CategoryTitle }).ToList();

                    case PairModelTitle.EducationType:
                        return (from c in await _unitOfWork.Db.Set<HREmployeeEducationType>().ToListAsync() select new KeyValuePairModel<int, string> { Key = c.Id, Value = c.EducationType }).ToList();

                    case PairModelTitle.BiometricType:
                        return (from c in await _unitOfWork.Db.Set<BiometricTemplateType>().ToListAsync() select new KeyValuePairModel<int, string> { Key = c.Id, Value = c.BiometricTemplateTypeTitle }).ToList();

                    case PairModelTitle.Year:
                        return (from c in await _unitOfWork.Db.Set<HRCalendar>().GroupBy(x => x.NepYear).ToListAsync() select new KeyValuePairModel<int, string> { Key = c.Key, Value = c.Key.ToString() }).ToList();

                    case PairModelTitle.CurrentYear:
                        string nepDate = Today.Result.Nepdate;
                        return (from c in await _unitOfWork.Db.Set<HRCalendar>().Where(x => x.NepDate == nepDate).Select(x => x.NepYear).ToListAsync() select new KeyValuePairModel<int, string> { Key = c, Value = c.ToString() }).ToList();
                    case PairModelTitle.PreviousYear:
                        DateTime currentDate = DateTime.Now.Date;
                        return (from c in await _unitOfWork.Db.Set<HRCalendar>().Where(x => x.EngDate == currentDate).Select(x => x.NepYear - 1).ToListAsync() select new KeyValuePairModel<int, string> { Key = c, Value = c.ToString() }).ToList();
                    case PairModelTitle.Month:
                        return (from c in await _unitOfWork.Db.Set<MonthNp>().ToListAsync() select new KeyValuePairModel<int, string> { Key = c.Id, Value = c.NepaliMonth }).ToList();

                    case PairModelTitle.Day:
                        var dateModel = this.Today.Result;
                        return (from c in await _unitOfWork.Db.Set<HRCalendar>().Where(x => x.NepYear == dateModel.NepYear && x.NepMonth == dateModel.NepMonth).ToListAsync() select new KeyValuePairModel<int, string> { Key = c.NepDay, Value = c.NepDay.ToString() }).ToList();

                    case PairModelTitle.RoleType:
                        switch (this.SessionDetail.IdRoleType)
                        {
                            case (int)RoleType.SuperUser:
                                model = Enum.GetValues(typeof(RoleType)).Cast<RoleType>().Where(x => (int)x != 0).Select(x => new KeyValuePairModel<int, string> { Key = (int)x, Value = x.ToString() }).ToList();
                                return model;
                            default:
                                model = Enum.GetValues(typeof(RoleType)).Cast<RoleType>().Where(x => (int)x != 0 ).Select(x => new KeyValuePairModel<int, string> { Key = (int)x, Value = x.ToString() }).ToList();
                                //adding 4(ReportAdmin) admin cannot add ReportAdmin Role


                                return model;
                        }
                    case PairModelTitle.LeaveApprovalStatus:
                        return (from c in await _unitOfWork.Db.Set<HRCompanyLeaveStatusType>().ToListAsync() where c.IsActive && c.Id != 1 select new KeyValuePairModel<int, string> { Key = c.Id, Value = c.LeaveStatusTypeNP }).ToList();

                    case PairModelTitle.LeaveRecommendationStatus:
                        return (from c in await _unitOfWork.Db.Set<HRCompanyLeaveStatusType>().ToListAsync() where c.IsActive && c.Id != 3 select new KeyValuePairModel<int, string> { Key = c.Id, Value = c.LeaveStatusTypeNP }).ToList();

                    case PairModelTitle.LeaveStatusType:
                        return (from c in await _unitOfWork.Db.Set<HRCompanyLeaveStatusType>().ToListAsync() where c.IsActive select new KeyValuePairModel<int, string> { Key = c.Id, Value = c.LeaveStatusTypeNP }).ToList();

                    default:
                        return null;
                }
            }
            catch (Exception exp)
            {
                throw new ArgumentException(exp.Message);
            }
        }

        public virtual async Task<ICollection<KeyValuePairModel<long, string>>> GetLongStringPairModel(PairModelTitle title, long? idHRCompany = 0, long? idHREmployee = 0)
        {
            try
            {
                var model = new List<KeyValuePairModel<long, string>>();
                switch (title)
                {
                    case PairModelTitle.HRCompanyHREmployeeShiftDate:
                        return (from c in await _unitOfWork.Db.Set<HRCompanyHREmployeeShiftDate>().ToListAsync() where c.IdHREmployee == idHREmployee select new KeyValuePairModel<long, string> { Key = c.Id, Value = $"{c.EffectiveFromDate.DateNp()} - {c.EffectiveToDate.DateNp()}" }).ToList();

                    case PairModelTitle.HRCompanyShiftRoaster:
                        return (from c in await _unitOfWork.Db.Set<HRCompanyShiftRoaster>().ToListAsync() where c.IdHRCompany == idHRCompany && c.IsActive select new KeyValuePairModel<long, string> { Key = c.Id, Value = $"{c.ShiftTitle} [{c.LoginTime}-{c.LogoutTime}]" }).ToList();

                    case PairModelTitle.DesignationLeaveApproval:
                        object[] paramCompany =
                                            {
                                                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value= idHRCompany}
                                              };
                        model = (from c in await _unitOfWork.Db.Database.SqlQuery<ifc_GetDesignationForLeaveApproval_Result>("SELECT * FROM dbo.ifc_GetDesignationForLeaveApproval(@paramIdHRCompany)", paramCompany).ToListAsync() select new KeyValuePairModel<long, string> { Key = c.IdHRDesignation, Value = $"{c.Designation}" }).ToList();
                        model.Insert(0, new KeyValuePairModel<long, string> { Key = 0, Value = ConfigurationManager.AppSettings["HRCompany"].ToString() });
                        return model;

                    case PairModelTitle.Gender:
                        model = (from c in await _unitOfWork.Db.Set<HREmployeeSex>().ToListAsync() select new KeyValuePairModel<long, string> { Key = c.Id, Value = c.HREmployeeSexTitleNP }).ToList();
                        model.Insert(0, new KeyValuePairModel<long, string> { Key = 0, Value = ConfigurationManager.AppSettings["Select"].ToString() });
                        return model;

                    case PairModelTitle.HRLanguage:
                        return (from c in await _unitOfWork.Db.Set<HRLanguage>().ToListAsync() select new KeyValuePairModel<long, string> { Key = c.Id, Value = c.HRLanguageTitle }).ToList();

                    case PairModelTitle.MaritalStatus:
                        model = (from c in await _unitOfWork.Db.Set<HREmployeeMarital>().ToListAsync() select new KeyValuePairModel<long, string> { Key = c.Id, Value = c.HREmployeeMaritalTitle }).ToList();
                        model.Insert(0, new KeyValuePairModel<long, string> { Key = 0, Value = ConfigurationManager.AppSettings["Select"].ToString() });
                        return model;

                    case PairModelTitle.HRCompany:
                        switch (this.SessionDetail.IdRoleType)
                        {
                            case (int)RoleType.SuperUser:
                                model = (from c in await _unitOfWork.Db.Set<HRCompany>().OrderBy(x => x.HRCompanyType.OrderBy).ToListAsync()
                                         select new KeyValuePairModel<long, string> { Key = c.Id, Value = $"{c.CompanyNameNP} [{c.HRCompanyType.CompanyType}]" }).ToList();
                                model.Insert(0, new KeyValuePairModel<long, string> { Key = 0, Value = ConfigurationManager.AppSettings["Select"].ToString() });
                                return model;
                            case (int)RoleType.Admin:
                                return (from c in await _unitOfWork.Db.Set<HRCompany>().OrderBy(x => x.HRCompanyType.OrderBy).ToListAsync() where c.Id == SessionDetail.IdHRCompany || c.IdParent_HRCompany == SessionDetail.IdHRCompany select new KeyValuePairModel<long, string> { Key = c.Id, Value = $"{c.CompanyNameNP} [{c.HRCompanyType.CompanyType}]" }).ToList();
                            case (int)RoleType.RootUser:
                                return (from c in await _unitOfWork.Db.Set<HRCompany>().OrderBy(x => x.HRCompanyType.OrderBy).ToListAsync() where c.Id == this.SessionDetail.IdHRCompany select new KeyValuePairModel<long, string> { Key = c.Id, Value = $"{c.CompanyNameNP}  [{c.HRCompanyType.CompanyType}]" }).ToList();
                            case (int)RoleType.SectionAdmin:
                                return (from c in await _unitOfWork.Db.Set<HRCompany>().OrderBy(x => x.HRCompanyType.OrderBy).ToListAsync() where c.Id == this.SessionDetail.IdHRCompany select new KeyValuePairModel<long, string> { Key = c.Id, Value = $"{c.CompanyNameNP}  [{c.HRCompanyCode.HRCompanyCodeTitle}]" }).ToList();
                            
                            default: return (from c in await _unitOfWork.Db.Set<HRCompany>().OrderBy(x => x.HRCompanyType.OrderBy).ToListAsync() where c.Id == this.SessionDetail.IdHRCompany select new KeyValuePairModel<long, string> { Key = c.Id, Value = $"{c.CompanyNameNP}  [{c.HRCompanyType.CompanyType}]" }).ToList();
                        }


                    case PairModelTitle.HRParentChildCompany:
                        switch (this.SessionDetail.IdRoleType)
                        {
                            case (int)RoleType.SuperUser:
                                model = (from c in await _unitOfWork.Db.Set<HRCompany>().OrderBy(x => x.HRCompanyType.OrderBy).ToListAsync()
                                         select new KeyValuePairModel<long, string> { Key = c.Id, Value = $"{c.CompanyNameNP} [{c.HRCompanyType.CompanyType}]" }).ToList();
                                model.Insert(0, new KeyValuePairModel<long, string> { Key = 0, Value = ConfigurationManager.AppSettings["Select"].ToString() });
                                return model;
                            case (int)RoleType.Admin:
                                return (from c in await _unitOfWork.Db.Set<HRCompany>().OrderBy(x => x.HRCompanyType.OrderBy).ToListAsync()
                                        select new KeyValuePairModel<long, string> { Key = c.Id, Value = $"{c.CompanyNameNP} [{c.HRCompanyType.CompanyType}]" }).ToList();
                            case (int)RoleType.RootUser:
                                return (from c in await _unitOfWork.Db.Set<HRCompany>().OrderBy(x => x.HRCompanyType.OrderBy).ToListAsync()
                                        select new KeyValuePairModel<long, string> { Key = c.Id, Value = $"{c.CompanyNameNP} [{c.HRCompanyType.CompanyType}]" }).ToList();

                            default: return (from c in await _unitOfWork.Db.Set<HRCompany>().OrderBy(x => x.HRCompanyType.OrderBy).ToListAsync()
                                        select new KeyValuePairModel<long, string> { Key = c.Id, Value = $"{c.CompanyNameNP} [{c.HRCompanyType.CompanyType}]" }).ToList();
                        }





                    case PairModelTitle.HRCompanyOnly:
                        switch (this.SessionDetail.IdRoleType)
                        {
                            case (int)RoleType.SuperUser:
                                model = (from c in await _unitOfWork.Db.Set<HRCompany>().OrderBy(x => x.HRCompanyType.OrderBy).ToListAsync()
                                         select new KeyValuePairModel<long, string> { Key = c.Id, Value = $"{c.CompanyNameNP} [{c.HRCompanyType.CompanyType}]" }).ToList();
                                model.Insert(0, new KeyValuePairModel<long, string> { Key = 0, Value = ConfigurationManager.AppSettings["Select"].ToString() });
                                return model;
                            case (int)RoleType.SectionAdmin:
                                return (from c in await _unitOfWork.Db.Set<HRCompany>().OrderBy(x => x.HRCompanyType.OrderBy).ToListAsync() where c.Id == this.SessionDetail.IdHRCompany select new KeyValuePairModel<long, string> { Key = c.Id, Value = $"{c.CompanyNameNP}  [{c.HRCompanyType.CompanyType}]" }).ToList();
                            
                            default: return (from c in await _unitOfWork.Db.Set<HRCompany>().ToListAsync() where c.Id == this.SessionDetail.IdHRCompany select new KeyValuePairModel<long, string> { Key = c.Id, Value = $"{c.CompanyNameNP}  [{c.HRCompanyType.CompanyType}]" }).ToList();
                        }

                    case PairModelTitle.HRCompanyWithCode:
                        return (from c in await _unitOfWork.Db.Set<HRCompany>().OrderBy(x => x.HRCompanyType.OrderBy).ToListAsync() select new KeyValuePairModel<long, string> { Key = c.Id, Value = $"{c.HRCompanyCode.HRCompanyCodeTitle}[{c.HRCompanyType.CompanyType}]" }).ToList();

                    case PairModelTitle.HRCompanyWithChildren:
                        switch (SessionDetail.IdRoleType)
                        {
                            case (int)RoleType.SuperUser:
                                return (from c in await _unitOfWork.Db.Set<HRCompany>().OrderBy(x => x.HRCompanyType.OrderBy).ToListAsync() select new KeyValuePairModel<long, string> { Key = c.Id, Value = $"{c.CompanyNameNP}[{c.HRCompanyCode.HRCompanyCodeTitle}][{c.HRCompanyType.CompanyType}]" }).ToList();
                            case (int)RoleType.Admin:
                                return (from c in await _unitOfWork.Db.Set<HRCompany>().OrderBy(x => x.HRCompanyType.OrderBy).ToListAsync() where c.Id == SessionDetail.IdHRCompany || c.IdParent_HRCompany == SessionDetail.IdHRCompany select new KeyValuePairModel<long, string> { Key = c.Id, Value = $"{c.HRCompanyCode.HRCompanyCodeTitle}[{c.HRCompanyType.CompanyType}]" }).ToList();
                            case (int)RoleType.RootUser:
                                return (from c in await _unitOfWork.Db.Set<HRCompany>().OrderBy(x => x.HRCompanyType.OrderBy).ToListAsync() where c.Id == this.SessionDetail.IdHRCompany select new KeyValuePairModel<long, string> { Key = c.Id, Value = $"{c.CompanyNameNP}[{c.HRCompanyCode.HRCompanyCodeTitle}][{c.HRCompanyType.CompanyType}]" }).ToList();
                            default: return (from c in await _unitOfWork.Db.Set<HRCompany>().OrderBy(x => x.HRCompanyType.OrderBy).ToListAsync() where c.Id == this.SessionDetail.IdHRCompany select new KeyValuePairModel<long, string> { Key = c.Id, Value = $"{c.CompanyNameNP}[{c.HRCompanyCode.HRCompanyCodeTitle}][{c.HRCompanyType.CompanyType}]" }).ToList();
                        }

                    case PairModelTitle.City:
                        return (from c in await _unitOfWork.Db.Set<City>().ToListAsync() select new KeyValuePairModel<long, string> { Key = c.Id, Value = c.CityName }).ToList();

                    case PairModelTitle.District:
                        model = (from c in await _unitOfWork.Db.Set<CountryDistrict>().ToListAsync() select new KeyValuePairModel<long, string> { Key = c.Id, Value = c.CountryDistrictNameNP }).ToList();
                        model.Insert(0, new KeyValuePairModel<long, string> { Key = 0, Value = ConfigurationManager.AppSettings["Select"].ToString() });
                        return model;

                    case PairModelTitle.DivisionType:
                        return (from c in await _unitOfWork.Db.Set<HRCompanyDivisionType>().ToListAsync() where c.IdHRCompany == idHRCompany select new KeyValuePairModel<long, string> { Key = c.Id, Value = c.HRCompanyDivisionTypeTitle }).ToList();

                    case PairModelTitle.Division:
                        switch (SessionDetail.IdRoleType)
                        {
                            case 0:
                                model = (from c in await _unitOfWork.Db.Set<HRCompanyDivision>().ToListAsync() where c.IdHRCompany == idHRCompany select new KeyValuePairModel<long, string> { Key = c.Id, Value = c.HRCompanyDivisionName }).ToList();
                                model.Insert(0, new KeyValuePairModel<long, string> { Key = 0, Value = ConfigurationManager.AppSettings["All"].ToString() });
                                return model;
                            case 1:
                                model = (from c in await _unitOfWork.Db.Set<HRCompanyDivision>().ToListAsync() where c.IdHRCompany == idHRCompany select new KeyValuePairModel<long, string> { Key = c.Id, Value = c.HRCompanyDivisionName }).ToList();
                                model.Insert(0, new KeyValuePairModel<long, string> { Key = 0, Value = ConfigurationManager.AppSettings["All"].ToString() });
                                return model;
                            case 2:
                                model.Insert(0, new KeyValuePairModel<long, string> { Key = 0, Value = ConfigurationManager.AppSettings["All"].ToString() });
                                return model;
                            case 4:
                                model = (from c in await _unitOfWork.Db.Set<HRCompanyDivision>().ToListAsync() where c.IdHRCompany == idHRCompany && c.Id==SessionDetail.IdHRCompanyDivision select new KeyValuePairModel<long, string> { Key = c.Id, Value = c.HRCompanyDivisionName }).ToList();
                                return model;
                            default:
                                model = (from c in await _unitOfWork.Db.Set<HRCompanyDivision>().ToListAsync() where c.IdHRCompany == idHRCompany && c.Id == this.SessionDetail.IdHRCompanyDivision select new KeyValuePairModel<long, string> { Key = c.Id, Value = $"{c.HRCompanyDivisionName}[{c.HRCompanyDivisionType.HRCompanyDivisionTypeTitle}] " }).ToList();
                                return model;
                        }

                    case PairModelTitle.EmployeeById:
                        return (from c in await _unitOfWork.Db.Set<HREmployee>().ToListAsync() where c.Id == idHREmployee || idHREmployee == 0 select new KeyValuePairModel<long, string> { Key = c.Id, Value = $"{c.HREmployeeNameNP} [{c.IdEnroll}]" }).ToList();

                    case PairModelTitle.Employee:
                        switch (SessionDetail.IdRoleType)
                        {
                            case 0:
                                model = (from c in await _unitOfWork.Db.Set<HREmployee>().ToListAsync() where c.IdHRCompany == idHRCompany || idHRCompany == 0 select new KeyValuePairModel<long, string> { Key = c.Id, Value = $"{c.HREmployeeNameNP} [{c.IdEnroll}]" }).ToList();
                                model.Insert(0, new KeyValuePairModel<long, string> { Key = 0, Value = ConfigurationManager.AppSettings["All"].ToString() });
                                return model;
                            case 1:
                                model = (from c in await _unitOfWork.Db.Set<HREmployee>().ToListAsync() where c.IdHRCompany == idHRCompany && (c.IdHRCompany == SessionDetail.IdHRCompany || c.HRCompany.IdParent_HRCompany == SessionDetail.IdHRCompany) select new KeyValuePairModel<long, string> { Key = c.Id, Value = $"{c.HREmployeeName} [{c.IdEnroll}]" }).ToList();
                                model.Insert(0, new KeyValuePairModel<long, string> { Key = 0, Value = ConfigurationManager.AppSettings["All"].ToString() });
                                return model;
                            case 2:
                                model = (from c in await _unitOfWork.Db.Set<HREmployee>().ToListAsync() where c.IdHRCompany == SessionDetail.IdHRCompany select new KeyValuePairModel<long, string> { Key = c.Id, Value = $"{c.HREmployeeNameNP} [{c.IdEnroll}]" }).ToList();
                                model.Insert(0, new KeyValuePairModel<long, string> { Key = 0, Value = ConfigurationManager.AppSettings["All"].ToString() });
                                return model;
                            default:
                                model = (from c in await _unitOfWork.Db.Set<HREmployee>().ToListAsync() where c.IdHRCompany == idHRCompany && c.Id == SessionDetail.IdHREmployee select new KeyValuePairModel<long, string> { Key = c.Id, Value = $"{c.HREmployeeNameNP} [{c.IdEnroll}]" }).ToList();
                                return model;
                        }
                    case PairModelTitle.EmployeeNP:
                        switch (SessionDetail.IdRoleType)
                        {
                            case 0:
                                model = (from c in await _unitOfWork.Db.Set<HREmployee>().ToListAsync() where c.IdHRCompany == idHRCompany || idHRCompany == 0 select new KeyValuePairModel<long, string> { Key = c.Id, Value = $"{c.HREmployeeNameNP} [{c.IdEnroll}]" }).ToList();
                                return model;
                            case 1:
                                model = (from c in await _unitOfWork.Db.Set<HREmployee>().ToListAsync() where c.IdHRCompany == idHRCompany && (c.IdHRCompany == SessionDetail.IdHRCompany || c.HRCompany.IdParent_HRCompany == SessionDetail.IdHRCompany) select new KeyValuePairModel<long, string> { Key = c.Id, Value = $"{c.HREmployeeNameNP} [{c.IdEnroll}]" }).ToList();
                                return model;
                            case 2:
                                model = (from c in await _unitOfWork.Db.Set<HREmployee>().ToListAsync() where c.IdHRCompany == SessionDetail.IdHRCompany select new KeyValuePairModel<long, string> { Key = c.Id, Value = $"{c.HREmployeeNameNP} [{c.IdEnroll}]" }).ToList();
                                return model;
                            default:
                                model = (from c in await _unitOfWork.Db.Set<HREmployee>().ToListAsync() where c.IdHRCompany == idHRCompany && c.Id == SessionDetail.IdHREmployee select new KeyValuePairModel<long, string> { Key = c.Id, Value = $"{c.HREmployeeNameNP} [{c.IdEnroll}]" }).ToList();
                                return model;
                        }
                    case PairModelTitle.EmployeeAttendanceNP:
                        model = (from c in await _unitOfWork.Db.Set<HREmployee>().ToListAsync() where c.Id == idHREmployee select new KeyValuePairModel<long, string> { Key = c.Id, Value = $"{c.HREmployeeNameNP} [{c.IdEnroll}]" }).ToList();
                        return model;

                    case PairModelTitle.Designation:
                         model= (from c in await _unitOfWork.Db.Set<HRDesignation>().OrderBy(x => x.HRDesignationOrder).ToListAsync() where c.IdHRCompany == idHRCompany select new KeyValuePairModel<long, string> { Key = c.Id, Value = $"{c.HRDesignationTitle} [{c.HREmployeeCategory.HREmployeeCategoryTitle}]" }).ToList();
                        model.Insert(0, new KeyValuePairModel<long, string> { Key = 0, Value = ConfigurationManager.AppSettings["Select"].ToString() });
                        return model;

                    case PairModelTitle.EmployeeCategory:
                        model = (from c in await _unitOfWork.Db.Set<HREmployeeCategory>().OrderBy(x => x.HREmployeeCategoryOrder).ToListAsync() where c.IdHRCompany == idHRCompany select new KeyValuePairModel<long, string> { Key = c.Id, Value = c.HREmployeeCategoryTitle }).ToList();
                        model.Insert(0, new KeyValuePairModel<long, string> { Key = 0, Value = ConfigurationManager.AppSettings["All"].ToString() });
                        return model;

                    case PairModelTitle.ServiceType:
                        model= (from c in await _unitOfWork.Db.Set<HREmployeeServiceType>().ToListAsync() where c.IdHRCompany == idHRCompany select new KeyValuePairModel<long, string> { Key = c.Id, Value = c.HREmployeeServiceTypeTitle }).ToList();
                        model.Insert(0, new KeyValuePairModel<long, string> { Key = 0, Value = ConfigurationManager.AppSettings["Select"].ToString() });
                        return model;

                    case PairModelTitle.Role:
                        return (from c in await _unitOfWork.Db.Set<HREmployeeRole>().ToListAsync() where c.IdHRCompany == idHRCompany select new KeyValuePairModel<long, string> { Key = c.Id, Value = c.HRRoleTitle }).ToList();

                    case PairModelTitle.SavingLeaveType:
                        return (from c in await _unitOfWork.Db.Set<HRCompanyLeaveType>().ToListAsync() where c.IdHRCompany == idHRCompany && c.IsSaving select new KeyValuePairModel<long, string> { Key = c.IdMasterLeaveTitle, Value = c.MasterLeaveTitle.LeaveTitleNP }).ToList();

                    case PairModelTitle.LeaveType:
                        object[] paramLeave =
                                              {
                                                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value= idHRCompany},
                                                new SqlParameter() {ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value = idHREmployee}
                                              };
                        model = (from c in await _unitOfWork.Db.Database.SqlQuery<proc_GetLeaveByEmployee_Result>("EXEC proc_GetLeaveByEmployee @paramIdHRCompany,@paramIdHREmployee", paramLeave).ToListAsync() select new KeyValuePairModel<long, string> { Key = c.IdLeaveType, Value = c.LeaveTitle }).ToList();
                        model.Insert(0, new KeyValuePairModel<long, string> { Key = 0, Value = ConfigurationManager.AppSettings["Select"].ToString() });
                        return model;

                    case PairModelTitle.ContactType:
                        return (from c in await _unitOfWork.Db.Set<HREmployeeContactType>().ToListAsync() select new KeyValuePairModel<long, string> { Key = c.Id, Value = c.ContactTypeTitle }).ToList();

                    case PairModelTitle.EmployeeId:
                        model = (from c in await _unitOfWork.Db.Set<HREmployee>().ToListAsync() where c.IdHRCompany == idHRCompany || idHRCompany == 0 select new KeyValuePairModel<long, string> { Key = c.Id, Value = $"{c.HREmployeeNameNP} [{c.IdEnroll}]" }).ToList();
                        model.Insert(0, new KeyValuePairModel<long, string> { Key = 0, Value = ConfigurationManager.AppSettings["All"].ToString() });
                        return model;

                    case PairModelTitle.CompanyId:
                        return (from c in await _unitOfWork.Db.Set<HRCompany>().OrderBy(x => x.HRCompanyType.OrderBy).ToListAsync() where c.Id == idHRCompany || idHRCompany == 0 select new KeyValuePairModel<long, string> { Key = c.Id, Value = $"{c.CompanyNameNP} [{c.HRCompanyType.CompanyType}]" }).ToList();

                    case PairModelTitle.EmployeeRecommended:
                        object[] param =
                                             {
                                                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value= idHRCompany},
                                                new SqlParameter() {ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value = idHREmployee}
                                              };
                        model = (from c in await _unitOfWork.Db.Database.SqlQuery<proc_GetRecommendedPerson_Result>("EXEC proc_GetRecommendedPerson @paramIdHRCompany,@paramIdHREmployee", param).ToListAsync() select new KeyValuePairModel<long, string> { Key = c.IdHREmployee, Value = $"{c.HREmployeeNameNP} [{c.HRDesignationTitle}]  ({c.CompanyShortName})" }).ToList();
                        return model;

                    //case PairModelTitle.EmployeeMannualAttendanceApproved:
                    //    object[] param =
                    //                         {
                    //                            new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value= idHRCompany},
                    //                          };
                    //    model = (from c in await _unitOfWork.Db.Database.SqlQuery<proc_GetRecommendedPerson_Result>("EXEC proc_GetRecommendedPerson @paramIdHRCompany, param).ToListAsync() select new KeyValuePairModel<long, string> { Key = c.IdHREmployee, Value = $"{c.HREmployeeNameNP} [{c.HRDesignationTitle}]  ({c.CompanyShortName})" }).ToList();
                    //    return model;

                    case PairModelTitle.EmployeeApproved:
                        object[] paramApproved =
                                             {
                                                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value= idHRCompany},
                                                new SqlParameter() {ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value = idHREmployee}
                                              };
                        model = (from c in await _unitOfWork.Db.Database.SqlQuery<proc_GetApprovalPerson_Result>("EXEC proc_GetApprovalPerson @paramIdHRCompany,@paramIdHREmployee", paramApproved).ToListAsync() select new KeyValuePairModel<long, string> { Key = c.IdHREmployee, Value = $"{c.HREmployeeNameNP} [{c.HRDesignationTitle}]  ({c.CompanyShortName})" }).ToList();
                        return model;

                    case PairModelTitle.EmployeeActive:
                        object[] paramActive =
                                             {
                                                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value= idHRCompany}
                                              };
                        model = (from c in await _unitOfWork.Db.Database.SqlQuery<proc_GetActivePerson_Result>("EXEC proc_GetActivePerson @paramIdHRCompany", paramActive).ToListAsync() select new KeyValuePairModel<long, string> { Key = c.IdHREmployee, Value = $"{c.HREmployeeNameNP} [{c.HRDesignationTitle}]  ({c.CompanyShortName})" }).ToList();
                        return model;

                    default:
                        return null;
                }
            }
            catch (Exception exp)
            {
                throw new ArgumentException(exp.Message);
            }
        }

        public virtual async Task<ICollection<KeyValuePairModel<string, string>>> GetStringStringPairModel(PairModelTitle title, long? idHRCompany = 0)
        {
            try
            {
                List<KeyValuePairModel<string, string>> _pairModel = new List<KeyValuePairModel<string, string>>();
                switch (title)
                {
                    case PairModelTitle.HRCompanyShiftRoaster:
                        return (from c in await _unitOfWork.Db.Set<HRCompanyShiftRoaster>().ToListAsync() where c.IdHRCompany == idHRCompany && c.IsActive select new KeyValuePairModel<string, string> { Key = $"{c.Id}", Value = $"{c.ShiftTitle} [{c.LoginTime}-{c.LogoutTime}]" }).ToList();

                    case PairModelTitle.HRLanguage:
                        return (from c in await _unitOfWork.Db.Set<HRLanguage>().ToListAsync() select new KeyValuePairModel<string, string> { Key = c.HRLanguageTitle, Value = c.HRLanguageTitle }).ToList();

                    case PairModelTitle.VehicleType:
                        _pairModel.Add(new KeyValuePairModel<string, string> { Key = "मोटरसाइकल", Value = "मोटरसाइकल" });
                        _pairModel.Add(new KeyValuePairModel<string, string> { Key = "ट्याक्सी", Value = "ट्याक्सी" });
                        _pairModel.Add(new KeyValuePairModel<string, string> { Key = "हावाइजहाज", Value = "हावाइजहाज" });
                        _pairModel.Add(new KeyValuePairModel<string, string> { Key = "बस", Value = "बस" });
                        _pairModel.Add(new KeyValuePairModel<string, string> { Key = "सरकारी सवारी साधन", Value = "सरकारी सवारी साधन" });
                        _pairModel.Add(new KeyValuePairModel<string, string> { Key = "कार", Value = "कार" });
                        _pairModel.Add(new KeyValuePairModel<string, string> { Key = "जीप", Value = "जीप" });
                        _pairModel.Add(new KeyValuePairModel<string, string> { Key = "टेम्पो", Value = "टेम्पो" });
                        _pairModel.Add(new KeyValuePairModel<string, string> { Key = "पैदल", Value = "पैदल" });
                        _pairModel.Add(new KeyValuePairModel<string, string> { Key = "भर्चुअल तालीम", Value = "भर्चुअल तालीम" });
                        return _pairModel;
                    default:
                        return null;
                }
            }
            catch (Exception exp)
            {
                throw new ArgumentException(exp.Message);
            }
        }

        public virtual async Task<ICollection<KeyValuePairModel<long, string>>> GetLongStringPairModelReport(PairModelTitle title, long? idHRCompany = 0, long? idHREmployee = 0)
        {
            try
            {
                var model = new List<KeyValuePairModel<long, string>>();
                switch (title)
                {
                    case PairModelTitle.HRCompany:
                        switch (SessionDetail.IdRoleType)
                        {
                            case (int)RoleType.SuperUser:
                                return (from c in await _unitOfWork.Db.Set<HRCompany>().OrderBy(x => x.HRCompanyType.OrderBy).ToListAsync() select new KeyValuePairModel<long, string> { Key = c.Id, Value = $"{c.CompanyNameNP}  [{c.HRCompanyCode.HRCompanyCodeTitle}]" }).ToList();
                            case (int)RoleType.Admin:
                                return (from c in await _unitOfWork.Db.Set<HRCompany>().OrderBy(x => x.HRCompanyType.OrderBy).ToListAsync() where c.Id == SessionDetail.IdHRCompany || c.IdParent_HRCompany == SessionDetail.IdHRCompany select new KeyValuePairModel<long, string> { Key = c.Id, Value = $"{c.CompanyNameNP}  [{c.HRCompanyCode.HRCompanyCodeTitle}]" }).ToList();
                            case (int)RoleType.RootUser:
                                return (from c in await _unitOfWork.Db.Set<HRCompany>().OrderBy(x => x.HRCompanyType.OrderBy).ToListAsync() where c.Id == this.SessionDetail.IdHRCompany select new KeyValuePairModel<long, string> { Key = c.Id, Value = $"{c.CompanyNameNP} [{c.HRCompanyCode.HRCompanyCodeTitle}]" }).ToList();
                            case (int)RoleType.SectionAdmin:
                                return (from c in await _unitOfWork.Db.Set<HRCompany>().OrderBy(x => x.HRCompanyType.OrderBy).ToListAsync() where c.Id == this.SessionDetail.IdHRCompany select new KeyValuePairModel<long, string> { Key = c.Id, Value = $"{c.CompanyNameNP}  [{c.HRCompanyCode.HRCompanyCodeTitle}]" }).ToList();

                            default: return (from c in await _unitOfWork.Db.Set<HRCompany>().OrderBy(x => x.HRCompanyType.OrderBy).ToListAsync() where c.Id == this.SessionDetail.IdHRCompany select new KeyValuePairModel<long, string> { Key = c.Id, Value = $"{c.CompanyNameNP}  [{c.HRCompanyCode.HRCompanyCodeTitle}]" }).ToList();
                        }

                    case PairModelTitle.Employee:
                        long? idHRDivision = this.SessionDetail.IdHRCompanyDivision;

                        object[] param =
                                            {
                                                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value= idHRCompany},
                                                new SqlParameter() {ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value = this.SessionDetail.IdHREmployee},
                                                new SqlParameter() {ParameterName = "@paramIdRoleType", SqlDbType = SqlDbType.Int, Value = this.SessionDetail.IdRoleType},
                                                new SqlParameter() {ParameterName = "@paramActive", SqlDbType = SqlDbType.Int, Value = 0},
                                                new SqlParameter() {ParameterName = "@paramSearch", SqlDbType = SqlDbType.NVarChar, Value = ""}
                                              };
                        model = (from c in await _unitOfWork.Db.Database.SqlQuery<proc_GetEmployeeByDesignation_Result>("EXEC proc_GetEmployeeByDesignation @paramIdHRCompany,@paramIdHREmployee,@paramIdRoleType,@paramActive,@paramSearch", param).ToListAsync()
                                 orderby c.HRDesignationOrder
                                 where c.IdHRCompany == idHRCompany && (c.IdHRCompanyDivision == idHRDivision || idHRDivision == 0)
                                 orderby c.HRDesignationOrder
                                 select new KeyValuePairModel<long, string> { Key = c.Id, Value = $"[{c.HREmployeeName}] {c.HREmployeeNameNP}    [{c.HRDesignationTitle}]   ({c.IdEnroll})" }).ToList();









                        switch (SessionDetail.IdRoleType)
                        {
                            case (int)RoleType.NormalUser:
                                return model;

                            default:
                                model.Insert(0, new KeyValuePairModel<long, string> { Key = 0, Value = ConfigurationManager.AppSettings["All"].ToString() });
                                return model;
                        }

                    case PairModelTitle.Division:

                        switch (SessionDetail.IdRoleType)
                        {
                            case (int)RoleType.SuperUser:
                                model = (from c in await _unitOfWork.Db.Set<HRCompanyDivision>().ToListAsync() where c.IdHRCompany == idHRCompany select new KeyValuePairModel<long, string> { Key = c.Id, Value = c.HRCompanyDivisionName }).ToList();
                                model.Insert(0, new KeyValuePairModel<long, string> { Key = 0, Value = ConfigurationManager.AppSettings["All"].ToString() });
                                return model;
                            case (int)RoleType.Admin:
                                model = (from c in await _unitOfWork.Db.Set<HRCompanyDivision>().ToListAsync() where c.IdHRCompany == idHRCompany select new KeyValuePairModel<long, string> { Key = c.Id, Value = c.HRCompanyDivisionName }).ToList();
                                model.Insert(0, new KeyValuePairModel<long, string> { Key = 0, Value = ConfigurationManager.AppSettings["All"].ToString() });
                                return model;
                            case (int)RoleType.RootUser:
                                model = (from c in await _unitOfWork.Db.Set<HRCompanyDivision>().ToListAsync() where c.IdHRCompany == idHRCompany && c.Id == this.SessionDetail.IdHRCompanyDivision select new KeyValuePairModel<long, string> { Key = c.Id, Value = c.HRCompanyDivisionName }).ToList();
                                return model;
                            case (int)RoleType.SectionAdmin:
                                model = (from c in await _unitOfWork.Db.Set<HRCompanyDivision>().ToListAsync() where c.IdHRCompany == idHRCompany && c.Id == this.SessionDetail.IdHRCompanyDivision select new KeyValuePairModel<long, string> { Key = c.Id, Value = c.HRCompanyDivisionName }).ToList();
                                return model;
                            default:
                                model = (from c in await _unitOfWork.Db.Set<HRCompanyDivision>().ToListAsync() where c.IdHRCompany == idHRCompany && c.Id == this.SessionDetail.IdHRCompanyDivision select new KeyValuePairModel<long, string> { Key = c.Id, Value = $"{c.HRCompanyDivisionName}[{c.HRCompanyDivisionType.HRCompanyDivisionTypeTitle}] " }).ToList();
                                return model;
                        }

                    case PairModelTitle.ShiftTitle:
                        model = (from c in await _unitOfWork.Db.Set<HRCompanyShiftRoaster>().ToListAsync() where c.IdHRCompany == idHRCompany && c.IsActive select new KeyValuePairModel<long, string> { Key = c.Id, Value = c.ShiftTitle }).ToList();
                        return model;
                    default:
                        return null;
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public virtual async Task<ICollection<KeyValuePairModel<Guid, string>>> GetGUIDStringPairModel(PairModelTitle title, long? idHRCompany = 0)
        {
            try
            {
                var model = new List<KeyValuePairModel<Guid, string>>();
                switch (title)
                {
                    case PairModelTitle.Device:
                        return (from c in await _unitOfWork.Db.Set<HRDevice>().ToListAsync() where c.IdHRCompany == idHRCompany || idHRCompany == 0 select new KeyValuePairModel<Guid, string> { Key = c.Id, Value = c.HRDeviceName }).ToList();
                    default:
                        model = new List<KeyValuePairModel<Guid, string>>() { new KeyValuePairModel<Guid, string> { Key = Guid.Empty, Value = ConfigurationManager.AppSettings["Select"].ToString() } };
                        return model;
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public virtual async Task<ICollection<KeyValuePairModel<long, string>>> GetEmployeeByDivision(long? idHRCompany, long? idDivision)
        {
            try
            {
                var model = new List<KeyValuePairModel<long, string>>();
                object[] param =
                                             {
                                                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value= idHRCompany},
                                                new SqlParameter() {ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value = this.SessionDetail.IdHREmployee},
                                                new SqlParameter() {ParameterName = "@paramIdRoleType", SqlDbType = SqlDbType.Int, Value = this.SessionDetail.IdRoleType},
                                                new SqlParameter() {ParameterName = "@paramSearch", SqlDbType = SqlDbType.NVarChar, Value = ""}
                                              };
                model = (from c in await _unitOfWork.Db.Database.SqlQuery<proc_GetEmployeeByDesignation_Result>("EXEC proc_GetEmployeeByDesignation @paramIdHRCompany,@paramIdHREmployee,@paramIdRoleType,@paramSearch", param).ToListAsync() orderby c.HRDesignationOrder where c.IdHRCompany == idHRCompany && (c.IdHRCompanyDivision == idDivision || idDivision == 0) select new KeyValuePairModel<long, string> { Key = c.Id, Value = $"{c.HREmployeeNameNP}    [{c.HRDesignationTitle}]   ({c.IdEnroll})" }).ToList();
                model.Insert(0, new KeyValuePairModel<long, string> { Key = 0, Value = ConfigurationManager.AppSettings["All"].ToString() });
                return model;
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }


        //public TreeModel<long, string> CompanyTree(long idHRCompany)
        //{
        //    try
        //    {
        //        var result = _unitOfWork.Db.Set<HRCompany>().FirstOrDefault(x => x.Id == idHRCompany);
        //        if (result == null)
        //        {
        //            result = new HRCompany()
        //            {
        //                Id = 0,
        //                CompanyNameNP = "नेपाल सरकार"
        //            };
        //        }
        //        TreeModel<long, string> tree = new TreeModel<long, string>()
        //        {
        //            key = result.Id,
        //            Value = result.CompanyNameNP,
        //            Branch = new List<TreeModel<long, string>>()
        //        };

        //        var resultlist = (_unitOfWork.Db.Set<HRCompany>().Where(x => x.IdParent_HRCompany == idHRCompany)).ToList();
        //        foreach (var item in resultlist)
        //        {
        //            tree.Branch.Add(CompanyTree(item.Id));
        //        }
        //        return tree;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ArgumentException(ex.Message);
        //    }
        //}
        #endregion

        #region JSON

        //Get HRCompanyShift Title
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<JsonResult> GetHRCompanyShiftTitle_Json(long? idHRCompany)
        {
            try
            {
                var model = (from c in await _unitOfWork.Db.Set<HRCompanyShiftRoaster>().ToListAsync() where c.IdHRCompany == idHRCompany && c.IsActive select new KeyValuePairModel<long, string> { Key = c.Id, Value = c.ShiftTitle }).ToList();
                //model.Insert(0, new KeyValuePairModel<long, string> { Key = 0, Value = "--चयन गर्नुहोस्--" });
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<JsonResult> GetProvinceWiseDistrict_Json(int? idState)
        {
            try
            {
                var model = (from c in await _unitOfWork.Db.Set<CountryDistrict>().ToListAsync() where c.IdCountryState == idState && c.IsActive select new KeyValuePairModel<long, string> { Key = c.Id, Value = c.CountryDistrictNameNP }).ToList();
                model.Insert(0, new KeyValuePairModel<long, string> { Key = 0, Value = "--चयन गर्नुहोस्--" });
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<JsonResult> GetAllDistrict_Json()
        {
            try
            {
                var model = (from c in await _unitOfWork.Db.Set<CountryDistrict>().ToListAsync() select new KeyValuePairModel<long, string> { Key = c.Id, Value = c.CountryDistrictNameNP }).ToList();
                model.Insert(0, new KeyValuePairModel<long, string> { Key = 0, Value = "--चयन गर्नुहोस्--" });
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        //Check Recommendation Enable
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<JsonResult> IsRecommendationEnable_Json(long? idLeaveType)
        {
            try
            {
                var model = await _unitOfWork.Db.Set<HRCompanyLeaveType>().Where(x => x.Id == idLeaveType).FirstOrDefaultAsync();
                if (model.IsRecommended)
                {
                    return Json("Enabled", JsonRequestBehavior.AllowGet);
                }
                else
                    return Json("NotEnable", JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        //CHECK National IDentity

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<JsonResult> CheckMobileNumber_Json(string mobileNumber)
        {
            try
            {
                var model = await _unitOfWork.Db.Set<HREmployee>().AnyAsync(x => x.MobileNumber == mobileNumber);
                if (!model)
                    return Json($"Available", JsonRequestBehavior.AllowGet);
                else
                    return Json(GetErrorMessage(UserErrorMessage.MOBILEEXIST), JsonRequestBehavior.AllowGet);
            }
            catch (Exception exp)
            {
                return Json(exp.Message, JsonRequestBehavior.AllowGet);
            }
        }

        //CHECK National IDentity

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<JsonResult> CheckNationalIdentity_Json(string identityNumber)
        {
            try
            {
                var model = await _unitOfWork.Db.Set<HREmployeeNationalIdentity>().AnyAsync(x => x.IdentityNumber == identityNumber);
                if (!model)
                    return Json($"Available", JsonRequestBehavior.AllowGet);
                else
                    return Json(GetErrorMessage(UserErrorMessage.NATIONALIDENTITYEXIST), JsonRequestBehavior.AllowGet);
            }
            catch (Exception exp)
            {
                return Json(exp.Message, JsonRequestBehavior.AllowGet);
            }
        }



        //check RoleType

        public int GetRole(long? IdEmployeeRole)
        {
            return this._unitOfWork.Db.Set<HREmployeeRole>().Where(x => x.Id == IdEmployeeRole).Select(x => x.IdRoleType).FirstOrDefault();
       }





        //CHECK IDEnroll

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<JsonResult> CheckIDEnroll_Json(long? idEnroll, long? idHRCompany,long? IdEmployeeRole)
        {
            try
            {
                var IDrole = GetRole(IdEmployeeRole);
                var model = await _unitOfWork.Db.Set<HREmployee>().AnyAsync(x => x.IdEnroll == idEnroll && x.IdHRCompany == idHRCompany);
                if (!model || IDrole ==4)
                    return Json($"Available", JsonRequestBehavior.AllowGet);
                else
                    return Json(GetErrorMessage(UserErrorMessage.IDENROLLEXIST), JsonRequestBehavior.AllowGet);
            }
            catch (Exception exp)
            {
                return Json(exp.Message, JsonRequestBehavior.AllowGet);
            }
        }

        //CHECK PIS Number

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<JsonResult> CheckPISNumber_Json(string pisNumber, long? IdEmployeeRole)
            {
            try
            {
                var IDrole = GetRole(IdEmployeeRole);
                
                var model = await _unitOfWork.Db.Set<HREmployee>().AnyAsync(x => x.PISNumber == pisNumber);
                if (!model || IDrole == 4)
                    return Json($"Available", JsonRequestBehavior.AllowGet);
                else
                    return Json(GetErrorMessage(UserErrorMessage.PISNUMBEREXIST), JsonRequestBehavior.AllowGet);
            }
            catch (Exception exp)
            {
                return Json(exp.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<JsonResult> Date_Json(DateTime date)
        {
            object[] myObjArray =
            {
                new SqlParameter() {ParameterName = "@p1", SqlDbType = SqlDbType.DateTime, Value= date}
            };
            var model = await _unitOfWork.Db.Database.SqlQuery<ifc_GetDateInfo_Result>("select * from ifc_GetDateInfo(@p1)", myObjArray).FirstOrDefaultAsync();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<JsonResult> GetRoleByCompany_Json(long? idHRCompany)
        {
            try
            {
                var model = await GetIntStringPairModel(PairModelTitle.Role, idHRCompany);
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exp)
            {
                return Json(exp.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<JsonResult> GetDivisionTypeByCompany_Json(long? idHRCompany)
        {
            try
            {
                var model = await GetLongStringPairModel(PairModelTitle.DivisionType, idHRCompany);
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exp)
            {
                return Json(exp.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<JsonResult> GetDivisionByCompany_Json(long? idHRCompany)
        {
            try
            {
                if (SessionDetail.IdRoleType == 3)
                {
                    var model = await GetLongStringPairModel(PairModelTitle.Division, idHRCompany, SessionDetail.IdHREmployee);
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
                else if (SessionDetail.IdRoleType == 4)
                {
                    var model = await GetLongStringPairModel(PairModelTitle.Division, idHRCompany, SessionDetail.IdHREmployee);
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var model = await GetLongStringPairModel(PairModelTitle.Division, idHRCompany);
                    return Json(model, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception exp)
            {
                return Json(exp.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<JsonResult> GetEmployeeByDivisionCompany_Json(long? idHRCompany, long? idDivision)
        {
            try
            {
                var model = new List<KeyValuePairModel<long, string>>();
                long? Idhremployee = 0;


                if (SessionDetail.IdRoleType == 4)
                {
                    object[] param =
                    {
                                                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value= idHRCompany},
                                                new SqlParameter() {ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value = SessionDetail.IdHREmployee},
                                                new SqlParameter() {ParameterName = "@paramIdRoleType", SqlDbType = SqlDbType.Int, Value = SessionDetail.IdRoleType},
                                                new SqlParameter() {ParameterName = "@paramActive", SqlDbType = SqlDbType.Int, Value = 0},
                                                new SqlParameter() {ParameterName = "@paramSearch", SqlDbType = SqlDbType.NVarChar, Value = ""}
                                              };
                    model = (from c in await _unitOfWork.Db.Database.SqlQuery<proc_GetEmployeeByDesignation_Result>("EXEC proc_GetEmployeeByDesignation @paramIdHRCompany,@paramIdHREmployee,@paramIdRoleType,@paramActive,@paramSearch", param).ToListAsync() orderby c.HRDesignationOrder where c.IdHRCompany == idHRCompany && c.IdParent_HREmployee == SessionDetail.IdHREmployee && (c.IdHRCompanyDivision == idDivision || idDivision == 0) orderby c.HRDesignationOrder select new KeyValuePairModel<long, string> { Key = c.Id, Value = $"{c.HREmployeeNameNP}    [{c.HRDesignationTitle}]   ({c.IdEnroll})" }).ToList();
                    model.Insert(0, new KeyValuePairModel<long, string> { Key = 0, Value = ConfigurationManager.AppSettings["All"].ToString() });
                }
                else if (SessionDetail.IdRoleType == 3)
                {
                    Idhremployee = SessionDetail.IdHREmployee;
                    object[] param =
                                             {   new SqlParameter() { ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value = idHRCompany },
                                                new SqlParameter() { ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value = Idhremployee },
                                                new SqlParameter() { ParameterName = "@paramIdRoleType", SqlDbType = SqlDbType.Int, Value = SessionDetail.IdRoleType },
                                                new SqlParameter() {ParameterName = "@paramActive", SqlDbType = SqlDbType.Int, Value = 0},
                                                new SqlParameter() { ParameterName = "@paramSearch", SqlDbType = SqlDbType.NVarChar, Value = "" }
                                              };
                    model = (from c in await _unitOfWork.Db.Database.SqlQuery<proc_GetEmployeeByDesignation_Result>("EXEC proc_GetEmployeeByDesignation @paramIdHRCompany,@paramIdHREmployee,@paramIdRoleType,@paramActive,@paramSearch", param).ToListAsync() orderby c.HRDesignationOrder where c.IdHRCompany == idHRCompany && (c.IdHRCompanyDivision == idDivision || idDivision == 0) orderby c.HRDesignationOrder select new KeyValuePairModel<long, string> { Key = c.Id, Value = $"[{c.HREmployeeName}] {c.HREmployeeNameNP}    [{c.HRDesignationTitle}]   ({c.IdEnroll})" }).ToList();

                }
                else if (SessionDetail.IdRoleType == 2)
                {
                    Idhremployee = SessionDetail.IdHREmployee;
                    object[] param =
                                             {   new SqlParameter() { ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value = idHRCompany },
                                                new SqlParameter() { ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value = 0 },
                                                new SqlParameter() { ParameterName = "@paramIdRoleType", SqlDbType = SqlDbType.Int, Value = 2 },
                                                new SqlParameter() {ParameterName = "@paramActive", SqlDbType = SqlDbType.Int, Value = 0},
                                                new SqlParameter() { ParameterName = "@paramSearch", SqlDbType = SqlDbType.NVarChar, Value = "" }
                                              };
                    model = (from c in await _unitOfWork.Db.Database.SqlQuery<proc_GetEmployeeByDesignation_Result>("EXEC proc_GetEmployeeByDesignation @paramIdHRCompany,@paramIdHREmployee,@paramIdRoleType,@paramActive,@paramSearch", param).ToListAsync() orderby c.HRDesignationOrder where c.IdHRCompany == idHRCompany && (c.IdHRCompanyDivision == idDivision || idDivision == 0) orderby c.HRDesignationOrder select new KeyValuePairModel<long, string> { Key = c.Id, Value = $"[{c.HREmployeeName}] {c.HREmployeeNameNP}    [{c.HRDesignationTitle}]   ({c.IdEnroll})" }).ToList();

                    model.Insert(0, new KeyValuePairModel<long, string> { Key = 0, Value = ConfigurationManager.AppSettings["All"].ToString() });
                }
                else 
                {
                object[] param =
                                         {   new SqlParameter() { ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value = idHRCompany },
                                                new SqlParameter() { ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value = 0 },
                                                new SqlParameter() { ParameterName = "@paramIdRoleType", SqlDbType = SqlDbType.Int, Value = 0 },
                                                new SqlParameter() {ParameterName = "@paramActive", SqlDbType = SqlDbType.Int, Value = 0},
                                                new SqlParameter() { ParameterName = "@paramSearch", SqlDbType = SqlDbType.NVarChar, Value = "" }
                                              };
                    model = (from c in await _unitOfWork.Db.Database.SqlQuery<proc_GetEmployeeByDesignation_Result>("EXEC proc_GetEmployeeByDesignation @paramIdHRCompany,@paramIdHREmployee,@paramIdRoleType,@paramActive,@paramSearch", param).ToListAsync() orderby c.HRDesignationOrder where c.IdHRCompany == idHRCompany && (c.IdHRCompanyDivision == idDivision || idDivision == 0) orderby c.HRDesignationOrder select new KeyValuePairModel<long, string> { Key = c.Id, Value = $"[{c.HREmployeeName}] {c.HREmployeeNameNP}    [{c.HRDesignationTitle}]   ({c.IdEnroll})" }).ToList();
                    model.Insert(0, new KeyValuePairModel<long, string> { Key = 0, Value = ConfigurationManager.AppSettings["All"].ToString() });

                }
             
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exp)
            {
                return Json(exp.Message, JsonRequestBehavior.AllowGet);
            }
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<JsonResult> GetEmployeeByJobStatus_Json(long? idHRCompany, long? idDivision, int? idJobStatus)
        {

            //total  child contains idjobstatus from storeprocedure
            try
            {
                var model = new List<KeyValuePairModel<long, string>>();
                long? Idhremployee = 0;

                if (SessionDetail.IdRoleType == 4)
                {
                    object[] param =
                    {
                                                new SqlParameter() {ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value= idHRCompany},
                                                new SqlParameter() {ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value = SessionDetail.IdHREmployee},
                                                new SqlParameter() {ParameterName = "@paramIdRoleType", SqlDbType = SqlDbType.Int, Value = SessionDetail.IdRoleType},
                                                new SqlParameter() {ParameterName = "@paramActive", SqlDbType = SqlDbType.Int, Value = 0},
                                                new SqlParameter() {ParameterName = "@paramSearch", SqlDbType = SqlDbType.NVarChar, Value = ""}
                                              };
                    model = (from c in await _unitOfWork.Db.Database.SqlQuery<proc_GetEmployeeByDesignation_Result>("EXEC proc_GetEmployeeByDesignation @paramIdHRCompany,@paramIdHREmployee,@paramIdRoleType,@paramActive,@paramSearch", param).ToListAsync() orderby c.HRDesignationOrder where c.IdHRCompany == idHRCompany && (c.IdHRCompanyDivision == idJobStatus || null == null) && (c.TotalChild == idJobStatus || idJobStatus == null) orderby c.HRDesignationOrder select new KeyValuePairModel<long, string> { Key = c.Id, Value = $"{c.HREmployeeNameNP}    [{c.HRDesignationTitle}]   ({c.IdEnroll})" }).ToList();
                    model.Insert(0, new KeyValuePairModel<long, string> { Key = 0, Value = ConfigurationManager.AppSettings["All"].ToString() });
                }
                else if (SessionDetail.IdRoleType == 3)
                {
                    Idhremployee = SessionDetail.IdHREmployee;
                    object[] param =
                                             {   new SqlParameter() { ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value = idHRCompany },
                                                new SqlParameter() { ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value = Idhremployee },
                                                new SqlParameter() { ParameterName = "@paramIdRoleType", SqlDbType = SqlDbType.Int, Value = SessionDetail.IdRoleType },
                                                new SqlParameter() {ParameterName = "@paramActive", SqlDbType = SqlDbType.Int, Value = 0},
                                                new SqlParameter() { ParameterName = "@paramSearch", SqlDbType = SqlDbType.NVarChar, Value = "" }
                                              };
                    model = (from c in await _unitOfWork.Db.Database.SqlQuery<proc_GetEmployeeByDesignation_Result>("EXEC proc_GetEmployeeByDesignation @paramIdHRCompany,@paramIdHREmployee,@paramIdRoleType,@paramActive,@paramSearch", param).ToListAsync() orderby c.HRDesignationOrder where c.IdHRCompany == idHRCompany && (c.IdHRCompanyDivision == idDivision || idDivision == 0) && (c.TotalChild == idJobStatus || idJobStatus == null) orderby c.HRDesignationOrder select new KeyValuePairModel<long, string> { Key = c.Id, Value = $"[{c.HREmployeeName}] {c.HREmployeeNameNP}    [{c.HRDesignationTitle}]   ({c.IdEnroll})" }).ToList();

                }
                else if (SessionDetail.IdRoleType == 2)
                {
                    Idhremployee = SessionDetail.IdHREmployee;
                    object[] param =
                                             {   new SqlParameter() { ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value = idHRCompany },
                                                new SqlParameter() { ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value = 0 },
                                                new SqlParameter() { ParameterName = "@paramIdRoleType", SqlDbType = SqlDbType.Int, Value = 2 },
                                                new SqlParameter() {ParameterName = "@paramActive", SqlDbType = SqlDbType.Int, Value = 0},
                                                new SqlParameter() { ParameterName = "@paramSearch", SqlDbType = SqlDbType.NVarChar, Value = "" }
                                              };
                    model = (from c in await _unitOfWork.Db.Database.SqlQuery<proc_GetEmployeeByDesignation_Result>("EXEC proc_GetEmployeeByDesignation @paramIdHRCompany,@paramIdHREmployee,@paramIdRoleType,@paramActive,@paramSearch", param).ToListAsync() orderby c.HRDesignationOrder where c.IdHRCompany == idHRCompany && (c.IdHRCompanyDivision == idDivision || idDivision == 0) && (c.TotalChild == idJobStatus || idJobStatus == null) orderby c.HRDesignationOrder select new KeyValuePairModel<long, string> { Key = c.Id, Value = $"[{c.HREmployeeName}] {c.HREmployeeNameNP}    [{c.HRDesignationTitle}]   ({c.IdEnroll})" }).ToList();

                    model.Insert(0, new KeyValuePairModel<long, string> { Key = 0, Value = ConfigurationManager.AppSettings["All"].ToString() });
                }
                else
                {
                    object[] param =
                                             {   new SqlParameter() { ParameterName = "@paramIdHRCompany", SqlDbType = SqlDbType.BigInt, Value = idHRCompany },
                                                new SqlParameter() { ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value = 0 },
                                                new SqlParameter() { ParameterName = "@paramIdRoleType", SqlDbType = SqlDbType.Int, Value = 0 },
                                                new SqlParameter() {ParameterName = "@paramActive", SqlDbType = SqlDbType.Int, Value = 0},
                                                new SqlParameter() { ParameterName = "@paramSearch", SqlDbType = SqlDbType.NVarChar, Value = "" }
                                              };
                    model = (from c in await _unitOfWork.Db.Database.SqlQuery<proc_GetEmployeeByDesignation_Result>("EXEC proc_GetEmployeeByDesignation @paramIdHRCompany,@paramIdHREmployee,@paramIdRoleType,@paramActive,@paramSearch", param).ToListAsync() orderby c.HRDesignationOrder where c.IdHRCompany == idHRCompany && (c.IdHRCompanyDivision == idDivision || idDivision == 0) && (c.TotalChild == idJobStatus || idJobStatus == null) orderby c.HRDesignationOrder select new KeyValuePairModel<long, string> { Key = c.Id, Value = $"[{c.HREmployeeName}] {c.HREmployeeNameNP}    [{c.HRDesignationTitle}]   ({c.IdEnroll})" }).ToList();
                    model.Insert(0, new KeyValuePairModel<long, string> { Key = 0, Value = ConfigurationManager.AppSettings["All"].ToString() });

                }

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exp)
            {
                return Json(exp.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<JsonResult> GetEmployeeDivisionByCompanyDivision_Json(long? idHRCompany, long? idDivision)
        {
            try
            {
                var model = new List<KeyValuePairModel<long, string>>();
                switch (SessionDetail.IdRoleType)
                {
                    case (int)RoleType.SuperUser:
                        model = (from c in await _unitOfWork.Db.Set<HREmployeeJobHistory>().GroupBy(x => new { x.HREmployee.Id, x.HREmployee.HREmployeeName, x.HREmployee.IdEnroll, x.IdHRCompany, x.IdHRCompanyDivision }).ToListAsync() where (c.Key.IdHRCompany == idHRCompany) && (c.Key.IdHRCompanyDivision == idDivision || idDivision == 0) select new KeyValuePairModel<long, string> { Key = c.Key.Id, Value = $"{c.Key.HREmployeeName} [{c.Key.IdEnroll}]" }).ToList();
                        model.Insert(0, new KeyValuePairModel<long, string> { Key = 0, Value = "None" });
                        return Json(model, JsonRequestBehavior.AllowGet);
                    case (int)RoleType.Admin:
                        model = (from c in await _unitOfWork.Db.Set<HREmployeeJobHistory>().GroupBy(x => new { x.HREmployee.Id, x.HREmployee.HREmployeeName, x.HREmployee.IdEnroll, x.IdHRCompany, x.IdHRCompanyDivision }).ToListAsync() where (c.Key.IdHRCompany == idHRCompany) && (c.Key.IdHRCompanyDivision == idDivision || idDivision == 0) select new KeyValuePairModel<long, string> { Key = c.Key.Id, Value = $"{c.Key.HREmployeeName} [{c.Key.IdEnroll}]" }).ToList();
                        model.Insert(0, new KeyValuePairModel<long, string> { Key = 0, Value = "None" });
                        return Json(model, JsonRequestBehavior.AllowGet);
                    case (int)RoleType.RootUser:
                        model = (from c in await _unitOfWork.Db.Set<HREmployeeJobHistory>().GroupBy(x => new { x.HREmployee.Id, x.HREmployee.HREmployeeName, x.HREmployee.IdEnroll, x.IdHRCompany, x.IdHRCompanyDivision }).ToListAsync() where (c.Key.IdHRCompany == SessionDetail.IdHRCompany) && (c.Key.IdHRCompanyDivision == idDivision || idDivision == 0) select new KeyValuePairModel<long, string> { Key = c.Key.Id, Value = $"{c.Key.HREmployeeName} [{c.Key.IdEnroll}]" }).ToList();
                        model.Insert(0, new KeyValuePairModel<long, string> { Key = 0, Value = "None" });
                        return Json(model, JsonRequestBehavior.AllowGet);
                    case (int)RoleType.NormalUser:
                        model = (from c in await _unitOfWork.Db.Set<HREmployee>().ToListAsync() where c.Id == SessionDetail.IdHREmployee select new KeyValuePairModel<long, string> { Key = c.Id, Value = $"{c.HREmployeeName} [{c.IdEnroll}]" }).ToList();
                        model.Insert(0, new KeyValuePairModel<long, string> { Key = 0, Value = "None" });
                        return Json(model, JsonRequestBehavior.AllowGet);
                    default:
                        return null;
                }
            }
            catch (Exception exp)
            {
                return Json(exp.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<JsonResult> GetEmployeeCategoryByCompany_Json(long? idHRCompany)
        {
            try
            {
                var model = await GetLongStringPairModel(PairModelTitle.EmployeeCategory, idHRCompany);
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exp)
            {
                return Json(exp.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<JsonResult> GetCompanyBranch_Json(long? IdHRCompany)
        {
            try
            {
                var model = await GetLongStringPairModel(PairModelTitle.HRCompany, IdHRCompany);
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exp)
            {
                return Json(exp.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<JsonResult> GetCivilServiceTypeByCompany_Json(long? idHRCompany)
        {
            try
            {
                var model = await GetLongStringPairModel(PairModelTitle.ServiceType, idHRCompany);
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exp)
            {
                return Json(exp.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<JsonResult> GetDesignationByCompany_Json(long? idHRCompany)
        {
            try
            {
                var model = await GetLongStringPairModel(PairModelTitle.Designation, idHRCompany);
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exp)
            {
                return Json(exp.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<JsonResult> GetSystemStructureBySystemMenu_Json(long? idSystemMenu)
        {
            try
            {
                var model = await GetIntStringPairModel(PairModelTitle.SystemStructureBySystemMenu, idSystemMenu);
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exp)
            {
                return Json(exp.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<JsonResult> GetEmployee_Json(long? Id)
        {
            try
            {
                var model = await GetLongStringPairModel(PairModelTitle.Employee, Id);
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exp)
            {
                return Json(exp.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<JsonResult> GetLeaveType_Json(long? Id)
        {
            try
            {
                var model = await GetLongStringPairModel(PairModelTitle.LeaveType, Id);
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exp)
            {
                return Json(exp.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<JsonResult> GetUser(string searchKey = "")
        {
            try
            {
                var model = await GetLongStringPairModel(PairModelTitle.Employee);
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exp)
            {
                return Json(exp.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<JsonResult> GetHREmployee_Json(string searchKey = "")
        {
            try
            {
                List<KeyValuePairModel<string, long>> model;
                IEnumerable<HREmployee> empList;
                switch (this.SessionDetail.IdRoleType)
                {
                    case (int)RoleType.SuperUser:
                        empList = await _unitOfWork.Db.Set<HREmployee>().Where(x => x.HREmployeeName.ToUpper().ToString().Contains(searchKey.ToUpper().ToString()) || x.PISNumber.ToUpper().ToString().Contains(searchKey.ToUpper().ToString()) || x.HREmployeeNameNP.ToUpper().ToString().Contains(searchKey.ToUpper().ToString())).ToListAsync();
                        model = empList.Select(x => new KeyValuePairModel<string, long>() { Key = $"{x.HREmployeeNameNP}[{x.PISNumber}]", Value = x.Id }).ToList();
                        break;
                    case (int)RoleType.Admin:
                        empList = await _unitOfWork.Db.Set<HREmployee>().Where(x => x.IdHRCompany == this.SessionDetail.IdHRCompany && (x.HREmployeeName.ToUpper().ToString().Contains(searchKey.ToUpper().ToString()) || x.PISNumber.ToUpper().ToString().Contains(searchKey.ToUpper().ToString()) || x.HREmployeeNameNP.ToUpper().ToString().Contains(searchKey.ToUpper().ToString()))).ToListAsync();
                        model = empList.Select(x => new KeyValuePairModel<string, long>() { Key = $"{x.HREmployeeNameNP}[{x.PISNumber}]", Value = x.Id }).ToList();
                        break;
                    default:
                        empList = await _unitOfWork.Db.Set<HREmployee>().Where(x => x.IdHRCompany == this.SessionDetail.IdHRCompany && x.Id == this.SessionDetail.IdHREmployee && (x.HREmployeeName.ToUpper().ToString().Contains(searchKey.ToUpper().ToString()) || x.PISNumber.ToUpper().ToString().Contains(searchKey.ToUpper().ToString()) || x.HREmployeeNameNP.ToUpper().ToString().Contains(searchKey.ToUpper().ToString()))).ToListAsync();
                        model = empList.Select(x => new KeyValuePairModel<string, long>() { Key = $"{x.HREmployeeNameNP}[{x.PISNumber}]", Value = x.Id }).ToList();
                        break;
                }
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exp)
            {
                return Json(exp.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<JsonResult> GetBalanceLeaveDays_Json(long? idLeaveType, long? idHREmployee)
        {
            try
            {
                int yearNP = this.Today.Result.NepYear;
                decimal days = await this.GetAvailableLeaveDays(idHREmployee, idLeaveType, yearNP);
                return Json(days.ToString(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception exp)
            {
                return Json(exp.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<decimal> GetAvailableLeaveDays(long? idHREmployee, long? idLeaveType, int yearNP)
        {
            try
            {
                object[] obj =
               {
                new SqlParameter() {ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value = idHREmployee},
                new SqlParameter() {ParameterName = "@paramIdLeaveType", SqlDbType = SqlDbType.BigInt, Value = idLeaveType},
                new SqlParameter() {ParameterName = "@paramYearNP", SqlDbType = SqlDbType.Int, Value =yearNP }
            };
                decimal day = await _unitOfWork.Db.Database.SqlQuery<decimal>("EXEC proc_GetAvailableAndTakenLeave @paramIdHREmployee,@paramIdLeaveType,@paramYearNP", obj).FirstOrDefaultAsync();
                return day;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<proc_GenerateEndDateOfLeave_Result> GetEndDateAndAvailableDaysLeave(long? idHREmployee, long? idLeaveType, DateTime startDate, DateTime endDate)
        {
            try
            {
                object[] obj =
              {
                new SqlParameter() {ParameterName = "@paramIdHREmployee", SqlDbType = SqlDbType.BigInt, Value = idHREmployee},
                new SqlParameter() {ParameterName = "@paramIdLeaveType", SqlDbType = SqlDbType.Int, Value = idLeaveType},
                new SqlParameter() {ParameterName = "@paramStartDate", SqlDbType = SqlDbType.Date, Value =startDate },
                new SqlParameter() {ParameterName = "@paramEndDate", SqlDbType = SqlDbType.Date, Value =endDate }
            };
                return await _unitOfWork.Db.Database.SqlQuery<proc_GenerateEndDateOfLeave_Result>("EXEC proc_GenerateEndDateOfLeave @paramIdHREmployee,@paramIdLeaveType,@paramStartDate,@paramEndDate", obj).FirstOrDefaultAsync();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<JsonResult> GetEndDateOfLeave_Json(long? idHREmployee, long? idLeaveType, decimal day, string startdateNP)
        {
            try
            {
                var NepDate = "";
                DateTime startDate = (DateTime)GetEnglishDate(startdateNP);
                DateTime endDate = startDate.AddDays(double.Parse(day.ToString()));
                var model = await this.GetEndDateAndAvailableDaysLeave(idHREmployee, idLeaveType, startDate, endDate);
                if (model == null)
                {
                    NepDate = DateTime.Now.DateNp();
                }
                else
                {
                    NepDate = model.NepDate;
                }
                return Json(NepDate, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exp)
            {
                return Json(exp.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<JsonResult> GetAvailableLeaveDays_Json(long? idHREmployee, long? idLeaveType, string dateNP)
        {
            try
            {
                decimal day = await this.GetAvailableLeaveDays(idHREmployee, idLeaveType, GetYearFromNepaliDate(dateNP));
                return Json(day.ToString(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception exp)
            {
                return Json(exp.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<JsonResult> GetLastDateLeave_Json(long? idHREmployee)
        {
            try
            {
                string date = $"{Today.Result.NepYear}-01-01";
                var model = await _unitOfWork.Db.Set<HREmployeeLeaveHistory>().Where(x => x.IdHREmployee == idHREmployee && x.IsActive && x.IdLeaveStatus != 4).ToListAsync();
                if (model.Count > 0)
                {
                    DateTime maxdate = model.Max(x => x.LeaveValidFrom);
                    maxdate = maxdate.AddDays(1);
                    date = GetNepaliDate(maxdate);
                }
                return Json(date, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exp)
            {
                return Json(exp.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<DateTime> GetLastDateKaaj(long? idHREmployee)
        {
            DateTime date = DateTime.Now.AddDays(-5);
            var model = await _unitOfWork.Db.Set<HREmployeeKaajHistory>().Where(x => x.IdHREmployee == idHREmployee && x.IsActive && x.IdKaajStatus != 4).ToListAsync();
            if (model.Count > 0)
            {
                date = model.Max(x => x.KaajToDate ?? DateTime.Now).AddDays(1);
            }
            return date;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<JsonResult> IsHalfDay_Json(long? idLeaveType)
        {
            try
            {
                var isHalfDay = await _unitOfWork.Db.Set<HRCompanyLeaveType>().FirstOrDefaultAsync(x => x.Id == idLeaveType);
                if (isHalfDay.EnableHalfDay)
                {
                    return Json("YES", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("NO", JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception)
            {
                return Json("NO", JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]

        public async Task<JsonResult> IsRecommended_Json(long? idLeaveType)
        {
            try
            {
                var isHalfDay = await _unitOfWork.Db.Set<HRCompanyLeaveType>().FirstOrDefaultAsync(x => x.Id == idLeaveType);
                if (isHalfDay.IsRecommended)
                {
                    return Json("YES", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("NO", JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception)
            {
                return Json("NO", JsonRequestBehavior.AllowGet);
            }
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<JsonResult> GetEmployeeParentsCompany(long? idHRCompany)
        {
            try
            {
                var model = await _unitOfWork.Db.Set<HRCompany>().FirstOrDefaultAsync(x => x.Id == idHRCompany);
                var data = await _unitOfWork.Db.Set<HRCompany>().FirstOrDefaultAsync(x => x.Id == model.IdParent_HRCompany);
                if (data == null)
                { return Json((""), JsonRequestBehavior.AllowGet); }
                return Json(data.CompanyNameNP.ToString(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception exp)
            {
                return Json(exp.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<JsonResult> GetParentsCompany(long? idHRCompany)
        {
            try
            {
                var model = await _unitOfWork.Db.Set<HRCompany>().FirstOrDefaultAsync(x => x.Id == idHRCompany);
                var data = await _unitOfWork.Db.Set<HRCompany>().FirstOrDefaultAsync(x => x.Id == model.IdParent_HRCompany);
                if (data == null)
                { return Json(ConfigurationManager.AppSettings["HRComanyCentral"].ToString(), JsonRequestBehavior.AllowGet); }
                return Json((data.CompanyNameNP.ToString() + "-" + data.HRCompanyType.CompanyType), JsonRequestBehavior.AllowGet);
            }
            catch (Exception exp)
            {
                return Json(exp.Message, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region File Services
        protected string SaveImage(string FolderName, HttpPostedFileWrapper image, bool resize = true)
        {
            Guid Name = Guid.NewGuid();
            string imageName = null;
            string filePath = string.Empty;
            try
            {
                if (image != null)
                {
                    WebImage img = new WebImage(image.InputStream);
                    if (resize || img.Width > 1000)
                    {
                        img.Resize(1000, 1000);
                    }
                    FileInfo fi = new FileInfo(image.FileName);
                    imageName = $"{Name}.{img.ImageFormat}";
                    filePath = $"{FileRootPath}\\Image\\{FolderName }";
                    if (!(new DirectoryInfo(filePath).Exists)) Directory.CreateDirectory(filePath);
                    string targetPath = Path.Combine(filePath, imageName);
                    img.Save(targetPath);
                    return imageName;
                }
                else
                    return imageName;
            }
            catch
            {
                return imageName;
            }
        }

        protected string SaveFiles(string FolderName, HttpPostedFileWrapper file)
        {
            Guid Name = Guid.NewGuid();
            string fileName = null;
            string filePath = string.Empty;
            try
            {
                if (file != null)
                {
                    FileInfo fi = new FileInfo(file.FileName);
                    fileName = Name + fi.Extension;
                    filePath = $"{FileRootPath }\\Files\\{FolderName}";
                    if (!(new DirectoryInfo(filePath).Exists)) Directory.CreateDirectory(filePath);
                    string targetPath = Path.Combine(filePath, fileName);
                    file.SaveAs(targetPath);
                    return fileName;
                }
                else
                    return fileName;
            }
            catch
            {
                return fileName;
            }
        }
        public string GetFilePath(string FolderName, string FileName)
        {
            string url = string.Empty;
            string serverPath = string.Empty;
            try
            {
                if (FileName != null)
                {
                    url = $"{FileUrlPath}Files/{FolderName}/{FileName}";
                    serverPath = System.Web.HttpContext.Current.Server.MapPath("~") + url;
                    if (!System.IO.File.Exists(serverPath))
                        serverPath = "";
                    return serverPath;
                }
                else return serverPath;
            }
            catch
            {
                return serverPath;
            }
        }
        public string GetImagePath(string FolderName, string ImageName)
        {
            string url = string.Empty;
            string serverPath = string.Empty;
            try
            {
                url = ImageName == null ? "/images/avatars/avatar6.png" : $"{FileUrlPath}Image/{FolderName}/{ImageName}";
                serverPath = System.Web.HttpContext.Current.Server.MapPath("~") + url;
                if (!System.IO.File.Exists(serverPath))
                    url = "/images/avatars/avatar6.png";
                return url;
            }
            catch
            {
                return "/images/avatars/avatar6.png";
            }
        }


        public string GetFileName(string uploadDirPath, string name, string fileExtension, int id = 0)
        {
            //name = name.Replace(' ', '-');
            var newName = name + "-" + id.ToString();
            // uploadDirPath = uploadDirPath + newName;
            id++;
            if (System.IO.File.Exists(uploadDirPath + "\\" + newName + fileExtension))
            {
                newName = GetFileName(uploadDirPath, name, fileExtension, id);
            }
            return newName;
        }


        public string FileUpload(HttpPostedFileBase File, string FolderName)
        {
            string serverPath = Server.MapPath("~");
            string DocName = File.FileName;
            string DocPath = serverPath + "Upload\\" + FolderName;
            var fileExt = Path.GetExtension(File.FileName);
            if (!(new DirectoryInfo(DocPath).Exists)) Directory.CreateDirectory(DocPath);
            var newDocName = GetFileName(DocPath, Path.GetFileNameWithoutExtension(DocName), fileExt);
            string targetPath = Path.Combine(DocPath, newDocName + fileExt);
            File.SaveAs(targetPath);
            return newDocName + fileExt;
        }
       
        #endregion

        #region date conversion
        public string GetNepaliDate(DateTime dateTime)
        {
            var model = Date(dateTime);
            return model.Nepdate;
        }

        public Nullable<DateTime> GetEnglishDate(string dateNP)
        {
            var model = GetDateByDateNP(dateNP);
            return model.EngDate;
        }
        #endregion

        public async Task<string> GetJobStatusTitle(int? idJobStatus)
        {
            try
            {
                if (idJobStatus == 0 || idJobStatus == null)
                {
                    return ConfigurationManager.AppSettings["All"].ToString();
                }
                else
                    return await _unitOfWork.Db.Set<HREmployeePositionStatusType>().Where(x => x.Id == idJobStatus).Select(x => x.Title).FirstOrDefaultAsync();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }
        public async Task<string> GetBranchTitle(long? idDivision)
        {
            try
            {
                if (idDivision == 0 || idDivision == null)
                {
                    return ConfigurationManager.AppSettings["All"].ToString();
                }
                else
                    return await _unitOfWork.Db.Set<HRCompanyDivision>().Where(x => x.Id == idDivision).Select(x => x.HRCompanyDivisionName).FirstOrDefaultAsync();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public bool ValidateSameYear(string fromDateNP, string toDateNP)
        {
            try
            {
                string[] fromDatenp = fromDateNP.Split('-');
                string[] todatenp = toDateNP.Split('-');
                return fromDatenp[0] == todatenp[0] ? true : false;
            }
            catch
            {
                return false;
            }
        }

        public int GetYearFromNepaliDate(string dateNP)
        {
            try
            {
                return int.Parse(dateNP.Split('-')[0]);
            }
            catch
            {
                return this.Today.Result.NepYear;
            }
        }

        public int GetMasterLeaveTitleId(long idLeaveType)
        {
            try
            {
                return this._unitOfWork.Db.Set<HRCompanyLeaveType>().Where(x => x.Id == idLeaveType).Select(x => x.IdMasterLeaveTitle).FirstOrDefault();
            }
            catch
            {
                return 0;
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<JsonResult> GetHRDeviceAsync_Json(long? idHRCompany)
        {
            try
            {
                return Json(await this.GetGUIDStringPairModel(PairModelTitle.Device, idHRCompany), JsonRequestBehavior.AllowGet);
            }
            catch (Exception exp)
            {
                return Json(exp.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public string GetDesignationByIDhremployee(long? idhremployeee)
        {
            try
            {
                var id= this._unitOfWork.Db.Set<HREmployeeJobHistory>().Where(x => x.IdHREmployee == idhremployeee && x.IsCurrentStatus==true).Select(x => x.IdHRDesignation).FirstOrDefault();
                var position = this._unitOfWork.Db.Set<HRDesignation>().Where(x => x.Id == id).FirstOrDefault().HRDesignationTitle;
                return position;
            }
            catch
            {
                return "";
            }
        }


        public long? Getidhremployee(string UserName)
        {
            try
            {
                var MembershipID = this._unitOfWork.Db.Set<SystemDatabase.Login>().Where(x => x.UserName == UserName).Select(x => x.Id).FirstOrDefault();
                var HremployeeID = this._unitOfWork.Db.Set<MembershipProvider>().Where(x => x.IdLogin == MembershipID).Select(x => x.IdHREmployee).FirstOrDefault();
                return HremployeeID;
            }
            catch
            {
                return 0;
            }
        }

        public string GetEmailByIdhremployee(long? Idhremployee)
        {
            try
            {
                var EmailAddress = this._unitOfWork.Db.Set<HREmployee>().Where(x => x.Id == Idhremployee).Select(x => x.Email).FirstOrDefault();
                return EmailAddress;
            }
            catch
            {
                return "";
            }
        }


        public long? GetEnrollByPiSNumber(string PISNumber)
        {
            try
            {
                var EnrollId = this._unitOfWork.Db.Set<HREmployee>().Where(x => x.PISNumber == PISNumber).Select(x=> x.IdEnroll).FirstOrDefault();
                return EnrollId;
            }
            catch
            {
                return 0;
            }
        }
        public Guid? GetLoginID(string UserName)
        {
            try
            {
                var MembershipID = this._unitOfWork.Db.Set<SystemDatabase.Login>().Where(x => x.UserName == UserName).Select(x => x.Id).FirstOrDefault();
                return MembershipID;
            }
            catch
            {
                return null;
            }
        }


        [HttpPost]
        public ActionResult SendEmail(string receiver, string subject, string message)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var senderEmail = new MailAddress("perfectactivesolutions@gmail.com", "EattendanceSystem");
                    var receiverEmail = new MailAddress(receiver, "Receiver");
                    var password = "Shreepur@61";
                    var sub = subject;
                    var body = message;
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }
                    return View();
                }
            }
            catch (Exception ex)
            {
                var a = ex.Message;
                ViewBag.Error = "Some Error";
            }
            return View();
        }
        public int Idroletype(int idrole)
        {
            
                var IdRoleType = this._unitOfWork.Db.Set<HREmployeeRole>().Where(x => x.Id == idrole).Select(x => x.IdRoleType).FirstOrDefault();
                return IdRoleType;
        }

        public int MasterRole(long? IdHREmployee)
        {

            var RoleID = this._unitOfWork.Db.Set<MembershipProvider>().Where(x => x.IdHREmployee == IdHREmployee).Select(x => x.IdHREmployeeRole).FirstOrDefault();
            return GetRole(RoleID);
        }

        public List<DeviceLogsModel> ReadManualAttendanceExcelFile(MemoryStream ms)
        {
            List<DeviceLogsModel> deviceLogs = new List<DeviceLogsModel>();
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (ExcelPackage pack = new ExcelPackage(ms))
            {
                var worksheet = pack.Workbook.Worksheets.FirstOrDefault();
                var start = worksheet.Dimension.Start;
                var end = worksheet.Dimension.End;

                for (int row = start.Row + 1; row <= end.Row; row++)
                {
                    var deviceLog = new DeviceLogsModel();
                    // Row by row...

                    deviceLog.Id = Guid.NewGuid();
                    deviceLog.DeviceNumber = int.Parse(worksheet.Cells[row, 1].Text);
                    deviceLog.IdEnroll = long.Parse(worksheet.Cells[row, 2].Text);
                    deviceLog.PunchDate = DateTime.Parse(worksheet.Cells[row, 3].Text);
                    deviceLog.IsProcessed = worksheet.Cells[row, 4].Text.ToLower() == "yes" ? true : false;
                    deviceLog.FetchedDate = DateTime.Parse(worksheet.Cells[row, 5].Text);
                    deviceLogs.Add(deviceLog);
                }
            }
            return deviceLogs;
        }
        public async Task<bool> SaveVerifyManualAttendance(long? IDHREmployee, DateTime Punchdate, long? IdHRCompany)
        {
            try
            {
                int DeviceNumber = this._unitOfWork.Db.Set<HRDevice>().Where(x => x.IdHRCompany == IdHRCompany).Select(x => x.DeviceNumber).FirstOrDefault();
                long? IdEnroll = this._unitOfWork.Db.Set<HREmployee>().Where(x => x.Id == IDHREmployee).Select(x => x.IdEnroll).FirstOrDefault();


                DataTable dt = new DataTable("DevicelogsTypeTable");
                dt.Columns.Add("Id", typeof(Guid)).AllowDBNull = true;
                dt.Columns.Add("DeviceNumber", typeof(int)).AllowDBNull = false;
                dt.Columns.Add("IdEnroll", typeof(long)).AllowDBNull = false;
                dt.Columns.Add("PunchDate", typeof(DateTime)).AllowDBNull = false;
                dt.Columns.Add("IsProcessed", typeof(bool)).AllowDBNull = false;
                dt.Columns.Add("FetchedDate", typeof(DateTime)).AllowDBNull = true;

                dt.Rows.Add(Guid.NewGuid(), DeviceNumber, IdEnroll, Punchdate, 0, DateTime.Now);
                var parameter = new SqlParameter("@p_devicelogs", SqlDbType.Structured);
                parameter.TypeName = "dbo.DevicelogsTypeTable";
                parameter.Value = dt;
                _unitOfWork.Db.Database.ExecuteSqlCommand("EXEC Proc_SaveDevicelogs @p_devicelogs", parameter);
                return true;
            }

            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> SaveBulkManualAttendance(List<DeviceLogsModel> devicelogs)
        {
            try
            {
                DataTable dt = new DataTable("DevicelogsTypeTable");
                dt.Columns.Add("Id", typeof(Guid)).AllowDBNull = true;
                dt.Columns.Add("DeviceNumber", typeof(int)).AllowDBNull = false;
                dt.Columns.Add("IdEnroll", typeof(long)).AllowDBNull = false;
                dt.Columns.Add("PunchDate", typeof(DateTime)).AllowDBNull = false;
                dt.Columns.Add("IsProcessed", typeof(bool)).AllowDBNull = false;
                dt.Columns.Add("FetchedDate", typeof(DateTime)).AllowDBNull = true;
                foreach (var devicelog in devicelogs)
                {
                    dt.Rows.Add(devicelog.Id, devicelog.DeviceNumber, devicelog.IdEnroll, devicelog.PunchDate, devicelog.IsProcessed, DateTime.Now);
                }
                var parameter = new SqlParameter("@p_devicelogs", SqlDbType.Structured);
                parameter.TypeName = "dbo.DevicelogsTypeTable";
                parameter.Value = dt;
                _unitOfWork.Db.Database.ExecuteSqlCommand("EXEC Proc_SaveDevicelogs @p_devicelogs", parameter);
                return true;
            }

            catch (Exception ex)
            {
                return false;
            }
        }

        public long GetCurrentActiveJob(long IdHRCompany,long IdHREmployee)
        {

            var Idjob = this._unitOfWork.Db.Set<HREmployeeJobHistory>().Where(x => x.IdHREmployee == IdHREmployee && x.IdHRCompany==IdHRCompany && x.IsCurrentStatus).Select(x => x.Id).FirstOrDefault();
            return Idjob;
        }

        public  IQueryable<long> CheckExistanceKaaj(long IdHRCompany,DateTime? Fromdate,DateTime? Todate)
        {

               return this._unitOfWork.Db.Set<HREmployeeKaajHistory>().Where(x => x.IdHrCompany == IdHRCompany &&
                (Fromdate >=x.KaajFromDate && Fromdate <= x.KaajToDate)
                ||
               (Todate >= x.KaajFromDate && Todate <= x.KaajToDate))
               .Select(x => x.IdHREmployee).ToList().AsQueryable();
        }

        public IQueryable<long> CheckExistanceLeave(long IdHRCompany, DateTime? Fromdate, DateTime? Todate)
        {

            return this._unitOfWork.Db.Set<HREmployeeLeaveHistory>().Where(x => x.IdHRCompany == IdHRCompany &&
              (Fromdate >= x.LeaveValidFrom && Fromdate <= x.LeaveValidTo)
                ||
               (Todate >= x.LeaveValidFrom && Todate <= x.LeaveValidTo)).Select(x => x.IdHREmployee).ToList().AsQueryable();



        }


        public bool checkingData(string loginuserName)
        {
            try
            {
                var EmployeeId = Getidhremployee(loginuserName);
                var FirstLoginExists = true;
                var Role = 3;
                if (EmployeeId != null)
                {
                     FirstLoginExists = this._unitOfWork.Db.Set<SystemDatabase.LoginLogs>().Any(x => x.IdHREmployee == EmployeeId);
                     Role = MasterRole(EmployeeId);
                }

                if (FirstLoginExists)
                {
                    return true;
                }
                else
                {
                    if (Role > 0)
                    {
                        using (EAttendanceSystemDBEntities db = new EAttendanceSystemDBEntities())
                        {
                            LoginLogs logs = new LoginLogs();
                            logs.IdHREmployee = EmployeeId ?? 0;
                            db.LoginLogs.Add(logs);
                            db.SaveChanges();

                        }
                        return false;
                    }
                    else
                    {
                        return true;
                    }

                }
            }

            catch (Exception ex)
            {
                return false;
            }
        }


        public List<long> GetCurrentActiveJobHolder(long? IdHRCompany)
        {

            var TemporaryEmployeeList = this._unitOfWork.Db.Set<HREmployeeJobHistory>().Where(x =>x.IdHRCompany == IdHRCompany && x.IsCurrentStatus && x.IdHREmployeePositionStatusType!=1).Select(x => x.IdHREmployee).ToList();
            return TemporaryEmployeeList;
        }


        public async Task<KeyValuePairModel<DateTime, DateTime>> GetStartEndDateNew(int year, int month)
        {
            var day = await _unitOfWork.Db.Set<HRCalendar>().Where(x => x.NepYear == year && x.NepMonth == month).OrderBy(x => x.NepDay).ToListAsync();
            var Data=new KeyValuePairModel<DateTime, DateTime> { Key = day.FirstOrDefault().EngDate, Value = day.LastOrDefault().EngDate };
            return Data;
        }


        //new 

        public long? GetParentCompanyId(long? IdHRCompany)
        {
            var IdParentCompany = this._unitOfWork.Db.Set<HRCompany>().Where(x => x.Id == IdHRCompany).Select(x => x.IdParent_HRCompany).FirstOrDefault();
            return IdParentCompany;
        }

        public virtual async Task<string> GetCompanyName(long? IdHRCompany)
        {
            var model = await _unitOfWork.Db.Set<HRCompany>().FirstOrDefaultAsync(x => x.Id == IdHRCompany);
            if (IdHRCompany == 0)
            {
                var datamodel = string.Empty;
                return datamodel;
            }

            return model.CompanyName;
        }

        public virtual async Task<string> GetCompanyNameNep(long? IdHRCompany)
        {
            var model = await _unitOfWork.Db.Set<HRCompany>().FirstOrDefaultAsync(x => x.Id == IdHRCompany);
            if (IdHRCompany == 0)
            {
                var datamodel = string.Empty;
                return datamodel;
            }
            return model.CompanyNameNP;
        }

        public virtual async Task<CompanyInformationModel> GetCompanyHeaderDetails(long? IdHRCompany)
        {
            CompanyInformationModel info = new CompanyInformationModel();
            //invalid
            if (IdHRCompany == 0)
            {
                info = new CompanyInformationModel
                {
                    CompanyName = string.Empty,
                    CompanyNameNP = string.Empty,
                    ParentCompanyName = string.Empty,
                    ParentCompanyNameNP = string.Empty

                };
                return info;
            }
            var Own = await _unitOfWork.Db.Set<HRCompany>().FirstOrDefaultAsync(x => x.Id == IdHRCompany);

            //parent is null  OR parent is itself OR parent is set 0
            if (Own.IdParent_HRCompany == IdHRCompany || Own.IdParent_HRCompany==null || Own.IdParent_HRCompany==0)
            {
                info = new CompanyInformationModel
                {
                    CompanyName = Own.CompanyName,
                    CompanyNameNP = Own.CompanyNameNP,
                    ParentCompanyName = string.Empty,
                    ParentCompanyNameNP = string.Empty

                };
                return info;
            }
            
            
            //company has different parent company name 
            var Parent = await _unitOfWork.Db.Set<HRCompany>().FirstOrDefaultAsync(x => x.Id == Own.IdParent_HRCompany);

            info = new CompanyInformationModel
            {
                CompanyName = Own.CompanyName,
                CompanyNameNP = Own.CompanyNameNP,
                ParentCompanyName = Parent.CompanyName,
                ParentCompanyNameNP = Parent.CompanyNameNP

            };
            return info;
        }
        public string GetEmployeeNameById(long? IdHRemployee)
        {
            var model = _unitOfWork.Db.Set<HREmployee>().Where(x => x.Id == IdHRemployee);
            return model.FirstOrDefault().HREmployeeNameNP;
        }
        public string GetEmployeeCurrentPostById(long? IdHRemployee)
        {
            var JobHistory = _unitOfWork.Db.Set<HREmployeeJobHistory>().Where(x => x.IdHREmployee == IdHRemployee && x.IsCurrentStatus == true);
            var IdDesignation = JobHistory.FirstOrDefault().IdHRDesignation;
            var Designation = _unitOfWork.Db.Set<HRDesignation>().Where(x => x.Id == IdDesignation);
            return Designation.FirstOrDefault().HRDesignationTitle;

        }



        public virtual async Task<string> GetDivisionNameNep(long? idHRCompanyDivision)
        {
            if (idHRCompanyDivision == 0)
            {
                return "सबै";
            }
            return await _unitOfWork.Db.Set<HRCompanyDivision>().Where(x => x.Id == idHRCompanyDivision).Select(x=>x.HRCompanyDivisionName).FirstOrDefaultAsync();
        }



        public int NormalRole(long IdHRCompany)
        {
            var RoleId = _unitOfWork.Db.Set<HREmployeeRole>().Where(x => x.IdHRCompany == IdHRCompany && x.IdRoleType == 3).FirstOrDefault().Id;
            return RoleId;
        }



        public async Task<bool> NewUpdateRole(long? IDHREmployee, int RoleId)
        {
            try
            {

               
                object[] obj =
               {
                    new SqlParameter() {ParameterName = "@IDHREmployee", SqlDbType = SqlDbType.BigInt, Value= IDHREmployee},
                    new SqlParameter() {ParameterName = "@RoleId", SqlDbType = SqlDbType.BigInt, Value = RoleId}
                };


                string Query = "Update MembershipProvider set IdHREmployeeRole="+ @RoleId + " where IdHREmployee= " + @IDHREmployee;
                _unitOfWork.Db.Database.ExecuteSqlCommand(Query, obj);
                return true;
            }

            catch (Exception ex)
            {
                return false;
            }
        }

    }
}


