using AttendanceManagementSystem.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static SystemStores.GlobalMethod.SystemGlobalMethod;
using static SystemStores.ENUMData.EnumGlobal;
using SystemServices.EmployeeManagement;
using SystemViewModels.EmployeeManagement;
using System.Threading.Tasks;
using System.Net;
using SystemModels.EmployeeManagement;
using System.Linq.Expressions;
using AttendanceManagementSystem.App_Auth;
using SystemDatabase;

namespace AttendanceManagementSystem.Areas.EmployeeManagement.Controllers
{
    public class HREmployeeEducationController : BaseController
    {
        // GET: EmployeeManagement/HREmployeeEducation

        private readonly HREmployeeEducationServices _hrEmployeeEducationServices;

        public HREmployeeEducationController()
        {
            this._hrEmployeeEducationServices = new HREmployeeEducationServices(this._unitOfWork);
        }




		#region HREmployee Education

		[NonAction]
		public Expression<Func<HREmployeeEducation, bool>> Condition(long? idHREmployee, string searchKey = "")
		{
			Expression<Func<HREmployeeEducation, bool>> retutndata = (x => false);
			if (this.SessionDetail.IdRoleType == 0) retutndata = (x => x.IdHREmployee == idHREmployee && (x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
			else if (this.SessionDetail.IdRoleType == 1 || this.SessionDetail.IdRoleType == 2 || this.SessionDetail.IdRoleType == 4) retutndata = (x => x.IdHREmployee == idHREmployee && x.HREmployee.IdHRCompany == SessionDetail.IdHRCompany && (x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
			else retutndata = (x => x.IdHREmployee == this.SessionDetail.IdHREmployee && (x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
			return retutndata;
		}

		[AuthorizeUser(ActionName = "Index")]
		[AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListWrapperHREmployeeEducationAsync(long? idHREmployee, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                if (idHREmployee == null || idHREmployee == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
				return PartialView(new HREmployeeEducationViewModelList
				{
					IdHREmployee = idHREmployee,
					SessionDetails = SessionDetail,
                    DBModelList = await _hrEmployeeEducationServices.GetPagedList(Condition(idHREmployee,searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeEducation",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeEducation",
                    BreadCrumbActionName = "_ListWrapperHREmployeeEducationAsync",
                    BreadCrumbTitle = "Employee Education History",
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
        public async Task<ActionResult> _ListHREmployeeEducationAsync(long? idHREmployee, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HREmployeeEducationViewModelList
                {
                    IdHREmployee = idHREmployee,
					SessionDetails = SessionDetail,
					DBModelList = await _hrEmployeeEducationServices.GetPagedList(Condition(idHREmployee,searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeEducation",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeEducation",
                    BreadCrumbActionName = "_ListHREmployeeEducationAsync",
                    BreadCrumbTitle = "Employee Education",
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
        public async Task<ActionResult> _CreateHREmployeeEducationAsync(long? idHREmployee)
        {
            try
            {
                
                return PartialView(new HREmployeeEducationViewModel
                {
                    SessionDetails = SessionDetail,
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeEducation",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeEducation",
                    BreadCrumbActionName = "_CreateHREmployeeEducationAsync",
                    FormModelName = "HREmployeeEducation",
                    ModalTitle = "नयाँ कर्मचारीको शिक्षा विवरण थप्नुहोस्",
                    BreadCrumbTitle = "Employee Education",
                    CRUDAction = CRUDType.CREATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Create,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLEducationType = await GetIntStringPairModel(PairModelTitle.EducationType),
                    DataModel = new HREmployeeEducationModel
                    {
                        Id = 0,
                        IdHREmployee = idHREmployee,
                        StartedDate=DateTime.Now.Date,
                        EndDate=DateTime.Now.Date,
                        StartedDateNp = Today.Result.Nepdate,
                        EndDateNp = Today.Result.Nepdate
                    },
                    IdHREmployee = idHREmployee
                  
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

		[AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadHREmployeeEducationAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);                
                var model = await _hrEmployeeEducationServices.GetModelByIdAsync(id);
                model.StartedDateNp = Date(model.StartedDate ?? DateTime.Now).Nepdate;
                model.EndDateNp = Date(model.EndDate ?? DateTime.Now).Nepdate;
                return PartialView(new HREmployeeEducationViewModel
                {
					SessionDetails = SessionDetail,
					HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeEducation",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeEducation",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_ReadHREmployeeEducationAsync",
                    FormModelName = "HREmployeeEducation",
                    ModalTitle = "कर्मचारीको शिक्षा विवरण हेर्नुहोस्",
                    BreadCrumbTitle = "Employee Education",
                    CRUDAction = CRUDType.READ,
                    OnlyCancelButton = true,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLEducationType = await GetIntStringPairModel(PairModelTitle.EducationType),
                    DataModel = model,
                    IdHREmployee = model.IdHREmployee,
                   
            });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

		[AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _UpdateHREmployeeEducationAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
              
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _hrEmployeeEducationServices.GetModelByIdAsync(id);
                model.StartedDateNp = Date(model.StartedDate??DateTime.Now).Nepdate;
                model.EndDateNp = Date(model.EndDate??DateTime.Now).Nepdate;
                return PartialView(new HREmployeeEducationViewModel
                {
					SessionDetails = SessionDetail,
					HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeManagement",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeManagement",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_UpdateHREmployeeEducationAsync",
                    FormModelName = "HREmployeeEducation",
                    ModalTitle = "कर्मचारीको शिक्षा विवरण सम्पादन गर्नुहोस्",
                    BreadCrumbTitle = "Employee",
                    CRUDAction = CRUDType.UPDATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Update,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLEducationType = await GetIntStringPairModel(PairModelTitle.EducationType),
                    DataModel = model,
                    IdHREmployee = model.IdHREmployee
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

		[AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _DeleteHREmployeeEducationAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _hrEmployeeEducationServices.GetModelByIdAsync(id);
                model.StartedDateNp = Date(model.StartedDate ?? DateTime.Now).Nepdate;
                model.EndDateNp = Date(model.EndDate ?? DateTime.Now).Nepdate;
                return PartialView(new HREmployeeEducationViewModel
                {
					SessionDetails = SessionDetail,
					HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeEducation",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeEducation",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    FormModelName = "HREmployeeEducation",
                    ModalTitle = " कर्मचारीको शिक्षा विवरण मेटाउनुहोस्",
                    BreadCrumbTitle = "Employee Education",
                    BreadCrumbActionName = "_DeleteHREmployeeEducationAsync",
                    CRUDAction = CRUDType.DELETE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Delete,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLEducationType = await GetIntStringPairModel(PairModelTitle.EducationType),
                    DataModel = model,
                    IdHREmployee = model.IdHREmployee
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

		[AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ExportHREmployeeEducationAsync(long? idHREmployee)
        {
            try
            {
                var model = await _hrEmployeeEducationServices.FindAllAsync(Condition(idHREmployee));
                return await ExportToExcel("EmployeeEducation", model);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

		[AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintHREmployeeEducationAsync(long? idHREmployee)
        {
            try
            {
                return PartialView(new HREmployeeEducationViewModel
                {
					SessionDetails = SessionDetail,
					HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeEducation",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeEducation",
                    BreadCrumbActionName = "_PrintEmployeeEducationAsync",
                    FormModelName = "HREmployeeEducation",
                    ModalTitle = "कर्मचारीको शिक्षा विवरण छाप्नुहोस",
                    BreadCrumbTitle = "Employee Education",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    IdHREmployee = idHREmployee,
                    DBModelList = await _hrEmployeeEducationServices.FindAllAsync(Condition(idHREmployee))
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
        public async Task<ActionResult> _CreateHREmployeeEducationAsync(HREmployeeEducationViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeEducationAsync(viewModel, CRUDType.CREATE, formCollection);
        }

		[AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _UpdateHREmployeeEducationAsync(HREmployeeEducationViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeEducationAsync(viewModel, CRUDType.UPDATE, formCollection);
        }

		[AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _DeleteHREmployeeEducationAsync(HREmployeeEducationViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeEducationAsync(viewModel, CRUDType.DELETE, formCollection);
        }

        [NonAction]
        protected async Task<ActionResult> RETSourceHREmployeeEducationAsync(HREmployeeEducationViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
				viewModel.SessionDetails = SessionDetail;
                viewModel.BreadCrumbArea = "EmployeeManagement";
                viewModel.BreadCrumbController = "HREmployeeEducation";
                viewModel.BreadCrumbBaseURL = "EmployeeManagement/HREmployeeEducation";
                viewModel.DDLEducationType = await GetIntStringPairModel(PairModelTitle.EducationType);
                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        viewModel.SubmitButtonText = SubmitButtonText.Create;
                        viewModel.BreadCrumbActionName = "_CreateHREmployeeEducationAsync";
                        viewModel.ModalTitle = "नयाँ कर्मचारीको शिक्षा विवरण थप्नुहोस्";
                        viewModel.FormModelName = "HREmployeeEducation";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.UPDATE:
                        viewModel.BreadCrumbActionName = "_UpdateHREmployeeEducationAsync";
                        viewModel.ModalTitle = "कर्मचारीको शिक्षा सम्पादन गर्नुहोस्";
                        viewModel.FormModelName = "HREmployeeEducation";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.DELETE:
                        viewModel.BreadCrumbActionName = "_DeleteHREmployeeEducationAsync";
                        viewModel.ModalTitle = "कर्मचारीको शिक्षा विवरण मेटाउनुहोस्";
                        viewModel.FormModelName = "HREmployeeEducation";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.PRINT:
                        viewModel.BreadCrumbActionName = "_PrintHREmployeeEducationAsync";
                        viewModel.ModalTitle = "कर्मचारीको शिक्षा विवरण छाप्नुहोस";
                        viewModel.FormModelName = "HREmployeeEducation";
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
        protected async Task<ActionResult> CRUDHREmployeeEducationAsync(HREmployeeEducationViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceHREmployeeEducationAsync(viewModel, cRUDType);
                }
                long id = await _hrEmployeeEducationServices.CommitSaveChangesAsync(viewModel.DataModel, cRUDType);
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceHREmployeeEducationAsync(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListHREmployeeEducationAsync", new { idHREmployee = viewModel.DataModel.IdHREmployee, pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }

        #endregion
    }
}