using AttendanceManagementSystem.Controllers;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using SystemDatabase;
using SystemServices.EmployeeManagement;
using SystemServices.SystemSetting;
using SystemViewModels.SystemSetting;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;

namespace AttendanceManagementSystem.Areas.SystemSetting.Controllers
{
	public class SystemPermissionByHREmployeeController : BaseController
    {
        // GET: SystemSetting/SystemPermissionByHREmployee

        private readonly SystemPermissionByHREmployeeServices _SystemPermissionByHREmployeeServices;
        private readonly HREmployeeServices _HREmployeeServices;

        public SystemPermissionByHREmployeeController()
        {
			_HREmployeeServices = new HREmployeeServices(this._unitOfWork);
			_SystemPermissionByHREmployeeServices = new SystemPermissionByHREmployeeServices(this._unitOfWork);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> Index(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return View(new SystemPermissionByHREmployeeViewModelList
                {
                    DBModelList = await _HREmployeeServices.GetsByCompany( SessionDetail.IdHRCompany,pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection,""),
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "SystemPermissionByHREmployee",
                    BreadCrumbBaseURL = "SystemSetting/SystemPermissionByHREmployee",
                    BreadCrumbTitle = "कर्मचारीको लागि अनुमति असाइन",
                    BreadCrumbActionName = "Index",
					SessionDetails=SessionDetail,
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
					DDLCompany=await GetLongStringPairModel(PairModelTitle.HRCompanyWithChildren,SessionDetail.IdHRCompany),
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListSystemPermissionByHREmployeeAsync(long? idHRCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new SystemPermissionByHREmployeeViewModelList
                {
					IdHRCompany= idHRCompany,
					DBModelList = await _HREmployeeServices.GetsByCompany(idHRCompany, pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection,""),
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "SystemPermissionByHREmployee",
                    BreadCrumbBaseURL = "SystemSetting/SystemPermissionByHREmployee",
                    BreadCrumbActionName = "_ListSystemPermissionByHREmployeeAsync",
                    BreadCrumbTitle = "Language",
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
        public async Task<ActionResult> _CreateSystemPermissionByHREmployeeAsync(long? idHRCompany,long? idHREmployee)
        {
            try
            {
				return PartialView(new SystemPermissionByHREmployeeViewModel
                {
					IdHRCompany= idHRCompany,
					IdHREmployee = idHREmployee,
					DataModelSystemStructure= await _SystemPermissionByHREmployeeServices.EmployeeMenuSetupList<proc_EmployeeMenuSetupList_Result>(idHREmployee),
					HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSetting",
                    BreadCrumbController = "SystemPermissionByHREmployee",
                    BreadCrumbBaseURL = "SystemSetting/SystemPermissionByHREmployee",
                    BreadCrumbActionName = "_CreateSystemPermissionByHREmployeeAsync",
                    FormModelName = "SystemPermissionByHREmployee",
                    ModalTitle = "कर्मचारीको लागि अनुमति असाइन गर्नुहोस्",
                    BreadCrumbTitle = "Language",
                    CRUDAction = CRUDType.CREATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Submit,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLSystemStructure = await GetIntStringPairModel(PairModelTitle.SystemStructure)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

		[AcceptVerbs(HttpVerbs.Get)]
		public async Task<ActionResult> _ReadSystemPermissionByHREmployeeAsync(int? idHREmployee, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
		{
			try
			{
				if (idHREmployee == null)
				{
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
				}
				var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
				return PartialView(new SystemPermissionByHREmployeeViewModel
				{
					HeaderTitle = "Attendance System Management",
					BreadCrumbArea = "SystemSetting",
					BreadCrumbController = "SystemPermissionByHREmployee",
					BreadCrumbBaseURL = "SystemSetting/SystemPermissionByHREmployee",
					PageNumber = pagination.PageNumber,
					PageSize = pagination.PageSize,
					OrderingBy = pagination.OrderingBy,
					OrderingDirection = pagination.OrderingDirection,
					SearchKey = searchKey,
					BreadCrumbActionName = "_ReadSystemPermissionByHREmployeeAsync",
					FormModelName = "SystemPermissionByHREmployee",
					ModalTitle = "कर्मचारीहरुको अनुमति असाइन विवरण हेर्नुहोस् ",
					BreadCrumbTitle = "Language",
					CRUDAction = CRUDType.READ,
					OnlyCancelButton = true,
					CancelButtonID = CancelButtonID.btnCancel,
					CancelButtonText = CancelButtonText.Cancel,
					DataModelSystemStructure= await _SystemPermissionByHREmployeeServices.EmployeeMenuSetupList<proc_EmployeeMenuSetupList_Result>(idHREmployee),
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
        public async Task<ActionResult> _CreateSystemPermissionByHREmployeeAsync(SystemPermissionByHREmployeeViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDSystemPermissionByHREmployee(viewModel, CRUDType.CREATE, formCollection);
        }

		[NonAction]
        protected async Task<ActionResult> RETSourceSystemPermissionByHREmployee(SystemPermissionByHREmployeeViewModel viewModel, CRUDType CRUDType)
        {
            try
            {
                viewModel.BreadCrumbArea = "SystemSetting";
                viewModel.BreadCrumbController = "SystemPermissionByHREmployee";
                viewModel.BreadCrumbBaseURL = "SystemSetting/SystemPermissionByHREmployee";
				viewModel.DataModelSystemStructure = await _SystemPermissionByHREmployeeServices.EmployeeMenuSetupList<proc_EmployeeMenuSetupList_Result>(viewModel.IdHREmployee);
                switch (CRUDType)
                {
                    case CRUDType.CREATE:
                        viewModel.SubmitButtonText = SubmitButtonText.Submit;
                        viewModel.BreadCrumbActionName = "_CreateSystemPermissionByHREmployeeAsync";
                        viewModel.ModalTitle = "कर्मचारीको लागि अनुमति असाइन गर्नुहोस्";
                        viewModel.FormModelName = "SystemPermissionByHREmployee";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.UPDATE:
                        viewModel.BreadCrumbActionName = "_UpdateSystemPermissionByHREmployeeAsync";
                        viewModel.ModalTitle = "कर्मचारीको लागि अनुमति असाइन सम्पादन गर्नुहोस्";
                        viewModel.FormModelName = "SystemPermissionByHREmployee";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.DELETE:
                        viewModel.BreadCrumbActionName = "_DeleteSystemPermissionByHREmployeeAsync";
                        viewModel.ModalTitle = "कर्मचारीको लागि अनुमति असाइन मेटाउन निश्चित हुनुहुन्छ?";
                        viewModel.FormModelName = "SystemPermissionByHREmployee";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.PRINT:
                        viewModel.BreadCrumbActionName = "_PrintSystemPermissionByHREmployeeAsync";
                        viewModel.ModalTitle = "कर्मचारीको लागि अनुमति असाइन सुची छाप्नुहोस्";
                        viewModel.FormModelName = "SystemPermissionByHREmployee";
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
        protected async Task<ActionResult> CRUDSystemPermissionByHREmployee(SystemPermissionByHREmployeeViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceSystemPermissionByHREmployee(viewModel, cRUDType);
                }
				var CreateList = viewModel.DataModel.Where(x=>x.Assigned&&x.Id==0);
				var UpdateList = viewModel.DataModel.Where(x=>x.Assigned&&x.Id> 0);
				var DeleteList = viewModel.DataModel.Where(x=>!x.Assigned&&x.Id > 0);
				await _SystemPermissionByHREmployeeServices.CommitBulkSaveChangesAsync(CreateList, CRUDType.CREATE);
				await _SystemPermissionByHREmployeeServices.CommitBulkSaveChangesAsync(UpdateList, CRUDType.UPDATE);
				await _SystemPermissionByHREmployeeServices.CommitBulkSaveChangesAsync(DeleteList, CRUDType.DELETE);
			}
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceSystemPermissionByHREmployee(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListSystemPermissionByHREmployeeAsync", new { idHRCompany= viewModel.IdHRCompany, pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }
    }
}