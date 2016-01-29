using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using UwpHelpers.TelerikUwp.CustomControls.EventArgs;

namespace UwpHelpers.TelerikUwp.CustomControls
{
    public sealed partial class DateRangePicker : UserControl
    {
        #region Dependency Properties

        public static readonly DependencyProperty StartDateProperty = DependencyProperty.Register(
            "StartDate", 
            typeof (DateTime), 
            typeof (DateRangePicker), 
            new PropertyMetadata(DateTime.Now, (o, e) =>
            {
                ((DateRangePicker)o).FireValueChanged(e);
            }));

        public DateTime StartDate
        {
            get { return (DateTime) GetValue(StartDateProperty); }
            set { SetValue(StartDateProperty, value); }
        }

        public static readonly DependencyProperty EndDateProperty = DependencyProperty.Register(
            "EndDate", 
            typeof (DateTime), 
            typeof (DateRangePicker), 
            new PropertyMetadata(DateTime.Now.AddDays(1), (o, e) =>
            {
                ((DateRangePicker) o).FireValueChanged(e);
            }));

        public DateTime EndDate
        {
            get { return (DateTime) GetValue(EndDateProperty); }
            set { SetValue(EndDateProperty, value); }
        }

        #endregion

        #region Events

        public delegate void DateRangeChanged(object sender, DateRangeChangedEventArgs e);
        public event DateRangeChanged DateRangeValueChanged;

        private void FireValueChanged(DependencyPropertyChangedEventArgs e)
        {
            DateRangeValueChanged?.Invoke(this, new DateRangeChangedEventArgs(this.StartDate, this.EndDate));
        }

        #endregion

        public DateRangePicker()
        {
            this.InitializeComponent();
        }
    }
}
