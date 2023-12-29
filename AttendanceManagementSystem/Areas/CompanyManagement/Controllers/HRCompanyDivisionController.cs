using AttendanceManagementSystem.App_Auth;
using AttendanceManagementSystem.Controllers;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
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
    public class HRCompanyDivisionController : BaseController
    {
        // GET: CompanyManagement/HRCompanyDivision
        private readonly HRCompanyDivisionServices _hRDivisionServices;

        public HRCompanyDivisionController()
        {
            this._hRDivisionServices = new HRCompanyDivisionServices(this._unitOfWork);
        }

        #region Division
        [NonAction]
        public Expression<Func<HRCompanyDivision, bool>> Condition(long? idHRCompany, string searchKey = "")
        {
            Expression<Func<HRCompanyDivision, bool>> retutndata = (x => false);
            if (this.SessionDetail.IdRoleType == 0) retutndata = (x => x.IdHRCompany == idHRCompany && (x.HRCompanyDivisionName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
            else if (this.SessionDetail.IdRoleType == 1)
                retutndata = (x => x.IdHRCompany == idHRCompany && (x.HRCompanyDivisionName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
            else if (this.SessionDetail.IdRoleType == 2)
                retutndata = (x => x.Id == SessionDetail.IdHRCompanyDivision && (x.HRCompanyDivisionName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
            else if (this.SessionDetail.IdRoleType == 4)
                retutndata = (x => x.Id == SessionDetail.IdHRCompanyDivision && (x.HRCompanyDivisionName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
            else retutndata = (x => false);
            return retutndata;
        }

        [AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListWrapperHRDivisionAsync(long? idHRCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (idHRCompany == null || idHRCompany == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, "IdDivisionType", orderingDirection);
                return PartialView(new HRCompanyDivisionViewModelList
                {
                    SessionDetails = SessionDetail,
                    DBModelList = await _hRDivisionServices.GetPagedList(Condition(idHRCompany, searchKey), pageNumber, pageSize, orderingBy, orderingDirection),
                    BreadCrumbTitle = "Office Management",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyDivision",
                    BreadCrumbActionName = "_ListWrapperHRDivisionAsync",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyDivision",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    IdHRCompany = idHRCompany
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListHRDivisionAsync(long? idHRCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                if (idHRCompany == null || idHRCompany == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, "IdDivisionType", orderingDirection);
                return PartialView(new HRCompanyDivisionViewModelList
                {
                    SessionDetails = SessionDetail,
                    DBModelList = await _hRDivisionServices.GetPagedList(Condition(idHRCompany, searchKey), pageNumber, pageSize, orderingBy, orderingDirection),
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyDivision",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyDivision",
                    BreadCrumbActionName = "_ListHRDivisionAsync",
                    BreadCrumbTitle = "Division/Section",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    IdHRCompany = idHRCompany
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }


        [AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadListHRDivisionAsync(long? idHRCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                if (idHRCompany == null || idHRCompany == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, "IdDivisionType", orderingDirection);
                return PartialView(new HRCompanyDivisionViewModelList
                {
                    SessionDetails = SessionDetail,
                    DBModelList = await _hRDivisionServices.GetPagedList(Condition(idHRCompany, searchKey), pageNumber, pageSize, orderingBy, orderingDirection),
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyDivision",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyDivision",
                    BreadCrumbActionName = "_ReadListHRDivisionAsync",
                    BreadCrumbTitle = "Division/Section",
                    CRUDAction = CRUDType.READ,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText=CancelButtonText.Cancel,
                    OnlyCancelButton = true,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    IdHRCompany = idHRCompany
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }


        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _CreateHRDivisionAsync(long? idHRCompany)
        {
            try
            {
                return PartialView(new HRCompanyDivisionViewModel
                {
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyDivision",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyDivision",
                    BreadCrumbTitle = "Office Management",
                    BreadCrumbActionName = "_CreateHRDivisionAsync",
                    FormModelName = "HRDivision",
                    ModalTitle = "नयाँ शाखा/सेक्सन थप्नुहोस्",
                    CRUDAction = CRUDType.CREATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Create,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    IdHRCompany = idHRCompany,
                    DataModel = new HRCompanyDivisionModel { Id = 0 },
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId, idHRCompany),
                    DDLDivision = await GetLongStringPairModel(PairModelTitle.Division, idHRCompany),
                    DDLDivisionType = await GetLongStringPairModel(PairModelTitle.DivisionType, idHRCompany)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadHRDivisionAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _hRDivisionServices.GetModelByIdAsync(id);
                return PartialView(new HRCompanyDivisionViewModel
                {
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyDivision",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyDivision",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_ReadHRDivisionAsync",
                    FormModelName = "HRDivision",
                    ModalTitle = "शाखा/सेक्सन विवरण हेर्नुहोस्",
                    CRUDAction = CRUDType.READ,
                    OnlyCancelButton = true,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId, model.IdHRCompany),
                    DDLDivisionType = await GetLongStringPairModel(PairModelTitle.DivisionType, model.IdHRCompany),
                    DataModel = model,
                    IdHRCompany = model.IdHRCompany,
                    DDLDivision = await GetLongStringPairModel(PairModelTitle.Division, model.IdHRCompany)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _UpdateHRDivisionAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _hRDivisionServices.GetModelByIdAsync(id);
                return PartialView(new HRCompanyDivisionViewModel
                {
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyDivision",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyDivision",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_UpdateHRDivisionAsync",
                    FormModelName = "HRDivision",
                    ModalTitle = "शाखा/सेक्सन सम्पादन गर्नुहोस्",
                    BreadCrumbTitle = "Office Management",
                    CRUDAction = CRUDType.UPDATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Update,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId, model.IdHRCompany),
                    DDLDivisionType = await GetLongStringPairModel(PairModelTitle.DivisionType, model.IdHRCompany),
                    DDLDivision = await GetLongStringPairModel(PairModelTitle.Division, model.IdHRCompany),
                    DataModel = model,
                    IdHRCompany = model.IdHRCompany
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _DeleteHRDivisionAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _hRDivisionServices.GetModelByIdAsync(id);
                return PartialView(new HRCompanyDivisionViewModel
                {
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyDivision",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyDivision",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    FormModelName = "HRDivision",
                    ModalTitle = "शाखा/सेक्सन विवरण मेटाउनुहोस्",
                    BreadCrumbTitle = "Office Managemen",
                    BreadCrumbActionName = "_DeleteHRDivisionAsync",
                    CRUDAction = CRUDType.DELETE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Delete,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId, model.IdHRCompany),
                    DDLDivisionType = await GetLongStringPairModel(PairModelTitle.DivisionType, model.IdHRCompany),
                    DDLDivision = await GetLongStringPairModel(PairModelTitle.Division, model.IdHRCompany),
                    DataModel = model,
                    IdHRCompany = model.IdHRCompany
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ExportHRDivisionAsync(long? idHRCompany)
        {
            try
            {
                var model = await _hRDivisionServices.FindAllAsync(Condition(idHRCompany));
                var data=model.Select(x=>new{
                Company= x.HRCompany?.HRCompanyCode?.HRCompanyCodeTitle,
                Type= x.HRCompanyDivisionType?.HRCompanyDivisionTypeTitle,
                DivisionName=x.HRCompanyDivisionName,
                CompanyDivisionShortName=x.HRCompanyDivisionShortName,
                });
                return await ExportToExcel("DivisionList", data);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintHRDivisionAsync(long? idHRCompany)
        {
            try
            {
                return PartialView(new HRCompanyDivisionViewModel
                {
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyDivision",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyDivision",
                    BreadCrumbActionName = "_PrintHRDivisionAsync",
                    FormModelName = "HRDivision",
                    ModalTitle = "शाखा सूची छाप्नुहोस",
                    BreadCrumbTitle = "Office Management",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBModelList = await _hRDivisionServices.FindAllAsync(Condition(idHRCompany)),
                    IdHRCompany = idHRCompany,
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _CreateHRDivisionAsync(HRCompanyDivisionViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHRDivisionAsync(viewModel, CRUDType.CREATE, formCollection);
        }

        [AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _UpdateHRDivisionAsync(HRCompanyDivisionViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHRDivisionAsync(viewModel, CRUDType.UPDATE, formCollection);
        }

        [AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _DeleteHRDivisionAsync(HRCompanyDivisionViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHRDivisionAsync(viewModel, CRUDType.DELETE, formCollection);
        }

        [NonAction]
        protected async Task<ActionResult> RETSourceHRDivisionAsync(HRCompanyDivisionViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
                viewModel.SessionDetails = SessionDetail;
                viewModel.BreadCrumbArea = "CompanyManagement";
                viewModel.BreadCrumbController = "HRCompanyDivision";
                viewModel.BreadCrumbBaseURL = "CompanyManagement/HRCompanyDivision";
                viewModel.DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId, viewModel.DataModel.IdHRCompany);
                viewModel.DDLDivisionType = await GetLongStringPairModel(PairModelTitle.DivisionType, viewModel.DataModel.IdHRCompany);
                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        viewModel.SubmitButtonText = SubmitButtonText.Create;
                        viewModel.BreadCrumbActionName = "_CreateHRDivisionAsync";
                        viewModel.ModalTitle = "नयाँ शाखा/सेक्सन थप्नुहोस्";
                        viewModel.FormModelName = "HRDivision";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.UPDATE:
                        viewModel.BreadCrumbActionName = "_UpdateHRDivisionAsync";
                        viewModel.ModalTitle = "शाखा/सेक्सन सम्पादन गर्नुहोस्";
                        viewModel.FormModelName = "HRDivision";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.DELETE:
                        viewModel.BreadCrumbActionName = "_DeleteHRDivisionAsync";
                        viewModel.ModalTitle = "शाखा/सेक्सन विवरण मेटाउनुहोस्";
                        viewModel.FormModelName = "HRDivision";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.PRINT:
                        viewModel.BreadCrumbActionName = "_PrintHRDivisionAsync";
                        viewModel.ModalTitle = "शाखा सूची छाप्नुहोस";
                        viewModel.FormModelName = "HRDivision";
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
        protected async Task<ActionResult> CRUDHRDivisionAsync(HRCompanyDivisionViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceHRDivisionAsync(viewModel, cRUDType);
                }
                await _hRDivisionServices.CommitSaveChangesAsync(viewModel.DataModel, cRUDType);
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceHRDivisionAsync(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListHRDivisionAsync", new { idHRCompany = viewModel.DataModel.IdHRCompany, pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }
        #endregion
    }
}