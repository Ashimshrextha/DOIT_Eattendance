using AttendanceManagementSystem.Controllers;
using System;
using System.IO;
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
    public class SystemDetailController : BaseController
    {
        // GET: SystemSecurity/SystemDetail
        private readonly SystemDetailServices _SystemDetailServices;

        public SystemDetailController()
        {
            this._SystemDetailServices = new SystemDetailServices(this._unitOfWork);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> Index(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return View(new SystemDetailViewModelList
                {
                    DBModelList = await _SystemDetailServices.GetPagedList(pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "SystemDetail",
                    BreadCrumbBaseURL = "SystemSecurity/SystemDetail",
                    BreadCrumbTitle = "प्रणाली विवरण",
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
        public async Task<ActionResult> _ListSystemDetailAsync(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new SystemDetailViewModelList
                {
                    DBModelList = await _SystemDetailServices.GetBySearchKey(pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, searchKey),
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "SystemDetail",
                    BreadCrumbBaseURL = "SystemSecurity/SystemDetail",
                    BreadCrumbActionName = "_ListSystemDetailAsync",
                    BreadCrumbTitle = "System Detail",
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
        public async Task<ActionResult> _CreateSystemDetailAsync()
        {
            try
            {
                return PartialView(new SystemDetailViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "SystemDetail",
                    BreadCrumbBaseURL = "SystemSecurity/SystemDetail",
                    BreadCrumbActionName = "_CreateSystemDetailAsync",
                    FormModelName = "SystemDetail",
                    ModalTitle = "नयाँ प्रणालीको विवरण थप्नुहोस्",
                    BreadCrumbTitle = "System Detail",
                    CRUDAction = CRUDType.CREATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Create,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = new SystemDetailModel { Id = 0 },
                    DDLSystemDetailCategory = await GetIntStringPairModel(PairModelTitle.SystemDetailCategory)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadSystemDetailAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new SystemDetailViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "SystemDetail",
                    BreadCrumbBaseURL = "SystemSecurity/SystemDetail",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_ReadSystemDetailAsync",
                    FormModelName = "SystemDetail",
                    ModalTitle = "प्रणाली विवरण हेर्नुहोस्",
                    BreadCrumbTitle = "System Detail",
                    CRUDAction = CRUDType.READ,
                    OnlyCancelButton = true,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = await _SystemDetailServices.GetModelByIdAsync(id),
                    DDLSystemDetailCategory = await GetIntStringPairModel(PairModelTitle.SystemDetailCategory)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _UpdateSystemDetailAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new SystemDetailViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "SystemDetail",
                    BreadCrumbBaseURL = "SystemSecurity/SystemDetail",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    BreadCrumbActionName = "_UpdateSystemDetailAsync",
                    FormModelName = "SystemDetail",
                    ModalTitle = "प्रणालीको विवरण सम्पादन गर्नुहोस्",
                    BreadCrumbTitle = "System Detail",
                    CRUDAction = CRUDType.UPDATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Update,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = await _SystemDetailServices.GetModelByIdAsync(id),
                    DDLSystemDetailCategory = await GetIntStringPairModel(PairModelTitle.SystemDetailCategory)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _DeleteSystemDetailAsync(int? id, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new SystemDetailViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "SystemDetail",
                    BreadCrumbBaseURL = "SystemSecurity/SystemDetail",
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    SearchKey = searchKey,
                    FormModelName = "SystemDetail",
                    ModalTitle = "प्रणाली विवरण मेटाउन निश्चित हुनुहुन्छ?",
                    BreadCrumbTitle = "System Detail",
                    BreadCrumbActionName = "_DeleteSystemDetailAsync",
                    CRUDAction = CRUDType.DELETE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Delete,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DataModel = await _SystemDetailServices.GetModelByIdAsync(id),
                    DDLSystemDetailCategory = await GetIntStringPairModel(PairModelTitle.SystemDetailCategory)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ExportSystemDetailAsync()
        {
            try
            {
                var model = await _SystemDetailServices.FindAllAsync(x => true);
                return await ExportToExcel("SystemDetail", model);
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintSystemDetailAsync()
        {
            try
            {
                return PartialView(new SystemDetailViewModel
                {
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "SystemSecurity",
                    BreadCrumbController = "SystemDetail",
                    BreadCrumbBaseURL = "SystemSecurity/SystemDetail",
                    BreadCrumbActionName = "_PrintSystemDetailAsync",
                    FormModelName = "SystemDetail",
                    ModalTitle = "प्रणाली विवरण सुची छाप्नुहोस्",
                    BreadCrumbTitle = "System Detail",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonType = SubmitButtonType.button,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DBModelList = await _SystemDetailServices.FindAllAsync(x => true)
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
        public async Task<ActionResult> _CreateSystemDetailAsync(SystemDetailViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDSystemDetail(viewModel, CRUDType.CREATE, formCollection);
        }

        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _UpdateSystemDetailAsync(SystemDetailViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDSystemDetail(viewModel, CRUDType.UPDATE, formCollection);
        }

        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _DeleteSystemDetailAsync(SystemDetailViewModel viewModel, FormCollection formCollection)
        {
            return await CRUDSystemDetail(viewModel, CRUDType.DELETE, formCollection);
        }

        [NonAction]
        protected async Task<ActionResult> RETSourceSystemDetail(SystemDetailViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
                viewModel.BreadCrumbArea = "SystemSecurity";
                viewModel.BreadCrumbController = "SystemDetail";
                viewModel.BreadCrumbBaseURL = "SystemSecurity/SystemDetail";
                viewModel.DDLSystemDetailCategory = await GetIntStringPairModel(PairModelTitle.SystemDetailCategory);
                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        viewModel.SubmitButtonText = SubmitButtonText.Create;
                        viewModel.BreadCrumbActionName = "_CreateSystemDetailAsync";
                        viewModel.ModalTitle = "नयाँ प्रणालीको विवरण थप्नुहोस्";
                        viewModel.FormModelName = "SystemDetail";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.UPDATE:
                        viewModel.BreadCrumbActionName = "_UpdateSystemDetailAsync";
                        viewModel.ModalTitle = "प्रणालीको विवरण सम्पादन गर्नुहोस्";
                        viewModel.FormModelName = "SystemDetail";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.DELETE:
                        viewModel.BreadCrumbActionName = "_DeleteSystemDetailAsync";
                        viewModel.ModalTitle = "प्रणाली विवरण मेटाउन निश्चित हुनुहुन्छ?";
                        viewModel.FormModelName = "SystemDetail";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.PRINT:
                        viewModel.BreadCrumbActionName = "_PrintSystemDetailAsync";
                        viewModel.ModalTitle = "प्रणाली विवरण सुची छाप्नुहोस्";
                        viewModel.FormModelName = "SystemDetail";
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
        protected async Task<ActionResult> CRUDSystemDetail(SystemDetailViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                if (viewModel.FileToUpload != null)
                {
                    string serverPath = Server.MapPath("~");
                    string DocName = viewModel.FileToUpload.FileName;
                    string DocPath = serverPath + "FileUpload\\";
                    if (!(new DirectoryInfo(DocPath).Exists)) Directory.CreateDirectory(DocPath);
                    string targetPath = Path.Combine(DocPath, DocName);
                    viewModel.DataModel.Document = DocName;
                    viewModel.FileToUpload.SaveAs(targetPath);
                }
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceSystemDetail(viewModel, cRUDType);
                }
                await _SystemDetailServices.CommitSaveChangesAsync(viewModel.DataModel, cRUDType);
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceSystemDetail(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListSystemDetailAsync", new { pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }

        public async Task <ActionResult> DownloadDocFiles(int? id)
        {
            var DataModel = await _SystemDetailServices.GetModelByIdAsync(id);
            var  DocName = DataModel.Document;
            string targetFolder = Server.MapPath("~") + "FileUpload\\" ;
            string targetPath = Path.Combine(targetFolder, DocName);
            string result = Path.GetFileName(targetPath);
            byte[] fileBytes = System.IO.File.ReadAllBytes(targetPath);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, result);
        }
    }
}