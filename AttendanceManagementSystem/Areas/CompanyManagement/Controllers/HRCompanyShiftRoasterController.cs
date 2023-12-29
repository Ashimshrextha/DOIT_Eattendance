using AttendanceManagementSystem.Controllers;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Mvc;
using SystemDatabase;
using SystemModels.CompanyManagement;
using SystemServices.CompanyManagement;
using SystemViewModels.CompanyManagement;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;

namespace AttendanceManagementSystem.Areas.CompanyManagement.Controllers
{
    public class HRCompanyShiftRoasterController : BaseController
    {
        // GET: CompanyManagement/HRCompanyShiftRoaster
        private readonly HRCompanyShiftRoasterServices _hRCompanyShiftRoasterServices;

        public HRCompanyShiftRoasterController()
        {
            this._hRCompanyShiftRoasterServices = new HRCompanyShiftRoasterServices(this._unitOfWork);
        }

        [NonAction]
        public Expression<Func<HRCompanyShiftRoaster, bool>> Condition(long? idHRCompany, string searchKey = "")
        {
            Expression<Func<HRCompanyShiftRoaster, bool>> returnData = (x => false);
            switch (this.SessionDetail.IdRoleType)
            {
                case (int)RoleType.SuperUser:
                    return returnData = (x => x.IdHRCompany == idHRCompany && (x.HRCompany.CompanyName.ToUpper().Contains(searchKey.ToUpper().ToString()) || searchKey == ""));
                case (int)RoleType.Admin:
                    return returnData = (x => x.IdHRCompany == idHRCompany);
                case (int)RoleType.RootUser:
                    return returnData = (x => x.IdHRCompany == idHRCompany);
                case (int)RoleType.NormalUser:
                    return returnData = (x => x.IdHRCompany == idHRCompany);
                default:
                    return returnData;
            }
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListWrapperHRCompanyShiftRoasterAsync(long? idHRCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRCompanyShiftRoasterViewModelList
                {
                    SessionDetails = this.SessionDetail,
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyShiftRoaster",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyShiftRoaster",
                    BreadCrumbTitle = "शिफ्ट व्यवस्थापन",
                    BreadCrumbActionName = "_ListWrapperHRCompanyShiftRoasterAsync",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    IdHRCompany = idHRCompany,
                    DBModelList = await this._hRCompanyShiftRoasterServices.GetPagedList(Condition(idHRCompany, searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListHRCompanyShiftRoasterAsync(long? idHRCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRCompanyShiftRoasterViewModelList
                {
                    SessionDetails = this.SessionDetail,
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyShiftRoaster",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyShiftRoaster",
                    BreadCrumbTitle = "शिफ्ट व्यवस्थापन",
                    BreadCrumbActionName = "_ListHRCompanyShiftRoasterAsync",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    DBModelList = await this._hRCompanyShiftRoasterServices.GetPagedList(Condition(idHRCompany, searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _CreateHRCompanyShiftRoasterAsync(long? idHRCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRCompanyShiftRoasterViewModel
                {
                    SessionDetails = this.SessionDetail,
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyShiftRoaster",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyShiftRoaster",
                    BreadCrumbTitle = "शिफ्ट व्यवस्थापन",
                    BreadCrumbActionName = "_CreateHRCompanyShiftRoasterAsync",
                    FormModelName = "HRCompanyShiftRoaster",
                    ModalTitle = "नयाँ शिफ्ट सिर्जना गर्नुहोस्",
                    CRUDAction = CRUDType.CREATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Create,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    IdHRCompany = idHRCompany,
                    DataModel = new HRCompanyShiftRoasterModel { Id = 0, IdHRCompany = (long)idHRCompany }
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
        public async Task<ActionResult> _CreateHRCompanyShiftRoasterAsync(HRCompanyShiftRoasterViewModel viewModel, FormCollection formCollection)
        {
            return await this.CRUDHRCompanyShiftRoasterAsync(viewModel, formCollection, CRUDType.CREATE);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadHRCompanyShiftRoasterAsync(long? id)
        {
            try
            {
                return PartialView(new HRCompanyShiftRoasterViewModel
                {
                    SessionDetails = this.SessionDetail,
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyShiftRoaster",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyShiftRoaster",
                    BreadCrumbTitle = "शिफ्ट व्यवस्थापन",
                    BreadCrumbActionName = "_UpdatedHRCompanyShiftRoasterAsync",
                    FormModelName = "HRCompanyShiftRoaster",
                    ModalTitle = "शिफ्ट हेर्नुहोस्",
                    CRUDAction = CRUDType.READ,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    OnlyCancelButton = true,
                    DataModel = await this._hRCompanyShiftRoasterServices.GetModelByIdAsync(id)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _UpdatedHRCompanyShiftRoasterAsync(long? id, long? idHRCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            try
            {
                if (id == 0 || id == null)
                {
                    return await this.AlertNotification("Error", "Your Id is null", AlertNotificationType.error);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRCompanyShiftRoasterViewModel
                {
                    SessionDetails = this.SessionDetail,
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyShiftRoaster",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyShiftRoaster",
                    BreadCrumbTitle = "शिफ्ट व्यवस्थापन",
                    BreadCrumbActionName = "_UpdatedHRCompanyShiftRoasterAsync",
                    FormModelName = "HRCompanyShiftRoaster",
                    ModalTitle = "शिफ्ट सम्पादन गर्नुहोस्",
                    CRUDAction = CRUDType.UPDATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Update,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    IdHRCompany = idHRCompany,
                    DataModel = await this._hRCompanyShiftRoasterServices.GetModelByIdAsync(id)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        [ValidateInput(true)]
        public async Task<ActionResult> _UpdatedHRCompanyShiftRoasterAsync(HRCompanyShiftRoasterViewModel viewModel, FormCollection formCollection)
        {
            return await this.CRUDHRCompanyShiftRoasterAsync(viewModel, formCollection, CRUDType.UPDATE);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _DeleteHRCompanyShiftRoasterAsync(long? id, long? idHRCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            try
            {
                if (id == 0 || id == null)
                {
                    return await this.AlertNotification("Error", "Your Id is null", AlertNotificationType.error);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRCompanyShiftRoasterViewModel
                {
                    SessionDetails = this.SessionDetail,
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyShiftRoaster",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyShiftRoaster",
                    BreadCrumbTitle = "शिफ्ट व्यवस्थापन",
                    BreadCrumbActionName = "_DeleteHRCompanyShiftRoasterAsync",
                    FormModelName = "HRCompanyShiftRoaster",
                    ModalTitle = "शिफ्ट  विवरण मेटाउनुहोस्",
                    CRUDAction = CRUDType.DELETE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Delete,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    IdHRCompany = idHRCompany,
                    DataModel = await this._hRCompanyShiftRoasterServices.GetModelByIdAsync(id)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [ValidateInput(true)]
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> _DeleteHRCompanyShiftRoasterAsync(HRCompanyShiftRoasterViewModel viewModel, FormCollection formCollection)
        {
            return await this.CRUDHRCompanyShiftRoasterAsync(viewModel, formCollection, CRUDType.DELETE);
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintHRCompanyShiftRoasterAsync(long? idHRCompany)
        {
            try
            {
                return PartialView(new HRCompanyShiftRoasterViewModel
                {
                    SessionDetails = this.SessionDetail,
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyShiftRoaster",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyShiftRoaster",
                    BreadCrumbTitle = "शिफ्ट व्यवस्थापन",
                    BreadCrumbActionName = "_PrintHRCompanyShiftRoasterAsync",
                    FormModelName = "HRCompanyShiftRoaster",
                    ModalTitle = "शिफ्ट  सूची छाप्नुहोस्",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [NonAction]
        private async Task<ActionResult> RETSourceHRCompanyShiftRoasterAsync(HRCompanyShiftRoasterViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
                viewModel.SessionDetails = SessionDetail;
                viewModel.BreadCrumbArea = "CompanyManagement";
                viewModel.BreadCrumbController = "HRCompanyShiftRoaster";
                viewModel.BreadCrumbBaseURL = "CompanyManagement/HRCompanyShiftRoaster";
                viewModel.FormModelName = "HRCompanyShiftRoaster";
                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        viewModel.ModalTitle = "नयाँ शिफ्ट सिर्जना गर्नुहोस्";
                        viewModel.BreadCrumbActionName = "_CreateHRCompanyShiftRoasterAsync";
                        viewModel.CRUDAction = CRUDType.CREATE;
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.UPDATE:
                        viewModel.ModalTitle = "शिफ्ट सम्पादन गर्नुहोस्";
                        viewModel.BreadCrumbActionName = "_UpdateHRCompanyShiftRoasterAsync";
                        viewModel.CRUDAction = CRUDType.UPDATE;
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.DELETE:
                        viewModel.ModalTitle = "शिफ्ट  विवरण मेटाउनुहोस्";
                        viewModel.BreadCrumbActionName = "_DeleteHRCompanyShiftRoasterAsync";
                        viewModel.CRUDAction = CRUDType.DELETE;
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
        private async Task<ActionResult> CRUDHRCompanyShiftRoasterAsync(HRCompanyShiftRoasterViewModel viewModel, FormCollection formCollection, CRUDType cRUDType)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await this.RETSourceHRCompanyShiftRoasterAsync(viewModel, cRUDType);
                }
                await this._hRCompanyShiftRoasterServices.CommitSaveChangesAsync(viewModel.DataModel, cRUDType);
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await this.RETSourceHRCompanyShiftRoasterAsync(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.IdHRCompany.ToString(), AlertNotificationType.success);
            return RedirectToAction("_ListHRCompanyShiftRoasterAsync", new { idHRCompany = viewModel.IdHRCompany, pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }
    }
}