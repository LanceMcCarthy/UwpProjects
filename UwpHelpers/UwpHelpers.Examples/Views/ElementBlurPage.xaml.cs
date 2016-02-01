using Microsoft.Graphics.Canvas;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Graphics.Display;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Microsoft.Graphics.Canvas.Effects;

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
            //You can pass any UIelement to the method and it will render all of the children into a bitmap with a Blur applied
            var blurredElement = await BlurElementAsync(ContentToBlur, (float) BlurAmountSlider.Value);

            //an example usage is the background of the page or as the PaneBackground of a SplitView
            ContentRoot.Background = new ImageBrush
            {
                ImageSource = blurredElement,
                Stretch = Stretch.UniformToFill
            };
        }

        /// <summary>
        /// Applys a blur to a UI element
        /// </summary>
        /// <param name="sourceElement">UIElement to blur, generally an Image control, but can be anything</param>
        /// <param name="blurAmount">Level of blur to apply</param>
        /// <returns>Blurred UIElement as BitmapImage</returns>
        private async Task<BitmapImage> BlurElementAsync(UIElement sourceElement, float blurAmount = 2.0f)
        {
            if (sourceElement == null)
                return null;

            var rtb = new RenderTargetBitmap();
            await rtb.RenderAsync(sourceElement);

            var buffer = await rtb.GetPixelsAsync();
            var array = buffer.ToArray();

            var displayInformation = DisplayInformation.GetForCurrentView();

            using (var stream = new InMemoryRandomAccessStream())
            {
                var pngEncoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, stream);

                pngEncoder.SetPixelData(BitmapPixelFormat.Bgra8,
                                     BitmapAlphaMode.Premultiplied,
                                     (uint) rtb.PixelWidth,
                                     (uint) rtb.PixelHeight,
                                     displayInformation.RawDpiX,
                                     displayInformation.RawDpiY,
                                     array);

                await pngEncoder.FlushAsync();
                stream.Seek(0);

                var canvasDevice = new CanvasDevice();
                var bitmap = await CanvasBitmap.LoadAsync(canvasDevice, stream);

                var renderer = new CanvasRenderTarget(canvasDevice,
                                                      bitmap.SizeInPixels.Width,
                                                      bitmap.SizeInPixels.Height,
                                                      bitmap.Dpi);

                using (var ds = renderer.CreateDrawingSession())
                {
                    var blur = new GaussianBlurEffect
                    {
                        BlurAmount = blurAmount,
                        Source = bitmap
                    };
                    ds.DrawImage(blur);
                }

                stream.Seek(0);
                await renderer.SaveAsync(stream, CanvasBitmapFileFormat.Png);

                var image = new BitmapImage();
                await image.SetSourceAsync(stream);

                return image;
            }
        }
    }
    
}
