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
    public class HREmployeeContactTypeController : BaseController
    {

        // GET: SystemSetting/HREmployeeContactType

        private readonly HREmployeeContactTypeServices _HREmployeeContactTypeServices;

        public HREmployeeContactTypeController()
        {
            _HREmployeeContactTypeServices = new HREmployeeContactTypeServices(this._unitOfWork);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> Index(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return View(new HREmployeeContactTypeViewModelList
                {
                    DBModelList = await _HREmployeeContactTypeServices.GetPagedList(pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "HREmployeeContactType",
                    BreadCrumbBaseURL = "SystemSetting/HREmployeeContactType",
                    BreadCrumbTitle = "सम्पर्क",
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
        public async Task<ActionResult> _ListHREmployeeContactTypeAsync(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HREmployeeContactTypeViewModelList
                {
                    DBModelList = await _HREmployeeContactTypeServices.GetsBySearchKey(pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, searchKey = ""),
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "HREmployeeContactType",
                    BreadCrumbBaseURL = "SystemSetting/HREmployeeContactType",
                    BreadCrumbActionName = "_ListHREmployeeContactTypeAsync",
                    BreadCrumbTitle = "Contact",
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
        public async Task<ActionResult> _CreateHREmployeeContactTypeAsync()
        {
            try
            {
                return PartialView(new HREmployeeContactTypeViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "HREmployeeContactType",
                    BreadCrumbBaseURL = "SystemSetting/HREmployeeContactType",
                    BreadCrumbActionName = "_CreateHREmployeeContactTypeAsync",
                    FormModelName = "HREmployeeContactType",
                    ModalTitle = "नयाँ सम्पर्क विवरण थप्नुहोस्",
                    BreadCrumbTitle = "Contact",
                    CRUDAction = CRUDType.CREATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Create,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = new HREmployeeContactTypeModel { Id=0},
                    DDLContactType = await GetLongStringPairModel(PairModelTitle.ContactType)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadHREmployeeContactTypeAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _HREmployeeContactTypeServices.GetModelByIdAsync(id);
                return PartialView(new HREmployeeContactTypeViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "HREmployeeContactType",
                    BreadCrumbBaseURL = "SystemSetting/HREmployeeContactType",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_ReadHREmployeeContactTypeAsync",
                    FormModelName = "HREmployeeContactType",
                    ModalTitle = "कर्मचारी सम्पर्क विवरण हेर्नुहोस्",
                    BreadCrumbTitle = "Contact",
                    CRUDAction = CRUDType.READ,
                    OnlyCancelButton = true,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLContactType = await GetLongStringPairModel(PairModelTitle.ContactType),
                    DataModel = model
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _UpdateHREmployeeContactTypeAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _HREmployeeContactTypeServices.GetModelByIdAsync(id);
                return PartialView(new HREmployeeContactTypeViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "HREmployeeContactType",
                    BreadCrumbBaseURL = "SystemSetting/HREmployeeContactType",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_UpdateHREmployeeContactTypeAsync",
                    FormModelName = "HREmployeeContactType",
                    ModalTitle = "सम्पर्क विवरण सम्पादन गर्नुहोस्",
                    BreadCrumbTitle = "Contact",
                    CRUDAction = CRUDType.UPDATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Update,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLContactType = await GetLongStringPairModel(PairModelTitle.ContactType),
                    DataModel = model
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _DeleteHREmployeeContactTypeAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _HREmployeeContactTypeServices.GetModelByIdAsync(id);
                return PartialView(new HREmployeeContactTypeViewModel
                {
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "HREmployeeContactType",
                    BreadCrumbBaseURL = "SystemSetting/HREmployeeContactType",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    FormModelName = "HREmployeeContactType",
                    ModalTitle = "सम्पर्क विवरण मेटाउन निश्चित हुनुहुन्छ?",
                    BreadCrumbTitle = "Contact",
                    BreadCrumbActionName = "_DeleteHREmployeeContactTypeAsync",
                    CRUDAction = CRUDType.DELETE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Delete,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLContactType = await GetLongStringPairModel(PairModelTitle.ContactType),
                    DataModel = model
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ExportHREmployeeContactTypeAsync()
        {
            try
            {
                var model = await _HREmployeeContactTypeServices.FindAllAsync(x => x.IsActive);
                return await ExportToExcel("HREmployeeContactType", model);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintHREmployeeContactTypeAsync()
        {
            try
            {
                return PartialView(new HREmployeeContactTypeViewModel
                {
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "HREmployeeContactType",
                    BreadCrumbBaseURL = "SystemSetting/HREmployeeContactType",
                    BreadCrumbActionName = "_PrintHREmployeeContactTypeAsync",
                    FormModelName = "HREmployeeContactType",
                    ModalTitle = "सम्पर्क विवरण सुची छाप्नुहोस्",
                    BreadCrumbTitle = "Contact",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBModelList = await _HREmployeeContactTypeServices.FindAllAsync(x => x.IsActive)
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
        public async Task<ActionResult> _CreateHREmployeeContactTypeAsync(HREmployeeContactTypeViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeContactType(viewModel, CRUDType.CREATE, formCollection);
        }

        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _UpdateHREmployeeContactTypeAsync(HREmployeeContactTypeViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeContactType(viewModel, CRUDType.UPDATE, formCollection);
        }

        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _DeleteHREmployeeContactTypeAsync(HREmployeeContactTypeViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeContactType(viewModel, CRUDType.DELETE, formCollection);
        }

        [NonAction]
        protected async Task<ActionResult> RETSourceHREmployeeContactType(HREmployeeContactTypeViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
                viewModel.BreadCrumbArea = "SystemSetting";
                viewModel.BreadCrumbController = "HREmployeeContactType";
                viewModel.BreadCrumbBaseURL = "SystemSetting/HREmployeeContactType";
                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        viewModel.SubmitButtonText = SubmitButtonText.Create;
                        viewModel.BreadCrumbActionName = "_CreateHREmployeeContactTypeAsync";
                        viewModel.ModalTitle = "नयाँ सम्पर्क विवरण थप्नुहोस्";
                        viewModel.FormModelName = "HREmployeeContactType";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.UPDATE:
                        viewModel.BreadCrumbActionName = "_UpdateHREmployeeContactTypeAsync";
                        viewModel.ModalTitle = "सम्पर्क विवरण सम्पादन गर्नुहोस्";
                        viewModel.FormModelName = "HREmployeeContactType";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.DELETE:
                        viewModel.BreadCrumbActionName = "_DeleteHREmployeeContactTypeAsync";
                        viewModel.ModalTitle = "सम्पर्क विवरण मेटाउन निश्चित हुनुहुन्छ?";
                        viewModel.FormModelName = "HREmployeeContactType";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.PRINT:
                        viewModel.BreadCrumbActionName = "_PrintHREmployeeContactTypeAsync";
                        viewModel.ModalTitle = "सम्पर्क विवरण सुची छाप्नुहोस्";
                        viewModel.FormModelName = "HREmployeeContactType";
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
        protected async Task<ActionResult> CRUDHREmployeeContactType(HREmployeeContactTypeViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceHREmployeeContactType(viewModel, cRUDType);
                }
                await _HREmployeeContactTypeServices.CommitSaveChangesAsync(viewModel.DataModel, cRUDType);
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceHREmployeeContactType(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListHREmployeeContactTypeAsync", new { pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }
    }
}
    
