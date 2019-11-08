using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace UWP_Data_Access_SQLSERVER.Models
{
    static class Utiles
    {


        public static string SafeGetString(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            return string.Empty;
        }

        public static int SafeGetInt32(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetInt32(colIndex);
            return 0;
        }

        public static DateTimeOffset SafeGetDateTimeOffset(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetDateTimeOffset(colIndex);
            return new DateTimeOffset();
        }

        public static DateTime SafeGetDateTime(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetDateTime(colIndex);
            return new DateTime();
        }
        public static Decimal SafeGetDecimal(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetDecimal(colIndex);
            return Decimal.Zero;
        }

    }
    public class DateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return new DateTimeOffset(((DateTime)value).ToUniversalTime());

        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            try
            {
                return ((DateTimeOffset)value).DateTime;
            }
            catch
            {
                return DateTimeOffset.MinValue;
            }
        }
    }
    public  class DecimalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)

        {
            string specifier = "C";
            CultureInfo culture = CultureInfo.CurrentCulture;
            if (value is decimal m)
            {
                return m == 0 ? "" : m.ToString(specifier, culture);
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {
                if (Decimal.TryParse(value.ToString(), out decimal m))
                {
                    return m;
                }
            }
            return 0m;
        }
    }
}
