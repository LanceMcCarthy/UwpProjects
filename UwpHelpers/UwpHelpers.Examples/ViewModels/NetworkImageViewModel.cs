using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using UwpHelpers.Examples.Annotations;

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

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        #endregion
    }
    
    #region demo models

    [DataContract]
    public class BingImagesRoot
    {
        [DataMember]
        public List<BingImage> images { get; set; }
        [DataMember]
        public Tooltips tooltips { get; set; }
    }

    [DataContract]
    public class Tooltips
    {
        [DataMember]
        public string loading { get; set; }
        [DataMember]
        public string previous { get; set; }
        [DataMember]
        public string next { get; set; }
        [DataMember]
        public string walle { get; set; }
        [DataMember]
        public string walls { get; set; }
    }

    [DataContract]
    public class BingImage
    {
        [DataMember]
        public string startdate { get; set; }
        [DataMember]
        public string fullstartdate { get; set; }
        [DataMember]
        public string enddate { get; set; }
        [DataMember]
        public string url { get; set; }
        [DataMember]
        public string urlbase { get; set; }
        [DataMember]
        public string copyright { get; set; }
        [DataMember]
        public string copyrightlink { get; set; }
        [DataMember]
        public bool wp { get; set; }
        [DataMember]
        public string hsh { get; set; }
        [DataMember]
        public int drk { get; set; }
        [DataMember]
        public int top { get; set; }
        [DataMember]
        public int bot { get; set; }
        [DataMember]
        public List<ImageHints> hs { get; set; }
        [DataMember]
        public object[] msg { get; set; }
    }

    [DataContract]
    public class ImageHints
    {
        [DataMember]
        public string desc { get; set; }
        [DataMember]
        public string link { get; set; }
        [DataMember]
        public string query { get; set; }
        [DataMember]
        public int locx { get; set; }
        [DataMember]
        public int locy { get; set; }
    }

    #endregion
}
