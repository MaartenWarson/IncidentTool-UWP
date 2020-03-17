using Hangfire.Annotations;
using IncidentTool.Interfaces.Services.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace IncidentTool.ViewModels.Base
{
    // Deze ViewModelBase wordt gebruikt zodat we deze code niet in ieder ViewModel opnieuw moeten schrijven
    public class ViewModelBase : INotifyPropertyChanged
    {
        // Code voor INotifyPropertyChanged (nodig voor databindings)
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual Task InitializeAsync(object data)
        {
            return Task.FromResult(false);
        }
    }
}
