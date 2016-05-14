using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UwpHelpers.Examples.Models;

namespace UwpHelpers.Examples.ViewModels
{
    public class AdaptiveGridViewViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<string> listItems;
        private ObservableCollection<GridViewItemGroup> groupedItems;

        public AdaptiveGridViewViewModel()
        {
        }
        
        public ObservableCollection<string> ListItems => listItems ?? (listItems = GenerateSampleListData());

        public ObservableCollection<GridViewItemGroup> GroupedItems => groupedItems ?? (groupedItems = GenerateGroupedItems());

        private ObservableCollection<GridViewItemGroup> GenerateGroupedItems() => new ObservableCollection<GridViewItemGroup>
        {
            new GridViewItemGroup(1),
            new GridViewItemGroup(2)
        };

        private static ObservableCollection<string> GenerateSampleListData()
        {
            var list = new ObservableCollection<string>();

            for (int i = 1; i < 10; i++)
                list.Add($"Item {i}");

            return list;
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