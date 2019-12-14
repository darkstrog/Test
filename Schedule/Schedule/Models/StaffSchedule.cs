using System;
using System.Collections.Generic;

namespace Schedule
{
    public class StaffSchedule
    {
        public string Name { get; set; }
        public IEnumerable<Zns> Zns { get; set; }
        public StaffSchedule()
        {
            Zns = new List<Zns>(13);
        }
    }
    public class Zns
    {
        public List<string> ZnIds { get; set; }
        public DateTime? Time { get; set; }
        public int Delta { get; set; }
        public string color { get; set; }
        public Zns()
        {
            ZnIds = new List<string>();
            Delta = 1;
            color = "white";
        }
    }
}
