using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using UwpHelpers.Controls.BusyIndicators.Primitives;

namespace UwpHelpers.Controls.BusyIndicators
{
    public sealed partial class DownloadUploadIndicator : UserControl
    {
        #region dependency properties

        public static readonly DependencyProperty DisplayMessageProperty = DependencyProperty.Register(
            "DisplayMessage",
            typeof(string),
            typeof(DownloadUploadIndicator),
            new PropertyMetadata(default(string), (o, e) =>
            {
                ((DownloadUploadIndicator) o).MessageTextBlock.Text = (string) e.NewValue;
            }));

        public string DisplayMessage
        {
            get { return (string) GetValue(DisplayMessageProperty); }
            set { SetValue(DisplayMessageProperty, value); }
        }

        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register(
            "IsActive",
            typeof(bool),
            typeof(DownloadUploadIndicator),
            new PropertyMetadata(false, (obj, args) =>
            {
                ((DownloadUploadIndicator) obj).IsActivePropertyChanged(args);
            }));

        private void IsActivePropertyChanged(DependencyPropertyChangedEventArgs args)
        {
            if ((bool) args.NewValue)
            {
                if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.Xaml.Media.Animation.RepeatBehavior"))
                {
                    DownloadingStory.RepeatBehavior = RepeatBehavior.Forever;
                }

                this.Visibility = this.BaseRectangle.Visibility = this.MessageTextBlock.Visibility = Visibility.Visible;
                
                switch (Direction)
                {
                    case AnimationDirection.Downloading:
                        StartDownloadAnimation();
                        break;
                    case AnimationDirection.Uploading:
                        StartUploadAnimation();
                        break;
                    default:
                        StartDownloadAnimation();
                        break;
                }
            }
            else
            {
                this.Visibility = this.BaseRectangle.Visibility = this.MessageTextBlock.Visibility = Visibility.Collapsed;
                DownloadingStory.Stop();
            }
        }

        public bool IsActive
        {
            get { return (bool) GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        public static readonly DependencyProperty DirectionProperty = DependencyProperty.Register(
            "Direction",
            typeof(AnimationDirection),
            typeof(DownloadUploadIndicator),
            new PropertyMetadata(AnimationDirection.Downloading, (obj, args) =>
            {
                ((DownloadUploadIndicator) obj).AnimationDirectionPropertyChanged(args);
            }));

        private void AnimationDirectionPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            //dont play a storyboard if the value is the same or if the control is not active
            if (e.NewValue == e.OldValue || !IsActive)
                return;

            switch ((AnimationDirection) e.NewValue)
            {
                case AnimationDirection.Downloading:
                    StartDownloadAnimation();
                    break;
                case AnimationDirection.Uploading:
                    StartUploadAnimation();
                    break;
                default:
                    StartDownloadAnimation();
                    break;
            }
        }

        public AnimationDirection Direction
        {
            get { return (AnimationDirection) GetValue(DirectionProperty); }
            set { SetValue(DirectionProperty, value); }
        }

        #endregion

        public DownloadUploadIndicator()
        {
            this.InitializeComponent();
        }

        private void StartDownloadAnimation()
        {
            if (UploadingStory.GetCurrentState() == ClockState.Active)
            {
                UploadingStory.Stop();
            }

            DownloadingStory.RepeatBehavior = RepeatBehavior.Forever;
            DownloadingStory.BeginTime = TimeSpan.Zero;
            DownloadingStory.Begin();
        }
        private void StartUploadAnimation()
        {
            if (DownloadingStory.GetCurrentState() == ClockState.Active)
            {
                DownloadingStory.Stop();
            }

            UploadingStory.RepeatBehavior = RepeatBehavior.Forever;
            UploadingStory.BeginTime = TimeSpan.Zero;
            UploadingStory.Begin();
        }
    }
}
