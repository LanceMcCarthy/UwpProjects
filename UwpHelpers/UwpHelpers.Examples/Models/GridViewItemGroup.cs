using System.Collections.ObjectModel;

namespace UwpHelpers.Examples.Models
{
    public class GridViewItemGroup
    {
        private readonly int groupId;

        public GridViewItemGroup(int id)
        {
            this.groupId = id;
        }

        public string GroupTitle => $"Group {groupId}";

        private ObservableCollection<GridViewGroupableItem> items;
        public ObservableCollection<GridViewGroupableItem> Items => items ?? (items = GenerateGroupItems());

        private ObservableCollection<GridViewGroupableItem> GenerateGroupItems()
        {
            var list = new ObservableCollection<GridViewGroupableItem>();

            for (int i = 1; i < 6; i++)
                list.Add(new GridViewGroupableItem(i));

            return list;
        }
    }
}
