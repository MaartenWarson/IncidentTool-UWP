using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using IncidentTool.Container;
using IncidentTool.Interfaces.Locator;
using IncidentTool.Interfaces.Services.General;
using IncidentTool.ViewModels;
using IncidentTool.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace IncidentTool.Services.Locator
{
    public class ViewModelLocator : IViewModelLocator
    {
        // Page keys declareren
        public const string HomeView = "HomeView";
        public const string CreateDeviceView = "CreateDeviceView";
        public const string CreateDeviceTypeView = "CreateDeviceTypeView";
        public const string CreateIncidentView = "CreateIncidentView";
        public const string CreateQRView = "CreateQRView";
        public const string UnsolvedIncidentsView = "UnsolvedIncidentsView";
        public const string SolvedIncidentsView = "SolvedIncidentsView";
        

        // Methods
        public Page NavigateTo(string pageKey)
        {
            switch(pageKey)
            {
                case HomeView:
                    return new HomeView();
                case CreateDeviceView:
                    return new CreateDeviceView();
                case CreateDeviceTypeView:
                    return new CreateDeviceTypeView();
                case CreateIncidentView:
                    return new CreateIncidentView();
                case CreateQRView:
                    return new CreateQRView();
                case UnsolvedIncidentsView:
                    return new UnsolvedIncidentsView();
                case SolvedIncidentsView:
                    return new SolvedIncidentsView();
                default:
                    return null;
            }
        }
    }
}
