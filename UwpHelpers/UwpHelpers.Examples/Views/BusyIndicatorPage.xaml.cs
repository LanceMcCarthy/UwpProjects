using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace UwpHelpers.Examples.Views
{
    public sealed partial class BusyIndicatorPage : Page
    {
        public BusyIndicatorPage()
        {
            this.InitializeComponent();
        }

        private void UpButton_OnClick(object sender, RoutedEventArgs e)
        {
            Demo1.DisplayMessageSize++;
            Demo2.DisplayMessageSize++;
        }

        private void DownButton_OnClick(object sender, RoutedEventArgs e)
        {
            Demo1.DisplayMessageSize--;
            Demo2.DisplayMessageSize--;
        }

        private void ForegroundChangeButton_OnClick(object sender, RoutedEventArgs e)
        {
            var nextColor = GetRandomColor();
            Demo1.Foreground = new SolidColorBrush(nextColor);
            Demo2.Foreground = new SolidColorBrush(nextColor);
        }

        private static Color GetRandomColor()
        {
            var rnd = new Random();
            return Color.FromArgb(255, (byte) rnd.Next(255), (byte) rnd.Next(255), (byte) rnd.Next(255));
        }
    }
}
