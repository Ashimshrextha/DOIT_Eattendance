using AttendanceManagementSystem.App_Auth;
using AttendanceManagementSystem.Controllers;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using SystemModels.SystemSecurity;
using SystemServices.SystemSecurity;
using SystemViewModels.SystemSecurity;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;

namespace AttendanceManagementSystem.Areas.SystemSecurity.Controllers
{
    public class HRCalendarController : BaseController
    {
        // GET: SystemSecurity/HRCalendar
        private readonly HRCalendarServices _HRCalendarServices;

        public HRCalendarController()
        {
            this._HRCalendarServices = new HRCalendarServices(this._unitOfWork);
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> Index(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, "NepDate", orderingDirection);
                return View(new HRCalendarViewModelList
                {
                    DBModelList = await _HRCalendarServices.GetPagedList(pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "HRCalendar",
                    BreadCrumbBaseURL = "SystemSecurity/HRCalendar",
                    BreadCrumbTitle = "क्यालेन्डर परिर्वतन",
                    BreadCrumbActionName = "Index",
                    CRUDAction = CRUDType.READ,
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
        public async Task<ActionResult> _ListHRCalendarAsync(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, "NepDate", orderingDirection);
                return PartialView(new HRCalendarViewModelList
                {
                    DBModelList = await _HRCalendarServices.GetBySearchKey(pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, searchKey),
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "HRCalendar",
                    BreadCrumbBaseURL = "SystemSecurity/HRCalendar",
                    BreadCrumbActionName = "_ListHRCalendarAsync",
                    BreadCrumbTitle = "Calender Conversion",
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
        public async Task<ActionResult> _CreateHRCalendarAsync()
        {
            try
            {
                return PartialView(new HRCalendarViewModel
                {
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "HRCalendar",
                    BreadCrumbBaseURL = "SystemSecurity/HRCalendar",
                    BreadCrumbActionName = "_CreateHRCalendarAsync",
                    FormModelName = "HRCalendar",
                    ModalTitle = "नयाँ क्यालेन्डर रूपान्तरण गर्नुहोस्",
                    CRUDAction = CRUDType.CREATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Create,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = new HRCalendarModel { Id = 0 },
                    DDLReligion = await this.GetIntStringPairModel(PairModelTitle.Religion)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadHRCalendarAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRCalendarViewModel
                {
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "HRCalendar",
                    BreadCrumbBaseURL = "SystemSecurity/HRCalendar",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_ReadHRCalendarAsync",
                    FormModelName = "HRCalendar",
                    ModalTitle = "क्यालेन्डर रूपान्तरण विवरण हेर्नुहोस्",
                    BreadCrumbTitle = "Calender Conversion",
                    CRUDAction = CRUDType.READ,
                    OnlyCancelButton = true,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = await _HRCalendarServices.GetModelByIdAsync(id),
                    DDLReligion = await this.GetIntStringPairModel(PairModelTitle.Religion)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _UpdateHRCalendarAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRCalendarViewModel
                {
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "HRCalendar",
                    BreadCrumbBaseURL = "SystemSecurity/HRCalendar",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_UpdateHRCalendarAsync",
                    FormModelName = "HRCalendar",
                    ModalTitle = "क्यालेन्डर रूपान्तरण सम्पादन गर्नुहोस्",
                    BreadCrumbTitle = "Calender Conversion",
                    CRUDAction = CRUDType.UPDATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Update,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = await _HRCalendarServices.GetModelByIdAsync(id),
                    DDLReligion = await this.GetIntStringPairModel(PairModelTitle.Religion)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _DeleteHRCalendarAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRCalendarViewModel
                {
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "HRCalendar",
                    BreadCrumbBaseURL = "SystemSecurity/HRCalendar",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    FormModelName = "HRCalendar",
                    ModalTitle = "क्यालेन्डर रूपान्तरण मेटाउन निश्चित हुनुहुन्छ?",
                    BreadCrumbTitle = "Calender Conversion",
                    BreadCrumbActionName = "_DeleteHRCalendarAsync",
                    CRUDAction = CRUDType.DELETE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Delete,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = await _HRCalendarServices.GetModelByIdAsync(id),
                    DDLReligion = await this.GetIntStringPairModel(PairModelTitle.Religion)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ExportHRCalendarAsync()
        {
            try
            {
                var model = await _HRCalendarServices.FindAllAsync(x => true);
                return await ExportToExcel("HRCalendar", model);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintHRCalendarAsync()
        {
            try
            {
                return PartialView(new HRCalendarViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "HRCalendar",
                    BreadCrumbBaseURL = "SystemSecurity/HRCalendar",
                    BreadCrumbActionName = "_PrintHRCalendarAsync",
                    FormModelName = "HRCalendar",
                    ModalTitle = "क्यालेन्डर रूपान्तरण सुची छाप्नुहोस्",
                    BreadCrumbTitle = "Calender Conversion",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBModelList = await _HRCalendarServices.FindAllAsync(x => true)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _CreateHRCalendarAsync(HRCalendarViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHRCalendar(viewModel, CRUDType.CREATE, formCollection);
        }

        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _UpdateHRCalendarAsync(HRCalendarViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHRCalendar(viewModel, CRUDType.UPDATE, formCollection);
        }

        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _DeleteHRCalendarAsync(HRCalendarViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDHRCalendar(viewModel, CRUDType.DELETE, formCollection);
        }

        [NonAction]
        protected async Task<ActionResult> RETSourceHRCalendar(HRCalendarViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
                viewModel.BreadCrumbArea = "SystemSecurity";
                viewModel.BreadCrumbController = "HRCalendar";
                viewModel.BreadCrumbBaseURL = "SystemSecurity/HRCalendar";
                viewModel.DDLReligion = await this.GetIntStringPairModel(PairModelTitle.Religion);
                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        viewModel.SubmitButtonText = SubmitButtonText.Create;
                        viewModel.CRUDAction = CRUDType.CREATE;
                        viewModel.BreadCrumbActionName = "_CreateHRCalendarAsync";
                        viewModel.ModalTitle = "नयाँ क्यालेन्डर रूपान्तरण गर्नुहोस्";
                        viewModel.FormModelName = "HRCalendar";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.UPDATE:
                        viewModel.SubmitButtonText = SubmitButtonText.Update;
                        viewModel.CRUDAction = CRUDType.UPDATE;
                        viewModel.BreadCrumbActionName = "_UpdateHRCalendarAsync";
                        viewModel.ModalTitle = "क्यालेन्डर रूपान्तरण सम्पादन गर्नुहोस्";
                        viewModel.FormModelName = "HRCalendar";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.DELETE:
                        viewModel.SubmitButtonText = SubmitButtonText.Delete;
                        viewModel.CRUDAction = CRUDType.DELETE;
                        viewModel.BreadCrumbActionName = "_DeleteHRCalendarAsync";
                        viewModel.ModalTitle = "क्यालेन्डर रूपान्तरण मेटाउन निश्चित हुनुहुन्छ?";
                        viewModel.FormModelName = "HRCalendar";
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
        protected async Task<ActionResult> CRUDHRCalendar(HRCalendarViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceHRCalendar(viewModel, cRUDType);
                }
                var data = await _HRCalendarServices.Exits(x => x.NepDate == viewModel.DataModel.NepDate);
                if (data == true && CRUDType.CREATE == cRUDType)
                {
                    Response.StatusCode = 350;
                    ModelState.AddModelError("", "Already Exists!!!");
                    return await RETSourceHRCalendar(viewModel, cRUDType);
                }
                var dateNP = viewModel.DataModel.NepDate.Split('-');
                viewModel.DataModel.NepYear = int.Parse(dateNP[0]);
                viewModel.DataModel.NepMonth = int.Parse(dateNP[1]);
                viewModel.DataModel.NepDay = int.Parse(dateNP[2]);
                viewModel.DataModel.EngYear = viewModel.DataModel.EngDate.Year;
                viewModel.DataModel.EngMonth = viewModel.DataModel.EngDate.Month;
                viewModel.DataModel.EngDay = viewModel.DataModel.EngDate.Day;
                await _HRCalendarServices.CommitSaveChangesAsync(viewModel.DataModel, cRUDType);
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceHRCalendar(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListHRCalendarAsync", new { pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }
    }
}