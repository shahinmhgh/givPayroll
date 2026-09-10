using System.Globalization;

namespace givPayroll
{
    public static class DateUtil
    {
        public static String M2S(DateTime gregorianDate)
        {
            var pc = new PersianCalendar();
            var date = gregorianDate;

            return $"{pc.GetYear(date):0000}/{pc.GetMonth(date):00}/{pc.GetDayOfMonth(date):00}";
        }
        private static readonly string[] PersianMonths =
  {
        "فروردین",
        "اردیبهشت",
        "خرداد",
        "تیر",
        "مرداد",
        "شهریور",
        "مهر",
        "آبان",
        "آذر",
        "دی",
        "بهمن",
        "اسفند"
    };

        public static string GetPersianMonthName(int month)
        {
            string[] persianMonths =
                {
                    "فروردین",
                    "اردیبهشت",
                    "خرداد",
                    "تیر",
                    "مرداد",
                    "شهریور",
                    "مهر",
                    "آبان",
                    "آذر",
                    "دی",
                    "بهمن",
                    "اسفند"
                };
            

            if (month < 1 || month > 12)
                throw new ArgumentOutOfRangeException(nameof(month));

            return PersianMonths[month - 1];
        }

        internal static DateTime S2M(string theDate)
        {
            if (string.IsNullOrWhiteSpace(theDate))
                throw new ArgumentException("Date cannot be empty.", nameof(theDate));

            if (!DateTime.TryParseExact(
                    theDate,
                    "yyyy/MM/dd",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out var dummy))
            {
                // We don't actually use dummy because this is a Gregorian parser.
            }

            var parts = theDate.Split('/');

            if (parts.Length != 3 ||
                !int.TryParse(parts[0], out int year) ||
                !int.TryParse(parts[1], out int month) ||
                !int.TryParse(parts[2], out int day))
            {
                throw new FormatException("Invalid Jalali date. Expected yyyy/MM/dd.");
            }

            var pc = new PersianCalendar();

            return pc.ToDateTime(year, month, day, 0, 0, 0, 0);
        }
    }
}
