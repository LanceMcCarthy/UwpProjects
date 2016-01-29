using System;

namespace UwpHelpers.TelerikUwp.CustomControls.EventArgs
{
    public class DateRangeChangedEventArgs : System.EventArgs
    {
        private DateTime startDate;
        private DateTime endDate;

        public DateRangeChangedEventArgs(DateTime startDate, DateTime endDate)
        {
            this.startDate = startDate;
            this.endDate = endDate;
        }

        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }
    }
}
