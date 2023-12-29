using AttendanceManagementSystem.Controllers;
using DeviceManagement.RealandUDPRepository;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using SystemModels.DeviceManagement;
using SystemServices.DeviceManagement;
using SystemViewModels.DeviceManagement;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;


namespace AttendanceManagementSystem.Areas.DeviceManagement.Controllers
{
    public class HREmployeeBiometricTemplateController : BaseController
    {
        // GET: DeviceManagement/HREmployeeBiometricTemplate

        private readonly HREmployeeBiometricTemplateServices _HREmployeeBiometricTemplateServices;
        private readonly RealandUDPRepository _realandUDPRepository;


        public HREmployeeBiometricTemplateController()
        {
            this._HREmployeeBiometricTemplateServices = new HREmployeeBiometricTemplateServices(this._unitOfWork);
            this._realandUDPRepository = new RealandUDPRepository();
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> Index(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return View(new HREmployeeBiometricTemplateViewModelList
                {
                    DBModelList = await _HREmployeeBiometricTemplateServices.GetPagedList(pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "DeviceManagement",
                    BreadCrumbController = "HREmployeeBiometricTemplate",
                    BreadCrumbBaseURL = "DeviceManagement/HREmployeeBiometricTemplate",
                    BreadCrumbTitle = "कर्मचारी बायोमेट्रिक टेम्प्लेटहरू",
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
                await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
                return View();
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListHREmployeeBiometricTemplateAsync(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey)
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HREmployeeBiometricTemplateViewModelList
                {
                    DBModelList = await _HREmployeeBiometricTemplateServices.GetsBySearchKey(pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, searchKey),
                    HeaderTitle = "Attendance System Management",
                    BreadCrumbArea = "DeviceManagement",
                    BreadCrumbController = "HREmployeeBiometricTemplate",
                    BreadCrumbBaseURL = "DeviceManagement/HREmployeeBiometricTemplate",
                    BreadCrumbActionName = "_ListHREmployeeBiometricTemplateAsync",
                    BreadCrumbTitle = "Employee Biometric Templates",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection
                });
            }
            catch (Exception exp)
            {
                await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
                return RedirectToAction("Index", new { pageNumber = pageNumber, pageSize = pageSize, orderingBy = orderingBy, orderingDirection = orderingDirection });
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _CreateHREmployeeBiometricTemplateAsync()
        {
            try
            {
                return PartialView(new HREmployeeBiometricTemplateViewModel
                {
                    BreadCrumbArea = "DeviceManagement",
                    BreadCrumbController = "HREmployeeBiometricTemplate",
                    BreadCrumbBaseURL = "DeviceManagement/HREmployeeBiometricTemplate",
                    BreadCrumbActionName = "_CreateHREmployeeBiometricTemplateAsync",
                    FormModelName = "HREmployeeBiometricTemplate",
                    ModalTitle = "उपकरण जडान गर्नुहोस्",
                    BreadCrumbTitle = "उपकरण जडान",
                    CRUDAction = CRUDType.CREATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Create,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLCompany = await this.GetLongStringPairModel(PairModelTitle.HRCompany),
                    DDLHRDevice = await this.GetGUIDStringPairModel(PairModelTitle.Default),
                    ConnectionModel = new DeviceConnectionModel { IdHRCompany = (long)this.SessionDetail.IdHRCompany, Protocol = CommunicationProtocol.TCP }
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
        public async Task<ActionResult> _CreateHREmployeeBiometricTemplateAsync(HREmployeeBiometricTemplateViewModel viewModel, FormCollection formCollection)
        {
            DeviceCommunicationEntity entityModel = new DeviceCommunicationEntity();
            try
            {
                switch (viewModel.ConnectionComponentModel.IdDeviceModel)
                {
                    case (int)DeviceModel.REALAND:
                        entityModel = await this._realandUDPRepository.ConnectAsync(viewModel.ConnectionComponentModel);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceHREmployeeBiometricTemplate(viewModel, CRUDType.CREATE);
            }
            await this.AlertNotification(CRUDType.CREATE.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListWrapperRealandDeviceManagementAsync", entityModel);
        }


        [NonAction]
        protected async Task<ActionResult> RETSourceHREmployeeBiometricTemplate(HREmployeeBiometricTemplateViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
                viewModel.BreadCrumbArea = "DeviceManagement";
                viewModel.BreadCrumbController = "HREmployeeBiometricTemplate";
                viewModel.BreadCrumbBaseURL = "DeviceManagement/HREmployeeBiometricTemplate";
                viewModel.DDLEmployee = await GetLongStringPairModel(PairModelTitle.Employee, viewModel.DataModel.IdHREmployee);
                viewModel.DDlBiometricType = await GetIntStringPairModel(PairModelTitle.BiometricType);
                await Task.WhenAll();
                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        viewModel.SubmitButtonText = SubmitButtonText.Create;
                        viewModel.BreadCrumbActionName = "_CreateHREmployeeBiometricTemplateAsync";
                        viewModel.ModalTitle = "कर्मचारी बायोमेट्रिक टेम्पलेटहरू थप्नुहोस्";
                        viewModel.FormModelName = "HREmployeeBiometricTemplate";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.UPDATE:
                        viewModel.BreadCrumbActionName = "_UpdateHREmployeeBiometricTemplateAsync";
                        viewModel.ModalTitle = "कर्मचारी बायोमेट्रिक टेम्पलेटहरूको विवरण हेर्नुहोस्";
                        viewModel.FormModelName = "HREmployeeBiometricTemplate";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.DELETE:
                        viewModel.BreadCrumbActionName = "_DeleteHREmployeeBiometricTemplateAsync";
                        viewModel.ModalTitle = "कर्मचारी बायोमेट्रिक टेम्पलेटहरू सम्पादन गर्नुहोस्";
                        viewModel.FormModelName = "HREmployeeBiometricTemplate";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.PRINT:
                        viewModel.BreadCrumbActionName = "_PrintHREmployeeBiometricTemplateAsync";
                        viewModel.ModalTitle = "कर्मचारी बायोमेट्रिक टेम्पलेटहरूको सुची छाप्नुहोस्";
                        viewModel.FormModelName = "HREmployeeBiometricTemplate";
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                }
                return null;
            }
            catch (Exception exp)
            {
                throw new ArgumentException(exp.Message);
            }
        }

        [NonAction]
        protected async Task<ActionResult> CRUDHREmployeeBiometricTemplate(HREmployeeBiometricTemplateViewModel viewModel, CRUDType cRUDType, FormCollection formCollection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await RETSourceHREmployeeBiometricTemplate(viewModel, cRUDType);
                }
                switch (cRUDType)
                {
                    case CRUDType.CREATE:

                        break;
                    default:
                        break;
                }
                await _HREmployeeBiometricTemplateServices.CommitSaveChangesAsync(viewModel.DataModel, cRUDType);
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await RETSourceHREmployeeBiometricTemplate(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.BreadCrumbTitle, AlertNotificationType.success);
            return RedirectToAction("_ListHREmployeeBiometricTemplateAsync", new { pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _GetDeviceConnectionComponentAsync(Guid idHRDevice)
        {
            try
            {
                return PartialView(new HREmployeeBiometricTemplateViewModel
                {
                    ConnectionComponentModel = await this._HREmployeeBiometricTemplateServices.GetConnectionComponentAsync(idHRDevice)
                });
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _RealandDeviceManagementAsync(DeviceCommunicationEntity entity)
        {
            try
            {
                return PartialView(new DeviceCommunicationEntityViewModel
                {

                });
            }
            catch
            {
                return await this.AlertNotification("Error", "Error", AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListWrapperRealandDeviceManagementAsync()
        {
            try
            {
                return PartialView(new DeviceCommunicationEntityViewModel
                {

                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _GetFingerPrintDataAsync()
        {
            try
            {
                return PartialView();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        [ValidateInput(true)]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _CreateBulkFingerPrintAsync()
        {
            try
            {
                return PartialView();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

    }
}