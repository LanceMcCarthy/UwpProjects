using Windows.UI.Xaml;

namespace UwpHelpers.Controls.Extensions
{
    /// <summary>
    /// Attachable properties to use instead of common ValueConverters
    /// </summary>
    public class XamlExtensions : DependencyObject
    {
        /// <summary>
        /// Use a bool to trigger UIElement's Visibility value (instead of using a ValueConverter)
        /// Usage: extensions:XamlExtensions.IsVisible="True"
        /// </summary>
        public static readonly DependencyProperty IsVisibleProperty = DependencyProperty.RegisterAttached(
            "IsVisible", typeof(bool), typeof(XamlExtensions), new PropertyMetadata(true, (o, e) =>
            {
                ((UIElement) o).Visibility = (bool) e.NewValue ? Visibility.Visible : Visibility.Collapsed;
            }));

        public static void SetIsVisible(DependencyObject element, bool value)
        {
            element.SetValue(IsVisibleProperty, value);
        }

        public static bool GetIsVisible(DependencyObject element) => (bool) element.GetValue(IsVisibleProperty);

        /// <summary>
        /// Use a bool to trigger UIElement's Visibility value (instead of using a ValueConverter)
        /// Usage: extensions:XamlExtensions.IsHidden="True"
        /// </summary>
        public static readonly DependencyProperty IsHiddenProperty = DependencyProperty.RegisterAttached(
            "IsHidden",
            typeof(bool),
            typeof(XamlExtensions),
            new PropertyMetadata(true, (o, e) =>
            {
                ((UIElement) o).Visibility = (bool) e.NewValue ? Visibility.Collapsed : Visibility.Visible;
            }));


        public static void SetIsHidden(DependencyObject element, bool value)
        {
            element.SetValue(IsHiddenProperty, value);
        }

        public static bool GetIsHidden(DependencyObject element) => (bool) element.GetValue(IsHiddenProperty);
    }
}
