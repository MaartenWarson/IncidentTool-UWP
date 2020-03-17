using IncidentTool.Interfaces.Locator;
using IncidentTool.Interfaces.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace IncidentTool.Services.Navigation
{
    public class NavigationService : INavigationService
    {
        // Membervariabelen
        private readonly IViewModelLocator _viewModelLocator; // Wordt gebruikt om effectief te navigeren


        // Constructor
        public NavigationService(IViewModelLocator viewModelLocator)
        {
            _viewModelLocator = viewModelLocator;
        }


        // Methods
        public Page NavigateTo(string pageKey)
        {
            return _viewModelLocator.NavigateTo(pageKey);
        }
    }
}
