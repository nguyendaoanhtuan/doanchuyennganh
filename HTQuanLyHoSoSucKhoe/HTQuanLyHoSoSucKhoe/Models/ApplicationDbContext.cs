namespace HTQuanLyHoSoSucKhoe.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    // Định nghĩa DbSet cho các thực thể
    public DbSet<User> Users { get; set; }
    public DbSet<BenhVien> BenhVien { get; set; }
    public DbSet<HoSoBenhAn> HoSoBenhAns { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<ChuyenKhoa> ChuyenKhoas { get; set; }
    public DbSet<TaiKhoan> TaiKhoans { get; set; }
    public DbSet<Role> Roles { get; set; }

    // Cấu hình bảng, ánh xạ cột và khóa
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Tên bảng chuẩn hóa
        modelBuilder.Entity<User>().ToTable("users");
        modelBuilder.Entity<BenhVien>().ToTable("benh_vien");
        modelBuilder.Entity<HoSoBenhAn>().ToTable("ho_so_benh_an");
        modelBuilder.Entity<Appointment>().ToTable("appointments");
        modelBuilder.Entity<ChuyenKhoa>().ToTable("chuyen_khoa");
        modelBuilder.Entity<TaiKhoan>().ToTable("tai_khoan");
        modelBuilder.Entity<Role>().ToTable("roles");

        // Ràng buộc UserId và BenhVienId là duy nhất trong TaiKhoan
        modelBuilder.Entity<TaiKhoan>()
            .HasIndex(t => t.UserId)
            .IsUnique();
        modelBuilder.Entity<TaiKhoan>()
            .HasIndex(t => t.BenhVienId)
            .IsUnique();

        // Cấu hình mối quan hệ một-một
        modelBuilder.Entity<TaiKhoan>()
            .HasOne(t => t.User)
            .WithOne(u => u.TaiKhoan)
            .HasForeignKey<TaiKhoan>(t => t.UserId);

        modelBuilder.Entity<TaiKhoan>()
            .HasOne(t => t.BenhVien)
            .WithOne(bv => bv.TaiKhoan)
            .HasForeignKey<TaiKhoan>(t => t.BenhVienId);

        // Cấu hình mối quan hệ User - HoSoBenhAn
        modelBuilder.Entity<HoSoBenhAn>()
            .HasOne(h => h.User)
            .WithOne(u => u.HoSoBenhAn)
            .HasForeignKey<HoSoBenhAn>(h => h.UserId)
            .OnDelete(DeleteBehavior.NoAction); // Thay đổi từ Cascade thành NoAction

        // Cấu hình mối quan hệ BenhVien - HoSoBenhAn
        modelBuilder.Entity<HoSoBenhAn>()
            .HasOne(h => h.BenhVien)
            .WithMany(bv => bv.HoSoBenhAns)
            .HasForeignKey(h => h.BenhVienId)
            .OnDelete(DeleteBehavior.NoAction); // Thay đổi từ Cascade thành NoAction

        // Cấu hình mối quan hệ User - Appointment (Many-to-One)
        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.User)  // Một Appointment chỉ thuộc về một User
            .WithMany(u => u.Appointments)  // Một User có thể có nhiều Appointment
            .HasForeignKey(a => a.UserId)  // Khóa ngoại từ Appointment đến User
            .OnDelete(DeleteBehavior.NoAction); // Không thực hiện Cascade khi User bị xóa

        // Cấu hình mối quan hệ BenhVien - Appointment (Many-to-One)
        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.BenhVien)  // Một Appointment chỉ thuộc về một BenhVien
            .WithMany(bv => bv.Appointments)  // Một BenhVien có thể có nhiều Appointment
            .HasForeignKey(a => a.BenhVienId)  // Khóa ngoại từ Appointment đến BenhVien
            .OnDelete(DeleteBehavior.NoAction); // Không thực hiện Cascade khi BenhVien bị xóa

        // Cấu hình mối quan hệ BenhVien - ChuyenKhoa
        modelBuilder.Entity<ChuyenKhoa>()
            .HasOne(ck => ck.BenhVien)
            .WithMany(bv => bv.ChuyenKhoas)
            .HasForeignKey(ck => ck.BenhVienId)
            .OnDelete(DeleteBehavior.NoAction); // Thay đổi từ Cascade thành NoAction

        // Cấu hình mối quan hệ User - Role
        modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

        // Cấu hình mối quan hệ BenhVien - Role
        modelBuilder.Entity<BenhVien>()
            .HasOne(bv => bv.Role)
            .WithMany(r => r.BenhViens)
            .HasForeignKey(bv => bv.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

        // Tạo chỉ mục cho Email và Phone_Number
        modelBuilder.Entity<User>()
            .HasIndex(u => new { u.Email, u.Phone_Number })
            .IsUnique();

        base.OnModelCreating(modelBuilder);
    }
}
