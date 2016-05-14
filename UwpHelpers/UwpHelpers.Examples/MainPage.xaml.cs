using Windows.UI.Xaml.Controls;
using UwpHelpers.Examples.Models;

namespace UwpHelpers.Examples
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void ListViewBase_OnItemClick(object sender, ItemClickEventArgs e)
        {
            Frame.Navigate((e.ClickedItem as DemoPage)?.DemoPageType);
        }
    }
}
