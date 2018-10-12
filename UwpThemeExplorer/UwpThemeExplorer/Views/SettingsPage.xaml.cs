using System;

using UwpThemeExplorer.ViewModels;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UwpThemeExplorer.Views
{
    // TODO WTS: Change the URL for your privacy policy in the Resource File, currently set to https://YourPrivacyUrlGoesHere
    public sealed partial class SettingsPage : Page
    {
        public SettingsViewModel ViewModel { get; } = new SettingsViewModel();

        public SettingsPage()
        {
            InitializeComponent();
            ViewModel.Initialize();
        }
    }
}
