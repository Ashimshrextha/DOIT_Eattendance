using AttendanceManagementSystem.Controllers;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using SystemViewModels.DeviceManagement;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;

namespace AttendanceManagementSystem.Areas.DeviceManagement.Controllers
{
    public class DeviceLogsFetcherController : BaseController
    {
        // GET: DeviceManagement/DeviceLogsFetcher

        public DeviceLogsFetcherController()
        {

        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> Index(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return View(new DeviceLogsFetcherViewModelList
                {
                    BreadCrumbArea = "DeviceManagement",
                    BreadCrumbController = "DeviceLogsFetcher",
                    BreadCrumbBaseURL = "DeviceManagement/DeviceLogsFetcher",
                    BreadCrumbTitle = "उपकरण लग फेचर",
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
        public async Task<ActionResult> _ListDeviceLogsFetcherAsync(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new DeviceLogsFetcherViewModelList
                {
                    BreadCrumbTitle = "Device Logs Fetcher",
                    BreadCrumbArea = "DeviceManagement",
                    BreadCrumbController = "DeviceLogsFetcher",
                    BreadCrumbActionName = "_ListDeviceLogsFetcherAsync",
                    BreadCrumbBaseURL = "DeviceManagement/DeviceLogsFetcher",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey=searchKey
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }
    }
}