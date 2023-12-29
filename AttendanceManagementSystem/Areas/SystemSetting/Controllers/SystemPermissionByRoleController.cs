using AttendanceManagementSystem.App_Auth;
using AttendanceManagementSystem.Controllers;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using SystemDatabase;
using SystemServices.SystemSetting;
using SystemViewModels.SystemSetting;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;

namespace AttendanceManagementSystem.Areas.SystemSetting.Controllers
{
	public class SystemPermissionByRoleController : BaseController
    {
        // GET: SystemSetting/SystemPermissionByRole

        private readonly SystemPermissionByRoleServices _SystemPermissionByRoleServices;
        private readonly HREmployeeRoleServices _HREmployeeRoleServices;

        public SystemPermissionByRoleController()
        {
            _HREmployeeRoleServices = new HREmployeeRoleServices(this._unitOfWork);
            _SystemPermissionByRoleServices = new SystemPermissionByRoleServices(this._unitOfWork);
        }


        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> Index(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return View(new SystemPermissionByRoleViewModelList
                {
                    DBModelList = await _HREmployeeRoleServices.GetsByCompanyAndSearchKey(SessionDetail.IdHRCompany, pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, ""),
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "SystemPermissionByRole",
                    BreadCrumbBaseURL = "SystemSetting/SystemPermissionByRole",
                    BreadCrumbTitle = "भूमिकाको लागि अनुमति असाइन",
                    BreadCrumbActionName = "Index",
                    SessionDetails = SessionDetail,
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    IdHRCompany = 0,
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.HRCompany, SessionDetail.IdHRCompany),
                    DDLRole = await GetIntStringPairModel(PairModelTitle.Role, SessionDetail.IdHRCompany)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser(ActionName ="Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListSystemPermissionByRoleAsync(int? idHRCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new SystemPermissionByRoleViewModelList
                {
                    IdHRCompany=idHRCompany,
                    DBModelList = await _HREmployeeRoleServices.GetsByCompanyAndSearchKey(idHRCompany, pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection,searchKey),
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "SystemPermissionByRole",
                    BreadCrumbBaseURL = "SystemSetting/SystemPermissionByRole",
                    BreadCrumbActionName = "_ListSystemPermissionByRoleAsync",
                    BreadCrumbTitle = "Assign Permission To Role",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey=searchKey,
                    SessionDetails = SessionDetail,
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _CreateSystemPermissionByRoleAsync(long? idHRCompany, int? idRole)
        {
            try
            {
                return PartialView(new SystemPermissionByRoleViewModel
                {
                    IdRole = idRole,
                    IdHRCompany = idHRCompany,
                    DataModelSystemStructure = await _SystemPermissionByRoleServices.RoleMenuSetupList<proc_RoleMenuSetupList_Result>(idRole,SessionDetail.IdHREmployee),
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "SystemPermissionByRole",
                    BreadCrumbBaseURL = "SystemSetting/SystemPermissionByRole",
                    BreadCrumbActionName = "_CreateSystemPermissionByRoleAsync",
                    FormModelName = "SystemPermissionByRole",
                    ModalTitle = "भूमिकाको लागि अनुमति असाइन गर्नुहोस्",
                    BreadCrumbTitle = "Assign Permission To Role",
                    CRUDAction = CRUDType.CREATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Submit,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLRole = await GetIntStringPairModel(PairModelTitle.Role),
                    DDLSystemStructure = await GetIntStringPairModel(PairModelTitle.SystemStructure)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadSystemPermissionByRoleAsync(int? idRole, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (idRole == null || idRole == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new SystemPermissionByRoleViewModel
                {
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "SystemPermissionByRole",
                    BreadCrumbBaseURL = "SystemSetting/SystemPermissionByRole",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_ReadSystemPermissionByRoleAsync",
                    FormModelName = "SystemPermissionByRole",
                    ModalTitle = "भूमिकाको लागि अनुमति असाइन गरेको विवरण हेर्नुहोस्",
                    BreadCrumbTitle = "Assign Permission To Role",
                    CRUDAction = CRUDType.READ,
                    OnlyCancelButton = true,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModelSystemStructure = await _SystemPermissionByRoleServices.RoleMenuSetupList<proc_RoleMenuSetupList_Result>(idRole,SessionDetail.IdHREmployee),
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
        public async Task<ActionResult> _CreateSystemPermissionByRoleAsync(SystemPermissionByRoleViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDSystemPermissionByRole(viewModel, CRUDType.CREATE, formCollection);
        }

        [NonAction]
        protected async Task<ActionResult> RETSourceSystemPermissionByRole(SystemPermissionByRoleViewModel viewModel, CRUDType CRUDType)
        {
            try
            {
                viewModel.BreadCrumbArea = "SystemSetting";
                viewModel.BreadCrumbController = "SystemPermissionByRole";
                viewModel.BreadCrumbBaseURL = "SystemSetting/SystemPermissionByRole";
                viewModel.DataModelSystemStructure = await _SystemPermissionByRoleServices.RoleMenuSetupList<proc_RoleMenuSetupList_Result>(viewModel.IdRole,SessionDetail.IdHREmployee);
                switch (CRUDType)
                {
                    case CRUDType.CREATE:
                        viewModel.SubmitButtonText = SubmitButtonText.Submit;
                        viewModel.BreadCrumbActionName = "_CreateSystemPermissionByRoleAsync";
                        viewModel.ModalTitle = "भूमिकाको लागि अनुमति असाइन गर्नुहोस्";
                        viewModel.FormModelName = "SystemPermissionByRole";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.UPDATE:
                        viewModel.BreadCrumbActionName = "_UpdateSystemPermissionByRoleAsync";
                        viewModel.ModalTitle = "भूमिकाको लागि अनुमति असाइन ";
                        viewModel.FormModelName = "SystemPermissionByRole";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.DELETE:
                        viewModel.BreadCrumbActionName = "_DeleteSystemPermissionByRoleAsync";
                        viewModel.ModalTitle = "Delete Existing Permission";
                        viewModel.FormModelName = "SystemPermissionByRole";
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
        protected async Task<ActionResult> CRUDSystemPermissionByRole(SystemPermissionByRoleViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceSystemPermissionByRole(viewModel, cRUDType);
                }
                var CreateList = viewModel.DataModel.Where(x => x.Assigned && x.Id == 0);
                var UpdateList = viewModel.DataModel.Where(x => x.Assigned && x.Id > 0);
                var DeleteList = viewModel.DataModel.Where(x => !x.Assigned && x.Id > 0);
                await _SystemPermissionByRoleServices.CommitBulkSaveChangesAsync(CreateList, CRUDType.CREATE);
                await _SystemPermissionByRoleServices.CommitBulkSaveChangesAsync(UpdateList, CRUDType.UPDATE);
                await _SystemPermissionByRoleServices.CommitBulkSaveChangesAsync(DeleteList, CRUDType.DELETE);
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceSystemPermissionByRole(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListSystemPermissionByRoleAsync", new { idHRCompany = viewModel.IdHRCompany, pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }
    }
}