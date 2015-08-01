using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace Lukkhacoder.UniversalWindowsSamples.Converters
{
    public class DisplayModeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return null;
            CalendarViewDisplayMode displayMode = (CalendarViewDisplayMode)value;
            return displayMode.ToString();
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return null;

            string displayMode = value.ToString().ToLower();
            switch (displayMode)
            {
                case "month":return CalendarViewDisplayMode.Month;
                case "year": return CalendarViewDisplayMode.Year;
                case "decade": return CalendarViewDisplayMode.Decade;
            }
            return CalendarViewDisplayMode.Month;

        }
    }
}
