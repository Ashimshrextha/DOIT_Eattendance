using AttendanceManagementSystem.App_Auth;
using AttendanceManagementSystem.Controllers;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;
using SystemServices.SystemSecurity;
using SystemViewModels.SystemSecurity;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;

namespace AttendanceManagementSystem.Areas.SystemSecurity.Controllers
{
    public class SystemDatabaseBackupController : BaseController
    {
        // GET: SystemSecurity/SystemDatabaseBackup
        private readonly SystemDatabaseBackupServices _systemDatabaseBackupServices;

        public SystemDatabaseBackupController()
        {
            this._systemDatabaseBackupServices = new SystemDatabaseBackupServices(this._unitOfWork);
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> Index(int? pageNumber, int? pageSize, string orderingDirection, string orderingBy)
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return View(new SystemDatabaseBackupViewModelList
                {
                    DBModelList = await _systemDatabaseBackupServices.GetPagedList(pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    BreadCrumbTitle = "डाटाबेस ब्याकअप",
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "SystemDatabaseBackup",
                    BreadCrumbActionName = "Index",
                    BreadCrumbBaseURL = "SystemSecurity/SystemDatabaseBackup",
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
        public async Task<ActionResult> _ListSystemDatabaseBackupAsync(int? pageNumber, int? pageSize, string orderingDirection, string orderingBy = "Area", string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new SystemDatabaseBackupViewModelList
                {
                    DBModelList = await _systemDatabaseBackupServices.GetBySearchKey(pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, searchKey),
                    BreadCrumbTitle = "Database  Backup",
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "SystemDatabaseBackup",
                    BreadCrumbActionName = "_ListSystemDatabaseBackupAsync",
                    BreadCrumbBaseURL = "SystemSecurity/SystemDatabaseBackup",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = "Title",
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
        public async Task<ActionResult> _CreateSystemDatabaseBackupAsync()
        {
            return await CRUDSystemDatabaseBackup(CRUDType.CREATE);
        }

        [AuthorizeUser]
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadSystemDatabaseBackupAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                return PartialView(new SystemDatabaseBackupViewModel
                {
                    ModalTitle = "डाटाबेस ब्याकअप विवरण हेर्नुहोस्",
                    FormModelName = "SystemDatabaseBackup",
                    BreadCrumbTitle = "Database Backup",
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "SystemDatabaseBackup",
                    BreadCrumbActionName = "_ReadSystemDatabaseBackupAsync",
                    BreadCrumbBaseURL = "SystemSecurity/SystemDatabaseBackup",
                    CRUDAction = CRUDType.READ,
                    HeaderTitle = "Attendance Management System",
                    OnlyCancelButton = true,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = await _systemDatabaseBackupServices.GetModelByIdAsync(id)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [NonAction]
        protected async Task<ActionResult> CRUDSystemDatabaseBackup(CRUDType cRUDType)
        {
            try
            {
                await this._systemDatabaseBackupServices.CreateBackupDatabase();
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
            }
            return RedirectToAction("_ListSystemDatabaseBackupAsync", new { pageNumber = 1, pageSize = 10, orderingBy = "Title", orderingDirection = "DESC", searchKey = "" });
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> DownloadDocumentAsync(long? id)
        {
            try
            {
                var model = await this._systemDatabaseBackupServices.GetByIdAsync(id);
                string targetPath = model.BackupPath;
                string result = Path.GetFileName(targetPath);
                byte[] fileBytes = System.IO.File.ReadAllBytes(targetPath);
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, result);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Download", exp.Message, AlertNotificationType.error);
            }
        }
    }
}
