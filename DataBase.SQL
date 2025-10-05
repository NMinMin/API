--============================================== TẠO DATABASE ============================================
use master
if exists(select * from sys.databases where name = 'QuanLySinhVien')
    drop database QuanLySinhVien
go
create database QuanLySinhVien
on primary
(
    name = QuanLySinhVien_data,
    filename = 'E:\BackEnd\DB\QuanLySinhVien.mdf',
    size = 50MB,
    filegrowth = 10MB,
    maxsize = 200MB
)
log on
(
    name = QuanLySinhVien_log,
    filename = 'E:\BackEnd\DB\QuanLySinhVien.ldf',
    size = 10MB,
    filegrowth = 5MB,
    maxsize = unlimited
)

--============================================ TẠO BẢNG =================================
go 
use QuanLySinhVien
--Bảng lớp
create table Class
(
	id int primary key identity(1,1),
	name nvarchar(50) unique not null
)
--Bảng sinh viên
create table Student
(
	id int primary key identity(1,1),
	name nvarchar(100) not null unique,
	dateofbirth date check (dateofbirth < getdate()),
	class_id int foreign key (class_id) references Class (id)
)