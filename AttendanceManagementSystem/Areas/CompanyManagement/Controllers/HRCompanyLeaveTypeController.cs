using AttendanceManagementSystem.App_Auth;
using AttendanceManagementSystem.Controllers;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using SystemDatabase;
using SystemModels.SystemSetting;
using SystemServices.SystemSetting;
using SystemViewModels.SystemSetting;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;

namespace AttendanceManagementSystem.Areas.CompanyManagement.Controllers
{

    public class HRCompanyLeaveTypeController : BaseController
    {
        // GET: CompanyManagement/HRCompanyLeaveType

        private readonly HRCompanyLeaveTypeServices _hRCompanyLeaveTypeServices;

        public HRCompanyLeaveTypeController()
        {
            this._hRCompanyLeaveTypeServices = new HRCompanyLeaveTypeServices(this._unitOfWork);
        }

        #region LeaveType

        [NonAction]
        public Expression<Func<HRCompanyLeaveType, bool>> Condition(long? idHRCompany, string searchKey = "")
        {
            Expression<Func<HRCompanyLeaveType, bool>> returnData = (x => false);
            switch (this.SessionDetail.IdRoleType)
            {
                case (int)RoleType.SuperUser:
                    return returnData = (x => x.IdHRCompany == idHRCompany && ( searchKey == ""));
                case (int)RoleType.Admin:
                    return returnData = (x => x.IdHRCompany == idHRCompany && ( searchKey == ""));
                case (int)RoleType.RootUser:
                    return returnData = (x => x.IdHRCompany == idHRCompany && ( searchKey == ""));
                case (int)RoleType.NormalUser:
                    return returnData = (x => x.IdHRCompany == SessionDetail.IdHRCompany && (searchKey == ""));
                case (int)RoleType.SectionAdmin:
                    return returnData = (x => x.IdHRCompany == SessionDetail.IdHRCompany && (searchKey == ""));
                default:
                    return returnData;
            }
        }

        [AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListHRCompanyLeaveTypeAsync(long? idHRCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                if (idHRCompany == null || idHRCompany == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRCompanyLeaveTypeViewModelList
                {
                    SessionDetails = SessionDetail,
                    DBModelList = await _hRCompanyLeaveTypeServices.GetPagedList(Condition(idHRCompany, searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyLeaveType",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyLeaveType",
                    BreadCrumbActionName = "_ListHRCompanyLeaveTypeAsync",
                    BreadCrumbTitle = "बिदाको प्रकार",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    IdHRCompany = idHRCompany,
                    OrderingDirection = pagination.OrderingDirection
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadListHRCompanyLeaveTypeAsync(long? idHRCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                if (idHRCompany == null || idHRCompany == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRCompanyLeaveTypeViewModelList
                {
                    SessionDetails = SessionDetail,
                    DBModelList = await _hRCompanyLeaveTypeServices.GetPagedList(Condition(idHRCompany, searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyLeaveType",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyLeaveType",
                    BreadCrumbActionName = "_ReadListHRCompanyLeaveTypeAsync",
                    BreadCrumbTitle = "बिदाको प्रकार",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    IdHRCompany = idHRCompany,
                    OrderingDirection = pagination.OrderingDirection
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListWrapperHRCompanyLeaveTypeAsync(long? idHRCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                if (idHRCompany == null || idHRCompany == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRCompanyLeaveTypeViewModelList
                {
                    SessionDetails = SessionDetail,
                    DBModelList = await _hRCompanyLeaveTypeServices.GetPagedList(Condition(idHRCompany, searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyLeaveType",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyLeaveType",
                    BreadCrumbActionName = "_ListHRCompanyLeaveTypeAsync",
                    BreadCrumbTitle = "बिदाको प्रकार",
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
        public async Task<ActionResult> _CreateHRCompanyLeaveTypeAsync(long? idHRCompany)
        {
            try
            {
                if (idHRCompany == null || idHRCompany == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                return PartialView(new HRCompanyLeaveTypeViewModel
                {
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyLeaveType",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyLeaveType",
                    BreadCrumbActionName = "_CreateHRCompanyLeaveTypeAsync",
                    FormModelName = "HRCompanyLeaveType",
                    ModalTitle = "नयाँ विदाको प्रकार थप्नुहोस्",
                    BreadCrumbTitle = "बिदाको प्रकार",
                    CRUDAction = CRUDType.CREATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Create,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId, idHRCompany),
                    DataModel = new HRCompanyLeaveTypeModel { IdHRCompany = (long)idHRCompany, IsActive = true },
                    IdHRCompany = idHRCompany,
                    DDLMasterLeaveTitle = await GetIntStringPairModel(PairModelTitle.UnAssignedMasterLeaveTitle, idHRCompany)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadHRCompanyLeaveTypeAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _hRCompanyLeaveTypeServices.GetModelByIdAsync(id);
                return PartialView(new HRCompanyLeaveTypeViewModel
                {
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyLeaveType",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyLeaveType",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_ReadHRCompanyLeaveTypeAsync",
                    FormModelName = "HRCompanyLeaveType",
                    ModalTitle = "विदाको प्रकारको विस्तार",
                    BreadCrumbTitle = "बिदाको प्रकार",
                    CRUDAction = CRUDType.READ,
                    OnlyCancelButton = true,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = model,
                    IdHRCompany = model.IdHRCompany,
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId, model.IdHRCompany),
                    DDLMasterLeaveTitle = await GetIntStringPairModel(PairModelTitle.MasterLeaveTitle)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _UpdateHRCompanyLeaveTypeAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _hRCompanyLeaveTypeServices.GetModelByIdAsync(id);
                return PartialView(new HRCompanyLeaveTypeViewModel
                {
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyLeaveType",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyLeaveType",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_UpdateHRCompanyLeaveTypeAsync",
                    FormModelName = "HRCompanyLeaveType",
                    ModalTitle = "विदाको प्रकार सम्पादन गर्नुहोस्",
                    BreadCrumbTitle = "बिदाको प्रकार",
                    CRUDAction = CRUDType.UPDATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Update,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = model,
                    IdHRCompany = model.IdHRCompany,
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId, model.IdHRCompany),
                    DDLMasterLeaveTitle = await GetIntStringPairModel(PairModelTitle.MasterLeaveTitle)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _DeleteHRCompanyLeaveTypeAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _hRCompanyLeaveTypeServices.GetModelByIdAsync(id);
                return PartialView(new HRCompanyLeaveTypeViewModel
                {
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyLeaveType",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyLeaveType",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    FormModelName = "HRCompanyLeaveType",
                    ModalTitle = "बिदाको प्रकार मेटाउनुहोस्",
                    BreadCrumbTitle = "बिदाको प्रकार",
                    BreadCrumbActionName = "_DeleteHRCompanyLeaveTypeAsync",
                    CRUDAction = CRUDType.DELETE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Delete,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = model,
                    IdHRCompany = model.IdHRCompany,
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId, model.IdHRCompany),
                    DDLMasterLeaveTitle = await GetIntStringPairModel(PairModelTitle.MasterLeaveTitle)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ExportHRCompanyLeaveTypeAsync(long? idHRCompany)
        {
            try
            {
                if (idHRCompany == null || idHRCompany == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var model = await _hRCompanyLeaveTypeServices.FindAllAsync(Condition(idHRCompany));
                var data = model.Select(x => new
                {
                    Company=(x.HRCompany?.HRCompanyCode?.HRCompanyCodeTitle),
                    LeaveType=(x.MasterLeaveTitle?.LeaveTitleNP),
                    Inshort=(x.MasterLeaveTitle?.LeaveShortTitle),
                    IsWeekend=x.IsIncludeWeekend.ToString() ,
                    IsPublicHoliday=x.IsIncludePublicHoliday.ToString() ,
                    IsSaving= x.IsSaving.ToString() ,
                    ForFemale= x.IsForMale.ToString() ,
                    ForMale= x.IsForMale.ToString(),
                    TotalLeaveDay= x.TotalLeaveDay,
                    SavingLimitDay=x.SavingLimitDay,

                });
                return await ExportToExcel("HRCompanyLeaveType", data);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintHRCompanyLeaveTypeAsync(long? idHRCompany)
        {
            try
            {
                if (idHRCompany == null || idHRCompany == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                return PartialView(new HRCompanyLeaveTypeViewModel
                {
                    SessionDetails = SessionDetail,
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyLeaveType",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyLeaveType",
                    BreadCrumbActionName = "_PrintHRCompanyLeaveTypeAsync",
                    FormModelName = "HRCompanyLeaveType",
                    ModalTitle = "बिदाको प्रकार सुचि छाप्नुहोस",
                    BreadCrumbTitle = "Leave Type",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBModelList = await _hRCompanyLeaveTypeServices.FindAllAsync(Condition(idHRCompany)),
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
        public async Task<ActionResult> _CreateHRCompanyLeaveTypeAsync(HRCompanyLeaveTypeViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHRCompanyLeaveType(viewModel, CRUDType.CREATE, formCollection);
        }

        [AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _UpdateHRCompanyLeaveTypeAsync(HRCompanyLeaveTypeViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHRCompanyLeaveType(viewModel, CRUDType.UPDATE, formCollection);
        }

        [AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _DeleteHRCompanyLeaveTypeAsync(HRCompanyLeaveTypeViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHRCompanyLeaveType(viewModel, CRUDType.DELETE, formCollection);
        }

        [NonAction]
        protected async Task<ActionResult> RETSourceHRCompanyLeaveType(HRCompanyLeaveTypeViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
                viewModel.SessionDetails = SessionDetail;
                viewModel.BreadCrumbArea = "CompanyManagement";
                viewModel.BreadCrumbController = "HRCompanyLeaveType";
                viewModel.BreadCrumbBaseURL = "CompanyManagement/HRCompanyLeaveType";
                viewModel.DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId, viewModel.DataModel.IdHRCompany);
                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        viewModel.SubmitButtonText = SubmitButtonText.Create;
                        viewModel.BreadCrumbActionName = "_CreateHRCompanyLeaveTypeAsync";
                        viewModel.ModalTitle = "नयाँ विदाको प्रकार थप्नुहोस्";
                        viewModel.FormModelName = "HRCompanyLeaveType";
                        viewModel.CRUDAction = CRUDType.CREATE;
                        viewModel.DDLMasterLeaveTitle = await GetIntStringPairModel(PairModelTitle.UnAssignedMasterLeaveTitle, viewModel.DataModel.IdHRCompany);
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);

                    case CRUDType.UPDATE:
                        viewModel.BreadCrumbActionName = "_UpdateHRCompanyLeaveTypeAsync";
                        viewModel.ModalTitle = "विदाको प्रकार सम्पादन गर्नुहोस्";
                        viewModel.FormModelName = "HRCompanyLeaveType";
                        viewModel.CRUDAction = CRUDType.UPDATE;
                        viewModel.DDLMasterLeaveTitle = await GetIntStringPairModel(PairModelTitle.MasterLeaveTitle);
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);

                    case CRUDType.DELETE:
                        viewModel.BreadCrumbActionName = "_DeleteHRCompanyLeaveTypeAsync";
                        viewModel.ModalTitle = "बिदाको प्रकार मेटाउनुहोस्";
                        viewModel.FormModelName = "HRCompanyLeaveType";
                        viewModel.CRUDAction = CRUDType.DELETE;
                        viewModel.DDLMasterLeaveTitle = await GetIntStringPairModel(PairModelTitle.MasterLeaveTitle);
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
        protected async Task<ActionResult> CRUDHRCompanyLeaveType(HRCompanyLeaveTypeViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceHRCompanyLeaveType(viewModel, cRUDType);
                }
                await _hRCompanyLeaveTypeServices.CommitSaveChangesAsync(viewModel.DataModel, cRUDType);
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceHRCompanyLeaveType(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListHRCompanyLeaveTypeAsync", new { idHRCompany = viewModel.DataModel.IdHRCompany, pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }
        #endregion
    }
}