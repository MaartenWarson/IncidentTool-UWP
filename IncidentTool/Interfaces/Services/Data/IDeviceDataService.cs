using IncidentTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncidentTool.Interfaces.Services.Data
{
    public interface IDeviceDataService
    {
        Task<IList<Device>> GetAllDevicesAsync();
        Task CreateDeviceAsync(string name, int deviceTypeId, string location);
        Task<Device> GetDeviceByIdAsync(int id);
    }
}
