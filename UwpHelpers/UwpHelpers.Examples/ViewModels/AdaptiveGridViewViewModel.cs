using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace UwpHelpers.Examples.ViewModels
{
    public class AdaptiveGridViewViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<string> listItems;
        private ObservableCollection<Group> groupedItems;

        public AdaptiveGridViewViewModel()
        {
        }
        
        public ObservableCollection<string> ListItems => listItems ?? (listItems = GenerateSampleListData());

        public ObservableCollection<Group> GroupedItems => groupedItems ?? (groupedItems = GenerateGroupedItems());

        private ObservableCollection<Group> GenerateGroupedItems() => new ObservableCollection<Group>
        {
            new Group(1),
            new Group(2)
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

    public class Group
    {
        private readonly int groupId;

        public Group(int id)
        {
            this.groupId = id;
        }
        
        public string GroupTitle => $"Group {groupId}";

        private ObservableCollection<GroupableItem> items;
        public ObservableCollection<GroupableItem> Items => items ?? (items = GenerateGroupItems());

        private ObservableCollection<GroupableItem> GenerateGroupItems()
        {
            var list = new ObservableCollection<GroupableItem>();
            
            for (int i = 1; i < 6; i++)
                list.Add(new GroupableItem(i));

            return list;
        }
    }

    public class GroupableItem
    {
        public GroupableItem(int id)
        {
            DisplayValue = $"Item {id}";
            IsEven = id % 2 == 0;
        }
        public string DisplayValue { get; set; }
        public bool IsEven { get; set; }
    }
}
