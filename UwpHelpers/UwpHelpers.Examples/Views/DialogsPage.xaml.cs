using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using UwpHelpers.Controls.Dialogs;

namespace UwpHelpers.Examples.Views
{
    public sealed partial class DialogsPage : Page
    {
        public DialogsPage()
        {
            this.InitializeComponent();
        }

        private async void ReleaseNotesDialogButton_OnClick(object sender, RoutedEventArgs e)
        {
            var rnd = new ReleaseNotesDialog();

            rnd.Message = "Thank you for checking out ReleaseNotesDialog! Here's a list of what's new and what's fixed: (note, if you dont add any features or fixes, the list will hide itself)";

            rnd.Features.Add("New Feature 1!");
            rnd.Features.Add("New Feature 2!");
            rnd.Features.Add("New Feature 3!");

            rnd.Fixes.Add("Fixed crash when opening");
            rnd.Fixes.Add("Added text wrapping to fix text being cut off");

            await rnd.ShowAsync();
        }
    }
}
