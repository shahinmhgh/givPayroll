using givPayroll.Data;
using givPayroll.Models;
using givPayroll.Services;
using givPayroll.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DiaSymReader;
using Microsoft.EntityFrameworkCore;
using Microsoft.Playwright;
using NPOI.HSSF.UserModel;
using Org.BouncyCastle.Security;
using Parlot.Fluent;
using System.Globalization;
using System.Text.RegularExpressions;
using static AttendanceCheckViewModel;
using static Microsoft.CodeAnalysis.CSharp.SyntaxTokenParser;

namespace givPayroll.Controllers
{
    [Authorize]
    public class AttendanceController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHolidayService _holidayService;

        public AttendanceController(ApplicationDbContext context, IHolidayService holidayService)
        {
            _context = context;
            _holidayService = holidayService;
        }

        [HttpGet]
        public async Task<IActionResult> Check(int? year, int? month)
        {
            var currentYear = year ?? 1405;
            var currentMonth = month ?? 1;

            ViewBag.Year = currentYear;
            ViewBag.Month = currentMonth;
            ViewBag.Searched = year.HasValue && month.HasValue;

            CalculateViewModel result = new CalculateViewModel();
            FillMonths(result);
            if (!year.HasValue || !month.HasValue)
            {
                result.Check = new List<AttendanceCheckViewModel>();
                return View(result);
            }
            string prefix =
                $"{currentYear:0000}/{currentMonth:00}/";

            // شروع و پایان ماه شمسی
            string monthStart =
                $"{currentYear:0000}/{currentMonth:00}/01";

            string monthEnd;

            if (currentMonth <= 6)
                monthEnd =
                    $"{currentYear:0000}/{currentMonth:00}/31";
            else if (currentMonth <= 11)
                monthEnd =
                    $"{currentYear:0000}/{currentMonth:00}/30";
            else
                monthEnd =
                    $"{currentYear:0000}/{currentMonth:00}/29";

            DateTime monthStartGregorian = AppUtil.S2M(monthStart);
            DateTime monthEndGregorian = AppUtil.S2M(monthEnd);

            // -----------------------------
            // 1. پرسنل دارای حکم فعال
            // -----------------------------

            var personnels = await _context.PersonnelOrders

                .Include(x => x.Personnel)
                .Where(x => x.StartDate <= monthEndGregorian && x.IsActive)
                .Select(x => new
                {
                    PersonnelId = x.PersonnelId,

                    //PersonnelCode =
                    //    x.Personnel.PersonnelCode,

                    PersonnelName =
                        x.Personnel.FirstName + " " +
                        x.Personnel.LastName,

                    //Required = x.Required
                })

                .ToListAsync();


            // -----------------------------
            // 2. Attendance همان ماه
            // -----------------------------

            var personnelIds =
                personnels
                .Select(x => x.PersonnelId)
                .ToList();


            var attendances = await _context.Attendances

                .Where(x =>
                    personnelIds.Contains(x.PersonnelId) &&
                    x.AttendancePersianDate.StartsWith(prefix)
                )

                .ToListAsync();


            // -----------------------------
            // 3. محاسبه
            // -----------------------------


            result.Check = new List<AttendanceCheckViewModel>();

            foreach (var personnel in personnels)
            {
                var attendance =
                    attendances
                    .Where(x => x.PersonnelId == personnel.PersonnelId)
                    .ToList();

                bool hasWorked = true; string errWorked = "";
                bool isCorrect = true; string errCorrect = "";
                List<AttendanceCheckViewModel> lst = new List<AttendanceCheckViewModel>();
                foreach (Attendance x in attendance)
                {
                    int Work = x.WorkingMinute;

                    int Leave = x.LeaveNormalMinute;
                    int LeaveWithoutSalary = x.LeaveWithoutSalaryMinute;
                    int LeaveNormalMinute = x.LeaveNormalMinute;
                    int LeaveSickMinute = x.LeaveSickMinute;

                    int Absence = x.AbsenceMinute;
                    int Mission = x.MissionMinute;

                    int Required = x.WorkingExpectedMinute;
                    int total = Work + Leave + LeaveWithoutSalary + LeaveNormalMinute + LeaveSickMinute + Absence + Mission;
                    bool iscorrect = total >= Required;
                    if (!iscorrect)
                    {
                        errCorrect += x.AttendancePersianDate + ", ";
                        isCorrect = false;
                    }
                    if (!x.HasWorked)
                    {
                        errWorked += x.AttendancePersianDate + ", ";
                        hasWorked = false;
                    }
                }

                AppUtil.enumAttendanceWorkStatus status = AppUtil.enumAttendanceWorkStatus.None;
                if (isCorrect == false)
                    status = AppUtil.enumAttendanceWorkStatus.Incorrect;
                else if (hasWorked == false)
                    status = AppUtil.enumAttendanceWorkStatus.Incomplete;
                else
                {
                    status = AppUtil.enumAttendanceWorkStatus.PayrollReadyForInsert;
                    Payroll pay = _context.Payrolls.Where(i => i.PersonnelId == personnel.PersonnelId && i.PayrollYear == year && i.PayrollMonth == month).FirstOrDefault();
                    if (pay != null)
                    {
                        status = AppUtil.enumAttendanceWorkStatus.PayrollInserted;
                        if (pay.PayrollStatusID == (int)AppUtil.enumPayrollStatus.Confirmed)
                            status = AppUtil.enumAttendanceWorkStatus.PayrollConfirmed;
                    }
                }

                var model = new AttendanceCheckViewModel
                {
                    PersonnelId = personnel.PersonnelId,
                    PersonnelName = personnel.PersonnelName,
                    Status = status,
                    //IsCorrect = isCorrect, // کارکرد درست / کارکرد نادرست
                    //hasWorked = hasWorked, // کارکرد کامل / کارکرد ناقص
                    IsCorrectErrText = errCorrect,
                    hasWorkedErrText = errWorked,
                };

                result.Check.Add(model);
            }
            if (attendances.Count == 0)
            {
                result = new CalculateViewModel();
                result.Check = new List<AttendanceCheckViewModel>();
            }
            //     
            ViewBag.ym = year + @"/" + month.GetValueOrDefault().ToString("00");

            return PartialView("_AttendanceCheckResult", result);
        }

        // =========================================================
        // INDEX
        // =========================================================

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var pc = new PersianCalendar();

            var now = DateTime.Now;

            int currentYear = pc.GetYear(now);
            int currentMonth = pc.GetMonth(now);


            var model = new AttendanceViewModel
            {
                Year = currentYear,
                Month = currentMonth
            };


            await FillSelections(model);


            // Select first personnel by default

            if (model.Personnels.Any())
            {
                model.PersonnelId =
                    int.Parse(model.Personnels.First().Value);

                model.PersonnelName =
                    model.Personnels.First().Text;
            }


            return View(model);
        }


        // =========================================================
        // SEARCH
        // =========================================================

        [HttpGet]
        public async Task<IActionResult> Search(
            int year,
            int month,
            int personnelId)
        {
            string prefix =
                $"{year:0000}/{month:00}";


            var attendance = await _context.Attendances

                .Include(x => x.Personnel)

                .Where(x =>
                    x.PersonnelId == personnelId &&
                    x.AttendancePersianDate.StartsWith(prefix))

                .OrderBy(x => x.AttendanceDate)

                .AsNoTracking()

                .ToListAsync();


            return PartialView(
                "_AttendanceList",
                attendance);
        }


        // =========================================================
        // CREATE - GET
        // =========================================================

        [HttpGet]
        public async Task<IActionResult> Create(
            int year,
            int month,
            int personnelId)
        {
            var model = new Attendance
            {
                PersonnelId = personnelId,
                Personnel = _context.Personnels.Where(i => i.Id == personnelId).FirstOrDefault(),
                AttendancePersianDate =
                    $"{year:0000}/{month:00}/01"
            };


            return PartialView(
                "_AttendanceForm",
                model);
        }


        // =========================================================
        // CREATE - POST
        // =========================================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            Attendance model)
        {
            ModelState.Remove(nameof(Attendance.Personnel));


            if (!ModelState.IsValid)
            {
                return PartialView(
                    "_AttendanceForm",
                    model);
            }


            // Prevent duplicate attendance date/personnel

            bool exists = await _context.Attendances
                .AnyAsync(x =>
                    x.PersonnelId == model.PersonnelId &&
                    x.AttendanceDate == model.AttendanceDate);


            if (exists)
            {
                ModelState.AddModelError(
                    nameof(model.AttendanceDate),
                    "برای این پرسنل در این تاریخ، رکورد حضور قبلاً ثبت شده است.");

                return PartialView(
                    "_AttendanceForm",
                    model);
            }


            // Id is NOT Identity

            var maxId = await _context.Attendances
                .Select(x => (int?)x.Id)
                .MaxAsync();


            model.Id = (maxId ?? 0) + 1;


            _context.Attendances.Add(model);

            await _context.SaveChangesAsync();


            return Json(new
            {
                success = true
            });
        }


        // =========================================================
        // EDIT - GET
        // =========================================================

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _context.Attendances.Include(x => x.Personnel).Where(x => x.Id == id).FirstOrDefaultAsync();


            if (item == null)
                return NotFound();


            return PartialView(
                "_AttendanceForm",
                item);
        }


        // =========================================================
        // EDIT - POST
        // =========================================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            Attendance model)
        {
            ModelState.Remove(nameof(Attendance.Personnel));


            if (!ModelState.IsValid)
            {
                return PartialView(
                    "_AttendanceForm",
                    model);
            }


            var existing = await _context.Attendances.FindAsync(model.Id);


            if (existing == null)
                return NotFound();

            existing.AttendancePersianDate = model.AttendancePersianDate;
            existing.AttendanceDate = AppUtil.S2M(model.AttendancePersianDate);
            existing.PersonnelId = model.PersonnelId;
            existing.WorkingExpectedMinute = model.WorkingExpectedMinute;
            existing.WorkingMinute = model.WorkingMinute;
            existing.ExtraMinute = model.ExtraMinute;
            existing.HolidayMinute = model.HolidayMinute;
            existing.DelayMinute = model.DelayMinute;
            existing.EarlyArrivalMinute = model.EarlyArrivalMinute;
            existing.LeaveNormalMinute = model.LeaveNormalMinute;
            existing.LeaveWithoutSalaryMinute = model.LeaveWithoutSalaryMinute;
            existing.LeaveSickMinute = model.LeaveSickMinute;
            existing.AbsenceMinute = model.AbsenceMinute;
            existing.MissionMinute = model.MissionMinute;
            existing.Status = model.Status;
            existing.HasWorked = model.HasWorked;
            existing.Personnel = _context.Personnels.Where(i => i.Id == model.PersonnelId).FirstOrDefault();
            await _context.SaveChangesAsync();


            return Json(new
            {
                success = true
            });
        }


        // =========================================================
        // DELETE
        // =========================================================

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Attendances
                .FindAsync(id);


            if (item == null)
                return NotFound();

            var existing = await _context.Attendances.FindAsync(id);


            if (existing == null)
                return NotFound();

            // existing.WorkingExpectedMinute = 0;
            existing.WorkingMinute = 0;
            existing.ExtraMinute = 0;
            existing.HolidayMinute = 0;
            existing.DelayMinute = 0;
            existing.EarlyArrivalMinute = 0;
            existing.LeaveNormalMinute = 0;
            existing.LeaveWithoutSalaryMinute = 0;
            existing.LeaveSickMinute = 0;
            existing.AbsenceMinute = 0; // existing.WorkingExpectedMinute;
            existing.MissionMinute = 0;
            existing.Status = 0;

            bool isholiday = await _holidayService.IsHolidayAsync(AppUtil.S2M(existing.AttendancePersianDate));
            bool isFriday = AppUtil.S2M(existing.AttendancePersianDate).DayOfWeek == DayOfWeek.Friday;
            if (isholiday || isFriday)
                existing.HasWorked = true;
            else
                existing.HasWorked = false;

            await _context.SaveChangesAsync();


            return Json(new
            {
                success = true
            });
        }

        [HttpPost]
        ////[ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckFile(
             AttendanceImportViewModel model)
        {
            if (model.File == null ||
                model.File.Length == 0)
            {
                ModelState.AddModelError(
                    "File",
                    "لطفاً فایل Excel را انتخاب کنید.");

                FillImportYears(model);
                FillMonths(model);

                return PartialView(
                    "_AttendanceImport",
                    model);
            }


            var extension =
                Path.GetExtension(
                    model.File.FileName)
                .ToLowerInvariant();



            if (extension != ".xlsx" && extension != ".xls")
            {
                ModelState.AddModelError(
                    "File",
                    "فقط فایل Excel با پسوند xlsx مجاز است.");

                FillImportYears(model);
                FillMonths(model);

                return PartialView(
                    "_AttendanceImport",
                    model);
            }

            try
            {
                var (imported, updated, correctDate, ym) =
                    await ImportExcel(
                        model.File,
                        model.Year,
                        model.Month, model.File.FileName);

                return Json(new
                {
                    success = true,
                    imported = imported,
                    updated = updated,
                    ym = ym,
                    correctDate = correctDate,
                    skipped = 0
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        // =========================================================
        // IMPORT EXCEL - GET
        // =========================================================

        [HttpGet]
        public IActionResult Import()
        {
            var model = new AttendanceImportViewModel();

            FillImportYears(model);

            FillMonths(model);


            return PartialView(
                "_AttendanceImport",
                model);
        }


        // =========================================================
        // IMPORT EXCEL - POST
        // =========================================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Import(
            AttendanceImportViewModel model)
        {
            if (model.File == null ||
                model.File.Length == 0)
            {
                ModelState.AddModelError(
                    "File",
                    "لطفاً فایل Excel را انتخاب کنید.");

                FillImportYears(model);
                FillMonths(model);

                return PartialView(
                    "_AttendanceImport",
                    model);
            }


            var extension =
                Path.GetExtension(
                    model.File.FileName)
                .ToLowerInvariant();



            if (extension != ".xlsx" && extension != ".xls")
            {
                ModelState.AddModelError(
                    "File",
                    "فقط فایل Excel با پسوند xlsx مجاز است.");

                FillImportYears(model);
                FillMonths(model);

                return PartialView(
                    "_AttendanceImport",
                    model);
            }


            try
            {
                var (imported, updated, correctDate, ym) =
                    await ImportExcel(
                        model.File,
                        model.Year,
                        model.Month, model.File.FileName);


                return Json(new
                {
                    success = true,
                    imported = imported,
                    updated = updated,
                    skipped = 0
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        private int hourToMinute(string hour)
        {
            //"08:00"
            var time = TimeSpan.Parse(hour);
            int minutes = (int)time.TotalMinutes;

            return minutes;
        }
        // =========================================================
        // EXCEL IMPORT
        // =========================================================

        private async Task<(int imported, int updated, bool correctDate, string ym)> ImportExcel(
            IFormFile file,
            int year,
            int month,
            string filename)
        {
            var match = Regex.Match(filename, @"(?<!\d)09\d{9}(?!\d)");
            string mobile = match.Value;
            //string mobile = Path.GetFileNameWithoutExtension(filename)
            //       .TakeLast(11)
            //       .Aggregate("", (s, c) => s + c);

            Personnel personnel = _context.Personnels.Where(i => i.Mobile == mobile).FirstOrDefault();
            if (personnel == null)
                return (-1, -1, false, "");
            int personnelid = personnel.Id;

            MemoryStream stream = new MemoryStream();
            string extension = System.IO.Path.GetExtension(filename);
            if (extension == ".xls")
            {
                Stream stream1 = null;
                string src = filename;
                string dest = System.IO.Path.GetFileNameWithoutExtension(filename) + ".xlsx";
                byte[] xlsxBytes = null;

                stream1 = file.OpenReadStream();

                xlsxBytes = AppUtil.ConvertXlsToXlsx(stream1);

                stream = new MemoryStream(xlsxBytes);
            }
            else
                await file.CopyToAsync(stream);

            stream.Position = 0;

            using var workbook = new ClosedXML.Excel.XLWorkbook(stream);
            var worksheet = workbook.Worksheets.First();
            var rows = worksheet.RowsUsed().Skip(2);

            var maxId =
                await _context.Attendances
                    .Select(x => (int?)x.Id)
                    .MaxAsync()
                ?? 0;
            int nextId = maxId + 1;

            int imported = 0;
            int updated = 0;
            bool correctDate = true;
            string ym = "";

            foreach (var row in rows)
            {
                // check last row
                string temp = row.Cell(1).GetString().Replace("-", "/").Trim();
                bool isValid = Regex.IsMatch(
                        temp,
                        @"^\d{4}[-/]\d{2}[-/]\d{2}$"
                    );
                if (!isValid)
                    continue;

                var attendance = new Attendance();

                /*
                 Excel columns:

                 A = AttendanceDate
                 B = PersonnelId
                 C = WorkingExpectedMinute
                 D = WorkingMinute
                 E = ExtraMinute
                 F = HolidayMinute
                 G = DelayMinute
                 H = EarlyArrivalMinute
                 I = LeaveNormalMinute
                 J = LeaveWithoutSalaryMinute
                 K = LeaveSickMinute
                 L = AbsenceMinute
                 M = MissionMinute
                 N = Status
                 O = HasWorked
                */
                attendance.Id = nextId++;
                //correctDate
                string AttendancePersianDate = row.Cell(1).GetString().Replace("-", "/").Trim();
                ym = AttendancePersianDate.Substring(0, 7);
                if (AttendancePersianDate.Substring(0, 7) != year + @"/" + month.ToString("00"))
                {
                    return (imported, updated, correctDate, ym);
                }

                attendance.AttendancePersianDate = AttendancePersianDate;
                attendance.AttendanceDate = AppUtil.S2M(attendance.AttendancePersianDate);
                attendance.PersonnelId = personnelid; // row.Cell(2).GetValue<int>();
                attendance.ShiftMinute = hourToMinute(row.Cell(8).GetString());
                attendance.WorkingExpectedMinute = hourToMinute(row.Cell(9).GetString());
                attendance.WorkingMinute = hourToMinute(row.Cell(10).GetString());
                attendance.ExtraMinute = hourToMinute(row.Cell(11).GetString());
                attendance.HolidayMinute = hourToMinute(row.Cell(12).GetString());
                attendance.DelayMinute = hourToMinute(row.Cell(13).GetString());
                attendance.EarlyArrivalMinute = hourToMinute(row.Cell(14).GetString());
                attendance.LeaveNormalMinute = hourToMinute(row.Cell(15).GetString());
                //attendance.LeaveWithoutSalaryMinute = row.Cell(10).GetValue<int>();
                //attendance.LeaveSickMinute = row.Cell(11).GetValue<int>();
                attendance.AbsenceMinute = hourToMinute(row.Cell(16).GetString());
                attendance.MissionMinute = hourToMinute(row.Cell(17).GetString());
                string stat = row.Cell(21).GetString();
                attendance.Status = stat == "صحیح" ? 1 : 0;

                //attendance.HasWorked = row.Cell(15).GetValue<bool>();

                // Make sure imported date belongs
                // to selected year/month

                string prefix = $"{year:0000}/{month:00}";

                if (!attendance.AttendancePersianDate
                    .StartsWith(prefix))
                {
                    continue;
                }


                // Check Personnel

                bool personnelExists =
                    await _context.Personnels
                        .AnyAsync(x =>
                            x.Id ==
                            attendance.PersonnelId);


                if (!personnelExists)
                    continue;


                // Existing record?

                var existing =
                    await _context.Attendances
                        .FirstOrDefaultAsync(x =>
                            x.PersonnelId ==
                            attendance.PersonnelId &&

                            x.AttendanceDate ==
                            attendance.AttendanceDate);


                if (existing != null)
                {
                    // Update existing

                    existing.WorkingExpectedMinute =
                        attendance.WorkingExpectedMinute;

                    existing.WorkingMinute =
                        attendance.WorkingMinute;

                    existing.ExtraMinute =
                        attendance.ExtraMinute;

                    existing.HolidayMinute =
                        attendance.HolidayMinute;

                    existing.DelayMinute =
                        attendance.DelayMinute;

                    existing.EarlyArrivalMinute =
                        attendance.EarlyArrivalMinute;

                    existing.LeaveNormalMinute =
                        attendance.LeaveNormalMinute;

                    existing.LeaveWithoutSalaryMinute =
                        attendance.LeaveWithoutSalaryMinute;

                    existing.LeaveSickMinute =
                        attendance.LeaveSickMinute;

                    existing.AbsenceMinute =
                        attendance.AbsenceMinute;

                    existing.MissionMinute =
                        attendance.MissionMinute;

                    existing.Status =
                        attendance.Status;

                    existing.HasWorked =
                        attendance.HasWorked;

                    updated++;
                }
                else
                {
                    _context.Attendances
                        .Add(attendance);

                    imported++;
                }
            }

            await _context.SaveChangesAsync();

            return (imported, updated, correctDate, ym);
        }


        // =========================================================
        // SELECT LISTS
        // =========================================================

        private async Task FillSelections(
            AttendanceViewModel model)
        {
            FillYears(model);
            FillMonths(model);

            model.Personnels =
                await _context.Personnels
                    .OrderBy(x => x.LastName)
                    .ThenBy(x => x.FirstName)
                    .Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.LastName + " " + x.FirstName
                    })
                    .ToListAsync();
        }

        private void FillYears(AttendanceViewModel model)
        {
            var pc = new PersianCalendar();
            int currentYear = pc.GetYear(DateTime.Now);

            model.Years =
                Enumerable.Range(currentYear - 29, 30)
                .OrderByDescending(x => x)
                .Select(x =>
                    new SelectListItem
                    {
                        Value = x.ToString(),
                        Text = x.ToString()
                    })
                .ToList();
        }

        private void FillImportYears(
            AttendanceImportViewModel model)
        {
            var pc =
                new PersianCalendar();


            int currentYear =
                pc.GetYear(DateTime.Now);


            model.Years =
                Enumerable.Range(
                    currentYear - 29,
                    30)

                .OrderByDescending(x => x)

                .Select(x =>
                    new SelectListItem
                    {
                        Value = x.ToString(),
                        Text = x.ToString()
                    })

                .ToList();
        }
        // =========================================================
        // List
        // =========================================================

        //[HttpGet]
        //public async Task<IActionResult> List(
        //    int year,
        //    int month,
        //    int personnelId)
        //{
        //    string prefix =
        //        $"{year:0000}/{month:00}/";


        //    var data = await _context.Attendances
        //        .Where(x =>
        //            x.PersonnelId == personnelId &&
        //            x.AttendanceDate.StartsWith(prefix))
        //        .OrderBy(x => x.AttendanceDate)
        //        .ToListAsync();


        //    ViewBag.Year = year;
        //    ViewBag.Month = month;
        //    ViewBag.PersonnelId = personnelId;


        //    return PartialView(
        //        "_AttendanceList",
        //        data);
        //}

        [HttpGet]
        public async Task<IActionResult> CheckList(
            int year,
            int month,
            int personnelId,
            string CorrectErrText = "",
            string HasWorkedText = ""
            )
        {
            ViewBag.year = year;
            ViewBag.month = month;
            ViewBag.personnelId = personnelId;
            ViewBag.CorrectErrText = CorrectErrText;
            ViewBag.HasWorkedText = HasWorkedText;
            return PartialView("_AttendanceCheckForm");
        }

        [HttpGet]
        public async Task<IActionResult> List(
            int year,
            int month,
            int personnelId,
            string CorrectErrText = "",
            string HasWorkedText = ""
            )
        {
            string prefix = $"{year:0000}/{month:00}/";

            var attendances = await _context.Attendances
                .Where(x =>
                    x.PersonnelId == personnelId &&
                    x.AttendancePersianDate.StartsWith(prefix))
                .OrderBy(x => x.AttendanceDate)
                .ToListAsync();

            var holidays = await _context.Holidays
                .Where(x => x.PersianDate.StartsWith(prefix))
                .Select(x => x.Date)
                .ToListAsync();

            List<AttendanceListViewModel> data = new List<AttendanceListViewModel>();

            data = attendances
                            .Select(x => new AttendanceListViewModel
                            {
                                Id = x.Id,
                                PersonnelId = x.PersonnelId,
                                AttendanceDate = x.AttendanceDate,
                                AttendancePersianDate = AppUtil.M2S(x.AttendanceDate),
                                WorkingExpectedMinute = x.WorkingExpectedMinute,
                                WorkingMinute = x.WorkingMinute,
                                ExtraMinute = x.ExtraMinute,
                                HolidayMinute = x.HolidayMinute,
                                DelayMinute = x.DelayMinute,
                                EarlyArrivalMinute = x.EarlyArrivalMinute,
                                LeaveNormalMinute = x.LeaveNormalMinute,
                                LeaveSickMinute = x.LeaveSickMinute,
                                AbsenceMinute = x.AbsenceMinute,
                                MissionMinute = x.MissionMinute,

                                HasWorked = x.HasWorked,

                                // New field
                                // Holiday = holidays.Contains(DateUtil.S2M(x.AttendanceDate)),

                            })
                            .ToList();

            foreach (AttendanceListViewModel item in data)
            {
                item.Holiday = await _holidayService.IsHolidayAsync(item.AttendanceDate);
                //if (item.Holiday)
                //    System.Diagnostics.Debugger.Break();

                item.HolidayDescriptrion = await _holidayService.HolidayDescriptionAsync(item.AttendanceDate);
            }
            ViewBag.CorrectErrText = CorrectErrText;
            ViewBag.HasWorkedText = HasWorkedText;

            ViewBag.Year = year;
            ViewBag.Month = month;
            ViewBag.PersonnelId = personnelId;
            ViewBag.PersonnelName = "";
            if (personnelId != 0)
            {
                Personnel person = await _context.Personnels.Where(i => i.Id == personnelId).FirstOrDefaultAsync();
                ViewBag.PersonnelName = person.FirstName + " " + person.LastName;
            }
            return PartialView("_AttendanceList", data);
        }

        private void FillMonths(
            AttendanceViewModel model)
        {
            model.Months =
                Enumerable.Range(1, 12)

                .Select(x =>
                    new SelectListItem
                    {
                        Value = x.ToString(),
                        Text =
                            PersianMonth.Names[x - 1]
                    })

                .ToList();
        }
        private void FillMonths(
           CalculateViewModel model)
        {
            model.Months =
                Enumerable.Range(1, 12)

                .Select(x =>
                    new SelectListItem
                    {
                        Value = x.ToString(),
                        Text =
                            PersianMonth.Names[x - 1]
                    })

                .ToList();
        }

        private void FillMonths(
            AttendanceImportViewModel model)
        {
            model.Months =
                Enumerable.Range(1, 12)

                .Select(x =>
                    new SelectListItem
                    {
                        Value = x.ToString(),
                        Text =
                            PersianMonth.Names[x - 1]
                    })

                .ToList();
        }
    }
}