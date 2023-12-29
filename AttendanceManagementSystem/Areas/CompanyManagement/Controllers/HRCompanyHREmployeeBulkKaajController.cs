using AttendanceManagementSystem.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    public class HRCompanyHREmployeeBulkKaajController : BaseController
    {
        // GET: CompanyManagement/HRCompanyHREmployeeShiftRoaster
        private readonly HRCompanyHREmployeeBulkKaajServices _hRCompanyHREmployeeBulkKaajServices;
        private readonly HRCompanyHREmployeeShiftDateServices _hRCompanyHREmployeeShiftDateServices;
        private readonly Random _random = new Random();
        public HRCompanyHREmployeeBulkKaajController()
        {
            this._hRCompanyHREmployeeBulkKaajServices = new HRCompanyHREmployeeBulkKaajServices(this._unitOfWork);
            this._hRCompanyHREmployeeShiftDateServices = new HRCompanyHREmployeeShiftDateServices(this._unitOfWork);
        }


        [NonAction]
        public Expression<Func<HREmployeeKaajHistory, bool>> Condition(long? IdHRCompany, string searchKey = "")
        {
            Expression<Func<HREmployeeKaajHistory, bool>> returnData = (x => false);
            switch (this.SessionDetail.IdRoleType)
            {
                case (int)RoleType.SuperUser:
                    return returnData = (x => x.IdHrCompany == IdHRCompany && ((x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString()))|| (x.HREmployee.HREmployeeNameNP.ToUpper().Contains(searchKey.ToString()))  || (x.HREmployee1.HREmployeeName.ToUpper().Contains(searchKey.ToString())) || (x.HREmployee1.HREmployeeNameNP.ToUpper().Contains(searchKey.ToString())) || (x.KaajFromNP.Contains(searchKey.ToString()) || searchKey == "")));

                case (int)RoleType.Admin:
                    return returnData = (x => x.IdHrCompany == IdHRCompany && ((x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString())) || (x.HREmployee.HREmployeeNameNP.ToUpper().Contains(searchKey.ToString())) || (x.HREmployee1.HREmployeeName.ToUpper().Contains(searchKey.ToString())) || (x.HREmployee1.HREmployeeNameNP.ToUpper().Contains(searchKey.ToString())) || (x.KaajFromNP.Contains(searchKey.ToString()) || searchKey == "")));

                case (int)RoleType.RootUser:
                    return returnData = (x => x.IdHrCompany == IdHRCompany && ( (x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString())) || (x.HREmployee.HREmployeeNameNP.ToUpper().Contains(searchKey.ToString())) || (x.HREmployee1.HREmployeeName.ToUpper().Contains(searchKey.ToString())) || (x.HREmployee1.HREmployeeNameNP.ToUpper().Contains(searchKey.ToString())) || (x.KaajFromNP.Contains(searchKey.ToString()) || searchKey == "")));
                case (int)RoleType.SectionAdmin:
                    return returnData = (x => x.IdHrCompany == IdHRCompany && ((x.HREmployee.HREmployeeName.ToUpper().Contains(searchKey.ToString())) || (x.HREmployee.HREmployeeNameNP.ToUpper().Contains(searchKey.ToString())) || (x.HREmployee1.HREmployeeName.ToUpper().Contains(searchKey.ToString())) || (x.HREmployee1.HREmployeeNameNP.ToUpper().Contains(searchKey.ToString())) || (x.KaajFromNP.Contains(searchKey.ToString()) || searchKey == "")));
                default:
                    return returnData;
            }
        }



        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListWrapperHRCompanyHREmployeeBulkKaajAsync(long? idHRCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, "Id", "DESC");
                return PartialView(new HRCompanyHREmployeeBulkKaajViewModelList
                {
                    IdHRCompany = idHRCompany,
                    SessionDetails = this.SessionDetail,
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyHREmployeeBulkKaaj",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyHREmployeeBulkKaaj",
                    BreadCrumbTitle = "कोविड काज",
                    BreadCrumbActionName = "_ListWrapperHRCompanyHREmployeeBulkKaajAsync",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    DBModelEmp = await _hRCompanyHREmployeeBulkKaajServices.GetPagedList(Condition(idHRCompany, searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ListHRCompanyHREmployeeBulkKaajAsync(long? idHRCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var pagination = Get_PaginationValue(pageNumber, pageSize, "Id", "DESC");
                return PartialView(new HRCompanyHREmployeeBulkKaajViewModelList
                {
                    IdHRCompany = idHRCompany,
                    SessionDetails = this.SessionDetail,
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyHREmployeeBulkKaaj",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyHREmployeeBulkKaaj",
                    BreadCrumbTitle = "कोविड काज",
                    BreadCrumbActionName = "_ListHRCompanyHREmployeeBulkKaaj",
                    CRUDAction = CRUDType.READ,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    OrderingBy = pagination.OrderingBy,
                    OrderingDirection = pagination.OrderingDirection,
                    DBModelEmp = await _hRCompanyHREmployeeBulkKaajServices.GetPagedList(Condition(idHRCompany, searchKey), pagination.PageNumber, pagination.PageSize, pagination.OrderingBy, pagination.OrderingDirection),

                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _CreateHRCompanyHREmployeeBulkKaajAsync(long? idHRCompany, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "")
        {
            try
            {
                var today = Today.Result;
                long? idHREmployee = this.SessionDetail.IdRoleType == 0 ? 0 : this.SessionDetail.IdHREmployee;
                var pagination = Get_PaginationValue(pageNumber, pageSize, "Id", "DESC");
                return PartialView(new HRCompanyHREmployeeBulkKaajViewModel
                {
                    SessionDetails = this.SessionDetail,
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyHREmployeeBulkKaaj",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyHREmployeeBulkKaaj",
                    BreadCrumbTitle = "कोविड काज",
                    BreadCrumbActionName = "_CreateHRCompanyHREmployeeBulkKaajAsync",
                    FormModelName = "HRCompanyHREmployeeBulkKaaj",
                    ModalTitle = "नयाँ सिर्जना गर्नुहोस्",
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
                    DDLKaajType = await this.GetIntStringPairModel(PairModelTitle.KaajType, idHRCompany),
                    DDLVehicleType = await GetStringStringPairModel(PairModelTitle.VehicleType),
                    DDLCountry = await this.GetIntStringPairModel(PairModelTitle.Country),
                    DBEmployeeList = await this._hRCompanyHREmployeeBulkKaajServices.GetsByCompany(idHRCompany, 0, this.SessionDetail.IdRoleType),
                    DataModel = new HRCompanyHREmployeeBulkKaajModel
                    {
                        Id = 0,
                        IdHrCompany = (long)idHRCompany,
                        KaajFromNP = Today.Result.Nepdate,
                        KaajToNP = Today.Result.Nepdate,
                        FiscalYear = today.FiscalYear,
                        KaajYearNP = this.Today.Result.NepYear,
                    },
                    DDLApprovalEmployee = await this.GetLongStringPairModel(PairModelTitle.EmployeeApproved, idHRCompany, idHREmployee),

                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _ReadHRCompanyHREmployeeBulkKaajAsync(long? idHREmployee)
        {
            try
            {
                var model = await this.GetLongStringPairModel(PairModelTitle.HRCompanyHREmployeeShiftDate, 0, idHREmployee);
                long id = model.FirstOrDefault().Key;
                return PartialView(new HRCompanyHREmployeeBulkKaajViewModel
                {
                    SessionDetails = this.SessionDetail,
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyHREmployeeBulkKaaj",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyHREmployeeBulkKaaj",
                    BreadCrumbTitle = "बल्क काज",
                    BreadCrumbActionName = "_ReadHRCompanyHREmployeeBulkKaajAsync",
                    FormModelName = "HRCompanyHREmployeeShiftRoaster",
                    ModalTitle = " कमचारीहरुको  विवरण हेर्नुहोस",
                    CRUDAction = CRUDType.READ,
                    OnlyCancelButton = true,
                    CancelButtonID = CancelButtonID.btnCancel,
                    CancelButtonText = CancelButtonText.Cancel,
                    DDLHRCompanyHREmployeeShiftDate = model,
                    DBEmpModelList = await this._hRCompanyHREmployeeBulkKaajServices.FindAllAsync(x => x.Id == id)
                });
            }
            catch (Exception exp)
            {
                return await this.AlertNotification("Error", exp.Message, AlertNotificationType.error);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> _DeleteHRCompanyHREmployeeBulkKaajAsync(long? idHREmployee)
        {
            try
            {
                var model = await this.GetLongStringPairModel(PairModelTitle.HRCompanyHREmployeeShiftDate, 0, idHREmployee);
                long id = model.FirstOrDefault().Key;
                return PartialView(new HRCompanyHREmployeeBulkKaajViewModel
                {
                    SessionDetails = this.SessionDetail,
                    BreadCrumbArea = "CompanyManagement",
                    BreadCrumbController = "HRCompanyHREmployeeBulkKaaj",
                    BreadCrumbBaseURL = "CompanyManagement/HRCompanyHREmployeeBulkKaaj",
                    BreadCrumbTitle = "बल्क काज",
                    BreadCrumbActionName = "_DeleteHRCompanyHREmployeeBulkKaajAsync",
                    FormModelName = "HRCompanyHREmployeeBulkKaaj",
                    ModalTitle = " कमचारीहरुको  विवरण हेर्नुहोस",
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


        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> _CreateHRCompanyHREmployeeBulkKaajAsync(HRCompanyHREmployeeBulkKaajViewModel viewModel, FormCollection formCollection)
        {
            return await this.CRUDHRCompanyHREmployeeBulkKaajAsync(viewModel, formCollection, CRUDType.CREATE);
        }

        [NonAction]
        private async Task<ActionResult> RETSourceHRCompanyHREmployeeBulkKaajAsync(HRCompanyHREmployeeBulkKaajViewModel viewModel, CRUDType cRUDType)
        {
            try
            {
                viewModel.SessionDetails = SessionDetail;
                viewModel.BreadCrumbArea = "CompanyManagement";
                viewModel.BreadCrumbController = "HRCompanyHREmployeeBulkKaaj";
                viewModel.BreadCrumbBaseURL = "CompanyManagement/HRCompanyHREmployeeBulkKaaj";
                viewModel.FormModelName = "HRCompanyHREmployeeBulkKaaj";
                viewModel.DBEmployeeList = await this._hRCompanyHREmployeeBulkKaajServices.GetsByCompany(viewModel.IdHRCompany, 0, this.SessionDetail.IdRoleType);
                viewModel.DDLCompany = await this.GetLongStringPairModel(PairModelTitle.CompanyId, viewModel.DataModel.IdHrCompany);
                viewModel.DDLKaajType = await this.GetIntStringPairModel(PairModelTitle.KaajType, viewModel.DataModel.IdHrCompany);
                viewModel.DDLApprovalEmployee = await this.GetLongStringPairModel(PairModelTitle.EmployeeApproved, viewModel.IdHRCompany, 0);
                viewModel.DDLVehicleType = await GetStringStringPairModel(PairModelTitle.VehicleType);
                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        viewModel.ModalTitle = "नयाँ सिर्जना गर्नुहोस्";
                        viewModel.BreadCrumbActionName = "_CreateHRCompanyHREmployeeBulkKaajAsync";
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
        private async Task<ActionResult> CRUDHRCompanyHREmployeeBulkKaajAsync(HRCompanyHREmployeeBulkKaajViewModel viewModel, FormCollection formCollection, CRUDType cRUDType)
        {
           
            try
            {

                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 350;
                    return await this.RETSourceHRCompanyHREmployeeBulkKaajAsync(viewModel, cRUDType);
                }
               
                viewModel.DataModel.KaajFromDate = (DateTime)this.GetEnglishDate(viewModel.DataModel.KaajFromNP);
                viewModel.DataModel.KaajToDate = (DateTime)this.GetEnglishDate(viewModel.DataModel.KaajToNP);
                viewModel.DataModel.KaajYearNP =this.GetYearFromNepaliDate(viewModel.DataModel.KaajFromNP) ;
                long CompanyId = viewModel.IdHRCompany ?? 0;
                int IdKaajType = viewModel.DataModel.IdKaajType;

                viewModel.DataModel.VehicleUsedInKaaj = string.Join(",", viewModel.ddlKajVehicalSelected);
                var assignedEmployeeKaajList = this.CheckExistanceKaaj(CompanyId, viewModel.DataModel.KaajFromDate, viewModel.DataModel.KaajToDate);
                var assignedEmployeeLeaveList = this.CheckExistanceLeave(CompanyId, viewModel.DataModel.KaajFromDate, viewModel.DataModel.KaajToDate);


                
                //validate date 
                if (viewModel.DataModel.KaajFromDate > viewModel.DataModel.KaajToDate)
                {
                    Response.StatusCode = 350;
                    ModelState.AddModelError("", "शुरु मिति अन्तिम मिति भन्दा सानो राख्नुहोस्");
                    return await this.RETSourceHRCompanyHREmployeeBulkKaajAsync(viewModel, cRUDType);
                }
            
                //Get Only Not Assigned Employee List
                viewModel.DBEmployeeList = (from c in viewModel.DBEmployeeList where !assignedEmployeeKaajList.Contains(c.IdHREmployee) && !assignedEmployeeLeaveList.Contains(c.IdHREmployee) && c.IsChecked select c).ToList();
              

                foreach (var item in viewModel.DBEmployeeList)
                {
                    var random = RandomNumber(1, 500);
                    viewModel.DataModel.IdHREmployee =item.IdHREmployee;
                    viewModel.DataModel.ApprovedOn = DateTime.UtcNow;
                    viewModel.DataModel.IdJob = this.GetCurrentActiveJob(CompanyId, item.IdHREmployee);

                    viewModel.DataModel.KaajRegistrationNumber = this._hRCompanyHREmployeeBulkKaajServices.kaajRegistrationNumber(CompanyId, viewModel.DataModel.FiscalYear);
                    viewModel.DataModel.KaajTakenNumber =await this._hRCompanyHREmployeeBulkKaajServices.KaajTakenNumber(CompanyId, viewModel.DataModel.FiscalYear, item.IdHREmployee);


                    



                    //viewModel.DataModel.KaajRegistrationNumber = $"{viewModel.DataModel.FiscalYear}-{CompanyId}-{viewModel.DataModel.IdHREmployee}-{random}-{DateTime.UtcNow.Hour}-{DateTime.UtcNow.Minute}";




                    if (viewModel.DataModel.IdJob > 0)
                    {
                        await this._hRCompanyHREmployeeBulkKaajServices.CommitSaveChangesAsync(viewModel.DataModel, cRUDType);
                    }
                   
                }

                //save changes to database
                await this._unitOfWork.SaveAsync();
            }
            catch (Exception exp)
            {
                Response.StatusCode = 350;
                ModelState.AddModelError("", exp.Message);
                return await this.RETSourceHRCompanyHREmployeeBulkKaajAsync(viewModel, cRUDType);
            }
            await this.AlertNotification(cRUDType.ToString(), viewModel.IdHRCompany.ToString(), AlertNotificationType.success);
            return RedirectToAction("_ListHRCompanyHREmployeeBulkKaajAsync", new { idHRCompany = viewModel.IdHRCompany, pageNumber = viewModel.PageNumber, pageSize = viewModel.PageSize, orderingBy = viewModel.OrderingBy, orderingDirection = viewModel.OrderingDirection, searchKey = viewModel.SearchKey });
        }
        public int RandomNumber(int min, int max)
        {

            return _random.Next(min, max);
        }

       
    }
}