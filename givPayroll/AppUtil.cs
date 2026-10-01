using System.Globalization;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;

namespace givPayroll
{
    public static class AppUtil
    {
        public enum enumMaritalStatus
        {
            Single = 1,
            Married = 2
        }
        public enum enumFamilyRelation
        {
            Child = 1,
            Spouse=2
        }
        public enum enumGender
        {
            Man = 1,
            Woman= 2
        }

        public static String M2S(DateTime gregorianDate)
        {
            var pc = new PersianCalendar();
            var date = gregorianDate;

            return $"{pc.GetYear(date):0000}/{pc.GetMonth(date):00}/{pc.GetDayOfMonth(date):00}";
        }

        public static string CompleteAge(DateTime birthDate, ref int ageYears)
        {

            DateTime today = DateTime.Today;

            int years = today.Year - birthDate.Year;
            int months = today.Month - birthDate.Month;
            int days = today.Day - birthDate.Day;

            if (days < 0)
            {
                months--;

                DateTime previousMonth = today.AddMonths(-1);
                days += DateTime.DaysInMonth(previousMonth.Year, previousMonth.Month);
            }

            if (months < 0)
            {
                years--;
                months += 12;
            }

            string age = $"{years} سال, {months} ماه, {days} روز";
            ageYears = years;
            return age;
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
         
        public static byte[] ConvertXlsToXlsx(Stream xlsStream)
        {
            // Read old .xls
            var hssfWorkbook = new HSSFWorkbook(xlsStream);

            // Create new .xlsx
            var xssfWorkbook = new XSSFWorkbook();

            for (int s = 0; s < hssfWorkbook.NumberOfSheets; s++)
            {
                var oldSheet = hssfWorkbook.GetSheetAt(s);
                var newSheet = xssfWorkbook.CreateSheet(oldSheet.SheetName);

                for (int r = oldSheet.FirstRowNum; r <= oldSheet.LastRowNum; r++)
                {
                    var oldRow = oldSheet.GetRow(r);

                    if (oldRow == null)
                        continue;

                    var newRow = newSheet.CreateRow(r);

                    for (int c = oldRow.FirstCellNum; c < oldRow.LastCellNum; c++)
                    {
                        if (c < 0)
                            continue;

                        var oldCell = oldRow.GetCell(c);

                        if (oldCell == null)
                            continue;

                        var newCell = newRow.CreateCell(c);

                        switch (oldCell.CellType)
                        {
                            case NPOI.SS.UserModel.CellType.String:
                                newCell.SetCellValue(oldCell.StringCellValue);
                                break;

                            case NPOI.SS.UserModel.CellType.Numeric:
                                newCell.SetCellValue(oldCell.NumericCellValue);
                                break;

                            case NPOI.SS.UserModel.CellType.Boolean:
                                newCell.SetCellValue(oldCell.BooleanCellValue);
                                break;

                            case NPOI.SS.UserModel.CellType.Formula:
                                newCell.SetCellFormula(oldCell.CellFormula);
                                break;

                            default:
                                newCell.SetCellValue(oldCell.ToString());
                                break;
                        }
                    }
                }
            }

            using var output = new MemoryStream();
            xssfWorkbook.Write(output, leaveOpen: true);

            return output.ToArray();
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
