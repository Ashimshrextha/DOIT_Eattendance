using AttendanceManagementSystem.App_Auth;
using AttendanceManagementSystem.Controllers;
using System;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using SystemDatabase;
using SystemModels.EmployeeManagement;
using SystemModels.SystemAuthentication;
using SystemServices.EmployeeManagement;
using SystemServices.SystemAuthentication;
using SystemViewModels.EmployeeManagement;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;

namespace AttendanceManagementSystem.Areas.EmployeeManagement.Controllers
{
	public class HREmployeeFamilyDetailsController : BaseController
	{
		// GET: EmployeeManagement/HREmployeeManagement
		private readonly HREmployeeServices _hREmployeeservices;

		public HREmployeeFamilyDetailsController()
		{
			this._hREmployeeservices = new HREmployeeServices(this._unitOfWork);
		}
		[NonAction]
		public Expression<Func<HREmployee, bool>> Condition(long? idHREmployee, string searchKey = "")
		{
			Expression<Func<HREmployee, bool>> retutndata = (x => false);
			if (this.SessionDetail.IdRoleType == 0) retutndata = (x => x.IdParent_HREmployee == idHREmployee && (x.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
			else if (this.SessionDetail.IdRoleType == 1 || this.SessionDetail.IdRoleType == 2) retutndata = (x => x.IdParent_HREmployee == idHREmployee && x.IdHRCompany == SessionDetail.IdHRCompany && (x.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
			else retutndata = (x => x.IdParent_HREmployee == this.SessionDetail.IdHREmployee && (x.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
			return retutndata;
		}
		#region HREmployee Family Detail

		[AuthorizeUser(ActionName = "Index")]
		[AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListWrapperHREmployeeFamilyDetailsAsync(long? idEmployee, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
				return PartialView(new HREmployeeFamilyDetailsViewModelList
				{
					SessionDetails = SessionDetail,
                    IdHREmployee = idEmployee,
                    DBModelList = await _hREmployeeservices.GetPagedList(Condition(idEmployee,searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeFamilyDetails",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeFamilyDetails",
                    BreadCrumbActionName = "_ListWrapperHREmployeeFamilyDetailsAsync",
                    BreadCrumbTitle = "Employee Job History",
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
        public async Task<ActionResult> _ListHREmployeeFamilyDetailsAsync(long? idEmployee, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HREmployeeFamilyDetailsViewModelList
                {
                    IdHREmployee = idEmployee,
                    DBModelList = await _hREmployeeservices.GetPagedList(Condition(idEmployee, searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
					SessionDetails = SessionDetail,
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeFamilyDetails",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeFamilyDetails",
                    BreadCrumbActionName = "_ListHREmployeeFamilyDetailsAsync",
                    BreadCrumbTitle = "Employee",
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
        public async Task<ActionResult> _CreateHREmployeeFamilyDetailsAsync(long? idEmployee)
        {
            try
            {
                var model = await _hREmployeeservices.GetByIdAsync(idEmployee);
                return PartialView(new HREmployeeFamilyDetailsViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeFamilyDetails",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeFamilyDetails",
                    BreadCrumbActionName = "_CreateHREmployeeFamilyDetailsAsync",
                    FormModelName = "HREmployeeFamilyDetails",
                    ModalTitle = "नयाँ परिवारको सदस्य थप्नुहोस्",
                    SessionDetails = SessionDetail,
                    BreadCrumbTitle = "Employee Job",
                    CRUDAction = CRUDType.CREATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Create,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLBloodGroup = await GetIntStringPairModel(PairModelTitle.BloodGroup),
                    DDLMaritalStatus = await GetLongStringPairModel(PairModelTitle.MaritalStatus),
                    DDLGender = await GetLongStringPairModel(PairModelTitle.Gender),
                    DDLLanguage = await GetLongStringPairModel(PairModelTitle.HRLanguage),
                    DDLRelation = await GetIntStringPairModel(PairModelTitle.Relation),
                    DataModel = new HREmployeeModel
                    {
                        Id = 0,
                        IdParent_HREmployee = idEmployee,
                        DOBNP = Today.Result.Nepdate
                    }
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

		[AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadHREmployeeFamilyDetailsAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _hREmployeeservices.GetModelByIdAsync(id);
                return PartialView(new HREmployeeFamilyDetailsViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeFamilyDetails",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeFamilyDetails",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
					SessionDetails = SessionDetail,
					OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_ReadHREmployeeFamilyDetailsAsync",
                    FormModelName = "HREmployeeFamilyDetails",
                    ModalTitle = "परिवारको सदस्यको विवरण हेर्नुहोस",
                    BreadCrumbTitle = "Employee",
                    CRUDAction = CRUDType.READ,
                    OnlyCancelButton = true,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = model,
                    DDLBloodGroup = await GetIntStringPairModel(PairModelTitle.BloodGroup),
                    DDLMaritalStatus = await GetLongStringPairModel(PairModelTitle.MaritalStatus),
                    DDLGender = await GetLongStringPairModel(PairModelTitle.Gender),
                    DDLLanguage = await GetLongStringPairModel(PairModelTitle.HRLanguage),
                    DDLRelation = await GetIntStringPairModel(PairModelTitle.Relation),
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

		[AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _UpdateHREmployeeFamilyDetailsAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _hREmployeeservices.GetModelByIdAsync(id);
                model.DOBNP = Date(model.DOB ?? DateTime.Now).Nepdate;
                return PartialView(new HREmployeeFamilyDetailsViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeFamilyDetails",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeFamilyDetails",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
					SessionDetails = SessionDetail,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_UpdateHREmployeeFamilyDetailsAsync",
                    FormModelName = "HREmployeeFamilyDetails",
                    ModalTitle = "परिवारको सदस्यको विवरण सम्पादन गर्नुहोस",
                    BreadCrumbTitle = "Employee",
                    CRUDAction = CRUDType.UPDATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Update,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = model,
                    DDLBloodGroup = await GetIntStringPairModel(PairModelTitle.BloodGroup),
                    DDLMaritalStatus = await GetLongStringPairModel(PairModelTitle.MaritalStatus),
                    DDLGender = await GetLongStringPairModel(PairModelTitle.Gender),
                    DDLLanguage = await GetLongStringPairModel(PairModelTitle.HRLanguage),
                    DDLRelation = await GetIntStringPairModel(PairModelTitle.Relation),
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

		[AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _DeleteHREmployeeFamilyDetailsAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _hREmployeeservices.GetModelByIdAsync(id);
                return PartialView(new HREmployeeFamilyDetailsViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeFamilyDetails",
					SessionDetails = SessionDetail,
					BreadCrumbBaseURL = "EmployeeManagement/HREmployeeFamilyDetails",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    FormModelName = "HREmployeeFamilyDetails",
                    ModalTitle = "परिवारको सदस्यको विवरण मेटाउनु्होस",
                    BreadCrumbTitle = "Employee",
                    BreadCrumbActionName = "_DeleteHREmployeeFamilyDetailsAsync",
                    CRUDAction = CRUDType.DELETE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Delete,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = model,
                    DDLBloodGroup = await GetIntStringPairModel(PairModelTitle.BloodGroup),
                    DDLMaritalStatus = await GetLongStringPairModel(PairModelTitle.MaritalStatus),
                    DDLGender = await GetLongStringPairModel(PairModelTitle.Gender),
                    DDLLanguage = await GetLongStringPairModel(PairModelTitle.HRLanguage),
                    DDLRelation = await GetIntStringPairModel(PairModelTitle.Relation),
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

		[AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ExportHREmployeeFamilyDetailsAsync(long? idEmployee)
        {
            try
            {
                var model = await _hREmployeeservices.FindAllAsync(Condition(idEmployee));
                return await ExportToExcel("EmployeeJobHistroy", model);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

		[AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintHREmployeeFamilyDetailsAsync(long? idEmployee)
        {
            try
            {
                return PartialView(new HREmployeeFamilyDetailsViewModel
                {
                    HeaderTitle = "Attendance System Management",
					SessionDetails = SessionDetail,
					BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeFamilyDetails",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeFamilyDetails",
                    BreadCrumbActionName = "_PrintHREmployeeFamilyDetailsAsync",
                    FormModelName = "HREmployeeFamilyDetails",
                    ModalTitle = "परिवारको सदस्यको विवरण सुची छाप्नुहोस्",
                    BreadCrumbTitle = "Employee Job",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBModelList = await _hREmployeeservices.FindAllAsync(Condition(idEmployee))
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
        public async Task<ActionResult> _CreateHREmployeeFamilyDetailsAsync(HREmployeeFamilyDetailsViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeFamilyDetailsAsync(viewModel, CRUDType.CREATE, formCollection);
        }

		[AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _UpdateHREmployeeFamilyDetailsAsync(HREmployeeFamilyDetailsViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeFamilyDetailsAsync(viewModel, CRUDType.UPDATE, formCollection);
        }

		[AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _DeleteHREmployeeFamilyDetailsAsync(HREmployeeFamilyDetailsViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeFamilyDetailsAsync(viewModel, CRUDType.DELETE, formCollection);
        }

        [NonAction]
        protected async Task<ActionResult> RETSourceHREmployeeFamilyDetailsAsync(HREmployeeFamilyDetailsViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
				viewModel.SessionDetails = SessionDetail;
                viewModel.BreadCrumbArea = "EmployeeManagement";
                viewModel.BreadCrumbController = "HREmployeeFamilyDetails";
                viewModel.BreadCrumbBaseURL = "EmployeeManagement/HREmployeeFamilyDetails";
                viewModel.DDLBloodGroup = await GetIntStringPairModel(PairModelTitle.BloodGroup);
                viewModel.DDLMaritalStatus = await GetLongStringPairModel(PairModelTitle.MaritalStatus);
                viewModel.DDLGender = await GetLongStringPairModel(PairModelTitle.Gender);
                viewModel.DDLLanguage = await GetLongStringPairModel(PairModelTitle.HRLanguage);
                viewModel.DDLRelation = await GetIntStringPairModel(PairModelTitle.Relation);
                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        viewModel.SubmitButtonText = SubmitButtonText.Create;
                        viewModel.BreadCrumbActionName = "_CreateHREmployeeFamilyDetailsAsync";
                        viewModel.ModalTitle = "नयाँ परिवारको सदस्य थप्नुहोस्";
                        viewModel.FormModelName = "HREmployeeFamilyDetails";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.UPDATE:
                        viewModel.BreadCrumbActionName = "_UpdateHREmployeeFamilyDetailsAsync";
                        viewModel.ModalTitle = "परिवारको सदस्यको विवरण सम्पादन गर्नुहोस";
                        viewModel.FormModelName = "HREmployeeFamilyDetails";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.DELETE:
                        viewModel.BreadCrumbActionName = "_DeleteHREmployeeFamilyDetailsAsync";
                        viewModel.ModalTitle = "परिवारको सदस्यको विवरण मेटाउनु्होस";
                        viewModel.FormModelName = "HREmployeeFamilyDetails";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.PRINT:
                        viewModel.BreadCrumbActionName = "_PrintHREmployeeFamilyDetailsAsync";
                        viewModel.ModalTitle = "परिवारको सदस्यको विवरण सुची छाप्नुहोस्";
                        viewModel.FormModelName = "HREmployeeFamilyDetails";
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
        protected async Task<ActionResult> CRUDHREmployeeFamilyDetailsAsync(HREmployeeFamilyDetailsViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceHREmployeeFamilyDetailsAsync(viewModel, cRUDType);
                }
                long id = await _hREmployeeservices.CommitSaveChangesAsync(viewModel.DataModel, cRUDType);
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceHREmployeeFamilyDetailsAsync(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListHREmployeeFamilyDetailsAsync", new { idEmployee = viewModel.DataModel.IdParent_HREmployee, pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }

        #endregion

    }
}