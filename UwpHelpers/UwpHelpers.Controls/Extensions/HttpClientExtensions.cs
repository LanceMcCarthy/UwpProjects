using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Storage;
using UwpHelpers.Controls.Common;

namespace UwpHelpers.Controls.Extensions
{
    public static class HttpClientExtensions
    {
        /// <summary>
        /// Helper method to POST binary image data to an API endpoint that expects the data to be accompanied by a parameter
        /// </summary>
        /// <param name="client">HttpClient instance</param>
        /// <param name="imageFile">Valie StorageFile of the image</param>
        /// <param name="url">The API's http or https endpoint</param>
        /// <param name="parameterName">The name of the parameter the API expects the image data in</param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> SendImageDataAsync(this HttpClient client, StorageFile imageFile, string url, string parameterName)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client), "HttpClient was null");

            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url), "You must set a URL for the API endpoint");

            if (imageFile == null)
                throw new ArgumentNullException(nameof(imageFile), "You must have a valid StorageFile for this method to work");

            if (string.IsNullOrEmpty(parameterName))
                throw new ArgumentNullException(nameof(parameterName), "You must set a parameter name for the image data");

            try
            {
                byte[] fileBytes = null;
                using (var fileStream = await imageFile.OpenStreamForReadAsync())
                {
                    var binaryReader = new BinaryReader(fileStream);
                    fileBytes = binaryReader.ReadBytes((int) fileStream.Length);
                }

                var multipartContent = new MultipartFormDataContent();
                multipartContent.Add(new ByteArrayContent(fileBytes), parameterName);
                return await client.PostAsync(new Uri(url), multipartContent);

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                client.Dispose();
            }
        }

        /// <summary>
        /// Helper method to POST binary image data to an API endpoint that expects the data to be accompanied by a parameter
        /// </summary>
        /// <param name="client">HttpClient instance</param>
        /// <param name="imageFile">Valie StorageFile of the image</param>
        /// <param name="apiUrl">The API's http or https endpoint</param>
        /// <param name="progessReporter">Progress reporter to track progress of the download operation</param>
        /// <param name="parameterName">The name of the parameter the API expects the image data in</param>
        /// <returns></returns>
        public static async Task<string> SendImageDataWithDownloadProgressAsync(this HttpClient client, StorageFile imageFile, string apiUrl, string parameterName, IProgress<DownloadProgressArgs> progessReporter)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client), "HttpClient was null");

            if (string.IsNullOrEmpty(apiUrl))
                throw new ArgumentNullException(nameof(apiUrl), "You must set a URL for the API endpoint");

            if (imageFile == null)
                throw new ArgumentNullException(nameof(imageFile), "You must have a valid StorageFile for this method to work");

            if (string.IsNullOrEmpty(parameterName))
                throw new ArgumentNullException(nameof(parameterName), "You must set a parameter name for the image data");

            if (progessReporter == null)
                throw new ArgumentNullException(nameof(progessReporter), "ProgressReporter was null");

            try
            {
                client.DefaultRequestHeaders.ExpectContinue = false;

                byte[] fileBytes = null;
                using (var fileStream = await imageFile.OpenStreamForReadAsync())
                {
                    var binaryReader = new BinaryReader(fileStream);
                    fileBytes = binaryReader.ReadBytes((int) fileStream.Length);
                }

                var multipartContent = new MultipartFormDataContent();
                multipartContent.Add(new ByteArrayContent(fileBytes), parameterName);
                var response = await client.PostAsync(new Uri(apiUrl), multipartContent);

                //Important - this makes it possible to rewind and re-read the stream
                await response.Content.LoadIntoBufferAsync();

                //NOTE - This Stream will need to be closed by the caller
                var stream = await response.Content.ReadAsStreamAsync();

                int receivedBytes = 0;
                var totalBytes = Convert.ToInt32(response.Content.Headers.ContentLength);

                while (true)
                {
                    var buffer = new byte[4096];
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                    {
                        break;
                    }

                    receivedBytes += bytesRead;

                    var args = new DownloadProgressArgs(receivedBytes, receivedBytes);
                    progessReporter.Report(args);

                    Debug.WriteLine($"Progress: {receivedBytes} of {totalBytes} bytes read");
                }

                stream.Position = 0;
                var stringContent = new StreamReader(stream);
                return stringContent.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw; 
            }
            finally
            {
                client.Dispose();
            }
        }

        /// <summary>
        /// Stand-in replacement for HttpClient.GetStreamAsync that can report download progress.
        /// IMPORTANT - The caller is responsible for disposing the Stream object
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

                var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);

                //Important - this makes it possible to rewind and re-read the stream
                await response.Content.LoadIntoBufferAsync();

                //NOTE - This Stream will need to be disposed by the caller
                var stream = await response.Content.ReadAsStreamAsync();

                int receivedBytes = 0;
                var totalBytes = Convert.ToInt32(response.Content.Headers.ContentLength);

                while (true)
                {
                    var buffer = new byte[4096];
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                    {
                        break;
                    }

                    receivedBytes += bytesRead;

                    var args = new DownloadProgressArgs(receivedBytes, receivedBytes);
                    progessReporter.Report(args);

                    Debug.WriteLine($"Progress: {receivedBytes} of {totalBytes} bytes read");
                }

                stream.Position = 0;
                return stream;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"DownloadStreamWithProgressAsync Exception\r\n{ex}");
                return null;
            }
            finally
            {
                client.Dispose();
            }
        }

        /// <summary>
        /// Stand-in replacement for HttpClient.GetStringAsync that can report download progress.
        /// IMPORTANT - The caller is responsible for disposing the Stream object
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

            using (var stream = await DownloadStreamWithProgressAsync(client, url, progessReporter))
            {
                if (stream == null)
                    return null;

                var stringContent = new StreamReader(stream);
                return stringContent.ReadToEnd();
            }
        }


        /*
        *******Example use of DownloadStreamWithProgress***********
        
        //----------------- In your GoGetSomethingFromTheInternet Task ------------------//

        var reporter = new Progress<DownloadProgressArgs>();

        //hook into the ProgressChanged event, this is where the progress is reported
        reporter.ProgressChanged += Reporter_ProgressChanged;
        
        using (var jsonOrLongStringResult = await new HttpClient.DownloadStringWithProgressAsync(fav.Photo.MediumUrl, reporter))
        {
            do something appropriate with the result, like JsonConvert.Deserialize<T>(jsonOrLongStringResult);
        }
       
        //when you're done, go ahead an unhook the event handler
        reporter.ProgressChanged -= Reporter_ProgressChanged;
        


        //-----------------------This is the event handler ------------------------------//

        private void Reporter_ProgressChanged(object sender, DownloadProgressArgs e)
        {
            SomeTextBlock.Text = $"{e.PercentComplete}% downloaded";
        }

        */
    }
}
