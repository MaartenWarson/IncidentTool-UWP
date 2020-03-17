using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncidentTool.Models
{
    public class OccurredIncident
    {
        public int OccurredIncidentId { get; set; }
        public int DeviceId { get; set; }
        public string DeviceName { get; set; } // Extra
        public string DeviceLocation { get; set; } // Extra
        public string IncidentDescription { get; set; }
        public int CurrentUserId { get; set; }
        public string UserName { get; set; } // Extra
        public bool Solved { get; set; }
    }
}
