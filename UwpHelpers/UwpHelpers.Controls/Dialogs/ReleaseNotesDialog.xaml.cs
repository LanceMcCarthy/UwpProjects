using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

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

        public static readonly DependencyProperty AddedFeaturesProperty = DependencyProperty.Register(
            "AddedFeatures", typeof(List<string>), typeof(ReleaseNotesDialog), new PropertyMetadata(new List<string>()));

        /// <summary>
        /// This is the list that will be displayed under the 'New Features' section
        /// NOTE - this is readonly, use AddedFeatures.Add(string) to add a feature
        /// </summary>
        public List<string> AddedFeatures => (List<string>) GetValue(AddedFeaturesProperty);

        
        public static readonly DependencyProperty BugFixesProperty = DependencyProperty.Register(
            "BugFixes", typeof(List<string>), typeof(ReleaseNotesDialog), new PropertyMetadata(new List<string>()));

        /// <summary>
        /// This is the list that will be shown under the 'Bug Fixes' section
        /// NOTE - this is readonly, use BugFixes.Add(string) to add a fix
        /// </summary>
        public List<string> BugFixes => (List<string>) GetValue(BugFixesProperty);

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

            FixesListView.Visibility = BugFixes.Count == 0? Visibility.Collapsed : Visibility.Visible;
            FeaturesListView.Visibility = AddedFeatures.Count == 0 ? Visibility.Collapsed : Visibility.Visible;
        }
    }
}
