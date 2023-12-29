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
    public class HRCompanyTypeController : BaseController
    {
        // GET: SystemSetting/HRCompanyTypeType
        private readonly HRCompanyTypeServices _HRCompanyTypeServices;

        public HRCompanyTypeController()
        {
            this._HRCompanyTypeServices = new HRCompanyTypeServices(this._unitOfWork);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> Index(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return View(new HRCompanyTypeViewModelList
                {
                    DBModelList = await _HRCompanyTypeServices.GetPagedList(pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "HRCompanyType",
                    BreadCrumbBaseURL = "SystemSetting/HRCompanyType",
                    BreadCrumbTitle = "कार्यालयको प्रकार",
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
        public async Task<ActionResult> _ListHRCompanyTypeAsync(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRCompanyTypeViewModelList
                {
                    DBModelList = await _HRCompanyTypeServices.GetsBySearchKey(pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, searchKey),
                    BreadCrumbTitle = "Office Management",
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "HRCompanyType",
                    BreadCrumbActionName = "_ListHRCompanyTypeAsync",
                    BreadCrumbBaseURL = "SystemSetting/HRCompanyType",
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
        public async Task<ActionResult> _CreateHRCompanyTypeAsync()
        {
            try
            {
                return PartialView(new HRCompanyTypeViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "HRCompanyType",
                    BreadCrumbBaseURL = "SystemSetting/HRCompanyType",
                    BreadCrumbTitle = "Office Type",
                    BreadCrumbActionName = "_CreateHRCompanyTypeAsync",
                    FormModelName = "HRCompanyType",
                    ModalTitle = "नयाँ कार्यालयको प्रकार थप्नुहोस्",
                    CRUDAction = CRUDType.CREATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Create,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLCompanyType = await GetIntStringPairModel(PairModelTitle.HRCompanyType),
                    DataModel = new HRCompanyTypeModel { Id = 0 }
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadHRCompanyTypeAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRCompanyTypeViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "HRCompanyType",
                    BreadCrumbBaseURL = "SystemSetting/HRCompanyType",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_ReadHRCompanyTypeAsync",
                    FormModelName = "HRCompanyType",
                    ModalTitle = "कार्यालयको प्रकार विवरण हेर्नुहोस्",
                    BreadCrumbTitle = "Office Type",
                    CRUDAction = CRUDType.READ,
                    OnlyCancelButton = true,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLCompanyType = await GetIntStringPairModel(PairModelTitle.HRCompanyType),
                    DataModel = await _HRCompanyTypeServices.GetModelByIdAsync(id)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _UpdateHRCompanyTypeAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRCompanyTypeViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "HRCompanyType",
                    BreadCrumbBaseURL = "SystemSetting/HRCompanyType",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_UpdateHRCompanyTypeAsync",
                    FormModelName = "HRCompanyType",
                    ModalTitle = "कार्यालयको प्रकार सम्पादन गर्नुहोस्",
                    BreadCrumbTitle = "Office Type",
                    CRUDAction = CRUDType.UPDATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Update,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLCompanyType = await GetIntStringPairModel(PairModelTitle.HRCompanyType),
                    DataModel = await _HRCompanyTypeServices.GetModelByIdAsync(id)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _DeleteHRCompanyTypeAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRCompanyTypeViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "HRCompanyType",
                    BreadCrumbBaseURL = "SystemSetting/HRCompanyType",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    FormModelName = "HRCompanyType",
                    ModalTitle = "कार्यालयको प्रकार मेटाउन निश्चित हुनुहुन्छ?",
                    BreadCrumbTitle = "Office Type",
                    BreadCrumbActionName = "_DeleteHRCompanyTypeAsync",
                    CRUDAction = CRUDType.DELETE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Delete,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLCompanyType = await GetIntStringPairModel(PairModelTitle.HRCompanyType),
                    DataModel = await _HRCompanyTypeServices.GetModelByIdAsync(id)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ExportHRCompanyTypeAsync()
        {
            try
            {
                var model = await _HRCompanyTypeServices.FindAllAsync(x => x.IsActive);
                return await ExportToExcel("CompanyType", model);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintHRCompanyTypeAsync()
        {
            try
            {
                return PartialView(new HRCompanyTypeViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "HRCompanyType",
                    BreadCrumbBaseURL = "SystemSetting/HRCompanyType",
                    BreadCrumbActionName = "_PrintHRCompanyTypeAsync",
                    FormModelName = "HRCompanyType",
                    ModalTitle = "कार्यालयको प्रकार सुची छाप्नुहोस्",
                    BreadCrumbTitle = "Office Type",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBModelList = await _HRCompanyTypeServices.FindAllAsync(x => x.IsActive)
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
        public async Task<ActionResult> _CreateHRCompanyTypeAsync(HRCompanyTypeViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHRCompanyTypeAsync(viewModel, CRUDType.CREATE, formCollection);
        }

        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _UpdateHRCompanyTypeAsync(HRCompanyTypeViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHRCompanyTypeAsync(viewModel, CRUDType.UPDATE, formCollection);
        }

        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _DeleteHRCompanyTypeAsync(HRCompanyTypeViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHRCompanyTypeAsync(viewModel, CRUDType.DELETE, formCollection);
        }

        [NonAction]
        protected async Task<ActionResult> RETSourceHRCompanyTypeAsync(HRCompanyTypeViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
                viewModel.HeaderTitle = "Attendance System Management";
                viewModel.BreadCrumbArea = "SystemSetting";
                viewModel.BreadCrumbController = "HRCompanyType";
                viewModel.BreadCrumbBaseURL = "SystemSetting/HRCompanyType";
                viewModel.DDLCompanyType = await GetIntStringPairModel(PairModelTitle.HRCompanyType);
                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        viewModel.SubmitButtonText = SubmitButtonText.Create;
                        viewModel.BreadCrumbActionName = "_CreateHRCompanyTypeAsync";
                        viewModel.ModalTitle = "नयाँ कार्यालयको प्रकार थप्नुहोस्";
                        viewModel.FormModelName = "HRCompanyType";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.UPDATE:
                        viewModel.BreadCrumbActionName = "_UpdateHRCompanyTypeAsync";
                        viewModel.ModalTitle = "कार्यालयको प्रकार सम्पादन गर्नुहोस्";
                        viewModel.FormModelName = "HRCompanyType";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.DELETE:
                        viewModel.BreadCrumbActionName = "_DeleteHRCompanyTypeAsync";
                        viewModel.ModalTitle = "कार्यालयको प्रकार मेटाउन निश्चित हुनुहुन्छ?";
                        viewModel.FormModelName = "HRCompanyType";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.PRINT:
                        viewModel.BreadCrumbActionName = "_PrintHRCompanyTypeAsync";
                        viewModel.ModalTitle = "कार्यालयको प्रकार सुची छाप्नुहोस्";
                        viewModel.FormModelName = "HRCompanyType";
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
        protected async Task<ActionResult> CRUDHRCompanyTypeAsync(HRCompanyTypeViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceHRCompanyTypeAsync(viewModel, cRUDType);
                }
                await _HRCompanyTypeServices.CommitSaveChangesAsync(viewModel.DataModel, cRUDType);
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceHRCompanyTypeAsync(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListHRCompanyTypeAsync", new { pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }

    }
}