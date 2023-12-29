using AttendanceManagementSystem.Controllers;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using SystemModels.SystemSecurity;
using SystemServices.SystemSecurity;
using SystemViewModels.SystemSecurity;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;

namespace AttendanceManagementSystem.Areas.SystemSecurity.Controllers
{
    public class SystemDetailCategoryController : BaseController
    {
        // GET: SystemSecurity/SystemDetailCategory

        private readonly SystemDetailCategoryServices _SystemDetailCategoryServices;

        public SystemDetailCategoryController()
        {
            _SystemDetailCategoryServices = new SystemDetailCategoryServices(this._unitOfWork);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> Index(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return View(new SystemDetailCategoryViewModelList
                {
                    DBModelList = await _SystemDetailCategoryServices.GetPagedList(pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "SystemDetailCategory",
                    BreadCrumbBaseURL = "SystemSecurity/SystemDetailCategory",
                    BreadCrumbTitle = "प्रणाली विवरण प्रकार",
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
        public async Task<ActionResult> _ListSystemDetailCategoryAsync(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new SystemDetailCategoryViewModelList
                {
                    DBModelList = await _SystemDetailCategoryServices.GetBySearchKey(pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, searchKey = ""),
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "SystemDetailCategory",
                    BreadCrumbBaseURL = "SystemSecurity/SystemDetailCategory",
                    BreadCrumbActionName = "_ListSystemDetailCategoryAsync",
                    BreadCrumbTitle = "System Detail Category",
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
        public async Task<ActionResult> _CreateSystemDetailCategoryAsync()
        {
            try
            {
                return PartialView(new SystemDetailCategoryViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "SystemDetailCategory",
                    BreadCrumbBaseURL = "SystemSecurity/SystemDetailCategory",
                    BreadCrumbActionName = "_CreateSystemDetailCategoryAsync",
                    FormModelName = "SystemDetailCategory",
                    ModalTitle = "नयाँ प्रणालीको प्रकार थप्नुहोस्",
                    BreadCrumbTitle = "System Detail Category",
                    CRUDAction = CRUDType.CREATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Create,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = new SystemDetailCategoryModel { Id = 0 }
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadSystemDetailCategoryAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _SystemDetailCategoryServices.GetModelByIdAsync(id);
                return PartialView(new SystemDetailCategoryViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "SystemDetailCategory",
                    BreadCrumbBaseURL = "SystemSecurity/SystemDetailCategory",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_ReadSystemDetailCategoryAsync",
                    FormModelName = "SystemDetailCategory",
                    ModalTitle = "प्रणालीको प्रकार हेर्नुहोस्",
                    BreadCrumbTitle = "System Detail Category",
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
        public async Task<ActionResult> _UpdateSystemDetailCategoryAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _SystemDetailCategoryServices.GetModelByIdAsync(id);
                return PartialView(new SystemDetailCategoryViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "SystemDetailCategory",
                    BreadCrumbBaseURL = "SystemSecurity/SystemDetailCategory",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_UpdateSystemDetailCategoryAsync",
                    FormModelName = "SystemDetailCategory",
                    ModalTitle = "प्रणालीको प्रकार सम्पादन गर्नुहोस्",
                    BreadCrumbTitle = "System Detail Category",
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
        public async Task<ActionResult> _DeleteSystemDetailCategoryAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _SystemDetailCategoryServices.GetModelByIdAsync(id);
                return PartialView(new SystemDetailCategoryViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "SystemDetailCategory",
                    BreadCrumbBaseURL = "SystemSecurity/SystemDetailCategory",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    FormModelName = "SystemDetailCategory",
                    ModalTitle = "प्रणालीको प्रकार मेटाउन निश्चित हुनुहुन्छ?",
                    BreadCrumbTitle = "System Detail Category",
                    BreadCrumbActionName = "_DeleteSystemDetailCategoryAsync",
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
        public async Task<ActionResult> _ExportSystemDetailCategoryAsync()
        {
            try
            {
                var model = await _SystemDetailCategoryServices.FindAllAsync(x => x.IsActive);
                return await ExportToExcel("SystemDetailCategory", model);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintSystemDetailCategoryAsync()
        {
            try
            {
                return PartialView(new SystemDetailCategoryViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "SystemDetailCategory",
                    BreadCrumbBaseURL = "SystemSecurity/SystemDetailCategory",
                    BreadCrumbActionName = "_PrintSystemDetailCategoryAsync",
                    FormModelName = "SystemDetailCategory",
                    ModalTitle = "प्रणालीको प्रकार सुची छाप्नुहोस्",
                    BreadCrumbTitle = "System Detail Category",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBModelList = await _SystemDetailCategoryServices.FindAllAsync(x => x.IsActive)
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
        public async Task<ActionResult> _CreateSystemDetailCategoryAsync(SystemDetailCategoryViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDSystemDetailCategory(viewModel, CRUDType.CREATE, formCollection);
        }

        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _UpdateSystemDetailCategoryAsync(SystemDetailCategoryViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDSystemDetailCategory(viewModel, CRUDType.UPDATE, formCollection);
        }

        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _DeleteSystemDetailCategoryAsync(SystemDetailCategoryViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDSystemDetailCategory(viewModel, CRUDType.DELETE, formCollection);
        }

        [NonAction]
        protected async Task<ActionResult> RETSourceSystemDetailCategory(SystemDetailCategoryViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
                viewModel.BreadCrumbArea = "SystemSecurity";
                viewModel.BreadCrumbController = "SystemDetailCategory";
                viewModel.BreadCrumbBaseURL = "SystemSecurity/SystemDetailCategory";
                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        viewModel.SubmitButtonText = SubmitButtonText.Create;
                        viewModel.BreadCrumbActionName = "_CreateSystemDetailCategoryAsync";
                        viewModel.ModalTitle = "नयाँ प्रणालीको प्रकार थप्नुहोस्";
                        viewModel.FormModelName = "SystemDetailCategory";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.UPDATE:
                        viewModel.BreadCrumbActionName = "_UpdateSystemDetailCategoryAsync";
                        viewModel.ModalTitle = "प्रणालीको प्रकार सम्पादन गर्नुहोस्";
                        viewModel.FormModelName = "SystemDetailCategory";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.DELETE:
                        viewModel.BreadCrumbActionName = "_DeleteSystemDetailCategoryAsync";
                        viewModel.ModalTitle = "प्रणालीको प्रकार मेटाउन निश्चित हुनुहुन्छ?";
                        viewModel.FormModelName = "SystemDetailCategory";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.PRINT:
                        viewModel.BreadCrumbActionName = "_PrintSystemDetailCategoryAsync";
                        viewModel.ModalTitle = "प्रणालीको प्रकार सुची छाप्नुहोस्";
                        viewModel.FormModelName = "SystemDetailCategory";
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
        protected async Task<ActionResult> CRUDSystemDetailCategory(SystemDetailCategoryViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceSystemDetailCategory(viewModel, cRUDType);
                }
                await _SystemDetailCategoryServices.CommitSaveChangesAsync(viewModel.DataModel, cRUDType);
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceSystemDetailCategory(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListSystemDetailCategoryAsync", new { pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }
    }
}