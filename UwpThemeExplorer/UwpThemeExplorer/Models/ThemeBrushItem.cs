using Windows.UI.Xaml.Media;
using CommonHelpers.Common;

namespace UwpThemeExplorer.Models
{
    public class ThemeBrushItem : BindableBase
    {
        private string _name;
        private string _hexValue;
        private SolidColorBrush _brushValue;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string HexValue
        {
            get => _hexValue;
            set => SetProperty(ref _hexValue, value);
        }

        public SolidColorBrush BrushValue
        {
            get => _brushValue;
            set => SetProperty(ref _brushValue, value);
        }

        public static string GetBrushHexValue(SolidColorBrush brush)
        {
            var bytes = new[] { brush.Color.A, brush.Color.R, brush.Color.G, brush.Color.B };

            var hexCharArray = new char[bytes.Length * 2];

            for (int bx = 0, cx = 0; bx < bytes.Length; ++bx, ++cx)
            {
                var b = (byte)(bytes[bx] >> 4);

                hexCharArray[cx] = (char)(b > 9 ? b + 0x37 + 0x20 : b + 0x30);

                b = (byte)(bytes[bx] & 0x0F);

                hexCharArray[++cx] = (char)(b > 9 ? b + 0x37 + 0x20 : b + 0x30);
            }

            return new string(hexCharArray);
        }
    }
}
