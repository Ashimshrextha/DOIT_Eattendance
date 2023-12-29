using AttendanceManagementSystem.App_Auth;
using AttendanceManagementSystem.Controllers;
using DeviceManagement.Models;
using System;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using SystemServices.DeviceManagement;
using SystemViewModels.DeviceManagement;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;

namespace AttendanceManagementSystem.Areas.DeviceManagement.Controllers
{
    public class DeviceLogsController : BaseController
    {
        // GET: DeviceManagement/DeviceLogs

        private readonly DeviceLogsServices _DeviceLogsServices;

        public DeviceLogsController()
        {
            this._DeviceLogsServices = new DeviceLogsServices(this._unitOfWork);
        }


        //[NonAction]
        //public Expression<Func<DeviceLogsModel, bool>> Condition(string searchKey = "")
        //{
        //    Expression<Func<DeviceLogsModel, bool>> returnData = (x => false);
        //    switch (this.SessionDetail.IdRoleType)
        //    {
        //        case (int)RoleType.SuperUser:
        //            return returnData = (x => x..ToUpper().Contains(searchKey.ToUpper().ToString()) || searchKey == "");
        //        case (int)RoleType.Admin:
        //            return returnData = (x => x.Id == this.SessionDetail.IdHRCompany || x.IdParent_HRCompany == this.SessionDetail.IdHRCompany && (x.CompanyName.ToUpper().Contains(searchKey.ToUpper().ToString()) || searchKey == ""));
        //        case (int)RoleType.RootUser:
        //            return returnData = (x => x.Id == this.SessionDetail.IdHRCompany && (x.CompanyName.ToUpper().Contains(searchKey.ToUpper().ToString()) || searchKey == ""));
        //        case (int)RoleType.NormalUser:
        //            return returnData = (x => x.Id == this.SessionDetail.IdHRCompany && (x.CompanyName.ToUpper().Contains(searchKey.ToUpper().ToString()) || searchKey == ""));
        //        default:
        //            return returnData;
        //    }
        //}

        [AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> Index(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return View(new DeviceLogsViewModelList
                {
                    DBModelList = await _DeviceLogsServices.GetPagedList(pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    BreadCrumbArea = "DeviceManagement",
                    BreadCrumbController = "DeviceLogs",
                    BreadCrumbBaseURL = "DeviceManagement/DeviceLogs",
                    BreadCrumbTitle = "उपकरण लगहरू",
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

        [AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListDeviceLogsAsync(long? idHRCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (idHRCompany == null || idHRCompany == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new DeviceLogsViewModelList
                {
                    DBModelList = await _DeviceLogsServices.GetsBySearchKey(pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, searchKey),
                    BreadCrumbArea = "DeviceManagement",
                    BreadCrumbController = "DeviceLogs",
                    BreadCrumbBaseURL = "DeviceManagement/DeviceLogs",
                    BreadCrumbActionName = "_ListDeviceLogsAsync",
                    BreadCrumbTitle = "Device Logs",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _CreateDeviceLogsAsync()
        {
            try
            {
                return PartialView(new DeviceLogsViewModel
                {
                    BreadCrumbArea = "DeviceManagement",
                    BreadCrumbController = "DeviceLogs",
                    BreadCrumbBaseURL = "DeviceManagement/DeviceLogs",
                    BreadCrumbActionName = "_CreateDeviceLogsAsync",
                    FormModelName = "DeviceLogs",
                    ModalTitle = "नयाँ उपकरण लगहरू थप्नुहोस्",
                    CRUDAction = CRUDType.CREATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Create,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadDeviceLogsAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new DeviceLogsViewModel
                {
                    BreadCrumbArea = "DeviceManagement",
                    BreadCrumbController = "DeviceLogs",
                    BreadCrumbBaseURL = "DeviceManagement/DeviceLogs",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_ReadDeviceLogsAsync",
                    FormModelName = "DeviceLogs",
                    ModalTitle = "उपकरण लगहरूको विवरण हेर्नुहोस्",
                    CRUDAction = CRUDType.READ,
                    OnlyCancelButton = true,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = await _DeviceLogsServices.GetModelByIdAsync(id)
                });
            }
            catch (Exception exp)
            {
                await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
                return RedirectToAction("Index", new { pageNumber = pageNumber, pageSize = pageSize, orderingBy = orderingBy, orderingDirection = orderingDirection });
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _UpdateDeviceLogsAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new DeviceLogsViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "DeviceManagement",
                    BreadCrumbController = "DeviceLogs",
                    BreadCrumbBaseURL = "DeviceManagement/DeviceLogs",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_UpdateDeviceLogsAsync",
                    FormModelName = "DeviceLogs",
                    ModalTitle = "उपकरण लगहरू सम्पादन गर्नुहोस्",
                    BreadCrumbTitle = "Device Logs",
                    CRUDAction = CRUDType.UPDATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Update,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = await _DeviceLogsServices.GetModelByIdAsync(id)
                });
            }
            catch (Exception exp)
            {
                await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
                return RedirectToAction("Index", new { pageNumber = pageNumber, pageSize = pageSize, orderingBy = orderingBy, orderingDirection = orderingDirection });
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _DeleteDeviceLogsAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new DeviceLogsViewModel
                {
                    BreadCrumbArea = "DeviceManagement",
                    BreadCrumbController = "DeviceLogs",
                    BreadCrumbBaseURL = "DeviceManagement/DeviceLogs",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    FormModelName = "DeviceLogs",
                    ModalTitle = "उपकरण लगहरू मेटाउन निश्चित हुनुहुन्छ?",
                    BreadCrumbActionName = "_DeleteDeviceLogsAsync",
                    CRUDAction = CRUDType.DELETE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Delete,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = await _DeviceLogsServices.GetModelByIdAsync(id)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ExportDeviceLogsAsync()
        {
            try
            {
                var model = await _DeviceLogsServices.FindAllAsync(x => x.IdEnroll > 0);
                return await ExportToExcel("DeviceLogs", model);
            }
            catch (Exception exp)
            {
                await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
                return RedirectToAction("Index", new { pageNumber = 1, pageSize = 10, orderingBy = "Id", orderingDirection = "DESC" });
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintDeviceLogsAsync()
        {
            try
            {
                return PartialView(new DeviceLogsViewModel
                {
                    BreadCrumbArea = "DeviceManagement",
                    BreadCrumbController = "DeviceLogs",
                    BreadCrumbBaseURL = "DeviceManagement/DeviceLogs",
                    BreadCrumbActionName = "_PrintDeviceLogsAsync",
                    FormModelName = "DeviceLogs",
                    ModalTitle = "उपकरण लगहरूको सुची छाप्नुहोस्",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBModelList = await _DeviceLogsServices.FindAllAsync(x => x.IdEnroll > 0)
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
        public async Task<ActionResult> _CreateDeviceLogsAsync(DeviceLogsViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDDeviceLogs(viewModel, CRUDType.CREATE, formCollection);
        }

        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _UpdateDeviceLogsAsync(DeviceLogsViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDDeviceLogs(viewModel, CRUDType.UPDATE, formCollection);
        }

        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _DeleteDeviceLogsAsync(DeviceLogsViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDDeviceLogs(viewModel, CRUDType.DELETE, formCollection);
        }

        [NonAction]
        protected async Task<ActionResult> RETSourceDeviceLogs(DeviceLogsViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
                viewModel.BreadCrumbArea = "DeviceManagement";
                viewModel.BreadCrumbController = "DeviceLogs";
                viewModel.BreadCrumbBaseURL = "DeviceManagement/DeviceLogs";
                await Task.WhenAll();
                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        viewModel.SubmitButtonText = SubmitButtonText.Create;
                        viewModel.BreadCrumbActionName = "_CreateDeviceLogsAsync";
                        viewModel.ModalTitle = "नयाँ उपकरण लगहरू थप्नुहोस्";
                        viewModel.FormModelName = "DeviceLogs";
                        viewModel.CRUDAction = CRUDType.CREATE;
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.UPDATE:
                        viewModel.BreadCrumbActionName = "_UpdateDeviceLogsAsync";
                        viewModel.ModalTitle = "उपकरण लगहरू सम्पादन गर्नुहोस्";
                        viewModel.FormModelName = "DeviceLogs";
                        viewModel.CRUDAction = CRUDType.UPDATE;
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.DELETE:
                        viewModel.BreadCrumbActionName = "_DeleteDeviceLogsAsync";
                        viewModel.ModalTitle = "उपकरण लगहरू मेटाउन निश्चित हुनुहुन्छ?";
                        viewModel.FormModelName = "DeviceLogs";
                        viewModel.CRUDAction = CRUDType.DELETE;
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
        protected async Task<ActionResult> CRUDDeviceLogs(DeviceLogsViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceDeviceLogs(viewModel, cRUDType);
                }
                viewModel.DataModel.FetchedDate = DateTime.Now;
                await _DeviceLogsServices.CommitSaveChangesAsync(viewModel.DataModel, cRUDType);
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceDeviceLogs(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListDeviceLogsAsync", new { pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }
    }
}