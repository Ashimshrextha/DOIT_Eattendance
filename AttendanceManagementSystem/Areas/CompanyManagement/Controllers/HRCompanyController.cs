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
using SystemModels.CompanyManagement;
using SystemServices.CompanyManagement;
using SystemViewModels.CompanyManagement;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;

namespace AttendanceManagementSystem.Areas.CompanyManagement.Controllers
{
    public class HRCompanyController : BaseController
    {
        // GET: CompanyManagement/HRCompany

        private readonly HRCompanyServices _hRCompanyServices;

        public HRCompanyController()
        {
            this._hRCompanyServices = new HRCompanyServices(this._unitOfWork);
        }

        [NonAction]
        public Expression<Func<HRCompany, bool>> Condition(string searchKey = "", int searchKey1=0)
        {
            Expression<Func<HRCompany, bool>> returnData = (x => false);
            switch (this.SessionDetail.IdRoleType)
            {
                case (int)RoleType.SuperUser:
                    return returnData = (x => (x.IdHRCompanyType == searchKey1 || searchKey1 == 0) && (x.CompanyName.ToUpper().Contains(searchKey.ToUpper().ToString()) || x.CompanyShortName.ToUpper().Contains(searchKey.ToUpper().ToString()) ||searchKey == ""));
                case (int)RoleType.Admin:
                    return returnData = (x => x.Id == this.SessionDetail.IdHRCompany || x.IdParent_HRCompany == this.SessionDetail.IdHRCompany && (x.CompanyName.ToUpper().Contains(searchKey.ToUpper().ToString()) || searchKey == ""));
                case (int)RoleType.RootUser:
                    return returnData = (x => x.Id == this.SessionDetail.IdHRCompany && (x.CompanyName.ToUpper().Contains(searchKey.ToUpper().ToString()) || searchKey == ""));
                case (int)RoleType.SectionAdmin:
                    return returnData = (x => x.Id == this.SessionDetail.IdHRCompany && (x.CompanyName.ToUpper().Contains(searchKey.ToUpper().ToString()) || searchKey == ""));
                case (int)RoleType.NormalUser:
                    return returnData = (x => x.Id == this.SessionDetail.IdHRCompany && (x.CompanyName.ToUpper().Contains(searchKey.ToUpper().ToString()) || searchKey == ""));
                default:
                    return returnData;
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> Index(int? pageNumber, int? pageSize, string orderingDirection = "ASC", string orderingBy = "IdHRCompanyType")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var userSession = SessionDetail;
                return View(new HRCompanyViewModelList
                {
                    SessionDetails = userSession,
                    DBModelList = await _hRCompanyServices.GetPagedList(Condition(), pagination.PageNumber, pagination.PageSize),
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompany",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompany",
                    BreadCrumbTitle = "कार्यालय व्यवस्थापन",
                    BreadCrumbActionName = "Index",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    DDLCompanyType = await GetIntStringPairModel(PairModelTitle.HRCompanyType),
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _Tabbed(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "" ,int searchKey1 =0)
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, "IdHRCompanyType", "ASC");
                return View(new HRCompanyViewModelList
                {
                    SessionDetails = SessionDetail,
                    DBModelList = await _hRCompanyServices.GetPagedList(Condition(searchKey, searchKey1), pagination.PageNumber, pagination.PageSize),
                    BreadCrumbTitle = "कार्यालय व्यवस्थापन",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompany",
                    BreadCrumbActionName = "_Tabbed",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompany",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    SearchKey1=searchKey1
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        //[AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> CompanyProfileAsync(long? idHRCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            try
            {

                var CurrentId = SessionDetail.IdHRCompany;
                var RequestedId = idHRCompany;
                var IdroleType = SessionDetail.IdRoleType;
                if (IdroleType > 0)
                {

                    if (CurrentId != RequestedId)
                {
                   
                        var check = Permission(CurrentId, RequestedId);

                        if (!check)
                        {
                            return PartialView("~/Views/Shared/Error.cshtml");
                        }

                    }
                }

                var compname =  _hRCompanyServices.GetCompanyName(idHRCompany);
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return View(new HRCompanyViewModelList
                {
                    SessionDetails = SessionDetail,
                    BreadCrumbTitle = "कार्यालय व्यवस्थापन" + " /  " + compname,
                    BreadCrumbArea = "HRCompany",
                    BreadCrumbController = "HRCompany",
                    BreadCrumbActionName = "CompanyProfileAsync",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompany",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    IdHRCompany = idHRCompany,
                    DBModel = await _hRCompanyServices.GetByIdAsync(idHRCompany),
                    DBModelList = await _hRCompanyServices.GetsChildCompany(idHRCompany, pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, ""),
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }


        //[AuthorizeUser(ActionName = "CompanyProfileAsync")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListChildHRCompany(long? idHRCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _hRCompanyServices.GetsChildCompany(idHRCompany, pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, "");
                return PartialView(new HRCompanyViewModelList
                {
                    SessionDetails = SessionDetail,
                    DBModelList = model,
                    BreadCrumbTitle = "कार्यालय व्यवस्थापन",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompany",
                    BreadCrumbActionName = "_ListChildHRCompany",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompany",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    IdHRCompany = idHRCompany,

                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        #region HRCompany

        [AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListHRCompanyAsync(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "", int searchKey1 =0)
       {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, "IdHRCompanyType", "ASC");
                return PartialView(new HRCompanyViewModelList
                {
                    SessionDetails = SessionDetail,
                    DBModelList = await _hRCompanyServices.GetPagedList(Condition(searchKey,searchKey1), pagination.PageNumber, pagination.PageSize),
                    BreadCrumbTitle = "कार्यालय व्यवस्थापन",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompany",
                    BreadCrumbActionName = "_ListHRCompanyAsync",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompany",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey=searchKey,
                    SearchKey1 = searchKey1,

                });
            }
            catch (Exception exp)
            {
                throw new ArgumentException(exp.Message);
            }
        }

        //[AuthorizeUser(ActionName = "CompanyProfileAsync")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListWrapperHRCompanyAsync(long? idHRCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey, int searchKey1)
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, "IdHRCompanyType", "ASC");
                return PartialView(new HRCompanyViewModelList
                {
                    SessionDetails = SessionDetail,
                    DBModelList = await _hRCompanyServices.GetsChildCompany(idHRCompany, pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, searchKey),
                    DBModel = await _hRCompanyServices.GetByIdAsync(idHRCompany),
                    BreadCrumbTitle = "कार्यालय व्यवस्थापन",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompany",
                    BreadCrumbActionName = "_ListWrapperHRCompanyAsync",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompany",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    IdHRCompany = idHRCompany,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    SearchKey1 = searchKey1
                });
            }
            catch (Exception exp)
            {
                throw new ArgumentException(exp.Message);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _CreateHRCompanyAsync()
        {
            try
            {
                return PartialView(new HRCompanyViewModel
                {
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompany",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompany",
                    BreadCrumbActionName = "_CreateHRCompanyAsync",
                    FormModelName = "HRCompany",
                    ModalTitle = "नयाँ कार्यालय सिर्जना गर्नुहोस्",
                    BreadCrumbTitle = "कार्यालय व्यवस्थापन",
                    CRUDAction = CRUDType.CREATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Create,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLHRCompanyCode = await GetIntStringPairModel(PairModelTitle.HRCompanyCode),
                    DDLCompanyType = await GetIntStringPairModel(PairModelTitle.HRCompanyType),
                    DDLParentChildCompany = await GetLongStringPairModel(PairModelTitle.HRParentChildCompany),
                    DDLState = await GetIntStringPairModel(PairModelTitle.State),
                    DataModel = new HRCompanyModel { Id = 0 }
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }


        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _UpdateHRCompanyAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey, int searchKey1 )
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var CompanyInfo = await _hRCompanyServices.GetModelByIdAsync(id);
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRCompanyViewModel
                {
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompany",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompany",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    SearchKey1 = searchKey1,
                    BreadCrumbActionName = "_UpdateHRCompanyAsync",
                    FormModelName = "HRCompany",
                    ModalTitle = "कार्यालय सम्पादन गर्नुहोस्",
                    BreadCrumbTitle = "कार्यालय व्यवस्थापन",
                    CRUDAction = CRUDType.UPDATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Update,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLHRCompanyCode = await GetIntStringPairModel(PairModelTitle.HRCompanyCode),
                    DataModel = CompanyInfo,
                    DDLCompanyType = await GetIntStringPairModel(PairModelTitle.HRCompanyType),
                    DDLParentChildCompany = await GetLongStringPairModel(PairModelTitle.HRParentChildCompany),
                    DDLState = await GetIntStringPairModel(PairModelTitle.State)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _DeleteHRCompanyAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey, int searchKey1 )
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRCompanyViewModel
                {
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompany",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompany",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    SearchKey1 = searchKey1,
                    FormModelName = "HRCompany",
                    ModalTitle = "कार्यालय विवरण मेटाउनुहोस्",
                    BreadCrumbTitle = "कार्यालय व्यवस्थापन",
                    BreadCrumbActionName = "_DeleteHRCompanyAsync",
                    CRUDAction = CRUDType.DELETE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Delete,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = await _hRCompanyServices.GetModelByIdAsync(id),
                    DDLHRCompanyCode = await GetIntStringPairModel(PairModelTitle.HRCompanyCode),
                    DDLCompanyType = await GetIntStringPairModel(PairModelTitle.HRCompanyType),
                    DDLParentChildCompany = await GetLongStringPairModel(PairModelTitle.HRParentChildCompany),
                    DDLState = await GetIntStringPairModel(PairModelTitle.State)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        //[AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ExportHRCompanyAsync(string searchKey = "", int searchKey1 = 0)
        {
            try
            {
                var model = await _hRCompanyServices.FindAllAsync(Condition(searchKey,searchKey1));
               var data= model.Select(x => new
                {
                    OfficeName = x.CompanyName,
                    OfficeCode = x.HRCompanyCode.HRCompanyCodeTitle,
                    OfficeNameNP = x.CompanyNameNP,
                    OfficeShortName = x.CompanyShortName,
                    OfficeType = x.HRCompanyType.CompanyType,
                    OfficeAddress = x.Address
                });
                return await ExportToExcel("Office", data);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        //[AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintHRCompanyAsync(string searchKey = "", int searchKey1 = 0)
        {
            try
            {
                return PartialView(new HRCompanyViewModel
                {
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompany",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompany",
                    BreadCrumbActionName = "_PrintHRCompanyAsync",
                    FormModelName = "HRCompany",
                    ModalTitle = "कार्यालय सूची छाप्नुहोस्",
                    BreadCrumbTitle = "कार्यालय व्यवस्थापन",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBModelList = await _hRCompanyServices.FindAllAsync(Condition(searchKey,searchKey1))
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
        public async Task<ActionResult> _CreateHRCompanyAsync(HRCompanyViewModel hRCompanyViewModel, FormCollection formCollection)
        {
            return await CRUDHRCompanyAsync(hRCompanyViewModel, CRUDType.CREATE, formCollection);
        }

        [AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _UpdateHRCompanyAsync(HRCompanyViewModel hRCompanyViewModel, FormCollection formCollection)
        {
            return await CRUDHRCompanyAsync(hRCompanyViewModel, CRUDType.UPDATE, formCollection);
        }

        [AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _DeleteHRCompanyAsync(HRCompanyViewModel hRCompanyViewModel, FormCollection formCollection)
        {
            return await CRUDHRCompanyAsync(hRCompanyViewModel, CRUDType.DELETE, formCollection);
        }

        [NonAction]
        protected async Task<ActionResult> RETSourceHRCompanyAsync(HRCompanyViewModel hRCompanyViewModel, CRUDType cRUDType)
        {
            try
            {
                hRCompanyViewModel.SessionDetails = SessionDetail;
                hRCompanyViewModel.BreadCrumbArea = "CompanyManagement";
                hRCompanyViewModel.BreadCrumbController = "HRCompany";
                hRCompanyViewModel.BreadCrumbBaseURL = "CompanyManagement/HRCompany";
                hRCompanyViewModel.FormModelName = "HRCompany";
                hRCompanyViewModel.DDLHRCompanyCode = await GetIntStringPairModel(PairModelTitle.HRCompanyCode);
                hRCompanyViewModel.DDLCompanyType = await GetIntStringPairModel(PairModelTitle.HRCompanyType);
                hRCompanyViewModel.DDLParentChildCompany = await GetLongStringPairModel(PairModelTitle.HRParentChildCompany);
                hRCompanyViewModel.DDLState = await GetIntStringPairModel(PairModelTitle.State);
                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        hRCompanyViewModel.ModalTitle = "नयाँ कार्यालय सिर्जना गर्नुहोस्";
                        hRCompanyViewModel.BreadCrumbActionName = "_CreateHRCompanyAsync";
                        hRCompanyViewModel.CRUDAction = CRUDType.CREATE;
                        return PartialView(hRCompanyViewModel.BreadCrumbActionName, hRCompanyViewModel);
                    case CRUDType.UPDATE:
                        hRCompanyViewModel.ModalTitle = "कार्यालय सम्पादन गर्नुहोस्";
                        hRCompanyViewModel.BreadCrumbActionName = "_UpdateHRCompanyAsync";
                        hRCompanyViewModel.CRUDAction = CRUDType.UPDATE;
                        return PartialView(hRCompanyViewModel.BreadCrumbActionName, hRCompanyViewModel);
                    case CRUDType.DELETE:
                        hRCompanyViewModel.ModalTitle = "कार्यालय विवरण मेटाउनुहोस्";
                        hRCompanyViewModel.BreadCrumbActionName = "_DeleteHRCompanyAsync";
                        hRCompanyViewModel.CRUDAction = CRUDType.DELETE;
                        return PartialView(hRCompanyViewModel.BreadCrumbActionName, hRCompanyViewModel);
                }
                return null;
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [NonAction]
        private async Task<ActionResult> CRUDHRCompanyAsync(HRCompanyViewModel hRCompanyViewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceHRCompanyAsync(hRCompanyViewModel, cRUDType);
                }
                //Document upload
                if (hRCompanyViewModel.HRCompanyLogo != null && ValidateImageFileType(hRCompanyViewModel.HRCompanyLogo.ContentType))
                {
                    if (hRCompanyViewModel.HRCompanyLogo.ContentLength <= 1048576)
                    {
                        string serverPath = Server.MapPath("~");
                        string DocName = hRCompanyViewModel.HRCompanyLogo.FileName;
                        string imageName = "\\Upload\\CompanyLogo\\" + Guid.NewGuid() + "\\";
                        string DocPath = $"{serverPath}{imageName}";
                        if (!(new DirectoryInfo(DocPath).Exists)) Directory.CreateDirectory(DocPath);
                        string targetPath = Path.Combine(DocPath, DocName);
                        hRCompanyViewModel.DataModel.Logo = $"{imageName}{DocName}";
                        hRCompanyViewModel.HRCompanyLogo.SaveAs(targetPath);
                    }
                    else
                    {
                        Response.StatusCode = 350;
                        ModelState.AddModelError("DataModel.Logo", GetErrorMessage(UserErrorMessage.IMAGESIZE));
                        return await RETSourceHRCompanyAsync(hRCompanyViewModel, cRUDType);
                    }
                }

                var DATA = await _hRCompanyServices.CommitSaveChangesAsync(hRCompanyViewModel.DataModel, cRUDType);
                if (CRUDType.CREATE == cRUDType)
                {
                    _hRCompanyServices.UpdateAutomatically(DATA);

                }


            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceHRCompanyAsync(hRCompanyViewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), hRCompanyViewModel.DataModel.CompanyName, AlertNotificationType.success);
            return RedirectToAction("_ListHRCompanyAsync", new { pageNumber = hRCompanyViewModel.PageNumber, pageSize = hRCompanyViewModel.PageSize, orderingBy = hRCompanyViewModel.OrderingBy, orderingDirection = hRCompanyViewModel.OrderingDirection, searchKey = hRCompanyViewModel.SearchKey , searchKey1 = hRCompanyViewModel.SearchKey1 });
        }

        #endregion


        #region permissionwithin
        public bool Permission(long? CurrentID, long? RequestedId)
        {

            try
            {
                
                    var test = _hRCompanyServices.CheckIdhremployee(CurrentID, RequestedId);
                    if (!test)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
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


      
        public HRCompanyModel CompanyInformation(long? id)
        {
            EAttendanceSystemDBEntities db = new EAttendanceSystemDBEntities();
            HRCompanyModel model= new HRCompanyModel();
            var data = db.HRCompanies.Where(x => x.Id == id).Select(x=>new HRCompanyModel { 
            Address=x.Address,
            CompanyName=x.CompanyName,
            CompanyNameNP=x.CompanyNameNP,
            CompanyShortName=x.CompanyShortName,
            FaxNumber=x.FaxNumber,
            Id=x.Id,
            IdCompanyCode=x.IdCompanyCode,
            IdHRCompanyType=x.IdHRCompanyType,
            IdParent_HRCompany=x.IdParent_HRCompany,
            IdState=x.IdState,
            IsActive=x.IsActive,
            IsCentralLevelHRCompany=x.IsCentralLevelHRCompany,
            Logo=x.Logo,
            PhoneNumber=x.PhoneNumber,
            Remark=x.Remark,
            }).FirstOrDefault();
            return data;
        }



    }
}