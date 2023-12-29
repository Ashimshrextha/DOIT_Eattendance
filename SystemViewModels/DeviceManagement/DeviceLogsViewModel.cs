using PagedList;
using System.Collections.Generic;
using SystemDatabase;
using SystemModels.DeviceManagement;
using SystemStores.GlobalModels;
using SystemViewModels.Shared;

namespace SystemViewModels.DeviceManagement
{
    public class DeviceLogsViewModel : BreadCrumbModel
    {
        public DeviceLogsModel DataModel { get; set; }
        public IPagedList<DeviceLogsModel> DataModelList { get; set; }
        public ICollection<DeviceLog> DBModelList { get; set; }
    }
    public class DeviceLogsViewModelList : BreadCrumbModel
    {
        public DeviceLog DBModel { get; set; }
        public IPagedList<DeviceLog> DBModelList { get; set; }
        //public ICollection<proc_GetDeviceLogsByPunchDate_Result> DbGetDeviceLogs { get; set; }
    }

    public class DeviceDatePunchedLogsViewModelList : BreadCrumbModel
    {
        public ReportHeaderViewModel Header { get; set; }
        public DeviceLog DBModel { get; set; }
        public IPagedList<DeviceLog> DBModelList { get; set; }
        public ICollection<proc_GetMonthlyWorkingSummaryReport_Result> DBModelCollection { get; set; }

        public ICollection<proc_GetDeviceLogsByPunchedDate_Result> DbGetDeviceLogs { get; set; }
    }
}
