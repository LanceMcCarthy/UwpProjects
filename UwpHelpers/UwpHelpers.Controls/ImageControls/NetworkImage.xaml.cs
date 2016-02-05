using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace UwpHelpers.Controls.ImageControls
{
    public sealed partial class NetworkImage : UserControl
    {
        #region dependency properties

        public static readonly DependencyProperty ImageUrlProperty = DependencyProperty.Register(
            "ImageUrl", typeof (string), typeof (NetworkImage), new PropertyMetadata(default(string)));

        public string ImageUrl
        {
            get { return (string) GetValue(ImageUrlProperty); }
            set { SetValue(ImageUrlProperty, value); }
        }

        public static readonly DependencyProperty ImageDecodeWidthProperty = DependencyProperty.Register(
            "ImageDecodeWidth", typeof (double), typeof (NetworkImage), new PropertyMetadata(default(double)));

        public double ImageDecodeWidth
        {
            get { return (double) GetValue(ImageDecodeWidthProperty); }
            set { SetValue(ImageDecodeWidthProperty, value); }
        }

        public static readonly DependencyProperty ImageDecodeHeightProperty = DependencyProperty.Register(
            "ImageDecodeHeight", typeof (double), typeof (NetworkImage), new PropertyMetadata(default(double)));

        public double ImageDecodeHeight
        {
            get { return (double) GetValue(ImageDecodeHeightProperty); }
            set { SetValue(ImageDecodeHeightProperty, value); }
        }

        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register(
            "IsActive", typeof (bool), typeof (NetworkImage), new PropertyMetadata(false));

        public bool IsActive
        {
            get { return (bool) GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        public static readonly DependencyProperty DownloadProgressStatusProperty = DependencyProperty.Register(
            "DownloadProgressStatus", typeof (string), typeof (NetworkImage), new PropertyMetadata(@"0%"));

        public string DownloadProgressStatus
        {
            get { return (string) GetValue(DownloadProgressStatusProperty); }
            set { SetValue(DownloadProgressStatusProperty, value); }
        }

        public static readonly DependencyProperty ImageStretchProperty = DependencyProperty.Register(
            "ImageStretch", typeof (Stretch), typeof (NetworkImage), new PropertyMetadata(default(Stretch)));

        public Stretch ImageStretch
        {
            get { return (Stretch) GetValue(ImageStretchProperty); }
            set { SetValue(ImageStretchProperty, value); }
        }

        public static readonly DependencyProperty DownloadPercentageVisibilityProperty = DependencyProperty.Register(
            "DownloadPercentageVisibility", 
            typeof (Visibility), 
            typeof (NetworkImage), 
            new PropertyMetadata(Visibility.Visible));

        public Visibility DownloadPercentageVisibility
        {
            get { return (Visibility) GetValue(DownloadPercentageVisibilityProperty); }
            set { SetValue(DownloadPercentageVisibilityProperty, value); }
        }

        public static readonly DependencyProperty ProgressRingVisibilityProperty = DependencyProperty.Register(
            "ProgressRingVisibility", 
            typeof (Visibility), 
            typeof (NetworkImage), 
            new PropertyMetadata(Visibility.Visible));

        public Visibility ProgressRingVisibility
        {
            get { return (Visibility) GetValue(ProgressRingVisibilityProperty); }
            set { SetValue(ProgressRingVisibilityProperty, value); }
        }

        #endregion

        public NetworkImage()
        {
            this.InitializeComponent();
            Loaded += NetworkImage_Loaded;
            Unloaded += NetworkImage_Unloaded;
        }

        #region event handlers

        private void NetworkImage_Unloaded(object sender, RoutedEventArgs e)
        {
            RootImageSource.DownloadProgress -= RootImageSource_DownloadProgress;
            RootImageSource.ImageOpened -= RootImageSource_ImageOpened;
            RootImageSource.ImageFailed -= RootImageSource_ImageFailed;
        }

        private void NetworkImage_Loaded(object sender, RoutedEventArgs e)
        {
            RootImageSource.DownloadProgress += RootImageSource_DownloadProgress;
            RootImageSource.ImageOpened += RootImageSource_ImageOpened;
            RootImageSource.ImageFailed += RootImageSource_ImageFailed;
        }

        private void RootImageSource_ImageOpened(object sender, RoutedEventArgs e)
        {
            IsActive = false;
            DownloadProgressStatus = string.Empty;
        }

        private void RootImageSource_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            IsActive = false;
            DownloadProgressStatus = $"Error: {e.ErrorMessage}%";
        }

        private void RootImageSource_DownloadProgress(object sender, Windows.UI.Xaml.Media.Imaging.DownloadProgressEventArgs e)
        {
            if(e.Progress < 100 && !IsActive)
                IsActive = true;
            DownloadProgressStatus = $"{e.Progress}%";
        }

        #endregion
    }
}
