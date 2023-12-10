using System;
using MovieBank.Utility;
using System.Windows.Data;
using System.Globalization;

namespace MovieBank.Convertor
{
    public class PosterConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!string.IsNullOrEmpty(value?.ToString()))
            {
                return Variable.ImageFullPath + value;
            }

            return Variable.DefaultPoster;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}