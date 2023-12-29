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
using SystemServices.EmployeeManagement;
using SystemServices.SystemSetting;
using SystemViewModels.EmployeeManagement;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;

namespace AttendanceManagementSystem.Areas.EmployeeManagement.Controllers
{
    public class HREmployeeLeaveSummaryController : BaseController
    {

        private readonly HREmployeeLeaveHistoryServices _hREmployeeLeaveHistoryServices;
        private readonly HRCompanyLeaveTypeServices _hRCompanyLeaveTypeServices;

        public HREmployeeLeaveSummaryController()
        {
            this._hREmployeeLeaveHistoryServices = new HREmployeeLeaveHistoryServices(this._unitOfWork);
            this._hRCompanyLeaveTypeServices = new HRCompanyLeaveTypeServices(this._unitOfWork);
        }

        [NonAction]
        public Expression<Func<HREmployeeLeaveHistory, bool>> Condition(long? idHREmployee, string searchKey = "")
        {
            int lastYear = (Date(DateTime.Now).NepYear - 1);
            Expression<Func<HREmployeeLeaveHistory, bool>> returnData = (x => false);
            switch (this.SessionDetail.IdRoleType)
            {
                case (int)RoleType.SuperUser:
                    return returnData = (x => x.IdHREmployee == idHREmployee && (x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
                case (int)RoleType.Admin:
                    return returnData = (x => x.IdHREmployee == idHREmployee && x.LeaveYearNP >= lastYear && (x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
                case (int)RoleType.RootUser:
                    return returnData = (x => x.IdHREmployee == idHREmployee && x.LeaveYearNP >= lastYear && x.HREmployeeJobHistory.IdHRCompanyDivision == this.SessionDetail.IdHRCompanyDivision && (x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
                case (int)RoleType.SectionAdmin:
                    return returnData = (x => x.IdHREmployee == idHREmployee && x.LeaveYearNP >= lastYear && x.HREmployeeJobHistory.IdHRCompanyDivision == this.SessionDetail.IdHRCompanyDivision && (x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
                case (int)RoleType.NormalUser:
                    return returnData = (x => x.IdHREmployee == this.SessionDetail.IdHREmployee && x.LeaveYearNP >= lastYear);
                default:
                    return returnData;
            }
        }

        #region HREmployee  Leave Summary

        [AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListWrapperLeaveSumaryAsync(long? idHREmployee, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
               
                var pagination = Get_PaginationValue(pageNumber, pageSize, "LeaveValidFromNP", orderingDirection);
                return PartialView(new HREmployeeLeaveSummaryViewModelList
                {
                    IdHREmployee = idHREmployee,
                    SessionDetails = SessionDetail,
                    DBModelList = await _hREmployeeLeaveHistoryServices.GetPagedList(Condition(idHREmployee, searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeLeaveSummary",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLeaveSummary",
                    BreadCrumbActionName = "_ListWrapperLeaveSumaryAsync",
                    BreadCrumbTitle = "Employee Leave Sumary",
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
        public async Task<ActionResult> _ListLeaveSummaryAsync(long? idHREmployee, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
              
                var pagination = Get_PaginationValue(pageNumber, pageSize, "LeaveValidFromNP", orderingDirection);
                return PartialView(new HREmployeeLeaveSummaryViewModelList
                {
                    IdHREmployee = idHREmployee,
                    DBModelList = await _hREmployeeLeaveHistoryServices.GetPagedList(Condition(idHREmployee, searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeLeaveSummary",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLeaveSummary",
                    BreadCrumbActionName = "_ListLeaveSummaryAsync",
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
        public async Task<ActionResult> _ReadLeaveSummaryAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _hREmployeeLeaveHistoryServices.GetModelByIdAsync(id);
                var empModel = await _hREmployeeLeaveHistoryServices.GetsEmployeeDetail(model.IdHREmployee, model.IdHRCompany);
                model.EmployeeName = empModel.HREmployeeNameNP;
                model.Division = empModel.HRCompanyDivisionName;
                model.Designation = empModel.HRDesignationTitle;
                model.Mobile = empModel.MobileNumber;
                model.PISNumber = empModel.PISNumber;
                model.Gender = empModel.HREmployeeSexTitle;
                model.HRCompanyName = empModel.CompanyNameNP;
                model.ImageName = empModel.ImageName;
                model.CurrentYearBalanceLeaveDay = await this.GetAvailableLeaveDays(model.IdHREmployee, model.IdLeaveType, (int)model.LeaveYearNP);

                return PartialView(new HREmployeeLeaveSummaryViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeLeaveSummary",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLeaveSummary",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    SessionDetails = SessionDetail,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_ReadLeaveSummaryAsync",
                    FormModelName = "HREmployeeLeaveSummary",
                    ModalTitle = "View Employee Detail",
                    BreadCrumbTitle = "Employee",
                    CRUDAction = CRUDType.READ,
                    OnlyCancelButton = true,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = model,
                    DDLDivision = await GetLongStringPairModel(PairModelTitle.Division, model.IdHRCompany),
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.HRCompany),
                    DDLDesignation = await GetLongStringPairModel(PairModelTitle.Designation, model.IdHRCompany),
                    DDLServiceType = await GetLongStringPairModel(PairModelTitle.ServiceType, model.IdHRCompany),
                    DDLJobStatus = await GetIntStringPairModel(PairModelTitle.JobStatus),
                    DDLLeaveType = await GetLongStringPairModel(PairModelTitle.LeaveType, model.IdHRCompany, model.IdHREmployee),
                     DDLApprovalEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeApproved, model.IdHRCompany, model.IdHREmployee),
                    DDLRecommendEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeRecommended, model.IdHRCompany, model.IdHREmployee),

                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _DeleteHREmployeeLeaveHistoryAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var model = await _hREmployeeLeaveHistoryServices.GetModelByIdAsync(id);
                var empModel = await _hREmployeeLeaveHistoryServices.GetsEmployeeDetail(model.IdHREmployee, model.IdHRCompany);
                model.EmployeeName = empModel.HREmployeeName;
                model.Division = empModel.HRCompanyDivisionName;
                model.Designation = empModel.HRDesignationTitle;
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HREmployeeLeaveHistoryViewModel
                {
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeLeaveSummary",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLeaveSummary",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    FormModelName = "HREmployeeLeaveHistory",
                    SessionDetails = this.SessionDetail,
                    ModalTitle = "बिदा विवरण मेटाउनुहोस्",
                    BreadCrumbActionName = "_DeleteHREmployeeLeaveHistoryAsync",
                    CRUDAction = CRUDType.DELETE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Delete,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = model,
                    DDLApprovalEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeApproved, model.IdHRCompany, model.IdHREmployee),
                    DDLRecommendEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeRecommended, model.IdHRCompany, model.IdHREmployee),
                    DDLLeaveType = await GetLongStringPairModel(PairModelTitle.LeaveType, model.IdHRCompany, model.IdHREmployee)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ExportLeaveSummaryAsync(long? idHREmployee)
        {
            try
            {
             
                var model = await _hREmployeeLeaveHistoryServices.FindAllAsync(Condition(idHREmployee));
                var data =model.Select(x=>new{
                    Name=x.HREmployee1.HREmployeeNameNP,
                    Company=x.HRCompany.HRCompanyCode.HRCompanyCodeTitle,
                    LeaveType =x.HRCompanyLeaveType.MasterLeaveTitle.LeaveTitleNP,
                    StartDate=x.LeaveValidFromNP,
                    EndDate=x.LeaveValidToNP,
                    RecomendatedBY=x.HREmployee2?.HREmployeeNameNP,
                    ApprovedBy= x.HREmployee?.HREmployeeNameNP,
                    Status = x.HRCompanyLeaveStatusType.LeaveStatusTypeNP,
                    createdBY=x.CreatedBy,
                   
                });
                var datalist = data.OrderByDescending(x => x.EndDate);
                return await ExportToExcel("HREmployeeLeaveSummary", datalist);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintLeaveSummaryAsync(long? idHREmployee)
        {
            try
            {
               
                var Designation = GetDesignationByIDhremployee(idHREmployee);
                var model = await _hREmployeeLeaveHistoryServices.FindAllAsync(Condition(idHREmployee));
                return PartialView(new HREmployeeLeaveSummaryViewModel
                {
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeLeaveSummary",
                    SessionDetails = SessionDetail,
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLeaveSummary",
                    BreadCrumbActionName = "_PrintLeaveSummaryAsync",
                    FormModelName = "HREmployeeLeaveSummary",
                    ModalTitle = "कर्मचारीको विदा छाप्नुहोस्",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    IdHREmployee = idHREmployee,
                    DBModelList = model,
                    CurrentDesignation=Designation,

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
        public async Task<ActionResult> _DeleteHREmployeeLeaveHistoryAsync(HREmployeeLeaveHistoryViewModel viewModel, FormCollection formCollection)
        {
            try
            {
                await _hREmployeeLeaveHistoryServices.DeleteByIdAsync(viewModel.DataModel.Id);
                await _hREmployeeLeaveHistoryServices.UnitOfWork.SaveAsync();
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
            await this.AlertNotification("Delete", viewModel.BreadCrumbTitle, AlertNotificationType.error);
            return RedirectToAction("_ListLeaveSummaryAsync", new { idHREmployee = viewModel.DataModel.IdHREmployee, pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }


        [NonAction]
        protected async Task<ActionResult> RETSourceHREmployeeLeaveSummaryAsync(HREmployeeLeaveSummaryViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
                viewModel.SessionDetails = SessionDetail;
                viewModel.BreadCrumbArea = "EmployeeManagement";
                viewModel.BreadCrumbController = "HREmployeeLeaveSummary";
                viewModel.BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLeaveSummary";
                switch (cRUDType)
                {
                    case CRUDType.PRINT:
                        viewModel.BreadCrumbActionName = "_PrintLeaveSummaryAsync";
                        viewModel.ModalTitle = "Print Employee Leave Summary";
                        viewModel.FormModelName = "HREmployeeLeaveSummary";
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
        protected async Task<ActionResult> CRUDHREmployeeLeaveSummaryAsync(HREmployeeLeaveSummaryViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceHREmployeeLeaveSummaryAsync(viewModel, cRUDType);
                }
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceHREmployeeLeaveSummaryAsync(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListLeaveSummaryAsync", new { pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });

        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> DownloadDocumentAsync(long? id, string FileName)
        {
            try
            {
                string dirPath = Server.MapPath("~") + "Upload\\LeaveForm\\" + id + "\\" + FileName;
                string result = Path.GetFileName(dirPath);
                byte[] fileBytes = System.IO.File.ReadAllBytes(dirPath);
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, result);

            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Download", exp.Message, AlertNotificationType.error);
            }
        }



    }

    #endregion
}

