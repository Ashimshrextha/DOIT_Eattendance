using AttendanceManagementSystem.App_Auth;
using AttendanceManagementSystem.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SystemDatabase;
using SystemModels.CompanyManagement;
using SystemServices.CompanyManagement;
using SystemViewModels.CompanyManagement;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;

namespace AttendanceManagementSystem.Areas.CompanyManagement.Controllers
{
    public class HRCompanyDivisionTypeController : BaseController
    {
        // GET: CompanyManagement/HRCompanyDivisionType

        private readonly HRCompanyDivisionTypeServices _hRDivisionTypeServices;

        public HRCompanyDivisionTypeController()
        {
            this._hRDivisionTypeServices = new HRCompanyDivisionTypeServices(this._unitOfWork);
        }

		#region Division Type
		[NonAction]
		public Expression<Func<HRCompanyDivisionType, bool>> Condition(long? idHRCompany, string searchKey = "")
		{
			Expression<Func<HRCompanyDivisionType, bool>> retutndata = (x => false);

			if (this.SessionDetail.IdRoleType == 0) retutndata = (x =>x.IdHRCompany== idHRCompany&&( x.HRCompanyDivisionTypeTitle.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));

			else if (this.SessionDetail.IdRoleType == 1 || SessionDetail.IdRoleType == 2 || SessionDetail.IdRoleType == 4) retutndata = (x => x.IdHRCompany == idHRCompany  && (x.HRCompanyDivisionTypeTitle.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));

			else retutndata = (x => false);

			return retutndata;
		}
		[AuthorizeUser(ActionName ="Index")]
		[AcceptVerbs(HttpVerbs.Get)]
		public async Task<ActionResult> _ListWrapperHRDivisionTypeAsync(long? idHRCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (idHRCompany == null || idHRCompany == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRCompanyDivisionTypeViewModelList
                {
					SessionDetails=SessionDetail,
                    DBModelList = await _hRDivisionTypeServices.GetPagedList(Condition(idHRCompany, searchKey), pageNumber, pageSize, orderingBy, orderingDirection),
                    BreadCrumbTitle = "कार्यालय व्यवस्थापन",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyDivisionType",
                    BreadCrumbActionName = "_ListWrapperHRDivisionTypeAsync",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyDivisionType",
                    CRUDAction = CRUDType.READ,
                    HeaderTitle = "Attendance System Management",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    IdHRCompany = idHRCompany
                });
            }
            catch (Exception exp)
            {
                throw new ArgumentException(exp.Message);
            }
        }

		[AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
		public async Task<ActionResult> _ListHRDivisionTypeAsync(long? idHRCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                if (idHRCompany == null || idHRCompany == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRCompanyDivisionTypeViewModelList
                {
					SessionDetails=SessionDetail,
					DBModelList = await _hRDivisionTypeServices.GetPagedList(Condition(idHRCompany, searchKey), pageNumber, pageSize, orderingBy, orderingDirection),
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyDivisionType",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyDivisionType",
                    BreadCrumbActionName = "_ListHRDivisionTypeAsync",
                    BreadCrumbTitle = "Division/Section Type",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    IdHRCompany = idHRCompany
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadListHRDivisionTypeAsync(long? idHRCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                if (idHRCompany == null || idHRCompany == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRCompanyDivisionTypeViewModelList
                {
                    SessionDetails = SessionDetail,
                    DBModelList = await _hRDivisionTypeServices.GetPagedList(Condition(idHRCompany, searchKey), pageNumber, pageSize, orderingBy, orderingDirection),
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyDivisionType",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyDivisionType",
                    BreadCrumbActionName = "_ReadListHRDivisionTypeAsync",
                    BreadCrumbTitle = "Division/Section Type",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    IdHRCompany = idHRCompany
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
		public async Task<ActionResult> _CreateHRDivisionTypeAsync(long? idHRCompany)
        {
            try
            {
                if (idHRCompany == null || idHRCompany == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                return PartialView(new HRCompanyDivisionTypeViewModel
                {
					SessionDetails=SessionDetail,
					HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyDivisionType",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyDivisionType",
                    BreadCrumbTitle = "Division/Section Type",
                    BreadCrumbActionName = "_CreateHRDivisionTypeAsync",
                    FormModelName = "HRDivisionType",
                    ModalTitle = "नयाँ शाखा/सेक्सन प्रकार थप्नुहोस्",
                    CRUDAction = CRUDType.CREATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Create,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    IdHRCompany = idHRCompany,
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId, idHRCompany),
                    DDLDivisionType = await GetLongStringPairModel(PairModelTitle.ContactType),
                    DataModel = new HRCompanyDivisionTypeModel { Id = 0, IdHRCompany = (long)idHRCompany, IsActive = true }
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

		[AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
		public async Task<ActionResult> _ReadHRDivisionTypeAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var DataModel = await _hRDivisionTypeServices.GetModelByIdAsync(id);
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRCompanyDivisionTypeViewModel
                {
					SessionDetails=SessionDetail,
					HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyDivisionType",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyDivisionType",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_ReadHRDivisionTypeAsync",
                    FormModelName = "HRDivisionType",
                    ModalTitle = "View Division/Section Type Detail",
                    BreadCrumbTitle = "Division/Section Type",
                    CRUDAction = CRUDType.READ,
                    OnlyCancelButton = true,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = DataModel,
                    IdHRCompany = DataModel.IdHRCompany,
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId, DataModel.IdHRCompany)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

		[AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
		public async Task<ActionResult> _UpdateHRDivisionTypeAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var DataModel = await _hRDivisionTypeServices.GetModelByIdAsync(id);
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRCompanyDivisionTypeViewModel
                {
					SessionDetails=SessionDetail,
					HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyDivisionType",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyDivisionType",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_UpdateHRDivisionTypeAsync",
                    FormModelName = "HRDivisionType",
                    ModalTitle = "शाखा/सेक्सन प्रकार सम्पादन गर्नुहोस्",
                    BreadCrumbTitle = "Division/Section Type",
                    CRUDAction = CRUDType.UPDATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Update,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId, DataModel.IdHRCompany),
                    DataModel = DataModel,
                    IdHRCompany = DataModel.IdHRCompany
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

		[AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
		public async Task<ActionResult> _DeleteHRDivisionTypeAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var DataModel = await _hRDivisionTypeServices.GetModelByIdAsync(id);
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRCompanyDivisionTypeViewModel
                {
					SessionDetails=SessionDetail,
					HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyDivisionType",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyDivisionType",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    FormModelName = "HRDivisionType",
                    ModalTitle = "शाखा/सेक्सन प्रकार विवरण मेटाउन निश्चित हुनुहुन्छ?",
                    BreadCrumbActionName = "_DeleteHRDivisionTypeAsync",
                    CRUDAction = CRUDType.DELETE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Delete,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId, DataModel.IdHRCompany),
                    DataModel = DataModel,
                    IdHRCompany = DataModel.IdHRCompany
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

		[AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
		public async Task<ActionResult> _ExportHRDivisionTypeAsync(long? idHRCompany)
        {
            try
            {
                if (idHRCompany == null || idHRCompany == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var model = await _hRDivisionTypeServices.FindAllAsync(Condition(idHRCompany));
               var data= model.Select(x => new
                {
                    Id=x.Id,
                    Office = x.HRCompany.CompanyName,
                    Division_Section = x.HRCompanyDivisionTypeTitle,
                    Count=x.HRCompanyDivisions.Count,

                   
                });
                return await ExportToExcel("Division/SectionTypeList", data);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

		[AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
		public async Task<ActionResult> _PrintHRDivisionTypeAsync(long? idHRCompany)
        {
            try
            {
                if (idHRCompany == null || idHRCompany == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                return PartialView(new HRCompanyDivisionTypeViewModel
                {
					SessionDetails=SessionDetail,
					HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyDivisionType",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyDivisionType",
                    BreadCrumbActionName = "_PrintHRDivisionTypeAsync",
                    FormModelName = "HRDivisionType",
                    ModalTitle = "शाखा/सेक्सन सूची छाप्नुहोस",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBModelList = await _hRDivisionTypeServices.FindAllAsync(Condition(idHRCompany))
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
        public async Task<ActionResult> _CreateHRDivisionTypeAsync(HRCompanyDivisionTypeViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHRDivisionTypeAsync(viewModel, CRUDType.CREATE, formCollection);
        }

		[AuthorizeUser]
        [ValidateInput(true)]
		[ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _UpdateHRDivisionTypeAsync(HRCompanyDivisionTypeViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHRDivisionTypeAsync(viewModel, CRUDType.UPDATE, formCollection);
        }

		[AuthorizeUser]
        [ValidateInput(true)]
		[ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _DeleteHRDivisionTypeAsync(HRCompanyDivisionTypeViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHRDivisionTypeAsync(viewModel, CRUDType.DELETE, formCollection);
        }

        [NonAction]
        protected async Task<ActionResult> RETSourceHRDivisionTypeAsync(HRCompanyDivisionTypeViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
				viewModel.SessionDetails = SessionDetail;
                viewModel.HeaderTitle = "Attendance System Management";
                viewModel.BreadCrumbArea = "CompanyManagement";
                viewModel.BreadCrumbController = "HRCompanyDivisionType";
                viewModel.BreadCrumbBaseURL = "CompanyManagement/HRCompanyDivisionType";
                viewModel.DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId, viewModel.DataModel.IdHRCompany);
                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        viewModel.SubmitButtonText = SubmitButtonText.Create;
                        viewModel.BreadCrumbActionName = "_CreateHRDivisionTypeAsync";
                        viewModel.ModalTitle = "नयाँ शाखा/सेक्सन प्रकार थप्नुहोस्";
                        viewModel.FormModelName = "HRDivisionType";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.UPDATE:
                        viewModel.BreadCrumbActionName = "_UpdateHRDivisionTypeAsync";
                        viewModel.ModalTitle = "शाखा/सेक्सन प्रकार सम्पादन गर्नुहोस्";
                        viewModel.FormModelName = "HRDivisionType";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.DELETE:
                        viewModel.BreadCrumbActionName = "_DeleteHRDivisionTypeAsync";
                        viewModel.ModalTitle = "शाखा/सेक्सन प्रकार विवरण मेटाउन निश्चित हुनुहुन्छ?";
                        viewModel.FormModelName = "HRDivisionType";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.PRINT:
                        viewModel.BreadCrumbActionName = "_PrintHRDivisionTypeAsync";
                        viewModel.ModalTitle = "शाखा/सेक्सन सूची छाप्नुहोस";
                        viewModel.FormModelName = "HRDivisionType";
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
        protected async Task<ActionResult> CRUDHRDivisionTypeAsync(HRCompanyDivisionTypeViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceHRDivisionTypeAsync(viewModel, cRUDType);
                }
                await _hRDivisionTypeServices.CommitSaveChangesAsync(viewModel.DataModel, cRUDType);
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceHRDivisionTypeAsync(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListHRDivisionTypeAsync", new { idHRCompany = viewModel.DataModel.IdHRCompany, pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }
        #endregion
    }
}