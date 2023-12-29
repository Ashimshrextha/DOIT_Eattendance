using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Threading.Tasks;

namespace AttendanceManagementSystem.HUBCenter
{
    public class DeviceLogsHub : Hub
    {
        [HubMethodName("getDeviceLogsAsync")]
        public async Task GetDeviceLogsAsync()
        {
            try
            {
                await Task.WhenAll();
                IHubContext context = GlobalHost.ConnectionManager.GetHubContext<DeviceLogsHub>();
                context.Clients.All.updatedData();
            }
            catch (Exception exp)
            {
                throw new ArgumentException(exp.Message);
            }
        }
    }
}