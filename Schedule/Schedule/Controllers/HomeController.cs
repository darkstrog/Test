using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Schedule.Models;

namespace Schedule.Controllers
{
    public class HomeController : Controller
    {
        ScheduleContext db;
        public HomeController(ScheduleContext context)
        {
            db = context;
        }
        public async Task<ActionResult> Index()
        {
            var model = await GetDaysAsync();
            return View(model);
        }
        
        public async Task<IEnumerable<DaysViewModel>> GetDaysAsync()
        {
            List<DaysViewModel> result=new List<DaysViewModel>();
            var staffs = await db.Staff.Include(st => st.ZnStaffTime).ThenInclude(zn => zn.Zn).ToListAsync();
            var days = db.Zn.Select(zd => zd.Todofrom).OrderBy(z => z).ToList();
            var sorted = days.Select(i => i.Value.Date).Distinct();
            foreach (var day in sorted)
            {
                bool IsEmpty = true;
                List<StaffSchedule> staffSchedules = new List<StaffSchedule>();
                foreach (var staff in staffs)
                {
                    Lazy <StaffSchedule> ss = new Lazy<StaffSchedule>();
                    List<Zns> znsList = new List<Zns>(13);
                    for (int i = 0; i < 13; i++) { znsList.Add(new Zns()); }
                    var ZnList = staff.ZnStaffTime.Where(z=>z.Zn.Todofrom.Value.Date.Day == day.Day).ToList();
                    if (ZnList.Count() > 0)
                    {
                        IsEmpty = false;
                        foreach (var zst in ZnList)
                        {
                            Zns znStaff = znsList[zst.Zn.Todofrom.Value.Hour - 8];
                            znStaff.ZnIds.Add(zst.Recordid.ToString());
                            znStaff.Time = zst.Zn.Todofrom;
                            znStaff.Delta = zst.Zn.Todotill.Value.Hour - zst.Zn.Todofrom.Value.Hour + 1;
                            if (znStaff.Delta > 1) { znsList.RemoveRange(znStaff.Time.Value.Hour - 7, znStaff.Delta-1); }
                            znStaff.color = "deepskyblue";

                        }
                    }
                    ss.Value.Name = staff.Name;
                    ss.Value.Zns = znsList;
                    staffSchedules.Add(ss.Value);
                }
                if (!IsEmpty)result.Add(new DaysViewModel { Date = (DateTime)day, StaffSchedules = staffSchedules });
            }
            return result;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
