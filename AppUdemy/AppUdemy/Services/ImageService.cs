using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Applier.Services
{
    public class ImageService
    {
        const int DownloadImageTimeoutSeconds = 15;

        readonly HttpClient _HttpClient = new HttpClient 
        {
            Timeout = TimeSpan.FromSeconds(DownloadImageTimeoutSeconds)
        };

        // Función para descargar una imagen desde su url
        async Task<byte[]> DownloadImageAsync(string imageUrl)
        {
            try
            {
                using (HttpResponseMessage httpResponse = await _HttpClient.GetAsync(imageUrl))
                {
                    if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return await httpResponse.Content.ReadAsByteArrayAsync();
                    }
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        // Función para descargar una imagen y regresarla en formato base 64
        public async Task<string> DownloadImageAsBase64Async(string imageUrl)
        {
            byte[] imageByteArray = await DownloadImageAsync(imageUrl);
            return System.Convert.ToBase64String(imageByteArray);
        }

        // Función para convertir una imagen de base 64 a ImageSource
        public ImageSource ConvertImageFromBase64ToImageSource(string imageBase64)
        {
            if (!string.IsNullOrEmpty(imageBase64))
            {
                return ImageSource.FromStream(() => 
                    new MemoryStream(System.Convert.FromBase64String(imageBase64))
                );
            }
            return null;
        }

        // Función para convertir una imagen desde su ruta al formato base 64
        public async Task<string> ConvertImageFileToBase64(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                FileStream stream = File.Open(filePath, FileMode.Open);
                byte[] bytes = new byte[stream.Length];
                await stream.ReadAsync(bytes, 0, (int)stream.Length);
                return System.Convert.ToBase64String(bytes);
            }
            return String.Empty;
        }
    }
}