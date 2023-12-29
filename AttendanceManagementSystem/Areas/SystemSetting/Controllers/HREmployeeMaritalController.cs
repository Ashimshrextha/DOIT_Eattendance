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
    public class HREmployeeMaritalController : BaseController
    {
        // GET: SystemSetting/HREmployeeMarital

        private readonly HREmployeeMaritalServices _HREmployeeMaritalServices;

        public HREmployeeMaritalController()
        {
            _HREmployeeMaritalServices = new HREmployeeMaritalServices(this._unitOfWork);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> Index(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return View(new HREmployeeMaritalViewModelList
                {
                    DBModelList = await _HREmployeeMaritalServices.GetPagedList(pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "HREmployeeMarital",
                    BreadCrumbBaseURL = "SystemSetting/HREmployeeMarital",
                    BreadCrumbTitle = "वैवाहिक अवस्था",
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
        public async Task<ActionResult> _ListHREmployeeMaritalAsync(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HREmployeeMaritalViewModelList
                {
                    DBModelList = await _HREmployeeMaritalServices.GetsBySearchKey(pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, searchKey = ""),
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "HREmployeeMarital",
                    BreadCrumbBaseURL = "SystemSetting/HREmployeeMarital",
                    BreadCrumbActionName = "_ListHREmployeeMaritalAsync",
                    BreadCrumbTitle = "Marital Status",
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
        public async Task<ActionResult> _CreateHREmployeeMaritalAsync()
        {
            try
            {
                return PartialView(new HREmployeeMaritalViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "HREmployeeMarital",
                    BreadCrumbBaseURL = "SystemSetting/HREmployeeMarital",
                    BreadCrumbActionName = "_CreateHREmployeeMaritalAsync",
                    FormModelName = "HREmployeeMarital",
                    ModalTitle = "नयाँ वैवाहिक अवस्था थप्नुहोस्",
                    BreadCrumbTitle = "Marital Status",
                    CRUDAction = CRUDType.CREATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Create,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = new HREmployeeMaritalModel { Id = 0 }
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadHREmployeeMaritalAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _HREmployeeMaritalServices.GetModelByIdAsync(id);
                return PartialView(new HREmployeeMaritalViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "HREmployeeMarital",
                    BreadCrumbBaseURL = "SystemSetting/HREmployeeMarital",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_ReadHREmployeeMaritalAsync",
                    FormModelName = "HREmployeeMarital",
                    ModalTitle = "वैवाहिक अवस्था विवरण हेर्नुहोस्",
                    BreadCrumbTitle = "Marital Status",
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
        public async Task<ActionResult> _UpdateHREmployeeMaritalAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _HREmployeeMaritalServices.GetModelByIdAsync(id);
                return PartialView(new HREmployeeMaritalViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "HREmployeeMarital",
                    BreadCrumbBaseURL = "SystemSetting/HREmployeeMarital",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_UpdateHREmployeeMaritalAsync",
                    FormModelName = "HREmployeeMarital",
                    ModalTitle = "वैवाहिक अवस्था विवरण सम्पादन गर्नुहोस्",
                    BreadCrumbTitle = "Marital Status",
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
        public async Task<ActionResult> _DeleteHREmployeeMaritalAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _HREmployeeMaritalServices.GetModelByIdAsync(id);
                return PartialView(new HREmployeeMaritalViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "HREmployeeMarital",
                    BreadCrumbBaseURL = "SystemSetting/HREmployeeMarital",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    FormModelName = "HREmployeeMarital",
                    ModalTitle = "वैवाहिक अवस्था विवरण मेटाउन निश्चित हुनुहुन्छ?",
                    BreadCrumbTitle = "Marital Status",
                    BreadCrumbActionName = "_DeleteHREmployeeMaritalAsync",
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
        public async Task<ActionResult> _ExportHREmployeeMaritalAsync()
        {
            try
            {
                var model = await _HREmployeeMaritalServices.FindAllAsync(x => x.IsActive);
                return await ExportToExcel("HREmployeeMarital", model);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintHREmployeeMaritalAsync()
        {
            try
            {
                return PartialView(new HREmployeeMaritalViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "HREmployeeMarital",
                    BreadCrumbBaseURL = "SystemSetting/HREmployeeMarital",
                    BreadCrumbActionName = "_PrintHREmployeeMaritalAsync",
                    FormModelName = "HREmployeeMarital",
                    ModalTitle = "वैवाहिक अवस्था सुची छाप्नुहोस्",
                    BreadCrumbTitle = "Marital Status",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBModelList = await _HREmployeeMaritalServices.FindAllAsync(x => x.IsActive)
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
        public async Task<ActionResult> _CreateHREmployeeMaritalAsync(HREmployeeMaritalViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeMarital(viewModel, CRUDType.CREATE, formCollection);
        }

        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _UpdateHREmployeeMaritalAsync(HREmployeeMaritalViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeMarital(viewModel, CRUDType.UPDATE, formCollection);
        }

        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _DeleteHREmployeeMaritalAsync(HREmployeeMaritalViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeMarital(viewModel, CRUDType.DELETE, formCollection);
        }

        [NonAction]
        protected async Task<ActionResult> RETSourceHREmployeeMarital(HREmployeeMaritalViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
                viewModel.BreadCrumbArea = "SystemSetting";
                viewModel.BreadCrumbController = "HREmployeeMarital";
                viewModel.BreadCrumbBaseURL = "SystemSetting/HREmployeeMarital";
                viewModel.DDLCompany = await GetLongStringPairModel(PairModelTitle.HRCompanyWithCode);
                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        viewModel.SubmitButtonText = SubmitButtonText.Create;
                        viewModel.BreadCrumbActionName = "_CreateHREmployeeMaritalAsync";
                        viewModel.ModalTitle = "नयाँ वैवाहिक अवस्था थप्नुहोस्";
                        viewModel.FormModelName = "HREmployeeMarital";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.UPDATE:
                        viewModel.BreadCrumbActionName = "_UpdateHREmployeeMaritalAsync";
                        viewModel.ModalTitle = "वैवाहिक अवस्था विवरण सम्पादन गर्नुहोस्";
                        viewModel.FormModelName = "HREmployeeMarital";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.DELETE:
                        viewModel.BreadCrumbActionName = "_DeleteHREmployeeMaritalAsync";
                        viewModel.ModalTitle = "वैवाहिक अवस्था विवरण मेटाउन निश्चित हुनुहुन्छ?";
                        viewModel.FormModelName = "HREmployeeMarital";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.PRINT:
                        viewModel.BreadCrumbActionName = "_PrintHREmployeeMaritalAsync";
                        viewModel.ModalTitle = "वैवाहिक अवस्था सुची छाप्नुहोस्";
                        viewModel.FormModelName = "HREmployeeMarital";
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
        protected async Task<ActionResult> CRUDHREmployeeMarital(HREmployeeMaritalViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceHREmployeeMarital(viewModel, cRUDType);
                }
                await _HREmployeeMaritalServices.CommitSaveChangesAsync(viewModel.DataModel, cRUDType);
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceHREmployeeMarital(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListHREmployeeMaritalAsync", new { pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }
    }
}