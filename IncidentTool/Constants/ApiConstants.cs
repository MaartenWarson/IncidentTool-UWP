using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncidentTool.Constants
{
    public class ApiConstants
    {
        public const string BaseApiUrl = "http://localhost:5000";
        public const string AllDevicesEndpoint = "/device/all";
        public const string AddDeviceEndpoint = "/device/add";
        public const string AllDeviceTypesEndpoint = "/devicetype/all";
        public const string AddDeviceTypeEndpoint = "/devicetype/add";
        public const string AllIncidentsEndpoint = "/incident/all";
        public const string AddIncidentEndpoint = "/incident/add";
        public const string AllSolvedOccurredIncidentsEndpoint = "/occurredincident/all/solved";
        public const string AllUnsolvedOccurredIncidentsEndpoint = "/occurredincident/all/unsolved";

        public static string DeviceByIdEndpoint(int id)
        {
            return "/device/" + id;
        }

        public static string UserByIdEndpoint(int id)
        {
            return "/user/" + id;
        }

        public static string SetIncidentSolvedEndpoint(int id)
        {
            return "/occurredincident/" + id + "/solved";
        }

        public static string SetIncidentUnsolvedEndpoint(int id)
        {
            return "/occurredincident/" + id + "/unsolved";
        }
    }
}
