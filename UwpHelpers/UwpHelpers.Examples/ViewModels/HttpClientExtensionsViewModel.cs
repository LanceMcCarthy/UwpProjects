using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media.Imaging;
using UwpHelpers.Controls.Common;
using UwpHelpers.Controls.Extensions;
using UwpHelpers.Examples.Annotations;

namespace UwpHelpers.Examples.ViewModels
{
    public class HttpClientExtensionsViewModel : INotifyPropertyChanged
    {
        private BitmapImage downloadedImage;
        private string downloadedString;
        private double downloadProgress;
        private bool isBusy;
        private string isBusyMessage;
        private ICommand downloadImageCommand;
        
        public HttpClientExtensionsViewModel()
        {
            
        }

        #region properties

        public BitmapImage DownloadedImage
        {
            get { return downloadedImage; }
            set { downloadedImage = value; OnPropertyChanged(); }
        }

        public string DownloadedString
        {
            get { return downloadedString; }
            set { downloadedString = value; OnPropertyChanged();}
        }

        public double DownloadProgress
        {
            get { return downloadProgress; }
            set { downloadProgress = value; OnPropertyChanged(); }
        }

        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value; OnPropertyChanged();}
        }

        public string IsBusyMessage
        {
            get { return isBusyMessage; }
            set { isBusyMessage = value; OnPropertyChanged();}
        }

        public ICommand DownloadImageCommand => downloadImageCommand ?? (downloadImageCommand=new DelegateCommand(async ()=> await GetImageAsync()));

        #endregion

        #region methods and event handlers

        private async Task GetImageAsync()
        {
            IsBusy = true;
            DownloadProgress = 0;
            
            //hook into the ProgressChanged event, this is where the progress is reported (note there is a DownloadProgresseventArgs in Windows.Imaging but we're using our own
            var reporter = new Progress<DownloadProgressArgs>();
            reporter.ProgressChanged += Reporter_ProgressChanged;

            try
            {
                IsBusy = true;
                
                var handler = new HttpClientHandler();
                if (handler.SupportsAutomaticDecompression)
                    handler.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                
                var bigImageUrl = $"http://www.tomswallpapers.com/images/201505/tomswallpapers.com_28074.jpg?dontCacheMeBro={DateTime.Now.Ticks}"; 

                //be a good citizen and dispose the stream
                using (var imageStream = await new HttpClient(handler).DownloadStreamWithProgressAsync(bigImageUrl, reporter))
                {
                    //I'm using BitmapImage, but do what you want with the returnes Stream (to disk, to LumiaSDK effect, to Win2D effect, etc)

                    DownloadedImage = new BitmapImage();
                    await DownloadedImage.SetSourceAsync(imageStream.AsRandomAccessStream());
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"HttpClientExtensionsViewModel.GetImageAsync Exception\r\n{ex}");
                await new MessageDialog("Whoops, something went wrong downloading the image. See Debug Output for details").ShowAsync();
            }
            finally
            {
                reporter.ProgressChanged -= Reporter_ProgressChanged;
                IsBusy = false;
                IsBusyMessage = "";
            }
        }

        //This is the event handler to update your UI of progress (there is no need to use UI Dispatcher)
        private void Reporter_ProgressChanged(object sender, DownloadProgressArgs e)
        {
            DownloadProgress = e.PercentComplete;
            IsBusyMessage = $"downloading {e.PercentComplete.ToString("N2")}%";
        }

        #endregion

        #region INPC

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
