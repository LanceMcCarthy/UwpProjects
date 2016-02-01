using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using UwpHelpers.Examples.Helpers;

namespace UwpHelpers.Examples.Views
{
    public sealed partial class ElementBlurPage : Page
    {
        public ElementBlurPage()
        {
            this.InitializeComponent();
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            //BlurElementAsync: UIElement extension method, with optional blur amount parameter (default is 2.0f )
            var blurredElement = await ContentToBlur.BlurElementAsync((float) BlurAmountSlider.Value);

            //an example usage is the background of the page or as the PaneBackground of a SplitView
            ContentRoot.Background = new ImageBrush
            {
                ImageSource = blurredElement,
                Stretch = Stretch.UniformToFill
            };
        }
    }
    
}
