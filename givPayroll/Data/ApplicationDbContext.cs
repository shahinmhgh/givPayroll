using givPayroll.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace givPayroll.Data
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
       : base(options)
        {
        }

        public DbSet<Personnel> Personnels { get; set; }
        public DbSet<MaritalStatus> MaritalStatus { get; set; }
        public DbSet<Education> Educations { get; set; }

        public DbSet<PersonnelFamily> PersonnelFamilies { get; set; }
        public DbSet<FamilyRelation> FamilyRelations { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<MaritalStatus> MarriageStatus { get; set; }
        public DbSet<Parameter> Parameters { get; set; }
        public DbSet<Company> Companies { get; set; }

        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<PersonnelContract> Contracts { get; set; }
        public DbSet<ContractType> ContractTypes { get; set; }
        public DbSet<ContractStatus> ContractStatuses { get; set; }

        public DbSet<SalaryItem> SalaryItems { get; set; }
        public DbSet<SalaryItemRule> SalaryItemRules { get; set; }
        public DbSet<PersonnelOrder> PersonnelOrders { get; set; }

        public DbSet<PersonnelOrderDetail> PersonnelOrderDetails { get; set; }

        public DbSet<RuleInsuranceGroup> RuleInsuranceGroups { get; set; }

        public DbSet<RuleInsurance> RuleInsurances { get; set; }

        public DbSet<RuleTax> RuleTaxes { get; set; }

        public DbSet<PayrollAdjustment> PayrollAdjustments { get; set; }
        public DbSet<PayrollAdjustmentPersonnel> PayrollAdjustmentPersonnels { get; set; }
        public DbSet<PayrollAdjustmentDetail> PayrollAdjustmentDetails { get; set; }

        public DbSet<Payroll> Payrolls { get; set; }
        public DbSet<PayrollItem> PayrollItems { get; set; }

        public DbSet<Job> Job { get; set; }
        public DbSet<JobGrade> JobGrades { get; set; }
        public DbSet<JobGroup> JobGroups { get; set; }
        public DbSet<Holiday> Holidays { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<PayrollStatus>().HasData(
        new PayrollStatus
        {
            Id = 0,
            StatusName = "جدید"
        },
        new PayrollStatus
        {
            Id = 1,
            StatusName = "حضور و غیاب ناقص"
        },
        new PayrollStatus
        {
            Id = 100,
            StatusName = "آماده محاسبه"
        },
        new PayrollStatus
        {
            Id = 200,
            StatusName = "محاسبه شده"
        },
        new PayrollStatus
        {
            Id = 220,
            StatusName = "تایید شده"
        },
        new PayrollStatus
        {
            Id = 300,
            StatusName = "ارسال به حسابداری"
        }
    );

            builder.Entity<Gender>().HasData(
                  new Gender
                  {
                      Id = 0,
                      GenderName = ""
                  },
                   new Gender
                   {
                       Id = 1,
                       GenderName = "مرد"
                   },
                new Gender
                {
                    Id = 2,
                    GenderName = "زن"
                }
            );

            builder.Entity<JobGrade>().HasData(
                new JobGrade { Id = 1, Code = 1, Title = "طبقه 1" },
                new JobGrade { Id = 2, Code = 2, Title = "طبقه 2" },
                new JobGrade { Id = 3, Code = 3, Title = "طبقه 3" },
                new JobGrade { Id = 4, Code = 4, Title = "طبقه 4" },
                new JobGrade { Id = 5, Code = 5, Title = "طبقه 5" },
                new JobGrade { Id = 6, Code = 6, Title = "طبقه 6" },
                new JobGrade { Id = 7, Code = 7, Title = "طبقه 7" },
                new JobGrade { Id = 8, Code = 8, Title = "طبقه 8" },
                new JobGrade { Id = 9, Code = 9, Title = "طبقه 9" },
                new JobGrade { Id = 10, Code = 10, Title = "طبقه 10" },
                new JobGrade { Id = 11, Code = 11, Title = "طبقه 11" },
                new JobGrade { Id = 12, Code = 12, Title = "طبقه 12" },
                new JobGrade { Id = 13, Code = 13, Title = "طبقه 13" },
                new JobGrade { Id = 14, Code = 14, Title = "طبقه 14" },
                new JobGrade { Id = 15, Code = 15, Title = "طبقه 15" },
                new JobGrade { Id = 16, Code = 16, Title = "طبقه 16" },
                new JobGrade { Id = 17, Code = 17, Title = "طبقه 17" },
                new JobGrade { Id = 18, Code = 18, Title = "طبقه 18" },
                new JobGrade { Id = 19, Code = 19, Title = "طبقه 19" },
                new JobGrade { Id = 20, Code = 20, Title = "طبقه 20" }
            );

            builder.Entity<JobGroup>().HasData(
                new JobGroup { Id = 1, Name = "مدیریت" },
                new JobGroup { Id = 2, Name = "منابع انسانی" },
                new JobGroup { Id = 3, Name = "مالی" },
                new JobGroup { Id = 4, Name = "اداری" },
                new JobGroup { Id = 5, Name = "بازرگانی" },
                new JobGroup { Id = 6, Name = "فروش" },
                new JobGroup { Id = 7, Name = "تولید" },
                new JobGroup { Id = 8, Name = "فنی" },
                new JobGroup { Id = 9, Name = "مهندسی" },
                new JobGroup { Id = 10, Name = "فناوری اطلاعات" },
                new JobGroup { Id = 11, Name = "لجستیک" },
                new JobGroup { Id = 12, Name = "کنترل کیفیت" },
                new JobGroup { Id = 13, Name = "ایمنی" },
                new JobGroup { Id = 14, Name = "خدمات" },
                new JobGroup { Id = 15, Name = "حراست" },
                new JobGroup { Id = 16, Name = "حمل‌ونقل" }
            );

            builder.Entity<Job>().HasData(

                // Management
                new Job { Id = 1, Code = 1001, Title = "مدیرعامل", JobGroupId = 1, JobGradeId = 20, Active = true },
                new Job { Id = 2, Code = 1002, Title = "معاون مدیرعامل", JobGroupId = 1, JobGradeId = 19, Active = true },
                new Job { Id = 3, Code = 1003, Title = "مدیر منابع انسانی", JobGroupId = 2, JobGradeId = 17, Active = true },
                new Job { Id = 4, Code = 1004, Title = "مدیر مالی", JobGroupId = 3, JobGradeId = 17, Active = true },
                new Job { Id = 5, Code = 1005, Title = "مدیر اداری", JobGroupId = 4, JobGradeId = 16, Active = true },
                new Job { Id = 6, Code = 1006, Title = "مدیر بازرگانی", JobGroupId = 5, JobGradeId = 17, Active = true },
                new Job { Id = 7, Code = 1007, Title = "مدیر فروش", JobGroupId = 6, JobGradeId = 17, Active = true },
                new Job { Id = 8, Code = 1008, Title = "مدیر تولید", JobGroupId = 7, JobGradeId = 17, Active = true },
                new Job { Id = 9, Code = 1009, Title = "مدیر کارخانه", JobGroupId = 7, JobGradeId = 18, Active = true },
                new Job { Id = 10, Code = 1010, Title = "مدیر فناوری اطلاعات", JobGroupId = 10, JobGradeId = 17, Active = true },
                new Job { Id = 11, Code = 1011, Title = "مدیر برنامه‌ریزی", JobGroupId = 1, JobGradeId = 16, Active = true },
                new Job { Id = 12, Code = 1012, Title = "مدیر کنترل کیفیت", JobGroupId = 12, JobGradeId = 16, Active = true },
                new Job { Id = 13, Code = 1013, Title = "مدیر تدارکات", JobGroupId = 11, JobGradeId = 16, Active = true },
                new Job { Id = 14, Code = 1014, Title = "مدیر انبار", JobGroupId = 11, JobGradeId = 15, Active = true },
                new Job { Id = 15, Code = 1015, Title = "مدیر پروژه", JobGroupId = 9, JobGradeId = 16, Active = true },

                // Supervisors
                new Job { Id = 16, Code = 1016, Title = "رئیس منابع انسانی", JobGroupId = 2, JobGradeId = 14, Active = true },
                new Job { Id = 17, Code = 1017, Title = "رئیس امور اداری", JobGroupId = 4, JobGradeId = 14, Active = true },
                new Job { Id = 18, Code = 1018, Title = "رئیس حسابداری", JobGroupId = 3, JobGradeId = 14, Active = true },
                new Job { Id = 19, Code = 1019, Title = "رئیس حسابرسی", JobGroupId = 3, JobGradeId = 14, Active = true },
                new Job { Id = 20, Code = 1020, Title = "رئیس تولید", JobGroupId = 7, JobGradeId = 14, Active = true },
                new Job { Id = 21, Code = 1021, Title = "رئیس تعمیرات", JobGroupId = 8, JobGradeId = 14, Active = true },
                new Job { Id = 22, Code = 1022, Title = "رئیس کنترل کیفیت", JobGroupId = 12, JobGradeId = 14, Active = true },
                new Job { Id = 23, Code = 1023, Title = "رئیس فناوری اطلاعات", JobGroupId = 10, JobGradeId = 14, Active = true },

                new Job { Id = 24, Code = 1024, Title = "سرپرست اداری", JobGroupId = 4, JobGradeId = 12, Active = true },
                new Job { Id = 25, Code = 1025, Title = "سرپرست مالی", JobGroupId = 3, JobGradeId = 12, Active = true },
                new Job { Id = 26, Code = 1026, Title = "سرپرست منابع انسانی", JobGroupId = 2, JobGradeId = 12, Active = true },
                new Job { Id = 27, Code = 1027, Title = "سرپرست تولید", JobGroupId = 7, JobGradeId = 12, Active = true },
                new Job { Id = 28, Code = 1028, Title = "سرپرست انبار", JobGroupId = 11, JobGradeId = 11, Active = true },
                new Job { Id = 29, Code = 1029, Title = "سرپرست تعمیرات", JobGroupId = 8, JobGradeId = 12, Active = true },
                new Job { Id = 30, Code = 1030, Title = "سرپرست فروش", JobGroupId = 6, JobGradeId = 12, Active = true },
                new Job { Id = 31, Code = 1031, Title = "سرپرست تدارکات", JobGroupId = 11, JobGradeId = 11, Active = true },
                new Job { Id = 32, Code = 1032, Title = "سرپرست شیفت", JobGroupId = 7, JobGradeId = 11, Active = true },

                // HR / Administrative
                new Job { Id = 33, Code = 1033, Title = "کارشناس منابع انسانی", JobGroupId = 2, JobGradeId = 9, Active = true },
                new Job { Id = 34, Code = 1034, Title = "کارشناس حقوق و دستمزد", JobGroupId = 3, JobGradeId = 10, Active = true },
                new Job { Id = 35, Code = 1035, Title = "کارشناس امور اداری", JobGroupId = 4, JobGradeId = 8, Active = true },
                new Job { Id = 36, Code = 1036, Title = "کارشناس استخدام", JobGroupId = 2, JobGradeId = 9, Active = true },
                new Job { Id = 37, Code = 1037, Title = "کارشناس آموزش", JobGroupId = 2, JobGradeId = 9, Active = true },
                new Job { Id = 38, Code = 1038, Title = "کارشناس روابط کار", JobGroupId = 2, JobGradeId = 10, Active = true },
                new Job { Id = 39, Code = 1039, Title = "کارشناس بیمه", JobGroupId = 3, JobGradeId = 9, Active = true },

                // Finance
                new Job { Id = 40, Code = 1040, Title = "کارشناس حسابداری", JobGroupId = 3, JobGradeId = 9, Active = true },
                new Job { Id = 41, Code = 1041, Title = "حسابدار ارشد", JobGroupId = 3, JobGradeId = 10, Active = true },
                new Job { Id = 42, Code = 1042, Title = "حسابدار", JobGroupId = 3, JobGradeId = 8, Active = true },
                new Job { Id = 43, Code = 1043, Title = "کمک حسابدار", JobGroupId = 3, JobGradeId = 6, Active = true },
                new Job { Id = 44, Code = 1044, Title = "کارشناس مالی", JobGroupId = 3, JobGradeId = 9, Active = true },
                new Job { Id = 45, Code = 1045, Title = "کارشناس بودجه", JobGroupId = 3, JobGradeId = 10, Active = true },
                new Job { Id = 46, Code = 1046, Title = "کارشناس حسابرسی", JobGroupId = 3, JobGradeId = 10, Active = true },
                new Job { Id = 47, Code = 1047, Title = "کارشناس مالیاتی", JobGroupId = 3, JobGradeId = 10, Active = true },

                // Commerce / Sales
                new Job { Id = 48, Code = 1048, Title = "کارشناس خرید", JobGroupId = 5, JobGradeId = 8, Active = true },
                new Job { Id = 49, Code = 1049, Title = "کارشناس بازرگانی", JobGroupId = 5, JobGradeId = 9, Active = true },
                new Job { Id = 50, Code = 1050, Title = "کارشناس قراردادها", JobGroupId = 5, JobGradeId = 10, Active = true },
                new Job { Id = 51, Code = 1051, Title = "کارشناس حقوقی", JobGroupId = 4, JobGradeId = 10, Active = true },
                new Job { Id = 52, Code = 1052, Title = "کارشناس فروش", JobGroupId = 6, JobGradeId = 8, Active = true },
                new Job { Id = 53, Code = 1053, Title = "کارشناس بازاریابی", JobGroupId = 6, JobGradeId = 9, Active = true },

                // Planning / Quality / Safety
                new Job { Id = 54, Code = 1054, Title = "کارشناس برنامه‌ریزی", JobGroupId = 1, JobGradeId = 9, Active = true },
                new Job { Id = 55, Code = 1055, Title = "کارشناس برنامه‌ریزی تولید", JobGroupId = 7, JobGradeId = 9, Active = true },
                new Job { Id = 56, Code = 1056, Title = "کارشناس کنترل کیفیت", JobGroupId = 12, JobGradeId = 9, Active = true },
                new Job { Id = 57, Code = 1057, Title = "کارشناس ایمنی", JobGroupId = 13, JobGradeId = 9, Active = true },

                // IT
                new Job { Id = 58, Code = 1058, Title = "کارشناس فناوری اطلاعات", JobGroupId = 10, JobGradeId = 9, Active = true },
                new Job { Id = 59, Code = 1059, Title = "کارشناس شبکه", JobGroupId = 10, JobGradeId = 10, Active = true },
                new Job { Id = 60, Code = 1060, Title = "کارشناس نرم‌افزار", JobGroupId = 10, JobGradeId = 10, Active = true },
                new Job { Id = 61, Code = 1061, Title = "کارشناس سخت‌افزار", JobGroupId = 10, JobGradeId = 9, Active = true },
                new Job { Id = 62, Code = 1062, Title = "کارشناس پایگاه داده", JobGroupId = 10, JobGradeId = 10, Active = true },
                new Job { Id = 63, Code = 1063, Title = "کارشناس امنیت اطلاعات", JobGroupId = 10, JobGradeId = 11, Active = true },

                // Warehouse / Logistics
                new Job { Id = 64, Code = 1064, Title = "کارشناس انبار", JobGroupId = 11, JobGradeId = 8, Active = true },
                new Job { Id = 65, Code = 1065, Title = "کارشناس لجستیک", JobGroupId = 11, JobGradeId = 9, Active = true },
                new Job { Id = 66, Code = 1066, Title = "کارشناس حمل‌ونقل", JobGroupId = 16, JobGradeId = 8, Active = true },

                new Job { Id = 67, Code = 1067, Title = "مسئول دفتر", JobGroupId = 4, JobGradeId = 7, Active = true },
                new Job { Id = 68, Code = 1068, Title = "منشی", JobGroupId = 4, JobGradeId = 5, Active = true },
                new Job { Id = 69, Code = 1069, Title = "مسئول بایگانی", JobGroupId = 4, JobGradeId = 5, Active = true },
                new Job { Id = 70, Code = 1070, Title = "بایگان", JobGroupId = 4, JobGradeId = 4, Active = true },

                // Purchasing / Warehouse
                new Job { Id = 71, Code = 1071, Title = "مسئول خرید", JobGroupId = 5, JobGradeId = 8, Active = true },
                new Job { Id = 72, Code = 1072, Title = "مسئول تدارکات", JobGroupId = 11, JobGradeId = 8, Active = true },
                new Job { Id = 73, Code = 1073, Title = "مسئول انبار", JobGroupId = 11, JobGradeId = 8, Active = true },
                new Job { Id = 74, Code = 1074, Title = "انباردار", JobGroupId = 11, JobGradeId = 6, Active = true },
                new Job { Id = 75, Code = 1075, Title = "کمک انباردار", JobGroupId = 11, JobGradeId = 4, Active = true },

                // Production
                new Job { Id = 76, Code = 1076, Title = "مسئول کنترل کیفیت", JobGroupId = 12, JobGradeId = 8, Active = true },
                new Job { Id = 77, Code = 1077, Title = "بازرس کنترل کیفیت", JobGroupId = 12, JobGradeId = 7, Active = true },
                new Job { Id = 78, Code = 1078, Title = "مسئول تولید", JobGroupId = 7, JobGradeId = 8, Active = true },
                new Job { Id = 79, Code = 1079, Title = "مسئول خط تولید", JobGroupId = 7, JobGradeId = 7, Active = true },
                new Job { Id = 80, Code = 1080, Title = "اپراتور تولید", JobGroupId = 7, JobGradeId = 5, Active = true },
                new Job { Id = 81, Code = 1081, Title = "اپراتور دستگاه", JobGroupId = 7, JobGradeId = 6, Active = true },
                new Job { Id = 82, Code = 1082, Title = "اپراتور ماشین‌آلات", JobGroupId = 7, JobGradeId = 6, Active = true },
                new Job { Id = 83, Code = 1083, Title = "اپراتور CNC", JobGroupId = 8, JobGradeId = 8, Active = true },
                new Job { Id = 84, Code = 1084, Title = "اپراتور بسته‌بندی", JobGroupId = 7, JobGradeId = 4, Active = true },
                new Job { Id = 85, Code = 1085, Title = "کارگر تولید", JobGroupId = 7, JobGradeId = 3, Active = true },
                new Job { Id = 86, Code = 1086, Title = "کارگر ماهر", JobGroupId = 7, JobGradeId = 5, Active = true },
                new Job { Id = 87, Code = 1087, Title = "کارگر ساده", JobGroupId = 7, JobGradeId = 2, Active = true },
                new Job { Id = 88, Code = 1088, Title = "کارگر بسته‌بندی", JobGroupId = 7, JobGradeId = 3, Active = true },
                new Job { Id = 89, Code = 1089, Title = "کارگر خدمات تولید", JobGroupId = 7, JobGradeId = 3, Active = true },
                new Job { Id = 90, Code = 1090, Title = "مسئول شیفت", JobGroupId = 7, JobGradeId = 8, Active = true },
                new Job { Id = 91, Code = 1091, Title = "سرشیفت", JobGroupId = 7, JobGradeId = 9, Active = true },

                // Technical
                new Job { Id = 92, Code = 1092, Title = "تکنسین فنی", JobGroupId = 8, JobGradeId = 7, Active = true },
                new Job { Id = 93, Code = 1093, Title = "تکنسین برق", JobGroupId = 8, JobGradeId = 7, Active = true },
                new Job { Id = 94, Code = 1094, Title = "تکنسین مکانیک", JobGroupId = 8, JobGradeId = 7, Active = true },
                new Job { Id = 95, Code = 1095, Title = "تکنسین تأسیسات", JobGroupId = 8, JobGradeId = 7, Active = true },
                new Job { Id = 96, Code = 1096, Title = "تکنسین برق صنعتی", JobGroupId = 8, JobGradeId = 8, Active = true },
                new Job { Id = 97, Code = 1097, Title = "تکنسین مکانیک صنعتی", JobGroupId = 8, JobGradeId = 8, Active = true },
                new Job { Id = 98, Code = 1098, Title = "تکنسین ابزار دقیق", JobGroupId = 8, JobGradeId = 9, Active = true },
                new Job { Id = 99, Code = 1099, Title = "تکنسین تعمیرات", JobGroupId = 8, JobGradeId = 7, Active = true },
                new Job { Id = 100, Code = 1100, Title = "تکنسین شبکه", JobGroupId = 10, JobGradeId = 7, Active = true },

                new Job { Id = 101, Code = 1101, Title = "تعمیرکار مکانیک", JobGroupId = 8, JobGradeId = 6, Active = true },
                new Job { Id = 102, Code = 1102, Title = "تعمیرکار برق", JobGroupId = 8, JobGradeId = 6, Active = true },
                new Job { Id = 103, Code = 1103, Title = "برق‌کار", JobGroupId = 8, JobGradeId = 6, Active = true },
                new Job { Id = 104, Code = 1104, Title = "مکانیک", JobGroupId = 8, JobGradeId = 6, Active = true },
                new Job { Id = 105, Code = 1105, Title = "جوشکار", JobGroupId = 8, JobGradeId = 6, Active = true },
                new Job { Id = 106, Code = 1106, Title = "تراشکار", JobGroupId = 8, JobGradeId = 6, Active = true },
                new Job { Id = 107, Code = 1107, Title = "فرزکار", JobGroupId = 8, JobGradeId = 6, Active = true },
                new Job { Id = 108, Code = 1108, Title = "نقشه‌کش صنعتی", JobGroupId = 8, JobGradeId = 8, Active = true },
                new Job { Id = 109, Code = 1109, Title = "کارشناس فنی", JobGroupId = 8, JobGradeId = 9, Active = true },

                // Engineering
                new Job { Id = 110, Code = 1110, Title = "مهندس مکانیک", JobGroupId = 9, JobGradeId = 11, Active = true },
                new Job { Id = 111, Code = 1111, Title = "مهندس برق", JobGroupId = 9, JobGradeId = 11, Active = true },
                new Job { Id = 112, Code = 1112, Title = "مهندس صنایع", JobGroupId = 9, JobGradeId = 11, Active = true },
                new Job { Id = 113, Code = 1113, Title = "مهندس عمران", JobGroupId = 9, JobGradeId = 11, Active = true },
                new Job { Id = 114, Code = 1114, Title = "مهندس کامپیوتر", JobGroupId = 10, JobGradeId = 11, Active = true },
                new Job { Id = 115, Code = 1115, Title = "مهندس نرم‌افزار", JobGroupId = 10, JobGradeId = 12, Active = true },
                new Job { Id = 116, Code = 1116, Title = "مهندس شبکه", JobGroupId = 10, JobGradeId = 12, Active = true },
                new Job { Id = 117, Code = 1117, Title = "مهندس برق صنعتی", JobGroupId = 9, JobGradeId = 12, Active = true },
                new Job { Id = 118, Code = 1118, Title = "مهندس مکانیک صنعتی", JobGroupId = 9, JobGradeId = 12, Active = true },
                new Job { Id = 119, Code = 1119, Title = "مهندس تولید", JobGroupId = 9, JobGradeId = 11, Active = true },
                new Job { Id = 120, Code = 1120, Title = "مهندس کنترل کیفیت", JobGroupId = 9, JobGradeId = 11, Active = true },
                new Job { Id = 121, Code = 1121, Title = "مهندس تعمیرات", JobGroupId = 9, JobGradeId = 11, Active = true },
                new Job { Id = 122, Code = 1122, Title = "مهندس پروژه", JobGroupId = 9, JobGradeId = 12, Active = true },

                new Job { Id = 123, Code = 1123, Title = "مدیر فنی", JobGroupId = 8, JobGradeId = 15, Active = true },
                new Job { Id = 124, Code = 1124, Title = "مدیر مهندسی", JobGroupId = 9, JobGradeId = 16, Active = true },
                new Job { Id = 125, Code = 1125, Title = "مدیر پروژه", JobGroupId = 9, JobGradeId = 16, Active = true },

                // Planning / Supply Chain
                new Job { Id = 126, Code = 1126, Title = "برنامه‌ریز تولید", JobGroupId = 7, JobGradeId = 8, Active = true },
                new Job { Id = 127, Code = 1127, Title = "برنامه‌ریز تعمیرات", JobGroupId = 8, JobGradeId = 8, Active = true },
                new Job { Id = 128, Code = 1128, Title = "برنامه‌ریز مواد", JobGroupId = 11, JobGradeId = 9, Active = true },
                new Job { Id = 129, Code = 1129, Title = "کارشناس زنجیره تأمین", JobGroupId = 11, JobGradeId = 10, Active = true },
                new Job { Id = 130, Code = 1130, Title = "کارشناس لجستیک", JobGroupId = 11, JobGradeId = 9, Active = true },
                new Job { Id = 131, Code = 1131, Title = "کارشناس تأمین", JobGroupId = 5, JobGradeId = 9, Active = true },
                new Job { Id = 132, Code = 1132, Title = "مسئول لجستیک", JobGroupId = 11, JobGradeId = 9, Active = true },
                new Job { Id = 133, Code = 1133, Title = "مسئول حمل‌ونقل", JobGroupId = 16, JobGradeId = 8, Active = true },

                // Transportation
                new Job { Id = 134, Code = 1134, Title = "راننده پایه یک", JobGroupId = 16, JobGradeId = 6, Active = true },
                new Job { Id = 135, Code = 1135, Title = "راننده پایه دو", JobGroupId = 16, JobGradeId = 5, Active = true },
                new Job { Id = 136, Code = 1136, Title = "راننده سواری", JobGroupId = 16, JobGradeId = 4, Active = true },
                new Job { Id = 137, Code = 1137, Title = "راننده کامیون", JobGroupId = 16, JobGradeId = 6, Active = true },
                new Job { Id = 138, Code = 1138, Title = "راننده لیفتراک", JobGroupId = 11, JobGradeId = 5, Active = true },
                new Job { Id = 139, Code = 1139, Title = "اپراتور لیفتراک", JobGroupId = 11, JobGradeId = 5, Active = true },

                // Services / Security
                new Job { Id = 140, Code = 1140, Title = "مسئول خدمات", JobGroupId = 14, JobGradeId = 6, Active = true },
                new Job { Id = 141, Code = 1141, Title = "نیروی خدماتی", JobGroupId = 14, JobGradeId = 3, Active = true },
                new Job { Id = 142, Code = 1142, Title = "آبدارچی", JobGroupId = 14, JobGradeId = 2, Active = true },
                new Job { Id = 143, Code = 1143, Title = "نظافتچی", JobGroupId = 14, JobGradeId = 2, Active = true },
                new Job { Id = 144, Code = 1144, Title = "سرایدار", JobGroupId = 14, JobGradeId = 3, Active = true },

                new Job { Id = 145, Code = 1145, Title = "نگهبان", JobGroupId = 15, JobGradeId = 4, Active = true },
                new Job { Id = 146, Code = 1146, Title = "مسئول حراست", JobGroupId = 15, JobGradeId = 8, Active = true },
                new Job { Id = 147, Code = 1147, Title = "نگهبان شیفت", JobGroupId = 15, JobGradeId = 5, Active = true },

                // HSE
                new Job { Id = 148, Code = 1148, Title = "مسئول ایمنی", JobGroupId = 13, JobGradeId = 9, Active = true },
                new Job { Id = 149, Code = 1149, Title = "کارشناس HSE", JobGroupId = 13, JobGradeId = 10, Active = true },
                new Job { Id = 150, Code = 1150, Title = "کارشناس محیط زیست", JobGroupId = 13, JobGradeId = 9, Active = true },
                new Job { Id = 151, Code = 1151, Title = "کارشناس بهداشت حرفه‌ای", JobGroupId = 13, JobGradeId = 10, Active = true },

                // Health
                new Job { Id = 152, Code = 1152, Title = "پزشک طب کار", JobGroupId = 13, JobGradeId = 12, Active = true },
                new Job { Id = 153, Code = 1153, Title = "پرستار", JobGroupId = 13, JobGradeId = 9, Active = true },
                new Job { Id = 154, Code = 1154, Title = "کمک پرستار", JobGroupId = 13, JobGradeId = 7, Active = true },
                new Job { Id = 155, Code = 1155, Title = "مسئول درمانگاه", JobGroupId = 13, JobGradeId = 9, Active = true },

                // Public Relations / Marketing
                new Job { Id = 156, Code = 1156, Title = "کارشناس روابط عمومی", JobGroupId = 4, JobGradeId = 9, Active = true },
                new Job { Id = 157, Code = 1157, Title = "کارشناس تبلیغات", JobGroupId = 6, JobGradeId = 8, Active = true },
                new Job { Id = 158, Code = 1158, Title = "کارشناس محتوا", JobGroupId = 6, JobGradeId = 8, Active = true },
                new Job { Id = 159, Code = 1159, Title = "کارشناس تحقیقات بازار", JobGroupId = 6, JobGradeId = 9, Active = true },

                // Customer Service
                new Job { Id = 160, Code = 1160, Title = "کارشناس خدمات مشتریان", JobGroupId = 6, JobGradeId = 8, Active = true },
                new Job { Id = 161, Code = 1161, Title = "کارشناس CRM", JobGroupId = 6, JobGradeId = 9, Active = true },
                new Job { Id = 162, Code = 1162, Title = "مسئول خدمات مشتریان", JobGroupId = 6, JobGradeId = 8, Active = true },

                // Import / Export
                new Job { Id = 163, Code = 1163, Title = "کارشناس صادرات", JobGroupId = 5, JobGradeId = 10, Active = true },
                new Job { Id = 164, Code = 1164, Title = "کارشناس واردات", JobGroupId = 5, JobGradeId = 10, Active = true },
                new Job { Id = 165, Code = 1165, Title = "کارشناس امور گمرکی", JobGroupId = 5, JobGradeId = 9, Active = true },
                new Job { Id = 166, Code = 1166, Title = "مسئول گمرک", JobGroupId = 5, JobGradeId = 9, Active = true },

                // HR Specialist
                new Job { Id = 167, Code = 1167, Title = "کارشناس منابع انسانی ارشد", JobGroupId = 2, JobGradeId = 11, Active = true },
                new Job { Id = 168, Code = 1168, Title = "مدیر جبران خدمات", JobGroupId = 2, JobGradeId = 14, Active = true },
                new Job { Id = 169, Code = 1169, Title = "کارشناس جبران خدمات", JobGroupId = 2, JobGradeId = 10, Active = true },
                new Job { Id = 170, Code = 1170, Title = "کارشناس ارزیابی عملکرد", JobGroupId = 2, JobGradeId = 10, Active = true },
                new Job { Id = 171, Code = 1171, Title = "کارشناس جذب و استخدام", JobGroupId = 2, JobGradeId = 9, Active = true },
                new Job { Id = 172, Code = 1172, Title = "کارشناس آموزش و توسعه", JobGroupId = 2, JobGradeId = 10, Active = true },
                new Job { Id = 173, Code = 1173, Title = "کارشناس رفاه کارکنان", JobGroupId = 2, JobGradeId = 8, Active = true },
                new Job { Id = 174, Code = 1174, Title = "کارشناس امور بازنشستگی", JobGroupId = 2, JobGradeId = 8, Active = true },
                new Job { Id = 175, Code = 1175, Title = "مسئول امور کارکنان", JobGroupId = 2, JobGradeId = 9, Active = true },
                new Job { Id = 176, Code = 1176, Title = "کارشناس روابط کار", JobGroupId = 2, JobGradeId = 10, Active = true },
                new Job { Id = 177, Code = 1177, Title = "مسئول حضور و غیاب", JobGroupId = 2, JobGradeId = 7, Active = true },
                new Job { Id = 178, Code = 1178, Title = "اپراتور حضور و غیاب", JobGroupId = 4, JobGradeId = 5, Active = true },
                new Job { Id = 179, Code = 1179, Title = "کارشناس قرارداد کارکنان", JobGroupId = 2, JobGradeId = 9, Active = true },
                new Job { Id = 180, Code = 1180, Title = "کارشناس مزایا و دستمزد", JobGroupId = 2, JobGradeId = 10, Active = true },
                new Job { Id = 181, Code = 1181, Title = "کارشناس منابع انسانی", JobGroupId = 2, JobGradeId = 9, Active = true },

                // Accounting / Treasury
                new Job { Id = 182, Code = 1182, Title = "مدیر حسابداری", JobGroupId = 3, JobGradeId = 15, Active = true },
                new Job { Id = 183, Code = 1183, Title = "مدیر خزانه‌داری", JobGroupId = 3, JobGradeId = 15, Active = true },
                new Job { Id = 184, Code = 1184, Title = "خزانه‌دار", JobGroupId = 3, JobGradeId = 9, Active = true },
                new Job { Id = 185, Code = 1185, Title = "کارشناس خزانه‌داری", JobGroupId = 3, JobGradeId = 9, Active = true },
                new Job { Id = 186, Code = 1186, Title = "صندوقدار", JobGroupId = 3, JobGradeId = 5, Active = true },
                new Job { Id = 187, Code = 1187, Title = "مسئول حسابداری", JobGroupId = 3, JobGradeId = 10, Active = true },
                new Job { Id = 188, Code = 1188, Title = "حسابدار صنعتی", JobGroupId = 3, JobGradeId = 10, Active = true },
                new Job { Id = 189, Code = 1189, Title = "حسابدار حقوق و دستمزد", JobGroupId = 3, JobGradeId = 9, Active = true },
                new Job { Id = 190, Code = 1190, Title = "کارشناس بیمه و مالیات", JobGroupId = 3, JobGradeId = 10, Active = true },
                new Job { Id = 191, Code = 1191, Title = "کارشناس وصول مطالبات", JobGroupId = 3, JobGradeId = 9, Active = true },
                new Job { Id = 192, Code = 1192, Title = "کارشناس اعتبارات", JobGroupId = 3, JobGradeId = 9, Active = true },
                new Job { Id = 193, Code = 1193, Title = "مسئول وصول مطالبات", JobGroupId = 3, JobGradeId = 9, Active = true },
                new Job { Id = 194, Code = 1194, Title = "کارشناس قیمت‌گذاری", JobGroupId = 3, JobGradeId = 9, Active = true },
                new Job { Id = 195, Code = 1195, Title = "کارشناس سرمایه‌گذاری", JobGroupId = 3, JobGradeId = 10, Active = true },
                new Job { Id = 196, Code = 1196, Title = "کارشناس تحلیل مالی", JobGroupId = 3, JobGradeId = 10, Active = true },
                new Job { Id = 197, Code = 1197, Title = "تحلیلگر مالی", JobGroupId = 3, JobGradeId = 10, Active = true },

                // Systems / Organization
                new Job { Id = 198, Code = 1198, Title = "کارشناس سیستم‌ها و روش‌ها", JobGroupId = 1, JobGradeId = 10, Active = true },
                new Job { Id = 199, Code = 1199, Title = "کارشناس توسعه سازمانی", JobGroupId = 2, JobGradeId = 10, Active = true },
                new Job { Id = 200, Code = 1200, Title = "مدیر سیستم‌ها و روش‌ها", JobGroupId = 1, JobGradeId = 15, Active = true }
            );

            builder.Entity<RuleInsuranceGroup>().HasData(
                  new RuleInsuranceGroup
                  {
                      Id = 0,
                      GroupName = ""
                  },
                   new RuleInsuranceGroup
                   {
                       Id = 1,
                       GroupName = "کارمندان"
                   },
                new RuleInsuranceGroup
                {
                    Id = 2,
                    GroupName = "مدیران"
                },
                new RuleInsuranceGroup
                {
                    Id = 3,
                    GroupName = "کارگران"
                }
            );

            builder.Entity<FamilyRelation>().HasData(
                  new FamilyRelation
                  {
                      Id = 0,
                      FamilyRelationName = ""
                  },
                   new FamilyRelation
                   {
                       Id = 1,
                       FamilyRelationName = "فرزند"
                   },
                new FamilyRelation
                {
                    Id = 2,
                    FamilyRelationName = "همسر"
                }
            );

            builder.Entity<MaritalStatus>().HasData(
                  new MaritalStatus
                  {
                      Id = 0,
                      MaritalStatusName = ""
                  },
                   new MaritalStatus
                   {
                       Id = 1,
                       MaritalStatusName = "مجرد"
                   },
                new MaritalStatus
                {
                    Id = 2,
                    MaritalStatusName = "متاهل"
                }
            );

            builder.Entity<Education>().HasData(
                    new Education
                    { Id = 0, EducationName = "" },
                   new Education
                   { Id = 1, EducationName = "زیر دیپلم" },
                    new Education
                    { Id = 2, EducationName = "دیپلم" },
                    new Education
                    { Id = 3, EducationName = "کارشناسی" },
                    new Education
                    { Id = 4, EducationName = "کارشناسی ارشد" },
                    new Education
                    { Id = 5, EducationName = "دکترا" }
            );

            builder.Entity<ContractStatus>().HasData(
                            new ContractStatus { Id = 1, ContractStatusName = "فعال" },
                            new ContractStatus { Id = 2, ContractStatusName = "خاتمه یافته" },
                            new ContractStatus { Id = 3, ContractStatusName = "لغو شده" }
                        );

            builder.Entity<ContractType>().HasData(

                   new ContractType
                   {
                       Id = 1,
                       ContractTypeName = "موقت تمام وقت"
                   },
                    new ContractType
                    {
                        Id = 2,
                        ContractTypeName = "موقت پاره وقت"
                    }
            );

            builder.Entity<PayrollAdjustmentPersonnel>()
                .HasIndex(x => new
                {
                    x.PayrollAdjustmentId,
                    x.PersonnelId
                })
                .IsUnique();

            builder.Entity<PersonnelOrder>()
               .HasOne(p => p.Job)
               .WithMany(j => j.PersonnelOrders)
               .HasForeignKey(p => p.JobId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<PayrollItem>()
                .HasOne(x => x.Payroll)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.PayrollId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<PayrollItem>()
                .HasOne(x => x.SalaryItem)
                .WithMany(x => x.PayrollItems)
                .HasForeignKey(x => x.SalaryItemId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<PayrollItem>()
                .HasOne(x => x.PayrollAdjustment)
                .WithMany(x => x.PayrollItems)
                .HasForeignKey(x => x.PayrollAdjustmentId)
                .OnDelete(DeleteBehavior.Restrict);



            builder.Entity<PayrollAdjustmentPersonnel>()
                .HasOne(x => x.Personnel)
                .WithMany(x => x.PayrollAdjustmentPersonnels)
                .HasForeignKey(x => x.PersonnelId)
                .OnDelete(DeleteBehavior.Restrict);




            builder.Entity<Company>().HasData(
              new Company
              { Id = 1, CompanyName = "Giv", InsuranceCode = "", Address = "تهران - ..." }
            );

            builder.Entity<Parameter>().HasData(

                   new Parameter
                   { Id = 1, Subject = "Insurance", OptionKey = "EmployeePercent", OptionName = "درصد بیمه کارمند", OptionValue = "7" },
                    new Parameter
                    { Id = 2, Subject = "Insurance", OptionKey = "CompanyPercent", OptionName = "درصد بیمه کارفرما", OptionValue = "23" }
            );

            builder.Entity<SalaryItem>(entity =>
            {
                entity.ToTable("SalaryItem", "ref");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Id)
                    .ValueGeneratedNever();

                entity.Property(x => x.Priority)
                    .HasDefaultValue(0);

                entity.Property(x => x.SalaryItemName)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(x => x.Label)
                    .HasMaxLength(100)
                    .HasDefaultValue("");

                entity.Property(x => x.Unit)
                    .HasMaxLength(50)
                    .HasDefaultValue("");

                entity.Property(x => x.Source)
                    .HasMaxLength(50)
                    .HasDefaultValue("");

                entity.Property(x => x.PlusMinus)
                    .HasDefaultValue(0);

                entity.Property(x => x.CalculationMode)
                    .HasMaxLength(20)
                    .HasDefaultValue("");

                entity.Property(x => x.FormulaValue)
                    .HasColumnType("ntext")
                    .HasDefaultValue("");

                entity.Property(x => x.AccAccountCode)
                    .HasMaxLength(50);

                entity.Property(x => x.AccUniqueId);
            });

            builder.Entity<PersonnelOrder>(entity =>
            {
                entity.ToTable("PersonnelOrder", "Payroll");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(x => x.IssueDate)
                    .HasMaxLength(10)
                    .IsRequired();

                entity.Property(x => x.Description)
                    .HasColumnType("ntext");

                entity.Property(x => x.IsActive)
                    .HasDefaultValue(false);


                // Personnel
                entity.HasOne(x => x.Personnel)
                    .WithMany()
                    .HasForeignKey(x => x.PersonnelId)
                    .OnDelete(DeleteBehavior.Restrict);


                // Contract
                entity.HasOne(x => x.Contract)
                    .WithMany()
                    .HasForeignKey(x => x.ContractId)
                    .OnDelete(DeleteBehavior.Restrict);


                // Details
                entity.HasMany(x => x.Details)
                    .WithOne(x => x.PersonnelOrder)
                    .HasForeignKey(x => x.PersonnelOrderId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<PersonnelOrderDetail>()
                 .HasOne(d => d.SalaryItem)
                 .WithMany()
                 .HasForeignKey(d => d.SalaryItemId)
                 .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<PersonnelOrderDetail>()
                .HasOne(d => d.PersonnelOrder)
                .WithMany(o => o.Details)
                .HasForeignKey(d => d.PersonnelOrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<PersonnelOrderDetail>(entity =>
            {
                entity.ToTable("PersonnelOrderDetail", "Payroll");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(x => x.Amount)
                    .HasPrecision(18, 3);


                entity.HasOne(x => x.PersonnelOrder)
                    .WithMany(x => x.Details)
                    .HasForeignKey(x => x.PersonnelOrderId)
                    .OnDelete(DeleteBehavior.Cascade);


                entity.HasOne(x => x.SalaryItem)
                    .WithMany()
                    .HasForeignKey(x => x.SalaryItemId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            SeedHoliday1405(builder);
            SeedSalaryItem(builder);
            builder.Entity<Gender>()
                .ToTable("Gender", "ref");

            builder.Entity<FamilyRelation>()
                .ToTable("FamilyRelation", "ref");

            builder.Entity<Personnel>()
                .HasOne(pf => pf.MaritalStatus)
                .WithMany()
                .HasForeignKey(pf => pf.MaritalStatusId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<PersonnelFamily>()
                .HasOne(pf => pf.Personnel)
                .WithMany(p => p.PersonnelFamilies)
                .HasForeignKey(pf => pf.PersonnelId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<PersonnelFamily>()
                .HasOne(pf => pf.FamilyRelation)
                .WithMany()
                .HasForeignKey(pf => pf.PersonnelRelationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<PersonnelFamily>()
                .HasOne(pf => pf.Gender)
                .WithMany()
                .HasForeignKey(pf => pf.GenderID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<PersonnelFamily>()
                .HasOne(pf => pf.MaritalStatus)
                .WithMany()
                .HasForeignKey(pf => pf.MaritalStatusId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<Attendance>()
      .ToTable("Attendance", "Payroll");

            builder.Entity<Attendance>()
                .HasOne(x => x.Personnel)
                .WithMany()
                .HasForeignKey(x => x.PersonnelId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Attendance>()
                .HasIndex(x => new
                {
                    x.PersonnelId,
                    x.AttendanceDate
                })
                .IsUnique();

        


            builder.Entity<PersonnelContract>()
                .HasOne(x => x.Personnel)
                .WithMany()
                .HasForeignKey(x => x.PersonnelId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<PersonnelContract>()
                .HasOne(x => x.ContractType)
                .WithMany()
                .HasForeignKey(x => x.ContractTypeId)
                .OnDelete(DeleteBehavior.Restrict);

           

            builder.Entity<RuleInsurance>()
               .ToTable("RuleInsurance", "Payroll");
        }

        private void SeedHoliday1405(ModelBuilder builder)
        {
            System.Diagnostics.Debug.Print("holiday seed");

            builder.Entity<Holiday>().HasData(

        // =====================================================
        // 1405 - فروردین
        // =====================================================
        
        new Holiday
        {
            Id = 1001,
            Year = 1405,
            Date = new DateTime(2026, 3, 21),
            Title = "عید سعید فطر و آغاز نوروز",
            IsOfficial = true,
            PersianDate = DateUtil.M2S(new DateTime(2026, 3, 21))
        },

        new Holiday
        {
            Id = 1002,
            Year = 1405,
            Date = new DateTime(2026, 3, 22),
            Title = "عید نوروز و تعطیل به مناسبت عید سعید فطر",
            IsOfficial = true,
            PersianDate = DateUtil.M2S(new DateTime(2026, 3, 22))
        },

        new Holiday
        {
            Id = 1003,
            Year = 1405,
            Date = new DateTime(2026, 3, 23),
            Title = "عید نوروز",
            IsOfficial = true,
            PersianDate = DateUtil.M2S(new DateTime(2026, 3, 23))
        },

        new Holiday
        {
            Id = 1004,
            Year = 1405,
            Date = new DateTime(2026, 3, 24),
            Title = "عید نوروز",
            IsOfficial = true,
            PersianDate = DateUtil.M2S(new DateTime(2026, 3, 24))
        },

        new Holiday
        {
            Id = 1005,
            Year = 1405,
            Date = new DateTime(2026, 4, 1),
            Title = "روز جمهوری اسلامی ایران",
            IsOfficial = true,
            PersianDate = DateUtil.M2S(new DateTime(2026, 4,  1))
        },

        new Holiday
        {
            Id = 1006,
            Year = 1405,
            Date = new DateTime(2026, 4, 2),
            Title = "روز طبیعت",
            IsOfficial = true,
            PersianDate = DateUtil.M2S(new DateTime(2026, 4, 2))
        },

        new Holiday
        {
            Id = 1007,
            Year = 1405,
            Date = new DateTime(2026, 4, 14),
            Title = "شهادت امام جعفر صادق (ع)",
            IsOfficial = true,
            PersianDate = DateUtil.M2S(new DateTime(2026, 4, 14))
        },


        // =====================================================
        // خرداد
        // =====================================================

        new Holiday
        {
            Id = 1008,
            Year = 1405,
            Date = new DateTime(2026, 5, 27),
            Title = "عید سعید قربان",
            IsOfficial = true,
            PersianDate = DateUtil.M2S(new DateTime(2026, 5, 27))
        },

        new Holiday
        {
            Id = 1009,
            Year = 1405,
            Date = new DateTime(2026, 6, 4),
            Title = "رحلت حضرت امام خمینی (ره) و عید سعید غدیر خم",
            IsOfficial = true,
            PersianDate = DateUtil.M2S(new DateTime(2026, 6, 4))
        },

        new Holiday
        {
            Id = 1010,
            Year = 1405,
            Date = new DateTime(2026, 6, 5),
            Title = "قیام ۱۵ خرداد",
            IsOfficial = true,
            PersianDate = DateUtil.M2S(new DateTime(2026, 6,5))
        },


        // =====================================================
        // تیر
        // =====================================================

        new Holiday
        {
            Id = 1011,
            Year = 1405,
            Date = new DateTime(2026, 6, 24),
            Title = "تاسوعای حسینی",
            IsOfficial = true,
            PersianDate = DateUtil.M2S(new DateTime(2026,6, 24))
        },

        new Holiday
        {
            Id = 1012,
            Year = 1405,
            Date = new DateTime(2026, 6, 25),
            Title = "عاشورای حسینی",
            IsOfficial = true,
            PersianDate = DateUtil.M2S(new DateTime(2026, 6, 25))
        },


        // =====================================================
        // مرداد
        // =====================================================

        new Holiday
        {
            Id = 1013,
            Year = 1405,
            Date = new DateTime(2026, 8, 4),
            Title = "اربعین حسینی",
            IsOfficial = true,
            PersianDate = DateUtil.M2S(new DateTime(2026, 8, 4))
        },

        new Holiday
        {
            Id = 1014,
            Year = 1405,
            Date = new DateTime(2026, 8, 12),
            Title = "رحلت پیامبر اکرم (ص) و شهادت امام حسن مجتبی (ع)",
            IsOfficial = true,
            PersianDate = DateUtil.M2S(new DateTime(2026, 8, 12))
        },

        new Holiday
        {
            Id = 1015,
            Year = 1405,
            Date = new DateTime(2026, 8, 13),
            Title = "شهادت امام رضا (ع)",
            IsOfficial = true,
            PersianDate = DateUtil.M2S(new DateTime(2026, 8, 13))
        },

        new Holiday
        {
            Id = 1016,
            Year = 1405,
            Date = new DateTime(2026, 8, 21),
            Title = "شهادت امام حسن عسکری (ع)",
            IsOfficial = true,
            PersianDate = DateUtil.M2S(new DateTime(2026, 8, 21))
        },


        // =====================================================
        // شهریور
        // =====================================================

        new Holiday
        {
            Id = 1017,
            Year = 1405,
            Date = new DateTime(2026, 8, 30),
            Title = "میلاد پیامبر اکرم (ص) و میلاد امام جعفر صادق (ع)",
            IsOfficial = true,
            PersianDate = DateUtil.M2S(new DateTime(2026, 8, 30))
        },


        // =====================================================
        // آبان
        // =====================================================

        new Holiday
        {
            Id = 1018,
            Year = 1405,
            Date = new DateTime(2026, 11, 13),
            Title = "شهادت حضرت فاطمه زهرا (س)",
            IsOfficial = true,
            PersianDate = DateUtil.M2S(new DateTime(2026, 11, 13))
        },


        // =====================================================
        // دی
        // =====================================================

        new Holiday
        {
            Id = 1019,
            Year = 1405,
            Date = new DateTime(2026, 12, 23),
            Title = "ولادت حضرت علی (ع) و روز پدر",
            IsOfficial = true,
            PersianDate = DateUtil.M2S(new DateTime(2026, 12, 23))
        },

        new Holiday
        {
            Id = 1020,
            Year = 1405,
            Date = new DateTime(2027, 1, 6),
            Title = "مبعث حضرت رسول اکرم (ص)",
            IsOfficial = true,
            PersianDate = DateUtil.M2S(new DateTime(2027, 1 , 6))
        },


        // =====================================================
        // بهمن
        // =====================================================

        new Holiday
        {
            Id = 1021,
            Year = 1405,
            Date = new DateTime(2027, 1, 24),
            Title = "ولادت حضرت قائم (عج) و جشن نیمه شعبان",
            IsOfficial = true,
            PersianDate = DateUtil.M2S(new DateTime(2027, 1, 24))
        },

        new Holiday
        {
            Id = 1022,
            Year = 1405,
            Date = new DateTime(2027, 2, 11),
            Title = "پیروزی انقلاب اسلامی ایران",
            IsOfficial = true,
            PersianDate = DateUtil.M2S(new DateTime(2027, 2, 11))
        },


        // =====================================================
        // اسفند
        // =====================================================

        new Holiday
        {
            Id = 1023,
            Year = 1405,
            Date = new DateTime(2027, 2, 28),
            Title = "شهادت حضرت علی (ع)",
            IsOfficial = true,
            PersianDate = DateUtil.M2S(new DateTime(2027, 2, 28))
        },

        new Holiday
        {
            Id = 1024,
            Year = 1405,
            Date = new DateTime(2027, 3, 10),
            Title = "عید سعید فطر",
            IsOfficial = true,
            PersianDate = DateUtil.M2S(new DateTime(2027, 3, 10))
        },

        new Holiday
        {
            Id = 1025,
            Year = 1405,
            Date = new DateTime(2027, 3, 11),
            Title = "تعطیل به مناسبت عید سعید فطر",
            IsOfficial = true,
            PersianDate = DateUtil.M2S(new DateTime(2027, 3, 11))
        },

        new Holiday
        {
            Id = 1026,
            Year = 1405,
            Date = new DateTime(2027, 3, 20),
            Title = "روز ملی شدن صنعت نفت ایران",
            IsOfficial = true,
            PersianDate = DateUtil.M2S(new DateTime(2027, 3, 20))
        }
    );
        }

        private void SeedSalaryItem(ModelBuilder builder)
        {
            builder.Entity<SalaryItem>().HasData(
     new SalaryItem
     {
         Id = 1,
         Priority = 10,
         SalaryItemName = "حقوق پایه",
         Label = "BaseSalary",
         Unit = "Amount",
         Source = "PersonnelOrder",
         PlusMinus = 1,
         CalculationMode = "Formula",
         FormulaValue = "BaseSalary * DaysWorked"
     },

     new SalaryItem
     {
         Id = 2,
         Priority = 70,
         SalaryItemName = "اضافه کار",
         Label = "OverTime",
         Unit = "Hour",
         Source = "Attendence",
         PlusMinus = 1,
         CalculationMode = "Formula",
         FormulaValue = "(BaseSalary * MonthDays/220)*1.2/60*ExtraMinute"
     },

     new SalaryItem
     {
         Id = 3,
         Priority = 50,
         SalaryItemName = "حق مسکن",
         Label = "HousingAllowance",
         Unit = "Amount",
         Source = "PersonnelOrder",
         PlusMinus = 1,
         CalculationMode = "Formula",
         FormulaValue = "(HousingAllowance / MonthDays) * DaysWorked"
     },

     new SalaryItem
     {
         Id = 4,
         Priority = 60,
         SalaryItemName = "حق اولاد",
         Label = "ChildAllowance",
         Unit = "Amount",
         Source = "PersonnelOrder",
         PlusMinus = 1,
         CalculationMode = "Formula",
         FormulaValue = "(ChildAllowance * ChildNo / MonthDays) * DaysWorked"
     },

     new SalaryItem
     {
         Id = 5,
         Priority = 110,
         SalaryItemName = "پاداش",
         Label = "Bonus",
         Unit = "Amount",
         Source = "PayrollAdjustment",
         PlusMinus = 1,
         CalculationMode = "Manual",
         FormulaValue = ""
     },

     new SalaryItem
     {
         Id = 6,
         Priority = 100,
         SalaryItemName = "ماموریت",
         Label = "MissionAllownce",
         Unit = "Day",
         Source = "Attendence",
         PlusMinus = 1,
         CalculationMode = "Formula",
         FormulaValue = "(BaseSalary * MonthDays/220)*2/60*MissionMinute"
     },

     new SalaryItem
     {
         Id = 7,
         Priority = 80,
         SalaryItemName = "کسرکار",
         Label = "AbsenceDeduction",
         Unit = "Hour",
         Source = "Attendence",
         PlusMinus = -1,
         CalculationMode = "Formula",
         FormulaValue = "(BaseSalary * MonthDays/220)*2/60*AbsenceMinute"
     },

     new SalaryItem
     {
         Id = 8,
         Priority = 90,
         SalaryItemName = "تاخیر",
         Label = "DelayDeduction",
         Unit = "Hour",
         Source = "Attendence",
         PlusMinus = -1,
         CalculationMode = "Formula",
         FormulaValue = "(BaseSalary * MonthDays/220)*2/60*DelayMinute"
     },

     new SalaryItem
     {
         Id = 9,
         Priority = 130,
         SalaryItemName = "مالیات",
         Label = "Tax",
         Unit = "Percent",
         Source = "RuleTax",
         PlusMinus = -1,
         CalculationMode = "Tax",
         FormulaValue = ""
     },

     new SalaryItem
     {
         Id = 10,
         Priority = 120,
         SalaryItemName = "بیمه سهم کارمند",
         Label = "Insurance",
         Unit = "Percent",
         Source = "RuleInsurance",
         PlusMinus = -1,
         CalculationMode = "Insurance",
         FormulaValue = ""
     },

     //new SalaryItem
     //{
     //    Id = 11,
     //    Priority = 140,
     //    SalaryItemName = "قسط وام",
     //    Label = "LoanInstallment",
     //    Unit = "Amount",
     //    Source = "PersonnelInstallment",
     //    PlusMinus = -1,
     //    CalculationMode = "Manual",
     //    FormulaValue = ""
     //},

     new SalaryItem
     {
         Id = 12,
         Priority = 40,
         SalaryItemName = "بن و خواربار",
         Label = "FoodAllowance",
         Unit = "Amount",
         Source = "PersonnelOrder",
         PlusMinus = 1,
         CalculationMode = "Formula",
         FormulaValue = "(FoodAllowance / MonthDays) * DaysWorked"
     },

     new SalaryItem
     {
         Id = 13,
         Priority = 30,
         SalaryItemName = "فوق العاده شغل",
         Label = "JobAllowance",
         Unit = "Amount",
         Source = "PersonnelOrder",
         PlusMinus = 1,
         CalculationMode = "Formula",
         FormulaValue = "(JobAllowance / MonthDays) * DaysWorked"
     },

     new SalaryItem
     {
         Id = 14,
         Priority = 20,
         SalaryItemName = "حق مسئولیت",
         Label = "ResponsibilityAllowance",
         Unit = "Amount",
         Source = "PersonnelOrder",
         PlusMinus = 1,
         CalculationMode = "Formula",
         FormulaValue = "(ResponsibilityAllowance / MonthDays) * DaysWorked"
     },

     new SalaryItem
     {
         Id = 15,
         Priority = 111,
         SalaryItemName = "جریمه",
         Label = "Penalty",
         Unit = "Amount",
         Source = "PayrollAdjustment",
         PlusMinus = -1,
         CalculationMode = "Manual",
         FormulaValue = ""
     }

 );
        }
    }
}