using Microsoft.EntityFrameworkCore;

namespace Schedule
{
    public partial class ScheduleContext : DbContext
    {
        public ScheduleContext()
        {
        }

        public ScheduleContext(DbContextOptions<ScheduleContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<Zn> Zn { get; set; }
        public virtual DbSet<ZnStaffTime> ZnStaffTime { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.HasKey(e => e.Recordid);

                entity.ToTable("staff");

                entity.Property(e => e.Recordid)
                    .HasColumnName("recordid")
                    .ValueGeneratedNever();

                entity.Property(e => e.Holidays).HasColumnName("holidays");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Parentid).HasColumnName("parentid");

                entity.Property(e => e.SalaryType).HasColumnName("salary_type");

                entity.Property(e => e.ShowInTimePlaner).HasColumnName("show_in_time_planer");

                entity.Property(e => e.StaffOrder).HasColumnName("staff_order");
            });

            modelBuilder.Entity<Zn>(entity =>
            {
                entity.HasKey(e => e.Recordid)
                    .HasName("PK_zn_temp");

                entity.ToTable("zn");

                entity.Property(e => e.Recordid).HasColumnName("recordid");

                entity.Property(e => e.Dataopen)
                    .HasColumnName("dataopen")
                    .HasColumnType("datetime");

                entity.Property(e => e.Todofrom)
                    .HasColumnName("todofrom")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.Todotill)
                    .HasColumnName("todotill")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.Typezn).HasColumnName("typezn");
            });

            modelBuilder.Entity<ZnStaffTime>(entity =>
            {
                entity.HasKey(e => e.Recordid);

                entity.ToTable("zn_staff_time");

                entity.Property(e => e.Recordid).HasColumnName("recordid");

                entity.Property(e => e.AssignDate)
                    .HasColumnName("assign_date")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.StaffId).HasColumnName("staff_id");

                entity.Property(e => e.UnassignDate)
                    .HasColumnName("unassign_date")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.ZnId).HasColumnName("zn_id");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.ZnStaffTime)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_zn_staff_time_staff");

                entity.HasOne(d => d.Zn)
                    .WithMany(p => p.ZnStaffTime)
                    .HasForeignKey(d => d.ZnId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_zn_staff_time_zn");
            });
        }
    }
}
