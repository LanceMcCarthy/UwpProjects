using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UwpHelpers.Examples.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private bool isBusy;
        private ObservableCollection<string> listItems;

        public MainPageViewModel()
        {

        }

        #region properties

        public ObservableCollection<string> ListItems => listItems ?? (listItems = GenerateSampleListData());
        
        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value; OnPropertyChanged(); }
        }

        #endregion

        #region methods and tasks

        private ObservableCollection<string> GenerateSampleListData()
        {
            var list = new ObservableCollection<string>();

            for (int i = 1; i < 10; i++)
            {
                list.Add($"Item {i}");
            }

            return list;
        }

        #endregion

        #region INPC

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
