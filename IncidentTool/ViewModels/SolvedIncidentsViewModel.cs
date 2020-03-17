using GalaSoft.MvvmLight.Command;
using IncidentTool.Container;
using IncidentTool.Interfaces.Services.Data;
using IncidentTool.Models;
using IncidentTool.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncidentTool.ViewModels
{
    public class SolvedIncidentsViewModel : ViewModelBase
    {
        // Membervariabelen
        private IList<OccurredIncident> _solvedOccurredIncidents;
        private Device _tempDevice;
        private string _message;
        private OccurredIncident _selectedOccurredIncident;
        private readonly IOccurredIncidentDataService _occurredIncidentDataService; // Service om te werken met OccurredIncident-data
        private readonly IDeviceDataService _deviceDataService; // Service om te werken met Device-data
        private readonly IUserDataService _userDataService; // Service om te werken met User-data


        // Properties
        public RelayCommand MarkAsUnsolvedCommand { get; set; }

        public IList<OccurredIncident> SolvedOccurredIncidents
        {
            get { return _solvedOccurredIncidents; }
            set
            {
                _solvedOccurredIncidents = value;
                OnPropertyChanged();
            }
        }

        public OccurredIncident SelectedOccurredIncident
        {
            get { return _selectedOccurredIncident; }
            set
            {
                _selectedOccurredIncident = value;
                OnPropertyChanged();
            }
        }

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }


        // Constructor
        public SolvedIncidentsViewModel() : base()
        {
            // Services initialiseren
            _occurredIncidentDataService = (IOccurredIncidentDataService)AppContainer.Instance.Resolve(typeof(IOccurredIncidentDataService));
            _deviceDataService = (IDeviceDataService)AppContainer.Instance.Resolve(typeof(IDeviceDataService));
            _userDataService = (IUserDataService)AppContainer.Instance.Resolve(typeof(IUserDataService));

            InitCommands();
            LoadSolvedOccurredIncidents(); 
        }


        // Methods
        private void InitCommands()
        {
            MarkAsUnsolvedCommand = new RelayCommand(MarkAsUnsolved);
        }

        private async Task LoadSolvedOccurredIncidents()
        {
            // Alle opgeloste incidenten ophalen
            var solvedOccurredIncidentsOriginal = (await _occurredIncidentDataService.GetAllSolvedOccurredIncidentsAsync()).ToList();
            IList<OccurredIncident> tempOccurredIncidents = new List<OccurredIncident>();

            // Voor ieder opgelost incident een nieuw OccurredIncident-object aanmaken met enkele andere gegevens (deze moeten getoond worden in UI)
            foreach(OccurredIncident incident in solvedOccurredIncidentsOriginal)
            {
                await InitDeviceById(incident.DeviceId);

                OccurredIncident occurredIncident = new OccurredIncident
                {
                    OccurredIncidentId = incident.OccurredIncidentId,
                    DeviceId = incident.DeviceId,
                    DeviceName = _tempDevice.Name,
                    DeviceLocation = _tempDevice.Location,
                    IncidentDescription = incident.IncidentDescription,
                    CurrentUserId = incident.CurrentUserId,
                    UserName = await GetUserNameById(incident.CurrentUserId),
                    Solved = incident.Solved
                };

                // Nieuw OccurredIncident-object toevoegen aan de lijst
                tempOccurredIncidents.Add(occurredIncident);
            }

            SolvedOccurredIncidents = tempOccurredIncidents;
        }

        private async Task InitDeviceById(int deviceId)
        {
            _tempDevice = await _deviceDataService.GetDeviceByIdAsync(deviceId);
        }

        private async Task<string> GetUserNameById(int userId)
        {
            var name = (await _userDataService.GetUserByIdAsync(userId)).Name;
            return name.Substring(0, 1).ToUpper() + name.Substring(1);
        }

        // Command methods
        public async void MarkAsUnsolved()
        {
            Message = "";

            if (_selectedOccurredIncident == null)
            {
                Message = "Selecteer een incident";
            }
            else
            {
                await _occurredIncidentDataService.SetOccurredIncidentUnsolvedAsync(_selectedOccurredIncident.OccurredIncidentId);

                _selectedOccurredIncident = null;
                Message = "Het geselecteerde incident is succesvol gemarkeerd als 'niet opgelost'";

                // De lijst refreshen
                await Task.Delay(1000);
                await LoadSolvedOccurredIncidents();
            }
        }
    }
}
