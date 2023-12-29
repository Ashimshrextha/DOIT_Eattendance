using AttendanceManagementSystem.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SystemModels.EmployeeManagement;
using SystemServices.SystemSecurity;
using SystemViewModels.SystemSecurity;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;


namespace AttendanceManagementSystem.Areas.SystemSetting.Controllers
{
    public class HREmployeeAttendanceHistoryController : BaseController
    {
        // GET: SystemSetting/HREmployeeAttendanceHistory
        private readonly HREmployeeAttendanceHistoryServices _HREmployeeAttendanceHistoryServices;

        public HREmployeeAttendanceHistoryController()
        {
            this._HREmployeeAttendanceHistoryServices = new HREmployeeAttendanceHistoryServices(this._unitOfWork);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> Index(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return View(new HREmployeeAttendanceHistoryViewModelList
                {
                    DBModelList = await _HREmployeeAttendanceHistoryServices.GetPagedList(pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "HREmployeeAttendanceHistory",
                    BreadCrumbBaseURL = "SystemSecurity/HREmployeeAttendanceHistory",
                    BreadCrumbTitle = "हाजिरी विवरण",
                    BreadCrumbActionName = "Index",
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

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListHREmployeeAttendanceHistoryAsync(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HREmployeeAttendanceHistoryViewModelList
                {
                    DBModelList = await _HREmployeeAttendanceHistoryServices.GetBySearchKey(pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, searchKey),
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "HREmployeeAttendanceHistory",
                    BreadCrumbBaseURL = "SystemSecurity/HREmployeeAttendanceHistory",
                    BreadCrumbActionName = "_ListHREmployeeAttendanceHistoryAsync",
                    BreadCrumbTitle = "Attendance History",
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

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _CreateHREmployeeAttendanceHistoryAsync()
        {
            try
            {
                return PartialView(new HREmployeeAttendanceHistoryViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "HREmployeeAttendanceHistory",
                    BreadCrumbBaseURL = "SystemSecurity/HREmployeeAttendanceHistory",
                    BreadCrumbActionName = "_CreateHREmployeeAttendanceHistoryAsync",
                    FormModelName = "HREmployeeAttendanceHistory",
                    ModalTitle = "नयाँ हाजिरी विवरण थप्नुहोस्",
                    BreadCrumbTitle = "Employee Attendance",
                    CRUDAction = CRUDType.CREATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Create,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLEmployee = await GetLongStringPairModel(PairModelTitle.Employee),
                    DataModel = new HREmployeeAttendanceHistoryModel { Id = 0 }
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadHREmployeeAttendanceHistoryAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var DataModel = await _HREmployeeAttendanceHistoryServices.GetModelByIdAsync(id);
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HREmployeeAttendanceHistoryViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "HREmployeeAttendanceHistory",
                    BreadCrumbBaseURL = "SystemSecurity/HREmployeeAttendanceHistory",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_ReadHREmployeeAttendanceHistoryAsync",
                    FormModelName = "HREmployeeAttendanceHistory",
                    ModalTitle = "हाजिरी विवरण हेर्नुहोस्",
                    BreadCrumbTitle = "Attendance History",
                    CRUDAction = CRUDType.READ,
                    OnlyCancelButton = true,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeId, DataModel.IdHREmployee),
                    DataModel = DataModel,
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _UpdateHREmployeeAttendanceHistoryAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var DataModel = await _HREmployeeAttendanceHistoryServices.GetModelByIdAsync(id);
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HREmployeeAttendanceHistoryViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "HREmployeeAttendanceHistory",
                    BreadCrumbBaseURL = "SystemSecurity/HREmployeeAttendanceHistory",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_UpdateHREmployeeAttendanceHistoryAsync",
                    FormModelName = "HREmployeeAttendanceHistory",
                    ModalTitle = "हाजिरी विवरण सम्पादन गर्नुहोस्",
                    BreadCrumbTitle = "Attendance History",
                    CRUDAction = CRUDType.UPDATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Update,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeId, DataModel.IdHREmployee),
                    DataModel = DataModel,
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _DeleteHREmployeeAttendanceHistoryAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var DataModel = await _HREmployeeAttendanceHistoryServices.GetModelByIdAsync(id);
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HREmployeeAttendanceHistoryViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "HREmployeeAttendanceHistory",
                    BreadCrumbBaseURL = "SystemSecurity/HREmployeeAttendanceHistory",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    FormModelName = "HREmployeeAttendanceHistory",
                    ModalTitle = "हाजिरी विवरण मेटाउन निश्चित हुनुहुन्छ?",
                    BreadCrumbTitle = "Attendance History",
                    BreadCrumbActionName = "_DeleteHREmployeeAttendanceHistoryAsync",
                    CRUDAction = CRUDType.DELETE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Delete,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeId, DataModel.IdHREmployee),
                    DataModel = DataModel,
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ExportHREmployeeAttendanceHistoryAsync()
        {
            try
            {
                var model = await _HREmployeeAttendanceHistoryServices.FindAllAsync(x => true);
                return await ExportToExcel("HREmployeeAttendanceHistory", model);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintHREmployeeAttendanceHistoryAsync()
        {
            try
            {
                return PartialView(new HREmployeeAttendanceHistoryViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "HREmployeeAttendanceHistory",
                    BreadCrumbBaseURL = "SystemSecurity/HREmployeeAttendanceHistory",
                    BreadCrumbActionName = "_PrintHREmployeeAttendanceHistoryAsync",
                    FormModelName = "HREmployeeAttendanceHistory",
                    ModalTitle = "हाजिरी विवरण सुची छाप्नुहोस्",
                    BreadCrumbTitle = "Attendance History",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBModelList = await _HREmployeeAttendanceHistoryServices.FindAllAsync(x => true)
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
        public async Task<ActionResult> _CreateHREmployeeAttendanceHistoryAsync(HREmployeeAttendanceHistoryViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeAttendanceHistory(viewModel, CRUDType.CREATE, formCollection);
        }

        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _UpdateHREmployeeAttendanceHistoryAsync(HREmployeeAttendanceHistoryViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeAttendanceHistory(viewModel, CRUDType.UPDATE, formCollection);
        }

        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _DeleteHREmployeeAttendanceHistoryAsync(HREmployeeAttendanceHistoryViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeAttendanceHistory(viewModel, CRUDType.DELETE, formCollection);
        }

        [NonAction]
        protected async Task<ActionResult> RETSourceHREmployeeAttendanceHistory(HREmployeeAttendanceHistoryViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
                viewModel.BreadCrumbArea = "SystemSecurity";
                viewModel.BreadCrumbController = "HREmployeeAttendanceHistory";
                viewModel.BreadCrumbBaseURL = "SystemSecurity/HREmployeeAttendanceHistory";
                viewModel.DDLEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeId, viewModel.DataModel.IdHREmployee);
                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        viewModel.SubmitButtonText = SubmitButtonText.Create;
                        viewModel.BreadCrumbActionName = "_CreateHREmployeeAttendanceHistoryAsync";
                        viewModel.ModalTitle = "नयाँ हाजिरी विवरण थप्नुहोस्";
                        viewModel.FormModelName = "HREmployeeAttendanceHistory";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.UPDATE:
                        viewModel.BreadCrumbActionName = "_UpdateHREmployeeAttendanceHistoryAsync";
                        viewModel.ModalTitle = "हाजिरी विवरण सम्पादन गर्नुहोस्";
                        viewModel.FormModelName = "HREmployeeAttendanceHistory";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.DELETE:
                        viewModel.BreadCrumbActionName = "_DeleteHREmployeeAttendanceHistoryAsync";
                        viewModel.ModalTitle = "हाजिरी विवरण मेटाउन निश्चित हुनुहुन्छ?";
                        viewModel.FormModelName = "HREmployeeAttendanceHistory";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.PRINT:
                        viewModel.BreadCrumbActionName = "_PrintHREmployeeAttendanceHistoryAsync";
                        viewModel.ModalTitle = "हाजिरी विवरण सुची छाप्नुहोस्";
                        viewModel.FormModelName = "HREmployeeAttendanceHistory";
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
        protected async Task<ActionResult> CRUDHREmployeeAttendanceHistory(HREmployeeAttendanceHistoryViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceHREmployeeAttendanceHistory(viewModel, cRUDType);
                }
                viewModel.DataModel.UpdatedOn = DateTime.Now;
                await _HREmployeeAttendanceHistoryServices.CommitSaveChangesAsync(viewModel.DataModel, cRUDType);
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceHREmployeeAttendanceHistory(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListHREmployeeAttendanceHistoryAsync", new { pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }
    }
}