using System.ComponentModel;
using System.Runtime.CompilerServices;
using UwpHelpers.Examples.Annotations;

namespace UwpHelpers.Examples.ViewModels
{
    public class BusyIndicatorViewModel : INotifyPropertyChanged
    {
        public BusyIndicatorViewModel()
        {
            
        }

        private bool isBusy = true;
        private string isBusyMessage = "please wait...";

        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value; OnPropertyChanged(); }
        }

        public string IsBusyMessage
        {
            get { return isBusyMessage; }
            set { isBusyMessage = value; OnPropertyChanged(); }
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
}
