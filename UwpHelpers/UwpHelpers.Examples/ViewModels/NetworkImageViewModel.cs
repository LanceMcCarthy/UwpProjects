using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using UwpHelpers.Examples.Models;

namespace UwpHelpers.Examples.ViewModels
{
    public class NetworkImageViewModel : INotifyPropertyChanged
    {
        private bool isBusy;
        private ObservableCollection<string> images;

        public NetworkImageViewModel()
        {

        }

        public ObservableCollection<string> Images
        {
            get { return images ?? (images = new ObservableCollection<string>()); }
            set { images = value; OnPropertyChanged(); }
        }

        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value; OnPropertyChanged(); }
        }

        public async Task LoadImagesAsync()
        {
            try
            {
                IsBusy = true;
                
                using (var client = new HttpClient())
                {
                    var response = await client.GetStringAsync(@"http://www.bing.com/HPImageArchive.aspx?format=js&idx=0&n=8");

                    byte[] bytes = Encoding.Unicode.GetBytes(response);
                    using (var stream = new MemoryStream(bytes))
                    {
                        var serializer = new DataContractJsonSerializer(typeof(BingImagesRoot));
                        var result = (BingImagesRoot) serializer.ReadObject(stream);

                        if (result.images.Any())
                        {
                            Images.Clear();

                            foreach (var bingImage in result.images)
                            {
                                Images.Add($"http://www.bing.com/{bingImage.url}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await new MessageDialog($"Error: {ex.Message}", "Problem downloading data").ShowAsync();
            }
            finally
            {
                IsBusy = false;
            }
        }

        #region INPC

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
