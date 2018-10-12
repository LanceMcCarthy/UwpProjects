using System;
using System.Linq;
using System.Threading.Tasks;

using UwpThemeExplorer.Activation;
using UwpThemeExplorer.Helpers;
using UwpThemeExplorer.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace UwpThemeExplorer.Views
{
    public sealed partial class PivotPage : Page
    {
        public PivotViewModel ViewModel { get; } = new PivotViewModel();

        public PivotPage()
        {
            NavigationCacheMode = NavigationCacheMode.Required;
            DataContext = ViewModel;
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ViewModel.LoadResources();

            if (e.Parameter is SchemeActivationData data)
            {
                await InitializeFromSchemeActivationAsync(data);
            }

            await Task.CompletedTask;
        }

        public async Task InitializeFromSchemeActivationAsync(SchemeActivationData schemeActivationData)
        {
            var selected = pivot?.Items?.Cast<PivotItem>().FirstOrDefault(i => i.IsOfPageType(schemeActivationData.PageType));

            var page = selected?.GetPage<IPivotActivationPage>();

            if(page == null)
                return;

            pivot.SelectedItem = selected;

            await page?.OnPivotActivatedAsync(schemeActivationData.Parameters);
        }
    }
}
