using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace IncidentTool.Interfaces.Locator
{
    public interface IViewModelLocator
    {
        Page NavigateTo(string pageKey);
    }
}
