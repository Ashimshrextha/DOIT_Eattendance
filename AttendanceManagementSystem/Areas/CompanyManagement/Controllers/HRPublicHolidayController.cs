using AttendanceManagementSystem.App_Auth;
using AttendanceManagementSystem.Controllers;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using SystemDatabase;
using SystemModels.SystemSetting;
using SystemServices.SystemSetting;
using SystemViewModels.SystemSetting;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;

namespace AttendanceManagementSystem.Areas.CompanyManagement.Controllers
{
    public class HRPublicHolidayController : BaseController
    {
        // GET: CompanyManagement/HRPublicHoliday

        private readonly HRPublicHolidayServices _HRPublicHolidayServices;

        public HRPublicHolidayController()
        {
            this._HRPublicHolidayServices = new HRPublicHolidayServices(this._unitOfWork);
        }


        #region PublicHoliday

        [NonAction]
        public Expression<Func<HRPublicHoliday, bool>> Condition(long? idHRCompany, string searchKey = "")
        {
            Expression<Func<HRPublicHoliday, bool>> returnData = (x => false);
            var currentyearNp = Date(DateTime.Now).NepYear;
            var NextYear = currentyearNp + 1;
            var CurrentNewYear = GetDateByDateNP($"{currentyearNp}-01-01").EngDate;
            var NextNewYear = GetDateByDateNP($"{NextYear}-01-01").EngDate;
          

            switch (this.SessionDetail.IdRoleType)
            {
                case (int)RoleType.SuperUser:
                    return returnData = (x => x.IdHRCompany == idHRCompany  && (x.HRPublicHolidayTitle.ToUpper().Contains(searchKey.ToUpper()) || searchKey == ""));
                case (int)RoleType.Admin:
                    return returnData = (x => x.IdHRCompany == idHRCompany && x.Fromdate >= CurrentNewYear && x.Fromdate < NextNewYear && (x.HRPublicHolidayTitle.ToUpper().Contains(searchKey.ToUpper()) || searchKey == ""));
                case (int)RoleType.RootUser:
                    return returnData = (x => x.IdHRCompany == idHRCompany && x.Fromdate >= CurrentNewYear && x.Fromdate < NextNewYear && (x.HRPublicHolidayTitle.ToUpper().Contains(searchKey.ToUpper()) || searchKey == ""));
                case (int)RoleType.NormalUser:
                    return returnData = (x => x.IdHRCompany == idHRCompany && x.Fromdate >= CurrentNewYear && x.Fromdate < NextNewYear && (x.HRPublicHolidayTitle.ToUpper().Contains(searchKey.ToUpper()) || searchKey == ""));
                case (int)RoleType.SectionAdmin:
                    return returnData = (x => x.IdHRCompany == idHRCompany && x.Fromdate >= CurrentNewYear && x.Fromdate < NextNewYear && (x.HRPublicHolidayTitle.ToUpper().Contains(searchKey.ToUpper()) || searchKey == ""));
                default:
                    return returnData;
            }
        }

        [AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListHRPublicHolidayAsync(long? idHRCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                if (idHRCompany == null || idHRCompany == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRPublicHolidayViewModelList
                {
                    SessionDetails = SessionDetail,
                    DBModelList = await _HRPublicHolidayServices.GetPagedList(Condition(idHRCompany, searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRPublicHoliday",
                    BreadCrumbBaseURL = "CompanyManagement/HRPublicHoliday",
                    BreadCrumbActionName = "_ListHRPublicHolidayAsync",
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

        [AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadListHRPublicHolidayAsync(long? idHRCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                if (idHRCompany == null || idHRCompany == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRPublicHolidayViewModelList
                {
                    SessionDetails = SessionDetail,
                    DBModelList = await _HRPublicHolidayServices.GetPagedList(Condition(idHRCompany, searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRPublicHoliday",
                    BreadCrumbBaseURL = "CompanyManagement/HRPublicHoliday",
                    BreadCrumbActionName = "_ReadListHRPublicHolidayAsync",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    IdHRCompany = idHRCompany,
                    DDLReligion = await this.GetIntStringPairModel(PairModelTitle.Religion),
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListWrapperHRPublicHolidayAsync(long? idHRCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                if (idHRCompany == null || idHRCompany == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRPublicHolidayViewModelList
                {
                    SessionDetails = SessionDetail,
                    DBModelList = await _HRPublicHolidayServices.GetPagedList(Condition(idHRCompany, searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRPublicHoliday",
                    BreadCrumbBaseURL = "CompanyManagement/HRPublicHoliday",
                    BreadCrumbActionName = "_ListHRPublicHolidayAsync",
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

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _CreateHRPublicHolidayAsync(long? idHRCompany)
        {
            try
            {
                if (idHRCompany == null || idHRCompany == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                return PartialView(new HRPublicHolidayViewModel
                {
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRPublicHoliday",
                    BreadCrumbBaseURL = "CompanyManagement/HRPublicHoliday",
                    BreadCrumbActionName = "_CreateHRPublicHolidayAsync",
                    FormModelName = "HRPublicHoliday",
                    ModalTitle = "नयाँ सार्वजनिक विदा थप्नुहोस्",
                    CRUDAction = CRUDType.CREATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Create,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = new HRPublicHolidayModel
                    {
                        Id = 0,
                        FromdateNp = Today.Result.Nepdate,
                        ToDateNp = Today.Result.Nepdate,
                        IsHolidayForDisabled = false,
                        IdHRCompany = (long)idHRCompany,
                        IsHolidayForFemale = true,
                        IsHolidayForMale = true
                    },
                    IdHRCompany = idHRCompany,
                    DDLReligion = await this.GetIntStringPairModel(PairModelTitle.Religion),
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId, idHRCompany)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadHRPublicHolidayAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _HRPublicHolidayServices.GetModelByIdAsync(id);
                model.FromdateNp = Date((DateTime)model.Fromdate).Nepdate;
                model.ToDateNp = Date((DateTime)model.ToDate).Nepdate;
                return PartialView(new HRPublicHolidayViewModel
                {
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRPublicHoliday",
                    BreadCrumbBaseURL = "CompanyManagement/HRPublicHoliday",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_ReadHRPublicHolidayAsync",
                    FormModelName = "HRPublicHoliday",
                    ModalTitle = "सार्वजनिक विदा विवरण हेर्नुहोस्",
                    BreadCrumbTitle = "Public Holiday",
                    CRUDAction = CRUDType.READ,
                    OnlyCancelButton = true,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId, model.IdHRCompany),
                    DataModel = model,
                    DDLReligion = await this.GetIntStringPairModel(PairModelTitle.Religion),
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _UpdateHRPublicHolidayAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _HRPublicHolidayServices.GetModelByIdAsync(id);
                model.FromdateNp = Date((DateTime)model.Fromdate).Nepdate;
                model.ToDateNp = Date((DateTime)model.ToDate).Nepdate;
                return PartialView(new HRPublicHolidayViewModel
                {
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRPublicHoliday",
                    BreadCrumbBaseURL = "CompanyManagement/HRPublicHoliday",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_UpdateHRPublicHolidayAsync",
                    FormModelName = "HRPublicHoliday",
                    ModalTitle = "सार्वजनिक विदा सम्पादन गर्नुहोस्",
                    CRUDAction = CRUDType.UPDATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Update,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId, model.IdHRCompany),
                    DDLReligion = await this.GetIntStringPairModel(PairModelTitle.Religion),
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
        public async Task<ActionResult> _DeleteHRPublicHolidayAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _HRPublicHolidayServices.GetModelByIdAsync(id);
                model.FromdateNp = Date((DateTime)model.Fromdate).Nepdate;
                model.ToDateNp = Date((DateTime)model.ToDate).Nepdate;
                return PartialView(new HRPublicHolidayViewModel
                {
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRPublicHoliday",
                    BreadCrumbBaseURL = "CompanyManagement/HRPublicHoliday",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    FormModelName = "HRPublicHoliday",
                    ModalTitle = "सार्वजनिक विदा मेटाउन निश्चित हुनुहुन्छ?",
                    BreadCrumbTitle = "Public Holiday",
                    BreadCrumbActionName = "_DeleteHRPublicHolidayAsync",
                    CRUDAction = CRUDType.DELETE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Delete,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId, model.IdHRCompany),
                    DDLReligion = await this.GetIntStringPairModel(PairModelTitle.Religion),
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
        public async Task<ActionResult> _ExportHRPublicHolidayAsync(long? idHRCompany)
        {
            try
            {
                if (idHRCompany == null || idHRCompany == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var model = await _HRPublicHolidayServices.FindAllAsync(Condition(idHRCompany));
                var data = model.Select(x => new {
                    Company= x.HRCompany?.HRCompanyCode?.HRCompanyCodeTitle,
                    PublicHoliday = x.HRPublicHolidayTitle,
                    PublicHolidayShortName = x.HRPublicHolidayShortName,
                    FromDate = x.Fromdate,
                    ToDate = x.ToDate,
                    HolidayForFemale = x.IsHolidayForFemale.ToString(),
                    HolidayForMale = x.IsHolidayForMale.ToString(),
                    HolidayForDisabled = x.IsHolidayForDisabled.ToString(),
                    Religion = x.HREmployeeReligion?.Religion,
                });

                return await ExportToExcel("PublicHoliday", data);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintHRPublicHolidayAsync(long? idHRCompany)
        {
            try
            {
                if (idHRCompany == null || idHRCompany == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                return PartialView(new HRPublicHolidayViewModel
                {
                    SessionDetails = SessionDetail,
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRPublicHoliday",
                    BreadCrumbBaseURL = "CompanyManagement/HRPublicHoliday",
                    BreadCrumbActionName = "_PrintHRPublicHolidayAsync",
                    FormModelName = "HRPublicHoliday",
                    ModalTitle = "सार्वजनिक विदा सुची छाप्नुहोस्",
                    BreadCrumbTitle = "Public Holiday",
                    CRUDAction = CRUDType.PRINT,
                    IdHRCompany = idHRCompany,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBModelList = await _HRPublicHolidayServices.FindAllAsync(Condition(idHRCompany)),
                    
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
        public async Task<ActionResult> _CreateHRPublicHolidayAsync(HRPublicHolidayViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHRPublicHoliday(viewModel, CRUDType.CREATE, formCollection);
        }

        [AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _UpdateHRPublicHolidayAsync(HRPublicHolidayViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHRPublicHoliday(viewModel, CRUDType.UPDATE, formCollection);
        }

        [AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _DeleteHRPublicHolidayAsync(HRPublicHolidayViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHRPublicHoliday(viewModel, CRUDType.DELETE, formCollection);
        }

        [NonAction]
        protected async Task<ActionResult> RETSourceHRPublicHoliday(HRPublicHolidayViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
                viewModel.SessionDetails = SessionDetail;
                viewModel.BreadCrumbArea = "CompanyManagement";
                viewModel.BreadCrumbController = "HRPublicHoliday";
                viewModel.BreadCrumbBaseURL = "CompanyManagement/HRPublicHoliday";
                viewModel.DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId, viewModel.DataModel.IdHRCompany);
                viewModel.DDLReligion = await this.GetIntStringPairModel(PairModelTitle.Religion);
                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        viewModel.SubmitButtonText = SubmitButtonText.Create;
                        viewModel.BreadCrumbActionName = "_CreateHRPublicHolidayAsync";
                        viewModel.ModalTitle = "नयाँ सार्वजनिक विदा थप्नुहोस्";
                        viewModel.FormModelName = "HRPublicHoliday";
                        viewModel.CRUDAction = CRUDType.CREATE;
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.UPDATE:
                        viewModel.BreadCrumbActionName = "_UpdateHRPublicHolidayAsync";
                        viewModel.ModalTitle = "सार्वजनिक विदा सम्पादन गर्नुहोस्";
                        viewModel.FormModelName = "HRPublicHoliday";
                        viewModel.CRUDAction = CRUDType.UPDATE;
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.DELETE:
                        viewModel.BreadCrumbActionName = "_DeleteHRPublicHolidayAsync";
                        viewModel.ModalTitle = "सार्वजनिक विदा मेटाउन निश्चित हुनुहुन्छ?";
                        viewModel.FormModelName = "HRPublicHoliday";
                        viewModel.CRUDAction = CRUDType.DELETE;
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    default:
                        return null;
                }
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [NonAction]
        protected async Task<ActionResult> CRUDHRPublicHoliday(HRPublicHolidayViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceHRPublicHoliday(viewModel, cRUDType);
                }
                viewModel.DataModel.Fromdate = (DateTime)this.GetEnglishDate(viewModel.DataModel.FromdateNp);
                viewModel.DataModel.ToDate = (DateTime)this.GetEnglishDate(viewModel.DataModel.ToDateNp);
                await _HRPublicHolidayServices.CommitSaveChangesAsync(viewModel.DataModel, cRUDType);
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceHRPublicHoliday(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListHRPublicHolidayAsync", new { idHRCompany = viewModel.DataModel.IdHRCompany, pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }
        #endregion
    }
}