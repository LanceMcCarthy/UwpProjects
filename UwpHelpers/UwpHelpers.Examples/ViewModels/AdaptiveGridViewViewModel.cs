using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UwpHelpers.Examples.Annotations;

namespace UwpHelpers.Examples.ViewModels
{
    public class AdaptiveGridViewViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<GridViewSampleItem> listItems;
        private ObservableCollection<GridViewItemGroup> groupedItems;

        public AdaptiveGridViewViewModel()
        {
        }

        public ObservableCollection<GridViewSampleItem> ListItems => listItems ?? (listItems = GenerateSampleListData());
        
        public ObservableCollection<GridViewItemGroup> GroupedItems => groupedItems ?? (groupedItems = GenerateGroupedItems());

        /// <summary>
        /// Generates grouped sample items
        /// </summary>
        /// <returns>A collection of pre-grouped sample items</returns>
        private ObservableCollection<GridViewItemGroup> GenerateGroupedItems() => new ObservableCollection<GridViewItemGroup>
        {
            new GridViewItemGroup(1),
            new GridViewItemGroup(2)
        };

        /// <summary>
        /// Generates sample items
        /// </summary>
        /// <returns>A collection of sample items</returns>
        private static ObservableCollection<GridViewSampleItem> GenerateSampleListData()
        {
            var list = new ObservableCollection<GridViewSampleItem>();

            for (int i = 1; i < 6; i++)
                list.Add(new GridViewSampleItem(i));

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

    #region demo data models

    public class GridViewSampleItem
    {
        public GridViewSampleItem(int id)
        {
            DisplayValue = $"Item {id}";
            IsEven = id % 2 == 0;
        }
        public string DisplayValue { get; set; }
        public bool IsEven { get; set; }
    }

    public class GridViewItemGroup
    {
        private readonly int groupId;

        public GridViewItemGroup(int id)
        {
            this.groupId = id;
        }

        public string GroupTitle => $"Group {groupId}";

        private ObservableCollection<GridViewSampleItem> items;
        public ObservableCollection<GridViewSampleItem> Items => items ?? (items = GenerateGroupItems());

        private ObservableCollection<GridViewSampleItem> GenerateGroupItems()
        {
            var list = new ObservableCollection<GridViewSampleItem>();

            for (int i = 1; i < 6; i++)
                list.Add(new GridViewSampleItem(i));

            return list;
        }
    }

    #endregion
}
