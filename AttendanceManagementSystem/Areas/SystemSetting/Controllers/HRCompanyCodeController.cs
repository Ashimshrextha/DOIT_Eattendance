using AttendanceManagementSystem.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SystemModels.SystemSecurity;
using SystemServices.SystemSecurity;
using SystemViewModels.SystemSecurity;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;

namespace AttendanceManagementSystem.Areas.SystemSetting.Controllers
{
    public class HRCompanyCodeController : BaseController
    {
        // GET: SystemSetting/HRCompanyCode
        private readonly HRCompanyCodeServices _hRCompanyCodeServices;

        public HRCompanyCodeController()
        {
            this._hRCompanyCodeServices = new HRCompanyCodeServices(this._unitOfWork);
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> Index(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return View(new HRCompanyCodeViewModelList
                {
                    DBModelList = await _hRCompanyCodeServices.GetPagedList(pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "HRCompanyCode",
                    BreadCrumbBaseURL = "SystemSetting/HRCompanyCode",
                    BreadCrumbTitle = "कार्यालय कोड",
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
        public async Task<ActionResult> _ListHRCompanyCodeAsync(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRCompanyCodeViewModelList
                {
                    DBModelList = await _hRCompanyCodeServices.GetBySearchKey(pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, searchKey),
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "HRCompanyCode",
                    BreadCrumbBaseURL = "SystemSetting/HRCompanyCode",
                    BreadCrumbActionName = "_ListHRCompanyCodeAsync",
                    BreadCrumbTitle = "Office Code",
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
        public async Task<ActionResult> _CreateHRCompanyCodeAsync()
        {
            try
            {
                return PartialView(new HRCompanyCodeViewModel
                {
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "HRCompanyCode",
                    BreadCrumbBaseURL = "SystemSetting/HRCompanyCode",
                    BreadCrumbActionName = "_CreateHRCompanyCodeAsync",
                    FormModelName = "HRCompanyCode",
                    ModalTitle = "नयाँ कार्यालय कोड थप्नुहोस्",
                    BreadCrumbTitle = "Office Code",
                    CRUDAction = CRUDType.CREATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Create,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = new HRCompanyCodeModel { Id = 0 }
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadHRCompanyCodeAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRCompanyCodeViewModel
                {
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "HRCompanyCode",
                    BreadCrumbBaseURL = "SystemSetting/HRCompanyCode",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_ReadHRCompanyCodeAsync",
                    FormModelName = "HRCompanyCode",
                    ModalTitle = "कार्यालय कोड विवरण हेर्नुहोस",
                    BreadCrumbTitle = "Office Code",
                    CRUDAction = CRUDType.READ,
                    OnlyCancelButton = true,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = await _hRCompanyCodeServices.GetModelByIdAsync(id)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _UpdateHRCompanyCodeAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRCompanyCodeViewModel
                {
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "HRCompanyCode",
                    BreadCrumbBaseURL = "SystemSetting/HRCompanyCode",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_UpdateHRCompanyCodeAsync",
                    FormModelName = "HRCompanyCode",
                    ModalTitle = "कार्यालय कोड सम्पादन गर्नुहोस्",
                    BreadCrumbTitle = "Office Code",
                    CRUDAction = CRUDType.UPDATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Update,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = await _hRCompanyCodeServices.GetModelByIdAsync(id)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _DeleteHRCompanyCodeAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null||id==0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRCompanyCodeViewModel
                {
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "HRCompanyCode",
                    BreadCrumbBaseURL = "SystemSetting/HRCompanyCode",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    FormModelName = "HRCompanyCode",
                    ModalTitle = "कार्यालय कोड मेटाउन निश्चित हुनुहुन्छ?",
                    BreadCrumbTitle = "Office Code",
                    BreadCrumbActionName = "_DeleteHRCompanyCodeAsync",
                    CRUDAction = CRUDType.DELETE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Delete,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = await _hRCompanyCodeServices.GetModelByIdAsync(id)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ExportHRCompanyCodeAsync()
        {
            try
            {
                var model = await _hRCompanyCodeServices.FindAllAsync(x => true);
                return await ExportToExcel("OfficeCode", model);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintHRCompanyCodeAsync()
        {
            try
            {
                return PartialView(new HRCompanyCodeViewModel
                {
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "HRCompanyCode",
                    BreadCrumbBaseURL = "SystemSetting/HRCompanyCode",
                    BreadCrumbActionName = "_PrintHRCompanyCodeAsync",
                    FormModelName = "HRCompanyCode",
                    ModalTitle = "कार्यालय कोड सुची छाप्नुहोस्",
                    BreadCrumbTitle = "Office Code",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBModelList = await _hRCompanyCodeServices.FindAllAsync(x => true)
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
        public async Task<ActionResult> _CreateHRCompanyCodeAsync(HRCompanyCodeViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHRCompanyCode(viewModel, CRUDType.CREATE, formCollection);
        }

        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _UpdateHRCompanyCodeAsync(HRCompanyCodeViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHRCompanyCode(viewModel, CRUDType.UPDATE, formCollection);
        }

        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _DeleteHRCompanyCodeAsync(HRCompanyCodeViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHRCompanyCode(viewModel, CRUDType.DELETE, formCollection);
        }

        [NonAction]
        protected async Task<ActionResult> RETSourceHRCompanyCode(HRCompanyCodeViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
                viewModel.BreadCrumbArea = "SystemSetting";
                viewModel.BreadCrumbController = "HRCompanyCode";
                viewModel.BreadCrumbBaseURL = "SystemSetting/HRCompanyCode";
                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        viewModel.SubmitButtonText = SubmitButtonText.Create;
                        viewModel.BreadCrumbActionName = "_CreateHRCompanyCodeAsync";
                        viewModel.ModalTitle = "नयाँ कार्यालय कोड थप्नुहोस्";
                        viewModel.FormModelName = "HRCompanyCode";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.UPDATE:
                        viewModel.BreadCrumbActionName = "_UpdateHRCompanyCodeAsync";
                        viewModel.ModalTitle = "कार्यालय कोड सम्पादन गर्नुहोस्";
                        viewModel.FormModelName = "HRCompanyCode";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.DELETE:
                        viewModel.BreadCrumbActionName = "_DeleteHRCompanyCodeAsync";
                        viewModel.ModalTitle = "कार्यालय कोड मेटाउन निश्चित हुनुहुन्छ?";
                        viewModel.FormModelName = "HRCompanyCode";
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
        protected async Task<ActionResult> CRUDHRCompanyCode(HRCompanyCodeViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceHRCompanyCode(viewModel, cRUDType);
                }
                await _hRCompanyCodeServices.CommitSaveChangesAsync(viewModel.DataModel, cRUDType);
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceHRCompanyCode(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListHRCompanyCodeAsync", new { pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }
    }
}
