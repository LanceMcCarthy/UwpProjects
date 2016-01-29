using Windows.UI.Xaml.Controls;
using UwpHelpers.TelerikUwp.CustomControls.EventArgs;

namespace UwpHelpers.TelerikUwp
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void MyDateRangePicker_OnDateRangeValueChanged(object sender, DateRangeChangedEventArgs e)
        {
            StartDateTextBlock.Text = e.StartDate.ToString("MM/dd/yyyy hh:mm tt");
            EndDateTextBlock.Text = e.EndDate.ToString("MM/dd/yyyy hh:mm tt");
        }
    }
}
