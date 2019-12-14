using System;
using System.Collections.Generic;

namespace Schedule
{
    public partial class Staff
    {
        public Staff()
        {
            ZnStaffTime = new HashSet<ZnStaffTime>();
        }

        public int Recordid { get; set; }
        public string Name { get; set; }
        public bool ShowInTimePlaner { get; set; }
        public byte Holidays { get; set; }
        public int StaffOrder { get; set; }
        public int? Parentid { get; set; }
        public int SalaryType { get; set; }

        public virtual ICollection<ZnStaffTime> ZnStaffTime { get; set; }
    }
}
