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
    public class BiometricTemplateTypeController : BaseController
    {
        // GET: DeviceManagement/BiometricTemplateType

        private readonly BiometricTemplateTypeServices _BiometricTemplateTypeServices;

        public BiometricTemplateTypeController()
        {
            this._BiometricTemplateTypeServices = new BiometricTemplateTypeServices(this._unitOfWork);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> Index(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return View(new BiometricTemplateTypeViewModelList
                {
                    DBModelList = await _BiometricTemplateTypeServices.GetPagedList(pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "DeviceManagement",
                    BreadCrumbController = "BiometricTemplateType",
                    BreadCrumbBaseURL = "DeviceManagement/BiometricTemplateType",
                    BreadCrumbTitle = "बायोमेट्रिक टेम्पलेट्स प्रकार",
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
        public async Task<ActionResult> _ListBiometricTemplateTypeAsync(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey="")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new BiometricTemplateTypeViewModelList
                {
                    DBModelList = await _BiometricTemplateTypeServices.GetsBySearchKey(pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, searchKey=""),
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "DeviceManagement",
                    BreadCrumbController = "BiometricTemplateType",
                    BreadCrumbBaseURL = "DeviceManagement/BiometricTemplateType",
                    BreadCrumbActionName = "_ListBiometricTemplateTypeAsync",
                    BreadCrumbTitle = "Biometric Templates Type",
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
        public async Task<ActionResult> _CreateBiometricTemplateTypeAsync()
        {
            try
            {
                return PartialView(new BiometricTemplateTypeViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "DeviceManagement",
                    BreadCrumbController = "BiometricTemplateType",
                    BreadCrumbBaseURL = "DeviceManagement/BiometricTemplateType",
                    BreadCrumbActionName = "_CreateBiometricTemplateTypeAsync",
                    FormModelName = "BiometricTemplateType",
                    ModalTitle = "नयाँ बायोमेट्रिक टेम्पलेटहरूको प्रकार थप्नुहोस्",
                    BreadCrumbTitle = "Biometric Templates Type",
                    CRUDAction = CRUDType.CREATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Create,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = new BiometricTemplateTypeModel { Id = 0, IsActive = true }
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadBiometricTemplateTypeAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new BiometricTemplateTypeViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "DeviceManagement",
                    BreadCrumbController = "BiometricTemplateType",
                    BreadCrumbBaseURL = "DeviceManagement/BiometricTemplateType",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_ReadBiometricTemplateTypeAsync",
                    FormModelName = "BiometricTemplateType",
                    ModalTitle = "बायोमेट्रिक टेम्पलेटहरूको प्रकार विवरण हेर्नुहोस्",
                    BreadCrumbTitle = "Biometric Templates",
                    CRUDAction = CRUDType.READ,
                    OnlyCancelButton = true,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = await _BiometricTemplateTypeServices.GetModelByIdAsync(id)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _UpdateBiometricTemplateTypeAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new BiometricTemplateTypeViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "DeviceManagement",
                    BreadCrumbController = "BiometricTemplateType",
                    BreadCrumbBaseURL = "DeviceManagement/BiometricTemplateType",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_UpdateBiometricTemplateTypeAsync",
                    FormModelName = "BiometricTemplateType",
                    ModalTitle = "बायोमेट्रिक टेम्पलेटहरूको प्रकार सम्पादन गर्नुहोस्",
                    BreadCrumbTitle = "Biometric Templates",
                    CRUDAction = CRUDType.UPDATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Update,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = await _BiometricTemplateTypeServices.GetModelByIdAsync(id)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _DeleteBiometricTemplateTypeAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new BiometricTemplateTypeViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "DeviceManagement",
                    BreadCrumbController = "BiometricTemplateType",
                    BreadCrumbBaseURL = "DeviceManagement/BiometricTemplateType",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    FormModelName = "BiometricTemplateType",
                    ModalTitle = "बायोमेट्रिक टेम्पलेटहरूको प्रकार मेटाउन निश्चित हुनुहुन्छ?",
                    BreadCrumbTitle = "Biometric Templates",
                    BreadCrumbActionName = "_DeleteBiometricTemplateTypeAsync",
                    CRUDAction = CRUDType.DELETE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Delete,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = await _BiometricTemplateTypeServices.GetModelByIdAsync(id)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ExportBiometricTemplateTypeAsync()
        {
            try
            {
                var model = await _BiometricTemplateTypeServices.FindAllAsync(x => x.IsActive == true);
                return await ExportToExcel("BiometricTemplateType", model);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintBiometricTemplateTypeAsync()
        {
            try
            {
                return PartialView(new BiometricTemplateTypeViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "DeviceManagement",
                    BreadCrumbController = "BiometricTemplateType",
                    BreadCrumbBaseURL = "DeviceManagement/BiometricTemplateType",
                    BreadCrumbActionName = "_PrintBiometricTemplateTypeAsync",
                    FormModelName = "BiometricTemplateType",
                    ModalTitle = "बायोमेट्रिक टेम्पलेटहरूको प्रकार सुची छाप्नुहोस्",
                    BreadCrumbTitle = "Biometric Templates Type",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBModelList = await _BiometricTemplateTypeServices.FindAllAsync(x => x.IsActive == true)
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
        public async Task<ActionResult> _CreateBiometricTemplateTypeAsync(BiometricTemplateTypeViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDBiometricTemplateType(viewModel, CRUDType.CREATE, formCollection);
        }

        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _UpdateBiometricTemplateTypeAsync(BiometricTemplateTypeViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDBiometricTemplateType(viewModel, CRUDType.UPDATE, formCollection);
        }

        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _DeleteBiometricTemplateTypeAsync(BiometricTemplateTypeViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDBiometricTemplateType(viewModel, CRUDType.DELETE, formCollection);
        }

        [NonAction]
        protected async Task<ActionResult> RETSourceBiometricTemplateType(BiometricTemplateTypeViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
                viewModel.BreadCrumbArea = "DeviceManagement";
                viewModel.BreadCrumbController = "BiometricTemplateType";
                viewModel.BreadCrumbBaseURL = "DeviceManagement/BiometricTemplateType";
                await Task.WhenAll();
                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        viewModel.SubmitButtonText = SubmitButtonText.Create;
                        viewModel.BreadCrumbActionName = "_CreateBiometricTemplateTypeAsync";
                        viewModel.ModalTitle = "नयाँ बायोमेट्रिक टेम्पलेटहरूको प्रकार थप्नुहोस्";
                        viewModel.FormModelName = "BiometricTemplateType";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.UPDATE:
                        viewModel.BreadCrumbActionName = "_UpdateBiometricTemplateTypeAsync";
                        viewModel.ModalTitle = "बायोमेट्रिक टेम्पलेटहरूको प्रकार सम्पादन गर्नुहोस्";
                        viewModel.FormModelName = "BiometricTemplateType";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.DELETE:
                        viewModel.BreadCrumbActionName = "_DeleteBiometricTemplateTypeAsync";
                        viewModel.ModalTitle = "बायोमेट्रिक टेम्पलेटहरूको प्रकार मेटाउन निश्चित हुनुहुन्छ?";
                        viewModel.FormModelName = "BiometricTemplateType";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.PRINT:
                        viewModel.BreadCrumbActionName = "_PrintBiometricTemplateTypeAsync";
                        viewModel.ModalTitle = "बायोमेट्रिक टेम्पलेटहरूको प्रकार सुची छाप्नुहोस्";
                        viewModel.FormModelName = "BiometricTemplateType";
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
        protected async Task<ActionResult> CRUDBiometricTemplateType(BiometricTemplateTypeViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceBiometricTemplateType(viewModel, cRUDType);
                }
                await _BiometricTemplateTypeServices.CommitSaveChangesAsync(viewModel.DataModel, cRUDType);
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceBiometricTemplateType(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListBiometricTemplateTypeAsync", new { pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }
    }
}