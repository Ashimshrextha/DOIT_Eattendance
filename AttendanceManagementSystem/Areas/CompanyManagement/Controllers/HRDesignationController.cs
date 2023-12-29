using AttendanceManagementSystem.App_Auth;
using AttendanceManagementSystem.Controllers;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using SystemDatabase;
using SystemModels.EmployeeManagement;
using SystemServices.SystemSetting;
using SystemViewModels.SystemSetting;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;

namespace AttendanceManagementSystem.Areas.CompanyManagement.Controllers
{
    public class HRDesignationController : BaseController
    {
        // GET: CompanyManagement/HRDesignation
        private readonly HRDesignationServices _hRDesignationServices;

        public HRDesignationController()
        {
            this._hRDesignationServices = new HRDesignationServices(this._unitOfWork);
        }

        #region Designation

        [NonAction]
        public Expression<Func<HRDesignation, bool>> Condition(long? idHRCompany, string searchKey = "")
        {
            Expression<Func<HRDesignation, bool>> retutndata = (x => false);

            if (this.SessionDetail.IdRoleType == 0) retutndata = (x => x.IdHRCompany == idHRCompany && (x.HRDesignationTitle.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));

            else if (this.SessionDetail.IdRoleType == 1 || SessionDetail.IdRoleType == 2 || SessionDetail.IdRoleType == 4) retutndata = (x => x.IdHRCompany == idHRCompany && (x.HRDesignationTitle.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));

            else retutndata = (x => false);

            return retutndata;
        }

        [AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListHRDesignationAsync(long? idHRCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                if (idHRCompany == null || idHRCompany == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, "HRDesignationOrder", orderingDirection);
                return PartialView(new HRDesignationViewModelList
                {
                    SessionDetails = SessionDetail,
                    DBModelList = await _hRDesignationServices.GetPagedList(Condition(idHRCompany, searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRDesignation",
                    BreadCrumbBaseURL = "CompanyManagement/HRDesignation",
                    BreadCrumbActionName = "_ListHRDesignationAsync",
                    BreadCrumbTitle = "पद",
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
        public async Task<ActionResult> _ReadListHRDesignationAsync(long? idHRCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                if (idHRCompany == null || idHRCompany == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, "HRDesignationOrder", orderingDirection);
                return PartialView(new HRDesignationViewModelList
                {
                    SessionDetails = SessionDetail,
                    DBModelList = await _hRDesignationServices.GetPagedList(Condition(idHRCompany, searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRDesignation",
                    BreadCrumbBaseURL = "CompanyManagement/HRDesignation",
                    BreadCrumbActionName = "_ReadListHRDesignationAsync",
                    BreadCrumbTitle = "पद",
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
        public async Task<ActionResult> _ListWrapperHRDesignationAsync(long? idHRCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                if (idHRCompany == null || idHRCompany == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, "HRDesignationOrder", orderingDirection);
                return PartialView(new HRDesignationViewModelList
                {
                    SessionDetails = SessionDetail,
                    DBModelList = await _hRDesignationServices.GetPagedList(Condition(idHRCompany, searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRDesignation",
                    BreadCrumbBaseURL = "CompanyManagement/HRDesignation",
                    BreadCrumbActionName = "_ListWrapperHRDesignationAsync",
                    BreadCrumbTitle = "पद",
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

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _CreateHRDesignationAsync(long? idHRCompany)
        {
            try
            {
                if (idHRCompany == null || idHRCompany == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                return PartialView(new HRDesignationViewModel
                {
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRDesignation",
                    BreadCrumbBaseURL = "CompanyManagement/HRDesignation",
                    BreadCrumbActionName = "_CreateHRDesignationAsync",
                    FormModelName = "HRDesignation",
                    ModalTitle = "नयाँ पद विवरण थप्नुहोस्",
                    BreadCrumbTitle = "पद",
                    CRUDAction = CRUDType.CREATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Create,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId, idHRCompany),
                    DDLDesignationLeaveApproval = await GetLongStringPairModel(PairModelTitle.DesignationLeaveApproval, idHRCompany),
                    DDLEmployeeCategory = await GetLongStringPairModel(PairModelTitle.EmployeeCategory, idHRCompany),
                    DataModel = new HRDesignationModel { Id = 0, IdHRCompany = (long)idHRCompany },
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
        public async Task<ActionResult> _ReadHRDesignationAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _hRDesignationServices.GetModelByIdAsync(id);
                return PartialView(new HRDesignationViewModel
                {
                    SessionDetails = SessionDetail,
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRDesignation",
                    BreadCrumbBaseURL = "CompanyManagement/HRDesignation",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_ReadHRDesignationAsync",
                    FormModelName = "HRDesignation",
                    ModalTitle = "View Employee Designation Detail",
                    BreadCrumbTitle = "Designation",
                    CRUDAction = CRUDType.READ,
                    OnlyCancelButton = true,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = model,
                    IdHRCompany = model.IdHRCompany,
                    DDLEmployeeCategory = await GetLongStringPairModel(PairModelTitle.EmployeeCategory, model.IdHRCompany),
                    DDLDesignationLeaveApproval = await GetLongStringPairModel(PairModelTitle.DesignationLeaveApproval, model.IdHRCompany),
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId, model.IdHRCompany)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _UpdateHRDesignationAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _hRDesignationServices.GetModelByIdAsync(id);
                return PartialView(new HRDesignationViewModel
                {
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRDesignation",
                    BreadCrumbBaseURL = "CompanyManagement/HRDesignation",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_UpdateHRDesignationAsync",
                    FormModelName = "HRDesignation",
                    ModalTitle = "पद सम्पादन गर्नुहोस्",
                    CRUDAction = CRUDType.UPDATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Update,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = model,
                    IdHRCompany = model.IdHRCompany,
                    DDLEmployeeCategory = await GetLongStringPairModel(PairModelTitle.EmployeeCategory, model.IdHRCompany),
                    DDLDesignationLeaveApproval = await GetLongStringPairModel(PairModelTitle.DesignationLeaveApproval, model.IdHRCompany),
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId, model.IdHRCompany)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _DeleteHRDesignationAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _hRDesignationServices.GetModelByIdAsync(id);
                return PartialView(new HRDesignationViewModel
                {
                    SessionDetails = SessionDetail,
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRDesignation",
                    BreadCrumbBaseURL = "CompanyManagement/HRDesignation",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    FormModelName = "HRDesignation",
                    ModalTitle = "पद मेटाउन निश्चित हुनुहुन्छ?",
                    BreadCrumbActionName = "_DeleteHRDesignationAsync",
                    CRUDAction = CRUDType.DELETE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Delete,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = model,
                    IdHRCompany = model.IdHRCompany,
                    DDLEmployeeCategory = await GetLongStringPairModel(PairModelTitle.EmployeeCategory, model.IdHRCompany),
                    DDLDesignationLeaveApproval = await GetLongStringPairModel(PairModelTitle.DesignationLeaveApproval, model.IdHRCompany),
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId, model.IdHRCompany)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ExportHRDesignationAsync(long? idHRCompany)
        {
            try
            {
                var model = await _hRDesignationServices.FindAllAsync(Condition(idHRCompany));
                var data = model.Select(x => new
                {
                    Office = x.HRCompany.HRCompanyCode.HRCompanyCodeTitle,
                    Category = x.HREmployeeCategory.HREmployeeCategoryTitle,
                    Designation = x.HRDesignationTitle,
                    DesignationInShort = x.HRDesignationShortName,
                    DesignationOrder = x.HRDesignationOrder,
                    DesignationRank = x.HRDesignationRank,
                    SalaryIncrementNumber = x.SalaryIncrementNumber,
                    LeaveApprovedBY = x.HRDesignation2 == null ? "नेपाल सरकार" : x.HRDesignation2.HRDesignationTitle,

                });
                return await ExportToExcel("HRDesignation", data);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintHRDesignationAsync(long? idHRCompany)
        {
            try
            {
                return PartialView(new HRDesignationViewModel
                {
                    SessionDetails = SessionDetail,
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRDesignation",
                    BreadCrumbBaseURL = "CompanyManagement/HRDesignation",
                    BreadCrumbActionName = "_PrintHRDesignationAsync",
                    FormModelName = "HRDesignation",
                    ModalTitle = "पद सुची छाप्नुहोस",
                    BreadCrumbTitle = "Designation",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    IdHRCompany = idHRCompany,
                    DBModelList = await _hRDesignationServices.FindAllAsync(Condition(idHRCompany))
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
        public async Task<ActionResult> _CreateHRDesignationAsync(HRDesignationViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHRDesignation(viewModel, CRUDType.CREATE, formCollection);
        }

        [AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _UpdateHRDesignationAsync(HRDesignationViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHRDesignation(viewModel, CRUDType.UPDATE, formCollection);
        }

        [AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _DeleteHRDesignationAsync(HRDesignationViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHRDesignation(viewModel, CRUDType.DELETE, formCollection);
        }

        [NonAction]
        protected async Task<ActionResult> RETSourceHRDesignation(HRDesignationViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
                viewModel.SessionDetails = SessionDetail;
                viewModel.BreadCrumbArea = "CompanyManagement";
                viewModel.BreadCrumbController = "HRDesignation";
                viewModel.BreadCrumbBaseURL = "CompanyManagement/HRDesignation";
                viewModel.IdHRCompany = viewModel.DataModel.IdHRCompany;
                viewModel.DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId, viewModel.DataModel.IdHRCompany);
                viewModel.DDLEmployeeCategory = await GetLongStringPairModel(PairModelTitle.EmployeeCategory, viewModel.DataModel.IdHRCompany);
                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        viewModel.SubmitButtonText = SubmitButtonText.Create;
                        viewModel.BreadCrumbActionName = "_CreateHRDesignationAsync";
                        viewModel.ModalTitle = "नयाँ पद विवरण थप्नुहोस्";
                        viewModel.FormModelName = "HRDesignation";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.UPDATE:
                        viewModel.BreadCrumbActionName = "_UpdateHRDesignationAsync";
                        viewModel.ModalTitle = "पद सम्पादन गर्नुहोस्";
                        viewModel.FormModelName = "HRDesignation";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.DELETE:
                        viewModel.BreadCrumbActionName = "_DeleteHRDesignationAsync";
                        viewModel.ModalTitle = "पद मेटाउन निश्चित हुनुहुन्छ?";
                        viewModel.FormModelName = "HRDesignation";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.PRINT:
                        viewModel.BreadCrumbActionName = "_PrintHRDesignationAsync";
                        viewModel.ModalTitle = "पद सुची छाप्नुहोस";
                        viewModel.FormModelName = "HRDesignation";
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
        protected async Task<ActionResult> CRUDHRDesignation(HRDesignationViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceHRDesignation(viewModel, cRUDType);
                }
                viewModel.DataModel.IdHRDesignationLeaveApproval = viewModel.DataModel.IdHRDesignationLeaveApproval == 0 ? null : viewModel.DataModel.IdHRDesignationLeaveApproval;
                await _hRDesignationServices.CommitSaveChangesAsync(viewModel.DataModel, cRUDType);
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceHRDesignation(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListHRDesignationAsync", new { idHRCompany = viewModel.DataModel.IdHRCompany, pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }
        #endregion
    }
}