using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace IncidentTool.Interfaces.Navigation
{
    public interface INavigationService
    {
        Page NavigateTo(string pageKey);
    }
}
