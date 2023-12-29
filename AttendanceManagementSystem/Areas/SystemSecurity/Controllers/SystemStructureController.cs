using AttendanceManagementSystem.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SystemServices.SystemSecurity;
using SystemUnitOfWork.Interfaces;
using SystemUnitOfWork.UOW;
using SystemViewModels.SystemSecurity;
using static SystemStores.GlobalMethod.SystemGlobalMethod;
using static SystemStores.ENUMData.EnumGlobal;
using System.Net;
using SystemModels.SystemSecurity;
using AttendanceManagementSystem.App_Auth;

namespace AttendanceManagementSystem.Areas.SystemSecurity.Controllers
{
    public class SystemStructureController : BaseController
    {
        // GET: SystemSecurity/SystemStructure
        private readonly SystemStructureServices _systemStructureServices;

        public SystemStructureController()
        {
            this._systemStructureServices = new SystemStructureServices(this._unitOfWork);
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> Index(int? pageNumber, int? pageSize, string orderingDirection, string orderingBy = "IdSystemMenu")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return View(new SystemStructureViewModelList
                {
                    DBModelList = await _systemStructureServices.GetPagedList(pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "SystemStructure",
                    BreadCrumbBaseURL = "SystemSecurity/SystemStructure",
                    BreadCrumbTitle = "प्रणाली संरचना",
                    BreadCrumbActionName = "Index",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = "IdSystemMenu",
                    OrderingDirection = pagination.OrderingDirection
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListSystemStructureAsync(int? pageNumber, int? pageSize, string orderingDirection, string orderingBy = "IdSystemMenu", string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new SystemStructureViewModelList
                {
                    DBModelList = await _systemStructureServices.GetBySearchKey(pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, searchKey),
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "SystemStructure",
                    BreadCrumbBaseURL = "SystemSecurity/SystemStructure",
                    BreadCrumbActionName = "_ListSystemStructureAsync",
                    BreadCrumbTitle = "System Structures",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = "IdSystemMenu",
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey=searchKey
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _CreateSystemStructureAsync()
        {
            try
            {
                return PartialView(new SystemStructureViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "SystemStructure",
                    BreadCrumbBaseURL = "SystemSecurity/SystemStructure",
                    BreadCrumbActionName = "_CreateSystemStructureAsync",
                    FormModelName = "SystemStructure",
                    ModalTitle = "नयाँ प्रणाली संरचना थप्नुहोस् ",
                    BreadCrumbTitle = "System Structure",
                    CRUDAction = CRUDType.CREATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Create,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLSystemMenu = await GetIntStringPairModel(PairModelTitle.SystemMenu),
                    DDLSystemStructure = await GetIntStringPairModel(PairModelTitle.SystemStructure),
                    DataModel = new SystemStructureModel { Id = 0 }
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadSystemStructureAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new SystemStructureViewModel
                {
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "SystemStructure",
                    BreadCrumbBaseURL = "SystemSecurity/SystemStructure",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_ReadSystemStructureAsync",
                    FormModelName = "SystemStructure",
                    ModalTitle = "प्रणाली संरचना हेर्नुहोस्",
                    BreadCrumbTitle = "System Structure",
                    CRUDAction = CRUDType.READ,
                    OnlyCancelButton = true,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = await _systemStructureServices.GetModelByIdAsync(id),
                    DDLSystemStructure = await GetIntStringPairModel(PairModelTitle.SystemStructure),
                    DDLSystemMenu = await GetIntStringPairModel(PairModelTitle.SystemMenu)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _UpdateSystemStructureAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new SystemStructureViewModel
                {
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "SystemStructure",
                    BreadCrumbBaseURL = "SystemSecurity/SystemStructure",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_UpdateSystemStructureAsync",
                    FormModelName = "SystemStructure",
                    ModalTitle = "प्रणाली संरचना सम्पादन गर्नुहोस्",
                    BreadCrumbTitle = "System Structure",
                    CRUDAction = CRUDType.UPDATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Update,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = await _systemStructureServices.GetModelByIdAsync(id),
                    DDLSystemStructure = await GetIntStringPairModel(PairModelTitle.SystemStructure),
                    DDLSystemMenu = await GetIntStringPairModel(PairModelTitle.SystemMenu)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _DeleteSystemStructureAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null||id==0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new SystemStructureViewModel
                {
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "SystemStructure",
                    BreadCrumbBaseURL = "SystemSecurity/SystemStructure",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    FormModelName = "SystemStructure",
                    ModalTitle = "प्रणाली संरचना मेटाउन निश्चित हुनुहुन्छ?",
                    BreadCrumbTitle = "System Structure",
                    BreadCrumbActionName = "_DeleteSystemStructureAsync",
                    CRUDAction = CRUDType.DELETE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Delete,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = await _systemStructureServices.GetModelByIdAsync(id),
                    DDLSystemStructure = await GetIntStringPairModel(PairModelTitle.SystemStructure),
                    DDLSystemMenu = await GetIntStringPairModel(PairModelTitle.SystemMenu)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ExportSystemStructureAsync()
        {
            try
            {
                var model = await _systemStructureServices.FindAllAsync(x => x.IsActive);
                return await ExportToExcel("SystemStructure", model);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintSystemStructureAsync()
        {
            try
            {
                return PartialView(new SystemStructureViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "SystemStructure",
                    BreadCrumbBaseURL = "SystemSecurity/SystemStructure",
                    BreadCrumbActionName = "_PrintSystemStructureAsync",
                    FormModelName = "SystemStructure",
                    ModalTitle = "प्रणाली संरचना सुची छाप्नुहोस्",
                    BreadCrumbTitle = "System Structure",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBModelList = await _systemStructureServices.FindAllAsync(x => x.IsActive)
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
        public async Task<ActionResult> _CreateSystemStructureAsync(SystemStructureViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDSystemStructure(viewModel, CRUDType.CREATE, formCollection);
        }

        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _UpdateSystemStructureAsync(SystemStructureViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDSystemStructure(viewModel, CRUDType.UPDATE, formCollection);
        }

        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _DeleteSystemStructureAsync(SystemStructureViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDSystemStructure(viewModel, CRUDType.DELETE, formCollection);
        }

        [NonAction]
        protected async Task<ActionResult> RETSourceSystemStructure(SystemStructureViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
                viewModel.BreadCrumbArea = "SystemSecurity";
                viewModel.BreadCrumbController = "SystemStructure";
                viewModel.BreadCrumbBaseURL = "SystemSecurity/SystemStructure";
                viewModel.DDLSystemMenu = await GetIntStringPairModel(PairModelTitle.SystemMenu);
                viewModel.DDLSystemStructure = await GetIntStringPairModel(PairModelTitle.SystemStructureBySystemMenu,viewModel.DataModel.IdSystemMenu);
                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        viewModel.SubmitButtonText = SubmitButtonText.Create;
                        viewModel.BreadCrumbActionName = "_CreateSystemStructureAsync";
                        viewModel.ModalTitle = "नयाँ प्रणाली संरचना थप्नुहोस्";
                        viewModel.FormModelName = "SystemStructure";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.UPDATE:
                        viewModel.SubmitButtonText = SubmitButtonText.Update;
                        viewModel.BreadCrumbActionName = "_UpdateSystemStructureAsync";
                        viewModel.ModalTitle = "प्रणाली संरचना सम्पादन गर्नुहोस्";
                        viewModel.FormModelName = "SystemStructure";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.DELETE:
                        viewModel.SubmitButtonText = SubmitButtonText.Delete;
                        viewModel.BreadCrumbActionName = "_DeleteSystemStructureAsync";
                        viewModel.ModalTitle = "प्रणाली संरचना मेटाउन निश्चित हुनुहुन्छ?";
                        viewModel.FormModelName = "SystemStructure";
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
        protected async Task<ActionResult> CRUDSystemStructure(SystemStructureViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceSystemStructure(viewModel, cRUDType);
                }
                await _systemStructureServices.CommitSaveChangesAsync(viewModel.DataModel, cRUDType);
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceSystemStructure(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListSystemStructureAsync", new { pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }
    }
}