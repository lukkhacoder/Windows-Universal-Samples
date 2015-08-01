using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace Lukkhacoder.UniversalWindowsSamples
{
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        private DateTimeOffset _FirstSelectedDate;
        private int _SelectedDatesCount;

        public DateTimeOffset FirstSelectedDate
        {
            get { return _FirstSelectedDate; }
            set
            {
                if (_FirstSelectedDate == value)
                    return;

                _FirstSelectedDate = value;
                this.NotifyPropertyChanged();
                SelectedDateAnimation.Begin();
            }
        }
        public int SelectedDatesCount
        {
            get { return _SelectedDatesCount; }
            set
            {
                if (_SelectedDatesCount == value)
                    return;

                _SelectedDatesCount = value;
                this.NotifyPropertyChanged();
                SelectedDatesCountAnimation.Begin();
            }
        }

        

        public MainPage()
        {
            this.InitializeComponent();
            this.DataContext = this;
            this.Loaded += OnLoad;
  
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            MyCalendarView.MinDate = DateTime.Now.AddYears(-12);
            MyCalendarView.MaxDate = DateTime.Now.AddYears(12);
            MyCalendarView.SetDisplayDate(DateTimeOffset.Now);
            //MyCalendarView.SelectedDates.Add(DateTimeOffset.Now);

        }

        private void NotifyPropertyChanged([CallerMemberName]string propertyName = "")
        {
            if (PropertyChanged != null && !String.IsNullOrEmpty(propertyName))
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void MyCalendarView_SelectedDatesChanged(CalendarView sender, CalendarViewSelectedDatesChangedEventArgs args)
        {            
            FirstSelectedDate = MyCalendarView.SelectedDates.OrderBy(date=> date).FirstOrDefault();
            SelectedDatesCount = MyCalendarView.SelectedDates.Count;

        }

        private void MyCalendarView_CalendarViewDayItemChanging(CalendarView sender, CalendarViewDayItemChangingEventArgs args)
        {
            if ((args.Item.Date.DayOfWeek == DayOfWeek.Saturday))
                args.Item.IsBlackout = true;
        }
    }
}
