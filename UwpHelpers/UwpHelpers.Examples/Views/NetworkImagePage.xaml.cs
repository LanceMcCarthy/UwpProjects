using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using UwpHelpers.Examples.ViewModels;

namespace UwpHelpers.Examples.Views
{
    public sealed partial class NetworkImagePage : Page
    {
        public NetworkImagePage()
        {
            this.InitializeComponent();
            Loaded += NetworkImagePage_Loaded;
        }

        private async void NetworkImagePage_Loaded(object sender, RoutedEventArgs e)
        {
            await ((NetworkImageViewModel) DataContext).LoadImagesAsync();
        }
    }
}
