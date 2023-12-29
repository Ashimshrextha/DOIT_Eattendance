using AttendanceManagementSystem.Controllers;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using SystemModels.SystemSetting;
using SystemServices.SystemSecurity;
using SystemViewModels.SystemSetting;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;

namespace AttendanceManagementSystem.Areas.SystemSetting.Controllers
{
    public class HRLanguageController : BaseController
    {
        // GET: SystemSetting/HRLanguage

        private readonly HRLanguageServices _HRLanguageServices;

        public HRLanguageController()
        {
            _HRLanguageServices = new HRLanguageServices(this._unitOfWork);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> Index(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return View(new HRLanguageViewModelList
                {
                    DBModelList = await _HRLanguageServices.GetPagedList(pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "HRLanguage",
                    BreadCrumbBaseURL = "SystemSetting/HRLanguage",
                    BreadCrumbTitle = "भाषा",
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
        public async Task<ActionResult> _ListHRLanguageAsync(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRLanguageViewModelList
                {
                    DBModelList = await _HRLanguageServices.GetsBySearchKey(pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, searchKey = ""),
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "HRLanguage",
                    BreadCrumbBaseURL = "SystemSetting/HRLanguage",
                    BreadCrumbActionName = "_ListHRLanguageAsync",
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
        public async Task<ActionResult> _CreateHRLanguageAsync()
        {
            try
            {
                return PartialView(new HRLanguageViewModel
                {
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "HRLanguage",
                    BreadCrumbBaseURL = "SystemSetting/HRLanguage",
                    BreadCrumbActionName = "_CreateHRLanguageAsync",
                    FormModelName = "HRLanguage",
                    ModalTitle = "नयाँ भाषा थप्नुहोस्",
                    CRUDAction = CRUDType.CREATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Create,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = new HRLanguageModel { Id = 0 }
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadHRLanguageAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _HRLanguageServices.GetModelByIdAsync(id);
                return PartialView(new HRLanguageViewModel
                {
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "HRLanguage",
                    BreadCrumbBaseURL = "SystemSetting/HRLanguage",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_ReadHRLanguageAsync",
                    FormModelName = "HRLanguage",
                    ModalTitle = "कर्मचारीको भाषा विवरण हेर्नुहोस्",
                    CRUDAction = CRUDType.READ,
                    OnlyCancelButton = true,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = model
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _UpdateHRLanguageAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _HRLanguageServices.GetModelByIdAsync(id);
                return PartialView(new HRLanguageViewModel
                {
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "HRLanguage",
                    BreadCrumbBaseURL = "SystemSetting/HRLanguage",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_UpdateHRLanguageAsync",
                    FormModelName = "HRLanguage",
                    ModalTitle = "भाषा सम्पादन गर्नुहोस्",
                    CRUDAction = CRUDType.UPDATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Update,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = model
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _DeleteHRLanguageAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _HRLanguageServices.GetModelByIdAsync(id);
                return PartialView(new HRLanguageViewModel
                {
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "HRLanguage",
                    BreadCrumbBaseURL = "SystemSetting/HRLanguage",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    FormModelName = "HRLanguage",
                    ModalTitle = "भाषा मेटाउन निश्चित हुनुहुन्छ?",
                    BreadCrumbActionName = "_DeleteHRLanguageAsync",
                    CRUDAction = CRUDType.DELETE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Delete,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = model
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ExportHRLanguageAsync()
        {
            try
            {
                var model = await _HRLanguageServices.FindAllAsync(x => x.IsActive);
                return await ExportToExcel("HRLanguage", model);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintHRLanguageAsync()
        {
            try
            {
                return PartialView(new HRLanguageViewModel
                {
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "HRLanguage",
                    BreadCrumbBaseURL = "SystemSetting/HRLanguage",
                    BreadCrumbActionName = "_PrintHRLanguageAsync",
                    FormModelName = "HRLanguage",
                    ModalTitle = "भाषा सुची छाप्नुहोस्",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBModelList = await _HRLanguageServices.FindAllAsync(x => x.IsActive)
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
        public async Task<ActionResult> _CreateHRLanguageAsync(HRLanguageViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHRLanguage(viewModel, CRUDType.CREATE, formCollection);
        }

        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _UpdateHRLanguageAsync(HRLanguageViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHRLanguage(viewModel, CRUDType.UPDATE, formCollection);
        }

        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _DeleteHRLanguageAsync(HRLanguageViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHRLanguage(viewModel, CRUDType.DELETE, formCollection);
        }

        [NonAction]
        protected async Task<ActionResult> RETSourceHRLanguage(HRLanguageViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
                viewModel.BreadCrumbArea = "SystemSetting";
                viewModel.BreadCrumbController = "HRLanguage";
                viewModel.BreadCrumbBaseURL = "SystemSetting/HRLanguage";
                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        viewModel.BreadCrumbActionName = "_CreateHRLanguageAsync";
                        viewModel.ModalTitle = "नयाँ भाषा थप्नुहोस्";
                        viewModel.FormModelName = "HRLanguage";
                        viewModel.SubmitButtonText = SubmitButtonText.Create;
                        viewModel.CRUDAction = CRUDType.CREATE;
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.UPDATE:
                        viewModel.BreadCrumbActionName = "_UpdateHRLanguageAsync";
                        viewModel.ModalTitle = "भाषा मेटाउन निश्चित हुनुहुन्छ?";
                        viewModel.FormModelName = "HRLanguage";
                        viewModel.SubmitButtonText = SubmitButtonText.Update;
                        viewModel.CRUDAction = CRUDType.UPDATE;
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.DELETE:
                        viewModel.BreadCrumbActionName = "_DeleteHRLanguageAsync";
                        viewModel.ModalTitle = "भाषा मेटाउन निश्चित हुनुहुन्छ?";
                        viewModel.FormModelName = "HRLanguage";
                        viewModel.SubmitButtonText = SubmitButtonText.Delete;
                        viewModel.CRUDAction = CRUDType.DELETE;
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    default:
                        return null;
                }
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [NonAction]
        protected async Task<ActionResult> CRUDHRLanguage(HRLanguageViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceHRLanguage(viewModel, cRUDType);
                }
                await _HRLanguageServices.CommitSaveChangesAsync(viewModel.DataModel, cRUDType);
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceHRLanguage(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListHRLanguageAsync", new { pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }
    }
}