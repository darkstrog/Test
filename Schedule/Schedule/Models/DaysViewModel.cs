using System;
using System.Collections.Generic;

namespace Schedule.Models
{
    public class DaysViewModel
    {
        public DateTime Date { get; set; }
        public IEnumerable<StaffSchedule> StaffSchedules { get; set; }
    }
}
