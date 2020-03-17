using GalaSoft.MvvmLight.Command;
using IncidentTool.Container;
using IncidentTool.Interfaces.Services.Data;
using IncidentTool.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncidentTool.ViewModels
{
    public class CreateDeviceTypeViewModel : ViewModelBase
    {
        // Membervariabelen
        private string _description;
        private string _message;
        private readonly IDeviceTypeDataService _deviceTypeDataService; // Service om te werken met DeviceType-data


        // Properties
        public RelayCommand CreateDeviceTypeCommand { get;  set; }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
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
        public CreateDeviceTypeViewModel() : base()
        {
            // Services initialiseren
            _deviceTypeDataService = (IDeviceTypeDataService)AppContainer.Instance.Resolve(typeof(IDeviceTypeDataService));

            InitCommands();
        }


        // Methods
        private void InitCommands()
        {
            CreateDeviceTypeCommand = new RelayCommand(CreateDeviceType);
        }


        // Command Methods
        public void CreateDeviceType()
        {
            Message = "";

            if (_description == null)
            {
                Message = "Vul een omschrijving in";
            }
            else
            {
                _deviceTypeDataService.CreateDeviceTypeAsync(_description);

                Message = "Toesteltype is succesvol aangemaakt";
                Description = null;
            }
        }
    }
}
