using AttendanceManagementSystem.App_Auth;
using AttendanceManagementSystem.Controllers;
using System;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using SystemDatabase;
using SystemModels.EmployeeManagement;
using SystemServices.EmployeeManagement;
using SystemViewModels.EmployeeManagement;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;

namespace AttendanceManagementSystem.Areas.EmployeeManagement.Controllers
{
    public class HREmployeeContactController : BaseController
    {
        // GET: EmployeeManagement/HREmployeeContact
        private readonly HREmployeeContactServices _hrEmployeeContactServices;

        public HREmployeeContactController()
        {
            this._hrEmployeeContactServices = new HREmployeeContactServices(this._unitOfWork);
        }


		#region HREmployee Contact

		[NonAction]
		public Expression<Func<HREmployeeContact, bool>> Condition(long? idHREmployee, string searchKey = "")
		{
			Expression<Func<HREmployeeContact, bool>> retutndata = (x => false);
			if (this.SessionDetail.IdRoleType == 0) retutndata = (x => x.IdHREmployee == idHREmployee && (x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
			else if (this.SessionDetail.IdRoleType == 1 || this.SessionDetail.IdRoleType == 2 || this.SessionDetail.IdRoleType == 4) retutndata = (x => x.IdHREmployee == idHREmployee && x.HREmployee.IdHRCompany == SessionDetail.IdHRCompany && (x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
			else retutndata = (x => x.IdHREmployee == this.SessionDetail.IdHREmployee && (x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
			return retutndata;
		}

		[AuthorizeUser(ActionName = "Index")]
		[AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListWrapperHREmployeeContactAsync(long? idHREmployee, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                if (idHREmployee == null || idHREmployee == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HREmployeeContactViewModelList
                {
                    IdHREmployee = idHREmployee,
					SessionDetails= SessionDetail,
                    DBModelList = await _hrEmployeeContactServices.GetPagedList(Condition(idHREmployee), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeContact",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeContact",
                    BreadCrumbActionName = "_ListWrapperHREmployeeContactAsync",
                    BreadCrumbTitle = "Employee Contact",
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
        public async Task<ActionResult> _ListHREmployeeContactAsync(long? idHREmployee, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                if (idHREmployee == null || idHREmployee == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HREmployeeContactViewModelList
                {
                    IdHREmployee = idHREmployee,
					SessionDetails=SessionDetail,
					DBModelList = await _hrEmployeeContactServices.GetPagedList(Condition(idHREmployee,searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeContact",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeContact",
                    BreadCrumbActionName = "_ListHREmployeeContactAsync",
                    BreadCrumbTitle = "Employee Contact",
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
        public async Task<ActionResult> _CreateHREmployeeContactAsync(long? idHREmployee)
        {
            try
            {
                if (idHREmployee == null || idHREmployee == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                return PartialView(new HREmployeeContactViewModel
                {
					SessionDetails=SessionDetail,
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeContact",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeContact",
                    BreadCrumbActionName = "_CreateHREmployeeContactAsync",
                    FormModelName = "HREmployeeContact",
                    ModalTitle = "नयाँ कर्मचारी सम्पर्क थप्नुहोस्  ",
                    BreadCrumbTitle = "Employee Contact",
                    CRUDAction = CRUDType.CREATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Create,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    IdHREmployee=idHREmployee,
                    DataModel = new HREmployeeContactModel { Id = 0, IdHREmployee = idHREmployee },
                    DDLContactType = await GetLongStringPairModel(PairModelTitle.ContactType),
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

		[AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadHREmployeeContactAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null||id==0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _hrEmployeeContactServices.GetModelByIdAsync(id);
                return PartialView(new HREmployeeContactViewModel
                {
					SessionDetails=SessionDetail,
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeContact",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeContact",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_ReadHREmployeeContactAsync",
                    FormModelName = "HREmployeeContact",
                    ModalTitle = " कर्मचारी सम्पर्क विवरण हेर्नुहोस",
                    BreadCrumbTitle = "Employee Contact",
                    CRUDAction = CRUDType.READ,
                    OnlyCancelButton = true,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLContactType = await GetLongStringPairModel(PairModelTitle.ContactType),
                    DataModel = model,
                    IdHREmployee=model.IdHREmployee
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

		[AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _UpdateHREmployeeContactAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null||id==0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _hrEmployeeContactServices.GetModelByIdAsync(id);
                return PartialView(new HREmployeeContactViewModel
                {
					SessionDetails=SessionDetail,
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeContact",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeContact",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_UpdateHREmployeeContactAsync",
                    FormModelName = "HREmployeeContact",
                    ModalTitle = "कर्मचारी सम्पर्क सम्पादन गर्नुहोस्",
                    BreadCrumbTitle = "Employee Contact",
                    CRUDAction = CRUDType.UPDATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Update,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLContactType = await GetLongStringPairModel(PairModelTitle.ContactType),
                    DataModel = model,
                    IdHREmployee=model.IdHREmployee
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

		[AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _DeleteHREmployeeContactAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null||id==0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _hrEmployeeContactServices.GetModelByIdAsync(id);
                return PartialView(new HREmployeeContactViewModel
                {
					SessionDetails=SessionDetail,
					HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeContact",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeContact",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    FormModelName = "HREmployeeContact",
                    ModalTitle = "कर्मचारी सम्पर्क विवरण मेटाउनुहोस्",
                    BreadCrumbTitle = "Employee Contact",
                    BreadCrumbActionName = "_DeleteHREmployeeContactAsync",
                    CRUDAction = CRUDType.DELETE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Delete,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLContactType = await GetLongStringPairModel(PairModelTitle.ContactType),
                    DataModel = model,
                    IdHREmployee=model.IdHREmployee
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

		[AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ExportHREmployeeContactAsync(long? idHREmployee)
        {
            try
            {
                if (idHREmployee == null || idHREmployee == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var model = await _hrEmployeeContactServices.FindAllAsync(Condition(idHREmployee));
                return await ExportToExcel("EmployeeContact", model);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

		[AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintHREmployeeContactAsync(long? idHREmployee)
        {
            try
            {
                return PartialView(new HREmployeeContactViewModel
                {
					SessionDetails=SessionDetail,
					HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeContact",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeContact",
                    BreadCrumbActionName = "_PrintHREmployeeContactAsync",
                    FormModelName = "HREmployeeContact",
                    ModalTitle = "कर्मचारी सम्पर्क छाप्नुहोस्",
                    BreadCrumbTitle = "Employee Contact",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBModelList = await _hrEmployeeContactServices.FindAllAsync(Condition(idHREmployee))
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
        public async Task<ActionResult> _CreateHREmployeeContactAsync(HREmployeeContactViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeContactAsync(viewModel, CRUDType.CREATE, formCollection);
        }

		[AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _UpdateHREmployeeContactAsync(HREmployeeContactViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeContactAsync(viewModel, CRUDType.UPDATE, formCollection);
        }

		[AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _DeleteHREmployeeContactAsync(HREmployeeContactViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeContactAsync(viewModel, CRUDType.DELETE, formCollection);
        }

        [NonAction]
        protected async Task<ActionResult> RETSourceHREmployeeContactAsync(HREmployeeContactViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
				viewModel.SessionDetails = SessionDetail;
                viewModel.BreadCrumbArea = "EmployeeManagement";
                viewModel.BreadCrumbController = "HREmployeeContact";
                viewModel.BreadCrumbBaseURL = "EmployeeManagement/HREmployeeContact";
                viewModel.DDLContactType = await GetLongStringPairModel(PairModelTitle.ContactType);
                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        viewModel.SubmitButtonText = SubmitButtonText.Create;
                        viewModel.BreadCrumbActionName = "_CreateHREmployeeContactAsync";
                        viewModel.ModalTitle = "कर्मचारी सम्पर्क विवरण हेर्नुहोस";
                        viewModel.FormModelName = "HREmployeeContact";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.UPDATE:
                        viewModel.BreadCrumbActionName = "_UpdateHREmployeeContactAsync";
                        viewModel.ModalTitle = "कर्मचारी सम्पर्क सम्पादन गर्नुहोस्";
                        viewModel.FormModelName = "HREmployeeContact";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.DELETE:
                        viewModel.BreadCrumbActionName = "_DeleteHREmployeeContactAsync";
                        viewModel.ModalTitle = "कर्मचारी सम्पर्क विवरण मेटाउनुहोस्";
                        viewModel.FormModelName = "HREmployeeContact";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.PRINT:
                        viewModel.BreadCrumbActionName = "_PrintHREmployeeContactAsync";
                        viewModel.ModalTitle = "कर्मचारी सम्पर्क छाप्नुहोस्";
                        viewModel.FormModelName = "HREmployeeContact";
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
        protected async Task<ActionResult> CRUDHREmployeeContactAsync(HREmployeeContactViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceHREmployeeContactAsync(viewModel, cRUDType);
                }
                long id = await _hrEmployeeContactServices.CommitSaveChangesAsync(viewModel.DataModel, cRUDType);
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceHREmployeeContactAsync(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListHREmployeeContactAsync", new { idHREmployee = viewModel.DataModel.IdHREmployee, pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }

        #endregion
    }
}