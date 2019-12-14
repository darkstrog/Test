using System;
using System.Collections.Generic;

namespace Schedule
{
    public partial class Zn
    {
        public Zn()
        {
            ZnStaffTime = new HashSet<ZnStaffTime>();
        }

        public int Recordid { get; set; }
        public DateTime? Dataopen { get; set; }
        public int? Typezn { get; set; }
        public DateTime? Todofrom { get; set; }
        public DateTime? Todotill { get; set; }

        public virtual ICollection<ZnStaffTime> ZnStaffTime { get; set; }
    }
}
