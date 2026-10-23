using DocumentFormat.OpenXml.InkML;
using givPayroll.Data;
using givPayroll.Models;
using MathNet.Numerics.Financial;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using SQLitePCL;
using System.Globalization;
using System.Text.RegularExpressions;

namespace givPayroll
{
    public static class AppUtil
    {
        public enum enumAttendanceWorkStatus
        {
            None,
            Incorrect = 1,
            Incomplete = 100,
            PayrollReadyForInsert = 200,
            PayrollInserted = 220,
            PayrollConfirmed = 300,
            PayrollVouchered = 350
        }

        public enum enumPayrollStatus
        {
            None,
            Confirmed,
            Vouchered,
        }

        public enum enumMaritalStatus
        {
            Single = 1,
            Married = 2
        }
        public enum enumFamilyRelation
        {
            Child = 1,
            Spouse = 2
        }
        public enum enumGender
        {
            Man = 1,
            Woman = 2
        }



        //select pc.* 
        //from PersonCategory pc inner join
        //     PersonCategoryLength pcl on len(pc.CategoryCode) = pcl.SumLevelLength
        //where pcl.CategoryLevel = 3
        public static List<SelectListItem> GetPersonsByCategoryCode(
           ApplicationDbContext context,
           string categoryCode)
        {
            var persons = context.Database
                .SqlQuery<PersonCategoryResult>(
                    $"SELECT * FROM dbo.GetPersonsByCategoryCode({categoryCode})")
                .ToList();

            return persons.Select(x => new SelectListItem
            {
                Value = x.PersonID.ToString(),
                Text = x.PersonFirstName
            }).ToList();
        }

        public static List<SelectListItem> GetMoein(ApplicationDbContext _context)
        {
            return _context.PersonCategories
                .Join(
                    _context.PersonCategoryLengths,
                    pc => pc.CategoryCode.Length,
                    pcl => pcl.SumLevelLength,
                    (pc, pcl) => new { pc, pcl }
                )
                .Where(x => x.pcl.CategoryLevel == 3)
                .OrderBy(x => x.pc.CategoryName)
                .Select(x => new SelectListItem
                {
                    Value = x.pc.CategoryCode,
                    Text = x.pc.CategoryName
                })
                .ToList();

        }

        public static List<SelectListItem> GetMoeinCategory(ApplicationDbContext _context)
        {
            var items = _context.PersonCategories
      .Join(
          _context.PersonCategoryLengths.Where(pcl => pcl.CategoryLevel == 3),
          pc => pc.CategoryCode.Length,
          pcl => pcl.SumLevelLength,
          (pc, pcl) => pc
      )
      .Join(
          _context.PersonCategoryGroups,
          pc => pc.CategoryCode,
          pcg => pcg.CategoryCode,
          (pc, pcg) => new
          {
              pc.CategoryName,
              pc.CategoryCode,
              pcg.PersonGroupID
          }
      )
      .Join(
          _context.PersonGroups,
          x => x.PersonGroupID,
          pg => pg.PersonGroupID,
          (x, pg) => new SelectListItem
          {
              Value = x.CategoryCode,
              Text = x.CategoryName + " - " + pg.PersonGroupName
          }
      )
      .OrderBy(x => x.Text)
      .ToList();

            return items;
        }

        public static List<SelectListItem> GetProject(ApplicationDbContext _context)
        {
            return _context.Projects.Select(x => new SelectListItem
            {
                Value = x.ProjectID.ToString(),
                Text = x.ProjectName
            }).ToList();
        }

        public static string FixYEKE(string Field2)
        {

            String ret;
            ret = Field2.Replace("ی", "ي");
            //  ret = ret.Replace("ک", "ك");
            ret = ret.Replace("ك", "ک");
            return ret;
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

        public static List<string> ExtractFormulaWords(IEnumerable<string> formulas)
        {
            var words = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (var formula in formulas)
            {
                if (string.IsNullOrWhiteSpace(formula))
                    continue;

                // Extract words/identifiers such as:
                // BaseSalary
                // DaysWorked
                // HousingAllowance
                // MonthDays
                //
                // Ignores:
                // + - * / ( ) 220 1.2 etc.
                var matches = Regex.Matches(
                    formula,
                    @"[A-Za-z_][A-Za-z0-9_]*"
                );

                foreach (Match match in matches)
                {
                    words.Add(match.Value);
                }
            }

            return words
                .OrderBy(x => x)
                .ToList();
        }


    }
}
