namespace HTQuanLyHoSoSucKhoe.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<BenhVien> BenhVien { get; set; }

    public DbSet<Appointment> Appointments{ get; set; }

    public DbSet<ChuyenKhoa> ChuyenKhoas{ get; set; }

    public DbSet<TKBenhVien> TKBenhVien{ get; set; }

    public DbSet<Role> Roles{ get; set; }
    // Cấu hình bảng, ánh xạ cột và khóa
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Cấu hình mối quan hệ User - Appointment
        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.User)
            .WithMany()  // Nếu một User có nhiều Appointment
            .OnDelete(DeleteBehavior.Cascade); // Hành động xóa

        // Cấu hình mối quan hệ BenhVien - Appointment
        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.BenhVien)
            .WithMany()  // Nếu một BenhVien có nhiều Appointment
            .OnDelete(DeleteBehavior.Cascade); // Hành động xóa

        modelBuilder.Entity<ChuyenKhoa>()
            .HasOne(a => a.BenhVien)
            .WithMany()  // Nếu một BenhVien có nhiều ChuyenKhoa
            .OnDelete(DeleteBehavior.Cascade); // Hành động xóa

        // Tạo chỉ mục cho PhoneNumber và đảm bảo tính duy nhất
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Phone_Number)
            .IsUnique();

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
           .HasIndex(u => u.Email)
           .IsUnique();

        base.OnModelCreating(modelBuilder);

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasOne(u => u.Role)  // Một User có một Role
            .WithMany(r => r.Users)  // Một Role có nhiều User
            .HasForeignKey(u => u.RoleId)  // Khóa ngoại RoleId
            .OnDelete(DeleteBehavior.Cascade);  // Xóa cascade khi xóa User

        modelBuilder.Entity<Appointment>().ToTable("appointments");
    }
    
}