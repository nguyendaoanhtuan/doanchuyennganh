﻿// <auto-generated />
using System;
using HTQuanLyHoSoSucKhoe.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HTQuanLyHoSoSucKhoe.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HTQuanLyHoSoSucKhoe.Models.Appointment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Appointment_Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("BenhVienId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created_At")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone_Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenBenhVien")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Updated_At")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BenhVienId");

                    b.HasIndex("UserId");

                    b.ToTable("appointments", (string)null);
                });

            modelBuilder.Entity("HTQuanLyHoSoSucKhoe.Models.BenhVien", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ChuyenKhoa")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("Created_At")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image_Path")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Phone_Number")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Updated_At")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("benh_vien", (string)null);
                });

            modelBuilder.Entity("HTQuanLyHoSoSucKhoe.Models.ChuyenKhoa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BenhVienId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("BenhVienId");

                    b.ToTable("chuyen_khoa", (string)null);
                });

            modelBuilder.Entity("HTQuanLyHoSoSucKhoe.Models.HoSoBenhAn", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BenhVienId")
                        .HasColumnType("int");

                    b.Property<string>("GhiChu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("chanDoan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("hinhAnh")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ngayCapNhat")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ngayTao")
                        .HasColumnType("datetime2");

                    b.Property<string>("thuocDuocKe")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("trieuChung")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BenhVienId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("ho_so_benh_an", (string)null);
                });

            modelBuilder.Entity("HTQuanLyHoSoSucKhoe.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Vaitro")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("roles", (string)null);
                });

            modelBuilder.Entity("HTQuanLyHoSoSucKhoe.Models.TaiKhoan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BenhVienId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("passWord")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BenhVienId")
                        .IsUnique()
                        .HasFilter("[BenhVienId] IS NOT NULL");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasFilter("[UserId] IS NOT NULL");

                    b.ToTable("tai_khoan", (string)null);
                });

            modelBuilder.Entity("HTQuanLyHoSoSucKhoe.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Cccd")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created_At")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Ho")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone_Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Updated_At")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("Email", "Phone_Number")
                        .IsUnique();

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("HTQuanLyHoSoSucKhoe.Models.Appointment", b =>
                {
                    b.HasOne("HTQuanLyHoSoSucKhoe.Models.BenhVien", "BenhVien")
                        .WithMany("Appointments")
                        .HasForeignKey("BenhVienId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("HTQuanLyHoSoSucKhoe.Models.User", "User")
                        .WithMany("Appointments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("BenhVien");

                    b.Navigation("User");
                });

            modelBuilder.Entity("HTQuanLyHoSoSucKhoe.Models.BenhVien", b =>
                {
                    b.HasOne("HTQuanLyHoSoSucKhoe.Models.Role", "Role")
                        .WithMany("BenhViens")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("HTQuanLyHoSoSucKhoe.Models.ChuyenKhoa", b =>
                {
                    b.HasOne("HTQuanLyHoSoSucKhoe.Models.BenhVien", "BenhVien")
                        .WithMany("ChuyenKhoas")
                        .HasForeignKey("BenhVienId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("BenhVien");
                });

            modelBuilder.Entity("HTQuanLyHoSoSucKhoe.Models.HoSoBenhAn", b =>
                {
                    b.HasOne("HTQuanLyHoSoSucKhoe.Models.BenhVien", "BenhVien")
                        .WithMany("HoSoBenhAns")
                        .HasForeignKey("BenhVienId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("HTQuanLyHoSoSucKhoe.Models.User", "User")
                        .WithOne("HoSoBenhAn")
                        .HasForeignKey("HTQuanLyHoSoSucKhoe.Models.HoSoBenhAn", "UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("BenhVien");

                    b.Navigation("User");
                });

            modelBuilder.Entity("HTQuanLyHoSoSucKhoe.Models.TaiKhoan", b =>
                {
                    b.HasOne("HTQuanLyHoSoSucKhoe.Models.BenhVien", "BenhVien")
                        .WithOne("TaiKhoan")
                        .HasForeignKey("HTQuanLyHoSoSucKhoe.Models.TaiKhoan", "BenhVienId");

                    b.HasOne("HTQuanLyHoSoSucKhoe.Models.User", "User")
                        .WithOne("TaiKhoan")
                        .HasForeignKey("HTQuanLyHoSoSucKhoe.Models.TaiKhoan", "UserId");

                    b.Navigation("BenhVien");

                    b.Navigation("User");
                });

            modelBuilder.Entity("HTQuanLyHoSoSucKhoe.Models.User", b =>
                {
                    b.HasOne("HTQuanLyHoSoSucKhoe.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("HTQuanLyHoSoSucKhoe.Models.BenhVien", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("ChuyenKhoas");

                    b.Navigation("HoSoBenhAns");

                    b.Navigation("TaiKhoan")
                        .IsRequired();
                });

            modelBuilder.Entity("HTQuanLyHoSoSucKhoe.Models.Role", b =>
                {
                    b.Navigation("BenhViens");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("HTQuanLyHoSoSucKhoe.Models.User", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("HoSoBenhAn")
                        .IsRequired();

                    b.Navigation("TaiKhoan")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
