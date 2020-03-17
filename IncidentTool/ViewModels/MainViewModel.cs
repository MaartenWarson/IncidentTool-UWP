using GalaSoft.MvvmLight.Command;
using IncidentTool.Container;
using IncidentTool.Interfaces.Navigation;
using IncidentTool.Interfaces.Services.General;
using IncidentTool.ViewModels.Base;
using IncidentTool.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace IncidentTool.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        // Membervariabelen
        private bool _isPaneOpen;
        private readonly INavigationService _navigationService; // Service om te werken met de navigatie
        private Page _page; // Wordt gebruikt om de juiste View in de MainPage te tonen (databinding)


        // Properties
        public RelayCommand HamburgerCommand { get; set; }
        public RelayCommand HomeCommand { get; set; }
        public RelayCommand CreateDeviceCommand { get; set; }
        public RelayCommand CreateDeviceTypeCommand { get; set; }
        public RelayCommand CreateIncidentCommand { get; set; }
        public RelayCommand CreateQRCommand { get; set; }
        public RelayCommand SolvedIncidentsCommand { get; set; }
        public RelayCommand UnsolvedIncidentsCommand { get; set; }

        public bool IsPaneOpen
        {
            get { return _isPaneOpen; }
            set
            {
                _isPaneOpen = value;
                OnPropertyChanged();
            }
        }

        public Page Page
        {
            get { return _page; }
            set
            {
                _page = value;
                OnPropertyChanged();
            }
        }


        // Constructor
        public MainViewModel() : base()
        {
            // Services initialiseren
            _navigationService = (INavigationService)AppContainer.Instance.Resolve(typeof(INavigationService));

            InitCommands();

            Page = new HomeView();
        }

        private void InitCommands()
        {
            HamburgerCommand = new RelayCommand(OpenAndCloseSplitview);
            HomeCommand = new RelayCommand(OpenHomeView);
            CreateDeviceCommand = new RelayCommand(OpenCreateDeviceView);
            CreateDeviceTypeCommand = new RelayCommand(OpenCreateDeviceTypeView);
            CreateIncidentCommand = new RelayCommand(OpenCreateIncidentView);
            CreateQRCommand = new RelayCommand(OpenCreateQRView);
            SolvedIncidentsCommand = new RelayCommand(OpenSolvedIncidentsView);
            UnsolvedIncidentsCommand = new RelayCommand(OpenUnsolvedIncidentsView);
        }

        // Command methods
        public void OpenAndCloseSplitview()
        {
            IsPaneOpen = !IsPaneOpen;
        }

        public void OpenHomeView()
        {
            Page = _navigationService.NavigateTo("HomeView");
        }

        public void OpenCreateDeviceView()
        {
            Page = _navigationService.NavigateTo("CreateDeviceView");
        }

        public void OpenCreateDeviceTypeView()
        {
            Page = _navigationService.NavigateTo("CreateDeviceTypeView");
        }

        public void OpenCreateIncidentView()
        {
            Page = _navigationService.NavigateTo("CreateIncidentView");
        }

        public void OpenCreateQRView()
        {
            Page = _navigationService.NavigateTo("CreateQRView");
        }

        public void OpenSolvedIncidentsView()
        {
            Page = _navigationService.NavigateTo("SolvedIncidentsView");
        }

        public void OpenUnsolvedIncidentsView()
        {
            Page = _navigationService.NavigateTo("UnsolvedIncidentsView");
        }
    }
}
