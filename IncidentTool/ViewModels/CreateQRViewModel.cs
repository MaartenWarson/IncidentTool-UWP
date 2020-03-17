using GalaSoft.MvvmLight.Command;
using IncidentTool.Container;
using IncidentTool.Interfaces.Services.Data;
using IncidentTool.Interfaces.Services.General;
using IncidentTool.Models;
using IncidentTool.ViewModels.Base;
using IncidentTool.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;
using ZXing;

namespace IncidentTool.ViewModels
{
    public class CreateQRViewModel : ViewModelBase
    {      
        // Membervariabelen
        private WriteableBitmap _qRImageUrl;
        private IList<Device> _devices;
        private Device _selectedDevice;
        private string _message;
        private readonly IDeviceDataService _deviceDataService; // Service om te werken met DeviceType-data
        private readonly IQRService _qRService; // Service om te werken met QR-code


        // Properties
        public RelayCommand CreateQRCommand { get; set; }
        public RelayCommand SaveQRCommand { get; set; }

        public WriteableBitmap QRImageUrl
        {
            get { return _qRImageUrl; }
            set
            {
                _qRImageUrl = value;
                OnPropertyChanged();
            }
        }

        public IList<Device> Devices
        {
            get => _devices;
            set
            {
                _devices = value;
                OnPropertyChanged();
            }
        }

        public Device SelectedDevice
        {
            get => _selectedDevice;
            set
            {
                _selectedDevice = value;
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
        public CreateQRViewModel() : base()
        {
            // Services initialiseren
            _qRService = (IQRService) AppContainer.Instance.Resolve(typeof(IQRService));
            _deviceDataService = (IDeviceDataService)AppContainer.Instance.Resolve(typeof(IDeviceDataService));

            InitCommands();
            LoadDevices();
        }

        private void InitCommands()
        {
            CreateQRCommand = new RelayCommand(CreateQR);
            SaveQRCommand = new RelayCommand(SaveQR);
        }

        // Methodes
        private async Task LoadDevices()
        {
            Devices = (await _deviceDataService.GetAllDevicesAsync()).ToList();
        }

        // Command methods
        public void CreateQR()
        {
            Message = "";

            if (_selectedDevice == null)
            {
                Message = "Selecteer een toestel";
            }
            else
            {
                // WriteableBitmap die wordt teruggegeven wordt in QRImageUrl gezet (= wordt getoond op scherm op de voorziene plaats)
                QRImageUrl = _qRService.CreateQRCode(_selectedDevice);

                if (QRImageUrl != null)
                {
                    Message = "QR-code is succesvol aangemaakt";
                }
                else
                {
                    Message = "Er is iets fout gegaan, probeer het opnieuw";
                }
            } 
        }

        public void SaveQR()
        {
            Message = "";

            if (_qRImageUrl == null)
            {
                Message = "Je hebt nog geen QR-code aangemaakt. Maak eerst een code aan voordat je deze kan opslaan";
            }
            else
            {
                _qRService.SaveQRCodeAsync(_qRImageUrl);

                Message = "QR-code succesvol opgeslagen";
                QRImageUrl = null;
                SelectedDevice = null;
            }
        }
    }
}
