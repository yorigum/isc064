/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2017 (14.0.2002)
    Source Database Engine Edition : Microsoft SQL Server Express Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2017
    Target Database Engine Edition : Microsoft SQL Server Standard Edition
    Target Database Engine Type : Standalone SQL Server
*/
USE [master]
GO
/****** Object:  Database [ISC064_FINANCEAR]    Script Date: 05/04/2019 15.49.42 ******/
CREATE DATABASE [ISC064_FINANCEAR]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ISC064_FINANCEAR', FILENAME = N'E:\ISC064\db\ISC064_FINANCEAR.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ISC064_FINANCEAR_log', FILENAME = N'E:\ISC064\db\ISC064_FINANCEAR_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [ISC064_FINANCEAR] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ISC064_FINANCEAR].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ISC064_FINANCEAR] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ISC064_FINANCEAR] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ISC064_FINANCEAR] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ISC064_FINANCEAR] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ISC064_FINANCEAR] SET ARITHABORT OFF 
GO
ALTER DATABASE [ISC064_FINANCEAR] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [ISC064_FINANCEAR] SET AUTO_SHRINK ON 
GO
ALTER DATABASE [ISC064_FINANCEAR] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ISC064_FINANCEAR] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ISC064_FINANCEAR] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ISC064_FINANCEAR] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ISC064_FINANCEAR] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ISC064_FINANCEAR] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ISC064_FINANCEAR] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ISC064_FINANCEAR] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ISC064_FINANCEAR] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ISC064_FINANCEAR] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ISC064_FINANCEAR] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ISC064_FINANCEAR] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ISC064_FINANCEAR] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ISC064_FINANCEAR] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ISC064_FINANCEAR] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ISC064_FINANCEAR] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ISC064_FINANCEAR] SET  MULTI_USER 
GO
ALTER DATABASE [ISC064_FINANCEAR] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ISC064_FINANCEAR] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ISC064_FINANCEAR] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ISC064_FINANCEAR] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ISC064_FINANCEAR] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ISC064_FINANCEAR] SET QUERY_STORE = OFF
GO
USE [ISC064_FINANCEAR]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [ISC064_FINANCEAR]
GO
/****** Object:  User [batavianet]    Script Date: 05/04/2019 15.49.42 ******/
CREATE USER [batavianet] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[batavianet]
GO
ALTER ROLE [db_datareader] ADD MEMBER [batavianet]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [batavianet]
GO
/****** Object:  Schema [batavianet]    Script Date: 05/04/2019 15.49.43 ******/
CREATE SCHEMA [batavianet]
GO
/****** Object:  Table [dbo].[LapPDF]    Script Date: 05/04/2019 15.49.43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LapPDF](
	[AttachmentID] [int] IDENTITY(1,1) NOT NULL,
	[Nama] [varchar](100) NOT NULL,
	[DefaultWP] [bit] NOT NULL,
	[Link] [varchar](max) NOT NULL,
	[TglGenerate] [datetime] NOT NULL,
	[IP] [varchar](50) NULL,
	[UserID] [varchar](20) NOT NULL,
	[FileName] [varchar](100) NOT NULL,
	[FileType] [varchar](100) NOT NULL,
	[FileSize] [decimal](18, 2) NULL,
	[FilterDari] [datetime] NULL,
	[FilterSampai] [datetime] NULL,
	[AccDari] [varchar](50) NULL,
	[AccSampai] [varchar](50) NULL,
	[PeriodeBln] [varchar](50) NULL,
	[PeriodeThn] [varchar](50) NULL,
	[SN] [int] NULL,
 CONSTRAINT [PK_LapPDF] PRIMARY KEY CLUSTERED 
(
	[AttachmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_ANONIM]    Script Date: 05/04/2019 15.49.43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_ANONIM](
	[NoAnonim] [int] NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Bank] [varchar](100) NOT NULL,
	[Nilai] [money] NOT NULL,
	[Status] [varchar](20) NOT NULL,
	[Unit] [varchar](100) NOT NULL,
	[Customer] [varchar](100) NOT NULL,
	[Ket] [varchar](200) NOT NULL,
	[AccountID] [varchar](20) NOT NULL,
	[BKMID] [varchar](88) NOT NULL,
	[FOBO] [bit] NOT NULL,
	[TglPostingJurnal] [datetime] NOT NULL,
	[JurnalID] [varchar](88) NOT NULL,
	[Project] [varchar](20) NOT NULL,
	[NamaProject] [varchar](50) NOT NULL,
	[Rekening] [varchar](50) NOT NULL,
	[NoKontrak] [varchar](50) NOT NULL,
 CONSTRAINT [PK_MS_ANONIM] PRIMARY KEY CLUSTERED 
(
	[NoAnonim] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_ANONIM_LOG]    Script Date: 05/04/2019 15.49.43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_ANONIM_LOG](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Aktivitas] [varchar](6) NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Ket] [text] NOT NULL,
	[Pk] [varchar](50) NOT NULL,
	[Approve] [varchar](50) NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_MS_ANONIM_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_CASHBACK]    Script Date: 05/04/2019 15.49.44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_CASHBACK](
	[Nocb] [int] NOT NULL,
	[NoKontrak] [varchar](20) NOT NULL,
	[NoUrut] [int] NOT NULL,
	[Unit] [varchar](50) NOT NULL,
	[Customer] [varchar](100) NOT NULL,
	[TglPengembalian] [datetime] NOT NULL,
	[SisaTagihan] [money] NOT NULL,
	[LebihBayar] [money] NOT NULL,
	[Cashback] [money] NOT NULL,
	[BankKeluar] [money] NOT NULL,
	[Bank] [varchar](50) NOT NULL,
	[AdminBank] [money] NOT NULL,
	[JurnalID] [varchar](88) NOT NULL,
	[BKKID] [varchar](88) NOT NULL,
	[FOBO] [bit] NOT NULL,
	[Tipe] [tinyint] NOT NULL,
	[Project] [varchar](20) NOT NULL,
	[NamaProject] [varchar](50) NOT NULL,
 CONSTRAINT [PK_MS_CASHBACK] PRIMARY KEY CLUSTERED 
(
	[Nocb] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_CASHBACK_LOG]    Script Date: 05/04/2019 15.49.44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_CASHBACK_LOG](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Aktivitas] [varchar](6) NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Ket] [text] NOT NULL,
	[Pk] [varchar](50) NOT NULL,
	[Approve] [varchar](50) NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_MS_CASHBACK_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_KASKELUAR]    Script Date: 05/04/2019 15.49.44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_KASKELUAR](
	[NoVoucher] [int] NOT NULL,
	[Acc] [varchar](50) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[CaraBayar] [varchar](2) NOT NULL,
	[AlatBayar] [varchar](50) NOT NULL,
	[DibayarKepada] [varchar](50) NOT NULL,
	[Keterangan] [varchar](200) NOT NULL,
	[Nilai] [money] NOT NULL,
	[Akunting] [int] NOT NULL,
	[TglInput] [datetime] NOT NULL,
	[PrintKK] [int] NOT NULL,
	[SubID] [varchar](20) NOT NULL,
 CONSTRAINT [PK_MS_KASKELUAR] PRIMARY KEY CLUSTERED 
(
	[NoVoucher] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_KASKELUAR_LOG]    Script Date: 05/04/2019 15.49.44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_KASKELUAR_LOG](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Aktivitas] [varchar](6) NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Ket] [text] NOT NULL,
	[Pk] [varchar](50) NOT NULL,
	[Approve] [varchar](50) NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_MS_KASKELUAR_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_KASMASUK]    Script Date: 05/04/2019 15.49.44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_KASMASUK](
	[NoVoucher] [int] NOT NULL,
	[Acc] [varchar](50) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[CaraBayar] [varchar](2) NOT NULL,
	[AlatBayar] [varchar](50) NOT NULL,
	[DiterimaDari] [varchar](50) NOT NULL,
	[Keterangan] [varchar](200) NOT NULL,
	[Nilai] [money] NOT NULL,
	[Akunting] [int] NOT NULL,
	[NoTTS] [int] NOT NULL,
	[TglInput] [datetime] NOT NULL,
	[PrintKM] [int] NOT NULL,
	[SubID] [varchar](20) NOT NULL,
 CONSTRAINT [PK_MS_KASMASUK] PRIMARY KEY CLUSTERED 
(
	[NoVoucher] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_KASMASUK_LOG]    Script Date: 05/04/2019 15.49.44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_KASMASUK_LOG](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Aktivitas] [varchar](6) NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Ket] [text] NOT NULL,
	[Pk] [varchar](50) NOT NULL,
	[Approve] [varchar](50) NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_MS_KASMASUK_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_MEMO]    Script Date: 05/04/2019 15.49.44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_MEMO](
	[NoMEMO] [int] NOT NULL,
	[TglMEMO] [datetime] NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Tipe] [varchar](6) NOT NULL,
	[Ref] [varchar](50) NOT NULL,
	[Unit] [varchar](100) NOT NULL,
	[Customer] [varchar](100) NOT NULL,
	[CaraBayar] [varchar](2) NOT NULL,
	[Ket] [varchar](200) NOT NULL,
	[Total] [money] NOT NULL,
	[Status] [varchar](4) NOT NULL,
	[PrintMEMO] [int] NOT NULL,
	[TglInput] [datetime] NOT NULL,
	[NoBG] [varchar](20) NOT NULL,
	[StatusBG] [varchar](3) NOT NULL,
	[TglBG] [datetime] NULL,
	[Titip] [varchar](200) NOT NULL,
	[Tolak] [varchar](200) NOT NULL,
	[NoBKM] [varchar](50) NOT NULL,
	[TglBKM] [datetime] NULL,
	[PrintBKM] [int] NOT NULL,
	[NoSlip] [int] NOT NULL,
	[Bank] [varchar](20) NOT NULL,
	[Pph] [bit] NOT NULL,
	[TglSetoran] [datetime] NULL,
	[NilaiKembali] [money] NOT NULL,
	[ManualMEMO] [varchar](50) NOT NULL,
	[ManualBKM] [varchar](50) NOT NULL,
	[Akunting] [int] NOT NULL,
	[Acc] [varchar](50) NOT NULL,
	[AccDP] [bit] NOT NULL,
	[NoFPS] [varchar](50) NOT NULL,
	[PrintFPS] [int] NOT NULL,
	[NoVoucher] [varchar](100) NOT NULL,
	[TipePosting] [bit] NOT NULL,
	[SumberBayar] [bit] NOT NULL,
	[FOBO] [bit] NOT NULL,
	[TglPenarikanBKM] [datetime] NULL,
	[BKMID] [varchar](88) NULL,
	[ANOID] [varchar](100) NOT NULL,
	[NoTTS] [int] NOT NULL,
	[NoMEMO2] [varchar](30) NOT NULL,
	[Project] [varchar](20) NOT NULL,
	[NamaProject] [varchar](50) NOT NULL,
 CONSTRAINT [PK_MS_MEMO] PRIMARY KEY CLUSTERED 
(
	[NoMEMO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_MEMO_LOG]    Script Date: 05/04/2019 15.49.44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_MEMO_LOG](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Aktivitas] [varchar](6) NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Ket] [text] NOT NULL,
	[Pk] [varchar](50) NOT NULL,
	[Approve] [varchar](50) NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_MS_MEMO_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_PAKAD]    Script Date: 05/04/2019 15.49.44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_PAKAD](
	[NoPAkad] [varchar](20) NOT NULL,
	[TglPAkad] [datetime] NOT NULL,
	[Status] [varchar](1) NOT NULL,
	[Ref] [varchar](50) NOT NULL,
	[Unit] [varchar](100) NOT NULL,
	[Customer] [varchar](100) NOT NULL,
	[NoTelp] [varchar](50) NOT NULL,
	[Alamat1] [varchar](50) NOT NULL,
	[Alamat2] [varchar](50) NOT NULL,
	[Alamat3] [varchar](50) NOT NULL,
	[PrintPAkad] [int] NOT NULL,
	[LevelPAkad] [int] NOT NULL,
 CONSTRAINT [PK_MS_PAKAD] PRIMARY KEY CLUSTERED 
(
	[NoPAkad] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_PENGAJUAN_KPA]    Script Date: 05/04/2019 15.49.44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_PENGAJUAN_KPA](
	[NoPengajuan] [int] IDENTITY(1,1) NOT NULL,
	[TglFormulir] [datetime] NOT NULL,
	[TglRencanacair] [datetime] NOT NULL,
	[Keterangan] [varchar](max) NOT NULL,
	[Total] [money] NOT NULL,
	[TglInput] [datetime] NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Status] [varchar](10) NOT NULL,
	[NoSurat] [varchar](50) NOT NULL,
	[PrintPengajuan] [int] NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_MS_PENGAJUAN_KPA] PRIMARY KEY CLUSTERED 
(
	[NoPengajuan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_PENGAJUAN_KPA_DETIL]    Script Date: 05/04/2019 15.49.44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_PENGAJUAN_KPA_DETIL](
	[NoPengajuan] [int] NOT NULL,
	[NoKontrak] [varchar](50) NOT NULL,
	[NoTagihan] [int] NOT NULL,
	[Nilai] [money] NOT NULL,
	[Nama] [varchar](100) NOT NULL,
	[NoUnit] [varchar](20) NOT NULL,
	[NamaTagihan] [varchar](100) NOT NULL,
 CONSTRAINT [PK_MS_PENGAJUAN_KPA_DETIL] PRIMARY KEY CLUSTERED 
(
	[NoPengajuan] ASC,
	[NoKontrak] ASC,
	[NoTagihan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_PENGAJUAN_KPA_LOG]    Script Date: 05/04/2019 15.49.44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_PENGAJUAN_KPA_LOG](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Aktivitas] [varchar](6) NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Ket] [text] NOT NULL,
	[Pk] [varchar](50) NOT NULL,
	[Approve] [varchar](50) NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_MS_PENGAJUAN_KPA_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_PJT]    Script Date: 05/04/2019 15.49.44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_PJT](
	[NoPJT] [varchar](50) NOT NULL,
	[TglPJT] [datetime] NOT NULL,
	[Tipe] [varchar](6) NOT NULL,
	[Ref] [varchar](50) NOT NULL,
	[Unit] [varchar](100) NOT NULL,
	[Customer] [varchar](100) NOT NULL,
	[NoTelp] [varchar](50) NOT NULL,
	[Alamat1] [varchar](50) NOT NULL,
	[Alamat2] [varchar](50) NOT NULL,
	[Alamat3] [varchar](50) NOT NULL,
	[Total] [money] NOT NULL,
	[PrintPJT] [int] NOT NULL,
	[TglJT] [datetime] NOT NULL,
	[Project] [varchar](20) NOT NULL,
	[Alamat4] [varchar](200) NOT NULL,
	[Alamat5] [varchar](200) NOT NULL,
 CONSTRAINT [PK_MS_PJT] PRIMARY KEY CLUSTERED 
(
	[NoPJT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_PJT_DETIL]    Script Date: 05/04/2019 15.49.44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_PJT_DETIL](
	[NoPJT] [varchar](50) NOT NULL,
	[NoUrut] [int] NOT NULL,
	[NoTagihan] [varchar](50) NOT NULL,
	[NamaTagihan] [varchar](100) NOT NULL,
	[Nilai] [money] NOT NULL,
	[TglJT] [datetime] NOT NULL,
 CONSTRAINT [PK_MS_PJT_DETIL] PRIMARY KEY CLUSTERED 
(
	[NoPJT] ASC,
	[NoUrut] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_PJT_JURNAL]    Script Date: 05/04/2019 15.49.44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_PJT_JURNAL](
	[JurnalID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[NoPJT] [varchar](50) NOT NULL,
	[Ket] [text] NOT NULL,
 CONSTRAINT [PK_MS_PJT_JURNAL] PRIMARY KEY CLUSTERED 
(
	[JurnalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_PJT_LOG]    Script Date: 05/04/2019 15.49.44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_PJT_LOG](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Aktivitas] [varchar](6) NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Ket] [text] NOT NULL,
	[Pk] [varchar](50) NOT NULL,
	[Approve] [varchar](50) NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_MS_PJT_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_PWAWANCARA]    Script Date: 05/04/2019 15.49.45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_PWAWANCARA](
	[NoPWawancara] [varchar](20) NOT NULL,
	[TglPWawancara] [datetime] NOT NULL,
	[Status] [varchar](1) NOT NULL,
	[Ref] [varchar](50) NOT NULL,
	[Unit] [varchar](100) NOT NULL,
	[Customer] [varchar](100) NOT NULL,
	[NoTelp] [varchar](50) NOT NULL,
	[Alamat1] [varchar](50) NOT NULL,
	[Alamat2] [varchar](50) NOT NULL,
	[Alamat3] [varchar](50) NOT NULL,
	[PrintPWawancara] [int] NOT NULL,
	[LevelPWawancara] [int] NOT NULL,
 CONSTRAINT [PK_MS_PWAWANCARA] PRIMARY KEY CLUSTERED 
(
	[NoPWawancara] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_REAL_KPA]    Script Date: 05/04/2019 15.49.45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_REAL_KPA](
	[NoReal] [int] IDENTITY(1,1) NOT NULL,
	[TglReal] [datetime] NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Ref] [varchar](50) NOT NULL,
	[Unit] [varchar](100) NOT NULL,
	[Customer] [varchar](100) NOT NULL,
	[Ket] [varchar](max) NOT NULL,
	[Total] [money] NOT NULL,
	[TglInput] [datetime] NOT NULL,
	[FOBO] [bit] NOT NULL,
	[JurnalID] [varchar](88) NULL,
	[NoPengajuan] [int] NOT NULL,
	[PrintReal] [int] NOT NULL,
	[Status] [varchar](4) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_REAL_KPA_LOG]    Script Date: 05/04/2019 15.49.45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_REAL_KPA_LOG](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Aktivitas] [varchar](6) NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Ket] [text] NOT NULL,
	[Pk] [varchar](50) NOT NULL,
	[Approve] [varchar](50) NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_MS_REAL_KPA_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_SKL]    Script Date: 05/04/2019 15.49.45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_SKL](
	[NoSKL] [varchar](100) NOT NULL,
	[TglSKL] [datetime] NOT NULL,
	[Ref] [varchar](50) NOT NULL,
	[NoSKLManual] [varchar](100) NOT NULL,
	[PrintSKL] [int] NOT NULL,
	[Used] [tinyint] NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_MS_SKL] PRIMARY KEY CLUSTERED 
(
	[NoSKL] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_SKL_LOG]    Script Date: 05/04/2019 15.49.45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_SKL_LOG](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Aktivitas] [varchar](50) NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Pk] [varchar](50) NOT NULL,
	[Ket] [text] NOT NULL,
	[Approve] [varchar](50) NOT NULL,
 CONSTRAINT [PK_MS_SKL_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_TTS]    Script Date: 05/04/2019 15.49.45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_TTS](
	[NoTTS] [int] NOT NULL,
	[TglTTS] [datetime] NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Tipe] [varchar](6) NOT NULL,
	[Ref] [varchar](50) NOT NULL,
	[Unit] [varchar](100) NOT NULL,
	[Customer] [varchar](100) NOT NULL,
	[CaraBayar] [varchar](2) NOT NULL,
	[Ket] [varchar](200) NOT NULL,
	[Total] [money] NOT NULL,
	[Status] [varchar](4) NOT NULL,
	[PrintTTS] [int] NOT NULL,
	[TglInput] [datetime] NOT NULL,
	[NoBG] [varchar](20) NOT NULL,
	[StatusBG] [varchar](3) NOT NULL,
	[TglBG] [datetime] NULL,
	[Titip] [varchar](200) NOT NULL,
	[Tolak] [varchar](200) NOT NULL,
	[TglBKM] [datetime] NULL,
	[PrintBKM] [int] NOT NULL,
	[NoSlip] [int] NOT NULL,
	[Bank] [varchar](20) NOT NULL,
	[Pph] [bit] NOT NULL,
	[TglSetoran] [datetime] NULL,
	[NilaiKembali] [money] NOT NULL,
	[ManualTTS] [varchar](50) NOT NULL,
	[ManualBKM] [varchar](50) NOT NULL,
	[Akunting] [int] NOT NULL,
	[Acc] [varchar](50) NOT NULL,
	[AccDP] [bit] NOT NULL,
	[NoFPS] [varchar](50) NOT NULL,
	[PrintFPS] [int] NOT NULL,
	[NoVoucher] [varchar](100) NOT NULL,
	[TipePosting] [bit] NOT NULL,
	[SumberBayar] [bit] NOT NULL,
	[FOBO] [bit] NOT NULL,
	[TglPenarikanBKM] [datetime] NULL,
	[BKMID] [varchar](88) NULL,
	[NoBKM] [int] NOT NULL,
	[NoBKM2] [varchar](30) NOT NULL,
	[NoTTS2] [varchar](30) NOT NULL,
	[TglBKM2] [datetime] NULL,
	[LebihBayar] [money] NOT NULL,
	[AdminBank] [money] NOT NULL,
	[Total2] [money] NOT NULL,
	[NoAnonim] [varchar](20) NOT NULL,
	[NoKK] [varchar](50) NOT NULL,
	[BankKK] [varchar](50) NOT NULL,
	[BankBG] [varchar](50) NOT NULL,
	[TanggalUangDiterima] [datetime] NULL,
	[LB] [decimal](18, 0) NOT NULL,
	[SubID] [varchar](20) NOT NULL,
	[TglFP] [datetime] NOT NULL,
	[TTSKPA] [bit] NOT NULL,
	[NoBKMManual] [varchar](50) NOT NULL,
	[BebanBiayaAdmin] [tinyint] NOT NULL,
	[TglJTBG] [datetime] NULL,
	[JurnalID] [varchar](88) NOT NULL,
	[NoNUP] [varchar](50) NOT NULL,
	[Jenis] [varchar](50) NOT NULL,
	[Catatan] [varchar](100) NOT NULL,
	[Project] [varchar](20) NOT NULL,
	[NamaProject] [varchar](50) NOT NULL,
	[NoSSP] [varchar](50) NOT NULL,
	[NoReservasi] [int] NOT NULL,
	[FOBOReservasi] [int] NOT NULL,
	[GLJMID] [varchar](88) NOT NULL,
 CONSTRAINT [PK_MS_TTS] PRIMARY KEY CLUSTERED 
(
	[NoTTS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_TTS_LOG]    Script Date: 05/04/2019 15.49.45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_TTS_LOG](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Aktivitas] [varchar](6) NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Ket] [text] NOT NULL,
	[Pk] [varchar](50) NOT NULL,
	[Approve] [varchar](50) NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_MS_TTS_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_TUNGGAKAN]    Script Date: 05/04/2019 15.49.45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_TUNGGAKAN](
	[NoTunggakan] [int] NOT NULL,
	[TglTunggakan] [datetime] NOT NULL,
	[Status] [varchar](1) NOT NULL,
	[Tipe] [varchar](6) NOT NULL,
	[Ref] [varchar](50) NOT NULL,
	[Unit] [varchar](100) NOT NULL,
	[Customer] [varchar](100) NOT NULL,
	[NoTelp] [varchar](50) NOT NULL,
	[Alamat1] [varchar](50) NOT NULL,
	[Alamat2] [varchar](50) NOT NULL,
	[Alamat3] [varchar](50) NOT NULL,
	[Total] [money] NOT NULL,
	[PrintST] [int] NOT NULL,
	[LevelTunggakan] [int] NOT NULL,
	[TglKuasaSomasi] [datetime] NULL,
	[ManualTunggakan] [varchar](50) NOT NULL,
	[Project] [varchar](20) NOT NULL,
	[Alamat4] [varchar](200) NOT NULL,
	[Alamat5] [varchar](200) NOT NULL,
 CONSTRAINT [PK_MS_TUNGGAKAN] PRIMARY KEY CLUSTERED 
(
	[NoTunggakan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_TUNGGAKAN_DETIL]    Script Date: 05/04/2019 15.49.45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_TUNGGAKAN_DETIL](
	[NoTunggakan] [int] NOT NULL,
	[NoUrut] [int] NOT NULL,
	[NoTagihan] [varchar](50) NOT NULL,
	[NamaTagihan] [varchar](100) NOT NULL,
	[Nilai] [money] NOT NULL,
	[TglJT] [datetime] NOT NULL,
	[Telat] [int] NOT NULL,
	[Denda] [money] NOT NULL,
 CONSTRAINT [PK_MS_TUNGGAKAN_DETIL] PRIMARY KEY CLUSTERED 
(
	[NoTunggakan] ASC,
	[NoUrut] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_TUNGGAKAN_JURNAL]    Script Date: 05/04/2019 15.49.45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_TUNGGAKAN_JURNAL](
	[JurnalID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[NoTunggakan] [int] NOT NULL,
	[Ket] [text] NOT NULL,
 CONSTRAINT [PK_MS_TUNGGAKAN_JURNAL] PRIMARY KEY CLUSTERED 
(
	[JurnalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_TUNGGAKAN_LOG]    Script Date: 05/04/2019 15.49.45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_TUNGGAKAN_LOG](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Aktivitas] [varchar](6) NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Ket] [text] NOT NULL,
	[Pk] [varchar](50) NOT NULL,
	[Approve] [varchar](50) NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_MS_TUNGGAKAN_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REF_ACC]    Script Date: 05/04/2019 15.49.45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REF_ACC](
	[Acc] [varchar](50) NOT NULL,
	[Rekening] [varchar](50) NOT NULL,
	[Bank] [varchar](50) NOT NULL,
	[AtasNama] [varchar](50) NOT NULL,
	[SaldoAwal] [money] NOT NULL,
	[SubID] [varchar](20) NOT NULL,
	[Cabang] [varchar](50) NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_REF_ACC_1] PRIMARY KEY CLUSTERED 
(
	[Acc] ASC,
	[SubID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REF_ACC_LOG]    Script Date: 05/04/2019 15.49.45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REF_ACC_LOG](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Aktivitas] [varchar](6) NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Ket] [text] NOT NULL,
	[Pk] [varchar](50) NOT NULL,
	[Approve] [varchar](50) NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_REF_ACC_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REF_EFAKTUR]    Script Date: 05/04/2019 15.49.45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REF_EFAKTUR](
	[Kode] [varchar](50) NOT NULL,
	[Uraian] [varchar](200) NOT NULL,
 CONSTRAINT [PK_REF_E-FAKTUR] PRIMARY KEY CLUSTERED 
(
	[Kode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REF_FP]    Script Date: 05/04/2019 15.49.45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REF_FP](
	[NoFPS] [varchar](50) NOT NULL,
	[Status] [bit] NOT NULL,
	[Project] [varchar](20) NOT NULL,
	[NoUrut] [int] NOT NULL,
	[NoSuratPermintaanFP] [varchar](50) NOT NULL,
	[NoSuratPemberianFP] [varchar](50) NOT NULL,
	[TglPengajuanFP] [date] NULL,
	[TglTerimaFP] [date] NULL,
	[LampiranSPT] [varchar](50) NOT NULL,
	[TotalFP1] [int] NOT NULL,
	[TotalFP2] [int] NOT NULL,
	[TotalFP3] [int] NOT NULL,
	[TotalFPMaksimal] [int] NOT NULL,
	[TotalFPDiterima] [int] NOT NULL,
	[PICNama] [varchar](50) NOT NULL,
	[PICBagian] [varchar](50) NOT NULL,
	[SN] [int] NOT NULL,
 CONSTRAINT [PK_REF_FP] PRIMARY KEY CLUSTERED 
(
	[NoFPS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REF_FP_LOG]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REF_FP_LOG](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Aktivitas] [varchar](6) NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Ket] [text] NOT NULL,
	[PK] [varchar](50) NOT NULL,
	[Approve] [varchar](50) NOT NULL,
 CONSTRAINT [PK_REF_FP_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REF_VA]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REF_VA](
	[NoVA] [varchar](50) NOT NULL,
	[Bank] [varchar](50) NOT NULL,
	[NoUnit] [varchar](20) NOT NULL,
	[TglInput] [datetime] NOT NULL,
	[Project] [varchar](20) NOT NULL,
	[NamaProject] [varchar](50) NOT NULL,
 CONSTRAINT [PK_REF_VA] PRIMARY KEY CLUSTERED 
(
	[NoVA] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REF_VA_LOG]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REF_VA_LOG](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Aktivitas] [varchar](6) NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Ket] [text] NOT NULL,
	[PK] [varchar](50) NOT NULL,
	[Approve] [varchar](50) NOT NULL,
 CONSTRAINT [PK_REF_VA_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[LapPDF] ADD  CONSTRAINT [DF_LapPDF_Nama]  DEFAULT ('') FOR [Nama]
GO
ALTER TABLE [dbo].[LapPDF] ADD  CONSTRAINT [DF_LapPDF_DefaultWP]  DEFAULT ((0)) FOR [DefaultWP]
GO
ALTER TABLE [dbo].[LapPDF] ADD  CONSTRAINT [DF_LapPDF_Link]  DEFAULT ('') FOR [Link]
GO
ALTER TABLE [dbo].[LapPDF] ADD  CONSTRAINT [DF_LapPDF_TglGenerate]  DEFAULT (getdate()) FOR [TglGenerate]
GO
ALTER TABLE [dbo].[LapPDF] ADD  CONSTRAINT [DF_LapPDF_IP_1]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[LapPDF] ADD  CONSTRAINT [DF_LapPDF_UserID_1]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[LapPDF] ADD  CONSTRAINT [DF_LapPDF_FileName]  DEFAULT ('') FOR [FileName]
GO
ALTER TABLE [dbo].[LapPDF] ADD  CONSTRAINT [DF_LapPDF_FileType_1]  DEFAULT ('') FOR [FileType]
GO
ALTER TABLE [dbo].[LapPDF] ADD  CONSTRAINT [DF_LapPDF_FileSize_1]  DEFAULT ((0)) FOR [FileSize]
GO
ALTER TABLE [dbo].[LapPDF] ADD  CONSTRAINT [DF_LapPDF_FilterDari]  DEFAULT (getdate()) FOR [FilterDari]
GO
ALTER TABLE [dbo].[LapPDF] ADD  CONSTRAINT [DF_LapPDF_FilterSampai]  DEFAULT (getdate()) FOR [FilterSampai]
GO
ALTER TABLE [dbo].[LapPDF] ADD  CONSTRAINT [DF_LapPDF_AccDari_1]  DEFAULT ('') FOR [AccDari]
GO
ALTER TABLE [dbo].[LapPDF] ADD  CONSTRAINT [DF_LapPDF_AccSampai_1]  DEFAULT ('') FOR [AccSampai]
GO
ALTER TABLE [dbo].[LapPDF] ADD  CONSTRAINT [DF_LapPDF_PeriodeBln]  DEFAULT ('') FOR [PeriodeBln]
GO
ALTER TABLE [dbo].[LapPDF] ADD  CONSTRAINT [DF_LapPDF_PeriodeThn]  DEFAULT ('') FOR [PeriodeThn]
GO
ALTER TABLE [dbo].[MS_ANONIM] ADD  CONSTRAINT [DF_MS_ANONIM_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[MS_ANONIM] ADD  CONSTRAINT [DF_MS_ANONIM_Bank]  DEFAULT ('') FOR [Bank]
GO
ALTER TABLE [dbo].[MS_ANONIM] ADD  CONSTRAINT [DF_MS_ANONIM_Nilai]  DEFAULT ((0)) FOR [Nilai]
GO
ALTER TABLE [dbo].[MS_ANONIM] ADD  CONSTRAINT [DF_MS_ANONIM_Status]  DEFAULT ('BARU') FOR [Status]
GO
ALTER TABLE [dbo].[MS_ANONIM] ADD  CONSTRAINT [DF_MS_ANONIM_Unit]  DEFAULT ('') FOR [Unit]
GO
ALTER TABLE [dbo].[MS_ANONIM] ADD  CONSTRAINT [DF_MS_ANONIM_Customer]  DEFAULT ('') FOR [Customer]
GO
ALTER TABLE [dbo].[MS_ANONIM] ADD  CONSTRAINT [DF_MS_ANONIM_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_ANONIM] ADD  DEFAULT ('') FOR [AccountID]
GO
ALTER TABLE [dbo].[MS_ANONIM] ADD  DEFAULT ('') FOR [BKMID]
GO
ALTER TABLE [dbo].[MS_ANONIM] ADD  CONSTRAINT [DF_MS_ANONIM_FOBO]  DEFAULT ((0)) FOR [FOBO]
GO
ALTER TABLE [dbo].[MS_ANONIM] ADD  CONSTRAINT [DF_MS_ANONIM_TglPostingJurnal]  DEFAULT (getdate()) FOR [TglPostingJurnal]
GO
ALTER TABLE [dbo].[MS_ANONIM] ADD  CONSTRAINT [DF_MS_ANONIM_JurnalID]  DEFAULT ('') FOR [JurnalID]
GO
ALTER TABLE [dbo].[MS_ANONIM] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_ANONIM] ADD  DEFAULT ('') FOR [NamaProject]
GO
ALTER TABLE [dbo].[MS_ANONIM] ADD  DEFAULT ('') FOR [Rekening]
GO
ALTER TABLE [dbo].[MS_ANONIM] ADD  DEFAULT ('') FOR [NoKontrak]
GO
ALTER TABLE [dbo].[MS_ANONIM_LOG] ADD  CONSTRAINT [DF_MS_ANONIM_LOG_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[MS_ANONIM_LOG] ADD  CONSTRAINT [DF_MS_ANONIM_LOG_Aktivitas]  DEFAULT ('') FOR [Aktivitas]
GO
ALTER TABLE [dbo].[MS_ANONIM_LOG] ADD  CONSTRAINT [DF_MS_ANONIM_LOG_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[MS_ANONIM_LOG] ADD  CONSTRAINT [DF_MS_ANONIM_LOG_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[MS_ANONIM_LOG] ADD  CONSTRAINT [DF_MS_ANONIM_LOG_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_ANONIM_LOG] ADD  CONSTRAINT [DF_MS_ANONIM_LOG_Pk]  DEFAULT ('') FOR [Pk]
GO
ALTER TABLE [dbo].[MS_ANONIM_LOG] ADD  CONSTRAINT [DF_MS_ANONIM_LOG_Approve]  DEFAULT ('') FOR [Approve]
GO
ALTER TABLE [dbo].[MS_ANONIM_LOG] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_CASHBACK] ADD  CONSTRAINT [DF_MS_CASHBACK_NoKontrak]  DEFAULT ('') FOR [NoKontrak]
GO
ALTER TABLE [dbo].[MS_CASHBACK] ADD  CONSTRAINT [DF__MS_CASHBA__NoUru__2F2FFC0C]  DEFAULT ((1)) FOR [NoUrut]
GO
ALTER TABLE [dbo].[MS_CASHBACK] ADD  CONSTRAINT [DF_MS_CASHBACK_Unit]  DEFAULT ('') FOR [Unit]
GO
ALTER TABLE [dbo].[MS_CASHBACK] ADD  CONSTRAINT [DF_MS_CASHBACK_Customer]  DEFAULT ('') FOR [Customer]
GO
ALTER TABLE [dbo].[MS_CASHBACK] ADD  CONSTRAINT [DF_MS_CASHBACK_TglPengembalian]  DEFAULT (getdate()) FOR [TglPengembalian]
GO
ALTER TABLE [dbo].[MS_CASHBACK] ADD  CONSTRAINT [DF_MS_CASHBACK_SisaTagihan]  DEFAULT ((0)) FOR [SisaTagihan]
GO
ALTER TABLE [dbo].[MS_CASHBACK] ADD  CONSTRAINT [DF_MS_CASHBACK_LebihBayar]  DEFAULT ((0)) FOR [LebihBayar]
GO
ALTER TABLE [dbo].[MS_CASHBACK] ADD  CONSTRAINT [DF_MS_CASHBACK_Cashback]  DEFAULT ((0)) FOR [Cashback]
GO
ALTER TABLE [dbo].[MS_CASHBACK] ADD  CONSTRAINT [DF__MS_CASHBA__BankK__7BB05806]  DEFAULT ((0)) FOR [BankKeluar]
GO
ALTER TABLE [dbo].[MS_CASHBACK] ADD  CONSTRAINT [DF_MS_CASHBACK_Bank]  DEFAULT ('') FOR [Bank]
GO
ALTER TABLE [dbo].[MS_CASHBACK] ADD  CONSTRAINT [DF__MS_CASHBA__Admin__69279377]  DEFAULT ((0)) FOR [AdminBank]
GO
ALTER TABLE [dbo].[MS_CASHBACK] ADD  CONSTRAINT [DF__MS_CASHBA__Jurna__1CDC41A7]  DEFAULT ('') FOR [JurnalID]
GO
ALTER TABLE [dbo].[MS_CASHBACK] ADD  CONSTRAINT [DF__MS_CASHBA__BKKID__5614BF03]  DEFAULT ('') FOR [BKKID]
GO
ALTER TABLE [dbo].[MS_CASHBACK] ADD  CONSTRAINT [DF__MS_CASHBAC__FOBO__43F60EC8]  DEFAULT ((0)) FOR [FOBO]
GO
ALTER TABLE [dbo].[MS_CASHBACK] ADD  DEFAULT ((0)) FOR [Tipe]
GO
ALTER TABLE [dbo].[MS_CASHBACK] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_CASHBACK] ADD  DEFAULT ('') FOR [NamaProject]
GO
ALTER TABLE [dbo].[MS_CASHBACK_LOG] ADD  CONSTRAINT [DF_MS_CASHBACK_LOG_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[MS_CASHBACK_LOG] ADD  CONSTRAINT [DF_MS_CASHBACK_LOG_Aktivitas]  DEFAULT ('') FOR [Aktivitas]
GO
ALTER TABLE [dbo].[MS_CASHBACK_LOG] ADD  CONSTRAINT [DF_MS_CASHBACK_LOG_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[MS_CASHBACK_LOG] ADD  CONSTRAINT [DF_MS_CASHBACK_LOG_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[MS_CASHBACK_LOG] ADD  CONSTRAINT [DF_MS_CASHBACK_LOG_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_CASHBACK_LOG] ADD  CONSTRAINT [DF_MS_CASHBACK_LOG_Pk]  DEFAULT ('') FOR [Pk]
GO
ALTER TABLE [dbo].[MS_CASHBACK_LOG] ADD  CONSTRAINT [DF_MS_CASHBACK_LOG_Approve]  DEFAULT ('') FOR [Approve]
GO
ALTER TABLE [dbo].[MS_CASHBACK_LOG] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_KASKELUAR] ADD  CONSTRAINT [DF_MS_KASKELUAR_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[MS_KASKELUAR] ADD  CONSTRAINT [DF_MS_KASKELUAR_CaraBayar]  DEFAULT ('') FOR [CaraBayar]
GO
ALTER TABLE [dbo].[MS_KASKELUAR] ADD  CONSTRAINT [DF_MS_KASKELUAR_AlatBayar]  DEFAULT ('') FOR [AlatBayar]
GO
ALTER TABLE [dbo].[MS_KASKELUAR] ADD  CONSTRAINT [DF_MS_KASKELUAR_DibayarKepada]  DEFAULT ('') FOR [DibayarKepada]
GO
ALTER TABLE [dbo].[MS_KASKELUAR] ADD  CONSTRAINT [DF_MS_KASKELUAR_Keterangan]  DEFAULT ('') FOR [Keterangan]
GO
ALTER TABLE [dbo].[MS_KASKELUAR] ADD  CONSTRAINT [DF_MS_KASKELUAR_Nilai]  DEFAULT ((0)) FOR [Nilai]
GO
ALTER TABLE [dbo].[MS_KASKELUAR] ADD  CONSTRAINT [DF_MS_KASKELUAR_Akunting]  DEFAULT ((0)) FOR [Akunting]
GO
ALTER TABLE [dbo].[MS_KASKELUAR] ADD  CONSTRAINT [DF_MS_KASKELUAR_TglInput]  DEFAULT (getdate()) FOR [TglInput]
GO
ALTER TABLE [dbo].[MS_KASKELUAR] ADD  CONSTRAINT [DF_MS_KASKELUAR_PrintKM]  DEFAULT ((0)) FOR [PrintKK]
GO
ALTER TABLE [dbo].[MS_KASKELUAR] ADD  DEFAULT ('') FOR [SubID]
GO
ALTER TABLE [dbo].[MS_KASKELUAR_LOG] ADD  CONSTRAINT [DF_MS_KASKELUAR_LOG_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[MS_KASKELUAR_LOG] ADD  CONSTRAINT [DF_MS_KASKELUAR_LOG_Aktivitas]  DEFAULT ('') FOR [Aktivitas]
GO
ALTER TABLE [dbo].[MS_KASKELUAR_LOG] ADD  CONSTRAINT [DF_MS_KASKELUAR_LOG_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[MS_KASKELUAR_LOG] ADD  CONSTRAINT [DF_MS_KASKELUAR_LOG_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[MS_KASKELUAR_LOG] ADD  CONSTRAINT [DF_MS_KASKELUAR_LOG_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_KASKELUAR_LOG] ADD  CONSTRAINT [DF_MS_KASKELUAR_LOG_Pk]  DEFAULT ('') FOR [Pk]
GO
ALTER TABLE [dbo].[MS_KASKELUAR_LOG] ADD  CONSTRAINT [DF_MS_KASKELUAR_LOG_Approve]  DEFAULT ('') FOR [Approve]
GO
ALTER TABLE [dbo].[MS_KASKELUAR_LOG] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_KASMASUK] ADD  CONSTRAINT [DF_MS_KASMASUK_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[MS_KASMASUK] ADD  CONSTRAINT [DF_MS_KASMASUK_CaraBayar]  DEFAULT ('') FOR [CaraBayar]
GO
ALTER TABLE [dbo].[MS_KASMASUK] ADD  CONSTRAINT [DF_MS_KASMASUK_AlatBayar]  DEFAULT ('') FOR [AlatBayar]
GO
ALTER TABLE [dbo].[MS_KASMASUK] ADD  CONSTRAINT [DF_MS_KASMASUK_DiterimaDari]  DEFAULT ('') FOR [DiterimaDari]
GO
ALTER TABLE [dbo].[MS_KASMASUK] ADD  CONSTRAINT [DF_MS_KASMASUK_Keterangan]  DEFAULT ('') FOR [Keterangan]
GO
ALTER TABLE [dbo].[MS_KASMASUK] ADD  CONSTRAINT [DF_MS_KASMASUK_Nilai]  DEFAULT ((0)) FOR [Nilai]
GO
ALTER TABLE [dbo].[MS_KASMASUK] ADD  CONSTRAINT [DF_MS_KASMASUK_Akunting]  DEFAULT ((0)) FOR [Akunting]
GO
ALTER TABLE [dbo].[MS_KASMASUK] ADD  CONSTRAINT [DF_MS_KASMASUK_NoTTS]  DEFAULT ((0)) FOR [NoTTS]
GO
ALTER TABLE [dbo].[MS_KASMASUK] ADD  CONSTRAINT [DF_MS_KASMASUK_TglInput]  DEFAULT (getdate()) FOR [TglInput]
GO
ALTER TABLE [dbo].[MS_KASMASUK] ADD  CONSTRAINT [DF_MS_KASMASUK_PrintKM]  DEFAULT ((0)) FOR [PrintKM]
GO
ALTER TABLE [dbo].[MS_KASMASUK] ADD  DEFAULT ('') FOR [SubID]
GO
ALTER TABLE [dbo].[MS_KASMASUK_LOG] ADD  CONSTRAINT [DF_MS_KASMASUK_LOG_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[MS_KASMASUK_LOG] ADD  CONSTRAINT [DF_MS_KASMASUK_LOG_Aktivitas]  DEFAULT ('') FOR [Aktivitas]
GO
ALTER TABLE [dbo].[MS_KASMASUK_LOG] ADD  CONSTRAINT [DF_MS_KASMASUK_LOG_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[MS_KASMASUK_LOG] ADD  CONSTRAINT [DF_MS_KASMASUK_LOG_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[MS_KASMASUK_LOG] ADD  CONSTRAINT [DF_MS_KASMASUK_LOG_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_KASMASUK_LOG] ADD  CONSTRAINT [DF_MS_KASMASUK_LOG_Pk]  DEFAULT ('') FOR [Pk]
GO
ALTER TABLE [dbo].[MS_KASMASUK_LOG] ADD  CONSTRAINT [DF_MS_KASMASUK_LOG_Approve]  DEFAULT ('') FOR [Approve]
GO
ALTER TABLE [dbo].[MS_KASMASUK_LOG] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_MEMO] ADD  CONSTRAINT [DF_MS_MEMO_TglMEMO]  DEFAULT (getdate()) FOR [TglMEMO]
GO
ALTER TABLE [dbo].[MS_MEMO] ADD  CONSTRAINT [DF_MS_MEMO_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[MS_MEMO] ADD  CONSTRAINT [DF_MS_MEMO_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[MS_MEMO] ADD  CONSTRAINT [DF_MS_MEMO_Tipe]  DEFAULT ('') FOR [Tipe]
GO
ALTER TABLE [dbo].[MS_MEMO] ADD  CONSTRAINT [DF_MS_MEMO_Ref]  DEFAULT ('') FOR [Ref]
GO
ALTER TABLE [dbo].[MS_MEMO] ADD  CONSTRAINT [DF_MS_MEMO_Unit]  DEFAULT ('') FOR [Unit]
GO
ALTER TABLE [dbo].[MS_MEMO] ADD  CONSTRAINT [DF_MS_MEMO_Customer]  DEFAULT ('') FOR [Customer]
GO
ALTER TABLE [dbo].[MS_MEMO] ADD  CONSTRAINT [DF_MS_MEMO_CaraBayar]  DEFAULT ('') FOR [CaraBayar]
GO
ALTER TABLE [dbo].[MS_MEMO] ADD  CONSTRAINT [DF_MS_MEMO_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_MEMO] ADD  CONSTRAINT [DF_MS_MEMO_Total]  DEFAULT ((0)) FOR [Total]
GO
ALTER TABLE [dbo].[MS_MEMO] ADD  CONSTRAINT [DF_MS_MEMO_Status]  DEFAULT ('BARU') FOR [Status]
GO
ALTER TABLE [dbo].[MS_MEMO] ADD  CONSTRAINT [DF_MS_MEMO_PrintMEMO]  DEFAULT ((0)) FOR [PrintMEMO]
GO
ALTER TABLE [dbo].[MS_MEMO] ADD  CONSTRAINT [DF_MS_MEMO_TglInput]  DEFAULT (getdate()) FOR [TglInput]
GO
ALTER TABLE [dbo].[MS_MEMO] ADD  CONSTRAINT [DF_MS_MEMO_NoBG]  DEFAULT ('') FOR [NoBG]
GO
ALTER TABLE [dbo].[MS_MEMO] ADD  CONSTRAINT [DF_MS_MEMO_StatusBG]  DEFAULT ('OK') FOR [StatusBG]
GO
ALTER TABLE [dbo].[MS_MEMO] ADD  CONSTRAINT [DF_MS_MEMO_Titip]  DEFAULT ('') FOR [Titip]
GO
ALTER TABLE [dbo].[MS_MEMO] ADD  CONSTRAINT [DF_MS_MEMO_Tolak]  DEFAULT ('') FOR [Tolak]
GO
ALTER TABLE [dbo].[MS_MEMO] ADD  CONSTRAINT [DF_MS_MEMO_NoBKM]  DEFAULT ('') FOR [NoBKM]
GO
ALTER TABLE [dbo].[MS_MEMO] ADD  CONSTRAINT [DF_MS_MEMO_PrintBKM]  DEFAULT ((0)) FOR [PrintBKM]
GO
ALTER TABLE [dbo].[MS_MEMO] ADD  CONSTRAINT [DF_MS_MEMO_NoSlip]  DEFAULT ((0)) FOR [NoSlip]
GO
ALTER TABLE [dbo].[MS_MEMO] ADD  CONSTRAINT [DF_MS_MEMO_Bank]  DEFAULT ('') FOR [Bank]
GO
ALTER TABLE [dbo].[MS_MEMO] ADD  CONSTRAINT [DF_MS_MEMO_Pph]  DEFAULT ((0)) FOR [Pph]
GO
ALTER TABLE [dbo].[MS_MEMO] ADD  CONSTRAINT [DF_MS_MEMO_NilaiKembali]  DEFAULT ((0)) FOR [NilaiKembali]
GO
ALTER TABLE [dbo].[MS_MEMO] ADD  CONSTRAINT [DF_MS_MEMO_ManualMEMO]  DEFAULT ('') FOR [ManualMEMO]
GO
ALTER TABLE [dbo].[MS_MEMO] ADD  CONSTRAINT [DF_MS_MEMO_ManualBKM]  DEFAULT ('') FOR [ManualBKM]
GO
ALTER TABLE [dbo].[MS_MEMO] ADD  CONSTRAINT [OF_MS_MEMO_Akunting]  DEFAULT ((0)) FOR [Akunting]
GO
ALTER TABLE [dbo].[MS_MEMO] ADD  CONSTRAINT [DF_MS_MEMO_Acc]  DEFAULT ('') FOR [Acc]
GO
ALTER TABLE [dbo].[MS_MEMO] ADD  CONSTRAINT [OF_MS_MEMO_AccDP]  DEFAULT ((0)) FOR [AccDP]
GO
ALTER TABLE [dbo].[MS_MEMO] ADD  CONSTRAINT [OF_MS_MEMO_NoFPS]  DEFAULT ('') FOR [NoFPS]
GO
ALTER TABLE [dbo].[MS_MEMO] ADD  CONSTRAINT [OF_MS_MEMO_PrintFPS]  DEFAULT ((0)) FOR [PrintFPS]
GO
ALTER TABLE [dbo].[MS_MEMO] ADD  CONSTRAINT [OF_MS_MEMO_NoVoucher]  DEFAULT ('') FOR [NoVoucher]
GO
ALTER TABLE [dbo].[MS_MEMO] ADD  CONSTRAINT [DF_MS_MEMO_TipePosting]  DEFAULT ((0)) FOR [TipePosting]
GO
ALTER TABLE [dbo].[MS_MEMO] ADD  CONSTRAINT [DF_MS_MEMO_SumberBayar]  DEFAULT ((0)) FOR [SumberBayar]
GO
ALTER TABLE [dbo].[MS_MEMO] ADD  CONSTRAINT [DF_MS_MEMO_FOBO]  DEFAULT ((0)) FOR [FOBO]
GO
ALTER TABLE [dbo].[MS_MEMO] ADD  CONSTRAINT [DF__MS_MEMO__TglPenar__367C1819]  DEFAULT (NULL) FOR [TglPenarikanBKM]
GO
ALTER TABLE [dbo].[MS_MEMO] ADD  CONSTRAINT [DF__MS_MEMO__BKMID__37703C52]  DEFAULT ('') FOR [BKMID]
GO
ALTER TABLE [dbo].[MS_MEMO] ADD  CONSTRAINT [DF__MS_MEMO__ANOID__151B244E]  DEFAULT ('') FOR [ANOID]
GO
ALTER TABLE [dbo].[MS_MEMO] ADD  CONSTRAINT [DF__MS_MEMO__NoTTS__27C3E46E]  DEFAULT ((0)) FOR [NoTTS]
GO
ALTER TABLE [dbo].[MS_MEMO] ADD  CONSTRAINT [DF_MS_MEMO_NoMEMO2]  DEFAULT ('') FOR [NoMEMO2]
GO
ALTER TABLE [dbo].[MS_MEMO] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_MEMO] ADD  DEFAULT ('') FOR [NamaProject]
GO
ALTER TABLE [dbo].[MS_MEMO_LOG] ADD  CONSTRAINT [DF_MS_MEMO_LOG_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[MS_MEMO_LOG] ADD  CONSTRAINT [DF_MS_MEMO_LOG_Aktivitas]  DEFAULT ('') FOR [Aktivitas]
GO
ALTER TABLE [dbo].[MS_MEMO_LOG] ADD  CONSTRAINT [DF_MS_MEMO_LOG_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[MS_MEMO_LOG] ADD  CONSTRAINT [DF_MS_MEMO_LOG_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[MS_MEMO_LOG] ADD  CONSTRAINT [DF_MS_MEMO_LOG_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_MEMO_LOG] ADD  CONSTRAINT [DF_MS_MEMO_LOG_Pk]  DEFAULT ('') FOR [Pk]
GO
ALTER TABLE [dbo].[MS_MEMO_LOG] ADD  CONSTRAINT [DF_MS_MEMO_LOG_Approve]  DEFAULT ('') FOR [Approve]
GO
ALTER TABLE [dbo].[MS_MEMO_LOG] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_PAKAD] ADD  CONSTRAINT [DF_MS_PAKAD_TglPAkad]  DEFAULT (getdate()) FOR [TglPAkad]
GO
ALTER TABLE [dbo].[MS_PAKAD] ADD  CONSTRAINT [DF_MS_PAKAD_Status]  DEFAULT ('A') FOR [Status]
GO
ALTER TABLE [dbo].[MS_PAKAD] ADD  CONSTRAINT [DF_MS_PAKAD_Ref]  DEFAULT ('') FOR [Ref]
GO
ALTER TABLE [dbo].[MS_PAKAD] ADD  CONSTRAINT [DF_MS_PAKAD_Unit]  DEFAULT ('') FOR [Unit]
GO
ALTER TABLE [dbo].[MS_PAKAD] ADD  CONSTRAINT [DF_MS_PAKAD_Customer]  DEFAULT ('') FOR [Customer]
GO
ALTER TABLE [dbo].[MS_PAKAD] ADD  CONSTRAINT [DF_MS_PAKAD_NoTelp]  DEFAULT ('') FOR [NoTelp]
GO
ALTER TABLE [dbo].[MS_PAKAD] ADD  CONSTRAINT [DF_MS_PAKAD_Alamat1]  DEFAULT ('') FOR [Alamat1]
GO
ALTER TABLE [dbo].[MS_PAKAD] ADD  CONSTRAINT [DF_MS_PAKAD_Alamat2]  DEFAULT ('') FOR [Alamat2]
GO
ALTER TABLE [dbo].[MS_PAKAD] ADD  CONSTRAINT [DF_MS_PAKAD_Alamat3]  DEFAULT ('') FOR [Alamat3]
GO
ALTER TABLE [dbo].[MS_PAKAD] ADD  CONSTRAINT [DF_MS_PAKAD_PrintPAkad]  DEFAULT ((0)) FOR [PrintPAkad]
GO
ALTER TABLE [dbo].[MS_PAKAD] ADD  CONSTRAINT [DF_MS_PAKAD_LevelPAkad]  DEFAULT ((1)) FOR [LevelPAkad]
GO
ALTER TABLE [dbo].[MS_PENGAJUAN_KPA] ADD  CONSTRAINT [DF_MS_PENGAJUAN_KPA_TglFormulir]  DEFAULT (getdate()) FOR [TglFormulir]
GO
ALTER TABLE [dbo].[MS_PENGAJUAN_KPA] ADD  CONSTRAINT [DF_MS_PENGAJUAN_KPA_TglRencanacair]  DEFAULT (getdate()) FOR [TglRencanacair]
GO
ALTER TABLE [dbo].[MS_PENGAJUAN_KPA] ADD  CONSTRAINT [DF_MS_PENGAJUAN_KPA_Keterangan]  DEFAULT ('') FOR [Keterangan]
GO
ALTER TABLE [dbo].[MS_PENGAJUAN_KPA] ADD  CONSTRAINT [DF_MS_PENGAJUAN_KPA_Total]  DEFAULT ((0)) FOR [Total]
GO
ALTER TABLE [dbo].[MS_PENGAJUAN_KPA] ADD  DEFAULT (getdate()) FOR [TglInput]
GO
ALTER TABLE [dbo].[MS_PENGAJUAN_KPA] ADD  CONSTRAINT [DF_MS_PENGAJUAN_KPA_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[MS_PENGAJUAN_KPA] ADD  CONSTRAINT [DF_MS_PENGAJUAN_KPA_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[MS_PENGAJUAN_KPA] ADD  CONSTRAINT [DF_MS_PENGAJUAN_KPA_Status]  DEFAULT ('') FOR [Status]
GO
ALTER TABLE [dbo].[MS_PENGAJUAN_KPA] ADD  CONSTRAINT [DF_MS_PENGAJUAN_KPA_NoSurat]  DEFAULT ('') FOR [NoSurat]
GO
ALTER TABLE [dbo].[MS_PENGAJUAN_KPA] ADD  CONSTRAINT [DF_MS_PENGAJUAN_KPA_PrintPengajuan]  DEFAULT ((0)) FOR [PrintPengajuan]
GO
ALTER TABLE [dbo].[MS_PENGAJUAN_KPA] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_PENGAJUAN_KPA_DETIL] ADD  CONSTRAINT [DF_MS_PENGAJUAN_KPA_DETIL_NoPengajuan]  DEFAULT ((0)) FOR [NoPengajuan]
GO
ALTER TABLE [dbo].[MS_PENGAJUAN_KPA_DETIL] ADD  DEFAULT ('') FOR [Nama]
GO
ALTER TABLE [dbo].[MS_PENGAJUAN_KPA_DETIL] ADD  DEFAULT ('') FOR [NoUnit]
GO
ALTER TABLE [dbo].[MS_PENGAJUAN_KPA_DETIL] ADD  DEFAULT ('') FOR [NamaTagihan]
GO
ALTER TABLE [dbo].[MS_PENGAJUAN_KPA_LOG] ADD  CONSTRAINT [DF_MS_PENGAJUAN_KPA_LOG_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[MS_PENGAJUAN_KPA_LOG] ADD  CONSTRAINT [DF_MS_PENGAJUAN_KPA_LOG_Aktivitas]  DEFAULT ('') FOR [Aktivitas]
GO
ALTER TABLE [dbo].[MS_PENGAJUAN_KPA_LOG] ADD  CONSTRAINT [DF_MS_PENGAJUAN_KPA_LOG_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[MS_PENGAJUAN_KPA_LOG] ADD  CONSTRAINT [DF_MS_PENGAJUAN_KPA_LOG_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[MS_PENGAJUAN_KPA_LOG] ADD  CONSTRAINT [DF_MS_PENGAJUAN_KPA_LOG_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_PENGAJUAN_KPA_LOG] ADD  CONSTRAINT [DF_MS_PENGAJUAN_KPA_LOG_Pk]  DEFAULT ('') FOR [Pk]
GO
ALTER TABLE [dbo].[MS_PENGAJUAN_KPA_LOG] ADD  CONSTRAINT [DF_MS_PENGAJUAN_KPA_LOG_Approve]  DEFAULT ('') FOR [Approve]
GO
ALTER TABLE [dbo].[MS_PENGAJUAN_KPA_LOG] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_PJT] ADD  CONSTRAINT [DF_MS_PJT_TglPJT]  DEFAULT (getdate()) FOR [TglPJT]
GO
ALTER TABLE [dbo].[MS_PJT] ADD  CONSTRAINT [DF_MS_PJT_Tipe]  DEFAULT ('') FOR [Tipe]
GO
ALTER TABLE [dbo].[MS_PJT] ADD  CONSTRAINT [DF_MS_PJT_Ref]  DEFAULT ('') FOR [Ref]
GO
ALTER TABLE [dbo].[MS_PJT] ADD  CONSTRAINT [DF_MS_PJT_Unit]  DEFAULT ('') FOR [Unit]
GO
ALTER TABLE [dbo].[MS_PJT] ADD  CONSTRAINT [DF_MS_PJT_Customer]  DEFAULT ('') FOR [Customer]
GO
ALTER TABLE [dbo].[MS_PJT] ADD  CONSTRAINT [DF_MS_PJT_NoTelp]  DEFAULT ('') FOR [NoTelp]
GO
ALTER TABLE [dbo].[MS_PJT] ADD  CONSTRAINT [DF_MS_PJT_Alamat1]  DEFAULT ('') FOR [Alamat1]
GO
ALTER TABLE [dbo].[MS_PJT] ADD  CONSTRAINT [DF_MS_PJT_Alamat2]  DEFAULT ('') FOR [Alamat2]
GO
ALTER TABLE [dbo].[MS_PJT] ADD  CONSTRAINT [DF_MS_PJT_Alamat3]  DEFAULT ('') FOR [Alamat3]
GO
ALTER TABLE [dbo].[MS_PJT] ADD  CONSTRAINT [DF_MS_PJT_Total]  DEFAULT ((0)) FOR [Total]
GO
ALTER TABLE [dbo].[MS_PJT] ADD  CONSTRAINT [DF_MS_PJT_PrintPJT]  DEFAULT ((0)) FOR [PrintPJT]
GO
ALTER TABLE [dbo].[MS_PJT] ADD  CONSTRAINT [DF__MS_PJT__TglJT__0C1BC9F9]  DEFAULT (getdate()) FOR [TglJT]
GO
ALTER TABLE [dbo].[MS_PJT] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_PJT] ADD  DEFAULT ('') FOR [Alamat4]
GO
ALTER TABLE [dbo].[MS_PJT] ADD  DEFAULT ('') FOR [Alamat5]
GO
ALTER TABLE [dbo].[MS_PJT_DETIL] ADD  CONSTRAINT [DF_MS_PJT_DETIL_NoTagihan]  DEFAULT ('') FOR [NoTagihan]
GO
ALTER TABLE [dbo].[MS_PJT_DETIL] ADD  CONSTRAINT [DF_MS_PJT_DETIL_NamaTagihan]  DEFAULT ('') FOR [NamaTagihan]
GO
ALTER TABLE [dbo].[MS_PJT_DETIL] ADD  CONSTRAINT [DF_MS_PJT_DETIL_Nilai]  DEFAULT ((0)) FOR [Nilai]
GO
ALTER TABLE [dbo].[MS_PJT_DETIL] ADD  CONSTRAINT [DF_MS_PJT_DETIL_TglJT]  DEFAULT (getdate()) FOR [TglJT]
GO
ALTER TABLE [dbo].[MS_PJT_JURNAL] ADD  CONSTRAINT [DF_MS_PJT_JURNAL_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[MS_PJT_JURNAL] ADD  CONSTRAINT [DF_MS_PJT_JURNAL_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[MS_PJT_JURNAL] ADD  CONSTRAINT [DF_MS_PJT_JURNAL_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_PJT_LOG] ADD  CONSTRAINT [DF_MS_PJT_LOG_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[MS_PJT_LOG] ADD  CONSTRAINT [DF_MS_PJT_LOG_Aktivitas]  DEFAULT ('') FOR [Aktivitas]
GO
ALTER TABLE [dbo].[MS_PJT_LOG] ADD  CONSTRAINT [DF_MS_PJT_LOG_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[MS_PJT_LOG] ADD  CONSTRAINT [DF_MS_PJT_LOG_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[MS_PJT_LOG] ADD  CONSTRAINT [DF_MS_PJT_LOG_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_PJT_LOG] ADD  CONSTRAINT [DF_MS_PJT_LOG_Pk]  DEFAULT ('') FOR [Pk]
GO
ALTER TABLE [dbo].[MS_PJT_LOG] ADD  CONSTRAINT [DF_MS_PJT_LOG_Approve]  DEFAULT ('') FOR [Approve]
GO
ALTER TABLE [dbo].[MS_PJT_LOG] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_PWAWANCARA] ADD  CONSTRAINT [DF_MS_PWAWANCARA_TglPWawancara]  DEFAULT (getdate()) FOR [TglPWawancara]
GO
ALTER TABLE [dbo].[MS_PWAWANCARA] ADD  CONSTRAINT [DF_MS_PWAWANCARA_Status]  DEFAULT ('A') FOR [Status]
GO
ALTER TABLE [dbo].[MS_PWAWANCARA] ADD  CONSTRAINT [DF_MS_PWAWANCARA_Ref]  DEFAULT ('') FOR [Ref]
GO
ALTER TABLE [dbo].[MS_PWAWANCARA] ADD  CONSTRAINT [DF_MS_PWAWANCARA_Unit]  DEFAULT ('') FOR [Unit]
GO
ALTER TABLE [dbo].[MS_PWAWANCARA] ADD  CONSTRAINT [DF_MS_PWAWANCARA_Customer]  DEFAULT ('') FOR [Customer]
GO
ALTER TABLE [dbo].[MS_PWAWANCARA] ADD  CONSTRAINT [DF_MS_PWAWANCARA_NoTelp]  DEFAULT ('') FOR [NoTelp]
GO
ALTER TABLE [dbo].[MS_PWAWANCARA] ADD  CONSTRAINT [DF_MS_PWAWANCARA_Alamat1]  DEFAULT ('') FOR [Alamat1]
GO
ALTER TABLE [dbo].[MS_PWAWANCARA] ADD  CONSTRAINT [DF_MS_PWAWANCARA_Alamat2]  DEFAULT ('') FOR [Alamat2]
GO
ALTER TABLE [dbo].[MS_PWAWANCARA] ADD  CONSTRAINT [DF_MS_PWAWANCARA_Alamat3]  DEFAULT ('') FOR [Alamat3]
GO
ALTER TABLE [dbo].[MS_PWAWANCARA] ADD  CONSTRAINT [DF_MS_PWAWANCARA_PrintPWawancara]  DEFAULT ((0)) FOR [PrintPWawancara]
GO
ALTER TABLE [dbo].[MS_PWAWANCARA] ADD  CONSTRAINT [DF_MS_PWAWANCARA_LevelPWawancara]  DEFAULT ((1)) FOR [LevelPWawancara]
GO
ALTER TABLE [dbo].[MS_REAL_KPA] ADD  CONSTRAINT [DF_MS_REAL_KPA2_TglReal]  DEFAULT (getdate()) FOR [TglReal]
GO
ALTER TABLE [dbo].[MS_REAL_KPA] ADD  CONSTRAINT [DF_MS_REAL_KPA2_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[MS_REAL_KPA] ADD  CONSTRAINT [DF_MS_REAL_KPA2_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[MS_REAL_KPA] ADD  CONSTRAINT [DF_MS_REAL_KPA2_Ref]  DEFAULT ('') FOR [Ref]
GO
ALTER TABLE [dbo].[MS_REAL_KPA] ADD  CONSTRAINT [DF_MS_REAL_KPA2_Unit]  DEFAULT ('') FOR [Unit]
GO
ALTER TABLE [dbo].[MS_REAL_KPA] ADD  CONSTRAINT [DF_MS_REAL_KPA2_Customer]  DEFAULT ('') FOR [Customer]
GO
ALTER TABLE [dbo].[MS_REAL_KPA] ADD  CONSTRAINT [DF_MS_REAL_KPA2_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_REAL_KPA] ADD  CONSTRAINT [DF_MS_REAL_KPA2_Total]  DEFAULT ((0)) FOR [Total]
GO
ALTER TABLE [dbo].[MS_REAL_KPA] ADD  CONSTRAINT [DF_MS_REAL_KPA2_TglInput]  DEFAULT (getdate()) FOR [TglInput]
GO
ALTER TABLE [dbo].[MS_REAL_KPA] ADD  CONSTRAINT [DF_MS_REAL_KPA2_FOBO]  DEFAULT ((0)) FOR [FOBO]
GO
ALTER TABLE [dbo].[MS_REAL_KPA] ADD  CONSTRAINT [DF_MS_REAL_KPA2_JurnalID]  DEFAULT ('') FOR [JurnalID]
GO
ALTER TABLE [dbo].[MS_REAL_KPA] ADD  CONSTRAINT [DF_MS_REAL_KPA_NoPengajuan]  DEFAULT ((0)) FOR [NoPengajuan]
GO
ALTER TABLE [dbo].[MS_REAL_KPA] ADD  CONSTRAINT [DF_MS_REAL_KPA_PrintReal]  DEFAULT ((0)) FOR [PrintReal]
GO
ALTER TABLE [dbo].[MS_REAL_KPA] ADD  DEFAULT ('BARU') FOR [Status]
GO
ALTER TABLE [dbo].[MS_REAL_KPA_LOG] ADD  CONSTRAINT [DF_MS_REAL_KPA_LOG_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[MS_REAL_KPA_LOG] ADD  CONSTRAINT [DF_MS_REAL_KPA_LOG_Aktivitas]  DEFAULT ('') FOR [Aktivitas]
GO
ALTER TABLE [dbo].[MS_REAL_KPA_LOG] ADD  CONSTRAINT [DF_MS_REAL_KPA_LOG_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[MS_REAL_KPA_LOG] ADD  CONSTRAINT [DF_MS_REAL_KPA_LOG_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[MS_REAL_KPA_LOG] ADD  CONSTRAINT [DF_MS_REAL_KPA_LOG_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_REAL_KPA_LOG] ADD  CONSTRAINT [DF_MS_REAL_KPA_LOG_Pk]  DEFAULT ('') FOR [Pk]
GO
ALTER TABLE [dbo].[MS_REAL_KPA_LOG] ADD  CONSTRAINT [DF_MS_REAL_KPA_LOG_Approve]  DEFAULT ('') FOR [Approve]
GO
ALTER TABLE [dbo].[MS_REAL_KPA_LOG] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_SKL] ADD  CONSTRAINT [DF_MS_SKL_PrintSKL]  DEFAULT ((0)) FOR [PrintSKL]
GO
ALTER TABLE [dbo].[MS_SKL] ADD  CONSTRAINT [DF_MS_SKL_Used]  DEFAULT ((0)) FOR [Used]
GO
ALTER TABLE [dbo].[MS_SKL] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_SKL_LOG] ADD  CONSTRAINT [DF_MS_SKL_LOG_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_SKL_LOG] ADD  CONSTRAINT [DF_MS_SKL_LOG_Approve]  DEFAULT ('') FOR [Approve]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  CONSTRAINT [DF_MS_TTS_TglTTS]  DEFAULT (getdate()) FOR [TglTTS]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  CONSTRAINT [DF_MS_TTS_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  CONSTRAINT [DF_MS_TTS_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  CONSTRAINT [DF_MS_TTS_Tipe]  DEFAULT ('') FOR [Tipe]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  CONSTRAINT [DF_MS_TTS_Ref]  DEFAULT ('') FOR [Ref]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  CONSTRAINT [DF_MS_TTS_Unit]  DEFAULT ('') FOR [Unit]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  CONSTRAINT [DF_MS_TTS_Customer]  DEFAULT ('') FOR [Customer]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  CONSTRAINT [DF_MS_TTS_CaraBayar]  DEFAULT ('') FOR [CaraBayar]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  CONSTRAINT [DF_MS_TTS_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  CONSTRAINT [DF_MS_TTS_Total]  DEFAULT ((0)) FOR [Total]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  CONSTRAINT [DF_MS_TTS_Status]  DEFAULT ('BARU') FOR [Status]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  CONSTRAINT [DF_MS_TTS_PrintTTS]  DEFAULT ((0)) FOR [PrintTTS]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  CONSTRAINT [DF_MS_TTS_TglInput]  DEFAULT (getdate()) FOR [TglInput]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  CONSTRAINT [DF_MS_TTS_NoBG]  DEFAULT ('') FOR [NoBG]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  CONSTRAINT [DF_MS_TTS_StatusBG]  DEFAULT ('OK') FOR [StatusBG]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  CONSTRAINT [DF_MS_TTS_Titip]  DEFAULT ('') FOR [Titip]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  CONSTRAINT [DF_MS_TTS_Tolak]  DEFAULT ('') FOR [Tolak]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  CONSTRAINT [DF_MS_TTS_PrintBKM]  DEFAULT ((0)) FOR [PrintBKM]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  CONSTRAINT [DF_MS_TTS_NoSlip]  DEFAULT ((0)) FOR [NoSlip]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  CONSTRAINT [DF_MS_TTS_Bank]  DEFAULT ('') FOR [Bank]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  CONSTRAINT [DF_MS_TTS_Pph]  DEFAULT ((0)) FOR [Pph]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  CONSTRAINT [DF_MS_TTS_NilaiKembali]  DEFAULT ((0)) FOR [NilaiKembali]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  CONSTRAINT [DF_MS_TTS_ManualTTS]  DEFAULT ('') FOR [ManualTTS]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  CONSTRAINT [DF_MS_TTS_ManualBKM]  DEFAULT ('') FOR [ManualBKM]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  CONSTRAINT [OF_MS_TTS_Akunting]  DEFAULT ((0)) FOR [Akunting]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  CONSTRAINT [DF_MS_TTS_Acc]  DEFAULT ('') FOR [Acc]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  CONSTRAINT [OF_MS_TTS_AccDP]  DEFAULT ((0)) FOR [AccDP]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  CONSTRAINT [OF_MS_TTS_NoFPS]  DEFAULT ('') FOR [NoFPS]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  CONSTRAINT [OF_MS_TTS_PrintFPS]  DEFAULT ((0)) FOR [PrintFPS]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  CONSTRAINT [OF_MS_TTS_NoVoucher]  DEFAULT ('') FOR [NoVoucher]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  CONSTRAINT [DF_MS_TTS_TipePosting]  DEFAULT ((0)) FOR [TipePosting]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  CONSTRAINT [DF_MS_TTS_SumberBayar]  DEFAULT ((0)) FOR [SumberBayar]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  CONSTRAINT [DF_MS_TTS_FOBO]  DEFAULT ((0)) FOR [FOBO]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  CONSTRAINT [DF__MS_TTS__TglPenar__58D1301D]  DEFAULT (NULL) FOR [TglPenarikanBKM]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  CONSTRAINT [DF__MS_TTS__BKMID__59C55456]  DEFAULT ('') FOR [BKMID]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  CONSTRAINT [DF__MS_TTS__NoBKM__5AB9788F]  DEFAULT ('') FOR [NoBKM]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  CONSTRAINT [DF__MS_TTS__NoBKM2__5BAD9CC8]  DEFAULT ('') FOR [NoBKM2]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  CONSTRAINT [DF__MS_TTS__NoTTS2__5CA1C101]  DEFAULT ('') FOR [NoTTS2]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  CONSTRAINT [DF__MS_TTS__LebihBay__5D95E53A]  DEFAULT ((0)) FOR [LebihBayar]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  CONSTRAINT [DF__MS_TTS__AdminBan__5E8A0973]  DEFAULT ((0)) FOR [AdminBank]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  CONSTRAINT [DF__MS_TTS__Total2__5F7E2DAC]  DEFAULT ((0)) FOR [Total2]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  CONSTRAINT [DF__MS_TTS__NoAnonim__607251E5]  DEFAULT ('') FOR [NoAnonim]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  CONSTRAINT [DF__MS_TTS__NoKK__6166761E]  DEFAULT ('') FOR [NoKK]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  CONSTRAINT [DF__MS_TTS__BankKK__625A9A57]  DEFAULT ('') FOR [BankKK]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  CONSTRAINT [DF__MS_TTS__BankBG__634EBE90]  DEFAULT ('') FOR [BankBG]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  CONSTRAINT [DF__MS_TTS__LB__6442E2C9]  DEFAULT ((0)) FOR [LB]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  DEFAULT ('') FOR [SubID]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  DEFAULT (getdate()) FOR [TglFP]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  DEFAULT ((0)) FOR [TTSKPA]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  DEFAULT ('') FOR [NoBKMManual]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  DEFAULT ((1)) FOR [BebanBiayaAdmin]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  DEFAULT ('') FOR [JurnalID]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  DEFAULT ('') FOR [NoNUP]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  DEFAULT ('') FOR [Jenis]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  DEFAULT ('') FOR [Catatan]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  DEFAULT ('') FOR [NamaProject]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  DEFAULT ('') FOR [NoSSP]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  DEFAULT ((0)) FOR [NoReservasi]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  DEFAULT ((0)) FOR [FOBOReservasi]
GO
ALTER TABLE [dbo].[MS_TTS] ADD  DEFAULT ('') FOR [GLJMID]
GO
ALTER TABLE [dbo].[MS_TTS_LOG] ADD  CONSTRAINT [DF_MS_TTS_LOG_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[MS_TTS_LOG] ADD  CONSTRAINT [DF_MS_TTS_LOG_Aktivitas]  DEFAULT ('') FOR [Aktivitas]
GO
ALTER TABLE [dbo].[MS_TTS_LOG] ADD  CONSTRAINT [DF_MS_TTS_LOG_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[MS_TTS_LOG] ADD  CONSTRAINT [DF_MS_TTS_LOG_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[MS_TTS_LOG] ADD  CONSTRAINT [DF_MS_TTS_LOG_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_TTS_LOG] ADD  CONSTRAINT [DF_MS_TTS_LOG_Pk]  DEFAULT ('') FOR [Pk]
GO
ALTER TABLE [dbo].[MS_TTS_LOG] ADD  CONSTRAINT [DF_MS_TTS_LOG_Approve]  DEFAULT ('') FOR [Approve]
GO
ALTER TABLE [dbo].[MS_TTS_LOG] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_TUNGGAKAN] ADD  CONSTRAINT [DF_MS_TUNGGAKAN_TglTunggakan]  DEFAULT (getdate()) FOR [TglTunggakan]
GO
ALTER TABLE [dbo].[MS_TUNGGAKAN] ADD  CONSTRAINT [DF_MS_TUNGGAKAN_Status]  DEFAULT ('A') FOR [Status]
GO
ALTER TABLE [dbo].[MS_TUNGGAKAN] ADD  CONSTRAINT [DF_MS_TUNGGAKAN_Tipe]  DEFAULT ('') FOR [Tipe]
GO
ALTER TABLE [dbo].[MS_TUNGGAKAN] ADD  CONSTRAINT [DF_MS_TUNGGAKAN_Ref]  DEFAULT ('') FOR [Ref]
GO
ALTER TABLE [dbo].[MS_TUNGGAKAN] ADD  CONSTRAINT [DF_MS_TUNGGAKAN_Unit]  DEFAULT ('') FOR [Unit]
GO
ALTER TABLE [dbo].[MS_TUNGGAKAN] ADD  CONSTRAINT [DF_MS_TUNGGAKAN_Customer]  DEFAULT ('') FOR [Customer]
GO
ALTER TABLE [dbo].[MS_TUNGGAKAN] ADD  CONSTRAINT [DF_MS_TUNGGAKAN_NoTelp]  DEFAULT ('') FOR [NoTelp]
GO
ALTER TABLE [dbo].[MS_TUNGGAKAN] ADD  CONSTRAINT [DF_MS_TUNGGAKAN_Alamat1]  DEFAULT ('') FOR [Alamat1]
GO
ALTER TABLE [dbo].[MS_TUNGGAKAN] ADD  CONSTRAINT [DF_MS_TUNGGAKAN_Alamat2]  DEFAULT ('') FOR [Alamat2]
GO
ALTER TABLE [dbo].[MS_TUNGGAKAN] ADD  CONSTRAINT [DF_MS_TUNGGAKAN_Alamat3]  DEFAULT ('') FOR [Alamat3]
GO
ALTER TABLE [dbo].[MS_TUNGGAKAN] ADD  CONSTRAINT [DF_MS_TUNGGAKAN_Total]  DEFAULT ((0)) FOR [Total]
GO
ALTER TABLE [dbo].[MS_TUNGGAKAN] ADD  CONSTRAINT [DF_MS_TUNGGAKAN_PrintTunggakan]  DEFAULT ((0)) FOR [PrintST]
GO
ALTER TABLE [dbo].[MS_TUNGGAKAN] ADD  CONSTRAINT [DF_MS_TUNGGAKAN_LevelTunggakan]  DEFAULT ((1)) FOR [LevelTunggakan]
GO
ALTER TABLE [dbo].[MS_TUNGGAKAN] ADD  CONSTRAINT [DF__MS_TUNGGA__Manua__7849DB76]  DEFAULT ('') FOR [ManualTunggakan]
GO
ALTER TABLE [dbo].[MS_TUNGGAKAN] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_TUNGGAKAN] ADD  DEFAULT ('') FOR [Alamat4]
GO
ALTER TABLE [dbo].[MS_TUNGGAKAN] ADD  DEFAULT ('') FOR [Alamat5]
GO
ALTER TABLE [dbo].[MS_TUNGGAKAN_DETIL] ADD  CONSTRAINT [DF_MS_TUNGGAKAN_DETIL_NoTagihan]  DEFAULT ('') FOR [NoTagihan]
GO
ALTER TABLE [dbo].[MS_TUNGGAKAN_DETIL] ADD  CONSTRAINT [DF_MS_TUNGGAKAN_DETIL_NamaTagihan]  DEFAULT ('') FOR [NamaTagihan]
GO
ALTER TABLE [dbo].[MS_TUNGGAKAN_DETIL] ADD  CONSTRAINT [DF_MS_TUNGGAKAN_DETIL_Nilai]  DEFAULT ((0)) FOR [Nilai]
GO
ALTER TABLE [dbo].[MS_TUNGGAKAN_DETIL] ADD  CONSTRAINT [DF_MS_TUNGGAKAN_DETIL_TglJT]  DEFAULT (getdate()) FOR [TglJT]
GO
ALTER TABLE [dbo].[MS_TUNGGAKAN_DETIL] ADD  CONSTRAINT [DF_MS_TUNGGAKAN_DETIL_Telat]  DEFAULT ((0)) FOR [Telat]
GO
ALTER TABLE [dbo].[MS_TUNGGAKAN_DETIL] ADD  CONSTRAINT [DF_MS_TUNGGAKAN_DETIL_Denda]  DEFAULT ((0)) FOR [Denda]
GO
ALTER TABLE [dbo].[MS_TUNGGAKAN_JURNAL] ADD  CONSTRAINT [DF_MS_TUNGGAKAN_JURNAL_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[MS_TUNGGAKAN_JURNAL] ADD  CONSTRAINT [DF_MS_TUNGGAKAN_JURNAL_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[MS_TUNGGAKAN_JURNAL] ADD  CONSTRAINT [DF_MS_TUNGGAKAN_JURNAL_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_TUNGGAKAN_LOG] ADD  CONSTRAINT [DF_MS_TUNGGAKAN_LOG_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[MS_TUNGGAKAN_LOG] ADD  CONSTRAINT [DF_MS_TUNGGAKAN_LOG_Aktivitas]  DEFAULT ('') FOR [Aktivitas]
GO
ALTER TABLE [dbo].[MS_TUNGGAKAN_LOG] ADD  CONSTRAINT [DF_MS_TUNGGAKAN_LOG_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[MS_TUNGGAKAN_LOG] ADD  CONSTRAINT [DF_MS_TUNGGAKAN_LOG_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[MS_TUNGGAKAN_LOG] ADD  CONSTRAINT [DF_MS_TUNGGAKAN_LOG_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_TUNGGAKAN_LOG] ADD  CONSTRAINT [DF_MS_TUNGGAKAN_LOG_Pk]  DEFAULT ('') FOR [Pk]
GO
ALTER TABLE [dbo].[MS_TUNGGAKAN_LOG] ADD  CONSTRAINT [DF_MS_TUNGGAKAN_LOG_Approve]  DEFAULT ('') FOR [Approve]
GO
ALTER TABLE [dbo].[MS_TUNGGAKAN_LOG] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[REF_ACC] ADD  CONSTRAINT [DF_REF_ACC_Rekening]  DEFAULT ('') FOR [Rekening]
GO
ALTER TABLE [dbo].[REF_ACC] ADD  CONSTRAINT [DF_REF_ACC_Bank]  DEFAULT ('') FOR [Bank]
GO
ALTER TABLE [dbo].[REF_ACC] ADD  CONSTRAINT [DF_REF_ACC_AtasNama]  DEFAULT ('') FOR [AtasNama]
GO
ALTER TABLE [dbo].[REF_ACC] ADD  CONSTRAINT [DF_REF_ACC_SaldoAwal]  DEFAULT ((0)) FOR [SaldoAwal]
GO
ALTER TABLE [dbo].[REF_ACC] ADD  DEFAULT ('') FOR [SubID]
GO
ALTER TABLE [dbo].[REF_ACC] ADD  DEFAULT ('') FOR [Cabang]
GO
ALTER TABLE [dbo].[REF_ACC] ADD  CONSTRAINT [REF_ACC_Project]  DEFAULT (N'') FOR [Project]
GO
ALTER TABLE [dbo].[REF_ACC_LOG] ADD  CONSTRAINT [DF_REF_ACC_LOG_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[REF_ACC_LOG] ADD  CONSTRAINT [DF_REF_ACC_LOG_Aktivitas]  DEFAULT ('') FOR [Aktivitas]
GO
ALTER TABLE [dbo].[REF_ACC_LOG] ADD  CONSTRAINT [DF_REF_ACC_LOG_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[REF_ACC_LOG] ADD  CONSTRAINT [DF_REF_ACC_LOG_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[REF_ACC_LOG] ADD  CONSTRAINT [DF_REF_ACC_LOG_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[REF_ACC_LOG] ADD  CONSTRAINT [DF_REF_ACC_LOG_Pk]  DEFAULT ('') FOR [Pk]
GO
ALTER TABLE [dbo].[REF_ACC_LOG] ADD  CONSTRAINT [DF_REF_ACC_LOG_Approve]  DEFAULT ('') FOR [Approve]
GO
ALTER TABLE [dbo].[REF_ACC_LOG] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[REF_EFAKTUR] ADD  CONSTRAINT [DF_REF_E-FAKTUR_Uraian]  DEFAULT ('') FOR [Uraian]
GO
ALTER TABLE [dbo].[REF_FP] ADD  CONSTRAINT [DF_REF_FP_Status]  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[REF_FP] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[REF_FP] ADD  DEFAULT ((0)) FOR [NoUrut]
GO
ALTER TABLE [dbo].[REF_FP] ADD  DEFAULT ('') FOR [NoSuratPermintaanFP]
GO
ALTER TABLE [dbo].[REF_FP] ADD  DEFAULT ('') FOR [NoSuratPemberianFP]
GO
ALTER TABLE [dbo].[REF_FP] ADD  DEFAULT ('') FOR [LampiranSPT]
GO
ALTER TABLE [dbo].[REF_FP] ADD  DEFAULT ((0)) FOR [TotalFP1]
GO
ALTER TABLE [dbo].[REF_FP] ADD  DEFAULT ((0)) FOR [TotalFP2]
GO
ALTER TABLE [dbo].[REF_FP] ADD  DEFAULT ((0)) FOR [TotalFP3]
GO
ALTER TABLE [dbo].[REF_FP] ADD  DEFAULT ((0)) FOR [TotalFPMaksimal]
GO
ALTER TABLE [dbo].[REF_FP] ADD  DEFAULT ((0)) FOR [TotalFPDiterima]
GO
ALTER TABLE [dbo].[REF_FP] ADD  DEFAULT ('') FOR [PICNama]
GO
ALTER TABLE [dbo].[REF_FP] ADD  DEFAULT ('') FOR [PICBagian]
GO
ALTER TABLE [dbo].[REF_FP] ADD  DEFAULT ((0)) FOR [SN]
GO
ALTER TABLE [dbo].[REF_FP_LOG] ADD  CONSTRAINT [DF_REF_FP_LOG_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[REF_FP_LOG] ADD  CONSTRAINT [DF_REF_FP_LOG_Aktivitas]  DEFAULT ('') FOR [Aktivitas]
GO
ALTER TABLE [dbo].[REF_FP_LOG] ADD  CONSTRAINT [DF_REF_FP_LOG_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[REF_FP_LOG] ADD  CONSTRAINT [DF_REF_FP_LOG_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[REF_FP_LOG] ADD  CONSTRAINT [DF_REF_FP_LOG_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[REF_FP_LOG] ADD  CONSTRAINT [DF_REF_FP_LOG_PK]  DEFAULT ('') FOR [PK]
GO
ALTER TABLE [dbo].[REF_FP_LOG] ADD  CONSTRAINT [DF_REF_FP_LOG_Approve]  DEFAULT ('') FOR [Approve]
GO
ALTER TABLE [dbo].[REF_VA] ADD  CONSTRAINT [DF_REF_VA_Bank]  DEFAULT ('') FOR [Bank]
GO
ALTER TABLE [dbo].[REF_VA] ADD  DEFAULT ('') FOR [NoUnit]
GO
ALTER TABLE [dbo].[REF_VA] ADD  DEFAULT (getdate()) FOR [TglInput]
GO
ALTER TABLE [dbo].[REF_VA] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[REF_VA] ADD  DEFAULT ('') FOR [NamaProject]
GO
ALTER TABLE [dbo].[REF_VA_LOG] ADD  CONSTRAINT [DF_REF_VA_LOG_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[REF_VA_LOG] ADD  CONSTRAINT [DF_REF_VA_LOG_Aktivitas]  DEFAULT ('') FOR [Aktivitas]
GO
ALTER TABLE [dbo].[REF_VA_LOG] ADD  CONSTRAINT [DF_REF_VA_LOG_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[REF_VA_LOG] ADD  CONSTRAINT [DF_REF_VA_LOG_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[REF_VA_LOG] ADD  CONSTRAINT [DF_REF_VA_LOG_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[REF_VA_LOG] ADD  CONSTRAINT [DF_REF_VA_LOG_PK]  DEFAULT ('') FOR [PK]
GO
ALTER TABLE [dbo].[REF_VA_LOG] ADD  CONSTRAINT [DF_REF_VA_LOG_Approve]  DEFAULT ('') FOR [Approve]
GO
ALTER TABLE [dbo].[MS_PENGAJUAN_KPA_DETIL]  WITH CHECK ADD  CONSTRAINT [FK_MS_PENGAJUAN_KPA_DETIL_MS_PENGAJUAN_KPA] FOREIGN KEY([NoPengajuan])
REFERENCES [dbo].[MS_PENGAJUAN_KPA] ([NoPengajuan])
GO
ALTER TABLE [dbo].[MS_PENGAJUAN_KPA_DETIL] CHECK CONSTRAINT [FK_MS_PENGAJUAN_KPA_DETIL_MS_PENGAJUAN_KPA]
GO
ALTER TABLE [dbo].[MS_PJT_DETIL]  WITH CHECK ADD  CONSTRAINT [FK_MS_PJT_DETIL_MS_PJT] FOREIGN KEY([NoPJT])
REFERENCES [dbo].[MS_PJT] ([NoPJT])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MS_PJT_DETIL] CHECK CONSTRAINT [FK_MS_PJT_DETIL_MS_PJT]
GO
ALTER TABLE [dbo].[MS_PJT_JURNAL]  WITH CHECK ADD  CONSTRAINT [FK_MS_PJT_JURNAL_MS_PJT] FOREIGN KEY([NoPJT])
REFERENCES [dbo].[MS_PJT] ([NoPJT])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MS_PJT_JURNAL] CHECK CONSTRAINT [FK_MS_PJT_JURNAL_MS_PJT]
GO
ALTER TABLE [dbo].[MS_TUNGGAKAN_DETIL]  WITH CHECK ADD  CONSTRAINT [FK_MS_TUNGGAKAN_DETIL_MS_TUNGGAKAN] FOREIGN KEY([NoTunggakan])
REFERENCES [dbo].[MS_TUNGGAKAN] ([NoTunggakan])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MS_TUNGGAKAN_DETIL] CHECK CONSTRAINT [FK_MS_TUNGGAKAN_DETIL_MS_TUNGGAKAN]
GO
ALTER TABLE [dbo].[MS_TUNGGAKAN_JURNAL]  WITH CHECK ADD  CONSTRAINT [FK_MS_TUNGGAKAN_JURNAL_MS_TUNGGAKAN] FOREIGN KEY([NoTunggakan])
REFERENCES [dbo].[MS_TUNGGAKAN] ([NoTunggakan])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MS_TUNGGAKAN_JURNAL] CHECK CONSTRAINT [FK_MS_TUNGGAKAN_JURNAL_MS_TUNGGAKAN]
GO
/****** Object:  StoredProcedure [dbo].[spAccBaru]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Pendaftaran account baru
*/

CREATE PROCEDURE [dbo].[spAccBaru]
	 @Acc varchar(20)
	,@Rekening varchar(50)
	,@Bank varchar(50)
	,@Cabang varchar(50)
	,@AtasNama varchar(50)
	,@SaldoAwal money
	,@SubID varchar (20)
	,@Project varchar(20)
AS

INSERT INTO REF_ACC
(
	 Acc
	,Rekening
	,Bank
	,Cabang
	,AtasNama
	,SaldoAwal
	,SubID
	,Project
)
VALUES
(
	 @Acc
	,@Rekening
	,@Bank
	,@Cabang
	,@AtasNama
	,@SaldoAwal
	,@SubID
	,@Project
)



GO
/****** Object:  StoredProcedure [dbo].[spAccDel]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Delete account (PERMANEN)
*/

CREATE PROCEDURE [dbo].[spAccDel]
	@Acc varchar(20),
	@SubID varchar(20)
AS

-- Acc tidak boleh dihapus jika sudah ada transaksi kas masuk dan kas keluar
IF EXISTS (SELECT Acc FROM MS_KASMASUK WHERE Acc = @Acc AND SubID = @SubID)
	RETURN
IF EXISTS (SELECT Acc FROM MS_KASKELUAR WHERE Acc = @Acc AND SubID = @SubID)
	RETURN
	
DELETE FROM REF_ACC WHERE Acc = @Acc AND SubID = @SubID









GO
/****** Object:  StoredProcedure [dbo].[spAccEdit]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Edit data account
*/

CREATE PROCEDURE [dbo].[spAccEdit]
	 @AccLama varchar(20)
	,@AccBaru varchar(20)
	,@Rekening varchar(50)
	,@Bank varchar(50)
	,@Cabang varchar(50)
	,@AtasNama varchar(50)
	,@SaldoAwal money
	,@SubID varchar(20)
	,@SubIDLama varchar(20)
	,@Project varchar(20)
AS

UPDATE REF_ACC SET
	 Acc = @AccBaru
	,Rekening = @Rekening
	,Bank = @Bank
	,Cabang = @Cabang
	,AtasNama = @AtasNama
	,SaldoAwal = @SaldoAwal
	,SubID = @SubID
	,Project = @Project
WHERE
Acc = @AccLama
AND
SubID = @SubIDLama

UPDATE REF_ACC_LOG SET Pk = @AccBaru WHERE Pk = @AccLama

UPDATE MS_TTS SET Acc=@AccBaru , SubID=@SubID WHERE Acc = @AccLama and SubID=@SubIDLama 












GO
/****** Object:  StoredProcedure [dbo].[spAutoPJT_JUAL]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Invoice JUAL
Interval Otomatis : 1x24 Jam
*/

CREATE PROCEDURE [dbo].[spAutoPJT_JUAL]
AS

DECLARE
	 @NoKontrak varchar(50)
	,@NoUnit varchar(20)
	,@Nama varchar(100)
	,@NoTelp varchar(50)
	,@Alamat1 varchar(50)
	,@Alamat2 varchar(50)
	,@Alamat3 varchar(50)
	,@TglPJT datetime
	,@AsOf datetime

-- tanggal jatuh tempo adalah 1 minggu ke depan, untuk memberi waktu bagi collection untuk mempersiapkan proses notifikasi
SET @TglPJT = CONVERT(varchar, GETDATE(), 101)
SET @AsOf = DATEADD(d,7,@TglPJT)

-- apabila PJT sudah di-generate, maka proses diabaikan
IF EXISTS
(SELECT NoPJT FROM MS_PJT WHERE CONVERT(varchar, TglPJT, 112) = CONVERT(varchar, @TglPJT, 112) AND Tipe = 'JUAL')
	RETURN

DECLARE rs CURSOR FOR
	SELECT NoKontrak, NoUnit, Nama, NoTelp, Alamat1, Alamat2, Alamat3
	FROM	
		ISC064_MARKETINGJUAL..MS_KONTRAK AS MS_KONTRAK INNER JOIN ISC064_MARKETINGJUAL..MS_CUSTOMER AS MS_CUSTOMER
		ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer
	WHERE MS_KONTRAK.Status = 'A'
	ORDER BY NoKontrak

OPEN rs
FETCH NEXT FROM rs INTO @NoKontrak, @NoUnit, @Nama, @NoTelp, @Alamat1, @Alamat2, @Alamat3

WHILE @@FETCH_STATUS = 0
BEGIN
	IF EXISTS(
	SELECT NoUrut FROM ISC064_MARKETINGJUAL..MS_TAGIHAN AS MS_TAGIHAN
	WHERE NoKontrak = @NoKontrak
	AND 
	(
		-- (CONVERT(varchar, TglJT, 112) <= CONVERT(varchar, @AsOf, 112) AND FlagPJT = 0) --skenario komputer mati
		--OR
		--(CONVERT(varchar, TglJT, 112) = CONVERT(varchar, @AsOf, 112)) --jatuh tempo natural
		(MONTH(TglJT) = MONTH(DATEADD(m,1,@TglPJT)) AND YEAR(TglJT) = YEAR(DATEADD(m,1,@TglPJT)) AND FlagPJT = 0) --skenario komputer mati
		--OR
		--(MONTH(TglJT) = MONTH(@TglPJT) AND YEAR(TglJT) = YEAR(@TglPJT)) --jatuh tempo natural
	)
	AND NilaiTagihan -
		(	SELECT ISNULL(SUM(NilaiPelunasan),0) FROM ISC064_MARKETINGJUAL..MS_PELUNASAN
			WHERE NoKontrak = @NoKontrak AND NoTagihan = MS_TAGIHAN.NoUrut
		) > 0 --kurang bayar
	)
	
	BEGIN
	
		--NoPJT
		DECLARE @jum_pjt int
		SELECT @jum_pjt = COUNT(*) FROM MS_PJT WHERE YEAR(TglPJT) = YEAR(@TglPJT)	
		DECLARE @NoPJT varchar(50)
		DECLARE @hasfound bit = 0
		WHILE(@hasfound = 0)
		BEGIN
			SET @jum_pjt += 1
			SET @NoPJT = CONVERT(VARCHAR,@jum_pjt,1) + '/' + @NoUnit + '/' + CONVERT(VARCHAR,YEAR(@TglPJT),1)
			DECLARE @jum int
			SELECT @jum = COUNT(*) FROM MS_PJT WHERE NoPJT = @NoPJT
			if(@jum = 0)
				SET @hasfound = 1
		END	
		
		EXEC spPJTRegistrasi
			@NoPJT
			,@TglPJT
			,@AsOf
			,'JUAL'
			,@NoKontrak
			,@NoUnit
			,@Nama
			,@NoTelp
			,@Alamat1
			,@Alamat2
			,@Alamat3
			
		DECLARE
			@NoTagihan int
			,@NamaTagihan varchar(100)
			,@Nilai money
			,@TglJT datetime
		
		DECLARE rsDetil CURSOR FOR
			SELECT NoUrut, NamaTagihan + ' (' +Tipe + ')'
			, NilaiTagihan -
				(	SELECT ISNULL(SUM(NilaiPelunasan),0) FROM ISC064_MARKETINGJUAL..MS_PELUNASAN
					WHERE NoKontrak = @NoKontrak AND NoTagihan = MS_TAGIHAN.NoUrut
				) AS NilaiTagihan
			, TglJT
			FROM ISC064_MARKETINGJUAL..MS_TAGIHAN AS MS_TAGIHAN
			WHERE NoKontrak = @NoKontrak
			--AND CONVERT(varchar, TglJT, 112) <= CONVERT(varchar, @AsOf, 112)
			AND MONTH(TglJT) = MONTH(DATEADD(m,1,@TglPJT))
			AND YEAR(TglJT) = YEAR(DATEADD(m,1,@TglPJT))
			AND NilaiTagihan -
				(	SELECT ISNULL(SUM(NilaiPelunasan),0) FROM ISC064_MARKETINGJUAL..MS_PELUNASAN
					WHERE NoKontrak = @NoKontrak AND NoTagihan = MS_TAGIHAN.NoUrut
				) > 0
			ORDER BY TglJT

		OPEN rsDetil
		FETCH NEXT FROM rsDetil INTO @NoTagihan, @NamaTagihan, @Nilai, @TglJT

		WHILE @@FETCH_STATUS = 0
		BEGIN
			UPDATE ISC064_MARKETINGJUAL..MS_TAGIHAN SET FlagPJT = 1
			WHERE NoUrut = @NoTagihan AND NoKontrak = @NoKontrak --kontrol pjt
			
			EXEC spPJTDetil
				@NoPJT
				,@NoTagihan
				,@NamaTagihan
				,@Nilai
				,@TglJT
				
			FETCH NEXT FROM rsDetil INTO @NoTagihan, @NamaTagihan, @Nilai, @TglJT
		END

		CLOSE rsDetil
		DEALLOCATE rsDetil
	END

	FETCH NEXT FROM rs INTO @NoKontrak, @NoUnit, @Nama, @NoTelp, @Alamat1, @Alamat2, @Alamat3
END
	
CLOSE rs
DEALLOCATE rs









GO
/****** Object:  StoredProcedure [dbo].[spAutoPJT_SB]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Invoice SB
Interval Otomatis : 1x24 Jam
*/

CREATE PROCEDURE [dbo].[spAutoPJT_SB]
AS

DECLARE
	 @NoKontrak varchar(50)
	,@NoUnit varchar(20)
	,@Nama varchar(100)
	,@NoTelp varchar(50)
	,@Alamat1 varchar(50)
	,@Alamat2 varchar(50)
	,@Alamat3 varchar(50)
	,@TglPJT datetime
	,@AsOf datetime

-- tanggal jatuh tempo adalah 1 minggu ke depan, untuk memberi waktu bagi collection untuk mempersiapkan proses notifikasi
SET @TglPJT = CONVERT(varchar, GETDATE(), 101)
SET @AsOf = DATEADD(d,7,@TglPJT)

-- apabila PJT sudah di-generate, maka proses diabaikan
IF EXISTS
(SELECT NoPJT FROM MS_PJT WHERE CONVERT(varchar, TglPJT, 112) = CONVERT(varchar, @TglPJT, 112) AND Tipe = 'SB')
	RETURN

DECLARE rs CURSOR FOR
	SELECT NoKontrak, NoUnit, Nama, NoTelp, Alamat1, Alamat2, Alamat3
	FROM	
		ISC064_MARKETINGSB..MS_KONTRAK AS MS_KONTRAK INNER JOIN ISC064_MARKETINGSB..MS_CUSTOMER AS MS_CUSTOMER
		ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer
	WHERE MS_KONTRAK.Status IN ('A','C')
	ORDER BY NoKontrak

OPEN rs
FETCH NEXT FROM rs INTO @NoKontrak, @NoUnit, @Nama, @NoTelp, @Alamat1, @Alamat2, @Alamat3

WHILE @@FETCH_STATUS = 0
BEGIN
	IF EXISTS(
	SELECT NoUrut FROM ISC064_MARKETINGSB..MS_TAGIHAN AS MS_TAGIHAN
	WHERE NoKontrak = @NoKontrak
	AND 
	(
		(CONVERT(varchar, TglJT, 112) <= CONVERT(varchar, @AsOf, 112) AND FlagPJT = 0) --skenario komputer mati
		OR
		(CONVERT(varchar, TglJT, 112) = CONVERT(varchar, @AsOf, 112)) --jatuh tempo natural
	)
	AND NilaiTagihan -
		(	SELECT ISNULL(SUM(NilaiPelunasan),0) FROM ISC064_MARKETINGSB..MS_PELUNASAN
			WHERE NoKontrak = @NoKontrak AND NoTagihan = MS_TAGIHAN.NoUrut
		) > 0  --kurang bayar
	)
	
	BEGIN
		EXEC spPJTRegistrasi
			@TglPJT
			,'SB'
			,@NoKontrak
			,@NoUnit
			,@Nama
			,@NoTelp
			,@Alamat1
			,@Alamat2
			,@Alamat3
			
		DECLARE
			 @NoPJT int
			,@NoTagihan int
			,@NamaTagihan varchar(100)
			,@Nilai money
			,@TglJT datetime
		
		SELECT @NoPJT = MAX(NoPJT) FROM MS_PJT
		
		DECLARE rsDetil CURSOR FOR
			SELECT NoUrut, NamaTagihan + ' (' +Tipe + ')'
			, NilaiTagihan -
				(	SELECT ISNULL(SUM(NilaiPelunasan),0) FROM ISC064_MARKETINGSB..MS_PELUNASAN
					WHERE NoKontrak = @NoKontrak AND NoTagihan = MS_TAGIHAN.NoUrut
				) AS NilaiTagihan
			, TglJT
			FROM ISC064_MARKETINGSB..MS_TAGIHAN AS MS_TAGIHAN
			WHERE NoKontrak = @NoKontrak
			AND CONVERT(varchar, TglJT, 112) <= CONVERT(varchar, @AsOf, 112)
			AND NilaiTagihan -
				(	SELECT ISNULL(SUM(NilaiPelunasan),0) FROM ISC064_MARKETINGSB..MS_PELUNASAN
					WHERE NoKontrak = @NoKontrak AND NoTagihan = MS_TAGIHAN.NoUrut
				) > 0
			ORDER BY TglJT

		OPEN rsDetil
		FETCH NEXT FROM rsDetil INTO @NoTagihan, @NamaTagihan, @Nilai, @TglJT

		WHILE @@FETCH_STATUS = 0
		BEGIN
			UPDATE ISC064_MARKETINGSB..MS_TAGIHAN SET FlagPJT = 1
			WHERE NoUrut = @NoTagihan --kontrol pjt
			
			EXEC spPJTDetil
				@NoPJT
				,@NoTagihan
				,@NamaTagihan
				,@Nilai
				,@TglJT
				
			FETCH NEXT FROM rsDetil INTO @NoTagihan, @NamaTagihan, @Nilai, @TglJT
		END

		CLOSE rsDetil
		DEALLOCATE rsDetil
	END

	FETCH NEXT FROM rs INTO @NoKontrak, @NoUnit, @Nama, @NoTelp, @Alamat1, @Alamat2, @Alamat3
END
	
CLOSE rs
DEALLOCATE rs









GO
/****** Object:  StoredProcedure [dbo].[spAutoPJT_SEWA]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Invoice SEWA
Interval Otomatis : 1x24 Jam
*/

CREATE PROCEDURE [dbo].[spAutoPJT_SEWA]
AS

DECLARE
	 @NoKontrak varchar(50)
	,@NoUnit varchar(20)
	,@Nama varchar(100)
	,@NoTelp varchar(50)
	,@Alamat1 varchar(50)
	,@Alamat2 varchar(50)
	,@Alamat3 varchar(50)
	,@TglPJT datetime
	,@AsOf datetime

-- tanggal jatuh tempo adalah 1 minggu ke depan, untuk memberi waktu bagi collection untuk mempersiapkan proses notifikasi
SET @TglPJT = CONVERT(varchar, GETDATE(), 101)
SET @AsOf = DATEADD(d,7,@TglPJT)

-- apabila PJT sudah di-generate, maka proses diabaikan
IF EXISTS
(SELECT NoPJT FROM MS_PJT WHERE CONVERT(varchar, TglPJT, 112) = CONVERT(varchar, @TglPJT, 112) AND Tipe = 'SEWA')
	RETURN

DECLARE rs CURSOR FOR
	SELECT NoKontrak, NoUnit, Nama, MS_CUSTOMER.NoTelp, Alamat1, Alamat2, Alamat3
	FROM	
		ISC064_MARKETINGSEWA..MS_KONTRAK AS MS_KONTRAK INNER JOIN ISC064_MARKETINGSEWA..MS_CUSTOMER AS MS_CUSTOMER
		ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer
	WHERE MS_KONTRAK.Status IN ('A','C')
	ORDER BY NoKontrak

OPEN rs
FETCH NEXT FROM rs INTO @NoKontrak, @NoUnit, @Nama, @NoTelp, @Alamat1, @Alamat2, @Alamat3

WHILE @@FETCH_STATUS = 0
BEGIN
	IF EXISTS(
	SELECT NoUrut FROM ISC064_MARKETINGSEWA..MS_TAGIHAN AS MS_TAGIHAN
	WHERE NoKontrak = @NoKontrak
	AND 
	(
		(CONVERT(varchar, TglJT, 112) <= CONVERT(varchar, @AsOf, 112) AND FlagPJT = 0) --skenario komputer mati
		OR
		(CONVERT(varchar, TglJT, 112) = CONVERT(varchar, @AsOf, 112)) --jatuh tempo natural
	)
	AND NilaiTagihan -
		(	SELECT ISNULL(SUM(NilaiPelunasan),0) FROM ISC064_MARKETINGSEWA..MS_PELUNASAN
			WHERE NoKontrak = @NoKontrak AND NoTagihan = MS_TAGIHAN.NoUrut
		) > 0 --kurang bayar
	)
	
	BEGIN
		EXEC spPJTRegistrasi
			@TglPJT
			,'SEWA'
			,@NoKontrak
			,@NoUnit
			,@Nama
			,@NoTelp
			,@Alamat1
			,@Alamat2
			,@Alamat3
			
		DECLARE
			 @NoPJT int
			,@NoTagihan int
			,@NamaTagihan varchar(100)
			,@Nilai money
			,@TglJT datetime
		
		SELECT @NoPJT = MAX(NoPJT) FROM MS_PJT
		
		DECLARE rsDetil CURSOR FOR
			SELECT NoUrut, NamaTagihan + ' (' +Tipe + ')'
			, NilaiTagihan -
				(	SELECT ISNULL(SUM(NilaiPelunasan),0) FROM ISC064_MARKETINGSEWA..MS_PELUNASAN
					WHERE NoKontrak = @NoKontrak AND NoTagihan = MS_TAGIHAN.NoUrut
				) AS NilaiTagihan
			, TglJT
			FROM ISC064_MARKETINGSEWA..MS_TAGIHAN AS MS_TAGIHAN
			WHERE NoKontrak = @NoKontrak
			AND CONVERT(varchar, TglJT, 112) <= CONVERT(varchar, @AsOf, 112)
			AND NilaiTagihan -
				(	SELECT ISNULL(SUM(NilaiPelunasan),0) FROM ISC064_MARKETINGSEWA..MS_PELUNASAN
					WHERE NoKontrak = @NoKontrak AND NoTagihan = MS_TAGIHAN.NoUrut
				) > 0
			ORDER BY TglJT

		OPEN rsDetil
		FETCH NEXT FROM rsDetil INTO @NoTagihan, @NamaTagihan, @Nilai, @TglJT

		WHILE @@FETCH_STATUS = 0
		BEGIN
			UPDATE ISC064_MARKETINGSEWA..MS_TAGIHAN SET FlagPJT = 1
			WHERE NoUrut = @NoTagihan --kontrol pjt
			
			EXEC spPJTDetil
				@NoPJT
				,@NoTagihan
				,@NamaTagihan
				,@Nilai
				,@TglJT
				
			FETCH NEXT FROM rsDetil INTO @NoTagihan, @NamaTagihan, @Nilai, @TglJT
		END

		CLOSE rsDetil
		DEALLOCATE rsDetil
	END

	FETCH NEXT FROM rs INTO @NoKontrak, @NoUnit, @Nama, @NoTelp, @Alamat1, @Alamat2, @Alamat3
END
	
CLOSE rs
DEALLOCATE rs









GO
/****** Object:  StoredProcedure [dbo].[spAutoPJT_TENANT]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Invoice TENANT
Interval Otomatis : 1x24 Jam
*/

CREATE PROCEDURE [dbo].[spAutoPJT_TENANT]
AS

DECLARE
	 @NoPenghuni varchar(20)
	,@NoUnit varchar(20)
	,@Nama varchar(100)
	,@NoTelp varchar(50)
	,@Alamat1 varchar(50)
	,@Alamat2 varchar(50)
	,@Alamat3 varchar(50)
	,@TglPJT datetime
	,@AsOf datetime

-- tanggal jatuh tempo adalah 1 minggu ke depan, untuk memberi waktu bagi collection untuk mempersiapkan proses notifikasi
SET @TglPJT = CONVERT(varchar, GETDATE(), 101)
SET @AsOf = DATEADD(d,7,@TglPJT)

-- apabila PJT sudah di-generate, maka proses diabaikan
IF EXISTS
(SELECT NoPJT FROM MS_PJT WHERE CONVERT(varchar, TglPJT, 112) = CONVERT(varchar, @TglPJT, 112) AND Tipe = 'TENANT')
	RETURN

DECLARE rs CURSOR FOR
	SELECT NoPenghuni, NoUnit, Nama, NoTelp, Alamat1, Alamat2, Alamat3
	FROM ISC064_TENANT..MS_PENGHUNI
	ORDER BY NoPenghuni

OPEN rs
FETCH NEXT FROM rs INTO @NoPenghuni, @NoUnit, @Nama, @NoTelp, @Alamat1, @Alamat2, @Alamat3

WHILE @@FETCH_STATUS = 0
BEGIN
	IF EXISTS(
	SELECT NoUrut FROM ISC064_TENANT..MS_TAGIHAN
	WHERE NoPenghuni = @NoPenghuni
	AND 
		(
			(CONVERT(varchar, TglJT, 112) <= CONVERT(varchar, @AsOf, 112) AND FlagPJT = 0) --skenario komputer mati
			OR
			(CONVERT(varchar, TglJT, 112) = CONVERT(varchar, @AsOf, 112)) --jatuh tempo natural
		)
	AND CaraBayar = ''
	)
	BEGIN
		EXEC spPJTRegistrasi
			@TglPJT
			,'TENANT'
			,@NoPenghuni
			,@NoUnit
			,@Nama
			,@NoTelp
			,@Alamat1
			,@Alamat2
			,@Alamat3
			
		DECLARE
			@NoPJT int
			,@NoTagihan varchar(50)
			,@NamaTagihan varchar(100)
			,@Nilai money
			,@TglJT datetime
		
		SELECT @NoPJT = MAX(NoPJT) FROM MS_PJT
		
		DECLARE rsDetil CURSOR FOR
			SELECT Tipe + '.' + CONVERT(varchar,NoUrut), NamaTagihan + ' (' +Tipe + ')', NilaiTagihan, TglJT
			FROM ISC064_TENANT..MS_TAGIHAN
			WHERE NoPenghuni = @NoPenghuni
			AND CONVERT(varchar, TglJT, 112) <= CONVERT(varchar, @AsOf, 112)
			AND CaraBayar = ''
			ORDER BY TglJT

		OPEN rsDetil
		FETCH NEXT FROM rsDetil INTO @NoTagihan, @NamaTagihan, @Nilai, @TglJT

		WHILE @@FETCH_STATUS = 0
		BEGIN
			UPDATE ISC064_TENANT..MS_TAGIHAN SET FlagPJT = 1
			WHERE Tipe + '.' + CONVERT(VARCHAR,NoUrut) = @NoTagihan --kontrol pjt
			
			EXEC spPJTDetil
				@NoPJT
				,@NoTagihan
				,@NamaTagihan
				,@Nilai
				,@TglJT
				
			FETCH NEXT FROM rsDetil INTO @NoTagihan, @NamaTagihan, @Nilai, @TglJT
		END

		CLOSE rsDetil
		DEALLOCATE rsDetil
	END

	FETCH NEXT FROM rs INTO @NoPenghuni, @NoUnit, @Nama, @NoTelp, @Alamat1, @Alamat2, @Alamat3
END
	
CLOSE rs
DEALLOCATE rs









GO
/****** Object:  StoredProcedure [dbo].[spAutoTunggakan_JUAL]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Tunggakan JUAL
Interval Otomatis : 1x24 Jam
*/

CREATE PROCEDURE [dbo].[spAutoTunggakan_JUAL]
AS

DECLARE
	 @NoKontrak varchar(50)
	,@NoUnit varchar(20)
	,@Nama varchar(100)
	,@NoTelp varchar(50)
	,@Alamat1 varchar(50)
	,@Alamat2 varchar(50)
	,@Alamat3 varchar(50)
	,@TglTunggakan datetime

SET @TglTunggakan = CONVERT(varchar, GETDATE(), 101)

-- apabila TUNGGAKAN sudah di-generate, maka proses diabaikan
IF EXISTS
(SELECT NoTunggakan FROM MS_TUNGGAKAN WHERE CONVERT(varchar, TglTunggakan, 112) = CONVERT(varchar, @TglTunggakan, 112) AND Tipe = 'JUAL')
	RETURN

DECLARE rs CURSOR FOR
	SELECT NoKontrak, NoUnit, Nama, NoTelp, Alamat1, Alamat2, Alamat3
	FROM	
		ISC064_MARKETINGJUAL..MS_KONTRAK AS MS_KONTRAK INNER JOIN ISC064_MARKETINGJUAL..MS_CUSTOMER AS MS_CUSTOMER
		ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer
	WHERE MS_KONTRAK.Status = 'A'
	ORDER BY NoKontrak

OPEN rs
FETCH NEXT FROM rs INTO @NoKontrak, @NoUnit, @Nama, @NoTelp, @Alamat1, @Alamat2, @Alamat3

WHILE @@FETCH_STATUS = 0
BEGIN
	IF EXISTS(
	SELECT NoUrut FROM ISC064_MARKETINGJUAL..MS_TAGIHAN AS MS_TAGIHAN
	WHERE NoKontrak = @NoKontrak
	AND DATEDIFF(d, TglJT, @TglTunggakan) > 30
	AND NilaiTagihan -
		(	SELECT ISNULL(SUM(NilaiPelunasan),0) FROM ISC064_MARKETINGJUAL..MS_PELUNASAN
			WHERE NoKontrak = @NoKontrak AND NoTagihan = MS_TAGIHAN.NoUrut
		) > 0 --kurang bayar
	)
	
	BEGIN
		EXEC spTunggakanRegistrasi
			 @TglTunggakan
			,'JUAL'
			,@NoKontrak
			,@NoUnit
			,@Nama
			,@NoTelp
			,@Alamat1
			,@Alamat2
			,@Alamat3
			
		DECLARE
			 @NoTunggakan int
			,@NoTagihan int
			,@NamaTagihan varchar(100)
			,@Nilai money
			,@TglJT datetime
		
		SELECT @NoTunggakan = MAX(NoTunggakan) FROM MS_TUNGGAKAN
		
		DECLARE rsDetil CURSOR FOR
			SELECT NoUrut, NamaTagihan + ' (' +Tipe + ')'
			, NilaiTagihan -
				(	SELECT ISNULL(SUM(NilaiPelunasan),0) FROM ISC064_MARKETINGJUAL..MS_PELUNASAN
					WHERE NoKontrak = @NoKontrak AND NoTagihan = MS_TAGIHAN.NoUrut
				) AS NilaiTagihan
			, TglJT
			FROM ISC064_MARKETINGJUAL..MS_TAGIHAN AS MS_TAGIHAN
			WHERE
				NoKontrak = @NoKontrak
				AND DATEDIFF(d, TglJT, @TglTunggakan) > 30
				AND NilaiTagihan -
					(	SELECT ISNULL(SUM(NilaiPelunasan),0) FROM ISC064_MARKETINGJUAL..MS_PELUNASAN
						WHERE NoKontrak = @NoKontrak AND NoTagihan = MS_TAGIHAN.NoUrut
					) > 0
			ORDER BY TglJT

		OPEN rsDetil
		FETCH NEXT FROM rsDetil INTO @NoTagihan, @NamaTagihan, @Nilai, @TglJT

		WHILE @@FETCH_STATUS = 0
		BEGIN
			EXEC spTunggakanDetil
				 @NoTunggakan
				,@NoTagihan
				,@NamaTagihan
				,@Nilai
				,@TglJT
				
			FETCH NEXT FROM rsDetil INTO @NoTagihan, @NamaTagihan, @Nilai, @TglJT
		END

		CLOSE rsDetil
		DEALLOCATE rsDetil
		
		EXEC spTunggakanUpgrade @NoTunggakan
	END

	FETCH NEXT FROM rs INTO @NoKontrak, @NoUnit, @Nama, @NoTelp, @Alamat1, @Alamat2, @Alamat3
END

CLOSE rs
DEALLOCATE rs









GO
/****** Object:  StoredProcedure [dbo].[spAutoTunggakan_SB]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Tunggakan SB
Interval Otomatis : 1x24 Jam
*/

CREATE PROCEDURE [dbo].[spAutoTunggakan_SB]
AS

DECLARE
	 @NoKontrak varchar(50)
	,@NoUnit varchar(20)
	,@Nama varchar(100)
	,@NoTelp varchar(50)
	,@Alamat1 varchar(50)
	,@Alamat2 varchar(50)
	,@Alamat3 varchar(50)
	,@TglTunggakan datetime

SET @TglTunggakan = CONVERT(varchar, GETDATE(), 101)

-- apabila TUNGGAKAN sudah di-generate, maka proses diabaikan
IF EXISTS
(SELECT NoTunggakan FROM MS_TUNGGAKAN WHERE CONVERT(varchar, TglTunggakan, 112) = CONVERT(varchar, @TglTunggakan, 112) AND Tipe = 'SB')
	RETURN

DECLARE rs CURSOR FOR
	SELECT NoKontrak, NoUnit, Nama, NoTelp, Alamat1, Alamat2, Alamat3
	FROM	
		ISC064_MARKETINGSB..MS_KONTRAK AS MS_KONTRAK INNER JOIN ISC064_MARKETINGSB..MS_CUSTOMER AS MS_CUSTOMER
		ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer
	WHERE MS_KONTRAK.Status IN ('A','C')
	ORDER BY NoKontrak

OPEN rs
FETCH NEXT FROM rs INTO @NoKontrak, @NoUnit, @Nama, @NoTelp, @Alamat1, @Alamat2, @Alamat3

WHILE @@FETCH_STATUS = 0
BEGIN
	IF EXISTS(
	SELECT NoUrut FROM ISC064_MARKETINGSB..MS_TAGIHAN AS MS_TAGIHAN
	WHERE NoKontrak = @NoKontrak
	AND DATEDIFF(d, TglJT, @TglTunggakan) > 30
	AND NilaiTagihan -
		(	SELECT ISNULL(SUM(NilaiPelunasan),0) FROM ISC064_MARKETINGSB..MS_PELUNASAN
			WHERE NoKontrak = @NoKontrak AND NoTagihan = MS_TAGIHAN.NoUrut
		) > 0 --kurang bayar
	)
	
	BEGIN
		EXEC spTunggakanRegistrasi
			 @TglTunggakan
			,'SB'
			,@NoKontrak
			,@NoUnit
			,@Nama
			,@NoTelp
			,@Alamat1
			,@Alamat2
			,@Alamat3
			
		DECLARE
			 @NoTunggakan int
			,@NoTagihan int
			,@NamaTagihan varchar(100)
			,@Nilai money
			,@TglJT datetime
		
		SELECT @NoTunggakan = MAX(NoTunggakan) FROM MS_TUNGGAKAN
		
		DECLARE rsDetil CURSOR FOR
			SELECT NoUrut, NamaTagihan + ' (' +Tipe + ')'
			, NilaiTagihan -
				(	SELECT ISNULL(SUM(NilaiPelunasan),0) FROM ISC064_MARKETINGSB..MS_PELUNASAN
					WHERE NoKontrak = @NoKontrak AND NoTagihan = MS_TAGIHAN.NoUrut
				) AS NilaiTagihan
			, TglJT
			FROM ISC064_MARKETINGSB..MS_TAGIHAN AS MS_TAGIHAN
			WHERE
				NoKontrak = @NoKontrak
				AND DATEDIFF(d, TglJT, @TglTunggakan) > 30
				AND NilaiTagihan -
					(	SELECT ISNULL(SUM(NilaiPelunasan),0) FROM ISC064_MARKETINGSB..MS_PELUNASAN
						WHERE NoKontrak = @NoKontrak AND NoTagihan = MS_TAGIHAN.NoUrut
					) > 0
			ORDER BY TglJT

		OPEN rsDetil
		FETCH NEXT FROM rsDetil INTO @NoTagihan, @NamaTagihan, @Nilai, @TglJT

		WHILE @@FETCH_STATUS = 0
		BEGIN
			EXEC spTunggakanDetil
				 @NoTunggakan
				,@NoTagihan
				,@NamaTagihan
				,@Nilai
				,@TglJT
				
			FETCH NEXT FROM rsDetil INTO @NoTagihan, @NamaTagihan, @Nilai, @TglJT
		END

		CLOSE rsDetil
		DEALLOCATE rsDetil
		
		EXEC spTunggakanUpgrade @NoTunggakan
	END

	FETCH NEXT FROM rs INTO @NoKontrak, @NoUnit, @Nama, @NoTelp, @Alamat1, @Alamat2, @Alamat3
END

CLOSE rs
DEALLOCATE rs









GO
/****** Object:  StoredProcedure [dbo].[spAutoTunggakan_SEWA]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Tunggakan SEWA
Interval Otomatis : 1x24 Jam
*/

CREATE PROCEDURE [dbo].[spAutoTunggakan_SEWA]
AS

DECLARE
	 @NoKontrak varchar(50)
	,@NoUnit varchar(20)
	,@Nama varchar(100)
	,@NoTelp varchar(50)
	,@Alamat1 varchar(50)
	,@Alamat2 varchar(50)
	,@Alamat3 varchar(50)
	,@TglTunggakan datetime

SET @TglTunggakan = CONVERT(varchar, GETDATE(), 101)

-- apabila TUNGGAKAN sudah di-generate, maka proses diabaikan
IF EXISTS
(SELECT NoTunggakan FROM MS_TUNGGAKAN WHERE CONVERT(varchar, TglTunggakan, 112) = CONVERT(varchar, @TglTunggakan, 112) AND Tipe = 'SEWA')
	RETURN

DECLARE rs CURSOR FOR
	SELECT NoKontrak, NoUnit, Nama, MS_CUSTOMER.NoTelp, Alamat1, Alamat2, Alamat3
	FROM	
		ISC064_MARKETINGSEWA..MS_KONTRAK AS MS_KONTRAK INNER JOIN ISC064_MARKETINGSEWA..MS_CUSTOMER AS MS_CUSTOMER
		ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer
	WHERE MS_KONTRAK.Status IN ('A','C')
	ORDER BY NoKontrak

OPEN rs
FETCH NEXT FROM rs INTO @NoKontrak, @NoUnit, @Nama, @NoTelp, @Alamat1, @Alamat2, @Alamat3

WHILE @@FETCH_STATUS = 0
BEGIN
	IF EXISTS(
	SELECT NoUrut FROM ISC064_MARKETINGSEWA..MS_TAGIHAN AS MS_TAGIHAN
	WHERE NoKontrak = @NoKontrak
	AND DATEDIFF(d, TglJT, @TglTunggakan) > 30
	AND NilaiTagihan -
		(	SELECT ISNULL(SUM(NilaiPelunasan),0) FROM ISC064_MARKETINGSEWA..MS_PELUNASAN
			WHERE NoKontrak = @NoKontrak AND NoTagihan = MS_TAGIHAN.NoUrut
		) > 0 --kurang bayar
	)
	
	BEGIN
		EXEC spTunggakanRegistrasi
			 @TglTunggakan
			,'SEWA'
			,@NoKontrak
			,@NoUnit
			,@Nama
			,@NoTelp
			,@Alamat1
			,@Alamat2
			,@Alamat3
			
		DECLARE
			 @NoTunggakan int
			,@NoTagihan int
			,@NamaTagihan varchar(100)
			,@Nilai money
			,@TglJT datetime
		
		SELECT @NoTunggakan = MAX(NoTunggakan) FROM MS_TUNGGAKAN
		
		DECLARE rsDetil CURSOR FOR
			SELECT NoUrut, NamaTagihan + ' (' +Tipe + ')'
			, NilaiTagihan -
				(	SELECT ISNULL(SUM(NilaiPelunasan),0) FROM ISC064_MARKETINGSEWA..MS_PELUNASAN
					WHERE NoKontrak = @NoKontrak AND NoTagihan = MS_TAGIHAN.NoUrut
				) AS NilaiTagihan
			, TglJT
			FROM ISC064_MARKETINGSEWA..MS_TAGIHAN AS MS_TAGIHAN
			WHERE
				NoKontrak = @NoKontrak
				AND DATEDIFF(d, TglJT, @TglTunggakan) > 30
				AND NilaiTagihan -
					(	SELECT ISNULL(SUM(NilaiPelunasan),0) FROM ISC064_MARKETINGSEWA..MS_PELUNASAN
						WHERE NoKontrak = @NoKontrak AND NoTagihan = MS_TAGIHAN.NoUrut
					) > 0
			ORDER BY TglJT

		OPEN rsDetil
		FETCH NEXT FROM rsDetil INTO @NoTagihan, @NamaTagihan, @Nilai, @TglJT

		WHILE @@FETCH_STATUS = 0
		BEGIN
			EXEC spTunggakanDetil
				 @NoTunggakan
				,@NoTagihan
				,@NamaTagihan
				,@Nilai
				,@TglJT
				
			FETCH NEXT FROM rsDetil INTO @NoTagihan, @NamaTagihan, @Nilai, @TglJT
		END

		CLOSE rsDetil
		DEALLOCATE rsDetil
		
		EXEC spTunggakanUpgrade @NoTunggakan
	END

	FETCH NEXT FROM rs INTO @NoKontrak, @NoUnit, @Nama, @NoTelp, @Alamat1, @Alamat2, @Alamat3
END

CLOSE rs
DEALLOCATE rs









GO
/****** Object:  StoredProcedure [dbo].[spAutoTunggakan_TENANT]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Tunggakan TENANT
Interval Otomatis : 1x24 Jam
*/

CREATE PROCEDURE [dbo].[spAutoTunggakan_TENANT]
AS

DECLARE
	 @NoPenghuni varchar(20)
	,@NoUnit varchar(20)
	,@Nama varchar(100)
	,@NoTelp varchar(50)
	,@Alamat1 varchar(50)
	,@Alamat2 varchar(50)
	,@Alamat3 varchar(50)
	,@TglTunggakan datetime

SET @TglTunggakan = CONVERT(varchar, GETDATE(), 101)

-- apabila TUNGGAKAN sudah di-generate, maka proses diabaikan
IF EXISTS
(SELECT NoTunggakan FROM MS_TUNGGAKAN WHERE CONVERT(varchar, TglTunggakan, 112) = CONVERT(varchar, @TglTunggakan, 112) AND Tipe = 'TENANT')
	RETURN

DECLARE rs CURSOR FOR
	SELECT NoPenghuni, NoUnit, Nama, NoTelp, Alamat1, Alamat2, Alamat3
	FROM ISC064_TENANT..MS_PENGHUNI
	ORDER BY NoPenghuni

OPEN rs
FETCH NEXT FROM rs INTO @NoPenghuni, @NoUnit, @Nama, @NoTelp, @Alamat1, @Alamat2, @Alamat3

WHILE @@FETCH_STATUS = 0
BEGIN
	IF EXISTS(
	SELECT NoUrut FROM ISC064_TENANT..MS_TAGIHAN
	WHERE NoPenghuni = @NoPenghuni
	AND DATEDIFF(d, TglJT, @TglTunggakan) > 30
	AND CaraBayar = ''
	)
	BEGIN
		EXEC spTunggakanRegistrasi
			 @TglTunggakan
			,'TENANT'
			,@NoPenghuni
			,@NoUnit
			,@Nama
			,@NoTelp
			,@Alamat1
			,@Alamat2
			,@Alamat3
			
		DECLARE
			 @NoTunggakan int
			,@NoTagihan varchar(50)
			,@NamaTagihan varchar(100)
			,@Nilai money
			,@TglJT datetime
		
		SELECT @NoTunggakan = MAX(NoTunggakan) FROM MS_TUNGGAKAN
		
		DECLARE rsDetil CURSOR FOR
			SELECT Tipe + '.' + CONVERT(varchar,NoUrut), NamaTagihan + ' (' +Tipe + ')', NilaiTagihan, TglJT
			FROM ISC064_TENANT..MS_TAGIHAN
			WHERE
				NoPenghuni = @NoPenghuni
				AND DATEDIFF(d, TglJT, @TglTunggakan) > 30
				AND CaraBayar = ''
			ORDER BY TglJT

		OPEN rsDetil
		FETCH NEXT FROM rsDetil INTO @NoTagihan, @NamaTagihan, @Nilai, @TglJT

		WHILE @@FETCH_STATUS = 0
		BEGIN
			EXEC spTunggakanDetil
				 @NoTunggakan
				,@NoTagihan
				,@NamaTagihan
				,@Nilai
				,@TglJT
				
			FETCH NEXT FROM rsDetil INTO @NoTagihan, @NamaTagihan, @Nilai, @TglJT
		END

		CLOSE rsDetil
		DEALLOCATE rsDetil
		
		EXEC spTunggakanUpgrade @NoTunggakan
	END

	FETCH NEXT FROM rs INTO @NoPenghuni, @NoUnit, @Nama, @NoTelp, @Alamat1, @Alamat2, @Alamat3
END
	
CLOSE rs
DEALLOCATE rs









GO
/****** Object:  StoredProcedure [dbo].[spBiayaTambahan]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[spBiayaTambahan]
	 @NoKontrak varchar (50)
	,@NoUrut int
	,@NamaTagihan varchar (50)
	,@TglJT datetime
	,@NilaiTagihan money
	,@Tipe varchar (3)
AS
--IF EXISTS(SELECT * FROM ISC064_MARKETINGJUAL..MS_TAGIHAN
--      WHERE NoKontrak  = @NoKontrak AND NoUrut = @NoUrut)
--   UPDATE ISC064_MARKETINGJUAL..MS_TAGIHAN SET TglJT = CONVERT(datetime, @TglJT, 101),
--   NilaiTagihan = @NilaiTagihan WHERE NoKontrak  = @NoKontrak AND NoUrut = @NoUrut
--INSERT INTO ISC064_MARKETINGJUAL..MS_TAGIHAN
--	(NoKontrak, NoUrut, NamaTagihan, TglJT, NilaiTagihan, Tipe)
--VALUES
--	(@NoKontrak,@NoUrut, @NamaTagihan, CONVERT(datetime, @TglJT, 101), @NilaiTagihan, @Tipe)
--SET QUOTED_IDENTIFIER OFF









GO
/****** Object:  StoredProcedure [dbo].[spCBRegistrasi]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/*
Transaksi penerimaan pembayaran di kasir
*/

CREATE PROCEDURE [dbo].[spCBRegistrasi]
	 @NoKontrak varchar(20),
	 @NoUrut int,
	 @Unit varchar (50),
	 @Customer varchar (100),
	 @TglPengembalian datetime,
	 @SisaTagihan money,
	 @LebihBayar money,
	 @Cashback money,
	 @BankKeluar money,
	 @Bank varchar(50),
	 @Tipe tinyint
AS

DECLARE @Nocb int
SELECT @Nocb = ISNULL(MAX(Nocb),0) + 1 FROM MS_CASHBACK

INSERT INTO MS_CASHBACK
(
	 Nocb,
	 NoKontrak,
	 NoUrut,
	 Unit,
	 Customer,
	 TglPengembalian,
	 SisaTagihan,
	 LebihBayar,
	 Cashback,
	 BankKeluar,
	 Bank,
	 Tipe
)
VALUES
(
	@Nocb,
	@NoKontrak,
	@NoUrut,
	@Unit,
	@Customer,
	CONVERT(datetime, @TglPengembalian, 101),
	@SisaTagihan,
	@LebihBayar,
	@Cashback,
	@BankKeluar,
	@Bank,
	@Tipe
)













GO
/****** Object:  StoredProcedure [dbo].[spFPRegis]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Pendaftaran account baru
*/

CREATE PROCEDURE [dbo].[spFPRegis]
	 @NoFPS varchar(50),
	 @Project varchar(20)
AS

INSERT INTO REF_FP
(
	 NoFPS,
	 Project
)
VALUES
(
	 @NoFPS,
	 @Project
)









GO
/****** Object:  StoredProcedure [dbo].[spJurnalPJT]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Jurnal PJT
*/

CREATE PROCEDURE [dbo].[spJurnalPJT]
	 @UserID varchar(20) = ''
	,@NoPJT varchar(50) = ''
	,@Ket text = ''
AS

INSERT INTO MS_PJT_JURNAL
(
	 UserID
	,NoPJT
	,Ket 
)
VALUES
(
	 @UserID
	,@NoPJT
	,@Ket 
)









GO
/****** Object:  StoredProcedure [dbo].[spJurnalTunggakan]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Jurnal surat tunggakan
*/

CREATE PROCEDURE [dbo].[spJurnalTunggakan]
	 @UserID varchar(20) = ''
	,@NoTunggakan int
	,@Ket text = ''
AS

INSERT INTO MS_TUNGGAKAN_JURNAL
(
	 UserID
	,NoTunggakan
	,Ket 
)
VALUES
(
	 @UserID
	,@NoTunggakan
	,@Ket 
)









GO
/****** Object:  StoredProcedure [dbo].[spKasKeluar]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spKasKeluar]
	@Tgl datetime,
	@Acc varchar(50),
	@CaraBayar varchar(2),
	@AlatBayar varchar(50),
	@DibayarKepada varchar(50),
	@Keterangan varchar(200),
	@Nilai money
AS

DECLARE @NoVoucher int
SELECT @NoVoucher = ISNULL(MAX(NoVoucher),0) + 1 FROM MS_KASKELUAR

INSERT INTO MS_KASKELUAR
(
	NoVoucher,
	Tgl,
	Acc,
	CaraBayar,
	AlatBayar,
	DibayarKepada,
	Keterangan,
	Nilai
)
VALUES
(
	@NoVoucher,
	CONVERT(datetime, @Tgl, 101),
	@Acc,
	@CaraBayar,
	@AlatBayar,
	@DibayarKepada,
	@Keterangan,
	@Nilai
)

RETURN @NoVoucher









GO
/****** Object:  StoredProcedure [dbo].[spKasKeluarEdit]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spKasKeluarEdit]
	@NoVoucher int,
	@Tgl datetime,
	@Acc varchar(50),
	@CaraBayar varchar(2),
	@AlatBayar varchar(50),
	@DibayarKepada varchar(50),
	@Keterangan varchar(200),
	@Nilai money
AS

UPDATE MS_KASKELUAR
SET
	 Tgl = CONVERT(datetime, @Tgl, 101)
	,Acc = @Acc
	,CaraBayar = @CaraBayar
	,AlatBayar = @AlatBayar
	,DibayarKepada = @DibayarKepada
	,Keterangan = @Keterangan
	,Nilai = @Nilai
WHERE
NoVoucher = @NoVoucher









GO
/****** Object:  StoredProcedure [dbo].[spKasMasuk]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spKasMasuk]
	@Tgl datetime,
	@Acc varchar(50),
	@CaraBayar varchar(2),
	@AlatBayar varchar(50),
	@DiterimaDari varchar(50),
	@Keterangan varchar(200),
	@Nilai money
AS

DECLARE @NoVoucher int
SELECT @NoVoucher = ISNULL(MAX(NoVoucher),0) + 1 FROM MS_KASMASUK

INSERT INTO MS_KASMASUK
(
	NoVoucher,
	Tgl,
	Acc,
	CaraBayar,
	AlatBayar,
	DiterimaDari,
	Keterangan,
	Nilai
)
VALUES
(
	@NoVoucher,
	CONVERT(datetime, @Tgl, 101),
	@Acc,
	@CaraBayar,
	@AlatBayar,
	@DiterimaDari,
	@Keterangan,
	@Nilai
)

RETURN @NoVoucher









GO
/****** Object:  StoredProcedure [dbo].[spKasMasukEdit]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spKasMasukEdit]
	@NoVoucher int,
	@Tgl datetime,
	@Acc varchar(50),
	@CaraBayar varchar(2),
	@AlatBayar varchar(50),
	@DiterimaDari varchar(50),
	@Keterangan varchar(200),
	@Nilai money
AS

UPDATE MS_KASMASUK
SET
	 Tgl = CONVERT(datetime, @Tgl, 101)
	,Acc = @Acc
	,CaraBayar = @CaraBayar
	,AlatBayar = @AlatBayar
	,DiterimaDari = @DiterimaDari
	,Keterangan = @Keterangan
	,Nilai = @Nilai
WHERE
NoVoucher = @NoVoucher









GO
/****** Object:  StoredProcedure [dbo].[spLapPDFDaftar]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




/*
Pendaftaran customer baru
*/

CREATE PROCEDURE [dbo].[spLapPDFDaftar]
	--@AttachmentID varchar(100) = ''
	 @Nama varchar (100) = ''
	,@Link varchar(MAX) = ''
	,@TglGenerate datetime
	,@IP varchar (50) = ''
	,@UserID varchar (20) = ''
	,@FileName varchar (100) = ''
	,@FileType varchar (100) = ''
	--,@FileSize decimal(18, 2) = ''
	,@FilterDari datetime
	,@FilterSampai datetime
AS

--DECLARE @AttachmentID int
--SELECT @AttachmentID = ISNULL(MAX(AttachmentID),0) + 1 FROM LapPDF

INSERT INTO LapPDF
(
	--AttachmentID
	Nama
	,Link
	,TglGenerate
	,IP
	,UserID
	,FileName
	,FileType
	--,FileSize
	,FilterDari
	,FilterSampai
)
VALUES
(
	--@AttachmentID
	@Nama
	,@Link
	,@TglGenerate
	,@IP
	,@UserID
	,@FileName
	,@FileType
	--,@FileSize
	,@FilterDari
	,@FilterSampai
)










GO
/****** Object:  StoredProcedure [dbo].[spLogAcc]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Logfile Account
*/

CREATE PROCEDURE [dbo].[spLogAcc]
	 @Aktivitas varchar (6)
	,@UserID varchar (20)
	,@IP varchar (50) = ''
	,@Ket text = ''
	,@Pk varchar (50) = ''
AS

INSERT INTO REF_ACC_LOG
(
	 Aktivitas
	,UserID
	,IP
	,Ket
	,Pk
)
VALUES
(
	 @Aktivitas
	,@UserID
	,@IP
	,@Ket
	,@Pk
)









GO
/****** Object:  StoredProcedure [dbo].[spLogAnonim]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Logfile Anonim
*/

CREATE PROCEDURE [dbo].[spLogAnonim]
	 @Aktivitas varchar (6)
	,@UserID varchar (20)
	,@IP varchar (50) = ''
	,@Ket text = ''
	,@Pk varchar (50) = ''
AS

INSERT INTO MS_ANONIM_LOG
(
	 Aktivitas
	,UserID
	,IP
	,Ket
	,Pk
)
VALUES
(
	 @Aktivitas
	,@UserID
	,@IP
	,@Ket
	,@Pk
)









GO
/****** Object:  StoredProcedure [dbo].[spLogCashback]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



/*
Logfile TTS
*/

CREATE PROCEDURE [dbo].[spLogCashback]
	 @Aktivitas varchar (6)
	,@UserID varchar (20)
	,@IP varchar (50) = ''
	,@Ket text = ''
	,@Pk varchar (50) = ''
AS

INSERT INTO MS_CASHBACK_LOG
(
	 Aktivitas
	,UserID
	,IP
	,Ket
	,Pk
)
VALUES
(
	 @Aktivitas
	,@UserID
	,@IP
	,@Ket
	,@Pk
)













GO
/****** Object:  StoredProcedure [dbo].[spLogFP]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Logfile Account
*/

CREATE PROCEDURE [dbo].[spLogFP]
	 @Aktivitas varchar (6)
	,@UserID varchar (20)
	,@IP varchar (50) = ''
	,@Ket text = ''
	,@Pk varchar (50) = ''
AS

INSERT INTO REF_FP_LOG
(
	 Aktivitas
	,UserID
	,IP
	,Ket
	,Pk
)
VALUES
(
	 @Aktivitas
	,@UserID
	,@IP
	,@Ket
	,@Pk
)









GO
/****** Object:  StoredProcedure [dbo].[spLogKasKeluar]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Logfile Kas Keluar
*/

CREATE PROCEDURE [dbo].[spLogKasKeluar]
	 @Aktivitas varchar (6)
	,@UserID varchar (20)
	,@IP varchar (50) = ''
	,@Ket text = ''
	,@Pk varchar (50) = ''
AS

INSERT INTO MS_KASKELUAR_LOG
(
	 Aktivitas
	,UserID
	,IP
	,Ket
	,Pk
)
VALUES
(
	 @Aktivitas
	,@UserID
	,@IP
	,@Ket
	,@Pk
)









GO
/****** Object:  StoredProcedure [dbo].[spLogKasMasuk]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Logfile Kas Masuk
*/

CREATE PROCEDURE [dbo].[spLogKasMasuk]
	 @Aktivitas varchar (6)
	,@UserID varchar (20)
	,@IP varchar (50) = ''
	,@Ket text = ''
	,@Pk varchar (50) = ''
AS

INSERT INTO MS_KASMASUK_LOG
(
	 Aktivitas
	,UserID
	,IP
	,Ket
	,Pk
)
VALUES
(
	 @Aktivitas
	,@UserID
	,@IP
	,@Ket
	,@Pk
)









GO
/****** Object:  StoredProcedure [dbo].[spLogMEMO]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Logfile MEMO
*/

CREATE PROCEDURE [dbo].[spLogMEMO]
	 @Aktivitas varchar (6)
	,@UserID varchar (20)
	,@IP varchar (50) = ''
	,@Ket text = ''
	,@Pk varchar (50) = ''
AS

INSERT INTO MS_MEMO_LOG
(
	 Aktivitas
	,UserID
	,IP
	,Ket
	,Pk
)
VALUES
(
	 @Aktivitas
	,@UserID
	,@IP
	,@Ket
	,@Pk
)









GO
/****** Object:  StoredProcedure [dbo].[spLogMemoCashback]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



/*
Logfile TTS
*/

CREATE PROCEDURE [dbo].[spLogMemoCashback]
	 @Aktivitas varchar (6)
	,@UserID varchar (20)
	,@IP varchar (50) = ''
	,@Ket text = ''
	,@Pk varchar (50) = ''
AS

INSERT INTO MS_CASHBACK_MEMO_LOG
(
	 Aktivitas
	,UserID
	,IP
	,Ket
	,Pk
)
VALUES
(
	 @Aktivitas
	,@UserID
	,@IP
	,@Ket
	,@Pk
)













GO
/****** Object:  StoredProcedure [dbo].[spLogPengajuanKPA]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Logfile TTS
*/

CREATE PROCEDURE [dbo].[spLogPengajuanKPA]
	 @Aktivitas varchar (6)
	,@UserID varchar (20)
	,@IP varchar (50) = ''
	,@Ket text = ''
	,@Pk varchar (50) = ''
AS

INSERT INTO MS_PENGAJUAN_KPA_LOG
(
	 Aktivitas
	,UserID
	,IP
	,Ket
	,Pk
)
VALUES
(
	 @Aktivitas
	,@UserID
	,@IP
	,@Ket
	,@Pk
)










GO
/****** Object:  StoredProcedure [dbo].[spLogPJT]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Logfile PJT
*/

CREATE PROCEDURE [dbo].[spLogPJT]
	 @Aktivitas varchar (6)
	,@UserID varchar (20)
	,@IP varchar (50) = ''
	,@Ket text = ''
	,@Pk varchar (50) = ''
AS

INSERT INTO MS_PJT_LOG
(
	 Aktivitas
	,UserID
	,IP
	,Ket
	,Pk
)
VALUES
(
	 @Aktivitas
	,@UserID
	,@IP
	,@Ket
	,@Pk
)









GO
/****** Object:  StoredProcedure [dbo].[spLogRealKPA]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Logfile TTS
*/

CREATE PROCEDURE [dbo].[spLogRealKPA]
	 @Aktivitas varchar (6)
	,@UserID varchar (20)
	,@IP varchar (50) = ''
	,@Ket text = ''
	,@Pk varchar (50) = ''
AS

INSERT INTO MS_REAL_KPA_LOG
(
	 Aktivitas
	,UserID
	,IP
	,Ket
	,Pk
)
VALUES
(
	 @Aktivitas
	,@UserID
	,@IP
	,@Ket
	,@Pk
)










GO
/****** Object:  StoredProcedure [dbo].[spLogSKL]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Logfile PJT
*/

CREATE PROCEDURE [dbo].[spLogSKL]
	@Tgl datetime
	,@Aktivitas varchar (10)
	,@UserID varchar (20)
	,@IP varchar (50) = ''
	,@Pk varchar (50) = ''
	,@Ket text
AS

INSERT INTO MS_SKL_LOG
(
	 Tgl
	,Aktivitas
	,UserID
	,IP
	,Pk
	,Ket
)
VALUES
(
	 @Tgl
	,@Aktivitas
	,@UserID
	,@IP
	,@Pk
	,@Ket
)









GO
/****** Object:  StoredProcedure [dbo].[spLogTTS]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Logfile TTS
*/

CREATE PROCEDURE [dbo].[spLogTTS]
	 @Aktivitas varchar (6)
	,@UserID varchar (20)
	,@IP varchar (50) = ''
	,@Ket text = ''
	,@Pk varchar (50) = ''
AS

INSERT INTO MS_TTS_LOG
(
	 Aktivitas
	,UserID
	,IP
	,Ket
	,Pk
)
VALUES
(
	 @Aktivitas
	,@UserID
	,@IP
	,@Ket
	,@Pk
)









GO
/****** Object:  StoredProcedure [dbo].[spLogTunggakan]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Logfile Tunggakan
*/

CREATE PROCEDURE [dbo].[spLogTunggakan]
	 @Aktivitas varchar (6)
	,@UserID varchar (20)
	,@IP varchar (50) = ''
	,@Ket text = ''
	,@Pk varchar (50) = ''
AS

INSERT INTO MS_TUNGGAKAN_LOG
(
	 Aktivitas
	,UserID
	,IP
	,Ket
	,Pk
)
VALUES
(
	 @Aktivitas
	,@UserID
	,@IP
	,@Ket
	,@Pk
)









GO
/****** Object:  StoredProcedure [dbo].[spLogVA]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Logfile Account
*/

CREATE PROCEDURE [dbo].[spLogVA]
	 @Aktivitas varchar (6)
	,@UserID varchar (20)
	,@IP varchar (50) = ''
	,@Ket text = ''
	,@Pk varchar (50) = ''
AS

INSERT INTO REF_VA_LOG
(
	 Aktivitas
	,UserID
	,IP
	,Ket
	,Pk
)
VALUES
(
	 @Aktivitas
	,@UserID
	,@IP
	,@Ket
	,@Pk
)









GO
/****** Object:  StoredProcedure [dbo].[spMEMOAlokasi]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
Alokasi MEMO ke tagihan di tabel marketing
*/

CREATE PROCEDURE [dbo].[spMEMOAlokasi]
	 @NoMEMO int
	,@NoTagihan varchar(50)
	,@Nilai money
AS

DECLARE
	 @Status varchar(4)
	,@Tipe varchar(6)
	,@Ref varchar(50)
	,@CaraBayar varchar(2)
	,@TglMEMO datetime

SELECT
	 @Status = Status
	,@Tipe = Tipe
	,@Ref = Ref
	,@CaraBayar = CaraBayar
	,@TglMEMO = TglMEMO
FROM MS_MEMO WHERE NoMEMO = @NoMEMO

-- validasi status
IF @Status <> 'BARU'
	RETURN

-- hitung total
UPDATE MS_MEMO SET Total = Total + @Nilai WHERE NoMEMO = @NoMEMO

-- POSTING ke Marketing
IF @Tipe = 'JUAL'
	EXEC ISC064_MARKETINGJUAL..spPelunasanMEMO @Ref,@NoTagihan,@Nilai,@NoMEMO
--IF @Tipe = 'SEWA'
--	EXEC ISC064_MARKETINGSEWA..spPelunasanMEMO @Ref,@NoTagihan,@Nilai,@NoMEMO
--IF @Tipe = 'SB'
--	EXEC ISC064_MARKETINGSB..spPelunasanMEMO @Ref,@NoTagihan,@Nilai,@NoMEMO

-- POSTING ke Tenant
--IF @Tipe = 'TENANT'
--BEGIN
--	DECLARE
--	 	 @TagihanTipe varchar(3)
--		,@TagihanNoUrut int
	
--	SET @TagihanTipe = SUBSTRING(@NoTagihan, 1 , CHARINDEX('.',@NoTagihan)-1)
--	SET @TagihanNoUrut = SUBSTRING(@NoTagihan, CHARINDEX('.',@NoTagihan)+1, LEN(@NoTagihan))
	
--	EXEC ISC064_TENANT..spPelunasan
--		 @TagihanTipe
--		,@TagihanNoUrut
--		,@CaraBayar
--		,@TglMEMO
--		,@NoMEMO
--		,@Nilai
--END

-- Edit data di tabel front end
EXEC spSinkronisasiMEMO @NoMEMO










GO
/****** Object:  StoredProcedure [dbo].[spMEMOCBRegistrasi]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spMEMOCBRegistrasi]
	-- Add the parameters for the stored procedure here
	 @NoKontrak varchar(20),
	 @NoUrut int,
	 @Unit varchar (50),
	 @Customer varchar (100),
	 @TglPengembalian datetime,
	 @SisaTagihan money,
	 @LebihBayar money	
AS

DECLARE @NoMemo int
SELECT @NoMemo = ISNULL(MAX(NoMemo),0) + 1 FROM MS_CASHBACK_MEMO

BEGIN
INSERT INTO MS_CASHBACK_MEMO
(
	 NoMemo,
	 NoKontrak,
	 NoUrut,
	 Unit,
	 Customer,
	 TglPengembalian,
	 SisaTagihan,
	 LebihBayar
)
VALUES
(
	@NoMemo,
	@NoKontrak,
	@NoUrut,
	@Unit,
	@Customer,
	CONVERT(datetime, @TglPengembalian, 101),
	@SisaTagihan,
	@LebihBayar
)
END





GO
/****** Object:  StoredProcedure [dbo].[spMEMOEdit]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Edit MEMO
*/

CREATE PROCEDURE [dbo].[spMEMOEdit]
	 @NoMEMO int
	,@TglMEMO datetime
	,@Unit varchar(100)
	,@Customer varchar(100)
	,@Ket varchar(200)
AS

DECLARE @CaraBayar varchar(2)
SELECT @CaraBayar = CaraBayar FROM MS_MEMO WHERE NoMEMO = @NoMEMO

UPDATE MS_MEMO
SET
	 TglMEMO = CONVERT(datetime, @TglMEMO, 101)
	,Unit = @Unit
	,Customer = @Customer
	,Ket = @Ket
WHERE
NoMEMO = @NoMEMO

-- Edit data di tabel front end
EXEC spSinkronisasiMEMO @NoMEMO









GO
/****** Object:  StoredProcedure [dbo].[spMEMORegistrasi]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Transaksi penerimaan pembayaran di kasir
*/

CREATE PROCEDURE [dbo].[spMEMORegistrasi]
	 @TglMEMO datetime
	,@UserID varchar(20)
	,@IP varchar(50)
	,@Tipe varchar(6)
	,@Ref varchar(50)
	,@Unit varchar(100)
	,@Customer varchar(100)
	,@CaraBayar varchar(2)
	,@Ket varchar(200)
	,@NoTTS int
AS

DECLARE @NoMEMO int
SELECT @NoMEMO = ISNULL(MAX(NoMEMO),0) + 1 FROM MS_MEMO

INSERT INTO MS_MEMO
(
	 NoMEMO
	,TglMEMO
	,UserID
	,IP
	,Tipe
	,Ref
	,Unit
	,Customer
	,CaraBayar
	,Ket
	,NoTTS
)
VALUES
(
	 @NoMEMO
	,CONVERT(datetime, @TglMEMO, 101)
	,@UserID
	,@IP
	,@Tipe
	,@Ref
	,@Unit
	,@Customer
	,@CaraBayar
	,@Ket
	,@NoTTS
)









GO
/****** Object:  StoredProcedure [dbo].[spMEMORegistrasiAlokasi]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spMEMORegistrasiAlokasi] 
	-- Add the parameters for the stored procedure here
	@TglMEMO datetime
	,@UserID varchar(20)
	,@IP varchar(50)
	,@Unit varchar(50)
	,@Customer varchar(50)
	,@Ref varchar(20)
	,@Ket varchar(200)
	
AS

DECLARE @NoMEMO int
SELECT @NoMEMO = ISNULL(MAX(NoMEMO),0) + 1 FROM MS_MEMO

BEGIN
INSERT INTO MS_MEMO
(
	 NoMEMO
	,TglMEMO
	,UserID
	,IP
	,Unit
	,Customer
	,Ref
	,Tipe
	,Status
	,CaraBayar
	,Ket
)
VALUES
(
	 @NoMEMO
	,CONVERT(datetime, @TglMEMO, 101)
	,@UserID
	,@IP
	,@Unit
	,@Customer
	,@Ref
	,'JUAL'
	,'POST'
	,'AL'
	,@Ket
)
grant Execute on spMEMORegistrasiAlokasi to batavianet
END





GO
/****** Object:  StoredProcedure [dbo].[spMEMOVoid]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Pembatalan penerimaan di kasir
*/

CREATE PROCEDURE [dbo].[spMEMOVoid]
	 @NoMEMO int
AS

DECLARE
	 @Status varchar(4)
	,@Tipe varchar(6)
SELECT
	 @Status = Status
	,@Tipe = Tipe
FROM MS_MEMO WHERE NoMEMO = @NoMEMO

-- validasi status
IF @Status = 'VOID'
	RETURN

UPDATE MS_MEMO SET
	 Total = 0
	,Status = 'VOID'
	,NoSlip = 0
WHERE NoMEMO = @NoMEMO

-- POSTING ke Marketing
IF @Tipe = 'JUAL'
	EXEC ISC064_MARKETINGJUAL..spPelunasanVoid @NoMEMO
IF @Tipe = 'SEWA'
	EXEC ISC064_MARKETINGSEWA..spPelunasanVoid @NoMEMO
IF @Tipe = 'SB'
	EXEC ISC064_MARKETINGSB..spPelunasanVoid @NoMEMO

-- POSTING ke Tenant
IF @Tipe = 'TENANT'
BEGIN
	DECLARE @TagihanTipe varchar(3) , @TagihanNoUrut int
	DECLARE rs CURSOR FOR SELECT Tipe,NoUrut FROM ISC064_TENANT..MS_TAGIHAN WHERE NoMEMO = @NoMEMO
	OPEN rs

	FETCH NEXT FROM rs INTO @TagihanTipe, @TagihanNoUrut
	WHILE @@FETCH_STATUS = 0
	BEGIN
		EXEC ISC064_TENANT..spPelunasanVoid @TagihanTipe, @TagihanNoUrut
		FETCH NEXT FROM rs INTO @TagihanTipe, @TagihanNoUrut
	END

	CLOSE rs
	DEALLOCATE rs
END









GO
/****** Object:  StoredProcedure [dbo].[spPengajuanDetil]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Transaksi penerimaan pembayaran di kasir
*/

CREATE PROCEDURE [dbo].[spPengajuanDetil]
	 @NoPengajuan int
	,@NoKontrak varchar(50)
	,@Nama varchar(100)
	,@NoUnit varchar(20)
	,@NoTagihan int
	,@NamaTagihan varchar(100)
	,@Nilai money
AS


INSERT INTO MS_PENGAJUAN_KPA_Detil
(
	NoPengajuan,
	NoKontrak,
	Nama,
	NoUnit,
	NoTagihan,
	NamaTagihan,
	Nilai
)
VALUES
(
	@NoPengajuan,
	@NoKontrak,
	@Nama,
	@NoUnit,
	@NoTagihan,
	@NamaTagihan,
	@Nilai

)










GO
/****** Object:  StoredProcedure [dbo].[spPengajuanEdit]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Edit PJT
*/

CREATE PROCEDURE [dbo].[spPengajuanEdit]
	 @NoPengajuan int
	,@TglFormulir datetime
	,@TglRencanacair datetime
	,@Keterangan varchar(MAX)
AS

UPDATE MS_PENGAJUAN_KPA
SET
	TglFormulir = @TglFormulir
	,TglRencanacair = @TglRencanacair
	,Keterangan = @Keterangan
	 
WHERE
NoPengajuan = @NoPengajuan










GO
/****** Object:  StoredProcedure [dbo].[spPengajuanRegistrasi]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Transaksi penerimaan pembayaran di kasir
*/

CREATE PROCEDURE [dbo].[spPengajuanRegistrasi]
	 @TglFormulir datetime
	,@TglRencanaCair datetime
	,@Keterangan varchar(MAX)
	,@Total money
	,@TglInput datetime
	,@UserID varchar(20)
	,@IP varchar(50)
AS


INSERT INTO MS_PENGAJUAN_KPA
(
	TglFormulir,
	TglRencanaCair,
	Keterangan,
	Total,
	TglInput,
	UserID,
	IP,
	Status
)
VALUES
(
	
	CONVERT(datetime, @TglFormulir, 101)
	,CONVERT(datetime, @TglRencanaCair, 101)
	,@Keterangan
	,@Total
	,CONVERT(datetime, @TglInput, 101)
	,@UserID
	,@IP
	,'BARU'
)










GO
/****** Object:  StoredProcedure [dbo].[spPJTDetil]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Detil tagihan di satu buah PJT
*/

CREATE PROCEDURE [dbo].[spPJTDetil]
	 @NoPJT varchar(50)
	,@NoTagihan varchar(50)
	,@NamaTagihan varchar(100)
	,@Nilai money
	,@TglJT datetime
AS

DECLARE @NoUrut int
SELECT @NoUrut = ISNULL(MAX(NoUrut),0) + 1 FROM MS_PJT_DETIL
WHERE NoPJT = @NoPJT

INSERT INTO MS_PJT_DETIL
(
	 NoPJT
	,NoUrut
	,NoTagihan
	,NamaTagihan
	,Nilai
	,TglJT
)
VALUES
(
	 @NoPJT
	,@NoUrut
	,@NoTagihan
	,@NamaTagihan
	,@Nilai
	,CONVERT(datetime, @TglJT, 101)
)

-- hitung total
UPDATE MS_PJT SET Total = Total + @Nilai WHERE NoPJT = @NoPJT









GO
/****** Object:  StoredProcedure [dbo].[spPJTEdit]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Edit PJT
*/

CREATE PROCEDURE [dbo].[spPJTEdit]
	 @NoPJT varchar(50)
	,@Unit varchar(100)
	,@Customer varchar(100)
	,@NoTelp varchar(50)
	,@Alamat1 varchar(50)
	,@Alamat2 varchar(50)
	,@Alamat3 varchar(50)
AS

UPDATE MS_PJT
SET
	 Unit = @Unit
	,Customer = @Customer
	,NoTelp = @NoTelp
	,Alamat1 = @Alamat1
	,Alamat2 = @Alamat2
	,Alamat3 = @Alamat3
WHERE
NoPJT = @NoPJT









GO
/****** Object:  StoredProcedure [dbo].[spPJTRegistrasi]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Proses mencetak surat PEMBERITAHUAN JATUH TEMPO
*/

CREATE PROCEDURE [dbo].[spPJTRegistrasi]
	 @NoPJT varchar(50)
	,@TglPJT datetime
	,@TglJT datetime
	,@Tipe varchar(6)
	,@Ref varchar(50)
	,@Unit varchar(100)
	,@Customer varchar(100)
	,@NoTelp varchar(50)
	,@Alamat1 varchar(50)
	,@Alamat2 varchar(50)
	,@Alamat3 varchar(50)
AS

INSERT INTO MS_PJT
(
	 NoPJT
	,TglPJT
	,TglJT
	,Tipe
	,Ref
	,Unit
	,Customer
	,NoTelp
	,Alamat1
	,Alamat2
	,Alamat3
)
VALUES
(
	 @NoPJT
	,CONVERT(datetime, @TglPJT, 101)
	,CONVERT(datetime, @TglJT, 101)
	,@Tipe
	,@Ref
	,@Unit
	,@Customer
	,@NoTelp
	,@Alamat1
	,@Alamat2
	,@Alamat3
)









GO
/****** Object:  StoredProcedure [dbo].[spPostingSlip]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Posting BKM atas dasar slip setoran
*/

CREATE PROCEDURE [dbo].[spPostingSlip]
	 @NoSlip int
	,@TglBKM datetime
AS

-- ambil semua TTS yang terkandung di dalam slip setoran dan di-posting masing-masing
DECLARE @NoTTS int

DECLARE rs CURSOR FOR
	SELECT NoTTS FROM MS_TTS WHERE NoSlip = @NoSlip
OPEN rs
FETCH NEXT FROM rs INTO @NoTTS

WHILE @@FETCH_STATUS = 0
BEGIN
	EXEC spPostingTTS @NoTTS, @TglBKM
	FETCH NEXT FROM rs INTO @NoTTS
END
	
CLOSE rs
DEALLOCATE rs









GO
/****** Object:  StoredProcedure [dbo].[spPostingSlipEdit]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Dinamika perubahan data setelah di-posting
*/

CREATE PROCEDURE [dbo].[spPostingSlipEdit]
	 @NoSlip int
	,@TglBKM datetime
AS

DECLARE @NoTTS int

DECLARE rs CURSOR FOR
	SELECT NoTTS FROM MS_TTS WHERE NoSlip = @NoSlip
OPEN rs
FETCH NEXT FROM rs INTO @NoTTS

WHILE @@FETCH_STATUS = 0
BEGIN
	EXEC spPostingTTSEdit @NoTTS, @TglBKM
	FETCH NEXT FROM rs INTO @NoTTS
END
	
CLOSE rs
DEALLOCATE rs









GO
/****** Object:  StoredProcedure [dbo].[spPostingSlipVoid]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Pembatalan posting BKM
*/

CREATE PROCEDURE [dbo].[spPostingSlipVoid]
	 @NoSlip int
AS

-- ambil semua TTS yang terkandung di dalam slip setoran dan di-posting masing-masing
DECLARE @NoTTS int

DECLARE rs CURSOR FOR
	SELECT NoTTS FROM MS_TTS WHERE NoSlip = @NoSlip
OPEN rs
FETCH NEXT FROM rs INTO @NoTTS

WHILE @@FETCH_STATUS = 0
BEGIN
	EXEC spPostingTTSVoid @NoTTS
	FETCH NEXT FROM rs INTO @NoTTS
END
	
CLOSE rs
DEALLOCATE rs









GO
/****** Object:  StoredProcedure [dbo].[spPostingTTS]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Posting BKM atas dasar tanda terima sementara
*/

CREATE PROCEDURE [dbo].[spPostingTTS]
	 @NoTTS int
	,@TglBKM datetime
AS

-- validasi status
DECLARE @Status varchar(4)
SELECT @Status = Status FROM MS_TTS WHERE NoTTS = @NoTTS
IF @Status <> 'BARU'
	RETURN

DECLARE @NoBKM int
SELECT @NoBKM = ISNULL(MAX(NoBKM),0) + 1 FROM MS_TTS

UPDATE MS_TTS SET
	 Status = 'POST'
	,NoBKM = @NoBKM
	,TglBKM = CONVERT(datetime, @TglBKM, 101)
WHERE NoTTS = @NoTTS

-- Edit data di tabel front end
EXEC spSinkronisasi @NoTTS









GO
/****** Object:  StoredProcedure [dbo].[spPostingTTSEdit]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Dinamika perubahan data setelah di-posting
*/

CREATE PROCEDURE [dbo].[spPostingTTSEdit]
	 @NoTTS int
	,@TglBKM datetime
AS

UPDATE MS_TTS SET
	TglBKM = CONVERT(datetime, @TglBKM, 101)
WHERE NoTTS = @NoTTS

-- Edit data di tabel front end
EXEC spSinkronisasi @NoTTS









GO
/****** Object:  StoredProcedure [dbo].[spPostingTTSVoid]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Pembatalan posting bukti kas masuk
*/

CREATE PROCEDURE [dbo].[spPostingTTSVoid]
	@NoTTS int
AS

-- validasi status
DECLARE @Status varchar(4)
SELECT @Status = Status FROM MS_TTS WHERE NoTTS = @NoTTS
IF @Status <> 'POST'
	RETURN

UPDATE MS_TTS SET
	 Status = 'BARU'
	,NoBKM = 0
	,TglBKM = Null
	,NoBKM2 = 0
WHERE NoTTS = @NoTTS

-- Edit data di tabel front end
EXEC spSinkronisasi @NoTTS









GO
/****** Object:  StoredProcedure [dbo].[spRealisasiKPA]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Transaksi penerimaan pembayaran di kasir
*/

CREATE PROCEDURE [dbo].[spRealisasiKPA]
	 @TglReal datetime
	,@UserID varchar(20)
	,@IP varchar(50)
	--,@Ref varchar(50)
	--,@Unit varchar(100)
	--,@Customer varchar(100)
	,@Ket varchar(MAX)
	,@Total money
	,@NoPengajuan int
AS


INSERT INTO MS_REAL_KPA
(
	TglReal
	,UserID
	,IP
	--,Ref
	--,Unit
	--,Customer
	,Ket
	,Total
	,TglInput
	,NoPengajuan
)
VALUES
(
	
	CONVERT(datetime, @TglReal, 101)
	,@UserID
	,@IP
	--,@Ref
	--,@Unit
	--,@Customer
	,@Ket
	,@Total
	,GETDATE()
	,@NoPengajuan
)










GO
/****** Object:  StoredProcedure [dbo].[spRealisasiKPAEdit]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Edit PJT
*/
CREATE PROCEDURE [dbo].[spRealisasiKPAEdit]
	 @NoReal int
	,@TglReal datetime
	,@Ket varchar(MAX)
AS

UPDATE MS_REAL_KPA
SET
	TglReal = @TglReal
	,Ket = @Ket
WHERE
NoReal = @NoReal









GO
/****** Object:  StoredProcedure [dbo].[spSinkronisasi]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Sinkronisasi antara tabel MKT dan FIN
*/

CREATE PROCEDURE [dbo].[spSinkronisasi]
	@NoTTS int
AS

DECLARE
	 @Tipe varchar(6)
	,@Ref varchar(50)
	,@CaraBayar varchar(2)
	,@Ket varchar(200)
	,@NoBG varchar(20)
	,@TglBG datetime
	,@TglTTS datetime
	,@TglBKM datetime
	,@Status varchar(4)
	,@StatusBG varchar(3)
	,@NoBKM int
	,@TglBayar datetime
	,@KetBayar varchar(255)
	,@SudahCair bit

SELECT
	 @Tipe = Tipe
	,@Ref = Ref
	,@CaraBayar = CaraBayar
	,@Ket = Ket
	,@NoBG = NoBG
	,@TglBG = TglBG
	,@TglTTS = TglTTS
	,@TglBKM = TglBKM
	,@Status = Status
	,@StatusBG = StatusBG
	,@NoBKM = NoBKM
FROM MS_TTS WHERE NoTTS = @NoTTS

-- keterangan pembayaran di customer information file
IF @CaraBayar = 'BG'
	SET @KetBayar = @Ket + ' ' + @NoBG + ' Tgl.' + CONVERT(varchar, @TglBG, 106)
ELSE
	SET @KetBayar = @Ket

-- Tolakan giro
IF @StatusBG = 'BAD'
	SET @KetBayar = '*TOLAK* ' + @KetBayar

-- tanggal pelunasan dan status pencairan
IF @Status = 'BARU' OR @Status = 'VOID'
	BEGIN
		SET @SudahCair = 0
		SET @TglBayar = @TglTTS
	END
IF @Status = 'POST'
	BEGIN
		SET @SudahCair = 1
		SET @TglBayar = @TglBKM
	END

-- PROSES SINKRONISASI
IF @Tipe = 'JUAL'
BEGIN
	UPDATE ISC064_MARKETINGJUAL..MS_PELUNASAN SET
		 TglPelunasan = CONVERT(datetime, @TglBayar, 101)
		,Ket = @KetBayar
		,NoBKM = @NoBKM
		,SudahCair = @SudahCair
		,CaraBayar = @CaraBayar
	WHERE NoTTS = @NoTTS
	EXEC ISC064_MARKETINGJUAL..spProsentasePelunasan @Ref
END

--IF @Tipe = 'SEWA'
--BEGIN
--	UPDATE AM068_MARKETINGSEWA..MS_PELUNASAN SET
--		 TglPelunasan = CONVERT(datetime, @TglBayar, 101)
--		,Ket = @KetBayar
--		,NoBKM = @NoBKM
--		,SudahCair = @SudahCair
--		,CaraBayar = @CaraBayar
--	WHERE NoTTS = @NoTTS
--	EXEC AM068_MARKETINGSEWA..spProsentasePelunasan @Ref
--END

--IF @Tipe = 'SB'
--BEGIN
--	UPDATE AM068_MARKETINGSB..MS_PELUNASAN SET
--		 TglPelunasan = CONVERT(datetime, @TglBayar, 101)
--		,Ket = @KetBayar
--		,NoBKM = @NoBKM
--		,SudahCair = @SudahCair
--		,CaraBayar = @CaraBayar
--	WHERE NoTTS = @NoTTS
--	EXEC AM068_MARKETINGSB..spProsentasePelunasan @Ref
--END

--IF @Tipe = 'TENANT'
--	UPDATE AM068_TENANT..MS_TAGIHAN SET
--		 TglBayar = CONVERT(datetime, @TglBayar, 101)
--		,KetBayar = @KetBayar
--		,NoBKM = @NoBKM
--		,SudahCair = @SudahCair
--		,CaraBayar = @CaraBayar
--	WHERE NoTTS = @NoTTS









GO
/****** Object:  StoredProcedure [dbo].[spSinkronisasiMEMO]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Sinkronisasi antara tabel MKT dan FIN
*/

CREATE PROCEDURE [dbo].[spSinkronisasiMEMO]
	@NoMEMO int
AS

DECLARE
	 @Tipe varchar(6)
	,@Ref varchar(50)
	,@CaraBayar varchar(2)
	,@Ket varchar(200)
	,@NoBG varchar(20)
	,@TglBG datetime
	,@TglMEMO datetime
	,@TglBKM datetime
	,@Status varchar(4)
	,@StatusBG varchar(3)
	,@NoBKM varchar(50)
	,@TglBayar datetime
	,@KetBayar varchar(255)
	,@SudahCair bit

SELECT
	 @Tipe = Tipe
	,@Ref = Ref
	,@CaraBayar = CaraBayar
	,@Ket = Ket
	,@NoBG = NoBG
	,@TglBG = TglBG
	,@TglMEMO = TglMEMO
	,@TglBKM = TglBKM
	,@Status = Status
	,@StatusBG = StatusBG
	,@NoBKM = NoBKM
FROM MS_MEMO WHERE NoMEMO = @NoMEMO

-- keterangan pembayaran di customer information file
IF @CaraBayar = 'BG'
	SET @KetBayar = @Ket + ' ' + @NoBG + ' Tgl.' + CONVERT(varchar, @TglBG, 106)
ELSE
	SET @KetBayar = @Ket

-- Tolakan giro
IF @StatusBG = 'BAD'
	SET @KetBayar = '*TOLAK* ' + @KetBayar

-- tanggal pelunasan dan status pencairan
IF @Status = 'BARU' OR @Status = 'VOID'
	BEGIN
		SET @SudahCair = 0
		SET @TglBayar = @TglMEMO
	END
IF @Status = 'POST'
	BEGIN
		SET @SudahCair = 1
		SET @TglBayar = @TglBKM
	END

-- PROSES SINKRONISASI
IF @Tipe = 'JUAL'
BEGIN
	UPDATE ISC064_MARKETINGJUAL..MS_PELUNASAN SET
		 TglPelunasan = CONVERT(datetime, @TglBayar, 101)
		,Ket = @KetBayar
		,NoBKM = @NoBKM
		,SudahCair = @SudahCair
		,CaraBayar = @CaraBayar
	WHERE NoMEMO = @NoMEMO
	EXEC ISC064_MARKETINGJUAL..spProsentasePelunasanMEMO @Ref
END

--IF @Tipe = 'SEWA'
--BEGIN
--	UPDATE ISC064_MARKETINGSEWA..MS_PELUNASAN SET
--		 TglPelunasan = CONVERT(datetime, @TglBayar, 101)
--		,Ket = @KetBayar
--		,NoBKM = @NoBKM
--		,SudahCair = @SudahCair
--		,CaraBayar = @CaraBayar
--	WHERE NoMEMO = @NoMEMO
--	EXEC ISC064_MARKETINGSEWA..spProsentasePelunasan @Ref
--END

--IF @Tipe = 'SB'
--BEGIN
--	UPDATE ISC064_MARKETINGSB..MS_PELUNASAN SET
--		 TglPelunasan = CONVERT(datetime, @TglBayar, 101)
--		,Ket = @KetBayar
--		,NoBKM = @NoBKM
--		,SudahCair = @SudahCair
--		,CaraBayar = @CaraBayar
--	WHERE NoMEMO = @NoMEMO
--	EXEC ISC064_MARKETINGSB..spProsentasePelunasan @Ref
--END

--IF @Tipe = 'TENANT'
--	UPDATE ISC064_TENANT..MS_TAGIHAN SET
--		 TglBayar = CONVERT(datetime, @TglBayar, 101)
--		,KetBayar = @KetBayar
--		,NoBKM = @NoBKM
--		,SudahCair = @SudahCair
--		,CaraBayar = @CaraBayar
--	WHERE NoMEMO = @NoMEMO









GO
/****** Object:  StoredProcedure [dbo].[spSKLEdit]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Edit SKL
*/

CREATE PROCEDURE [dbo].[spSKLEdit]
	 @NoSKL varchar(100)
	,@TglSKL datetime
	,@NoSKLManual varchar(100)
	,@Used tinyint
AS

UPDATE MS_SKL
SET
	TglSKL = @TglSKL
	,NoSKLManual = @NoSKLManual
	,Used = @Used
WHERE
NoSKL = @NoSKL









GO
/****** Object:  StoredProcedure [dbo].[spSKLRegistrasi]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Proses mencetak surat PEMBERITAHUAN JATUH TEMPO
*/

CREATE PROCEDURE [dbo].[spSKLRegistrasi]
	 @NoSKL varchar(100)
	,@TglSKL datetime
	,@Ref varchar(50)
	,@NoSKLManual varchar(100)
AS

INSERT INTO MS_SKL
(
	 NoSKL
	,TglSKL
	,Ref
	,NoSKLManual
)
VALUES
(
	@NoSKL
	,CONVERT(datetime, @TglSKL, 101)
	,@Ref
	,@NoSKLManual
)









GO
/****** Object:  StoredProcedure [dbo].[spTransferAnonim]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spTransferAnonim]
	@Tgl datetime,
	@Bank varchar(100),
	@Nilai money,
	@Ket varchar(200)
AS

DECLARE @NoAnonim int
SELECT @NoAnonim = ISNULL(MAX(NoAnonim),0) + 1 FROM MS_ANONIM

INSERT INTO MS_ANONIM
(
	NoAnonim,
	Tgl,
	Bank,
	Nilai,
	Ket
)
VALUES
(
	@NoAnonim,
	CONVERT(datetime, @Tgl, 101),
	@Bank,
	@Nilai,
	@Ket
)

RETURN @NoAnonim









GO
/****** Object:  StoredProcedure [dbo].[spTTSAlokasi]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Alokasi TTS ke tagihan di tabel marketing
*/

CREATE PROCEDURE [dbo].[spTTSAlokasi]
	 @NoTTS int
	,@NoTagihan varchar(50)
	,@Nilai money
AS

DECLARE
	 @Status varchar(4)
	,@Tipe varchar(6)
	,@Ref varchar(50)
	,@CaraBayar varchar(2)
	,@TglTTS datetime

SELECT
	 @Status = Status
	,@Tipe = Tipe
	,@Ref = Ref
	,@CaraBayar = CaraBayar
	,@TglTTS = TglTTS
FROM MS_TTS WHERE NoTTS = @NoTTS

-- validasi status
IF @Status <> 'BARU'
	RETURN

-- hitung total
UPDATE MS_TTS SET Total = Total + @Nilai WHERE NoTTS = @NoTTS

-- POSTING ke Marketing
IF @Tipe = 'JUAL'
	EXEC ISC064_MARKETINGJUAL..spPelunasan @Ref,@NoTagihan,@Nilai,@NoTTS
--IF @Tipe = 'SEWA'
--	EXEC ISC064_MARKETINGSEWA..spPelunasan @Ref,@NoTagihan,@Nilai,@NoTTS
--IF @Tipe = 'SB'
--	EXEC ISC064_MARKETINGSB..spPelunasan @Ref,@NoTagihan,@Nilai,@NoTTS

-- POSTING ke Tenant
IF @Tipe = 'TENANT'
BEGIN
	DECLARE
	 	 @TagihanTipe varchar(3)
		,@TagihanNoUrut int
	
	SET @TagihanTipe = SUBSTRING(@NoTagihan, 1 , CHARINDEX('.',@NoTagihan)-1)
	SET @TagihanNoUrut = SUBSTRING(@NoTagihan, CHARINDEX('.',@NoTagihan)+1, LEN(@NoTagihan))
	
	--EXEC ISC064_TENANT..spPelunasan
	--	 @TagihanTipe
	--	,@TagihanNoUrut
	--	,@CaraBayar
	--	,@TglTTS
	--	,@NoTTS
	--	,@Nilai
END

-- Edit data di tabel front end
EXEC spSinkronisasi @NoTTS









GO
/****** Object:  StoredProcedure [dbo].[spTTSAlokasiReservasi]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
Alokasi TTS ke tagihan di tabel marketing
*/

CREATE PROCEDURE [dbo].[spTTSAlokasiReservasi]
	 @NoTTS int
	,@NoTagihan varchar(50)
	,@Nilai money
AS

DECLARE
	 @Status varchar(4)
	,@Tipe varchar(6)
	,@Ref varchar(50)
	,@CaraBayar varchar(2)
	,@TglTTS datetime

SELECT
	 @Status = Status
	,@Tipe = Tipe
	,@Ref = Ref
	,@CaraBayar = CaraBayar
	,@TglTTS = TglTTS
FROM MS_TTS WHERE NoTTS = @NoTTS

-- validasi status
IF @Status <> 'BARU'
	RETURN

-- hitung total
UPDATE MS_TTS SET Total = @Nilai WHERE NoTTS = @NoTTS

-- POSTING ke Marketing
IF @Tipe = 'JUAL'
	EXEC ISC064_MARKETINGJUAL..spPelunasan @Ref,@NoTagihan,@Nilai,@NoTTS
--IF @Tipe = 'SEWA'
--	EXEC ISC064_MARKETINGSEWA..spPelunasan @Ref,@NoTagihan,@Nilai,@NoTTS
--IF @Tipe = 'SB'
--	EXEC ISC064_MARKETINGSB..spPelunasan @Ref,@NoTagihan,@Nilai,@NoTTS

-- POSTING ke Tenant
IF @Tipe = 'TENANT'
BEGIN
	DECLARE
	 	 @TagihanTipe varchar(3)
		,@TagihanNoUrut int
	
	SET @TagihanTipe = SUBSTRING(@NoTagihan, 1 , CHARINDEX('.',@NoTagihan)-1)
	SET @TagihanNoUrut = SUBSTRING(@NoTagihan, CHARINDEX('.',@NoTagihan)+1, LEN(@NoTagihan))
	
	--EXEC ISC064_TENANT..spPelunasan
	--	 @TagihanTipe
	--	,@TagihanNoUrut
	--	,@CaraBayar
	--	,@TglTTS
	--	,@NoTTS
	--	,@Nilai
END

-- Edit data di tabel front end
EXEC spSinkronisasi @NoTTS









GO
/****** Object:  StoredProcedure [dbo].[spTTSEdit]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Edit TTS
*/

CREATE PROCEDURE [dbo].[spTTSEdit]
	 @NoTTS int
	,@TglTTS datetime
	,@Unit varchar(100)
	,@Customer varchar(100)
	,@Ket varchar(200)
	,@NoBG varchar(20)
	,@TglBG datetime
	,@TglJTBG datetime
	,@Titip varchar(200)
AS

DECLARE @CaraBayar varchar(2)
SELECT @CaraBayar = CaraBayar FROM MS_TTS WHERE NoTTS = @NoTTS

UPDATE MS_TTS
SET
	 TglTTS = CONVERT(datetime, @TglTTS, 101)
	,Unit = @Unit
	,Customer = @Customer
	,Ket = @Ket
WHERE
NoTTS = @NoTTS

IF @CaraBayar = 'BG'
BEGIN
	UPDATE MS_TTS
	SET
		 NoBG = @NoBG
		,TglBG = CONVERT(datetime, @TglBG, 101)
		,TglJTBG = CONVERT(datetime, @TglJTBG, 101)
		,Titip = @Titip
	WHERE
	NoTTS = @NoTTS
END

-- Edit data di tabel front end
EXEC spSinkronisasi @NoTTS









GO
/****** Object:  StoredProcedure [dbo].[spTTSGiroGanti]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Prosedur penggantian cek giro
*/

CREATE PROCEDURE [dbo].[spTTSGiroGanti]
	 @NoBG varchar(20)
	,@NoBGBaru varchar(20)
	,@TglBG datetime
AS

UPDATE MS_TTS SET
	 StatusBG = 'OK'
	,Tolak = ''
	,NoBG = @NoBGBaru
	,TglBG = CONVERT(datetime, @TglBG, 101)
WHERE NoBG = @NoBG

-- Edit data di tabel front end
DECLARE @NoTTS int

DECLARE rs CURSOR FOR
	SELECT NoTTS FROM MS_TTS WHERE NoBG = @NoBGBaru
OPEN rs
FETCH NEXT FROM rs INTO @NoTTS

WHILE @@FETCH_STATUS = 0
BEGIN
	EXEC spSinkronisasi @NoTTS
	FETCH NEXT FROM rs INTO @NoTTS
END
	
CLOSE rs
DEALLOCATE rs









GO
/****** Object:  StoredProcedure [dbo].[spTTSGiroTolak]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Prosedur tolakan giro
*/

CREATE PROCEDURE [dbo].[spTTSGiroTolak]
	 @NoBG varchar(20)
	,@Tolak varchar(200)
AS

UPDATE MS_TTS SET
	 StatusBG = 'BAD'
	,Tolak = @Tolak
WHERE NoBG = @NoBG

-- Edit data di tabel front end
DECLARE @NoTTS int

DECLARE rs CURSOR FOR
	SELECT NoTTS FROM MS_TTS WHERE NoBG = @NoBG
OPEN rs
FETCH NEXT FROM rs INTO @NoTTS

WHILE @@FETCH_STATUS = 0
BEGIN
	EXEC spSinkronisasi @NoTTS
	FETCH NEXT FROM rs INTO @NoTTS
END
	
CLOSE rs
DEALLOCATE rs









GO
/****** Object:  StoredProcedure [dbo].[spTTSRegistrasi]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Transaksi penerimaan pembayaran di kasir
*/

CREATE PROCEDURE [dbo].[spTTSRegistrasi]
	 @TglTTS datetime
	,@UserID varchar(20)
	,@IP varchar(50)
	,@Tipe varchar(6)
	,@Ref varchar(50)
	,@Unit varchar(100)
	,@Customer varchar(100)
	,@CaraBayar varchar(2)
	,@Ket varchar(200)
AS

DECLARE @NoTTS int
SELECT @NoTTS = ISNULL(MAX(NoTTS),0) + 1 FROM MS_TTS

INSERT INTO MS_TTS
(
	 NoTTS
	,TglTTS
	,UserID
	,IP
	,Tipe
	,Ref
	,Unit
	,Customer
	,CaraBayar
	,Ket
)
VALUES
(
	 @NoTTS
	,CONVERT(datetime, @TglTTS, 101)
	,@UserID
	,@IP
	,@Tipe
	,@Ref
	,@Unit
	,@Customer
	,@CaraBayar
	,@Ket
)









GO
/****** Object:  StoredProcedure [dbo].[spTTSRegistrasiBG]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Pendaftaran cek giro
*/

CREATE PROCEDURE [dbo].[spTTSRegistrasiBG]
	 @NoTTS int
	,@NoBG varchar(20)
	,@TglBG datetime
AS

UPDATE MS_TTS
SET
	 NoBG = @NoBG
	,TglBG = CONVERT(datetime, @TglBG, 101)
WHERE
NoTTS = @NoTTS









GO
/****** Object:  StoredProcedure [dbo].[spTTSRegistrasiMigrate]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
Transaksi penerimaan pembayaran di kasir
*/

CREATE PROCEDURE [dbo].[spTTSRegistrasiMigrate]
	 @NoTTS int 
	,@TglTTS datetime
	,@UserID varchar(20)
	,@IP varchar(50)
	,@Tipe varchar(20)
	,@Ref varchar(50)
	,@Unit varchar(100)
	,@Customer varchar(100)
	,@CaraBayar varchar(2)
	,@Ket varchar(200)
AS



INSERT INTO MS_TTS
(
	 NoTTS
	,TglTTS
	,UserID
	,IP
	,Tipe
	,Ref
	,Unit
	,Customer
	,CaraBayar
	,Ket
)
VALUES
(
	 @NoTTS
	,CONVERT(datetime, @TglTTS, 101)
	,@UserID
	,@IP
	,@Tipe
	,@Ref
	,@Unit
	,@Customer
	,@CaraBayar
	,@Ket
)









GO
/****** Object:  StoredProcedure [dbo].[spTTSVoid]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Pembatalan penerimaan di kasir
*/

CREATE PROCEDURE [dbo].[spTTSVoid]
	 @NoTTS int
AS

DECLARE
	 @Status varchar(4)
	,@Tipe varchar(6)
SELECT
	 @Status = Status
	,@Tipe = Tipe
FROM MS_TTS WHERE NoTTS = @NoTTS

-- validasi status
IF @Status <> 'BARU'
	RETURN

UPDATE MS_TTS SET
	 Total = 0
	,Status = 'VOID'
	,NoSlip = 0
WHERE NoTTS = @NoTTS

-- POSTING ke Marketing
IF @Tipe = 'JUAL'
	EXEC ISC064_MARKETINGJUAL..spPelunasanVoid @NoTTS
IF @Tipe = 'SEWA'
	EXEC ISC064_MARKETINGSEWA..spPelunasanVoid @NoTTS
IF @Tipe = 'SB'
	EXEC ISC064_MARKETINGSB..spPelunasanVoid @NoTTS

-- POSTING ke Tenant
IF @Tipe = 'TENANT'
BEGIN
	DECLARE @TagihanTipe varchar(3) , @TagihanNoUrut int
	DECLARE rs CURSOR FOR SELECT Tipe,NoUrut FROM ISC064_TENANT..MS_TAGIHAN WHERE NoTTS = @NoTTS
	OPEN rs

	FETCH NEXT FROM rs INTO @TagihanTipe, @TagihanNoUrut
	WHILE @@FETCH_STATUS = 0
	BEGIN
		EXEC ISC064_TENANT..spPelunasanVoid @TagihanTipe, @TagihanNoUrut
		FETCH NEXT FROM rs INTO @TagihanTipe, @TagihanNoUrut
	END

	CLOSE rs
	DEALLOCATE rs
END









GO
/****** Object:  StoredProcedure [dbo].[spTunggakanDetil]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Detil tagihan di satu buah surat tunggakan
*/

CREATE PROCEDURE [dbo].[spTunggakanDetil]
	 @NoTunggakan int
	,@NoTagihan varchar(50)
	,@NamaTagihan varchar(100)
	,@Nilai money
	,@TglJT datetime
	,@Denda money
AS

DECLARE @NoUrut int
SELECT @NoUrut = ISNULL(MAX(NoUrut),0) + 1 FROM MS_TUNGGAKAN_DETIL
WHERE NoTunggakan = @NoTunggakan

DECLARE @TglTunggakan datetime
DECLARE @Tipe varchar(6)
DECLARE @Ref varchar(20)
SELECT @TglTunggakan = TglTunggakan, @Tipe = Tipe, @Ref = Ref 
FROM MS_TUNGGAKAN
WHERE NoTunggakan = @NoTunggakan

--DECLARE @Denda money
--set @Denda =0
--SELECT @Denda = Denda FROM ISC064_MARKETINGJUAL..MS_TAGIHAN WHERE NoKontrak = @Ref AND NoUrut = @NoTagihan

INSERT INTO MS_TUNGGAKAN_DETIL
(
	 NoTunggakan
	,NoUrut
	,NoTagihan
	,NamaTagihan
	,Nilai
	,TglJT
	,Telat
	,Denda
)
VALUES
(
	 @NoTunggakan
	,@NoUrut
	,@NoTagihan
	,@NamaTagihan
	,@Nilai
	,CONVERT(datetime, @TglJT, 101)
	,DATEDIFF(d, @TglJT, @TglTunggakan)
	,@Denda
)

-- hitung total dan maksimum level
DECLARE @Level int
DECLARE @MaxTelat int
SELECT @MaxTelat = MAX(Telat) FROM MS_TUNGGAKAN_DETIL WHERE NoTunggakan = @NoTunggakan
IF @MaxTelat >= 121
	SET @Level = 4
ELSE IF @MaxTelat >=91 AND @MaxTelat <= 120
	SET @Level = 3
ELSE IF @MaxTelat >=61 AND @MaxTelat <= 90
	SET @Level = 2
ELSE IF @MaxTelat >=31 AND @MaxTelat <= 60
	SET @Level = 1
ELSE
	SET @Level = 1

UPDATE MS_TUNGGAKAN SET
	 Total = Total + @Nilai
	,LevelTunggakan = @Level
WHERE NoTunggakan = @NoTunggakan









GO
/****** Object:  StoredProcedure [dbo].[spTunggakanEdit]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Edit surat tunggakan
*/

CREATE PROCEDURE [dbo].[spTunggakanEdit]
	 @NoTunggakan int
	,@Unit varchar(100)
	,@Customer varchar(100)
	,@NoTelp varchar(50)
	,@Alamat1 varchar(50)
	,@Alamat2 varchar(50)
	,@Alamat3 varchar(50)
AS

UPDATE MS_TUNGGAKAN
SET
	 Unit = @Unit
	,Customer = @Customer
	,NoTelp = @NoTelp
	,Alamat1 = @Alamat1
	,Alamat2 = @Alamat2
	,Alamat3 = @Alamat3
WHERE
NoTunggakan = @NoTunggakan









GO
/****** Object:  StoredProcedure [dbo].[spTunggakanRegistrasi]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Proses mencetak surat tunggakan
*/

CREATE PROCEDURE [dbo].[spTunggakanRegistrasi]
	 @TglTunggakan datetime
	,@Tipe varchar(6)
	,@Ref varchar(50)
	,@Unit varchar(100)
	,@Customer varchar(100)
	,@NoTelp varchar(50)
	,@Alamat1 varchar(50)
	,@Alamat2 varchar(50)
	,@Alamat3 varchar(50)
AS

DECLARE @NoTunggakan int
SELECT @NoTunggakan = ISNULL(MAX(NoTunggakan),0) + 1 FROM MS_TUNGGAKAN

INSERT INTO MS_TUNGGAKAN
(
	 NoTunggakan
	,TglTunggakan
	,Tipe
	,Ref
	,Unit
	,Customer
	,NoTelp
	,Alamat1
	,Alamat2
	,Alamat3
)
VALUES
(
	 @NoTunggakan
	,CONVERT(datetime, @TglTunggakan, 101)
	,@Tipe
	,@Ref
	,@Unit
	,@Customer
	,@NoTelp
	,@Alamat1
	,@Alamat2
	,@Alamat3
)









GO
/****** Object:  StoredProcedure [dbo].[spTunggakanSettle]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
SETTLEMENT tunggakan, manual
*/

CREATE PROCEDURE [dbo].[spTunggakanSettle]
	 @NoTunggakan int
AS

DECLARE @Status varchar(1)
SELECT @Status = Status FROM MS_TUNGGAKAN
WHERE NoTunggakan = @NoTunggakan

-- settlement hanya bisa dilakukan dari status AKTIF saja
IF @Status <> 'A'
	RETURN

UPDATE MS_TUNGGAKAN
SET
	 Status = 'S'
WHERE
NoTunggakan = @NoTunggakan









GO
/****** Object:  StoredProcedure [dbo].[spTunggakanUpgrade]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Levelisasi tunggakan
*/

CREATE PROCEDURE [dbo].[spTunggakanUpgrade]
	@NoTunggakan int
AS

DECLARE
	 @LevelTunggakan int
	,@Tipe varchar(6)
	,@Ref varchar(50)

SELECT
	 @LevelTunggakan = LevelTunggakan
	,@Tipe = Tipe
	,@Ref = Ref
FROM MS_TUNGGAKAN
WHERE NoTunggakan = @NoTunggakan

IF EXISTS(
	SELECT NoTunggakan FROM MS_TUNGGAKAN
	WHERE
		Status = 'A'
		AND NoTunggakan <> @NoTunggakan
		AND Tipe = @Tipe
		AND Ref = @Ref
		AND LevelTunggakan >= @LevelTunggakan
)
	DELETE FROM MS_TUNGGAKAN
	WHERE
		NoTunggakan = @NoTunggakan
ELSE
	UPDATE MS_TUNGGAKAN
	SET Status = 'U'
	WHERE
		Status = 'A'
		AND NoTunggakan <> @NoTunggakan
		AND Tipe = @Tipe
		AND Ref = @Ref









GO
/****** Object:  StoredProcedure [dbo].[spVADel]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Delete account (PERMANEN)
*/

CREATE PROCEDURE [dbo].[spVADel]
	@NoVA varchar(20)
AS

IF EXISTS (SELECT NoVA FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE NoVA = @NoVA)
	RETURN
	
DELETE FROM REF_VA WHERE NoVA = @NoVA









GO
/****** Object:  StoredProcedure [dbo].[spVARegis]    Script Date: 05/04/2019 15.49.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Pendaftaran account baru
*/

CREATE PROCEDURE [dbo].[spVARegis]
	 @NoVA varchar(50),
	 @NoUnit varchar(20),
	 @Bank varchar(50)
AS

INSERT INTO REF_VA
(
	 NoVA
	,NoUnit
	,Bank
)
VALUES
(
	 @NoVA
	,@NoUnit
	,@Bank
)









GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 = UM;1=Piutang;' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MS_MEMO', @level2type=N'COLUMN',@level2name=N'TipePosting'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=NULL , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MS_PJT_DETIL', @level2type=N'COLUMN',@level2name=N'Nilai'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 = UM;1=Piutang;' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MS_TTS', @level2type=N'COLUMN',@level2name=N'TipePosting'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=NULL , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MS_TUNGGAKAN_DETIL', @level2type=N'COLUMN',@level2name=N'Nilai'
GO
USE [master]
GO
ALTER DATABASE [ISC064_FINANCEAR] SET  READ_WRITE 
GO
