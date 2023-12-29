using AttendanceManagementSystem.App_Auth;
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

namespace AttendanceManagementSystem.Areas.SystemSecurity.Controllers
{
    public class MasterLeaveTitleController : BaseController
    {
        // GET: SystemSecurity/MasterLeaveTitle

        private readonly MasterLeaveTitleServices _masterLeaveTitleServices;

        public MasterLeaveTitleController()
        {
            this._masterLeaveTitleServices = new MasterLeaveTitleServices(this._unitOfWork);
        }


        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> Index(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return View(new MasterLeaveTitleViewModelList
                {
                    DBModelList = await _masterLeaveTitleServices.GetPagedList(pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "MasterLeaveTitle",
                    BreadCrumbBaseURL = "SystemSecurity/MasterLeaveTitle",
                    BreadCrumbTitle = "प्रणाली विवरण",
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

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListMasterLeaveTitleAsync(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new MasterLeaveTitleViewModelList
                {
                    DBModelList = await _masterLeaveTitleServices.GetBySearchKey(pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, searchKey),
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "MasterLeaveTitle",
                    BreadCrumbBaseURL = "SystemSecurity/MasterLeaveTitle",
                    BreadCrumbTitle = "प्रणाली विवरण",
                    BreadCrumbActionName = "Index",
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
        public async Task<ActionResult> _CreateMasterLeaveTitleAsync()
        {
            try
            {
                return PartialView(new MasterLeaveTitleViewModel
                {
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "MasterLeaveTitle",
                    BreadCrumbBaseURL = "SystemSecurity/MasterLeaveTitle",
                    BreadCrumbActionName = "_CreateMasterLeaveTitleAsync",
                    FormModelName = "MasterLeaveTitle",
                    ModalTitle = "नयाँ बिदा सिर्जना गर्नुहोस्",
                    CRUDAction = CRUDType.CREATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Create,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = new MasterLeaveTitleModel { Id = 0, IsActive = true }
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadMasterLeaveTitleAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new MasterLeaveTitleViewModel
                {
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "MasterLeaveTitle",
                    BreadCrumbBaseURL = "SystemSecurity/MasterLeaveTitle",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_ReadMasterLeaveTitleAsync",
                    FormModelName = "MasterLeaveTitle",
                    ModalTitle = "बिदा विवरण हेर्नुहोस्",
                    BreadCrumbTitle = "Leave Title",
                    CRUDAction = CRUDType.READ,
                    OnlyCancelButton = true,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = await this._masterLeaveTitleServices.GetModelByIdAsync(id)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _UpdateMasterLeaveTitleAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new MasterLeaveTitleViewModel
                {
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "MasterLeaveTitle",
                    BreadCrumbBaseURL = "SystemSecurity/MasterLeaveTitle",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_UpdateMasterLeaveTitleAsync",
                    FormModelName = "MasterLeaveTitle",
                    ModalTitle = "बिदा सम्पादन गर्नुहोस्",
                    BreadCrumbTitle = "Leave Type",
                    CRUDAction = CRUDType.UPDATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Update,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = await this._masterLeaveTitleServices.GetModelByIdAsync(id)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _DeleteMasterLeaveTitleAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new MasterLeaveTitleViewModel
                {
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "MasterLeaveTitle",
                    BreadCrumbBaseURL = "SystemSecurity/MasterLeaveTitle",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    FormModelName = "MasterLeaveTitle",
                    ModalTitle = "बिदा मेटाउन निश्चित हुनुहुन्छ?",
                    BreadCrumbTitle = "Leave Title",
                    BreadCrumbActionName = "_DeleteMasterLeaveTitleAsync",
                    CRUDAction = CRUDType.DELETE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Delete,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = await this._masterLeaveTitleServices.GetModelByIdAsync(id)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ExportMasterLeaveTitleAsync()
        {
            try
            {
                var model = await _masterLeaveTitleServices.FindAllAsync(x => true);
                return await ExportToExcel("LeaveTitle", model);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintMasterLeaveTitleAsync()
        {
            try
            {
                return PartialView(new MasterLeaveTitleViewModel
                {
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "MasterLeaveTitle",
                    BreadCrumbBaseURL = "SystemSecurity/MasterLeaveTitle",
                    BreadCrumbActionName = "_PrintMasterLeaveTitleAsync",
                    FormModelName = "MasterLeaveTitle",
                    ModalTitle = "क्यालेन्डर रूपान्तरण सुची छाप्नुहोस्",
                    BreadCrumbTitle = "Calender Conversion",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel
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
        public async Task<ActionResult> _CreateMasterLeaveTitleAsync(MasterLeaveTitleViewModel viewModel, FormCollection formCollection)
        {
            return await this.CRUDMasterLeaveTitleAsync(viewModel, CRUDType.CREATE, formCollection);
        }

        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _UpdateMasterLeaveTitleAsync(MasterLeaveTitleViewModel viewModel, FormCollection formCollection)
        {
            return await this.CRUDMasterLeaveTitleAsync(viewModel, CRUDType.UPDATE, formCollection);
        }

        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _DeleteMasterLeaveTitleAsync(MasterLeaveTitleViewModel viewModel, FormCollection formCollection)
        {
            return await this.CRUDMasterLeaveTitleAsync(viewModel, CRUDType.DELETE, formCollection);
        }

        [NonAction]
        protected async Task<ActionResult> RETSourceMasterLeaveTitleAsync(MasterLeaveTitleViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
                viewModel.BreadCrumbArea = "SystemSecurity";
                viewModel.BreadCrumbController = "MasterLeaveTitle";
                viewModel.BreadCrumbBaseURL = "SystemSecurity/MasterLeaveTitle";
                viewModel.DDLReligion = await this.GetIntStringPairModel(PairModelTitle.Religion);
                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        viewModel.SubmitButtonText = SubmitButtonText.Create;
                        viewModel.CRUDAction = CRUDType.CREATE;
                        viewModel.BreadCrumbActionName = "_CreateMasterLeaveTitleAsync";
                        viewModel.ModalTitle = "नयाँ बिदा सिर्जना गर्नुहोस्";
                        viewModel.FormModelName = "MasterLeaveTitle";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);

                    case CRUDType.UPDATE:
                        viewModel.SubmitButtonText = SubmitButtonText.Update;
                        viewModel.CRUDAction = CRUDType.UPDATE;
                        viewModel.BreadCrumbActionName = "_UpdateMasterLeaveTitleAsync";
                        viewModel.ModalTitle = "बिदा सम्पादन गर्नुहोस्";
                        viewModel.FormModelName = "MasterLeaveTitle";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);

                    case CRUDType.DELETE:
                        viewModel.SubmitButtonText = SubmitButtonText.Delete;
                        viewModel.CRUDAction = CRUDType.DELETE;
                        viewModel.BreadCrumbActionName = "_DeleteMasterLeaveTitleAsync";
                        viewModel.ModalTitle = "बिदा मेटाउन निश्चित हुनुहुन्छ?";
                        viewModel.FormModelName = "MasterLeaveTitle";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);

                    default:
                        return null;
                }
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [NonAction]
        protected async Task<ActionResult> CRUDMasterLeaveTitleAsync(MasterLeaveTitleViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceMasterLeaveTitleAsync(viewModel, cRUDType);
                }

                await _masterLeaveTitleServices.CommitSaveChangesAsync(viewModel.DataModel, cRUDType);
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceMasterLeaveTitleAsync(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListMasterLeaveTitleAsync", new { pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }
    }
}