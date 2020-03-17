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
    public class UnsolvedIncidentsViewModel : ViewModelBase
    {
        // Membervariabelen
        private Device _tempDevice;
        private OccurredIncident _selectedOccurrecIncident;
        private string _message;
        private IList<OccurredIncident> _unsolvedOccurredIncidents;
        private readonly IOccurredIncidentDataService _occurredIncidentDataService; // Service om te werken met OccurredIncident-data
        private readonly IDeviceDataService _deviceDataService; // Service om te werken met Device-data
        private readonly IUserDataService _userDataService;  // Service om te werken met User-data


        // Properties
        public RelayCommand MarkAsSolvedCommand { get; set; }

        public IList<OccurredIncident> UnsolvedOccurredIncidents
        {
            get { return _unsolvedOccurredIncidents; }
            set
            {
                _unsolvedOccurredIncidents = value;
                OnPropertyChanged();
            }
        }

        public OccurredIncident SelectedOccurredIncident
        {
            get { return _selectedOccurrecIncident; }
            set
            {
                _selectedOccurrecIncident = value;
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
        public UnsolvedIncidentsViewModel() : base()
        {
            // Services initialiseren
            _occurredIncidentDataService = (IOccurredIncidentDataService)AppContainer.Instance.Resolve(typeof(IOccurredIncidentDataService));
            _deviceDataService = (IDeviceDataService)AppContainer.Instance.Resolve(typeof(IDeviceDataService));
            _userDataService = (IUserDataService)AppContainer.Instance.Resolve(typeof(IUserDataService));

            InitCommands();
            LoadUnsolvedOccurredIncidents();
        }


        // Methods
        private void InitCommands()
        {
            MarkAsSolvedCommand = new RelayCommand(MarkAsSolved);
        }

        private async Task LoadUnsolvedOccurredIncidents()
        {
            // Alle onopgeloste incidenten ophalen
            var unsolvedOccuredIncidentsOriginal = (await _occurredIncidentDataService.GetAllUnsolvedOccurredIncidentsAsync()).ToList();
            IList<OccurredIncident> tempOccurredIncidents = new List<OccurredIncident>();

            // Voor ieder opgelost incident een nieuw OccurredIncident-object aanmaken met enkele andere gegevens (deze moeten getoond worden in UI)
            foreach (OccurredIncident incident in unsolvedOccuredIncidentsOriginal)
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

            UnsolvedOccurredIncidents = tempOccurredIncidents;
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
        public async void MarkAsSolved()
        {
            Message = "";

            if (_selectedOccurrecIncident == null)
            {
                Message = "Selecteer een incident";
            }
            else
            {
                await _occurredIncidentDataService.SetOccurredIncidentSolvedAsync(_selectedOccurrecIncident.OccurredIncidentId);

                _selectedOccurrecIncident = null;
                Message = "Het geselecteerde incident is succesvol gemarkeerd als 'opgelost'";

                // De lijst refreshen
                await Task.Delay(1000);
                await LoadUnsolvedOccurredIncidents();

            }
        }
    }
}
