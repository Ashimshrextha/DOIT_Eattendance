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
    public class HRCompanyOfficeLogsController : BaseController
    {
        // GET: CompanyManagement/HRCompanyOfficeLogs

        private readonly HRCompanyOfficeLogsServices _HRCompanyOfficeLogsServices;


        public HRCompanyOfficeLogsController()
        {
            _HRCompanyOfficeLogsServices = new HRCompanyOfficeLogsServices(this._unitOfWork);
        }
        #region Office Logs

        [NonAction]
        public Expression<Func<HRCompanyEmployeeOfficeLog, bool>> Condition(long? idHRCompany, string searchKey = "")
        {
            Expression<Func<HRCompanyEmployeeOfficeLog, bool>> retutndata = (x => false);

            if (this.SessionDetail.IdRoleType == 0) retutndata = (x => x.HRCompany.Id == idHRCompany && (x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));

            else if (this.SessionDetail.IdRoleType == 1 || SessionDetail.IdRoleType == 2 || SessionDetail.IdRoleType == 4) retutndata = (x => x.HRCompany.Id == SessionDetail.IdHRCompany && (x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));

            else retutndata = (x => false);

            return retutndata;
        }

        //[AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListWrapperHRCompanyOfficeLogsAsync(long? idHRCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (idHRCompany == null || idHRCompany == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, "CreatedOn", "DESC");

                return PartialView(new HRCompanyOfficeLogsViewModellist
                {
                    SessionDetails = SessionDetail,
                    DBModelList = await _HRCompanyOfficeLogsServices.GetPagedList(Condition(idHRCompany, searchKey), pageNumber, pageSize, orderingBy, orderingDirection),
                    BreadCrumbTitle = "कार्यालय व्यवस्थापन",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyOfficeLogs",
                    BreadCrumbActionName = "_ListWrapperHRCompanyOfficeLogsAsync",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyOfficeLogs",
                    CRUDAction = CRUDType.READ,
                    HeaderTitle = "Office Logs System Management",
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
        public async Task<ActionResult> _ListHRCompanyOfficeLogsAsync(long? idHRCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                if (idHRCompany == null || idHRCompany == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRCompanyOfficeLogsViewModellist
                {
                    SessionDetails = SessionDetail,
                    DBModelList = await _HRCompanyOfficeLogsServices.GetPagedList(Condition(idHRCompany, searchKey), pageNumber, pageSize, orderingBy, orderingDirection),
                    HeaderTitle = "Office Logs System Management",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyOfficeLogs",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyOfficeLogs",
                    BreadCrumbActionName = "_ListHRCompanyOfficeLogsAsync",
                    BreadCrumbTitle = "HRCompanyOfficeLogs",
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
        public async Task<ActionResult> _CreateHRCompanyOfficeLogsAsync(long? idHRCompany)
        {
            try
            {
                if (idHRCompany == null || idHRCompany == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                long? idHREmployee;
                idHREmployee = this.SessionDetail.IdHREmployee;
                return PartialView(new HRCompanyOfficeLogsViewModel
                {
                    SessionDetails = SessionDetail,
                    HeaderTitle = "Office Logs System Management",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyOfficeLogs",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyOfficeLogs",
                    BreadCrumbTitle = " Company Office Logs",
                    BreadCrumbActionName = "_CreateHRCompanyOfficeLogsAsync",
                    FormModelName = "HRCompanyOfficeLogs",
                    ModalTitle = "नयाँ  थप्नुहोस्",
                    CRUDAction = CRUDType.CREATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Create,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    IdHRCompany = idHRCompany,
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId, idHRCompany),
                    DDLEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeNP, idHRCompany),
                    DataModel = new HRCompanyOfficeLogsModel { Id = 0, IdHRCompany = (long)idHRCompany, IsActive = true }
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
        public async Task<ActionResult> _CreateHRCompanyOfficeLogsAsync(HRCompanyOfficeLogsViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHRCompanyOfficeLogsAsync(viewModel, CRUDType.CREATE, formCollection);
        }

        [NonAction]
        protected async Task<ActionResult> RETSourceHRCompanyOfficeLogsAsync(HRCompanyOfficeLogsViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
                viewModel.SessionDetails = SessionDetail;
                viewModel.HeaderTitle = "Office Logs System Management";
                viewModel.BreadCrumbArea = "CompanyManagement";
                viewModel.BreadCrumbController = "HRCompanyOfficeLogs";
                viewModel.BreadCrumbBaseURL = "CompanyManagement/HRCompanyOfficeLogs";
                viewModel.DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId, viewModel.DataModel.IdHRCompany);
                viewModel.DDLEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeNP, viewModel.DataModel.IdHRCompany);

                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        viewModel.SubmitButtonText = SubmitButtonText.Create;
                        viewModel.BreadCrumbActionName = "_CreateHRCompanyOfficeLogsAsync";
                        viewModel.ModalTitle = "नयाँ थप्नुहोस्";
                        viewModel.FormModelName = "HRCompanyOfficeLogs";
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
        protected async Task<ActionResult> CRUDHRCompanyOfficeLogsAsync(HRCompanyOfficeLogsViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                await _HRCompanyOfficeLogsServices.CommitSaveChangesAsync(viewModel.DataModel, cRUDType);
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceHRCompanyOfficeLogsAsync(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListHRCompanyOfficeLogsAsync", new { idHRCompany = viewModel.DataModel.IdHRCompany, pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
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
            string dirPath = Server.MapPath("~") + "Upload\\OfficeLogsDocuments\\" + FolderName + "\\" + FileName;
            string result = Path.GetFileName(dirPath);
            byte[] fileBytes = System.IO.File.ReadAllBytes(dirPath);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, result);
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ExportHRCompanyOfficeLogsAsync(long? idHRCompany)
        {
            try
            {
                if (idHRCompany == 0 || idHRCompany == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var model = await _HRCompanyOfficeLogsServices.FindAllAsync(Condition(idHRCompany));
                var data = model.Select(x => new
                {
                    Company = x.HRCompany?.HRCompanyCode?.HRCompanyCodeTitle,
                    Employee = x.HREmployee.HREmployeeNameNP,
                    OfficeDate = x.OfficeDate + x.OfficeTime,
                    Remarks = x.Remarks,
                });
                return await ExportToExcel("OfficeLogs", data);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintHRCompanyOfficeLogsAsync(long? idHRCompany)
        {
            try
            {
                if (idHRCompany == 0 || idHRCompany == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                return PartialView(new HRCompanyOfficeLogsViewModel
                {
                    SessionDetails = SessionDetail,
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyOfficeLogs",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyOfficeLogs",
                    BreadCrumbActionName = "_PrintHRCompanyOfficeLogsAsync",
                    FormModelName = "HRCompanyOfficeLogs",
                    ModalTitle = "छाप्नुहोस्",
                    BreadCrumbTitle = "Office Logs",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBModelList = await _HRCompanyOfficeLogsServices.FindAllAsync(Condition(idHRCompany))
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }
        #endregion
    }
}