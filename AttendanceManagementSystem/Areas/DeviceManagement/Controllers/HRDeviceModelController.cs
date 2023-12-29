using AttendanceManagementSystem.Controllers;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using SystemModels.DeviceManagement;
using SystemServices.DeviceManagement;
using SystemViewModels.DeviceManagement;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;

namespace AttendanceManagementSystem.Areas.DeviceManagement.Controllers
{
    public class HRDeviceModelController : BaseController
    {
        // GET: DeviceManagement/HRDevice

        private readonly HRDeviceModelServices _HRDeviceModelServices;

        public HRDeviceModelController()
        {
            this._HRDeviceModelServices = new HRDeviceModelServices(this._unitOfWork);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> Index(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return View(new HRDeviceModelViewModelList
                {
                    DBModelList = await _HRDeviceModelServices.GetPagedList(pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "DeviceManagement",
                    BreadCrumbController = "HRDeviceModel",
                    BreadCrumbBaseURL = "DeviceManagement/HRDeviceModel",
                    BreadCrumbTitle = "उपकरण मोडेल",
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
                await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
                return View();
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListHRDeviceModelAsync(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRDeviceModelViewModelList
                {
                    DBModelList = await _HRDeviceModelServices.GetsBySearchKey(pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, searchKey),
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "DeviceManagement",
                    BreadCrumbController = "HRDeviceModel",
                    BreadCrumbBaseURL = "DeviceManagement/HRDeviceModel",
                    BreadCrumbActionName = "_ListHRDeviceModelAsync",
                    BreadCrumbTitle = "Device Model",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection
                });
            }
            catch (Exception exp)
            {
                await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
                return RedirectToAction("Index", new { pageNumber = pageNumber, pageSize = pageSize, orderingBy = orderingBy, orderingDirection = orderingDirection });
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _CreateHRDeviceModelAsync()
        {
            try
            {
                await Task.WhenAll();
                return PartialView(new HRDeviceModelViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "DeviceManagement",
                    BreadCrumbController = "HRDeviceModel",
                    BreadCrumbBaseURL = "DeviceManagement/HRDeviceModel",
                    BreadCrumbActionName = "_CreateHRDeviceModelAsync",
                    FormModelName = "HRDeviceModel",
                    ModalTitle = "नयाँ उपकरण मोडेल थप्नुहोस्",
                    BreadCrumbTitle = "Device Model",
                    CRUDAction = CRUDType.CREATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Create,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = new HRDeviceModels { Id=0}
                });
            }
            catch (Exception exp)
            {
                await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
                return RedirectToAction("Index", new { pageNumber = 1, pageSize = 10, orderingBy = "Id", orderingDirection = "DESC" });
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadHRDeviceModelAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRDeviceModelViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "DeviceManagement",
                    BreadCrumbController = "HRDeviceModel",
                    BreadCrumbBaseURL = "DeviceManagement/HRDeviceModel",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_ReadHRDeviceModelAsync",
                    FormModelName = "HRDeviceModel",
                    ModalTitle = "उपकरण मोडेल विवरण हेर्नुहोस्",
                    BreadCrumbTitle = "Device Model",
                    CRUDAction = CRUDType.READ,
                    OnlyCancelButton = true,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = await _HRDeviceModelServices.GetModelByIdAsync(id)
                });
            }
            catch (Exception exp)
            {
                await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
                return RedirectToAction("Index", new { pageNumber = pageNumber, pageSize = pageSize, orderingBy = orderingBy, orderingDirection = orderingDirection });
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _UpdateHRDeviceModelAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRDeviceModelViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "DeviceManagement",
                    BreadCrumbController = "HRDeviceModel",
                    BreadCrumbBaseURL = "DeviceManagement/HRDeviceModel",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_UpdateHRDeviceModelAsync",
                    FormModelName = "HRDeviceModel",
                    ModalTitle = "उपकरण मोडेल सम्पादन गर्नुहोस्",
                    BreadCrumbTitle = "Device Model",
                    CRUDAction = CRUDType.UPDATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Update,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = await _HRDeviceModelServices.GetModelByIdAsync(id)
                });
            }
            catch (Exception exp)
            {
                await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
                return RedirectToAction("Index", new { pageNumber = pageNumber, pageSize = pageSize, orderingBy = orderingBy, orderingDirection = orderingDirection });
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _DeleteHRDeviceModelAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRDeviceModelViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "DeviceManagement",
                    BreadCrumbController = "HRDeviceModel",
                    BreadCrumbBaseURL = "DeviceManagement/HRDeviceModel",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    FormModelName = "HRDeviceModel",
                    ModalTitle = "उपकरण मोडेलहरु मेटाउन निश्चित हुनुहुन्छ?",
                    BreadCrumbTitle = "Device Model",
                    BreadCrumbActionName = "_DeleteHRDeviceModelAsync",
                    CRUDAction = CRUDType.DELETE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Delete,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = await _HRDeviceModelServices.GetModelByIdAsync(id)
                });
            }
            catch (Exception exp)
            {
                await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
                return RedirectToAction("Index", new { pageNumber = pageNumber, pageSize = pageSize, orderingBy = orderingBy, orderingDirection = orderingDirection });
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ExportHRDeviceModelAsync()
        {
            try
            {
                var model = await _HRDeviceModelServices.FindAllAsync(x => x.IsActive == true);
                return await ExportToExcel("HRDeviceModel", model);
            }
            catch (Exception exp)
            {
                await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
                return RedirectToAction("Index", new { pageNumber = 1, pageSize = 10, orderingBy = "Id", orderingDirection = "DESC" });
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintHRDeviceModelAsync()
        {
            try
            {
                return PartialView(new HRDeviceModelViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "DeviceManagement",
                    BreadCrumbController = "HRDeviceModel",
                    BreadCrumbBaseURL = "DeviceManagement/HRDeviceModel",
                    BreadCrumbActionName = "_PrintHRDeviceModelAsync",
                    FormModelName = "HRDevice",
                    ModalTitle = "उपकरण मोडेलहरुको सुची छाप्नुहोस्",
                    BreadCrumbTitle = "Device Model",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBModelList = await _HRDeviceModelServices.FindAllAsync(x=>x.IsActive == true)
                });
            }
            catch (Exception exp)
            {
                await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
                return RedirectToAction("Index", new { pageNumber = 1, pageSize = 10, orderingBy = "Id", orderingDirection = "DESC" });
            }
        }

        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _CreateHRDeviceModelAsync(HRDeviceModelViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHRDeviceModel(viewModel, CRUDType.CREATE, formCollection);
        }

        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _UpdateHRDeviceModelAsync(HRDeviceModelViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHRDeviceModel(viewModel, CRUDType.UPDATE, formCollection);
        }

        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _DeleteHRDeviceModelAsync(HRDeviceModelViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHRDeviceModel(viewModel, CRUDType.DELETE, formCollection);
        }

        [NonAction]
        protected async Task<ActionResult> RETSourceHRDeviceModel(HRDeviceModelViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
                viewModel.BreadCrumbArea = "DeviceManagement";
                viewModel.BreadCrumbController = "HRDeviceModel";
                viewModel.BreadCrumbBaseURL = "DeviceManagement/HRDeviceModel";
                await Task.WhenAll();
                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        viewModel.SubmitButtonText = SubmitButtonText.Create;
                        viewModel.BreadCrumbActionName = "_CreateHRDeviceModelAsync";
                        viewModel.ModalTitle = "नयाँ उपकरण मोडेल थप्नुहोस्";
                        viewModel.FormModelName = "HRDeviceModel";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.UPDATE:
                        viewModel.BreadCrumbActionName = "_UpdateHRDeviceModelAsync";
                        viewModel.ModalTitle = "उपकरण मोडेल सम्पादन गर्नुहोस्";
                        viewModel.FormModelName = "HRDeviceModel";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.DELETE:
                        viewModel.BreadCrumbActionName = "_DeleteHRDeviceModelAsync";
                        viewModel.ModalTitle = "उपकरण मोडेलहरु मेटाउन निश्चित हुनुहुन्छ?";
                        viewModel.FormModelName = "HRDeviceModel";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.PRINT:
                        viewModel.BreadCrumbActionName = "_PrintHRDeviceModelAsync";
                        viewModel.ModalTitle = "उपकरण मोडेलहरुको सुची छाप्नुहोस्";
                        viewModel.FormModelName = "HRDeviceModel";
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
        protected async Task<ActionResult> CRUDHRDeviceModel(HRDeviceModelViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceHRDeviceModel(viewModel, cRUDType);
                }
                await _HRDeviceModelServices.CommitSaveChangesAsync(viewModel.DataModel, cRUDType);
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceHRDeviceModel(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListHRDeviceModelAsync", new { pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }
    }
}