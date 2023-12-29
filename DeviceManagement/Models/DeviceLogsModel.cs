using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceManagement.Models
{
    public class DeviceLogsModel
    {
        public long IdEnroll { get; set; }
        public int DeviceNumber { get; set; }
        public int VerifyMode { get; set; }
        public int InOutMode { get; set; }
        public string PunchDateTime { get; set; }
    }
}
