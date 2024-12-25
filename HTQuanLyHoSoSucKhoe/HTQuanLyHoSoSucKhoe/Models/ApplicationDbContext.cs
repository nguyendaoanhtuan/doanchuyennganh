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
    public DbSet<BacSi> BacSis { get; set; }
    public DbSet<LoaiPhieu> LoaiPhieus { get; set; }

    public DbSet<LoaiDichVuKham> LoaiDichVuThamKhams { get; set; }

    public DbSet<LoaiDichVuChuyenKhoa> LoaiDichVuChuyenKhoa { get; set; }

    public DbSet<DonThuoc> DonThuocs { get; set; }

    public DbSet<PhieuChiDinh> PhieuChiDinhs { get; set; }

    public DbSet<PhieuKetQua> PhieuKetQuas { get; set; }

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
        modelBuilder.Entity<BacSi>().ToTable("bac_sis");
        modelBuilder.Entity<DonThuoc>().ToTable("don_thuocs");
        modelBuilder.Entity<PhieuChiDinh>().ToTable("phieu_chi_dinhs");
        modelBuilder.Entity<LoaiDichVuKham>().ToTable("loai_dich_vu_khams");
        modelBuilder.Entity<LoaiDichVuChuyenKhoa>().ToTable("loai_dich_vu_chuyen_khoas");
        modelBuilder.Entity<LoaiPhieu>().ToTable("loai_phieus");
        modelBuilder.Entity<PhieuKetQua>().ToTable("phieu_ket_quas");


        // Mối quan hệ giữa BacSi và PhieuChiDinh
        modelBuilder.Entity<BacSi>()
          .HasMany(cd => cd.PhieuChiDinhs)
          .WithOne(bs => bs.BacSi)
          .HasForeignKey(bs => bs.bacSiId);
        modelBuilder.Entity<PhieuChiDinh>()
           .HasOne(bv => bv.BacSi)
           .WithMany(bs => bs.PhieuChiDinhs)
           .HasForeignKey(bs => bs.bacSiId);

        // Mối quan hệ giữa Appointment và PhieuChiDinh
        modelBuilder.Entity<Appointment>()
           .HasMany(cd => cd.PhieuChiDinhs)
           .WithOne(bs => bs.Appointment)
           .HasForeignKey(bs => bs.appointmentId);
        modelBuilder.Entity<PhieuChiDinh>()
           .HasOne(bv => bv.Appointment)
           .WithMany(bs => bs.PhieuChiDinhs)
           .HasForeignKey(bs => bs.appointmentId);

        // Mối quan hệ giữa PhieuChiDinh và PhieuKetQua
        modelBuilder.Entity<PhieuChiDinh>()
           .HasMany(cd => cd.PhieuKetQuas)
           .WithOne(bs => bs.PhieuChiDinh)
           .HasForeignKey(bs => bs.phieuChiDinhId);
        modelBuilder.Entity<PhieuKetQua>()
           .HasOne(bv => bv.Appointment)
           .WithMany(bs => bs.PhieuKetQuas)
           .HasForeignKey(bs => bs.phieuChiDinhId);

        // Mối quan hệ giữa Appointment và PhieuKetQua
        modelBuilder.Entity<Appointment>()
           .HasMany(cd => cd.PhieuKetQuas)
           .WithOne(bs => bs.Appointment)
           .HasForeignKey(bs => bs.appointmentId);
        modelBuilder.Entity<PhieuKetQua>()
           .HasOne(bv => bv.Appointment)
           .WithMany(bs => bs.PhieuKetQuas)
           .HasForeignKey(bs => bs.appointmentId);

        // Mối quan hệ giữa BacSi và PhieuKetQua
        modelBuilder.Entity<BacSi>()
           .HasMany(cd => cd.PhieuKetQuas)
           .WithOne(bs => bs.BacSi)
           .HasForeignKey(bs => bs.bacSiId);
        modelBuilder.Entity<PhieuKetQua>()
           .HasOne(bv => bv.BacSi)
           .WithMany(bs => bs.PhieuKetQuas)
           .HasForeignKey(bs => bs.bacSiId);

        // Mối quan hệ giữa ChuyenKhoa và PhieuKetQua
        modelBuilder.Entity<ChuyenKhoa>()
           .HasMany(cd => cd.PhieuKetQuas)
           .WithOne(bs => bs.ChuyenKhoa)
           .HasForeignKey(bs => bs.chuyenKhoaId);
        modelBuilder.Entity<PhieuKetQua>()
           .HasOne(bv => bv.ChuyenKhoa)
           .WithMany(bs => bs.PhieuKetQuas)
           .HasForeignKey(bs => bs.chuyenKhoaId);

        // Mối quan hệ giữa HoSoBenhAn và PhieuKetQua
        modelBuilder.Entity<HoSoBenhAn>()
           .HasMany(cd => cd.PhieuKetQuas)
           .WithOne(bs => bs.HoSoBenhAn)
           .HasForeignKey(bs => bs.hoSoBenhAnId);
        modelBuilder.Entity<PhieuKetQua>()
           .HasOne(bv => bv.HoSoBenhAn)
           .WithMany(bs => bs.PhieuKetQuas)
           .HasForeignKey(bs => bs.hoSoBenhAnId);

        // Mối quan hệ giữa HoSoBenhAn và DonThuoc
        modelBuilder.Entity<BacSi>()
           .HasMany(cd => cd.DonThuocs)
           .WithOne(bs => bs.BacSi)
           .HasForeignKey(bs => bs.bacSiId);
        modelBuilder.Entity<DonThuoc>()
           .HasOne(bv => bv.BacSi)
           .WithMany(bs => bs.DonThuocs)
           .HasForeignKey(bs => bs.bacSiId);

        // Mối quan hệ giữa PhieuChiDinh và ChuyenKhoa
        modelBuilder.Entity<ChuyenKhoa>()
           .HasMany(cd => cd.PhieuChiDinhs)
           .WithOne(bs => bs.ChuyenKhoa)
           .HasForeignKey(bs => bs.chuyenKhoaId);
        modelBuilder.Entity<PhieuChiDinh>()
           .HasOne(bv => bv.ChuyenKhoa)
           .WithMany(bs => bs.PhieuChiDinhs)
           .HasForeignKey(bs => bs.chuyenKhoaId);

        // Mối quan hệ giữa HoSoBenhAn và DonThuoc
        modelBuilder.Entity<HoSoBenhAn>()
           .HasMany(cd => cd.DonThuocs)
           .WithOne(bs => bs.HoSoBenhAn)
           .HasForeignKey(bs => bs.hoSoBenhAnId);
        modelBuilder.Entity<DonThuoc>()
           .HasOne(bv => bv.HoSoBenhAn)
           .WithMany(bs => bs.DonThuocs)
           .HasForeignKey(bs => bs.hoSoBenhAnId);

        // Mối quan hệ giữa HoSoBenhAn và BacSi
        modelBuilder.Entity<BacSi>()
           .HasMany(cd => cd.HoSoBenhAns)
           .WithOne(bs => bs.BacSi)
           .HasForeignKey(bs => bs.bacSiId);
        modelBuilder.Entity<HoSoBenhAn>()
           .HasOne(bv => bv.BacSi)
           .WithMany(bs => bs.HoSoBenhAns)
           .HasForeignKey(bs => bs.bacSiId);

        // Mối quan hệ giữa HoSoBenhAn và ChuyenKhoa
        modelBuilder.Entity<ChuyenKhoa>()
           .HasMany(cd => cd.HoSoBenhAns)
           .WithOne(bs => bs.ChuyenKhoa)
           .HasForeignKey(bs => bs.chuyenKhoaId);
        modelBuilder.Entity<HoSoBenhAn>()
           .HasOne(bv => bv.ChuyenKhoa)
           .WithMany(bs => bs.HoSoBenhAns)
           .HasForeignKey(bs => bs.chuyenKhoaId);

        // Mối quan hệ giữa HoSoBenhAn và PhieuChiDinh
        modelBuilder.Entity<HoSoBenhAn>()
           .HasMany(cd => cd.PhieuChiDinhs)
           .WithOne(bs => bs.HoSoBenhAn)
           .HasForeignKey(bs => bs.HoSoBenhAnId);
        modelBuilder.Entity<PhieuChiDinh>()
           .HasOne(bv => bv.HoSoBenhAn)
           .WithMany(bs => bs.PhieuChiDinhs)
           .HasForeignKey(bs => bs.HoSoBenhAnId);
      
        // Mối quan hệ giữa LoaiDichVuChuyenKhoa và LoaiDichVuKham
        modelBuilder.Entity<LoaiDichVuChuyenKhoa>()
            .HasOne(ldv => ldv.LoaiDichVuKham)  // Mỗi LoaiDichVuChuyenKhoa có một LoaiDichVuKham
            .WithMany(ldvK => ldvK.LoaiDichVuChuyenKhoas)  // Mỗi LoaiDichVuKham có thể có nhiều LoaiDichVuChuyenKhoa
            .HasForeignKey(ldv => ldv.LoaiDichVuId);  // Chỉ định khóa ngoại
        // Mối quan hệ giữa ChuyenKhoa và LoaiDichVuChuyenKhoa
        modelBuilder.Entity<LoaiDichVuChuyenKhoa>()
            .HasOne(ldv => ldv.ChuyenKhoa)  // Mỗi LoaiDichVuChuyenKhoa có một ChuyenKhoa
            .WithMany(c => c.LoaiDichVuChuyenKhoas)  // Mỗi ChuyenKhoa có nhiều LoaiDichVuChuyenKhoa
            .HasForeignKey(ldv => ldv.ChuyenKhoaId);  // Chỉ định khóa ngoại
        modelBuilder.Entity<Appointment>()
           .HasOne(a => a.ChuyenKhoa) // Một Appointment có một ChuyenKhoa
           .WithMany(c => c.Appointments) // Một ChuyenKhoa có nhiều Appointment
           .HasForeignKey(a => a.ChuyenKhoaId); // Chỉ định khóa ngoại trong Appointment
        // Appointment - LoaiDichVuKham: 1:N
        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.LoaiDichVuKham) // Mỗi Appointment có một LoaiDichVuKham
            .WithMany(ldv => ldv.Appointments) // Một LoaiDichVuKham có nhiều Appointment
            .HasForeignKey(a => a.LoaiDichVuId); // Khóa ngoại LoaiDichVuId trong Appointment

        // Cấu hình quan hệ BenhVien - ChuyenKhoa (1-nhiều)
        modelBuilder.Entity<BenhVien>()
            .HasMany(bv => bv.ChuyenKhoas)
            .WithOne(ck => ck.BenhVien)
            .HasForeignKey(ck => ck.BenhVienId)
            .OnDelete(DeleteBehavior.NoAction);

        // Cấu hình quan hệ BenhVien - BacSi (1-nhiều)
        modelBuilder.Entity<BenhVien>()
            .HasMany(bv => bv.BacSis)
            .WithOne(bs => bs.BenhVien)
            .HasForeignKey(bs => bs.BenhVienId)
            .OnDelete(DeleteBehavior.Cascade);

        // Cấu hình quan hệ ChuyenKhoa - BacSi (1-nhiều)
        modelBuilder.Entity<ChuyenKhoa>()
            .HasMany(ck => ck.BacSis)
            .WithOne(bs => bs.ChuyenKhoa)
            .HasForeignKey(bs => bs.ChuyenKhoaId)
            .OnDelete(DeleteBehavior.Cascade);

        // Ràng buộc UserId và BenhVienId là duy nhất trong TaiKhoan
        modelBuilder.Entity<TaiKhoan>()
            .HasIndex(t => t.UserId)
            .IsUnique();
        modelBuilder.Entity<TaiKhoan>()
            .HasIndex(t => t.BenhVienId)
            .IsUnique();
        // Cấu hình mối quan hệ một-một
        modelBuilder.Entity<TaiKhoan>()
            .HasOne(t => t.ChuyenKhoa)
            .WithOne(c => c.TaiKhoan)
            .HasForeignKey<TaiKhoan>(t => t.ChuyenKhoaId);

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
            .WithMany(u => u.HoSoBenhAns)
            .HasForeignKey(h => h.UserId)
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

        // Cấu hình mối quan hệ Role - ChuyenKhoa (1-nhiều)
        modelBuilder.Entity<Role>()
            .HasMany(r => r.ChuyenKhoas)
            .WithOne(ck => ck.Role)
            .HasForeignKey(ck => ck.RoleId)
            .OnDelete(DeleteBehavior.Cascade); // Xóa Role thì xóa các ChuyenKhoa liên quan
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
