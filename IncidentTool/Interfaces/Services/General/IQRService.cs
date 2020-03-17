using IncidentTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace IncidentTool.Interfaces.Services.General
{
    public interface IQRService
    {
        WriteableBitmap CreateQRCode(Device device);
        Task SaveQRCodeAsync(WriteableBitmap qRImageUrl);
    }
}
