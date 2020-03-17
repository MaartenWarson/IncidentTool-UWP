using IncidentTool.Constants;
using IncidentTool.Interfaces.Repositories;
using IncidentTool.Interfaces.Services.Data;
using IncidentTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncidentTool.Services.Data
{
    public class DeviceDataService : IDeviceDataService
    {
        private readonly IGenericRepository _repository; // Taken m.b.t. databasebewerkingen worden doorgegeven aan deze repository

        public DeviceDataService(IGenericRepository repository)
        {
            _repository = repository;
        }

        public async Task<IList<Device>> GetAllDevicesAsync()
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.AllDevicesEndpoint
            };

            var devices = await _repository.GetAsync<List<Device>>(builder.ToString());

            return devices;
        }

        public async Task<Device> GetDeviceByIdAsync(int id)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.DeviceByIdEndpoint(id)
            };

            var device = await _repository.GetAsync<Device>(builder.ToString());

            return device;
        }

        public async Task CreateDeviceAsync(string name, int deviceTypeId, string location)
        {
            Device device = new Device
            {
                DeviceId = await GenerateDeviceId(),
                Name = name,
                CurrentDeviceTypeId = deviceTypeId,
                Location = location
            };

            await AddDeviceToDatabase(device);
        }

        private async Task<int> GenerateDeviceId()
        {
            int count = (await GetAllDevicesAsync()).ToList().Count;

            return count + 1;
        }

        private async Task AddDeviceToDatabase(Device device)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.AddDeviceEndpoint
            };

            await _repository.PostAsync<Device>(builder.ToString(), device);
        }
    }
}
