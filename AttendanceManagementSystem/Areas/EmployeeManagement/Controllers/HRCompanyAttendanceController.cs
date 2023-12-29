using AttendanceManagementSystem.App_Auth;
using AttendanceManagementSystem.Controllers;
using DeviceManagement.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SystemDatabase;
using SystemModels.CompanyManagement;
using SystemServices.CompanyManagement;
using SystemViewModels.CompanyManagement;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;

namespace AttendanceManagementSystem.Areas.EmployeeManagement.Controllers
{
    public class HRCompanyAttendanceController : BaseController
    {
        // GET: EmployeeManagement/HRCompanyAttendance

        private readonly HRCompanyAttendanceServices _HRCompanyAttendanceServices;


        public HRCompanyAttendanceController()
        {
            _HRCompanyAttendanceServices = new HRCompanyAttendanceServices(this._unitOfWork);
        }
        #region ManualAttendance

        [NonAction]
        public Expression<Func<HRCompanyManualAttendance, bool>> Condition(long? idHREmployee, string searchKey = "")
        {
            Expression<Func<HRCompanyManualAttendance, bool>> retutndata = (x => false);

            if (this.SessionDetail.IdRoleType == 0)
            {
                retutndata = (x => x.IdApprovedBy == idHREmployee && x.IdAttendanceStatus == 1 && (x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));

            }
            else if (this.SessionDetail.IdRoleType == 1 || SessionDetail.IdRoleType == 2 || SessionDetail.IdRoleType == 3|| SessionDetail.IdRoleType == 4)
            {
                retutndata = (x => x.IdApprovedBy == idHREmployee && x.IdAttendanceStatus == 1 && (x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));

            }

            else
            {
                retutndata = (x => false);
            }
            var a = retutndata;
            return a;
        }



        //[AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListWrapperHRCompanyAttendanceAsync(long? idHREmployee, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (idHREmployee == null || idHREmployee == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, "Id", orderingDirection);

                return PartialView(new HRCompanyAttendanceViewModellist
                {
                    SessionDetails = SessionDetail,
                    DBModelList = await _HRCompanyAttendanceServices.GetPagedList(Condition(idHREmployee, searchKey), pageNumber, pageSize, orderingBy, orderingDirection),
                    BreadCrumbTitle = "कार्यालय व्यवस्थापन",
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HRCompanyAttendance",
                    BreadCrumbActionName = "_ListWrapperHRCompanyAttendanceAsync",
                    BreadCrumbBaseURL = "EmployeeManagement/HRCompanyAttendance",
                    CRUDAction = CRUDType.READ,
                    HeaderTitle = "Attendance System Management",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    IdHRCompany = SessionDetail.IdHRCompany,
                    IdHREmployee = idHREmployee,
                });
            }
            catch (Exception exp)
            {
                throw new ArgumentException(exp.Message);
            }
        }


        //[AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListHRCompanyAttendanceAsync(long? idHREmployee, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                if (idHREmployee == null || idHREmployee == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRCompanyAttendanceViewModellist
                {
                    SessionDetails = SessionDetail,
                    DBModelList = await _HRCompanyAttendanceServices.GetPagedList(Condition(idHREmployee, searchKey), pageNumber, pageSize, orderingBy, orderingDirection),
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HRCompanyAttendance",
                    BreadCrumbBaseURL = "EmployeeManagement/HRCompanyAttendance",
                    BreadCrumbActionName = "_ListHRCompanyAttendanceAsync",
                    BreadCrumbTitle = "HRCompanyAttendance",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    IdHRCompany = SessionDetail.IdHRCompany,
                    IdHREmployee = idHREmployee
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        //[AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadHRCompanyAttendanceAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var DataModel = await _HRCompanyAttendanceServices.GetModelByIdAsync(id);
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRCompanyAttendanceViewModel
                {
                    SessionDetails = SessionDetail,
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HRCompanyAttendance",
                    BreadCrumbBaseURL = "EmployeeManagement/HRCompanyAttendance",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_ReadHRCompanyAttendanceAsync",
                    FormModelName = "HRCompanyAttendance",
                    ModalTitle = "हाजिरीको विस्तार",
                    BreadCrumbTitle = "Company Manual Attendance",
                    CRUDAction = CRUDType.READ,
                    OnlyCancelButton = true,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = DataModel,
                    IdHRCompany = DataModel.IdHrCompany,
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId, DataModel.IdHrCompany),
                    DDLEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeAttendanceNP, SessionDetail.IdHRCompany, DataModel.IdHREmployee),
                    DDLApprovalEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeApproved, SessionDetail.IdHRCompany, SessionDetail.IdHREmployee),
                    DDLLeaveStatusType = await GetIntStringPairModel(PairModelTitle.LeaveApprovalStatus),

                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }


        //[AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _UpdateHRCompanyAttendanceAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                long? idHrCompany;
                idHrCompany = this.SessionDetail.IdHRCompany;
                long? idHrEmployee;

                //var fullpath = "ManualAttendanceDocuments\\" + DataModel.FolderName + "\\" + DataModel.Document;

                idHrEmployee = this.SessionDetail.IdHREmployee;
                var DataModel = await _HRCompanyAttendanceServices.GetModelByIdAsync(id);
                string dirPath = Server.MapPath("~") + "Upload\\ManualAttendanceDocuments\\" + DataModel.FolderName + "\\" + DataModel.FileName;

                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRCompanyAttendanceViewModel
                {
                    SessionDetails = SessionDetail,
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HRCompanyAttendance",
                    BreadCrumbBaseURL = "EmployeeManagement/HRCompanyAttendance",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_UpdateHRCompanyAttendanceAsync",
                    FormModelName = "HRCompanyAttendance",
                    ModalTitle = "हाजिरी सम्पादन गर्नुहोस्",
                    BreadCrumbTitle = "HRCompanyAttendance",
                    CRUDAction = CRUDType.UPDATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Update,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId, DataModel.IdHrCompany),
                    DDLEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeAttendanceNP, SessionDetail.IdHRCompany, DataModel.IdHREmployee),
                    DDLApprovalEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeApproved, SessionDetail.IdHRCompany, idHrEmployee),
                    DDLLeaveStatusType = await GetIntStringPairModel(PairModelTitle.LeaveApprovalStatus),
                    DataModel = DataModel,
                    files = dirPath,
                    IdHRCompany = DataModel.IdHrCompany
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
        public async Task<ActionResult> _UpdateHRCompanyAttendanceAsync(HRCompanyAttendanceViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHRCompanyAttendanceAsync(viewModel, CRUDType.UPDATE, formCollection);
        }



        [NonAction]
        protected async Task<ActionResult> RETSourceHRCompanyAttendanceAsync(HRCompanyAttendanceViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
                viewModel.SessionDetails = SessionDetail;
                viewModel.HeaderTitle = "Attendance System Management";
                viewModel.BreadCrumbArea = "EmployeeManagement";
                viewModel.BreadCrumbController = "HRCompanyAttendance";
                viewModel.BreadCrumbBaseURL = "EmployeeManagement/HRCompanyAttendance";
                viewModel.DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId, viewModel.DataModel.IdHrCompany);
                viewModel.DDLEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeAttendanceNP, viewModel.DataModel.IdHrCompany, viewModel.DataModel.IdHREmployee);
                viewModel.DDLApprovalEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeApproved, SessionDetail.IdHRCompany, SessionDetail.IdHREmployee);
                viewModel.DDLLeaveStatusType = await GetIntStringPairModel(PairModelTitle.LeaveApprovalStatus);

                switch (cRUDType)
                {

                    case CRUDType.UPDATE:
                        viewModel.BreadCrumbActionName = "_UpdateHRCompanyAttendanceAsync";
                        viewModel.ModalTitle = " सम्पादन गर्नुहोस्";
                        viewModel.FormModelName = "HRCompanyAttendance";
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
        protected async Task<ActionResult> CRUDHRCompanyAttendanceAsync(HRCompanyAttendanceViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {

                if (viewModel.DataModel.IdAttendanceStatus == 3)
                {
                    DateTime PunchDate = DateTime.Now;
                    var date = viewModel.DataModel.PunchDate.Date;
                    var time = TimeSpan.Parse(viewModel.DataModel.PunchTime.ToString());
                    PunchDate = date.Add(time);

                    var isSaved = await SaveVerifyManualAttendance(viewModel.DataModel.IdHREmployee, PunchDate, viewModel.DataModel.IdHrCompany);
                    if (!isSaved)
                    {
                        return Redirect("index");
                    }
                    viewModel.DataModel.ApprovedOn = DateTime.Now;
                }
                await _HRCompanyAttendanceServices.CommitSaveChangesAsync(viewModel.DataModel, cRUDType);
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceHRCompanyAttendanceAsync(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListHRCompanyAttendanceAsync", new { idHREmployee = viewModel.DataModel.IdHREmployee, pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }

        [NonAction]
        public FileResult File(string id)
        {
            string dirPath = Server.MapPath("~") + id;
            string result = Path.GetFileName(dirPath);
            byte[] fileBytes = System.IO.File.ReadAllBytes(dirPath);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, result);
        }

        public FileResult DownloadFile(string FolderName, string FileName)
        {
            string dirPath = Server.MapPath("~") + "Upload\\ManualAttendanceDocuments\\" + FolderName + "\\" + FileName;
            string result = Path.GetFileName(dirPath);
            byte[] fileBytes = System.IO.File.ReadAllBytes(dirPath);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, result);
        }

        #endregion

    }
}