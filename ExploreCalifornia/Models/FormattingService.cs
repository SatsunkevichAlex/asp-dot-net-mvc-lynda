using System;
using System.Globalization;

namespace ExploreCalifornia.Models
{
    public class FormattingService
    {
        public string AsReadableDate(DateTime date)
        {
            return date.ToString("d", CultureInfo.CurrentCulture);
        }
    }
}
