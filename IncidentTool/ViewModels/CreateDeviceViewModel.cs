using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using IncidentTool.Container;
using IncidentTool.Interfaces.Services.Data;
using IncidentTool.Interfaces.Services.General;
using IncidentTool.Models;
using IncidentTool.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncidentTool.ViewModels
{
    public class CreateDeviceViewModel : ViewModelBase
    {
        // Membervariabelen
        private string _name;
        private string _location;
        private IList<DeviceType> _deviceTypes;
        private DeviceType _selectedDeviceType;
        private string _message;
        private readonly IDeviceDataService _deviceDataService; // Service om te werken met Device-data
        private readonly IDeviceTypeDataService _deviceTypeDataService; // Service om te werken met DeviceType-data


        // Properties
        public RelayCommand CreateDeviceCommand { get; set; }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Location
        {
            get { return _location; }
            set
            {
                _location = value;
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
        public CreateDeviceViewModel() : base()
        {
            // Services initialiseren
            _deviceDataService = (IDeviceDataService)AppContainer.Instance.Resolve(typeof(IDeviceDataService));
            _deviceTypeDataService = (IDeviceTypeDataService)AppContainer.Instance.Resolve(typeof(IDeviceTypeDataService));

            InitCommands();
            LoadDeviceTypes();
        }


        // Methods
        private void InitCommands()
        {
            CreateDeviceCommand = new RelayCommand(CreateDevice);
        }

        private async Task LoadDeviceTypes()
        {
            DeviceTypes = (await _deviceTypeDataService.GetAllDeviceTypesAsync()).ToList();
        }


        // Command methods
        public void CreateDevice()
        {
            Message = "";

            if (_name == null || _selectedDeviceType == null || _location == null)
            {
                Message = "Vul alle velden in";
            }
            else
            {
                _deviceDataService.CreateDeviceAsync(_name, _selectedDeviceType.DeviceTypeId, _location);

                Message = "Toestel is succesvol aangemaakt";
                Name = null;
                SelectedDeviceType = null;
                Location = null;
            }
        }
    }
}
