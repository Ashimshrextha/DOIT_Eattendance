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
    public class HREmployeeServiceTypeController : BaseController
    {
        // GET: CompanyManagement/HREmployeeServiceType
        private readonly HREmployeeServiceTypeServices _HREmployeeServiceTypeServices;
        public HREmployeeServiceTypeController()
        {
            this._HREmployeeServiceTypeServices = new HREmployeeServiceTypeServices(this._unitOfWork);
        }

		#region ServiceType

		[NonAction]
		public Expression<Func<HREmployeeServiceType, bool>> Condition(long? idHRCompany, string searchKey = "")
		{
			Expression<Func<HREmployeeServiceType, bool>> retutndata = (x => false);

			if (this.SessionDetail.IdRoleType == 0) retutndata = (x => x.IdHRCompany == idHRCompany && (x.HREmployeeServiceTypeTitle.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));

			else if (this.SessionDetail.IdRoleType == 1 || SessionDetail.IdRoleType == 2 || SessionDetail.IdRoleType == 4) retutndata = (x => x.IdHRCompany ==idHRCompany && (x.HREmployeeServiceTypeTitle.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));

			else retutndata = (x => false);

			return retutndata;
		}

		[AuthorizeUser(ActionName = "Index")]
		[AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListWrapperHREmployeeServiceTypeAsync(long? idHRCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                if (idHRCompany == null||idHRCompany==0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HREmployeeServiceTypeViewModelList
                {
                    IdHRCompany = idHRCompany,
					SessionDetails= SessionDetail,
					DBModelList = await _HREmployeeServiceTypeServices.GetPagedList(Condition(idHRCompany,searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HREmployeeServiceType",
                    BreadCrumbBaseURL = "CompanyManagement/HREmployeeServiceType",
                    BreadCrumbActionName = "_ListWrapperHREmployeeServiceTypeeAsync",
                    BreadCrumbTitle = "Employee Service Type",
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

		[AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListHREmployeeServiceTypeAsync(long? idHRCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                if (idHRCompany == null || idHRCompany == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HREmployeeServiceTypeViewModelList
                {
                    IdHREmployee = idHRCompany,
					SessionDetails= SessionDetail,
					DBModelList = await _HREmployeeServiceTypeServices.GetPagedList(Condition(idHRCompany, searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HREmployeeServiceType",
                    BreadCrumbBaseURL = "CompanyManagement/HREmployeeServiceType",
                    BreadCrumbActionName = "_ListHREmployeeServiceTypeAsync",
                    BreadCrumbTitle = "Employee Service Type",
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


        [AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadListHREmployeeServiceTypeAsync(long? idHRCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                if (idHRCompany == null || idHRCompany == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HREmployeeServiceTypeViewModelList
                {
                    IdHREmployee = idHRCompany,
                    SessionDetails = SessionDetail,
                    DBModelList = await _HREmployeeServiceTypeServices.GetPagedList(Condition(idHRCompany, searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HREmployeeServiceType",
                    BreadCrumbBaseURL = "CompanyManagement/HREmployeeServiceType",
                    BreadCrumbActionName = "_ReadListHREmployeeServiceTypeAsync",
                    BreadCrumbTitle = "Employee Service Type",
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
        public async Task<ActionResult> _CreateHREmployeeServiceTypeAsync(long? idHRCompany)
        {
            try
            {
                if (idHRCompany == null || idHRCompany == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var model = await _HREmployeeServiceTypeServices.GetByIdAsync(idHRCompany);
                return PartialView(new HREmployeeServiceTypeViewModel
                {
					SessionDetails= SessionDetail,
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HREmployeeServiceType",
                    BreadCrumbBaseURL = "CompanyManagement/HREmployeeServiceType",
                    BreadCrumbActionName = "_CreateHREmployeeServiceTypeAsync",
                    FormModelName = "HREmployeeServiceType",
                    ModalTitle = "नयाँ सेवा विवरण थप्नुहोस्",
                    CRUDAction = CRUDType.CREATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Create,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLDivision = await GetLongStringPairModel(PairModelTitle.Division),
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId, idHRCompany),
                    DDLDesignation = await GetLongStringPairModel(PairModelTitle.Designation),
                    DDLServiceType = await GetLongStringPairModel(PairModelTitle.ServiceType),
                    DDLJobStatus = await GetIntStringPairModel(PairModelTitle.JobStatus),
                    DataModel = new HREmployeeServiceTypeModel { Id = 0 }
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

		[AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadHREmployeeServiceTypeAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null||id==0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _HREmployeeServiceTypeServices.GetModelByIdAsync(id);
                return PartialView(new HREmployeeServiceTypeViewModel
                {
					SessionDetails= SessionDetail,
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HREmployeeServiceType",
                    BreadCrumbBaseURL = "CompanyManagement/HREmployeeServiceType",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_ReadServiceTypeAsync",
                    FormModelName = "HREmployeeServiceType",
                    ModalTitle = "सेवा विवरण",
                    CRUDAction = CRUDType.READ,
                    OnlyCancelButton = true,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = model,
                    DDLDivision = await GetLongStringPairModel(PairModelTitle.Division, model.IdHRCompany),
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId, model.IdHRCompany),
                    DDLDesignation = await GetLongStringPairModel(PairModelTitle.Designation, model.IdHRCompany),
                    DDLServiceType = await GetLongStringPairModel(PairModelTitle.ServiceType, model.IdHRCompany),
                    DDLJobStatus = await GetIntStringPairModel(PairModelTitle.JobStatus)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

		[AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _UpdateHREmployeeServiceTypeAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null||id==0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _HREmployeeServiceTypeServices.GetModelByIdAsync(id);
                return PartialView(new HREmployeeServiceTypeViewModel
                {
					SessionDetails= SessionDetail,
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HREmployeeServiceType",
                    BreadCrumbBaseURL = "CompanyManagement/HREmployeeServiceType",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_UpdateHREmployeeServiceTypeAsync",
                    FormModelName = "HREmployeeServiceType",
                    ModalTitle = "सेवा विवरण सम्पादन गर्नुहोस्",
                    CRUDAction = CRUDType.UPDATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Update,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = model,
                    DDLDivision = await GetLongStringPairModel(PairModelTitle.Division, model.IdHRCompany),
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId, model.IdHRCompany),
                    DDLDesignation = await GetLongStringPairModel(PairModelTitle.Designation, model.IdHRCompany),
                    DDLServiceType = await GetLongStringPairModel(PairModelTitle.ServiceType, model.IdHRCompany),
                    DDLJobStatus = await GetIntStringPairModel(PairModelTitle.JobStatus)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

		[AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _DeleteHREmployeeServiceTypeAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null||id==0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _HREmployeeServiceTypeServices.GetModelByIdAsync(id);
                return PartialView(new HREmployeeServiceTypeViewModel
                {
					SessionDetails= SessionDetail,
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HREmployeeServiceType",
                    BreadCrumbBaseURL = "CompanyManagement/HREmployeeServiceType",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    FormModelName = "HREmployeeServiceType",
                    ModalTitle = "के तपाईं यसलाई मेटाउन निश्चित हुनुहुन्छ?",
                    BreadCrumbActionName = "_DeleteHREmployeeServiceTypeAsync",
                    CRUDAction = CRUDType.DELETE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Delete,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = model,
                    DDLDivision = await GetLongStringPairModel(PairModelTitle.Division, model.IdHRCompany),
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId, model.IdHRCompany),
                    DDLDesignation = await GetLongStringPairModel(PairModelTitle.Designation, model.IdHRCompany),
                    DDLServiceType = await GetLongStringPairModel(PairModelTitle.ServiceType, model.IdHRCompany),
                    DDLJobStatus = await GetIntStringPairModel(PairModelTitle.JobStatus)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

		[AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ExportHREmployeeServiceTypeAsync(long? idHRCompany)
        {
            try
            {
                var model = await _HREmployeeServiceTypeServices.FindAllAsync(Condition(idHRCompany));
                var data = model.Select(x => new
                {
                    Company=x.HRCompany?.HRCompanyCode?.HRCompanyCodeTitle,
                    ServiceType=x.HREmployeeServiceTypeTitle,
                    ServiceTypeInShort=x.HREmployeeServiceTypeShortName,
                    ServiceOrder=x.HREmployeeServiceTypeOrder,


                });
                return await ExportToExcel("EmployeeServiceType", data);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

		[AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintHREmployeeServiceTypeAsync(long? idHRCompany)
        {
            try
            {
                if (idHRCompany == null || idHRCompany == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                return PartialView(new HREmployeeServiceTypeViewModel
                {
					SessionDetails= SessionDetail,
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HREmployeeServiceType",
                    BreadCrumbBaseURL = "CompanyManagement/HREmployeeServiceType",
                    BreadCrumbActionName = "_PrintServiceTypeAsync",
                    FormModelName = "HREmployeeServiceType",
                    ModalTitle = "सेवा विवरण सुची छाप्नुहोस्",
                    BreadCrumbTitle = "Employee ServiceType",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBModelList = await _HREmployeeServiceTypeServices.FindAllAsync(Condition(idHRCompany))
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
        public async Task<ActionResult> _CreateHREmployeeServiceTypeAsync(HREmployeeServiceTypeViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeServiceTypeAsync(viewModel, CRUDType.CREATE, formCollection);
        }

		[AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _UpdateHREmployeeServiceTypeAsync(HREmployeeServiceTypeViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeServiceTypeAsync(viewModel, CRUDType.UPDATE, formCollection);
        }

		[AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _DeleteHREmployeeServiceTypeAsync(HREmployeeServiceTypeViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeServiceTypeAsync(viewModel, CRUDType.DELETE, formCollection);
        }

        [NonAction]
        protected async Task<ActionResult> RETSourceHREmployeeServiceTypeAsync(HREmployeeServiceTypeViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
				viewModel.SessionDetails = SessionDetail;
                viewModel.BreadCrumbArea = "CompanyManagement";
                viewModel.BreadCrumbController = "HREmployeeServiceType";
                viewModel.BreadCrumbBaseURL = "CompanyManagement/HREmployeeServiceType";
                viewModel.DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId, viewModel.DataModel.IdHRCompany);
                viewModel.DDLDesignation = await GetLongStringPairModel(PairModelTitle.Designation, viewModel.DataModel.IdHRCompany);
                viewModel.DDLDivision = await GetLongStringPairModel(PairModelTitle.Division, viewModel.DataModel.IdHRCompany);
                viewModel.DDLServiceType = await GetLongStringPairModel(PairModelTitle.ServiceType, viewModel.DataModel.IdHRCompany);
                viewModel.DDLJobStatus = await GetIntStringPairModel(PairModelTitle.JobStatus);
                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        viewModel.SubmitButtonText = SubmitButtonText.Create;
                        viewModel.BreadCrumbActionName = "_CreateHREmployeeServiceTypeAsync";
                        viewModel.ModalTitle = "नयाँ सेवा विवरण थप्नुहोस्";
                        viewModel.FormModelName = "ServiceType";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.UPDATE:
                        viewModel.BreadCrumbActionName = "_UpdateHREmployeeServiceTypeAsync";
                        viewModel.ModalTitle = "सेवा विवरण सम्पादन गर्नुहोस्";
                        viewModel.FormModelName = "ServiceType";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.DELETE:
                        viewModel.BreadCrumbActionName = "_DeleteHREmployeeServiceTypeAsync";
                        viewModel.ModalTitle = "के तपाईं यसलाई मेटाउन निश्चित हुनुहुन्छ?";
                        viewModel.FormModelName = "ServiceType";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.PRINT:
                        viewModel.BreadCrumbActionName = "_PrintHREmployeeServiceTypeAsync";
                        viewModel.ModalTitle = "सेवा विवरण सुची छाप्नुहोस्";
                        viewModel.FormModelName = "ServiceType";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                }
                return null;
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("", exp.Message, AlertNotificationType.error);
            }
        }

        [NonAction]
        protected async Task<ActionResult> CRUDHREmployeeServiceTypeAsync(HREmployeeServiceTypeViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceHREmployeeServiceTypeAsync(viewModel, cRUDType);
                }
                int jobCounter = await _HREmployeeServiceTypeServices.CountAsync(x => x.IdHRCompany == viewModel.DataModel.IdHRCompany);
                long id = await _HREmployeeServiceTypeServices.CommitSaveChangesAsync(viewModel.DataModel, cRUDType);
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceHREmployeeServiceTypeAsync(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListHREmployeeServiceTypeAsync", new { idHRCompany = viewModel.DataModel.IdHRCompany, pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }

        #endregion
    }
}