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
    public class OccurredIncidentDataService : IOccurredIncidentDataService
    {
        private readonly IGenericRepository _repository; // Taken m.b.t. databasebewerkingen worden doorgegeven aan deze repository

        public OccurredIncidentDataService(IGenericRepository repository)
        {
            _repository = repository;
        }

        public async Task<IList<OccurredIncident>> GetAllUnsolvedOccurredIncidentsAsync()
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.AllUnsolvedOccurredIncidentsEndpoint
            };

            var unsolvedOccurredIncidents = await _repository.GetAsync<List<OccurredIncident>>(builder.ToString());

            return unsolvedOccurredIncidents;
        }

        public async Task<IList<OccurredIncident>> GetAllSolvedOccurredIncidentsAsync()
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.AllSolvedOccurredIncidentsEndpoint
            };

            var solvedOccurredIncidents = await _repository.GetAsync<List<OccurredIncident>>(builder.ToString());

            return solvedOccurredIncidents;
        }

        public async Task SetOccurredIncidentSolvedAsync(int id)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.SetIncidentSolvedEndpoint(id)
            };

            await _repository.PutAsync<OccurredIncident>(builder.ToString());
        }

        public async Task SetOccurredIncidentUnsolvedAsync(int id)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.SetIncidentUnsolvedEndpoint(id)
            };

            await _repository.PutAsync<OccurredIncident>(builder.ToString());
        }
    }
}
