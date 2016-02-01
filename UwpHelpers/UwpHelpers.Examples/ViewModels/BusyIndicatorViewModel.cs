using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UwpHelpers.Examples.ViewModels
{
    public class BusyIndicatorViewModel : INotifyPropertyChanged
    {
        public BusyIndicatorViewModel()
        {
            
        }

        private bool isBusy;
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

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
