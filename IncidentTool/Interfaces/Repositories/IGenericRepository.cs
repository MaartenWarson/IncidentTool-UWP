using IncidentTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncidentTool.Interfaces.Repositories
{
    public interface IGenericRepository
    {
        Task<T> GetAsync<T>(string uri);
        Task PostAsync<T>(string uri, T data);
        Task PutAsync<T>(string uri);
    }
}
