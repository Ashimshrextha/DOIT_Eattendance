using Riss.Devices;
using System;
using System.Threading.Tasks;
using SystemModels.DeviceManagement;
using SystemViewModels.DeviceManagement;

namespace DeviceManagement.RealandUDPRepository
{
    public class RealandUDPRepository
    {
        private DeviceConnection _deviceConnection;
        private DeviceCommunicationEntity _DeviceCommunicationEntity;

        public RealandUDPRepository()
        {

        }

        public async Task<DeviceCommunicationEntity> ConnectAsync(DeviceConnectionComponentModel model)
        {
            Device device = new Device();
            try
            {
                device.IpAddress = model.IPAddress;
                device.IpPort = model.PortNumber;
                device.CommunicationType = CommunicationType.Tcp;
                device.Password = model.CommunicationKey.ToString();
                device.Model = "ZDC2911";
                device.ConnectionModel = 5;
                device.DN = model.DeviceNumber;
                _deviceConnection = DeviceConnection.CreateConnection(ref device);
                if (_deviceConnection.Open() > 0)
                {
                    _DeviceCommunicationEntity = new DeviceCommunicationEntity();
                    _DeviceCommunicationEntity.Device = device;
                    _DeviceCommunicationEntity.DeviceConnection = _deviceConnection;
                }
                await Task.WhenAll();
                return _DeviceCommunicationEntity;
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        //public async Task<string> GetFingerPrintAsync()
        //{
        //    try
        //    {
        //        return null;
        //    }
        //    catch (Exception exp)
        //    {
        //        throw new Exception(exp.Message);
        //    }
        //}
    }
}
