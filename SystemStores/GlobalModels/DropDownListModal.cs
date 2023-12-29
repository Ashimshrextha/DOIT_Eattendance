using System;
using System.Collections.Generic;
using SystemStores.PairModels;

namespace SystemStores.GlobalModels
{
    public class DropDownListModal : FilterModel
    {
        public virtual ICollection<KeyValuePairModel<int, string>> DDLReligion { get; set; }
        public virtual ICollection<KeyValuePairModel<int, string>> DDLRelation { get; set; }
        public virtual ICollection<KeyValuePairModel<long, string>> DDLGender { get; set; }
        public virtual ICollection<KeyValuePairModel<int, string>> DDLBloodGroup { get; set; }
        public virtual ICollection<KeyValuePairModel<long, string>> DDLMaritalStatus { get; set; }
        public virtual ICollection<KeyValuePairModel<long, string>> DDLLanguage { get; set; }
        public virtual ICollection<KeyValuePairModel<int, string>> DDLSystemLanguage { get; set; }
        public virtual ICollection<KeyValuePairModel<int, string>> DDLSystemMenu { get; set; }
        public virtual ICollection<KeyValuePairModel<int, string>> DDLSystemStructure { get; set; }
        public virtual ICollection<KeyValuePairModel<int, string>> DDLCompanyType { get; set; }
        public virtual ICollection<KeyValuePairModel<long, string>> DDLCompany { get; set; }
        public virtual ICollection<KeyValuePairModel<long, string>> DDLParentChildCompany { get; set; }
        public virtual ICollection<KeyValuePairModel<int, string>> DDLState { get; set; }
        public virtual ICollection<KeyValuePairModel<long, string>> DDLDistrict { get; set; }
        public virtual ICollection<KeyValuePairModel<int, string>> DDLCountry { get; set; }
        public virtual ICollection<KeyValuePairModel<long, string>> DDLCity { get; set; }
        public virtual ICollection<KeyValuePairModel<int, string>> DDLCityCategory { get; set; }
        public virtual ICollection<KeyValuePairModel<long, string>> DDLDivisionType { get; set; }
        public virtual ICollection<KeyValuePairModel<long, string>> DDLDivision { get; set; }
        public virtual ICollection<KeyValuePairModel<long, string>> DDLEmployee { get; set; }
        public virtual ICollection<KeyValuePairModel<long, string>> DDLApprovalEmployee { get; set; }
        public virtual ICollection<KeyValuePairModel<long, string>> DDLRecommendEmployee { get; set; }
        public virtual ICollection<KeyValuePairModel<int, string>> DDLRole { get; set; }
        public virtual ICollection<KeyValuePairModel<int, string>> DDLYearNp { get; set; }
        public virtual ICollection<KeyValuePairModel<int, string>> DDLMonthNp { get; set; }
        public virtual ICollection<KeyValuePairModel<int, string>> DDLDayNp { get; set; }
        public virtual ICollection<KeyValuePairModel<int, string>> DDLRoleType { get; set; }
        public virtual ICollection<KeyValuePairModel<int, string>> DDLDeviceModel { get; set; }
        public virtual ICollection<KeyValuePairModel<Guid, string>> DDLHRDevice { get; set; }
        public virtual ICollection<KeyValuePairModel<long, string>> DDLEmployeeCategory { get; set; }
        public virtual ICollection<KeyValuePairModel<long, string>> DDLDesignation { get; set; }
        public virtual ICollection<KeyValuePairModel<long, string>> DDLDesignationLeaveApproval { get; set; }
        public virtual ICollection<KeyValuePairModel<long, string>> DDLBank { get; set; }
        public virtual ICollection<KeyValuePairModel<long, string>> DDLServiceType { get; set; }
        public virtual ICollection<KeyValuePairModel<int, string>> DDLJobStatus { get; set; }
        public virtual ICollection<KeyValuePairModel<int, string>> DDLHREmployeePositionStatusType { get; set; }
        public virtual ICollection<KeyValuePairModel<int, string>> DDLNationalIdentityType { get; set; }
        public virtual ICollection<KeyValuePairModel<int, string>> DDLSystemDetailCategory { get; set; }
        public virtual ICollection<KeyValuePairModel<long, string>> DDLLeaveType { get; set; }
        public virtual ICollection<KeyValuePairModel<int, string>> DDLMasterLeaveTitle { get; set; }
        public virtual ICollection<KeyValuePairModel<long, string>> DDLJobType { get; set; }
        public virtual ICollection<KeyValuePairModel<long, string>> DDLContactType { get; set; }
        public virtual ICollection<KeyValuePairModel<int, string>> DDLEducationType { get; set; }
        public virtual ICollection<KeyValuePairModel<int, string>> DDlBiometricType { get; set; }
        public virtual ICollection<KeyValuePairModel<string, string>> DDLLeaveStatus { get; set; }
        public virtual ICollection<KeyValuePairModel<int, string>> DDLLeaveStatusType { get; set; }
        public virtual ICollection<KeyValuePairModel<int, string>> DDLHRCompanyCode { get; set; }
        public virtual ICollection<KeyValuePairModel<string, string>> DDLVehicleType { get; set; }
        public virtual ICollection<KeyValuePairModel<long, string>> DDLHRCompanyShiftRoaster { get; set; }
        public virtual ICollection<KeyValuePairModel<long, string>> DDLHRCompanyHREmployeeShiftDate { get; set; }
        public virtual ICollection<KeyValuePairModel<long, string>> DDLShiftTitle { get; set; }
        public virtual ICollection<KeyValuePairModel<int, string>> DDLKaajType { get; set; }
    }
}
