using givPayroll.Data;
using givPayroll.Models;
using Microsoft.EntityFrameworkCore;

namespace givPayroll.Services
{
    public interface IHolidayService
    {
        Task<bool> IsHolidayAsync(DateTime date);
  Task<string> HolidayDescriptionAsync(string date);
        Task<HolidayInfo?> GetHolidayAsync(DateTime date);
        Task<List<HolidayInfo>> GetHolidaysAsync(
            DateTime fromDate,
            DateTime toDate);
    }

    public class HolidayInfo
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; } = "";
        public bool IsOfficial { get; set; }
    }

    public class HolidayService : IHolidayService
    {
        private readonly ApplicationDbContext _context;

        public HolidayService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsHolidayAsync(DateTime date)
        {
            return await _context.Holidays
                .AnyAsync(x =>
                    x.Date.Date == date.Date);
        }
        public async Task<string> HolidayDescriptionAsync(string date)
        {
            Holiday obj =await _context.Holidays.Where(x => x.PersianDate == date).FirstOrDefaultAsync();
            if (obj == null)
                return "";
            return obj.Title;

        }

        public async Task<HolidayInfo?> GetHolidayAsync(DateTime date)
        {
            return await _context.Holidays
                .Where(x => x.Date.Date == date.Date)
                .Select(x => new HolidayInfo
                {
                    Id = x.Id,
                    Date = x.Date,
                    Title = x.Title,
                    IsOfficial = x.IsOfficial
                })
                .FirstOrDefaultAsync();
        }

        public async Task<List<HolidayInfo>> GetHolidaysAsync(
            DateTime fromDate,
            DateTime toDate)
        {
            return await _context.Holidays
                .Where(x =>
                    x.Date >= fromDate.Date &&
                    x.Date <= toDate.Date)
                .OrderBy(x => x.Date)
                .Select(x => new HolidayInfo
                {
                    Id = x.Id,
                    Date = x.Date,
                    Title = x.Title,
                    IsOfficial = x.IsOfficial
                })
                .ToListAsync();
        }
    }
}