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
    public class DeviceTypeDataService : IDeviceTypeDataService
    {
        private readonly IGenericRepository _repository; // Taken m.b.t. databasebewerkingen worden doorgegeven aan deze repository

        public DeviceTypeDataService(IGenericRepository repository)
        {
            _repository = repository;
        }

        public async Task<IList<DeviceType>> GetAllDeviceTypesAsync()
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.AllDeviceTypesEndpoint
            };

            var deviceTypes = await _repository.GetAsync<List<DeviceType>>(builder.ToString());

            return deviceTypes;
        }

        public async Task CreateDeviceTypeAsync(string description)
        {
            DeviceType deviceType = new DeviceType
            {
                DeviceTypeId = await GenerateDeviceTypeId(),
                Description = description
            };

            await AddDeviceTypeToDatabase(deviceType);
        }

        private async Task<int> GenerateDeviceTypeId()
        {
            int count = (await GetAllDeviceTypesAsync()).ToList().Count;

            return count + 1;
        }

        private async Task AddDeviceTypeToDatabase(DeviceType deviceType)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.AddDeviceTypeEndpoint
            };

            await _repository.PostAsync<DeviceType>(builder.ToString(), deviceType);
        }
    }
}
