using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


}
