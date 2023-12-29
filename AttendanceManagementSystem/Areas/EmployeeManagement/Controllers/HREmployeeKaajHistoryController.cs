using AttendanceManagementSystem.App_Auth;
using AttendanceManagementSystem.Controllers;
using System;
using System.Configuration;
using System.IO;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SystemDatabase;
using SystemModels.EmployeeManagement;
using SystemModels.SystemSecurity;
using SystemServices.EmployeeManagement;
using SystemServices.SystemSecurity;
using SystemViewModels.EmployeeManagement;
using SystemViewModels.Shared;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;

namespace AttendanceManagementSystem.Areas.EmployeeManagement.Controllers
{
    public class HREmployeeKaajHistoryController : BaseController
    {
        // GET: EmployeeManagement/HREmployeeKaajHistory

        private readonly HREmployeeKaajHistoryServices _HREmployeeKaajHistoryServices;
        private readonly KaajAppliedSummaryServices _kaajAppliedSummaryServices;

        public HREmployeeKaajHistoryController()
        {
            this._HREmployeeKaajHistoryServices = new HREmployeeKaajHistoryServices(this._unitOfWork);
            this._kaajAppliedSummaryServices = new KaajAppliedSummaryServices(this._unitOfWork);
        }

        [NonAction]
        public Expression<Func<HREmployeeKaajHistory, bool>> Condition(long? id = null, string searchKey = "")
        {
            Expression<Func<HREmployeeKaajHistory, bool>> returnData = (x => false);
            switch (this.SessionDetail.IdRoleType)
            {
                case (int)RoleType.SuperUser:
                    return returnData = (x => (x.HREmployee1.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || x.HREmployee1.PISNumber.Contains(searchKey) || searchKey == ""));
                case (int)RoleType.Admin:
                    return returnData = (x => x.IdHREmployee == this.SessionDetail.IdHREmployee && (x.Id == id || id == null) && (x.HREmployee1.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || x.HREmployee1.PISNumber.Contains(searchKey) || searchKey == ""));
                case (int)RoleType.RootUser:
                    return returnData = (x => x.IdHREmployee == this.SessionDetail.IdHREmployee && (x.Id == id || id == null) && (x.HREmployee1.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || x.HREmployee1.PISNumber.Contains(searchKey) || searchKey == ""));
                case (int)RoleType.SectionAdmin:
                    return returnData = (x => x.IdHREmployee == this.SessionDetail.IdHREmployee && (x.Id == id || id == null) && (x.HREmployee1.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || x.HREmployee1.PISNumber.Contains(searchKey) || searchKey == ""));

                case (int)RoleType.NormalUser:
                    return returnData = (x => x.IdHREmployee == this.SessionDetail.IdHREmployee && (x.Id == id || id == null) && (x.HREmployee1.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || x.HREmployee1.PISNumber.Contains(searchKey) || searchKey == ""));
                default:
                    return returnData;
            }
        }

        [AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> Index(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, "KaajFromNP", "DESC");
                return View(new HREmployeeKaajHistoryViewModelList
                {
                    SessionDetails = SessionDetail,
                    DBModelList = await _HREmployeeKaajHistoryServices.GetPagedList(Condition(id), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeKaajHistory",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeKaajHistory",
                    BreadCrumbTitle = "काज/तालिम अनुरोध",
                    BreadCrumbActionName = "Index",
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
        public async Task<ActionResult> _ListHREmployeeKaajHistoryAsync(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, "KaajFromNP", "DESC");
                return PartialView(new HREmployeeKaajHistoryViewModelList
                {
                    SessionDetails = SessionDetail,
                    DBModelList = await _HREmployeeKaajHistoryServices.GetPagedList(Condition(null, searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeKaajHistory",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeKaajHistory",
                    BreadCrumbActionName = "_ListHREmployeeKaajHistoryAsync",
                    BreadCrumbTitle = "काज/तालिम अनुरोध",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _CreateHREmployeeKaajHistoryAsync(long? idHREmployee, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            var today = Today.Result;
            try
            {
                idHREmployee = idHREmployee == 0 ? this.SessionDetail.IdHREmployee : idHREmployee;
                var model = await _HREmployeeKaajHistoryServices.GetsEmployeeDetail(idHREmployee, SessionDetail.IdHRCompany);
                var lastDate = await GetLastDateKaaj(idHREmployee);
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HREmployeeKaajHistoryViewModel
                {
                    SessionDetails = SessionDetail,
                    EmployeeDetail = model,
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeKaajHistory",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeKaajHistory",
                    BreadCrumbActionName = "_CreateHREmployeeKaajHistoryAsync",
                    FormModelName = "HREmployeeKaajHistory",
                    ModalTitle = "नया काज/तालिम थप्नुहोस्",
                    CRUDAction = CRUDType.CREATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Create,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    IdHREmployee = model.Id,
                    DDLApprovalEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeApproved, SessionDetail.IdHRCompany, idHREmployee),
                    DBModelPagedList = await _HREmployeeKaajHistoryServices.GetPagedList((x => x.IdHREmployee == idHREmployee && x.KaajYearNP == today.NepYear), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    DDLVehicleType = await GetStringStringPairModel(PairModelTitle.VehicleType),
                    DDLKaajType = await this.GetIntStringPairModel(PairModelTitle.KaajType),
                    DDLCountry = await this.GetIntStringPairModel(PairModelTitle.Country),
                    DDLYearNp = await this.GetIntStringPairModel(PairModelTitle.Year),
                    DataModel = new HREmployeeKaajHistoryModel
                    {
                        FiscalYear = today.FiscalYear,
                        IdCountry = 1,
                        IdKaajType = 1,
                        KaajYearNP = this.Today.Result.NepYear,
                        IdHREmployee = model.Id,
                        IdHrCompany = (long)model.IdHRCompany,
                        IdJob = model.IdJob.Value,
                        IdKaajStatus = 2,
                        KaajFromNP = today.Nepdate,
                        EmployeeName = model.HREmployeeNameNP,
                        IsActive = true
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
        public async Task<ActionResult> _ReadHREmployeeKaajHistoryAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var DataModel = await _HREmployeeKaajHistoryServices.GetModelByIdAsync(id);
                var model = await _HREmployeeKaajHistoryServices.GetsEmployeeDetail(DataModel.IdHREmployee, DataModel.IdHrCompany);
                var selectedList = DataModel.VehicleUsedInKaaj == null ? (new string[] { }) : DataModel.VehicleUsedInKaaj.Split(',');
                DataModel.EmployeeName = model.HREmployeeName;
                return PartialView(new HREmployeeKaajHistoryViewModel
                {
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeKaajHistory",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeKaajHistory",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_ReadHREmployeeKaajHistoryAsync",
                    FormModelName = "HREmployeeKaajHistory",
                    ModalTitle = "काज/तालिमको विवरण",
                    BreadCrumbTitle = "HREmployeeKaajHistory",
                    CRUDAction = CRUDType.READ,
                    OnlyCancelButton = true,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLApprovalEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeApproved, DataModel.IdHrCompany, DataModel.IdHREmployee),
                    DBModelPagedList = await _HREmployeeKaajHistoryServices.GetPagedList((x => x.IdHREmployee == DataModel.IdHREmployee && x.KaajYearNP == DataModel.KaajYearNP), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    DDLVehicleType = await GetStringStringPairModel(PairModelTitle.VehicleType),
                    ddlKajVehicalSelected = selectedList,
                    DataModel = DataModel,
                    EmployeeDetail = model,
                    DDLKaajType = await this.GetIntStringPairModel(PairModelTitle.KaajType),
                    DDLCountry = await this.GetIntStringPairModel(PairModelTitle.Country),
                    DDLYearNp = await this.GetIntStringPairModel(PairModelTitle.Year)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _UpdateHREmployeeKaajHistoryAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var DataModel = await _HREmployeeKaajHistoryServices.GetModelByIdAsync(id);
                var model = await _HREmployeeKaajHistoryServices.GetsEmployeeDetail(DataModel.IdHREmployee, DataModel.IdHrCompany);
                var selectedList = DataModel.VehicleUsedInKaaj == null ? (new string[] { }) : DataModel.VehicleUsedInKaaj.Split(',');
                DataModel.EmployeeName = model.HREmployeeName;
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HREmployeeKaajHistoryViewModel
                {
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeKaajHistory",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeKaajHistory",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_UpdateHREmployeeKaajHistoryAsync",
                    FormModelName = "HREmployeeKaajHistory",
                    ModalTitle = "काज/तालिम सम्पादन गर्नुहोस्",
                    BreadCrumbTitle = "HREmployeeKaajHistory",
                    CRUDAction = CRUDType.UPDATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Update,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLApprovalEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeApproved, DataModel.IdHrCompany, DataModel.IdHREmployee),
                    DBModelPagedList = await _HREmployeeKaajHistoryServices.GetPagedList((x => x.IdHREmployee == DataModel.IdHREmployee && x.KaajYearNP == DataModel.KaajYearNP), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    DDLVehicleType = await GetStringStringPairModel(PairModelTitle.VehicleType),
                    ddlKajVehicalSelected = selectedList,
                    DataModel = DataModel,
                    EmployeeDetail = model,
                    DDLKaajType = await this.GetIntStringPairModel(PairModelTitle.KaajType),
                    DDLCountry = await this.GetIntStringPairModel(PairModelTitle.Country),
                    DDLYearNp = await this.GetIntStringPairModel(PairModelTitle.Year)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _DeleteHREmployeeKaajHistoryAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var DataModel = await _HREmployeeKaajHistoryServices.GetModelByIdAsync(id);
                var model = await _HREmployeeKaajHistoryServices.GetsEmployeeDetail(DataModel.IdHREmployee, DataModel.IdHrCompany);
                var selectedList = DataModel.VehicleUsedInKaaj == null ? (new string[] { }) : DataModel.VehicleUsedInKaaj.Split(',');
                return PartialView(new HREmployeeKaajHistoryViewModel
                {
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeKaajHistory",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeKaajHistory",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    FormModelName = "HREmployeeKaajHistory",
                    ModalTitle = "काज/तालिम मेटाउनुहोस्",
                    BreadCrumbActionName = "_DeleteHREmployeeKaajHistoryAsync",
                    CRUDAction = CRUDType.DELETE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Delete,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLApprovalEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeApproved, DataModel.IdHrCompany, DataModel.IdHREmployee),
                    DBModelPagedList = await _HREmployeeKaajHistoryServices.GetPagedList((x => x.IdHREmployee == DataModel.IdHREmployee && x.KaajYearNP == DataModel.KaajYearNP), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    DDLVehicleType = await GetStringStringPairModel(PairModelTitle.VehicleType),
                    ddlKajVehicalSelected = selectedList,
                    DataModel = DataModel,
                    EmployeeDetail = model,
                    DDLKaajType = await this.GetIntStringPairModel(PairModelTitle.KaajType),
                    DDLCountry = await this.GetIntStringPairModel(PairModelTitle.Country),
                    DDLYearNp = await this.GetIntStringPairModel(PairModelTitle.Year)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ExportHREmployeeKaajHistoryAsync()
        {
            try
            {
                var model = await _HREmployeeKaajHistoryServices.FindAllAsync(Condition());
                return await ExportToExcel("HREmployeeKaajHistory", model);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintHREmployeeKaajHistoryAsync()
        {
            try
            {
                return PartialView(new HREmployeeKaajHistoryViewModel
                {
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeKaajHistory",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeKaajHistory",
                    BreadCrumbActionName = "_PrintHREmployeeKaajHistoryAsync",
                    FormModelName = "HREmployeeKaajHistory",
                    ModalTitle = "काज/तालिम सुची छाप्नुहोस्",
                    BreadCrumbTitle = "HREmployeeKaajHistory",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBModelList = await _HREmployeeKaajHistoryServices.FindAllAsync(Condition())
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]

        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _CreateHREmployeeKaajHistoryAsync(HREmployeeKaajHistoryViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeKaajHistory(viewModel, CRUDType.CREATE, formCollection);
        }

        [AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _UpdateHREmployeeKaajHistoryAsync(HREmployeeKaajHistoryViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeKaajHistory(viewModel, CRUDType.UPDATE, formCollection);
        }

        [AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _DeleteHREmployeeKaajHistoryAsync(HREmployeeKaajHistoryViewModel viewModel, FormCollection formCollection)
        {
            return await this.CRUDHREmployeeKaajHistory(viewModel, CRUDType.DELETE, formCollection);
        }

        [NonAction]
        protected async Task<ActionResult> RETSourceHREmployeeKaajHistory(HREmployeeKaajHistoryViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
                var pagination = Get_PaginationValue(viewModel.PageNumber, viewModel.PageSize, viewModel.OrderingBy, viewModel.OrderingDirection);
                viewModel.SessionDetails = SessionDetail;
                viewModel.BreadCrumbArea = "EmployeeManagement";
                viewModel.BreadCrumbController = "HREmployeeKaajHistory";
                viewModel.BreadCrumbBaseURL = "EmployeeManagement/HREmployeeKaajHistory";
                viewModel.DDLApprovalEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeApproved, viewModel.DataModel.IdHrCompany, viewModel.DataModel.IdHREmployee);
                viewModel.DDLVehicleType = await GetStringStringPairModel(PairModelTitle.VehicleType);
                var selectedList = viewModel.DataModel.VehicleUsedInKaaj == null ? (new string[] { }) : viewModel.DataModel.VehicleUsedInKaaj.Split(',');
                viewModel.ddlKajVehicalSelected = selectedList;
                viewModel.DBModelPagedList = await _HREmployeeKaajHistoryServices.GetPagedList((x => x.IdHREmployee == viewModel.DataModel.IdHREmployee && x.KaajYearNP == viewModel.DataModel.KaajYearNP), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection);
                viewModel.DDLKaajType = await this.GetIntStringPairModel(PairModelTitle.KaajType);
                viewModel.DDLCountry = await this.GetIntStringPairModel(PairModelTitle.Country);
                viewModel.DDLYearNp = await this.GetIntStringPairModel(PairModelTitle.Year);
                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        viewModel.SubmitButtonText = SubmitButtonText.Create;
                        viewModel.BreadCrumbActionName = "_CreateHREmployeeKaajHistoryAsync";
                        viewModel.ModalTitle = "नया काज/तालिम थप्नुहोस्";
                        viewModel.FormModelName = "HREmployeeKaajHistory";
                        viewModel.CRUDAction = CRUDType.CREATE;
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);

                    case CRUDType.UPDATE:
                        viewModel.BreadCrumbActionName = "_UpdateHREmployeeKaajHistoryAsync";
                        viewModel.ModalTitle = "काज/तालिम सम्पादन गर्नुहोस्";
                        viewModel.FormModelName = "HREmployeeKaajHistory";
                        viewModel.CRUDAction = CRUDType.UPDATE;
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);

                    case CRUDType.DELETE:
                        viewModel.BreadCrumbActionName = "_DeleteHREmployeeKaajHistoryAsync";
                        viewModel.ModalTitle = "काज/तालिम मेटाउनुहोस्";
                        viewModel.FormModelName = "HREmployeeKaajHistory";
                        viewModel.CRUDAction = CRUDType.DELETE;
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                }
                return null;
            }
            catch (Exception exp)
            {
                throw new ArgumentException(exp.Message);
            }
        }

        [NonAction]
        protected async Task<ActionResult> CRUDHREmployeeKaajHistory(HREmployeeKaajHistoryViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {

                if (CRUDType.DELETE == cRUDType)
                {
                    await this._HREmployeeKaajHistoryServices.DeleteByIdAsync(viewModel.DataModel.Id);
                    await _HREmployeeKaajHistoryServices.UnitOfWork.SaveAsync();
                }

                else if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceHREmployeeKaajHistory(viewModel, cRUDType);
                }

                else
                {
                    if (viewModel.ddlKajVehicalSelected == null)
                    {
                        Response.StatusCode = 350;
                        ModelState.AddModelError("", "सवारी साधन चयन गर्नुहोस्");
                        ModelState.AddModelError("DataModel.VehicleUsedInKaaj", "सवारी साधन चयन गर्नुहोस्");
                        return await RETSourceHREmployeeKaajHistory(viewModel, cRUDType);
                    }

                    viewModel.DataModel.KaajFromDate = this.GetEnglishDate(viewModel.DataModel.KaajFromNP);
                    viewModel.DataModel.KaajToDate = this.GetEnglishDate(viewModel.DataModel.KaajToNP);

                    if (viewModel.DataModel.KaajFromDate > viewModel.DataModel.KaajToDate)
                    {
                        Response.StatusCode = 350;
                        ModelState.AddModelError("", GetErrorMessage(UserErrorMessage.FromToGreaterThanToDate));
                        return await RETSourceHREmployeeKaajHistory(viewModel, cRUDType);
                    }

                    //Check Existing data
                    if (cRUDType != CRUDType.DELETE && await this._HREmployeeKaajHistoryServices.CheckKaajTrainingExistance(viewModel.DataModel.IdHREmployee, (DateTime)viewModel.DataModel.KaajFromDate, (DateTime)viewModel.DataModel.KaajToDate))
                    {
                        Response.StatusCode = 350;
                        ModelState.AddModelError("", $"यो मितिमा काज/तालिम लिइसक्नु भएको छ");
                        return await RETSourceHREmployeeKaajHistory(viewModel, cRUDType);
                    }
                     if (viewModel.FormDocument != null)
                    {
                        var SubFolderName = viewModel.DataModel.IdHREmployee.ToString();
                        if (viewModel.FormDocument != null)
                        {
                            var FolderName = "EmployeeKaajProfile\\" + SubFolderName + "\\";
                            var DocumentName = FileUpload(viewModel.FormDocument, FolderName);
                            viewModel.DataModel.Document = DocumentName;
                        }
                    }
                    viewModel.DataModel.IdKaajStatus = (this.SessionDetail.IdRoleType == (int)RoleType.Admin || this.SessionDetail.IdRoleType == (int)RoleType.SectionAdmin) ? (this.SessionDetail.IdHREmployee == viewModel.DataModel.IdHREmployee ? 2 : 3) : 2;
                    viewModel.DataModel.VehicleUsedInKaaj = string.Join(",", viewModel.ddlKajVehicalSelected);
                    await _HREmployeeKaajHistoryServices.CommitAsync(viewModel.DataModel, cRUDType);
                }

            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", "पुन: काज राख्नुहोश");
                return await RETSourceHREmployeeKaajHistory(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListHREmployeeKaajHistoryAsync", new { pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }
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
        public async Task<ActionResult> DownloadDocumentAsync(long? id, string FileName)
        {
            try
            {
                //var model = await this._HREmployeeKaajHistoryServices.GetByIdAsync(id);
                //string targetFolder = Server.MapPath("~");
                //string targetPath = Path.Combine(targetFolder, model.Document);
                //string result = Path.GetFileName(targetPath);
                //byte[] fileBytes = System.IO.File.ReadAllBytes(targetPath);
                //return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, result);
               
                //Modiy 2020-05-01 Prem Prakash Dhakal
                string dirPath = Server.MapPath("~") + "Upload\\EmployeeKaajProfile\\" + id + "\\" + FileName;
                string result = Path.GetFileName(dirPath);
                byte[] fileBytes = System.IO.File.ReadAllBytes(dirPath);
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, result);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Download", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _GetHREmployeeKaajSummaryAsync(long? idHREmployee, string dateNP)
        {
            try
            {
                int yearNP = this.GetYearFromNepaliDate(dateNP);
                var pagination = Get_PaginationValue(1, 20, "KaajFromNP", "DESC");
                return PartialView(new HREmployeeKaajHistoryViewModel
                {
                    DBModelPagedList = await _HREmployeeKaajHistoryServices.GetPagedList((x => x.IdHREmployee == idHREmployee && x.KaajYearNP == yearNP), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = string.Empty
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintIndividualHREmployeeKaajHistoryAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var DataModel = await _HREmployeeKaajHistoryServices.GetModelByIdAsync(id);
                var model = await _HREmployeeKaajHistoryServices.GetsEmployeeDetail(DataModel.IdHREmployee, DataModel.IdHrCompany);
                var selectedList = DataModel.VehicleUsedInKaaj == null ? (new string[] { }) : DataModel.VehicleUsedInKaaj.Split(',');
                DataModel.EmployeeName = model.HREmployeeName;
                var ApprovalName = GetEmployeeNameById(DataModel.IdApprovedBy);
                var information = await GetCompanyHeaderDetails(model.IdHRCompany);
                string CompanyNameNP = information.CompanyNameNP;
                string ParentCompanyNameNP = information.ParentCompanyNameNP;
                return PartialView(new HREmployeeKaajHistoryViewModel
                {
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeKaajHistory",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeKaajHistory",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_PrintIndividualHREmployeeKaajHistoryAsync",
                    FormModelName = "HREmployeeKaajHistory",
                    ModalTitle = "काजको विवरण",
                    BreadCrumbTitle = "HREmployeeKaajHistory",
                    CRUDAction = CRUDType.SEARCH,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLApprovalEmployee = await GetLongStringPairModel(PairModelTitle.EmployeeApproved, DataModel.IdHrCompany, DataModel.IdHREmployee),
                    DBModelPagedList = await _HREmployeeKaajHistoryServices.GetPagedList((x => x.IdHREmployee == DataModel.IdHREmployee && x.KaajYearNP == DataModel.KaajYearNP), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    DDLVehicleType = await GetStringStringPairModel(PairModelTitle.VehicleType),
                    ddlKajVehicalSelected = selectedList,
                    DataModel = DataModel,
                    EmployeeDetail = model,
                    DDLKaajType = await this.GetIntStringPairModel(PairModelTitle.KaajType),
                    DDLCountry = await this.GetIntStringPairModel(PairModelTitle.Country),
                    DDLYearNp = await this.GetIntStringPairModel(PairModelTitle.Year),
                    ApprovedBy = ApprovalName,
                    Header = new ReportHeaderViewModel
                    {
                        CompanyName = CompanyNameNP,
                        ParentCompanyName = ParentCompanyNameNP,
                        DivisionName = SessionDetail.IdHRCompanyDivision.ToString(),
                        ReportName = "कार्यालय कोड नं. "
                    },
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

    }
}