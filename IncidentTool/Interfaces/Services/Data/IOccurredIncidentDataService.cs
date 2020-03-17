using IncidentTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncidentTool.Interfaces.Services.Data
{
    public interface IOccurredIncidentDataService
    {
        Task<IList<OccurredIncident>> GetAllUnsolvedOccurredIncidentsAsync();
        Task<IList<OccurredIncident>> GetAllSolvedOccurredIncidentsAsync();
        Task SetOccurredIncidentSolvedAsync(int id);
        Task SetOccurredIncidentUnsolvedAsync(int id);
    }
}
