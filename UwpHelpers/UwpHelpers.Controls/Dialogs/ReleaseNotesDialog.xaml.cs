using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UwpHelpers.Controls.Dialogs
{
    public sealed partial class ReleaseNotesDialog : ContentDialog
    {
        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(
            "Message", typeof(string), typeof(ReleaseNotesDialog), new PropertyMetadata(default(string)));

        /// <summary>
        /// This is used for the general message area
        /// </summary>
        public string Message
        {
            get { return (string) GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public static readonly DependencyProperty FeaturesProperty = DependencyProperty.Register(
            "Features", typeof(ObservableCollection<string>), typeof(ReleaseNotesDialog), new PropertyMetadata(new ObservableCollection<string>()));

        /// <summary>
        /// This is the list that will be displayed under the 'New Features' section
        /// Usage: Features.Add(string) to add a feature
        /// </summary>
        public ObservableCollection<string> Features => (ObservableCollection<string>) GetValue(FeaturesProperty);

        
        public static readonly DependencyProperty FixesProperty = DependencyProperty.Register(
            "Fixes", typeof(ObservableCollection<string>), typeof(ReleaseNotesDialog), new PropertyMetadata(new ObservableCollection<string>()));

        /// <summary>
        /// This is the list that will be shown under the 'Bug Fixes' section
        /// NOTE - this is readonly, use Fixes.Add(string) to add a fix
        /// </summary>
        public ObservableCollection<string> Fixes => (ObservableCollection<string>) GetValue(FixesProperty);

        public static readonly DependencyProperty UseFullVersionNumberProperty = DependencyProperty.Register(
            "UseFullVersionNumber", typeof(bool), typeof(ReleaseNotesDialog), new PropertyMetadata(default(bool)));

        public bool UseFullVersionNumber
        {
            get { return (bool) GetValue(UseFullVersionNumberProperty); }
            set { SetValue(UseFullVersionNumberProperty, value); }
        }
        
        public ReleaseNotesDialog()
        {
            this.InitializeComponent();
            Features.CollectionChanged += Features_CollectionChanged;
            Fixes.CollectionChanged += Fixes_CollectionChanged;
        }

        private void Fixes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            FixesListView.Visibility = ((ObservableCollection<string>) sender).Count > 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        private void Features_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            FeaturesListView.Visibility = ((ObservableCollection<string>)sender).Count > 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// Create a dialog with a message to the user
        /// </summary>
        /// <param name="message">Content to be shown to the user above the features and fixes lists</param>
        public ReleaseNotesDialog(string message)
        {
            this.InitializeComponent();
            this.Message = message;
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            
        }

        private string AppName
        {
            get
            {
                //return "App Name shows Here";

                if (DesignMode.DesignModeEnabled)
                    return "App Name shows Here";

                return Package.Current.Id.FullName;
            }
        }

        private string AppVersion
        {
            get
            {
                if (DesignMode.DesignModeEnabled)
                    return "123.456.789.1004";

                var version = Package.Current.Id.Version;

                return UseFullVersionNumber 
                    ? $"{version.Major}.{version.Minor}.{version.Build}.{version.Revision}" 
                    : $"{version.Major}.{version.Minor}.{version.Build}";
            }
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            FixesListView.Visibility = Fixes.Count == 0? Visibility.Collapsed : Visibility.Visible;
            FeaturesListView.Visibility = Features.Count == 0 ? Visibility.Collapsed : Visibility.Visible;
        }
    }
}
