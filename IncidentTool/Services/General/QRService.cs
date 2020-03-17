using IncidentTool.Interfaces.Services.General;
using IncidentTool.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Media.Imaging;
using ZXing;

namespace IncidentTool.Services.General
{
    public class QRService : IQRService
    {
        public WriteableBitmap CreateQRCode(Device device)
        {
            if (device != null)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(device.DeviceId).Append(",");
                builder.Append(device.Name).Append(",");
                builder.Append(device.Location).Append(",");
                builder.Append(device.CurrentDeviceTypeId);

                BarcodeWriter barcodeWriter = new BarcodeWriter();
                barcodeWriter.Format = BarcodeFormat.QR_CODE;

                return barcodeWriter.Write(builder.ToString());
            }

            return null;
            
        }

        public async Task SaveQRCodeAsync(WriteableBitmap qRImageUrl)
        {
            FileSavePicker savePicker = new FileSavePicker();
            savePicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            savePicker.FileTypeChoices.Add("Image", new List<string>() { ".jpg", ".png" });
            savePicker.SuggestedFileName = "QRCode";

            StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                using (var fileStream = await file.OpenAsync(FileAccessMode.ReadWrite))
                {
                    BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, fileStream);
                    Stream pixelStream = qRImageUrl.PixelBuffer.AsStream();
                    byte[] pixels = new byte[pixelStream.Length];
                    await pixelStream.ReadAsync(pixels, 0, pixels.Length);
                    encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore,
                        (uint)qRImageUrl.PixelWidth,
                        (uint)qRImageUrl.PixelHeight,
                        150.0,
                        150.0,
                        pixels);
                    await encoder.FlushAsync();
                }
            }
        }
    }
}
