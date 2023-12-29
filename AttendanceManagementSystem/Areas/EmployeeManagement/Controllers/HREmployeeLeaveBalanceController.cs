using AttendanceManagementSystem.App_Auth;
using AttendanceManagementSystem.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using SystemDatabase;
using SystemModels.EmployeeManagement;
using SystemServices.EmployeeManagement;
using SystemServices.SystemSetting;
using SystemViewModels.EmployeeManagement;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;

namespace AttendanceManagementSystem.Areas.EmployeeManagement.Controllers
{
    public class HREmployeeLeaveBalanceController : BaseController
    {
        // GET: EmployeeManagement/HREmployeeLeaveBalance

        private readonly HREmployeeLeaveBalanceServices _hREmployeeLeaveBalanceServices;
        private readonly HREmployeeServices _hREmployeeServices;
        private readonly HRCompanyLeaveTypeServices _hRCompanyLeaveTypeServices;

        public HREmployeeLeaveBalanceController()
        {
            this._hREmployeeLeaveBalanceServices = new HREmployeeLeaveBalanceServices(this._unitOfWork);
            this._hREmployeeServices = new HREmployeeServices(this._unitOfWork);
            this._hRCompanyLeaveTypeServices = new HRCompanyLeaveTypeServices(this._unitOfWork);
        }

        [NonAction]
        public Expression<Func<HREmployeeLeaveBalance, bool>> Condition(long? idHREmployee, string searchKey = "")
        {
            int lastYear = (Date(DateTime.Now).NepYear - 1);
            List<long> list = GetCurrentActiveJobHolder(SessionDetail.IdHRCompany);


            Expression<Func<HREmployeeLeaveBalance, bool>> returnData = (x => false);
            switch (this.SessionDetail.IdRoleType)
            {
                case (int)RoleType.SuperUser:
                    return returnData = (x => x.IdHREmployee == idHREmployee && !list.Contains(x.IdHREmployee) && (x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
                case (int)RoleType.Admin:
                    return returnData = (x => x.IdHREmployee == idHREmployee && x.LeaveYear >= lastYear  && !list.Contains(x.IdHREmployee)  && x.HREmployee.IdHRCompany == SessionDetail.IdHRCompany && (x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
                case (int)RoleType.RootUser:
                    return returnData = (x => x.IdHREmployee == idHREmployee && x.LeaveYear >= lastYear && !list.Contains(x.IdHREmployee)  && x.HREmployee.IdHRCompany == SessionDetail.IdHRCompany && (x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
                case (int)RoleType.SectionAdmin:
                    return returnData = (x => x.IdHREmployee == idHREmployee && x.LeaveYear >= lastYear && !list.Contains(x.IdHREmployee) && x.HREmployee.IdHRCompany == SessionDetail.IdHRCompany && (x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
                case (int)RoleType.NormalUser:
                    return returnData = (x => x.IdHREmployee == idHREmployee && x.LeaveYear >= lastYear && x.HREmployee.IdHRCompany == SessionDetail.IdHRCompany && (x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
                default:
                    return returnData;
            }
        }

        [AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> Index(long? idHREmployee, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            try
            {
               
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return View(new HREmployeeLeaveBalanceViewModelList
                {
                    SessionDetails = SessionDetail,
                    DBModelList = await _hREmployeeLeaveBalanceServices.GetPagedList(Condition(idHREmployee), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeLeaveBalance",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLeaveBalance",
                    BreadCrumbTitle = "Leave Balance",
                    BreadCrumbActionName = "Index",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    IdHREmployee = idHREmployee,

                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListWrapperHREmployeeLeaveBalanceAsync(long? idHREmployee, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
               
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _hREmployeeLeaveBalanceServices.GetPagedList(Condition(idHREmployee, searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection);
               
                //var empModel = await _hREmployeeServices.GetModelByIdAsync(idHREmployee);
                var empModel = await EmployeeDetails(idHREmployee);
                return PartialView(new HREmployeeLeaveBalanceViewModelList
                {
                    DBModelList = model,
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeLeaveBalance",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLeaveBalance",
                    BreadCrumbActionName = "_ListHREmployeeAsync",
                    BreadCrumbTitle = "Employee Leave Balance",
                    CRUDAction = CRUDType.READ,
                    SessionDetails = SessionDetail,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    IdHRCompany = empModel.IdHRCompany,
                    IdHREmployee = idHREmployee,
                   
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListHREmployeeLeaveBalanceAsync(long? idHREmployee, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
               
                var empModel = await EmployeeDetails(idHREmployee);
                //var empModel = await _hREmployeeServices.GetModelByIdAsync(idHREmployee);



                return PartialView(new HREmployeeLeaveBalanceViewModelList
                {
                    SessionDetails = SessionDetail,
                    DBModelList = await _hREmployeeLeaveBalanceServices.GetPagedList(Condition(idHREmployee, searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeLeaveBalance",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLeaveBalance",
                    BreadCrumbActionName = "_ListHREmployeeLeaveBalanceAsync",
                    BreadCrumbTitle = "Leave Balance",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    IdHREmployee = idHREmployee,
                    IdHRCompany = empModel.IdHRCompany,
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _CreateHREmployeeLeaveBalanceAsync(long? idHREmployee)
        {
            try
            {
                if (idHREmployee == 0 || idHREmployee == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var yearNP = Date(DateTime.Now).NepYear;
                var lastyearNp = yearNP - 1;

                var model = await EmployeeDetails(idHREmployee);
                //var model = await _hREmployeeServices.GetModelByIdAsync(idHREmployee);
                return PartialView(new HREmployeeLeaveBalanceViewModel
                {
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeLeaveBalance",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLeaveBalance",
                    BreadCrumbActionName = "_CreateHREmployeeLeaveBalanceAsync",
                    FormModelName = "HREmployeeLeaveBalance",
                    ModalTitle = "नयाँ सञ्चित बिदा थप्नुहोस्",
                    BreadCrumbTitle = "सञ्चित बिदा",
                    CRUDAction = CRUDType.CREATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SessionDetails = SessionDetail,
                    SubmitButtonText = SubmitButtonText.Create,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    IdHREmployee = idHREmployee,
                    lastYearNp = lastyearNp,
                    IdHRCompany = model.IdHRCompany,
                    DDLYearNp = await GetIntStringPairModel(PairModelTitle.Year),
                    //DDLEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeById, 0, idHREmployee),
                    //DDLLeaveType = await GetLongStringPairModel(PairModelTitle.SavingLeaveType, this.SessionDetail.IdHRCompany, idHREmployee),
                    DataModel = new HREmployeeLeaveBalanceModel { IdHREmployee = (long)idHREmployee, IsActive = true, LeaveYear = this.Today.Result.NepYear }
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadHREmployeeLeaveBalanceAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var yearNP = Date(DateTime.Now).NepYear;
                var lastyearNp = yearNP - 1;
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);


                var model = await EmployeeLeaveBalanceDetails(id);
                //var model = await _hREmployeeLeaveBalanceServices.GetModelByIdAsync(id);

                var empModel = await EmployeeDetails(model.IdHREmployee);
                //var empModel = await _hREmployeeServices.GetModelByIdAsync(model.IdHREmployee);
                return PartialView(new HREmployeeLeaveBalanceViewModel
                {
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeLeaveBalance",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLeaveBalance",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    SessionDetails = SessionDetail,
                    BreadCrumbActionName = "_ReadHREmployeeLeaveBalanceAsync",
                    FormModelName = "HREmployeeLeaveBalance",
                    ModalTitle = "सञ्चित बिदा हेर्नुहोस ",
                    CRUDAction = CRUDType.READ,
                    OnlyCancelButton = true,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    IdHRCompany = empModel.IdHRCompany,
                    IdHREmployee = empModel.Id,
                    DDLYearNp = await GetIntStringPairModel(PairModelTitle.Year),
                    //DDLLeaveType = await GetLongStringPairModel(PairModelTitle.SavingLeaveType, empModel.IdHRCompany),
                    //DDLEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeById, 0, model.IdHREmployee),
                    DataModel = model,
                    lastYearNp = lastyearNp,
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _UpdateHREmployeeLeaveBalanceAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var yearNP = Date(DateTime.Now).NepYear;
                var lastyearNp = yearNP - 1;
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
               
                
                var model = await EmployeeLeaveBalanceDetails(id);
                //var model = await _hREmployeeLeaveBalanceServices.GetModelFindAsync(x => x.Id == id);

                var empModel = await EmployeeDetails(model.IdHREmployee);
                //var empModel = await _hREmployeeServices.GetModelFindAsync(x => x.Id == model.IdHREmployee);

                return PartialView(new HREmployeeLeaveBalanceViewModel
                {
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeLeaveBalance",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLeaveBalance",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    SessionDetails = SessionDetail,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_UpdateHREmployeeLeaveBalanceAsync",
                    FormModelName = "HREmployeeLeaveBalance",
                    ModalTitle = "जम्मा सञ्चित बिदा सम्पादन गर्नुहोस्",
                    BreadCrumbTitle = "जम्मा सञ्चित बिदा",
                    CRUDAction = CRUDType.UPDATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Update,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = model,
                    lastYearNp = lastyearNp,
                    IdHRCompany = empModel.IdHRCompany,
                    IdHREmployee = model.IdHREmployee,
                    DDLYearNp = await GetIntStringPairModel(PairModelTitle.Year),
                    //DDLLeaveType = await GetLongStringPairModel(PairModelTitle.SavingLeaveType, empModel.IdHRCompany),
                    //DDLEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeById, 0, model.IdHREmployee)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _DeleteHREmployeeLeaveBalanceAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var yearNP = Date(DateTime.Now).NepYear;
                var lastyearNp = yearNP - 1;
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                
                var model = await EmployeeLeaveBalanceDetails(id);
                //var model = await _hREmployeeLeaveBalanceServices.GetModelByIdAsync(id);

                var empModel = await EmployeeDetails(model.IdHREmployee);
                //var empModel = await _hREmployeeServices.GetModelByIdAsync(model.IdHREmployee);
                return PartialView(new HREmployeeLeaveBalanceViewModel
                {
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeLeaveBalance",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLeaveBalance",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    FormModelName = "HREmployeeLeaveBalance",
                    ModalTitle = "सञ्चित बिदा मेटाउनुहोस",
                    BreadCrumbTitle = "Leave Balance",
                    BreadCrumbActionName = "_DeleteHREmployeeLeaveBalanceAsync",
                    CRUDAction = CRUDType.DELETE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Delete,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = model,
                    lastYearNp = lastyearNp,
                    IdHREmployee = model.IdHREmployee,
                    IdHRCompany = empModel.IdHRCompany,
                    DDLYearNp = await GetIntStringPairModel(PairModelTitle.Year),
                    //DDLLeaveType = await GetLongStringPairModel(PairModelTitle.SavingLeaveType, empModel.IdHRCompany),
                    //DDLEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeById, 0, model.IdHREmployee)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ExportHREmployeeLeaveBalanceAsync(long? idHREmployee)
        {
            try
            {
                var model = await _hREmployeeLeaveBalanceServices.FindAllAsync(Condition(idHREmployee));
                var datalist = model.Select(x=>new {
                    Name = x.HREmployee.HREmployeeNameNP,
                    LeaveType = x.HRCompanyLeaveType.MasterLeaveTitle.LeaveTitleNP,
                    Year=x.LeaveYear,
            });
                return await ExportToExcel("HREmployeeLeaveBalance", datalist);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintHREmployeeLeaveBalanceAsync(long? idHREmployee)
        {
            try
            {
                return PartialView(new HREmployeeLeaveBalanceViewModel
                {
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeLeaveBalance",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLeaveBalance",
                    BreadCrumbActionName = "_PrintHREmployeeLeaveBalanceAsync",
                    FormModelName = "HREmployeeLeaveBalance",
                    ModalTitle = "सञ्चित बिदा छाप्नुहोस्",
                    SessionDetails = SessionDetail,
                    BreadCrumbTitle = "Leave Balance",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBModelList = await _hREmployeeLeaveBalanceServices.FindAllAsync(Condition(idHREmployee))
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
        public async Task<ActionResult> _CreateHREmployeeLeaveBalanceAsync(HREmployeeLeaveBalanceViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeLeaveBalance(viewModel, CRUDType.CREATE, formCollection);
        }

        [AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _UpdateHREmployeeLeaveBalanceAsync(HREmployeeLeaveBalanceViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeLeaveBalance(viewModel, CRUDType.UPDATE, formCollection);
        }

        [AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _DeleteHREmployeeLeaveBalanceAsync(HREmployeeLeaveBalanceViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeLeaveBalance(viewModel, CRUDType.DELETE, formCollection);
        }

        [NonAction]
        protected async Task<ActionResult> RETSourceHREmployeeLeaveBalance(HREmployeeLeaveBalanceViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
                viewModel.SessionDetails = SessionDetail;
                viewModel.BreadCrumbArea = "EmployeeManagement";
                viewModel.BreadCrumbController = "HREmployeeLeaveBalance";
                viewModel.BreadCrumbBaseURL = "EmployeeManagement/HREmployeeLeaveBalance";
                viewModel.DDLEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeById, 0, viewModel.DataModel.IdHREmployee);
                viewModel.DDLLeaveType = await GetLongStringPairModel(PairModelTitle.SavingLeaveType, viewModel.IdHRCompany);
                viewModel.DDLYearNp = await GetIntStringPairModel(PairModelTitle.Year);
                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        viewModel.SubmitButtonText = SubmitButtonText.Create;
                        viewModel.BreadCrumbActionName = "_CreateHREmployeeLeaveBalanceAsync";
                        viewModel.ModalTitle = "नयाँ सञ्चित बिदा थप्नुहोस्";
                        viewModel.FormModelName = "HREmployeeLeaveBalance";
                        viewModel.CRUDAction = CRUDType.CREATE;
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.UPDATE:
                        viewModel.BreadCrumbActionName = "_UpdateHREmployeeLeaveBalanceAsync";
                        viewModel.ModalTitle = "जम्मा सञ्चित बिदा सम्पादन गर्नुहोस्";
                        viewModel.FormModelName = "HREmployeeLeaveBalance";
                        viewModel.CRUDAction = CRUDType.UPDATE;
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.DELETE:
                        viewModel.BreadCrumbActionName = "_DeleteHREmployeeLeaveBalanceAsync";
                        viewModel.ModalTitle = "सञ्चित बिदा मेटाउनुहोस";
                        viewModel.FormModelName = "HREmployeeLeaveBalance";
                        viewModel.CRUDAction = CRUDType.DELETE;
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
        protected async Task<ActionResult> CRUDHREmployeeLeaveBalance(HREmployeeLeaveBalanceViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceHREmployeeLeaveBalance(viewModel, cRUDType);
                }
                //Get Leave Type limit
                var leaveModel = await _hRCompanyLeaveTypeServices.GetModelFindAsync(x => x.IdMasterLeaveTitle == viewModel.DataModel.IdMasterLeaveTitle && x.IdHRCompany == this.SessionDetail.IdHRCompany);
                viewModel.DataModel.IdHRCompanyLeaveType = leaveModel.Id;
                if ((decimal.Parse(leaveModel.SavingLimitDay.ToString()) < viewModel.DataModel.TotalLeave) && leaveModel.SavingLimitDay > 0)
                {
                    Response.StatusCode = 350;
                    ModelState.AddModelError("", $"माफ गर्नुहोस् तपाईंको सञ्चित बिदा सीमा केवल {leaveModel.SavingLimitDay} दिन छ ");
                    ModelState.AddModelError("DataModel.TotalLeave", $"सञ्चित बिदा सीमा केवल {leaveModel.SavingLimitDay} दिन");
                    return await RETSourceHREmployeeLeaveBalance(viewModel, cRUDType);
                }
                //if exist 
                var model = await _hREmployeeLeaveBalanceServices.FindAsync(x => x.IdMasterLeaveTitle == viewModel.DataModel.IdMasterLeaveTitle && x.LeaveYear == viewModel.DataModel.LeaveYear && x.IdHREmployee == viewModel.DataModel.IdHREmployee);
                if (cRUDType == CRUDType.CREATE && model != null)
                {
                    Response.StatusCode = 350;
                    ModelState.AddModelError("", $"माफ गर्नुहोस् सञ्चित बिदा {viewModel.DataModel.LeaveYear} को पहिले नै लिनु भएको छ");
                    return await RETSourceHREmployeeLeaveBalance(viewModel, cRUDType);
                }
                if (cRUDType == CRUDType.UPDATE)
                {
                    model.LeaveYear = viewModel.DataModel.LeaveYear;
                    model.TotalLeave = viewModel.DataModel.TotalLeave;
                    await _hREmployeeLeaveBalanceServices.UpdateAsync(model);
                    await this._unitOfWork.SaveAsync();
                }
                else
                {
                    viewModel.DataModel.IdMasterLeaveTitle = leaveModel.IdMasterLeaveTitle;
                    await _hREmployeeLeaveBalanceServices.CommitSaveChangesAsync(viewModel.DataModel, cRUDType);
                }
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceHREmployeeLeaveBalance(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListHREmployeeLeaveBalanceAsync", new { idHREmployee = viewModel.DataModel.IdHREmployee, pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }


        #region NEWDATA

        public async Task<HREmployeeModel> EmployeeDetails(long? id)
         {
            EAttendanceSystemDBEntities db = new EAttendanceSystemDBEntities();
            HREmployeeModel model = new HREmployeeModel();
            var data = db.HREmployee.Where(x => x.Id == id).Select(x => new HREmployeeModel
            {

                Id = x.Id,
                IdHRCompany = x.IdHRCompany,
                AppointmentDate = x.AppointmentDate,
                AppointmentDateNP = x.AppointmentDateNP,
                HREmployeeName = x.HREmployeeName,
                HREmployeeNameNP = x.HREmployeeNameNP,
                IdBloodGroup = x.IdBloodGroup,
                IdEnroll = x.IdEnroll,
                IdHREmployeeReligion = x.IdHREmployeeReligion,
                IdMarital = x.IdMarital,
                IdMotherTongue = x.IdMotherTongue,
                IdParent_HREmployee = x.IdParent_HREmployee,
                IdRelation = x.IdRelation,
                ImageName = x.ImageName,
                IsActive = x.IsActive,
                IsDisabled = x.IsDisabled,
                PISNumber = x.PISNumber,
                MobileNumber = x.MobileNumber,
                Remark = x.Remark,
                RetiredDate = x.RetiredDate,
                RetiredDateNP = x.RetiredDateNP,
                Status = x.Status,

            }).FirstOrDefault();

            return data;
        }

        public async Task<HREmployeeLeaveBalanceModel> EmployeeLeaveBalanceDetails(long? id)
        {
            EAttendanceSystemDBEntities db = new EAttendanceSystemDBEntities();
            HREmployeeLeaveBalanceModel model = new HREmployeeLeaveBalanceModel();
            var data = db.HREmployeeLeaveBalance.Where(x => x.Id == id).Select(x => new HREmployeeLeaveBalanceModel
            {

              Id = x.Id,
              IdHRCompanyLeaveType = x.IdHRCompanyLeaveType,
              IdHREmployee=x.IdHREmployee,
              IdMasterLeaveTitle=x.IdMasterLeaveTitle,
              LeaveYear=x.LeaveYear,    
              TotalLeave=x.TotalLeave,
             
              

            }).FirstOrDefault();

            return data;
        }




      

        [AllowAnonymous]
        [HttpGet]
        public async Task<JsonResult> DetailsList(long IdHRCompany, long IdHREmployee)
        {
            try
            {
                var DDLLeaveType = await GetLongStringPairModel(PairModelTitle.SavingLeaveType, IdHRCompany);
                var DDLEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeById, 0, IdHREmployee);



                return Json(new
                {

                    dDLEmployee = DDLEmployee,
                    dDLLeaveType = DDLLeaveType,

                }, JsonRequestBehavior.AllowGet);
            }

            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        
        #endregion
    }
}