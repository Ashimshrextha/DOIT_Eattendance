using AttendanceManagementSystem.Controllers;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using SystemModels.SystemSecurity;
using SystemServices.SystemSecurity;
using SystemViewModels.SystemSecurity;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;

namespace AttendanceManagementSystem.Areas.SystemSecurity.Controllers
{
    public class SystemMenuController : BaseController
    {
        // GET: SystemSecurity/SystemMenu

        private readonly SystemMenuServices _systemMenuServices;


        public SystemMenuController()
        {
            this._systemMenuServices = new SystemMenuServices(this._unitOfWork);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> Index(int? pageNumber, int? pageSize, string orderingDirection, string orderingBy = "Area")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return View(new SystemMenuViewModelList
                {
                    DBModelList = await _systemMenuServices.GetPagedList(pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    BreadCrumbTitle = "प्रणाली मेनु",
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "SystemMenu",
                    BreadCrumbActionName = "Index",
                    BreadCrumbBaseURL = "SystemSecurity/SystemMenu",
                    CRUDAction = CRUDType.READ,
                    HeaderTitle = "Attendance System Management",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = "Area",
                    OrderingDirection = pagination.OrderingDirection
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListSystemMenuAsync(int? pageNumber, int? pageSize, string orderingDirection, string orderingBy = "Area")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new SystemMenuViewModelList
                {
                    DBModelList = await _systemMenuServices.GetPagedList(pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    BreadCrumbTitle = "System Menu",
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "SystemMenu",
                    BreadCrumbActionName = "_ListSystemMenuAsync",
                    BreadCrumbBaseURL = "SystemSecurity/SystemMenu",
                    CRUDAction = CRUDType.READ,
                    HeaderTitle = "Attendance System Management",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = "Area",
                    OrderingDirection = pagination.OrderingDirection
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _CreateSystemMenuAsync()
        {
            try
            {
                return PartialView(new SystemMenuViewModel
                {
                    ModalTitle = "नयाँ प्रणाली मेनु थप्नुहोस्",
                    FormModelName = "SystemMenu",
                    BreadCrumbTitle = "System Menu",
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "SystemMenu",
                    BreadCrumbActionName = "_CreateSystemMenuAsync",
                    BreadCrumbBaseURL = "SystemSecurity/SystemMenu",
                    CRUDAction = CRUDType.CREATE,
                    HeaderTitle = "Attendance Management System",
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Create,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = new SystemMenuModel { Id = 0 }
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadSystemMenuAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                return PartialView(new SystemMenuViewModel
                {
                    ModalTitle = "प्रणाली मेनुको विवरण",
                    FormModelName = "SystemMenu",
                    BreadCrumbTitle = "System Menu",
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "SystemMenu",
                    BreadCrumbActionName = "_ReadSystemMenuAsync",
                    BreadCrumbBaseURL = "SystemSecurity/SystemMenu",
                    CRUDAction = CRUDType.READ,
                    HeaderTitle = "Attendance Management System",
                    OnlyCancelButton = true,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = await _systemMenuServices.GetModelByIdAsync(id)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _UpdateSystemMenuAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                return PartialView(new SystemMenuViewModel
                {
                    ModalTitle = "प्रणाली मेनु सम्पादन गर्नुहोस्",
                    FormModelName = "SystemMenu",
                    BreadCrumbTitle = "System Menu",
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "SystemMenu",
                    BreadCrumbActionName = "_UpdateSystemMenuAsync",
                    BreadCrumbBaseURL = "SystemSecurity/SystemMenu",
                    CRUDAction = CRUDType.UPDATE,
                    HeaderTitle = "Attendance Management System",
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Update,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = await _systemMenuServices.GetModelByIdAsync(id)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _DeleteSystemMenuAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                return PartialView(new SystemMenuViewModel
                {
                    ModalTitle = "प्रणाली मेनु मेटाउन निश्चित हुनुहुन्छ?",
                    FormModelName = "SystemMenu",
                    BreadCrumbTitle = "System Menu",
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "SystemMenu",
                    BreadCrumbActionName = "_DeleteSystemMenuAsync",
                    BreadCrumbBaseURL = "SystemSecurity/SystemMenu",
                    CRUDAction = CRUDType.READ,
                    HeaderTitle = "Attendance Management System",
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Delete,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = await _systemMenuServices.GetModelByIdAsync(id)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ExportSystemMenuAsync()
        {
            try
            {
                var model = await _systemMenuServices.FindAllAsync(x => x.IsActive);
                return await ExportToExcel("SystemMenu", model);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintSystemMenuAsync(int? id)
        {
            try
            {
                return PartialView(new SystemMenuViewModel
                {
                    ModalTitle = "प्रणाली मेनु सुची छाप्नुहोस्",
                    FormModelName = "SystemMenu",
                    BreadCrumbTitle = "System Menu",
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "SystemMenu",
                    BreadCrumbActionName = "_DeleteSystemMenuAsync",
                    BreadCrumbBaseURL = "SystemSecurity/SystemMenu",
                    CRUDAction = CRUDType.PRINT,
                    HeaderTitle = "Attendance Management System",
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBModelList = await _systemMenuServices.FindAllAsync(x => x.IsActive)
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
        public async Task<ActionResult> _CreateSystemMenuAsync(SystemMenuViewModel systemMenuViewModel, FormCollection formCollection)
        {
            return await CRUDSystemMenu(systemMenuViewModel, CRUDType.CREATE, formCollection);
        }

        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _UpdateSystemMenuAsync(SystemMenuViewModel systemMenuViewModel, FormCollection formCollection)
        {
            return await CRUDSystemMenu(systemMenuViewModel, CRUDType.UPDATE, formCollection);
        }

        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _DeleteSystemMenuAsync(SystemMenuViewModel systemMenuViewModel, FormCollection formCollection)
        {
            return await CRUDSystemMenu(systemMenuViewModel, CRUDType.DELETE, formCollection);
        }

        [NonAction]
        protected async Task<ActionResult> RETSourceSystemMenuAsync(SystemMenuViewModel systemMenuViewModel, CRUDType cRUDType)
        {
            try
            {
                systemMenuViewModel.FormModelName = "SystemMenu";
                systemMenuViewModel.BreadCrumbArea = "SystemSecurity";
                systemMenuViewModel.BreadCrumbController = "SystemMenu";
                systemMenuViewModel.BreadCrumbBaseURL = "SystemSecurity/SystemMenu";
                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        systemMenuViewModel.ModalTitle = "नयाँ प्रणाली मेनु थप्नुहोस्";
                        systemMenuViewModel.BreadCrumbActionName = "_CreateSystemMenuAsync";
                        return PartialView(systemMenuViewModel.BreadCrumbActionName, systemMenuViewModel);
                    case CRUDType.UPDATE:
                        systemMenuViewModel.ModalTitle = "प्रणाली मेनु सम्पादन गर्नुहोस्";
                        systemMenuViewModel.BreadCrumbActionName = "_UpdateSystemMenuAsync";
                        return PartialView(systemMenuViewModel.BreadCrumbActionName, systemMenuViewModel);
                    case CRUDType.DELETE:
                        systemMenuViewModel.ModalTitle = "प्रणाली मेनु मेटाउन निश्चित हुनुहुन्छ?";
                        systemMenuViewModel.BreadCrumbActionName = "_DeleteSystemMenuAsync";
                        return PartialView(systemMenuViewModel.BreadCrumbActionName, systemMenuViewModel);
                    case CRUDType.PRINT:
                        systemMenuViewModel.ModalTitle = "प्रणाली मेनु सुची छाप्नुहोस्";
                        systemMenuViewModel.BreadCrumbActionName = "_PrintSystemMenuAsync";
                        return PartialView(systemMenuViewModel.BreadCrumbActionName, systemMenuViewModel);
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
        protected async Task<ActionResult> CRUDSystemMenu(SystemMenuViewModel systemMenuViewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceSystemMenuAsync(systemMenuViewModel, cRUDType);
                }
                await _systemMenuServices.CommitSaveChangesAsync(systemMenuViewModel.DataModel, cRUDType);
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceSystemMenuAsync(systemMenuViewModel, cRUDType);
            }
            return RedirectToAction("_ListSystemMenuAsync", new { pageNumber = systemMenuViewModel.PageNumber, pageSize = systemMenuViewModel.PageSize, orderingBy = systemMenuViewModel.OrderingBy, orderingDirection = systemMenuViewModel.OrderingDirection, searchKey = systemMenuViewModel.SearchKey });
        }
    }
}