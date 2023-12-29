using AttendanceManagementSystem.App_Auth;
using AttendanceManagementSystem.Controllers;
using System;
using System.IO;
using System.Linq;
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
using SystemViewModels.Shared;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;
namespace AttendanceManagementSystem.Areas.EmployeeManagement.Controllers
{
    public class HREmployeeManagementController : BaseController
    {
        private readonly LoginsServices _loginsServices;
        private readonly HREmployeeServices _hREmployeeservices;
        private readonly MembershipProviderServices _MembershipProviderServices;
        public HREmployeeManagementController()
        {
            this._loginsServices = new LoginsServices(this._unitOfWork);
            this._hREmployeeservices = new HREmployeeServices(this._unitOfWork);
            this._MembershipProviderServices = new MembershipProviderServices(this._unitOfWork);
            //this._hREmployeeNationalIdentityServices = new HREmployeeNationalIdentityServices(this._unitOfWork);
        }
        #region Main Page
        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> Index(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            try
            {
                string ordering = this.SessionDetail.IdRoleType == 0 ? "IdHRCompany" : "HRDesignationOrder";
                var pagination = Get_PaginationValue(pageNumber, pageSize, ordering, "ASC");
                var model = await _hREmployeeservices.GetPagedList(this.SessionDetail.IdHRCompany, this.SessionDetail.IdHREmployee, this.SessionDetail.IdRoleType, 0, pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, "");
                var hrcompanyModel = await GetLongStringPairModel(PairModelTitle.HRCompanyOnly);
                return View(new HREmployeeViewModelList
                {
                    SessionDetails = this.SessionDetail,
                    DBModelEmp = model,
                    BreadCrumbTitle = "कर्मचारी व्यवस्थापन",
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeManagement",
                    BreadCrumbActionName = "Index",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeManagement",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    DDLCompany = hrcompanyModel,
                    IdHRCompany = SessionDetail.IdHRCompany,
                    IdActiveEmployee = 0,
                    
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _Tabbed(long? idHREmployee, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HREmployeeViewModelList
                {
                    SessionDetails = this.SessionDetail,
                    IdHREmployee = idHREmployee,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingDirection,
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
        public async Task<ActionResult> EmployeeProfileAsync(long? idHREmployee, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            try
            {
                var IdroleType = SessionDetail.IdRoleType;
                var CurrentId = SessionDetail.IdHREmployee;
                var idcompany = SessionDetail.IdHRCompany;
                var IdHRCompanyDivision = SessionDetail.IdHRCompanyDivision;

                if (IdroleType>0)
                {
                    var check = Permission(CurrentId, idHREmployee, IdroleType, idcompany, IdHRCompanyDivision);

                    if (!check)
                    {
                        return PartialView("~/Views/Shared/Error.cshtml");
                    }
                }
             

                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _hREmployeeservices.GetByIdAsync(idHREmployee);
                model.ImageName = model.ImageName == null ? "/images/avatars/avatar7.png" : model.ImageName;
                return View(new HREmployeeViewModelList
                {
                    SessionDetails = this.SessionDetail,
                    BreadCrumbTitle = "कर्मचारी विवरण",
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeManagement",
                    BreadCrumbActionName = "EmployeeProfileAsync",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeManagement",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    IdHREmployee = idHREmployee,
                    DBModel = model
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        //autocomplete search
        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> EmployeeProfileSearchAsync(long? idHREmployee, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _hREmployeeservices.GetByIdAsync(idHREmployee);
                model.ImageName = model.ImageName == null ? "/images/avatars/avatar7.png" : model.ImageName;
                return View(new HREmployeeViewModelList
                {
                    SessionDetails = this.SessionDetail,
                    BreadCrumbTitle = "कर्मचारी विवरण",
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeManagement",
                    BreadCrumbActionName = "EmployeeProfileAsync",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeManagement",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    IdHREmployee = idHREmployee,
                    DBModel = model
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }
        #endregion
        #region HREmployee
        [NonAction]
        public Expression<Func<HREmployeeJobHistory, bool>> Condition(string searchKey = "")
        {
            Expression<Func<HREmployeeJobHistory, bool>> returnData = (x => false);
            switch (this.SessionDetail.IdRoleType)
            {
                case (int)RoleType.SuperUser:
                    return returnData = (x => (x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || x.HRCompany.CompanyName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
                case (int)RoleType.Admin:
                    return returnData = (x => x.IdHRCompany == SessionDetail.IdHRCompany && (x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
                case (int)RoleType.RootUser:
                    return returnData = (x => x.IdHRCompany == SessionDetail.IdHRCompany && x.IdHRCompanyDivision == this.SessionDetail.IdHRCompanyDivision && (x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
                case (int)RoleType.NormalUser:
                    return returnData = (x => x.IdHRCompany == SessionDetail.IdHRCompany && x.Id == SessionDetail.IdHREmployee && (x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));
                case (int)RoleType.SectionAdmin:
                    return returnData = (x => x.IdHRCompany == SessionDetail.IdHRCompany && x.IdHRCompanyDivision == this.SessionDetail.IdHRCompanyDivision && (x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));

                default:
                    return returnData;
            }
        }


        [AuthorizeUser(ActionName = "EmployeeProfileAsync")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListWrapperHREmployeeAsync(long? idHREmployee, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _hREmployeeservices.GetByIdAsync(idHREmployee);
                return PartialView(new HREmployeeViewModelList
                {
                    SessionDetails = this.SessionDetail,
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeManagement",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeManagement",
                    BreadCrumbActionName = "_ListHREmployeeAsync",
                    BreadCrumbTitle = "कर्मचारी",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    IdHREmployee = idHREmployee,
                    IdHRCompany = model.IdHRCompany,
                    DBModel = model,
                    IdActiveEmployee = 0,
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }


        [AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListHREmployeeAsync(long? idHRCompany, int? active, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                string ordering = this.SessionDetail.IdRoleType == 0 ? "IdHRCompany" : "HRDesignationOrder";
                var pagination = Get_PaginationValue(pageNumber, pageSize, ordering, "ASC");
                return PartialView(new HREmployeeViewModelList
                {
                    SessionDetails = this.SessionDetail,
                    DBModelEmp = await _hREmployeeservices.GetPagedList(idHRCompany, this.SessionDetail.IdHREmployee, this.SessionDetail.IdRoleType, active, pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, searchKey),
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeManagement",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeManagement",
                    BreadCrumbActionName = "_ListHREmployeeAsync",
                    BreadCrumbTitle = "कर्मचारी",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    IdHRCompany = idHRCompany,
                    IdActiveEmployee = active,
                    
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }


        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _CreateHREmployeeAsync(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            try
            {
                string ordering = this.SessionDetail.IdRoleType == 0 ? "IdHRCompany" : "HRDesignationOrder";
                var pagination = Get_PaginationValue(pageNumber, pageSize, ordering, "ASC");
                Guid IdLogin = Guid.NewGuid();
                return PartialView(new HREmployeeViewModel
                {
                    SessionDetails = this.SessionDetail,
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeManagement",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeManagement",
                    BreadCrumbActionName = "_CreateHREmployeeAsync",
                    FormModelName = "HREmployee",
                    ModalTitle = "नयाँ कर्मचारीको बिवरण थप्नुहोस्",
                    BreadCrumbTitle = "कर्मचारी",
                    CRUDAction = CRUDType.CREATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Create,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.HRCompany),
                    DDLBloodGroup = await GetIntStringPairModel(PairModelTitle.BloodGroup),
                    DDLMaritalStatus = await GetLongStringPairModel(PairModelTitle.MaritalStatus),
                    DDLGender = await GetLongStringPairModel(PairModelTitle.Gender),
                    DDLLanguage = await GetLongStringPairModel(PairModelTitle.HRLanguage),
                    //DDLNationalIdentityType = await GetIntStringPairModel(PairModelTitle.NationalIdentityType),
                    //DDLDistrict = await GetLongStringPairModel(PairModelTitle.District),
                    DDLRole = await GetIntStringPairModel(PairModelTitle.Role),
                    DDLReligion = await GetIntStringPairModel(PairModelTitle.Religion),
                    DataModel = new HREmployeeModel { Id = 0, ImageName = "/images/avatars/avatar7.png" },
                    DataModelLogin = new LoginsModel { Id = IdLogin, IsActive = true, IsEmailConfirmed = false, TwoFactorEnabled = false, LockOutEnabled = false, AccessFailedCount = 0 },
                    DataModelMembership = new MembershipProviderModel { Id = 0, IdLogin = IdLogin, IdHREmployee = 0 },
                    //DataModelNIdentity = new HREmployeeNationalIdentityModel { Id = 0, IssueDate = DateTime.Now.Date, ExpiryDate = DateTime.Now.Date, IsDefault = true },
                    DDLSystemLanguage = await GetIntStringPairModel(PairModelTitle.SystemLanguage, SessionDetail.IdHRCompany),
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
        public async Task<ActionResult> _UpdateHREmployeeAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey,int status)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return await this.AlertNotification("Error", "Id Should not be null or empty", AlertNotificationType.error);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                
                //var model = await _hREmployeeservices.GetModelByIdAsync(id);
                var model = await EmployeeInformation(id);

                var membershipModel = await _MembershipProviderServices.FindAsync(x => x.IdHREmployee == model.Id);
                model.UserName = membershipModel == null ? "" : membershipModel.Login.UserName;
                model.SystemLanguage = membershipModel == null ? "1" : membershipModel.Login.Language;
                // var modelNIdentity = await _hREmployeeNationalIdentityServices.GetModelFindAsync(x => x.IdHREmployee == model.Id);
                var ddlHRCompany = await GetLongStringPairModel(PairModelTitle.CompanyId, model.IdHRCompany);
                model.ImageName = model.ImageName == null ? "images/avatars/avatar7.png" : model.ImageName;
                model.Document = model.Document == null ? "images/avatars/avatar.png" : model.Document;
                return PartialView(new HREmployeeViewModel
                {
                    SessionDetails = this.SessionDetail,
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeManagement",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeManagement",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_UpdateHREmployeeAsync",
                    FormModelName = "HREmployee",
                    ModalTitle = "कर्मचारीको बिवरण सम्पादन गर्नुहोस्",
                    BreadCrumbTitle = "कर्मचारी",
                    CRUDAction = CRUDType.UPDATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Update,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = model,
                    DataModelMembership = new MembershipProviderModel { Id = membershipModel == null ? 0 : membershipModel.Id, IdHREmployee = (long)id, IdHREmployeeRole = membershipModel == null ? 0 : membershipModel.IdHREmployeeRole },
                    //DataModelNIdentity = modelNIdentity ?? new HREmployeeNationalIdentityModel { Id = 0 },
                    DDLCompany = ddlHRCompany,
                    DDLBloodGroup = await GetIntStringPairModel(PairModelTitle.BloodGroup),
                    DDLMaritalStatus = await GetLongStringPairModel(PairModelTitle.MaritalStatus),
                    DDLGender = await GetLongStringPairModel(PairModelTitle.Gender),
                    DDLLanguage = await GetLongStringPairModel(PairModelTitle.HRLanguage),
                    DDLNationalIdentityType = await GetIntStringPairModel(PairModelTitle.NationalIdentityType),
                    //DDLDistrict = await GetLongStringPairModel(PairModelTitle.District),
                    DDLRole = await GetIntStringPairModel(PairModelTitle.Role, model.IdHRCompany),
                    DDLSystemLanguage = await GetIntStringPairModel(PairModelTitle.SystemLanguage),
                    DDLReligion = await GetIntStringPairModel(PairModelTitle.Religion),
                    IdHRCompany = model.IdHRCompany,
                    IdHREmployee = model.Id,
                    IdActiveEmployee=status
                    
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _DeleteHREmployeeAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _hREmployeeservices.GetModelByIdAsync(id);
                return PartialView(new HREmployeeViewModel
                {
                    SessionDetails = this.SessionDetail,
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeManagement",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeManagement",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    FormModelName = "HREmployee",
                    ModalTitle = "कर्मचारीको बिवरण मेटाउनुहोस्",
                    BreadCrumbTitle = "कर्मचारी",
                    BreadCrumbActionName = "_DeleteHREmployeeAsync",
                    CRUDAction = CRUDType.DELETE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Delete,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = model,
                    IdHRCompany = model.IdHRCompany
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ExportHREmployeeAsync()
        {
            try
            {
                var model = await _hREmployeeservices.FindAllAsync(x => x.IsActive);
                return await ExportToExcel("HREmployee", model);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintHREmployeeAsync(int? status)
        {
            var idHRCompany = this.SessionDetail.IdHRCompany;

            var information = await GetCompanyHeaderDetails(idHRCompany);
            string CompanyNameNP = information.CompanyNameNP;
            string ParentCompanyNameNP = information.ParentCompanyNameNP;

            try
            {
                return PartialView(new HREmployeeViewModel
                {
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HREmployeeManagement",
                    BreadCrumbBaseURL = "EmployeeManagement/HREmployeeManagement",
                    BreadCrumbActionName = "_PrintHREmployeeAsync",
                    FormModelName = "HREmployee",
                    ModalTitle = "कर्मचारी सूची छाप्नुहोस्",
                    BreadCrumbTitle = "कर्मचारी",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBModelEmp = await _hREmployeeservices.Gets(this.SessionDetail.IdHRCompany, this.SessionDetail.IdHREmployee, this.SessionDetail.IdRoleType,status),
                    Header = new ReportHeaderViewModel
                    {
                        CompanyName = CompanyNameNP,
                        ParentCompanyName = ParentCompanyNameNP,
                        DivisionName = SessionDetail.IdHRCompanyDivision.ToString(),
                        ReportName = $"{Today.Result.Nepdate} को निजामती सेवाका कर्मचारीहरुको सूची"
                    }
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<JsonResult> _CheckUserNameAsync(string UserName)
        {
            try
            {
                var model = await _hREmployeeservices.CheckUserNameAsync(UserName);
                if (!model)
                    return Json("Available", JsonRequestBehavior.AllowGet);
                else
                    return Json(GetErrorMessage(UserErrorMessage.USERNAMEEXIST), JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _CreateHREmployeeAsync(HREmployeeViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployee(viewModel, CRUDType.CREATE, formCollection);
        }

        [AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _UpdateHREmployeeAsync(HREmployeeViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployee(viewModel, CRUDType.UPDATE, formCollection);
        }

        [AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _DeleteHREmployeeAsync(HREmployeeViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployee(viewModel, CRUDType.DELETE, formCollection);
        }

        [NonAction]
        protected async Task<ActionResult> RETSourceHREmployee(HREmployeeViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
                viewModel.DataModel.ImageName = viewModel.ImageProfile == null ? "images/avatars/avatar7.png" : viewModel.ImageProfile.FileName;
                // viewModel.DataModel.Document = viewModel.NationalIdentity == null ? null : viewModel.NationalIdentity.FileName;
                viewModel.SessionDetails = this.SessionDetail;
                viewModel.BreadCrumbArea = "EmployeeManagement";
                viewModel.BreadCrumbController = "HREmployeeManagement";
                viewModel.BreadCrumbBaseURL = "EmployeeManagement/HREmployeeManagement";
                viewModel.IdHRCompany = viewModel.DataModel.IdHRCompany;
                viewModel.DDLCompany = await GetLongStringPairModel(PairModelTitle.HRCompany);
                viewModel.DDLBloodGroup = await GetIntStringPairModel(PairModelTitle.BloodGroup);
                viewModel.DDLMaritalStatus = await GetLongStringPairModel(PairModelTitle.MaritalStatus);
                viewModel.DDLGender = await GetLongStringPairModel(PairModelTitle.Gender);
                viewModel.DDLLanguage = await GetLongStringPairModel(PairModelTitle.HRLanguage);
                //viewModel.DDLNationalIdentityType = await GetIntStringPairModel(PairModelTitle.NationalIdentityType);
                // viewModel.DDLDistrict = await GetLongStringPairModel(PairModelTitle.District);
                viewModel.DDLRole = await GetIntStringPairModel(PairModelTitle.Role, viewModel.DataModel.IdHRCompany);
                viewModel.DDLSystemLanguage = await GetIntStringPairModel(PairModelTitle.SystemLanguage, viewModel.DataModel.IdHRCompany);
                viewModel.DDLReligion = await GetIntStringPairModel(PairModelTitle.Religion);
                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        viewModel.SubmitButtonText = SubmitButtonText.Create;
                        viewModel.BreadCrumbActionName = "_CreateHREmployeeAsync";
                        viewModel.ModalTitle = "नयाँ कर्मचारीको बिवरण थप्नुहोस्";
                        viewModel.FormModelName = "HREmployee";
                        viewModel.CRUDAction = CRUDType.CREATE;
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);

                    case CRUDType.UPDATE:
                        viewModel.BreadCrumbActionName = "_UpdateHREmployeeAsync";
                        viewModel.ModalTitle = "कर्मचारीको बिवरण सम्पादन गर्नुहोस्";
                        viewModel.FormModelName = "HREmployee";
                        viewModel.CRUDAction = CRUDType.UPDATE;
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);

                    case CRUDType.DELETE:
                        viewModel.BreadCrumbActionName = "_DeleteHREmployeeAsync";
                        viewModel.ModalTitle = "कर्मचारीको बिवरण मेटाउनुहोस्";
                        viewModel.FormModelName = "HREmployee";
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
        protected async Task<ActionResult> CRUDHREmployee(HREmployeeViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
          
            try
            {
               
                //super user can only delete the employee detail 
                if (cRUDType == CRUDType.DELETE && this.SessionDetail.IdRoleType == 0)
                {
                    await _hREmployeeservices.DeleteEmployee(viewModel.DataModel.Id);
                }
                else
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceHREmployee(viewModel, cRUDType);
                }
                else
                {
                    long idHREmployee = 0;
                    Guid idLogin = Guid.Empty;

                    //image upload
                    if (viewModel.ImageProfile != null && ValidateImageFileType(viewModel.ImageProfile.ContentType))
                    {
                        if (viewModel.ImageProfile.ContentLength <= 1048576)
                        {
                            string serverPath = Server.MapPath("~");
                            string DocName = viewModel.ImageProfile.FileName;
                            string imageName = "Upload\\EmployeeProfile\\" + Guid.NewGuid() + "\\";
                            string DocPath = $"{serverPath}{imageName}";
                            if (!(new DirectoryInfo(DocPath).Exists)) Directory.CreateDirectory(DocPath);
                            string targetPath = Path.Combine(DocPath, DocName);
                            viewModel.DataModel.ImageName = $"\\{imageName}{DocName}";
                            viewModel.ImageProfile.SaveAs(targetPath);
                        }
                        else
                        {
                            Response.StatusCode = 350;
                            ModelState.AddModelError("DataModel.ImageName", GetErrorMessage(UserErrorMessage.IMAGESIZE));
                            return await RETSourceHREmployee(viewModel, cRUDType);
                        }
                    }

                    //document upload
                    if (viewModel.NationalIdentity != null && ValidateDocumentType(viewModel.NationalIdentity.ContentType))
                    {
                        if (viewModel.NationalIdentity.ContentLength < 1048576)
                        {
                            string serverPath = Server.MapPath("~");
                            string DocName = viewModel.NationalIdentity.FileName;
                            string imageName = "Upload\\NationalIdentity\\" + Guid.NewGuid() + "\\";
                            string DocPath = $"{serverPath}{imageName}";
                            if (!(new DirectoryInfo(DocPath).Exists)) Directory.CreateDirectory(DocPath);
                            string targetPath = Path.Combine(DocPath, DocName);
                            viewModel.DataModel.Document = $"\\{imageName}{DocName}";
                            viewModel.ImageProfile.SaveAs(targetPath);
                        }
                        else
                        {
                            Response.StatusCode = 350;
                            ModelState.AddModelError("", GetErrorMessage(UserErrorMessage.IMAGESIZE));
                            return await RETSourceHREmployee(viewModel, cRUDType);
                        }
                    }

                    //Assign required values
                    viewModel.DataModel.DOB = !string.IsNullOrEmpty(viewModel.DataModel.DOBNP) ? GetEnglishDate(viewModel.DataModel.DOBNP) : null;
                    viewModel.DataModel.AppointmentDate = !string.IsNullOrEmpty(viewModel.DataModel.AppointmentDateNP) ? GetEnglishDate(viewModel.DataModel.AppointmentDateNP) : null;
                    viewModel.DataModel.IdBloodGroup = viewModel.DataModel.IdBloodGroup == 0 ? null : viewModel.DataModel.IdBloodGroup;
                    viewModel.DataModel.IdHREmployeeReligion = viewModel.DataModel.IdHREmployeeReligion == 0 ? null : viewModel.DataModel.IdHREmployeeReligion;
                    viewModel.DataModel.IdMarital = viewModel.DataModel.IdMarital == 0 ? null : viewModel.DataModel.IdMarital;
                    viewModel.DataModel.IdMotherTongue = viewModel.DataModel.IdMotherTongue == 0 ? null : viewModel.DataModel.IdMotherTongue;
                    viewModel.DataModel.RetiredDate = !string.IsNullOrEmpty(viewModel.DataModel.RetiredDateNP) ? GetEnglishDate(viewModel.DataModel.RetiredDateNP) : null;
                    switch (cRUDType)
                    {
                        case CRUDType.CREATE:
                            //Check first user name exists or not if exists return back to form
                            if (string.IsNullOrEmpty(viewModel.DataModel.UserName) || string.IsNullOrWhiteSpace(viewModel.DataModel.UserName))
                            {
                                Response.StatusCode = 350;
                                ModelState.AddModelError("DataModel.UserName", GetErrorMessage(UserErrorMessage.USERNAMEEMPTY));
                                return await RETSourceHREmployee(viewModel, cRUDType);
                            }
                            else
                            {
                                var model = await _hREmployeeservices.CheckUserNameAsync(viewModel.DataModel.UserName);
                                if (model)
                                {
                                    Response.StatusCode = 350;
                                    ModelState.AddModelError("DataModel.UserName", GetErrorMessage(UserErrorMessage.USERNAMEEXIST));
                                    return await RETSourceHREmployee(viewModel, cRUDType);
                                }
                            }

                           
                            //CHECK IDENROLL 
                            var IdhremployeeRole = viewModel.DataModelMembership.IdHREmployeeRole;
                            var RoleType = GetRole(IdhremployeeRole);
                            if (viewModel.DataModel.IdEnroll != null)
                            {
                                var isExistIdEnroll = await _hREmployeeservices.Exits(x => x.IdHRCompany == viewModel.DataModel.IdHRCompany && x.IdEnroll == viewModel.DataModel.IdEnroll);

                                if (isExistIdEnroll && RoleType !=4)
                                {
                                    Response.StatusCode = 350;
                                    ModelState.AddModelError("DataModel.IdEnroll", GetErrorMessage(UserErrorMessage.IDENROLLEXIST));
                                    return await RETSourceHREmployee(viewModel, cRUDType);
                                }
                            }

                            //CHECK PIS Number

                            if (!string.IsNullOrEmpty(viewModel.DataModel.PISNumber))
                            {
                                var isExistPis = await _hREmployeeservices.Exits(x => x.PISNumber == viewModel.DataModel.PISNumber);
                                if (isExistPis && RoleType!=4 )
                                {
                                    Response.StatusCode = 350;
                                    ModelState.AddModelError("DataModel.PISNumber", GetErrorMessage(UserErrorMessage.PISNUMBEREXIST));
                                    return await RETSourceHREmployee(viewModel, cRUDType);
                                }
                            }

                            //CHECK National Identity Number
                            //if (!string.IsNullOrEmpty(viewModel.DataModelNIdentity.IdentityNumber))
                            //{
                            //    viewModel.DataModelNIdentity.IssueDate = GetEnglishDate(viewModel.DataModelNIdentity.IssueDateNP);
                            //    viewModel.DataModelNIdentity.ExpiryDate = !string.IsNullOrEmpty(viewModel.DataModelNIdentity.ExpiryDateNP) ? GetEnglishDate(viewModel.DataModelNIdentity.ExpiryDateNP) : null;
                            //    var isExistNumber = await _hREmployeeNationalIdentityServices.Exits(x => x.IdentityNumber == viewModel.DataModelNIdentity.IdentityNumber);
                            //    if (isExistNumber)
                            //    {
                            //        Response.StatusCode = 350;
                            //        ModelState.AddModelError("DataModelNIdentity.IdentityNumber", GetErrorMessage(UserErrorMessage.NATIONALIDENTITYEXIST));
                            //        return await RETSourceHREmployee(viewModel, cRUDType);
                            //    }
                            //}
                            //commit hremploye model only and getting id 
                            idHREmployee = await _hREmployeeservices.CommitSaveChangesAsync(viewModel.DataModel, cRUDType);
                            //check username is null  or not if not null then commit to login table only
                            if (!string.IsNullOrEmpty(viewModel.DataModel.UserName) && !string.IsNullOrEmpty(viewModel.DataModel.SystemLanguage) && idHREmployee > 0)
                            {
                                LoginsModel loginsModel = new LoginsModel
                                {
                                    Id = Guid.NewGuid(),
                                    Password_MD5 = Encryption("1234"),
                                    Password_SHA = Get_HASH_SHA512("1234", viewModel.DataModel.UserName, 256),
                                    Email = viewModel.DataModel.Email,
                                    Mobile = viewModel.DataModel.MobileNumber,
                                    IsEmailConfirmed = true,
                                    TwoFactorEnabled = false,
                                    LockOutEndDate = null,
                                    LockOutEnabled = false,
                                    AccessFailedCount = 0,
                                    Language = viewModel.DataModel.SystemLanguage,
                                    UserName = viewModel.DataModel.UserName
                                };
                                idLogin = await _loginsServices.CommitSaveChangesAsync(loginsModel, cRUDType);
                            }
                            //commit to membership provider
                            if (idHREmployee > 0 && idLogin != null)
                            {
                                viewModel.DataModelMembership.IdHREmployee = idHREmployee;
                                viewModel.DataModelMembership.IdLogin = idLogin;
                                await _MembershipProviderServices.CommitSaveChangesAsync(viewModel.DataModelMembership, cRUDType);

                                //commit national Identity
                                //viewModel.DataModelNIdentity.IdHREmployee = idHREmployee;
                                // await _hREmployeeNationalIdentityServices.CommitSaveChangesAsync(viewModel.DataModelNIdentity, cRUDType);
                            }
                            break;

                        case CRUDType.UPDATE:
                            //commit hremploye model only and getting id 
                            idHREmployee = await _hREmployeeservices.CommitAsync(viewModel.DataModel, cRUDType);
                            //check username is null  or not if not null then commit to login table only
                            if (!string.IsNullOrEmpty(viewModel.DataModel.UserName) && !string.IsNullOrEmpty(viewModel.DataModel.SystemLanguage) && idHREmployee > 0)
                            {
                                var membershipModel = await _MembershipProviderServices.FindAsync(x => x.IdHREmployee == idHREmployee);//get membership model to find idLogin of user
                                if (membershipModel == null)
                                {
                                    LoginsModel loginsModel = new LoginsModel
                                    {
                                        Id = Guid.NewGuid(),
                                        Password_MD5 = Encryption("1234"),
                                        Password_SHA = Get_HASH_SHA512("1234", viewModel.DataModel.UserName, 256),
                                        Email = viewModel.DataModel.Email,
                                        Mobile = viewModel.DataModel.MobileNumber,
                                        IsEmailConfirmed = true,
                                        TwoFactorEnabled = false,
                                        LockOutEndDate = null,
                                        LockOutEnabled = false,
                                        AccessFailedCount = 0,
                                        UserName = viewModel.DataModel.UserName,
                                        Language = viewModel.DataModel.SystemLanguage
                                    };
                                    idLogin = await _loginsServices.CommitAsync(loginsModel, CRUDType.CREATE);//create login 
                                    if (idHREmployee > 0 && idLogin != null)
                                    {
                                        viewModel.DataModelMembership.IdHREmployee = idHREmployee;
                                        viewModel.DataModelMembership.IdLogin = idLogin;
                                        await _MembershipProviderServices.CommitAsync(viewModel.DataModelMembership, CRUDType.CREATE);//create membershipprovier
                                    }
                                }
                                else
                                {
                                    var loginModel = await _loginsServices.GetByIdAsync(membershipModel.IdLogin);//Get loginsModel
                                                                                                                 //updating the entities of logins model
                                    loginModel.UserName = viewModel.DataModel.UserName;
                                    loginModel.Email = viewModel.DataModel.Email;
                                    loginModel.Mobile = viewModel.DataModel.MobileNumber;
                                    loginModel.Language = viewModel.DataModel.SystemLanguage;
                                    idLogin = await _loginsServices.UpdateAsync(loginModel);
                                    membershipModel.IdHREmployeeRole = viewModel.DataModelMembership.IdHREmployeeRole;
                                    await _MembershipProviderServices.UpdateAsync(membershipModel);
                                }
                            }
                            //if (viewModel.DataModelNIdentity != null && idHREmployee > 0)
                            //{
                            //    viewModel.DataModelNIdentity.IdHREmployee = idHREmployee;
                            //    if (viewModel.DataModelNIdentity.Id == 0)
                            //    {
                            //        await _hREmployeeNationalIdentityServices.CommitAsync(viewModel.DataModelNIdentity, CRUDType.CREATE);
                            //    }
                            //    else
                            //        await _hREmployeeNationalIdentityServices.CommitAsync(viewModel.DataModelNIdentity, cRUDType);
                            //}
                            await _unitOfWork.SaveAsync();
                            break;
                    }
                }
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceHREmployee(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListHREmployeeAsync", new { idHRCompany = viewModel.DataModel.IdHRCompany == null ? 0 : viewModel.DataModel.IdHRCompany, active= viewModel.IdActiveEmployee==null ?1: viewModel.IdActiveEmployee, pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }

        #endregion
        #region National Identity
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _CreateNationalIdentity()
        {
            try
            {
                return PartialView(new HREmployeeViewModel
                {
                    SessionDetails = this.SessionDetail,
                    DDLNationalIdentityType = await GetIntStringPairModel(PairModelTitle.NationalIdentityType),
                    DDLDistrict = await GetLongStringPairModel(PairModelTitle.District),
                    DataModelNIdentity = new HREmployeeNationalIdentityModel { Id = 0, IssueDate = DateTime.Now.Date, ExpiryDate = DateTime.Now.Date, IsDefault = true }
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }
        #endregion
        #region permissionwithin
        public bool Permission(long? CurrentID,long? RequestedId,int? IdroleType, long? idCompany,long? IdHRCompanyDivision)
        {
            try
            {
                if (IdroleType == 1 || IdroleType==2)
                {
                    var test = _hREmployeeservices.CheckIdhremployee(RequestedId, idCompany,null, IdroleType);
                    if (!test)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else if (IdroleType == 4)
                {
                    var test = _hREmployeeservices.CheckIdhremployee(RequestedId, idCompany, IdHRCompanyDivision, 4);
                    if (!test)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else if (IdroleType == 3 && CurrentID==RequestedId)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return false;
            }
        }
        #endregion


        #region NEWDATA
        public async Task<HREmployeeModel> EmployeeInformation(long? id)
        {
            EAttendanceSystemDBEntities db = new EAttendanceSystemDBEntities();
            HREmployeeModel model = new HREmployeeModel();
            var data =  db.HREmployee.Where(x => x.Id == id).Select(x => new HREmployeeModel 
            {
               AppointmentDate = x.AppointmentDate,
               AppointmentDateNP=x.AppointmentDateNP,
               BirthPlace=x.BirthPlace,
               Color=x.Color,
               DOB=x.DOB,
               DOBNP=x.DOBNP,
               Document=x.Document,
               Document1=x.Document1,
               Email=x.Email,
               HREmployeeName=x.HREmployeeName,
               HREmployeeNameNP=x.HREmployeeNameNP,
               Id=x.Id,
               IdBloodGroup=x.IdBloodGroup,
               IdEnroll=x.IdEnroll,
               IdGender=x.IdGender==null?0:x.IdGender.Value,
               IdHRCompany=x.IdHRCompany,
               IdHREmployeeReligion=x.IdHREmployeeReligion,
               IdMarital=x.IdMarital,
               IdMotherTongue=x.IdMotherTongue,
               IdParent_HREmployee=x.IdParent_HREmployee,
               IdRelation=x.IdRelation,
               ImageName=x.ImageName,
               IsActive=x.IsActive,
               IsDisabled=x.IsDisabled,
               IsOfficier= x.IsOfficier == null ? false : x.IsOfficier.Value,
               LeftEye=x.LeftEye,
               LeftThumb=x.LeftThumb,
               MobileNumber=x.MobileNumber,
               PISNumber=x.PISNumber,
               Remark=x.Remark,
               RetiredDate=x.RetiredDate,
               RetiredDateNP=x.RetiredDateNP,
               RightEye=x.RightEye,
               RightThumb=x.RightThumb,
               Signature=x.Signature,
               Status=x.Status,
               TotalChild=x.TotalChild,
               TotalFamilyNumber=x.TotalFamilyNumber,
            }).FirstOrDefault();
            return data;
        }


        #endregion
    }
}