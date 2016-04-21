using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UwpHelpers.Examples.Annotations;

namespace UwpHelpers.Examples.ViewModels
{
    public class AdaptiveGridViewViewModel : INotifyPropertyChanged
    {
        public AdaptiveGridViewViewModel()
        {
            
        }

        private ObservableCollection<string> listItems;
        public ObservableCollection<string> ListItems => listItems ?? (listItems = GenerateSampleListData());
        
        private ObservableCollection<string> GenerateSampleListData()
        {
            var list = new ObservableCollection<string>();

            for (int i = 1; i < 10; i++)
            {
                list.Add($"Item {i}");
            }

            return list;
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
