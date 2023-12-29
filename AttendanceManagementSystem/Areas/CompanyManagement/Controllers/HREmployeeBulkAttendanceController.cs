using AttendanceManagementSystem.App_Auth;
using AttendanceManagementSystem.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using SystemDatabase;
using SystemModels.CompanyManagement;
using SystemServices.EmployeeManagement;
using SystemServices.SystemSetting;
using SystemViewModels.CompanyManagement;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;
namespace AttendanceManagementSystem.Areas.CompanyManagement.Controllers
{
    public class HREmployeeBulkAttendanceController :  BaseController
    {
        // GET: CompanyManagement/HRCompanyShiftRoaster
        private readonly HREmployeeBulkAttendanceServices _hrEmployeeBulkAttendanceServices;
        public HREmployeeBulkAttendanceController()
        {
            this._hrEmployeeBulkAttendanceServices = new HREmployeeBulkAttendanceServices(this._unitOfWork);
        }
        [NonAction]
        public Expression<Func<HREmployeeManualAttendance, bool>> Condition(long? idHRCompany, string searchKey = "")
        {
            Expression<Func<HREmployeeManualAttendance, bool>> returnData = (x => false);
            switch (this.SessionDetail.IdRoleType)
            {
                case (int)RoleType.SuperUser:
                    return returnData = (x => x.IdHRCompany == idHRCompany); //&& (x.CompanyName.ToUpper().Contains(searchKey.ToUpper().ToString()) || searchKey == ""));
                case (int)RoleType.Admin:
                    return returnData = (x => x.IdHRCompany == idHRCompany);
                case (int)RoleType.RootUser:
                    return returnData = (x => x.IdHRCompany == idHRCompany);
                case (int)RoleType.NormalUser:
                    return returnData = (x => x.IdHRCompany == idHRCompany);
                default:
                    return returnData;
            }
        }
        //[AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListWrapperHREmployeeBulkAttendanceAsync(long idHRCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                if (idHRCompany == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HREmployeeBulkAttendanceViewModelList
                {
                    IdHREmployee = idHRCompany,
                    SessionDetails = SessionDetail,
                    //DBModelList = await _hrEmployeeBulkAttendanceServices.GetPagedLists(Condition(idHRCompany, searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    DBModelManualList = await _hrEmployeeBulkAttendanceServices.GetPagedList(this.SessionDetail.IdHRCompany, this.SessionDetail.IdHREmployee, this.SessionDetail.IdRoleType, 0, pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, ""),
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HREmployeeBulkAttendance",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeBulkAttendance",
                    BreadCrumbActionName = "_ListHREmployeeBulkAttendanceAsync",
                    BreadCrumbTitle = "Employee Bulk Attendance",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        //[AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListHREmployeeBulkAttendanceAsync(long idHRCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                if (idHRCompany == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _hrEmployeeBulkAttendanceServices.GetPagedList(this.SessionDetail.IdHRCompany, this.SessionDetail.IdHREmployee, this.SessionDetail.IdRoleType, 0, pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, "");
                return PartialView(new HREmployeeBulkAttendanceViewModelList
                {
                    IdHRCompany = idHRCompany,
                    SessionDetails = SessionDetail,
                    //DBModelList = await _hrEmployeeBulkAttendanceServices.GetPagedList(Condition(idHRCompany, searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    DBModelManualList= await _hrEmployeeBulkAttendanceServices.GetPagedList(this.SessionDetail.IdHRCompany, this.SessionDetail.IdHREmployee, this.SessionDetail.IdRoleType, 0, pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, ""),
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HREmployeeBulkAttendance",
                    BreadCrumbBaseURL = "CompanyManagement/HREmployeeBulkAttendance",
                    BreadCrumbActionName = "_ListHREmployeeBulkAttendanceAsync",
                    BreadCrumbTitle = "Employee Bulk Attendance",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection
                }); ;
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

       // [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _CreateHREmployeeBulkAttendanceAsync(int idHREmployee)
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
                    BreadCrumbController = "HREmployeeBulkAttendance",
                    BreadCrumbBaseURL = "CompanyManagement/HREmployeeBulkAttendance",
                    BreadCrumbActionName = "_CreateHREmployeeBulkAttendanceAsync",
                    FormModelName = "HREmployeeBulkAttendance",
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
                    DDLApprovalEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeApproved, this.SessionDetail.IdHRCompany, idHREmployee),
                    DDLRecommendEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeRecommended, this.SessionDetail.IdHRCompany, idHREmployee),
                    //DDLLeaveType = await GetLongStringPairModel(PairModelTitle.LeaveType, this.SessionDetail.IdHRCompany, idHREmployee),

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
        public async Task<ActionResult> _CreateHREmployeeBulkAttendanceAsync(HREmployeeBulkAttendanceViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeBulkAttendanceAsync(viewModel, CRUDType.CREATE, formCollection);
        }
        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadHREmployeeBulkAttendanceAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
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
                    BreadCrumbController = "HREmployeeBulkAttendance",
                    BreadCrumbBaseURL = "CompanyManagement/HREmployeeBulkAttendance",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_ReadHREmployeeBulkAttendanceAsync",
                    FormModelName = "EmployeeBulkAttendance",
                    ModalTitle = "कर्मचारीको हाजिरी अपलोड विवरण हेर्नुहोस्",
                    BreadCrumbTitle = "Employee Bulk Attendance",
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

       // [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _UpdateHREmployeeBulkAttendanceAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
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
                    BreadCrumbController = "HREmployeeBulkAttendance",
                    BreadCrumbBaseURL = "CompanyManagement/HREmployeeBulkAttendance",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_UpdateHREmployeeBulkAttendanceAsync",
                    FormModelName = "HREmployeeBulkAttendance",
                    ModalTitle = "कर्मचारीको हाजिरी अपलोड सम्पादन गर्नुहोस्",
                    BreadCrumbTitle = "Employee Bulk Attendance",
                    CRUDAction = CRUDType.UPDATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Update,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = model,
                    IdHREmployee = model.IdHREmployee
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

       // [AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _UpdateHREmployeeBulkAttendanceAsync(HREmployeeBulkAttendanceViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeBulkAttendanceAsync(viewModel, CRUDType.UPDATE, formCollection);
        }

       // [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _DeleteHREmployeeBulkAttendanceAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
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
                    BreadCrumbController = "HREmployeeBulkAttendance",
                    BreadCrumbBaseURL = "CompanyManagement/HREmployeeBulkAttendance",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    FormModelName = "HREmployeeBulkAttendance",
                    ModalTitle = "कर्मचारीको हाजिरी अपलोड विवरण मेटाउनुहोस्",
                    BreadCrumbTitle = "Employee Bulk Attendance",
                    BreadCrumbActionName = "_DeleteHREmployeeBulkAttendanceAsync",
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

       // [AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _DeleteHREmployeeBulkAttendanceAsync(HREmployeeBulkAttendanceViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeBulkAttendanceAsync(viewModel, CRUDType.DELETE, formCollection);
        }

        //[AuthorizeUser]
        //[AcceptVerbs(HttpVerbs.Get)]
        //public async Task<ActionResult> _ExportEmployeeBulkAttendanceAsync(long idHREmployee)
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

      //  [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintHREmployeeBulkAttendanceAsync(long idHREmployee)
        {
            try
            {
                return PartialView(new HREmployeeBulkAttendanceViewModel
                {
                    SessionDetails = SessionDetail,
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HREmployeeBulkAttendance",
                    BreadCrumbBaseURL = "CompanyManagement/HREmployeeBulkAttendance",
                    BreadCrumbActionName = "_PrintHREmployeeBulkAttendanceAsync",
                    FormModelName = "HREmployeeBulkAttendance",
                    ModalTitle = "कर्मचारीको हाजिरी अपलोड छाप्नुहोस्",
                    BreadCrumbTitle = "Employee Bulk Attendance",
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
        protected async Task<ActionResult> RETSourceHREmployeeBulkAttendanceAsync(HREmployeeBulkAttendanceViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
                viewModel.SessionDetails = SessionDetail;
                viewModel.BreadCrumbArea = "CompanyManagement";
                viewModel.BreadCrumbController = "HREmployeeBulkAttendance";
                viewModel.BreadCrumbBaseURL = "CompanyManagement/HREmployeeBulkAttendance";
                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        viewModel.SubmitButtonText = SubmitButtonText.Create;
                        viewModel.BreadCrumbActionName = "_CreateHREmployeeBulkAttendanceAsync";
                        viewModel.ModalTitle = "कर्मचारीको हाजिरी अपलोड थप्नुहोस्";
                        viewModel.FormModelName = "HREmployeeBulkAttendance";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.UPDATE:
                        viewModel.BreadCrumbActionName = "_UpdateHREmployeeBulkAttendanceAsync";
                        viewModel.ModalTitle = "कर्मचारीको हाजिरी अपलोड सम्पादन गर्नुहोस्";
                        viewModel.FormModelName = "HREmployeeBulkAttendance";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.DELETE:
                        viewModel.BreadCrumbActionName = "_DeleteHREmployeeBulkAttendanceAsync";
                        viewModel.ModalTitle = "कर्मचारीको हाजिरी अपलोड विवरण मेटाउनुहोस्";
                        viewModel.FormModelName = "HREmployeeBulkAttendance";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.PRINT:
                        viewModel.BreadCrumbActionName = "_PrintHREmployeeBulkAttendanceAsync";
                        viewModel.ModalTitle = "कर्मचारीको हाजिरी अपलोड छाप्नुहोस्";
                        viewModel.FormModelName = "HREmployeeBulkAttendance";
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
        protected async Task<ActionResult> CRUDHREmployeeBulkAttendanceAsync(HREmployeeBulkAttendanceViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceHREmployeeBulkAttendanceAsync(viewModel, cRUDType);
                }
                if (viewModel.FileToUpload != null)
                {

                    var SubFolderName = viewModel.DataModel.IdHREmployee.ToString();

                    if (viewModel.FileToUpload != null)
                    {
                        var FolderName = "EmployeeBulkAttendance\\" + SubFolderName + "\\";
                        var DocumentName = FileUpload(viewModel.FileToUpload, FolderName);
                        //viewModel.DataModel.FileName = DocumentName;
                    }
                    viewModel.DataModel.FolderName = SubFolderName;
                }
                viewModel.DataModel.IdHRCompany =Convert.ToInt32(SessionDetail.IdHRCompany);
                viewModel.DataModel.Status = "1";
               long id = await _hrEmployeeBulkAttendanceServices.CommitSaveChangesAsync(viewModel.DataModel, cRUDType);
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceHREmployeeBulkAttendanceAsync(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListHREmployeeBulkAttendanceAsync", new { idHRCompany = viewModel.DataModel.IdHRCompany, pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
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
}
