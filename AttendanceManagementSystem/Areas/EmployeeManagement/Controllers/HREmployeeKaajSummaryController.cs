using AttendanceManagementSystem.App_Auth;
using AttendanceManagementSystem.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SystemDatabase;
using SystemServices.EmployeeManagement;
using SystemViewModels.EmployeeManagement;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;

namespace AttendanceManagementSystem.Areas.EmployeeManagement.Controllers
{
    public class HREmployeeKaajSummaryController : BaseController
    {
        // GET: EmployeeManagement/HREmployeeKaajSummary

        private readonly HREmployeeKaajHistoryServices _HREmployeeKaajHistoryServices;

        public HREmployeeKaajSummaryController()
        {
            this._HREmployeeKaajHistoryServices = new HREmployeeKaajHistoryServices(this._unitOfWork);
        }

        [NonAction]
        public Expression<Func<HREmployeeKaajHistory, bool>> Condition(long? idHREmployee, string searchKey = "")
        {
            int lastYear = (Date(DateTime.Now).NepYear - 1);
            Expression<Func<HREmployeeKaajHistory, bool>> returnData = (x => false);
            switch (this.SessionDetail.IdRoleType)
            {
                case (int)RoleType.SuperUser:
                    return returnData = (x => x.IdHREmployee == idHREmployee && (x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) ||
                    searchKey == ""));
                case (int)RoleType.Admin:
                    return returnData = (x => x.IdHREmployee == idHREmployee && x.KaajYearNP >= lastYear  && (x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
                case (int)RoleType.RootUser:
                    return returnData = (x => x.IdHREmployee == idHREmployee && x.KaajYearNP >= lastYear && x.HREmployeeJobHistory.IdHRCompanyDivision == this.SessionDetail.IdHRCompanyDivision && (x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
                case (int)RoleType.SectionAdmin:
                    return returnData = (x => x.IdHREmployee == idHREmployee && x.KaajYearNP >= lastYear && x.HREmployeeJobHistory.IdHRCompanyDivision == this.SessionDetail.IdHRCompanyDivision && (x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));

                case (int)RoleType.NormalUser:
                    return returnData = (x => x.IdHREmployee == this.SessionDetail.IdHREmployee && x.KaajYearNP >= lastYear);
                default:
                    return returnData;
            }
        }


        #region HREmployee  Leave Summary

        [AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListWrapperKaajSumaryAsync(long? idHREmployee, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, "KaajFromNP", orderingDirection);
                return PartialView(new HREmployeeKaajHistoryViewModelList
                {
                    IdHREmployee = idHREmployee,
                    SessionDetails = SessionDetail,
                    DBModelList = await _HREmployeeKaajHistoryServices.GetPagedList(Condition(idHREmployee, searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeKaajSummary",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeKaajSummary",
                    BreadCrumbActionName = "_ListWrapperKaajSumaryAsync",
                    BreadCrumbTitle = "Employee Kaaj Sumary",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListKaajSummaryAsync(long? idHREmployee, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, "KaajFromNP", orderingDirection);
                return PartialView(new HREmployeeKaajHistoryViewModelList
                {
                    IdHREmployee = idHREmployee,
                    DBModelList = await _HREmployeeKaajHistoryServices.GetPagedList(Condition(idHREmployee, searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeKaajSummary",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeKaajSummary",
                    BreadCrumbActionName = "_ListKaajSummaryAsync",
                    BreadCrumbTitle = "Employee",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadKaajSummaryAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, "KaajFromNP", orderingDirection);
                var model = await _HREmployeeKaajHistoryServices.GetModelByIdAsync(id);
                return PartialView(new HREmployeeKaajHistoryViewModelList
                {
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeKaajSummary",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeKaajSummary",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    SessionDetails = SessionDetail,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_ReadKaajSummaryAsync",
                    FormModelName = "HREmployeeKaajSummary",
                    ModalTitle = "View Employee Detail",
                    BreadCrumbTitle = "Employee",
                    CRUDAction = CRUDType.READ,
                    OnlyCancelButton = true,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _DeleteHREmployeeKaajHistoryAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var DataModel = await _HREmployeeKaajHistoryServices.GetModelByIdAsync(id);
                var model = await _HREmployeeKaajHistoryServices.GetsEmployeeDetail(DataModel.IdHREmployee, DataModel.IdHrCompany);
                var selectedList = DataModel.VehicleUsedInKaaj == null ? (new string[] { }) : DataModel.VehicleUsedInKaaj.Split(',');
                DataModel.EmployeeName = model.HREmployeeName;
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HREmployeeKaajHistoryViewModel
                {
                    EmployeeDetail = model,
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeKaajSummary",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeKaajSummary",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    FormModelName = "HREmployeeKaajHistory",
                    ModalTitle = "काज/तालिम मेटाउनुहोस्",
                    BreadCrumbActionName = "_DeleteHREmployeeKaajHistoryAsync",
                    CRUDAction = CRUDType.DELETE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Delete,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLApprovalEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeApproved, DataModel.IdHrCompany, DataModel.IdHREmployee),
                    DDLRecommendEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeRecommended, DataModel.IdHrCompany, DataModel.IdHREmployee),
                    DBModelPagedList = await _HREmployeeKaajHistoryServices.GetPagedList((x => x.IdHREmployee == DataModel.IdHREmployee && x.FiscalYear == DataModel.FiscalYear), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    DDLVehicleType = await GetStringStringPairModel(PairModelTitle.VehicleType),
                    ddlKajVehicalSelected = selectedList,
                    DataModel = DataModel,
                    DDLKaajType = await this.GetIntStringPairModel(PairModelTitle.KaajType),
                    DDLCountry = await this.GetIntStringPairModel(PairModelTitle.Country),
                    DDLYearNp = await this.GetIntStringPairModel(PairModelTitle.Year)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ExportKaajSummaryAsync(long? idHREmployee)
        {
            try
            {
                var yearNP = Date(DateTime.Now).NepYear;
                var lastyearNp = yearNP - 1;

                var model = await _HREmployeeKaajHistoryServices.FindAllAsync(Condition(idHREmployee));
                var data = model.Select(x=>new
                {
                    Name=x.HREmployee1.HREmployeeNameNP,
                    Company= x?.HRCompany?.HRCompanyCode?.HRCompanyCodeTitle,
                    Type=x?.KaajType?.TitleNP,
                    Place=x.KaajLocation,
                    Purpose = x.KaajPurpose,
                    StartDate = x.KaajFromNP,
                    ToDate=x.KaajToNP,
                    ApprovedBy=x.HREmployee?.HREmployeeNameNP,
                    Status=x.HRCompanyLeaveStatusType.LeaveStatusTypeNP,
                    KaajYearNp=x.KaajYearNP,
                    createdBY=x.CreatedBy

                });
                var datalist = data.Where(x => x.KaajYearNp >= lastyearNp).OrderByDescending(x => x.ToDate);
                return await ExportToExcel("HREmployeeKaajSummary", datalist);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintKaajSummaryAsync(long? idHREmployee)
        {
            try
            {
                var yearNP = Date(DateTime.Now).NepYear;
                var lastyearNp = yearNP - 1;
                var Designation = GetDesignationByIDhremployee(idHREmployee);
                var model = await _HREmployeeKaajHistoryServices.FindAllAsync(Condition(idHREmployee));
               
                return PartialView(new HREmployeeKaajHistoryViewModel
                {
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeKaajSummary",
                    SessionDetails = SessionDetail,
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeKaajSummary",
                    BreadCrumbActionName = "_PrintKaajSummaryAsync",
                    FormModelName = "HREmployeeKaajSummary",
                    ModalTitle = "कर्मचारीको काज/तालिम छाप्नुहोस्",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    IdHREmployee = idHREmployee,
                    DBModelList = model,
                    lastYearNp=lastyearNp,
                    CurrentDesignation=Designation
                    
                    
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _DeleteHREmployeeKaajHistoryAsync(HREmployeeKaajHistoryViewModel viewModel, FormCollection formCollection)
        {
            try
            {
                await _HREmployeeKaajHistoryServices.DeleteByIdAsync(viewModel.DataModel.Id);
                await _HREmployeeKaajHistoryServices.UnitOfWork.SaveAsync();
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
            await this.AlertNotification("Delete", viewModel.BreadCrumbTitle, AlertNotificationType.error);
            return RedirectToAction("_ListKaajSummaryAsync", new { idHREmployee = viewModel.DataModel.IdHREmployee, pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }

        [NonAction]
        protected async Task<ActionResult> RETSourceHREmployeeKaajSummaryAsync(HREmployeeKaajHistoryViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
                viewModel.SessionDetails = SessionDetail;
                viewModel.BreadCrumbArea = "EmployeeManagement";
                viewModel.BreadCrumbController = "HREmployeeKaajSummary";
                viewModel.BreadCrumbBaseURL = "EmployeeManagement/HREmployeeKaajSummary";
                switch (cRUDType)
                {
                    case CRUDType.PRINT:
                        viewModel.BreadCrumbActionName = "_PrintKaajSummaryAsync";
                        viewModel.ModalTitle = "Print Employee Kaaj Summary";
                        viewModel.FormModelName = "HREmployeeKaajSummary";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                }
                return null;
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("", exp.Message, AlertNotificationType.error);
            }
        }

        [NonAction]
        protected async Task<ActionResult> CRUDHREmployeeLeaveSummaryAsync(HREmployeeKaajHistoryViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceHREmployeeKaajSummaryAsync(viewModel, cRUDType);
                }
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceHREmployeeKaajSummaryAsync(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListKaajSummaryAsync", new { pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> DownloadDocumentAsync(long? id, string FileName)
        {
            try
            {
                string dirPath = Server.MapPath("~") + "Upload\\EmployeeKaajProfile\\" + id + "\\" + FileName;
                string result = Path.GetFileName(dirPath);
                byte[] fileBytes = System.IO.File.ReadAllBytes(dirPath);
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, result);

            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Download", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _GetHREmployeeKaajSummaryAsync(long? idHREmployee, string dateNP)
        {
            try
            {
                int yearNP = this.GetYearFromNepaliDate(dateNP);
                var pagination = Get_PaginationValue(1, 20, "KaajFromNP", "DESC");
                return PartialView(new HREmployeeKaajHistoryViewModel
                {
                    DBModelPagedList = await _HREmployeeKaajHistoryServices.GetPagedList((x => x.IdHREmployee == idHREmployee && x.KaajYearNP == yearNP), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = string.Empty
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }
    }
    #endregion
}
