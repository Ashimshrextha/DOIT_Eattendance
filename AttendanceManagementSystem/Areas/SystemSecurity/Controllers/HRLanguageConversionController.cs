using AttendanceManagementSystem.App_Auth;
using AttendanceManagementSystem.Controllers;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using SystemModels.SystemSetting;
using SystemServices.SystemSecurity;
using SystemViewModels.SystemSecurity;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;

namespace AttendanceManagementSystem.Areas.SystemSecurity.Controllers
{
    public class HRLanguageConversionController : BaseController
    {
        // GET: SystemSecurity/HRLanguageConversion
        private readonly HRLanguageConversionServices _HRLanguageConversionServices;

        public HRLanguageConversionController()
        {
            this._HRLanguageConversionServices = new HRLanguageConversionServices(this._unitOfWork);
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> Index(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return View(new HRLanguageConversionViewModelList
                {
                    DBModelList = await _HRLanguageConversionServices.GetPagedList(pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "HRLanguageConversion",
                    BreadCrumbBaseURL = "SystemSecurity/HRLanguageConversion",
                    BreadCrumbTitle = "भाषा परिर्वतन",
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

        [AuthorizeUser(ActionName ="Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListHRLanguageConversionAsync(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRLanguageConversionViewModelList
                {
                    DBModelList = await _HRLanguageConversionServices.GetBySearchKey(pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, searchKey),
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "HRLanguageConversion",
                    BreadCrumbBaseURL = "SystemSecurity/HRLanguageConversion",
                    BreadCrumbActionName = "_ListHRLanguageConversionAsync",
                    BreadCrumbTitle = "Language Conversion",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
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
        public async Task<ActionResult> _CreateHRLanguageConversionAsync()
        {
            try
            {
                return PartialView(new HRLanguageConversionViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "HRLanguageConversion",
                    BreadCrumbBaseURL = "SystemSecurity/HRLanguageConversion",
                    BreadCrumbActionName = "_CreateHRLanguageConversionAsync",
                    FormModelName = "HRLanguageConversion",
                    ModalTitle = "नयाँ भाषा रूपान्तरण गर्नुहोस्",
                    BreadCrumbTitle = "Language Conversion",
                    CRUDAction = CRUDType.CREATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Create,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = new HRLanguageConversionModel { Id = 0 }
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser(ActionName ="Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadHRLanguageConversionAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRLanguageConversionViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "HRLanguageConversion",
                    BreadCrumbBaseURL = "SystemSecurity/HRLanguageConversion",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_ReadHRLanguageConversionAsync",
                    FormModelName = "HRLanguageConversion",
                    ModalTitle = "भाषा रूपान्तरण विवरण हेर्नुहोस्",
                    BreadCrumbTitle = "Language Conversion",
                    CRUDAction = CRUDType.READ,
                    OnlyCancelButton = true,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = await _HRLanguageConversionServices.GetModelByIdAsync(id),
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _UpdateHRLanguageConversionAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRLanguageConversionViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "HRLanguageConversion",
                    BreadCrumbBaseURL = "SystemSecurity/HRLanguageConversion",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_UpdateHRLanguageConversionAsync",
                    FormModelName = "HRLanguageConversion",
                    ModalTitle = "भाषा रूपान्तरण सम्पादन गर्नुहोस्",
                    BreadCrumbTitle = "Language Conversion",
                    CRUDAction = CRUDType.UPDATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Update,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = await _HRLanguageConversionServices.GetModelByIdAsync(id),
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _DeleteHRLanguageConversionAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRLanguageConversionViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "HRLanguageConversion",
                    BreadCrumbBaseURL = "SystemSecurity/HRLanguageConversion",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    FormModelName = "HRLanguageConversion",
                    ModalTitle = "भाषा रूपान्तरण मेटाउन निश्चित हुनुहुन्छ?",
                    BreadCrumbTitle = "Language Conversion",
                    BreadCrumbActionName = "_DeleteHRLanguageConversionAsync",
                    CRUDAction = CRUDType.DELETE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Delete,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = await _HRLanguageConversionServices.GetModelByIdAsync(id),
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ExportHRLanguageConversionAsync()
        {
            try
            {
                var model = await _HRLanguageConversionServices.FindAllAsync(x => true);
                return await ExportToExcel("HRLanguageConversion", model);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintHRLanguageConversionAsync()
        {
            try
            {
                return PartialView(new HRLanguageConversionViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "HRLanguageConversion",
                    BreadCrumbBaseURL = "SystemSecurity/HRLanguageConversion",
                    BreadCrumbActionName = "_PrintHRLanguageConversionAsync",
                    FormModelName = "HRLanguageConversion",
                    ModalTitle = "भाषा रूपान्तरण सुची छाप्नुहोस्",
                    BreadCrumbTitle = "Language Conversion",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBModelList = await _HRLanguageConversionServices.FindAllAsync(x => true)
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
        public async Task<ActionResult> _CreateHRLanguageConversionAsync(HRLanguageConversionViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHRLanguageConversion(viewModel, CRUDType.CREATE, formCollection);
        }

        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _UpdateHRLanguageConversionAsync(HRLanguageConversionViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHRLanguageConversion(viewModel, CRUDType.UPDATE, formCollection);
        }

        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _DeleteHRLanguageConversionAsync(HRLanguageConversionViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHRLanguageConversion(viewModel, CRUDType.DELETE, formCollection);
        }

        [NonAction]
        protected async Task<ActionResult> RETSourceHRLanguageConversion(HRLanguageConversionViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
                viewModel.BreadCrumbArea = "SystemSecurity";
                viewModel.BreadCrumbController = "HRLanguageConversion";
                viewModel.BreadCrumbBaseURL = "SystemSecurity/HRLanguageConversion";
                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        viewModel.SubmitButtonText = SubmitButtonText.Create;
                        viewModel.BreadCrumbActionName = "_CreateHRLanguageConversionAsync";
                        viewModel.ModalTitle = "नयाँ भाषा रूपान्तरण गर्नुहोस्";
                        viewModel.FormModelName = "HRLanguageConversion";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.UPDATE:
                        viewModel.BreadCrumbActionName = "_UpdateHRLanguageConversionAsync";
                        viewModel.ModalTitle = "भाषा रूपान्तरण सम्पादन गर्नुहोस्";
                        viewModel.FormModelName = "HRLanguageConversion";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.DELETE:
                        viewModel.BreadCrumbActionName = "_DeleteHRLanguageConversionAsync";
                        viewModel.ModalTitle = "भाषा रूपान्तरण मेटाउन निश्चित हुनुहुन्छ?";
                        viewModel.FormModelName = "HRLanguageConversion";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.PRINT:
                        viewModel.BreadCrumbActionName = "_PrintHRLanguageConversionAsync";
                        viewModel.ModalTitle = "भाषा रूपान्तरण सुची छाप्नुहोस्";
                        viewModel.FormModelName = "HRLanguageConversion";
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
        protected async Task<ActionResult> CRUDHRLanguageConversion(HRLanguageConversionViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceHRLanguageConversion(viewModel, cRUDType);
                }
                await _HRLanguageConversionServices.CommitSaveChangesAsync(viewModel.DataModel, cRUDType);
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceHRLanguageConversion(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListHRLanguageConversionAsync", new { pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }
    }
}