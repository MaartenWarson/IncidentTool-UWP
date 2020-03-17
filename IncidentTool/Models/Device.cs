using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncidentTool.Models
{
    public class Device
    {
        public int DeviceId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int CurrentDeviceTypeId { get; set; }
        public DeviceType DeviceType { get; set; }
    }
}
