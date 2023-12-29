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
using SystemModels.SystemSetting;
using SystemServices.SystemSetting;
using SystemViewModels.SystemSetting;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;

namespace AttendanceManagementSystem.Areas.CompanyManagement.Controllers
{
    public class HREmployeeCategoryController : BaseController
    {
        // GET: CompanyManagement/HREmployeeCategory
        private readonly HREmployeeCategoryServices _hREmployeeCategoryServices;


        public HREmployeeCategoryController()
        {
            this._hREmployeeCategoryServices = new HREmployeeCategoryServices(this._unitOfWork);
        }

		#region Employee Category
		[NonAction]
		public Expression<Func<HREmployeeCategory, bool>> Condition(long? idHRCompany, string searchKey = "")
		{
			Expression<Func<HREmployeeCategory, bool>> retutndata = (x => false);

			if (this.SessionDetail.IdRoleType == 0) retutndata = (x => x.IdHRCompany == idHRCompany && (x.HREmployeeCategoryTitle.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));

			else if (this.SessionDetail.IdRoleType == 1 || SessionDetail.IdRoleType == 2 || SessionDetail.IdRoleType == 4) retutndata = (x => x.IdHRCompany == idHRCompany && (x.HREmployeeCategoryTitle.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));

			else retutndata = (x => false);

			return retutndata;
		}
		[AuthorizeUser(ActionName ="Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListWrapperHREmployeeCategoryAsync(long? idHRCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                if (idHRCompany == null || idHRCompany == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HREmployeeCategoryViewModelList
                {
                    IdHRCompany = idHRCompany,
					SessionDetails = SessionDetail,
					DBModelList = await _hREmployeeCategoryServices.GetPagedList(Condition(idHRCompany,searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HREmployeeCategory",
                    BreadCrumbBaseURL = "CompanyManagement/HREmployeeCategory",
                    BreadCrumbActionName = "_ListWrapperHREmployeeCategoryAsync",
                    BreadCrumbTitle = "Employee Category Type",
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

		[AuthorizeUser(ActionName ="Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListHREmployeeCategoryAsync(long? idHRCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                if (idHRCompany == null || idHRCompany == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HREmployeeCategoryViewModelList
                {
					SessionDetails = SessionDetail,
					DBModelList = await _hREmployeeCategoryServices.GetPagedList(Condition(idHRCompany,searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HREmployeeCategory",
                    BreadCrumbBaseURL = "CompanyManagement/HREmployeeCategory",
                    BreadCrumbActionName = "_ListHREmployeeCategoryAsync",
                    BreadCrumbTitle = "Employee Category",
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
        public async Task<ActionResult> _ReadListHREmployeeCategoryAsync(long? idHRCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                if (idHRCompany == null || idHRCompany == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HREmployeeCategoryViewModelList
                {
                    SessionDetails = SessionDetail,
                    DBModelList = await _hREmployeeCategoryServices.GetPagedList(Condition(idHRCompany, searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HREmployeeCategory",
                    BreadCrumbBaseURL = "CompanyManagement/HREmployeeCategory",
                    BreadCrumbActionName = "_ReadListHREmployeeCategoryAsync",
                    BreadCrumbTitle = "Employee Category",
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
        public async Task<ActionResult> _CreateHREmployeeCategoryAsync(long? idHRCompany)
        {
            try
            {
                if (idHRCompany == null || idHRCompany == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                return PartialView(new HREmployeeCategoryViewModel
                {
					SessionDetails = SessionDetail,
					HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HREmployeeCategory",
                    BreadCrumbBaseURL = "CompanyManagement/HREmployeeCategory",
                    BreadCrumbActionName = "_CreateHREmployeeCategoryAsync",
                    FormModelName = "HREmployeeCategory",
                    ModalTitle = "नयाँ कर्मचारी प्रकार थप्नुहोस",
                    CRUDAction = CRUDType.CREATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Create,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId, idHRCompany),
                    DataModel = new HREmployeeCategoryModel { Id = 0, IdHRCompany = (long)idHRCompany },
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
        public async Task<ActionResult> _ReadHREmployeeCategoryAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _hREmployeeCategoryServices.GetModelByIdAsync(id);
                return PartialView(new HREmployeeCategoryViewModel
                {
					SessionDetails = SessionDetail,
					HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HREmployeeCategory",
                    BreadCrumbBaseURL = "CompanyManagement/HREmployeeCategory",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_ReadHREmployeeCategoryAsync",
                    FormModelName = "HREmployeeCategory",
                    ModalTitle = "कर्मचारी प्रकार विवरण हेर्नुहोस्",
                    BreadCrumbTitle = "Employee Category Type",
                    CRUDAction = CRUDType.READ,
                    OnlyCancelButton = true,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = model,
                    IdHRCompany = model.IdHRCompany,
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId, model.IdHRCompany)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

		[AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _UpdateHREmployeeCategoryAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _hREmployeeCategoryServices.GetModelByIdAsync(id);
                return PartialView(new HREmployeeCategoryViewModel
                {
					SessionDetails = SessionDetail,
					HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HREmployeeCategory",
                    BreadCrumbBaseURL = "CompanyManagement/HREmployeeCategory",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_UpdateHREmployeeCategoryAsync",
                    FormModelName = "HREmployeeCategory",
                    ModalTitle = "कर्मचारी प्रकार सम्पादन गर्नुहोस",
                    BreadCrumbTitle = "Employee Category",
                    CRUDAction = CRUDType.UPDATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Update,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = model,
                    IdHRCompany = model.IdHRCompany,
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId, model.IdHRCompany)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

		[AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _DeleteHREmployeeCategoryAsync(long? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var model = await _hREmployeeCategoryServices.GetModelByIdAsync(id);
                return PartialView(new HREmployeeCategoryViewModel
                {
					SessionDetails = SessionDetail,
					HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HREmployeeCategory",
                    BreadCrumbBaseURL = "CompanyManagement/HREmployeeCategory",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    FormModelName = "HREmployeeCategory",
                    ModalTitle = "कर्मचारी प्रकार मेटाउन निश्चित हुनुहुन्छ?",
                    BreadCrumbTitle = "Employee Category",
                    BreadCrumbActionName = "_DeleteHREmployeeCategoryAsync",
                    CRUDAction = CRUDType.DELETE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Delete,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = model,
                    IdHRCompany = model.IdHRCompany,
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId, model.IdHRCompany)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

		[AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ExportHREmployeeCategoryAsync(long? idHRCompany)
        {
            try
            {
                if (idHRCompany == null || idHRCompany == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var model = await _hREmployeeCategoryServices.FindAllAsync(Condition(idHRCompany));
              var data=  model.Select(x => new
                {
                    Office = x.HRCompany.CompanyName,
                    EmployeeCategory = x.HREmployeeCategoryTitle,
                    HigherLevelOfficier = x.IsHigherLevelOfficer.ToString(),
                    MiddleLevelOfficier = x.IsMidLevelOfficer.ToString(),
                    LowLevelOfficier = x.IsLowLevelOfficer.ToString(),
                    Abbreviation = x.HREmployeeCategoryShortName,
                    OrderBy = x.HREmployeeCategoryOrder,
                    
                });
                return await ExportToExcel("HREmployeeCategory", data);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

		[AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintHREmployeeCategoryAsync(long? idHRCompany)
        {
            try
            {
                if (idHRCompany == null || idHRCompany == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                return PartialView(new HREmployeeCategoryViewModel
                {
					SessionDetails = SessionDetail,
					HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HREmployeeCategory",
                    BreadCrumbBaseURL = "CompanyManagement/HREmployeeCategory",
                    BreadCrumbActionName = "_PrintHREmployeeCategoryAsync",
                    FormModelName = "HREmployeeCategory",
                    ModalTitle = "कर्मचारी प्रकार सुची छाप्नुहोस",
                    BreadCrumbTitle = "Employee Category Type",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    IdHRCompany = idHRCompany,
                    DBModelList = await _hREmployeeCategoryServices.FindAllAsync(Condition(idHRCompany))

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
        public async Task<ActionResult> _CreateHREmployeeCategoryAsync(HREmployeeCategoryViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeCategory(viewModel, CRUDType.CREATE, formCollection);
        }

		[AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _UpdateHREmployeeCategoryAsync(HREmployeeCategoryViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeCategory(viewModel, CRUDType.UPDATE, formCollection);
        }

		[AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _DeleteHREmployeeCategoryAsync(HREmployeeCategoryViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHREmployeeCategory(viewModel, CRUDType.DELETE, formCollection);
        }

        [NonAction]
        protected async Task<ActionResult> RETSourceHREmployeeCategory(HREmployeeCategoryViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
				viewModel.SessionDetails = SessionDetail;
                viewModel.BreadCrumbArea = "CompanyManagement";
                viewModel.BreadCrumbController = "HREmployeeCategory";
                viewModel.BreadCrumbBaseURL = "CompanyManagement/HREmployeeCategory";
                viewModel.IdHRCompany = viewModel.DataModel.IdHRCompany;
                viewModel.DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId, viewModel.DataModel.IdHRCompany);
                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        viewModel.SubmitButtonText = SubmitButtonText.Create;
                        viewModel.BreadCrumbActionName = "_CreateHREmployeeCategoryAsync";
                        viewModel.ModalTitle = "नयाँ कर्मचारी प्रकार थप्नुहोस";
                        viewModel.FormModelName = "HREmployeeCategory";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.UPDATE:
                        viewModel.BreadCrumbActionName = "_UpdateHREmployeeCategoryAsync";
                        viewModel.ModalTitle = "कर्मचारी प्रकार सम्पादन गर्नुहोस";
                        viewModel.FormModelName = "HREmployeeCategory";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.DELETE:
                        viewModel.BreadCrumbActionName = "_DeleteHREmployeeCategoryAsync";
                        viewModel.ModalTitle = "कर्मचारी प्रकार मेटाउन निश्चित हुनुहुन्छ?";
                        viewModel.FormModelName = "HREmployeeCategory";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.PRINT:
                        viewModel.BreadCrumbActionName = "_PrintHREmployeeCategoryAsync";
                        viewModel.ModalTitle = "कर्मचारी प्रकार सुची छाप्नुहोस";
                        viewModel.FormModelName = "HREmployeeCategory";
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
        protected async Task<ActionResult> CRUDHREmployeeCategory(HREmployeeCategoryViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceHREmployeeCategory(viewModel, cRUDType);
                }
                await _hREmployeeCategoryServices.CommitSaveChangesAsync(viewModel.DataModel, cRUDType);
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceHREmployeeCategory(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListHREmployeeCategoryAsync", new { idHRCompany = viewModel.DataModel.IdHRCompany, pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }

        #endregion
    }
}