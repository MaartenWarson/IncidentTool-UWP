using IncidentTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncidentTool.Interfaces.Services.Data
{
    public interface IIncidentDataService
    {
        Task<IList<Incident>> GetAllIncidentsAsync();
        Task CreateIncidentAsync(string description, int deviceTypeId);
    }
}
