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
        /// A list of new app features to be shown in the dialog
        /// </summary>
        public ObservableCollection<string> Features
        {
            get { return (ObservableCollection<string>) GetValue(FeaturesProperty); }
            set { SetValue(FeaturesProperty, value); }
        }

        public static readonly DependencyProperty FixesProperty = DependencyProperty.Register(
            "Fixes", typeof(ObservableCollection<string>), typeof(ReleaseNotesDialog), new PropertyMetadata(new ObservableCollection<string>()));

        /// <summary>
        /// A list of bug fixes to be shown in the dialog
        /// </summary>
        public ObservableCollection<string> Fixes
        {
            get { return (ObservableCollection<string>) GetValue(FixesProperty); }
            set { SetValue(FixesProperty, value); }
        }

        public static readonly DependencyProperty UseFullVersionNumberProperty = DependencyProperty.Register(
            "UseFullVersionNumber", typeof(bool), typeof(ReleaseNotesDialog), new PropertyMetadata(default(bool)));

        public bool UseFullVersionNumber
        {
            get { return (bool) GetValue(UseFullVersionNumberProperty); }
            set { SetValue(UseFullVersionNumberProperty, value); }
        }

        public static readonly DependencyProperty AppNameProperty = DependencyProperty.Register(
            "AppName", typeof(string), typeof(ReleaseNotesDialog), new PropertyMetadata("Release Notes"));

        /// <summary>
        /// Use your appname for the title. 
        /// Note: Default Value is  "Release Notes"
        /// </summary>
        public string AppName
        {
            get { return (string) GetValue(AppNameProperty); }
            set { SetValue(AppNameProperty, value); }
        }
        
        public ReleaseNotesDialog()
        {
            this.InitializeComponent();
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

        private string AppVersion
        {
            get
            {
                if (DesignMode.DesignModeEnabled)
                    return "123.456.789.1004";

                var version = Package.Current.Id.Version;

                return UseFullVersionNumber 
                    ? $"{version.Major}.{version.Minor}.{version.Build}" 
                    : $"{version.Major}.{version.Minor}";
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
