namespace UwpHelpers.Examples.Models
{
    public class GridViewGroupableItem
    {
        public GridViewGroupableItem(int id)
        {
            DisplayValue = $"Item {id}";
            IsEven = id % 2 == 0;
        }
        public string DisplayValue { get; set; }
        public bool IsEven { get; set; }
    }
}
