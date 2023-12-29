using AttendanceManagementSystem.App_Auth;
using AttendanceManagementSystem.Controllers;
using System;
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
    public class HRDeviceController : BaseController
    {
        // GET: CompanyManagement/HRDevice
        private readonly HRDeviceServices _hRDeviceServices;
        public HRDeviceController()
        {
            this._hRDeviceServices = new HRDeviceServices(this._unitOfWork);
        }

        #region HRDevice
        [NonAction]
        public Expression<Func<HRDevice, bool>> Condition(long? idHRCompany, string searchKey = "")
        {
            Expression<Func<HRDevice, bool>> retutndata = (x => false);

            if (this.SessionDetail.IdRoleType == 0) retutndata = (x => x.IdHRCompany == idHRCompany && (x.HRDeviceName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));

            else if (this.SessionDetail.IdRoleType == 1 || SessionDetail.IdRoleType == 2 || SessionDetail.IdRoleType == 4) retutndata = (x => x.IdHRCompany == idHRCompany && (x.HRDeviceName.ToUpper().Contains(searchKey.ToString().ToUpper()) || searchKey == ""));

            else retutndata = (x => false);

            return retutndata;
        }

        [AuthorizeUser(ActionName = "Index")]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListHRDeviceAsync(long? idHRCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                if (idHRCompany == 0 || idHRCompany == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRDeviceViewModelList
                {
                    SessionDetails = SessionDetail,
                    DBModelList = await _hRDeviceServices.GetPagedList(Condition(idHRCompany, searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    BreadCrumbTitle = "Attendance Device",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRDevice",
                    BreadCrumbActionName = "_ListHRDeviceAsync",
                    BreadCrumbBaseURL = "CompanyManagement/HRDevice",
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
        public async Task<ActionResult> _ReadListHRDeviceAsync(long? idHRCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                if (idHRCompany == 0 || idHRCompany == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRDeviceViewModelList
                {
                    SessionDetails = SessionDetail,
                    DBModelList = await _hRDeviceServices.GetPagedList(Condition(idHRCompany, searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    BreadCrumbTitle = "Attendance Device",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRDevice",
                    BreadCrumbActionName = "_ReadListHRDeviceAsync",
                    BreadCrumbBaseURL = "CompanyManagement/HRDevice",
                    CRUDAction = CRUDType.READ,
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
        public async Task<ActionResult> _ListWrapperHRDeviceAsync(long? idHRCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (idHRCompany == 0 || idHRCompany == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRDeviceViewModelList
                {
                    SessionDetails = SessionDetail,
                    DBModelList = await _hRDeviceServices.GetPagedList(Condition(idHRCompany, searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    BreadCrumbTitle = "Attendance Device",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRDevice",
                    BreadCrumbActionName = "_ListWrapperHRDeviceAsync",
                    BreadCrumbBaseURL = "CompanyManagement/HRDevice",
                    CRUDAction = CRUDType.READ,
                    HeaderTitle = "Attendance System Management",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    IdHRCompany = idHRCompany
                });
            }
            catch (Exception exp)
            {
                throw new ArgumentException(exp.Message);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _CreateHRDeviceAsync(long? idHRCompany)
        {
            try
            {
                if (idHRCompany == 0 || idHRCompany == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                return PartialView(new HRDeviceViewModel
                {
                    SessionDetails = SessionDetail,
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRDevice",
                    BreadCrumbBaseURL = "CompanyManagement/HRDevice",
                    BreadCrumbTitle = "Attendance Device",
                    BreadCrumbActionName = "_CreateHRDeviceAsync",
                    FormModelName = "HRDevice",
                    ModalTitle = "नयाँ उपकरण थप्नुहोस",
                    CRUDAction = CRUDType.CREATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Create,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    IdHRCompany = idHRCompany,
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId, idHRCompany),
                    DDLDeviceModel = await GetIntStringPairModel(PairModelTitle.DeviceModel),
                    DataModel = new HRDevicesModel { Id = Guid.Empty }
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadHRDeviceAsync(Guid id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == Guid.Empty)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var DataModel = await _hRDeviceServices.GetModelByIdAsync(id);
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRDeviceViewModel
                {
                    SessionDetails = SessionDetail,
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRDevice",
                    BreadCrumbBaseURL = "CompanyManagement/HRDevice",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_ReadHRDeviceAsync",
                    FormModelName = "HRDevice",
                    ModalTitle = "उपकरण विवरण हेर्नुहोस",
                    BreadCrumbTitle = "Attendance Device",
                    CRUDAction = CRUDType.READ,
                    OnlyCancelButton = true,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId, DataModel.IdHRCompany),
                    DDLDeviceModel = await GetIntStringPairModel(PairModelTitle.DeviceModel),
                    DataModel = DataModel
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _UpdateHRDeviceAsync(Guid id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == Guid.Empty)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var DataModel = await _hRDeviceServices.GetModelByIdAsync(id);
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRDeviceViewModel
                {
                    SessionDetails = SessionDetail,
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRDevice",
                    BreadCrumbBaseURL = "CompanyManagement/HRDevice",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_UpdateHRDeviceAsync",
                    FormModelName = "HRDevice",
                    ModalTitle = "उपकरण सम्पादन गर्नुहोस",
                    BreadCrumbTitle = "Attendance Device",
                    CRUDAction = CRUDType.UPDATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Update,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId, DataModel.IdHRCompany),
                    DDLDeviceModel = await GetIntStringPairModel(PairModelTitle.DeviceModel),
                    DataModel = DataModel
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _DeleteHRDeviceAsync(Guid id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == Guid.Empty)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var DataModel = await _hRDeviceServices.GetModelByIdAsync(id);
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRDeviceViewModel
                {
                    SessionDetails = SessionDetail,
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRDevice",
                    BreadCrumbBaseURL = "CompanyManagement/HRDevice",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    FormModelName = "HRDevice",
                    ModalTitle = "उपकरण मेटाउनुहोस्",
                    BreadCrumbTitle = "Attendance Device",
                    BreadCrumbActionName = "_DeleteHRDeviceAsync",
                    CRUDAction = CRUDType.DELETE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Delete,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId, DataModel.IdHRCompany),
                    DDLDeviceModel = await GetIntStringPairModel(PairModelTitle.DeviceModel),
                    DataModel = DataModel
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ExportHRDeviceAsync(long? idHRCompany)
        {
            try
            {
                if (idHRCompany == 0 || idHRCompany == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var model = await _hRDeviceServices.FindAllAsync(Condition(idHRCompany));
                var data = model.Select(x => new
                {
                    Company=x.HRCompany?.HRCompanyCode?.HRCompanyCodeTitle,
                    DeviceNumber=x.DeviceNumber,
                    Name=x.HRDeviceName,
                    Model=x.HRDeviceModel.HRDeviceModel1,
                    IP =x.IPAddress,
                    Port=x.CommunicationPort,
                    Register=x.IsRegister.ToString(),
                    AutoPush=x.HRDeviceModel.IsRealTime.ToString(),
                });
                return await ExportToExcel("AttendanceDevice", data);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintHRDeviceAsync(long? idHRCompany)
        {
            try
            {
                if (idHRCompany == 0 || idHRCompany == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                return PartialView(new HRDeviceViewModel
                {
                    SessionDetails = SessionDetail,
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRDevice",
                    BreadCrumbBaseURL = "CompanyManagement/HRDevice",
                    BreadCrumbActionName = "_PrintHRDeviceAsync",
                    FormModelName = "HRDevice",
                    ModalTitle = "उपकरण सुची छाप्नुहोस्",
                    BreadCrumbTitle = "Attendance Device",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBModelList = await _hRDeviceServices.FindAllAsync(Condition(idHRCompany))
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
        public async Task<ActionResult> _CreateHRDeviceAsync(HRDeviceViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHRDeviceAsync(viewModel, CRUDType.CREATE, formCollection);
        }

        [AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _UpdateHRDeviceAsync(HRDeviceViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHRDeviceAsync(viewModel, CRUDType.UPDATE, formCollection);
        }

        [AuthorizeUser]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _DeleteHRDeviceAsync(HRDeviceViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHRDeviceAsync(viewModel, CRUDType.DELETE, formCollection);
        }

        [NonAction]
        protected async Task<ActionResult> RETSourceHRDeviceAsync(HRDeviceViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
                viewModel.SessionDetails = SessionDetail;
                viewModel.HeaderTitle = "Attendance System Management";
                viewModel.BreadCrumbArea = "CompanyManagement";
                viewModel.BreadCrumbController = "HRDevice";
                viewModel.BreadCrumbBaseURL = "CompanyManagement/HRDevice";
                viewModel.DDLCompany = await GetLongStringPairModel(PairModelTitle.CompanyId, viewModel.DataModel.IdHRCompany);
                viewModel.DDLDeviceModel = await GetIntStringPairModel(PairModelTitle.DeviceModel);
                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        viewModel.SubmitButtonText = SubmitButtonText.Create;
                        viewModel.BreadCrumbActionName = "_CreateHRDeviceAsync";
                        viewModel.ModalTitle = "नयाँ उपकरण थप्नुहोस";
                        viewModel.FormModelName = "HRDevice";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.UPDATE:
                        viewModel.BreadCrumbActionName = "_UpdateHRDeviceAsync";
                        viewModel.ModalTitle = "उपकरण सम्पादन गर्नुहोस";
                        viewModel.FormModelName = "HRDevice";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.DELETE:
                        viewModel.BreadCrumbActionName = "_DeleteHRDeviceAsync";
                        viewModel.ModalTitle = "उपकरण मेटाउनुहोस्";
                        viewModel.FormModelName = "HRDevice";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.PRINT:
                        viewModel.BreadCrumbActionName = "_PrintHRDeviceAsync";
                        viewModel.ModalTitle = "उपकरण सुची छाप्नुहोस्";
                        viewModel.FormModelName = "HRDevice";
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
        protected async Task<ActionResult> CRUDHRDeviceAsync(HRDeviceViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceHRDeviceAsync(viewModel, cRUDType);
                }
                if (cRUDType == CRUDType.CREATE)
                {
                    viewModel.DataModel.Id = Guid.NewGuid();
                }
                await _hRDeviceServices.CommitSaveChangesAsync(viewModel.DataModel, cRUDType);
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceHRDeviceAsync(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListHRDeviceAsync", new { idHRCompany = viewModel.DataModel.IdHRCompany, pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }
        #endregion
    }
}