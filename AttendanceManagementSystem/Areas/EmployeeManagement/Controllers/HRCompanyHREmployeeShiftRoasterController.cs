using AttendanceManagementSystem.Controllers;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using SystemDatabase;
using SystemModels.CompanyManagement;
using SystemServices.CompanyManagement;
using SystemServices.EmployeeManagement;
using SystemViewModels.CompanyManagement;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;

namespace AttendanceManagementSystem.Areas.EmployeeManagement.Controllers
{
    public class HRCompanyHREmployeeShiftRoasterController : BaseController
    {
        // GET: EmployeeManagement/HRCompanyHREmployeeShiftRoaster

        private readonly HRCompanyHREmployeeShiftRoasterServices _hRCompanyHREmployeeShiftRoasterServices;
        private readonly HRCompanyHREmployeeShiftDateServices _hRCompanyHREmployeeShiftDateServices;
        private readonly HREmployeeServices _hREmployeeServices;

        public HRCompanyHREmployeeShiftRoasterController()
        {
            this._hRCompanyHREmployeeShiftRoasterServices = new HRCompanyHREmployeeShiftRoasterServices(this._unitOfWork);
            this._hRCompanyHREmployeeShiftDateServices = new HRCompanyHREmployeeShiftDateServices(this._unitOfWork);
            this._hREmployeeServices = new HREmployeeServices(this._unitOfWork);
        }


        [NonAction]
        public Expression<Func<HRCompanyHREmployeeShiftDate, bool>> Condition(long? idHREmployee, string searchKey = "")
        {
            Expression<Func<HRCompanyHREmployeeShiftDate, bool>> returnData = (x => false);
            switch (this.SessionDetail.IdRoleType)
            {
                case (int)RoleType.SuperUser:
                    return returnData = (x => x.IdHREmployee == idHREmployee);
                case (int)RoleType.Admin:
                    return returnData = (x => x.IdHREmployee == idHREmployee);
                case (int)RoleType.RootUser:
                    return returnData = (x => x.IdHREmployee == idHREmployee);
                case (int)RoleType.SectionAdmin:
                    return returnData = (x => x.IdHREmployee == idHREmployee);
                case (int)RoleType.NormalUser:
                    return returnData = (x => x.IdHREmployee == idHREmployee);
                default:
                    return returnData;
            }
        }



        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListWrapperHRCompanyHREmployeeShiftRoasterAsync(long? idHREmployee, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                return PartialView(new HRCompanyHREmployeeShiftRoasterViewModelList
                {
                    IdHREmployee = idHREmployee,
                    SessionDetails = this.SessionDetail,
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HRCompanyHREmployeeShiftRoaster",
                    BreadCrumbBaseURL = "EmployeeManagement/HRCompanyHREmployeeShiftRoaster",
                    BreadCrumbTitle = "शिफ्ट व्यवस्थापन",
                    BreadCrumbActionName = "_ListWrapperHRCompanyHREmployeeShiftRoasterAsync",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    DBModelList = await this._hRCompanyHREmployeeShiftDateServices.GetPagedList(Condition(idHREmployee, searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListHRCompanyHREmployeeShiftRoasterAsync(long? idHREmployee, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, "EffectiveToDate", "DESC");
                return PartialView(new HRCompanyHREmployeeShiftRoasterViewModelList
                {
                    IdHREmployee = idHREmployee,
                    SessionDetails = this.SessionDetail,
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HRCompanyHREmployeeShiftRoaster",
                    BreadCrumbBaseURL = "EmployeeManagement/HRCompanyHREmployeeShiftRoaster",
                    BreadCrumbTitle = "शिफ्ट व्यवस्थापन",
                    BreadCrumbActionName = "_ListHRCompanyHREmployeeShiftRoaster",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    DBModelList = await this._hRCompanyHREmployeeShiftDateServices.GetPagedList(Condition(idHREmployee), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _CreateHRCompanyHREmployeeShiftRoasterAsync(long? idHREmployee, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var empModel = await this._hREmployeeServices.GetByIdAsync(idHREmployee);
                return PartialView(new HRCompanyHREmployeeShiftRoasterViewModel
                {
                    SessionDetails = this.SessionDetail,
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HRCompanyHREmployeeShiftRoaster",
                    BreadCrumbBaseURL = "EmployeeManagement/HRCompanyHREmployeeShiftRoaster",
                    BreadCrumbTitle = "शिफ्ट व्यवस्थापन",
                    BreadCrumbActionName = "_CreateHRCompanyHREmployeeShiftRoasterAsync",
                    FormModelName = "HRCompanyHREmployeeShiftRoaster",
                    ModalTitle = "नयाँ शिफ्ट सिर्जना गर्नुहोस्",
                    CRUDAction = CRUDType.CREATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Create,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    IdHREmployee = idHREmployee,
                    IdHRCompany = empModel.IdHRCompany,
                    DDLHRCompanyShiftRoaster = await this.GetLongStringPairModel(PairModelTitle.HRCompanyShiftRoaster, empModel.IdHRCompany),
                    DataModelList = await this._hRCompanyHREmployeeShiftRoasterServices.GetsModel(idHREmployee),
                    DataModel = new HRCompanyHREmployeeShiftDateModel { IdHREmployee = (long)idHREmployee, IdHRCompany = (long)empModel.IdHRCompany, EffectiveFromDateNP = Today.Result.Nepdate, EffectiveToDateNP = Today.Result.Nepdate }
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
        public async Task<ActionResult> _CreateHRCompanyHREmployeeShiftRoasterAsync(HRCompanyHREmployeeShiftRoasterViewModel viewModel, FormCollection formCollection)
        {
            return await this.CRUDHRCompanyHREmployeeShiftRoasterAsync(viewModel, formCollection, CRUDType.CREATE);
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadHRCompanyHREmployeeShiftRoasterAsync(long? id, long? idHREmployee)
        {
            try
            {
                var empModel = await this._hREmployeeServices.GetByIdAsync(idHREmployee);
                return PartialView(new HRCompanyHREmployeeShiftRoasterViewModel
                {
                    SessionDetails = this.SessionDetail,
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HRCompanyHREmployeeShiftRoaster",
                    BreadCrumbBaseURL = "EmployeeManagement/HRCompanyHREmployeeShiftRoaster",
                    BreadCrumbTitle = "शिफ्ट व्यवस्थापन",
                    BreadCrumbActionName = "_ReadHRCompanyHREmployeeShiftRoasterAsync",
                    FormModelName = "HRCompanyHREmployeeShiftRoaster",
                    ModalTitle = "नयाँ शिफ्ट सिर्जना गर्नुहोस्",
                    CRUDAction = CRUDType.READ,
                    OnlyCancelButton = true,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    IdHREmployee = idHREmployee,
                    IdHRCompany = empModel.IdHRCompany,
                    DBEmpModelList = await this._hRCompanyHREmployeeShiftRoasterServices.FindAllAsync(x => x.IdHRCompanyHREmployeeShiftDate == id)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _UpdateHRCompanyHREmployeeShiftRoasterAsync(long? id, long? idHREmployee, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
                var empModel = await this._hREmployeeServices.GetByIdAsync(idHREmployee);
                var shiftDateModel = await this._hRCompanyHREmployeeShiftDateServices.GetModelByIdAsync(id);
                var ch = await this._hRCompanyHREmployeeShiftRoasterServices.GetModelFindAsync(x => x.IdHRCompanyHREmployeeShiftDate == id);
                return PartialView(new HRCompanyHREmployeeShiftRoasterViewModel
                {
                    SessionDetails = this.SessionDetail,
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HRCompanyHREmployeeShiftRoaster",
                    BreadCrumbBaseURL = "EmployeeManagement/HRCompanyHREmployeeShiftRoaster",
                    BreadCrumbTitle = "शिफ्ट व्यवस्थापन",
                    BreadCrumbActionName = "_UpdateHRCompanyHREmployeeShiftRoasterAsync",
                    FormModelName = "HRCompanyHREmployeeShiftRoaster",
                    ModalTitle = "शिफ्ट सम्पादन  गर्नुहोस्",
                    CRUDAction = CRUDType.UPDATE,
                    SubmitButtonType = SubmitButtonType.submit,
                    SubmitButtonText = SubmitButtonText.Update,
                    SubmitButtonID = SubmitButtonID.btnSubmit,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    IdHREmployee = idHREmployee,
                    IdHRCompany = empModel.IdHRCompany,
                    DBEmployee = empModel,
                    DDLHRCompanyShiftRoaster = await this.GetLongStringPairModel(PairModelTitle.HRCompanyShiftRoaster, empModel.IdHRCompany),
                    DataModelList = await this._hRCompanyHREmployeeShiftRoasterServices.GetsModel(idHREmployee),
                    DataModel = new HRCompanyHREmployeeShiftDateModel { Id = shiftDateModel.Id, IdHREmployee = shiftDateModel.IdHREmployee, EffectiveFromDate = shiftDateModel.EffectiveFromDate, EffectiveToDate = shiftDateModel.EffectiveToDate, EffectiveFromDateNP = shiftDateModel.EffectiveFromDate.DateNp(), EffectiveToDateNP = shiftDateModel.EffectiveToDate.DateNp(), IsActive = true },
                    DBEmpModelList = await this._hRCompanyHREmployeeShiftRoasterServices.FindAllAsync(x => x.IdHRCompanyHREmployeeShiftDate == id)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _UpdateHRCompanyHREmployeeShiftRoasterAsync(HRCompanyHREmployeeShiftRoasterViewModel viewModel, FormCollection formCollection)
        {
            return await this.CRUDHRCompanyHREmployeeShiftRoasterAsync(viewModel, formCollection, CRUDType.UPDATE);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _PrintHRCompanyHREmployeeShiftRoasterAsync(long? id, long? idHREmployee)
        {
            try
            {
                var empModel = await this._hREmployeeServices.GetByIdAsync(idHREmployee);
                return PartialView(new HRCompanyHREmployeeShiftRoasterViewModel
                {
                    SessionDetails = this.SessionDetail,
                    BreadCrumbArea = "EmployeeManagement",
                    BreadCrumbController = "HRCompanyHREmployeeShiftRoaster",
                    BreadCrumbBaseURL = "EmployeeManagement/HRCompanyHREmployeeShiftRoaster",
                    BreadCrumbTitle = "शिफ्ट व्यवस्थापन",
                    BreadCrumbActionName = "_PrintHRCompanyHREmployeeShiftRoasterAsync",
                    FormModelName = "HRCompanyHREmployeeShiftRoaster",
                    ModalTitle = "शिफ्ट छाप्नुहोस्",
                    CRUDAction = CRUDType.PRINT,
                    SubmitButtonID = SubmitButtonID.btnPrint,
                    SubmitButtonText = SubmitButtonText.Print,
                    SubmitButtonType = SubmitButtonType.button,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    IdHREmployee = idHREmployee,
                    IdHRCompany = empModel.IdHRCompany,
                    DBEmpModelList = await this._hRCompanyHREmployeeShiftRoasterServices.FindAllAsync(x => x.IdHRCompanyHREmployeeShiftDate == id)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [NonAction]
        private async Task<ActionResult> RETSourceHRCompanyHREmployeeShiftRoasterAsync(HRCompanyHREmployeeShiftRoasterViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
                viewModel.SessionDetails = SessionDetail;
                viewModel.BreadCrumbArea = "EmployeeManagement";
                viewModel.BreadCrumbController = "HRCompanyHREmployeeShiftRoaster";
                viewModel.BreadCrumbBaseURL = "EmployeeManagement/HRCompanyHREmployeeShiftRoaster";
                viewModel.FormModelName = "HRCompanyHREmployeeShiftRoaster";
                viewModel.DDLHRCompanyShiftRoaster = await this.GetLongStringPairModel(PairModelTitle.HRCompanyShiftRoaster, viewModel.IdHRCompany);
                switch (cRUDType)
                {
                    case CRUDType.UPDATE:
                        viewModel.ModalTitle = "शिफ्ट सम्पादन  गर्नुहोस्";
                        viewModel.BreadCrumbActionName = "_UpdateHRCompanyHREmployeeShiftRoasterAsync";
                        viewModel.CRUDAction = CRUDType.UPDATE;
                        viewModel.SubmitButtonText = SubmitButtonText.Update;
                        return PartialView(viewModel.BreadCrumbActionName, viewModel);
                    case CRUDType.CREATE:
                        viewModel.ModalTitle = "नयाँ शिफ्ट सिर्जना गर्नुहोस्";
                        viewModel.BreadCrumbActionName = "_CreateHRCompanyHREmployeeShiftRoasterAsync";
                        viewModel.CRUDAction = CRUDType.CREATE;
                        viewModel.SubmitButtonText = SubmitButtonText.Create;
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
        private async Task<ActionResult> CRUDHRCompanyHREmployeeShiftRoasterAsync(HRCompanyHREmployeeShiftRoasterViewModel viewModel, FormCollection formCollection, CRUDType cRUDType)
        {
            try
            {
                int count = 0;
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await this.RETSourceHRCompanyHREmployeeShiftRoasterAsync(viewModel, cRUDType);
                }

                viewModel.DataModel.EffectiveFromDate = (DateTime)this.GetEnglishDate(viewModel.DataModel.EffectiveFromDateNP);
                viewModel.DataModel.EffectiveToDate = (DateTime)this.GetEnglishDate(viewModel.DataModel.EffectiveToDateNP);
                viewModel.DataModel.IdHRCompany = viewModel.DataModel.IdHRCompany;

                long id = 0;
                List<HRCompanyHREmployeeShiftRoasterModel> list = new List<HRCompanyHREmployeeShiftRoasterModel>();

                foreach (var item in viewModel.DataModelList)
                {
                    if (item.IsWeekend)
                    {
                        list.Add(item);
                    }
                    else if (item.DDLShiftRoasterSelected != null)
                    {
                        foreach (var items in item.DDLShiftRoasterSelected)
                        {
                            list.Add(new HRCompanyHREmployeeShiftRoasterModel
                            {
                                Id = 0,
                                IdDayOfWeekNP = item.IdDayOfWeekNP,
                                IdHRCompanyShiftRoaster = items,
                                IsWeekend = false,
                                IsActive = true
                            });
                        }
                    }
                    else
                    {
                        Response.StatusCode = 350;
                        ModelState.AddModelError($"DataModelList[{count}].DOWNP", "कृपया कुनै पनि शिफ्ट वा सप्ताहांत चयन गर्नुहोस्");
                        return await this.RETSourceHRCompanyHREmployeeShiftRoasterAsync(viewModel, cRUDType);
                    }
                    count++;
                }

                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        //Check this employee shift is exist or not on following date time
                        if (await this._hRCompanyHREmployeeShiftDateServices.Exits(x => x.IdHREmployee == viewModel.DataModel.IdHREmployee && x.IdHRCompany == viewModel.DataModel.IdHRCompany && ((viewModel.DataModel.EffectiveFromDate >= x.EffectiveFromDate && viewModel.DataModel.EffectiveToDate <= x.EffectiveToDate) || (viewModel.DataModel.EffectiveToDate >= x.EffectiveFromDate && viewModel.DataModel.EffectiveToDate <= x.EffectiveToDate))))
                        {
                            Response.StatusCode = 350;
                            ModelState.AddModelError("DataModel.EffectiveToDateNP", "Sorry Already Exists Shift");
                            return await this.RETSourceHRCompanyHREmployeeShiftRoasterAsync(viewModel, cRUDType);
                        }
                        // first get id of shifroaster date
                        id = await this._hRCompanyHREmployeeShiftDateServices.CommitSaveChangesAsync(viewModel.DataModel, cRUDType);
                        break;
                    case CRUDType.UPDATE:
                        var model = await _hRCompanyHREmployeeShiftRoasterServices.FindAllAsync(x => x.IdHRCompanyHREmployeeShiftDate == viewModel.DataModel.Id);
                        bool IsDeleted = await _hRCompanyHREmployeeShiftRoasterServices.DeleteHRCompanyHREmployeeShiftRoasterAsync(model);
                        if (IsDeleted)
                        {
                            id = viewModel.DataModel.Id;
                        }
                        else
                        {
                            Response.StatusCode = 350;
                            ModelState.AddModelError("", "Please Try Again!!");
                            return await this.RETSourceHRCompanyHREmployeeShiftRoasterAsync(viewModel, cRUDType);
                        }
                        break;
                }
                foreach (var item in list)
                {
                    item.IdHRCompanyHREmployeeShiftDate = id;
                }
                await this._hRCompanyHREmployeeShiftRoasterServices.CommitBulkSaveChangesAsync(list, CRUDType.CREATE);

            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await this.RETSourceHRCompanyHREmployeeShiftRoasterAsync(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.IdHRCompany.ToString(), AlertNotificationType.success);
            return RedirectToAction("_ListHRCompanyHREmployeeShiftRoasterAsync", new { idHREmployee = viewModel.IdHREmployee, pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }
    }
}