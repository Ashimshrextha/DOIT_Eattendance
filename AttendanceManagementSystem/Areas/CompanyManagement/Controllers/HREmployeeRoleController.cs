using AttendanceManagementSystem.App_Auth;
using AttendanceManagementSystem.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SystemDatabase;
using SystemModels.SystemSetting;
using SystemServices.SystemSetting;
using SystemViewModels.SystemSetting;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;

namespace AttendanceManagementSystem.Areas.CompanyManagement.Controllers
{
    public class HREmployeeRoleController : BaseController
    {
        // GET: CompanyManagement/HREmployeeRole

        private readonly HREmployeeRoleServices _hREmployeeRoleServices;

        public HREmployeeRoleController()
        {
            this._hREmployeeRoleServices = new HREmployeeRoleServices(this._unitOfWork);
        }



        #region Role Type
        [NonAction]
        public Expression<Func<HREmployeeRole, bool>> Condition(long? idHRCompany, string searchKey = "")
        {
            Expression<Func<HREmployeeRole, bool>> retutndata = (x => false);

            if (this.SessionDetail.IdRoleType == 0) retutndata = (x => x.IdHRCompany == idHRCompany && x.IdRoleType!=0 && (x.HRRoleTitle.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));

            else if (this.SessionDetail.IdRoleType == 1 || SessionDetail.IdRoleType == 2 || SessionDetail.IdRoleType == 4) retutndata = (x => x.IdHRCompany == idHRCompany && (x.HRRoleTitle.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));

            else retutndata = (x => false);

            return retutndata;
        }

        [AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListWrapperHREmployeeRoleAsync(long? idHRCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HREmployeeRoleViewModelList
                {
                    SessionDetails = SessionDetail,
                    DBModelList = await _hREmployeeRoleServices.GetPagedList(Condition(idHRCompany, searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HREmployeeRole",
                    BreadCrumbBaseURL = "CompanyManagement/HREmployeeRole",
                    BreadCrumbActionName = "_ListWrapperHREmployeeRoleAsync",
                    BreadCrumbTitle = "कर्मचारी भूमिका",
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
        public async Task<ActionResult> _ListHREmployeeRoleAsync(long? idHRCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                if (idHRCompany == 0 || idHRCompany == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HREmployeeRoleViewModelList
                {
                    SessionDetails = SessionDetail,
                    DBModelList = await _hREmployeeRoleServices.GetPagedList(Condition(idHRCompany, searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HREmployeeRole",
                    BreadCrumbBaseURL = "CompanyManagement/HREmployeeRole",
                    BreadCrumbActionName = "_ListHREmployeeRoleAsync",
                    BreadCrumbTitle = "कर्मचारी भूमिका",
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
        public async Task<ActionResult> _ReadListHREmployeeRoleAsync(long? idHRCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                if (idHRCompany == 0 || idHRCompany == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HREmployeeRoleViewModelList
                {
                    SessionDetails = SessionDetail,
                    DBModelList = await _hREmployeeRoleServices.GetPagedList(Condition(idHRCompany, searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HREmployeeRole",
                    BreadCrumbBaseURL = "CompanyManagement/HREmployeeRole",
                    BreadCrumbActionName = "_ReadListHREmployeeRoleAsync",
                    BreadCrumbTitle = "कर्मचारी भूमिका",
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
        public async Task<ActionResult> _CreateHREmployeeRoleAsync(long? idHRCompany)
        {
            try
            {
                if (idHRCompany == 0 || idHRCompany == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                else
                    return PartialView(new HREmployeeRoleViewModel
                    {
                        SessionDetails = SessionDetail,
                        BreadCrumbArea = "CompanyManagement",
                        BreadCrumbController = "HREmployeeRole",
                        BreadCrumbBaseURL = "CompanyManagement/HREmployeeRole",
                        BreadCrumbActionName = "_CreateHREmployeeRoleAsync",
                        FormModelName = "HREmployeeRole",
                        ModalTitle = "नयाँ कर्मचारी भूमिका थप्नुहोस्",
                        BreadCrumbTitle = "कर्मचारी भूमिका",
                        CRUDAction = CRUDType.CREATE,
                        SubmitButtonType = SubmitButtonType.submit,
                        SubmitButtonText = SubmitButtonText.Create,
                        SubmitButtonID = SubmitButtonID.btnSubmit,
                        CancelButtonID = CancelButtonID.btnCancel,
                        CancelButtonText = CancelButtonText.Cancel,
                        IdHRCompany = idHRCompany,
                        DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId, idHRCompany),
                        DDLRoleType = await GetIntStringPairModel(PairModelTitle.RoleType),
                        DataModel = new HREmployeeRoleModel { Id = 0, IdHRCompany = idHRCompany }
                    });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadHREmployeeRoleAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _hREmployeeRoleServices.GetModelByIdAsync(id);
                return PartialView(new HREmployeeRoleViewModel
                {
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HREmployeeRole",
                    BreadCrumbBaseURL = "CompanyManagement/HREmployeeRole",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_ReadHREmployeeRoleAsync",
                    FormModelName = "HREmployeeRole",
                    ModalTitle = "कर्मचारी भूमिका विवरण हेर्नुहोस्",
                    BreadCrumbTitle = "कर्मचारी भूमिकाe",
                    CRUDAction = CRUDType.READ,
                    OnlyCancelButton = true,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = model,
                    IdHRCompany = model.IdHRCompany,
                    DDLRoleType = await GetIntStringPairModel(PairModelTitle.RoleType, model.IdHRCompany),
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
        public async Task<ActionResult> _UpdateHREmployeeRoleAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _hREmployeeRoleServices.GetModelByIdAsync(id);
                return PartialView(new HREmployeeRoleViewModel
                {
                    SessionDetails = SessionDetail,
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HREmployeeRole",
                    BreadCrumbBaseURL = "CompanyManagement/HREmployeeRole",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_UpdateHREmployeeRoleAsync",
                    FormModelName = "HREmployeeRole",
                    ModalTitle = "कर्मचारी भूमिका सम्पादन गर्नुहोस्",
                    BreadCrumbTitle = "Employee Role",
                    CRUDAction = CRUDType.UPDATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Update,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = model,
                    IdHRCompany = model.IdHRCompany,
                    DDLRoleType = await GetIntStringPairModel(PairModelTitle.RoleType, model.IdHRCompany),
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
        public async Task<ActionResult> _DeleteHREmployeeRoleAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _hREmployeeRoleServices.GetModelByIdAsync(id);
                return PartialView(new HREmployeeRoleViewModel
                {
                    SessionDetails = SessionDetail,
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HREmployeeRole",
                    BreadCrumbBaseURL = "CompanyManagement/HREmployeeRole",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    FormModelName = "HREmployeeRole",
                    ModalTitle = "कर्मचारी भूमिका मेटाउन निश्चित हुनुहुन्छ?",
                    BreadCrumbTitle = "Employee Role",
                    BreadCrumbActionName = "_DeleteHREmployeeRoleAsync",
                    CRUDAction = CRUDType.DELETE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Delete,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = model,
                    IdHRCompany = model.IdHRCompany,
                    DDLRoleType = await GetIntStringPairModel(PairModelTitle.RoleType, model.IdHRCompany),
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
        public async Task<ActionResult> _ExportHREmployeeRoleAsync(long? idHRCompany)
        {
            try
            {
                if (idHRCompany == 0 || idHRCompany == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var model = await _hREmployeeRoleServices.FindAllAsync(Condition(idHRCompany));
                var data = model.Select(x => new {
                    Company= x.HRCompany?.HRCompanyCode?.HRCompanyCodeTitle,
                    Role= x.HRRoleTitle,
                    Title=x.HRRoleTitle,
                    TotalEmployee=x.MembershipProviders.Count,

                });
                return await ExportToExcel("HREmployeeRole", data);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintHREmployeeRoleAsync(long? idHRCompany)
        {
            try
            {
                if (idHRCompany == 0 || idHRCompany == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                return PartialView(new HREmployeeRoleViewModel
                {
                    SessionDetails = SessionDetail,
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HREmployeeRole",
                    BreadCrumbBaseURL = "CompanyManagement/HREmployeeRole",
                    BreadCrumbActionName = "_PrintHREmployeeRoleAsync",
                    FormModelName = "HREmployeeRole",
                    ModalTitle = "कर्मचारी भूमिका सुची छाप्नुहोस्",
                    BreadCrumbTitle = "Employee Role",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBModelList = await _hREmployeeRoleServices.FindAllAsync(Condition(idHRCompany))
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
        public async Task<ActionResult> _CreateHREmployeeRoleAsync(HREmployeeRoleViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeRole(viewModel, CRUDType.CREATE, formCollection);
        }

        [AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _UpdateHREmployeeRoleAsync(HREmployeeRoleViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeRole(viewModel, CRUDType.UPDATE, formCollection);
        }

        [AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _DeleteHREmployeeRoleAsync(HREmployeeRoleViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeRole(viewModel, CRUDType.DELETE, formCollection);
        }

        [NonAction]
        protected async Task<ActionResult> RETSourceHREmployeeRole(HREmployeeRoleViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
                viewModel.SessionDetails = SessionDetail;
                viewModel.BreadCrumbArea = "CompanyManagement";
                viewModel.BreadCrumbController = "HREmployeeRole";
                viewModel.BreadCrumbBaseURL = "CompanyManagement/HREmployeeRole";
                viewModel.DDLRoleType = await GetIntStringPairModel(PairModelTitle.RoleType, viewModel.DataModel.IdHRCompany);
                viewModel.DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId, viewModel.DataModel.IdHRCompany);
                viewModel.IdHRCompany = viewModel.DataModel.IdHRCompany;
                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        viewModel.SubmitButtonText = SubmitButtonText.Create;
                        viewModel.BreadCrumbActionName = "_CreateHREmployeeRoleAsync";
                        viewModel.ModalTitle = "नयाँ कर्मचारी भूमिका थप्नुहोस्";
                        viewModel.FormModelName = "HREmployeeRole";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.UPDATE:
                        viewModel.BreadCrumbActionName = "_UpdateHREmployeeRoleAsync";
                        viewModel.ModalTitle = "कर्मचारी भूमिका सम्पादन गर्नुहोस्";
                        viewModel.FormModelName = "HREmployeeRole";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.DELETE:
                        viewModel.BreadCrumbActionName = "_DeleteHREmployeeRoleAsync";
                        viewModel.ModalTitle = "कर्मचारी भूमिका मेटाउन निश्चित हुनुहुन्छ?";
                        viewModel.FormModelName = "HREmployeeRole";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.PRINT:
                        viewModel.BreadCrumbActionName = "_PrintHREmployeeRoleAsync";
                        viewModel.ModalTitle = "कर्मचारी भूमिका सुची छाप्नुहोस्";
                        viewModel.FormModelName = "HREmployeeRole";
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
        protected async Task<ActionResult> CRUDHREmployeeRole(HREmployeeRoleViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceHREmployeeRole(viewModel, cRUDType);
                }
               int RoleMasterID = viewModel.DataModel.IdRoleType;
               var data= await _hREmployeeRoleServices.CommitSaveChangesAsync(viewModel.DataModel, cRUDType);
                if (CRUDType.CREATE == cRUDType  && (RoleMasterID==1 || RoleMasterID==3 || RoleMasterID==4))
                {
                     _hREmployeeRoleServices.UpdateAutomatically(data,RoleMasterID);
                }
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceHREmployeeRole(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListHREmployeeRoleAsync", new { idHRCompany = viewModel.DataModel.IdHRCompany, pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }

        #endregion
    }
}