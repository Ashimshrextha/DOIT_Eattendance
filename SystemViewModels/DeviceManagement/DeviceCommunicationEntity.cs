using Riss.Devices;

namespace SystemViewModels.DeviceManagement
{
    public class DeviceCommunicationEntity
    {
        public DeviceCommunicationEntity()
        {

        }
        public DeviceConnection DeviceConnection { get; set; }

        public Device Device { get; set; }

    }
}
