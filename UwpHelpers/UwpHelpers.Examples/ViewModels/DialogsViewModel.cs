using System;
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

        public DialogsViewModel()
        {
                
        }

        #region Commands

        public DelegateCommand ShowReleaseNotesCommand => showReleaseNotesCommand ?? (showReleaseNotesCommand = new DelegateCommand( async () =>
        {
            var rnd = new ReleaseNotesDialog();

            rnd.Message = "Thank you for checking out ReleaseNotesDialog! Here's a list of what's new and what's fixed: (note, if you dont add any features or fixes, the list will hide itself)";

            rnd.Features.Add("Amaing feature!");
            rnd.Features.Add("Added theming");
            rnd.Features.Add("Backup and restore added!");

            rnd.Fixes.Add("Fixed crash when opening");
            rnd.Fixes.Add("Added text wrapping to fix text being cut off");

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
