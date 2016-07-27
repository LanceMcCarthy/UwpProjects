using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UwpHelpers.Examples.Annotations;
using UwpHelpers.Examples.Views;

namespace UwpHelpers.Examples.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public MainPageViewModel()
        {
            Demos = new ObservableCollection<Demo>()
            {
                new Demo { DemoTitle = "AdaptiveGridView Demo", GlyphIcon ="", DemoPage = typeof(AdaptiveGridViewPage)},
                new Demo { DemoTitle = "BusyIndicator Demos", GlyphIcon ="", DemoPage = typeof(BusyIndicatorPage)},
                new Demo { DemoTitle = "UIElement Blur Demo", GlyphIcon = "", DemoPage = typeof(ElementBlurPage) },
                new Demo { DemoTitle = "Incremental Scrolling Demo", GlyphIcon = "", DemoPage = typeof(IncrementalScrollingPage) },
                new Demo { DemoTitle = "NetworkImage Demo", GlyphIcon = "", DemoPage = typeof(NetworkImagePage) },
                new Demo { DemoTitle = "HttpClientExtensions Demo", GlyphIcon = "", DemoPage = typeof(HttpClientExtensionsPage) },
                new Demo { DemoTitle = "Dialogs Demo", GlyphIcon = "", DemoPage = typeof(DialogsPage) }
            };
        }

        public ObservableCollection<Demo> Demos { get; set; }

        #region INPC

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }

    public class Demo
    {
        public Type DemoPage { get; set; }
        public string DemoTitle { get; set; }
        public string GlyphIcon { get; set; }
    }
}
