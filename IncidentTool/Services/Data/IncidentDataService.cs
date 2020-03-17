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
    public class IncidentDataService : IIncidentDataService
    {
        private readonly IGenericRepository _repository; // Taken m.b.t. databasebewerkingen worden doorgegeven aan deze repository

        public IncidentDataService(IGenericRepository repository)
        {
            _repository = repository;
        }

        public async Task<IList<Incident>> GetAllIncidentsAsync()
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.AllIncidentsEndpoint
            };

            var incidents = await _repository.GetAsync<List<Incident>>(builder.ToString());

            return incidents;
        }

        public async Task CreateIncidentAsync(string description, int deviceTypeId)
        {
            Incident incident = new Incident()
            {
                IncidentId = await GenerateIncidentId(),
                Description = description,
                CurrentDeviceTypeId = deviceTypeId
            };

            await AddIncidentToDatabase(incident);
        }

        private async Task<int> GenerateIncidentId()
        {
            int count = (await GetAllIncidentsAsync()).ToList().Count;

            return count + 1;
        }

        private async Task AddIncidentToDatabase(Incident incident)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.AddIncidentEndpoint
            };

            await _repository.PostAsync<Incident>(builder.ToString(), incident);
        }
    }
}
