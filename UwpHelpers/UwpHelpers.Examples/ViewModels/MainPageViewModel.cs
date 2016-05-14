using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UwpHelpers.Examples.Models;
using UwpHelpers.Examples.Views;

namespace UwpHelpers.Examples.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public MainPageViewModel()
        {
            Demos = new ObservableCollection<DemoPage>
            {
                new DemoPage { DemoTitle = "AdaptiveGridView Demo", GlyphIcon ="", DemoPageType = typeof(AdaptiveGridViewPage)},
                new DemoPage { DemoTitle = "BusyIndicator Demos", GlyphIcon ="", DemoPageType = typeof(BusyIndicatorPage)},
                new DemoPage {DemoTitle = "UIElement Blur Demo", GlyphIcon = "", DemoPageType = typeof(ElementBlurPage) },
                new DemoPage {DemoTitle = "Incremental Scrolling Demo", GlyphIcon = "", DemoPageType = typeof(IncrementalScrollingPage) },
                new DemoPage {DemoTitle = "NetworkImage Demo", GlyphIcon = "", DemoPageType = typeof(NetworkImagePage) }
            };
        }

        public ObservableCollection<DemoPage> Demos { get; set; }
        
        #region INPC

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
