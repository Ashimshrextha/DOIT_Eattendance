using AttendanceManagementSystem.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SystemServices.Reports;
using SystemViewModels.Reports;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;

namespace AttendanceManagementSystem.Areas.Reports.Controllers
{
    public class UserLogsController : BaseController
    {
        // GET: Reports/UserLogs
        private readonly UserLogsServices _userLogsServices;

        public UserLogsController()
        {
            this._userLogsServices = new UserLogsServices(this._unitOfWork);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> Index(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return View(new UserLogsViewModelList
                {
                    DBModelList = await _userLogsServices.GetPagedList(pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "UserLogs",
                    BreadCrumbBaseURL = "Reports/UserLogs",
                    BreadCrumbTitle = "प्रयोगकर्ता लगहरू",
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

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListUserLogsAsync(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new UserLogsViewModelList
                {
                    DBModelList = await _userLogsServices.GetBySearchKey(pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, searchKey = ""),
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "UserLogs",
                    BreadCrumbBaseURL = "Reports/UserLogs",
                    BreadCrumbActionName = "_ListUserLogsAsync",
                    BreadCrumbTitle = "User Logs",
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

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ExportUserLogsAsync()
        {
            try
            {
                var model = await _userLogsServices.FindAllAsync(x => x.IdLogin != null);
                return await ExportToExcel("UserLogs", model);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintUserLogsAsync()
        {
            try
            {
                return PartialView(new UserLogsViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "Reports",
                    BreadCrumbController = "UserLogs",
                    BreadCrumbBaseURL = "Reports/UserLogs",
                    BreadCrumbActionName = "_PrintUserLogsAsync",
                    FormModelName = "UserLogs",
                    ModalTitle = "प्रयोगकर्ता लगहरूको सुची छाप्नुहोस्",
                    BreadCrumbTitle = "User Logs",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBModelList = await _userLogsServices.FindAllAsync(x => x.IdLogin != null)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }
    }
}