using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UwpHelpers.Controls.Common;
using UwpHelpers.Controls.Dialogs;
using UwpHelpers.Examples.Annotations;

namespace UwpHelpers.Examples.ViewModels
{
    public class DialogsViewModel : INotifyPropertyChanged
    {
        private DelegateCommand showReleaseNotesCommand;
        private DelegateCommand showReleaseNotesWithFixesCommand;

        public DialogsViewModel()
        {
                
        }

        #region Commands

        public DelegateCommand ShowReleaseNotesCommand => showReleaseNotesCommand ?? (showReleaseNotesCommand = new DelegateCommand( async () =>
        {
            var rnd = new ReleaseNotesDialog();

            rnd.Message = "Thank you for checking out ReleaseNotesDialog! Here's a list of what's new: (since we didnt add anything to the Fixes list, the Fixes ListView will hide itself)";

            rnd.Features = new ObservableCollection<string>
            {
                "Amazing feature!",
                "Added theming",
                "Backup and restore added!"
            };
            
            await rnd.ShowAsync();
        }));

        public DelegateCommand ShowReleaseNotesWithFixesCommand => showReleaseNotesWithFixesCommand ?? (showReleaseNotesWithFixesCommand = new DelegateCommand(async () =>
        {
            var rnd = new ReleaseNotesDialog();

            rnd.AppName = "My App Name";
            rnd.Message = "Thank you for checking out ReleaseNotesDialog! Here's a list of what's new and what's fixed:";

            rnd.Features = new ObservableCollection<string>
            {
                "Amazing feature!",
                "Added theming",
                "Backup and restore added!"
            };

            rnd.Fixes = new ObservableCollection<string>
            {
                "Amazing feature!",
                "Added theming",
                "Fixed crash when opening",
                "Added text wrapping to fix text being cut off"
            };

            await rnd.ShowAsync();
        }));

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
