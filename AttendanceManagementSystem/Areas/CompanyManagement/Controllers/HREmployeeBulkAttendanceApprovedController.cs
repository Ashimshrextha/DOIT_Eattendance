using AttendanceManagementSystem.App_Auth;
using AttendanceManagementSystem.Controllers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using SystemDatabase;
using SystemModels.CompanyManagement;
using SystemServices.SystemSetting;
using SystemViewModels.CompanyManagement;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;
namespace AttendanceManagementSystem.Areas.CompanyManagement.Controllers
{
    public class HREmployeeBulkAttendanceApprovedController : BaseController
    {
        private readonly HREmployeeBulkAttendanceApprovedServices _hrEmployeeBulkAttendanceServices;

        public HREmployeeBulkAttendanceApprovedController()
        {
            this._hrEmployeeBulkAttendanceServices = new HREmployeeBulkAttendanceApprovedServices(this._unitOfWork);
        }
        #region HREmployee HREmployeeBulkAttendanceApproved
        [NonAction]
        public Expression<Func<HREmployeeManualAttendance, bool>> Condition(long idHREmployee, string searchKey = "")
        {
            Expression<Func<HREmployeeManualAttendance, bool>> retutndata = (x => false);
            if (this.SessionDetail.IdRoleType == 0) retutndata = (x => x.IdReuestId == idHREmployee && (x.FileName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
            else if (this.SessionDetail.IdRoleType == 1 || this.SessionDetail.IdRoleType == 2) retutndata = (x => x.IdReuestId == idHREmployee && x.IdHRCompany == SessionDetail.IdHRCompany && (x.FileName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
            else retutndata = (x => x.IdReuestId == this.SessionDetail.IdHREmployee && (x.FileName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
            return retutndata;
        }
        //[AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListWrapperHREmployeeBulkAttendanceApprovedAsync(long idHREmployee, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                if (idHREmployee == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HREmployeeBulkAttendanceViewModelList
                {
                    IdHREmployee = idHREmployee,
                    SessionDetails = SessionDetail,
                    //DBModelList = await _hrEmployeeBulkAttendanceServices.GetPagedLists(Condition(idHREmployee, searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    DBModelManualList = await _hrEmployeeBulkAttendanceServices.GetPagedList(this.SessionDetail.IdHRCompany, this.SessionDetail.IdHREmployee, this.SessionDetail.IdRoleType, 0, pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, ""),
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HREmployeeBulkAttendanceApproved",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeBulkAttendanceApproved",
                    BreadCrumbActionName = "_ListHREmployeeBulkAttendanceApprovedAsync",
                    BreadCrumbTitle = "Employee Bulk Attendance Approved",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection
                    //DDLRecommendEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeRecommended, this.SessionDetail.IdHRCompany, idHREmployee),
                    //DDLApprovalEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeApproved, this.SessionDetail.IdHRCompany, idHREmployee),

                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        //[AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListHREmployeeBulkAttendanceApprovedAsync(long idHREmployee, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                if (idHREmployee == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HREmployeeBulkAttendanceViewModelList
                {
                    IdHREmployee = idHREmployee,
                    SessionDetails = SessionDetail,
                    //DBModelList = await _hrEmployeeBulkAttendanceServices.GetPagedList(Condition(idHREmployee, searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    DBModelManualList = await _hrEmployeeBulkAttendanceServices.GetPagedList(this.SessionDetail.IdHRCompany, this.SessionDetail.IdHREmployee, this.SessionDetail.IdRoleType, 0, pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, ""),
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HREmployeeBulkAttendanceApproved",
                    BreadCrumbBaseURL = "CompanyManagement/HREmployeeBulkAttendanceApprovedAsync",
                    BreadCrumbActionName = "_ListHREmployeeBulkAttendanceApprovedAsync",
                    BreadCrumbTitle = "Employee Bulk Attendance Approved",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection
                    //DDLRecommendEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeRecommended, this.SessionDetail.IdHRCompany, idHREmployee),
                    //DDLApprovalEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeApproved, this.SessionDetail.IdHRCompany, idHREmployee),

                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        //[AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _CreateHREmployeeBulkAttendanceApprovedAsync(int idHREmployee)
        {
            try
            {
                if (idHREmployee == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                return PartialView(new HREmployeeBulkAttendanceViewModel
                {
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HREmployeeBulkAttendanceApproved",
                    BreadCrumbBaseURL = "CompanyManagement/HREmployeeBulkAttendance",
                    BreadCrumbActionName = "_CreateHREmployeeBulkAttendanceAsync",
                    FormModelName = "EmployeeBulkAttendanceApproved",
                    ModalTitle = "कर्मचारीको हाजिरी अपलोड गर्नुहोस",
                    BreadCrumbTitle = "Employee Bulk Attendance",
                    CRUDAction = CRUDType.CREATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Create,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    IdHREmployee = idHREmployee,
                    DataModel = new HREmployeeManualAttendanceModel { Id = 0, IdHREmployee = idHREmployee },
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        //[AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _CreateHREmployeeBulkAttendanceApprovedAsync(HREmployeeBulkAttendanceViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeBulkAttendanceApprovedAsync(viewModel, CRUDType.CREATE, formCollection);
        }
        //[AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadHREmployeeBulkAttendanceApprovedAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _hrEmployeeBulkAttendanceServices.GetModelByIdAsync(id);
                return PartialView(new HREmployeeBulkAttendanceViewModel
                {
                    SessionDetails = SessionDetail,
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HREmployeeBulkAttendanceApproved",
                    BreadCrumbBaseURL = "CompanyManagement/HREmployeeBulkAttendanceApproved",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_ReadHREmployeeBulkAttendanceApprovedAsync",
                    FormModelName = "EmployeeBulkAttendanceApproved",
                    ModalTitle = "कर्मचारीको हाजिरी अपलोड विवरण हेर्नुहोस्",
                    BreadCrumbTitle = "Employee Bulk Attendance Approved",
                    CRUDAction = CRUDType.READ,
                    OnlyCancelButton = true,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = model,
                    IdHREmployee = model.IdHREmployee,

                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        //[AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _UpdateHREmployeeBulkAttendanceApprovedAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _hrEmployeeBulkAttendanceServices.GetModelByIdAsync(id);
                return PartialView(new HREmployeeBulkAttendanceViewModel
                {
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HREmployeeBulkAttendanceApproved",
                    BreadCrumbBaseURL = "CompanyManagement/HREmployeeBulkAttendanceApproved",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_UpdateHREmployeeBulkAttendanceApprovedAsync",
                    FormModelName = "HREmployeeBulkAttendanceApproved",
                    ModalTitle = "कर्मचारीको हाजिरी अपलोड सम्पादन गर्नुहोस्",
                    BreadCrumbTitle = "Employee Bulk Attendance Approved",
                    CRUDAction = CRUDType.UPDATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Update,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = model,
                    IdHREmployee = model.IdHREmployee,
                    DDLRecommendEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeRecommended, this.SessionDetail.IdHRCompany, id),
                    //DDLApprovalEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeApproved, this.SessionDetail.IdHRCompany, id),
                    DDLApprovalEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeMannualAttendanceApproved, this.SessionDetail.IdHRCompany, id),
                    //DDLRecommendEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeMannualAttendanceRecommended, this.SessionDetail.IdHRCompany, id),

                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        //[AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _UpdateHREmployeeBulkAttendanceApprovedAsync(HREmployeeBulkAttendanceViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeBulkAttendanceApprovedAsync(viewModel, CRUDType.UPDATE, formCollection);
        }

        //[AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _DeleteHREmployeeBulkAttendanceApprovedAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _hrEmployeeBulkAttendanceServices.GetModelByIdAsync(id);
                return PartialView(new HREmployeeBulkAttendanceViewModel
                {
                    SessionDetails = SessionDetail,
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HREmployeeBulkAttendanceApproved",
                    BreadCrumbBaseURL = "CompanyManagement/HREmployeeBulkAttendanceApproved",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    FormModelName = "HREmployeeBulkAttendanceApproved",
                    ModalTitle = "कर्मचारीको हाजिरी अपलोड विवरण मेटाउनुहोस्",
                    BreadCrumbTitle = "Employee Bulk Attendance Approved",
                    BreadCrumbActionName = "_DeleteHREmployeeBulkAttendanceApprovedAsync",
                    CRUDAction = CRUDType.DELETE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Delete,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLContactType = await GetLongStringPairModel(PairModelTitle.ContactType),
                    DataModel = model,
                    IdHREmployee = model.IdHREmployee
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        //[AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _DeleteHREmployeeBulkAttendanceApprovedAsync(HREmployeeBulkAttendanceViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeBulkAttendanceApprovedAsync(viewModel, CRUDType.DELETE, formCollection);
        }

        //[AuthorizeUser]
        //[AcceptVerbs(HttpVerbs.Get)]
        //public async Task<ActionResult> _ExportHREmployeeBulkAttendanceApprovedAsync(long idHREmployee)
        //{
        //    try
        //    {
        //        if (idHREmployee == 0)
        //        {
        //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //        }
        //        var model = await _hrEmployeeBulkAttendanceServices.FindAllAsync(Condition(idHREmployee));
        //        var data = model.Select(x => new
        //        {
        //            id = x.Id,

        //            EmployeeName = x.HREmployee.HREmployeeNameNP,
        //            Reason = x.Reason,
        //            Remarks = x.Remarks,
        //            Document = x.Document,
        //            Title = x.Title,
        //        });

        //        return await ExportToExcel("EmployeeBulkAttendance", data);
        //    }
        //    catch (Exception exp)
        //    {
        //        return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
        //    }
        //}

        //[AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintHREmployeeBulkAttendanceApprovedAsync(long idHREmployee)
        {
            try
            {
                return PartialView(new HREmployeeBulkAttendanceViewModel
                {
                    SessionDetails = SessionDetail,
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HREmployeeBulkAttendanceApproved",
                    BreadCrumbBaseURL = "CompanyManagement/HREmployeeBulkAttendanceApproved",
                    BreadCrumbActionName = "_PrintHREmployeeBulkAttendanceApprovedAsync",
                    FormModelName = "HREmployeeBulkAttendanceApproved",
                    ModalTitle = "कर्मचारीको हाजिरी अपलोड छाप्नुहोस्",
                    BreadCrumbTitle = "Employee Bulk Attendance Approved",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBModelList = await _hrEmployeeBulkAttendanceServices.FindAllAsync(Condition(idHREmployee)),
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }
        [NonAction]
        protected async Task<ActionResult> RETSourceHREmployeeBulkAttendanceApprovedAsync(HREmployeeBulkAttendanceViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
                viewModel.SessionDetails = SessionDetail;
                viewModel.BreadCrumbArea = "CompanyManagement";
                viewModel.BreadCrumbController = "HREmployeeBulkAttendanceApproved";
                viewModel.BreadCrumbBaseURL = "CompanyManagement/HREmployeeBulkAttendanceApproved";
                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        viewModel.SubmitButtonText = SubmitButtonText.Create;
                        viewModel.BreadCrumbActionName = "Create_HREmployeeBulkAttendanceApproved";
                        viewModel.ModalTitle = "कर्मचारीको हाजिरी अपलोड थप्नुहोस्";
                        viewModel.FormModelName = "HREmployeeBulkAttendance";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.UPDATE:
                        viewModel.BreadCrumbActionName = "_UpdateHREmployeeBulkAttendanceApprovedAsync";
                        viewModel.ModalTitle = "कर्मचारीको हाजिरी अपलोड सम्पादन गर्नुहोस्";
                        viewModel.FormModelName = "HREmployeeBulkAttendance";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.DELETE:
                        viewModel.BreadCrumbActionName = "_DeleteHREmployeeBulkAttendanceApprovedAsync";
                        viewModel.ModalTitle = "कर्मचारीको हाजिरी अपलोड विवरण मेटाउनुहोस्";
                        viewModel.FormModelName = "HREmployeeBulkAttendance";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.PRINT:
                        viewModel.BreadCrumbActionName = "_PrintHREmployeeBulkAttendanceApprovedAsync";
                        viewModel.ModalTitle = "कर्मचारीको हाजिरी अपलोड छाप्नुहोस्";
                        viewModel.FormModelName = "HREmployeeBulkAttendanceApproved";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                }
                return null;
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [NonAction]
        protected async Task<ActionResult> CRUDHREmployeeBulkAttendanceApprovedAsync(HREmployeeBulkAttendanceViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                var DocumentName = "";
                var FolderName = "";
                var Path = "";
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceHREmployeeBulkAttendanceApprovedAsync(viewModel, cRUDType);
                }
                if (viewModel.FileToUpload != null)
                {
                    var SubFolderName = viewModel.DataModel.IdHREmployee.ToString();
                    if (viewModel.FileToUpload != null)
                    {
                        FolderName = "EmployeeBulkAttendance\\" + SubFolderName + "\\";
                        DocumentName = FileUpload(viewModel.FileToUpload, FolderName);
                        Path = Server.MapPath("~/Upload/" + FolderName + "" + DocumentName);
                    }
                    viewModel.DataModel.FolderName = SubFolderName;
                }
                long id = await _hrEmployeeBulkAttendanceServices.CommitSaveChangesAsync(viewModel.DataModel, cRUDType);
                #region Data Import Bulk to Excel Prem Prakash Dhakal
                string excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                OleDbConnection connExcel = new OleDbConnection(excelConnectionString);
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                DataTable dtExcel = new DataTable();
                cmdExcel.Connection = connExcel;
                connExcel.Open();
                DataTable dtExcelSchema;
                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                connExcel.Close();
                OleDbCommand cmd = new OleDbCommand("Select * from [" + SheetName + "]", connExcel);
                connExcel.Open();
                OleDbDataReader dReader;
                dReader = cmd.ExecuteReader();
                //Check Data for data reader 
                while (dReader.Read())
                {
                    string DeviceNumber = dReader["DeviceNumber"].ToString();
                    string IdEnroll = dReader["IdEnroll"].ToString();
                    string PunchDate = dReader["PunchDate"].ToString();
                    string VerificationMode = dReader["VerificationMode"].ToString();
                    string InOutMode = dReader["InOutMode"].ToString();
                    string IsProcessed = dReader["IsProcessed"].ToString();
                    string FetchedDate = dReader["FetchedDate"].ToString();
                    string DeviceModel = dReader["DeviceModel"].ToString();
                    string TerminalIP = dReader["TerminalIP"].ToString();
                }
                string sqlcontring = ConfigurationManager.ConnectionStrings["EAttendanceSystemDBDATA"].ToString();
                SqlConnection sqlcon = new SqlConnection(sqlcontring);
                sqlcon.Open();
                SqlBulkCopy sqlBulk = new SqlBulkCopy(sqlcon);
                sqlBulk.DestinationTableName = "DeviceLogs";
                sqlBulk.ColumnMappings.Add("DeviceNumber", "DeviceNumber");
                sqlBulk.ColumnMappings.Add("IdEnroll", "IdEnroll");
                sqlBulk.ColumnMappings.Add("PunchDate", "PunchDate");
                sqlBulk.ColumnMappings.Add("VerificationMode", "VerificationMode");
                sqlBulk.ColumnMappings.Add("InOutMode", "InOutMode");
                sqlBulk.ColumnMappings.Add("IsProcessed", "IsProcessed");
                sqlBulk.ColumnMappings.Add("FetchedDate", "FetchedDate");
                sqlBulk.ColumnMappings.Add("DeviceModel", "DeviceModel");
                sqlBulk.ColumnMappings.Add("TerminalIP", "TerminalIP");
                sqlBulk.WriteToServer(dReader);
                connExcel.Close();
                #endregion
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceHREmployeeBulkAttendanceApprovedAsync(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListHREmployeeBulkAttendanceApprovedAsync", new { idHREmployee = viewModel.DataModel.IdHREmployee, pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }

        public FileResult File(string id)
        {
            string dirPath = Server.MapPath("~") + id;
            string result = Path.GetFileName(dirPath);
            byte[] fileBytes = System.IO.File.ReadAllBytes(dirPath);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, result);
        }
        public FileResult DownloadFile(string FolderName, string FileName)
        {
            string dirPath = Server.MapPath("~") + "Upload\\EmployeeBulkAttendance\\" + FolderName + "\\" + FileName;
            string result = Path.GetFileName(dirPath);
            byte[] fileBytes = System.IO.File.ReadAllBytes(dirPath);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, result);
        }
    }
    #endregion
}

