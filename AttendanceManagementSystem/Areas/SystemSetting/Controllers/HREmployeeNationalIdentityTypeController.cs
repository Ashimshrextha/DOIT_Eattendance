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
    public class HREmployeeNationalIdentityTypeController : BaseController
    {
        // GET: SystemSetting/HREmployeeNationalIdentityType

        private readonly HREmployeeNationalIdentityTypeServices _HREmployeeNationalIdentityTypeServices;

        public HREmployeeNationalIdentityTypeController()
        {
            _HREmployeeNationalIdentityTypeServices = new HREmployeeNationalIdentityTypeServices(this._unitOfWork);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> Index(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return View(new HREmployeeNationalIdentityTypeViewModelList
                {
                    DBModelList = await _HREmployeeNationalIdentityTypeServices.GetPagedList(pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "HREmployeeNationalIdentityType",
                    BreadCrumbBaseURL = "SystemSetting/HREmployeeNationalIdentityType",
                    BreadCrumbTitle = "राष्ट्रिय पहिचान प्रकार",
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
        public async Task<ActionResult> _ListHREmployeeNationalIdentityTypeAsync(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HREmployeeNationalIdentityTypeViewModelList
                {
                    DBModelList = await _HREmployeeNationalIdentityTypeServices.GetsBySearchKey(pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, searchKey = ""),
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "HREmployeeNationalIdentityType",
                    BreadCrumbBaseURL = "SystemSetting/HREmployeeNationalIdentityType",
                    BreadCrumbActionName = "_ListHREmployeeNationalIdentityTypeAsync",
                    BreadCrumbTitle = "NationalIdentityType",
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
        public async Task<ActionResult> _CreateHREmployeeNationalIdentityTypeAsync()
        {
            try
            {
                return PartialView(new HREmployeeNationalIdentityTypeViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "HREmployeeNationalIdentityType",
                    BreadCrumbBaseURL = "SystemSetting/HREmployeeNationalIdentityType",
                    BreadCrumbActionName = "_CreateHREmployeeNationalIdentityTypeAsync",
                    FormModelName = "HREmployeeNationalIdentityType",
                    ModalTitle = "नयाँ राष्ट्रिय पहिचान प्रकार थप्नुहोस्",
                    BreadCrumbTitle = "NationalIdentityType",
                    CRUDAction = CRUDType.CREATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Create,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.HRCompanyWithCode),
                    DDLEmployeeCategory = await GetLongStringPairModel(PairModelTitle.EmployeeCategory),
                    DataModel = new HREmployeeNationalIdentityTypeModel { Id = 0 }
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadHREmployeeNationalIdentityTypeAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _HREmployeeNationalIdentityTypeServices.GetModelByIdAsync(id);
                return PartialView(new HREmployeeNationalIdentityTypeViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "HREmployeeNationalIdentityType",
                    BreadCrumbBaseURL = "SystemSetting/HREmployeeNationalIdentityType",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_ReadHREmployeeNationalIdentityTypeAsync",
                    FormModelName = "HREmployeeNationalIdentityType",
                    ModalTitle = "View Employee NationalIdentityType Detail",
                    BreadCrumbTitle = "NationalIdentityType",
                    CRUDAction = CRUDType.READ,
                    OnlyCancelButton = true,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = model,
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _UpdateHREmployeeNationalIdentityTypeAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _HREmployeeNationalIdentityTypeServices.GetModelByIdAsync(id);
                return PartialView(new HREmployeeNationalIdentityTypeViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "HREmployeeNationalIdentityType",
                    BreadCrumbBaseURL = "SystemSetting/HREmployeeNationalIdentityType",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_UpdateHREmployeeNationalIdentityTypeAsync",
                    FormModelName = "HREmployeeNationalIdentityType",
                    ModalTitle = "राष्ट्रिय पहिचान प्रकार सम्पादन गर्नुहोस्",
                    BreadCrumbTitle = "NationalIdentityType",
                    CRUDAction = CRUDType.UPDATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Update,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = model,
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _DeleteHREmployeeNationalIdentityTypeAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _HREmployeeNationalIdentityTypeServices.GetModelByIdAsync(id);
                return PartialView(new HREmployeeNationalIdentityTypeViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "HREmployeeNationalIdentityType",
                    BreadCrumbBaseURL = "SystemSetting/HREmployeeNationalIdentityType",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    FormModelName = "HREmployeeNationalIdentityType",
                    ModalTitle = "राष्ट्रिय पहिचान प्रकार मेटाउन निश्चित हुनुहुन्छ?",
                    BreadCrumbTitle = "NationalIdentityType",
                    BreadCrumbActionName = "_DeleteHREmployeeNationalIdentityTypeAsync",
                    CRUDAction = CRUDType.DELETE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Delete,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = model,
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ExportHREmployeeNationalIdentityTypeAsync()
        {
            try
            {
                var model = await _HREmployeeNationalIdentityTypeServices.FindAllAsync(x => x.IsActive);
                return await ExportToExcel("HREmployeeNationalIdentityType", model);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintHREmployeeNationalIdentityTypeAsync()
        {
            try
            {
                return PartialView(new HREmployeeNationalIdentityTypeViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "HREmployeeNationalIdentityType",
                    BreadCrumbBaseURL = "SystemSetting/HREmployeeNationalIdentityType",
                    BreadCrumbActionName = "_PrintHREmployeeNationalIdentityTypeAsync",
                    FormModelName = "HREmployeeNationalIdentityType",
                    ModalTitle = "राष्ट्रिय पहिचान प्रकार सुची छाप्नुहोस्",
                    BreadCrumbTitle = "NationalIdentityType",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBModelList = await _HREmployeeNationalIdentityTypeServices.FindAllAsync(x => x.IsActive)
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
        public async Task<ActionResult> _CreateHREmployeeNationalIdentityTypeAsync(HREmployeeNationalIdentityTypeViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeNationalIdentityType(viewModel, CRUDType.CREATE, formCollection);
        }

        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _UpdateHREmployeeNationalIdentityTypeAsync(HREmployeeNationalIdentityTypeViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeNationalIdentityType(viewModel, CRUDType.UPDATE, formCollection);
        }

        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _DeleteHREmployeeNationalIdentityTypeAsync(HREmployeeNationalIdentityTypeViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeNationalIdentityType(viewModel, CRUDType.DELETE, formCollection);
        }

        [NonAction]
        protected async Task<ActionResult> RETSourceHREmployeeNationalIdentityType(HREmployeeNationalIdentityTypeViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
                viewModel.BreadCrumbArea = "SystemSetting";
                viewModel.BreadCrumbController = "HREmployeeNationalIdentityType";
                viewModel.BreadCrumbBaseURL = "SystemSetting/HREmployeeNationalIdentityType";
                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        viewModel.SubmitButtonText = SubmitButtonText.Create;
                        viewModel.BreadCrumbActionName = "_CreateHREmployeeNationalIdentityTypeAsync";
                        viewModel.ModalTitle = "नयाँ राष्ट्रिय पहिचान प्रकार थप्नुहोस्";
                        viewModel.FormModelName = "HREmployeeNationalIdentityType";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.UPDATE:
                        viewModel.BreadCrumbActionName = "_UpdateHREmployeeNationalIdentityTypeAsync";
                        viewModel.ModalTitle = "राष्ट्रिय पहिचान प्रकार सम्पादन गर्नुहोस्";
                        viewModel.FormModelName = "HREmployeeNationalIdentityType";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.DELETE:
                        viewModel.BreadCrumbActionName = "_DeleteHREmployeeNationalIdentityTypeAsync";
                        viewModel.ModalTitle = "राष्ट्रिय पहिचान प्रकार मेटाउन निश्चित हुनुहुन्छ?";
                        viewModel.FormModelName = "HREmployeeNationalIdentityType";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.PRINT:
                        viewModel.BreadCrumbActionName = "_PrintHREmployeeNationalIdentityTypeAsync";
                        viewModel.ModalTitle = "राष्ट्रिय पहिचान प्रकार सुची छाप्नुहोस्";
                        viewModel.FormModelName = "HREmployeeNationalIdentityType";
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
        protected async Task<ActionResult> CRUDHREmployeeNationalIdentityType(HREmployeeNationalIdentityTypeViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceHREmployeeNationalIdentityType(viewModel, cRUDType);
                }
                await _HREmployeeNationalIdentityTypeServices.CommitSaveChangesAsync(viewModel.DataModel, cRUDType);
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceHREmployeeNationalIdentityType(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListHREmployeeNationalIdentityTypeAsync", new { pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }
    }
}