using System;
using System.Collections.Generic;

namespace Schedule
{
    public partial class ZnStaffTime
    {
        public int Recordid { get; set; }
        public int ZnId { get; set; }
        public int StaffId { get; set; }
        public DateTime AssignDate { get; set; }
        public DateTime? UnassignDate { get; set; }

        public virtual Staff Staff { get; set; }
        public virtual Zn Zn { get; set; }
    }
}
