using AttendanceManagementSystem.Controllers;
using System.Threading.Tasks;
using System.Web.Mvc;
using SystemInterfaces.EmployeeManagement;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;
using SystemViewModels.EmployeeManagement;
using System;
using System.Linq.Expressions;
using SystemDatabase;
using AttendanceManagementSystem.App_Auth;
using System.Net;
using SystemServices.EmployeeManagement;
using SystemModels.EmployeeManagement;

namespace AttendanceManagementSystem.Areas.EmployeeManagement.Controllers
{
    public class HREmployeeAddressController : BaseController
    {
        private readonly HREmployeeAddressServices _hrEmployeeAddressServices;

        public HREmployeeAddressController()
        {
            this._hrEmployeeAddressServices = new HREmployeeAddressServices(this._unitOfWork);
        }


        // GET: EmployeeManagement/HREmployeeAddress
        [NonAction]
        public Expression<Func<HREmployeeAddress, bool>> Condition(long? idHREmployee, string searchKey = "")
        {
            Expression<Func<HREmployeeAddress, bool>> retutndata = (x => false);
            if (this.SessionDetail.IdRoleType == 0) retutndata = (x => x.IdHREmployee == idHREmployee && (x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
            else if (this.SessionDetail.IdRoleType == 1 || this.SessionDetail.IdRoleType == 2 || this.SessionDetail.IdRoleType == 4) retutndata = (x => x.IdHREmployee == idHREmployee && x.HREmployee.IdHRCompany == SessionDetail.IdHRCompany && (x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
            else retutndata = (x => x.IdHREmployee == this.SessionDetail.IdHREmployee && (x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
            return retutndata;
        }



        [AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListWrapperHREmployeeAddressAsync(long? idHREmployee, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                if (idHREmployee == null || idHREmployee == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HREmployeeAddressViewModelList
                {
                    IdHREmployee = idHREmployee,
                    SessionDetails = SessionDetail,
                    DBModelList = await _hrEmployeeAddressServices.GetPagedList(Condition(idHREmployee, searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeAddress",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeAddress",
                    BreadCrumbActionName = "_ListWrapperHREmployeeAddressAsync",
                    BreadCrumbTitle = "Employee Address",
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
        public async Task<ActionResult> _ListHREmployeeAddressAsync(long? idHREmployee, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HREmployeeAddressViewModelList
                {
                    IdHREmployee = idHREmployee,
                    SessionDetails = SessionDetail,
                    DBModelList = await _hrEmployeeAddressServices.GetPagedList(Condition(idHREmployee, searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeAddress",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeAddress",
                    BreadCrumbActionName = "_ListHREmployeeAddressAsync",
                    BreadCrumbTitle = "Employee Address",
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
        public async Task<ActionResult> _CreateHREmployeeAddressAsync(long? idHREmployee)
        {
            try
            {
                return PartialView(new HREmployeeAddressViewModel
                {
                    SessionDetails = SessionDetail,
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeAddress",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeAddress",
                    BreadCrumbActionName = "_CreateHREmployeeAddressAsync",
                    FormModelName = "HREmployeeAddress",
                    ModalTitle = "Create New Address",
                    BreadCrumbTitle = "Employee Address",
                    CRUDAction = CRUDType.CREATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Create,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = new HREmployeeAddressModel { Id = 0, IdHREmployee = (long)idHREmployee, IsActive = true },
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
        public async Task<ActionResult> _ReadHREmployeeAddressAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _hrEmployeeAddressServices.GetModelByIdAsync(id);
                return PartialView(new HREmployeeAddressViewModel
                {
                    SessionDetails = SessionDetail,
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeAddress",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeAddress",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_ReadHREmployeeAddressAsync",
                    FormModelName = "HREmployeeAddress",
                    ModalTitle = "View Employee Address",
                    BreadCrumbTitle = "Employee Address",
                    CRUDAction = CRUDType.READ,
                    OnlyCancelButton = true,
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
        public async Task<ActionResult> _UpdateHREmployeeAddressAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _hrEmployeeAddressServices.GetModelByIdAsync(id);
                return PartialView(new HREmployeeAddressViewModel
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
                    BreadCrumbActionName = "_UpdateHREmployeeAddressAsync",
                    FormModelName = "HREmployeeAddress",
                    ModalTitle = "Update Existing Employee Address",
                    CRUDAction = CRUDType.UPDATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Update,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
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
        public async Task<ActionResult> _DeleteHREmployeeAddressAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _hrEmployeeAddressServices.GetModelByIdAsync(id);
                return PartialView(new HREmployeeAddressViewModel
                {
                    SessionDetails = SessionDetail,
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeAddress",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeAddress",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    FormModelName = "HREmployeeAddress",
                    ModalTitle = "Delete Employee Address",
                    BreadCrumbTitle = "Employee Address",
                    BreadCrumbActionName = "_DeleteHREmployeeAddressAsync",
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
        public async Task<ActionResult> _ExportHREmployeeAddressAsync(long? idHREmployee)
        {
            try
            {
                var model = await _hrEmployeeAddressServices.FindAllAsync(Condition(idHREmployee));
                return await ExportToExcel("EmployeeAddress", model);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintHREmployeeAddressAsync(long? idHREmployee)
        {
            try
            {
                return PartialView(new HREmployeeAddressViewModel
                {
                    SessionDetails = SessionDetail,
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeAddress",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeAddress",
                    BreadCrumbActionName = "_PrintEmployeeEducationAsync",
                    FormModelName = "HREmployeeAddress",
                    ModalTitle = "Print Employee Address",
                    BreadCrumbTitle = "Employee Address",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    IdHREmployee = idHREmployee,
                    DBModelList = await _hrEmployeeAddressServices.FindAllAsync(Condition(idHREmployee))
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
        public async Task<ActionResult> _CreateHREmployeeAddressAsync(HREmployeeAddressViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeAddressAsync(viewModel, CRUDType.CREATE, formCollection);
        }

        [AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _UpdateHREmployeeAddressAsync(HREmployeeAddressViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeAddressAsync(viewModel, CRUDType.UPDATE, formCollection);
        }

        [AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _DeleteHREmployeeAddressAsync(HREmployeeAddressViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeAddressAsync(viewModel, CRUDType.DELETE, formCollection);
        }

        [NonAction]
        protected async Task<ActionResult> RETSourceHREmployeeAddressAsync(HREmployeeAddressViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
                viewModel.SessionDetails = SessionDetail;
                viewModel.BreadCrumbArea = "EmployeeManagement";
                viewModel.BreadCrumbController = "HREmployeeAddress";
                viewModel.BreadCrumbBaseURL = "EmployeeManagement/HREmployeeAddress";
                viewModel.DDLEducationType = await GetIntStringPairModel(PairModelTitle.EducationType);
                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        viewModel.SubmitButtonText = SubmitButtonText.Create;
                        viewModel.BreadCrumbActionName = "_CreateHREmployeeAddressAsync";
                        viewModel.ModalTitle = "Create New Address";
                        viewModel.FormModelName = "HREmployeeAddress";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.UPDATE:
                        viewModel.BreadCrumbActionName = "_UpdateHREmployeeAddressAsync";
                        viewModel.ModalTitle = "Update Existing Address";
                        viewModel.FormModelName = "HREmployeeAddress";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.DELETE:
                        viewModel.BreadCrumbActionName = "_DeleteHREmployeeAddressAsync";
                        viewModel.ModalTitle = "Delete Existing Address";
                        viewModel.FormModelName = "HREmployeeAddress";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.PRINT:
                        viewModel.BreadCrumbActionName = "_PrintHREmployeeAddressAsync";
                        viewModel.ModalTitle = "Print Employee Address";
                        viewModel.FormModelName = "HREmployeeAddress";
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
        protected async Task<ActionResult> CRUDHREmployeeAddressAsync(HREmployeeAddressViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceHREmployeeAddressAsync(viewModel, cRUDType);
                }
                long id = await _hrEmployeeAddressServices.CommitSaveChangesAsync(viewModel.DataModel, cRUDType);
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceHREmployeeAddressAsync(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListHREmployeeAddressAsync", new { idHREmployee = viewModel.DataModel.IdHREmployee, pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }
    }
}