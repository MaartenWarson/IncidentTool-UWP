using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace IncidentTool.Converters
{
    public class UserConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string user = (string)value;

            return $"Door {user}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            string user = (string)value;
            user = user.Substring(3); // 'Door ' ervan afknippen

            return user;
        }
    }
}
