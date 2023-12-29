using AttendanceManagementSystem.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using SystemModels.CompanyManagement;
using SystemServices.CompanyManagement;
using SystemViewModels.CompanyManagement;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;

namespace AttendanceManagementSystem.Areas.CompanyManagement.Controllers
{
    public class HRCompanyHREmployeeShiftRoasterController : BaseController
    {
        // GET: CompanyManagement/HRCompanyHREmployeeShiftRoaster
        private readonly HRCompanyHREmployeeShiftRoasterServices _hRCompanyHREmployeeShiftRoasterServices;
        private readonly HRCompanyHREmployeeShiftDateServices _hRCompanyHREmployeeShiftDateServices;

        public HRCompanyHREmployeeShiftRoasterController()
        {
            this._hRCompanyHREmployeeShiftRoasterServices = new HRCompanyHREmployeeShiftRoasterServices(this._unitOfWork);
            this._hRCompanyHREmployeeShiftDateServices = new HRCompanyHREmployeeShiftDateServices(this._unitOfWork);
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListWrapperHRCompanyHREmployeeShiftRoasterAsync(long? idHRCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, "HRDesignationRank", "ASC");
                return PartialView(new HRCompanyHREmployeeShiftRoasterViewModelList
                {
                    IdHRCompany = idHRCompany,
                    SessionDetails = this.SessionDetail,
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyHREmployeeShiftRoaster",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyHREmployeeShiftRoaster",
                    BreadCrumbTitle = "शिफ्ट व्यवस्थापन",
                    BreadCrumbActionName = "_ListWrapperHRCompanyHREmployeeShiftRoasterAsync",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    DBModelEmp = await _hRCompanyHREmployeeShiftRoasterServices.Gets(idHRCompany, pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection,searchKey)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListHRCompanyHREmployeeShiftRoasterAsync(long? idHRCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, "HRDesignationRank", "ASC");
                return PartialView(new HRCompanyHREmployeeShiftRoasterViewModelList
                {
                    IdHRCompany = idHRCompany,
                    SessionDetails = this.SessionDetail,
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyHREmployeeShiftRoaster",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyHREmployeeShiftRoaster",
                    BreadCrumbTitle = "शिफ्ट व्यवस्थापन",
                    BreadCrumbActionName = "_ListHRCompanyHREmployeeShiftRoaster",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    DBModelEmp = await _hRCompanyHREmployeeShiftRoasterServices.Gets(idHRCompany, pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection, searchKey)

                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _CreateHRCompanyHREmployeeShiftRoasterAsync(long? idHRCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, "HRDesignationRank", "ASC");
                return PartialView(new HRCompanyHREmployeeShiftRoasterViewModel
                {
                    SessionDetails = this.SessionDetail,
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyHREmployeeShiftRoaster",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyHREmployeeShiftRoaster",
                    BreadCrumbTitle = "शिफ्ट रोस्टर",
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
                    IdHRCompany = idHRCompany,
                    DDLCompany = await this.GetLongStringPairModel(PairModelTitle.CompanyId, idHRCompany),
                    DDLHRCompanyShiftRoaster = await this.GetLongStringPairModel(PairModelTitle.HRCompanyShiftRoaster, idHRCompany),
                    DBEmployeeList = await this._hRCompanyHREmployeeShiftRoasterServices.GetsByCompany(idHRCompany, 0, this.SessionDetail.IdRoleType),
                    DataModelList = await this._hRCompanyHREmployeeShiftRoasterServices.GetsWeekdayModel(0),
                    DataModel = new HRCompanyHREmployeeShiftDateModel { Id = 0, IdHRCompany = (long)idHRCompany, EffectiveFromDateNP = Today.Result.Nepdate, EffectiveToDateNP = Today.Result.Nepdate }
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadHRCompanyHREmployeeShiftRoasterAsync(long? idHREmployee)
        {
            try
            {
                var model = await this.GetLongStringPairModel(PairModelTitle.HRCompanyHREmployeeShiftDate, 0, idHREmployee);
                long id = model.FirstOrDefault().Key;
                return PartialView(new HRCompanyHREmployeeShiftRoasterViewModel
                {
                    SessionDetails = this.SessionDetail,
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyHREmployeeShiftRoaster",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyHREmployeeShiftRoaster",
                    BreadCrumbTitle = "शिफ्ट रोस्टर",
                    BreadCrumbActionName = "_ReadHRCompanyHREmployeeShiftRoasterAsync",
                    FormModelName = "HRCompanyHREmployeeShiftRoaster",
                    ModalTitle = " कमचारीहरुको शिफ्ट विवरण हेर्नुहोस",
                    CRUDAction = CRUDType.READ,
                    OnlyCancelButton = true,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLHRCompanyHREmployeeShiftDate = model,
                    DBEmpModelList = await this._hRCompanyHREmployeeShiftRoasterServices.FindAllAsync(x => x.IdHRCompanyHREmployeeShiftDate == id)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _DeleteHRCompanyHREmployeeShiftRoasterAsync(long? idHREmployee)
        {
            try
            {
                var model = await this.GetLongStringPairModel(PairModelTitle.HRCompanyHREmployeeShiftDate, 0, idHREmployee);
                long id = model.FirstOrDefault().Key;
                return PartialView(new HRCompanyHREmployeeShiftRoasterViewModel
                {
                    SessionDetails = this.SessionDetail,
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyHREmployeeShiftRoaster",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyHREmployeeShiftRoaster",
                    BreadCrumbTitle = "शिफ्ट रोस्टर",
                    BreadCrumbActionName = "_DeleteHRCompanyHREmployeeShiftRoasterAsync",
                    FormModelName = "HRCompanyHREmployeeShiftRoaster",
                    ModalTitle = " कमचारीहरुको शिफ्ट विवरण हेर्नुहोस",
                    CRUDAction = CRUDType.DELETE,
                    OnlyCancelButton = true,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLHRCompanyHREmployeeShiftDate = model
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<JsonResult> DeleteHRCompanyHREmployeeShiftRoaster(long? idShiftRoaster)
        {
            try
            {
                await this._hRCompanyHREmployeeShiftRoasterServices.DeleteHREmployeeShiftRoaster(idShiftRoaster);
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception exp)
            {
                return Json(exp.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _GetHRCompanyHREmployeeShiftRoasterAsync(long? id)
        {
            try
            {
                return PartialView(new HRCompanyHREmployeeShiftRoasterViewModel
                {
                    DBEmpModelList = await this._hRCompanyHREmployeeShiftRoasterServices.FindAllAsync(x => x.IdHRCompanyHREmployeeShiftDate == id)
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

        [NonAction]
        private async Task<ActionResult> RETSourceHRCompanyHREmployeeShiftRoasterAsync(HRCompanyHREmployeeShiftRoasterViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
                viewModel.SessionDetails = SessionDetail;
                viewModel.BreadCrumbArea = "CompanyManagement";
                viewModel.BreadCrumbController = "HRCompanyHREmployeeShiftRoaster";
                viewModel.BreadCrumbBaseURL = "CompanyManagement/HRCompanyHREmployeeShiftRoaster";
                viewModel.FormModelName = "HRCompanyHREmployeeShiftRoaster";
                viewModel.DDLHRCompanyShiftRoaster = await this.GetLongStringPairModel(PairModelTitle.HRCompanyShiftRoaster, viewModel.IdHRCompany);
                viewModel.DBEmployeeList = await this._hRCompanyHREmployeeShiftRoasterServices.GetsByCompany(viewModel.IdHRCompany, 0, this.SessionDetail.IdRoleType);
                viewModel.DataModelList = await this._hRCompanyHREmployeeShiftRoasterServices.GetsWeekdayModel(0);
                viewModel.DDLCompany = await this.GetLongStringPairModel(PairModelTitle.CompanyId, viewModel.DataModel.IdHRCompany);
                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        viewModel.ModalTitle = "नयाँ शिफ्ट सिर्जना गर्नुहोस्";
                        viewModel.BreadCrumbActionName = "_CreateHRCompanyHREmployeeShiftRoasterAsync";
                        viewModel.CRUDAction = CRUDType.CREATE;
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
                //validate date 
                if (viewModel.DataModel.EffectiveFromDate > viewModel.DataModel.EffectiveToDate)
                {
                    Response.StatusCode = 350;
                    ModelState.AddModelError("", "शुरु मिति अन्तिम मिति भन्दा सानो राख्नुहोस्");
                    return await this.RETSourceHRCompanyHREmployeeShiftRoasterAsync(viewModel, cRUDType);
                }
                //Get Already Assigned Employee on this date
                var assignedEmployeeList = await this._hRCompanyHREmployeeShiftRoasterServices.GetsAssignEmployee(viewModel.IdHRCompany, viewModel.DataModel.EffectiveFromDate, viewModel.DataModel.EffectiveToDate);

                //Get Only Not Assigned Employee List
                viewModel.DBEmployeeList = (from c in viewModel.DBEmployeeList where !assignedEmployeeList.Contains(c.IdHREmployee) && c.IsChecked select c).ToList();

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

                foreach (var item in viewModel.DBEmployeeList)
                {
                    viewModel.DataModel.IdHREmployee = item.IdHREmployee;
                    long id = await this._hRCompanyHREmployeeShiftDateServices.CommitSaveChangesAsync(viewModel.DataModel, cRUDType);
                    foreach (var subitem in list)
                    {
                        subitem.IdHRCompanyHREmployeeShiftDate = id;
                        await this._hRCompanyHREmployeeShiftRoasterServices.CommitAsync(subitem, cRUDType);
                    }
                }

                //save changes to database
                await this._unitOfWork.SaveAsync();
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await this.RETSourceHRCompanyHREmployeeShiftRoasterAsync(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.IdHRCompany.ToString(), AlertNotificationType.success);
            return RedirectToAction("_ListHRCompanyHREmployeeShiftRoasterAsync", new { idHRCompany = viewModel.IdHRCompany, pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }
    }
}