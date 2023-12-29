using AttendanceManagementSystem.App_Auth;
using AttendanceManagementSystem.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SystemServices.Reports;
using SystemServices.SystemSecurity;
using SystemViewModels.Reports;
using SystemViewModels.Shared;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;

namespace AttendanceManagementSystem.Areas.Reports.Controllers
{
    public class EarlyCheckoutAttendanceReportController : BaseController
    {
        //GET: Reports/EarlyCheckoutAttendanceReport


        private readonly EarlyCheckoutAttendanceReportServices _earlyCheckoutAttendanceReportServices;
        private readonly HREmployeeAttendanceHistoryServices _hREmployeeAttendanceHistoryServices;

        public EarlyCheckoutAttendanceReportController()
        {
            this._earlyCheckoutAttendanceReportServices = new EarlyCheckoutAttendanceReportServices(this._unitOfWork);
            this._hREmployeeAttendanceHistoryServices = new HREmployeeAttendanceHistoryServices(this._unitOfWork);
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> Index(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return View(new EarlyCheckoutAttendanceReportViewModelList
                {
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "EarlyCheckoutAttendanceReport",
                    BreadCrumbBaseURL = "Reports/EarlyCheckoutAttendanceReport",
                    BreadCrumbTitle = "Early Checkout Attendance Report",
                    BreadCrumbActionName = "Index",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    DBModelPageList = await _earlyCheckoutAttendanceReportServices.GetPagedList(this.SessionDetail.IdHRCompany, this.SessionDetail.IdHRCompanyDivision, this.SessionDetail.IdHREmployeeJobStatus, this.SessionDetail.IdHREmployee, DateTime.Now, pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, ""),
                    DDLCompany = await GetLongStringPairModelReport(PairModelTitle.HRCompany),
                    DDLJobStatus = await this.GetIntStringPairModel(PairModelTitle.HREmployeePositionStatusType),
                    Date = this.Today.Result.EngDate,
                    DateNp = this.Today.Result.Nepdate,
                    IdHRCompany = this.SessionDetail.IdHRCompany,
                    IdDivision = this.SessionDetail.IdHRCompanyDivision,
                    IdJobStatus = this.SessionDetail.IdHREmployeeJobStatus
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListEarlyCheckoutAttendanceReportAsync(long? idHRCompany, long? idHRCompanyDivision, int? idJobStatus, long? idHREmployee, DateTime date, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                idJobStatus = idJobStatus == 0 ? null : idJobStatus;
                return PartialView(new EarlyCheckoutAttendanceReportViewModelList
                {
                    IdHRCompany = idHRCompany,
                    IdDivision = idHRCompanyDivision,
                    IdHREmployee = idHREmployee,
                    Date = date,
                    IdJobStatus = idJobStatus,
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "EarlyCheckoutAttendanceReport",
                    BreadCrumbBaseURL = "Reports/EarlyCheckoutAttendanceReport",
                    BreadCrumbActionName = "_ListEarlyCheckoutAttendanceReportAsync",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    DBModelPageList = await this._earlyCheckoutAttendanceReportServices.GetPagedList(idHRCompany, idHRCompanyDivision, idJobStatus, idHREmployee, date, pageNumber, pageSize, orderingBy, orderingDirection, searchKey)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser(ActionName = "_UpdateEarlyCheckoutAttendanceReportAsync")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _UpdateEarlyCheckoutAttendanceReportAsync(long? idHRCompany, long? idHRCompanyDivision, long? idHREmployee, DateTime date, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var model = this._earlyCheckoutAttendanceReportServices.Get(idHRCompany, idHRCompanyDivision, 0, idHREmployee, date);
                var empModel = await _earlyCheckoutAttendanceReportServices.GetEmployeeDetail(model.IdHREmployee, model.IdHRCompany);
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new EarlyCheckoutAttendanceReportViewModel
                {
                    IdHRCompany = idHRCompany,
                    IdDivision = idHRCompanyDivision,
                    IdHREmployee = idHREmployee,
                    Date = date,
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "EarlyCheckoutAttendanceReport",
                    BreadCrumbBaseURL = "Reports/EarlyCheckoutAttendanceReport",
                    BreadCrumbActionName = "_UpdateEarlyCheckoutAttendanceReportAsync",
                    FormModelName = "EarlyCheckoutAttendanceReport",
                    ModalTitle = "Reason For Early Checkout",
                    CRUDAction = CRUDType.UPDATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Update,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBModelEmployee = empModel,
                    DataModel = model,
                    DDLApprovalEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeApproved, this.SessionDetail.IdHRCompany, this.SessionDetail.IdHREmployee)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _UpdateEarlyCheckoutAttendanceReportAsync(EarlyCheckoutAttendanceReportViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDEarlyCheckoutAttendanceReportAsync(viewModel, formCollection, CRUDType.UPDATE);
        }



        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintEarlyCheckoutAttendanceReportAsync(long? idHRCompany, long? idHRCompanyDivision, int? idJobStatus, long? idHREmployee, DateTime date)
        {
            try
            {
                var Section = "सबै शाखा";
                var jobStatus = await this.GetJobStatusTitle(idJobStatus);
                Section = await GetDivisionNameNep(idHRCompanyDivision);
                var information =await GetCompanyHeaderDetails(idHRCompany);
                string CompanyNameNP = information.CompanyNameNP;
                string ParentCompanyNameNP =  information.ParentCompanyNameNP;


                EarlyCheckoutAttendanceReportViewModelList modeldata = new EarlyCheckoutAttendanceReportViewModelList();

                modeldata = new EarlyCheckoutAttendanceReportViewModelList
                {
                    DBModelList = await _earlyCheckoutAttendanceReportServices.GetPrintResult(idHRCompany, idHRCompanyDivision, idJobStatus, idHREmployee, date),
                    BreadCrumbArea = "Reports",
                    IdJobStatus = idJobStatus,
                    BreadCrumbController = "EarlyCheckoutAttendanceReport",
                    BreadCrumbBaseURL = "Reports/EarlyCheckoutAttendanceReport",
                    BreadCrumbActionName = "_PrintEarlyCheckoutAttendanceReportAsync",
                    ModalTitle = "छिटो गएको विवरण छाप्नुहोस्",
                    FormModelName = "EarlyCheckoutAttendancePrintReport",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    Header = new ReportHeaderViewModel
                    {
                        CompanyName = CompanyNameNP,
                        ParentCompanyName = ParentCompanyNameNP,
                        DivisionName = SessionDetail.IdHRCompanyDivision.ToString(),
                        ReportName = $" को {Section} (शाखा) को {jobStatus} कर्मचारीहरुको छिटो गएको विवरण"
                    }
                };

                return PartialView(modeldata);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }







        [NonAction]
        protected async Task<ActionResult> RETSourceEarlyCheckoutAttendanceReportAsync(EarlyCheckoutAttendanceReportViewModel viewModel, CRUDType cRUDType)
        {
            try
            {

                viewModel.BreadCrumbArea = "Reports";
                viewModel.BreadCrumbController = "EarlyCheckoutAttendanceReport";
                viewModel.BreadCrumbBaseURL = "Reports/EarlyCheckoutAttendanceReport";
                viewModel.DataModel = this._earlyCheckoutAttendanceReportServices.Get(viewModel.DataModel.IdHRCompany, viewModel.DataModel.IdHRCompanyDivision, 0, viewModel.DataModel.IdHREmployee, viewModel.DataModel.AttendanceDate);
                viewModel.DBModelEmployee = await _earlyCheckoutAttendanceReportServices.GetEmployeeDetail(viewModel.DataModel.IdHREmployee, viewModel.DataModel.IdHRCompany);
                switch (cRUDType)
                {
                    case CRUDType.UPDATE:
                        viewModel.BreadCrumbActionName = "_UpdateLateAttendanceReportAsync";
                        viewModel.ModalTitle = "Update Early Checkout Reason";
                        viewModel.FormModelName = "EarlyCheckoutAttendanceReport";
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
        protected async Task<ActionResult> CRUDEarlyCheckoutAttendanceReportAsync(EarlyCheckoutAttendanceReportViewModel viewModel, FormCollection formCollection, CRUDType cRUDType)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceEarlyCheckoutAttendanceReportAsync(viewModel, cRUDType);
                }
                var model = await this._hREmployeeAttendanceHistoryServices.GetByIdAsync(viewModel.DataModel.Id);
                model.CheckOutEarlyReason = viewModel.DataModel.CheckOutEarlyReason;
                model.IdApprovedBy = viewModel.DataModel.IdApprovedBy;
                await this._hREmployeeAttendanceHistoryServices.UpdateAsync(model);
                await this._hREmployeeAttendanceHistoryServices.UnitOfWork.SaveAsync();

                //Send E-Mail
                string body = $"<p>Dear Sir/Madam, </p>";
                body += $"<p>I, {viewModel.DBModelEmployee?.HREmployeeName} left office early on {viewModel.DataModel?.AttendanceDate.ToShortDateString()}</p>";
                body += $"<p>Logout Time : {viewModel.DataModel?.LogOutTime}, Checkout Time : {viewModel.DataModel?.CheckOutTime}</p><p>Early Checkout By: {viewModel.DataModel?.CheckOutEarly}</p>";
                body += $"<p>Reason :{viewModel.DataModel?.CheckOutEarlyReason}</p>";
                body += $"<p>Thank You</p>";
                await this._earlyCheckoutAttendanceReportServices.SendEmailAsync($"{this.GetApprovalEmployeeEmail(viewModel.DataModel.IdApprovedBy)}", $"Early Checkout Reason Request by {viewModel.DBModelEmployee?.HREmployeeName} on {viewModel.DataModel?.AttendanceDate}", body);
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceEarlyCheckoutAttendanceReportAsync(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListEarlyCheckoutAttendanceReportAsync", new { idHRCompany = viewModel.DataModel.IdHRCompany, idHRCompanyDivision = this.SessionDetail.IdHRCompanyDivision, idHREmployee = this.SessionDetail.IdHREmployee, date = viewModel.DataModel.AttendanceDate, pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }



      
    }
}