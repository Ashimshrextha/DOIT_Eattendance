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
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;

namespace AttendanceManagementSystem.Areas.EmployeeManagement.Controllers
{
    public class EarlyCheckoutAttendanceApprovalController: BaseController
    {

        private readonly EarlyCheckoutAttendanceReportServices _earlyCheckoutAttendanceReportServices;
        private readonly HREmployeeAttendanceHistoryServices _hREmployeeAttendanceHistoryServices;

        public EarlyCheckoutAttendanceApprovalController()
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
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "EarlyCheckoutAttendanceApproval",
                    BreadCrumbBaseURL = "EmployeeManagement/EarlyCheckoutAttendanceApproval",
                    BreadCrumbTitle = "Early Checkout Approval",
                    BreadCrumbActionName = "Index",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    DBModelPageList = await _earlyCheckoutAttendanceReportServices.GetPagedList(this.SessionDetail.IdHRCompany, this.SessionDetail.IdHRCompanyDivision, this.SessionDetail.IdHREmployeeJobStatus, this.SessionDetail.IdHREmployee, DateTime.Now, pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, ""),
                    DDLCompany = await GetLongStringPairModelReport(PairModelTitle.HRCompany),
                    IdHRCompany=this.SessionDetail.IdHRCompany,
                    IdDivision=this.SessionDetail.IdHRCompanyDivision,
                    IdHREmployee=this.SessionDetail.IdHREmployee,
                    IdJobStatus=this.SessionDetail.IdHREmployeeJobStatus,
                    Date=DateTime.Now
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }


        [AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListEarlyCheckoutAttendanceApprovalAsync(long? idHRCompany, long? idHRCompanyDivision, int? idJobStatus, long? idHREmployee, DateTime date, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
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
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "EarlyCheckoutAttendanceApproval",
                    BreadCrumbBaseURL = "Reports/EarlyCheckoutAttendanceApproval",
                    BreadCrumbActionName = "_ListEarlyCheckoutAttendanceApprovalAsync",
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


        [AuthorizeUser(ActionName = "_UpdateEarlyCheckoutAttendanceApprovalAsync")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _UpdateEarlyCheckoutAttendanceApprovalAsync(long? idHRCompany, long? idHRCompanyDivision, long? idHREmployee, DateTime date, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
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
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "EarlyCheckoutAttendanceApproval",
                    BreadCrumbBaseURL = "EmployeeManagement/EarlyCheckoutAttendanceApproval",
                    BreadCrumbActionName = "_UpdateEarlyCheckoutAttendanceApprovalAsync",
                    FormModelName = "EarlyCheckoutAttendanceApproval",
                    ModalTitle = "Approval For Early Checkout",
                    CRUDAction = CRUDType.UPDATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Update,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBModelEmployee = empModel,
                    DataModel = model
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }


        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        [ValidateInput(true)]
        //public async Task<ActionResult> _UpdateEarlyCheckoutAttendanceApprovalAsync(EarlyCheckoutAttendanceReportViewModel viewModel, FormCollection formCollection)
        //{
        //    return await this.CRUDEarlyCheckoutAttendanceApprovalAsync(viewModel, formCollection, CRUDType.UPDATE);
        //}


        [NonAction]
        protected async Task<ActionResult> RETSourceEarlyCheckoutAttendanceApprovalAsync(EarlyCheckoutAttendanceReportViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
                viewModel.BreadCrumbArea = "EmployeeManagement";
                viewModel.BreadCrumbController = "EarlyCheckoutAttendanceApproval";
                viewModel.BreadCrumbBaseURL = "EmployeeManagement/EarlyCheckoutAttendanceApproval";
                viewModel.DataModel = this._earlyCheckoutAttendanceReportServices.Get(viewModel.DataModel.IdHRCompany, viewModel.DataModel.IdHRCompanyDivision, 0, viewModel.DataModel.IdHREmployee, viewModel.DataModel.AttendanceDate);
                viewModel.DBModelEmployee = await _earlyCheckoutAttendanceReportServices.GetEmployeeDetail(viewModel.DataModel.IdHREmployee, viewModel.DataModel.IdHRCompany);
                switch (cRUDType)
                {
                    case CRUDType.UPDATE:
                        viewModel.BreadCrumbActionName = "_UpdateEarlyCheckoutAttendanceApprovalAsync";
                        viewModel.ModalTitle = "Approve Early Checkout Reason";
                        viewModel.FormModelName = "EarlyCheckoutAttendanceApproval";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                }
                return null;
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }


        //    [NonAction]
        //    protected async Task<ActionResult> CRUDEarlyCheckoutAttendanceApprovalAsync(EarlyCheckoutAttendanceReportViewModel viewModel, FormCollection formCollection, CRUDType cRUDType)
        //    {
        //        try
        //        {

        //            if (!ModelState.IsValid)
        //            {
        //                Response.StatusCode = 350;
        //                return await RETSourceEarlyCheckoutAttendanceApprovalAsync(viewModel, cRUDType);
        //            }
        //            var model = await this._hREmployeeAttendanceHistoryServices.GetByIdAsync(viewModel.DataModel.Id);
        //            model.IsCheckOutEarlyReasonApproved = viewModel.DataModel.IsCheckOutEarlyReasonApproved;
        //            await this._hREmployeeAttendanceHistoryServices.UpdateAsync(model);
        //            await this._hREmployeeAttendanceHistoryServices.UnitOfWork.SaveAsync();

        //            //Send E-Mail
        //            string status = viewModel.DataModel.IsCheckOutEarlyReasonApproved ? "Approved" : "Rejected";
        //            string body = $"<p>Dear {viewModel.DBModelEmployee?.HREmployeeName} </p>";
        //            body += $"<p>Your Early Checkout Reason has been {status}</p>";
        //            body += $"<p>Thank You</p>";
        //            await this._earlyCheckoutAttendanceReportServices.SendEmailAsync($"{this.GetApprovalEmployeeEmail(viewModel.DataModel?.IdHREmployee)}", $"Early Checkout Reason {status} on {viewModel.DataModel?.AttendanceDate.ToShortDateString()}", body);
        //        }
        //        catch (Exception exp)
        //        {
        //            Response.StatusCode = 350;
        //            ModelState.AddModelError("", exp.Message);
        //            return await RETSourceEarlyCheckoutAttendanceApprovalAsync(viewModel, cRUDType);
        //        }
        //        await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
        //        return RedirectToAction("_ListEarlyCheckoutAttendanceApprovalAsync", new { idHRCompany = viewModel.DataModel.IdHRCompany, idHRCompanyDivision = this.SessionDetail.IdHRCompanyDivision, idHREmployee = this.SessionDetail.IdHREmployee, date = viewModel.DataModel.AttendanceDate, pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });

        //    }
    }
}