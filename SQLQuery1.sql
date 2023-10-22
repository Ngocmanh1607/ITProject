-- Tao Bang Mon Hoc --
Create Table MonHoc
 (
   MaMH char(5) primary key,
   TenMH nvarchar(30) not null,
   SoTC int not null check ( (SoTC>0)and (SoTC<5) )
 )
 --- Tao Bang He Dao Tao ---
Create Table HeDT
 (
   MaHeDT char(5) primary key,
   TenHeDT nvarchar(40) not null
 )

 --- Tao Bang Khoa --
Create Table Khoa
 (
   MaKhoa char(5) primary key,
   TenKhoa nvarchar(30) not null,
   MaHeDT char(5) not null foreign key references HeDT(MaHeDT),
   SL int not null,
   DienThoai varchar(20) not null
 )
 --tao bang nganh
 Create Table Nganh
 (
   MaNganh char(20) primary key,
   TenNganh  char(30) not null,
   MaKhoa char(5) not null foreign key references Khoa(MaKhoa),
   SL int not null
 )
 -- Tao Bang Lop ---
Create Table Lop
 (
   MaLop char(5) primary key,
   TenLop nvarchar(30) not null,
   MaKhoa char(5) foreign key references Khoa (MaKhoa),
   MaHeDT char(5) foreign key references HeDT (MaHeDT),
   Soluong int,
   MaNH char(5)
 )
 --- Tao Bang Sinh Vien ---
Create Table SinhVien
 (
   MaSV char(15) primary key,
   TenSV nvarchar(20) ,
   GioiTinh nvarchar(20) ,
   NgaySinh datetime ,
   QueQuan nvarchar(50) ,
   MaLop char(5) foreign key references Lop(MaLop)
 )
 --- Tao Bang Diem ---
Create Table Diem
 (
   MaSV char(15) foreign key references SinhVien(MaSV),
   MaMH char(5) foreign key references MonHoc (MaMH),
   HocKy int check(HocKy>0) not null,
   DiemGk float,
   DiemCk float,
   DiemTong float,
   KetQua bit
)
ALTER TABLE Nganh
ADD MaHeDT char(5) foreign key references HeDT (MaHeDT)
ALTER TABLE Lop
ADD MaNganh char(20) foreign key references Nganh(MaNganh)