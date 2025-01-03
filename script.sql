USE [master]
GO
/****** Object:  Database [Qltaphoan]    Script Date: 02/12/2024 10:03:02 CH ******/
CREATE DATABASE [Qltaphoan]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Qltaphoan', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS07\MSSQL\DATA\Qltaphoan.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Qltaphoan_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS07\MSSQL\DATA\Qltaphoan_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Qltaphoan] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Qltaphoan].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Qltaphoan] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Qltaphoan] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Qltaphoan] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Qltaphoan] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Qltaphoan] SET ARITHABORT OFF 
GO
ALTER DATABASE [Qltaphoan] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Qltaphoan] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Qltaphoan] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Qltaphoan] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Qltaphoan] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Qltaphoan] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Qltaphoan] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Qltaphoan] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Qltaphoan] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Qltaphoan] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Qltaphoan] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Qltaphoan] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Qltaphoan] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Qltaphoan] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Qltaphoan] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Qltaphoan] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Qltaphoan] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Qltaphoan] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Qltaphoan] SET  MULTI_USER 
GO
ALTER DATABASE [Qltaphoan] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Qltaphoan] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Qltaphoan] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Qltaphoan] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Qltaphoan] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Qltaphoan] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Qltaphoan] SET QUERY_STORE = ON
GO
ALTER DATABASE [Qltaphoan] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Qltaphoan]
GO
/****** Object:  UserDefinedFunction [dbo].[CalculateFinalPrice]    Script Date: 02/12/2024 10:03:02 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*Function tính giá sản phẩm theo số lượng*/
CREATE FUNCTION [dbo].[CalculateFinalPrice]
(
    @UnitPrice DECIMAL(18,2),
    @Quantity INT
)
RETURNS DECIMAL(18,2)
AS
BEGIN
    RETURN @UnitPrice * @Quantity;
END

GO
/****** Object:  Table [dbo].[Products]    Script Date: 02/12/2024 10:03:02 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [varchar](255) NOT NULL,
	[UnitPrice] [money] NOT NULL,
	[QuantityInStock] [int] NOT NULL,
	[Mavach] [nvarchar](20) NOT NULL,
	[SupplierID] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK__Products__B40CC6ED1C39F84C] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ProductInfoView]    Script Date: 02/12/2024 10:03:02 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* View View */
CREATE VIEW [dbo].[ProductInfoView] AS
SELECT ProductID, ProductName, UnitPrice, QuantityInStock
FROM Products;
GO
/****** Object:  Table [dbo].[Account]    Script Date: 02/12/2024 10:03:02 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[AccountID] [int] IDENTITY(1,1) NOT NULL,
	[AccountName] [varchar](255) NOT NULL,
	[AccountGmail] [text] NULL,
	[Password] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[AccountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Inventory]    Script Date: 02/12/2024 10:03:02 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inventory](
	[InventoryID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NULL,
	[QuantityIn] [int] NULL,
	[QuantityOut] [int] NULL,
	[Date] [datetime] NULL,
	[InventoryName] [ntext] NULL,
PRIMARY KEY CLUSTERED 
(
	[InventoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 02/12/2024 10:03:02 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[OrderDetailID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NULL,
	[ProductID] [int] NULL,
	[Quantity] [int] NOT NULL,
	[UnitPrice] [decimal](10, 2) NOT NULL,
	[DiscountCode] [varchar](50) NULL,
	[DiscountAmount] [decimal](10, 2) NULL,
	[FinalPrice] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 02/12/2024 10:03:02 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[Status] [varchar](50) NULL,
	[TotalAmount] [decimal](10, 2) NOT NULL,
	[OrderDate] [datetime] NULL,
	[MaHoaDon] [varchar](50) NULL,
	[PaymentMethod] [nvarchar](250) NULL,
 CONSTRAINT [PK__Orders__C3905BAFA769A053] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payments]    Script Date: 02/12/2024 10:03:02 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payments](
	[PaymentID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NULL,
	[PaymentDate] [datetime] NULL,
	[Amount] [decimal](10, 2) NOT NULL,
	[PaymentMethod] [varchar](50) NULL,
	[DiscountCode] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[PaymentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseOrderDetails]    Script Date: 02/12/2024 10:03:02 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseOrderDetails](
	[PurchaseOrderDetailID] [int] NOT NULL,
	[PurchaseOrderID] [int] NULL,
	[ProductID] [int] NULL,
	[Quantity] [int] NULL,
	[PurchasePrice] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[PurchaseOrderDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseOrders]    Script Date: 02/12/2024 10:03:02 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseOrders](
	[PurchaseOrderID] [int] NOT NULL,
	[OrderDate] [datetime] NULL,
	[TotalAmount] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[PurchaseOrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Suppliers]    Script Date: 02/12/2024 10:03:02 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Suppliers](
	[SupplierName] [nvarchar](100) NULL,
	[ContactInfo] [nvarchar](255) NULL,
	[SupplierID] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Suppliers_1] PRIMARY KEY CLUSTERED 
(
	[SupplierID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Inventory] ADD  DEFAULT (getdate()) FOR [Date]
GO
ALTER TABLE [dbo].[Payments] ADD  DEFAULT (getdate()) FOR [PaymentDate]
GO
ALTER TABLE [dbo].[Inventory]  WITH CHECK ADD  CONSTRAINT [FK__Inventory__Produ__49C3F6B7] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
GO
ALTER TABLE [dbo].[Inventory] CHECK CONSTRAINT [FK__Inventory__Produ__49C3F6B7]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK__OrderDeta__Order__44FF419A] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Orders] ([OrderID])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK__OrderDeta__Order__44FF419A]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK__OrderDeta__Produ__45F365D3] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK__OrderDeta__Produ__45F365D3]
GO
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD  CONSTRAINT [FK__Payments__OrderI__4D94879B] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Orders] ([OrderID])
GO
ALTER TABLE [dbo].[Payments] CHECK CONSTRAINT [FK__Payments__OrderI__4D94879B]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Suppliers] FOREIGN KEY([SupplierID])
REFERENCES [dbo].[Suppliers] ([SupplierID])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Suppliers]
GO
ALTER TABLE [dbo].[PurchaseOrderDetails]  WITH CHECK ADD  CONSTRAINT [FK__PurchaseO__Produ__5441852A] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
GO
ALTER TABLE [dbo].[PurchaseOrderDetails] CHECK CONSTRAINT [FK__PurchaseO__Produ__5441852A]
GO
ALTER TABLE [dbo].[PurchaseOrderDetails]  WITH CHECK ADD FOREIGN KEY([PurchaseOrderID])
REFERENCES [dbo].[PurchaseOrders] ([PurchaseOrderID])
GO
/****** Object:  StoredProcedure [dbo].[GetMonthlyProductRevenue]    Script Date: 02/12/2024 10:03:02 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* Procedure doanh thu theo tháng */
CREATE PROCEDURE [dbo].[GetMonthlyProductRevenue]
    @SelectedMonth INT,
    @SelectedYear INT
AS
BEGIN
    SELECT 
        p.ProductName, 
        SUM(od.Quantity) AS TotalQuantity, 
        SUM(od.FinalPrice) AS TotalRevenue
    FROM 
        OrderDetails od
    JOIN 
        Orders o ON od.OrderID = o.OrderID
    JOIN 
        Products p ON od.ProductID = p.ProductID
    WHERE 
        MONTH(o.OrderDate) = @SelectedMonth
        AND YEAR(o.OrderDate) = @SelectedYear
    GROUP BY 
        p.ProductName
END
GO
/****** Object:  StoredProcedure [dbo].[GetOrdersByDate]    Script Date: 02/12/2024 10:03:02 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetOrdersByDate]
    @SelectedDate DATE
AS
BEGIN
    SELECT * FROM Orders 
    WHERE CAST(OrderDate AS DATE) = @SelectedDate
END
SELECT p.ProductName, SUM(od.Quantity) AS TotalQuantity, SUM(od.FinalPrice) AS TotalRevenue 
                                   FROM OrderDetails od  
                                   JOIN Orders o ON od.OrderID = o.OrderID 
                                   JOIN Products p ON od.ProductID = p.ProductID 
                                   WHERE o.OrderDate = @SelectedDate 
                                   GROUP BY p.ProductName;
GO
USE [master]
GO
ALTER DATABASE [Qltaphoan] SET  READ_WRITE 
GO
