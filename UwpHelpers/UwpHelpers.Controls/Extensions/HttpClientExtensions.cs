using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Storage;
using UwpHelpers.Controls.Common;

namespace UwpHelpers.Controls.Extensions
{
    /// <summary>
    /// A set of extension methods for System.Net.Http.HttpClient. These can be used in PCLs (e.g. Xamarin apps).
    /// Note- Windows.Web.Http.HttpClient has support for Progress. If you do not need System.Net.Http, investigate the possibility of using Windows.Web.Http
    /// </summary>
    public static class HttpClientExtensions
    {
        /// <summary>
        /// Helper method to POST binary image data to an API endpoint that expects the data to be accompanied by a parameter
        /// </summary>
        /// <param name="client">HttpClient instance</param>
        /// <param name="imageFile">Valie StorageFile of the image</param>
        /// <param name="url">The API's http or https endpoint</param>
        /// <param name="progessReporter">Progress reporter to track progress of the download operation</param>
        /// <param name="parameterName">The name of the parameter the API expects the image data in</param>
        /// <returns></returns>
        public static async Task<string> SendImageDataWithDownloadProgressAsync(this HttpClient client, StorageFile imageFile, string url, string parameterName, IProgress<DownloadProgressArgs> progessReporter)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client), "HttpClient was null");

            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url), "You must set a URL for the the HttpClient");

            if (imageFile == null)
                throw new ArgumentNullException(nameof(imageFile), "You must have a valid StorageFile for this method to work");

            if (string.IsNullOrEmpty(parameterName))
                throw new ArgumentNullException(nameof(parameterName), "You must set a parameter name for the image data");

            if (progessReporter == null)
                throw new ArgumentNullException(nameof(progessReporter), "ProgressReporter was null");

            try
            {
                client.DefaultRequestHeaders.ExpectContinue = false;

                byte[] fileBytes;
                using (var fileStream = await imageFile.OpenStreamForReadAsync())
                {
                    var binaryReader = new BinaryReader(fileStream);
                    fileBytes = binaryReader.ReadBytes((int) fileStream.Length);
                }

                var multipartContent = new MultipartFormDataContent();
                multipartContent.Add(new ByteArrayContent(fileBytes), parameterName);

                using (var response = await client.PostAsync(new Uri(url), multipartContent))
                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var memStream = new MemoryStream())
                {
                    int receivedBytes = 0;
                    var totalBytes = Convert.ToInt32(response.Content.Headers.ContentLength);

                    while (true)
                    {
                        var buffer = new byte[4096];
                        int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);

                        //write the current loop's bytes into the MemoryStream that will be returned
                        await memStream.WriteAsync(buffer, 0, bytesRead);

                        if (bytesRead == 0)
                        {
                            break;
                        }

                        receivedBytes += bytesRead;

                        var args = new DownloadProgressArgs(receivedBytes, receivedBytes);
                        progessReporter.Report(args);

                        Debug.WriteLine($"Progress: {receivedBytes} of {totalBytes} bytes read");
                    }

                    memStream.Position = 0;
                    var stringContent = new StreamReader(memStream);
                    return stringContent.ReadToEnd();
                }
            }
            finally
            {
                client.Dispose();
            }
        }

        /// <summary>
        /// Stand-in replacement for HttpClient.GetStreamAsync that can report download progress.
        /// </summary>
        /// <param name="client">HttpClient instance</param>
        /// <param name="url">Url of where to download the stream from</param>
        /// <param name="progessReporter">Progress reporter to track progress of the download operation</param>
        /// <returns>Stream result of the GET request</returns>
        public static async Task<Stream> DownloadStreamWithProgressAsync(this HttpClient client, string url, IProgress<DownloadProgressArgs> progessReporter)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client), "HttpClient was null");

            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url), "You must set a URL for the API endpoint");
            
            if (progessReporter == null)
                throw new ArgumentNullException(nameof(progessReporter), "ProgressReporter was null");

            try
            {
                client.DefaultRequestHeaders.ExpectContinue = false;

                using (var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    int receivedBytes = 0;
                    var totalBytes = Convert.ToInt32(response.Content.Headers.ContentLength);

                    var memStream = new MemoryStream();

                    while (true)
                    {
                        var buffer = new byte[4096];
                        int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);

                        //write the current loop's bytes into the MemoryStream that will be returned
                        await memStream.WriteAsync(buffer, 0, bytesRead);

                        if (bytesRead == 0)
                        {
                            break;
                        }

                        receivedBytes += bytesRead;

                        var args = new DownloadProgressArgs(receivedBytes, totalBytes);
                        progessReporter.Report(args);
                    }

                    memStream.Position = 0;
                    return memStream;
                }
            }
            finally
            {
                client.Dispose();
            }
        }

        /// <summary>
        /// Stand-in replacement for HttpClient.GetStringAsync that can report download progress.
        /// </summary>
        /// <param name="client">HttpClient instance</param>
        /// <param name="url">Url of where to download the stream from</param>
        /// <param name="progessReporter">Progress reporter to track progress of the download operation</param>
        /// <returns>String result of the GET request</returns>
        public static async Task<string> DownloadStringWithProgressAsync(this HttpClient client, string url, IProgress<DownloadProgressArgs> progessReporter)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client), "HttpClient was null");

            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url), "You must set a URL for the API endpoint");

            if (progessReporter == null)
                throw new ArgumentNullException(nameof(progessReporter), "ProgressReporter was null");

            try
            {
                client.DefaultRequestHeaders.ExpectContinue = false;

                using (var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var memStream = new MemoryStream())
                {
                    int receivedBytes = 0;
                    var totalBytes = Convert.ToInt32(response.Content.Headers.ContentLength);

                    while (true)
                    {
                        var buffer = new byte[4096];
                        int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);

                        //write the current loop's bytes into the MemoryStream that will be returned
                        await memStream.WriteAsync(buffer, 0, bytesRead);

                        if (bytesRead == 0)
                        {
                            break;
                        }

                        receivedBytes += bytesRead;

                        var args = new DownloadProgressArgs(receivedBytes, totalBytes);
                        progessReporter.Report(args);
                    }

                    memStream.Position = 0;

                    var stringContent = new StreamReader(memStream);
                    return stringContent.ReadToEnd();
                }
            }
            finally
            {
                client.Dispose();
            }
        }
    }
}
