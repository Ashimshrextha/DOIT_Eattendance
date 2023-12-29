using AttendanceManagementSystem.App_Auth;
using AttendanceManagementSystem.Controllers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SystemDatabase;
using SystemModels.EmployeeManagement;
using SystemServices.EmployeeManagement;
using SystemStores.PairModels;
using SystemViewModels.EmployeeManagement;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;

namespace AttendanceManagementSystem.Areas.EmployeeManagement.Controllers
{
    public class HREmployeeJobHistoryController : BaseController
    {
        // GET: EmployeeManagement/HREmployeeJobHistory

        private readonly HREmployeeJobHistoryServices _hREmployeeJobHistoryServices;
        private readonly HREmployeeServices _hREmployeeServices;

        public HREmployeeJobHistoryController()
        {
            this._hREmployeeJobHistoryServices = new HREmployeeJobHistoryServices(this._unitOfWork);
            this._hREmployeeServices = new HREmployeeServices(this._unitOfWork);
        }

        #region HREmployee JobHistory

        [NonAction]
        public Expression<Func<HREmployeeJobHistory, bool>> Condition(long? idHREmployee, string searchKey = "")
        {
            Expression<Func<HREmployeeJobHistory, bool>> returnData = (x => false);
            switch (this.SessionDetail.IdRoleType)
            {
                case (int)RoleType.SuperUser:
                    return returnData = (x => x.IdHREmployee == idHREmployee && (x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
                case (int)RoleType.Admin:
                    return returnData = (x => x.IdHREmployee == idHREmployee && x.HREmployee.IdHRCompany == SessionDetail.IdHRCompany && (x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
                case (int)RoleType.RootUser:
                    return returnData = (x => x.IdHREmployee == idHREmployee && x.HREmployee.IdHRCompany == SessionDetail.IdHRCompany && (x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
                
                case (int)RoleType.SectionAdmin:
                    return returnData = (x => x.IdHREmployee == idHREmployee && x.HREmployee.IdHRCompany == SessionDetail.IdHRCompany && (x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));

                case (int)RoleType.NormalUser:
                    return returnData = (x => x.IdHREmployee == this.SessionDetail.IdHREmployee && (x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
                default:
                    return returnData;
            }
        }

        [AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListWrapperHREmployeeJobAsync(long? idHREmployee, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                if (idHREmployee == null || idHREmployee == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, "Id", orderingDirection);
                var model = await _hREmployeeJobHistoryServices.GetPagedList(Condition(idHREmployee, searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection);
                return PartialView(new HREmployeeJobHistoryViewModelList
                {
                    IdHREmployee = idHREmployee,
                    SessionDetails = SessionDetail,
                    DBModelList = model,
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeJobHistory",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeJobHistory",
                    BreadCrumbActionName = "_ListWrapperHREmployeeJobAsync",
                    BreadCrumbTitle = "कर्मचारी नौकरी विवरण",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = string.Empty,
                    JobHistoryCounter = model.Count()
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListHREmployeeJobAsync(long? idHREmployee, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                if (idHREmployee == null || idHREmployee == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HREmployeeJobHistoryViewModelList
                {
                    IdHREmployee = idHREmployee,
                    SessionDetails = SessionDetail,
                    DBModelList = await _hREmployeeJobHistoryServices.GetPagedList(Condition(idHREmployee, searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeJobHistory",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeJobHistory",
                    BreadCrumbActionName = "_ListHREmployeeJobAsync",
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
        public async Task<ActionResult> _CreateHREmployeeJobAsync(long? idHREmployee)
        {
            try
            {
                if (idHREmployee == null || idHREmployee == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var today = Today.Result;
                //var model = await _hREmployeeServices.GetByIdAsync(idHREmployee);
                var model =await EmployeeDetails(idHREmployee);
                


                return PartialView(new HREmployeeJobHistoryViewModel
                {
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeJobHistory",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeJobHistory",
                    BreadCrumbActionName = "_CreateHREmployeeJobAsync",
                    FormModelName = "HREmployeeJob",
                    ModalTitle = "कर्मचारी नयाँ जागीर विवरण थप्नुहोस्",
                    CRUDAction = CRUDType.CREATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Create,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    IdHREmployee = idHREmployee,
                    //DDLEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeById, model.IdHRCompany, idHREmployee),
                    //DDLDivision = await GetLongStringPairModel(PairModelTitle.Division, model.IdHRCompany),
                    //DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId),
                    //DDLDesignation = await GetLongStringPairModel(PairModelTitle.Designation, model.IdHRCompany),
                    //DDLServiceType = await GetLongStringPairModel(PairModelTitle.ServiceType, model.IdHRCompany),
                    //DDLJobStatus = await GetIntStringPairModel(PairModelTitle.JobStatus),
                    //DDLHREmployeePositionStatusType = await GetIntStringPairModel(PairModelTitle.HREmployeePositionStatusType),
                    DataModel = new HREmployeeJobHistoryModel
                    {
                        Id = 0,
                        IdHREmployee = idHREmployee,
                        IdHRCompany = (long)model.IdHRCompany,
                        JoiningDateNp = today.Nepdate,
                        IdHRCompanyDivision=0
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
        public async Task<ActionResult> _ReadHREmployeeJobAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
           
                //var model = await _hREmployeeJobHistoryServices.GetModelByIdAsync(id);
                var model = await JobHistoryDetails(id);



                model.ImplementationDateNp = model.ImplementationDate != null ? Date((DateTime)model.ImplementationDate).Nepdate : string.Empty;
                model.JoiningDateNp = Date(model.JoiningDate).Nepdate;
                model.ExpiryDateNP = model.ExpiryDate != null ? Date((DateTime)model.ExpiryDate).Nepdate : null;
                model.DecisionDateNP = model.DecisionDate == null ? string.Empty : Date((DateTime)model.DecisionDate).Nepdate;
                return PartialView(new HREmployeeJobHistoryViewModel
                {
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeJobHistory",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeJobHistory",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_ReadHREmployeeJobAsync",
                    FormModelName = "HREmployeeJob",
                    ModalTitle = "कर्मचारी जागीर विवरण हेर्नुहोस्",
                    CRUDAction = CRUDType.READ,
                    OnlyCancelButton = true,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = model,
                    IdHREmployee = model.IdHREmployee,
                    IdHRCompany = model.IdHRCompany,
                    //DDLEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeById, model.IdHRCompany, model.IdHREmployee),
                    //DDLDivision = await GetLongStringPairModel(PairModelTitle.Division, model.IdHRCompany),
                    //DDLCompany = await GetLongStringPairModel(PairModelTitle.HRCompany),
                    //DDLDesignation = await GetLongStringPairModel(PairModelTitle.Designation, model.IdHRCompany),
                    //DDLServiceType = await GetLongStringPairModel(PairModelTitle.ServiceType, model.IdHRCompany),
                    //DDLJobStatus = await GetIntStringPairModel(PairModelTitle.JobStatus),
                    //DDLHREmployeePositionStatusType = await GetIntStringPairModel(PairModelTitle.HREmployeePositionStatusType),
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _UpdateHREmployeeJobAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);

                var model = await JobHistoryDetails(id);

                //var model = await _hREmployeeJobHistoryServices.GetModelByIdAsync(id);

                model.ImplementationDateNp = model.ImplementationDate != null ? Date((DateTime)model.ImplementationDate).Nepdate : string.Empty;
                model.JoiningDateNp = Date(model.JoiningDate).Nepdate;
                model.ExpiryDateNP = model.ExpiryDate != null ? Date((DateTime)model.ExpiryDate).Nepdate : null;
                model.DecisionDateNP = model.DecisionDate == null ? string.Empty : Date((DateTime)model.DecisionDate).Nepdate;
                return PartialView(new HREmployeeJobHistoryViewModel
                {
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeJobHistory",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeJobHistory",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_UpdateHREmployeeJobAsync",
                    FormModelName = "HREmployeeJob",
                    ModalTitle = "कर्मचारी जागिर विवरण सम्पादन गर्नुहोस्",
                    CRUDAction = CRUDType.UPDATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Update,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = model,
                    IdHRCompany = model.IdHRCompany,
                    IdHREmployee = model.IdHREmployee,
                    //DDLEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeById, model.IdHRCompany, model.IdHREmployee),
                    //DDLDivision = await GetLongStringPairModel(PairModelTitle.Division, model.IdHRCompany),
                    //DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId,model.IdHRCompany),
                    //DDLDesignation = await GetLongStringPairModel(PairModelTitle.Designation, model.IdHRCompany),
                    //DDLServiceType = await GetLongStringPairModel(PairModelTitle.ServiceType, model.IdHRCompany),
                    //DDLJobStatus = await GetIntStringPairModel(PairModelTitle.JobStatus),
                    //DDLHREmployeePositionStatusType = await GetIntStringPairModel(PairModelTitle.HREmployeePositionStatusType),
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _DeleteHREmployeeJobAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await JobHistoryDetails(id);

                //var model = await _hREmployeeJobHistoryServices.GetModelByIdAsync(id);

                model.ImplementationDateNp = model.ImplementationDate != null ? Date((DateTime)model.ImplementationDate).Nepdate : string.Empty;
                model.JoiningDateNp = Date(model.JoiningDate).Nepdate;
                model.ExpiryDateNP = model.ExpiryDate != null ? Date((DateTime)model.ExpiryDate).Nepdate : null;
                model.DecisionDateNP = model.DecisionDate == null ? string.Empty : Date((DateTime)model.DecisionDate).Nepdate;
                return PartialView(new HREmployeeJobHistoryViewModel
                {
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeJobHistory",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeJobHistory",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    FormModelName = "HREmployeeJob",
                    ModalTitle = "कर्मचारी जागीर विवरण मेटाउनुहोस्",
                    BreadCrumbActionName = "_DeleteHREmployeeJobAsync",
                    CRUDAction = CRUDType.DELETE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Delete,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = model,
                    IdHREmployee = model.IdHREmployee,
                    IdHRCompany = model.IdHRCompany,
                    DDLEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeById, model.IdHRCompany, model.IdHREmployee),
                    DDLDivision = await GetLongStringPairModel(PairModelTitle.Division, model.IdHRCompany),
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.HRCompany),
                    DDLDesignation = await GetLongStringPairModel(PairModelTitle.Designation, model.IdHRCompany),
                    DDLServiceType = await GetLongStringPairModel(PairModelTitle.ServiceType, model.IdHRCompany),
                    DDLJobStatus = await GetIntStringPairModel(PairModelTitle.JobStatus),
                    DDLHREmployeePositionStatusType = await GetIntStringPairModel(PairModelTitle.HREmployeePositionStatusType)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ExportHREmployeeJobAsync(long? idHREmployee)
        {
            try
            {
                if (idHREmployee == null || idHREmployee == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var model = await _hREmployeeJobHistoryServices.FindAllAsync(Condition(idHREmployee));
                var data= model.Select(x => new
                {
                    Company= x.HRCompany.HRCompanyCode.HRCompanyCodeTitle,
                   EmployeeServiceType= x.HREmployeeServiceType.HREmployeeServiceTypeTitle,
                    CompanyDivisionName=x.HRCompanyDivision.HRCompanyDivisionName,
                   DesignationTitle= x.HRDesignation.HRDesignationTitle,
                    JoiningDate = x.JoiningDate.DateNp(),
                    HREmployeeJobStatus=  x.HREmployeeJobStatu.HREmployeeJobStatusTitle,
                    
                });
                return await ExportToExcel("EmployeeJobHistroy", data);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintHREmployeejobAsync(long? idHREmployee)
        {
            try
            {
                if (idHREmployee == null || idHREmployee == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var model = await _hREmployeeJobHistoryServices.FindAllAsync(Condition(idHREmployee));
                return PartialView(new HREmployeeJobHistoryViewModel
                {
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeJobHistory",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeJobHistory",
                    BreadCrumbActionName = "_PrintHREmployeejobAsync",
                    SessionDetails = SessionDetail,
                    FormModelName = "HREmployeeJob",
                    ModalTitle = "कर्मचारी पेशा विवरण छाप्नुहोस्",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    IdHREmployee = idHREmployee,
                    DBModelList = model
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
        public async Task<ActionResult> _CreateHREmployeeJobAsync(HREmployeeJobHistoryViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeJobAsync(viewModel, CRUDType.CREATE, formCollection);
        }

        [AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _UpdateHREmployeeJobAsync(HREmployeeJobHistoryViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeJobAsync(viewModel, CRUDType.UPDATE, formCollection);
        }

        [AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _DeleteHREmployeeJobAsync(HREmployeeJobHistoryViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeJobAsync(viewModel, CRUDType.DELETE, formCollection);
        }

        [NonAction]
        protected async Task<ActionResult> RETSourceHREmployeeJobAsync(HREmployeeJobHistoryViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
                viewModel.SessionDetails = SessionDetail;
                viewModel.BreadCrumbArea = "EmployeeManagement";
                viewModel.BreadCrumbController = "HREmployeeJobHistory";
                viewModel.BreadCrumbBaseURL = "EmployeeManagement/HREmployeeJobHistory";
                viewModel.DDLEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeById, viewModel.DataModel.IdHRCompany, viewModel.DataModel.IdHREmployee);
                viewModel.DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId, viewModel.DataModel.IdHRCompany);
                viewModel.DDLDesignation = await GetLongStringPairModel(PairModelTitle.Designation, viewModel.DataModel.IdHRCompany);
                viewModel.DDLDivision = await GetLongStringPairModel(PairModelTitle.Division, viewModel.DataModel.IdHRCompany);
                viewModel.DDLServiceType = await GetLongStringPairModel(PairModelTitle.ServiceType, viewModel.DataModel.IdHRCompany);
                viewModel.DDLJobStatus = await GetIntStringPairModel(PairModelTitle.JobStatus);
                viewModel.DDLHREmployeePositionStatusType = await GetIntStringPairModel(PairModelTitle.HREmployeePositionStatusType);
                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        viewModel.SubmitButtonText = SubmitButtonText.Create;
                        viewModel.BreadCrumbActionName = "_CreateHREmployeeJobAsync";
                        viewModel.ModalTitle = "कर्मचारी नयाँ जागीर विवरण थप्नुहोस्";
                        viewModel.FormModelName = "HREmployeeJob";
                        viewModel.CRUDAction = CRUDType.CREATE;
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.UPDATE:
                        viewModel.BreadCrumbActionName = "_UpdateHREmployeeJobAsync";
                        viewModel.ModalTitle = "कर्मचारी जागीर विवरण सम्पादन गर्नुहोस्";
                        viewModel.FormModelName = "HREmployeeJob";
                        viewModel.CRUDAction = CRUDType.UPDATE;
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.DELETE:
                        viewModel.BreadCrumbActionName = "_DeleteHREmployeeJobAsync";
                        viewModel.ModalTitle = "कर्मचारी जागीर विवरण मेटाउनुहोस्";
                        viewModel.FormModelName = "HREmployeeJob";
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
        protected async Task<ActionResult> CRUDHREmployeeJobAsync(HREmployeeJobHistoryViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceHREmployeeJobAsync(viewModel, cRUDType);
                }
                if (CRUDType.CREATE == cRUDType && await this._hREmployeeJobHistoryServices.Exits(x => x.IdHREmployee == viewModel.DataModel.IdHREmployee && x.ExpiryDate == null))
                {
                    Response.StatusCode = 350;
                    ModelState.AddModelError("", "नयाँ जागीर विवरण थप्न लाई रमाना मिती राख्नुहोस्");
                    return await RETSourceHREmployeeJobAsync(viewModel, cRUDType);
                }

                var InfoJObId=  GetCurrentCompany(viewModel.DataModel.IdHREmployee);
                if (InfoJObId > 0)
                {
                    if (InfoJObId != viewModel.DataModel.IdHRCompany)
                    {
                        await UpdateRoleToNormal(viewModel.DataModel.IdHREmployee, viewModel.DataModel.IdHRCompany);

                    }
                }


                //if (CRUDType.CREATE == cRUDType)
                //{
                //    _hREmployeeJobHistoryServices.SynchronizeDeviceData(viewModel.DataModel.IdHRCompany, viewModel.DataModel.IdHREmployee);
                //}

                viewModel.DataModel.JoiningDate = (DateTime)this.GetEnglishDate(viewModel.DataModel.JoiningDateNp);
                viewModel.DataModel.ExpiryDate = viewModel.DataModel.ExpiryDateNP == null ? viewModel.DataModel.ExpiryDate : (DateTime)this.GetEnglishDate(viewModel.DataModel.ExpiryDateNP);
                viewModel.DataModel.ImplementationDate = viewModel.DataModel.ImplementationDateNp == null ? viewModel.DataModel.ImplementationDate : (DateTime)this.GetEnglishDate(viewModel.DataModel.ImplementationDateNp);
                viewModel.DataModel.DecisionDate =  viewModel.DataModel.DecisionDateNP == null ? viewModel.DataModel.DecisionDate : (DateTime)this.GetEnglishDate(viewModel.DataModel.DecisionDateNP);

                if (viewModel.DataModel.ExpiryDate != null && (viewModel.DataModel.JoiningDate > viewModel.DataModel.ExpiryDate))
                {
                    Response.StatusCode = 350;
                    ModelState.AddModelError("", "उपस्थिति मिति रमाना मिती भन्दा सानो राख्नुहोस्");
                    return await RETSourceHREmployeeJobAsync(viewModel, cRUDType);
                }
             
               
                int jobCounter = await _hREmployeeJobHistoryServices.CountAsync(x => x.IdHREmployee == viewModel.DataModel.IdHREmployee && x.IdHRCompany == viewModel.DataModel.IdHRCompany);
                viewModel.DataModel.JobStatusCounter = jobCounter + 1;

        
                if (viewModel.DocumentUpload != null)
                {
                    var SubFolderName = viewModel.DataModel.IdHREmployee.ToString();
                    if (viewModel.DocumentUpload != null)
                    {
                        var FolderName = "EmployeeJobProfile\\" + SubFolderName + "\\";
                        var DocumentName = FileUpload(viewModel.DocumentUpload, FolderName);
                        viewModel.DataModel.DocumentName = DocumentName;
                    }
                }
                await _hREmployeeJobHistoryServices.CommitSaveChangesAsync(viewModel.DataModel, cRUDType);
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", "पुन: सम्पादन गर्नुहोश");
                return await RETSourceHREmployeeJobAsync(viewModel, cRUDType);
            }

          

            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListHREmployeeJobAsync", new { idHREmployee = viewModel.DataModel.IdHREmployee, pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }
        //Modiy 2020-05-01 Prem Prakash Dhakal
        public string FileUpload(HttpPostedFileBase File, string FolderName)
        {
            string serverPath = Server.MapPath("~");
            string DocName = File.FileName;
            string DocPath = serverPath + "Upload\\" + FolderName;
            var fileExt = Path.GetExtension(File.FileName);
            if (!(new DirectoryInfo(DocPath).Exists)) Directory.CreateDirectory(DocPath);
            var newDocName = GetFileName(DocPath, Path.GetFileNameWithoutExtension(DocName), fileExt);
            string targetPath = Path.Combine(DocPath, newDocName + fileExt);
            File.SaveAs(targetPath);
            return newDocName + fileExt;
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> DownloadDocumentAsync(long? id,string FileName)
        {
            try
            {
                //var model = await this._hREmployeeJobHistoryServices.GetByIdAsync(id);
                //string targetFolder = Server.MapPath("~");
                //string targetPath = Path.Combine(targetFolder, model.DocumentName);
                //string result = Path.GetFileName(targetPath);
                //byte[] fileBytes = System.IO.File.ReadAllBytes(targetPath);

                //Modiy 2020-05-01 Prem Prakash Dhakal
                string dirPath = Server.MapPath("~") + "Upload\\EmployeeJobProfile\\" + id + "\\" + FileName;
                string result = Path.GetFileName(dirPath);
                byte[] fileBytes = System.IO.File.ReadAllBytes(dirPath);
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, result);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Download", exp.Message, AlertNotificationType.error);
            }
        }




        #endregion


        #region NEWDATA
        public async Task<HREmployeeJobHistoryModel> JobHistoryDetails(long? id)
        {
            EAttendanceSystemDBEntities db = new EAttendanceSystemDBEntities();
            HREmployeeJobHistoryModel model = new HREmployeeJobHistoryModel();
            var data = db.HREmployeeJobHistories.Where(x => x.Id == id).Select(x => new HREmployeeJobHistoryModel
            {
            DecisionDate=x.DecisionDate,    
            DocumentName=x.DocumentName,
            ExpiryDate = x.ExpiryDate,
            Id=x.Id,
            IdHRCompany=x.IdHRCompany,
            IdHRCompanyDivision=x.IdHRCompanyDivision,
            IdHRDesignation=x.IdHRDesignation,
            IdHREmployee=x.IdHREmployee,
            IdHREmployeeJobStatus=x.IdHREmployeeJobStatus,
            IdHREmployeePositionStatusType = x.IdHREmployeePositionStatusType,
            IdHREmployeeServiceType=x.IdHREmployeeServiceType,
            ImplementationDate=x.ImplementationDate,
            IsCurrentStatus=x.IsCurrentStatus,
            IsHead=x.IsHead,
            FinalApprove = x.FinalApprove,
            JobStatusCounter=x.JobStatusCounter,
            JoiningDate=x.JoiningDate,
            }).FirstOrDefault();
            return data;
        }

        public async Task<HREmployeeModel> EmployeeDetails(long? id)
        {
            EAttendanceSystemDBEntities db = new EAttendanceSystemDBEntities();
            HREmployeeModel model = new HREmployeeModel();
            var data = db.HREmployee.Where(x => x.Id == id).Select(x => new HREmployeeModel
            {

                Id = x.Id,
                IdHRCompany=x.IdHRCompany,
                AppointmentDate=x.AppointmentDate,
                AppointmentDateNP=x.AppointmentDateNP,
                HREmployeeName=x.HREmployeeName,
                HREmployeeNameNP=x.HREmployeeNameNP,
                IdBloodGroup=x.IdBloodGroup,
                IdEnroll=x.IdEnroll,
                IdHREmployeeReligion=x.IdHREmployeeReligion,
                IdMarital=x.IdMarital,
                IdMotherTongue=x.IdMotherTongue,
                IdParent_HREmployee=x.IdParent_HREmployee,
                IdRelation=x.IdRelation,
                ImageName=x.ImageName,
                IsActive=x.IsActive,
                IsDisabled=x.IsDisabled,
                PISNumber=x.PISNumber,
                MobileNumber=x.MobileNumber,
                Remark=x.Remark,
                RetiredDate=x.RetiredDate,
                RetiredDateNP=x.RetiredDateNP,
                Status=x.Status,

            }).FirstOrDefault();

            return data;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<JsonResult> CompanyList(long IdHRCompany,long IdHREmployee,string crudType="")
        {
            try
            {
                ICollection<KeyValuePairModel<long, string>> DDLCompany = new List<KeyValuePairModel<long, string>>();

                if (crudType == "CREATE")
                {
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId);
                }
                else if(crudType == "READ")
                {
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.HRCompany);
                }
                else if (crudType =="UPDATE")
                {
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId, IdHRCompany);
                }
                else
                {
                    DDLCompany = DDLCompany;
                }
                var DDLDivision = await GetLongStringPairModel(PairModelTitle.Division, IdHRCompany);
                var DDLDesignation = await GetLongStringPairModel(PairModelTitle.Designation, IdHRCompany);
                var DDLServiceType = await GetLongStringPairModel(PairModelTitle.ServiceType, IdHRCompany);
           

              
                return Json(new { 
                  
                    dDLDivision = DDLDivision,
                    dDLCompany = DDLCompany,
                    dDLDesignation= DDLDesignation,
                    dDLServiceType = DDLServiceType,
                 
                }, JsonRequestBehavior.AllowGet);
            }

            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<JsonResult> CompanyEmployeeList(long IdHRCompany, long IdHREmployee)
        {
            try
            {
                var DDLEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeById, IdHRCompany, IdHREmployee);
                return Json(new
                {
                    dDLEmployee = DDLEmployee,

                }, JsonRequestBehavior.AllowGet);
            }

            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<JsonResult> BasicInfo(long IdHRCompany, long IdHREmployee)
        {
            try
            {
                var DDLJobStatus = await GetIntStringPairModel(PairModelTitle.JobStatus);
                var DDLHREmployeePositionStatusType = await GetIntStringPairModel(PairModelTitle.HREmployeePositionStatusType);
                return Json(new
                {
                    dDLJobStatus = DDLJobStatus,
                    dDLHREmployeePositionStatusType = DDLHREmployeePositionStatusType

                }, JsonRequestBehavior.AllowGet);
            }

            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion



        #region roleupDatE
        public async Task<bool> UpdateRoleToNormal(long? Idhremployee,long IdhrCompany)
        {
            var RoleId = NormalRole(IdhrCompany);
            await NewUpdateRole(Idhremployee, RoleId);
            return true;
        }



        public long GetCurrentCompany(long? IdHREmployee)
        {
            EAttendanceSystemDBEntities db = new EAttendanceSystemDBEntities();
            long CompanyID = 0;
            CompanyID = db.HREmployeeJobHistories.Any(x => x.IdHREmployee == IdHREmployee && x.IsCurrentStatus == true)==false?0: db.HREmployeeJobHistories.Where(x => x.IdHREmployee == IdHREmployee && x.IsCurrentStatus == true).Select(x=>x.IdHRCompany).FirstOrDefault();
            return CompanyID;
        }
        #endregion
    }
}