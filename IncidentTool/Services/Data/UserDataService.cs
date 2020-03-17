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
    public class UserDataService : IUserDataService
    {
        private readonly IGenericRepository _repository; // Taken m.b.t. databasebewerkingen worden doorgegeven aan deze repository

        public UserDataService(IGenericRepository repository)
        {
            _repository = repository;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.UserByIdEndpoint(id)
            };

            var user = await _repository.GetAsync<User>(builder.ToString());

            return user;
        }
    }
}
