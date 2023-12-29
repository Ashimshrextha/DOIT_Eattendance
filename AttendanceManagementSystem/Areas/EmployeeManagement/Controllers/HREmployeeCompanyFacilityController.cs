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
    public class HREmployeeCompanyFacilityController : BaseController
    {
        // GET: EmployeeManagement/HREmployeeCompanyFacility

        private readonly HREmployeeCompanyFacilityServices _hrEmployeeCompanyFacilityServices;
        public HREmployeeCompanyFacilityController()
        {
            this._hrEmployeeCompanyFacilityServices = new HREmployeeCompanyFacilityServices(this._unitOfWork);
        }

        #region HREmployee Facilities
        [NonAction]
        public Expression<Func<HREmployeeCompanyFacility, bool>> Condition(long? idHREmployee, string searchKey = "")
        {
            Expression<Func<HREmployeeCompanyFacility, bool>> retutndata = (x => false);
            if (this.SessionDetail.IdRoleType == 0) retutndata = (x => x.IdHREmployee == idHREmployee && (x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
            else if (this.SessionDetail.IdRoleType == 1 || this.SessionDetail.IdRoleType == 2 || this.SessionDetail.IdRoleType == 4) retutndata = (x => x.IdHREmployee == idHREmployee && x.HREmployee.IdHRCompany == SessionDetail.IdHRCompany && (x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
            else retutndata = (x => x.IdHREmployee == this.SessionDetail.IdHREmployee && (x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
            return retutndata;
        }

        [AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListWrapperHREmployeeCompanyFacilityAsync(long? idHREmployee, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                if (idHREmployee == null || idHREmployee == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HREmployeeCompanyFacilityViewModelList
                {
                    IdHREmployee = idHREmployee,
                    SessionDetails = SessionDetail,
                    DBModelList = await _hrEmployeeCompanyFacilityServices.GetPagedList(Condition(idHREmployee, searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeCompanyFacility",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeCompanyFacility",
                    BreadCrumbActionName = "_ListWrapperHREmployeeCompanyFacilityAsync",
                    BreadCrumbTitle = "Employee Office Facility",
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
        public async Task<ActionResult> _ListHREmployeeCompanyFacilityAsync(long? idHREmployee, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                if (idHREmployee == null || idHREmployee == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HREmployeeCompanyFacilityViewModelList
                {
                    IdHREmployee = idHREmployee,
                    SessionDetails = SessionDetail,
                    DBModelList = await _hrEmployeeCompanyFacilityServices.GetPagedList(Condition(idHREmployee, searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeCompanyFacility",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeCompanyFacility",
                    BreadCrumbActionName = "_ListHREmployeeCompanyFacilityAsync",
                    BreadCrumbTitle = "Office Facility",
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
        public async Task<ActionResult> _CreateHREmployeeCompanyFacilityAsync(long? idHREmployee)
        {
            try
            {
                var today = Today.Result;
                if (idHREmployee == null || idHREmployee == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                return PartialView(new HREmployeeCompanyFacilityViewModel
                {
                    SessionDetails = SessionDetail,
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeCompanyFacility",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeCompanyFacility",
                    BreadCrumbActionName = "_CreateHREmployeeCompanyFacilityAsync",
                    FormModelName = "HREmployeeCompanyFacility",
                    ModalTitle = "नयाँ कार्यालय सुविधा थप्नुहोस्",
                    BreadCrumbTitle = "Office Facility ",
                    CRUDAction = CRUDType.CREATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Create,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    IdHREmployee = idHREmployee,
                    DDLEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeById, 0, idHREmployee),
                    DataModel = new HREmployeeCompanyFacilityModel
                    {
                        Id = 0,
                        IdHREmployee = idHREmployee,
                        IsActive = true,
                        IsReturnBack = false,
                        ReturnDateNp = today.Nepdate
                    },
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadHREmployeeCompanyFacilityAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _hrEmployeeCompanyFacilityServices.GetModelByIdAsync(id);
                return PartialView(new HREmployeeCompanyFacilityViewModel
                {
                    SessionDetails = SessionDetail,
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeCompanyFacility",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeCompanyFacility",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_ReadHREmployeeCompanyFacilityAsync",
                    FormModelName = "HREmployeeCompanyFacility",
                    ModalTitle = "कार्यालय सुविधा विवरण हेर्नुहोस्",
                    BreadCrumbTitle = "Employee",
                    CRUDAction = CRUDType.READ,
                    OnlyCancelButton = true,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeById, 0, model.IdHREmployee),
                    DataModel = model,
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _UpdateHREmployeeCompanyFacilityAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _hrEmployeeCompanyFacilityServices.GetModelByIdAsync(id);
                return PartialView(new HREmployeeCompanyFacilityViewModel
                {
                    SessionDetails = SessionDetail,
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeCompanyFacility",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeCompanyFacility",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_UpdateHREmployeeCompanyFacilityAsync",
                    FormModelName = "HREmployeeCompanyFacility",
                    ModalTitle = "कार्यालय सुविधा विवरण सम्पादन गर्नुहोस्",
                    BreadCrumbTitle = "Employee",
                    CRUDAction = CRUDType.UPDATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Update,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeById, 0, model.IdHREmployee),
                    DataModel = model,
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _DeleteHREmployeeCompanyFacilityAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _hrEmployeeCompanyFacilityServices.GetModelByIdAsync(id);
                return PartialView(new HREmployeeCompanyFacilityViewModel
                {
                    SessionDetails = SessionDetail,
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeCompanyFacility",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeCompanyFacility",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    FormModelName = "HREmployeeCompanyFacility",
                    ModalTitle = "कार्यालय सुविधा मेटाउनुहोस्",
                    BreadCrumbActionName = "_DeleteHREmployeeCompanyFacilityAsync",
                    CRUDAction = CRUDType.DELETE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Delete,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeById, 0, model.IdHREmployee),
                    DataModel = model
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ExportHREmployeeCompanyFacilityAsync(long? idHREmployee)
        {
            try
            {
                if (idHREmployee == null || idHREmployee == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var model = await _hrEmployeeCompanyFacilityServices.FindAllAsync(Condition(idHREmployee));
                return await ExportToExcel("OfficeFacility", model);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintHREmployeeCompanyFacilityAsync(long? idHREmployee)
        {
            try
            {
                if (idHREmployee == null || idHREmployee == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                return PartialView(new HREmployeeCompanyFacilityViewModel
                {
                    SessionDetails = SessionDetail,
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeCompanyFacility",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeCompanyFacility",
                    BreadCrumbActionName = "_PrintHREmployeeCompanyFacilityAsync",
                    FormModelName = "HREmployeeCompanyFacility",
                    ModalTitle = "कार्यालय सुविधा सुची छाप्नुहोस",
                    BreadCrumbTitle = "Office Facility",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBModelList = await _hrEmployeeCompanyFacilityServices.FindAllAsync(Condition(idHREmployee))
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
        public async Task<ActionResult> _CreateHREmployeeCompanyFacilityAsync(HREmployeeCompanyFacilityViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeCompanyFacilityAsync(viewModel, CRUDType.CREATE, formCollection);
        }

        [AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _UpdateHREmployeeCompanyFacilityAsync(HREmployeeCompanyFacilityViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeCompanyFacilityAsync(viewModel, CRUDType.UPDATE, formCollection);
        }

        [AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _DeleteHREmployeeCompanyFacilityAsync(HREmployeeCompanyFacilityViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeCompanyFacilityAsync(viewModel, CRUDType.DELETE, formCollection);
        }

        [NonAction]
        protected async Task<ActionResult> RETSourceHREmployeeCompanyFacilityAsync(HREmployeeCompanyFacilityViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
                viewModel.SessionDetails = SessionDetail;
                viewModel.BreadCrumbArea = "EmployeeManagement";
                viewModel.BreadCrumbController = "HREmployeeCompanyFacility";
                viewModel.BreadCrumbBaseURL = "EmployeeManagement/HREmployeeCompanyFacility";
                viewModel.DDLEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeById, 0, viewModel.DataModel.IdHREmployee);
                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        viewModel.SubmitButtonText = SubmitButtonText.Create;
                        viewModel.BreadCrumbActionName = "_CreateHREmployeeCompanyFacilityAsync";
                        viewModel.ModalTitle = "नयाँ कार्यालय सुविधा थप्नुहोस्";
                        viewModel.FormModelName = "HREmployeeCompanyFacility";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.UPDATE:
                        viewModel.BreadCrumbActionName = "_UpdateHREmployeeCompanyFacilityAsync";
                        viewModel.ModalTitle = "कार्यालय सुविधा विवरण सम्पादन गर्नुहोस्";
                        viewModel.FormModelName = "HREmployeeCompanyFacility";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.DELETE:
                        viewModel.BreadCrumbActionName = "_DeleteHREmployeeCompanyFacilityAsync";
                        viewModel.ModalTitle = "कार्यालय सुविधा मेटाउनुहोस्";
                        viewModel.FormModelName = "HREmployeeCompanyFacility";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.PRINT:
                        viewModel.BreadCrumbActionName = "_PrintHREmployeeCompanyFacilityAsync";
                        viewModel.ModalTitle = "कार्यालय सुविधा सुची छाप्नुहोस";
                        viewModel.FormModelName = "HREmployeeCompanyFacility";
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
        protected async Task<ActionResult> CRUDHREmployeeCompanyFacilityAsync(HREmployeeCompanyFacilityViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceHREmployeeCompanyFacilityAsync(viewModel, cRUDType);
                }
                long id = await _hrEmployeeCompanyFacilityServices.CommitSaveChangesAsync(viewModel.DataModel, cRUDType);
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceHREmployeeCompanyFacilityAsync(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListHREmployeeCompanyFacilityAsync", new { idHREmployee = viewModel.DataModel.IdHREmployee, pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }

        #endregion
    }
}