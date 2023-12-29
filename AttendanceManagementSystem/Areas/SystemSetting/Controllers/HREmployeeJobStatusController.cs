using AttendanceManagementSystem.Controllers;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using SystemModels.SystemSetting;
using SystemServices.SystemSetting;
using SystemViewModels.SystemSetting;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;

namespace AttendanceManagementSystem.Areas.SystemSetting.Controllers
{
    public class HREmployeeJobStatusController : BaseController
    {
        // GET: SystemSetting/HREmployeeJobStatus
        private readonly HREmployeeJobStatusServices _hREmployeeJobStatusServices;

        public HREmployeeJobStatusController()
        {
            this._hREmployeeJobStatusServices = new HREmployeeJobStatusServices(this._unitOfWork);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> Index(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return View(new HREmployeeJobStatusViewModelList
                {
                    DBModelList = await _hREmployeeJobStatusServices.GetPagedList(pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "HREmployeeJobStatus",
                    BreadCrumbBaseURL = "SystemSetting/HREmployeeJobStatus",
                    BreadCrumbTitle = "कर्मचारीको जागिर अवस्था",
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
        public async Task<ActionResult> _ListHREmployeeJobStatusAsync(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HREmployeeJobStatusViewModelList
                {
                    DBModelList = await _hREmployeeJobStatusServices.GetsBySearchKey(pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, searchKey=""),
                    BreadCrumbTitle = "Employee Job Status",
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "HREmployeeJobStatus",
                    BreadCrumbActionName = "_ListHREmployeeJobStatusAsync",
                    BreadCrumbBaseURL = "SystemSetting/HREmployeeJobStatus",
                    CRUDAction = CRUDType.READ,
                    HeaderTitle = "Attendance System Management",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection
                });
            }
            catch (Exception exp)
            {
                throw new ArgumentException(exp.Message);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _CreateHREmployeeJobStatusAsync()
        {
            try
            {
                return PartialView(new HREmployeeJobStatusViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "HREmployeeJobStatus",
                    BreadCrumbBaseURL = "SystemSetting/HREmployeeJobStatus",
                    BreadCrumbTitle = "Employee Job Status",
                    BreadCrumbActionName = "_CreateHREmployeeJobStatusAsync",
                    FormModelName = "HREmployeeJobStatus",
                    ModalTitle = "नयाँ जागिर अवस्था थप्नुहोस्",
                    CRUDAction = CRUDType.CREATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Create,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = new HREmployeeJobStatusModel { Id = 0 }
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadHREmployeeJobStatusAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HREmployeeJobStatusViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "HREmployeeJobStatus",
                    BreadCrumbBaseURL = "SystemSetting/HREmployeeJobStatus",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_ReadHREmployeeJobStatusAsync",
                    FormModelName = "HREmployeeJobStatus",
                    ModalTitle = "जागिर अवस्था विवरण हेर्नुहोस्",
                    BreadCrumbTitle = "Job Status",
                    CRUDAction = CRUDType.READ,
                    OnlyCancelButton = true,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = await _hREmployeeJobStatusServices.GetModelByIdAsync(id)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _UpdateHREmployeeJobStatusAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HREmployeeJobStatusViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "HREmployeeJobStatus",
                    BreadCrumbBaseURL = "SystemSetting/HREmployeeJobStatus",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_UpdateHREmployeeJobStatusAsync",
                    FormModelName = "HREmployeeJobStatus",
                    ModalTitle = "जागिर अवस्था सम्पादन गर्नुहोस्",
                    BreadCrumbTitle = "Job Status",
                    CRUDAction = CRUDType.UPDATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Update,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = await _hREmployeeJobStatusServices.GetModelByIdAsync(id)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _DeleteHREmployeeJobStatusAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HREmployeeJobStatusViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "HREmployeeJobStatus",
                    BreadCrumbBaseURL = "SystemSetting/HREmployeeJobStatus",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    FormModelName = "HREmployeeJobStatus",
                    ModalTitle = "जागिर अवस्था मेटाउन निश्चित हुनुहुन्छ?",
                    BreadCrumbTitle = "Office Management",
                    BreadCrumbActionName = "_DeleteHREmployeeJobStatusAsync",
                    CRUDAction = CRUDType.DELETE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Delete,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.HRCompanyWithCode),
                    DataModel = await _hREmployeeJobStatusServices.GetModelByIdAsync(id)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ExportHREmployeeJobStatusAsync()
        {
            try
            {
                var model = await _hREmployeeJobStatusServices.FindAllAsync(x => x.IsActive);
                return await ExportToExcel("JobStatus", model);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintHREmployeeJobStatusAsync()
        {
            try
            {
                return PartialView(new HREmployeeJobStatusViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "HREmployeeJobStatus",
                    BreadCrumbBaseURL = "SystemSetting/HREmployeeJobStatus",
                    BreadCrumbActionName = "_PrintHREmployeeJobStatusAsync",
                    FormModelName = "HREmployeeJobStatus",
                    ModalTitle = "जागिर अवस्था सुची छाप्नुहोस्",
                    BreadCrumbTitle = "Job Status",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBModelList = await _hREmployeeJobStatusServices.FindAllAsync(x => x.IsActive)
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
        public async Task<ActionResult> _CreateHREmployeeJobStatusAsync(HREmployeeJobStatusViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeJobStatusAsync(viewModel, CRUDType.CREATE, formCollection);
        }

        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _UpdateHREmployeeJobStatusAsync(HREmployeeJobStatusViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeJobStatusAsync(viewModel, CRUDType.UPDATE, formCollection);
        }

        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _DeleteHREmployeeJobStatusAsync(HREmployeeJobStatusViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeJobStatusAsync(viewModel, CRUDType.DELETE, formCollection);
        }

        [NonAction]
        protected async Task<ActionResult> RETSourceHREmployeeJobStatusAsync(HREmployeeJobStatusViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
                viewModel.HeaderTitle = "Attendance System Management";
                viewModel.BreadCrumbArea = "SystemSetting";
                viewModel.BreadCrumbController = "HREmployeeJobStatus";
                viewModel.BreadCrumbBaseURL = "SystemSetting/HREmployeeJobStatus";
                viewModel.DDLCompany = await GetLongStringPairModel(PairModelTitle.HRCompanyWithCode);
                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        viewModel.SubmitButtonText = SubmitButtonText.Create;
                        viewModel.BreadCrumbActionName = "_CreateHREmployeeJobStatusAsync";
                        viewModel.ModalTitle = "नयाँ जागिर अवस्था थप्नुहोस्";
                        viewModel.FormModelName = "HREmployeeJobStatus";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.UPDATE:
                        viewModel.BreadCrumbActionName = "_UpdateHREmployeeJobStatusAsync";
                        viewModel.ModalTitle = "जागिर अवस्था सम्पादन गर्नुहोस्";
                        viewModel.FormModelName = "HREmployeeJobStatus";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.DELETE:
                        viewModel.BreadCrumbActionName = "_DeleteHREmployeeJobStatusAsync";
                        viewModel.ModalTitle = "जागिर अवस्था मेटाउन निश्चित हुनुहुन्छ?";
                        viewModel.FormModelName = "HREmployeeJobStatus";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.PRINT:
                        viewModel.BreadCrumbActionName = "_PrintHREmployeeJobStatusAsync";
                        viewModel.ModalTitle = "जागिर अवस्था सुची छाप्नुहोस्";
                        viewModel.FormModelName = "HREmployeeJobStatus";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                }
                return null;
            }
            catch (Exception exp)
            {
                throw new ArgumentException(exp.Message);
            }
        }

        [NonAction]
        protected async Task<ActionResult> CRUDHREmployeeJobStatusAsync(HREmployeeJobStatusViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceHREmployeeJobStatusAsync(viewModel, cRUDType);
                }
                await _hREmployeeJobStatusServices.CommitSaveChangesAsync(viewModel.DataModel, cRUDType);
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceHREmployeeJobStatusAsync(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListHREmployeeJobStatusAsync", new { pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }
    }
}