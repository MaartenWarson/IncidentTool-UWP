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
    public class CreateIncidentViewModel : ViewModelBase
    {
        // Membervariabelen
        private string _description;
        private IList<DeviceType> _deviceTypes;
        private DeviceType _selectedDeviceType;
        private string _message;
        private readonly IDeviceTypeDataService _deviceTypeDataService; // Service om te werken met DeviceType-data
        private readonly IIncidentDataService _incidentDataService; // Service om te werken met Incident-data


        // Properties
        public RelayCommand CreateIncidentCommand { get; set; }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        public IList<DeviceType> DeviceTypes
        {
            get { return _deviceTypes; }
            set
            {
                _deviceTypes = value;
                OnPropertyChanged();
            }
        }

        public DeviceType SelectedDeviceType
        {
            get { return _selectedDeviceType; }
            set
            {
                _selectedDeviceType = value;
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
        public CreateIncidentViewModel()
        {
            // Services initialiseren
            _deviceTypeDataService = (IDeviceTypeDataService)AppContainer.Instance.Resolve(typeof(IDeviceTypeDataService));
            _incidentDataService = (IIncidentDataService)AppContainer.Instance.Resolve(typeof(IIncidentDataService));

            InitCommands();
            LoadDeviceTypes();
        }


        // Methods
        private void InitCommands()
        {
            CreateIncidentCommand = new RelayCommand(CreateIncident);
        }

        private async Task LoadDeviceTypes()
        {
            DeviceTypes = (await _deviceTypeDataService.GetAllDeviceTypesAsync()).ToList();
        }


        // Command methods
        public void CreateIncident()
        {
            Message = "";

            if (_description == null || _selectedDeviceType == null)
            {
                Message = "Vul alle velden in";
            }
            else
            {
                _incidentDataService.CreateIncidentAsync(_description, _selectedDeviceType.DeviceTypeId);

                Message = "Incident is succesvol aangemaakt";
                Description = null;
                SelectedDeviceType = null;
            }
        }
    }
}
