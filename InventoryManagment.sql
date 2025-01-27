USE [master]
GO
/****** Object:  Database [InventoryManagment]    Script Date: 01.10.2024 14:40:05 ******/
CREATE DATABASE [InventoryManagment]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'InventoryManagment', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\InventoryManagment.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'InventoryManagment_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\InventoryManagment_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [InventoryManagment] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [InventoryManagment].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [InventoryManagment] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [InventoryManagment] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [InventoryManagment] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [InventoryManagment] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [InventoryManagment] SET ARITHABORT OFF 
GO
ALTER DATABASE [InventoryManagment] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [InventoryManagment] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [InventoryManagment] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [InventoryManagment] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [InventoryManagment] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [InventoryManagment] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [InventoryManagment] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [InventoryManagment] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [InventoryManagment] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [InventoryManagment] SET  DISABLE_BROKER 
GO
ALTER DATABASE [InventoryManagment] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [InventoryManagment] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [InventoryManagment] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [InventoryManagment] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [InventoryManagment] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [InventoryManagment] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [InventoryManagment] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [InventoryManagment] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [InventoryManagment] SET  MULTI_USER 
GO
ALTER DATABASE [InventoryManagment] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [InventoryManagment] SET DB_CHAINING OFF 
GO
ALTER DATABASE [InventoryManagment] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [InventoryManagment] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [InventoryManagment] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [InventoryManagment] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [InventoryManagment] SET QUERY_STORE = OFF
GO
USE [InventoryManagment]
GO
/****** Object:  Table [dbo].[Audits]    Script Date: 01.10.2024 14:40:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Audits](
	[IdAudit] [int] IDENTITY(1,1) NOT NULL,
	[AuditDate] [date] NOT NULL,
	[PerformedByEmployeeID] [int] NOT NULL,
	[Notes] [text] NULL,
	[Discrepancies] [text] NULL,
 CONSTRAINT [PK_Audits] PRIMARY KEY CLUSTERED 
(
	[IdAudit] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BudgetAllocations]    Script Date: 01.10.2024 14:40:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BudgetAllocations](
	[IDBudget] [int] IDENTITY(1,1) NOT NULL,
	[AllocationDate] [nchar](10) NOT NULL,
	[DepartmentID] [int] NOT NULL,
	[Amount] [nchar](10) NOT NULL,
	[Purpose] [nchar](10) NOT NULL,
 CONSTRAINT [PK_BudgetAllocations] PRIMARY KEY CLUSTERED 
(
	[IDBudget] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 01.10.2024 14:40:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[IdCategories] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](50) NOT NULL,
	[Description] [text] NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[IdCategories] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Departments]    Script Date: 01.10.2024 14:40:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departments](
	[IdDepartments] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](50) NOT NULL,
	[HeadOfDepartment] [nchar](75) NULL,
 CONSTRAINT [PK_Departments] PRIMARY KEY CLUSTERED 
(
	[IdDepartments] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 01.10.2024 14:40:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[IdEmployee] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nchar](50) NOT NULL,
	[LastName] [nchar](50) NOT NULL,
	[Patronymic] [nchar](50) NULL,
	[Position] [nchar](50) NULL,
	[DepartmentID] [int] NOT NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[IdEmployee] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Equipments]    Script Date: 01.10.2024 14:40:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Equipments](
	[IdEquipment] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Serial_Number] [nvarchar](50) NOT NULL,
	[CategoryID] [int] NOT NULL,
	[DepartmentID] [int] NOT NULL,
	[LocationID] [int] NOT NULL,
	[StatusID] [int] NOT NULL,
	[PurchaseDate] [datetime] NOT NULL,
	[WarrantyExpiration] [date] NULL,
	[SupplierID] [int] NULL,
	[Cost] [money] NULL,
 CONSTRAINT [PK_Equipment] PRIMARY KEY CLUSTERED 
(
	[IdEquipment] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EquipmentStatus]    Script Date: 01.10.2024 14:40:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EquipmentStatus](
	[IdStatus] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](50) NOT NULL,
	[Description] [text] NULL,
 CONSTRAINT [PK_EquipmentStatus] PRIMARY KEY CLUSTERED 
(
	[IdStatus] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InventoryMovements]    Script Date: 01.10.2024 14:40:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InventoryMovements](
	[IdMovement] [int] IDENTITY(1,1) NOT NULL,
	[EquipmentID] [int] NOT NULL,
	[FromLocationID] [int] NOT NULL,
	[ToLocationID] [int] NOT NULL,
	[MovementDate] [datetime] NOT NULL,
	[MovedByEmployeeID] [int] NOT NULL,
	[ReceivedByEmployeeID] [int] NULL,
 CONSTRAINT [PK_InventoryMovements] PRIMARY KEY CLUSTERED 
(
	[IdMovement] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Locations]    Script Date: 01.10.2024 14:40:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Locations](
	[IdLocations] [int] IDENTITY(1,1) NOT NULL,
	[Description] [text] NULL,
	[DepartmentID] [int] NOT NULL,
 CONSTRAINT [PK_Locations] PRIMARY KEY CLUSTERED 
(
	[IdLocations] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MaintenanceRecords]    Script Date: 01.10.2024 14:40:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaintenanceRecords](
	[IdMaintenance] [int] IDENTITY(1,1) NOT NULL,
	[EquipmentID] [int] NOT NULL,
	[MaintensnceDate] [datetime] NOT NULL,
	[PerformedByEmployeeID] [int] NULL,
	[MaintenanceType] [nchar](50) NOT NULL,
	[Cost] [money] NULL,
 CONSTRAINT [PK_MaintenanceRecords] PRIMARY KEY CLUSTERED 
(
	[IdMaintenance] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 01.10.2024 14:40:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[IdOrder] [int] IDENTITY(1,1) NOT NULL,
	[OrderDate] [date] NOT NULL,
	[SupplierID] [int] NOT NULL,
	[Status] [nchar](50) NOT NULL,
	[TotalCost] [money] NULL,
	[OrderItems] [text] NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[IdOrder] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Students]    Script Date: 01.10.2024 14:40:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Students](
	[IdStudents] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nchar](50) NOT NULL,
	[LastName] [nchar](50) NOT NULL,
	[Patronymic] [nchar](50) NULL,
	[StudentIDСard] [nchar](50) NOT NULL,
	[DepartmentID] [int] NOT NULL,
	[Course] [nchar](50) NOT NULL,
 CONSTRAINT [PK_Students] PRIMARY KEY CLUSTERED 
(
	[IdStudents] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Suppliers]    Script Date: 01.10.2024 14:40:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Suppliers](
	[IdSuppliers] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](50) NOT NULL,
	[ContactInfo] [text] NULL,
	[Adress] [nchar](150) NULL,
	[Email] [nchar](50) NULL,
 CONSTRAINT [PK_Suppliers] PRIMARY KEY CLUSTERED 
(
	[IdSuppliers] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UtilizationRecords]    Script Date: 01.10.2024 14:40:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UtilizationRecords](
	[IdUtilization] [int] IDENTITY(1,1) NOT NULL,
	[EquipmentID] [int] NOT NULL,
	[StudentID] [int] NULL,
	[EmployeeID] [int] NULL,
	[UsageStart] [datetime] NOT NULL,
	[UsageEnd] [datetime] NULL,
	[Purpose] [nchar](50) NULL,
 CONSTRAINT [PK_UtilizationRecords] PRIMARY KEY CLUSTERED 
(
	[IdUtilization] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WarrantyClaims]    Script Date: 01.10.2024 14:40:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WarrantyClaims](
	[IdWarranty] [int] IDENTITY(1,1) NOT NULL,
	[EquipmentID] [int] NOT NULL,
	[ClaimDate] [date] NOT NULL,
	[IssueDescription] [text] NOT NULL,
	[Status] [nchar](50) NOT NULL,
	[ResolvedDate] [date] NULL,
 CONSTRAINT [PK_WarrantyClaims] PRIMARY KEY CLUSTERED 
(
	[IdWarranty] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Audits]  WITH CHECK ADD  CONSTRAINT [FK_Audits_Employees] FOREIGN KEY([PerformedByEmployeeID])
REFERENCES [dbo].[Employees] ([IdEmployee])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Audits] CHECK CONSTRAINT [FK_Audits_Employees]
GO
ALTER TABLE [dbo].[BudgetAllocations]  WITH CHECK ADD  CONSTRAINT [FK_BudgetAllocations_Departments] FOREIGN KEY([DepartmentID])
REFERENCES [dbo].[Departments] ([IdDepartments])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BudgetAllocations] CHECK CONSTRAINT [FK_BudgetAllocations_Departments]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Departments] FOREIGN KEY([DepartmentID])
REFERENCES [dbo].[Departments] ([IdDepartments])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Departments]
GO
ALTER TABLE [dbo].[Equipments]  WITH CHECK ADD  CONSTRAINT [FK_Equipment_Categories] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Categories] ([IdCategories])
GO
ALTER TABLE [dbo].[Equipments] CHECK CONSTRAINT [FK_Equipment_Categories]
GO
ALTER TABLE [dbo].[Equipments]  WITH CHECK ADD  CONSTRAINT [FK_Equipment_Departments] FOREIGN KEY([DepartmentID])
REFERENCES [dbo].[Departments] ([IdDepartments])
GO
ALTER TABLE [dbo].[Equipments] CHECK CONSTRAINT [FK_Equipment_Departments]
GO
ALTER TABLE [dbo].[Equipments]  WITH CHECK ADD  CONSTRAINT [FK_Equipment_EquipmentStatus] FOREIGN KEY([StatusID])
REFERENCES [dbo].[EquipmentStatus] ([IdStatus])
GO
ALTER TABLE [dbo].[Equipments] CHECK CONSTRAINT [FK_Equipment_EquipmentStatus]
GO
ALTER TABLE [dbo].[Equipments]  WITH CHECK ADD  CONSTRAINT [FK_Equipment_Locations] FOREIGN KEY([LocationID])
REFERENCES [dbo].[Locations] ([IdLocations])
GO
ALTER TABLE [dbo].[Equipments] CHECK CONSTRAINT [FK_Equipment_Locations]
GO
ALTER TABLE [dbo].[Equipments]  WITH CHECK ADD  CONSTRAINT [FK_Equipment_Suppliers] FOREIGN KEY([SupplierID])
REFERENCES [dbo].[Suppliers] ([IdSuppliers])
GO
ALTER TABLE [dbo].[Equipments] CHECK CONSTRAINT [FK_Equipment_Suppliers]
GO
ALTER TABLE [dbo].[InventoryMovements]  WITH CHECK ADD  CONSTRAINT [FK_InventoryMovements_Employees] FOREIGN KEY([MovedByEmployeeID])
REFERENCES [dbo].[Employees] ([IdEmployee])
GO
ALTER TABLE [dbo].[InventoryMovements] CHECK CONSTRAINT [FK_InventoryMovements_Employees]
GO
ALTER TABLE [dbo].[InventoryMovements]  WITH CHECK ADD  CONSTRAINT [FK_InventoryMovements_Employees1] FOREIGN KEY([ReceivedByEmployeeID])
REFERENCES [dbo].[Employees] ([IdEmployee])
GO
ALTER TABLE [dbo].[InventoryMovements] CHECK CONSTRAINT [FK_InventoryMovements_Employees1]
GO
ALTER TABLE [dbo].[InventoryMovements]  WITH CHECK ADD  CONSTRAINT [FK_InventoryMovements_Equipment] FOREIGN KEY([EquipmentID])
REFERENCES [dbo].[Equipments] ([IdEquipment])
GO
ALTER TABLE [dbo].[InventoryMovements] CHECK CONSTRAINT [FK_InventoryMovements_Equipment]
GO
ALTER TABLE [dbo].[InventoryMovements]  WITH CHECK ADD  CONSTRAINT [FK_InventoryMovements_Locations] FOREIGN KEY([FromLocationID])
REFERENCES [dbo].[Locations] ([IdLocations])
GO
ALTER TABLE [dbo].[InventoryMovements] CHECK CONSTRAINT [FK_InventoryMovements_Locations]
GO
ALTER TABLE [dbo].[InventoryMovements]  WITH CHECK ADD  CONSTRAINT [FK_InventoryMovements_Locations1] FOREIGN KEY([ToLocationID])
REFERENCES [dbo].[Locations] ([IdLocations])
GO
ALTER TABLE [dbo].[InventoryMovements] CHECK CONSTRAINT [FK_InventoryMovements_Locations1]
GO
ALTER TABLE [dbo].[Locations]  WITH CHECK ADD  CONSTRAINT [FK_Locations_Departments] FOREIGN KEY([DepartmentID])
REFERENCES [dbo].[Departments] ([IdDepartments])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Locations] CHECK CONSTRAINT [FK_Locations_Departments]
GO
ALTER TABLE [dbo].[MaintenanceRecords]  WITH CHECK ADD  CONSTRAINT [FK_MaintenanceRecords_Employees] FOREIGN KEY([PerformedByEmployeeID])
REFERENCES [dbo].[Employees] ([IdEmployee])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MaintenanceRecords] CHECK CONSTRAINT [FK_MaintenanceRecords_Employees]
GO
ALTER TABLE [dbo].[MaintenanceRecords]  WITH CHECK ADD  CONSTRAINT [FK_MaintenanceRecords_Equipment] FOREIGN KEY([EquipmentID])
REFERENCES [dbo].[Equipments] ([IdEquipment])
GO
ALTER TABLE [dbo].[MaintenanceRecords] CHECK CONSTRAINT [FK_MaintenanceRecords_Equipment]
GO
ALTER TABLE [dbo].[MaintenanceRecords]  WITH CHECK ADD  CONSTRAINT [FK_MaintenanceRecords_Equipment1] FOREIGN KEY([EquipmentID])
REFERENCES [dbo].[Equipments] ([IdEquipment])
GO
ALTER TABLE [dbo].[MaintenanceRecords] CHECK CONSTRAINT [FK_MaintenanceRecords_Equipment1]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Suppliers] FOREIGN KEY([SupplierID])
REFERENCES [dbo].[Suppliers] ([IdSuppliers])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Suppliers]
GO
ALTER TABLE [dbo].[Students]  WITH CHECK ADD  CONSTRAINT [FK_Students_Departments] FOREIGN KEY([DepartmentID])
REFERENCES [dbo].[Departments] ([IdDepartments])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Students] CHECK CONSTRAINT [FK_Students_Departments]
GO
ALTER TABLE [dbo].[UtilizationRecords]  WITH CHECK ADD  CONSTRAINT [FK_UtilizationRecords_Employees] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employees] ([IdEmployee])
GO
ALTER TABLE [dbo].[UtilizationRecords] CHECK CONSTRAINT [FK_UtilizationRecords_Employees]
GO
ALTER TABLE [dbo].[UtilizationRecords]  WITH CHECK ADD  CONSTRAINT [FK_UtilizationRecords_Equipment] FOREIGN KEY([EquipmentID])
REFERENCES [dbo].[Equipments] ([IdEquipment])
GO
ALTER TABLE [dbo].[UtilizationRecords] CHECK CONSTRAINT [FK_UtilizationRecords_Equipment]
GO
ALTER TABLE [dbo].[UtilizationRecords]  WITH CHECK ADD  CONSTRAINT [FK_UtilizationRecords_Students] FOREIGN KEY([StudentID])
REFERENCES [dbo].[Students] ([IdStudents])
GO
ALTER TABLE [dbo].[UtilizationRecords] CHECK CONSTRAINT [FK_UtilizationRecords_Students]
GO
ALTER TABLE [dbo].[WarrantyClaims]  WITH CHECK ADD  CONSTRAINT [FK_WarrantyClaims_Equipment] FOREIGN KEY([EquipmentID])
REFERENCES [dbo].[Equipments] ([IdEquipment])
GO
ALTER TABLE [dbo].[WarrantyClaims] CHECK CONSTRAINT [FK_WarrantyClaims_Equipment]
GO
USE [master]
GO
ALTER DATABASE [InventoryManagment] SET  READ_WRITE 
GO
