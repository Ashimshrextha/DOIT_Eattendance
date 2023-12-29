using AttendanceManagementSystem.App_Auth;
using AttendanceManagementSystem.Controllers;
using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using SystemDatabase;
using SystemModels.CompanyManagement;
using SystemServices.CompanyManagement;
using SystemViewModels.CompanyManagement;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;

namespace AttendanceManagementSystem.Areas.CompanyManagement.Controllers
{
    public class HRCompanyAttendanceController : BaseController
    {
        // GET: CompanyManagement/HRCompanyAttendance

        private readonly HRCompanyAttendanceServices _HRCompanyAttendanceServices;


        public HRCompanyAttendanceController()
        {
            _HRCompanyAttendanceServices = new HRCompanyAttendanceServices(this._unitOfWork);
        }
        #region ManualAttendance

        [NonAction]
        public Expression<Func<HRCompanyManualAttendance, bool>> Condition(long? idHRCompany, string searchKey = "")
        {
            Expression<Func<HRCompanyManualAttendance, bool>> retutndata = (x => false);

            if (this.SessionDetail.IdRoleType == 0) retutndata = (x => x.HRCompany.Id == idHRCompany && (x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));

            else if (this.SessionDetail.IdRoleType == 1 || SessionDetail.IdRoleType == 2 || SessionDetail.IdRoleType == 4) retutndata = (x => x.HRCompany.Id == SessionDetail.IdHRCompany && (x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));

            else retutndata = (x => false);

            return retutndata;
        }

        //[AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListWrapperHRCompanyAttendanceAsync(long? idHRCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (idHRCompany == null || idHRCompany == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, "Id", orderingDirection);

                return PartialView(new HRCompanyAttendanceViewModellist
                {
                    SessionDetails = SessionDetail,
                    DBModelList = await _HRCompanyAttendanceServices.GetPagedList(Condition(idHRCompany, searchKey), pageNumber, pageSize, orderingBy, orderingDirection),
                    BreadCrumbTitle = "कार्यालय व्यवस्थापन",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyAttendance",
                    BreadCrumbActionName = "_ListWrapperHRCompanyAttendanceAsync",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyAttendance",
                    CRUDAction = CRUDType.READ,
                    HeaderTitle = "Attendance System Management",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    IdHRCompany = idHRCompany
                });
            }
            catch (Exception exp)
            {
                throw new ArgumentException(exp.Message);
            }
        }


        //[AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListHRCompanyAttendanceAsync(long? idHRCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                if (idHRCompany == null || idHRCompany == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRCompanyAttendanceViewModellist
                {
                    SessionDetails = SessionDetail,
                    DBModelList = await _HRCompanyAttendanceServices.GetPagedList(Condition(idHRCompany, searchKey), pageNumber, pageSize, orderingBy, orderingDirection),
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyAttendance",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyAttendance",
                    BreadCrumbActionName = "_ListHRCompanyAttendanceAsync",
                    BreadCrumbTitle = "HRCompanyAttendance",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    IdHRCompany = idHRCompany
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }


        //[AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _CreateHRCompanyAttendanceAsync(long? idHRCompany)
        {
            try
            {
                if (idHRCompany == null || idHRCompany == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                long? idHREmployee;
                idHREmployee = this.SessionDetail.IdHREmployee;
                return PartialView(new HRCompanyAttendanceViewModel
                {
                    SessionDetails = SessionDetail,
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyAttendance",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyAttendance",
                    BreadCrumbTitle = " Company Manual Attendance",
                    BreadCrumbActionName = "_CreateHRCompanyAttendanceAsync",
                    FormModelName = "HRCompanyAttendance",
                    ModalTitle = "नयाँ  थप्नुहोस्",
                    CRUDAction = CRUDType.CREATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Create,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    IdHRCompany = idHRCompany,
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId, idHRCompany),
                    DDLEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeActive, idHRCompany),
                    DDLApprovalEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeApproved, idHRCompany, idHREmployee),
                    DDLLeaveStatusType = await GetIntStringPairModel(PairModelTitle.LeaveApprovalStatus),
                    DataModel = new HRCompanyManualAttendanceModel { Id = 0, IdHrCompany = (long)idHRCompany, IsActive = true }
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
        public async Task<ActionResult> _CreateHRCompanyAttendanceAsync(HRCompanyAttendanceViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHRCompanyAttendanceAsync(viewModel, CRUDType.CREATE, formCollection);
        }

        [NonAction]
        protected async Task<ActionResult> RETSourceHRCompanyAttendanceAsync(HRCompanyAttendanceViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
                viewModel.SessionDetails = SessionDetail;
                viewModel.HeaderTitle = "Attendance System Management";
                viewModel.BreadCrumbArea = "CompanyManagement";
                viewModel.BreadCrumbController = "HRCompanyAttendance";
                viewModel.BreadCrumbBaseURL = "CompanyManagement/HRCompanyAttendance";
                viewModel.DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId, viewModel.DataModel.IdHrCompany);
                viewModel.DDLEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeNP, viewModel.DataModel.IdHrCompany);
                viewModel.DDLApprovalEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeApproved, SessionDetail.IdHRCompany, SessionDetail.IdHREmployee);
                viewModel.DDLLeaveStatusType = await GetIntStringPairModel(PairModelTitle.LeaveApprovalStatus);

                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        viewModel.SubmitButtonText = SubmitButtonText.Create;
                        viewModel.BreadCrumbActionName = "_CreateHRCompanyAttendanceAsync";
                        viewModel.ModalTitle = "नयाँ थप्नुहोस्";
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
                viewModel.DataModel.IdAttendanceStatus = 1;


                if (viewModel.FileToUpload != null && viewModel.FileToUploadGoverment != null)
                {
                    var SubFolderName = viewModel.DataModel.IdHrCompany.ToString();

                    var FolderName = "ManualAttendanceDocuments\\" + SubFolderName + "\\";

                    var DocumentName = FileUpload(viewModel.FileToUpload, FolderName);
                    var OfficialDocumentName = FileUpload(viewModel.FileToUploadGoverment, FolderName);

                    viewModel.DataModel.Document = DocumentName;
                    viewModel.DataModel.FileName = OfficialDocumentName;

                    viewModel.DataModel.FolderName = SubFolderName;
                    viewModel.DataModel.IdAttendanceStatus = 1;
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
            return RedirectToAction("_ListHRCompanyAttendanceAsync", new { idHRCompany = viewModel.DataModel.IdHrCompany, pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
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