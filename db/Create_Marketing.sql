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
/****** Object:  Database [ISC064_MARKETINGJUAL]    Script Date: 05/04/2019 15.50.57 ******/
CREATE DATABASE [ISC064_MARKETINGJUAL]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ISC064_MARKETINGJUAL', FILENAME = N'E:\ISC064\db\ISC064_MARKETINGJUAL.mdf' , SIZE = 75200KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ISC064_MARKETINGJUAL_log', FILENAME = N'E:\ISC064\db\ISC064_MARKETINGJUAL_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [ISC064_MARKETINGJUAL] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ISC064_MARKETINGJUAL].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ISC064_MARKETINGJUAL] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ISC064_MARKETINGJUAL] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ISC064_MARKETINGJUAL] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ISC064_MARKETINGJUAL] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ISC064_MARKETINGJUAL] SET ARITHABORT OFF 
GO
ALTER DATABASE [ISC064_MARKETINGJUAL] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [ISC064_MARKETINGJUAL] SET AUTO_SHRINK ON 
GO
ALTER DATABASE [ISC064_MARKETINGJUAL] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ISC064_MARKETINGJUAL] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ISC064_MARKETINGJUAL] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ISC064_MARKETINGJUAL] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ISC064_MARKETINGJUAL] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ISC064_MARKETINGJUAL] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ISC064_MARKETINGJUAL] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ISC064_MARKETINGJUAL] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ISC064_MARKETINGJUAL] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ISC064_MARKETINGJUAL] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ISC064_MARKETINGJUAL] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ISC064_MARKETINGJUAL] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ISC064_MARKETINGJUAL] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ISC064_MARKETINGJUAL] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ISC064_MARKETINGJUAL] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ISC064_MARKETINGJUAL] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ISC064_MARKETINGJUAL] SET  MULTI_USER 
GO
ALTER DATABASE [ISC064_MARKETINGJUAL] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ISC064_MARKETINGJUAL] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ISC064_MARKETINGJUAL] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ISC064_MARKETINGJUAL] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ISC064_MARKETINGJUAL] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ISC064_MARKETINGJUAL] SET QUERY_STORE = OFF
GO
USE [ISC064_MARKETINGJUAL]
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
USE [ISC064_MARKETINGJUAL]
GO
/****** Object:  User [batavianet]    Script Date: 05/04/2019 15.50.58 ******/
CREATE USER [batavianet] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[batavianet]
GO
ALTER ROLE [db_datareader] ADD MEMBER [batavianet]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [batavianet]
GO
/****** Object:  Schema [batavianet]    Script Date: 05/04/2019 15.50.58 ******/
CREATE SCHEMA [batavianet]
GO
/****** Object:  Table [dbo].[LapPDF]    Script Date: 05/04/2019 15.50.58 ******/
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
/****** Object:  Table [dbo].[MIGRATE_KONTRAK]    Script Date: 05/04/2019 15.51.00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MIGRATE_KONTRAK](
	[NoKontrak] [varchar](50) NOT NULL,
	[Status] [varchar](1) NOT NULL,
	[Customer] [varchar](100) NOT NULL,
	[NoKTP] [varchar](50) NOT NULL,
	[KTPAlamat] [varchar](200) NOT NULL,
	[TempatLahir] [varchar](100) NOT NULL,
	[TglLahir] [datetime] NULL,
	[Marital] [varchar](50) NOT NULL,
	[Agama] [varchar](50) NOT NULL,
	[NoTelp] [varchar](50) NOT NULL,
	[NoFax] [varchar](50) NOT NULL,
	[NPWP] [varchar](100) NOT NULL,
	[NPWPAlamat] [varchar](100) NOT NULL,
	[AlamatSurat] [varchar](100) NOT NULL,
	[Agent] [varchar](100) NOT NULL,
	[TglKontrak] [datetime] NOT NULL,
	[NoUnit] [varchar](20) NOT NULL,
	[Gross] [money] NOT NULL,
	[DiskonRupiah] [money] NOT NULL,
	[NilaiKontrak] [money] NOT NULL,
	[Skema] [varchar](150) NOT NULL,
	[CaraBayar] [varchar](50) NOT NULL,
	[JenisPPN] [varchar](50) NOT NULL,
	[NilaiPPN] [money] NOT NULL,
	[NilaiDPP] [money] NOT NULL,
	[NoVA] [varchar](50) NOT NULL,
	[TglST] [datetime] NULL,
	[NoST] [varchar](20) NOT NULL,
	[TargetST] [datetime] NULL,
	[TglPPJB] [datetime] NULL,
	[NoPPJB] [varchar](20) NOT NULL,
	[TglAJB] [datetime] NULL,
	[NoAJB] [varchar](20) NOT NULL,
	[TglBatal] [datetime] NULL,
	[AlasanBatal] [varchar](100) NOT NULL,
	[BatalMasuk] [money] NOT NULL,
	[NilaiKlaim] [money] NOT NULL,
	[NilaiPulang] [money] NOT NULL,
	[AccBatal] [varchar](50) NOT NULL,
	[TglInput] [datetime] NOT NULL,
	[Approved] [bit] NOT NULL,
 CONSTRAINT [PK_MIGRATE_KONTRAK] PRIMARY KEY CLUSTERED 
(
	[NoKontrak] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MIGRATE_PEMBAYARAN]    Script Date: 05/04/2019 15.51.00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MIGRATE_PEMBAYARAN](
	[NoUrut] [int] IDENTITY(1,1) NOT NULL,
	[NoKontrak] [varchar](50) NOT NULL,
	[NoTTS] [varchar](50) NOT NULL,
	[TglTTS] [datetime] NULL,
	[NoBKM] [varchar](50) NOT NULL,
	[TglBKM] [datetime] NULL,
	[CaraBayar] [varchar](2) NOT NULL,
	[Nilai] [money] NOT NULL,
	[NamaTagihan] [varchar](50) NOT NULL,
	[Rekening] [varchar](50) NOT NULL,
	[TglInput] [datetime] NOT NULL,
	[Approved] [bit] NOT NULL,
	[NoTTSManual] [varchar](30) NOT NULL,
 CONSTRAINT [PK_MIGRATE_PEMBAYARAN] PRIMARY KEY CLUSTERED 
(
	[NoUrut] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MIGRATE_TAGIHAN]    Script Date: 05/04/2019 15.51.00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MIGRATE_TAGIHAN](
	[NoKontrak] [varchar](50) NOT NULL,
	[NoUrut] [int] NOT NULL,
	[NamaTagihan] [varchar](50) NOT NULL,
	[Tipe] [varchar](3) NOT NULL,
	[TglJT] [datetime] NOT NULL,
	[NilaiTagihan] [money] NOT NULL,
	[Denda] [money] NOT NULL,
	[TglInput] [datetime] NOT NULL,
	[Approved] [bit] NOT NULL,
 CONSTRAINT [PK_MIGRATE_TAGIHAN] PRIMARY KEY CLUSTERED 
(
	[NoKontrak] ASC,
	[NoUrut] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_AGENT]    Script Date: 05/04/2019 15.51.01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_AGENT](
	[NoAgent] [int] NOT NULL,
	[Status] [varchar](1) NOT NULL,
	[Nama] [varchar](100) NOT NULL,
	[Principal] [varchar](100) NOT NULL,
	[TglInput] [datetime] NOT NULL,
	[TglEdit] [datetime] NOT NULL,
	[Target1] [money] NOT NULL,
	[Target2] [money] NOT NULL,
	[Target3] [money] NOT NULL,
	[Target4] [money] NOT NULL,
	[Target5] [money] NOT NULL,
	[Skema0] [int] NOT NULL,
	[Skema1] [int] NOT NULL,
	[Skema2] [int] NOT NULL,
	[Skema3] [int] NOT NULL,
	[Skema4] [int] NOT NULL,
	[Skema5] [int] NOT NULL,
	[Alamat] [text] NOT NULL,
	[Kontak] [varchar](100) NOT NULL,
	[NPWP] [varchar](50) NOT NULL,
	[Tipe] [varchar](50) NOT NULL,
	[Rekening] [varchar](50) NOT NULL,
	[SalesManager] [varchar](100) NOT NULL,
	[GeneralManager] [varchar](100) NOT NULL,
	[AdminSales] [varchar](50) NOT NULL,
	[ProjectManager] [varchar](50) NOT NULL,
	[KepalaUnitSales] [varchar](50) NOT NULL,
	[MarketingSupport] [varchar](100) NOT NULL,
	[BillingCollection] [varchar](50) NOT NULL,
	[Cadangan] [varchar](50) NOT NULL,
	[Kinerja] [varchar](50) NOT NULL,
	[KantorPusat] [varchar](50) NOT NULL,
	[CrossSelling] [bit] NOT NULL,
	[CrossGM] [varchar](50) NOT NULL,
	[CrossSM] [varchar](50) NOT NULL,
	[AtasNama] [varchar](100) NOT NULL,
	[RekBank] [varchar](50) NOT NULL,
	[NpwpSalesManager] [text] NOT NULL,
	[BankSalesManager] [text] NOT NULL,
	[RekSalesManager] [text] NOT NULL,
	[AtasNamaSalesManager] [text] NOT NULL,
	[NpwpGeneralManager] [text] NOT NULL,
	[BankGeneralManager] [text] NOT NULL,
	[RekGeneralManager] [text] NOT NULL,
	[AtasNamaGeneralManager] [text] NOT NULL,
	[GMMarketing] [text] NOT NULL,
	[NpwpGMMarketing] [text] NOT NULL,
	[BankGMMarketing] [text] NOT NULL,
	[RekGMMarketing] [text] NOT NULL,
	[AtasNamaGMMarketing] [text] NOT NULL,
	[NpwpSupporting] [text] NOT NULL,
	[BankSupporting] [text] NOT NULL,
	[RekSupporting] [text] NOT NULL,
	[AtasNamaSupporting] [text] NOT NULL,
	[Bod] [text] NOT NULL,
	[NpwpBod] [text] NOT NULL,
	[BankBod] [text] NOT NULL,
	[RekBod] [text] NOT NULL,
	[AtasNamaBod] [text] NOT NULL,
	[coAgent] [text] NOT NULL,
	[NpwpcoAgent] [text] NOT NULL,
	[BankcoAgent] [text] NOT NULL,
	[RekcoAgent] [text] NOT NULL,
	[AtasNamacoAgent] [text] NOT NULL,
	[Wadir] [text] NOT NULL,
	[NpwpWadir] [text] NOT NULL,
	[BankWadir] [text] NOT NULL,
	[RekWadir] [text] NOT NULL,
	[AtasNamaWadir] [text] NOT NULL,
	[KodeSales] [varchar](50) NOT NULL,
	[Whatsapp] [varchar](50) NOT NULL,
	[Handphone] [varchar](50) NOT NULL,
	[Jabatan] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[LvlAtasan] [int] NOT NULL,
	[SalesTipe] [int] NOT NULL,
	[SalesLevel] [int] NOT NULL,
	[SalesGrade] [int] NOT NULL,
	[Atasan] [int] NOT NULL,
	[Cabang] [varchar](50) NULL,
	[PrincipalMgr] [varchar](50) NULL,
	[Project] [varchar](20) NOT NULL,
	[NoAgentNUP] [varchar](50) NOT NULL,
 CONSTRAINT [PK_MS_AGENT] PRIMARY KEY CLUSTERED 
(
	[NoAgent] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_AGENT_LEVEL]    Script Date: 05/04/2019 15.51.01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_AGENT_LEVEL](
	[LevelID] [int] NOT NULL,
	[Nama] [varchar](50) NULL,
	[Tipe] [varchar](10) NOT NULL,
	[Limit] [int] NOT NULL,
 CONSTRAINT [PK_MS_AGENT_LEVEL] PRIMARY KEY CLUSTERED 
(
	[LevelID] ASC,
	[Tipe] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_AGENT_LOG]    Script Date: 05/04/2019 15.51.01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_AGENT_LOG](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Aktivitas] [varchar](6) NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Ket] [text] NOT NULL,
	[Pk] [varchar](50) NOT NULL,
	[Approve] [varchar](50) NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_MS_AGENT_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_AGENT_TARGET]    Script Date: 05/04/2019 15.51.01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_AGENT_TARGET](
	[NoAgent] [int] NOT NULL,
	[Bulan] [int] NOT NULL,
	[Tahun] [int] NOT NULL,
	[Target] [money] NOT NULL,
 CONSTRAINT [PK_MS_AGENT_TARGET] PRIMARY KEY CLUSTERED 
(
	[NoAgent] ASC,
	[Bulan] ASC,
	[Tahun] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_AJB]    Script Date: 05/04/2019 15.51.01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_AJB](
	[NoKontrak] [varchar](50) NOT NULL,
	[NoAJB] [varchar](50) NOT NULL,
	[NoAJBm] [varchar](50) NULL,
	[AJB] [varchar](1) NOT NULL,
	[ajbu] [tinyint] NULL,
	[PrintAJB] [int] NOT NULL,
	[StatusAJB] [varchar](20) NOT NULL,
	[NamaNotaris] [varchar](50) NOT NULL,
	[KetAJB] [text] NOT NULL,
	[TglAJB] [datetime] NULL,
	[TglTTDAJB] [datetime] NULL,
	[TglTargetAJB] [datetime] NULL,
	[Biaya] [money] NOT NULL,
	[NoST] [varchar](50) NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_MS_AJB] PRIMARY KEY CLUSTERED 
(
	[NoKontrak] ASC,
	[NoST] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_APPROVAL]    Script Date: 05/04/2019 15.51.01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_APPROVAL](
	[NoApproval] [varchar](20) NOT NULL,
	[Sumber] [varchar](10) NOT NULL,
	[SumberID] [varchar](50) NOT NULL,
	[TglPengajuan] [datetime] NOT NULL,
	[Status] [varchar](10) NOT NULL,
	[TglApproval] [datetime] NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_MS_APPROVAL] PRIMARY KEY CLUSTERED 
(
	[NoApproval] ASC,
	[Project] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_APPROVAL_ADJUSMENT]    Script Date: 05/04/2019 15.51.01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_APPROVAL_ADJUSMENT](
	[NoApproval] [varchar](20) NOT NULL,
	[NoKontrak] [varchar](50) NOT NULL,
	[GrossBfr] [money] NOT NULL,
	[GrossAft] [money] NOT NULL,
	[DPPBfr] [money] NOT NULL,
	[DPPAft] [money] NOT NULL,
	[PPNBfr] [bit] NOT NULL,
	[PPNAft] [bit] NOT NULL,
	[NilaiPPNBfr] [money] NOT NULL,
	[NilaiPPNAft] [money] NOT NULL,
	[SkemaBfr] [varchar](100) NOT NULL,
	[SkemaAft] [varchar](100) NOT NULL,
	[BungaBfr] [money] NOT NULL,
	[BungaAft] [money] NOT NULL,
	[DiskonRupiahBfr] [money] NOT NULL,
	[DiskonRupiahAft] [money] NOT NULL,
	[DiskonTambahanBfr] [money] NOT NULL,
	[DiskonTambahanAft] [money] NOT NULL,
	[NilaiKontrakBfr] [money] NOT NULL,
	[NilaiKontrakAft] [money] NOT NULL,
	[TglPengajuan] [datetime] NOT NULL,
 CONSTRAINT [PK_MS_APPROVAL_ADJUSMENT] PRIMARY KEY CLUSTERED 
(
	[NoApproval] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_APPROVAL_BATAL]    Script Date: 05/04/2019 15.51.01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_APPROVAL_BATAL](
	[NoApproval] [varchar](20) NOT NULL,
	[NoKontrak] [varchar](50) NOT NULL,
	[TglPengajuan] [datetime] NOT NULL,
	[TglPengembalian] [datetime] NOT NULL,
	[AlasanBatal] [varchar](50) NOT NULL,
	[TotalPelunasan] [money] NOT NULL,
	[NilaiPengembalian] [money] NOT NULL,
	[NilaiKlaim] [money] NOT NULL,
	[Keterangan] [varchar](max) NOT NULL,
 CONSTRAINT [PK_MS_APPROVAL_BATAL] PRIMARY KEY CLUSTERED 
(
	[NoApproval] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_APPROVAL_CUSTOMIZE]    Script Date: 05/04/2019 15.51.01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_APPROVAL_CUSTOMIZE](
	[NoApproval] [varchar](20) NOT NULL,
	[NoKontrak] [varchar](50) NOT NULL,
	[CaraBayarBfr] [varchar](50) NOT NULL,
	[CaraBayarAft] [varchar](50) NOT NULL,
	[SkemaBfr] [varchar](100) NOT NULL,
	[SkemaAft] [varchar](100) NOT NULL,
	[TglPengajuan] [datetime] NOT NULL,
 CONSTRAINT [PK_MS_APPROVAL_CUSTOMIZE] PRIMARY KEY CLUSTERED 
(
	[NoApproval] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_APPROVAL_DETAIL]    Script Date: 05/04/2019 15.51.01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_APPROVAL_DETAIL](
	[NoApproval] [varchar](20) NOT NULL,
	[SN] [int] NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[Lvl] [int] NOT NULL,
	[Nama] [varchar](50) NOT NULL,
	[Approve] [int] NOT NULL,
	[Note] [varchar](max) NOT NULL,
	[TglApproval] [datetime] NULL,
 CONSTRAINT [PK_MS_APPROVAL_DETAIL] PRIMARY KEY CLUSTERED 
(
	[NoApproval] ASC,
	[SN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_APPROVAL_DISKON]    Script Date: 05/04/2019 15.51.02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_APPROVAL_DISKON](
	[NoApproval] [varchar](20) NOT NULL,
	[NoKontrakTemp] [varchar](50) NOT NULL,
	[NoKontrakAfter] [varchar](50) NOT NULL,
	[TglPengajuan] [datetime] NOT NULL,
 CONSTRAINT [PK_MS_APPROVAL_DISKON] PRIMARY KEY CLUSTERED 
(
	[NoApproval] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_APPROVAL_GN]    Script Date: 05/04/2019 15.51.02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_APPROVAL_GN](
	[NoApproval] [varchar](20) NOT NULL,
	[NoKontrak] [varchar](50) NOT NULL,
	[CustomerLama] [varchar](100) NOT NULL,
	[CustomerBaru] [varchar](100) NOT NULL,
	[TglPengajuan] [datetime] NOT NULL,
	[BiayaAdmin] [money] NOT NULL,
	[Keterangan] [varchar](max) NOT NULL,
 CONSTRAINT [PK_MS_APPROVAL_GN] PRIMARY KEY CLUSTERED 
(
	[NoApproval] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_APPROVAL_GU]    Script Date: 05/04/2019 15.51.02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_APPROVAL_GU](
	[NoApproval] [varchar](20) NOT NULL,
	[NoKontrak] [varchar](50) NOT NULL,
	[UnitLama] [varchar](20) NOT NULL,
	[UnitBaru] [varchar](20) NOT NULL,
	[TglPengajuan] [datetime] NOT NULL,
	[BiayaAdmin] [money] NOT NULL,
	[Keterangan] [varchar](max) NOT NULL,
 CONSTRAINT [PK_MS_APPROVAL_GU] PRIMARY KEY CLUSTERED 
(
	[NoApproval] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_APPROVAL_RESCHEDULE]    Script Date: 05/04/2019 15.51.02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_APPROVAL_RESCHEDULE](
	[NoApproval] [varchar](20) NOT NULL,
	[NoKontrak] [varchar](50) NOT NULL,
	[TglPengajuan] [datetime] NOT NULL,
	[SkemaBef] [varchar](150) NOT NULL,
	[SkemaAft] [varchar](150) NOT NULL,
	[CaraBayarBef] [varchar](50) NOT NULL,
	[CaraBayarAft] [varchar](50) NOT NULL,
 CONSTRAINT [PK_MS_APPROVAL_RESCHEDULE] PRIMARY KEY CLUSTERED 
(
	[NoApproval] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_BAST]    Script Date: 05/04/2019 15.51.02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_BAST](
	[NoST] [varchar](20) NOT NULL,
	[NoKontrak] [varchar](50) NOT NULL,
	[NoSTm] [varchar](50) NOT NULL,
	[ST] [varchar](1) NOT NULL,
	[stu] [tinyint] NOT NULL,
	[KetST] [text] NOT NULL,
	[TargetST] [datetime] NOT NULL,
	[PrintBAST] [int] NOT NULL,
	[TglST] [datetime] NULL,
	[TglTTDST] [datetime] NULL,
	[LuasGross] [money] NOT NULL,
	[LuasNett] [money] NOT NULL,
	[Biaya] [money] NOT NULL,
	[LebihBayar] [money] NOT NULL,
	[MasaGaransi] [int] NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_MS_BAST] PRIMARY KEY CLUSTERED 
(
	[NoST] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_BERKAS_PPJB]    Script Date: 05/04/2019 15.51.02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_BERKAS_PPJB](
	[NoKontrak] [varchar](50) NOT NULL,
	[NoBerkas] [int] NOT NULL,
	[Nama] [varchar](100) NOT NULL,
	[Value] [tinyint] NOT NULL,
	[TglLengkap] [datetime] NULL,
 CONSTRAINT [PK_MS_BERKAS_PPJB] PRIMARY KEY CLUSTERED 
(
	[NoKontrak] ASC,
	[NoBerkas] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_COMPLAIN]    Script Date: 05/04/2019 15.51.02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_COMPLAIN](
	[SN] [int] IDENTITY(1,1) NOT NULL,
	[NoKontrak] [varchar](50) NOT NULL,
	[NoComplain] [int] NOT NULL,
	[NoCustomer] [int] NOT NULL,
	[Detil] [varchar](max) NOT NULL,
	[Solusi] [varchar](max) NOT NULL,
	[TglComplain] [datetime] NOT NULL,
	[TglInput] [datetime] NOT NULL,
	[Status] [tinyint] NOT NULL,
	[TglSolved] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_CUSTOMER]    Script Date: 05/04/2019 15.51.02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_CUSTOMER](
	[NoCustomer] [int] NOT NULL,
	[Status] [varchar](1) NOT NULL,
	[Nama] [varchar](100) NOT NULL,
	[NoTelp] [varchar](50) NOT NULL,
	[NoHp] [varchar](50) NOT NULL,
	[NoKantor] [varchar](50) NOT NULL,
	[NoFax] [varchar](50) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[NamaBisnis] [varchar](100) NOT NULL,
	[NoKTP] [varchar](50) NOT NULL,
	[KTP1] [varchar](150) NULL,
	[KTP2] [varchar](50) NOT NULL,
	[KTP3] [varchar](50) NOT NULL,
	[KTP4] [varchar](50) NOT NULL,
	[Alamat1] [varchar](150) NULL,
	[Alamat2] [varchar](50) NOT NULL,
	[Alamat3] [varchar](50) NOT NULL,
	[Kantor1] [varchar](150) NULL,
	[Kantor2] [varchar](50) NOT NULL,
	[Kantor3] [varchar](50) NOT NULL,
	[Agama] [varchar](50) NOT NULL,
	[JenisBisnis] [varchar](100) NOT NULL,
	[MerekBisnis] [varchar](100) NOT NULL,
	[UnitLama] [varchar](20) NOT NULL,
	[LuasLama] [money] NOT NULL,
	[TokoLama] [varchar](100) NOT NULL,
	[ZoningLama] [varchar](100) NOT NULL,
	[GedungLama] [varchar](100) NOT NULL,
	[TeleponLama] [varchar](100) NOT NULL,
	[AkteLama] [varchar](100) NOT NULL,
	[TipeCs] [varchar](20) NOT NULL,
	[TglLahir] [datetime] NULL,
	[TglTransaksi] [datetime] NOT NULL,
	[TglInput] [datetime] NOT NULL,
	[TglEdit] [datetime] NOT NULL,
	[Salutation] [varchar](50) NOT NULL,
	[SumberData] [varchar](100) NOT NULL,
	[TempatLahir] [varchar](100) NOT NULL,
	[AgentInput] [varchar](100) NOT NULL,
	[NPWP] [varchar](100) NOT NULL,
	[Marital] [varchar](50) NOT NULL,
	[Kewarganegaraan] [varchar](50) NOT NULL,
	[Pekerjaan] [varchar](100) NOT NULL,
	[NPWPAlamat1] [varchar](150) NULL,
	[NPWPAlamat2] [varchar](100) NOT NULL,
	[NPWPAlamat3] [varchar](100) NOT NULL,
	[Refferator] [int] NOT NULL,
	[Nama2] [varchar](100) NOT NULL,
	[PPJBNama] [varchar](100) NOT NULL,
	[PenanggungjawabKorp] [varchar](100) NOT NULL,
	[JabatanKorp] [varchar](100) NOT NULL,
	[NoSKKorp] [varchar](100) NOT NULL,
	[BentukKorp] [varchar](100) NOT NULL,
	[TglKTP] [datetime] NOT NULL,
	[KTPSeumurHidup] [bit] NOT NULL,
	[KetEsales] [varchar](max) NOT NULL,
	[ProspectID] [varchar](88) NOT NULL,
	[Kodepos] [varchar](6) NOT NULL,
	[Alamat4] [varchar](50) NOT NULL,
	[Alamat5] [varchar](50) NOT NULL,
	[NPWPAlamat4] [varchar](100) NOT NULL,
	[NPWPAlamat5] [varchar](100) NOT NULL,
	[NamaNPWP] [varchar](100) NOT NULL,
	[NoHP2] [varchar](50) NOT NULL,
	[NamaKerabat] [varchar](100) NOT NULL,
	[Hubungan] [varchar](50) NOT NULL,
	[NoHPKerabat] [varchar](50) NOT NULL,
	[EmailKerabat] [varchar](100) NOT NULL,
	[KTP5] [varchar](50) NOT NULL,
	[Kantor4] [varchar](50) NOT NULL,
	[Kantor5] [varchar](50) NOT NULL,
	[RekNama] [varchar](100) NOT NULL,
	[RekBank] [varchar](100) NOT NULL,
	[RekCabang] [varchar](100) NOT NULL,
	[RekNo] [varchar](100) NOT NULL,
	[Project] [varchar](20) NOT NULL,
	[NoNUP] [varchar](20) NOT NULL,
 CONSTRAINT [PK_MS_CUSTOMER] PRIMARY KEY CLUSTERED 
(
	[NoCustomer] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_CUSTOMER_JURNAL]    Script Date: 05/04/2019 15.51.02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_CUSTOMER_JURNAL](
	[JurnalID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[NoCustomer] [int] NOT NULL,
	[Ket] [text] NOT NULL,
 CONSTRAINT [PK_MS_CUSTOMER_JURNAL] PRIMARY KEY CLUSTERED 
(
	[JurnalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_CUSTOMER_LOG]    Script Date: 05/04/2019 15.51.02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_CUSTOMER_LOG](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Aktivitas] [varchar](6) NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Ket] [text] NOT NULL,
	[Pk] [varchar](50) NOT NULL,
	[Approve] [varchar](50) NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_MS_CUSTOMER_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_FOLLOWUP]    Script Date: 05/04/2019 15.51.02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_FOLLOWUP](
	[NoFU] [int] NOT NULL,
	[NoKontrak] [varchar](50) NOT NULL,
	[TglFU] [datetime] NOT NULL,
	[NamaGrouping] [varchar](50) NOT NULL,
	[Ket] [varchar](max) NOT NULL,
	[NoHP] [varchar](50) NOT NULL,
	[Alamat] [varchar](max) NOT NULL,
	[TglInput] [datetime] NOT NULL,
	[TglUpdate] [datetime] NULL,
	[TglJanjiBayar] [datetime] NULL,
	[Collector] [varchar](50) NOT NULL,
	[NoTagihan] [int] NOT NULL,
 CONSTRAINT [PK_MS_FOLLOWUP] PRIMARY KEY CLUSTERED 
(
	[NoFU] ASC,
	[NoKontrak] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_FOLLOWUP_DETIL]    Script Date: 05/04/2019 15.51.03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_FOLLOWUP_DETIL](
	[NoFU] [int] NOT NULL,
	[NoUrut] [int] NOT NULL,
	[TglJT] [datetime] NOT NULL,
	[NamaTagihan] [varchar](50) NOT NULL,
	[Nilai] [money] NOT NULL,
	[Tipe] [varchar](3) NOT NULL,
	[TglJanjiBayar] [datetime] NOT NULL,
	[NoTagihan] [int] NOT NULL,
	[NoKontrak] [varchar](50) NOT NULL,
 CONSTRAINT [PK_MS_FOLLOWUP_DETIL] PRIMARY KEY CLUSTERED 
(
	[NoFU] ASC,
	[NoUrut] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_FOLLOWUP_LOG]    Script Date: 05/04/2019 15.51.03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_FOLLOWUP_LOG](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Aktivitas] [varchar](6) NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Ket] [text] NOT NULL,
	[Pk] [varchar](50) NOT NULL,
	[Approve] [varchar](50) NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_MS_FOLLOWUP_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_GIMMICK]    Script Date: 05/04/2019 15.51.03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_GIMMICK](
	[ItemID] [varchar](150) NOT NULL,
	[Status] [int] NOT NULL,
	[Nama] [varchar](250) NOT NULL,
	[Ket] [varchar](500) NULL,
	[Tipe] [int] NOT NULL,
	[Satuan] [varchar](20) NOT NULL,
	[TglInput] [datetime] NOT NULL,
	[TglEdit] [datetime] NOT NULL,
	[Stock] [money] NOT NULL,
	[HargaSatuan] [money] NOT NULL,
	[HargaTotal] [money] NOT NULL,
	[Project] [varchar](50) NOT NULL,
 CONSTRAINT [PK_MS_GIMMICK] PRIMARY KEY CLUSTERED 
(
	[ItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_GIMMICK_LOG]    Script Date: 05/04/2019 15.51.03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_GIMMICK_LOG](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Aktivitas] [varchar](6) NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Ket] [text] NOT NULL,
	[Pk] [varchar](50) NOT NULL,
	[Approve] [varchar](50) NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_MS_GIMMICK_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_HOLD]    Script Date: 05/04/2019 15.51.03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_HOLD](
	[NoHold] [varchar](20) NOT NULL,
	[TglHold] [datetime] NOT NULL,
	[TglHoldExpired] [datetime] NOT NULL,
	[NoStock] [varchar](20) NOT NULL,
	[NoCustomer] [int] NOT NULL,
	[NoAgent] [int] NOT NULL,
	[NoKontrak] [varchar](50) NOT NULL,
	[LokasiKontrak] [int] NULL,
	[UserClosing] [varchar](20) NOT NULL,
	[Status] [varchar](1) NOT NULL,
 CONSTRAINT [PK_MS_HOLD] PRIMARY KEY CLUSTERED 
(
	[NoHold] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_HOLD_LOG]    Script Date: 05/04/2019 15.51.03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_HOLD_LOG](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Aktivitas] [varchar](6) NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Ket] [text] NOT NULL,
	[Pk] [varchar](50) NOT NULL,
	[Approve] [varchar](50) NOT NULL,
 CONSTRAINT [PK_MS_HOLD_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_IMB]    Script Date: 05/04/2019 15.51.03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_IMB](
	[NoKontrak] [varchar](50) NOT NULL,
	[NoIMB] [varchar](20) NOT NULL,
	[StatusIMB] [varchar](1) NOT NULL,
	[KetIMB] [text] NOT NULL,
	[TglIMB] [datetime] NULL,
	[TglProsesIMB] [datetime] NULL,
	[TglTargetIMB] [datetime] NULL,
 CONSTRAINT [PK_MS_IMB] PRIMARY KEY CLUSTERED 
(
	[NoKontrak] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_KOMISI]    Script Date: 05/04/2019 15.51.03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_KOMISI](
	[NoKomisi] [varchar](88) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[NoSkema] [int] NOT NULL,
	[NamaSkema] [varchar](100) NOT NULL,
	[NoTermin] [int] NOT NULL,
	[NamaTermin] [varchar](100) NOT NULL,
	[CaraBayar] [varchar](50) NOT NULL,
	[NoKontrak] [varchar](100) NOT NULL,
	[NoUnit] [varchar](100) NOT NULL,
	[NoAgent] [int] NOT NULL,
	[NamaAgent] [varchar](100) NOT NULL,
	[NoCust] [int] NOT NULL,
	[NamaCust] [varchar](100) NOT NULL,
	[TglInput] [datetime] NOT NULL,
	[TglUpdate] [datetime] NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_MS_KOMISI] PRIMARY KEY CLUSTERED 
(
	[NoKomisi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_KOMISI_CF]    Script Date: 05/04/2019 15.51.03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_KOMISI_CF](
	[NoCF] [varchar](88) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[NoSkema] [int] NOT NULL,
	[NamaSkema] [varchar](100) NOT NULL,
	[NoKontrak] [varchar](100) NOT NULL,
	[NoAgent] [int] NOT NULL,
	[NamaAgent] [varchar](100) NOT NULL,
	[TglInput] [datetime] NOT NULL,
	[TglUpdate] [datetime] NOT NULL,
	[NoCustomer] [int] NOT NULL,
	[NamaCust] [varchar](100) NOT NULL,
	[NoUnit] [varchar](100) NOT NULL,
	[Project] [varchar](20) NOT NULL,
	[SalesTipe] [int] NOT NULL,
 CONSTRAINT [PK_MS_CF] PRIMARY KEY CLUSTERED 
(
	[NoCF] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_KOMISI_CF_DETAIL]    Script Date: 05/04/2019 15.51.03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_KOMISI_CF_DETAIL](
	[NoCF] [varchar](88) NOT NULL,
	[SN] [int] NOT NULL,
	[NoAgent] [int] NOT NULL,
	[NamaAgent] [varchar](100) NOT NULL,
	[PotongKomisi] [bit] NOT NULL,
	[Nilai] [money] NOT NULL,
 CONSTRAINT [PK_MS_CF_DETAIL] PRIMARY KEY CLUSTERED 
(
	[NoCF] ASC,
	[SN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_KOMISI_CF_LOG]    Script Date: 05/04/2019 15.51.04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_KOMISI_CF_LOG](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Aktivitas] [varchar](6) NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Ket] [text] NOT NULL,
	[Pk] [varchar](50) NOT NULL,
	[Approve] [varchar](50) NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_MS_KOMISI_CF_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_KOMISI_CFP]    Script Date: 05/04/2019 15.51.04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_KOMISI_CFP](
	[NoCFP] [varchar](88) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Ket] [varchar](max) NOT NULL,
	[TglInput] [datetime] NOT NULL,
	[TglUpdate] [datetime] NOT NULL,
	[Realisasi] [bit] NOT NULL,
	[Project] [varchar](20) NOT NULL,
	[SalesTipe] [int] NOT NULL,
 CONSTRAINT [PK_MS_CFP] PRIMARY KEY CLUSTERED 
(
	[NoCFP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_KOMISI_CFP_DETAIL]    Script Date: 05/04/2019 15.51.04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_KOMISI_CFP_DETAIL](
	[NoCFP] [varchar](88) NOT NULL,
	[SN] [int] NOT NULL,
	[NoCF] [varchar](88) NOT NULL,
	[SN_NoCF] [int] NOT NULL,
	[Nilai] [money] NOT NULL,
	[NoAgent] [int] NOT NULL,
	[NamaAgent] [varchar](200) NOT NULL,
 CONSTRAINT [PK_MS_CFP_DETAIL] PRIMARY KEY CLUSTERED 
(
	[NoCFP] ASC,
	[SN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_KOMISI_CFP_LOG]    Script Date: 05/04/2019 15.51.04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_KOMISI_CFP_LOG](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Aktivitas] [varchar](6) NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Ket] [text] NOT NULL,
	[Pk] [varchar](50) NOT NULL,
	[Approve] [varchar](50) NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_MS_KOMISI_CFP_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_KOMISI_CFR]    Script Date: 05/04/2019 15.51.04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_KOMISI_CFR](
	[NoCFR] [varchar](88) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[NoCFP] [varchar](88) NOT NULL,
	[Ket] [varchar](max) NOT NULL,
	[TglInput] [datetime] NOT NULL,
	[TglUpdate] [datetime] NOT NULL,
	[Project] [varchar](20) NOT NULL,
	[SalesTipe] [int] NOT NULL,
 CONSTRAINT [PK_MS_CFR] PRIMARY KEY CLUSTERED 
(
	[NoCFR] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_KOMISI_CFR_DETAIL]    Script Date: 05/04/2019 15.51.04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_KOMISI_CFR_DETAIL](
	[NoCFR] [varchar](88) NOT NULL,
	[SN] [int] NOT NULL,
	[NoCF] [varchar](88) NOT NULL,
	[SN_NoCF] [int] NOT NULL,
	[Nilai] [money] NOT NULL,
	[NoAgent] [int] NOT NULL,
	[NamaAgent] [varchar](200) NOT NULL,
	[NoCFP] [varchar](88) NOT NULL,
	[NilaiPPH] [money] NOT NULL,
 CONSTRAINT [PK_MS_CFR_DETAIL] PRIMARY KEY CLUSTERED 
(
	[NoCFR] ASC,
	[SN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_KOMISI_CFR_LOG]    Script Date: 05/04/2019 15.51.04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_KOMISI_CFR_LOG](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Aktivitas] [varchar](6) NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Ket] [text] NOT NULL,
	[Pk] [varchar](50) NOT NULL,
	[Approve] [varchar](50) NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_MS_KOMISI_CFR_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_KOMISI_DETAIL]    Script Date: 05/04/2019 15.51.04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_KOMISI_DETAIL](
	[NoKomisi] [varchar](88) NOT NULL,
	[SN] [int] NOT NULL,
	[NoAgent] [int] NOT NULL,
	[NamaAgent] [varchar](100) NOT NULL,
	[Nilai] [money] NOT NULL,
 CONSTRAINT [PK_MS_KOMISI_DETAIL] PRIMARY KEY CLUSTERED 
(
	[NoKomisi] ASC,
	[SN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_KOMISI_LOG]    Script Date: 05/04/2019 15.51.04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_KOMISI_LOG](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Aktivitas] [varchar](6) NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Ket] [text] NOT NULL,
	[Pk] [varchar](50) NOT NULL,
	[Approve] [varchar](50) NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_MS_KOMISI_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_KOMISI_REWARD]    Script Date: 05/04/2019 15.51.04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_KOMISI_REWARD](
	[NoReward] [varchar](88) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[NoAgent] [int] NOT NULL,
	[NamaAgent] [varchar](100) NOT NULL,
	[NoSkema] [int] NOT NULL,
	[NamaSkema] [varchar](100) NOT NULL,
	[Rumus] [varchar](50) NOT NULL,
	[PeriodeDari] [datetime] NOT NULL,
	[PeriodeSampai] [datetime] NOT NULL,
	[Reward] [varchar](100) NOT NULL,
	[TglInput] [datetime] NOT NULL,
	[TglUpdate] [datetime] NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_MS_KOMISI_REWARD] PRIMARY KEY CLUSTERED 
(
	[NoReward] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_KOMISI_REWARD_DETAIL]    Script Date: 05/04/2019 15.51.05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_KOMISI_REWARD_DETAIL](
	[NoReward] [varchar](88) NOT NULL,
	[SN] [int] NOT NULL,
	[NoKontrak] [varchar](100) NOT NULL,
	[NoUnit] [varchar](100) NOT NULL,
	[NoCustomer] [int] NOT NULL,
	[NamaCust] [varchar](100) NOT NULL,
 CONSTRAINT [PK_MS_KOMISI_REWARD_DETAIL] PRIMARY KEY CLUSTERED 
(
	[NoReward] ASC,
	[SN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_KOMISI_REWARD_LOG]    Script Date: 05/04/2019 15.51.05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_KOMISI_REWARD_LOG](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Aktivitas] [varchar](6) NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Ket] [text] NOT NULL,
	[Pk] [varchar](50) NOT NULL,
	[Approve] [varchar](50) NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_MS_KOMISI_REWARD_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_KOMISI_REWARD_P]    Script Date: 05/04/2019 15.51.05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_KOMISI_REWARD_P](
	[NoRP] [varchar](88) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Ket] [varchar](max) NOT NULL,
	[TglInput] [datetime] NOT NULL,
	[TglUpdate] [datetime] NOT NULL,
	[Realisasi] [bit] NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_MS_KOMISI_REWARD_P] PRIMARY KEY CLUSTERED 
(
	[NoRP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_KOMISI_REWARD_P_DETAIL]    Script Date: 05/04/2019 15.51.05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_KOMISI_REWARD_P_DETAIL](
	[NoRP] [varchar](88) NOT NULL,
	[SN] [int] NOT NULL,
	[NoReward] [varchar](88) NOT NULL,
	[Reward] [varchar](100) NOT NULL,
 CONSTRAINT [PK_MS_KOMISI_REWARD_P_DETAIL] PRIMARY KEY CLUSTERED 
(
	[NoRP] ASC,
	[SN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_KOMISI_REWARD_P_LOG]    Script Date: 05/04/2019 15.51.05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_KOMISI_REWARD_P_LOG](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Aktivitas] [varchar](6) NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Ket] [text] NOT NULL,
	[Pk] [varchar](50) NOT NULL,
	[Approve] [varchar](50) NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_MS_KOMISI_REWARD_P_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_KOMISI_REWARD_R]    Script Date: 05/04/2019 15.51.05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_KOMISI_REWARD_R](
	[NoRR] [varchar](88) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[NoRP] [varchar](88) NOT NULL,
	[Ket] [varchar](max) NOT NULL,
	[TglInput] [datetime] NOT NULL,
	[TglUpdate] [datetime] NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_MS_KOMISI_REWARD_R] PRIMARY KEY CLUSTERED 
(
	[NoRR] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_KOMISI_REWARD_R_DETAIL]    Script Date: 05/04/2019 15.51.05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_KOMISI_REWARD_R_DETAIL](
	[NoRR] [varchar](88) NOT NULL,
	[SN] [int] NOT NULL,
	[NoReward] [varchar](88) NOT NULL,
	[Reward] [varchar](100) NOT NULL,
 CONSTRAINT [PK_MS_KOMISI_REWARD_R_DETAIL] PRIMARY KEY CLUSTERED 
(
	[NoRR] ASC,
	[SN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_KOMISI_REWARD_R_LOG]    Script Date: 05/04/2019 15.51.05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_KOMISI_REWARD_R_LOG](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Aktivitas] [varchar](6) NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Ket] [text] NOT NULL,
	[Pk] [varchar](50) NOT NULL,
	[Approve] [varchar](50) NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_MS_KOMISI_REWARD_R_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_KOMISI_TERM]    Script Date: 05/04/2019 15.51.05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_KOMISI_TERM](
	[NoKomisi] [varchar](88) NOT NULL,
	[NoAgent] [int] NOT NULL,
	[SN] [int] NOT NULL,
	[Nama] [varchar](100) NOT NULL,
	[PersenCair] [money] NOT NULL,
	[NilaiCair] [money] NOT NULL,
	[Lunas] [bit] NOT NULL,
	[PersenLunas] [money] NOT NULL,
	[BF] [bit] NOT NULL,
	[PersenBF] [money] NOT NULL,
	[DP] [bit] NOT NULL,
	[PersenDP] [money] NOT NULL,
	[ANG] [bit] NOT NULL,
	[PersenANG] [money] NOT NULL,
	[PPJB] [bit] NOT NULL,
	[AJB] [bit] NOT NULL,
	[AKAD] [bit] NOT NULL,
	[TipeCair] [tinyint] NOT NULL,
	[NamaAgent] [varchar](100) NOT NULL,
 CONSTRAINT [PK_MS_KOMISI_TERM] PRIMARY KEY CLUSTERED 
(
	[NoKomisi] ASC,
	[NoAgent] ASC,
	[SN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_KOMISIP]    Script Date: 05/04/2019 15.51.05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_KOMISIP](
	[NoKomisiP] [varchar](88) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Ket] [varchar](max) NOT NULL,
	[Realisasi] [bit] NOT NULL,
	[TglInput] [datetime] NOT NULL,
	[TglUpdate] [datetime] NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_MS_KOMISIP] PRIMARY KEY CLUSTERED 
(
	[NoKomisiP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_KOMISIP_DETAIL]    Script Date: 05/04/2019 15.51.05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_KOMISIP_DETAIL](
	[NoKomisiP] [varchar](88) NOT NULL,
	[SN] [int] NOT NULL,
	[NoKomisi] [varchar](88) NOT NULL,
	[SN_KomisiTermin] [int] NOT NULL,
	[Nilai] [money] NOT NULL,
 CONSTRAINT [PK_MS_KOMISIP_DETAIL] PRIMARY KEY CLUSTERED 
(
	[NoKomisiP] ASC,
	[SN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_KOMISIP_LOG]    Script Date: 05/04/2019 15.51.06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_KOMISIP_LOG](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Aktivitas] [varchar](6) NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Ket] [text] NOT NULL,
	[Pk] [varchar](50) NOT NULL,
	[Approve] [varchar](50) NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_MS_KOMISIP_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_KOMISIR]    Script Date: 05/04/2019 15.51.06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_KOMISIR](
	[NoKomisiR] [varchar](88) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[NoKomisiP] [varchar](88) NOT NULL,
	[Ket] [varchar](max) NOT NULL,
	[TglInput] [datetime] NOT NULL,
	[TglUpdate] [datetime] NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_MS_KOMISIR] PRIMARY KEY CLUSTERED 
(
	[NoKomisiR] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_KOMISIR_DETAIL]    Script Date: 05/04/2019 15.51.06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_KOMISIR_DETAIL](
	[NoKomisiR] [varchar](88) NOT NULL,
	[SN] [int] NOT NULL,
	[NoKomisi] [varchar](88) NOT NULL,
	[SN_KomisiTermin] [int] NOT NULL,
	[Nilai] [money] NOT NULL,
	[NoAgent] [int] NOT NULL,
 CONSTRAINT [PK_MS_KOMISIR_DETAIL] PRIMARY KEY CLUSTERED 
(
	[NoKomisiR] ASC,
	[SN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_KOMISIR_LOG]    Script Date: 05/04/2019 15.51.06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_KOMISIR_LOG](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Aktivitas] [varchar](6) NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Ket] [text] NOT NULL,
	[Pk] [varchar](50) NOT NULL,
	[Approve] [varchar](50) NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_MS_KOMISIR_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_KONTRAK]    Script Date: 05/04/2019 15.51.06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_KONTRAK](
	[NoKontrak] [varchar](50) NOT NULL,
	[Status] [varchar](1) NOT NULL,
	[NoStock] [varchar](20) NOT NULL,
	[NoCustomer] [int] NOT NULL,
	[NoAgent] [int] NOT NULL,
	[TglKontrak] [datetime] NOT NULL,
	[Jenis] [varchar](20) NOT NULL,
	[Lokasi] [varchar](50) NOT NULL,
	[NoUnit] [varchar](20) NOT NULL,
	[Luas] [money] NOT NULL,
	[Gross] [money] NOT NULL,
	[DiskonRupiah] [money] NOT NULL,
	[DiskonPersen] [varchar](100) NOT NULL,
	[DiskonKet] [varchar](1000) NOT NULL,
	[NilaiKontrak] [money] NOT NULL,
	[OutBalance] [money] NOT NULL,
	[TglInput] [datetime] NOT NULL,
	[TglEdit] [datetime] NOT NULL,
	[FlagGross] [int] NOT NULL,
	[Skema] [varchar](150) NOT NULL,
	[SkemaKomisi] [varchar](150) NOT NULL,
	[AlasanBatal] [varchar](100) NOT NULL,
	[ST] [varchar](1) NOT NULL,
	[TglST] [datetime] NULL,
	[NoST] [varchar](20) NOT NULL,
	[FlagKomisi] [int] NOT NULL,
	[TargetST] [datetime] NOT NULL,
	[PrintSP] [int] NOT NULL,
	[PrintPPJB] [int] NOT NULL,
	[PrintAJB] [int] NOT NULL,
	[PrintBAST] [int] NOT NULL,
	[PrintRKOM] [int] NOT NULL,
	[PrintKUP] [int] NOT NULL,
	[PPJB] [varchar](1) NOT NULL,
	[NoPPJB] [varchar](100) NULL,
	[TglPPJB] [datetime] NULL,
	[AJB] [varchar](1) NOT NULL,
	[NoAJB] [varchar](50) NOT NULL,
	[TglAJB] [datetime] NULL,
	[PersenLunas] [money] NOT NULL,
	[BatalMasuk] [money] NOT NULL,
	[JenisPPN] [varchar](50) NOT NULL,
	[NoKwitansiGabung] [int] NOT NULL,
	[PrintKwitansiGabung] [int] NOT NULL,
	[NoVoucher] [varchar](100) NOT NULL,
	[FOBO] [bit] NOT NULL,
	[FOBO1] [bit] NOT NULL,
	[FOBO3] [bit] NOT NULL,
	[FOBO2] [bit] NOT NULL,
	[JenisPenjualan] [bit] NOT NULL,
	[JenisKPR] [bit] NOT NULL,
	[NilaiRealisasiKPR] [money] NOT NULL,
	[RekeningCairKPR] [varchar](100) NOT NULL,
	[NilaiKlaim] [money] NOT NULL,
	[TglBatal] [datetime] NULL,
	[TotalLunasBatal] [money] NOT NULL,
	[NilaiPulang] [money] NOT NULL,
	[FOBOBatal] [bit] NOT NULL,
	[AccBatal] [varchar](50) NOT NULL,
	[PPN] [bit] NOT NULL,
	[NilaiPPN] [money] NOT NULL,
	[CaraBayar] [varchar](50) NOT NULL,
	[StatusWawancara] [varchar](20) NOT NULL,
	[LokasiWawancara] [varchar](20) NOT NULL,
	[KetWawancara] [text] NOT NULL,
	[StatusSP3K] [varchar](20) NOT NULL,
	[NoSP3K] [varchar](20) NOT NULL,
	[HasilSP3K] [varchar](20) NOT NULL,
	[KetSP3k] [text] NOT NULL,
	[StatusLPA] [varchar](20) NOT NULL,
	[NoLPA] [varchar](20) NOT NULL,
	[KetLPA] [text] NOT NULL,
	[StatusAkad] [varchar](20) NOT NULL,
	[NoAkad] [varchar](20) NOT NULL,
	[KetAkad] [text] NOT NULL,
	[StatusAJB] [varchar](20) NOT NULL,
	[KetAJB] [text] NOT NULL,
	[NilaiPengajuan] [money] NOT NULL,
	[StatusOTS] [varchar](20) NOT NULL,
	[HasilOTS] [varchar](20) NOT NULL,
	[KetOTS] [text] NOT NULL,
	[ApprovalKPR] [money] NOT NULL,
	[RealisasiAkad] [money] NOT NULL,
	[NamaNotaris] [varchar](50) NOT NULL,
	[NoIMB] [varchar](20) NOT NULL,
	[NoPBB] [varchar](20) NOT NULL,
	[NoSertifikat] [varchar](20) NOT NULL,
	[StatusSertifikat] [int] NOT NULL,
	[KetIMB] [text] NOT NULL,
	[StatusHak] [bit] NOT NULL,
	[BankKPR] [varchar](20) NOT NULL,
	[NoRoya] [int] NOT NULL,
	[NamaPerusahaan] [varchar](50) NOT NULL,
	[JangkaWaktu] [int] NOT NULL,
	[NoPengukuranBidang] [int] NOT NULL,
	[NoSuratUkur] [int] NOT NULL,
	[JumlahBarang] [int] NOT NULL,
	[StatusBerkas] [bit] NOT NULL,
	[CheckListDokumen] [text] NOT NULL,
	[StatusIMB] [bit] NOT NULL,
	[TargetWawancara] [datetime] NULL,
	[TglWawancara] [datetime] NULL,
	[TargetSP3K] [datetime] NULL,
	[TglPengajuanSP3K] [datetime] NULL,
	[TargetLPA] [datetime] NULL,
	[TglLPA] [datetime] NULL,
	[TargetAkad] [datetime] NULL,
	[TglAkad] [datetime] NULL,
	[TargetOTS] [datetime] NULL,
	[TglOTS] [datetime] NULL,
	[TglIMB] [datetime] NULL,
	[TglSertifikat] [datetime] NULL,
	[TglAkhir] [datetime] NULL,
	[TglPengukuranBidang] [datetime] NULL,
	[TglSuratUkur] [datetime] NULL,
	[TglHasilSP3K] [datetime] NULL,
	[TglSelesaiBerkas] [datetime] NULL,
	[BungaPersen] [varchar](100) NULL,
	[BungaNominal] [money] NULL,
	[NoFPS] [varchar](20) NOT NULL,
	[PrintFPS] [int] NOT NULL,
	[FlagCaraBayar] [bit] NOT NULL,
	[NilaiDPP] [money] NULL,
	[FlagProsesBatal] [int] NOT NULL,
	[NilaiKembali] [money] NOT NULL,
	[DiskonTambahan] [money] NOT NULL,
	[Note] [varchar](max) NOT NULL,
	[PrintJadwalTagihan] [int] NOT NULL,
	[HargaLainLain] [money] NOT NULL,
	[HargaGimmick] [money] NOT NULL,
	[NoVA] [varchar](50) NOT NULL,
	[ApprovalGN] [bit] NOT NULL,
	[ApprovalGU] [bit] NOT NULL,
	[ApprovalBatal] [bit] NOT NULL,
	[TempGN] [int] NOT NULL,
	[TempGU] [varchar](20) NOT NULL,
	[TempBiayaGN] [money] NOT NULL,
	[TempBiayaGU] [money] NOT NULL,
	[BiayaBatal] [money] NOT NULL,
	[TglGantiNama] [datetime] NULL,
	[TglGantiUnit] [datetime] NULL,
	[TglApGN] [datetime] NULL,
	[TglApGU] [datetime] NULL,
	[TglApBatal] [datetime] NULL,
	[SumberDana] [int] NOT NULL,
	[SumberDanaLainnya] [varchar](200) NOT NULL,
	[TujuanKontrak] [varchar](50) NOT NULL,
	[Revisi] [int] NOT NULL,
	[PPNPemerintah] [money] NOT NULL,
	[NUP] [varchar](20) NOT NULL,
	[RefSkema] [int] NOT NULL,
	[FlagMEMO] [bit] NOT NULL,
	[TempBiayaPPH] [money] NOT NULL,
	[NoRefferatorAgent] [varchar](100) NOT NULL,
	[NoRefferatorCustomer] [varchar](100) NOT NULL,
	[NoKontrakManual] [varchar](50) NOT NULL,
	[novoucherbatal] [varchar](100) NOT NULL,
	[jurnalid] [varchar](88) NOT NULL,
	[FOBOLEASE] [bit] NOT NULL,
	[IncludePPN] [bit] NOT NULL,
	[TujuanLainnya] [varchar](200) NOT NULL,
	[TglKembali] [datetime] NULL,
	[NoPPJBm] [varchar](50) NOT NULL,
	[ppjbu] [tinyint] NOT NULL,
	[TglCetakPPJB] [datetime] NULL,
	[TglLengkapPPJB] [datetime] NULL,
	[TglTTDPPJB] [datetime] NULL,
	[KTPMilik] [tinyint] NOT NULL,
	[KK] [tinyint] NOT NULL,
	[SNKH] [tinyint] NOT NULL,
	[SKK] [tinyint] NOT NULL,
	[RK] [tinyint] NOT NULL,
	[BT] [tinyint] NOT NULL,
	[KW] [tinyint] NOT NULL,
	[DU] [tinyint] NOT NULL,
	[DL] [tinyint] NOT NULL,
	[SM] [tinyint] NOT NULL,
	[KTPIstri] [tinyint] NOT NULL,
	[nokused] [tinyint] NOT NULL,
	[RetensiKPA] [varchar](50) NOT NULL,
	[JumlahBidang] [money] NOT NULL,
	[NilaiHPP] [money] NOT NULL,
	[Refferal] [varchar](50) NOT NULL,
	[NilaiHPPTanah] [money] NOT NULL,
	[ClosingID] [varchar](88) NOT NULL,
	[NilaiKelebihanKPA] [money] NOT NULL,
	[TitipJual] [tinyint] NOT NULL,
	[KetAlasanBatal] [varchar](max) NOT NULL,
	[PaketInvestasi] [tinyint] NOT NULL,
	[TglPaketInvestasi] [datetime] NOT NULL,
	[NoReferratorAgent] [int] NOT NULL,
	[NoReferratorCustomer] [int] NOT NULL,
	[ReffCust] [text] NOT NULL,
	[anreff] [text] NOT NULL,
	[bankreff] [text] NOT NULL,
	[npwpreff] [text] NOT NULL,
	[norekreff] [text] NOT NULL,
	[TambahanSurat] [money] NOT NULL,
	[TambahanBPHTB] [money] NOT NULL,
	[RewardID] [varchar](88) NOT NULL,
	[KomisiID] [varchar](88) NOT NULL,
	[CFID] [varchar](88) NOT NULL,
	[FOBOCOGS] [bit] NOT NULL,
	[DendaST] [money] NOT NULL,
	[RealisasiDendaST] [money] NOT NULL,
	[PemutihanDendaST] [money] NOT NULL,
	[TglApproveDiskon] [datetime] NULL,
	[Project] [varchar](20) NOT NULL,
	[NamaProject] [varchar](50) NOT NULL,
	[Pers] [varchar](20) NOT NULL,
	[NamaPers] [varchar](50) NOT NULL,
	[FlagReschedule] [int] NOT NULL,
	[ApprovelReschedule] [int] NOT NULL,
	[FlagADJ] [bit] NOT NULL,
	[TempDiskonTambahan] [money] NOT NULL,
	[TempDiscTambahPersen] [varchar](100) NOT NULL,
	[TempDiscTambahKet] [varchar](1000) NOT NULL,
	[TempNilaiPPN] [money] NOT NULL,
	[TempNilaiDPP] [money] NOT NULL,
	[TempBungaNominal] [money] NOT NULL,
	[TempBungaPersen] [varchar](100) NOT NULL,
	[TempBungaKet] [varchar](1000) NOT NULL,
	[TempGross] [money] NOT NULL,
	[TempBiayaBPHTB] [money] NOT NULL,
	[TempNilaiKontrak] [money] NOT NULL,
	[TempDiskonPersen] [varchar](100) NOT NULL,
	[TempDiskonKet] [varchar](1000) NOT NULL,
	[TempDiskonRupiah] [money] NOT NULL,
	[TempFlagGross] [int] NOT NULL,
	[ApprovalCustomTagihan] [bit] NOT NULL,
	[TempPPN] [bit] NOT NULL,
	[TempSkema] [varchar](150) NOT NULL,
	[PemohonBatal] [varchar](20) NOT NULL,
	[PemohonGU] [varchar](20) NOT NULL,
	[PemohonGN] [varchar](20) NOT NULL,
	[LokasiPenjualan] [int] NOT NULL,
	[PemohonDiskon] [varchar](20) NOT NULL,
	[PemohonADJ] [varchar](20) NOT NULL,
	[PemohonRE] [varchar](20) NOT NULL,
	[PemohonCU] [varchar](20) NOT NULL,
	[HargaTanah] [money] NOT NULL,
 CONSTRAINT [PK_MS_KONTRAK] PRIMARY KEY CLUSTERED 
(
	[NoKontrak] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_KONTRAK_AGENT]    Script Date: 05/04/2019 15.51.06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_KONTRAK_AGENT](
	[NoKontrak] [varchar](50) NOT NULL,
	[NoUrut] [int] NOT NULL,
	[SalesTipe] [int] NOT NULL,
	[SalesLevel] [int] NOT NULL,
	[NoAgent] [int] NOT NULL,
 CONSTRAINT [PK_M_KONTRAK_AGENT] PRIMARY KEY CLUSTERED 
(
	[NoKontrak] ASC,
	[NoUrut] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_KONTRAK_APP_LOG]    Script Date: 05/04/2019 15.51.06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_KONTRAK_APP_LOG](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[NoKontrak] [varchar](50) NOT NULL,
	[ApprovedBy] [varchar](20) NOT NULL,
	[Approve] [tinyint] NOT NULL,
	[TglApprove] [datetime] NOT NULL,
	[Lvl] [tinyint] NOT NULL,
	[Tipe] [tinyint] NOT NULL,
	[Finish] [bit] NOT NULL,
	[Komentar] [varchar](100) NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_MS_KONTRAK_APP_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_KONTRAK_APPROVAL]    Script Date: 05/04/2019 15.51.06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_KONTRAK_APPROVAL](
	[NoKontrak] [varchar](50) NOT NULL,
	[NoStock] [varchar](20) NOT NULL,
	[NoCustomer] [int] NOT NULL,
	[NoAgent] [int] NOT NULL,
	[TglKontrak] [datetime] NOT NULL,
	[Jenis] [varchar](20) NOT NULL,
	[Lokasi] [varchar](50) NOT NULL,
	[NoUnit] [varchar](20) NOT NULL,
	[Luas] [money] NOT NULL,
	[Gross] [money] NOT NULL,
	[NilaiKontrak] [money] NOT NULL,
	[Skema] [varchar](150) NOT NULL,
	[TargetST] [datetime] NOT NULL,
	[JenisKPR] [bit] NOT NULL,
	[CaraBayar] [varchar](50) NOT NULL,
	[RefSkema] [int] NOT NULL,
	[JenisPPN] [varchar](50) NOT NULL,
	[BungaPersen] [varchar](100) NOT NULL,
	[BungaNominal] [money] NOT NULL,
	[BungaKet] [varchar](200) NOT NULL,
	[SumberDana] [int] NOT NULL,
	[SumberDanaLainnya] [varchar](200) NOT NULL,
	[TujuanKontrak] [varchar](50) NOT NULL,
	[LuasNett] [money] NOT NULL,
	[LuasSG] [money] NOT NULL,
	[DiskonRupiah] [money] NOT NULL,
	[DiskonPersen] [varchar](100) NOT NULL,
	[DiskonTambahan] [money] NOT NULL,
	[DiskonKet] [varchar](200) NOT NULL,
	[Project] [varchar](20) NOT NULL,
	[NamaProject] [varchar](50) NOT NULL,
	[Pers] [varchar](20) NOT NULL,
	[NamaPers] [varchar](50) NOT NULL,
	[PPN] [bit] NOT NULL,
	[PPNBulat] [bit] NOT NULL,
	[NoApproval] [varchar](50) NOT NULL,
	[TipeApproval] [varchar](50) NOT NULL,
	[FlagApproval] [bit] NULL,
	[NamaApproval] [varchar](100) NOT NULL,
	[AlasanApproval] [varchar](max) NOT NULL,
	[StatusApproval] [int] NOT NULL,
	[Status] [varchar](50) NOT NULL,
	[SN] [int] NOT NULL,
	[LokasiPenjualan] [int] NOT NULL,
 CONSTRAINT [PK_MS_KONTRAK_APPROVAL] PRIMARY KEY CLUSTERED 
(
	[NoKontrak] ASC,
	[NoStock] ASC,
	[SN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_KONTRAK_APPROVAL_DETAIL]    Script Date: 05/04/2019 15.51.06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_KONTRAK_APPROVAL_DETAIL](
	[Nokontrak] [varchar](50) NOT NULL,
	[SN] [int] NOT NULL,
	[NoStock] [varchar](20) NOT NULL,
	[NoCustomer] [int] NOT NULL,
	[TitleJabatan] [varchar](50) NOT NULL,
	[Nama] [varchar](150) NOT NULL,
	[FlagApprov] [int] NOT NULL,
	[Ket] [varchar](max) NOT NULL,
 CONSTRAINT [PK_MS_KONTRAK_APPROVAL_DETAIL] PRIMARY KEY CLUSTERED 
(
	[Nokontrak] ASC,
	[SN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_KONTRAK_APPROVAL_LOG]    Script Date: 05/04/2019 15.51.06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_KONTRAK_APPROVAL_LOG](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Aktivitas] [varchar](6) NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Ket] [text] NOT NULL,
	[Pk] [varchar](50) NOT NULL,
	[Approve] [varchar](50) NOT NULL,
 CONSTRAINT [PK_MS_KONTRAK_APPROVAL_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_KONTRAK_GIMMICK]    Script Date: 05/04/2019 15.51.07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_KONTRAK_GIMMICK](
	[NoKontrak] [varchar](50) NOT NULL,
	[SN] [int] NOT NULL,
	[ItemID] [int] NOT NULL,
	[Nama] [varchar](150) NOT NULL,
	[Tipe] [varchar](30) NOT NULL,
	[Satuan] [varchar](20) NOT NULL,
	[Stock] [money] NOT NULL,
	[HargaSatuan] [money] NOT NULL,
	[HargaTotal] [money] NOT NULL,
	[Catatan] [varchar](max) NOT NULL,
	[Diterima] [int] NOT NULL,
	[TglDiterima] [datetime] NULL,
	[PrintGimmick] [int] NOT NULL,
 CONSTRAINT [PK_MS_KONTRAK_GIMMICK] PRIMARY KEY CLUSTERED 
(
	[NoKontrak] ASC,
	[SN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_KONTRAK_JURNAL]    Script Date: 05/04/2019 15.51.07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_KONTRAK_JURNAL](
	[JurnalID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[NoKontrak] [varchar](50) NOT NULL,
	[Ket] [text] NOT NULL,
	[Complain] [varchar](100) NOT NULL,
 CONSTRAINT [PK_MS_KONTRAK_JURNAL] PRIMARY KEY CLUSTERED 
(
	[JurnalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_KONTRAK_LOG]    Script Date: 05/04/2019 15.51.07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_KONTRAK_LOG](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Aktivitas] [varchar](10) NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Ket] [text] NOT NULL,
	[Pk] [varchar](50) NOT NULL,
	[Approve] [varchar](50) NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_MS_KONTRAK_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_LAUNCHING_CALL]    Script Date: 05/04/2019 15.51.07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_LAUNCHING_CALL](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CounterID] [int] NOT NULL,
	[NUPID] [varchar](20) NULL,
	[isCalled] [bit] NOT NULL,
	[Status] [tinyint] NOT NULL,
 CONSTRAINT [PK_MS_LAUNCHING_CALL] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_NUP]    Script Date: 05/04/2019 15.51.07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_NUP](
	[NoNUP] [varchar](50) NOT NULL,
	[NoAgent] [int] NULL,
	[NoCustomer] [int] NULL,
	[TglDaftar] [datetime] NULL,
	[TglEdit] [datetime] NULL,
	[Tipe] [varchar](50) NOT NULL,
	[TglPrintNUP] [datetime] NULL,
	[PrintNUP] [int] NOT NULL,
	[NilaiBayar] [money] NOT NULL,
	[CaraBayar] [varchar](20) NOT NULL,
	[NoRekPers] [varchar](50) NOT NULL,
	[Keterangan] [text] NOT NULL,
	[Revisi] [int] NOT NULL,
	[BolehPilih] [bit] NULL,
	[UserInput] [int] NULL,
	[UserInputNama] [varchar](80) NULL,
	[UserInputID] [varchar](15) NOT NULL,
	[Status] [int] NOT NULL,
	[TglAktivasi] [datetime] NULL,
	[FlagStatus] [int] NOT NULL,
	[PrintRefund] [int] NOT NULL,
	[NamaBfr] [varchar](100) NOT NULL,
	[Project] [varchar](20) NOT NULL,
	[NoCustomerBfr] [int] NOT NULL,
	[TglRevisi] [datetime] NULL,
 CONSTRAINT [PK_MS_NUP_1] PRIMARY KEY CLUSTERED 
(
	[NoNUP] ASC,
	[Tipe] ASC,
	[Project] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_NUP_LOG]    Script Date: 05/04/2019 15.51.07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_NUP_LOG](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Aktivitas] [varchar](10) NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Ket] [text] NOT NULL,
	[Pk] [varchar](50) NOT NULL,
	[Approve] [varchar](50) NOT NULL,
	[Project] [varchar](20) NOT NULL,
	[Tipe] [varchar](50) NOT NULL,
 CONSTRAINT [PK_MS_NUP_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_NUP_PELUNASAN]    Script Date: 05/04/2019 15.51.07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_NUP_PELUNASAN](
	[NoNUP] [varchar](10) NOT NULL,
	[TglBayar] [datetime] NULL,
	[NilaiBayar] [money] NOT NULL,
	[CaraBayar] [varchar](10) NOT NULL,
	[Keterangan] [varchar](200) NOT NULL,
	[NoTTS] [varchar](20) NOT NULL,
	[RekBank] [varchar](30) NOT NULL,
	[PelunasanKe] [smallint] NOT NULL,
	[FlagUntukBayar] [smallint] NOT NULL,
	[UserInputNama] [varchar](80) NOT NULL,
	[UserInputID] [varchar](15) NOT NULL,
	[NoNUPHeader] [varchar](20) NOT NULL,
	[NoTTSNUP] [varchar](50) NOT NULL,
	[Tipe] [varchar](50) NOT NULL,
	[Project] [varchar](20) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_NUP_PRIORITY]    Script Date: 05/04/2019 15.51.07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_NUP_PRIORITY](
	[NoNUP] [varchar](50) NOT NULL,
	[NoStock] [varchar](20) NOT NULL,
	[NomorSkema] [int] NOT NULL,
	[SN] [int] IDENTITY(1,1) NOT NULL,
	[NoKontrak] [varchar](50) NOT NULL,
	[NoNUPHeader] [varchar](50) NOT NULL,
	[NoCustomerMKT] [varchar](20) NOT NULL,
	[Tipe] [varchar](50) NOT NULL,
	[Project] [varchar](20) NOT NULL,
	[Harga] [money] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_PELUNASAN]    Script Date: 05/04/2019 15.51.08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_PELUNASAN](
	[NoKontrak] [varchar](50) NOT NULL,
	[NoUrut] [int] NOT NULL,
	[NoTagihan] [int] NOT NULL,
	[CaraBayar] [varchar](2) NOT NULL,
	[Ket] [varchar](255) NOT NULL,
	[TglPelunasan] [datetime] NOT NULL,
	[NilaiPelunasan] [money] NOT NULL,
	[SudahCair] [bit] NOT NULL,
	[NoTTS] [int] NOT NULL,
	[NoBKM] [int] NOT NULL,
	[NoBKM2] [varchar](30) NOT NULL,
	[NoTTS2] [varchar](30) NOT NULL,
	[TglBKM2] [datetime] NULL,
	[NoMEMO] [int] NOT NULL,
	[NoRealKPA] [int] NOT NULL,
	[noALOKASI] [int] NOT NULL,
	[NilaiDPP] [money] NOT NULL,
	[NilaiPPN] [money] NOT NULL,
 CONSTRAINT [PK_MS_PELUNASAN] PRIMARY KEY CLUSTERED 
(
	[NoKontrak] ASC,
	[NoUrut] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_PELUNASAN_KPA]    Script Date: 05/04/2019 15.51.08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_PELUNASAN_KPA](
	[NoKontrak] [varchar](50) NOT NULL,
	[NoUrut] [int] NOT NULL,
	[NoTagihan] [int] NOT NULL,
	[CaraBayar] [varchar](2) NOT NULL,
	[Ket] [varchar](255) NOT NULL,
	[TglPelunasan] [datetime] NOT NULL,
	[NilaiPelunasan] [money] NOT NULL,
	[SudahCair] [bit] NOT NULL,
	[NoRealKPA] [int] NOT NULL,
	[NoTTS] [int] NOT NULL,
	[Status] [varchar](10) NOT NULL,
 CONSTRAINT [PK_MS_PELUNASAN_KPA] PRIMARY KEY CLUSTERED 
(
	[NoKontrak] ASC,
	[NoUrut] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_PPJB]    Script Date: 05/04/2019 15.51.08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_PPJB](
	[NoKontrak] [varchar](50) NOT NULL,
	[NoPPJB] [varchar](100) NOT NULL,
	[NoPPJBm] [varchar](50) NOT NULL,
	[PPJB] [varchar](1) NOT NULL,
	[ppjbu] [tinyint] NOT NULL,
	[PrintPPJB] [int] NOT NULL,
	[TglPPJB] [datetime] NULL,
	[TglCetakPPJB] [datetime] NULL,
	[TglLengkapPPJB] [datetime] NULL,
	[TglTTDPPJB] [datetime] NULL,
	[TglTargetPPJB] [datetime] NULL,
	[KTPMilik] [tinyint] NOT NULL,
	[KK] [tinyint] NOT NULL,
	[SNKH] [tinyint] NOT NULL,
	[SKK] [tinyint] NOT NULL,
	[RK] [tinyint] NOT NULL,
	[BT] [tinyint] NOT NULL,
	[KW] [tinyint] NOT NULL,
	[DU] [tinyint] NOT NULL,
	[DL] [tinyint] NOT NULL,
	[SM] [tinyint] NOT NULL,
	[KTPIstri] [tinyint] NOT NULL,
	[Biaya] [money] NOT NULL,
	[KetPPJB] [text] NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_MS_PPJB] PRIMARY KEY CLUSTERED 
(
	[NoKontrak] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_PRICELIST]    Script Date: 05/04/2019 15.51.08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_PRICELIST](
	[Nomor] [int] NOT NULL,
	[NoStock] [varchar](20) NOT NULL,
	[NoSkema] [int] NOT NULL,
	[PriceList] [money] NOT NULL,
	[Tgl] [datetime] NULL,
 CONSTRAINT [PK_MS_PRICELIST] PRIMARY KEY CLUSTERED 
(
	[Nomor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_PRICELIST_HISTORY]    Script Date: 05/04/2019 15.51.08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_PRICELIST_HISTORY](
	[No] [int] NOT NULL,
	[NoStock] [varchar](20) NOT NULL,
	[PriceListMin] [money] NOT NULL,
	[PriceList] [money] NOT NULL,
	[Periode] [datetime] NOT NULL,
	[PricelistKavling] [money] NOT NULL,
 CONSTRAINT [PK_MS_PRICELIST_HISTORY] PRIMARY KEY CLUSTERED 
(
	[No] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_PRIORITY]    Script Date: 05/04/2019 15.51.08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_PRIORITY](
	[NoNUP] [varchar](20) NOT NULL,
	[NoStock] [varchar](20) NOT NULL,
	[NoUnit] [varchar](20) NOT NULL,
	[NoCustomer] [int] NOT NULL,
	[NoKontrak] [varchar](20) NOT NULL,
	[NilaiKontrak] [money] NOT NULL,
	[TglInput] [datetime] NOT NULL,
	[TambahanHook] [money] NOT NULL,
	[TambahanLantaiStrategis] [money] NOT NULL,
	[TambahanViewVillage] [money] NOT NULL,
	[TambahanViewPool] [money] NOT NULL,
	[TambahanViewKampus] [money] NOT NULL,
	[TambahanViewCity] [money] NOT NULL,
	[Pricelist] [money] NOT NULL,
	[Diskon] [money] NOT NULL,
	[NomorSkema] [int] NOT NULL,
	[Bunga] [money] NOT NULL,
 CONSTRAINT [PK_MS_PRIORITY] PRIMARY KEY CLUSTERED 
(
	[NoNUP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_PROPERTI]    Script Date: 05/04/2019 15.51.08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_PROPERTI](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nama] [varchar](50) NULL,
 CONSTRAINT [PK_MS_PROPERTI] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_PUTIHDENDA_LOG]    Script Date: 05/04/2019 15.51.08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_PUTIHDENDA_LOG](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Aktivitas] [varchar](6) NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Ket] [text] NOT NULL,
	[Pk] [varchar](50) NOT NULL,
	[Approve] [varchar](50) NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_MS_PUTIHDENDA_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_REALISASIDENDA_LOG]    Script Date: 05/04/2019 15.51.08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_REALISASIDENDA_LOG](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Aktivitas] [varchar](6) NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Ket] [text] NOT NULL,
	[Pk] [varchar](50) NOT NULL,
	[Approve] [varchar](50) NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_MS_REALISASIDENDA_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_RESERVASI]    Script Date: 05/04/2019 15.51.08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_RESERVASI](
	[NoReservasi] [int] NOT NULL,
	[NoStock] [varchar](20) NOT NULL,
	[NoUrut] [int] NOT NULL,
	[NoCustomer] [int] NOT NULL,
	[NoAgent] [int] NOT NULL,
	[Status] [varchar](1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[TglExpire] [datetime] NOT NULL,
	[Netto] [money] NOT NULL,
	[Skema] [varchar](150) NOT NULL,
	[Jenis] [varchar](20) NOT NULL,
	[Lokasi] [varchar](50) NOT NULL,
	[NoUnit] [varchar](20) NOT NULL,
	[TglInput] [datetime] NOT NULL,
	[TglEdit] [datetime] NOT NULL,
	[PrintWL] [int] NOT NULL,
	[NoQueue] [int] NOT NULL,
	[NoRefferatorAgent] [varchar](100) NOT NULL,
	[NoRefferatorCustomer] [varchar](100) NOT NULL,
	[CaraBayar] [varchar](50) NOT NULL,
	[Acc] [varchar](50) NOT NULL,
	[FOBO] [bit] NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[PrintJadwalTagihan] [int] NOT NULL,
	[OutBalance] [money] NOT NULL,
	[Gross] [money] NOT NULL,
	[Supervisor] [varchar](50) NOT NULL,
	[Manager] [varchar](50) NOT NULL,
	[RefSkema] [int] NOT NULL,
	[PrintBForm] [int] NOT NULL,
	[IncludePPN] [bit] NOT NULL,
	[NilaiPPN] [money] NOT NULL,
	[PPN] [bit] NOT NULL,
	[NilaiDPP] [money] NOT NULL,
	[DiskonRupiah] [money] NOT NULL,
	[DiskonPersen] [varchar](50) NOT NULL,
	[NilaiReservasi] [money] NOT NULL,
	[NoKontrak] [varchar](50) NOT NULL,
	[Alasan] [varchar](50) NOT NULL,
	[ReffCust] [text] NOT NULL,
	[anreff] [text] NOT NULL,
	[bankreff] [text] NOT NULL,
	[npwpreff] [text] NOT NULL,
	[norekreff] [text] NOT NULL,
	[BungaNominal] [money] NOT NULL,
	[BungaPersen] [varchar](50) NOT NULL,
	[LokasiPenjualan] [int] NOT NULL,
	[Project] [varchar](50) NOT NULL,
	[NoReservasi2] [varchar](50) NOT NULL,
	[DiskonTambahan] [money] NOT NULL,
 CONSTRAINT [PK_MS_RESERVASI] PRIMARY KEY CLUSTERED 
(
	[NoReservasi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_RESERVASI_JURNAL]    Script Date: 05/04/2019 15.51.09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_RESERVASI_JURNAL](
	[JurnalID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[NoReservasi] [int] NOT NULL,
	[Ket] [text] NOT NULL,
 CONSTRAINT [PK_MS_RESERVASI_JURNAL] PRIMARY KEY CLUSTERED 
(
	[JurnalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_RESERVASI_LOG]    Script Date: 05/04/2019 15.51.09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_RESERVASI_LOG](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Aktivitas] [varchar](6) NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Ket] [text] NOT NULL,
	[Pk] [varchar](50) NOT NULL,
	[Approve] [varchar](50) NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_MS_RESERVASI_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_RESERVASI_OBS]    Script Date: 05/04/2019 15.51.09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_RESERVASI_OBS](
	[NoObs] [bigint] IDENTITY(1,1) NOT NULL,
	[NoUrut] [int] NOT NULL,
	[NoStock] [varchar](20) NOT NULL,
	[Customer] [varchar](200) NOT NULL,
	[Agent] [varchar](200) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[TglExpire] [datetime] NOT NULL,
	[Netto] [money] NOT NULL,
	[Skema] [varchar](150) NOT NULL,
	[Jenis] [varchar](20) NOT NULL,
	[Lokasi] [varchar](20) NOT NULL,
	[NoUnit] [varchar](20) NOT NULL,
	[NoQueue] [int] NOT NULL,
	[Reminder] [bit] NOT NULL,
 CONSTRAINT [PK_MS_RESERVASI_OBS] PRIMARY KEY CLUSTERED 
(
	[NoObs] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_RESERVASI_TAGIHAN]    Script Date: 05/04/2019 15.51.09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_RESERVASI_TAGIHAN](
	[NoReservasi] [int] NOT NULL,
	[NoUrut] [int] NOT NULL,
	[NamaTagihan] [varchar](50) NOT NULL,
	[TglJT] [datetime] NOT NULL,
	[NilaiTagihan] [money] NOT NULL,
	[Tipe] [varchar](3) NOT NULL,
	[FlagPJT] [bit] NOT NULL,
	[Denda] [money] NOT NULL,
	[DendaReal] [money] NOT NULL,
	[KPR] [bit] NOT NULL,
	[Akad] [bit] NOT NULL,
	[PutihDenda] [bit] NOT NULL,
	[NilaiPutihDenda] [money] NOT NULL,
	[NoTTS] [int] NOT NULL,
 CONSTRAINT [PK_MS_RESERVASI_TAGIHAN] PRIMARY KEY CLUSTERED 
(
	[NoReservasi] ASC,
	[NoUrut] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_SERTIFIKAT]    Script Date: 05/04/2019 15.51.09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_SERTIFIKAT](
	[NoKontrak] [varchar](50) NOT NULL,
	[NoSertifikat] [varchar](20) NOT NULL,
	[StatusSertifikat] [varchar](1) NOT NULL,
	[KetSertifikat] [text] NOT NULL,
	[JangkaWaktu] [int] NOT NULL,
	[TglSertifikat] [datetime] NULL,
	[TglProsesSertifikat] [datetime] NULL,
	[TglTargetSertifikat] [datetime] NULL,
	[StatusHak] [bit] NOT NULL,
	[TglAkhir] [datetime] NULL,
	[NoPengukuranBidang] [int] NOT NULL,
	[NoSuratUkur] [int] NOT NULL,
	[TglPengukuranBidang] [datetime] NULL,
	[TglSuratUkur] [datetime] NULL,
	[NamaPerusahaan] [varchar](50) NOT NULL,
	[JumlahBidang] [money] NOT NULL,
 CONSTRAINT [PK_MS_Sertifikat] PRIMARY KEY CLUSTERED 
(
	[NoKontrak] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_SITEPLAN]    Script Date: 05/04/2019 15.51.09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_SITEPLAN](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ParentID] [int] NOT NULL,
	[Nama] [nvarchar](50) NULL,
	[PathGambarDasar] [varchar](200) NULL,
	[PathGambarTransparent] [varchar](200) NULL,
	[isParent] [bit] NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_MS_SITEPLAN] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_TAGIHAN]    Script Date: 05/04/2019 15.51.09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_TAGIHAN](
	[NoKontrak] [varchar](50) NOT NULL,
	[NoUrut] [int] NOT NULL,
	[NamaTagihan] [varchar](50) NOT NULL,
	[TglJT] [datetime] NOT NULL,
	[NilaiTagihan] [money] NOT NULL,
	[Tipe] [varchar](3) NOT NULL,
	[FlagPJT] [bit] NOT NULL,
	[Denda] [money] NOT NULL,
	[DendaReal] [money] NOT NULL,
	[KPR] [bit] NOT NULL,
	[Akad] [bit] NOT NULL,
	[PutihDenda] [bit] NOT NULL,
	[NilaiPutihDenda] [money] NOT NULL,
	[FlagPengajuanKPA] [tinyint] NOT NULL,
	[TglJTB] [datetime] NULL,
	[Ket] [varchar](max) NOT NULL,
	[KetJBLog] [varchar](max) NOT NULL,
	[Grouping] [varchar](50) NOT NULL,
	[NoUrut2] [varchar](50) NOT NULL,
	[Jenis] [varchar](50) NOT NULL,
	[Benefit] [money] NOT NULL,
	[BenefitReal] [money] NOT NULL,
	[AlokasiBenefit] [money] NOT NULL,
	[ApprovelReschedule] [int] NOT NULL,
 CONSTRAINT [PK_MS_TAGIHAN] PRIMARY KEY CLUSTERED 
(
	[NoKontrak] ASC,
	[NoUrut] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_TAGIHAN_HEADER]    Script Date: 05/04/2019 15.51.09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_TAGIHAN_HEADER](
	[NoKontrak] [varchar](50) NOT NULL,
	[Skema] [varchar](150) NOT NULL,
	[NoCustomer] [int] NOT NULL,
	[NoUnit] [varchar](20) NOT NULL,
	[NoAgent] [int] NOT NULL,
	[TglReschedule] [datetime] NULL,
	[CaraBayar] [varchar](50) NOT NULL,
	[RefSkema] [int] NOT NULL,
	[TglApprovelReschedule] [datetime] NULL,
	[ApprovelReschedule] [int] NOT NULL,
 CONSTRAINT [PK_MS_TAGIHAN_HEADER] PRIMARY KEY CLUSTERED 
(
	[NoKontrak] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_TAGIHAN_KPA]    Script Date: 05/04/2019 15.51.10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_TAGIHAN_KPA](
	[NoKontrak] [varchar](50) NOT NULL,
	[NoUrut] [int] NOT NULL,
	[NamaTagihan] [varchar](50) NOT NULL,
	[TglJT] [datetime] NOT NULL,
	[NilaiTagihan] [money] NOT NULL,
	[Tipe] [varchar](20) NULL,
	[FlagPJT] [bit] NOT NULL,
	[Denda] [money] NOT NULL,
	[DendaReal] [money] NOT NULL,
	[KPR] [bit] NOT NULL,
	[Akad] [bit] NOT NULL,
	[PutihDenda] [bit] NOT NULL,
	[NilaiPutihDenda] [money] NOT NULL,
	[FlagPengajuanKPA] [tinyint] NOT NULL,
	[NilaiTagihanTipe] [varchar](20) NOT NULL,
	[NilaiTagihanPersen] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_MS_TAGIHAN_KPA] PRIMARY KEY CLUSTERED 
(
	[NoKontrak] ASC,
	[NoUrut] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_TAGIHAN_LAPORAN]    Script Date: 05/04/2019 15.51.10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_TAGIHAN_LAPORAN](
	[NoApproval] [varchar](20) NOT NULL,
	[NoKontrak] [varchar](50) NOT NULL,
	[NoUrut] [int] NOT NULL,
	[NamaTagihan] [varchar](50) NOT NULL,
	[TglJT] [datetime] NOT NULL,
	[NilaiTagihan] [money] NOT NULL,
	[Tipe] [varchar](3) NOT NULL,
	[FlagPJT] [bit] NOT NULL,
	[Denda] [money] NOT NULL,
	[DendaReal] [money] NOT NULL,
	[KPR] [bit] NOT NULL,
	[Akad] [bit] NOT NULL,
	[PutihDenda] [bit] NOT NULL,
	[NilaiPutihDenda] [money] NOT NULL,
	[FlagPengajuanKPA] [tinyint] NOT NULL,
	[TagihanKe] [int] NOT NULL,
	[UserInput] [varchar](100) NOT NULL,
	[TglInput] [datetime] NULL,
	[Skema] [varchar](150) NOT NULL,
	[NilaiKontrak] [money] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_TAGIHAN_TEMP]    Script Date: 05/04/2019 15.51.10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_TAGIHAN_TEMP](
	[NoKontrak] [varchar](50) NOT NULL,
	[NoUrut] [int] NOT NULL,
	[NamaTagihan] [varchar](50) NOT NULL,
	[TglJT] [datetime] NOT NULL,
	[NilaiTagihan] [money] NOT NULL,
	[Tipe] [varchar](3) NOT NULL,
	[FlagPJT] [bit] NOT NULL,
	[Denda] [money] NOT NULL,
	[DendaReal] [money] NOT NULL,
	[KPR] [bit] NOT NULL,
	[Akad] [bit] NOT NULL,
	[PutihDenda] [bit] NOT NULL,
	[NilaiPutihDenda] [money] NOT NULL,
	[TglPutihDenda] [datetime] NULL,
	[SN] [int] NOT NULL,
 CONSTRAINT [PK_MS_TAGIHAN_TEMP] PRIMARY KEY CLUSTERED 
(
	[NoKontrak] ASC,
	[NoUrut] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_TTR]    Script Date: 05/04/2019 15.51.10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_TTR](
	[NoTTR] [varchar](20) NOT NULL,
	[NoReservasi] [varchar](20) NOT NULL,
	[TglTTR] [datetime] NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Unit] [varchar](100) NOT NULL,
	[Customer] [varchar](100) NOT NULL,
	[CaraBayar] [varchar](2) NOT NULL,
	[Ket] [varchar](200) NOT NULL,
	[Total] [money] NOT NULL,
	[Status] [varchar](4) NOT NULL,
	[PrintTTR] [int] NOT NULL,
	[TglInput] [datetime] NOT NULL,
	[NoBG] [varchar](20) NOT NULL,
	[StatusBG] [varchar](3) NOT NULL,
	[TglBG] [datetime] NULL,
	[NilaiKembali] [money] NOT NULL,
	[ManualTTR] [varchar](50) NOT NULL,
	[Acc] [varchar](20) NOT NULL,
	[FOBO] [bit] NULL,
	[FOBOVoid] [bit] NULL,
 CONSTRAINT [PK_MS_TTR] PRIMARY KEY CLUSTERED 
(
	[NoTTR] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_TTR_LOG]    Script Date: 05/04/2019 15.51.10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_TTR_LOG](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Aktivitas] [varchar](6) NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Ket] [text] NOT NULL,
	[Pk] [varchar](50) NOT NULL,
	[Approve] [varchar](50) NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_MS_TTR_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_UNIT]    Script Date: 05/04/2019 15.51.10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_UNIT](
	[NoStock] [varchar](20) NOT NULL,
	[Status] [varchar](1) NOT NULL,
	[Jenis] [varchar](20) NOT NULL,
	[Lokasi] [varchar](50) NOT NULL,
	[NoUnit] [varchar](20) NOT NULL,
	[Luas] [money] NOT NULL,
	[TglInput] [datetime] NOT NULL,
	[TglEdit] [datetime] NOT NULL,
	[PriceListMin] [money] NOT NULL,
	[PriceList] [money] NOT NULL,
	[FlagSPL] [int] NOT NULL,
	[Peta] [varchar](100) NOT NULL,
	[Koordinat] [varchar](255) NOT NULL,
	[Zoning] [varchar](100) NOT NULL,
	[Panjang] [money] NOT NULL,
	[Lebar] [money] NOT NULL,
	[Tinggi] [money] NOT NULL,
	[LuasSG] [money] NOT NULL,
	[LuasNett] [money] NOT NULL,
	[HadapAtrium] [bit] NOT NULL,
	[HadapEntrance] [bit] NOT NULL,
	[HadapEskalator] [bit] NOT NULL,
	[HadapLift] [bit] NOT NULL,
	[HadapParkir] [bit] NOT NULL,
	[HadapAxis] [bit] NOT NULL,
	[Hook] [bit] NOT NULL,
	[LebarJalan] [money] NOT NULL,
	[Outdoor] [bit] NOT NULL,
	[ArahHadap] [varchar](50) NOT NULL,
	[Panorama] [varchar](100) NOT NULL,
	[DiscountAuthorized] [int] NOT NULL,
	[TambahanHargaGimmick] [money] NOT NULL,
	[TambahanHargaLainLain] [money] NOT NULL,
	[JenisProperti] [varchar](50) NOT NULL,
	[Lantai] [varchar](6) NOT NULL,
	[Nomor] [varchar](6) NOT NULL,
	[TglPriceList] [datetime] NULL,
	[Project] [varchar](20) NOT NULL,
	[SifatPPN] [int] NOT NULL,
	[BiayaBPHTB] [money] NOT NULL,
	[BiayaSurat] [money] NOT NULL,
	[BiayaProses] [money] NOT NULL,
	[BiayaLainLain] [money] NOT NULL,
	[PricelistKavling] [money] NOT NULL,
	[DefaultPL] [int] NOT NULL,
	[LuasLebih] [money] NOT NULL,
	[Kategori] [varchar](100) NOT NULL,
	[NamaJalan] [varchar](250) NOT NULL,
	[HargaTanah] [money] NOT NULL,
 CONSTRAINT [PK_MS_UNIT] PRIMARY KEY CLUSTERED 
(
	[NoStock] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_UNIT_CLOSING]    Script Date: 05/04/2019 15.51.10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_UNIT_CLOSING](
	[UserID] [varchar](50) NOT NULL,
	[NoStock] [varchar](50) NOT NULL,
	[Connected] [bit] NOT NULL,
 CONSTRAINT [PK_MS_UNIT_CLOSING_1] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[NoStock] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MS_UNIT_LOG]    Script Date: 05/04/2019 15.51.10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MS_UNIT_LOG](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Aktivitas] [varchar](6) NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Ket] [text] NOT NULL,
	[Pk] [varchar](50) NOT NULL,
	[Approve] [varchar](50) NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_MS_UNIT_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REF_AGENT_LEVEL]    Script Date: 05/04/2019 15.51.10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REF_AGENT_LEVEL](
	[LevelID] [int] IDENTITY(1,1) NOT NULL,
	[Nama] [varchar](50) NOT NULL,
	[Tipe] [int] NOT NULL,
	[ParentID] [int] NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_REF_AGENT_LEVEL] PRIMARY KEY CLUSTERED 
(
	[LevelID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REF_AGENT_LEVEL_LOG]    Script Date: 05/04/2019 15.51.10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REF_AGENT_LEVEL_LOG](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Aktivitas] [varchar](6) NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Ket] [text] NOT NULL,
	[Pk] [varchar](20) NOT NULL,
	[Approve] [varchar](50) NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_REF_AGENT_LEVEL_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REF_AGENT_TIPE]    Script Date: 05/04/2019 15.51.10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REF_AGENT_TIPE](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Tipe] [varchar](50) NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_REF_AGENT_TIPE] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REF_AGENT_TIPE_LOG]    Script Date: 05/04/2019 15.51.11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REF_AGENT_TIPE_LOG](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Aktivitas] [varchar](6) NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Ket] [text] NOT NULL,
	[Pk] [varchar](20) NOT NULL,
	[Approve] [varchar](50) NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_REF_AGENT_TIPE_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REF_BANKKPA]    Script Date: 05/04/2019 15.51.11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REF_BANKKPA](
	[KodeBank] [varchar](20) NOT NULL,
	[Bank] [varchar](50) NOT NULL,
	[Project] [varchar](20) NOT NULL,
	[Nilai] [money] NOT NULL,
 CONSTRAINT [PK_REF_BANKKPA] PRIMARY KEY CLUSTERED 
(
	[KodeBank] ASC,
	[Project] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REF_BANKKPA_LOG]    Script Date: 05/04/2019 15.51.11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REF_BANKKPA_LOG](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Aktivitas] [varchar](6) NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Ket] [text] NOT NULL,
	[Pk] [varchar](50) NOT NULL,
	[Approve] [varchar](50) NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_[REF_BANKKPA_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REF_BERKAS_PPJB]    Script Date: 05/04/2019 15.51.11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REF_BERKAS_PPJB](
	[Nama] [varchar](100) NOT NULL,
	[SN] [int] NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_REF_BERKAS_PPJB] PRIMARY KEY CLUSTERED 
(
	[Nama] ASC,
	[SN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REF_BERKAS_PPJB_LOG]    Script Date: 05/04/2019 15.51.11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REF_BERKAS_PPJB_LOG](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Aktivitas] [varchar](6) NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Ket] [text] NOT NULL,
	[Pk] [varchar](50) NOT NULL,
	[Approve] [varchar](50) NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_[REF_BERKAS_PPJB_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REF_COMPLAIN]    Script Date: 05/04/2019 15.51.11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REF_COMPLAIN](
	[NoComplain] [int] IDENTITY(1,1) NOT NULL,
	[Judul] [varchar](max) NOT NULL,
	[PIC] [varchar](50) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REF_FOLLOWUP]    Script Date: 05/04/2019 15.51.11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REF_FOLLOWUP](
	[No] [int] IDENTITY(1,1) NOT NULL,
	[NamaGrouping] [varchar](50) NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_REF_FOLLOWUP] PRIMARY KEY CLUSTERED 
(
	[No] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REF_FOLLOWUP_LOG]    Script Date: 05/04/2019 15.51.11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REF_FOLLOWUP_LOG](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Aktivitas] [varchar](6) NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Ket] [text] NOT NULL,
	[Pk] [varchar](20) NOT NULL,
	[Approve] [varchar](50) NOT NULL,
 CONSTRAINT [PK_REF_FOLLOWUP_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REF_JENIS]    Script Date: 05/04/2019 15.51.11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REF_JENIS](
	[Jenis] [varchar](20) NOT NULL,
	[Nama] [varchar](100) NOT NULL,
	[SN] [int] NOT NULL,
	[Project] [varchar](20) NOT NULL,
	[Gambar] [varchar](50) NOT NULL,
 CONSTRAINT [PK_REF_JENIS] PRIMARY KEY CLUSTERED 
(
	[Jenis] ASC,
	[SN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REF_JENIS_LOG]    Script Date: 05/04/2019 15.51.11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REF_JENIS_LOG](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Aktivitas] [varchar](6) NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Ket] [text] NOT NULL,
	[Pk] [varchar](20) NOT NULL,
	[Approve] [varchar](50) NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_REF_JENIS_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REF_JENISPROPERTI]    Script Date: 05/04/2019 15.51.11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REF_JENISPROPERTI](
	[JenisProperti] [varchar](20) NOT NULL,
	[Nama] [varchar](100) NOT NULL,
	[SN] [int] NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_REF_JENISPROPERTI] PRIMARY KEY CLUSTERED 
(
	[JenisProperti] ASC,
	[SN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REF_JENISPROPERTI_LOG]    Script Date: 05/04/2019 15.51.12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REF_JENISPROPERTI_LOG](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Aktivitas] [varchar](6) NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Ket] [text] NOT NULL,
	[Pk] [varchar](20) NOT NULL,
	[Approve] [varchar](50) NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_REF_JENISPROPERTI_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REF_LOKASI]    Script Date: 05/04/2019 15.51.12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REF_LOKASI](
	[Lokasi] [varchar](20) NOT NULL,
	[Nama] [varchar](100) NOT NULL,
	[SN] [int] NOT NULL,
	[Project] [varchar](20) NOT NULL,
	[SNVA] [int] NOT NULL,
 CONSTRAINT [PK_REF_LOKASI] PRIMARY KEY CLUSTERED 
(
	[Lokasi] ASC,
	[SN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REF_LOKASI_KONTRAK]    Script Date: 05/04/2019 15.51.12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REF_LOKASI_KONTRAK](
	[SN] [int] NOT NULL,
	[Lokasi] [varchar](50) NULL,
	[Nama] [varchar](100) NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_REF_LOKASI_KONTRAK] PRIMARY KEY CLUSTERED 
(
	[SN] ASC,
	[Nama] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REF_LOKASI_KONTRAK_LOG]    Script Date: 05/04/2019 15.51.12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REF_LOKASI_KONTRAK_LOG](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NULL,
	[Aktivitas] [varchar](6) NULL,
	[UserID] [varchar](20) NULL,
	[IP] [varchar](50) NULL,
	[Ket] [text] NULL,
	[Pk] [varchar](20) NULL,
	[Approve] [varchar](50) NULL,
 CONSTRAINT [PK_REF_LOKASI_KONTRAK_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REF_LOKASI_LOG]    Script Date: 05/04/2019 15.51.12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REF_LOKASI_LOG](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Aktivitas] [varchar](6) NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Ket] [text] NOT NULL,
	[Pk] [varchar](20) NOT NULL,
	[Approve] [varchar](50) NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_REF_LOKASI_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REF_RETENSI]    Script Date: 05/04/2019 15.51.12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REF_RETENSI](
	[Kode] [varchar](50) NOT NULL,
	[NamaKategori] [varchar](max) NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_REF_RETENSI] PRIMARY KEY CLUSTERED 
(
	[Kode] ASC,
	[Project] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REF_RETENSI_LOG]    Script Date: 05/04/2019 15.51.12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REF_RETENSI_LOG](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Aktivitas] [varchar](6) NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Ket] [text] NOT NULL,
	[Pk] [varchar](50) NOT NULL,
	[Approve] [varchar](50) NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_[REF_RETENSI_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REF_SKEMA]    Script Date: 05/04/2019 15.51.12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REF_SKEMA](
	[Nomor] [int] NOT NULL,
	[Nama] [varchar](100) NOT NULL,
	[Status] [varchar](1) NOT NULL,
	[Diskon] [varchar](100) NOT NULL,
	[DiskonKet] [varchar](1000) NOT NULL,
	[RThousand] [bit] NOT NULL,
	[Jenis] [varchar](50) NOT NULL,
	[Bunga] [varchar](100) NOT NULL,
	[BungaKet] [varchar](1000) NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_REF_SKEMA] PRIMARY KEY CLUSTERED 
(
	[Nomor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REF_SKEMA_DETAIL]    Script Date: 05/04/2019 15.51.12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REF_SKEMA_DETAIL](
	[Nomor] [int] NOT NULL,
	[Baris] [int] NOT NULL,
	[Tipe] [varchar](3) NOT NULL,
	[Nama] [varchar](50) NOT NULL,
	[Nominal] [money] NOT NULL,
	[TipeNominal] [varchar](1) NOT NULL,
	[TglFix] [datetime] NULL,
	[TipeJadwal] [varchar](1) NOT NULL,
	[IntJadwal] [int] NOT NULL,
	[RefJadwal] [int] NOT NULL,
	[BF] [bit] NOT NULL,
	[KPR] [bit] NOT NULL,
 CONSTRAINT [PK_REF_SKEMA_DETAIL] PRIMARY KEY CLUSTERED 
(
	[Nomor] ASC,
	[Baris] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REF_SKEMA_LOG]    Script Date: 05/04/2019 15.51.12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REF_SKEMA_LOG](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Aktivitas] [varchar](6) NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Ket] [text] NOT NULL,
	[Pk] [varchar](50) NOT NULL,
	[Approve] [varchar](50) NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_REF_SKEMA_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REF_SKOM]    Script Date: 05/04/2019 15.51.12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REF_SKOM](
	[NoSkema] [int] NOT NULL,
	[SalesTipe] [int] NOT NULL,
	[Nama] [varchar](100) NOT NULL,
	[Dari] [datetime] NOT NULL,
	[Sampai] [datetime] NOT NULL,
	[Rumus] [varchar](50) NOT NULL,
	[DasarHitung] [varchar](50) NOT NULL,
	[Inaktif] [bit] NOT NULL,
	[TglInput] [datetime] NOT NULL,
	[TglUpdate] [datetime] NOT NULL,
	[Project] [varchar](20) NOT NULL,
	[NoTermin] [int] NOT NULL,
	[CaraBayar] [varchar](50) NOT NULL,
 CONSTRAINT [PK_REF_SKOM] PRIMARY KEY CLUSTERED 
(
	[NoSkema] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REF_SKOM_CF]    Script Date: 05/04/2019 15.51.13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REF_SKOM_CF](
	[NoSkema] [int] NOT NULL,
	[SalesTipe] [int] NOT NULL,
	[Nama] [varchar](100) NOT NULL,
	[Dari] [datetime] NOT NULL,
	[Sampai] [datetime] NOT NULL,
	[Rumus] [varchar](50) NOT NULL,
	[DasarHitung] [varchar](50) NOT NULL,
	[Inaktif] [bit] NOT NULL,
	[TglInput] [datetime] NOT NULL,
	[TglUpdate] [datetime] NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_REF_SKOM_CF] PRIMARY KEY CLUSTERED 
(
	[NoSkema] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REF_SKOM_CF_DETAIL]    Script Date: 05/04/2019 15.51.13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REF_SKOM_CF_DETAIL](
	[NoSkema] [int] NOT NULL,
	[SN] [int] NOT NULL,
	[SalesLevel] [int] NOT NULL,
	[TipeTarif] [varchar](5) NOT NULL,
	[Nilai] [money] NOT NULL,
	[PotongKomisi] [bit] NOT NULL,
 CONSTRAINT [PK_REF_SKOM_CF_DETAIL] PRIMARY KEY CLUSTERED 
(
	[NoSkema] ASC,
	[SN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REF_SKOM_CF_DETAIL2]    Script Date: 05/04/2019 15.51.13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REF_SKOM_CF_DETAIL2](
	[NoSkema] [int] NOT NULL,
	[SN] [int] NOT NULL,
	[SalesLevel] [int] NOT NULL,
	[TipeTarget] [varchar](50) NOT NULL,
	[TargetBawah] [money] NOT NULL,
	[TargetAtas] [money] NOT NULL,
	[TipeTarif] [varchar](5) NOT NULL,
	[Nilai] [money] NOT NULL,
	[PotongKomisi] [bit] NOT NULL,
 CONSTRAINT [PK_REF_SKOM_CF_DETAIL2] PRIMARY KEY CLUSTERED 
(
	[NoSkema] ASC,
	[SN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REF_SKOM_CF_LOG]    Script Date: 05/04/2019 15.51.13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REF_SKOM_CF_LOG](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Aktivitas] [varchar](6) NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Ket] [text] NOT NULL,
	[Pk] [varchar](50) NOT NULL,
	[Approve] [varchar](50) NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_REF_SKOM_CF_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REF_SKOM_DETAIL]    Script Date: 05/04/2019 15.51.13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REF_SKOM_DETAIL](
	[NoSkema] [int] NOT NULL,
	[SN] [int] NOT NULL,
	[SalesLevel] [int] NOT NULL,
	[TipeTarif] [varchar](5) NOT NULL,
	[Nilai] [money] NOT NULL,
 CONSTRAINT [PK_REF_SKOM_DETAIL] PRIMARY KEY CLUSTERED 
(
	[NoSkema] ASC,
	[SN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REF_SKOM_DETAIL2]    Script Date: 05/04/2019 15.51.13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REF_SKOM_DETAIL2](
	[NoSkema] [int] NOT NULL,
	[SN] [int] NOT NULL,
	[SalesLevel] [int] NOT NULL,
	[TipeTarget] [varchar](50) NOT NULL,
	[TargetBawah] [money] NOT NULL,
	[TargetAtas] [money] NOT NULL,
	[TipeTarif] [varchar](5) NOT NULL,
	[Nilai] [money] NOT NULL,
 CONSTRAINT [PK_REF_SKOM_DETAIL2] PRIMARY KEY CLUSTERED 
(
	[NoSkema] ASC,
	[SN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REF_SKOM_LOG]    Script Date: 05/04/2019 15.51.13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REF_SKOM_LOG](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Aktivitas] [varchar](6) NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Ket] [text] NOT NULL,
	[Pk] [varchar](50) NOT NULL,
	[Approve] [varchar](50) NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_REF_SKOM_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REF_SKOM_REWARD]    Script Date: 05/04/2019 15.51.13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REF_SKOM_REWARD](
	[NoSkema] [int] NOT NULL,
	[SalesTipe] [int] NOT NULL,
	[Nama] [varchar](100) NOT NULL,
	[Dari] [datetime] NOT NULL,
	[Sampai] [datetime] NOT NULL,
	[Rumus] [varchar](50) NOT NULL,
	[Inaktif] [bit] NOT NULL,
	[TglInput] [datetime] NOT NULL,
	[TglUpdate] [datetime] NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_REF_SREWARD] PRIMARY KEY CLUSTERED 
(
	[NoSkema] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REF_SKOM_REWARD_DETAIL]    Script Date: 05/04/2019 15.51.13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REF_SKOM_REWARD_DETAIL](
	[NoSkema] [int] NOT NULL,
	[SN] [int] NOT NULL,
	[SalesLevel] [int] NOT NULL,
	[Penjualan] [money] NOT NULL,
	[Reward] [varchar](100) NOT NULL,
 CONSTRAINT [PK_REF_SREWARD_DETAIL] PRIMARY KEY CLUSTERED 
(
	[NoSkema] ASC,
	[SN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REF_SKOM_REWARD_DETAIL2]    Script Date: 05/04/2019 15.51.13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REF_SKOM_REWARD_DETAIL2](
	[NoSkema] [int] NOT NULL,
	[SN] [int] NOT NULL,
	[SalesLevel] [int] NOT NULL,
	[TipeTarget] [varchar](50) NOT NULL,
	[TargetBawah] [money] NOT NULL,
	[TargetAtas] [money] NOT NULL,
	[Reward] [varchar](100) NOT NULL,
 CONSTRAINT [PK_REF_SREWARD_DETAIL2] PRIMARY KEY CLUSTERED 
(
	[NoSkema] ASC,
	[SN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REF_SKOM_REWARD_LOG]    Script Date: 05/04/2019 15.51.14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REF_SKOM_REWARD_LOG](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Aktivitas] [varchar](6) NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Ket] [text] NOT NULL,
	[Pk] [varchar](50) NOT NULL,
	[Approve] [varchar](50) NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_REF_SKOM_REWARD_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REF_SKOM_TERM]    Script Date: 05/04/2019 15.51.14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REF_SKOM_TERM](
	[NoTermin] [int] NOT NULL,
	[Nama] [varchar](100) NOT NULL,
	[CaraBayar] [varchar](50) NOT NULL,
	[Inaktif] [bit] NOT NULL,
	[TglInput] [datetime] NOT NULL,
	[TglUpdate] [datetime] NOT NULL,
	[Project] [varchar](20) NOT NULL,
	[SalesTipe] [int] NOT NULL,
	[TipeAgent] [int] NOT NULL,
 CONSTRAINT [PK_REF_SKOM_TERM] PRIMARY KEY CLUSTERED 
(
	[NoTermin] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REF_SKOM_TERM_DETAIL]    Script Date: 05/04/2019 15.51.14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REF_SKOM_TERM_DETAIL](
	[NoTermin] [int] NOT NULL,
	[SN] [int] NOT NULL,
	[Nama] [varchar](100) NOT NULL,
	[PersenCair] [money] NOT NULL,
	[Lunas] [bit] NOT NULL,
	[PersenLunas] [money] NOT NULL,
	[BF] [bit] NOT NULL,
	[PersenBF] [money] NOT NULL,
	[DP] [bit] NOT NULL,
	[PersenDP] [money] NOT NULL,
	[ANG] [bit] NOT NULL,
	[PersenANG] [money] NOT NULL,
	[PPJB] [bit] NOT NULL,
	[AJB] [bit] NOT NULL,
	[AKAD] [bit] NOT NULL,
	[TipeCair] [tinyint] NOT NULL,
	[SalesLevel] [int] NOT NULL,
 CONSTRAINT [PK_REF_SKOM_TERM_DETAIL] PRIMARY KEY CLUSTERED 
(
	[NoTermin] ASC,
	[SN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REF_SKOM_TERM_LOG]    Script Date: 05/04/2019 15.51.14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REF_SKOM_TERM_LOG](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Aktivitas] [varchar](6) NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Ket] [text] NOT NULL,
	[Pk] [varchar](50) NOT NULL,
	[Approve] [varchar](50) NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_REF_SKOM_TERM_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REF_TIPE]    Script Date: 05/04/2019 15.51.14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REF_TIPE](
	[Tipe] [varchar](30) NOT NULL,
	[SN] [int] IDENTITY(1,1) NOT NULL,
	[Keterangan] [text] NOT NULL,
	[Jenis] [varchar](30) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REF_TIPE_AGENT]    Script Date: 05/04/2019 15.51.14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REF_TIPE_AGENT](
	[NamaTipe] [varchar](50) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REF_TIPE_GIMMICK]    Script Date: 05/04/2019 15.51.14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REF_TIPE_GIMMICK](
	[ID] [int] NOT NULL,
	[Nama] [varchar](250) NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_REF_TIPE_GIMMICK] PRIMARY KEY CLUSTERED 
(
	[ID] ASC,
	[Project] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REF_TIPE_GIMMICK_LOG]    Script Date: 05/04/2019 15.51.14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REF_TIPE_GIMMICK_LOG](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tgl] [datetime] NOT NULL,
	[Aktivitas] [varchar](6) NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[IP] [varchar](50) NOT NULL,
	[Ket] [text] NOT NULL,
	[Pk] [varchar](50) NOT NULL,
	[Approve] [varchar](50) NOT NULL,
	[Project] [varchar](20) NOT NULL,
 CONSTRAINT [PK_REF_TIPE_GIMMICK_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SmsVendor]    Script Date: 05/04/2019 15.51.14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SmsVendor](
	[SMSID] [varchar](50) NOT NULL,
	[Nama] [varchar](100) NOT NULL,
	[Ket] [varchar](max) NOT NULL,
 CONSTRAINT [PK_SmsVendor] PRIMARY KEY CLUSTERED 
(
	[SMSID] ASC
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
ALTER TABLE [dbo].[MIGRATE_KONTRAK] ADD  CONSTRAINT [DF_MIGRATE_KONTRAK_Status]  DEFAULT ('A') FOR [Status]
GO
ALTER TABLE [dbo].[MIGRATE_KONTRAK] ADD  CONSTRAINT [DF_MIGRATE_KONTRAK_NoKTP]  DEFAULT ('') FOR [NoKTP]
GO
ALTER TABLE [dbo].[MIGRATE_KONTRAK] ADD  CONSTRAINT [DF_MIGRATE_KONTRAK_KTP1]  DEFAULT ('') FOR [KTPAlamat]
GO
ALTER TABLE [dbo].[MIGRATE_KONTRAK] ADD  CONSTRAINT [DF_MIGRATE_KONTRAK_TempatLahir]  DEFAULT ('') FOR [TempatLahir]
GO
ALTER TABLE [dbo].[MIGRATE_KONTRAK] ADD  CONSTRAINT [DF_MIGRATE_KONTRAK_NoTelp]  DEFAULT ('') FOR [NoTelp]
GO
ALTER TABLE [dbo].[MIGRATE_KONTRAK] ADD  CONSTRAINT [DF_MIGRATE_KONTRAK_NoFax]  DEFAULT ('') FOR [NoFax]
GO
ALTER TABLE [dbo].[MIGRATE_KONTRAK] ADD  CONSTRAINT [DF_MIGRATE_KONTRAK_NPWP]  DEFAULT ('') FOR [NPWP]
GO
ALTER TABLE [dbo].[MIGRATE_KONTRAK] ADD  CONSTRAINT [DF_MIGRATE_KONTRAK_NPWPAlamat1]  DEFAULT ('') FOR [NPWPAlamat]
GO
ALTER TABLE [dbo].[MIGRATE_KONTRAK] ADD  CONSTRAINT [DF_MIGRATE_KONTRAK_Alamat1]  DEFAULT ('') FOR [AlamatSurat]
GO
ALTER TABLE [dbo].[MIGRATE_KONTRAK] ADD  CONSTRAINT [DF_MIGRATE_KONTRAK_TglKontrak]  DEFAULT (getdate()) FOR [TglKontrak]
GO
ALTER TABLE [dbo].[MIGRATE_KONTRAK] ADD  CONSTRAINT [DF_MIGRATE_KONTRAK_Gross]  DEFAULT ((0)) FOR [Gross]
GO
ALTER TABLE [dbo].[MIGRATE_KONTRAK] ADD  CONSTRAINT [DF_MIGRATE_KONTRAK_DiskonRupiah]  DEFAULT ((0)) FOR [DiskonRupiah]
GO
ALTER TABLE [dbo].[MIGRATE_KONTRAK] ADD  CONSTRAINT [DF_MIGRATE_KONTRAK_NilaiKontrak]  DEFAULT ((0)) FOR [NilaiKontrak]
GO
ALTER TABLE [dbo].[MIGRATE_KONTRAK] ADD  CONSTRAINT [DF_MIGRATE_KONTRAK_Skema]  DEFAULT ('') FOR [Skema]
GO
ALTER TABLE [dbo].[MIGRATE_KONTRAK] ADD  CONSTRAINT [DF_MIGRATE_KONTRAK_CaraBayar]  DEFAULT ('') FOR [CaraBayar]
GO
ALTER TABLE [dbo].[MIGRATE_KONTRAK] ADD  CONSTRAINT [DF_MIGRATE_KONTRAK_JenisPPN]  DEFAULT ('KONSUMEN') FOR [JenisPPN]
GO
ALTER TABLE [dbo].[MIGRATE_KONTRAK] ADD  CONSTRAINT [DF_MIGRATE_KONTRAK_NilaiPPN]  DEFAULT ((0)) FOR [NilaiPPN]
GO
ALTER TABLE [dbo].[MIGRATE_KONTRAK] ADD  CONSTRAINT [DF_MIGRATE_KONTRAK_NilaiDPP]  DEFAULT ((0)) FOR [NilaiDPP]
GO
ALTER TABLE [dbo].[MIGRATE_KONTRAK] ADD  CONSTRAINT [DF_MIGRATE_KONTRAK_NoVA]  DEFAULT ('') FOR [NoVA]
GO
ALTER TABLE [dbo].[MIGRATE_KONTRAK] ADD  CONSTRAINT [DF_MIGRATE_KONTRAK_NoST]  DEFAULT ('') FOR [NoST]
GO
ALTER TABLE [dbo].[MIGRATE_KONTRAK] ADD  CONSTRAINT [DF_MIGRATE_KONTRAK_TargetST]  DEFAULT (getdate()) FOR [TargetST]
GO
ALTER TABLE [dbo].[MIGRATE_KONTRAK] ADD  CONSTRAINT [DF_MIGRATE_KONTRAK_NoPPJB]  DEFAULT ('') FOR [NoPPJB]
GO
ALTER TABLE [dbo].[MIGRATE_KONTRAK] ADD  CONSTRAINT [DF_MIGRATE_KONTRAK_NoAJB]  DEFAULT ('') FOR [NoAJB]
GO
ALTER TABLE [dbo].[MIGRATE_KONTRAK] ADD  CONSTRAINT [DF_MIGRATE_KONTRAK_AlasanBatal]  DEFAULT ('') FOR [AlasanBatal]
GO
ALTER TABLE [dbo].[MIGRATE_KONTRAK] ADD  CONSTRAINT [DF_MIGRATE_KONTRAK_BatalMasuk]  DEFAULT ((0)) FOR [BatalMasuk]
GO
ALTER TABLE [dbo].[MIGRATE_KONTRAK] ADD  CONSTRAINT [DF_MIGRATE_KONTRAK_NilaiKlaim]  DEFAULT ((0)) FOR [NilaiKlaim]
GO
ALTER TABLE [dbo].[MIGRATE_KONTRAK] ADD  CONSTRAINT [DF_MIGRATE_KONTRAK_NilaiPulang]  DEFAULT ((0)) FOR [NilaiPulang]
GO
ALTER TABLE [dbo].[MIGRATE_KONTRAK] ADD  CONSTRAINT [DF_MIGRATE_KONTRAK_AccBatal]  DEFAULT ('') FOR [AccBatal]
GO
ALTER TABLE [dbo].[MIGRATE_KONTRAK] ADD  CONSTRAINT [DF__MIGRATE_K__TglIn__71FCD09A]  DEFAULT (getdate()) FOR [TglInput]
GO
ALTER TABLE [dbo].[MIGRATE_KONTRAK] ADD  CONSTRAINT [DF__MIGRATE_K__Appro__72F0F4D3]  DEFAULT ((0)) FOR [Approved]
GO
ALTER TABLE [dbo].[MIGRATE_PEMBAYARAN] ADD  CONSTRAINT [DF_MIGRATE_PEMBAYARAN_NoTTS]  DEFAULT ('') FOR [NoTTS]
GO
ALTER TABLE [dbo].[MIGRATE_PEMBAYARAN] ADD  CONSTRAINT [DF_MIGRATE_PEMBAYARAN_NoBKM]  DEFAULT ('') FOR [NoBKM]
GO
ALTER TABLE [dbo].[MIGRATE_PEMBAYARAN] ADD  CONSTRAINT [DF_MIGRATE_PEMBAYARAN_CaraBayar]  DEFAULT ('') FOR [CaraBayar]
GO
ALTER TABLE [dbo].[MIGRATE_PEMBAYARAN] ADD  CONSTRAINT [DF_MIGRATE_PEMBAYARAN_Nilai]  DEFAULT ((0)) FOR [Nilai]
GO
ALTER TABLE [dbo].[MIGRATE_PEMBAYARAN] ADD  CONSTRAINT [DF_MIGRATE_PEMBAYARAN_NamaTagihan]  DEFAULT ('') FOR [NamaTagihan]
GO
ALTER TABLE [dbo].[MIGRATE_PEMBAYARAN] ADD  CONSTRAINT [DF_MIGRATE_PEMBAYARAN_Rekening]  DEFAULT ('') FOR [Rekening]
GO
ALTER TABLE [dbo].[MIGRATE_PEMBAYARAN] ADD  DEFAULT (getdate()) FOR [TglInput]
GO
ALTER TABLE [dbo].[MIGRATE_PEMBAYARAN] ADD  DEFAULT ((0)) FOR [Approved]
GO
ALTER TABLE [dbo].[MIGRATE_PEMBAYARAN] ADD  DEFAULT ('') FOR [NoTTSManual]
GO
ALTER TABLE [dbo].[MIGRATE_TAGIHAN] ADD  CONSTRAINT [DF_MIGRATE_TAGIHAN_NamaTagihan]  DEFAULT ('') FOR [NamaTagihan]
GO
ALTER TABLE [dbo].[MIGRATE_TAGIHAN] ADD  CONSTRAINT [DF_MIGRATE_TAGIHAN_Tipe]  DEFAULT ('') FOR [Tipe]
GO
ALTER TABLE [dbo].[MIGRATE_TAGIHAN] ADD  CONSTRAINT [DF_MIGRATE_TAGIHAN_TglJT]  DEFAULT (getdate()) FOR [TglJT]
GO
ALTER TABLE [dbo].[MIGRATE_TAGIHAN] ADD  CONSTRAINT [DF_MIGRATE_TAGIHAN_NilaiTagihan]  DEFAULT ((0)) FOR [NilaiTagihan]
GO
ALTER TABLE [dbo].[MIGRATE_TAGIHAN] ADD  CONSTRAINT [DF_MIGRATE_TAGIHAN_Denda]  DEFAULT ((0)) FOR [Denda]
GO
ALTER TABLE [dbo].[MIGRATE_TAGIHAN] ADD  DEFAULT (getdate()) FOR [TglInput]
GO
ALTER TABLE [dbo].[MIGRATE_TAGIHAN] ADD  DEFAULT ((0)) FOR [Approved]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF_MS_AGENT_Status]  DEFAULT ('A') FOR [Status]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF_MS_AGENT_Nama]  DEFAULT ('') FOR [Nama]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF_MS_AGENT_Principal]  DEFAULT ('') FOR [Principal]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF_MS_AGENT_TglInput]  DEFAULT (getdate()) FOR [TglInput]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF_MS_AGENT_TglEdit]  DEFAULT (getdate()) FOR [TglEdit]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF_MS_AGENT_Target1]  DEFAULT ((0)) FOR [Target1]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF_MS_AGENT_Target2]  DEFAULT ((0)) FOR [Target2]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF_MS_AGENT_Target3]  DEFAULT ((0)) FOR [Target3]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF_MS_AGENT_Target4]  DEFAULT ((0)) FOR [Target4]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF_MS_AGENT_Target5]  DEFAULT ((0)) FOR [Target5]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF_MS_AGENT_Skema0]  DEFAULT ((0)) FOR [Skema0]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF_MS_AGENT_Skema1]  DEFAULT ((0)) FOR [Skema1]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF_MS_AGENT_Skema2]  DEFAULT ((0)) FOR [Skema2]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF_MS_AGENT_Skema3]  DEFAULT ((0)) FOR [Skema3]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF_MS_AGENT_Skema4]  DEFAULT ((0)) FOR [Skema4]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF_MS_AGENT_Skema5]  DEFAULT ((0)) FOR [Skema5]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF_MS_AGENT_Alamat]  DEFAULT ('') FOR [Alamat]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF_MS_AGENT_Kontak]  DEFAULT ('') FOR [Kontak]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF__MS_AGENT__NPWP__21B6055D]  DEFAULT ('') FOR [NPWP]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF__MS_AGENT__Tipe__22AA2996]  DEFAULT ('') FOR [Tipe]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF__MS_AGENT__Rekeni__239E4DCF]  DEFAULT ('') FOR [Rekening]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF__MS_AGENT__Manage__4AB81AF0]  DEFAULT ('') FOR [SalesManager]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF_MS_AGENT_GeneralManager]  DEFAULT ('') FOR [GeneralManager]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF_MS_AGENT_AdminSales]  DEFAULT ('') FOR [AdminSales]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF_MS_AGENT_ProjectManager]  DEFAULT ('') FOR [ProjectManager]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF_MS_AGENT_KepalaUnitSales]  DEFAULT ('') FOR [KepalaUnitSales]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF_MS_AGENT_KepalaUnitSales1]  DEFAULT ('') FOR [MarketingSupport]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF_MS_AGENT_MarketingSupport1]  DEFAULT ('') FOR [BillingCollection]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF_MS_AGENT_Cadangan]  DEFAULT ('') FOR [Cadangan]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF_MS_AGENT_Cadangan1]  DEFAULT ('') FOR [Kinerja]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF_MS_AGENT_KantorPusat]  DEFAULT ('') FOR [KantorPusat]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF__MS_AGENT__CrossS__125EB334]  DEFAULT ((0)) FOR [CrossSelling]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF__MS_AGENT__CrossG__1352D76D]  DEFAULT ('') FOR [CrossGM]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF__MS_AGENT__CrossS__1446FBA6]  DEFAULT ('') FOR [CrossSM]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF_MS_AGENT_AtasNama]  DEFAULT ('') FOR [AtasNama]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF_MS_AGENT_RekBank]  DEFAULT ('') FOR [RekBank]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF__MS_AGENT__NpwpSa__064DE20A]  DEFAULT ('') FOR [NpwpSalesManager]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF__MS_AGENT__BankSa__07420643]  DEFAULT ('') FOR [BankSalesManager]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF__MS_AGENT__RekSal__08362A7C]  DEFAULT ('') FOR [RekSalesManager]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF__MS_AGENT__AtasNa__092A4EB5]  DEFAULT ('') FOR [AtasNamaSalesManager]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF__MS_AGENT__NpwpGe__0A1E72EE]  DEFAULT ('') FOR [NpwpGeneralManager]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF__MS_AGENT__BankGe__0B129727]  DEFAULT ('') FOR [BankGeneralManager]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF__MS_AGENT__RekGen__0C06BB60]  DEFAULT ('') FOR [RekGeneralManager]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF__MS_AGENT__AtasNa__0CFADF99]  DEFAULT ('') FOR [AtasNamaGeneralManager]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF__MS_AGENT__GMMark__0DEF03D2]  DEFAULT ('') FOR [GMMarketing]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF__MS_AGENT__NpwpGM__0EE3280B]  DEFAULT ('') FOR [NpwpGMMarketing]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF__MS_AGENT__BankGM__0FD74C44]  DEFAULT ('') FOR [BankGMMarketing]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF__MS_AGENT__RekGMM__10CB707D]  DEFAULT ('') FOR [RekGMMarketing]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF__MS_AGENT__AtasNa__11BF94B6]  DEFAULT ('') FOR [AtasNamaGMMarketing]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF__MS_AGENT__NpwpSu__12B3B8EF]  DEFAULT ('') FOR [NpwpSupporting]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF__MS_AGENT__BankSu__13A7DD28]  DEFAULT ('') FOR [BankSupporting]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF__MS_AGENT__RekSup__149C0161]  DEFAULT ('') FOR [RekSupporting]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF__MS_AGENT__AtasNa__1590259A]  DEFAULT ('') FOR [AtasNamaSupporting]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF__MS_AGENT__Bod__168449D3]  DEFAULT ('') FOR [Bod]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF__MS_AGENT__NpwpBo__17786E0C]  DEFAULT ('') FOR [NpwpBod]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF__MS_AGENT__BankBo__186C9245]  DEFAULT ('') FOR [BankBod]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF__MS_AGENT__RekBod__1960B67E]  DEFAULT ('') FOR [RekBod]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF__MS_AGENT__AtasNa__1A54DAB7]  DEFAULT ('') FOR [AtasNamaBod]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF__MS_AGENT__coAgen__1B48FEF0]  DEFAULT ('') FOR [coAgent]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF__MS_AGENT__Npwpco__1C3D2329]  DEFAULT ('') FOR [NpwpcoAgent]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF__MS_AGENT__Bankco__1D314762]  DEFAULT ('') FOR [BankcoAgent]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF__MS_AGENT__RekcoA__1E256B9B]  DEFAULT ('') FOR [RekcoAgent]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF__MS_AGENT__AtasNa__1F198FD4]  DEFAULT ('') FOR [AtasNamacoAgent]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF__MS_AGENT__Wadir__200DB40D]  DEFAULT ('') FOR [Wadir]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF__MS_AGENT__NpwpWa__2101D846]  DEFAULT ('') FOR [NpwpWadir]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF__MS_AGENT__BankWa__21F5FC7F]  DEFAULT ('') FOR [BankWadir]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF__MS_AGENT__RekWad__22EA20B8]  DEFAULT ('') FOR [RekWadir]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF__MS_AGENT__AtasNa__23DE44F1]  DEFAULT ('') FOR [AtasNamaWadir]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF__MS_AGENT__KodeSa__353DDB1D]  DEFAULT ('') FOR [KodeSales]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF__MS_AGENT__Whatsa__3631FF56]  DEFAULT ('') FOR [Whatsapp]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF__MS_AGENT__Handph__3726238F]  DEFAULT ('') FOR [Handphone]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  CONSTRAINT [DF__MS_AGENT__Jabata__381A47C8]  DEFAULT ('') FOR [Jabatan]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  DEFAULT ('') FOR [Email]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  DEFAULT ((0)) FOR [LvlAtasan]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  DEFAULT ((0)) FOR [SalesTipe]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  DEFAULT ((0)) FOR [SalesLevel]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  DEFAULT ((0)) FOR [SalesGrade]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  DEFAULT ((0)) FOR [Atasan]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_AGENT] ADD  DEFAULT ('') FOR [NoAgentNUP]
GO
ALTER TABLE [dbo].[MS_AGENT_LEVEL] ADD  CONSTRAINT [DF_MS_AGENT_LEVEL_Tipe]  DEFAULT ('Internal') FOR [Tipe]
GO
ALTER TABLE [dbo].[MS_AGENT_LEVEL] ADD  CONSTRAINT [DF_MS_AGENT_LEVEL_Limit]  DEFAULT ((0)) FOR [Limit]
GO
ALTER TABLE [dbo].[MS_AGENT_LOG] ADD  CONSTRAINT [DF_MS_AGENT_LOG_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[MS_AGENT_LOG] ADD  CONSTRAINT [DF_MS_AGENT_LOG_Aktivitas]  DEFAULT ('') FOR [Aktivitas]
GO
ALTER TABLE [dbo].[MS_AGENT_LOG] ADD  CONSTRAINT [DF_MS_AGENT_LOG_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[MS_AGENT_LOG] ADD  CONSTRAINT [DF_MS_AGENT_LOG_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[MS_AGENT_LOG] ADD  CONSTRAINT [DF_MS_AGENT_LOG_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_AGENT_LOG] ADD  CONSTRAINT [DF_MS_AGENT_LOG_Pk]  DEFAULT ('') FOR [Pk]
GO
ALTER TABLE [dbo].[MS_AGENT_LOG] ADD  CONSTRAINT [DF_MS_AGENT_LOG_Approve]  DEFAULT ('') FOR [Approve]
GO
ALTER TABLE [dbo].[MS_AGENT_LOG] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_AGENT_TARGET] ADD  CONSTRAINT [DF_MS_AGENT_TARGET_Target]  DEFAULT ((0)) FOR [Target]
GO
ALTER TABLE [dbo].[MS_AJB] ADD  CONSTRAINT [DF_MS_AJB_AJB]  DEFAULT ('B') FOR [AJB]
GO
ALTER TABLE [dbo].[MS_AJB] ADD  CONSTRAINT [DF_MS_AJB_PrintAJB]  DEFAULT ((0)) FOR [PrintAJB]
GO
ALTER TABLE [dbo].[MS_AJB] ADD  CONSTRAINT [DF_MS_AJB_StatusAJB]  DEFAULT ('') FOR [StatusAJB]
GO
ALTER TABLE [dbo].[MS_AJB] ADD  CONSTRAINT [DF_MS_AJB_NamaNotaris]  DEFAULT ('') FOR [NamaNotaris]
GO
ALTER TABLE [dbo].[MS_AJB] ADD  CONSTRAINT [DF_MS_AJB_KetAJB]  DEFAULT ('') FOR [KetAJB]
GO
ALTER TABLE [dbo].[MS_AJB] ADD  CONSTRAINT [DF__MS_AJB__Biaya__3CF40B7E]  DEFAULT ((0)) FOR [Biaya]
GO
ALTER TABLE [dbo].[MS_AJB] ADD  CONSTRAINT [DF__MS_AJB__NoST__45C948A1]  DEFAULT ('') FOR [NoST]
GO
ALTER TABLE [dbo].[MS_AJB] ADD  CONSTRAINT [DF__MS_AJB__Project__6E4C3B47]  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_APPROVAL_DETAIL] ADD  CONSTRAINT [DF_MS_APPROVAL_DETAIL_Note]  DEFAULT ('') FOR [Note]
GO
ALTER TABLE [dbo].[MS_APPROVAL_GN] ADD  CONSTRAINT [DF_MS_APPROVAL_GN_BiayaAdmin]  DEFAULT ((0)) FOR [BiayaAdmin]
GO
ALTER TABLE [dbo].[MS_APPROVAL_GN] ADD  CONSTRAINT [DF_MS_APPROVAL_GN_Keterangan]  DEFAULT ('') FOR [Keterangan]
GO
ALTER TABLE [dbo].[MS_APPROVAL_GU] ADD  CONSTRAINT [DF_MS_APPROVAL_GU_BiayaAdmin]  DEFAULT ((0)) FOR [BiayaAdmin]
GO
ALTER TABLE [dbo].[MS_APPROVAL_GU] ADD  CONSTRAINT [DF_MS_APPROVAL_GU_Keterangan]  DEFAULT ('') FOR [Keterangan]
GO
ALTER TABLE [dbo].[MS_APPROVAL_RESCHEDULE] ADD  CONSTRAINT [DF_MS_APPROVAL_RESCHEDULE_SkemaBef]  DEFAULT ('') FOR [SkemaBef]
GO
ALTER TABLE [dbo].[MS_APPROVAL_RESCHEDULE] ADD  CONSTRAINT [DF_MS_APPROVAL_RESCHEDULE_SkemaAft]  DEFAULT ('') FOR [SkemaAft]
GO
ALTER TABLE [dbo].[MS_APPROVAL_RESCHEDULE] ADD  CONSTRAINT [DF_MS_APPROVAL_RESCHEDULE_CaraBayarBef]  DEFAULT ('') FOR [CaraBayarBef]
GO
ALTER TABLE [dbo].[MS_APPROVAL_RESCHEDULE] ADD  CONSTRAINT [DF_MS_APPROVAL_RESCHEDULE_CaraBayarAft]  DEFAULT ('') FOR [CaraBayarAft]
GO
ALTER TABLE [dbo].[MS_BAST] ADD  CONSTRAINT [DF_MS_BAST_NoSTm]  DEFAULT ('') FOR [NoSTm]
GO
ALTER TABLE [dbo].[MS_BAST] ADD  CONSTRAINT [DF_MS_BAST_ST]  DEFAULT ('B') FOR [ST]
GO
ALTER TABLE [dbo].[MS_BAST] ADD  CONSTRAINT [DF_MS_BAST_stu]  DEFAULT ((0)) FOR [stu]
GO
ALTER TABLE [dbo].[MS_BAST] ADD  CONSTRAINT [DF_MS_BAST_KetST]  DEFAULT ('') FOR [KetST]
GO
ALTER TABLE [dbo].[MS_BAST] ADD  CONSTRAINT [DF_MS_BAST_TargetST]  DEFAULT (getdate()) FOR [TargetST]
GO
ALTER TABLE [dbo].[MS_BAST] ADD  CONSTRAINT [DF_MS_BAST_PrintBAST]  DEFAULT ((0)) FOR [PrintBAST]
GO
ALTER TABLE [dbo].[MS_BAST] ADD  DEFAULT ((0)) FOR [LuasGross]
GO
ALTER TABLE [dbo].[MS_BAST] ADD  DEFAULT ((0)) FOR [LuasNett]
GO
ALTER TABLE [dbo].[MS_BAST] ADD  DEFAULT ((0)) FOR [Biaya]
GO
ALTER TABLE [dbo].[MS_BAST] ADD  DEFAULT ((0)) FOR [LebihBayar]
GO
ALTER TABLE [dbo].[MS_BAST] ADD  DEFAULT ((0)) FOR [MasaGaransi]
GO
ALTER TABLE [dbo].[MS_BAST] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_BERKAS_PPJB] ADD  DEFAULT ((0)) FOR [Value]
GO
ALTER TABLE [dbo].[MS_COMPLAIN] ADD  CONSTRAINT [DF_MS_COMPLAIN_NoKontrak]  DEFAULT ('') FOR [NoKontrak]
GO
ALTER TABLE [dbo].[MS_COMPLAIN] ADD  CONSTRAINT [DF_MS_COMPLAIN_NoComplain]  DEFAULT ((0)) FOR [NoComplain]
GO
ALTER TABLE [dbo].[MS_COMPLAIN] ADD  CONSTRAINT [DF_MS_COMPLAIN_NoCustomer]  DEFAULT ((0)) FOR [NoCustomer]
GO
ALTER TABLE [dbo].[MS_COMPLAIN] ADD  CONSTRAINT [DF_MS_COMPLAIN_Detil]  DEFAULT ('') FOR [Detil]
GO
ALTER TABLE [dbo].[MS_COMPLAIN] ADD  CONSTRAINT [DF_MS_COMPLAIN_Solusi]  DEFAULT ('') FOR [Solusi]
GO
ALTER TABLE [dbo].[MS_COMPLAIN] ADD  CONSTRAINT [DF_MS_COMPLAIN_TglComplain]  DEFAULT (getdate()) FOR [TglComplain]
GO
ALTER TABLE [dbo].[MS_COMPLAIN] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  CONSTRAINT [DF_MS_CUSTOMER_Status]  DEFAULT ('A') FOR [Status]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  CONSTRAINT [DF_MS_CUSTOMER_Nama]  DEFAULT ('') FOR [Nama]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  CONSTRAINT [DF_MS_CUSTOMER_NoTelp]  DEFAULT ('') FOR [NoTelp]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  CONSTRAINT [DF_MS_CUSTOMER_NoHp]  DEFAULT ('') FOR [NoHp]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  CONSTRAINT [DF_MS_CUSTOMER_NoKantor]  DEFAULT ('') FOR [NoKantor]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  CONSTRAINT [DF_MS_CUSTOMER_NoFax]  DEFAULT ('') FOR [NoFax]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  CONSTRAINT [DF_MS_CUSTOMER_Email]  DEFAULT ('') FOR [Email]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  CONSTRAINT [DF_MS_CUSTOMER_NamaBisnis]  DEFAULT ('') FOR [NamaBisnis]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  CONSTRAINT [DF_MS_CUSTOMER_NoKTP]  DEFAULT ('') FOR [NoKTP]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  CONSTRAINT [DF_MS_CUSTOMER_KTP1]  DEFAULT ('') FOR [KTP1]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  CONSTRAINT [DF_MS_CUSTOMER_KTP2]  DEFAULT ('') FOR [KTP2]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  CONSTRAINT [DF_MS_CUSTOMER_KTP3]  DEFAULT ('') FOR [KTP3]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  CONSTRAINT [DF_MS_CUSTOMER_KTP4]  DEFAULT ('') FOR [KTP4]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  CONSTRAINT [DF_MS_CUSTOMER_Alamat1]  DEFAULT ('') FOR [Alamat1]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  CONSTRAINT [DF_MS_CUSTOMER_Alamat2]  DEFAULT ('') FOR [Alamat2]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  CONSTRAINT [DF_MS_CUSTOMER_Alamat3]  DEFAULT ('') FOR [Alamat3]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  CONSTRAINT [DF_MS_CUSTOMER_Kantor1]  DEFAULT ('') FOR [Kantor1]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  CONSTRAINT [DF_MS_CUSTOMER_Kantor2]  DEFAULT ('') FOR [Kantor2]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  CONSTRAINT [DF_MS_CUSTOMER_Kantor3]  DEFAULT ('') FOR [Kantor3]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  CONSTRAINT [DF_MS_CUSTOMER_Agama]  DEFAULT ('') FOR [Agama]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  CONSTRAINT [DF_MS_CUSTOMER_JenisBisnis]  DEFAULT ('') FOR [JenisBisnis]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  CONSTRAINT [DF_MS_CUSTOMER_MerekBisnis]  DEFAULT ('') FOR [MerekBisnis]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  CONSTRAINT [DF_MS_CUSTOMER_UnitLama]  DEFAULT ('') FOR [UnitLama]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  CONSTRAINT [DF_MS_CUSTOMER_LuasLama]  DEFAULT ((0)) FOR [LuasLama]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  CONSTRAINT [DF_MS_CUSTOMER_TokoLama]  DEFAULT ('') FOR [TokoLama]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  CONSTRAINT [DF_MS_CUSTOMER_ZoningLama]  DEFAULT ('') FOR [ZoningLama]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  CONSTRAINT [DF_MS_CUSTOMER_GedungLama]  DEFAULT ('') FOR [GedungLama]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  CONSTRAINT [DF_MS_CUSTOMER_TeleponLama]  DEFAULT ('') FOR [TeleponLama]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  CONSTRAINT [DF_MS_CUSTOMER_AkteLama]  DEFAULT ('') FOR [AkteLama]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  CONSTRAINT [DF_MS_CUSTOMER_TipeCs]  DEFAULT ('PERORANGAN') FOR [TipeCs]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  CONSTRAINT [DF_MS_CUSTOMER_TglTransaksi]  DEFAULT (getdate()) FOR [TglTransaksi]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  CONSTRAINT [DF_MS_CUSTOMER_TglInput]  DEFAULT (getdate()) FOR [TglInput]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  CONSTRAINT [DF_MS_CUSTOMER_TglEdit]  DEFAULT (getdate()) FOR [TglEdit]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  CONSTRAINT [DF_MS_CUSTOMER_Salutation]  DEFAULT ('') FOR [Salutation]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  CONSTRAINT [DF_MS_CUSTOMER_SumberData]  DEFAULT ('') FOR [SumberData]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  CONSTRAINT [DF_MS_CUSTOMER_TempatLahir]  DEFAULT ('') FOR [TempatLahir]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  CONSTRAINT [OF_MS_KONTRAK_AgentInput]  DEFAULT ('') FOR [AgentInput]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  CONSTRAINT [DF_MS_CUSTOMER_NPWP]  DEFAULT ('') FOR [NPWP]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  DEFAULT ('') FOR [Marital]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  DEFAULT ('') FOR [Kewarganegaraan]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  DEFAULT ('') FOR [Pekerjaan]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  DEFAULT ('') FOR [NPWPAlamat1]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  DEFAULT ('') FOR [NPWPAlamat2]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  DEFAULT ('') FOR [NPWPAlamat3]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  DEFAULT ((0)) FOR [Refferator]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  DEFAULT ('') FOR [Nama2]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  DEFAULT ('') FOR [PPJBNama]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  DEFAULT ('') FOR [PenanggungjawabKorp]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  DEFAULT ('') FOR [JabatanKorp]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  DEFAULT ('') FOR [NoSKKorp]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  DEFAULT ('') FOR [BentukKorp]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  DEFAULT (getdate()) FOR [TglKTP]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  DEFAULT ((0)) FOR [KTPSeumurHidup]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  CONSTRAINT [DF_MS_CUSTOMER_KetEsales]  DEFAULT ('') FOR [KetEsales]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  CONSTRAINT [DF_MS_CUSTOMER_ProspectID]  DEFAULT ('') FOR [ProspectID]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  DEFAULT ('') FOR [Kodepos]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  CONSTRAINT [DF_MS_CUSTOMER_Alamat4]  DEFAULT ('') FOR [Alamat4]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  CONSTRAINT [DF_MS_CUSTOMER_Alamat5]  DEFAULT ('') FOR [Alamat5]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  CONSTRAINT [DF_MS_CUSTOMER_NPWPAlamat4]  DEFAULT ('') FOR [NPWPAlamat4]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  CONSTRAINT [DF_MS_CUSTOMER_NPWPAlamat5]  DEFAULT ('') FOR [NPWPAlamat5]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  CONSTRAINT [DF_MS_CUSTOMER_NamaNPWP]  DEFAULT ('') FOR [NamaNPWP]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  CONSTRAINT [DF_MS_CUSTOMER_NoHP2]  DEFAULT ('') FOR [NoHP2]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  CONSTRAINT [DF_MS_CUSTOMER_NamaKerabat]  DEFAULT ('') FOR [NamaKerabat]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  CONSTRAINT [DF_MS_CUSTOMER_Hubungan]  DEFAULT ('') FOR [Hubungan]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  CONSTRAINT [DF_MS_CUSTOMER_NoHPKerabat]  DEFAULT ('') FOR [NoHPKerabat]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  CONSTRAINT [DF_MS_CUSTOMER_EmailKerabat]  DEFAULT ('') FOR [EmailKerabat]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  CONSTRAINT [DF_MS_CUSTOMER_KTP5]  DEFAULT ('') FOR [KTP5]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  CONSTRAINT [DF_MS_CUSTOMER_Kantor4]  DEFAULT ('') FOR [Kantor4]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  CONSTRAINT [DF_MS_CUSTOMER_Kantor5]  DEFAULT ('') FOR [Kantor5]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  DEFAULT ('') FOR [RekNama]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  DEFAULT ('') FOR [RekBank]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  DEFAULT ('') FOR [RekCabang]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  DEFAULT ('') FOR [RekNo]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_CUSTOMER] ADD  DEFAULT ('') FOR [NoNUP]
GO
ALTER TABLE [dbo].[MS_CUSTOMER_JURNAL] ADD  CONSTRAINT [DF_MS_CUSTOMER_JURNAL_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[MS_CUSTOMER_JURNAL] ADD  CONSTRAINT [DF_MS_CUSTOMER_JURNAL_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[MS_CUSTOMER_JURNAL] ADD  CONSTRAINT [DF_MS_CUSTOMER_JURNAL_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_CUSTOMER_LOG] ADD  CONSTRAINT [DF_MS_CUSTOMER_LOG_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[MS_CUSTOMER_LOG] ADD  CONSTRAINT [DF_MS_CUSTOMER_LOG_Aktivitas]  DEFAULT ('') FOR [Aktivitas]
GO
ALTER TABLE [dbo].[MS_CUSTOMER_LOG] ADD  CONSTRAINT [DF_MS_CUSTOMER_LOG_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[MS_CUSTOMER_LOG] ADD  CONSTRAINT [DF_MS_CUSTOMER_LOG_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[MS_CUSTOMER_LOG] ADD  CONSTRAINT [DF_MS_CUSTOMER_LOG_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_CUSTOMER_LOG] ADD  CONSTRAINT [DF_MS_CUSTOMER_LOG_Pk]  DEFAULT ('') FOR [Pk]
GO
ALTER TABLE [dbo].[MS_CUSTOMER_LOG] ADD  CONSTRAINT [DF_MS_CUSTOMER_LOG_Approve]  DEFAULT ('') FOR [Approve]
GO
ALTER TABLE [dbo].[MS_CUSTOMER_LOG] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_FOLLOWUP] ADD  CONSTRAINT [DF_MS_FOLLOWUP_Nama]  DEFAULT ('') FOR [NoKontrak]
GO
ALTER TABLE [dbo].[MS_FOLLOWUP] ADD  CONSTRAINT [DF_MS_FOLLOWUP_TglFU]  DEFAULT (getdate()) FOR [TglFU]
GO
ALTER TABLE [dbo].[MS_FOLLOWUP] ADD  CONSTRAINT [DF_MS_FOLLOWUP_NamaGrouping]  DEFAULT ('') FOR [NamaGrouping]
GO
ALTER TABLE [dbo].[MS_FOLLOWUP] ADD  CONSTRAINT [DF_MS_FOLLOWUP_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_FOLLOWUP] ADD  CONSTRAINT [DF_MS_FOLLOWUP_NoHP]  DEFAULT ('') FOR [NoHP]
GO
ALTER TABLE [dbo].[MS_FOLLOWUP] ADD  CONSTRAINT [DF_MS_FOLLOWUP_Alamat]  DEFAULT ('') FOR [Alamat]
GO
ALTER TABLE [dbo].[MS_FOLLOWUP] ADD  CONSTRAINT [DF_MS_FOLLOWUP_TglInput]  DEFAULT (getdate()) FOR [TglInput]
GO
ALTER TABLE [dbo].[MS_FOLLOWUP] ADD  DEFAULT ('') FOR [Collector]
GO
ALTER TABLE [dbo].[MS_FOLLOWUP] ADD  DEFAULT ((0)) FOR [NoTagihan]
GO
ALTER TABLE [dbo].[MS_FOLLOWUP_DETIL] ADD  CONSTRAINT [DF_MS_FOLLOWUP_DETIL_TglJT]  DEFAULT (getdate()) FOR [TglJT]
GO
ALTER TABLE [dbo].[MS_FOLLOWUP_DETIL] ADD  CONSTRAINT [DF_MS_FOLLOWUP_DETIL_NamaTagihan]  DEFAULT ('') FOR [NamaTagihan]
GO
ALTER TABLE [dbo].[MS_FOLLOWUP_DETIL] ADD  CONSTRAINT [DF_MS_FOLLOWUP_DETIL_Tipe]  DEFAULT ('') FOR [Tipe]
GO
ALTER TABLE [dbo].[MS_FOLLOWUP_DETIL] ADD  CONSTRAINT [DF_MS_FOLLOWUP_DETIL_TglJanjiBayar]  DEFAULT (getdate()) FOR [TglJanjiBayar]
GO
ALTER TABLE [dbo].[MS_FOLLOWUP_DETIL] ADD  DEFAULT ('') FOR [NoKontrak]
GO
ALTER TABLE [dbo].[MS_FOLLOWUP_LOG] ADD  CONSTRAINT [DF_MS_FOLLOWUP_LOG_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[MS_FOLLOWUP_LOG] ADD  CONSTRAINT [DF_MS_FOLLOWUP_LOG_Aktivitas]  DEFAULT ('') FOR [Aktivitas]
GO
ALTER TABLE [dbo].[MS_FOLLOWUP_LOG] ADD  CONSTRAINT [DF_MS_FOLLOWUP_LOG_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[MS_FOLLOWUP_LOG] ADD  CONSTRAINT [DF_MS_FOLLOWUP_LOG_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[MS_FOLLOWUP_LOG] ADD  CONSTRAINT [DF_MS_FOLLOWUP_LOG_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_FOLLOWUP_LOG] ADD  CONSTRAINT [DF_MS_FOLLOWUP_LOG_Pk]  DEFAULT ('') FOR [Pk]
GO
ALTER TABLE [dbo].[MS_FOLLOWUP_LOG] ADD  CONSTRAINT [DF_MS_FOLLOWUP_LOG_Approve]  DEFAULT ('') FOR [Approve]
GO
ALTER TABLE [dbo].[MS_FOLLOWUP_LOG] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_GIMMICK] ADD  CONSTRAINT [DF_MS_GIMMICK_ItemID]  DEFAULT ('') FOR [ItemID]
GO
ALTER TABLE [dbo].[MS_GIMMICK] ADD  CONSTRAINT [DF_MS_GIMMICK_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[MS_GIMMICK] ADD  CONSTRAINT [DF_MS_GIMMICK_Nama]  DEFAULT ('') FOR [Nama]
GO
ALTER TABLE [dbo].[MS_GIMMICK] ADD  CONSTRAINT [DF_MS_GIMMICK_Tipe]  DEFAULT ((0)) FOR [Tipe]
GO
ALTER TABLE [dbo].[MS_GIMMICK] ADD  CONSTRAINT [DF_MS_GIMMICK_Satuan]  DEFAULT ('') FOR [Satuan]
GO
ALTER TABLE [dbo].[MS_GIMMICK] ADD  CONSTRAINT [DF_MS_GIMMICK_TglInput]  DEFAULT (getdate()) FOR [TglInput]
GO
ALTER TABLE [dbo].[MS_GIMMICK] ADD  CONSTRAINT [DF_MS_GIMMICK_TglEdit]  DEFAULT (getdate()) FOR [TglEdit]
GO
ALTER TABLE [dbo].[MS_GIMMICK] ADD  CONSTRAINT [DF_MS_GIMMICK_Stock]  DEFAULT ((0)) FOR [Stock]
GO
ALTER TABLE [dbo].[MS_GIMMICK] ADD  CONSTRAINT [DF_MS_GIMMICK_HargaSatuan]  DEFAULT ((0)) FOR [HargaSatuan]
GO
ALTER TABLE [dbo].[MS_GIMMICK] ADD  CONSTRAINT [DF_MS_GIMMICK_HargaTotal]  DEFAULT ((0)) FOR [HargaTotal]
GO
ALTER TABLE [dbo].[MS_GIMMICK] ADD  CONSTRAINT [DF_MS_GIMMICK_Project]  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_GIMMICK_LOG] ADD  CONSTRAINT [DF_MS_GIMMICK_LOG_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[MS_GIMMICK_LOG] ADD  CONSTRAINT [DF_MS_GIMMICK_LOG_Aktivitas]  DEFAULT ('') FOR [Aktivitas]
GO
ALTER TABLE [dbo].[MS_GIMMICK_LOG] ADD  CONSTRAINT [DF_MS_GIMMICK_LOG_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[MS_GIMMICK_LOG] ADD  CONSTRAINT [DF_MS_GIMMICK_LOG_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[MS_GIMMICK_LOG] ADD  CONSTRAINT [DF_MS_GIMMICK_LOG_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_GIMMICK_LOG] ADD  CONSTRAINT [DF_MS_GIMMICK_LOG_Pk]  DEFAULT ('') FOR [Pk]
GO
ALTER TABLE [dbo].[MS_GIMMICK_LOG] ADD  CONSTRAINT [DF_MS_GIMMICK_LOG_Approve]  DEFAULT ('') FOR [Approve]
GO
ALTER TABLE [dbo].[MS_GIMMICK_LOG] ADD  CONSTRAINT [DF_MS_GIMMICK_LOG_Project]  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_HOLD] ADD  CONSTRAINT [DF_MS_HOLD_TglHold]  DEFAULT (getdate()) FOR [TglHold]
GO
ALTER TABLE [dbo].[MS_HOLD] ADD  CONSTRAINT [DF_MS_HOLD_TglHoldExpired]  DEFAULT (getdate()) FOR [TglHoldExpired]
GO
ALTER TABLE [dbo].[MS_HOLD] ADD  CONSTRAINT [DF_MS_HOLD_NoStock]  DEFAULT ('') FOR [NoStock]
GO
ALTER TABLE [dbo].[MS_HOLD] ADD  CONSTRAINT [DF_MS_HOLD_NoCustomer]  DEFAULT ('') FOR [NoCustomer]
GO
ALTER TABLE [dbo].[MS_HOLD] ADD  CONSTRAINT [DF_MS_HOLD_NoAgent]  DEFAULT ('') FOR [NoAgent]
GO
ALTER TABLE [dbo].[MS_HOLD] ADD  CONSTRAINT [DF_MS_HOLD_NoKontrak]  DEFAULT ('') FOR [NoKontrak]
GO
ALTER TABLE [dbo].[MS_HOLD] ADD  CONSTRAINT [DF_MS_HOLD_LokasiKontrak]  DEFAULT ((0)) FOR [LokasiKontrak]
GO
ALTER TABLE [dbo].[MS_HOLD] ADD  CONSTRAINT [DF_MS_HOLD_UserClosing]  DEFAULT ('') FOR [UserClosing]
GO
ALTER TABLE [dbo].[MS_HOLD] ADD  CONSTRAINT [DF_MS_HOLD_Status]  DEFAULT ('A') FOR [Status]
GO
ALTER TABLE [dbo].[MS_HOLD_LOG] ADD  CONSTRAINT [DF_MS_HOLD_LOG_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[MS_HOLD_LOG] ADD  CONSTRAINT [DF_MS_HOLD_LOG_Aktivitas]  DEFAULT ('') FOR [Aktivitas]
GO
ALTER TABLE [dbo].[MS_HOLD_LOG] ADD  CONSTRAINT [DF_MS_HOLD_LOG_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[MS_HOLD_LOG] ADD  CONSTRAINT [DF_MS_HOLD_LOG_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[MS_HOLD_LOG] ADD  CONSTRAINT [DF_MS_HOLD_LOG_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_HOLD_LOG] ADD  CONSTRAINT [DF_MS_HOLD_LOG_Pk]  DEFAULT ('') FOR [Pk]
GO
ALTER TABLE [dbo].[MS_HOLD_LOG] ADD  CONSTRAINT [DF_MS_HOLD_LOG_Approve]  DEFAULT ('') FOR [Approve]
GO
ALTER TABLE [dbo].[MS_IMB] ADD  CONSTRAINT [DF_MS_IMB_StatusIMB]  DEFAULT ('B') FOR [StatusIMB]
GO
ALTER TABLE [dbo].[MS_IMB] ADD  CONSTRAINT [DF_MS_IMB_KetIMB]  DEFAULT ('') FOR [KetIMB]
GO
ALTER TABLE [dbo].[MS_KOMISI] ADD  CONSTRAINT [DF_MS_KOMISI_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[MS_KOMISI] ADD  CONSTRAINT [DF_MS_KOMISI_NoSkema]  DEFAULT ((0)) FOR [NoSkema]
GO
ALTER TABLE [dbo].[MS_KOMISI] ADD  CONSTRAINT [DF_MS_KOMISI_NamaSkema]  DEFAULT ('') FOR [NamaSkema]
GO
ALTER TABLE [dbo].[MS_KOMISI] ADD  CONSTRAINT [DF_MS_KOMISI_NoTermin]  DEFAULT ((0)) FOR [NoTermin]
GO
ALTER TABLE [dbo].[MS_KOMISI] ADD  CONSTRAINT [DF_MS_KOMISI_NamaTermin]  DEFAULT ('') FOR [NamaTermin]
GO
ALTER TABLE [dbo].[MS_KOMISI] ADD  CONSTRAINT [DF_MS_KOMISI_CaraBayar]  DEFAULT ('') FOR [CaraBayar]
GO
ALTER TABLE [dbo].[MS_KOMISI] ADD  CONSTRAINT [DF_MS_KOMISI_NoKontrak]  DEFAULT ('') FOR [NoKontrak]
GO
ALTER TABLE [dbo].[MS_KOMISI] ADD  CONSTRAINT [DF_MS_KOMISI_NoUnit]  DEFAULT ('') FOR [NoUnit]
GO
ALTER TABLE [dbo].[MS_KOMISI] ADD  CONSTRAINT [DF_MS_KOMISI_NoAgent]  DEFAULT ((0)) FOR [NoAgent]
GO
ALTER TABLE [dbo].[MS_KOMISI] ADD  CONSTRAINT [DF_MS_KOMISI_NamaAgent]  DEFAULT ('') FOR [NamaAgent]
GO
ALTER TABLE [dbo].[MS_KOMISI] ADD  CONSTRAINT [DF_MS_KOMISI_NoCust]  DEFAULT ((0)) FOR [NoCust]
GO
ALTER TABLE [dbo].[MS_KOMISI] ADD  CONSTRAINT [DF_MS_KOMISI_NamaCust]  DEFAULT ('') FOR [NamaCust]
GO
ALTER TABLE [dbo].[MS_KOMISI] ADD  CONSTRAINT [DF_MS_KOMISI_TglInput]  DEFAULT (getdate()) FOR [TglInput]
GO
ALTER TABLE [dbo].[MS_KOMISI] ADD  CONSTRAINT [DF_MS_KOMISI_TglUpdate]  DEFAULT (getdate()) FOR [TglUpdate]
GO
ALTER TABLE [dbo].[MS_KOMISI] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_KOMISI_CF] ADD  CONSTRAINT [DF_MS_CF_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[MS_KOMISI_CF] ADD  CONSTRAINT [DF_MS_CF_SkemaID]  DEFAULT ((0)) FOR [NoSkema]
GO
ALTER TABLE [dbo].[MS_KOMISI_CF] ADD  CONSTRAINT [DF_MS_CF_NamaSkema]  DEFAULT ('') FOR [NamaSkema]
GO
ALTER TABLE [dbo].[MS_KOMISI_CF] ADD  CONSTRAINT [DF_MS_CF_NoAgent]  DEFAULT ((0)) FOR [NoAgent]
GO
ALTER TABLE [dbo].[MS_KOMISI_CF] ADD  CONSTRAINT [DF_MS_CF_NamaAgent]  DEFAULT ('') FOR [NamaAgent]
GO
ALTER TABLE [dbo].[MS_KOMISI_CF] ADD  CONSTRAINT [DF_MS_CF_TglInput]  DEFAULT (getdate()) FOR [TglInput]
GO
ALTER TABLE [dbo].[MS_KOMISI_CF] ADD  CONSTRAINT [DF_MS_CF_TglUpdate]  DEFAULT (getdate()) FOR [TglUpdate]
GO
ALTER TABLE [dbo].[MS_KOMISI_CF] ADD  CONSTRAINT [DF__MS_CF__NoCust__646DCB52]  DEFAULT ((0)) FOR [NoCustomer]
GO
ALTER TABLE [dbo].[MS_KOMISI_CF] ADD  CONSTRAINT [DF__MS_CF__NamaCust__6561EF8B]  DEFAULT ('') FOR [NamaCust]
GO
ALTER TABLE [dbo].[MS_KOMISI_CF] ADD  CONSTRAINT [DF__MS_CF__NoUnit__665613C4]  DEFAULT ('') FOR [NoUnit]
GO
ALTER TABLE [dbo].[MS_KOMISI_CF] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_KOMISI_CF] ADD  DEFAULT ((0)) FOR [SalesTipe]
GO
ALTER TABLE [dbo].[MS_KOMISI_CF_DETAIL] ADD  CONSTRAINT [DF_MS_CF_DETAIL_NoAgent]  DEFAULT ((0)) FOR [NoAgent]
GO
ALTER TABLE [dbo].[MS_KOMISI_CF_DETAIL] ADD  CONSTRAINT [DF_MS_CF_DETAIL_NamaAgent]  DEFAULT ('') FOR [NamaAgent]
GO
ALTER TABLE [dbo].[MS_KOMISI_CF_DETAIL] ADD  CONSTRAINT [DF_MS_CF_DETAIL_PotongKomisi]  DEFAULT ((0)) FOR [PotongKomisi]
GO
ALTER TABLE [dbo].[MS_KOMISI_CF_DETAIL] ADD  CONSTRAINT [DF_MS_CF_DETAIL_Nilai]  DEFAULT ((0)) FOR [Nilai]
GO
ALTER TABLE [dbo].[MS_KOMISI_CF_LOG] ADD  CONSTRAINT [DF_MS_KOMISI_CF_LOG_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[MS_KOMISI_CF_LOG] ADD  CONSTRAINT [DF_MS_KOMISI_CF_LOG_Aktivitas]  DEFAULT ('') FOR [Aktivitas]
GO
ALTER TABLE [dbo].[MS_KOMISI_CF_LOG] ADD  CONSTRAINT [DF_MS_KOMISI_CF_LOG_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[MS_KOMISI_CF_LOG] ADD  CONSTRAINT [DF_MS_KOMISI_CF_LOG_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[MS_KOMISI_CF_LOG] ADD  CONSTRAINT [DF_MS_KOMISI_CF_LOG_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_KOMISI_CF_LOG] ADD  CONSTRAINT [DF_MS_KOMISI_CF_LOG_Pk]  DEFAULT ('') FOR [Pk]
GO
ALTER TABLE [dbo].[MS_KOMISI_CF_LOG] ADD  CONSTRAINT [DF_MS_KOMISI_CF_LOG_Approve]  DEFAULT ('') FOR [Approve]
GO
ALTER TABLE [dbo].[MS_KOMISI_CF_LOG] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_KOMISI_CFP] ADD  CONSTRAINT [DF_MS_CFP_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[MS_KOMISI_CFP] ADD  CONSTRAINT [DF_MS_CFP_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_KOMISI_CFP] ADD  CONSTRAINT [DF_MS_CFP_TglInput]  DEFAULT (getdate()) FOR [TglInput]
GO
ALTER TABLE [dbo].[MS_KOMISI_CFP] ADD  CONSTRAINT [DF_MS_CFP_TglUpdate]  DEFAULT (getdate()) FOR [TglUpdate]
GO
ALTER TABLE [dbo].[MS_KOMISI_CFP] ADD  DEFAULT ((0)) FOR [Realisasi]
GO
ALTER TABLE [dbo].[MS_KOMISI_CFP] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_KOMISI_CFP] ADD  DEFAULT ((0)) FOR [SalesTipe]
GO
ALTER TABLE [dbo].[MS_KOMISI_CFP_DETAIL] ADD  CONSTRAINT [DF_MS_CFP_DETAIL_NoCF]  DEFAULT ('') FOR [NoCF]
GO
ALTER TABLE [dbo].[MS_KOMISI_CFP_DETAIL] ADD  CONSTRAINT [DF_MS_CFP_DETAIL_SN_NoCF]  DEFAULT ((0)) FOR [SN_NoCF]
GO
ALTER TABLE [dbo].[MS_KOMISI_CFP_DETAIL] ADD  CONSTRAINT [DF_MS_CFP_DETAIL_Nilai]  DEFAULT ((0)) FOR [Nilai]
GO
ALTER TABLE [dbo].[MS_KOMISI_CFP_DETAIL] ADD  DEFAULT ((0)) FOR [NoAgent]
GO
ALTER TABLE [dbo].[MS_KOMISI_CFP_DETAIL] ADD  DEFAULT ('') FOR [NamaAgent]
GO
ALTER TABLE [dbo].[MS_KOMISI_CFP_LOG] ADD  CONSTRAINT [DF_MS_KOMISI_CFP_LOG_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[MS_KOMISI_CFP_LOG] ADD  CONSTRAINT [DF_MS_KOMISI_CFP_LOG_Aktivitas]  DEFAULT ('') FOR [Aktivitas]
GO
ALTER TABLE [dbo].[MS_KOMISI_CFP_LOG] ADD  CONSTRAINT [DF_MS_KOMISI_CFP_LOG_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[MS_KOMISI_CFP_LOG] ADD  CONSTRAINT [DF_MS_KOMISI_CFP_LOG_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[MS_KOMISI_CFP_LOG] ADD  CONSTRAINT [DF_MS_KOMISI_CFP_LOG_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_KOMISI_CFP_LOG] ADD  CONSTRAINT [DF_MS_KOMISI_CFP_LOG_Pk]  DEFAULT ('') FOR [Pk]
GO
ALTER TABLE [dbo].[MS_KOMISI_CFP_LOG] ADD  CONSTRAINT [DF_MS_KOMISI_CFP_LOG_Approve]  DEFAULT ('') FOR [Approve]
GO
ALTER TABLE [dbo].[MS_KOMISI_CFP_LOG] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_KOMISI_CFR] ADD  CONSTRAINT [DF_MS_CFR_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[MS_KOMISI_CFR] ADD  CONSTRAINT [DF_MS_CFR_NoCFP]  DEFAULT ('') FOR [NoCFP]
GO
ALTER TABLE [dbo].[MS_KOMISI_CFR] ADD  CONSTRAINT [DF_MS_CFR_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_KOMISI_CFR] ADD  CONSTRAINT [DF_MS_CFR_TglInput]  DEFAULT (getdate()) FOR [TglInput]
GO
ALTER TABLE [dbo].[MS_KOMISI_CFR] ADD  CONSTRAINT [DF_MS_CFR_TglUpdate]  DEFAULT (getdate()) FOR [TglUpdate]
GO
ALTER TABLE [dbo].[MS_KOMISI_CFR] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_KOMISI_CFR] ADD  DEFAULT ((0)) FOR [SalesTipe]
GO
ALTER TABLE [dbo].[MS_KOMISI_CFR_DETAIL] ADD  CONSTRAINT [DF_MS_CFR_DETAIL_NoCF]  DEFAULT ('') FOR [NoCF]
GO
ALTER TABLE [dbo].[MS_KOMISI_CFR_DETAIL] ADD  CONSTRAINT [DF_MS_CFR_DETAIL_SN_NoCF]  DEFAULT ((0)) FOR [SN_NoCF]
GO
ALTER TABLE [dbo].[MS_KOMISI_CFR_DETAIL] ADD  CONSTRAINT [DF_MS_CFR_DETAIL_Nilai]  DEFAULT ((0)) FOR [Nilai]
GO
ALTER TABLE [dbo].[MS_KOMISI_CFR_DETAIL] ADD  DEFAULT ((0)) FOR [NoAgent]
GO
ALTER TABLE [dbo].[MS_KOMISI_CFR_DETAIL] ADD  DEFAULT ('') FOR [NamaAgent]
GO
ALTER TABLE [dbo].[MS_KOMISI_CFR_DETAIL] ADD  DEFAULT ('') FOR [NoCFP]
GO
ALTER TABLE [dbo].[MS_KOMISI_CFR_DETAIL] ADD  DEFAULT ((0)) FOR [NilaiPPH]
GO
ALTER TABLE [dbo].[MS_KOMISI_CFR_LOG] ADD  CONSTRAINT [DF_MS_KOMISI_CFR_LOG_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[MS_KOMISI_CFR_LOG] ADD  CONSTRAINT [DF_MS_KOMISI_CFR_LOG_Aktivitas]  DEFAULT ('') FOR [Aktivitas]
GO
ALTER TABLE [dbo].[MS_KOMISI_CFR_LOG] ADD  CONSTRAINT [DF_MS_KOMISI_CFR_LOG_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[MS_KOMISI_CFR_LOG] ADD  CONSTRAINT [DF_MS_KOMISI_CFR_LOG_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[MS_KOMISI_CFR_LOG] ADD  CONSTRAINT [DF_MS_KOMISI_CFR_LOG_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_KOMISI_CFR_LOG] ADD  CONSTRAINT [DF_MS_KOMISI_CFR_LOG_Pk]  DEFAULT ('') FOR [Pk]
GO
ALTER TABLE [dbo].[MS_KOMISI_CFR_LOG] ADD  CONSTRAINT [DF_MS_KOMISI_CFR_LOG_Approve]  DEFAULT ('') FOR [Approve]
GO
ALTER TABLE [dbo].[MS_KOMISI_CFR_LOG] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_KOMISI_DETAIL] ADD  CONSTRAINT [DF_MS_KOMISI_DETAIL_NoAgent]  DEFAULT ((0)) FOR [NoAgent]
GO
ALTER TABLE [dbo].[MS_KOMISI_DETAIL] ADD  CONSTRAINT [DF_MS_KOMISI_DETAIL_NamaAgent]  DEFAULT ('') FOR [NamaAgent]
GO
ALTER TABLE [dbo].[MS_KOMISI_DETAIL] ADD  CONSTRAINT [DF_MS_KOMISI_DETAIL_Nilai]  DEFAULT ((0)) FOR [Nilai]
GO
ALTER TABLE [dbo].[MS_KOMISI_LOG] ADD  CONSTRAINT [DF_MS_KOMISI_LOG_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[MS_KOMISI_LOG] ADD  CONSTRAINT [DF_MS_KOMISI_LOG_Aktivitas]  DEFAULT ('') FOR [Aktivitas]
GO
ALTER TABLE [dbo].[MS_KOMISI_LOG] ADD  CONSTRAINT [DF_MS_KOMISI_LOG_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[MS_KOMISI_LOG] ADD  CONSTRAINT [DF_MS_KOMISI_LOG_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[MS_KOMISI_LOG] ADD  CONSTRAINT [DF_MS_KOMISI_LOG_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_KOMISI_LOG] ADD  CONSTRAINT [DF_MS_KOMISI_LOG_Pk]  DEFAULT ('') FOR [Pk]
GO
ALTER TABLE [dbo].[MS_KOMISI_LOG] ADD  CONSTRAINT [DF_MS_KOMISI_LOG_Approve]  DEFAULT ('') FOR [Approve]
GO
ALTER TABLE [dbo].[MS_KOMISI_LOG] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD] ADD  CONSTRAINT [DF_MS_KOMISI_REWARD_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD] ADD  CONSTRAINT [DF_MS_KOMISI_REWARD_NoAgent]  DEFAULT ((0)) FOR [NoAgent]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD] ADD  CONSTRAINT [DF_MS_KOMISI_REWARD_NamaAgent]  DEFAULT ('') FOR [NamaAgent]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD] ADD  CONSTRAINT [DF_MS_KOMISI_REWARD_NoSkema]  DEFAULT ((0)) FOR [NoSkema]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD] ADD  CONSTRAINT [DF_MS_KOMISI_REWARD_NamaSkema]  DEFAULT ('') FOR [NamaSkema]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD] ADD  CONSTRAINT [DF_MS_KOMISI_REWARD_Rumus]  DEFAULT ('') FOR [Rumus]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD] ADD  CONSTRAINT [DF_MS_KOMISI_REWARD_PeriodeDari]  DEFAULT (getdate()) FOR [PeriodeDari]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD] ADD  CONSTRAINT [DF_MS_KOMISI_REWARD_PeriodeSampai]  DEFAULT (getdate()) FOR [PeriodeSampai]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD] ADD  CONSTRAINT [DF_MS_KOMISI_REWARD_Reward]  DEFAULT ('') FOR [Reward]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD] ADD  CONSTRAINT [DF_MS_KOMISI_REWARD_TglInput]  DEFAULT (getdate()) FOR [TglInput]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD] ADD  CONSTRAINT [DF_MS_KOMISI_REWARD_TglUpdate]  DEFAULT (getdate()) FOR [TglUpdate]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD_DETAIL] ADD  CONSTRAINT [DF_MS_KOMISI_REWARD_DETAIL_NoKontrak]  DEFAULT ('') FOR [NoKontrak]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD_DETAIL] ADD  CONSTRAINT [DF_MS_KOMISI_REWARD_DETAIL_NoUnit]  DEFAULT ('') FOR [NoUnit]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD_DETAIL] ADD  CONSTRAINT [DF_MS_KOMISI_REWARD_DETAIL_NoCustomer]  DEFAULT ((0)) FOR [NoCustomer]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD_DETAIL] ADD  CONSTRAINT [DF_MS_KOMISI_REWARD_DETAIL_NamaCust]  DEFAULT ('') FOR [NamaCust]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD_LOG] ADD  CONSTRAINT [DF_MS_KOMISI_REWARD_LOG_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD_LOG] ADD  CONSTRAINT [DF_MS_KOMISI_REWARD_LOG_Aktivitas]  DEFAULT ('') FOR [Aktivitas]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD_LOG] ADD  CONSTRAINT [DF_MS_KOMISI_REWARD_LOG_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD_LOG] ADD  CONSTRAINT [DF_MS_KOMISI_REWARD_LOG_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD_LOG] ADD  CONSTRAINT [DF_MS_KOMISI_REWARD_LOG_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD_LOG] ADD  CONSTRAINT [DF_MS_KOMISI_REWARD_LOG_Pk]  DEFAULT ('') FOR [Pk]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD_LOG] ADD  CONSTRAINT [DF_MS_KOMISI_REWARD_LOG_Approve]  DEFAULT ('') FOR [Approve]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD_LOG] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD_P] ADD  CONSTRAINT [DF_MS_KOMISI_REWARD_P_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD_P] ADD  CONSTRAINT [DF_MS_KOMISI_REWARD_P_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD_P] ADD  CONSTRAINT [DF_MS_KOMISI_REWARD_P_TglInput]  DEFAULT (getdate()) FOR [TglInput]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD_P] ADD  CONSTRAINT [DF_MS_KOMISI_REWARD_P_TglUpdate]  DEFAULT (getdate()) FOR [TglUpdate]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD_P] ADD  CONSTRAINT [DF_MS_KOMISI_REWARD_P_Realisasi]  DEFAULT ((0)) FOR [Realisasi]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD_P] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD_P_DETAIL] ADD  CONSTRAINT [DF_MS_KOMISI_REWARD_P_DETAIL_NoReward]  DEFAULT ('') FOR [NoReward]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD_P_DETAIL] ADD  CONSTRAINT [DF_MS_KOMISI_REWARD_P_DETAIL_Reward]  DEFAULT ('') FOR [Reward]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD_P_LOG] ADD  CONSTRAINT [DF_MS_KOMISI_REWARD_P_LOG_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD_P_LOG] ADD  CONSTRAINT [DF_MS_KOMISI_REWARD_P_LOG_Aktivitas]  DEFAULT ('') FOR [Aktivitas]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD_P_LOG] ADD  CONSTRAINT [DF_MS_KOMISI_REWARD_P_LOG_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD_P_LOG] ADD  CONSTRAINT [DF_MS_KOMISI_REWARD_P_LOG_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD_P_LOG] ADD  CONSTRAINT [DF_MS_KOMISI_REWARD_P_LOG_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD_P_LOG] ADD  CONSTRAINT [DF_MS_KOMISI_REWARD_P_LOG_Pk]  DEFAULT ('') FOR [Pk]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD_P_LOG] ADD  CONSTRAINT [DF_MS_KOMISI_REWARD_P_LOG_Approve]  DEFAULT ('') FOR [Approve]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD_P_LOG] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD_R] ADD  CONSTRAINT [DF_MS_KOMISI_REWARD_R_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD_R] ADD  CONSTRAINT [DF_MS_KOMISI_REWARD_R_NoRP]  DEFAULT ('') FOR [NoRP]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD_R] ADD  CONSTRAINT [DF_MS_KOMISI_REWARD_R_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD_R] ADD  CONSTRAINT [DF_MS_KOMISI_REWARD_R_TglInput]  DEFAULT (getdate()) FOR [TglInput]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD_R] ADD  CONSTRAINT [DF_MS_KOMISI_REWARD_R_TglUpdate]  DEFAULT (getdate()) FOR [TglUpdate]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD_R] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD_R_DETAIL] ADD  CONSTRAINT [DF_MS_KOMISI_REWARD_R_DETAIL_NoReward]  DEFAULT ('') FOR [NoReward]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD_R_DETAIL] ADD  CONSTRAINT [DF_MS_KOMISI_REWARD_R_DETAIL_Reward]  DEFAULT ('') FOR [Reward]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD_R_LOG] ADD  CONSTRAINT [DF_MS_KOMISI_REWARD_R_LOG_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD_R_LOG] ADD  CONSTRAINT [DF_MS_KOMISI_REWARD_R_LOG_Aktivitas]  DEFAULT ('') FOR [Aktivitas]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD_R_LOG] ADD  CONSTRAINT [DF_MS_KOMISI_REWARD_R_LOG_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD_R_LOG] ADD  CONSTRAINT [DF_MS_KOMISI_REWARD_R_LOG_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD_R_LOG] ADD  CONSTRAINT [DF_MS_KOMISI_REWARD_R_LOG_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD_R_LOG] ADD  CONSTRAINT [DF_MS_KOMISI_REWARD_R_LOG_Pk]  DEFAULT ('') FOR [Pk]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD_R_LOG] ADD  CONSTRAINT [DF_MS_KOMISI_REWARD_R_LOG_Approve]  DEFAULT ('') FOR [Approve]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD_R_LOG] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_KOMISI_TERM] ADD  CONSTRAINT [DF_MS_KOMISI_TERM_Nama]  DEFAULT ('') FOR [Nama]
GO
ALTER TABLE [dbo].[MS_KOMISI_TERM] ADD  CONSTRAINT [DF_MS_KOMISI_TERM_PersenCair]  DEFAULT ((0)) FOR [PersenCair]
GO
ALTER TABLE [dbo].[MS_KOMISI_TERM] ADD  CONSTRAINT [DF_MS_KOMISI_TERM_NilaiCair]  DEFAULT ((0)) FOR [NilaiCair]
GO
ALTER TABLE [dbo].[MS_KOMISI_TERM] ADD  CONSTRAINT [DF_MS_KOMISI_TERM_Lunas]  DEFAULT ((0)) FOR [Lunas]
GO
ALTER TABLE [dbo].[MS_KOMISI_TERM] ADD  CONSTRAINT [DF_MS_KOMISI_TERM_PersenLunas]  DEFAULT ((0)) FOR [PersenLunas]
GO
ALTER TABLE [dbo].[MS_KOMISI_TERM] ADD  CONSTRAINT [DF_MS_KOMISI_TERM_BF]  DEFAULT ((0)) FOR [BF]
GO
ALTER TABLE [dbo].[MS_KOMISI_TERM] ADD  CONSTRAINT [DF_MS_KOMISI_TERM_PersenBF]  DEFAULT ((0)) FOR [PersenBF]
GO
ALTER TABLE [dbo].[MS_KOMISI_TERM] ADD  CONSTRAINT [DF_MS_KOMISI_TERM_DP]  DEFAULT ((0)) FOR [DP]
GO
ALTER TABLE [dbo].[MS_KOMISI_TERM] ADD  CONSTRAINT [DF_MS_KOMISI_TERM_PersenDP]  DEFAULT ((0)) FOR [PersenDP]
GO
ALTER TABLE [dbo].[MS_KOMISI_TERM] ADD  CONSTRAINT [DF_MS_KOMISI_TERM_ANG]  DEFAULT ((0)) FOR [ANG]
GO
ALTER TABLE [dbo].[MS_KOMISI_TERM] ADD  CONSTRAINT [DF_MS_KOMISI_TERM_PersenANG]  DEFAULT ((0)) FOR [PersenANG]
GO
ALTER TABLE [dbo].[MS_KOMISI_TERM] ADD  CONSTRAINT [DF_MS_KOMISI_TERM_PPJB]  DEFAULT ((0)) FOR [PPJB]
GO
ALTER TABLE [dbo].[MS_KOMISI_TERM] ADD  CONSTRAINT [DF_MS_KOMISI_TERM_AJB]  DEFAULT ((0)) FOR [AJB]
GO
ALTER TABLE [dbo].[MS_KOMISI_TERM] ADD  CONSTRAINT [DF_MS_KOMISI_TERM_AKAD]  DEFAULT ((0)) FOR [AKAD]
GO
ALTER TABLE [dbo].[MS_KOMISI_TERM] ADD  CONSTRAINT [DF_MS_KOMISI_TERM_TipeCair]  DEFAULT ((0)) FOR [TipeCair]
GO
ALTER TABLE [dbo].[MS_KOMISI_TERM] ADD  CONSTRAINT [DF__MS_KOMISI__NamaA__12349602]  DEFAULT ('') FOR [NamaAgent]
GO
ALTER TABLE [dbo].[MS_KOMISIP] ADD  CONSTRAINT [DF_MS_KOMISIP_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[MS_KOMISIP] ADD  CONSTRAINT [DF_MS_KOMISIP_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_KOMISIP] ADD  CONSTRAINT [DF_MS_KOMISIP_Realisasi]  DEFAULT ((0)) FOR [Realisasi]
GO
ALTER TABLE [dbo].[MS_KOMISIP] ADD  CONSTRAINT [DF_MS_KOMISIP_TglInput]  DEFAULT (getdate()) FOR [TglInput]
GO
ALTER TABLE [dbo].[MS_KOMISIP] ADD  CONSTRAINT [DF_MS_KOMISIP_TglUpdate]  DEFAULT (getdate()) FOR [TglUpdate]
GO
ALTER TABLE [dbo].[MS_KOMISIP] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_KOMISIP_DETAIL] ADD  CONSTRAINT [DF_MS_KOMISIP_DETAIL_NoKomisi]  DEFAULT ('') FOR [NoKomisi]
GO
ALTER TABLE [dbo].[MS_KOMISIP_DETAIL] ADD  CONSTRAINT [DF_MS_KOMISIP_DETAIL_SN_KomisiTermin]  DEFAULT ((0)) FOR [SN_KomisiTermin]
GO
ALTER TABLE [dbo].[MS_KOMISIP_DETAIL] ADD  CONSTRAINT [DF_MS_KOMISIP_DETAIL_Nilai]  DEFAULT ((0)) FOR [Nilai]
GO
ALTER TABLE [dbo].[MS_KOMISIP_LOG] ADD  CONSTRAINT [DF_MS_KOMISIP_LOG_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[MS_KOMISIP_LOG] ADD  CONSTRAINT [DF_MS_KOMISIP_LOG_Aktivitas]  DEFAULT ('') FOR [Aktivitas]
GO
ALTER TABLE [dbo].[MS_KOMISIP_LOG] ADD  CONSTRAINT [DF_MS_KOMISIP_LOG_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[MS_KOMISIP_LOG] ADD  CONSTRAINT [DF_MS_KOMISIP_LOG_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[MS_KOMISIP_LOG] ADD  CONSTRAINT [DF_MS_KOMISIP_LOG_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_KOMISIP_LOG] ADD  CONSTRAINT [DF_MS_KOMISIP_LOG_Pk]  DEFAULT ('') FOR [Pk]
GO
ALTER TABLE [dbo].[MS_KOMISIP_LOG] ADD  CONSTRAINT [DF_MS_KOMISIP_LOG_Approve]  DEFAULT ('') FOR [Approve]
GO
ALTER TABLE [dbo].[MS_KOMISIP_LOG] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_KOMISIR] ADD  CONSTRAINT [DF_MS_KOMISIR_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[MS_KOMISIR] ADD  CONSTRAINT [DF_MS_KOMISIR_NoKomisiP]  DEFAULT ('') FOR [NoKomisiP]
GO
ALTER TABLE [dbo].[MS_KOMISIR] ADD  CONSTRAINT [DF_MS_KOMISIR_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_KOMISIR] ADD  CONSTRAINT [DF_MS_KOMISIR_TglInput]  DEFAULT (getdate()) FOR [TglInput]
GO
ALTER TABLE [dbo].[MS_KOMISIR] ADD  CONSTRAINT [DF_MS_KOMISIR_TglUpdate]  DEFAULT (getdate()) FOR [TglUpdate]
GO
ALTER TABLE [dbo].[MS_KOMISIR] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_KOMISIR_DETAIL] ADD  CONSTRAINT [DF_MS_KOMISIR_DETAIL_NoKomisi]  DEFAULT ('') FOR [NoKomisi]
GO
ALTER TABLE [dbo].[MS_KOMISIR_DETAIL] ADD  CONSTRAINT [DF_MS_KOMISIR_DETAIL_SN_KomisiTermin]  DEFAULT ((0)) FOR [SN_KomisiTermin]
GO
ALTER TABLE [dbo].[MS_KOMISIR_DETAIL] ADD  CONSTRAINT [DF_MS_KOMISIR_DETAIL_Nilai]  DEFAULT ((0)) FOR [Nilai]
GO
ALTER TABLE [dbo].[MS_KOMISIR_DETAIL] ADD  DEFAULT ((0)) FOR [NoAgent]
GO
ALTER TABLE [dbo].[MS_KOMISIR_LOG] ADD  CONSTRAINT [DF_MS_KOMISIR_LOG_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[MS_KOMISIR_LOG] ADD  CONSTRAINT [DF_MS_KOMISIR_LOG_Aktivitas]  DEFAULT ('') FOR [Aktivitas]
GO
ALTER TABLE [dbo].[MS_KOMISIR_LOG] ADD  CONSTRAINT [DF_MS_KOMISIR_LOG_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[MS_KOMISIR_LOG] ADD  CONSTRAINT [DF_MS_KOMISIR_LOG_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[MS_KOMISIR_LOG] ADD  CONSTRAINT [DF_MS_KOMISIR_LOG_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_KOMISIR_LOG] ADD  CONSTRAINT [DF_MS_KOMISIR_LOG_Pk]  DEFAULT ('') FOR [Pk]
GO
ALTER TABLE [dbo].[MS_KOMISIR_LOG] ADD  CONSTRAINT [DF_MS_KOMISIR_LOG_Approve]  DEFAULT ('') FOR [Approve]
GO
ALTER TABLE [dbo].[MS_KOMISIR_LOG] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_NoKontrak]  DEFAULT ('') FOR [NoKontrak]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_Status]  DEFAULT ('A') FOR [Status]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_TglKontrak]  DEFAULT (getdate()) FOR [TglKontrak]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_Jenis]  DEFAULT ('') FOR [Jenis]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_Lokasi]  DEFAULT ('') FOR [Lokasi]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_NoUnit_1]  DEFAULT ('') FOR [NoUnit]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_Luas]  DEFAULT ((0)) FOR [Luas]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_Gross]  DEFAULT ((0)) FOR [Gross]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_DiskonRupiah]  DEFAULT ((0)) FOR [DiskonRupiah]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_DiskonPersen]  DEFAULT ('') FOR [DiskonPersen]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_DiskonKet_1]  DEFAULT ('') FOR [DiskonKet]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_NilaiKontrak]  DEFAULT ((0)) FOR [NilaiKontrak]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_OutBalance]  DEFAULT ((0)) FOR [OutBalance]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_TglInput]  DEFAULT (getdate()) FOR [TglInput]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_TglEdit]  DEFAULT (getdate()) FOR [TglEdit]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_FlagGross]  DEFAULT ((0)) FOR [FlagGross]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_Skema]  DEFAULT ('') FOR [Skema]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_SkemaKomisi]  DEFAULT ('') FOR [SkemaKomisi]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_AlasanBatal]  DEFAULT ('') FOR [AlasanBatal]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_ST]  DEFAULT ('B') FOR [ST]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_NoST]  DEFAULT ('') FOR [NoST]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_FlagKomisi]  DEFAULT ((0)) FOR [FlagKomisi]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_TargetST]  DEFAULT (getdate()) FOR [TargetST]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_PrintSP]  DEFAULT ((0)) FOR [PrintSP]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_PrintPPJB]  DEFAULT ((0)) FOR [PrintPPJB]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_PrintAJB]  DEFAULT ((0)) FOR [PrintAJB]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_PrintBAST]  DEFAULT ((0)) FOR [PrintBAST]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_PrintRKOM]  DEFAULT ((0)) FOR [PrintRKOM]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_PrintKUP]  DEFAULT ((0)) FOR [PrintKUP]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_PPJB]  DEFAULT ('B') FOR [PPJB]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_NoPPJB]  DEFAULT ('') FOR [NoPPJB]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_AJB]  DEFAULT ('B') FOR [AJB]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_NoAJB]  DEFAULT ('') FOR [NoAJB]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_PersenLunas]  DEFAULT ((0)) FOR [PersenLunas]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_BatalMasuk]  DEFAULT ((0)) FOR [BatalMasuk]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [OF_MS_KONTRAK_JenisPPN]  DEFAULT ('KONSUMEN') FOR [JenisPPN]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [OF_MS_KONTRAK_NoKwitansiGabung]  DEFAULT ((0)) FOR [NoKwitansiGabung]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [OF_MS_KONTRAK_PrintKwitansiGabung]  DEFAULT ((0)) FOR [PrintKwitansiGabung]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [OF_MS_KONTRAK_NoVoucher]  DEFAULT ('') FOR [NoVoucher]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_FOBO]  DEFAULT ((0)) FOR [FOBO]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [OF_MS_KONTRAK_Akunting]  DEFAULT ((0)) FOR [FOBO1]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_FOBO21]  DEFAULT ((0)) FOR [FOBO3]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_Akunting2]  DEFAULT ((0)) FOR [FOBO2]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_JenisPenjualan]  DEFAULT ((0)) FOR [JenisPenjualan]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_JenisKPR]  DEFAULT ((0)) FOR [JenisKPR]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_NilaiRealisasiKPR]  DEFAULT ((0)) FOR [NilaiRealisasiKPR]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_RekeningCairKPR]  DEFAULT ('') FOR [RekeningCairKPR]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [OF_MS_KONTRAK_NilaiKlaim]  DEFAULT ((0)) FOR [NilaiKlaim]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [OF_MS_KONTRAK_TotalLunasBatal]  DEFAULT ((0)) FOR [TotalLunasBatal]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [OF_MS_KONTRAK_NilaiPulang]  DEFAULT ((0)) FOR [NilaiPulang]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_FOBOBatal]  DEFAULT ((0)) FOR [FOBOBatal]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_Acc]  DEFAULT ('') FOR [AccBatal]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_PPnNominal]  DEFAULT ((0)) FOR [PPN]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_NilaiPPN]  DEFAULT ((0)) FOR [NilaiPPN]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_CaraBayar]  DEFAULT ('') FOR [CaraBayar]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_StatusWawancara]  DEFAULT ('') FOR [StatusWawancara]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_LokasiWawancara]  DEFAULT ('') FOR [LokasiWawancara]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_KetWawancara]  DEFAULT ('') FOR [KetWawancara]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_StatusSP3K]  DEFAULT ('') FOR [StatusSP3K]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_NoSP3K]  DEFAULT ('') FOR [NoSP3K]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_HasilSP3K]  DEFAULT ('') FOR [HasilSP3K]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_KetSP3K]  DEFAULT ('') FOR [KetSP3k]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_StatusLPA]  DEFAULT ('') FOR [StatusLPA]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_NoLPA]  DEFAULT ('') FOR [NoLPA]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_KetLPA]  DEFAULT ('') FOR [KetLPA]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_StatusAkad]  DEFAULT ('') FOR [StatusAkad]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_NoAkad]  DEFAULT ('') FOR [NoAkad]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_KetAkad]  DEFAULT ('') FOR [KetAkad]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_StatusAJB]  DEFAULT ('') FOR [StatusAJB]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_KetAJB]  DEFAULT ('') FOR [KetAJB]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_Pengajuan]  DEFAULT ((0)) FOR [NilaiPengajuan]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_StatusOTS]  DEFAULT ('') FOR [StatusOTS]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_HasilOTS]  DEFAULT ('') FOR [HasilOTS]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_KetOTS]  DEFAULT ('') FOR [KetOTS]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_ApprovalKPR]  DEFAULT ((0)) FOR [ApprovalKPR]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_RealisasiAkad]  DEFAULT ((0)) FOR [RealisasiAkad]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_NamaNotaris]  DEFAULT ('') FOR [NamaNotaris]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_NoIMB]  DEFAULT ('') FOR [NoIMB]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_NoPBB]  DEFAULT ('') FOR [NoPBB]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_NoSertifikat]  DEFAULT ('') FOR [NoSertifikat]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_StatusSertifikat]  DEFAULT ((0)) FOR [StatusSertifikat]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_KetIMB]  DEFAULT ('') FOR [KetIMB]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_StatusHak]  DEFAULT ('') FOR [StatusHak]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_BankKPR]  DEFAULT ('') FOR [BankKPR]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_NoRoya]  DEFAULT ((0)) FOR [NoRoya]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_NamaPerusahaan]  DEFAULT ('') FOR [NamaPerusahaan]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_JangkaWaktu]  DEFAULT ((0)) FOR [JangkaWaktu]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_NoPengukuranBidang]  DEFAULT ((0)) FOR [NoPengukuranBidang]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_NoSuratUkur]  DEFAULT ((0)) FOR [NoSuratUkur]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_JumlahBarang]  DEFAULT ((0)) FOR [JumlahBarang]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_StatusBerkas]  DEFAULT ((0)) FOR [StatusBerkas]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_CheckListDokumen]  DEFAULT ('') FOR [CheckListDokumen]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_StatusIMB]  DEFAULT ((0)) FOR [StatusIMB]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_BungaPersen]  DEFAULT ('') FOR [BungaPersen]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_BungaNominal]  DEFAULT ((0)) FOR [BungaNominal]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__NoFPS__671F4F74]  DEFAULT ('') FOR [NoFPS]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__Print__681373AD]  DEFAULT ('0') FOR [PrintFPS]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_FlagCaraBayar]  DEFAULT ('0') FOR [FlagCaraBayar]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__Nilai__60FC61CA]  DEFAULT ((0)) FOR [NilaiDPP]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__FlagP__62E4AA3C]  DEFAULT ((0)) FOR [FlagProsesBatal]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__Nilai__6AEFE058]  DEFAULT ('0') FOR [NilaiKembali]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__Disko__6BE40491]  DEFAULT ('0') FOR [DiskonTambahan]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRAK__Note__6CD828CA]  DEFAULT ('') FOR [Note]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__Print__6DCC4D03]  DEFAULT ('0') FOR [PrintJadwalTagihan]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__Harga__6EC0713C]  DEFAULT ('0') FOR [HargaLainLain]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__Harga__6FB49575]  DEFAULT ('0') FOR [HargaGimmick]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_NoVA]  DEFAULT ('') FOR [NoVA]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_ApprovalGN]  DEFAULT ((0)) FOR [ApprovalGN]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_ApprovalGU]  DEFAULT ((0)) FOR [ApprovalGU]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_ApprovalBatal]  DEFAULT ((0)) FOR [ApprovalBatal]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_TempGN]  DEFAULT ('') FOR [TempGN]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_TempGU]  DEFAULT ((0)) FOR [TempGU]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_TempBiayaGN]  DEFAULT ((0)) FOR [TempBiayaGN]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_TempBiayaGU]  DEFAULT ((0)) FOR [TempBiayaGU]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_BiayaBatal]  DEFAULT ((0)) FOR [BiayaBatal]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__Sumbe__025D5595]  DEFAULT ((0)) FOR [SumberDana]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__Sumbe__035179CE]  DEFAULT ('') FOR [SumberDanaLainnya]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__Tujua__04459E07]  DEFAULT ('') FOR [TujuanKontrak]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__Revis__369C13AA]  DEFAULT ((0)) FOR [Revisi]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__PPNPe__379037E3]  DEFAULT ((0)) FOR [PPNPemerintah]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRAK__NUP__38845C1C]  DEFAULT ('') FOR [NUP]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__RefSk__39788055]  DEFAULT ((0)) FOR [RefSkema]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__FlagM__3F3159AB]  DEFAULT ((0)) FOR [FlagMEMO]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__TempB__40257DE4]  DEFAULT ((0)) FOR [TempBiayaPPH]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__NoRef__1209AD79]  DEFAULT ('') FOR [NoRefferatorAgent]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__NoRef__12FDD1B2]  DEFAULT ('') FOR [NoRefferatorCustomer]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__NoKon__1C1D2798]  DEFAULT ('') FOR [NoKontrakManual]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__novou__15DA3E5D]  DEFAULT ('') FOR [novoucherbatal]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__jurna__16CE6296]  DEFAULT ('') FOR [jurnalid]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF_MS_KONTRAK_FOBOLEASE]  DEFAULT ((0)) FOR [FOBOLEASE]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__Inclu__3AA1AEB8]  DEFAULT ((0)) FOR [IncludePPN]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__Tujua__4707859D]  DEFAULT ('') FOR [TujuanLainnya]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__NoPPJ__52793849]  DEFAULT ('') FOR [NoPPJBm]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__ppjbu__546180BB]  DEFAULT ((0)) FOR [ppjbu]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__KTPMi__7E57BA87]  DEFAULT ((0)) FOR [KTPMilik]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRAK__KK__7F4BDEC0]  DEFAULT ((0)) FOR [KK]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRAK__SNKH__004002F9]  DEFAULT ((0)) FOR [SNKH]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRAK__SKK__01342732]  DEFAULT ((0)) FOR [SKK]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRAK__RK__02284B6B]  DEFAULT ((0)) FOR [RK]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRAK__BT__031C6FA4]  DEFAULT ((0)) FOR [BT]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRAK__KW__041093DD]  DEFAULT ((0)) FOR [KW]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRAK__DU__0504B816]  DEFAULT ((0)) FOR [DU]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRAK__DL__05F8DC4F]  DEFAULT ((0)) FOR [DL]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRAK__SM__06ED0088]  DEFAULT ((0)) FOR [SM]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__KTPIs__07E124C1]  DEFAULT ((0)) FOR [KTPIstri]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__nokus__08D548FA]  DEFAULT ((0)) FOR [nokused]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__Reten__09C96D33]  DEFAULT ('') FOR [RetensiKPA]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__Jumla__0ABD916C]  DEFAULT ((0)) FOR [JumlahBidang]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__Nilai__0BB1B5A5]  DEFAULT ((0)) FOR [NilaiHPP]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__Reffe__0CA5D9DE]  DEFAULT ('') FOR [Refferal]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__Nilai__0D99FE17]  DEFAULT ((0)) FOR [NilaiHPPTanah]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__Closi__0E8E2250]  DEFAULT ('') FOR [ClosingID]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__Nilai__0F824689]  DEFAULT ((0)) FOR [NilaiKelebihanKPA]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__Titip__10766AC2]  DEFAULT ((0)) FOR [TitipJual]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__KetAl__116A8EFB]  DEFAULT ('') FOR [KetAlasanBatal]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__Paket__125EB334]  DEFAULT ((0)) FOR [PaketInvestasi]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__TglPa__1352D76D]  DEFAULT (getdate()) FOR [TglPaketInvestasi]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__NoRef__1446FBA6]  DEFAULT ((0)) FOR [NoReferratorAgent]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__NoRef__153B1FDF]  DEFAULT ((0)) FOR [NoReferratorCustomer]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__ReffC__162F4418]  DEFAULT ('') FOR [ReffCust]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__anref__17236851]  DEFAULT ('') FOR [anreff]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__bankr__18178C8A]  DEFAULT ('') FOR [bankreff]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__npwpr__190BB0C3]  DEFAULT ('') FOR [npwpreff]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__norek__19FFD4FC]  DEFAULT ('') FOR [norekreff]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__Tamba__1AF3F935]  DEFAULT ((0)) FOR [TambahanSurat]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__Tamba__1BE81D6E]  DEFAULT ((0)) FOR [TambahanBPHTB]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__Rewar__1CDC41A7]  DEFAULT ('') FOR [RewardID]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__Komis__1DD065E0]  DEFAULT ('') FOR [KomisiID]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRAK__CFID__1EC48A19]  DEFAULT ('') FOR [CFID]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__FOBOC__1FB8AE52]  DEFAULT ((0)) FOR [FOBOCOGS]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__Denda__20ACD28B]  DEFAULT ((0)) FOR [DendaST]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__Reali__21A0F6C4]  DEFAULT ((0)) FOR [RealisasiDendaST]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__Pemut__22951AFD]  DEFAULT ((0)) FOR [PemutihanDendaST]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__Proje__23893F36]  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__NamaP__247D636F]  DEFAULT ('') FOR [NamaProject]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRAK__Pers__257187A8]  DEFAULT ('') FOR [Pers]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__NamaP__2665ABE1]  DEFAULT ('') FOR [NamaPers]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__FlagR__7F76C749]  DEFAULT ((0)) FOR [FlagReschedule]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__Appro__006AEB82]  DEFAULT ((0)) FOR [ApprovelReschedule]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__FlagA__1B1EE1BE]  DEFAULT ((0)) FOR [FlagADJ]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__TempD__1C1305F7]  DEFAULT ((0)) FOR [TempDiskonTambahan]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__TempD__1D072A30]  DEFAULT ('') FOR [TempDiscTambahPersen]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__TempD__1DFB4E69]  DEFAULT ('') FOR [TempDiscTambahKet]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__TempN__1EEF72A2]  DEFAULT ((0)) FOR [TempNilaiPPN]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__TempN__1FE396DB]  DEFAULT ((0)) FOR [TempNilaiDPP]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__TempB__20D7BB14]  DEFAULT ((0)) FOR [TempBungaNominal]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__TempB__21CBDF4D]  DEFAULT ('') FOR [TempBungaPersen]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__TempB__22C00386]  DEFAULT ('') FOR [TempBungaKet]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__TempG__23B427BF]  DEFAULT ((0)) FOR [TempGross]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__TempB__24A84BF8]  DEFAULT ((0)) FOR [TempBiayaBPHTB]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__TempN__259C7031]  DEFAULT ((0)) FOR [TempNilaiKontrak]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__TempD__2690946A]  DEFAULT ('') FOR [TempDiskonPersen]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__TempD__2784B8A3]  DEFAULT ('') FOR [TempDiskonKet]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__TempD__2878DCDC]  DEFAULT ((0)) FOR [TempDiskonRupiah]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__TempF__296D0115]  DEFAULT ((0)) FOR [TempFlagGross]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__Appro__310E22DD]  DEFAULT ((0)) FOR [ApprovalCustomTagihan]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__TempP__32024716]  DEFAULT ((0)) FOR [TempPPN]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__TempS__32F66B4F]  DEFAULT ('') FOR [TempSkema]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__Pemoh__33EA8F88]  DEFAULT ('') FOR [PemohonBatal]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__Pemoh__34DEB3C1]  DEFAULT ('') FOR [PemohonGU]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__Pemoh__35D2D7FA]  DEFAULT ('') FOR [PemohonGN]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  CONSTRAINT [DF__MS_KONTRA__Lokas__10F65906]  DEFAULT ((0)) FOR [LokasiPenjualan]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  DEFAULT ('') FOR [PemohonDiskon]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  DEFAULT ('') FOR [PemohonADJ]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  DEFAULT ('') FOR [PemohonRE]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  DEFAULT ('') FOR [PemohonCU]
GO
ALTER TABLE [dbo].[MS_KONTRAK] ADD  DEFAULT ((0)) FOR [HargaTanah]
GO
ALTER TABLE [dbo].[MS_KONTRAK_AGENT] ADD  CONSTRAINT [DF_M_KONTRAK_AGENT_SalesTipe]  DEFAULT ((0)) FOR [SalesTipe]
GO
ALTER TABLE [dbo].[MS_KONTRAK_AGENT] ADD  CONSTRAINT [DF_M_KONTRAK_AGENT_SalesLevel]  DEFAULT ((0)) FOR [SalesLevel]
GO
ALTER TABLE [dbo].[MS_KONTRAK_AGENT] ADD  CONSTRAINT [DF_M_KONTRAK_AGENT_NoAgent]  DEFAULT ((0)) FOR [NoAgent]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APP_LOG] ADD  CONSTRAINT [DF_MS_KONTRAK_APP_LOG_ApprovedBy]  DEFAULT ('') FOR [ApprovedBy]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APP_LOG] ADD  CONSTRAINT [DF_MS_KONTRAK_APP_LOG_Approve]  DEFAULT ((0)) FOR [Approve]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APP_LOG] ADD  CONSTRAINT [DF_MS_KONTRAK_APP_LOG_TglApprove]  DEFAULT (getdate()) FOR [TglApprove]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APP_LOG] ADD  DEFAULT ((0)) FOR [Lvl]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APP_LOG] ADD  DEFAULT ((0)) FOR [Tipe]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APP_LOG] ADD  DEFAULT ((0)) FOR [Finish]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APP_LOG] ADD  DEFAULT ('') FOR [Komentar]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APP_LOG] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL] ADD  CONSTRAINT [DF_MS_KONTRAK_APPROVAL_NoKontrak]  DEFAULT ('') FOR [NoKontrak]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL] ADD  CONSTRAINT [DF_MS_KONTRAK_APPROVAL_NoStock]  DEFAULT ('') FOR [NoStock]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL] ADD  CONSTRAINT [DF_MS_KONTRAK_APPROVAL_NoCustomer]  DEFAULT ((0)) FOR [NoCustomer]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL] ADD  CONSTRAINT [DF_MS_KONTRAK_APPROVAL_NoAgent]  DEFAULT ((0)) FOR [NoAgent]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL] ADD  CONSTRAINT [DF_MS_KONTRAK_APPROVAL_TglKontrak]  DEFAULT (getdate()) FOR [TglKontrak]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL] ADD  CONSTRAINT [DF_MS_KONTRAK_APPROVAL_Jenis]  DEFAULT ('') FOR [Jenis]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL] ADD  CONSTRAINT [DF_MS_KONTRAK_APPROVAL_Lokasi]  DEFAULT ('') FOR [Lokasi]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL] ADD  CONSTRAINT [DF_MS_KONTRAK_APPROVAL_NoUnit]  DEFAULT ('') FOR [NoUnit]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL] ADD  CONSTRAINT [DF_MS_KONTRAK_APPROVAL_Luas]  DEFAULT ((0)) FOR [Luas]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL] ADD  CONSTRAINT [DF_MS_KONTRAK_APPROVAL_Gross]  DEFAULT ((0)) FOR [Gross]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL] ADD  CONSTRAINT [DF_MS_KONTRAK_APPROVAL_NilaiKontrak]  DEFAULT ((0)) FOR [NilaiKontrak]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL] ADD  CONSTRAINT [DF_MS_KONTRAK_APPROVAL_Skema]  DEFAULT ((0)) FOR [Skema]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL] ADD  CONSTRAINT [DF_MS_KONTRAK_APPROVAL_TargetST]  DEFAULT (getdate()) FOR [TargetST]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL] ADD  CONSTRAINT [DF_MS_KONTRAK_APPROVAL_JenisKPR]  DEFAULT ((0)) FOR [JenisKPR]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL] ADD  CONSTRAINT [DF_MS_KONTRAK_APPROVAL_CaraBayar]  DEFAULT ((0)) FOR [CaraBayar]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL] ADD  CONSTRAINT [DF_MS_KONTRAK_APPROVAL_RefSkema]  DEFAULT ((0)) FOR [RefSkema]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL] ADD  CONSTRAINT [DF_MS_KONTRAK_APPROVAL_JenisPPN]  DEFAULT ('') FOR [JenisPPN]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL] ADD  CONSTRAINT [DF_MS_KONTRAK_APPROVAL_BungaNominal]  DEFAULT ('') FOR [BungaPersen]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL] ADD  CONSTRAINT [DF_MS_KONTRAK_APPROVAL_BungaNominal_1]  DEFAULT ((0)) FOR [BungaNominal]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL] ADD  CONSTRAINT [DF_MS_KONTRAK_APPROVAL_BungaKet]  DEFAULT ('') FOR [BungaKet]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL] ADD  CONSTRAINT [DF_MS_KONTRAK_APPROVAL_SumberDana]  DEFAULT ((0)) FOR [SumberDana]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL] ADD  CONSTRAINT [DF_MS_KONTRAK_APPROVAL_SumberDanaLainnya]  DEFAULT ('') FOR [SumberDanaLainnya]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL] ADD  CONSTRAINT [DF_MS_KONTRAK_APPROVAL_TujuanKontrak]  DEFAULT ('') FOR [TujuanKontrak]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL] ADD  CONSTRAINT [DF_MS_KONTRAK_APPROVAL_LuasNett]  DEFAULT ((0)) FOR [LuasNett]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL] ADD  CONSTRAINT [DF_MS_KONTRAK_APPROVAL_LuasSG]  DEFAULT ((0)) FOR [LuasSG]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL] ADD  CONSTRAINT [DF_MS_KONTRAK_APPROVAL_DiskonRupiah]  DEFAULT ((0)) FOR [DiskonRupiah]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL] ADD  CONSTRAINT [DF_MS_KONTRAK_APPROVAL_DiskonPersen]  DEFAULT ('') FOR [DiskonPersen]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL] ADD  CONSTRAINT [DF_MS_KONTRAK_APPROVAL_NoDiskon]  DEFAULT ((0)) FOR [DiskonTambahan]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL] ADD  CONSTRAINT [DF__MS_KONTRAK__Note__7C5A637C]  DEFAULT ('') FOR [DiskonKet]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL] ADD  DEFAULT ('') FOR [NamaProject]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL] ADD  DEFAULT ('') FOR [Pers]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL] ADD  DEFAULT ('') FOR [NamaPers]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL] ADD  DEFAULT ((0)) FOR [PPN]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL] ADD  DEFAULT ((0)) FOR [PPNBulat]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL] ADD  DEFAULT ('') FOR [NoApproval]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL] ADD  DEFAULT ('') FOR [TipeApproval]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL] ADD  DEFAULT ('') FOR [FlagApproval]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL] ADD  DEFAULT ('') FOR [NamaApproval]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL] ADD  DEFAULT ('') FOR [AlasanApproval]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL] ADD  DEFAULT ((0)) FOR [StatusApproval]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL] ADD  DEFAULT ('') FOR [Status]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL] ADD  CONSTRAINT [DF_MS_KONTRAK_APPROVAL_SN]  DEFAULT ((0)) FOR [SN]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL] ADD  DEFAULT ((0)) FOR [LokasiPenjualan]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL_DETAIL] ADD  CONSTRAINT [DF_MS_KONTRAK_APPROVAL_DETAIL_Nokontrak]  DEFAULT ('') FOR [Nokontrak]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL_DETAIL] ADD  CONSTRAINT [DF_MS_KONTRAK_APPROVAL_DETAIL_SN]  DEFAULT ((0)) FOR [SN]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL_DETAIL] ADD  CONSTRAINT [DF_MS_KONTRAK_APPROVAL_DETAIL_NoStock]  DEFAULT ('') FOR [NoStock]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL_DETAIL] ADD  CONSTRAINT [DF_MS_KONTRAK_APPROVAL_DETAIL_NoCustomer]  DEFAULT ((0)) FOR [NoCustomer]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL_DETAIL] ADD  CONSTRAINT [DF_MS_KONTRAK_APPROVAL_DETAIL_TitleJabatan]  DEFAULT ('') FOR [TitleJabatan]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL_DETAIL] ADD  CONSTRAINT [DF_MS_KONTRAK_APPROVAL_DETAIL_Nama]  DEFAULT ('') FOR [Nama]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL_DETAIL] ADD  CONSTRAINT [DF_MS_KONTRAK_APPROVAL_DETAIL_FlagApprov]  DEFAULT ((0)) FOR [FlagApprov]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL_DETAIL] ADD  CONSTRAINT [DF_MS_KONTRAK_APPROVAL_DETAIL_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL_LOG] ADD  CONSTRAINT [DF_MS_KONTRAK_APPROVAL_LOG_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL_LOG] ADD  CONSTRAINT [DF_MS_KONTRAK_APPROVAL_LOG_Aktivitas]  DEFAULT ('') FOR [Aktivitas]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL_LOG] ADD  CONSTRAINT [DF_MS_KONTRAK_APPROVAL_LOG_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL_LOG] ADD  CONSTRAINT [DF_MS_KONTRAK_APPROVAL_LOG_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL_LOG] ADD  CONSTRAINT [DF_MS_KONTRAK_APPROVAL_LOG_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL_LOG] ADD  CONSTRAINT [DF_MS_KONTRAK_APPROVAL_LOG_Pk]  DEFAULT ('') FOR [Pk]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL_LOG] ADD  CONSTRAINT [DF_MS_KONTRAK_APPROVAL_LOG_Approve]  DEFAULT ('') FOR [Approve]
GO
ALTER TABLE [dbo].[MS_KONTRAK_GIMMICK] ADD  CONSTRAINT [DF_MS_KONTRAK_GIMMICK_NoKontrak]  DEFAULT ('') FOR [NoKontrak]
GO
ALTER TABLE [dbo].[MS_KONTRAK_GIMMICK] ADD  CONSTRAINT [DF_MS_KONTRAK_GIMMICK_SN]  DEFAULT ((0)) FOR [SN]
GO
ALTER TABLE [dbo].[MS_KONTRAK_GIMMICK] ADD  CONSTRAINT [DF_MS_KONTRAK_GIMMICK_ItemID]  DEFAULT ((0)) FOR [ItemID]
GO
ALTER TABLE [dbo].[MS_KONTRAK_GIMMICK] ADD  CONSTRAINT [DF_MS_KONTRAK_GIMMICK_Nama]  DEFAULT ('') FOR [Nama]
GO
ALTER TABLE [dbo].[MS_KONTRAK_GIMMICK] ADD  CONSTRAINT [DF_MS_KONTRAK_GIMMICK_Tipe]  DEFAULT ('') FOR [Tipe]
GO
ALTER TABLE [dbo].[MS_KONTRAK_GIMMICK] ADD  CONSTRAINT [DF_MS_KONTRAK_GIMMICK_Satuan]  DEFAULT ('') FOR [Satuan]
GO
ALTER TABLE [dbo].[MS_KONTRAK_GIMMICK] ADD  CONSTRAINT [DF_MS_KONTRAK_GIMMICK_Stock]  DEFAULT ((0)) FOR [Stock]
GO
ALTER TABLE [dbo].[MS_KONTRAK_GIMMICK] ADD  CONSTRAINT [DF_MS_KONTRAK_GIMMICK_HargaSatuan]  DEFAULT ((0)) FOR [HargaSatuan]
GO
ALTER TABLE [dbo].[MS_KONTRAK_GIMMICK] ADD  CONSTRAINT [DF_MS_KONTRAK_GIMMICK_HargaTotal]  DEFAULT ((0)) FOR [HargaTotal]
GO
ALTER TABLE [dbo].[MS_KONTRAK_GIMMICK] ADD  DEFAULT ('') FOR [Catatan]
GO
ALTER TABLE [dbo].[MS_KONTRAK_GIMMICK] ADD  DEFAULT ((0)) FOR [Diterima]
GO
ALTER TABLE [dbo].[MS_KONTRAK_GIMMICK] ADD  DEFAULT ((0)) FOR [PrintGimmick]
GO
ALTER TABLE [dbo].[MS_KONTRAK_JURNAL] ADD  CONSTRAINT [DF_MS_KONTRAK_JURNAL_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[MS_KONTRAK_JURNAL] ADD  CONSTRAINT [DF_MS_KONTRAK_JURNAL_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[MS_KONTRAK_JURNAL] ADD  CONSTRAINT [DF_MS_KONTRAK_JURNAL_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_KONTRAK_JURNAL] ADD  CONSTRAINT [DF_MS_KONTRAK_JURNAL_Complain]  DEFAULT ('') FOR [Complain]
GO
ALTER TABLE [dbo].[MS_KONTRAK_LOG] ADD  CONSTRAINT [DF_MS_KONTRAK_LOG_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[MS_KONTRAK_LOG] ADD  CONSTRAINT [DF_MS_KONTRAK_LOG_Aktivitas]  DEFAULT ('') FOR [Aktivitas]
GO
ALTER TABLE [dbo].[MS_KONTRAK_LOG] ADD  CONSTRAINT [DF_MS_KONTRAK_LOG_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[MS_KONTRAK_LOG] ADD  CONSTRAINT [DF_MS_KONTRAK_LOG_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[MS_KONTRAK_LOG] ADD  CONSTRAINT [DF_MS_KONTRAK_LOG_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_KONTRAK_LOG] ADD  CONSTRAINT [DF_MS_KONTRAK_LOG_Pk]  DEFAULT ('') FOR [Pk]
GO
ALTER TABLE [dbo].[MS_KONTRAK_LOG] ADD  CONSTRAINT [DF_MS_KONTRAK_LOG_Approve]  DEFAULT ('') FOR [Approve]
GO
ALTER TABLE [dbo].[MS_KONTRAK_LOG] ADD  CONSTRAINT [DF__MS_KONTRA__Proje__79E80B25]  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_LAUNCHING_CALL] ADD  CONSTRAINT [DF_MS_LAUNCHING_CALL_CounterID]  DEFAULT ((0)) FOR [CounterID]
GO
ALTER TABLE [dbo].[MS_LAUNCHING_CALL] ADD  CONSTRAINT [DF_MS_LAUNCHING_CALL_isCalled]  DEFAULT ((0)) FOR [isCalled]
GO
ALTER TABLE [dbo].[MS_LAUNCHING_CALL] ADD  CONSTRAINT [DF_MS_LAUNCHING_CALL_Status]  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[MS_NUP] ADD  CONSTRAINT [DF_MS_NUP_NoNUP]  DEFAULT ('') FOR [NoNUP]
GO
ALTER TABLE [dbo].[MS_NUP] ADD  CONSTRAINT [DF_MS_NUP_TglDaftar]  DEFAULT (getdate()) FOR [TglDaftar]
GO
ALTER TABLE [dbo].[MS_NUP] ADD  CONSTRAINT [DF_MS_NUP_Tipe]  DEFAULT ('') FOR [Tipe]
GO
ALTER TABLE [dbo].[MS_NUP] ADD  CONSTRAINT [DF_Table_1_NilaiBayar]  DEFAULT ((0)) FOR [TglPrintNUP]
GO
ALTER TABLE [dbo].[MS_NUP] ADD  CONSTRAINT [DF_MS_NUP_PrintNUP]  DEFAULT ((0)) FOR [PrintNUP]
GO
ALTER TABLE [dbo].[MS_NUP] ADD  CONSTRAINT [DF_Table_1_TglPrint]  DEFAULT ((0)) FOR [NilaiBayar]
GO
ALTER TABLE [dbo].[MS_NUP] ADD  CONSTRAINT [DF_MS_NUP_CaraBayar]  DEFAULT ('') FOR [CaraBayar]
GO
ALTER TABLE [dbo].[MS_NUP] ADD  CONSTRAINT [DF_MS_NUP_NoRekPers]  DEFAULT ('') FOR [NoRekPers]
GO
ALTER TABLE [dbo].[MS_NUP] ADD  CONSTRAINT [DF_MS_NUP_Keterangan]  DEFAULT ('') FOR [Keterangan]
GO
ALTER TABLE [dbo].[MS_NUP] ADD  CONSTRAINT [DF_MS_NUP_Revisi]  DEFAULT ((0)) FOR [Revisi]
GO
ALTER TABLE [dbo].[MS_NUP] ADD  CONSTRAINT [DF_MS_NUP_BolehPilih]  DEFAULT ((0)) FOR [BolehPilih]
GO
ALTER TABLE [dbo].[MS_NUP] ADD  DEFAULT ('') FOR [UserInputNama]
GO
ALTER TABLE [dbo].[MS_NUP] ADD  DEFAULT ('') FOR [UserInputID]
GO
ALTER TABLE [dbo].[MS_NUP] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[MS_NUP] ADD  DEFAULT (NULL) FOR [TglAktivasi]
GO
ALTER TABLE [dbo].[MS_NUP] ADD  DEFAULT ((0)) FOR [FlagStatus]
GO
ALTER TABLE [dbo].[MS_NUP] ADD  DEFAULT ((0)) FOR [PrintRefund]
GO
ALTER TABLE [dbo].[MS_NUP] ADD  DEFAULT ('') FOR [NamaBfr]
GO
ALTER TABLE [dbo].[MS_NUP] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_NUP] ADD  DEFAULT ((0)) FOR [NoCustomerBfr]
GO
ALTER TABLE [dbo].[MS_NUP_LOG] ADD  CONSTRAINT [DF_MS_NUP_LOG_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[MS_NUP_LOG] ADD  CONSTRAINT [DF_MS_NUP_LOG_Aktivitas]  DEFAULT ('') FOR [Aktivitas]
GO
ALTER TABLE [dbo].[MS_NUP_LOG] ADD  CONSTRAINT [DF_MS_NUP_LOG_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[MS_NUP_LOG] ADD  CONSTRAINT [DF_MS_NUP_LOG_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_NUP_LOG] ADD  CONSTRAINT [DF_MS_NUP_LOG_Pk]  DEFAULT ('') FOR [Pk]
GO
ALTER TABLE [dbo].[MS_NUP_LOG] ADD  CONSTRAINT [DF_MS_NUP_LOG_Approve]  DEFAULT ('') FOR [Approve]
GO
ALTER TABLE [dbo].[MS_NUP_LOG] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_NUP_LOG] ADD  DEFAULT ('') FOR [Tipe]
GO
ALTER TABLE [dbo].[MS_NUP_PELUNASAN] ADD  CONSTRAINT [DF_MS_NUP_PELUNASAN_NoNUP]  DEFAULT ('') FOR [NoNUP]
GO
ALTER TABLE [dbo].[MS_NUP_PELUNASAN] ADD  CONSTRAINT [DF_MS_NUP_PELUNASAN_TglBayar1]  DEFAULT (getdate()) FOR [TglBayar]
GO
ALTER TABLE [dbo].[MS_NUP_PELUNASAN] ADD  CONSTRAINT [DF_MS_NUP_PELUNASAN_NilaiBayar1]  DEFAULT ((0)) FOR [NilaiBayar]
GO
ALTER TABLE [dbo].[MS_NUP_PELUNASAN] ADD  CONSTRAINT [DF_MS_NUP_PELUNASAN_CaraBayar1]  DEFAULT ('') FOR [CaraBayar]
GO
ALTER TABLE [dbo].[MS_NUP_PELUNASAN] ADD  CONSTRAINT [DF_MS_NUP_PELUNASAN_Keterangan1]  DEFAULT ('') FOR [Keterangan]
GO
ALTER TABLE [dbo].[MS_NUP_PELUNASAN] ADD  CONSTRAINT [DF_MS_NUP_PELUNASAN_NoTTS1]  DEFAULT ('') FOR [NoTTS]
GO
ALTER TABLE [dbo].[MS_NUP_PELUNASAN] ADD  CONSTRAINT [DF_MS_NUP_PELUNASAN_RekBank2]  DEFAULT ('') FOR [RekBank]
GO
ALTER TABLE [dbo].[MS_NUP_PELUNASAN] ADD  CONSTRAINT [DF_MS_NUP_PELUNASAN_PelunasanKe]  DEFAULT ((0)) FOR [PelunasanKe]
GO
ALTER TABLE [dbo].[MS_NUP_PELUNASAN] ADD  CONSTRAINT [DF_MS_NUP_PELUNASAN_FlagUntukBayar]  DEFAULT ((0)) FOR [FlagUntukBayar]
GO
ALTER TABLE [dbo].[MS_NUP_PELUNASAN] ADD  DEFAULT ('') FOR [UserInputNama]
GO
ALTER TABLE [dbo].[MS_NUP_PELUNASAN] ADD  DEFAULT ('') FOR [UserInputID]
GO
ALTER TABLE [dbo].[MS_NUP_PELUNASAN] ADD  DEFAULT ('') FOR [NoNUPHeader]
GO
ALTER TABLE [dbo].[MS_NUP_PELUNASAN] ADD  DEFAULT ('') FOR [NoTTSNUP]
GO
ALTER TABLE [dbo].[MS_NUP_PELUNASAN] ADD  DEFAULT ('') FOR [Tipe]
GO
ALTER TABLE [dbo].[MS_NUP_PELUNASAN] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_NUP_PRIORITY] ADD  CONSTRAINT [DF_MS_NUP_PRIORITY_NoNUP]  DEFAULT ('') FOR [NoNUP]
GO
ALTER TABLE [dbo].[MS_NUP_PRIORITY] ADD  CONSTRAINT [DF__MS_NUP_PR__NoSto__40657506]  DEFAULT ('') FOR [NoStock]
GO
ALTER TABLE [dbo].[MS_NUP_PRIORITY] ADD  CONSTRAINT [DF__MS_NUP_PR__Nomor__4159993F]  DEFAULT ((0)) FOR [NomorSkema]
GO
ALTER TABLE [dbo].[MS_NUP_PRIORITY] ADD  CONSTRAINT [DF_MS_NUP_PRIORITY_NoKontrak]  DEFAULT ('') FOR [NoKontrak]
GO
ALTER TABLE [dbo].[MS_NUP_PRIORITY] ADD  CONSTRAINT [DF_MS_NUP_PRIORITY_NoNUPHeader]  DEFAULT ('') FOR [NoNUPHeader]
GO
ALTER TABLE [dbo].[MS_NUP_PRIORITY] ADD  CONSTRAINT [DF_MS_NUP_PRIORITY_NoCustomerMKT]  DEFAULT ('') FOR [NoCustomerMKT]
GO
ALTER TABLE [dbo].[MS_NUP_PRIORITY] ADD  DEFAULT ('') FOR [Tipe]
GO
ALTER TABLE [dbo].[MS_NUP_PRIORITY] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_NUP_PRIORITY] ADD  DEFAULT ((0)) FOR [Harga]
GO
ALTER TABLE [dbo].[MS_PELUNASAN] ADD  CONSTRAINT [DF_MS_PELUNASAN_NoTagihan]  DEFAULT ((0)) FOR [NoTagihan]
GO
ALTER TABLE [dbo].[MS_PELUNASAN] ADD  CONSTRAINT [DF_MS_PELUNASAN_CaraBayar]  DEFAULT ('') FOR [CaraBayar]
GO
ALTER TABLE [dbo].[MS_PELUNASAN] ADD  CONSTRAINT [DF_MS_PELUNASAN_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_PELUNASAN] ADD  CONSTRAINT [DF_MS_PELUNASAN_TglPelunasan]  DEFAULT (getdate()) FOR [TglPelunasan]
GO
ALTER TABLE [dbo].[MS_PELUNASAN] ADD  CONSTRAINT [DF_MS_PELUNASAN_NilaiPelunasan]  DEFAULT ((0)) FOR [NilaiPelunasan]
GO
ALTER TABLE [dbo].[MS_PELUNASAN] ADD  CONSTRAINT [DF_MS_PELUNASAN_SudahCair]  DEFAULT ((0)) FOR [SudahCair]
GO
ALTER TABLE [dbo].[MS_PELUNASAN] ADD  CONSTRAINT [DF_MS_PELUNASAN_NoTTS]  DEFAULT ((0)) FOR [NoTTS]
GO
ALTER TABLE [dbo].[MS_PELUNASAN] ADD  CONSTRAINT [DF__MS_PELUNA__NoBKM__0E391C95]  DEFAULT ('') FOR [NoBKM]
GO
ALTER TABLE [dbo].[MS_PELUNASAN] ADD  CONSTRAINT [DF__MS_PELUNA__NoBKM__0F2D40CE]  DEFAULT ('') FOR [NoBKM2]
GO
ALTER TABLE [dbo].[MS_PELUNASAN] ADD  CONSTRAINT [DF__MS_PELUNA__NoTTS__10216507]  DEFAULT ('') FOR [NoTTS2]
GO
ALTER TABLE [dbo].[MS_PELUNASAN] ADD  CONSTRAINT [DF__MS_PELUNA__NoMEM__11158940]  DEFAULT ('0') FOR [NoMEMO]
GO
ALTER TABLE [dbo].[MS_PELUNASAN] ADD  DEFAULT ((0)) FOR [NoRealKPA]
GO
ALTER TABLE [dbo].[MS_PELUNASAN] ADD  DEFAULT ((0)) FOR [noALOKASI]
GO
ALTER TABLE [dbo].[MS_PELUNASAN] ADD  DEFAULT ((0)) FOR [NilaiDPP]
GO
ALTER TABLE [dbo].[MS_PELUNASAN] ADD  DEFAULT ((0)) FOR [NilaiPPN]
GO
ALTER TABLE [dbo].[MS_PELUNASAN_KPA] ADD  CONSTRAINT [DF_MS_PELUNASAN_KPA_NoTagihan]  DEFAULT ((0)) FOR [NoTagihan]
GO
ALTER TABLE [dbo].[MS_PELUNASAN_KPA] ADD  CONSTRAINT [DF_MS_PELUNASAN_KPA_CaraBayar]  DEFAULT ('') FOR [CaraBayar]
GO
ALTER TABLE [dbo].[MS_PELUNASAN_KPA] ADD  CONSTRAINT [DF_MS_PELUNASAN_KPA_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_PELUNASAN_KPA] ADD  CONSTRAINT [DF_MS_PELUNASAN_KPA_TglPelunasan]  DEFAULT (getdate()) FOR [TglPelunasan]
GO
ALTER TABLE [dbo].[MS_PELUNASAN_KPA] ADD  CONSTRAINT [DF_MS_PELUNASAN_KPA_NilaiPelunasan]  DEFAULT ((0)) FOR [NilaiPelunasan]
GO
ALTER TABLE [dbo].[MS_PELUNASAN_KPA] ADD  CONSTRAINT [DF_MS_PELUNASAN_KPA_SudahCair]  DEFAULT ((0)) FOR [SudahCair]
GO
ALTER TABLE [dbo].[MS_PELUNASAN_KPA] ADD  DEFAULT ((0)) FOR [NoRealKPA]
GO
ALTER TABLE [dbo].[MS_PELUNASAN_KPA] ADD  CONSTRAINT [DF_MS_PELUNASAN_KPA_NoTTS]  DEFAULT ((0)) FOR [NoTTS]
GO
ALTER TABLE [dbo].[MS_PELUNASAN_KPA] ADD  CONSTRAINT [DF_MS_PELUNASAN_KPA_Status]  DEFAULT ('') FOR [Status]
GO
ALTER TABLE [dbo].[MS_PPJB] ADD  CONSTRAINT [DF__MS_PPJB__NoPPJ__52793849]  DEFAULT ('') FOR [NoPPJBm]
GO
ALTER TABLE [dbo].[MS_PPJB] ADD  CONSTRAINT [DF_MS_PPJB_PPJB]  DEFAULT ('B') FOR [PPJB]
GO
ALTER TABLE [dbo].[MS_PPJB] ADD  CONSTRAINT [DF__MS_PPJB__ppjbu__546180BB]  DEFAULT ((0)) FOR [ppjbu]
GO
ALTER TABLE [dbo].[MS_PPJB] ADD  CONSTRAINT [DF_MS_PPJB_PrintPPJB]  DEFAULT ((0)) FOR [PrintPPJB]
GO
ALTER TABLE [dbo].[MS_PPJB] ADD  DEFAULT ((0)) FOR [KTPMilik]
GO
ALTER TABLE [dbo].[MS_PPJB] ADD  DEFAULT ((0)) FOR [KK]
GO
ALTER TABLE [dbo].[MS_PPJB] ADD  DEFAULT ((0)) FOR [SNKH]
GO
ALTER TABLE [dbo].[MS_PPJB] ADD  DEFAULT ((0)) FOR [SKK]
GO
ALTER TABLE [dbo].[MS_PPJB] ADD  DEFAULT ((0)) FOR [RK]
GO
ALTER TABLE [dbo].[MS_PPJB] ADD  DEFAULT ((0)) FOR [BT]
GO
ALTER TABLE [dbo].[MS_PPJB] ADD  DEFAULT ((0)) FOR [KW]
GO
ALTER TABLE [dbo].[MS_PPJB] ADD  DEFAULT ((0)) FOR [DU]
GO
ALTER TABLE [dbo].[MS_PPJB] ADD  DEFAULT ((0)) FOR [DL]
GO
ALTER TABLE [dbo].[MS_PPJB] ADD  DEFAULT ((0)) FOR [SM]
GO
ALTER TABLE [dbo].[MS_PPJB] ADD  DEFAULT ((0)) FOR [KTPIstri]
GO
ALTER TABLE [dbo].[MS_PPJB] ADD  DEFAULT ((0)) FOR [Biaya]
GO
ALTER TABLE [dbo].[MS_PPJB] ADD  DEFAULT ('') FOR [KetPPJB]
GO
ALTER TABLE [dbo].[MS_PPJB] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_PRICELIST] ADD  CONSTRAINT [DF__MS_PRICEL__Price__7D98A078]  DEFAULT ((0)) FOR [PriceList]
GO
ALTER TABLE [dbo].[MS_PRICELIST_HISTORY] ADD  CONSTRAINT [DF_MS_PRICELIST_HISTORY_PriceListMin]  DEFAULT ((0)) FOR [PriceListMin]
GO
ALTER TABLE [dbo].[MS_PRICELIST_HISTORY] ADD  CONSTRAINT [DF_MS_PRICELIST_HISTORY_PriceList]  DEFAULT ((0)) FOR [PriceList]
GO
ALTER TABLE [dbo].[MS_PRICELIST_HISTORY] ADD  CONSTRAINT [DF_MS_PRICELIST_HISTORY_Periode]  DEFAULT (getdate()) FOR [Periode]
GO
ALTER TABLE [dbo].[MS_PRICELIST_HISTORY] ADD  DEFAULT ((0)) FOR [PricelistKavling]
GO
ALTER TABLE [dbo].[MS_PRIORITY] ADD  CONSTRAINT [DF_PriorityLaunching_NoStock]  DEFAULT ('') FOR [NoStock]
GO
ALTER TABLE [dbo].[MS_PRIORITY] ADD  CONSTRAINT [DF_PriorityLaunching_NoUnit]  DEFAULT ('') FOR [NoUnit]
GO
ALTER TABLE [dbo].[MS_PRIORITY] ADD  CONSTRAINT [DF_MS_PRIORITY_NoCustomer]  DEFAULT ('') FOR [NoCustomer]
GO
ALTER TABLE [dbo].[MS_PRIORITY] ADD  CONSTRAINT [DF_MS_PRIORITY_NoKontrak]  DEFAULT ('') FOR [NoKontrak]
GO
ALTER TABLE [dbo].[MS_PRIORITY] ADD  CONSTRAINT [DF_MS_PRIORITY_NilaiKontrak]  DEFAULT ((0)) FOR [NilaiKontrak]
GO
ALTER TABLE [dbo].[MS_PRIORITY] ADD  CONSTRAINT [DF__MS_PRIORI__TglIn__6B44E613]  DEFAULT (getdate()) FOR [TglInput]
GO
ALTER TABLE [dbo].[MS_PRIORITY] ADD  CONSTRAINT [DF_MS_PRIORITY_TambahanLantaiStartegis]  DEFAULT ((0)) FOR [TambahanLantaiStrategis]
GO
ALTER TABLE [dbo].[MS_PRIORITY] ADD  CONSTRAINT [DF_MS_PRIORITY_TambahanViewKampus]  DEFAULT ((0)) FOR [TambahanViewKampus]
GO
ALTER TABLE [dbo].[MS_PRIORITY] ADD  CONSTRAINT [DF_MS_PRIORITY_TambahanViewCity]  DEFAULT ((0)) FOR [TambahanViewCity]
GO
ALTER TABLE [dbo].[MS_PRIORITY] ADD  CONSTRAINT [DF_MS_PRIORITY_Pricelist]  DEFAULT ((0)) FOR [Pricelist]
GO
ALTER TABLE [dbo].[MS_PRIORITY] ADD  CONSTRAINT [DF_MS_PRIORITY_Diskon]  DEFAULT ((0)) FOR [Diskon]
GO
ALTER TABLE [dbo].[MS_PRIORITY] ADD  DEFAULT ((0)) FOR [NomorSkema]
GO
ALTER TABLE [dbo].[MS_PRIORITY] ADD  CONSTRAINT [DF_MS_PRIORITY_Bunga]  DEFAULT ((0)) FOR [Bunga]
GO
ALTER TABLE [dbo].[MS_PROPERTI] ADD  CONSTRAINT [DF_MS_PROPERTI_Nama]  DEFAULT ('') FOR [Nama]
GO
ALTER TABLE [dbo].[MS_PUTIHDENDA_LOG] ADD  CONSTRAINT [DF_MS_PUTIHDENDA_LOG_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[MS_PUTIHDENDA_LOG] ADD  CONSTRAINT [DF_MS_PUTIHDENDA_LOG_Aktivitas]  DEFAULT ('') FOR [Aktivitas]
GO
ALTER TABLE [dbo].[MS_PUTIHDENDA_LOG] ADD  CONSTRAINT [DF_MS_PUTIHDENDA_LOG_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[MS_PUTIHDENDA_LOG] ADD  CONSTRAINT [DF_MS_PUTIHDENDA_LOG_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[MS_PUTIHDENDA_LOG] ADD  CONSTRAINT [DF_MS_PUTIHDENDA_LOG_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_PUTIHDENDA_LOG] ADD  CONSTRAINT [DF_MS_PUTIHDENDA_LOG_Pk]  DEFAULT ('') FOR [Pk]
GO
ALTER TABLE [dbo].[MS_PUTIHDENDA_LOG] ADD  CONSTRAINT [DF_MS_PUTIHDENDA_LOG_Approve]  DEFAULT ('') FOR [Approve]
GO
ALTER TABLE [dbo].[MS_PUTIHDENDA_LOG] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_REALISASIDENDA_LOG] ADD  CONSTRAINT [DF_MS_REALISASIDENDA_LOG_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[MS_REALISASIDENDA_LOG] ADD  CONSTRAINT [DF_MS_REALISASIDENDA_LOG_Aktivitas]  DEFAULT ('') FOR [Aktivitas]
GO
ALTER TABLE [dbo].[MS_REALISASIDENDA_LOG] ADD  CONSTRAINT [DF_MS_REALISASIDENDA_LOG_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[MS_REALISASIDENDA_LOG] ADD  CONSTRAINT [DF_MS_REALISASIDENDA_LOG_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[MS_REALISASIDENDA_LOG] ADD  CONSTRAINT [DF_MS_REALISASIDENDA_LOG_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_REALISASIDENDA_LOG] ADD  CONSTRAINT [DF_MS_REALISASIDENDA_LOG_Pk]  DEFAULT ('') FOR [Pk]
GO
ALTER TABLE [dbo].[MS_REALISASIDENDA_LOG] ADD  CONSTRAINT [DF_MS_REALISASIDENDA_LOG_Approve]  DEFAULT ('') FOR [Approve]
GO
ALTER TABLE [dbo].[MS_REALISASIDENDA_LOG] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_RESERVASI] ADD  CONSTRAINT [DF_MS_RESERVASI_Status]  DEFAULT ('A') FOR [Status]
GO
ALTER TABLE [dbo].[MS_RESERVASI] ADD  CONSTRAINT [DF_MS_RESERVASI_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[MS_RESERVASI] ADD  CONSTRAINT [DF_MS_RESERVASI_TglExpire]  DEFAULT (getdate()) FOR [TglExpire]
GO
ALTER TABLE [dbo].[MS_RESERVASI] ADD  CONSTRAINT [DF_MS_RESERVASI_Netto]  DEFAULT ((0)) FOR [Netto]
GO
ALTER TABLE [dbo].[MS_RESERVASI] ADD  CONSTRAINT [DF_MS_RESERVASI_Skema]  DEFAULT ('') FOR [Skema]
GO
ALTER TABLE [dbo].[MS_RESERVASI] ADD  CONSTRAINT [DF_MS_RESERVASI_Jenis]  DEFAULT ('') FOR [Jenis]
GO
ALTER TABLE [dbo].[MS_RESERVASI] ADD  CONSTRAINT [DF_MS_RESERVASI_Lokasi]  DEFAULT ('') FOR [Lokasi]
GO
ALTER TABLE [dbo].[MS_RESERVASI] ADD  CONSTRAINT [DF_MS_RESERVASI_NoUnit]  DEFAULT ('') FOR [NoUnit]
GO
ALTER TABLE [dbo].[MS_RESERVASI] ADD  CONSTRAINT [DF_MS_RESERVASI_TglInput]  DEFAULT (getdate()) FOR [TglInput]
GO
ALTER TABLE [dbo].[MS_RESERVASI] ADD  CONSTRAINT [DF_MS_RESERVASI_TglEdit]  DEFAULT (getdate()) FOR [TglEdit]
GO
ALTER TABLE [dbo].[MS_RESERVASI] ADD  CONSTRAINT [DF_MS_RESERVASI_PrintWL]  DEFAULT ((0)) FOR [PrintWL]
GO
ALTER TABLE [dbo].[MS_RESERVASI] ADD  CONSTRAINT [DF_MS_RESERVASI_NoQueue]  DEFAULT ((0)) FOR [NoQueue]
GO
ALTER TABLE [dbo].[MS_RESERVASI] ADD  DEFAULT ('') FOR [NoRefferatorAgent]
GO
ALTER TABLE [dbo].[MS_RESERVASI] ADD  DEFAULT ('') FOR [NoRefferatorCustomer]
GO
ALTER TABLE [dbo].[MS_RESERVASI] ADD  CONSTRAINT [DF_MS_RESERVASI_CaraBayar]  DEFAULT ('') FOR [CaraBayar]
GO
ALTER TABLE [dbo].[MS_RESERVASI] ADD  CONSTRAINT [DF_MS_RESERVASI_Acc]  DEFAULT ('') FOR [Acc]
GO
ALTER TABLE [dbo].[MS_RESERVASI] ADD  CONSTRAINT [DF_MS_RESERVASI_FOBO]  DEFAULT ((0)) FOR [FOBO]
GO
ALTER TABLE [dbo].[MS_RESERVASI] ADD  CONSTRAINT [DF_MS_RESERVASI_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[MS_RESERVASI] ADD  DEFAULT ((0)) FOR [PrintJadwalTagihan]
GO
ALTER TABLE [dbo].[MS_RESERVASI] ADD  DEFAULT ((0)) FOR [OutBalance]
GO
ALTER TABLE [dbo].[MS_RESERVASI] ADD  DEFAULT ((0)) FOR [Gross]
GO
ALTER TABLE [dbo].[MS_RESERVASI] ADD  DEFAULT ('') FOR [Supervisor]
GO
ALTER TABLE [dbo].[MS_RESERVASI] ADD  DEFAULT ('') FOR [Manager]
GO
ALTER TABLE [dbo].[MS_RESERVASI] ADD  DEFAULT ((0)) FOR [RefSkema]
GO
ALTER TABLE [dbo].[MS_RESERVASI] ADD  DEFAULT ((0)) FOR [PrintBForm]
GO
ALTER TABLE [dbo].[MS_RESERVASI] ADD  DEFAULT ((0)) FOR [IncludePPN]
GO
ALTER TABLE [dbo].[MS_RESERVASI] ADD  DEFAULT ((0)) FOR [NilaiPPN]
GO
ALTER TABLE [dbo].[MS_RESERVASI] ADD  DEFAULT ((0)) FOR [PPN]
GO
ALTER TABLE [dbo].[MS_RESERVASI] ADD  DEFAULT ((0)) FOR [NilaiDPP]
GO
ALTER TABLE [dbo].[MS_RESERVASI] ADD  DEFAULT ((0)) FOR [DiskonRupiah]
GO
ALTER TABLE [dbo].[MS_RESERVASI] ADD  DEFAULT ('') FOR [DiskonPersen]
GO
ALTER TABLE [dbo].[MS_RESERVASI] ADD  DEFAULT ((0)) FOR [NilaiReservasi]
GO
ALTER TABLE [dbo].[MS_RESERVASI] ADD  CONSTRAINT [DF_MS_RESERVASI_NoKontrak]  DEFAULT ('') FOR [NoKontrak]
GO
ALTER TABLE [dbo].[MS_RESERVASI] ADD  CONSTRAINT [DF_MS_RESERVASI_Alasan]  DEFAULT (' ') FOR [Alasan]
GO
ALTER TABLE [dbo].[MS_RESERVASI] ADD  DEFAULT ('') FOR [ReffCust]
GO
ALTER TABLE [dbo].[MS_RESERVASI] ADD  DEFAULT ('') FOR [anreff]
GO
ALTER TABLE [dbo].[MS_RESERVASI] ADD  DEFAULT ('') FOR [bankreff]
GO
ALTER TABLE [dbo].[MS_RESERVASI] ADD  DEFAULT ('') FOR [npwpreff]
GO
ALTER TABLE [dbo].[MS_RESERVASI] ADD  DEFAULT ('') FOR [norekreff]
GO
ALTER TABLE [dbo].[MS_RESERVASI] ADD  DEFAULT ((0)) FOR [BungaNominal]
GO
ALTER TABLE [dbo].[MS_RESERVASI] ADD  DEFAULT ('') FOR [BungaPersen]
GO
ALTER TABLE [dbo].[MS_RESERVASI] ADD  DEFAULT ((0)) FOR [LokasiPenjualan]
GO
ALTER TABLE [dbo].[MS_RESERVASI] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_RESERVASI] ADD  DEFAULT ('') FOR [NoReservasi2]
GO
ALTER TABLE [dbo].[MS_RESERVASI] ADD  DEFAULT ((0)) FOR [DiskonTambahan]
GO
ALTER TABLE [dbo].[MS_RESERVASI_JURNAL] ADD  CONSTRAINT [DF_MS_RESERVASI_JURNAL_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[MS_RESERVASI_JURNAL] ADD  CONSTRAINT [DF_MS_RESERVASI_JURNAL_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[MS_RESERVASI_JURNAL] ADD  CONSTRAINT [DF_MS_RESERVASI_JURNAL_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_RESERVASI_LOG] ADD  CONSTRAINT [DF_MS_RESERVASI_LOG_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[MS_RESERVASI_LOG] ADD  CONSTRAINT [DF_MS_RESERVASI_LOG_Aktivitas]  DEFAULT ('') FOR [Aktivitas]
GO
ALTER TABLE [dbo].[MS_RESERVASI_LOG] ADD  CONSTRAINT [DF_MS_RESERVASI_LOG_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[MS_RESERVASI_LOG] ADD  CONSTRAINT [DF_MS_RESERVASI_LOG_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[MS_RESERVASI_LOG] ADD  CONSTRAINT [DF_MS_RESERVASI_LOG_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_RESERVASI_LOG] ADD  CONSTRAINT [DF_MS_RESERVASI_LOG_Pk]  DEFAULT ('') FOR [Pk]
GO
ALTER TABLE [dbo].[MS_RESERVASI_LOG] ADD  CONSTRAINT [DF_MS_RESERVASI_LOG_Approve]  DEFAULT ('') FOR [Approve]
GO
ALTER TABLE [dbo].[MS_RESERVASI_LOG] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_RESERVASI_OBS] ADD  CONSTRAINT [DF_MS_RESERVASI_OBS_Reminder]  DEFAULT ((0)) FOR [Reminder]
GO
ALTER TABLE [dbo].[MS_RESERVASI_TAGIHAN] ADD  CONSTRAINT [DF_MS_RESERVASI_TAGIHAN_NoReservasi]  DEFAULT ((0)) FOR [NoReservasi]
GO
ALTER TABLE [dbo].[MS_RESERVASI_TAGIHAN] ADD  CONSTRAINT [DF_MS_RESERVASI_TAGIHAN_NoUrut]  DEFAULT ((0)) FOR [NoUrut]
GO
ALTER TABLE [dbo].[MS_RESERVASI_TAGIHAN] ADD  CONSTRAINT [DF_MS_RESERVASI_TAGIHAN_NamaTagihan]  DEFAULT ('') FOR [NamaTagihan]
GO
ALTER TABLE [dbo].[MS_RESERVASI_TAGIHAN] ADD  CONSTRAINT [DF_MS_RESERVASI_TAGIHAN_NilaiTagihan]  DEFAULT ((0)) FOR [NilaiTagihan]
GO
ALTER TABLE [dbo].[MS_RESERVASI_TAGIHAN] ADD  CONSTRAINT [DF_MS_RESERVASI_TAGIHAN_Tipe]  DEFAULT ('') FOR [Tipe]
GO
ALTER TABLE [dbo].[MS_RESERVASI_TAGIHAN] ADD  CONSTRAINT [DF_MS_RESERVASI_TAGIHAN_FlagPJT]  DEFAULT ((0)) FOR [FlagPJT]
GO
ALTER TABLE [dbo].[MS_RESERVASI_TAGIHAN] ADD  CONSTRAINT [DF_MS_RESERVASI_TAGIHAN_Denda]  DEFAULT ((0)) FOR [Denda]
GO
ALTER TABLE [dbo].[MS_RESERVASI_TAGIHAN] ADD  CONSTRAINT [DF_MS_RESERVASI_TAGIHAN_DendaReal]  DEFAULT ((0)) FOR [DendaReal]
GO
ALTER TABLE [dbo].[MS_RESERVASI_TAGIHAN] ADD  CONSTRAINT [DF_MS_RESERVASI_TAGIHAN_KPR]  DEFAULT ((0)) FOR [KPR]
GO
ALTER TABLE [dbo].[MS_RESERVASI_TAGIHAN] ADD  CONSTRAINT [DF_MS_RESERVASI_TAGIHAN_Akad]  DEFAULT ((0)) FOR [Akad]
GO
ALTER TABLE [dbo].[MS_RESERVASI_TAGIHAN] ADD  CONSTRAINT [DF_MS_RESERVASI_TAGIHAN_PutihDenda]  DEFAULT ((0)) FOR [PutihDenda]
GO
ALTER TABLE [dbo].[MS_RESERVASI_TAGIHAN] ADD  CONSTRAINT [DF_MS_RESERVASI_TAGIHAN_NilaiPutihDenda]  DEFAULT ((0)) FOR [NilaiPutihDenda]
GO
ALTER TABLE [dbo].[MS_RESERVASI_TAGIHAN] ADD  DEFAULT ((0)) FOR [NoTTS]
GO
ALTER TABLE [dbo].[MS_SERTIFIKAT] ADD  CONSTRAINT [DF_MS_SERTIFIKAT_StatusSertifikat]  DEFAULT ('B') FOR [StatusSertifikat]
GO
ALTER TABLE [dbo].[MS_SERTIFIKAT] ADD  CONSTRAINT [DF_MS_SERTIFIKAT_KetSertifikat]  DEFAULT ('') FOR [KetSertifikat]
GO
ALTER TABLE [dbo].[MS_SERTIFIKAT] ADD  CONSTRAINT [DF_MS_SERTIFIKAT_JangkaWaktu]  DEFAULT ((0)) FOR [JangkaWaktu]
GO
ALTER TABLE [dbo].[MS_SERTIFIKAT] ADD  CONSTRAINT [DF__MS_SERTIF__Statu__36BC0F3B]  DEFAULT ('') FOR [StatusHak]
GO
ALTER TABLE [dbo].[MS_SERTIFIKAT] ADD  CONSTRAINT [DF__MS_SERTIF__NoPen__37B03374]  DEFAULT ('') FOR [NoPengukuranBidang]
GO
ALTER TABLE [dbo].[MS_SERTIFIKAT] ADD  CONSTRAINT [DF__MS_SERTIF__NoSur__38A457AD]  DEFAULT ('') FOR [NoSuratUkur]
GO
ALTER TABLE [dbo].[MS_SERTIFIKAT] ADD  CONSTRAINT [DF__MS_SERTIF__NamaP__39987BE6]  DEFAULT ('') FOR [NamaPerusahaan]
GO
ALTER TABLE [dbo].[MS_SERTIFIKAT] ADD  CONSTRAINT [DF__MS_SERTIF__Jumla__3A8CA01F]  DEFAULT ((0)) FOR [JumlahBidang]
GO
ALTER TABLE [dbo].[MS_SITEPLAN] ADD  CONSTRAINT [DF_MS_SITEPLAN_isParent]  DEFAULT ((1)) FOR [isParent]
GO
ALTER TABLE [dbo].[MS_SITEPLAN] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_TAGIHAN] ADD  CONSTRAINT [DF_MS_TAGIHAN_NamaTagihan]  DEFAULT ('') FOR [NamaTagihan]
GO
ALTER TABLE [dbo].[MS_TAGIHAN] ADD  CONSTRAINT [DF_MS_TAGIHAN_TglJT]  DEFAULT (getdate()) FOR [TglJT]
GO
ALTER TABLE [dbo].[MS_TAGIHAN] ADD  CONSTRAINT [DF_MS_TAGIHAN_NilaiTagihan]  DEFAULT ((0)) FOR [NilaiTagihan]
GO
ALTER TABLE [dbo].[MS_TAGIHAN] ADD  CONSTRAINT [DF_MS_TAGIHAN_Tipe]  DEFAULT ('') FOR [Tipe]
GO
ALTER TABLE [dbo].[MS_TAGIHAN] ADD  CONSTRAINT [DF_MS_TAGIHAN_FlagPJT_1]  DEFAULT ((0)) FOR [FlagPJT]
GO
ALTER TABLE [dbo].[MS_TAGIHAN] ADD  CONSTRAINT [DF_MS_TAGIHAN_Denda]  DEFAULT ((0)) FOR [Denda]
GO
ALTER TABLE [dbo].[MS_TAGIHAN] ADD  CONSTRAINT [OF_MS_TAGIHAN_DendaReal]  DEFAULT ((0)) FOR [DendaReal]
GO
ALTER TABLE [dbo].[MS_TAGIHAN] ADD  CONSTRAINT [DF_MS_TAGIHAN_KPR]  DEFAULT ((0)) FOR [KPR]
GO
ALTER TABLE [dbo].[MS_TAGIHAN] ADD  CONSTRAINT [DF_MS_TAGIHAN_Akad]  DEFAULT ((0)) FOR [Akad]
GO
ALTER TABLE [dbo].[MS_TAGIHAN] ADD  CONSTRAINT [DF_MS_TAGIHAN_PutihDenda]  DEFAULT ((0)) FOR [PutihDenda]
GO
ALTER TABLE [dbo].[MS_TAGIHAN] ADD  CONSTRAINT [DF__MS_TAGIHA__Nilai__318258D2]  DEFAULT ((0)) FOR [NilaiPutihDenda]
GO
ALTER TABLE [dbo].[MS_TAGIHAN] ADD  DEFAULT ((0)) FOR [FlagPengajuanKPA]
GO
ALTER TABLE [dbo].[MS_TAGIHAN] ADD  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_TAGIHAN] ADD  DEFAULT ('') FOR [KetJBLog]
GO
ALTER TABLE [dbo].[MS_TAGIHAN] ADD  DEFAULT ('') FOR [Grouping]
GO
ALTER TABLE [dbo].[MS_TAGIHAN] ADD  DEFAULT ('') FOR [NoUrut2]
GO
ALTER TABLE [dbo].[MS_TAGIHAN] ADD  DEFAULT ('') FOR [Jenis]
GO
ALTER TABLE [dbo].[MS_TAGIHAN] ADD  DEFAULT ((0)) FOR [Benefit]
GO
ALTER TABLE [dbo].[MS_TAGIHAN] ADD  DEFAULT ((0)) FOR [BenefitReal]
GO
ALTER TABLE [dbo].[MS_TAGIHAN] ADD  DEFAULT ((0)) FOR [AlokasiBenefit]
GO
ALTER TABLE [dbo].[MS_TAGIHAN] ADD  DEFAULT ((0)) FOR [ApprovelReschedule]
GO
ALTER TABLE [dbo].[MS_TAGIHAN_HEADER] ADD  CONSTRAINT [DF_MS_TAGIHAN_HEADER_Skema]  DEFAULT ('') FOR [Skema]
GO
ALTER TABLE [dbo].[MS_TAGIHAN_HEADER] ADD  CONSTRAINT [DF_MS_TAGIHAN_HEADER_NoCustomer]  DEFAULT ((0)) FOR [NoCustomer]
GO
ALTER TABLE [dbo].[MS_TAGIHAN_HEADER] ADD  CONSTRAINT [DF_MS_TAGIHAN_HEADER_NoUnit]  DEFAULT ('') FOR [NoUnit]
GO
ALTER TABLE [dbo].[MS_TAGIHAN_HEADER] ADD  CONSTRAINT [DF_MS_TAGIHAN_HEADER_NoAgent]  DEFAULT ((0)) FOR [NoAgent]
GO
ALTER TABLE [dbo].[MS_TAGIHAN_HEADER] ADD  CONSTRAINT [DF_MS_TAGIHAN_HEADER_CaraBayar]  DEFAULT ('') FOR [CaraBayar]
GO
ALTER TABLE [dbo].[MS_TAGIHAN_HEADER] ADD  CONSTRAINT [DF_MS_TAGIHAN_HEADER_RefSkema]  DEFAULT ((0)) FOR [RefSkema]
GO
ALTER TABLE [dbo].[MS_TAGIHAN_HEADER] ADD  CONSTRAINT [DF_MS_TAGIHAN_HEADER_ApprovelReschedule]  DEFAULT ((0)) FOR [ApprovelReschedule]
GO
ALTER TABLE [dbo].[MS_TAGIHAN_KPA] ADD  CONSTRAINT [DF_MS_TAGIHAN_KPA_NamaTagihan]  DEFAULT ('') FOR [NamaTagihan]
GO
ALTER TABLE [dbo].[MS_TAGIHAN_KPA] ADD  CONSTRAINT [DF_MS_TAGIHAN_KPA_TglJT]  DEFAULT (getdate()) FOR [TglJT]
GO
ALTER TABLE [dbo].[MS_TAGIHAN_KPA] ADD  CONSTRAINT [DF_MS_TAGIHAN_KPA_NilaiTagihan]  DEFAULT ((0)) FOR [NilaiTagihan]
GO
ALTER TABLE [dbo].[MS_TAGIHAN_KPA] ADD  CONSTRAINT [DF_MS_TAGIHAN_KPA_Tipe]  DEFAULT ('') FOR [Tipe]
GO
ALTER TABLE [dbo].[MS_TAGIHAN_KPA] ADD  CONSTRAINT [DF_MS_TAGIHAN_KPA_FlagPJT_1]  DEFAULT ((0)) FOR [FlagPJT]
GO
ALTER TABLE [dbo].[MS_TAGIHAN_KPA] ADD  CONSTRAINT [DF_MS_TAGIHAN_KPA_Denda]  DEFAULT ((0)) FOR [Denda]
GO
ALTER TABLE [dbo].[MS_TAGIHAN_KPA] ADD  CONSTRAINT [OF_MS_TAGIHAN_KPA_DendaReal]  DEFAULT ((0)) FOR [DendaReal]
GO
ALTER TABLE [dbo].[MS_TAGIHAN_KPA] ADD  CONSTRAINT [DF_MS_TAGIHAN_KPA_KPR]  DEFAULT ((0)) FOR [KPR]
GO
ALTER TABLE [dbo].[MS_TAGIHAN_KPA] ADD  CONSTRAINT [DF_MS_TAGIHAN_KPA_Akad]  DEFAULT ((0)) FOR [Akad]
GO
ALTER TABLE [dbo].[MS_TAGIHAN_KPA] ADD  CONSTRAINT [DF_MS_TAGIHAN_KPA_PutihDenda]  DEFAULT ((0)) FOR [PutihDenda]
GO
ALTER TABLE [dbo].[MS_TAGIHAN_KPA] ADD  CONSTRAINT [DF__MS_TAGIHAN_KPA__Nilai__318258D2]  DEFAULT ((0)) FOR [NilaiPutihDenda]
GO
ALTER TABLE [dbo].[MS_TAGIHAN_KPA] ADD  CONSTRAINT [DF__MS_TAGIHA__FlagP__4D1564AE]  DEFAULT ((0)) FOR [FlagPengajuanKPA]
GO
ALTER TABLE [dbo].[MS_TAGIHAN_KPA] ADD  DEFAULT ('') FOR [NilaiTagihanTipe]
GO
ALTER TABLE [dbo].[MS_TAGIHAN_KPA] ADD  DEFAULT ((0)) FOR [NilaiTagihanPersen]
GO
ALTER TABLE [dbo].[MS_TAGIHAN_LAPORAN] ADD  CONSTRAINT [DF_MS_TAGIHAN_LAPORAN_NamaTagihan]  DEFAULT ('') FOR [NamaTagihan]
GO
ALTER TABLE [dbo].[MS_TAGIHAN_LAPORAN] ADD  CONSTRAINT [DF_MS_TAGIHAN_LAPORAN_TglJT]  DEFAULT (getdate()) FOR [TglJT]
GO
ALTER TABLE [dbo].[MS_TAGIHAN_LAPORAN] ADD  CONSTRAINT [DF_MS_TAGIHAN_LAPORAN_NilaiTagihan]  DEFAULT ((0)) FOR [NilaiTagihan]
GO
ALTER TABLE [dbo].[MS_TAGIHAN_LAPORAN] ADD  CONSTRAINT [DF_MS_TAGIHAN_LAPORAN_Tipe]  DEFAULT ('') FOR [Tipe]
GO
ALTER TABLE [dbo].[MS_TAGIHAN_LAPORAN] ADD  CONSTRAINT [DF_MS_TAGIHAN_LAPORAN_FlagPJT_1]  DEFAULT ((0)) FOR [FlagPJT]
GO
ALTER TABLE [dbo].[MS_TAGIHAN_LAPORAN] ADD  CONSTRAINT [DF_MS_TAGIHAN_LAPORAN_Denda]  DEFAULT ((0)) FOR [Denda]
GO
ALTER TABLE [dbo].[MS_TAGIHAN_LAPORAN] ADD  CONSTRAINT [OF_MS_TAGIHAN_LAPORAN_DendaReal]  DEFAULT ((0)) FOR [DendaReal]
GO
ALTER TABLE [dbo].[MS_TAGIHAN_LAPORAN] ADD  CONSTRAINT [DF_MS_TAGIHAN_LAPORAN_KPR]  DEFAULT ((0)) FOR [KPR]
GO
ALTER TABLE [dbo].[MS_TAGIHAN_LAPORAN] ADD  CONSTRAINT [DF_MS_TAGIHAN_LAPORAN_Akad]  DEFAULT ((0)) FOR [Akad]
GO
ALTER TABLE [dbo].[MS_TAGIHAN_LAPORAN] ADD  CONSTRAINT [DF_MS_TAGIHAN_LAPORAN_PutihDenda]  DEFAULT ((0)) FOR [PutihDenda]
GO
ALTER TABLE [dbo].[MS_TAGIHAN_LAPORAN] ADD  CONSTRAINT [DF_MS_TAGIHAN_LAPORAN_Nilai__318258D2]  DEFAULT ((0)) FOR [NilaiPutihDenda]
GO
ALTER TABLE [dbo].[MS_TAGIHAN_LAPORAN] ADD  CONSTRAINT [DF_MS_TAGIHAN_LAPORAN_FlagPengajuanKPA]  DEFAULT ((0)) FOR [FlagPengajuanKPA]
GO
ALTER TABLE [dbo].[MS_TAGIHAN_TEMP] ADD  CONSTRAINT [DF_MS_TAGIHAN_TEMP_NoUrut]  DEFAULT ((0)) FOR [NoUrut]
GO
ALTER TABLE [dbo].[MS_TAGIHAN_TEMP] ADD  CONSTRAINT [DF_MS_TAGIHAN_TEMP_NamaTagihan]  DEFAULT ('') FOR [NamaTagihan]
GO
ALTER TABLE [dbo].[MS_TAGIHAN_TEMP] ADD  CONSTRAINT [DF_MS_TAGIHAN_TEMP_TglJT]  DEFAULT (getdate()) FOR [TglJT]
GO
ALTER TABLE [dbo].[MS_TAGIHAN_TEMP] ADD  CONSTRAINT [DF_MS_TAGIHAN_TEMP_NilaiTagihan]  DEFAULT ((0)) FOR [NilaiTagihan]
GO
ALTER TABLE [dbo].[MS_TAGIHAN_TEMP] ADD  CONSTRAINT [DF_MS_TAGIHAN_TEMP_Tipe]  DEFAULT ('') FOR [Tipe]
GO
ALTER TABLE [dbo].[MS_TAGIHAN_TEMP] ADD  CONSTRAINT [DF_MS_TAGIHAN_TEMP_FlagPJT]  DEFAULT ((0)) FOR [FlagPJT]
GO
ALTER TABLE [dbo].[MS_TAGIHAN_TEMP] ADD  CONSTRAINT [DF_MS_TAGIHAN_TEMP_Denda]  DEFAULT ((0)) FOR [Denda]
GO
ALTER TABLE [dbo].[MS_TAGIHAN_TEMP] ADD  CONSTRAINT [DF_MS_TAGIHAN_TEMP_DendaReal]  DEFAULT ((0)) FOR [DendaReal]
GO
ALTER TABLE [dbo].[MS_TAGIHAN_TEMP] ADD  CONSTRAINT [DF_MS_TAGIHAN_TEMP_KPR]  DEFAULT ((0)) FOR [KPR]
GO
ALTER TABLE [dbo].[MS_TAGIHAN_TEMP] ADD  CONSTRAINT [DF_MS_TAGIHAN_TEMP_Akad]  DEFAULT ((0)) FOR [Akad]
GO
ALTER TABLE [dbo].[MS_TAGIHAN_TEMP] ADD  CONSTRAINT [DF_MS_TAGIHAN_TEMP_PutihDenda]  DEFAULT ((0)) FOR [PutihDenda]
GO
ALTER TABLE [dbo].[MS_TAGIHAN_TEMP] ADD  CONSTRAINT [DF_MS_TAGIHAN_TEMP_NilaiPutihDenda]  DEFAULT ((0)) FOR [NilaiPutihDenda]
GO
ALTER TABLE [dbo].[MS_TAGIHAN_TEMP] ADD  CONSTRAINT [DF_MS_TAGIHAN_TEMP_SN]  DEFAULT ((0)) FOR [SN]
GO
ALTER TABLE [dbo].[MS_TTR] ADD  CONSTRAINT [DF_MS_TTR_NoReservasi]  DEFAULT ('') FOR [NoReservasi]
GO
ALTER TABLE [dbo].[MS_TTR] ADD  CONSTRAINT [DF_MS_TTR_TglTTR]  DEFAULT (getdate()) FOR [TglTTR]
GO
ALTER TABLE [dbo].[MS_TTR] ADD  CONSTRAINT [DF_MS_TTR_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[MS_TTR] ADD  CONSTRAINT [DF_MS_TTR_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[MS_TTR] ADD  CONSTRAINT [DF_MS_TTR_Unit]  DEFAULT ('') FOR [Unit]
GO
ALTER TABLE [dbo].[MS_TTR] ADD  CONSTRAINT [DF_MS_TTR_Customer]  DEFAULT ('') FOR [Customer]
GO
ALTER TABLE [dbo].[MS_TTR] ADD  CONSTRAINT [DF_MS_TTR_CaraBayar]  DEFAULT ('') FOR [CaraBayar]
GO
ALTER TABLE [dbo].[MS_TTR] ADD  CONSTRAINT [DF_MS_TTR_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_TTR] ADD  CONSTRAINT [DF_MS_TTR_Total]  DEFAULT ((0)) FOR [Total]
GO
ALTER TABLE [dbo].[MS_TTR] ADD  CONSTRAINT [DF_MS_TTR_Status]  DEFAULT ('BARU') FOR [Status]
GO
ALTER TABLE [dbo].[MS_TTR] ADD  CONSTRAINT [DF_MS_TTR_PrintTTR]  DEFAULT ((0)) FOR [PrintTTR]
GO
ALTER TABLE [dbo].[MS_TTR] ADD  CONSTRAINT [DF_MS_TTR_TglInput]  DEFAULT (getdate()) FOR [TglInput]
GO
ALTER TABLE [dbo].[MS_TTR] ADD  CONSTRAINT [DF_MS_TTR_NoBG]  DEFAULT ('') FOR [NoBG]
GO
ALTER TABLE [dbo].[MS_TTR] ADD  CONSTRAINT [DF_MS_TTR_StatusBG]  DEFAULT ('OK') FOR [StatusBG]
GO
ALTER TABLE [dbo].[MS_TTR] ADD  CONSTRAINT [DF_MS_TTR_TglBG]  DEFAULT (getdate()) FOR [TglBG]
GO
ALTER TABLE [dbo].[MS_TTR] ADD  CONSTRAINT [DF_MS_TTR_NilaiKembali]  DEFAULT ((0)) FOR [NilaiKembali]
GO
ALTER TABLE [dbo].[MS_TTR] ADD  CONSTRAINT [DF_MS_TTR_ManualTTR]  DEFAULT ('') FOR [ManualTTR]
GO
ALTER TABLE [dbo].[MS_TTR] ADD  DEFAULT ('') FOR [Acc]
GO
ALTER TABLE [dbo].[MS_TTR] ADD  DEFAULT ((0)) FOR [FOBO]
GO
ALTER TABLE [dbo].[MS_TTR] ADD  DEFAULT ((0)) FOR [FOBOVoid]
GO
ALTER TABLE [dbo].[MS_TTR_LOG] ADD  CONSTRAINT [DF_MS_TTR_LOG_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[MS_TTR_LOG] ADD  CONSTRAINT [DF_MS_TTR_LOG_Aktivitas]  DEFAULT ('') FOR [Aktivitas]
GO
ALTER TABLE [dbo].[MS_TTR_LOG] ADD  CONSTRAINT [DF_MS_TTR_LOG_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[MS_TTR_LOG] ADD  CONSTRAINT [DF_MS_TTR_LOG_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[MS_TTR_LOG] ADD  CONSTRAINT [DF_MS_TTR_LOG_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_TTR_LOG] ADD  CONSTRAINT [DF_MS_TTR_LOG_Pk]  DEFAULT ('') FOR [Pk]
GO
ALTER TABLE [dbo].[MS_TTR_LOG] ADD  CONSTRAINT [DF_MS_TTR_LOG_Approve]  DEFAULT ('') FOR [Approve]
GO
ALTER TABLE [dbo].[MS_TTR_LOG] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_UNIT] ADD  CONSTRAINT [DF_MS_UNIT_Status]  DEFAULT ('A') FOR [Status]
GO
ALTER TABLE [dbo].[MS_UNIT] ADD  CONSTRAINT [DF_MS_UNIT_Jenis]  DEFAULT ('') FOR [Jenis]
GO
ALTER TABLE [dbo].[MS_UNIT] ADD  CONSTRAINT [DF_MS_UNIT_Lokasi]  DEFAULT ('') FOR [Lokasi]
GO
ALTER TABLE [dbo].[MS_UNIT] ADD  CONSTRAINT [DF_MS_UNIT_NoUnit]  DEFAULT ('') FOR [NoUnit]
GO
ALTER TABLE [dbo].[MS_UNIT] ADD  CONSTRAINT [DF_MS_UNIT_Luas]  DEFAULT ((0)) FOR [Luas]
GO
ALTER TABLE [dbo].[MS_UNIT] ADD  CONSTRAINT [DF_MS_UNIT_TglInput]  DEFAULT (getdate()) FOR [TglInput]
GO
ALTER TABLE [dbo].[MS_UNIT] ADD  CONSTRAINT [DF_MS_UNIT_TglEdit]  DEFAULT (getdate()) FOR [TglEdit]
GO
ALTER TABLE [dbo].[MS_UNIT] ADD  CONSTRAINT [DF_MS_UNIT_PriceListMin]  DEFAULT ((0)) FOR [PriceListMin]
GO
ALTER TABLE [dbo].[MS_UNIT] ADD  CONSTRAINT [DF_MS_UNIT_PriceList]  DEFAULT ((0)) FOR [PriceList]
GO
ALTER TABLE [dbo].[MS_UNIT] ADD  CONSTRAINT [DF_MS_UNIT_FlagSPL]  DEFAULT ((0)) FOR [FlagSPL]
GO
ALTER TABLE [dbo].[MS_UNIT] ADD  CONSTRAINT [DF_MS_UNIT_Peta]  DEFAULT ('') FOR [Peta]
GO
ALTER TABLE [dbo].[MS_UNIT] ADD  CONSTRAINT [DF_MS_UNIT_Koordinat]  DEFAULT ('') FOR [Koordinat]
GO
ALTER TABLE [dbo].[MS_UNIT] ADD  CONSTRAINT [DF_MS_UNIT_Zoning]  DEFAULT ('') FOR [Zoning]
GO
ALTER TABLE [dbo].[MS_UNIT] ADD  CONSTRAINT [DF_MS_UNIT_Panjang]  DEFAULT ((0)) FOR [Panjang]
GO
ALTER TABLE [dbo].[MS_UNIT] ADD  CONSTRAINT [DF_MS_UNIT_Lebar]  DEFAULT ((0)) FOR [Lebar]
GO
ALTER TABLE [dbo].[MS_UNIT] ADD  CONSTRAINT [DF_MS_UNIT_Tinggi]  DEFAULT ((0)) FOR [Tinggi]
GO
ALTER TABLE [dbo].[MS_UNIT] ADD  CONSTRAINT [DF_MS_UNIT_LuasSG]  DEFAULT ((0)) FOR [LuasSG]
GO
ALTER TABLE [dbo].[MS_UNIT] ADD  CONSTRAINT [DF_MS_UNIT_LuasNett]  DEFAULT ((0)) FOR [LuasNett]
GO
ALTER TABLE [dbo].[MS_UNIT] ADD  CONSTRAINT [DF_MS_UNIT_HadapAtrium]  DEFAULT ((0)) FOR [HadapAtrium]
GO
ALTER TABLE [dbo].[MS_UNIT] ADD  CONSTRAINT [DF_MS_UNIT_HadapEntrance]  DEFAULT ((0)) FOR [HadapEntrance]
GO
ALTER TABLE [dbo].[MS_UNIT] ADD  CONSTRAINT [DF_MS_UNIT_HadapEskalator]  DEFAULT ((0)) FOR [HadapEskalator]
GO
ALTER TABLE [dbo].[MS_UNIT] ADD  CONSTRAINT [DF_MS_UNIT_HadapLift]  DEFAULT ((0)) FOR [HadapLift]
GO
ALTER TABLE [dbo].[MS_UNIT] ADD  CONSTRAINT [DF_MS_UNIT_HadapParkir]  DEFAULT ((0)) FOR [HadapParkir]
GO
ALTER TABLE [dbo].[MS_UNIT] ADD  CONSTRAINT [DF_MS_UNIT_HadapParkir1]  DEFAULT ((0)) FOR [HadapAxis]
GO
ALTER TABLE [dbo].[MS_UNIT] ADD  CONSTRAINT [DF_MS_UNIT_Hook]  DEFAULT ((0)) FOR [Hook]
GO
ALTER TABLE [dbo].[MS_UNIT] ADD  CONSTRAINT [DF_MS_UNIT_LebarJalan]  DEFAULT ((0)) FOR [LebarJalan]
GO
ALTER TABLE [dbo].[MS_UNIT] ADD  CONSTRAINT [DF_MS_UNIT_Outdoor]  DEFAULT ((0)) FOR [Outdoor]
GO
ALTER TABLE [dbo].[MS_UNIT] ADD  CONSTRAINT [DF_MS_UNIT_ArahHadap]  DEFAULT ('') FOR [ArahHadap]
GO
ALTER TABLE [dbo].[MS_UNIT] ADD  CONSTRAINT [DF_MS_UNIT_Panorama]  DEFAULT ('') FOR [Panorama]
GO
ALTER TABLE [dbo].[MS_UNIT] ADD  CONSTRAINT [DF__MS_UNIT__Discoun__61F08603]  DEFAULT ((0)) FOR [DiscountAuthorized]
GO
ALTER TABLE [dbo].[MS_UNIT] ADD  CONSTRAINT [DF__MS_UNIT__Tambaha__6E765879]  DEFAULT ('0') FOR [TambahanHargaGimmick]
GO
ALTER TABLE [dbo].[MS_UNIT] ADD  CONSTRAINT [DF__MS_UNIT__Tambaha__6F6A7CB2]  DEFAULT ('0') FOR [TambahanHargaLainLain]
GO
ALTER TABLE [dbo].[MS_UNIT] ADD  CONSTRAINT [DF__MS_UNIT__JenisPr__705EA0EB]  DEFAULT ('') FOR [JenisProperti]
GO
ALTER TABLE [dbo].[MS_UNIT] ADD  CONSTRAINT [DF__MS_UNIT__Lantai__7152C524]  DEFAULT ('00') FOR [Lantai]
GO
ALTER TABLE [dbo].[MS_UNIT] ADD  CONSTRAINT [DF__MS_UNIT__Nomor__7246E95D]  DEFAULT ('00') FOR [Nomor]
GO
ALTER TABLE [dbo].[MS_UNIT] ADD  CONSTRAINT [DF__MS_UNIT__Project__733B0D96]  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[MS_UNIT] ADD  CONSTRAINT [DF__MS_UNIT__SifatPP__28F7FFC9]  DEFAULT ((0)) FOR [SifatPPN]
GO
ALTER TABLE [dbo].[MS_UNIT] ADD  DEFAULT ((0)) FOR [BiayaBPHTB]
GO
ALTER TABLE [dbo].[MS_UNIT] ADD  DEFAULT ((0)) FOR [BiayaSurat]
GO
ALTER TABLE [dbo].[MS_UNIT] ADD  DEFAULT ((0)) FOR [BiayaProses]
GO
ALTER TABLE [dbo].[MS_UNIT] ADD  DEFAULT ((0)) FOR [BiayaLainLain]
GO
ALTER TABLE [dbo].[MS_UNIT] ADD  DEFAULT ((0)) FOR [PricelistKavling]
GO
ALTER TABLE [dbo].[MS_UNIT] ADD  DEFAULT ((0)) FOR [DefaultPL]
GO
ALTER TABLE [dbo].[MS_UNIT] ADD  DEFAULT ((0)) FOR [LuasLebih]
GO
ALTER TABLE [dbo].[MS_UNIT] ADD  DEFAULT ('') FOR [Kategori]
GO
ALTER TABLE [dbo].[MS_UNIT] ADD  DEFAULT ('') FOR [NamaJalan]
GO
ALTER TABLE [dbo].[MS_UNIT] ADD  DEFAULT ((0)) FOR [HargaTanah]
GO
ALTER TABLE [dbo].[MS_UNIT_LOG] ADD  CONSTRAINT [DF_MS_UNIT_LOG_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[MS_UNIT_LOG] ADD  CONSTRAINT [DF_MS_UNIT_LOG_Aktivitas]  DEFAULT ('') FOR [Aktivitas]
GO
ALTER TABLE [dbo].[MS_UNIT_LOG] ADD  CONSTRAINT [DF_MS_UNIT_LOG_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[MS_UNIT_LOG] ADD  CONSTRAINT [DF_MS_UNIT_LOG_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[MS_UNIT_LOG] ADD  CONSTRAINT [DF_MS_UNIT_LOG_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_UNIT_LOG] ADD  CONSTRAINT [DF_MS_UNIT_LOG_Pk]  DEFAULT ('') FOR [Pk]
GO
ALTER TABLE [dbo].[MS_UNIT_LOG] ADD  CONSTRAINT [DF_MS_UNIT_LOG_Approve]  DEFAULT ('') FOR [Approve]
GO
ALTER TABLE [dbo].[MS_UNIT_LOG] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[REF_AGENT_LEVEL] ADD  CONSTRAINT [DF_REF_AGENT_LEVEL_Nama]  DEFAULT ('') FOR [Nama]
GO
ALTER TABLE [dbo].[REF_AGENT_LEVEL] ADD  CONSTRAINT [DF_REF_AGENT_LEVEL_Tipe]  DEFAULT ((0)) FOR [Tipe]
GO
ALTER TABLE [dbo].[REF_AGENT_LEVEL] ADD  DEFAULT ((0)) FOR [ParentID]
GO
ALTER TABLE [dbo].[REF_AGENT_LEVEL] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[REF_AGENT_LEVEL_LOG] ADD  CONSTRAINT [DF_REF_AGENT_LEVEL_LOG_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[REF_AGENT_LEVEL_LOG] ADD  CONSTRAINT [DF_REF_AGENT_LEVEL_LOG_Aktivitas]  DEFAULT ('') FOR [Aktivitas]
GO
ALTER TABLE [dbo].[REF_AGENT_LEVEL_LOG] ADD  CONSTRAINT [DF_REF_AGENT_LEVEL_LOG_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[REF_AGENT_LEVEL_LOG] ADD  CONSTRAINT [DF_REF_AGENT_LEVEL_LOG_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[REF_AGENT_LEVEL_LOG] ADD  CONSTRAINT [DF_REF_AGENT_LEVEL_LOG_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[REF_AGENT_LEVEL_LOG] ADD  CONSTRAINT [DF_REF_AGENT_LEVEL_LOG_Pk]  DEFAULT ('') FOR [Pk]
GO
ALTER TABLE [dbo].[REF_AGENT_LEVEL_LOG] ADD  CONSTRAINT [DF_REF_AGENT_LEVEL_LOG_Approve]  DEFAULT ('') FOR [Approve]
GO
ALTER TABLE [dbo].[REF_AGENT_LEVEL_LOG] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[REF_AGENT_TIPE] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[REF_AGENT_TIPE_LOG] ADD  CONSTRAINT [DF_REF_AGENT_TIPE_LOG_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[REF_AGENT_TIPE_LOG] ADD  CONSTRAINT [DF_REF_AGENT_TIPE_LOG_Aktivitas]  DEFAULT ('') FOR [Aktivitas]
GO
ALTER TABLE [dbo].[REF_AGENT_TIPE_LOG] ADD  CONSTRAINT [DF_REF_AGENT_TIPE_LOG_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[REF_AGENT_TIPE_LOG] ADD  CONSTRAINT [DF_REF_AGENT_TIPE_LOG_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[REF_AGENT_TIPE_LOG] ADD  CONSTRAINT [DF_REF_AGENT_TIPE_LOG_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[REF_AGENT_TIPE_LOG] ADD  CONSTRAINT [DF_REF_AGENT_TIPE_LOG_Pk]  DEFAULT ('') FOR [Pk]
GO
ALTER TABLE [dbo].[REF_AGENT_TIPE_LOG] ADD  CONSTRAINT [DF_REF_AGENT_TIPE_LOG_Approve]  DEFAULT ('') FOR [Approve]
GO
ALTER TABLE [dbo].[REF_AGENT_TIPE_LOG] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[REF_BANKKPA] ADD  CONSTRAINT [DF_REF_ACC_KodeBank]  DEFAULT ('') FOR [KodeBank]
GO
ALTER TABLE [dbo].[REF_BANKKPA] ADD  CONSTRAINT [DF_REF_ACC_Bank]  DEFAULT ('') FOR [Bank]
GO
ALTER TABLE [dbo].[REF_BANKKPA] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[REF_BANKKPA] ADD  DEFAULT ((0)) FOR [Nilai]
GO
ALTER TABLE [dbo].[REF_BANKKPA_LOG] ADD  CONSTRAINT [DF_REF_BANKKPA_LOG_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[REF_BANKKPA_LOG] ADD  CONSTRAINT [DF_REF_BANKKPA_LOG_Approve]  DEFAULT ('') FOR [Approve]
GO
ALTER TABLE [dbo].[REF_BANKKPA_LOG] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[REF_BERKAS_PPJB] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[REF_BERKAS_PPJB_LOG] ADD  CONSTRAINT [DF_REF_BERKAS_PPJB_LOG_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[REF_BERKAS_PPJB_LOG] ADD  CONSTRAINT [DF_REF_BERKAS_PPJB_LOG_Approve]  DEFAULT ('') FOR [Approve]
GO
ALTER TABLE [dbo].[REF_BERKAS_PPJB_LOG] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[REF_COMPLAIN] ADD  DEFAULT ('') FOR [PIC]
GO
ALTER TABLE [dbo].[REF_FOLLOWUP] ADD  CONSTRAINT [DF_REF_FOLLOWUP_Nama]  DEFAULT ('') FOR [NamaGrouping]
GO
ALTER TABLE [dbo].[REF_FOLLOWUP] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[REF_FOLLOWUP_LOG] ADD  CONSTRAINT [DF_REF_FOLLOWUP_LOG_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[REF_FOLLOWUP_LOG] ADD  CONSTRAINT [DF_REF_FOLLOWUP_LOG_Aktivitas]  DEFAULT ('') FOR [Aktivitas]
GO
ALTER TABLE [dbo].[REF_FOLLOWUP_LOG] ADD  CONSTRAINT [DF_REF_FOLLOWUP_LOG_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[REF_FOLLOWUP_LOG] ADD  CONSTRAINT [DF_REF_FOLLOWUP_LOG_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[REF_FOLLOWUP_LOG] ADD  CONSTRAINT [DF_REF_FOLLOWUP_LOG_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[REF_FOLLOWUP_LOG] ADD  CONSTRAINT [DF_REF_FOLLOWUP_LOG_Pk]  DEFAULT ('') FOR [Pk]
GO
ALTER TABLE [dbo].[REF_FOLLOWUP_LOG] ADD  CONSTRAINT [DF_REF_FOLLOWUP_LOG_Approve]  DEFAULT ('') FOR [Approve]
GO
ALTER TABLE [dbo].[REF_JENIS] ADD  CONSTRAINT [DF_REF_JENIS_Nama]  DEFAULT ('') FOR [Nama]
GO
ALTER TABLE [dbo].[REF_JENIS] ADD  CONSTRAINT [DF_REF_JENIS_SN]  DEFAULT ((0)) FOR [SN]
GO
ALTER TABLE [dbo].[REF_JENIS] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[REF_JENIS] ADD  DEFAULT ('') FOR [Gambar]
GO
ALTER TABLE [dbo].[REF_JENIS_LOG] ADD  CONSTRAINT [DF_REF_JENIS_LOG_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[REF_JENIS_LOG] ADD  CONSTRAINT [DF_REF_JENIS_LOG_Aktivitas]  DEFAULT ('') FOR [Aktivitas]
GO
ALTER TABLE [dbo].[REF_JENIS_LOG] ADD  CONSTRAINT [DF_REF_JENIS_LOG_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[REF_JENIS_LOG] ADD  CONSTRAINT [DF_REF_JENIS_LOG_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[REF_JENIS_LOG] ADD  CONSTRAINT [DF_REF_JENIS_LOG_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[REF_JENIS_LOG] ADD  CONSTRAINT [DF_REF_JENIS_LOG_Pk]  DEFAULT ('') FOR [Pk]
GO
ALTER TABLE [dbo].[REF_JENIS_LOG] ADD  CONSTRAINT [DF_REF_JENIS_LOG_Approve]  DEFAULT ('') FOR [Approve]
GO
ALTER TABLE [dbo].[REF_JENIS_LOG] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[REF_JENISPROPERTI] ADD  CONSTRAINT [DF_REF_JENISPROPERTI_Nama]  DEFAULT ('') FOR [Nama]
GO
ALTER TABLE [dbo].[REF_JENISPROPERTI] ADD  CONSTRAINT [DF_REF_JENISPROPERTI_SN]  DEFAULT ((0)) FOR [SN]
GO
ALTER TABLE [dbo].[REF_JENISPROPERTI] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[REF_JENISPROPERTI_LOG] ADD  CONSTRAINT [DF_REF_JENISPROPERTI_LOG_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[REF_JENISPROPERTI_LOG] ADD  CONSTRAINT [DF_REF_JENISPROPERTI_LOG_Aktivitas]  DEFAULT ('') FOR [Aktivitas]
GO
ALTER TABLE [dbo].[REF_JENISPROPERTI_LOG] ADD  CONSTRAINT [DF_REF_JENISPROPERTI_LOG_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[REF_JENISPROPERTI_LOG] ADD  CONSTRAINT [DF_REF_JENISPROPERTI_LOG_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[REF_JENISPROPERTI_LOG] ADD  CONSTRAINT [DF_REF_JENISPROPERTI_LOG_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[REF_JENISPROPERTI_LOG] ADD  CONSTRAINT [DF_REF_JENISPROPERTI_LOG_Pk]  DEFAULT ('') FOR [Pk]
GO
ALTER TABLE [dbo].[REF_JENISPROPERTI_LOG] ADD  CONSTRAINT [DF_REF_JENISPROPERTI_LOG_Approve]  DEFAULT ('') FOR [Approve]
GO
ALTER TABLE [dbo].[REF_JENISPROPERTI_LOG] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[REF_LOKASI] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[REF_LOKASI] ADD  DEFAULT ((0)) FOR [SNVA]
GO
ALTER TABLE [dbo].[REF_LOKASI_KONTRAK] ADD  CONSTRAINT [DF_REF_LOKASI_KONTRAK_Project]  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[REF_LOKASI_KONTRAK_LOG] ADD  CONSTRAINT [DF_REF_LOKASI_KONTRAK_LOG_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[REF_LOKASI_KONTRAK_LOG] ADD  CONSTRAINT [DF_REF_LOKASI_KONTRAK_LOG_Aktivitas]  DEFAULT ('') FOR [Aktivitas]
GO
ALTER TABLE [dbo].[REF_LOKASI_KONTRAK_LOG] ADD  CONSTRAINT [DF_REF_LOKASI_KONTRAK_LOG_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[REF_LOKASI_KONTRAK_LOG] ADD  CONSTRAINT [DF_REF_LOKASI_KONTRAK_LOG_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[REF_LOKASI_KONTRAK_LOG] ADD  CONSTRAINT [DF_REF_LOKASI_KONTRAK_LOG_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[REF_LOKASI_KONTRAK_LOG] ADD  CONSTRAINT [DF_REF_LOKASI_KONTRAK_LOG_Pk]  DEFAULT ('') FOR [Pk]
GO
ALTER TABLE [dbo].[REF_LOKASI_KONTRAK_LOG] ADD  CONSTRAINT [DF_REF_LOKASI_KONTRAK_LOG_Approve]  DEFAULT ('') FOR [Approve]
GO
ALTER TABLE [dbo].[REF_LOKASI_LOG] ADD  CONSTRAINT [DF_REF_LOKASI_LOG_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[REF_LOKASI_LOG] ADD  CONSTRAINT [DF_REF_LOKASI_LOG_Aktivitas]  DEFAULT ('') FOR [Aktivitas]
GO
ALTER TABLE [dbo].[REF_LOKASI_LOG] ADD  CONSTRAINT [DF_REF_LOKASI_LOG_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[REF_LOKASI_LOG] ADD  CONSTRAINT [DF_REF_LOKASI_LOG_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[REF_LOKASI_LOG] ADD  CONSTRAINT [DF_REF_LOKASI_LOG_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[REF_LOKASI_LOG] ADD  CONSTRAINT [DF_REF_LOKASI_LOG_Pk]  DEFAULT ('') FOR [Pk]
GO
ALTER TABLE [dbo].[REF_LOKASI_LOG] ADD  CONSTRAINT [DF_REF_LOKASI_LOG_Approve]  DEFAULT ('') FOR [Approve]
GO
ALTER TABLE [dbo].[REF_LOKASI_LOG] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[REF_RETENSI] ADD  CONSTRAINT [DF_REF_RETENSI_Kode]  DEFAULT ('') FOR [Kode]
GO
ALTER TABLE [dbo].[REF_RETENSI] ADD  CONSTRAINT [DF_REF_RETENSI_NamaKategori]  DEFAULT ('') FOR [NamaKategori]
GO
ALTER TABLE [dbo].[REF_RETENSI] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[REF_RETENSI_LOG] ADD  CONSTRAINT [DF_REF_RETENSI_LOG_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[REF_RETENSI_LOG] ADD  CONSTRAINT [DF_REF_RETENSI_LOG_Approve]  DEFAULT ('') FOR [Approve]
GO
ALTER TABLE [dbo].[REF_RETENSI_LOG] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[REF_SKEMA] ADD  CONSTRAINT [DF_REF_SKEMA_Nama]  DEFAULT ('') FOR [Nama]
GO
ALTER TABLE [dbo].[REF_SKEMA] ADD  CONSTRAINT [DF_REF_SKEMA_Status]  DEFAULT ('A') FOR [Status]
GO
ALTER TABLE [dbo].[REF_SKEMA] ADD  CONSTRAINT [DF_REF_SKEMA_Diskon]  DEFAULT ('') FOR [Diskon]
GO
ALTER TABLE [dbo].[REF_SKEMA] ADD  CONSTRAINT [DF_REF_SKEMA_DiskonKet]  DEFAULT ('') FOR [DiskonKet]
GO
ALTER TABLE [dbo].[REF_SKEMA] ADD  CONSTRAINT [DF_REF_SKEMA_RThousand]  DEFAULT ((0)) FOR [RThousand]
GO
ALTER TABLE [dbo].[REF_SKEMA] ADD  DEFAULT ('') FOR [Jenis]
GO
ALTER TABLE [dbo].[REF_SKEMA] ADD  DEFAULT ('') FOR [Bunga]
GO
ALTER TABLE [dbo].[REF_SKEMA] ADD  DEFAULT ('') FOR [BungaKet]
GO
ALTER TABLE [dbo].[REF_SKEMA] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[REF_SKEMA_DETAIL] ADD  CONSTRAINT [DF_REF_SKEMA_DETAIL_Tipe]  DEFAULT ('') FOR [Tipe]
GO
ALTER TABLE [dbo].[REF_SKEMA_DETAIL] ADD  CONSTRAINT [DF_REF_SKEMA_DETAIL_Nama]  DEFAULT ('') FOR [Nama]
GO
ALTER TABLE [dbo].[REF_SKEMA_DETAIL] ADD  CONSTRAINT [DF_REF_SKEMA_DETAIL_Nominal]  DEFAULT ((0)) FOR [Nominal]
GO
ALTER TABLE [dbo].[REF_SKEMA_DETAIL] ADD  CONSTRAINT [DF_REF_SKEMA_DETAIL_TipeNominal]  DEFAULT ('') FOR [TipeNominal]
GO
ALTER TABLE [dbo].[REF_SKEMA_DETAIL] ADD  CONSTRAINT [DF_REF_SKEMA_DETAIL_TipeJadwal]  DEFAULT ('') FOR [TipeJadwal]
GO
ALTER TABLE [dbo].[REF_SKEMA_DETAIL] ADD  CONSTRAINT [DF_REF_SKEMA_DETAIL_IntJadwal]  DEFAULT ((0)) FOR [IntJadwal]
GO
ALTER TABLE [dbo].[REF_SKEMA_DETAIL] ADD  CONSTRAINT [DF_REF_SKEMA_DETAIL_RefJadwal]  DEFAULT ((0)) FOR [RefJadwal]
GO
ALTER TABLE [dbo].[REF_SKEMA_DETAIL] ADD  CONSTRAINT [DF_REF_SKEMA_DETAIL_BF]  DEFAULT ((0)) FOR [BF]
GO
ALTER TABLE [dbo].[REF_SKEMA_DETAIL] ADD  CONSTRAINT [DF_REF_SKEMA_DETAIL_KPR]  DEFAULT ((0)) FOR [KPR]
GO
ALTER TABLE [dbo].[REF_SKEMA_LOG] ADD  CONSTRAINT [DF_REF_SKEMA_LOG_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[REF_SKEMA_LOG] ADD  CONSTRAINT [DF_REF_SKEMA_LOG_Aktivitas]  DEFAULT ('') FOR [Aktivitas]
GO
ALTER TABLE [dbo].[REF_SKEMA_LOG] ADD  CONSTRAINT [DF_REF_SKEMA_LOG_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[REF_SKEMA_LOG] ADD  CONSTRAINT [DF_REF_SKEMA_LOG_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[REF_SKEMA_LOG] ADD  CONSTRAINT [DF_REF_SKEMA_LOG_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[REF_SKEMA_LOG] ADD  CONSTRAINT [DF_REF_SKEMA_LOG_Pk]  DEFAULT ('') FOR [Pk]
GO
ALTER TABLE [dbo].[REF_SKEMA_LOG] ADD  CONSTRAINT [DF_REF_SKEMA_LOG_Approve]  DEFAULT ('') FOR [Approve]
GO
ALTER TABLE [dbo].[REF_SKEMA_LOG] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[REF_SKOM] ADD  CONSTRAINT [DF_REF_SKOM_SalesTipe]  DEFAULT ((0)) FOR [SalesTipe]
GO
ALTER TABLE [dbo].[REF_SKOM] ADD  CONSTRAINT [DF_REF_SKOM_Nama]  DEFAULT ('') FOR [Nama]
GO
ALTER TABLE [dbo].[REF_SKOM] ADD  CONSTRAINT [DF_REF_SKOM_Dari]  DEFAULT (getdate()) FOR [Dari]
GO
ALTER TABLE [dbo].[REF_SKOM] ADD  CONSTRAINT [DF_REF_SKOM_Sampai]  DEFAULT (getdate()) FOR [Sampai]
GO
ALTER TABLE [dbo].[REF_SKOM] ADD  CONSTRAINT [DF_REF_SKOM_Rumus]  DEFAULT ((0)) FOR [Rumus]
GO
ALTER TABLE [dbo].[REF_SKOM] ADD  CONSTRAINT [DF_REF_SKOM_DasarHitung]  DEFAULT ((0)) FOR [DasarHitung]
GO
ALTER TABLE [dbo].[REF_SKOM] ADD  CONSTRAINT [DF_REF_SKOM_Inaktif]  DEFAULT ((0)) FOR [Inaktif]
GO
ALTER TABLE [dbo].[REF_SKOM] ADD  CONSTRAINT [DF_REF_SKOM_TglInput]  DEFAULT (getdate()) FOR [TglInput]
GO
ALTER TABLE [dbo].[REF_SKOM] ADD  CONSTRAINT [DF_REF_SKOM_TglUpdate]  DEFAULT (getdate()) FOR [TglUpdate]
GO
ALTER TABLE [dbo].[REF_SKOM] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[REF_SKOM] ADD  DEFAULT ((0)) FOR [NoTermin]
GO
ALTER TABLE [dbo].[REF_SKOM] ADD  DEFAULT ('') FOR [CaraBayar]
GO
ALTER TABLE [dbo].[REF_SKOM_CF] ADD  CONSTRAINT [DF_REF_SKOM_CF_SalesTipe]  DEFAULT ((0)) FOR [SalesTipe]
GO
ALTER TABLE [dbo].[REF_SKOM_CF] ADD  CONSTRAINT [DF_REF_SKOM_CF_Nama]  DEFAULT ('') FOR [Nama]
GO
ALTER TABLE [dbo].[REF_SKOM_CF] ADD  CONSTRAINT [DF_REF_SKOM_CF_Dari]  DEFAULT (getdate()) FOR [Dari]
GO
ALTER TABLE [dbo].[REF_SKOM_CF] ADD  CONSTRAINT [DF_REF_SKOM_CF_Sampai]  DEFAULT (getdate()) FOR [Sampai]
GO
ALTER TABLE [dbo].[REF_SKOM_CF] ADD  CONSTRAINT [DF_REF_SKOM_CF_Rumus]  DEFAULT ('') FOR [Rumus]
GO
ALTER TABLE [dbo].[REF_SKOM_CF] ADD  CONSTRAINT [DF_REF_SKOM_CF_DasarHitung]  DEFAULT ('') FOR [DasarHitung]
GO
ALTER TABLE [dbo].[REF_SKOM_CF] ADD  CONSTRAINT [DF_REF_SKOM_CF_Inaktif]  DEFAULT ((0)) FOR [Inaktif]
GO
ALTER TABLE [dbo].[REF_SKOM_CF] ADD  CONSTRAINT [DF_REF_SKOM_CF_TglInput]  DEFAULT (getdate()) FOR [TglInput]
GO
ALTER TABLE [dbo].[REF_SKOM_CF] ADD  CONSTRAINT [DF_REF_SKOM_CF_TglUpdate]  DEFAULT (getdate()) FOR [TglUpdate]
GO
ALTER TABLE [dbo].[REF_SKOM_CF] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[REF_SKOM_CF_DETAIL] ADD  CONSTRAINT [DF_REF_SKOM_CF_DETAIL_SalesLevel]  DEFAULT ((0)) FOR [SalesLevel]
GO
ALTER TABLE [dbo].[REF_SKOM_CF_DETAIL] ADD  CONSTRAINT [DF_REF_SKOM_CF_DETAIL_TipeTarif]  DEFAULT ('') FOR [TipeTarif]
GO
ALTER TABLE [dbo].[REF_SKOM_CF_DETAIL] ADD  CONSTRAINT [DF_REF_SKOM_CF_DETAIL_Nilai]  DEFAULT ((0)) FOR [Nilai]
GO
ALTER TABLE [dbo].[REF_SKOM_CF_DETAIL] ADD  CONSTRAINT [DF_REF_SKOM_CF_DETAIL_PotongKomisi]  DEFAULT ((0)) FOR [PotongKomisi]
GO
ALTER TABLE [dbo].[REF_SKOM_CF_DETAIL2] ADD  CONSTRAINT [DF_REF_SKOM_CF_DETAIL2_SalesLevel]  DEFAULT ((0)) FOR [SalesLevel]
GO
ALTER TABLE [dbo].[REF_SKOM_CF_DETAIL2] ADD  CONSTRAINT [DF_REF_SKOM_CF_DETAIL2_TipeTarget]  DEFAULT ('') FOR [TipeTarget]
GO
ALTER TABLE [dbo].[REF_SKOM_CF_DETAIL2] ADD  CONSTRAINT [DF_REF_SKOM_CF_DETAIL2_TargetBawah]  DEFAULT ((0)) FOR [TargetBawah]
GO
ALTER TABLE [dbo].[REF_SKOM_CF_DETAIL2] ADD  CONSTRAINT [DF_REF_SKOM_CF_DETAIL2_TargetAtas]  DEFAULT ((0)) FOR [TargetAtas]
GO
ALTER TABLE [dbo].[REF_SKOM_CF_DETAIL2] ADD  CONSTRAINT [DF_REF_SKOM_CF_DETAIL2_TipeTarif]  DEFAULT ('') FOR [TipeTarif]
GO
ALTER TABLE [dbo].[REF_SKOM_CF_DETAIL2] ADD  CONSTRAINT [DF_REF_SKOM_CF_DETAIL2_Nilai]  DEFAULT ((0)) FOR [Nilai]
GO
ALTER TABLE [dbo].[REF_SKOM_CF_DETAIL2] ADD  CONSTRAINT [DF_REF_SKOM_CF_DETAIL2_PotongKomisi]  DEFAULT ((0)) FOR [PotongKomisi]
GO
ALTER TABLE [dbo].[REF_SKOM_CF_LOG] ADD  CONSTRAINT [DF_REF_SKOM_CF_LOG_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[REF_SKOM_CF_LOG] ADD  CONSTRAINT [DF_REF_SKOM_CF_LOG_Aktivitas]  DEFAULT ('') FOR [Aktivitas]
GO
ALTER TABLE [dbo].[REF_SKOM_CF_LOG] ADD  CONSTRAINT [DF_REF_SKOM_CF_LOG_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[REF_SKOM_CF_LOG] ADD  CONSTRAINT [DF_REF_SKOM_CF_LOG_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[REF_SKOM_CF_LOG] ADD  CONSTRAINT [DF_REF_SKOM_CF_LOG_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[REF_SKOM_CF_LOG] ADD  CONSTRAINT [DF_REF_SKOM_CF_LOG_Pk]  DEFAULT ('') FOR [Pk]
GO
ALTER TABLE [dbo].[REF_SKOM_CF_LOG] ADD  CONSTRAINT [DF_REF_SKOM_CF_LOG_Approve]  DEFAULT ('') FOR [Approve]
GO
ALTER TABLE [dbo].[REF_SKOM_CF_LOG] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[REF_SKOM_DETAIL] ADD  CONSTRAINT [DF_REF_SKOM_DETAIL_SalesLevel]  DEFAULT ((0)) FOR [SalesLevel]
GO
ALTER TABLE [dbo].[REF_SKOM_DETAIL] ADD  CONSTRAINT [DF_REF_SKOM_DETAIL_TipeTarif]  DEFAULT ('') FOR [TipeTarif]
GO
ALTER TABLE [dbo].[REF_SKOM_DETAIL] ADD  CONSTRAINT [DF_REF_SKOM_DETAIL_Nilai]  DEFAULT ((0)) FOR [Nilai]
GO
ALTER TABLE [dbo].[REF_SKOM_DETAIL2] ADD  CONSTRAINT [DF_REF_SKOM_DETAIL2_SalesLevel]  DEFAULT ((0)) FOR [SalesLevel]
GO
ALTER TABLE [dbo].[REF_SKOM_DETAIL2] ADD  CONSTRAINT [DF_REF_SKOM_DETAIL2_TipeTarget]  DEFAULT ('') FOR [TipeTarget]
GO
ALTER TABLE [dbo].[REF_SKOM_DETAIL2] ADD  CONSTRAINT [DF_REF_SKOM_DETAIL2_TargetBawah]  DEFAULT ((0)) FOR [TargetBawah]
GO
ALTER TABLE [dbo].[REF_SKOM_DETAIL2] ADD  CONSTRAINT [DF_REF_SKOM_DETAIL2_TargetAtas]  DEFAULT ((0)) FOR [TargetAtas]
GO
ALTER TABLE [dbo].[REF_SKOM_DETAIL2] ADD  CONSTRAINT [DF_REF_SKOM_DETAIL2_TipeTarif]  DEFAULT ('') FOR [TipeTarif]
GO
ALTER TABLE [dbo].[REF_SKOM_DETAIL2] ADD  CONSTRAINT [DF_REF_SKOM_DETAIL2_Nilai]  DEFAULT ((0)) FOR [Nilai]
GO
ALTER TABLE [dbo].[REF_SKOM_LOG] ADD  CONSTRAINT [DF_REF_SKOM_LOG_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[REF_SKOM_LOG] ADD  CONSTRAINT [DF_REF_SKOM_LOG_Aktivitas]  DEFAULT ('') FOR [Aktivitas]
GO
ALTER TABLE [dbo].[REF_SKOM_LOG] ADD  CONSTRAINT [DF_REF_SKOM_LOG_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[REF_SKOM_LOG] ADD  CONSTRAINT [DF_REF_SKOM_LOG_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[REF_SKOM_LOG] ADD  CONSTRAINT [DF_REF_SKOM_LOG_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[REF_SKOM_LOG] ADD  CONSTRAINT [DF_REF_SKOM_LOG_Pk]  DEFAULT ('') FOR [Pk]
GO
ALTER TABLE [dbo].[REF_SKOM_LOG] ADD  CONSTRAINT [DF_REF_SKOM_LOG_Approve]  DEFAULT ('') FOR [Approve]
GO
ALTER TABLE [dbo].[REF_SKOM_LOG] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[REF_SKOM_REWARD] ADD  CONSTRAINT [DF_REF_SREWARD_SalesTipe]  DEFAULT ((0)) FOR [SalesTipe]
GO
ALTER TABLE [dbo].[REF_SKOM_REWARD] ADD  CONSTRAINT [DF_REF_SREWARD_Nama]  DEFAULT ('') FOR [Nama]
GO
ALTER TABLE [dbo].[REF_SKOM_REWARD] ADD  CONSTRAINT [DF_REF_SREWARD_Dari]  DEFAULT (getdate()) FOR [Dari]
GO
ALTER TABLE [dbo].[REF_SKOM_REWARD] ADD  CONSTRAINT [DF_REF_SREWARD_Sampai]  DEFAULT (getdate()) FOR [Sampai]
GO
ALTER TABLE [dbo].[REF_SKOM_REWARD] ADD  CONSTRAINT [DF_REF_SREWARD_Rumus]  DEFAULT ('') FOR [Rumus]
GO
ALTER TABLE [dbo].[REF_SKOM_REWARD] ADD  CONSTRAINT [DF_REF_SREWARD_Inaktif]  DEFAULT ((0)) FOR [Inaktif]
GO
ALTER TABLE [dbo].[REF_SKOM_REWARD] ADD  CONSTRAINT [DF_REF_SREWARD_TglInput]  DEFAULT (getdate()) FOR [TglInput]
GO
ALTER TABLE [dbo].[REF_SKOM_REWARD] ADD  CONSTRAINT [DF_REF_SREWARD_TglUpdate]  DEFAULT (getdate()) FOR [TglUpdate]
GO
ALTER TABLE [dbo].[REF_SKOM_REWARD] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[REF_SKOM_REWARD_DETAIL] ADD  CONSTRAINT [DF_REF_SREWARD_DETAIL_SalesLevel]  DEFAULT ((0)) FOR [SalesLevel]
GO
ALTER TABLE [dbo].[REF_SKOM_REWARD_DETAIL] ADD  CONSTRAINT [DF_REF_SREWARD_DETAIL_Penjualan]  DEFAULT ((0)) FOR [Penjualan]
GO
ALTER TABLE [dbo].[REF_SKOM_REWARD_DETAIL] ADD  CONSTRAINT [DF_REF_SREWARD_DETAIL_Reward]  DEFAULT ('') FOR [Reward]
GO
ALTER TABLE [dbo].[REF_SKOM_REWARD_DETAIL2] ADD  CONSTRAINT [DF_REF_SREWARD_DETAIL2_SalesLevel]  DEFAULT ((0)) FOR [SalesLevel]
GO
ALTER TABLE [dbo].[REF_SKOM_REWARD_DETAIL2] ADD  CONSTRAINT [DF_REF_SREWARD_DETAIL2_TipeTarget]  DEFAULT ('') FOR [TipeTarget]
GO
ALTER TABLE [dbo].[REF_SKOM_REWARD_DETAIL2] ADD  CONSTRAINT [DF_REF_SREWARD_DETAIL2_TargetBawah]  DEFAULT ((0)) FOR [TargetBawah]
GO
ALTER TABLE [dbo].[REF_SKOM_REWARD_DETAIL2] ADD  CONSTRAINT [DF_REF_SREWARD_DETAIL2_TargetAtas]  DEFAULT ((0)) FOR [TargetAtas]
GO
ALTER TABLE [dbo].[REF_SKOM_REWARD_DETAIL2] ADD  CONSTRAINT [DF_REF_SREWARD_DETAIL2_Reward]  DEFAULT ('') FOR [Reward]
GO
ALTER TABLE [dbo].[REF_SKOM_REWARD_LOG] ADD  CONSTRAINT [DF_REF_SKOM_REWARD_LOG_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[REF_SKOM_REWARD_LOG] ADD  CONSTRAINT [DF_REF_SKOM_REWARD_LOG_Aktivitas]  DEFAULT ('') FOR [Aktivitas]
GO
ALTER TABLE [dbo].[REF_SKOM_REWARD_LOG] ADD  CONSTRAINT [DF_REF_SKOM_REWARD_LOG_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[REF_SKOM_REWARD_LOG] ADD  CONSTRAINT [DF_REF_SKOM_REWARD_LOG_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[REF_SKOM_REWARD_LOG] ADD  CONSTRAINT [DF_REF_SKOM_REWARD_LOG_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[REF_SKOM_REWARD_LOG] ADD  CONSTRAINT [DF_REF_SKOM_REWARD_LOG_Pk]  DEFAULT ('') FOR [Pk]
GO
ALTER TABLE [dbo].[REF_SKOM_REWARD_LOG] ADD  CONSTRAINT [DF_REF_SKOM_REWARD_LOG_Approve]  DEFAULT ('') FOR [Approve]
GO
ALTER TABLE [dbo].[REF_SKOM_REWARD_LOG] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[REF_SKOM_TERM] ADD  CONSTRAINT [DF_REF_SKOM_TERM_Nama]  DEFAULT ('') FOR [Nama]
GO
ALTER TABLE [dbo].[REF_SKOM_TERM] ADD  CONSTRAINT [DF_REF_SKOM_TERM_CaraBayar]  DEFAULT ('') FOR [CaraBayar]
GO
ALTER TABLE [dbo].[REF_SKOM_TERM] ADD  CONSTRAINT [DF_REF_SKOM_TERM_Inaktif]  DEFAULT ((0)) FOR [Inaktif]
GO
ALTER TABLE [dbo].[REF_SKOM_TERM] ADD  CONSTRAINT [DF_REF_SKOM_TERM_TglInput]  DEFAULT (getdate()) FOR [TglInput]
GO
ALTER TABLE [dbo].[REF_SKOM_TERM] ADD  CONSTRAINT [DF_REF_SKOM_TERM_TglUpdate]  DEFAULT (getdate()) FOR [TglUpdate]
GO
ALTER TABLE [dbo].[REF_SKOM_TERM] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[REF_SKOM_TERM] ADD  DEFAULT ((0)) FOR [TipeAgent]
GO
ALTER TABLE [dbo].[REF_SKOM_TERM_DETAIL] ADD  CONSTRAINT [DF_REF_SKOM_TERM_DETAIL_Nama]  DEFAULT ('') FOR [Nama]
GO
ALTER TABLE [dbo].[REF_SKOM_TERM_DETAIL] ADD  CONSTRAINT [DF_REF_SKOM_TERM_DETAIL_PersenCair]  DEFAULT ((0)) FOR [PersenCair]
GO
ALTER TABLE [dbo].[REF_SKOM_TERM_DETAIL] ADD  CONSTRAINT [DF_REF_SKOM_TERM_DETAIL_Lunas]  DEFAULT ((0)) FOR [Lunas]
GO
ALTER TABLE [dbo].[REF_SKOM_TERM_DETAIL] ADD  CONSTRAINT [DF_REF_SKOM_TERM_DETAIL_PersenLunas]  DEFAULT ((0)) FOR [PersenLunas]
GO
ALTER TABLE [dbo].[REF_SKOM_TERM_DETAIL] ADD  CONSTRAINT [DF_REF_SKOM_TERM_DETAIL_BF]  DEFAULT ((0)) FOR [BF]
GO
ALTER TABLE [dbo].[REF_SKOM_TERM_DETAIL] ADD  CONSTRAINT [DF_REF_SKOM_TERM_DETAIL_PersenBF]  DEFAULT ((0)) FOR [PersenBF]
GO
ALTER TABLE [dbo].[REF_SKOM_TERM_DETAIL] ADD  CONSTRAINT [DF_REF_SKOM_TERM_DETAIL_DP]  DEFAULT ((0)) FOR [DP]
GO
ALTER TABLE [dbo].[REF_SKOM_TERM_DETAIL] ADD  CONSTRAINT [DF_REF_SKOM_TERM_DETAIL_PersenDP]  DEFAULT ((0)) FOR [PersenDP]
GO
ALTER TABLE [dbo].[REF_SKOM_TERM_DETAIL] ADD  CONSTRAINT [DF_REF_SKOM_TERM_DETAIL_ANG]  DEFAULT ((0)) FOR [ANG]
GO
ALTER TABLE [dbo].[REF_SKOM_TERM_DETAIL] ADD  CONSTRAINT [DF_REF_SKOM_TERM_DETAIL_PersenANG]  DEFAULT ((0)) FOR [PersenANG]
GO
ALTER TABLE [dbo].[REF_SKOM_TERM_DETAIL] ADD  CONSTRAINT [DF_REF_SKOM_TERM_DETAIL_PPJB]  DEFAULT ((0)) FOR [PPJB]
GO
ALTER TABLE [dbo].[REF_SKOM_TERM_DETAIL] ADD  CONSTRAINT [DF_REF_SKOM_TERM_DETAIL_AJB]  DEFAULT ((0)) FOR [AJB]
GO
ALTER TABLE [dbo].[REF_SKOM_TERM_DETAIL] ADD  CONSTRAINT [DF_REF_SKOM_TERM_DETAIL_AKAD]  DEFAULT ((0)) FOR [AKAD]
GO
ALTER TABLE [dbo].[REF_SKOM_TERM_DETAIL] ADD  CONSTRAINT [DF_REF_SKOM_TERM_DETAIL_TipeCair]  DEFAULT ((0)) FOR [TipeCair]
GO
ALTER TABLE [dbo].[REF_SKOM_TERM_LOG] ADD  CONSTRAINT [DF_REF_SKOM_TERM_LOG_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[REF_SKOM_TERM_LOG] ADD  CONSTRAINT [DF_REF_SKOM_TERM_LOG_Aktivitas]  DEFAULT ('') FOR [Aktivitas]
GO
ALTER TABLE [dbo].[REF_SKOM_TERM_LOG] ADD  CONSTRAINT [DF_REF_SKOM_TERM_LOG_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[REF_SKOM_TERM_LOG] ADD  CONSTRAINT [DF_REF_SKOM_TERM_LOG_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[REF_SKOM_TERM_LOG] ADD  CONSTRAINT [DF_REF_SKOM_TERM_LOG_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[REF_SKOM_TERM_LOG] ADD  CONSTRAINT [DF_REF_SKOM_TERM_LOG_Pk]  DEFAULT ('') FOR [Pk]
GO
ALTER TABLE [dbo].[REF_SKOM_TERM_LOG] ADD  CONSTRAINT [DF_REF_SKOM_TERM_LOG_Approve]  DEFAULT ('') FOR [Approve]
GO
ALTER TABLE [dbo].[REF_SKOM_TERM_LOG] ADD  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[REF_TIPE] ADD  CONSTRAINT [DF_REF_TIPE_Tipe]  DEFAULT ('') FOR [Tipe]
GO
ALTER TABLE [dbo].[REF_TIPE] ADD  CONSTRAINT [DF_REF_TIPE_Keterangan]  DEFAULT ('') FOR [Keterangan]
GO
ALTER TABLE [dbo].[REF_TIPE] ADD  CONSTRAINT [DF_REF_TIPE_Jenis]  DEFAULT ('') FOR [Jenis]
GO
ALTER TABLE [dbo].[REF_TIPE_AGENT] ADD  CONSTRAINT [DF_REF_TIPE_NamTipe]  DEFAULT ('') FOR [NamaTipe]
GO
ALTER TABLE [dbo].[REF_TIPE_GIMMICK] ADD  CONSTRAINT [DF_REF_GIMMICK_ID]  DEFAULT ((0)) FOR [ID]
GO
ALTER TABLE [dbo].[REF_TIPE_GIMMICK] ADD  CONSTRAINT [DF_REF_TIPE_GIMMICK_Project]  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[REF_TIPE_GIMMICK_LOG] ADD  CONSTRAINT [DF_REF_TIPE_GIMMICK_LOG_Tgl]  DEFAULT (getdate()) FOR [Tgl]
GO
ALTER TABLE [dbo].[REF_TIPE_GIMMICK_LOG] ADD  CONSTRAINT [DF_REF_TIPE_GIMMICK_LOG_Aktivitas]  DEFAULT ('') FOR [Aktivitas]
GO
ALTER TABLE [dbo].[REF_TIPE_GIMMICK_LOG] ADD  CONSTRAINT [DF_REF_TIPE_GIMMICK_LOG_UserID]  DEFAULT ('') FOR [UserID]
GO
ALTER TABLE [dbo].[REF_TIPE_GIMMICK_LOG] ADD  CONSTRAINT [DF_REF_TIPE_GIMMICK_LOG_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[REF_TIPE_GIMMICK_LOG] ADD  CONSTRAINT [DF_REF_TIPE_GIMMICK_LOG_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[REF_TIPE_GIMMICK_LOG] ADD  CONSTRAINT [DF_REF_TIPE_GIMMICK_LOG_Pk]  DEFAULT ('') FOR [Pk]
GO
ALTER TABLE [dbo].[REF_TIPE_GIMMICK_LOG] ADD  CONSTRAINT [DF_REF_TIPE_GIMMICK_LOG_Approve]  DEFAULT ('') FOR [Approve]
GO
ALTER TABLE [dbo].[REF_TIPE_GIMMICK_LOG] ADD  CONSTRAINT [DF_REF_TIPE_GIMMICK_LOG_Project]  DEFAULT ('') FOR [Project]
GO
ALTER TABLE [dbo].[SmsVendor] ADD  CONSTRAINT [DF_SmsVendor_Nama]  DEFAULT ('') FOR [Nama]
GO
ALTER TABLE [dbo].[SmsVendor] ADD  CONSTRAINT [DF_SmsVendor_Ket]  DEFAULT ('') FOR [Ket]
GO
ALTER TABLE [dbo].[MS_AGENT_TARGET]  WITH CHECK ADD  CONSTRAINT [FK_MS_AGENT_TARGET_MS_AGENT] FOREIGN KEY([NoAgent])
REFERENCES [dbo].[MS_AGENT] ([NoAgent])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MS_AGENT_TARGET] CHECK CONSTRAINT [FK_MS_AGENT_TARGET_MS_AGENT]
GO
ALTER TABLE [dbo].[MS_AJB]  WITH CHECK ADD  CONSTRAINT [FK_MS_AJB_MS_KONTRAK] FOREIGN KEY([NoKontrak])
REFERENCES [dbo].[MS_KONTRAK] ([NoKontrak])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MS_AJB] CHECK CONSTRAINT [FK_MS_AJB_MS_KONTRAK]
GO
ALTER TABLE [dbo].[MS_BAST]  WITH CHECK ADD  CONSTRAINT [FK_MS_BAST_MS_KONTRAK] FOREIGN KEY([NoKontrak])
REFERENCES [dbo].[MS_KONTRAK] ([NoKontrak])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MS_BAST] CHECK CONSTRAINT [FK_MS_BAST_MS_KONTRAK]
GO
ALTER TABLE [dbo].[MS_CUSTOMER_JURNAL]  WITH CHECK ADD  CONSTRAINT [FK_MS_CUSTOMER_JURNAL_MS_CUSTOMER] FOREIGN KEY([NoCustomer])
REFERENCES [dbo].[MS_CUSTOMER] ([NoCustomer])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MS_CUSTOMER_JURNAL] CHECK CONSTRAINT [FK_MS_CUSTOMER_JURNAL_MS_CUSTOMER]
GO
ALTER TABLE [dbo].[MS_IMB]  WITH CHECK ADD  CONSTRAINT [FK_MS_IMB_MS_KONTRAK] FOREIGN KEY([NoKontrak])
REFERENCES [dbo].[MS_KONTRAK] ([NoKontrak])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MS_IMB] CHECK CONSTRAINT [FK_MS_IMB_MS_KONTRAK]
GO
ALTER TABLE [dbo].[MS_KOMISI_CF_DETAIL]  WITH CHECK ADD  CONSTRAINT [FK_MS_CF_DETAIL_MS_CF] FOREIGN KEY([NoCF])
REFERENCES [dbo].[MS_KOMISI_CF] ([NoCF])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MS_KOMISI_CF_DETAIL] CHECK CONSTRAINT [FK_MS_CF_DETAIL_MS_CF]
GO
ALTER TABLE [dbo].[MS_KOMISI_CFP_DETAIL]  WITH CHECK ADD  CONSTRAINT [FK_MS_CFP_DETAIL_MS_CFP] FOREIGN KEY([NoCFP])
REFERENCES [dbo].[MS_KOMISI_CFP] ([NoCFP])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MS_KOMISI_CFP_DETAIL] CHECK CONSTRAINT [FK_MS_CFP_DETAIL_MS_CFP]
GO
ALTER TABLE [dbo].[MS_KOMISI_CFR_DETAIL]  WITH CHECK ADD  CONSTRAINT [FK_MS_CFR_DETAIL_MS_CFR] FOREIGN KEY([NoCFR])
REFERENCES [dbo].[MS_KOMISI_CFR] ([NoCFR])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MS_KOMISI_CFR_DETAIL] CHECK CONSTRAINT [FK_MS_CFR_DETAIL_MS_CFR]
GO
ALTER TABLE [dbo].[MS_KOMISI_DETAIL]  WITH CHECK ADD  CONSTRAINT [FK_MS_KOMISI_DETAIL_MS_KOMISI] FOREIGN KEY([NoKomisi])
REFERENCES [dbo].[MS_KOMISI] ([NoKomisi])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MS_KOMISI_DETAIL] CHECK CONSTRAINT [FK_MS_KOMISI_DETAIL_MS_KOMISI]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD_DETAIL]  WITH CHECK ADD  CONSTRAINT [FK_MS_KOMISI_REWARD_DETAIL_MS_KOMISI_REWARD] FOREIGN KEY([NoReward])
REFERENCES [dbo].[MS_KOMISI_REWARD] ([NoReward])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD_DETAIL] CHECK CONSTRAINT [FK_MS_KOMISI_REWARD_DETAIL_MS_KOMISI_REWARD]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD_P_DETAIL]  WITH CHECK ADD  CONSTRAINT [FK_MS_KOMISI_REWARD_P_DETAIL_MS_KOMISI_REWARD_P] FOREIGN KEY([NoRP])
REFERENCES [dbo].[MS_KOMISI_REWARD_P] ([NoRP])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD_P_DETAIL] CHECK CONSTRAINT [FK_MS_KOMISI_REWARD_P_DETAIL_MS_KOMISI_REWARD_P]
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD_R_DETAIL]  WITH CHECK ADD  CONSTRAINT [FK_MS_KOMISI_REWARD_R_DETAIL_MS_KOMISI_REWARD_R] FOREIGN KEY([NoRR])
REFERENCES [dbo].[MS_KOMISI_REWARD_R] ([NoRR])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MS_KOMISI_REWARD_R_DETAIL] CHECK CONSTRAINT [FK_MS_KOMISI_REWARD_R_DETAIL_MS_KOMISI_REWARD_R]
GO
ALTER TABLE [dbo].[MS_KOMISI_TERM]  WITH CHECK ADD  CONSTRAINT [FK_MS_KOMISI_TERM_MS_KOMISI] FOREIGN KEY([NoKomisi])
REFERENCES [dbo].[MS_KOMISI] ([NoKomisi])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MS_KOMISI_TERM] CHECK CONSTRAINT [FK_MS_KOMISI_TERM_MS_KOMISI]
GO
ALTER TABLE [dbo].[MS_KOMISIP_DETAIL]  WITH CHECK ADD  CONSTRAINT [FK_MS_KOMISIP_DETAIL_MS_KOMISIP] FOREIGN KEY([NoKomisiP])
REFERENCES [dbo].[MS_KOMISIP] ([NoKomisiP])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MS_KOMISIP_DETAIL] CHECK CONSTRAINT [FK_MS_KOMISIP_DETAIL_MS_KOMISIP]
GO
ALTER TABLE [dbo].[MS_KOMISIR_DETAIL]  WITH CHECK ADD  CONSTRAINT [FK_MS_KOMISIR_DETAIL_MS_KOMISIR] FOREIGN KEY([NoKomisiR])
REFERENCES [dbo].[MS_KOMISIR] ([NoKomisiR])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MS_KOMISIR_DETAIL] CHECK CONSTRAINT [FK_MS_KOMISIR_DETAIL_MS_KOMISIR]
GO
ALTER TABLE [dbo].[MS_KONTRAK]  WITH CHECK ADD  CONSTRAINT [FK_MS_KONTRAK_MS_AGENT] FOREIGN KEY([NoAgent])
REFERENCES [dbo].[MS_AGENT] ([NoAgent])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MS_KONTRAK] CHECK CONSTRAINT [FK_MS_KONTRAK_MS_AGENT]
GO
ALTER TABLE [dbo].[MS_KONTRAK]  WITH CHECK ADD  CONSTRAINT [FK_MS_KONTRAK_MS_CUSTOMER] FOREIGN KEY([NoCustomer])
REFERENCES [dbo].[MS_CUSTOMER] ([NoCustomer])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MS_KONTRAK] CHECK CONSTRAINT [FK_MS_KONTRAK_MS_CUSTOMER]
GO
ALTER TABLE [dbo].[MS_KONTRAK]  WITH CHECK ADD  CONSTRAINT [FK_MS_KONTRAK_MS_UNIT] FOREIGN KEY([NoStock])
REFERENCES [dbo].[MS_UNIT] ([NoStock])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MS_KONTRAK] CHECK CONSTRAINT [FK_MS_KONTRAK_MS_UNIT]
GO
ALTER TABLE [dbo].[MS_KONTRAK_AGENT]  WITH CHECK ADD  CONSTRAINT [FK_M_KONTRAK_AGENT_MS_AGENT] FOREIGN KEY([NoAgent])
REFERENCES [dbo].[MS_AGENT] ([NoAgent])
GO
ALTER TABLE [dbo].[MS_KONTRAK_AGENT] CHECK CONSTRAINT [FK_M_KONTRAK_AGENT_MS_AGENT]
GO
ALTER TABLE [dbo].[MS_KONTRAK_AGENT]  WITH CHECK ADD  CONSTRAINT [FK_M_KONTRAK_AGENT_MS_KONTRAK] FOREIGN KEY([NoKontrak])
REFERENCES [dbo].[MS_KONTRAK] ([NoKontrak])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MS_KONTRAK_AGENT] CHECK CONSTRAINT [FK_M_KONTRAK_AGENT_MS_KONTRAK]
GO
ALTER TABLE [dbo].[MS_KONTRAK_AGENT]  WITH CHECK ADD  CONSTRAINT [FK_M_KONTRAK_AGENT_REF_AGENT_LEVEL] FOREIGN KEY([SalesLevel])
REFERENCES [dbo].[REF_AGENT_LEVEL] ([LevelID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MS_KONTRAK_AGENT] CHECK CONSTRAINT [FK_M_KONTRAK_AGENT_REF_AGENT_LEVEL]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL]  WITH CHECK ADD  CONSTRAINT [FK_MS_KONTRAK_APPROVAL_MS_AGENT] FOREIGN KEY([NoAgent])
REFERENCES [dbo].[MS_AGENT] ([NoAgent])
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL] CHECK CONSTRAINT [FK_MS_KONTRAK_APPROVAL_MS_AGENT]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL]  WITH CHECK ADD  CONSTRAINT [FK_MS_KONTRAK_APPROVAL_MS_CUSTOMER] FOREIGN KEY([NoCustomer])
REFERENCES [dbo].[MS_CUSTOMER] ([NoCustomer])
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL] CHECK CONSTRAINT [FK_MS_KONTRAK_APPROVAL_MS_CUSTOMER]
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL]  WITH CHECK ADD  CONSTRAINT [FK_MS_KONTRAK_APPROVAL_MS_UNIT] FOREIGN KEY([NoStock])
REFERENCES [dbo].[MS_UNIT] ([NoStock])
GO
ALTER TABLE [dbo].[MS_KONTRAK_APPROVAL] CHECK CONSTRAINT [FK_MS_KONTRAK_APPROVAL_MS_UNIT]
GO
ALTER TABLE [dbo].[MS_KONTRAK_GIMMICK]  WITH CHECK ADD  CONSTRAINT [FK_MS_KONTRAK_GIMMICK_MS_KONTRAK] FOREIGN KEY([NoKontrak])
REFERENCES [dbo].[MS_KONTRAK] ([NoKontrak])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MS_KONTRAK_GIMMICK] CHECK CONSTRAINT [FK_MS_KONTRAK_GIMMICK_MS_KONTRAK]
GO
ALTER TABLE [dbo].[MS_KONTRAK_JURNAL]  WITH CHECK ADD  CONSTRAINT [FK_MS_KONTRAK_JURNAL_MS_KONTRAK] FOREIGN KEY([NoKontrak])
REFERENCES [dbo].[MS_KONTRAK] ([NoKontrak])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MS_KONTRAK_JURNAL] CHECK CONSTRAINT [FK_MS_KONTRAK_JURNAL_MS_KONTRAK]
GO
ALTER TABLE [dbo].[MS_PELUNASAN]  WITH CHECK ADD  CONSTRAINT [FK_MS_PELUNASAN_MS_KONTRAK] FOREIGN KEY([NoKontrak])
REFERENCES [dbo].[MS_KONTRAK] ([NoKontrak])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MS_PELUNASAN] CHECK CONSTRAINT [FK_MS_PELUNASAN_MS_KONTRAK]
GO
ALTER TABLE [dbo].[MS_PELUNASAN_KPA]  WITH CHECK ADD  CONSTRAINT [FK_MS_PELUNASAN_KPA_MS_KONTRAK] FOREIGN KEY([NoKontrak])
REFERENCES [dbo].[MS_KONTRAK] ([NoKontrak])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MS_PELUNASAN_KPA] CHECK CONSTRAINT [FK_MS_PELUNASAN_KPA_MS_KONTRAK]
GO
ALTER TABLE [dbo].[MS_PPJB]  WITH CHECK ADD  CONSTRAINT [FK_MS_PPJB_MS_KONTRAK] FOREIGN KEY([NoKontrak])
REFERENCES [dbo].[MS_KONTRAK] ([NoKontrak])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MS_PPJB] CHECK CONSTRAINT [FK_MS_PPJB_MS_KONTRAK]
GO
ALTER TABLE [dbo].[MS_PRICELIST]  WITH CHECK ADD  CONSTRAINT [FK_MS_PRICELIST_MS_UNIT] FOREIGN KEY([NoStock])
REFERENCES [dbo].[MS_UNIT] ([NoStock])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MS_PRICELIST] CHECK CONSTRAINT [FK_MS_PRICELIST_MS_UNIT]
GO
ALTER TABLE [dbo].[MS_PRICELIST]  WITH CHECK ADD  CONSTRAINT [FK_MS_PRICELIST_REF_SKEMA] FOREIGN KEY([NoSkema])
REFERENCES [dbo].[REF_SKEMA] ([Nomor])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MS_PRICELIST] CHECK CONSTRAINT [FK_MS_PRICELIST_REF_SKEMA]
GO
ALTER TABLE [dbo].[MS_PRICELIST_HISTORY]  WITH CHECK ADD  CONSTRAINT [FK_MS_PRICELIST_HISTORY_MS_UNIT] FOREIGN KEY([NoStock])
REFERENCES [dbo].[MS_UNIT] ([NoStock])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MS_PRICELIST_HISTORY] CHECK CONSTRAINT [FK_MS_PRICELIST_HISTORY_MS_UNIT]
GO
ALTER TABLE [dbo].[MS_RESERVASI]  WITH CHECK ADD  CONSTRAINT [FK_MS_RESERVASI_MS_AGENT] FOREIGN KEY([NoAgent])
REFERENCES [dbo].[MS_AGENT] ([NoAgent])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MS_RESERVASI] CHECK CONSTRAINT [FK_MS_RESERVASI_MS_AGENT]
GO
ALTER TABLE [dbo].[MS_RESERVASI]  WITH CHECK ADD  CONSTRAINT [FK_MS_RESERVASI_MS_CUSTOMER] FOREIGN KEY([NoCustomer])
REFERENCES [dbo].[MS_CUSTOMER] ([NoCustomer])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MS_RESERVASI] CHECK CONSTRAINT [FK_MS_RESERVASI_MS_CUSTOMER]
GO
ALTER TABLE [dbo].[MS_RESERVASI]  WITH CHECK ADD  CONSTRAINT [FK_MS_RESERVASI_MS_UNIT] FOREIGN KEY([NoStock])
REFERENCES [dbo].[MS_UNIT] ([NoStock])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MS_RESERVASI] CHECK CONSTRAINT [FK_MS_RESERVASI_MS_UNIT]
GO
ALTER TABLE [dbo].[MS_RESERVASI_JURNAL]  WITH CHECK ADD  CONSTRAINT [FK_MS_RESERVASI_JURNAL_MS_RESERVASI] FOREIGN KEY([NoReservasi])
REFERENCES [dbo].[MS_RESERVASI] ([NoReservasi])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MS_RESERVASI_JURNAL] CHECK CONSTRAINT [FK_MS_RESERVASI_JURNAL_MS_RESERVASI]
GO
ALTER TABLE [dbo].[MS_RESERVASI_TAGIHAN]  WITH CHECK ADD  CONSTRAINT [FK_MS_RESERVASI_TAGIHAN_MS_RESERVASI] FOREIGN KEY([NoReservasi])
REFERENCES [dbo].[MS_RESERVASI] ([NoReservasi])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MS_RESERVASI_TAGIHAN] CHECK CONSTRAINT [FK_MS_RESERVASI_TAGIHAN_MS_RESERVASI]
GO
ALTER TABLE [dbo].[MS_SERTIFIKAT]  WITH CHECK ADD  CONSTRAINT [FK_MS_SERTIFIKAT_MS_KONTRAK] FOREIGN KEY([NoKontrak])
REFERENCES [dbo].[MS_KONTRAK] ([NoKontrak])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MS_SERTIFIKAT] CHECK CONSTRAINT [FK_MS_SERTIFIKAT_MS_KONTRAK]
GO
ALTER TABLE [dbo].[MS_TAGIHAN]  WITH CHECK ADD  CONSTRAINT [FK_MS_TAGIHAN_MS_KONTRAK] FOREIGN KEY([NoKontrak])
REFERENCES [dbo].[MS_KONTRAK] ([NoKontrak])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MS_TAGIHAN] CHECK CONSTRAINT [FK_MS_TAGIHAN_MS_KONTRAK]
GO
ALTER TABLE [dbo].[MS_TAGIHAN_KPA]  WITH CHECK ADD  CONSTRAINT [FK_MS_TAGIHAN_KPA_MS_KONTRAK] FOREIGN KEY([NoKontrak])
REFERENCES [dbo].[MS_KONTRAK] ([NoKontrak])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MS_TAGIHAN_KPA] CHECK CONSTRAINT [FK_MS_TAGIHAN_KPA_MS_KONTRAK]
GO
ALTER TABLE [dbo].[REF_AGENT_LEVEL]  WITH CHECK ADD  CONSTRAINT [FK_REF_AGENT_LEVEL_REF_AGENT_TIPE] FOREIGN KEY([Tipe])
REFERENCES [dbo].[REF_AGENT_TIPE] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[REF_AGENT_LEVEL] CHECK CONSTRAINT [FK_REF_AGENT_LEVEL_REF_AGENT_TIPE]
GO
ALTER TABLE [dbo].[REF_SKEMA_DETAIL]  WITH CHECK ADD  CONSTRAINT [FK_REF_SKEMA_DETAIL_REF_SKEMA] FOREIGN KEY([Nomor])
REFERENCES [dbo].[REF_SKEMA] ([Nomor])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[REF_SKEMA_DETAIL] CHECK CONSTRAINT [FK_REF_SKEMA_DETAIL_REF_SKEMA]
GO
ALTER TABLE [dbo].[REF_SKOM_CF_DETAIL]  WITH CHECK ADD  CONSTRAINT [FK_REF_SKOM_CF_DETAIL_REF_SKOM_CF] FOREIGN KEY([NoSkema])
REFERENCES [dbo].[REF_SKOM_CF] ([NoSkema])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[REF_SKOM_CF_DETAIL] CHECK CONSTRAINT [FK_REF_SKOM_CF_DETAIL_REF_SKOM_CF]
GO
ALTER TABLE [dbo].[REF_SKOM_CF_DETAIL2]  WITH CHECK ADD  CONSTRAINT [FK_REF_SKOM_CF_DETAIL2_REF_SKOM_CF] FOREIGN KEY([NoSkema])
REFERENCES [dbo].[REF_SKOM_CF] ([NoSkema])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[REF_SKOM_CF_DETAIL2] CHECK CONSTRAINT [FK_REF_SKOM_CF_DETAIL2_REF_SKOM_CF]
GO
ALTER TABLE [dbo].[REF_SKOM_DETAIL]  WITH CHECK ADD  CONSTRAINT [FK_REF_SKOM_DETAIL_REF_SKOM] FOREIGN KEY([NoSkema])
REFERENCES [dbo].[REF_SKOM] ([NoSkema])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[REF_SKOM_DETAIL] CHECK CONSTRAINT [FK_REF_SKOM_DETAIL_REF_SKOM]
GO
ALTER TABLE [dbo].[REF_SKOM_DETAIL2]  WITH CHECK ADD  CONSTRAINT [FK_REF_SKOM_DETAIL2_REF_SKOM] FOREIGN KEY([NoSkema])
REFERENCES [dbo].[REF_SKOM] ([NoSkema])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[REF_SKOM_DETAIL2] CHECK CONSTRAINT [FK_REF_SKOM_DETAIL2_REF_SKOM]
GO
ALTER TABLE [dbo].[REF_SKOM_REWARD_DETAIL]  WITH CHECK ADD  CONSTRAINT [FK_REF_SKOM_REWARD_DETAIL_REF_SKOM_REWARD] FOREIGN KEY([NoSkema])
REFERENCES [dbo].[REF_SKOM_REWARD] ([NoSkema])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[REF_SKOM_REWARD_DETAIL] CHECK CONSTRAINT [FK_REF_SKOM_REWARD_DETAIL_REF_SKOM_REWARD]
GO
ALTER TABLE [dbo].[REF_SKOM_REWARD_DETAIL2]  WITH CHECK ADD  CONSTRAINT [FK_REF_SKOM_REWARD_DETAIL2_REF_SKOM_REWARD] FOREIGN KEY([NoSkema])
REFERENCES [dbo].[REF_SKOM_REWARD] ([NoSkema])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[REF_SKOM_REWARD_DETAIL2] CHECK CONSTRAINT [FK_REF_SKOM_REWARD_DETAIL2_REF_SKOM_REWARD]
GO
ALTER TABLE [dbo].[REF_SKOM_TERM_DETAIL]  WITH CHECK ADD  CONSTRAINT [FK_REF_SKOM_TERM_DETAIL_REF_SKOM_TERM] FOREIGN KEY([NoTermin])
REFERENCES [dbo].[REF_SKOM_TERM] ([NoTermin])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[REF_SKOM_TERM_DETAIL] CHECK CONSTRAINT [FK_REF_SKOM_TERM_DETAIL_REF_SKOM_TERM]
GO
/****** Object:  StoredProcedure [dbo].[spAgentDaftar]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spAgentDaftar]
	 @Nama varchar (100) = ''
	,@Principal varchar (100) = ''
	,@Principalmgr varchar (100) = ''
	,@Alamat varchar (200) = ''
	,@Kontak varchar (100) = ''
	,@NPWP varchar(50) = ''
	,@Tipe int
	,@Level int
	,@Grade int
	,@Atasan int
	,@Email varchar(50) = ''
	,@RekBank varchar(50) = ''
	,@Cabang varchar(30) = ''
	,@NoRek varchar(30) = ''
	,@AtasNama varchar(50) = ''
	,@Tipe2 varchar(50) = ''
	,@Jabatan varchar(50) = ''
AS

DECLARE @NoAgent int
SELECT @NoAgent = ISNULL(MAX(NoAgent),0) + 1 FROM MS_AGENT

INSERT INTO MS_AGENT
(
	 NoAgent
	,Nama
	,Principal
	,PrincipalMgr
	,Alamat
	,Kontak
	,NPWP
	,SalesTipe
	,SalesLevel
	,SalesGrade
	,Atasan
	,Email
	,RekBank
	,Cabang
	,Rekening
	,AtasNama
	,Tipe
	,Jabatan
)
VALUES
(
	 @NoAgent
	,@Nama
	,@Principal
	,@PrincipalMgr
	,@Alamat
	,@Kontak
	,@NPWP
	,@Tipe
	,@Level
	,@Grade
	,@Atasan
	,@Email
	,@RekBank
	,@Cabang
	,@NoRek
	,@AtasNama
	,@Tipe2
	,@Jabatan
)





GO
/****** Object:  StoredProcedure [dbo].[spAgentDaftar2]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[spAgentDaftar2]
	 @NoAgent int
AS
--IF NOT EXISTS(SELECT * FROM ISC064a_MARKETINGJUAL..MS_AGENT
--      WHERE NoAgent  = @NoAgent)
--INSERT ISC064a_MARKETINGJUAL..MS_AGENT SELECT * FROM ISC064_MARKETINGJUAL..MS_AGENT 
--WHERE ISC064_MARKETINGJUAL..MS_AGENT.NoAgent  = @NoAgent









GO
/****** Object:  StoredProcedure [dbo].[spAgentDel]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Hapus agent PERMANEN
*/

CREATE PROCEDURE [dbo].[spAgentDel]
	@NoAgent int
AS

-- Agent tidak boleh dihapus jika sudah pernah melakukan kontrak
IF EXISTS (SELECT NoAgent FROM MS_RESERVASI WHERE NoAgent = @NoAgent)
	RETURN
IF EXISTS (SELECT NoAgent FROM MS_KONTRAK WHERE NoAgent = @NoAgent)
	RETURN

DELETE FROM MS_AGENT WHERE NoAgent = @NoAgent









GO
/****** Object:  StoredProcedure [dbo].[spAgentEdit]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Edit data agent
*/

CREATE PROCEDURE [dbo].[spAgentEdit]
	 @NoAgent int
	,@Nama varchar (100) = ''
	,@Alamat varchar (100) = ''
	,@Kontak varchar (100) = ''
	,@NPWP varchar (50) = ''
	,@Tipe int 
	,@Level int 
	,@Atasan int
	,@Email varchar(50) = ''
	,@RekBank varchar(50) = ''
	,@NoRek varchar(50)
	,@AtasNama varchar(50) = ''
	,@Status varchar (1) = ''
AS

UPDATE MS_AGENT
SET
	 Nama = @Nama
	,Alamat = @Alamat
	,Kontak = @Kontak
	,NPWP = @NPWP
	,SalesTipe = @Tipe
	,SalesLevel = @Level
	,Atasan = @Atasan
	,Email = @Email
	,RekBank = @RekBank
	,Rekening = @NoRek
	,AtasNama = @AtasNama
	,Status = @Status
	,TglEdit = GETDATE()
WHERE NoAgent = @NoAgent







GO
/****** Object:  StoredProcedure [dbo].[spAgentEditSSK]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Set skema komisi
*/

CREATE PROCEDURE [dbo].[spAgentEditSSK]
	 @NoAgent int
	,@Target1 money = 0
	,@Target2 money = 0
	,@Target3 money = 0
	,@Target4 money = 0
	,@Target5 money = 0
	,@Skema0 int = 0
	,@Skema1 int = 0
	,@Skema2 int = 0
	,@Skema3 int = 0
	,@Skema4 int = 0
	,@Skema5 int = 0
AS

UPDATE MS_AGENT
SET
	 Target1 = @Target1
	,Target2 = @Target2
	,Target3 = @Target3
	,Target4 = @Target4
	,Target5 = @Target5
	,Skema0 = @Skema0
	,Skema1 = @Skema1
	,Skema2 = @Skema2
	,Skema3 = @Skema3
	,Skema4 = @Skema4
	,Skema5 = @Skema5
	,TglEdit = GETDATE()
WHERE NoAgent = @NoAgent









GO
/****** Object:  StoredProcedure [dbo].[spAgentLevelDaftar]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
Pendaftaran agent baru
*/

CREATE PROCEDURE [dbo].[spAgentLevelDaftar]
	 @Nama varchar (100) = ''
	 ,@Tipe varchar(10) =''
AS

DECLARE @LevelID int
SELECT @LevelID = ISNULL(MAX(LevelID),0) + 1 FROM MS_AGENT_LEVEL

INSERT INTO MS_AGENT_LEVEL
(
	 LevelID
	,Nama
	,Tipe

)
VALUES
(
	 @LevelID
	,@Nama
	,@Tipe
)







GO
/****** Object:  StoredProcedure [dbo].[spAgentTarget]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Pendaftaran kontrak baru
*/

CREATE PROCEDURE [dbo].[spAgentTarget]
	 @NoAgent int
	,@Bulan int
	,@Tahun int
	,@Target money
AS

INSERT INTO MS_AGENT_TARGET
(
	 NoAgent
	,Bulan
	,Tahun
	,Target
)
VALUES
(
	@NoAgent
	,@Bulan
	,@Tahun
	,@Target
)









GO
/****** Object:  StoredProcedure [dbo].[spAJB]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Prosedur AJB
*/

CREATE PROCEDURE [dbo].[spAJB]
	 @NoKontrak varchar(50)
	,@NoAJB varchar(20) = ''
	,@TglAJB datetime
AS

INSERT INTO MS_AJB
(
	 NoKontrak
	,NoAJB
	,TglAJB
)
VALUES
(
	 @NoKontrak
	,@NoAJB
	,@TglAJB 
)








GO
/****** Object:  StoredProcedure [dbo].[spApproval]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
Prosedur ganti unit
*/

CREATE PROCEDURE [dbo].[spApproval]
	 @NoApproval varchar(20)
	,@Sumber varchar(10)
	,@SumberID varchar(50)	
	,@TglPengajuan datetime
	,@Project varchar(20)	
AS

INSERT INTO MS_APPROVAL 
(
	 NoApproval
	,Sumber
	,SumberID
	,TglPengajuan
	,Status
	,Project	
)
VALUES 
(
	 @NoApproval
	,@Sumber
	,@SumberID
	,@TglPengajuan
	,'NEW'
	,@Project	
)







GO
/****** Object:  StoredProcedure [dbo].[spApprovalDetil]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
Prosedur ganti unit
*/

CREATE PROCEDURE [dbo].[spApprovalDetil]
	 @NoApproval varchar(20)
	,@SN int
	,@UserID varchar(20)
	,@Level int
	,@Nama varchar(50)		
AS

INSERT INTO MS_APPROVAL_DETAIL
(
	 NoApproval
	,SN
	,UserID
	,Lvl
	,Nama
	,Approve	
)
VALUES 
(
	 @NoApproval
	,@SN
	,@UserID
	,@Level
	,@Nama
	,0
)







GO
/****** Object:  StoredProcedure [dbo].[spAutoExpHoldUnit]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[spAutoExpHoldUnit]
AS

UPDATE MS_HOLD
SET
	Status = 'E'
WHERE
	Status = 'A' AND
	DATEDIFF(n,TglHoldExpired,GETDATE()) >= 0
DECLARE @NoHold varchar(50)
DECLARE @NoStock varchar(50)
DECLARE rs CURSOR FOR
	SELECT NoHold,NoStock
	FROM MS_HOLD A WHERE A.Status='E' AND A.NoStock NOT IN (SELECT NoStock FROM MS_KONTRAK 
	WHERE NoStock = A.NoStock AND Status='A')
OPEN rs
FETCH NEXT FROM rs INTO @NoHold, @NoStock

WHILE @@FETCH_STATUS = 0
BEGIN
UPDATE MS_UNIT SET
		Status='A'
	WHERE
	NoStock = @NoStock

	FETCH NEXT FROM rs INTO @NoHold, @NoStock
END

CLOSE rs
DEALLOCATE rs













GO
/****** Object:  StoredProcedure [dbo].[spAutoExpireReservasi]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Job : ISC064_RT
Sistem expire reservasi

SIMULASI TANGGAL
Tgl GetDate	TglTransaksi
----------------------------------------------------------------------------------------------
1/1			4/1
2/1			4/1
3/1			4/1
4/1			4/1	Edit !
5/1			4/1	Edit !
*/

CREATE PROCEDURE [dbo].[spAutoExpireReservasi]
AS

UPDATE MS_RESERVASI
SET
	Status = 'E'
WHERE
	Status = 'A' AND
	DATEDIFF(n,TglExpire,GETDATE()) >= 0









GO
/****** Object:  StoredProcedure [dbo].[spAutoInaktivasi]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Job : ISC064_DAILY
Customer yang tidak pernah melakukan transaksi lagi setelah masa 1 tahun akan dicatat sebagai INAKTIF.

SIMULASI TANGGAL
Tgl GetDate	TglTransaksi + 1 tahun
----------------------------------------------------------------------------------------------
1/1			4/1
2/1			4/1
3/1			4/1
4/1			4/1	Edit !
5/1			4/1	Edit !
*/

CREATE PROCEDURE [dbo].[spAutoInaktivasi]
AS

UPDATE MS_CUSTOMER
SET
	Status = 'I'
WHERE
	Status = 'A'
AND
	CONVERT(varchar, GETDATE(), 112) >= 
	CONVERT(varchar, DATEADD(yy, 1, TglTransaksi), 112)
AND 
	(SELECT COUNT(*) FROM MS_KONTRAK Kontrak WHERE Kontrak.NoCustomer = MS_CUSTOMER.NoCustomer AND Kontrak.Status = 'A') = 0









GO
/****** Object:  StoredProcedure [dbo].[spAutoJatuhTempo]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Job : ISC064_DAILY
Sistem reminder untuk tagihan, unit dan kontrak yang akan jatuh tempo
*/

CREATE PROCEDURE [dbo].[spAutoJatuhTempo]
AS

UPDATE MS_KONTRAK SET ST = 'T'
WHERE ST = 'B'
AND
	CONVERT(varchar, GETDATE(), 112) >
	CONVERT(varchar, TargetST, 112)
AND Status <> 'B'









GO
/****** Object:  StoredProcedure [dbo].[spBankKPABaru]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
Pendaftaran account baru
*/

CREATE PROCEDURE [dbo].[spBankKPABaru]
	 @KodeBank varchar(20)
	,@Bank varchar(50)
	,@Project varchar(20)
AS

INSERT INTO REF_BANKKPA
(
	 KodeBank
	,Bank
	,Project
)
VALUES
(
	 @KodeBank
	,@Bank
	,@Project
)













GO
/****** Object:  StoredProcedure [dbo].[spBankKPADel]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
Delete account (PERMANEN)
*/

CREATE PROCEDURE [dbo].[spBankKPADel]
	 @KodeBank varchar(20)
	,@Project varchar(20)
AS

-- Bank Tidak Boleh dihapus jika sudah menjadi referensi Bank KPA di MS_KONTRAK
IF EXISTS (SELECT BankKPR FROM MS_KONTRAK WHERE BankKPR = @KodeBank AND Project = @Project)
	RETURN
	
DELETE FROM REF_BANKKPA WHERE KodeBank = @KodeBank AND Project = @Project













GO
/****** Object:  StoredProcedure [dbo].[spBankKPAEdit]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
Edit data account
*/

CREATE PROCEDURE [dbo].[spBankKPAEdit]
	 @KodeBankLama varchar(20)
	,@KodeBankBaru varchar(20)
	,@Bank varchar(50)
	,@Project varchar(50)
AS

UPDATE REF_BANKKPA SET
	 KodeBank = @KodeBankBaru
	,Bank = @Bank	
WHERE
KodeBank = @KodeBankLama AND Project = @Project

UPDATE REF_BANKKPA_LOG SET Pk = @KodeBankBaru WHERE Pk = @KodeBankLama













GO
/****** Object:  StoredProcedure [dbo].[spBiayaTambahan]    Script Date: 05/04/2019 15.51.15 ******/
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
--IF EXISTS(SELECT * FROM ISC064a_MARKETINGJUAL..MS_TAGIHAN
--      WHERE NoKontrak  = @NoKontrak AND NoUrut = @NoUrut)
--   UPDATE ISC064a_MARKETINGJUAL..MS_TAGIHAN SET TglJT = CONVERT(datetime, @TglJT, 101),
--   NilaiTagihan = @NilaiTagihan WHERE NoKontrak  = @NoKontrak AND NoUrut = @NoUrut
--INSERT INTO ISC064a_MARKETINGJUAL..MS_TAGIHAN
--	(NoKontrak, NoUrut, NamaTagihan, TglJT, NilaiTagihan, Tipe)
--VALUES
--	(@NoKontrak,@NoUrut, @NamaTagihan, CONVERT(datetime, @TglJT, 101), @NilaiTagihan, @Tipe)
--SET QUOTED_IDENTIFIER OFF









GO
/****** Object:  StoredProcedure [dbo].[spCall_NUP]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/*
	insert launching call
*/

CREATE PROCEDURE [dbo].[spCall_NUP]
	@NUPID varchar(20),
	@CounterID int
AS


INSERT INTO  MS_LAUNCHING_CALL
(
	NUPID,
	CounterID,
	isCalled
)
VALUES
(	
	@NUPID,
	@CounterID,
	0
)


Delete MS_LAUNCHING_CALL where CounterID=0





GO
/****** Object:  StoredProcedure [dbo].[spComplain]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Jurnal Kontrak
*/
CREATE PROCEDURE [dbo].[spComplain]
	 @NoKontrak varchar(50)
	,@NoComplain int
	,@NoCustomer int
	,@Detil varchar(MAX)
	,@TglComplain datetime

AS

INSERT INTO MS_COMPLAIN
(
	 NoKontrak,
	 NoComplain,
	 NoCustomer,
	 Detil,
	 TglComplain,
	 TglInput
)
VALUES
(
	 @NoKontrak,
	 @NoComplain,
	 @NoCustomer,
	 @Detil,
	 @TglComplain,
	 GETDATE()
)









GO
/****** Object:  StoredProcedure [dbo].[spCustomerDaftar]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Pendaftaran customer baru
*/

CREATE PROCEDURE [dbo].[spCustomerDaftar]
	 @Nama varchar (100) = ''
	,@NamaBisnis varchar (100) = ''
	,@NoTelp varchar (50) = ''
	,@NoHp varchar (50) = ''
	,@NoKantor varchar (50) = ''
	,@NoFax varchar (50) = ''
	,@Email varchar (100) = ''
	,@NoKTP varchar (50) = ''
	,@KTP1 varchar (150) = ''
	,@KTP2 varchar (50) = ''
	,@KTP3 varchar (50) = ''
	,@KTP4 varchar (50) = ''
	,@Kodepos varchar(6) = ''
	,@Alamat1 varchar (150) = ''
	,@Alamat2 varchar (50) = ''
	,@Alamat3 varchar (50) = ''
	,@Kantor1 varchar (150) = ''
	,@Kantor2 varchar (50) = ''
	,@Kantor3 varchar (50) = ''
	,@Agama varchar (50) = ''
	--,@TglLahir datetime
	,@JenisBisnis varchar(100) = ''
	,@MerekBisnis varchar(100) = ''
	,@UnitLama varchar(20) = ''
	,@LuasLama money = 0
	,@TokoLama varchar(100) = ''
	,@ZoningLama varchar(100) = ''
	,@GedungLama varchar(100) = ''
	,@TeleponLama varchar(100) = ''
	,@AkteLama varchar(100) = ''
	,@TipeCs varchar(20) = ''
	,@Salutation varchar(50) = ''
	,@NPWP varchar (100) = ''
	,@NPWPAlamat varchar (150) = ''
	,@wn varchar (50) = ''
	,@Status varchar (1) = ''
	,@Pekerjaan varchar (100) = ''
AS

DECLARE @NoCustomer int
SELECT @NoCustomer = ISNULL(MAX(NoCustomer),0) + 1 FROM MS_CUSTOMER

INSERT INTO MS_CUSTOMER
(
	 NoCustomer
	,Nama
	,NamaBisnis
	,NoTelp
	,NoHp
	,NoKantor
	,NoFax
	,Email
	,NoKTP
	,KTP1
	,KTP2
	,KTP3
	,KTP4
	,Alamat1
	,Alamat2
	,Alamat3
	,Kantor1
	,Kantor2
	,Kantor3
	,Agama
	--,TglLahir
	,JenisBisnis
	,MerekBisnis
	,UnitLama
	,LuasLama
	,TokoLama
	,ZoningLama
	,GedungLama
	,TeleponLama
	,AkteLama
	,TipeCs
	,Salutation
	,NPWP
	,NPWPAlamat1
	,Kewarganegaraan
	,Marital
	,Pekerjaan
	,Kodepos
)
VALUES
(
	 @NoCustomer
	,@Nama
	,@NamaBisnis
	,@NoTelp
	,@NoHp
	,@NoKantor
	,@NoFax
	,@Email
	,@NoKTP
	,@KTP1
	,@KTP2
	,@KTP3
	,@KTP4
	,@Alamat1
	,@Alamat2
	,@Alamat3
	,@Kantor1
	,@Kantor2
	,@Kantor3
	,@Agama
	--,CONVERT(datetime, @TglLahir, 101)
	,@JenisBisnis
	,@MerekBisnis
	,@UnitLama
	,@LuasLama
	,@TokoLama
	,@ZoningLama
	,@GedungLama
	,@TeleponLama
	,@AkteLama
	,@TipeCs
	,@Salutation
	,@NPWP
	,@NPWPAlamat
	,@wn
	,@Status
	,@Pekerjaan
	,@Kodepos
)









GO
/****** Object:  StoredProcedure [dbo].[spCustomerDaftar2]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[spCustomerDaftar2]
	 @NoCustomer int
AS
--IF NOT EXISTS(SELECT * FROM ISC064_MARKETINGJUAL..MS_Customer
--      WHERE NoCustomer  = @NoCustomer)
--INSERT ISC064_MARKETINGJUAL..MS_CUSTOMER SELECT * FROM ISC064_MARKETINGJUAL..MS_CUSTOMER 
--WHERE ISC064_MARKETINGJUAL..MS_CUSTOMER.NoCustomer  = @NoCustomer
--SET QUOTED_IDENTIFIER OFF









GO
/****** Object:  StoredProcedure [dbo].[spCustomerDaftarBSA]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Pendaftaran customer baru
*/

CREATE PROCEDURE [dbo].[spCustomerDaftarBSA]
	@NoCustomer int
	,@Nama varchar (100) = ''
	,@NoHp varchar (50) = ''
	,@NoTelp varchar (50) = ''
	,@NoKTP varchar (50) = ''
	,@KTP1 varchar (50) = ''
	,@KTP2 varchar (50) = ''
	,@KTP3 varchar (50) = ''
	,@KTP4 varchar (50) = ''
	,@Korespon1 varchar (50) = ''
	,@Korespon2 varchar (50) = ''
	,@Korespon3 varchar (50) = ''
	,@Korespon4 varchar (50) = ''
	,@RekNama varchar (50) = ''
	,@RekBank varchar (50) = ''
	,@RekCabang varchar (50) = ''
	,@RekNo varchar (50) = ''
AS

INSERT INTO MS_CUSTOMER
(
	 NoCustomer
	,Nama
	,NoTelp
	,NoHp
	,NoKTP
	,KTP1
	,KTP2
	,KTP3
	,KTP4
	,Alamat1
	,Alamat2
	,Alamat3
	,Alamat4
	,RekNama
	,RekBank
	,RekCabang
	,RekNo
)
VALUES
(
	 @NoCustomer
	,@Nama
	,@NoTelp
	,@NoHp
	,@NoKTP
	,@KTP1
	,@KTP2
	,@KTP3
	,@KTP4
	,@Korespon1
	,@Korespon2
	,@Korespon3
	,@Korespon4
	,@RekNama
	,@RekBank
	,@RekCabang
	,@RekNo
)





GO
/****** Object:  StoredProcedure [dbo].[spCustomerDaftarEsales]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Pendaftaran customer baru
*/
CREATE PROCEDURE [dbo].[spCustomerDaftarEsales]
	 @Nama varchar (100) = ''
	,@NoHp varchar (50) = ''
	,@Email varchar (100) = ''	
	,@KTP1 varchar (50) = ''
	,@KTP2 varchar (50) = ''
	,@KTP3 varchar (50) = ''
	,@KTP4 varchar (50) = ''
	,@Agama varchar (50) = ''
	,@KetEsales varchar (MAX) = ''
	,@ProspectID varchar (88) = ''
	,@TglLahir datetime
AS

DECLARE @NoCustomer int
SELECT @NoCustomer = ISNULL(MAX(NoCustomer),0) + 1 FROM MS_CUSTOMER

INSERT INTO MS_CUSTOMER
(
	 NoCustomer
	,Nama
	,NoHp
	,Email
	,KTP1
	,KTP2
	,KTP3
	,KTP4
	,Agama
	,SumberData
	,KetEsales
	,ProspectID
	,TglLahir
)
VALUES
(
	 @NoCustomer
	,@Nama
	,@NoHp
	,@Email
	,@KTP1
	,@KTP2
	,@KTP3
	,@KTP4
	,@Agama
	,'eSales'
	,@KetEsales
	,@ProspectID
	,@TglLahir
)











GO
/****** Object:  StoredProcedure [dbo].[spCustomerDaftarEsales2]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Pendaftaran customer baru
*/
CREATE PROCEDURE [dbo].[spCustomerDaftarEsales2]
	 @Nama varchar (100) = ''
	,@NoHp varchar (50) = ''
	,@Email varchar (100) = ''	
	,@KTP1 varchar (50) = ''
	,@KTP2 varchar (50) = ''
	,@KTP3 varchar (50) = ''
	,@KTP4 varchar (50) = ''
	,@Agama varchar (50) = ''
	,@KetEsales varchar (MAX) = ''
	,@ProspectID varchar (88) = ''
AS

DECLARE @NoCustomer int
SELECT @NoCustomer = ISNULL(MAX(NoCustomer),0) + 1 FROM MS_CUSTOMER

INSERT INTO MS_CUSTOMER
(
	 NoCustomer
	,Nama
	,NoHp
	,Email
	,KTP1
	,KTP2
	,KTP3
	,KTP4
	,Agama
	,SumberData
	,KetEsales
	,ProspectID
)
VALUES
(
	 @NoCustomer
	,@Nama
	,@NoHp
	,@Email
	,@KTP1
	,@KTP2
	,@KTP3
	,@KTP4
	,@Agama
	,'eSales'
	,@KetEsales
	,@ProspectID
)











GO
/****** Object:  StoredProcedure [dbo].[spCustomerDaftarHOLD]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Pendaftaran customer baru
*/

CREATE PROCEDURE [dbo].[spCustomerDaftarHOLD]
	 @Nama varchar (100) = ''	
	,@NoTelp varchar (50) = ''
	,@NoHp varchar (50) = ''	
	,@NoKTP varchar (50) = ''
	,@KTP1 varchar (150) = ''
	,@KTP2 varchar (50) = ''
	,@KTP3 varchar (50) = ''
	,@KTP4 varchar (50) = ''	
	,@TipeCs varchar(20) = ''	
	,@wn varchar (50) = ''
	,@Status varchar (1) = ''
AS

DECLARE @NoCustomer int
SELECT @NoCustomer = ISNULL(MAX(NoCustomer),0) + 1 FROM MS_CUSTOMER

INSERT INTO MS_CUSTOMER
(
	 NoCustomer
	,Nama	
	,NoTelp
	,NoHp	
	,NoKTP
	,KTP1
	,KTP2
	,KTP3
	,KTP4	
	,TipeCs	
	,Kewarganegaraan
	,Status
)
VALUES
(
	 @NoCustomer
	,@Nama	
	,@NoTelp
	,@NoHp	
	,@NoKTP
	,@KTP1
	,@KTP2
	,@KTP3
	,@KTP4	
	,@TipeCs	
	,@wn
	,@Status
)











GO
/****** Object:  StoredProcedure [dbo].[spCustomerDaftarNUPUpload]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Pendaftaran customer baru
*/

CREATE PROCEDURE [dbo].[spCustomerDaftarNUPUpload]
	 @NoNUP varchar(20)
	 ,@NoCustomer int
	,@NoAgent int	
	,@Nama varchar (100) = ''
	,@NoHp varchar (50) = ''
	,@NoTelp varchar (50) = ''
	,@Email varchar (100) = ''
	,@TglLahir datetime
	,@NoKTP varchar (50) = ''
	,@NPWP varchar (100) = ''
	,@KTP1 varchar (50) = ''
	,@KTP2 varchar (50) = ''
	,@KTP3 varchar (50) = ''
	,@KTP4 varchar (50) = ''
	,@Korespon1 varchar (50) = ''
	,@Korespon2 varchar (50) = ''
	,@Korespon3 varchar (50) = ''
	,@Korespon4 varchar (50) = ''
	,@RekNama varchar (50) = ''
	,@RekBank varchar (50) = ''
	,@RekCabang varchar (50) = ''
	,@RekNo varchar (50) = ''
	,@Jenis varchar(50)
	,@Project varchar(20)
	,@SumberData varchar(50)
AS

INSERT INTO MS_CUSTOMER
(
	 NoCustomer
	,Nama
	,NoTelp
	,NoHp
	,Email
	,TglLahir
	,NoKTP
	,NPWP
	,KTP1
	,KTP2
	,KTP3
	,KTP4
	,Alamat1
	,Alamat2
	,Alamat3
	,Alamat4
	,RekNama
	,RekBank
	,RekCabang
	,RekNo
	,NoNUP
	,SumberData
	,Project
)
VALUES
(
	 @NoCustomer
	,@Nama
	,@NoTelp
	,@NoHp
	,@Email
	,CONVERT(datetime, @TglLahir, 101)
	,@NoKTP
	,@NPWP
	,@KTP1
	,@KTP2
	,@KTP3
	,@KTP4
	,@Korespon1
	,@Korespon2
	,@Korespon3
	,@Korespon4
	,@RekNama
	,@RekBank
	,@RekCabang
	,@RekNo
	,@NoNUP
	,@SumberData
	,@Project
)

EXEC [spNUPDaftar] @NoNUP,@NoCustomer,@NoAgent,@Jenis,@Project

GO
/****** Object:  StoredProcedure [dbo].[spCustomerDel]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Hapus customer PERMANEN
*/

CREATE PROCEDURE [dbo].[spCustomerDel]
	@NoCustomer int
AS

-- Customer tidak boleh dihapus jika sudah pernah melakukan kontrak
IF EXISTS (SELECT NoCustomer FROM MS_RESERVASI WHERE NoCustomer = @NoCustomer)
	RETURN
IF EXISTS (SELECT NoCustomer FROM MS_KONTRAK WHERE NoCustomer = @NoCustomer)
	RETURN

DELETE FROM MS_CUSTOMER WHERE NoCustomer = @NoCustomer









GO
/****** Object:  StoredProcedure [dbo].[spCustomerEdit]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Edit data customer
*/

CREATE PROCEDURE [dbo].[spCustomerEdit]
	 @NoCustomer int
	,@Nama varchar (100) = ''
	,@NamaBisnis varchar (100) = ''
	,@NoTelp varchar (50) = ''
	,@NoHp varchar (50) = ''
	,@NoKantor varchar (50) = ''
	,@NoFax varchar (50) = ''
	,@Email varchar (100) = ''
	,@NoKTP varchar (50) = ''
	,@KTP1 varchar (50) = ''
	,@KTP2 varchar (50) = ''
	,@KTP3 varchar (50) = ''
	,@KTP4 varchar (50) = ''
	,@Alamat1 varchar (50) = ''
	,@Alamat2 varchar (50) = ''
	,@Alamat3 varchar (50) = ''
	,@Kantor1 varchar (50) = ''
	,@Kantor2 varchar (50) = ''
	,@Kantor3 varchar (50) = ''
	,@Agama varchar (50) = ''
	--,@TglLahir datetime
	,@Status varchar (1) = ''
	,@JenisBisnis varchar(100) = ''
	,@MerekBisnis varchar(100) = ''
	,@UnitLama varchar(20) = ''
	,@LuasLama money = 0
	,@TokoLama varchar(100) = ''
	,@ZoningLama varchar(100) = ''
	,@GedungLama varchar(100) = ''
	,@TeleponLama varchar(100) = ''
	,@AkteLama varchar(100) = ''
	,@TipeCs varchar(20) = ''
	,@Salutation varchar(50) = ''
	,@NPWP varchar(100) = ''
	,@Refferator bit = false
	,@Kodepos varchar(6) = ''
AS

UPDATE MS_CUSTOMER
SET
	 Nama = @Nama
	,NamaBisnis = @NamaBisnis
	,NoTelp = @NoTelp
	,NoHp = @NoHp
	,NoKantor = @NoKantor
	,NoFax = @NoFax
	,Email = @Email
	,NoKTP = @NoKTP
	,KTP1 = @KTP1
	,KTP2 = @KTP2
	,KTP3 = @KTP3
	,KTP4 = @KTP4
	,Alamat1 = @Alamat1
	,Alamat2 = @Alamat2
	,Alamat3 = @Alamat3
	,Kantor1 = @Kantor1
	,Kantor2 = @Kantor2
	,Kantor3 = @Kantor3
	,Agama = @Agama
	--,TglLahir = CONVERT(datetime, @TglLahir, 101)
	,Status = @Status
	,JenisBisnis = @JenisBisnis
	,MerekBisnis = @MerekBisnis
	,UnitLama = @UnitLama
	,LuasLama = @LuasLama
	,TokoLama = @TokoLama
	,ZoningLama = @ZoningLama
	,GedungLama = @GedungLama
	,TeleponLama = @TeleponLama
	,AkteLama = @AkteLama
	,TipeCs = @TipeCs
	,Salutation = @Salutation
	,NPWP = @NPWP
	,Refferator = @Refferator
	,TglEdit = GETDATE()
	,Kodepos = @Kodepos
WHERE NoCustomer = @NoCustomer









GO
/****** Object:  StoredProcedure [dbo].[spCustomerGabung]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Prosedur gabung nomor customer
Dua customer di-merge jadi satu
*/

CREATE PROCEDURE [dbo].[spCustomerGabung]
	 @NoCustomerSimpan int
	,@NoCustomerBuang int
AS

UPDATE MS_KONTRAK SET
	NoCustomer = @NoCustomerSimpan
WHERE
	NoCustomer = @NoCustomerBuang
	
UPDATE MS_RESERVASI SET
	NoCustomer = @NoCustomerSimpan
WHERE
	NoCustomer = @NoCustomerBuang

-- Hapus customer
DELETE FROM MS_CUSTOMER WHERE NoCustomer = @NoCustomerBuang









GO
/****** Object:  StoredProcedure [dbo].[spDenda]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Rolling 0,4 per mil per hari
Catatan : Proporsional terhadap pembayaran partial
*/

CREATE PROCEDURE [dbo].[spDenda]
AS

UPDATE MS_TAGIHAN SET Denda = 0

DECLARE @NoKontrak varchar(50)
DECLARE @NoUrut int
DECLARE @TglJT datetime
DECLARE @SisaTagihan money
DECLARE @Telat int
DECLARE @PutihDenda bit

DECLARE rs CURSOR FOR
	SELECT
		 NoKontrak
		,NoUrut
		,TglJT
		,NilaiTagihan -
		 (	SELECT ISNULL(SUM(NilaiPelunasan),0) FROM MS_PELUNASAN
			WHERE NoKontrak = MS_TAGIHAN.NoKontrak AND NoTagihan = MS_TAGIHAN.NoUrut AND MS_PELUNASAN.SudahCair=1)
		 AS SisaTagihan
		,DATEDIFF(d, TglJT, GETDATE()) AS Telat
		,PutihDenda
	FROM MS_TAGIHAN
	--WHERE TglJT >= '2011-08-01'
OPEN rs

FETCH NEXT FROM rs INTO @NoKontrak, @NoUrut, @TglJT, @SisaTagihan, @Telat, @PutihDenda

WHILE @@FETCH_STATUS = 0
BEGIN
	DECLARE @Denda money
	SET @Denda = 0
	IF @Telat > 0 SET @Denda = ROUND((@SisaTagihan * ( ( power(1.001,@Telat) ) - 1) ),0)
	
	DECLARE @NilaiPelunasan money
	DECLARE @TelatLunas int
	DECLARE rsDetil CURSOR FOR
		SELECT
			 NilaiPelunasan
			,DATEDIFF(d, @TglJT, TglPelunasan) AS TelatLunas
		FROM MS_PELUNASAN WHERE NoKontrak = @NoKontrak AND NoTagihan = @NoUrut AND SudahCair=1
	OPEN rsDetil
	
	FETCH NEXT FROM rsDetil INTO @NilaiPelunasan, @TelatLunas
	WHILE @@FETCH_STATUS = 0
	BEGIN
		IF (@TelatLunas > 0)
			SET @Denda = ROUND((@NilaiPelunasan * (  ( power(1.001,@TelatLunas) ) - 1) ),0)
		
		FETCH NEXT FROM rsDetil INTO @NilaiPelunasan, @TelatLunas
	END
	
	CLOSE rsDetil
	DEALLOCATE rsDetil
	
	if @PutihDenda = 1
		SET @denda = 0
	
	UPDATE MS_TAGIHAN SET
		Denda = @Denda
	WHERE
	NoKontrak = @NoKontrak
	AND NoUrut = @NoUrut
	
	FETCH NEXT FROM rs INTO @NoKontrak, @NoUrut, @TglJT, @SisaTagihan, @Telat, @PutihDenda
END

CLOSE rs
DEALLOCATE rs









GO
/****** Object:  StoredProcedure [dbo].[spFollowUp]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/*
Pendaftaran satu buah tagihan baru
*/

CREATE PROCEDURE [dbo].[spFollowUp]
	 @Ref varchar (50)
	,@TglFU datetime
	,@Grouping varchar (50)
	,@Ket varchar (MAX)
	,@NoTelp varchar (50)
	,@Alamat varchar (MAX)
	,@TglInput datetime
AS

DECLARE @NoFU int
SELECT @NoFU = ISNULL(MAX(NoFU),0) + 1 FROM MS_FOLLOWUP
WHERE NoKontrak = @Ref

INSERT INTO MS_FOLLOWUP
(
	 NoFU
	,NoKontrak
	,TglFU
	,NamaGrouping
	,Ket	
	,NoHP
	,Alamat
	,TglInput	
)
VALUES
(	
	 @NoFU	
	,@Ref
	,CONVERT(datetime, @TglFU, 101)
	,@Grouping
	,@Ket
	,@NoTelp
	,@Alamat
	,CONVERT(datetime, @TglInput, 101)
)






GO
/****** Object:  StoredProcedure [dbo].[spFollowUpDetil]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/*
Pendaftaran satu buah tagihan baru
*/

CREATE PROCEDURE [dbo].[spFollowUpDetil]
	 @NoFU int
	,@TglJT datetime
	,@NamaTagihan varchar (50)
	,@NoTagihan int
	,@Nilai money
	,@Tipe varchar (50)
	,@TglJanji datetime
	,@Ref varchar (50)
AS

DECLARE @NoUrut int
SELECT @NoUrut = ISNULL(MAX(NoUrut),0) + 1 FROM MS_FOLLOWUP_DETIL
WHERE NoFU = @NoFU

INSERT INTO MS_FOLLOWUP_DETIL
(
	 NoFU
	,NoUrut
	,TglJT
	,NamaTagihan
	,NoTagihan
	,Nilai
	,Tipe
	,TglJanjiBayar
	,NoKontrak
)
VALUES
(	
	 @NoFU	
	,@NoUrut
	,CONVERT(datetime, @TglJT, 101)
	,@NamaTagihan
	,@NoTagihan
	,@Nilai
	,@Tipe
	,CONVERT(datetime, @TglJanji, 101)
	,@Ref
)






GO
/****** Object:  StoredProcedure [dbo].[spFPDel]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Hapus floor plan
*/

CREATE PROCEDURE [dbo].[spFPDel]
	 @Peta varchar (100) = ''
AS

UPDATE MS_UNIT SET
	 Peta = ''
	,Koordinat = ''
WHERE Peta = @Peta









GO
/****** Object:  StoredProcedure [dbo].[spFUDel]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/*
Hapus agent PERMANEN
*/

CREATE PROCEDURE [dbo].[spFUDel]
	@NoFU int
	,@NoKontrak varchar(50)
AS

DELETE FROM MS_FOLLOWUP WHERE NoFU = @NoFU AND NoKontrak = @NoKontrak






GO
/****** Object:  StoredProcedure [dbo].[spFUDelDetil]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/*
Hapus agent PERMANEN
*/

CREATE PROCEDURE [dbo].[spFUDelDetil]
	@NoFU int
	,@NoKontrak varchar(50)
AS

DELETE FROM MS_FOLLOWUP_DETIL WHERE NoFU = @NoFU AND NoKontrak = @NoKontrak






GO
/****** Object:  StoredProcedure [dbo].[spHoldDaftar]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Pendaftaran hold baru
*/

CREATE PROCEDURE [dbo].[spHoldDaftar]
	 @NoHold varchar (20) = ''
	 ,@TglHold Datetime
	 ,@TglHoldExpired Datetime
	 ,@NoStock varchar(20) = ''
	 ,@NoCustomer int
	 ,@NoAgent int
AS


INSERT INTO MS_HOLD
(
	 NoHold 
	 ,TglHold 
	 ,TglHoldExpired 
	 ,NoStock
	 ,NoCustomer 
	 ,NoAgent 
)
VALUES
(
	 @NoHold 
	 ,@TglHold 
	 ,@TglHoldExpired 
	 ,@NoStock
	 ,@NoCustomer 
	 ,@NoAgent 
)











GO
/****** Object:  StoredProcedure [dbo].[spInsertNUPPelunasan]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Insert Data Pelunasan NUP
*/

CREATE PROCEDURE [dbo].[spInsertNUPPelunasan]
	 @NoNUP varchar(20)
	,@TglBayar datetime
	,@NilaiBayar money
	,@CaraBayar varchar(10)
	,@Ket varchar(200)
	,@NoTTS varchar(20)
	,@PelunasanKe int
	,@NoRek varchar(25)
	,@Tipe varchar(50)
	,@Project varchar(20)
AS

INSERT INTO MS_NUP_PELUNASAN
(
	NoNUP
	,TglBayar
	,NilaiBayar
	,CaraBayar
	,Keterangan
	,NoTTS
	,PelunasanKe
	,RekBank
	,Tipe
	,Project
)
VALUES
(
	 @NoNUP
	,@TglBayar
	,@NilaiBayar
	,@CaraBayar
	,@Ket
	,@NoTTS
	,@PelunasanKe
	,@NoRek
	,@Tipe
	,@Project
)





GO
/****** Object:  StoredProcedure [dbo].[spJurnalCustomer]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Jurnal Customer
*/

CREATE PROCEDURE [dbo].[spJurnalCustomer]
	 @UserID varchar(20) = ''
	,@NoCustomer int
	,@Ket text = ''
AS

INSERT INTO MS_CUSTOMER_JURNAL
(
	 UserID
	,NoCustomer
	,Ket 
)
VALUES
(
	 @UserID
	,@NoCustomer
	,@Ket 
)









GO
/****** Object:  StoredProcedure [dbo].[spJurnalKontrak]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Jurnal Kontrak
*/

CREATE PROCEDURE [dbo].[spJurnalKontrak]
	 @UserID varchar(20) = ''
	,@NoKontrak varchar(50)
	,@Ket text = ''
AS

INSERT INTO MS_KONTRAK_JURNAL
(
	 UserID
	,NoKontrak
	,Ket 
)
VALUES
(
	 @UserID
	,@NoKontrak
	,@Ket 
)









GO
/****** Object:  StoredProcedure [dbo].[spJurnalReservasi]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Jurnal Reservasi
*/

CREATE PROCEDURE [dbo].[spJurnalReservasi]
	 @UserID varchar(20) = ''
	,@NoReservasi int
	,@Ket text = ''
AS

INSERT INTO MS_RESERVASI_JURNAL
(
	 UserID
	,NoReservasi
	,Ket 
)
VALUES
(
	 @UserID
	,@NoReservasi
	,@Ket 
)









GO
/****** Object:  StoredProcedure [dbo].[spKomisiCFDaftar]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Penambahan closing fee
*/

CREATE PROCEDURE [dbo].[spKomisiCFDaftar]
	 @NoCF varchar(88)
	 ,@Tgl datetime
	 ,@NoSkema int
	 ,@NamaSkema varchar(100)
	 ,@NoKontrak varchar(100)
	 ,@NoAgent int
	 ,@NamaAgent varchar(100)
	 ,@NoCustomer int
	 ,@NamaCust varchar(100)
	 ,@NoUnit varchar(100)
AS

INSERT INTO MS_KOMISI_CF
(
	 NoCF
	,Tgl
	,NoSkema
	,NamaSkema
	,NoKontrak
	,NoAgent
	,NamaAgent
	,NoCustomer
	,NamaCust
	,NoUnit
)
VALUES
(
	 @NoCF
	,@Tgl
	,@NoSkema
	,@NamaSkema
	,@NoKontrak
	,@NoAgent
	,@NamaAgent
	,@NoCustomer
	,@NamaCust
	,@NoUnit
)









GO
/****** Object:  StoredProcedure [dbo].[spKomisiCFDel]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Hapus cf PERMANEN
*/

CREATE PROCEDURE [dbo].[spKomisiCFDel]
	 @NoCF varchar (88)
AS

DELETE FROM MS_KOMISI_CF
WHERE NoCF = @NoCF

-- Flag cf
UPDATE MS_KONTRAK SET CFID = '' WHERE CFID = @NoCF










GO
/****** Object:  StoredProcedure [dbo].[spKomisiCFDetil]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Penambahan closing fee
*/

CREATE PROCEDURE [dbo].[spKomisiCFDetil]
	 @NoCF varchar(88)
	 ,@NoAgent int
	 ,@NamaAgent varchar(100)
	 ,@PotongKomisi bit
	 ,@Nilai money
AS

-- Nomor urut terbesar ditambahkan satu
DECLARE @Baris int
SELECT @Baris = ISNULL(MAX(SN),0) + 1 FROM MS_KOMISI_CF_DETAIL
WHERE NoCF = @NoCF

INSERT INTO MS_KOMISI_CF_DETAIL
(
	 NoCF
	,SN
	,NoAgent
	,NamaAgent
	,PotongKomisi
	,Nilai
)
VALUES
(
	 @NoCF
	,@Baris
	,@NoAgent
	,@NamaAgent
	,@PotongKomisi
	,@Nilai
)









GO
/****** Object:  StoredProcedure [dbo].[spKomisiCFPDaftar]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Penambahan pengajuan pencairan closing fee
*/

CREATE PROCEDURE [dbo].[spKomisiCFPDaftar]
	 @NoCFP varchar(88)
	 ,@Tgl datetime
	 ,@Ket varchar(max)
AS

INSERT INTO MS_KOMISI_CFP
(
	 NoCFP
	,Tgl
	,Ket
)
VALUES
(
	 @NoCFP
	,@Tgl
	,@Ket
)









GO
/****** Object:  StoredProcedure [dbo].[spKomisiCFPDel]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Hapus pengajuan closing fee PERMANEN
*/

CREATE PROCEDURE [dbo].[spKomisiCFPDel]
	@Nomor varchar(88)
AS

DELETE FROM MS_KOMISI_CFP WHERE NoCFP = @Nomor









GO
/****** Object:  StoredProcedure [dbo].[spKomisiCFPDetil]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Penambahan detail pengajuan closing fee
*/

CREATE PROCEDURE [dbo].[spKomisiCFPDetil]
	 @NoCFP varchar(88)
	 ,@NoCF varchar(88)
	 ,@SN_NoCF int
	 ,@Nilai money
	 ,@NoAgent int
	 ,@NamaAgent varchar(200)
AS

-- Nomor urut terbesar ditambahkan satu
DECLARE @Baris int
SELECT @Baris = ISNULL(MAX(SN),0) + 1 FROM MS_KOMISI_CFP_DETAIL
WHERE NoCFP = @NoCFP

INSERT INTO MS_KOMISI_CFP_DETAIL
(
	 NoCFP
	,SN
	,NoCF
	,SN_NoCF
	,Nilai
	,NoAgent
	,NamaAgent
)
VALUES
(
	 @NoCFP
	,@Baris
	,@NoCF
	,@SN_NoCF
	,@Nilai
	,@NoAgent
	,@NamaAgent
)







GO
/****** Object:  StoredProcedure [dbo].[spKomisiCFPEdit]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Edit pengajuan cf
*/

CREATE PROCEDURE [dbo].[spKomisiCFPEdit]
	 @Nomor varchar(88)
	,@Tgl datetime
	,@Ket varchar(max)
AS

UPDATE MS_KOMISI_CFP
SET
	 Tgl = @Tgl
	,Ket = @Ket
	,TglUpdate = getdate()
WHERE NoCFP = @Nomor






GO
/****** Object:  StoredProcedure [dbo].[spKomisiCFRDaftar]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Penambahan realisasi pencairan closing fee
*/

CREATE PROCEDURE [dbo].[spKomisiCFRDaftar]
	 @NoCFR varchar(88)
	 ,@Tgl datetime
	 ,@NoCFP varchar(88)
	 ,@Ket varchar(max)
AS

INSERT INTO MS_KOMISI_CFR
(
	 NoCFR
	,Tgl
	,NoCFP
	,Ket
)
VALUES
(
	 @NoCFR
	,@Tgl
	,@NoCFP
	,@Ket
)









GO
/****** Object:  StoredProcedure [dbo].[spKomisiCFRDel]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Hapus realisasi closing fee PERMANEN
*/

CREATE PROCEDURE [dbo].[spKomisiCFRDel]
	@Nomor varchar(88)
AS

DELETE FROM MS_KOMISI_CFR WHERE NoCFR = @Nomor









GO
/****** Object:  StoredProcedure [dbo].[spKomisiCFRDetil]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Penambahan realisasi closing fee
*/

CREATE PROCEDURE [dbo].[spKomisiCFRDetil]
	 @NoCFR varchar(88)
	 ,@NoCF varchar(88)
	 ,@SN_NoCF int
	 ,@Nilai money
	 ,@NoAgent int
	 ,@NamaAgent varchar(200)
	 ,@NoCFP varchar(88)
	 ,@NilaiPPH money
AS

-- Nomor urut terbesar ditambahkan satu
DECLARE @Baris int
SELECT @Baris = ISNULL(MAX(SN),0) + 1 FROM MS_KOMISI_CFR_DETAIL
WHERE NoCFR = @NoCFR

INSERT INTO MS_KOMISI_CFR_DETAIL
(
	 NoCFR
	,SN
	,NoCF
	,SN_NoCF
	,Nilai
	,NoAgent
	,NamaAgent
	,NoCFP
	,NilaiPPH
)
VALUES
(
	 @NoCFR
	,@Baris
	,@NoCF
	,@SN_NoCF
	,@Nilai
	,@NoAgent
	,@NamaAgent
	,@NoCFP
	,@NilaiPPH
)







GO
/****** Object:  StoredProcedure [dbo].[spKomisiCFREdit]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Edit realisasi cf
*/

CREATE PROCEDURE [dbo].[spKomisiCFREdit]
	 @Nomor varchar(88)
	,@Tgl datetime
	,@Ket varchar(max)
AS

UPDATE MS_KOMISI_CFR
SET
	 Tgl = @Tgl
	,Ket = @Ket
	,TglUpdate = getdate()
WHERE NoCFR = @Nomor






GO
/****** Object:  StoredProcedure [dbo].[spKomisiDaftar]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Penambahan komisi
*/

CREATE PROCEDURE [dbo].[spKomisiDaftar]
	 @NoKomisi varchar(88)
	 ,@Tgl datetime
	 ,@NoSkema int
	 ,@NamaSkema varchar(100)
	 ,@NoTermin int
	 ,@NamaTermin varchar(100)
	 ,@CaraBayar varchar(100)
	 ,@NoKontrak varchar(100)
	 ,@NoAgent int
	 ,@NamaAgent varchar(100)
	 ,@NoCustomer int
	 ,@NamaCust varchar(100)
	 ,@NoUnit varchar(100)
AS

INSERT INTO MS_KOMISI
(
	 NoKomisi
	,Tgl
	,NoSkema
	,NamaSkema
	,NoTermin
	,NamaTermin
	,CaraBayar
	,NoKontrak
	,NoAgent
	,NamaAgent
	,NoCust
	,NamaCust
	,NoUnit
)
VALUES
(
	 @NoKomisi
	,@Tgl
	,@NoSkema
	,@NamaSkema
	,@NoTermin
	,@NamaTermin
	,@CaraBayar
	,@NoKontrak
	,@NoAgent
	,@NamaAgent
	,@NoCustomer
	,@NamaCust
	,@NoUnit
)









GO
/****** Object:  StoredProcedure [dbo].[spKomisiDel]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Hapus komisi PERMANEN
*/

CREATE PROCEDURE [dbo].[spKomisiDel]
	 @NoKomisi varchar (88)
AS

DELETE FROM MS_KOMISI
WHERE NoKomisi = @NoKomisi

-- Flag cf
UPDATE MS_KONTRAK SET KomisiID = '' WHERE KomisiID = @NoKomisi










GO
/****** Object:  StoredProcedure [dbo].[spKomisiDetil]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Penambahan komisi
*/

CREATE PROCEDURE [dbo].[spKomisiDetil]
	 @NoKomisi varchar(88)
	 ,@NoAgent int
	 ,@NamaAgent varchar(100)
	 ,@Nilai money
AS

-- Nomor urut terbesar ditambahkan satu
DECLARE @Baris int
SELECT @Baris = ISNULL(MAX(SN),0) + 1 FROM MS_KOMISI_DETAIL
WHERE NoKomisi = @NoKomisi

INSERT INTO MS_KOMISI_DETAIL
(
	 NoKomisi
	,SN
	,NoAgent
	,NamaAgent
	,Nilai
)
VALUES
(
	 @NoKomisi
	,@Baris
	,@NoAgent
	,@NamaAgent
	,@Nilai
)









GO
/****** Object:  StoredProcedure [dbo].[spKomisiPDaftar]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Penambahan pengajuan pencairan komisi
*/

CREATE PROCEDURE [dbo].[spKomisiPDaftar]
	 @NoKomisiP varchar(88)
	 ,@Tgl datetime
	 ,@Ket varchar(max)
AS

INSERT INTO MS_KOMISIP
(
	 NoKomisiP
	,Tgl
	,Ket
)
VALUES
(
	 @NoKomisiP
	,@Tgl
	,@Ket
)









GO
/****** Object:  StoredProcedure [dbo].[spKomisiPDel]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Hapus pengajuan komisi fee PERMANEN
*/

CREATE PROCEDURE [dbo].[spKomisiPDel]
	@Nomor varchar(88)
AS

DELETE FROM MS_KOMISIP WHERE NoKomisiP = @Nomor


GO
/****** Object:  StoredProcedure [dbo].[spKomisiPDetil]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Penambahan pengajuan komisi
*/

CREATE PROCEDURE [dbo].[spKomisiPDetil]
	 @NoKomisiP varchar(88)
	 ,@NoKomisi varchar(88)
	 ,@SN_KomisiTermin int
	 ,@Nilai money
AS

-- Nomor urut terbesar ditambahkan satu
DECLARE @Baris int
SELECT @Baris = ISNULL(MAX(SN),0) + 1 FROM MS_KOMISIP_DETAIL
WHERE NoKomisiP = @NoKomisiP

INSERT INTO MS_KOMISIP_DETAIL
(
	 NoKomisiP
	,SN
	,NoKomisi
	,SN_KomisiTermin
	,Nilai
)
VALUES
(
	 @NoKomisiP
	,@Baris
	,@NoKomisi
	,@SN_KomisiTermin
	,@Nilai
)









GO
/****** Object:  StoredProcedure [dbo].[spKomisiPEdit]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Edit pengajuan komisi
*/

CREATE PROCEDURE [dbo].[spKomisiPEdit]
	 @Nomor varchar(88)
	,@Tgl datetime
	,@Ket varchar(max)
AS

UPDATE MS_KOMISIP
SET
	 Tgl = @Tgl
	,Ket = @Ket
	,TglUpdate = getdate()
WHERE NoKomisiP = @Nomor






GO
/****** Object:  StoredProcedure [dbo].[spKomisiRDaftar]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Penambahan realisasi pencairan komisi
*/

CREATE PROCEDURE [dbo].[spKomisiRDaftar]
	 @NoKomisiR varchar(88)
	 ,@Tgl datetime
	 ,@NoKomisiP varchar(88)
	 ,@Ket varchar(max)
AS

INSERT INTO MS_KOMISIR
(
	 NoKomisiR
	,Tgl
	,NoKomisiP
	,Ket
)
VALUES
(
	 @NoKomisiR
	,@Tgl
	,@NoKomisiP
	,@Ket
)









GO
/****** Object:  StoredProcedure [dbo].[spKomisiRDel]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Hapus pengajuan realisasi pengajuan komisi PERMANEN
*/

CREATE PROCEDURE [dbo].[spKomisiRDel]
	@Nomor varchar(88)
AS

DELETE FROM MS_KOMISIR WHERE NoKomisiR = @Nomor


GO
/****** Object:  StoredProcedure [dbo].[spKomisiRDetil]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Penambahan realisasi komisi
*/

CREATE PROCEDURE [dbo].[spKomisiRDetil]
	 @NoKomisiR varchar(88)
	 ,@NoKomisi varchar(88)
	 ,@SN_KomisiTermin int
	 ,@Nilai money
AS

-- Nomor urut terbesar ditambahkan satu
DECLARE @Baris int
SELECT @Baris = ISNULL(MAX(SN),0) + 1 FROM MS_KOMISIR_DETAIL
WHERE NoKomisiR = @NoKomisiR

INSERT INTO MS_KOMISIR_DETAIL
(
	 NoKomisiR
	,SN
	,NoKomisi
	,SN_KomisiTermin
	,Nilai
)
VALUES
(
	 @NoKomisiR
	,@Baris
	,@NoKomisi
	,@SN_KomisiTermin
	,@Nilai
)









GO
/****** Object:  StoredProcedure [dbo].[spKomisiREdit]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Edit realisasi komisi
*/

CREATE PROCEDURE [dbo].[spKomisiREdit]
	 @Nomor varchar(88)
	,@Tgl datetime
	,@Ket varchar(max)
AS

UPDATE MS_KOMISIR
SET
	 Tgl = @Tgl
	,Ket = @Ket
	,TglUpdate = getdate()
WHERE NoKomisiR = @Nomor






GO
/****** Object:  StoredProcedure [dbo].[spKomisiRewardDaftar]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Penambahan reward
*/

CREATE PROCEDURE [dbo].[spKomisiRewardDaftar]
	  @NoReward varchar(88)
	 ,@Tgl datetime
	 ,@NoAgent int
	 ,@NamaAgent varchar(100)
	 ,@NoSkema int
	 ,@NamaSkema varchar(100)
	 ,@Rumus varchar(50)
	 ,@Dari datetime
	 ,@Sampai datetime
	 ,@Reward varchar(100)
AS

INSERT INTO MS_KOMISI_REWARD
(
	 NoReward
	,Tgl
	,NoAgent
	,NamaAgent
	,NoSkema
	,NamaSkema
	,Rumus
	,PeriodeDari
	,PeriodeSampai
	,Reward
)
VALUES
(
	 @NoReward
	,@Tgl
	,@NoAgent
	,@NamaAgent
	,@NoSkema
	,@NamaSkema
	,@Rumus
	,@Dari
	,@Sampai
	,@Reward
)









GO
/****** Object:  StoredProcedure [dbo].[spKomisiRewardDel]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Hapus reward PERMANEN
*/

CREATE PROCEDURE [dbo].[spKomisiRewardDel]
	 @NoReward varchar (88)
AS

DELETE FROM MS_KOMISI_REWARD
WHERE NoReward = @NoReward

-- Flag cf
UPDATE MS_KONTRAK SET RewardID = '' WHERE RewardID = @NoReward










GO
/****** Object:  StoredProcedure [dbo].[spKomisiRewardDetil]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Penambahan reward
*/

CREATE PROCEDURE [dbo].[spKomisiRewardDetil]
	 @NoReward varchar(88)
	 ,@NoKontrak varchar(100)
	 ,@NoUnit varchar(100)
	 ,@NoCust int
	 ,@NamaCust varchar(100)
AS

-- Nomor urut terbesar ditambahkan satu
DECLARE @Baris int
SELECT @Baris = ISNULL(MAX(SN),0) + 1 FROM MS_KOMISI_REWARD_DETAIL
WHERE NoReward = @NoReward

INSERT INTO MS_KOMISI_REWARD_DETAIL
(
	 NoReward
	,SN
	,NoKontrak
	,NoUnit
	,NoCustomer
	,NamaCust
)
VALUES
(
	 @NoReward
	,@Baris
	,@NoKontrak
	,@NoUnit
	,@NoCust
	,@NamaCust
)









GO
/****** Object:  StoredProcedure [dbo].[spKomisiRewardPDaftar]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Penambahan pengajuan pencairan reward
*/

CREATE PROCEDURE [dbo].[spKomisiRewardPDaftar]
	 @NoRP varchar(88)
	 ,@Tgl datetime
	 ,@Ket varchar(max)
AS

INSERT INTO MS_KOMISI_REWARD_P
(
	 NoRP
	,Tgl
	,Ket
)
VALUES
(
	 @NoRP
	,@Tgl
	,@Ket
)









GO
/****** Object:  StoredProcedure [dbo].[spKomisiRewardPDel]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Hapus pengajuan komisi fee PERMANEN
*/

CREATE PROCEDURE [dbo].[spKomisiRewardPDel]
	@Nomor varchar(88)
AS

DELETE FROM MS_KOMISI_REWARD_P WHERE NoRP = @Nomor


GO
/****** Object:  StoredProcedure [dbo].[spKomisiRewardPDetil]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Penambahan pengajuan reward
*/

CREATE PROCEDURE [dbo].[spKomisiRewardPDetil]
	 @NoRP varchar(88)
	 ,@NoReward varchar(88)
	 ,@Reward varchar(100)
AS

-- Nomor urut terbesar ditambahkan satu
DECLARE @Baris int
SELECT @Baris = ISNULL(MAX(SN),0) + 1 FROM MS_KOMISI_REWARD_P_DETAIL
WHERE NoRP = @NoRP

INSERT INTO MS_KOMISI_REWARD_P_DETAIL
(
	 NoRP
	,SN
	,NoReward
	,Reward
)
VALUES
(
	 @NoRP
	,@Baris
	,@NoReward
	,@Reward
)









GO
/****** Object:  StoredProcedure [dbo].[spKomisiRewardPEdit]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Edit pengajuan cf
*/

CREATE PROCEDURE [dbo].[spKomisiRewardPEdit]
	 @Nomor varchar(88)
	,@Tgl datetime
	,@Ket varchar(max)
AS

UPDATE MS_KOMISI_REWARD_P
SET
	 Tgl = @Tgl
	,Ket = @Ket
	,TglUpdate = getdate()
WHERE NoRP = @Nomor






GO
/****** Object:  StoredProcedure [dbo].[spKomisiRewardRDaftar]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Penambahan realisasi pencairan reward
*/

CREATE PROCEDURE [dbo].[spKomisiRewardRDaftar]
	 @NoRR varchar(88)
	 ,@Tgl datetime
	 ,@NoRP varchar(88)
	 ,@Ket varchar(max)
AS

INSERT INTO MS_KOMISI_REWARD_R
(
	 NoRR
	,Tgl
	,NoRP
	,Ket
)
VALUES
(
	 @NoRR
	,@Tgl
	,@NoRP
	,@Ket
)









GO
/****** Object:  StoredProcedure [dbo].[spKomisiRewardRDel]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Hapus pengajuan komisi fee PERMANEN
*/

CREATE PROCEDURE [dbo].[spKomisiRewardRDel]
	@Nomor varchar(88)
AS

DELETE FROM MS_KOMISI_REWARD_R WHERE NoRR = @Nomor


GO
/****** Object:  StoredProcedure [dbo].[spKomisiRewardRDetil]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Penambahan realisasi reward
*/

CREATE PROCEDURE [dbo].[spKomisiRewardRDetil]
	 @NoRR varchar(88)
	 ,@NoReward varchar(88)
	 ,@Reward varchar(100)
AS

-- Nomor urut terbesar ditambahkan satu
DECLARE @Baris int
SELECT @Baris = ISNULL(MAX(SN),0) + 1 FROM MS_KOMISI_REWARD_R_DETAIL
WHERE NoRR = @NoRR

INSERT INTO MS_KOMISI_REWARD_R_DETAIL
(
	 NoRR
	,SN
	,NoReward
	,Reward
)
VALUES
(
	 @NoRR
	,@Baris
	,@NoReward
	,@Reward
)









GO
/****** Object:  StoredProcedure [dbo].[spKomisiRewardREdit]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Edit realisasi reward
*/

CREATE PROCEDURE [dbo].[spKomisiRewardREdit]
	 @Nomor varchar(88)
	,@Tgl datetime
	,@Ket varchar(max)
AS

UPDATE MS_KOMISI_REWARD_R
SET
	 Tgl = @Tgl
	,Ket = @Ket
	,TglUpdate = getdate()
WHERE NoRR = @Nomor






GO
/****** Object:  StoredProcedure [dbo].[spKomisiTerm]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Penambahan termin komisi
*/

CREATE PROCEDURE [dbo].[spKomisiTerm]
	 @NoKomisi varchar(88)
	 ,@NoAgent int
	 ,@NamaAgent varchar(100)
	 ,@Nama varchar(100)
	 ,@PersenCair money
	 ,@NilaiCair money
	 ,@Lunas bit
	 ,@PersenLunas money
	 ,@BF bit
	 ,@PersenBF money
	 ,@DP bit
	 ,@PersenDP money
	 ,@ANG bit
	 ,@PersenANG money
	 ,@PPJB bit
	 ,@AJB bit
	 ,@AKAD bit
	 ,@TipeCair tinyint
AS

-- Nomor urut terbesar ditambahkan satu
DECLARE @Baris int
SELECT @Baris = ISNULL(MAX(SN),0) + 1 FROM MS_KOMISI_TERM
WHERE NoKomisi = @NoKomisi

INSERT INTO MS_KOMISI_TERM
(
	 NoKomisi
	,SN
	,NoAgent
	,NamaAgent
	,Nama
	,PersenCair
	,NilaiCair
	,Lunas
	,PersenLunas
	,BF
	,PersenBF
	,DP
	,PersenDP
	,ANG
	,PersenANG
	,PPJB
	,AJB
	,AKAD
	,TipeCair
)
VALUES
(
	 @NoKomisi
	,@Baris
	,@NoAgent
	,@NamaAgent
	,@Nama
	,@PersenCair
	,@NilaiCair
	,@Lunas
	,@PersenLunas
	,@BF
	,@PersenBF
	,@DP
	,@PersenDP
	,@ANG
	,@PersenANG
	,@PPJB
	,@AJB
	,@AKAD
	,@TipeCair
)









GO
/****** Object:  StoredProcedure [dbo].[spKontrakADJTemp]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/*
Prosedur ganti nama customer
*/

CREATE PROCEDURE [dbo].[spKontrakADJTemp]
	 @NoApproval varchar(50)
	,@NoKontrak varchar(50)	
	,@GrossAft money
	,@DPPAft money
	,@PPNAft bit
	,@NilaiPPNAft money
	,@SkemaAft varchar(100)
	,@BungaAft money
	,@DiskonRupiahAft money
	,@DiskonTambahanAft money
	,@NilaiKontrakAft money
	,@TglPengajuan datetime
AS

DECLARE 
	 @GrossBfr money
	,@DPPBfr money
	,@PPNBfr bit
	,@NilaiPPNBfr money
	,@SkemaBfr varchar(100)
	,@BungaBfr money
	,@DiskonRupiahBfr money
	,@DiskonTambahanBfr money
	,@NilaiKontrakBfr money

SELECT 
	 @GrossBfr = Gross
	,@DPPBfr = NilaiDPP
	,@PPNBfr = PPN
	,@NilaiPPNBfr = NilaiPPN
	,@SkemaBfr = Skema
	,@BungaBfr = BungaNominal
	,@DiskonRupiahBfr = DiskonRupiah
	,@DiskonTambahanBfr = DiskonTambahan
	,@NilaiKontrakBfr = NilaiKontrak
FROM MS_KONTRAK WHERE NoKontrak = @NoKontrak

INSERT INTO MS_APPROVAL_ADJUSMENT
(
	 NoApproval
	,NoKontrak
	,GrossAft
	,DPPAft
	,PPNAft
	,NilaiPPNAft
	,SkemaAft
	,BungaAft
	,DiskonRupiahAft
	,DiskonTambahanAft
	,NilaiKontrakAft
	,TglPengajuan
	,GrossBfr
	,DPPBfr
	,PPNBfr
	,NilaiPPNBfr
	,SkemaBfr
	,BungaBfr
	,DiskonRupiahBfr
	,DiskonTambahanBfr
	,NilaiKontrakBfr
)
VALUES
(
	 @NoApproval
	,@NoKontrak
	,@GrossAft
	,@DPPAft
	,@PPNAft
	,@NilaiPPNAft
	,@SkemaAft
	,@BungaAft
	,@DiskonRupiahAft
	,@DiskonTambahanAft
	,@NilaiKontrakAft
	,@TglPengajuan
	,@GrossBfr
	,@DPPBfr
	,@PPNBfr
	,@NilaiPPNBfr
	,@SkemaBfr
	,@BungaBfr
	,@DiskonRupiahBfr
	,@DiskonTambahanBfr
	,@NilaiKontrakBfr
)


GO
/****** Object:  StoredProcedure [dbo].[spKontrakAJB]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Prosedur AJB
*/

CREATE PROCEDURE [dbo].[spKontrakAJB]
	 @NoKontrak varchar(50)
	,@NoAJB varchar(20) = ''
	,@TglAJB datetime
AS

UPDATE MS_KONTRAK SET
	  TglAJB = CONVERT(datetime, @TglAJB, 101)
	 ,NoAJB = @NoAJB
	 ,AJB = 'D'
WHERE NoKontrak = @NoKontrak









GO
/****** Object:  StoredProcedure [dbo].[spKontrakApprov]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/*
Pendaftaran kontrak baru
*/

CREATE PROCEDURE [dbo].[spKontrakApprov]
	 @NoKontrak varchar(50)
	,@NoStock varchar(20)
	,@TglKontrak datetime
	,@Skema varchar(150)
	,@TargetST datetime
	,@NoCustomer int
	,@NoAgent int
	,@PriceList money
AS

-- Validasi unit dahulu
DECLARE @Validasi varchar(3)
EXEC spValidasiDaftar
	 @NoStock
	,@Validasi OUTPUT
IF @Validasi <> 'OK'
	RETURN

DECLARE
	 @Jenis varchar(20)
	,@Lokasi varchar(20)
	,@NoUnit varchar(20)
	,@Luas money
SELECT
	 @Jenis = Jenis
	,@Lokasi = Lokasi
	,@NoUnit = NoUnit
	,@Luas = Luas
FROM MS_UNIT 
WHERE NoStock = @NoStock

INSERT INTO MS_KONTRAK_Approval
(
	 NoKontrak
	,NoStock
	,NoCustomer
	,NoAgent
	,TglKontrak
	,Jenis
	,Lokasi
	,NoUnit
	,Luas
	,Gross
	,NilaiKontrak
	,Skema
	,TargetST
)
VALUES
(
	 @NoKontrak
	,@NoStock
	,@NoCustomer
	,@NoAgent
	,CONVERT(datetime, @TglKontrak, 101)
	,@Jenis
	,@Lokasi
	,@NoUnit
	,@Luas
	,@PriceList
	,@PriceList
	,@Skema
	,CONVERT(datetime, @TargetST, 101)
)









GO
/****** Object:  StoredProcedure [dbo].[spKontrakApprovDetail]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create procedure [dbo].[spKontrakApprovDetail]
	@Nokontrak varchar(50)
	,@SN int
	,@stock varchar(20)
	,@cs int
	,@jab varchar(50)
	,@nama varchar(150)
AS

INSERT INTO MS_KONTRAK_APPROVAL_DETAIL
(
	Nokontrak
	,SN
	,NoStock
	,NoCustomer
	,TitleJabatan
	,Nama
)
values
(
	@Nokontrak
	,@SN
	,@stock
	,@cs
	,@jab
	,@nama
)








GO
/****** Object:  StoredProcedure [dbo].[spKontrakBatal]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Prosedur pembatalan kontrak
*/

CREATE PROCEDURE [dbo].[spKontrakBatal]
	 @NoKontrak varchar(50)
AS

UPDATE MS_KONTRAK SET
	 Status = 'B'
	 , ApprovalBatal = 0
WHERE NoKontrak = @NoKontrak

-- Status Unit
DECLARE @NoStock varchar(20)
SELECT @NoStock = NoStock FROM MS_KONTRAK WHERE NoKontrak = @NoKontrak
UPDATE MS_UNIT SET Status = 'A' WHERE NoStock = @NoStock









GO
/****** Object:  StoredProcedure [dbo].[spKontrakBatalTemp]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/*
Prosedur pembatalan kontrak
*/

CREATE PROCEDURE [dbo].[spKontrakBatalTemp]
	 @NoApproval varchar(20)
	 ,@NoKontrak varchar(50)
	 ,@TglPengajuan datetime
	 ,@TglPengembalian datetime
	 ,@AlasanBatal varchar(50)
	 ,@TotalPelunasan money
	 ,@NilaiPengembalian money
	 ,@NilaiKlaim money
	 ,@Keterangan varchar(max)
AS

--DECLARE
--	@Jml int

---- Periksa NoStock di TempGU
--SELECT @Jml = COUNT(NoKontrak) FROM MS_APPROVAL_BATAL 
--WHERE NoKontrak = @NoKontrak
--AND NoApproval NOT IN (SELECT NoApproval FROM MS_APPROVAL WHERE Status <> 'DONE' AND Sumber = 'BATAL')

--if(@Jml > 0)
--	RETURN 

INSERT INTO MS_APPROVAL_BATAL
(
	 NoApproval
	,NoKontrak
	,TglPengajuan
	,TglPengembalian
	,AlasanBatal
	,TotalPelunasan
	,NilaiPengembalian
	,NilaiKlaim
	,Keterangan
)
VALUES
(
	 @NoApproval
	,@NoKontrak
	,@TglPengajuan
	,@TglPengembalian
	,@AlasanBatal
	,@TotalPelunasan
	,@NilaiPengembalian
	,@NilaiKlaim
	,@Keterangan
)









GO
/****** Object:  StoredProcedure [dbo].[spKontrakBunga]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Proses merubah NETTO dan DISCOUNT
*/

CREATE PROCEDURE [dbo].[spKontrakBunga]
	 @NoKontrak varchar(50)
	,@Gross money
	,@NilaiKontrak money
	,@BungaPersen varchar(100)
	
AS

DECLARE	
	@Status varchar(1)
	
SELECT
	@Status = Status
	
FROM MS_KONTRAK
WHERE NoKontrak = @NoKontrak

-- validasi status
IF @Status <> 'A'
	RETURN
	
DECLARE @BungaNominal money
SET @BungaNominal = ROUND((@Gross * @BungaPersen)/100,0)

--SET @NilaiKontrak = @Gross + @surcharg - @disrupiah + @BungaNominal - @Disc + @NilaiPPn
--set @DPP = ( @Gross + @surcharg - @disrupiah + @BungaNominal - @Disc ) / 1.1
--set @PPn = @DPP * 0.1


UPDATE MS_KONTRAK
SET 
	 NilaiKontrak = @NilaiKontrak
	,BungaPersen  = @BungaPersen
	,BungaNominal = @BungaNominal
	,FlagGross = 0
	,TglEdit = GETDATE()
WHERE
NoKontrak = @NoKontrak

-- MS_KONTRAK.OutBalance
EXEC spTagihanBalance @NoKontrak

-- Customer
DECLARE @NoCustomer int
SELECT @NoCustomer = NoCustomer FROM MS_KONTRAK WHERE NoKontrak = @NoKontrak
EXEC spTglTransaksi @NoCustomer




GO
/****** Object:  StoredProcedure [dbo].[spKontrakCustomTemp]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
Prosedur ganti nama customer
*/

CREATE PROCEDURE [dbo].[spKontrakCustomTemp]
	 @NoApproval varchar(50)
	,@NoKontrak varchar(50)	
	,@SkemaBfr varchar(100)
	,@SkemaAft varchar(100)
	,@CaraBayarBfr varchar(50)
	,@CaraBayarAft varchar(50)
	,@TglPengajuan datetime	
AS

-- validasi status
DECLARE 
	 @Status varchar(1)	

SELECT @Status = Status FROM MS_KONTRAK WHERE NoKontrak = @NoKontrak

IF @Status <> 'A'
	RETURN
	

INSERT INTO MS_APPROVAL_CUSTOMIZE
(
	 NoApproval
	,NoKontrak
	,CaraBayarBfr
	,CaraBayarAft
	,SkemaBfr
	,SkemaAft
	,TglPengajuan
)
VALUES
(
	 @NoApproval
	,@NoKontrak
	,@CaraBayarBfr
	,@CaraBayarAft
	,@SkemaBfr
	,@SkemaAft
	,@TglPengajuan
)

GO
/****** Object:  StoredProcedure [dbo].[spKontrakDaftar]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Pendaftaran kontrak baru
*/

CREATE PROCEDURE [dbo].[spKontrakDaftar]
	 @NoKontrak varchar(50)
	,@NoStock varchar(20)
	,@TglKontrak datetime
	,@Skema varchar(150)
	,@TargetST datetime
	,@PriceList money
AS

-- Validasi unit dahulu
DECLARE @Validasi varchar(3)
EXEC spValidasiDaftar
	 @NoStock
	,@Validasi OUTPUT
IF @Validasi <> 'OK'
	RETURN

DECLARE
	 @Jenis varchar(20)
	,@Lokasi varchar(20)
	,@NoUnit varchar(20)
	,@Luas money
	--,@PriceList money

SELECT
	 @Jenis = Jenis
	,@Lokasi = Lokasi
	,@NoUnit = NoUnit
	,@Luas = Luas
	--,@PriceList = PriceList
FROM MS_UNIT 
WHERE NoStock = @NoStock

-- Sistem connect dengan top reservasi
DECLARE
	 @NoReservasi int
	,@NoCustomer int
	,@NoAgent int
SELECT
	 @NoCustomer = NoCustomer
	,@NoAgent = NoAgent
	,@NoReservasi = NoReservasi
FROM MS_RESERVASI
WHERE NoStock = @NoStock AND NoUrut = 1

INSERT INTO MS_KONTRAK
(
	 NoKontrak
	,NoStock
	,NoCustomer
	,NoAgent
	,TglKontrak
	,Jenis
	,Lokasi
	,NoUnit
	,Luas
	,Gross
	,NilaiKontrak
	,Skema
	,TargetST
)
VALUES
(
	 @NoKontrak
	,@NoStock
	,@NoCustomer
	,@NoAgent
	,CONVERT(datetime, @TglKontrak, 101)
	,@Jenis
	,@Lokasi
	,@NoUnit
	,@Luas
	,@PriceList
	,@PriceList
	,@Skema
	,CONVERT(datetime, @TargetST, 101)
)

-- jurnal marketing
INSERT INTO MS_KONTRAK_JURNAL
(
	 Tgl
	,UserID
	,NoKontrak
	,Ket
)
SELECT
	 Tgl
	,UserID
	,@NoKontrak
	,Ket
FROM MS_RESERVASI_JURNAL
WHERE NoReservasi = @NoReservasi

-- MS_KONTRAK.OutBalance
EXEC spReservasiObs @NoStock, @NoReservasi, @NoKontrak

-- MS_KONTRAK.OutBalance
EXEC spTagihanBalance @NoKontrak

-- Update tanggal transaksi di tabel customer
EXEC spTglTransaksi @NoCustomer

-- Status Unit
UPDATE MS_UNIT SET Status = 'B' WHERE NoStock = @NoStock









GO
/****** Object:  StoredProcedure [dbo].[spKontrakDaftar2]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spKontrakDaftar2]
	 @NoKontrak VARCHAR(50)
AS
--IF EXISTS(SELECT * FROM ISC064a_MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak  = @NoKontrak)
--	DELETE ISC064a_MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = @NoKontrak
--INSERT ISC064a_MARKETINGJUAL..MS_KONTRAK 
--SELECT * FROM ISC064_MARKETINGJUAL..MS_KONTRAK 
--WHERE ISC064_MARKETINGJUAL..MS_KONTRAK.NoKontrak  = @NoKontrak
--SET QUOTED_IDENTIFIER OFF









GO
/****** Object:  StoredProcedure [dbo].[spKontrakDaftar3]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Pendaftaran kontrak baru
*/

CREATE PROCEDURE [dbo].[spKontrakDaftar3]
	 @NoKontrak varchar(50)
	,@NoStock varchar(20)
	,@TglKontrak datetime
	,@SkemaBayar varchar(150)
	,@CaraBayar varchar(50)
	,@Gross Money
	,@NoCustomer int
	,@NoAgent int
	,@Status varchar(1)
	,@DiskonRupiah money
	,@NilaiKontrak money
	,@JenisPPN varchar(50)
	,@NilaiPPN money
	,@NilaiDPP money
AS

-- Validasi unit dahulu
DECLARE @Validasi varchar(3)
EXEC spValidasiDaftar
	 @NoStock
	,@Validasi OUTPUT
IF @Validasi <> 'OK'
	RETURN

DECLARE
	 @Jenis varchar(20)
	,@Lokasi varchar(20)
	,@NoUnit varchar(20)
	,@Luas money
	,@PriceListStandard money

SELECT
	 @Jenis = Jenis
	,@Lokasi = Lokasi
	,@NoUnit = NoUnit
	,@Luas = Luas
	,@PriceListStandard = PriceList
FROM MS_UNIT 
WHERE NoStock = @NoStock

-- Sistem connect dengan top reservasi
--DECLARE
--	 @NoReservasi int
--	,@NoCustomer int
--	,@NoAgent int
--SELECT
--	 @NoCustomer = NoCustomer
--	,@NoAgent = NoAgent
--	,@NoReservasi = NoReservasi
--FROM MS_RESERVASI
--WHERE NoStock = @NoStock AND NoUrut = 1

INSERT INTO MS_KONTRAK
(
	 NoKontrak
	,NoStock
	,TglKontrak
	,Skema
	,SkemaKomisi
	,Gross
	,NoCustomer
	,NoAgent
	,Status
	,DiskonRupiah
	,NilaiKontrak
	,JenisPPN
	,NilaiPPN
	,NilaiDPP
	,NoUnit
)
VALUES
(
	 @NoKontrak
	,@NoStock
	,CONVERT(datetime, @TglKontrak, 101)
	,@SkemaBayar
	,@CaraBayar
	,@Gross
	,@NoCustomer
	,@NoAgent
	,@Status
	,@DiskonRupiah
	,@NilaiKontrak
	,@JenisPPN
	,@NilaiPPN
	,@NilaiDPP
	,@NoUNit
)

-- jurnal marketing
INSERT INTO MS_KONTRAK_JURNAL
(
	 Tgl
	,UserID
	,NoKontrak
	,Ket
)
--SELECT
--	 Tgl
--	,UserID
--	,@NoKontrak
--	,Ket
--FROM MS_RESERVASI_JURNAL
--WHERE NoReservasi = @NoReservasi

-- MS_KONTRAK.OutBalance
--EXEC spReservasiObs @NoStock, @NoReservasi

-- MS_KONTRAK.OutBalance
EXEC spTagihanBalance @NoKontrak

-- Update tanggal transaksi di tabel customer
EXEC spTglTransaksi @NoCustomer

-- Status Unit
UPDATE MS_UNIT SET Status = 'B' WHERE NoStock = @NoStock









GO
/****** Object:  StoredProcedure [dbo].[spKontrakDaftar4]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Pendaftaran kontrak baru
*/

CREATE PROCEDURE [dbo].[spKontrakDaftar4]
	 @NoKontrak varchar(50)
	,@NoStock varchar(20)
	,@TglKontrak datetime
	,@Skema varchar(150)
	,@TargetST datetime
	,@NoCustomer int
	,@NoAgent int
	,@PriceList money
AS

-- Validasi unit dahulu
DECLARE @Validasi varchar(3)
EXEC spValidasiDaftar
	 @NoStock
	,@Validasi OUTPUT
IF @Validasi <> 'OK'
	RETURN

DECLARE
	 @Jenis varchar(20)
	,@Lokasi varchar(20)
	,@NoUnit varchar(20)
	,@Luas money
	,@PL2 money

SELECT
	 @Jenis = Jenis
	,@Lokasi = Lokasi
	,@NoUnit = NoUnit
	,@Luas = Luas
	,@PL2 = PriceList
FROM MS_UNIT 
WHERE NoStock = @NoStock

INSERT INTO MS_KONTRAK
(
	 NoKontrak
	,NoStock
	,NoCustomer
	,NoAgent
	,TglKontrak
	,Jenis
	,Lokasi
	,NoUnit
	,Luas
	,Gross
	,NilaiKontrak
	,Skema
	,TargetST
)
VALUES
(
	 @NoKontrak
	,@NoStock
	,@NoCustomer
	,@NoAgent
	,CONVERT(datetime, @TglKontrak, 101)
	,@Jenis
	,@Lokasi
	,@NoUnit
	,@Luas
	,@PriceList
	,@PriceList
	,@Skema
	,CONVERT(datetime, @TargetST, 101)
)

-- jurnal marketing
INSERT INTO MS_KONTRAK_JURNAL
(
	 Tgl
	,UserID
	,NoKontrak
	,Ket
)

-- MS_KONTRAK.OutBalance
EXEC spTagihanBalance @NoKontrak

-- Update tanggal transaksi di tabel customer
EXEC spTglTransaksi @NoCustomer

-- Status Unit
UPDATE MS_UNIT SET Status = 'B' WHERE NoStock = @NoStock









GO
/****** Object:  StoredProcedure [dbo].[spKontrakDaftarNUP]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Pendaftaran kontrak baru
*/

CREATE PROCEDURE [dbo].[spKontrakDaftarNUP]
	 @NoKontrak varchar(50)
	,@NoStock varchar(20)
	,@TglKontrak datetime
	,@Skema varchar(150)
	,@TargetST datetime
	,@NoCustomer int
	,@NoAgent int
	,@Pricelist money
	,@NilaiKontrak money
	,@BiayaSurat money
	,@BiayaBPHTB money
AS

-- Validasi unit dahulu
DECLARE @Validasi varchar(3)
EXEC spValidasiDaftar
	 @NoStock
	,@Validasi OUTPUT
IF @Validasi <> 'OK'
	RETURN

DECLARE
	 @Jenis varchar(30)
	,@Lokasi varchar(20)
	,@NoUnit varchar(20)
	,@Luas money

SELECT
	 @Jenis = Jenis
	,@Lokasi = Lokasi
	,@NoUnit = NoUnit
	,@Luas = Luas
FROM MS_UNIT 
WHERE NoStock = @NoStock

-- Sistem connect dengan top reservasi
--DECLARE
--	 @NoReservasi int
--	,@NoCustomer int
--	,@NoAgent int
--SELECT
--	 @NoCustomer = NoCustomer
--	,@NoAgent = NoAgent
--	,@NoReservasi = NoReservasi
--FROM MS_RESERVASI
--WHERE NoStock = @NoStock AND NoUrut = 1

INSERT INTO MS_KONTRAK
(
	 NoKontrak
	,NoStock
	,NoCustomer
	,NoAgent
	,TglKontrak
	,Jenis
	,Lokasi
	,NoUnit
	,Luas
	,Gross
	,NilaiKontrak
	,Skema
	,TargetST
	,TambahanSurat
	,TambahanBPHTB
)
VALUES
(
	 @NoKontrak
	,@NoStock
	,@NoCustomer
	,@NoAgent
	,CONVERT(datetime, @TglKontrak, 101)
	,@Jenis
	,@Lokasi
	,@NoUnit
	,@Luas
	,@PriceList
	,@NilaiKontrak
	,@Skema
	,CONVERT(datetime, @TargetST, 101)
	,@BiayaSurat
	,@BiayaBPHTB
)

-- jurnal marketing
INSERT INTO MS_KONTRAK_JURNAL
(
	 Tgl
	,UserID
	,NoKontrak
	,Ket
)
--SELECT
--	 Tgl
--	,UserID
--	,@NoKontrak
--	,Ket
--FROM MS_RESERVASI_JURNAL
--WHERE NoReservasi = @NoReservasi

-- MS_KONTRAK.OutBalance
--EXEC spReservasiObs @NoStock, @NoReservasi

-- MS_KONTRAK.OutBalance
EXEC spTagihanBalance @NoKontrak

-- Update tanggal transaksi di tabel customer
EXEC spTglTransaksi @NoCustomer

-- Status Unit
UPDATE MS_UNIT SET Status = 'B' WHERE NoStock = @NoStock





GO
/****** Object:  StoredProcedure [dbo].[spKontrakDel]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Hapus kontrak PERMANEN
*/

CREATE PROCEDURE [dbo].[spKontrakDel]
	@NoKontrak varchar(50)
AS

DECLARE @NoStock varchar(20)
SELECT @NoStock = NoStock
FROM MS_KONTRAK WHERE NoKontrak = @NoKontrak 

DELETE FROM MS_KONTRAK WHERE NoKontrak = @NoKontrak

-- update status unit
UPDATE MS_UNIT SET Status = 'A' WHERE NoStock = @NoStock

-- delete tagihan
DELETE FROM MS_TAGIHAN WHERE NoKontrak = @NoKontrak










GO
/****** Object:  StoredProcedure [dbo].[spKontrakDiskon]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Proses merubah NETTO dan DISCOUNT
*/

CREATE PROCEDURE [dbo].[spKontrakDiskon]
	 @NoKontrak varchar(30)
	,@Gross money
	,@NilaiKontrak money
	,@NilaiDiskon money
	,@DiskonPersen varchar(100)
	,@DiskonKet varchar(1000)
AS

DECLARE
	 @NilaiKontrakLama money
	,@Status varchar(1)
	
SELECT
	 @NilaiKontrakLama = NilaiKontrak
	,@Status = Status
FROM MS_KONTRAK
WHERE NoKontrak = @NoKontrak

-- validasi status
IF @Status <> 'A'
	RETURN
	
--DECLARE @DiskonRupiah money
----SET @DiskonRupiah = @Gross - @NilaiKontrak
--SET @DiskonRupiah = (@Gross * @DiskonPersen)/100

UPDATE MS_KONTRAK
SET 
	 NilaiKontrak = @NilaiKontrak
	,DiskonPersen = @DiskonPersen
	,DiskonKet = @DiskonKet
	,DiskonRupiah = @NilaiDiskon
	,FlagGross = 0
	,TglEdit = GETDATE()
WHERE
NoKontrak = @NoKontrak

-- MS_KONTRAK.OutBalance
EXEC spTagihanBalance @NoKontrak

-- Customer
DECLARE @NoCustomer int
SELECT @NoCustomer = NoCustomer FROM MS_KONTRAK WHERE NoKontrak = @NoKontrak
EXEC spTglTransaksi @NoCustomer











GO
/****** Object:  StoredProcedure [dbo].[spKontrakDiskonTemp]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
Prosedur ganti nama customer
*/

CREATE PROCEDURE [dbo].[spKontrakDiskonTemp]
	 @NoApproval varchar(50)
	,@NoKontrak varchar(50)	
	,@NoKontrakAfter varchar(50)	
	,@TglPengajuan datetime
AS

INSERT INTO MS_APPROVAL_DISKON
(
	 NoApproval
	,NoKontrakTemp
	,NoKontrakAfter
	,TglPengajuan
)
VALUES
(
	 @NoApproval
	,@NoKontrak
	,@NoKontrakAfter
	,@TglPengajuan
)

GO
/****** Object:  StoredProcedure [dbo].[spKontrakEdit]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Edit data kontrak
*/

CREATE PROCEDURE [dbo].[spKontrakEdit]
	 @NoKontrak varchar(50)
	,@TglKontrak datetime
	,@NoAgent int
	,@TargetST datetime
AS

UPDATE MS_KONTRAK
SET
	 TglKontrak = CONVERT(datetime, @TglKontrak, 101)
	,NoAgent = @NoAgent
	,TglEdit = GETDATE()
	,TargetST = CONVERT(datetime, @TargetST, 101)
WHERE NoKontrak = @NoKontrak

-- Customer
DECLARE @NoCustomer int
SELECT @NoCustomer = NoCustomer FROM MS_KONTRAK WHERE NoKontrak = @NoKontrak
EXEC spTglTransaksi @NoCustomer









GO
/****** Object:  StoredProcedure [dbo].[spKontrakGantiKey]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Prosedur ganti primary key
*/

CREATE PROCEDURE [dbo].[spKontrakGantiKey]
	 @Lama varchar(50)
	,@Baru varchar(50)
AS

UPDATE MS_KONTRAK SET
	 NoKontrak = @Baru
	,TglEdit = GETDATE()
WHERE
	NoKontrak = @Lama

-- Normalisasi primary key
UPDATE MS_KONTRAK_LOG SET Pk = @Baru WHERE Pk = @Lama

-- Integrate Finance
UPDATE ISC064_FINANCEAR..MS_PJT SET Ref = @Baru WHERE Ref = @Lama AND Tipe = 'JUAL'
UPDATE ISC064_FINANCEAR..MS_TTS SET Ref = @Baru WHERE Ref = @Lama AND Tipe = 'JUAL'
UPDATE ISC064_FINANCEAR..MS_TUNGGAKAN SET Ref = @Baru WHERE Ref = @Lama AND Tipe = 'JUAL'









GO
/****** Object:  StoredProcedure [dbo].[spKontrakGantiNama]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Prosedur ganti nama customer
*/

CREATE PROCEDURE [dbo].[spKontrakGantiNama]
	 @NoKontrak varchar(50)
	,@NoCustomer int
AS

-- validasi status
DECLARE @Status varchar(1)
SELECT @Status = Status
FROM MS_KONTRAK WHERE NoKontrak = @NoKontrak
IF @Status <> 'A'
	RETURN
	
UPDATE MS_KONTRAK
	SET NoCustomer = @NoCustomer
WHERE NoKontrak = @NoKontrak

EXEC spTglTransaksi @NoCustomer









GO
/****** Object:  StoredProcedure [dbo].[spKontrakGantiNamaTemp]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
Prosedur ganti nama customer
*/

CREATE PROCEDURE [dbo].[spKontrakGantiNamaTemp]
	 @NoApproval varchar(50)
	,@NoKontrak varchar(50)	
	,@NoCustomerBaru int
	,@Biaya money	
	,@TglGantiNama datetime
	,@Keterangan varchar(max)
AS

-- validasi status
DECLARE 
	 @Status varchar(1)
	,@NoCustomerLama int

SELECT @Status = Status,@NoCustomerLama = NoCustomer
FROM MS_KONTRAK WHERE NoKontrak = @NoKontrak

IF @Status <> 'A'
	RETURN
	

INSERT INTO MS_APPROVAL_GN
(
	 NoApproval
	,NoKontrak
	,CustomerLama
	,CustomerBaru
	,TglPengajuan
	,BiayaAdmin
	,Keterangan
)
VALUES
(
	 @NoApproval
	,@NoKontrak
	,@NoCustomerLama
	,@NoCustomerBaru
	,@TglGantiNama
	,@Biaya
	,@Keterangan
)

EXEC spTglTransaksi @NoCustomerBaru







GO
/****** Object:  StoredProcedure [dbo].[spKontrakGantiUnit]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Prosedur ganti unit
*/

CREATE PROCEDURE [dbo].[spKontrakGantiUnit]
	 @NoKontrak varchar(50)
	,@NoStock_Baru varchar(20)
AS
	
DECLARE
	 @Validasi varchar(3)
	,@Status varchar(1)
	,@NoStock_Lama varchar(20)
	
SELECT
	@Status = Status
	,@NoStock_Lama = NoStock
FROM MS_KONTRAK
WHERE NoKontrak = @NoKontrak

-- validasi status
IF @Status <> 'A'
	RETURN

EXEC spValidasiDaftar
	 @NoStock_Baru
	,@Validasi OUTPUT
IF @Validasi <> 'OK'
	RETURN

UPDATE MS_KONTRAK
SET
	 NoStock = @NoStock_Baru
WHERE NoKontrak = @NoKontrak

---- Refresh data unit
--EXEC spKontrakRefresh @NoKontrak

UPDATE MS_KONTRAK
SET
	FlagGross = 3
WHERE NoKontrak = @NoKontrak

-- Customer
DECLARE @NoCustomer int
SELECT @NoCustomer = NoCustomer FROM MS_KONTRAK WHERE NoKontrak = @NoKontrak
EXEC spTglTransaksi @NoCustomer

-- Status Unit
UPDATE MS_UNIT SET Status = 'A' WHERE NoStock = @NoStock_Lama
UPDATE MS_UNIT SET Status = 'B' WHERE NoStock = @NoStock_Baru









GO
/****** Object:  StoredProcedure [dbo].[spKontrakGantiUnitTemp]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
Prosedur ganti unit
*/

CREATE PROCEDURE [dbo].[spKontrakGantiUnitTemp]
	 @NoApproval varchar(20)
	,@NoKontrak varchar(50)
	,@NoStockBaru varchar(20)
	,@Biaya money
	,@TglPengajuan datetime
	,@Keterangan varchar(max)
AS
	
DECLARE
	 @Validasi varchar(3)
	,@Status varchar(1)
	,@NoStockLama varchar(20)
	
SELECT
	@Status = Status
	,@NoStockLama = NoStock
FROM MS_KONTRAK
WHERE NoKontrak = @NoKontrak

DECLARE
	@Jml int

-- Periksa NoStock di TempGU
SELECT @Jml = COUNT(UnitBaru) FROM MS_APPROVAL_GU 
WHERE UnitBaru = @NoStockBaru

if(@Jml > 0)
	RETURN 

-- validasi status
IF @Status <> 'A'
	RETURN

EXEC spValidasiDaftar
	 @NoStockBaru
	,@Validasi OUTPUT
IF @Validasi <> 'OK'
	RETURN

INSERT INTO MS_APPROVAL_GU
(
	 NoApproval
	,NoKontrak
	,UnitLama
	,UnitBaru
	,TglPengajuan
	,BiayaAdmin
	,Keterangan
)
VALUES
(
	 @NoApproval
	,@NoKontrak
	,@NoStockLama
	,@NoStockBaru
	,@TglPengajuan
	,@Biaya
	,@Keterangan	
)






GO
/****** Object:  StoredProcedure [dbo].[spKontrakGimmick]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
Pendaftaran Gimmick di closing baru
*/

CREATE PROCEDURE [dbo].[spKontrakGimmick]
	  @NoKontrak varchar (50) = ''
	 ,@SN int
	 ,@ItemID int
	 ,@Nama varchar (150) = ''
	 ,@Tipe varchar (30) = ''
	 ,@Satuan varchar (20) = ''
	 ,@Stock money
	 ,@HargaSatuan money
	 ,@HargaTotal money
AS

INSERT INTO MS_KONTRAK_GIMMICK
(
	 NoKontrak
	,SN
	,ItemID
	,Nama
	,Tipe
	,Satuan
	,Stock
	,HargaSatuan
	,HargaTotal
)
VALUES
(
	 @NoKontrak
	,@SN
	,@ItemID
	,@Nama
	,@Tipe
	,@Satuan
	,@Stock
	,@HargaSatuan
	,@HargaTotal
)







GO
/****** Object:  StoredProcedure [dbo].[spKontrakKomisiOverDaftar]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Pendaftaran agent baru
*/

CREATE PROCEDURE [dbo].[spKontrakKomisiOverDaftar]
	 @NoKontrak varchar (50) 
	,@GeneralManager varchar (50) 
      ,@SalesManager varchar(50)
      ,@AdminSales varchar(50)
      ,@ProjectManager varchar(50)
      ,@KepalaUnitSales varchar(50)
      ,@MarketingSupport varchar(50)
      ,@BillingCollection varchar(50)
	  ,@Cadangan varchar(50)
      ,@Kinerja varchar(50)
      ,@KantorPusat varchar(50)
      ,@GeneralManagerCross varchar(50)
      ,@SalesManagerCross varchar(50)

	  AS

INSERT INTO MS_KOMISI_OVER
(
	NoKontrak
	,[GeneralManager]
      ,[SalesManager]
      ,[AdminSales]
      ,[ProjectManager]
      ,[KepalaUnitSales]
      ,[MarketingSupport]
      ,[BillingCollection]
	  ,[Cadangan]
	  ,[Kinerja]
	  ,[KantorPusat]
      ,[GeneralManagerCross]
      ,[SalesManagerCross]

)
VALUES
(
	 @NoKontrak
	,@GeneralManager  
      ,@SalesManager 
      ,@AdminSales 
      ,@ProjectManager 
      ,@KepalaUnitSales
      ,@MarketingSupport
      ,@BillingCollection
	  ,@Cadangan
	  ,@Kinerja
	  ,@KantorPusat
      ,@GeneralManagerCross
      ,@SalesManagerCross 
)










GO
/****** Object:  StoredProcedure [dbo].[spKontrakPPJB]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Prosedur PPJB
*/

CREATE PROCEDURE [dbo].[spKontrakPPJB]
	 @NoKontrak varchar(50)
	,@NoPPJB varchar(100)
	,@TglPPJB datetime
AS

UPDATE MS_KONTRAK SET
	  TglPPJB = CONVERT(datetime, @TglPPJB, 101)
	 ,NoPPJB = @NoPPJB
	 ,PPJB = 'D'
WHERE NoKontrak = @NoKontrak









GO
/****** Object:  StoredProcedure [dbo].[spKontrakRefresh]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Edit data-data yang terkait dengan MS_UNIT
*/

CREATE PROCEDURE [dbo].[spKontrakRefresh]
	 @NoKontrak varchar(50)
AS

DECLARE
	 @Luas money
	,@PriceList money
	,@NoStock varchar(20)
	,@Status varchar(1)
	,@Jenis varchar(20)
	,@Lokasi varchar(20)
	,@NoUnit varchar(20)

-- Ambil periode sewa
SELECT
	 @NoStock = NoStock
	,@Status = Status
FROM MS_KONTRAK WHERE NoKontrak = @NoKontrak

IF @Status <> 'A'
	RETURN

-- Luas dan harga price list yang ditentukan administrasi sewa masuk
SELECT
	 @Luas = Luas
	,@PriceList = PriceList
	,@Jenis = Jenis
	,@Lokasi = Lokasi
	,@NoUnit = NoUnit
FROM MS_UNIT WHERE NoStock = @NoStock

UPDATE MS_KONTRAK
SET
	 Luas = @Luas
	,Gross = @PriceList
	,DiskonPersen = ''
	,DiskonRupiah = @PriceList --- NilaiKontrak
	,FlagGross = 2
    ,Jenis = @Jenis
	,Lokasi = @Lokasi
	,NoUnit = @NoUnit
	,TglEdit = GETDATE()
WHERE NoKontrak = @NoKontrak









GO
/****** Object:  StoredProcedure [dbo].[spKontrakST]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Prosedur serah terima
*/

CREATE PROCEDURE [dbo].[spKontrakST]
	 @NoKontrak varchar(50)
	,@NoST varchar(20) = ''
	,@TglST datetime
	,@Luas money = 0
AS

UPDATE MS_KONTRAK SET
	  ST = 'D'
	 ,TglST = CONVERT(datetime, @TglST, 101)
	 ,NoST = @NoST
WHERE NoKontrak = @NoKontrak

-- Perbedaan Luas
DECLARE
	 @LuasSkr money
	
SELECT
	 @LuasSkr = Luas
FROM MS_KONTRAK
WHERE NoKontrak = @NoKontrak

IF @LuasSkr <> @Luas
BEGIN
	UPDATE MS_KONTRAK SET
		 Luas = @Luas
		,FlagGross = 4
	WHERE NoKontrak = @NoKontrak
END









GO
/****** Object:  StoredProcedure [dbo].[spKontrakUndoAJB]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Undo AJB
*/

CREATE PROCEDURE [dbo].[spKontrakUndoAJB]
	 @NoKontrak varchar(50)
AS

UPDATE MS_KONTRAK SET
	  TglAJB = NULL
	 ,NoAJB = ''
	 ,AJB = 'B'
WHERE NoKontrak = @NoKontrak









GO
/****** Object:  StoredProcedure [dbo].[spKontrakUndoBatal]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Undo PEMBATALAN
*/

CREATE PROCEDURE [dbo].[spKontrakUndoBatal]
	 @NoKontrak varchar(50)
AS

DECLARE
	 @Validasi varchar(3)
	,@NoStock varchar(20)
	,@Status varchar(1)

SELECT
	 @NoStock = NoStock
	,@Status = Status
FROM MS_KONTRAK
WHERE NoKontrak = @NoKontrak

EXEC spValidasiDaftar
	 @NoStock
	,@Validasi OUTPUT

-- Validasi unit
IF @Validasi <> 'OK'
	RETURN
	
-- Validasi status
IF @Status <> 'B'
	RETURN

UPDATE MS_KONTRAK SET
	  Status = 'A'
	 ,AlasanBatal = ''
WHERE NoKontrak = @NoKontrak

UPDATE MS_UNIT
SET Status = 'B'
WHERE NoStock = @NoStock









GO
/****** Object:  StoredProcedure [dbo].[spKontrakUndoPPJB]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Undo PPJB
*/

CREATE PROCEDURE [dbo].[spKontrakUndoPPJB]
	 @NoKontrak varchar(50)
AS

UPDATE MS_KONTRAK SET
	  TglPPJB = NULL
	 ,NoPPJB = ''
	 ,PPJB = 'B'
WHERE NoKontrak = @NoKontrak









GO
/****** Object:  StoredProcedure [dbo].[spKontrakUndoST]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Undo SERAH TERIMA
*/

CREATE PROCEDURE [dbo].[spKontrakUndoST]
	 @NoKontrak varchar(50)
AS

DECLARE
	@ST varchar(1)

SELECT
	@ST = ST
FROM MS_KONTRAK
WHERE NoKontrak = @NoKontrak

-- Validasi status
IF @ST <> 'D'
	RETURN

UPDATE MS_KONTRAK SET
	  ST = 'B'
	 ,TglST = NULL
	 ,NoST = ''
WHERE NoKontrak = @NoKontrak









GO
/****** Object:  StoredProcedure [dbo].[spLapPDFDaftar]    Script Date: 05/04/2019 15.51.15 ******/
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
/****** Object:  StoredProcedure [dbo].[spLogAgent]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Logfile Master Agent
*/

CREATE PROCEDURE [dbo].[spLogAgent]
	 @Aktivitas varchar (6)
	,@UserID varchar (20)
	,@IP varchar (50) = ''
	,@Ket text = ''
	,@Pk varchar (50) = ''
AS

INSERT INTO MS_AGENT_LOG
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
/****** Object:  StoredProcedure [dbo].[spLogBankKPA]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
Logfile Account
*/

CREATE PROCEDURE [dbo].[spLogBankKPA]
	 @Aktivitas varchar (6)
	,@UserID varchar (20)
	,@IP varchar (50) = ''
	,@Ket text = ''
	,@Pk varchar (50) = ''
AS

INSERT INTO REF_BANKKPA_LOG
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
/****** Object:  StoredProcedure [dbo].[spLogBerkasPPJB]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/*
Logfile Master Unit
*/

CREATE PROCEDURE [dbo].[spLogBerkasPPJB]
	 @Aktivitas varchar (6)
	,@UserID varchar (20)
	,@IP varchar (50) = ''
	,@Ket text = ''
	,@Pk varchar (20) = ''
AS

INSERT INTO REF_BERKAS_PPJB_LOG
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
/****** Object:  StoredProcedure [dbo].[spLogCustomer]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Logfile Master Customer
*/

CREATE PROCEDURE [dbo].[spLogCustomer]
	 @Aktivitas varchar (6)
	,@UserID varchar (20)
	,@IP varchar (50) = ''
	,@Ket text = ''
	,@Pk varchar (50) = ''
AS

INSERT INTO MS_CUSTOMER_LOG
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
/****** Object:  StoredProcedure [dbo].[spLogFU]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/*
Logfile Master Unit
*/

CREATE PROCEDURE [dbo].[spLogFU]
	 @Aktivitas varchar (6)
	,@UserID varchar (20)
	,@IP varchar (50) = ''
	,@Ket text = ''
	,@Pk varchar (20) = ''
AS

INSERT INTO MS_FOLLOWUP_LOG
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
/****** Object:  StoredProcedure [dbo].[spLogGimmick]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Logfile Master Kontrak
*/

CREATE PROCEDURE [dbo].[spLogGimmick]
	 @Aktivitas varchar (6)
	,@UserID varchar (20)
	,@IP varchar (50) = ''
	,@Ket text = ''
	,@Pk varchar (50) = ''
AS

INSERT INTO MS_GIMMICK_LOG
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
/****** Object:  StoredProcedure [dbo].[spLogGrouping]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[spLogGrouping]
	 @Aktivitas varchar (6)
	,@UserID varchar (20)
	,@IP varchar (50) = ''
	,@Ket text = ''
	,@Pk varchar (20) = ''
AS

INSERT INTO REF_FOLLOWUP_LOG
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
/****** Object:  StoredProcedure [dbo].[spLogHold]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Logfile Master Hold
*/

CREATE PROCEDURE [dbo].[spLogHold]
	 @Aktivitas varchar (6)
	,@UserID varchar (20)
	,@IP varchar (50) = ''
	,@Ket text = ''
	,@Pk varchar (50) = ''
AS

INSERT INTO MS_HOLD_LOG
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
/****** Object:  StoredProcedure [dbo].[spLogJenis]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Logfile Master Unit
*/

CREATE PROCEDURE [dbo].[spLogJenis]
	 @Aktivitas varchar (6)
	,@UserID varchar (20)
	,@IP varchar (50) = ''
	,@Ket text = ''
	,@Pk varchar (20) = ''
AS

INSERT INTO REF_JENIS_LOG
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
/****** Object:  StoredProcedure [dbo].[spLogJenisProperti]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Logfile Master Unit
*/

CREATE PROCEDURE [dbo].[spLogJenisProperti]
	 @Aktivitas varchar (6)
	,@UserID varchar (20)
	,@IP varchar (50) = ''
	,@Ket text = ''
	,@Pk varchar (20) = ''
AS

INSERT INTO REF_JenisProperti_LOG
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
/****** Object:  StoredProcedure [dbo].[spLogKomisi]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Logfile Master komisi
*/

CREATE PROCEDURE [dbo].[spLogKomisi]
	 @Aktivitas varchar (6)
	,@UserID varchar (20)
	,@IP varchar (50) = ''
	,@Ket text = ''
	,@Pk varchar (50) = ''
AS

INSERT INTO MS_KOMISI_LOG
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
/****** Object:  StoredProcedure [dbo].[spLogKomisiCF]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Logfile Master closing fee
*/

CREATE PROCEDURE [dbo].[spLogKomisiCF]
	 @Aktivitas varchar (6)
	,@UserID varchar (20)
	,@IP varchar (50) = ''
	,@Ket text = ''
	,@Pk varchar (50) = ''
AS

INSERT INTO MS_KOMISI_CF_LOG
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
/****** Object:  StoredProcedure [dbo].[spLogKomisiCFP]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Logfile Master pengajuan closing fee
*/

CREATE PROCEDURE [dbo].[spLogKomisiCFP]
	 @Aktivitas varchar (6)
	,@UserID varchar (20)
	,@IP varchar (50) = ''
	,@Ket text = ''
	,@Pk varchar (50) = ''
AS

INSERT INTO MS_KOMISI_CFP_LOG
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
/****** Object:  StoredProcedure [dbo].[spLogKomisiCFR]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Logfile Master realisasi closing fee
*/

CREATE PROCEDURE [dbo].[spLogKomisiCFR]
	 @Aktivitas varchar (6)
	,@UserID varchar (20)
	,@IP varchar (50) = ''
	,@Ket text = ''
	,@Pk varchar (50) = ''
AS

INSERT INTO MS_KOMISI_CFR_LOG
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
/****** Object:  StoredProcedure [dbo].[spLogKomisiP]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Logfile Master pengajuan komisi
*/

CREATE PROCEDURE [dbo].[spLogKomisiP]
	 @Aktivitas varchar (6)
	,@UserID varchar (20)
	,@IP varchar (50) = ''
	,@Ket text = ''
	,@Pk varchar (50) = ''
AS

INSERT INTO MS_KOMISIP_LOG
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
/****** Object:  StoredProcedure [dbo].[spLogKomisiR]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Logfile Master realisasi komisi
*/

CREATE PROCEDURE [dbo].[spLogKomisiR]
	 @Aktivitas varchar (6)
	,@UserID varchar (20)
	,@IP varchar (50) = ''
	,@Ket text = ''
	,@Pk varchar (50) = ''
AS

INSERT INTO MS_KOMISIR_LOG
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
/****** Object:  StoredProcedure [dbo].[spLogKomisiReward]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Logfile Master reward
*/

CREATE PROCEDURE [dbo].[spLogKomisiReward]
	 @Aktivitas varchar (6)
	,@UserID varchar (20)
	,@IP varchar (50) = ''
	,@Ket text = ''
	,@Pk varchar (50) = ''
AS

INSERT INTO MS_KOMISI_REWARD_LOG
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
/****** Object:  StoredProcedure [dbo].[spLogKomisiRewardP]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Logfile Master pengajuan reward
*/

CREATE PROCEDURE [dbo].[spLogKomisiRewardP]
	 @Aktivitas varchar (6)
	,@UserID varchar (20)
	,@IP varchar (50) = ''
	,@Ket text = ''
	,@Pk varchar (50) = ''
AS

INSERT INTO MS_KOMISI_REWARD_P_LOG
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
/****** Object:  StoredProcedure [dbo].[spLogKomisiRewardR]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Logfile Master realisasi reward
*/

CREATE PROCEDURE [dbo].[spLogKomisiRewardR]
	 @Aktivitas varchar (6)
	,@UserID varchar (20)
	,@IP varchar (50) = ''
	,@Ket text = ''
	,@Pk varchar (50) = ''
AS

INSERT INTO MS_KOMISI_REWARD_R_LOG
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
/****** Object:  StoredProcedure [dbo].[spLogKontrak]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Logfile Master Kontrak
*/

CREATE PROCEDURE [dbo].[spLogKontrak]
	 @Aktivitas varchar (10)
	,@UserID varchar (20)
	,@IP varchar (50) = ''
	,@Ket text = ''
	,@Pk varchar (50) = ''
AS

INSERT INTO MS_KONTRAK_LOG
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
/****** Object:  StoredProcedure [dbo].[spLogKontrakApp]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spLogKontrakApp]
	 @NoKontrak varchar (50)
	,@ApprovedBy varchar (20)
	,@Approve tinyint
	,@TglApprove datetime
	,@Lvl tinyint
	,@Tipe tinyint
	,@Komentar varchar(100)
AS

INSERT INTO MS_KONTRAK_APP_LOG
(
	 NoKontrak
	,ApprovedBy
	,Approve
	,TglApprove
	,Lvl
	,Tipe
	,Komentar
)
VALUES
(
	 @NoKontrak
	,@ApprovedBy
	,@Approve
	,CONVERT(datetime, @TglApprove, 101)
	,@Lvl
	,@Tipe
	,@Komentar
)









GO
/****** Object:  StoredProcedure [dbo].[spLogKontrakApprov]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/*
Logfile Master Kontrak
*/

CREATE PROCEDURE [dbo].[spLogKontrakApprov]
	 @Aktivitas varchar (6)
	,@UserID varchar (20)
	,@IP varchar (50) = ''
	,@Ket text = ''
	,@Pk varchar (50) = ''
AS

INSERT INTO MS_KONTRAK_APPROVAL_LOG
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
/****** Object:  StoredProcedure [dbo].[spLogLevelSales]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[spLogLevelSales]
	 @Aktivitas varchar (6)
	,@UserID varchar (20)
	,@IP varchar (50) = ''
	,@Ket text = ''
	,@Pk varchar (20) = ''
AS

INSERT INTO REF_AGENT_LEVEL_LOG
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
/****** Object:  StoredProcedure [dbo].[spLogLokasi]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Logfile Master Unit
*/

CREATE PROCEDURE [dbo].[spLogLokasi]
	 @Aktivitas varchar (6)
	,@UserID varchar (20)
	,@IP varchar (50) = ''
	,@Ket text = ''
	,@Pk varchar (20) = ''
AS

INSERT INTO REF_LOKASI_LOG
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
/****** Object:  StoredProcedure [dbo].[spLogLokasiKontrak]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
Logfile Master Unit
*/

CREATE PROCEDURE [dbo].[spLogLokasiKontrak]
	 @Aktivitas varchar (6)
	,@UserID varchar (20)
	,@IP varchar (50) = ''
	,@Ket text = ''
	,@Pk varchar (20) = ''
AS
BEGIN
	INSERT INTO REF_LOKASI_KONTRAK_LOG
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
END





GO
/****** Object:  StoredProcedure [dbo].[spLogNUP]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spLogNUP]
	 @Aktivitas varchar (6)
	,@UserID varchar (20)
	,@IP varchar (50) = ''
	,@Ket text = ''
	,@Pk varchar (50) = ''
	,@Project varchar (20) = ''
	,@Tipe varchar (50) = ''
AS

INSERT INTO MS_NUP_LOG
(
	 Aktivitas
	,UserID
	,IP
	,Ket
	,Pk
	,Project
	,Tipe
)
VALUES
(
	 @Aktivitas
	,@UserID
	,@IP
	,@Ket
	,@Pk
	,@Project
	,@Tipe
)





GO
/****** Object:  StoredProcedure [dbo].[spLogPutihDenda]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/*
Logfile Master Kontrak
*/

CREATE PROCEDURE [dbo].[spLogPutihDenda]
	 @Aktivitas varchar (6)
	,@UserID varchar (20)
	,@IP varchar (50) = ''
	,@Ket text = ''
	,@Pk varchar (50) = ''
AS

INSERT INTO MS_PUTIHDENDA_LOG
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
/****** Object:  StoredProcedure [dbo].[spLogRealisasiDenda]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/*
Logfile Master Kontrak
*/

CREATE PROCEDURE [dbo].[spLogRealisasiDenda]
	 @Aktivitas varchar (6)
	,@UserID varchar (20)
	,@IP varchar (50) = ''
	,@Ket text = ''
	,@Pk varchar (50) = ''
AS

INSERT INTO MS_REALISASIDENDA_LOG
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
/****** Object:  StoredProcedure [dbo].[spLogReservasi]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Logfile Master Reservasi
*/

CREATE PROCEDURE [dbo].[spLogReservasi]
	 @Aktivitas varchar (6)
	,@UserID varchar (20)
	,@IP varchar (50) = ''
	,@Ket text = ''
	,@Pk varchar (50) = ''
AS

INSERT INTO MS_RESERVASI_LOG
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
/****** Object:  StoredProcedure [dbo].[spLogRetensiKPA]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
Logfile Account
*/

CREATE PROCEDURE [dbo].[spLogRetensiKPA]
	 @Aktivitas varchar (6)
	,@UserID varchar (20)
	,@IP varchar (50) = ''
	,@Ket text = ''
	,@Pk varchar (50) = ''
AS

INSERT INTO REF_RETENSI_LOG
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
/****** Object:  StoredProcedure [dbo].[spLogSkema]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Logfile Referensi Skema
*/

CREATE PROCEDURE [dbo].[spLogSkema]
	 @Aktivitas varchar (6)
	,@UserID varchar (20)
	,@IP varchar (50) = ''
	,@Ket text = ''
	,@Pk varchar (50) = ''
AS

INSERT INTO REF_SKEMA_LOG
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
/****** Object:  StoredProcedure [dbo].[spLogSkom]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[spLogSkom]
	 @Aktivitas varchar (6)
	,@UserID varchar (20)
	,@IP varchar (50) = ''
	,@Ket text = ''
	,@Pk varchar (50) = ''
AS

INSERT INTO REF_SKOM_LOG
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
/****** Object:  StoredProcedure [dbo].[spLogSkomCF]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[spLogSkomCF]
	 @Aktivitas varchar (6)
	,@UserID varchar (20)
	,@IP varchar (50) = ''
	,@Ket text = ''
	,@Pk varchar (50) = ''
AS

INSERT INTO REF_SKOM_CF_LOG
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
/****** Object:  StoredProcedure [dbo].[spLogSkomReward]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[spLogSkomReward]
	 @Aktivitas varchar (6)
	,@UserID varchar (20)
	,@IP varchar (50) = ''
	,@Ket text = ''
	,@Pk varchar (50) = ''
AS

INSERT INTO REF_SKOM_REWARD_LOG
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
/****** Object:  StoredProcedure [dbo].[spLogSkomTerm]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[spLogSkomTerm]
	 @Aktivitas varchar (6)
	,@UserID varchar (20)
	,@IP varchar (50) = ''
	,@Ket text = ''
	,@Pk varchar (50) = ''
AS

INSERT INTO REF_SKOM_TERM_LOG
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
/****** Object:  StoredProcedure [dbo].[spLogTipeGimmick]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Logfile Master Kontrak
*/

CREATE PROCEDURE [dbo].[spLogTipeGimmick]
	 @Aktivitas varchar (6)
	,@UserID varchar (20)
	,@IP varchar (50) = ''
	,@Ket text = ''
	,@Pk varchar (50) = ''
AS

INSERT INTO REF_TIPE_GIMMICK_LOG
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
/****** Object:  StoredProcedure [dbo].[spLogTipeSales]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[spLogTipeSales]
	 @Aktivitas varchar (6)
	,@UserID varchar (20)
	,@IP varchar (50) = ''
	,@Ket text = ''
	,@Pk varchar (20) = ''
AS

INSERT INTO REF_AGENT_TIPE_LOG
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
/****** Object:  StoredProcedure [dbo].[spLogTTR]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spLogTTR]
	 @Aktivitas varchar (6)
	,@UserID varchar (20)
	,@IP varchar (50) = ''
	,@Ket text = ''
	,@Pk varchar (50) = ''
AS

INSERT INTO MS_TTR_LOG
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
/****** Object:  StoredProcedure [dbo].[spLogUnit]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Logfile Master Unit
*/

CREATE PROCEDURE [dbo].[spLogUnit]
	 @Aktivitas varchar (6)
	,@UserID varchar (20)
	,@IP varchar (50) = ''
	,@Ket text = ''
	,@Pk varchar (50) = ''
AS

INSERT INTO MS_UNIT_LOG
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
/****** Object:  StoredProcedure [dbo].[spMigratePembayaran]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spMigratePembayaran] 
	 @NoKontrak varchar(50)
	,@NoTTS varchar(50)
	,@NoTTSManual varchar(30)
	,@TglTTS datetime
	,@NoBKM varchar(50)
	,@TglBKM datetime
	,@CaraBayar varchar(2)
	,@Nilai money
	,@NamaTagihan varchar(50)
	,@Rekening varchar(50)
	
AS

INSERT INTO MIGRATE_PEMBAYARAN
(
	 NoKontrak
	,NoTTS
	,NoTTSManual
	,TglTTS
	,NoBKM
	,TglBKM
	,CaraBayar
	,Nilai
	,NamaTagihan
	,Rekening
)
VALUES
(
	 @NoKontrak
	,@NoTTS
	,@NoTTSManual
	,@TglTTS
	,@NoBKM
	,@TglBKM
	,@CaraBayar
	,@Nilai
	,@NamaTagihan
	,@Rekening
)










GO
/****** Object:  StoredProcedure [dbo].[spNUPDaftar]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Pendaftaran customer baru
*/

CREATE PROCEDURE [dbo].[spNUPDaftar]
	 @NoNUP varchar(20)
	,@NoCustomer int
	,@NoAgent int
	,@Tipe varchar (20)
	,@Project varchar (20)
AS

INSERT INTO MS_NUP
(
	 NoNUP
	,NoCustomer
	,NoAgent
	,TglDaftar
	,Tipe
	,Project
)
VALUES
(
	 @NoNUP
	,@NoCustomer
	,@NoAgent
	,GETDATE()
	,@Tipe
	,@Project
)





GO
/****** Object:  StoredProcedure [dbo].[spPelunasan]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Proses posting TTS dari finance ar
*/

CREATE PROCEDURE [dbo].[spPelunasan]
	 @NoKontrak varchar(50)
	,@NoTagihan int = 0
	,@NilaiPelunasan money = 0
	,@NoTTS int = 0
AS

DECLARE @NoUrut int
SELECT @NoUrut = ISNULL(MAX(NoUrut),0) + 1 FROM MS_PELUNASAN
WHERE NoKontrak = @NoKontrak

DECLARE @NilaiDPP money = 0
DECLARE @NilaiPPN money = 0

DECLARE @TipePPN BIT
DECLARE @TipeTagihan VARCHAR(3)

SELECT @TipePPN = PPN FROM MS_KONTRAK
WHERE NoKontrak = @NoKontrak

SELECT @TipeTagihan = Tipe FROM MS_TAGIHAN
WHERE NoKontrak = @NoKontrak AND NoUrut = @NoTagihan

IF(@TipePPN = 1 AND @TipeTagihan <> 'ADM')
	BEGIN
		SET @NilaiDPP = ROUND(@NilaiPelunasan / 1.1, 0)
		SEt @NilaiPPN = @NilaiPelunasan - @NilaiDPP
	END
ELSE IF(@TipePPN = 0)
	BEGIN
		SET @NilaiDPP = @NilaiPelunasan
	END

INSERT INTO MS_PELUNASAN
(
	 NoKontrak
	,NoUrut
	,NoTagihan
	,NilaiPelunasan
	,NoTTS
	,NilaiDPP
	,NilaiPPN
)
VALUES
(
	 @NoKontrak
	,@NoUrut
	,@NoTagihan
	,@NilaiPelunasan
	,@NoTTS
	,@NilaiDPP
	,@NilaiPPN
)

-- Customer
DECLARE @NoCustomer int
SELECT @NoCustomer = NoCustomer FROM MS_KONTRAK WHERE NoKontrak = @NoKontrak
EXEC spTglTransaksi @NoCustomer







GO
/****** Object:  StoredProcedure [dbo].[spPelunasanAlokasi]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spPelunasanAlokasi] 
	 @NoKontrak varchar(20)
	,@NoTagihan int = 0
	,@NilaiPelunasan money = 0
	,@NoALOKASI int = 0
AS

DECLARE @NoUrut int
SELECT @NoUrut = ISNULL(MAX(NoUrut),0) + 1 FROM MS_PELUNASAN
WHERE NoKontrak = @NoKontrak

BEGIN

INSERT INTO MS_PELUNASAN
(
	 NoKontrak
	,NoUrut
	,NoTagihan
	,CaraBayar
	,NilaiPelunasan
	,NoALOKASI
)
VALUES
(
	 @NoKontrak
	,@NoUrut
	,@NoTagihan
	,'AL'
	,@NilaiPelunasan
	,@NoALOKASI
)

-- Customer
DECLARE @NoCustomer int
SELECT @NoCustomer = NoCustomer FROM MS_KONTRAK WHERE NoKontrak = @NoKontrak

EXEC spTglTransaksi @NoCustomer
END





GO
/****** Object:  StoredProcedure [dbo].[spPelunasanEdit]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Edit Alokasi Pelunasan
*/

CREATE PROCEDURE [dbo].[spPelunasanEdit]
	 @NoKontrak varchar(50)
	,@NoUrut int
	,@NoTagihan int = 0
AS

UPDATE MS_PELUNASAN SET
	 NoTagihan = @NoTagihan
WHERE
NoKontrak = @NoKontrak
AND NoUrut = @NoUrut

-- MS_KONTRAK.PersenLunas
EXEC spProsentasePelunasan @NoKontrak









GO
/****** Object:  StoredProcedure [dbo].[spPelunasanKPA]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Proses posting TTS dari finance ar
*/

CREATE PROCEDURE [dbo].[spPelunasanKPA]
	 @NoKontrak varchar(50)
	,@NoTagihan int 
	,@NilaiPelunasan money
	,@NoRealKPA int
	,@NoTTS int
	,@CaraBayar varchar(10)
AS

DECLARE @NoUrut int
SELECT @NoUrut = ISNULL(MAX(NoUrut),0) + 1 FROM MS_PELUNASAN_KPA
WHERE NoKontrak = @NoKontrak

INSERT INTO MS_PELUNASAN_KPA
(
	 NoKontrak
	,NoUrut
	,NoTagihan
	,NilaiPelunasan
	,NoRealKPA
	,NoTTS
	,CaraBayar
	,SudahCair
)
VALUES
(
	 @NoKontrak
	,@NoUrut
	,@NoTagihan
	,@NilaiPelunasan
	,@NoRealKPA
	,@NoTTS
	,@CaraBayar
	,1
)











GO
/****** Object:  StoredProcedure [dbo].[spPelunasanMEMO]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Proses posting TTS dari finance ar
*/

CREATE PROCEDURE [dbo].[spPelunasanMEMO]
	 @NoKontrak varchar(50)
	,@NoTagihan int = 0
	,@NilaiPelunasan money = 0
	,@NoMEMO int = 0
AS

DECLARE @NoUrut int
SELECT @NoUrut = ISNULL(MAX(NoUrut),0) + 1 FROM MS_PELUNASAN
WHERE NoKontrak = @NoKontrak

DECLARE @NilaiDPP money = 0
DECLARE @NilaiPPN money = 0

DECLARE @TipePPN BIT
DECLARE @TipeTagihan VARCHAR(3)

SELECT @TipePPN = PPN FROM MS_KONTRAK
WHERE NoKontrak = @NoKontrak

SELECT @TipeTagihan = Tipe FROM MS_TAGIHAN
WHERE NoKontrak = @NoKontrak AND NoUrut = @NoTagihan

IF(@TipePPN = 1 AND @TipeTagihan <> 'ADM') -- cek apabila kontrak tersebut kena ppn, dan apabila tagihan itu bukan biaya admin
	BEGIN
		SET @NilaiDPP = ROUND(@NilaiPelunasan / 1.1, 0) -- ppn tidak menggunakan koma, jadi round aja
		SEt @NilaiPPN = @NilaiPelunasan - @NilaiDPP
	END
ELSE
	BEGIN
		SET @NilaiDPP = @NilaiPelunasan
	END

INSERT INTO MS_PELUNASAN
(
	 NoKontrak
	,NoUrut
	,NoTagihan
	,NilaiPelunasan
	,NoMEMO
	,NilaiDPP
	,NilaiPPN
)
VALUES
(
	 @NoKontrak
	,@NoUrut
	,@NoTagihan
	,@NilaiPelunasan
	,@NoMEMO
	,@NilaiDPP
	,@NilaiPPN
)

-- Customer
DECLARE @NoCustomer int
SELECT @NoCustomer = NoCustomer FROM MS_KONTRAK WHERE NoKontrak = @NoKontrak
EXEC spTglTransaksi @NoCustomer









GO
/****** Object:  StoredProcedure [dbo].[spPelunasanVoid]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Proses pembatalan posting TTS dari finance ar
*/

CREATE PROCEDURE [dbo].[spPelunasanVoid]
	@NoTTS int
AS

UPDATE MS_PELUNASAN SET NilaiPelunasan = 0, NilaiDPP = 0, NilaiPPN = 0 WHERE NoTTS = @NoTTS









GO
/****** Object:  StoredProcedure [dbo].[spPelunasanVoidMEMO]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Proses pembatalan posting MEMO dari finance ar
*/

CREATE PROCEDURE [dbo].[spPelunasanVoidMEMO]
	@NoMEMO int
AS

UPDATE MS_PELUNASAN SET NilaiPelunasan = 0 WHERE NoMEMO = @NoMEMO









GO
/****** Object:  StoredProcedure [dbo].[spPPJB]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Prosedur PPJB
*/

CREATE PROCEDURE [dbo].[spPPJB]
	 @NoKontrak varchar(50)
	,@NoPPJB varchar(100)
	,@TglPPJB datetime
AS

	INSERT INTO MS_PPJB
	(
		NoKontrak
		,NoPPJB
		,TglPPJB
	)
	VALUES
	(
		@NoKontrak
		,@NoPPJB
		,@TglPPJB 
	)











GO
/****** Object:  StoredProcedure [dbo].[spPriceListHistory]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
History price list unit
*/

CREATE PROCEDURE [dbo].[spPriceListHistory]
	 @NoStock varchar(20)
	,@PriceListMin money
	,@PriceList money
	,@PricelistKavling money
	,@Periode datetime
AS

DECLARE @No int
SELECT @No = ISNULL(MAX(No),0) + 1 FROM MS_PRICELIST_HISTORY

INSERT INTO MS_PRICELIST_HISTORY
(
	No,
	NoStock,
    PriceListMin,
    PriceList,
	PricelistKavling,
    Periode
)
VALUES
(
	@No
   ,@NoStock
   ,@PriceListMin
   ,@PriceList
   ,@PricelistKavling
   ,@Periode
)










GO
/****** Object:  StoredProcedure [dbo].[spPriorityDaftar]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[spPriorityDaftar]
	 @NoNUP varchar(20)
	,@NoStock varchar(20) = ''
	,@NoUnit varchar(20) = ''
	,@NoCustomer varchar(20) = ''
	,@TambahanHook money = 0
	,@TambahanLantaiStartegis money = 0
	,@TambahanViewVillage money = 0
	,@TambahanViewPool money = 0
	,@TambahanViewKampus money = 0
	,@TambahanViewCity money = 0
	,@NilaiKontrak money = 0
	,@Pricelist money = 0
	,@Diskon money = 0
	,@NomorSkema int
AS


INSERT INTO MS_PRIORITY
(
	 NoNUP 
	,NoStock
	,NoUnit
	,NoCustomer 
	,TambahanHook
	,TambahanLantaiStrategis
	,TambahanViewVillage
	,TambahanViewPool
	,TambahanViewKampus
	,TambahanViewCity
	,NilaiKontrak
	,Pricelist
	,Diskon
	,NomorSkema
)
VALUES
(
	 @NoNUP
	,@NoStock
	,@NoUnit
	,@NoCustomer
	,@TambahanHook
	,@TambahanLantaiStartegis
	,@TambahanViewVillage
	,@TambahanViewPool
	,@TambahanViewKampus
	,@TambahanViewCity
	,@NilaiKontrak
	,@Pricelist
	,@Diskon
	,@NomorSkema
)





GO
/****** Object:  StoredProcedure [dbo].[spProsentasePelunasan]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Hitung ulang prosentase pelunasan
*/

CREATE PROCEDURE [dbo].[spProsentasePelunasan]
	@NoKontrak varchar(50)
AS

DECLARE
	 @TotalTagihan money
	,@TotalPelunasan money
	,@PersenLunas money

SELECT @TotalTagihan = ISNULL(SUM(NilaiTagihan),0)
FROM MS_TAGIHAN WHERE NoKontrak = @NoKontrak AND Tipe <> 'ADM'

SELECT @TotalPelunasan = ISNULL(SUM(NilaiPelunasan),0)
FROM MS_PELUNASAN WHERE NoKontrak = @NoKontrak AND NoTagihan IN (SELECT NoTagihan FROM MS_TAGIHAN WHERE NoKontrak = @NoKontrak AND Tipe <> 'ADM')
AND SudahCair = 1

IF @TotalTagihan = 0 SET
	@PersenLunas = 0
ELSE
BEGIN
	SET @PersenLunas = (@TotalPelunasan / @TotalTagihan) * 100
	IF @PersenLunas > 100 SET
		@PersenLunas = 100
END

UPDATE MS_KONTRAK SET PersenLunas = @PersenLunas WHERE NoKontrak = @NoKontrak








GO
/****** Object:  StoredProcedure [dbo].[spProsentasePelunasanMEMO]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Hitung ulang prosentase pelunasan
*/

CREATE PROCEDURE [dbo].[spProsentasePelunasanMEMO]
	@NoKontrak varchar(50)
AS

DECLARE
	 @TotalTagihan money
	,@TotalPelunasan money
	,@PersenLunas money

SELECT @TotalTagihan = ISNULL(SUM(NilaiTagihan),0)
FROM MS_TAGIHAN WHERE NoKontrak = @NoKontrak

SELECT @TotalPelunasan = ISNULL(SUM(NilaiPelunasan),0)
FROM MS_PELUNASAN WHERE NoKontrak = @NoKontrak
AND SudahCair = 1

IF @TotalTagihan = 0 SET
	@PersenLunas = 0
ELSE
BEGIN
	SET @PersenLunas = (@TotalPelunasan / @TotalTagihan) * 100
	IF @PersenLunas > 100 SET
		@PersenLunas = 100
END

UPDATE MS_KONTRAK SET PersenLunas = @PersenLunas WHERE NoKontrak = @NoKontrak









GO
/****** Object:  StoredProcedure [dbo].[spReservasiDaftar]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Proses pendaftaran reservasi baru
*/

CREATE PROCEDURE [dbo].[spReservasiDaftar]
	 @NoStock varchar(20)
	,@NoCustomer int
	,@NoAgent int
	,@Tgl datetime
	,@TglExpire datetime
	,@Netto money = 0
	,@Skema varchar(150) = ''
	,@NoQueue int = 0
AS

DECLARE @NoReservasi int
SELECT @NoReservasi = ISNULL(MAX(NoReservasi),0) + 1 FROM MS_RESERVASI

-- nomor urut waiting list per unit
DECLARE @NoUrut int
SELECT @NoUrut = ISNULL(MAX(NoUrut),0) + 1 FROM MS_RESERVASI
WHERE NoStock = @NoStock

DECLARE
	 @Jenis varchar(20)
	,@Lokasi varchar(20)
	,@NoUnit varchar(20)

SELECT
	 @Jenis = Jenis
	,@Lokasi = Lokasi
	,@NoUnit = NoUnit
FROM MS_UNIT 
WHERE NoStock = @NoStock


INSERT INTO MS_RESERVASI
(
	 NoReservasi
	,NoStock
	,NoUrut
	,NoCustomer
	,NoAgent
	,Tgl
	,TglExpire
	,Netto
	,Skema
	,Jenis
	,Lokasi
	,NoUnit
	,NoQueue
)
VALUES
(
	 @NoReservasi
	,@NoStock
	,@NoUrut
	,@NoCustomer
	,@NoAgent
	,CONVERT(datetime, @Tgl, 101)
	,CONVERT(datetime, @TglExpire, 100)
	,@Netto
	,@Skema
	,@Jenis
	,@Lokasi
	,@NoUnit
	,@NoQueue
)

-- sistem batas waktu
DECLARE @Status varchar(1)
IF (SELECT DATEDIFF(n,@TglExpire,GETDATE())) >= 0
	SET @Status = 'E'
ELSE
	SET @Status = 'A'
	
UPDATE MS_RESERVASI
SET
	Status = @Status
WHERE
	NoReservasi = @NoReservasi


-- Update tanggal transaksi di tabel customer
EXEC spTglTransaksi @NoCustomer









GO
/****** Object:  StoredProcedure [dbo].[spReservasiDel]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Hapus reservasi PERMANEN
*/

CREATE PROCEDURE [dbo].[spReservasiDel]
	@NoReservasi int
AS

DECLARE
	 @NoStock varchar(20)
	,@NoUrut int
SELECT
	 @NoStock = NoStock
	,@NoUrut = NoUrut
FROM MS_RESERVASI
WHERE NoReservasi = @NoReservasi

DELETE FROM MS_RESERVASI
WHERE
	NoReservasi = @NoReservasi

-- Reorganize nomor urut
UPDATE MS_RESERVASI SET
	NoUrut = NoUrut - 1
WHERE
	NoStock = @NoStock
	AND NoUrut > @NoUrut

--Reorganize TTR
UPDATE MS_TTR
SET NoReservasi = ''
WHERE NoReservasi = @NoReservasi









GO
/****** Object:  StoredProcedure [dbo].[spReservasiEdit]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Edit data reservasi
*/

CREATE PROCEDURE [dbo].[spReservasiEdit]
	 @NoReservasi int
	,@NoAgent int
	,@Tgl datetime
	,@TglExpire datetime
	,@Netto money
	,@Skema varchar(150)
	,@NoQueue int = 0
AS

-- sistem batas waktu
DECLARE @Status varchar(1)
IF (SELECT DATEDIFF(n,@TglExpire,GETDATE())) >= 0
	SET @Status = 'E'
ELSE
	SET @Status = 'A'
	
UPDATE MS_RESERVASI
SET
	 NoAgent = @NoAgent
	,Tgl = CONVERT(datetime, @Tgl, 101)
	,TglExpire = CONVERT(datetime, @TglExpire, 100)
	,Netto = @Netto
	,Skema = @Skema
	,Status = @Status
	,NoQueue = @NoQueue
	,TglEdit = GETDATE()
WHERE
	NoReservasi = @NoReservasi

-- Customer
DECLARE @NoCustomer int
SELECT @NoCustomer = NoCustomer FROM MS_RESERVASI WHERE NoReservasi = @NoReservasi
EXEC spTglTransaksi @NoCustomer









GO
/****** Object:  StoredProcedure [dbo].[spReservasiLaunching]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spReservasiLaunching]
     @Nama varchar(100) = ''
	,@KTP1 varchar (50) = ''
	,@KTP2 varchar (50) = ''
	,@KTP3 varchar (50) = ''
	,@KTP4 varchar (50) = ''
	,@NoKTP varchar (50) = ''
	,@NoTelp varchar (50) = ''
	,@NoHp varchar (50) = ''
	,@NoStock varchar (20) = ''
	,@Skema varchar (150) = ''
	,@NoAgent int
	,@NoKontrak varchar (50) = ''
	,@PriceList money
AS

DECLARE @Tgl datetime
SET @Tgl = CONVERT(varchar,GETDATE(),101)

-- CUSTOMER
DECLARE @NoCustomer int
SELECT @NoCustomer = ISNULL(MAX(NoCustomer),0) + 1 FROM MS_CUSTOMER
INSERT INTO MS_CUSTOMER
(
	 NoCustomer
	,Nama
	,NoTelp
	,NoHp
	,NoKTP
	,KTP1
	,KTP2
	,KTP3
	,KTP4
	,Agama
)
VALUES
(
	 @NoCustomer
	,@Nama
	,@NoTelp
	,@NoHp
	,@NoKTP
	,@KTP1
	,@KTP2
	,@KTP3
	,@KTP4
	,'LAINNYA'
)

-- RESERVASI
EXEC spReservasiDaftar
	@NoStock,@NoCustomer,@NoAgent,@Tgl,@Tgl,0,@Skema,0

-- KONTRAK
EXEC spKontrakDaftar
	@NoKontrak,@NoStock,@Tgl,@Skema,'1/1/2010',@PriceList









GO
/****** Object:  StoredProcedure [dbo].[spReservasiObs]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Sistem reservasi yang sudah obsolete
*/

CREATE PROCEDURE [dbo].[spReservasiObs]
	 @NoStock varchar(20)
	,@NoReservasi int
	,@NoKontrak varchar(50)
AS

INSERT INTO MS_RESERVASI_OBS
(
	 NoUrut
	,NoStock
	,Customer
	,Agent
	,Tgl
	,TglExpire
	,Netto
	,Skema
	,Jenis
	,Lokasi
	,NoUnit
	,NoQueue
)
SELECT
	 NoUrut
	,NoStock
	,(SELECT Nama FROM MS_CUSTOMER WHERE NoCustomer = MS_RESERVASI.NoCustomer)
	,(SELECT Nama FROM MS_AGENT WHERE NoAgent = MS_RESERVASI.NoAgent)
	,Tgl
	,TglExpire
	,Netto
	,Skema
	,Jenis
	,Lokasi
	,NoUnit
	,NoQueue
FROM MS_RESERVASI
WHERE
	NoStock = @NoStock
	AND NoReservasi <> @NoReservasi

UPDATE MS_RESERVASI SET NoKontrak = @NoKontrak WHERE NoStock = @NoStock

DELETE FROM MS_RESERVASI WHERE NoStock = @NoStock 
AND NoUrut != 1 --Tidak mendelete yang sudah menjad kontrak









GO
/****** Object:  StoredProcedure [dbo].[spReservasiPromote]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Prosedur promote waiting list
*/

CREATE PROCEDURE [dbo].[spReservasiPromote]
	@NoReservasi int
AS

DECLARE
	 @NoStock varchar(20)
	,@NoUrut int
SELECT
	 @NoStock = NoStock
	,@NoUrut = NoUrut
FROM MS_RESERVASI
WHERE NoReservasi = @NoReservasi

-- naikin dulu reservasi yang di-promote
UPDATE MS_RESERVASI SET NoUrut = 0
WHERE NoReservasi = @NoReservasi

-- geser!
UPDATE MS_RESERVASI SET NoUrut = NoUrut + 1
WHERE NoStock = @NoStock AND NoUrut < @NoUrut

-- Customer
DECLARE @NoCustomer int
SELECT @NoCustomer = NoCustomer FROM MS_RESERVASI WHERE NoReservasi = @NoReservasi
EXEC spTglTransaksi @NoCustomer









GO
/****** Object:  StoredProcedure [dbo].[spReservasiTagihanDaftar]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Pendaftaran satu buah tagihan baru
*/

CREATE PROCEDURE [dbo].[spReservasiTagihanDaftar]
	 @NoReservasi int
	,@NamaTagihan varchar (50)
	,@TglJT datetime
	,@NilaiTagihan money
	,@Tipe varchar (3)
AS

DECLARE @NoUrut int
SELECT @NoUrut = ISNULL(MAX(NoUrut),0) + 1 FROM MS_RESERVASI_TAGIHAN
WHERE NoReservasi = @NoReservasi

INSERT INTO MS_RESERVASI_TAGIHAN
(
	 NoReservasi
	,NoUrut
	,NamaTagihan
	,TglJT
	,NilaiTagihan
	,Tipe
)
VALUES
(
	 @NoReservasi
	,@NoUrut
	,@NamaTagihan
	,CONVERT(datetime, @TglJT, 101)
	,@NilaiTagihan
	,@Tipe
)









GO
/****** Object:  StoredProcedure [dbo].[spRetensiKPABaru]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
Pendaftaran account baru
*/

CREATE PROCEDURE [dbo].[spRetensiKPABaru]
	 @Kode varchar(50)
	,@NamaKategori varchar(MAX)
	,@Project varchar(20)
AS

INSERT INTO REF_RETENSI
(
	 Kode
	,NamaKategori
	,Project
)
VALUES
(
	 @Kode
	,@NamaKategori
	,@Project
)













GO
/****** Object:  StoredProcedure [dbo].[spRetensiKPADel]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
Delete account (PERMANEN)
*/

CREATE PROCEDURE [dbo].[spRetensiKPADel]
	@Kode varchar(50)
	,@Project varchar(20)
AS

-- Bank Tidak Boleh dihapus jika sudah menjadi referensi Bank KPA di MS_KONTRAK
IF EXISTS (SELECT RetensiKPA FROM MS_KONTRAK WHERE RetensiKPA = @Kode AND Project = @Project)
	RETURN
	
DELETE FROM REF_RETENSI WHERE Kode = @Kode AND Project = @Project













GO
/****** Object:  StoredProcedure [dbo].[spRetensiKPAEdit]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
Edit data account
*/

CREATE PROCEDURE [dbo].[spRetensiKPAEdit]
	 @KodeLama varchar(50)
	,@KodeBaru varchar(50)
	,@NamaKategori varchar(MAX)
	,@Project varchar(20)
AS

UPDATE REF_RETENSI SET
	 Kode = @KodeBaru
	,NamaKategori = @NamaKategori
WHERE
Kode = @KodeLama AND Project = @Project

UPDATE REF_RETENSI_LOG SET Pk = @KodeBaru WHERE Pk = @KodeLama













GO
/****** Object:  StoredProcedure [dbo].[spSitePlanDaftar]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spSitePlanDaftar]
	 @ParentID int,
	 @Nama nvarchar(50),
	 @isParent bit,
	 @PathGambarDasar nvarchar(200),
	 @PathGambarTransparent nvarchar(200)
AS

INSERT INTO MS_SITEPLAN
(
	ParentID,
	Nama,
	isParent,
	PathGambarDasar,
	PathGambarTransparent	
)
VALUES
(
	@ParentID,
	@Nama,
	@isParent,
	@PathGambarDasar,
	@PathGambarTransparent
)


GO
/****** Object:  StoredProcedure [dbo].[spSkemaBaru]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Penambahan skema cara bayar baru
*/

CREATE PROCEDURE [dbo].[spSkemaBaru]
	 @Nama varchar (100) = ''
	,@Diskon varchar (100) = ''
	,@DiskonKet varchar (1000) = ''
	,@RThousand bit = 1
	,@Bunga varchar (100) = ''
	,@BungaKet varchar(1000) = ''
	,@Project varchar(20) = ''
AS

-- Nomor urut terbesar ditambahkan satu
DECLARE @Nomor int
SELECT @Nomor = ISNULL(MAX(Nomor),0) + 1 FROM REF_SKEMA

INSERT INTO REF_SKEMA
(
	 Nomor
	,Nama
	,Diskon
	,DiskonKet
	,RThousand
	,Bunga
	,BungaKet
	,Project
)
VALUES
(
	 @Nomor
	,@Nama
	,@Diskon
	,@DiskonKet
	,@RThousand
	,@Bunga
	,@BungaKet
	,@Project
)



GO
/****** Object:  StoredProcedure [dbo].[spSkemaDel]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Delete skema cara bayar
*/

CREATE PROCEDURE [dbo].[spSkemaDel]
	@Nomor int
AS

DELETE FROM REF_SKEMA WHERE Nomor = @Nomor









GO
/****** Object:  StoredProcedure [dbo].[spSkemaDelBaris]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Hapus satu buah baris rumus dari skema cara bayar
*/

CREATE PROCEDURE [dbo].[spSkemaDelBaris]
	 @Nomor int
	,@Baris int
AS

DELETE FROM REF_SKEMA_DETAIL
WHERE
	Nomor = @Nomor
	AND Baris = @Baris

-- Reorganize nomor urut
UPDATE REF_SKEMA_DETAIL SET
	Baris = Baris - 1
WHERE
	Nomor = @Nomor
	AND Baris > @Baris









GO
/****** Object:  StoredProcedure [dbo].[spSkemaEdit]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Edit skema cara bayar
*/

CREATE PROCEDURE [dbo].[spSkemaEdit]
	 @Nomor int
	,@Nama varchar (100) = ''
	,@Diskon varchar (100) = ''
	,@DiskonKet varchar (1000) = ''
	,@Bunga varchar (100)
	,@BungaKet varchar (1000) = ''
	,@RThousand bit = 1
	,@Status varchar (1)
	,@Project varchar(20) = ''
AS

UPDATE REF_SKEMA
SET
	 Nama = @Nama
	,Diskon = @Diskon
	,DiskonKet = @DiskonKet
	,Bunga = @Bunga
	,BungaKet = @BungaKet
	,RThousand = @RThousand
	,Status = @Status
	,Project = @Project
WHERE Nomor = @Nomor



GO
/****** Object:  StoredProcedure [dbo].[spSkemaEditBaris]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Edit satu buah baris rumus cara bayar
*/

CREATE PROCEDURE [dbo].[spSkemaEditBaris]
	 @Nomor int
	,@Baris int
	,@Tipe varchar (3) = ''
	,@Nama varchar (50) = ''
	,@Nominal money = 0
	,@TipeNominal varchar (1) = ''
	,@TglFix datetime = null
	,@TipeJadwal varchar (1) = ''
	,@IntJadwal int = 0
	,@RefJadwal int = 0
	,@BF bit = 0
AS

UPDATE REF_SKEMA_DETAIL SET
	 Tipe = @Tipe
	,Nama = @Nama
	,Nominal = @Nominal
	,TipeNominal = @TipeNominal
	,TglFix = @TglFix
	,TipeJadwal = @TipeJadwal
	,IntJadwal = @IntJadwal
	,RefJadwal = @RefJadwal
	,BF = @BF
WHERE
	Nomor = @Nomor
	AND Baris = @Baris









GO
/****** Object:  StoredProcedure [dbo].[spSkemaTambah]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Tambah satu buah baris rumus cara bayar
*/

CREATE PROCEDURE [dbo].[spSkemaTambah]
	 @Nomor int
	,@Tipe varchar (3) = ''
	,@Nama varchar (50) = ''
	,@Nominal money = 0
	,@TipeNominal varchar (1) = ''
	,@TglFix datetime
	,@TipeJadwal varchar (1) = ''
	,@IntJadwal int = 0
	,@RefJadwal int = 0
	,@BF bit = 0
AS

-- Nomor urut terbesar ditambahkan satu
DECLARE @Baris int
SELECT @Baris = ISNULL(MAX(Baris),0) + 1 FROM REF_SKEMA_DETAIL
WHERE Nomor = @Nomor

INSERT INTO REF_SKEMA_DETAIL
(
	 Nomor
	,Baris
	,Tipe
	,Nama
	,Nominal
	,TipeNominal
	,TglFix
	,TipeJadwal
	,IntJadwal
	,RefJadwal
	,BF
)
VALUES
(
	 @Nomor
	,@Baris
	,@Tipe
	,@Nama
	,@Nominal
	,@TipeNominal
	,CONVERT(datetime, @TglFix, 101)
	,@TipeJadwal
	,@IntJadwal
	,@RefJadwal
	,@BF
)









GO
/****** Object:  StoredProcedure [dbo].[spSkomBaru]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Penambahan skema komisi baru
*/

CREATE PROCEDURE [dbo].[spSkomBaru]
	 @Nama varchar (100) = ''
	 ,@SalesTipe int
	 ,@Dari datetime
	 ,@Sampai datetime
	 ,@Rumus varchar(50)
	 ,@DasarHitung varchar(50)
AS

-- Nomor urut terbesar ditambahkan satu
DECLARE @Nomor int
SELECT @Nomor = ISNULL(MAX(NoSkema),0) + 1 FROM REF_SKOM

INSERT INTO REF_SKOM
(
	 NoSkema
	,SalesTipe
	,Nama	
	,Dari
	,Sampai
	,Rumus
	,DasarHitung
)
VALUES
(
	 @Nomor
	,@SalesTipe
	,@Nama
	,@Dari
	,@Sampai
	,@Rumus
	,@DasarHitung
)










GO
/****** Object:  StoredProcedure [dbo].[spSkomCFBaru]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Penambahan skema cf baru
*/

CREATE PROCEDURE [dbo].[spSkomCFBaru]
	 @Nama varchar (100) = ''
	 ,@SalesTipe int
	 ,@Dari datetime
	 ,@Sampai datetime
	 ,@Rumus varchar(50)
	 ,@DasarHitung varchar(50)
AS

-- Nomor urut terbesar ditambahkan satu
DECLARE @Nomor int
SELECT @Nomor = ISNULL(MAX(NoSkema),0) + 1 FROM REF_SKOM_CF

INSERT INTO REF_SKOM_CF
(
	 NoSkema
	,SalesTipe
	,Nama	
	,Dari
	,Sampai
	,Rumus
	,DasarHitung
)
VALUES
(
	 @Nomor
	,@SalesTipe
	,@Nama
	,@Dari
	,@Sampai
	,@Rumus
	,@DasarHitung
)










GO
/****** Object:  StoredProcedure [dbo].[spSkomCFDel]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Delete skema komisi
*/

CREATE PROCEDURE [dbo].[spSkomCFDel]
	@Nomor int
AS

DELETE FROM REF_SKOM_CF WHERE NoSkema = @Nomor










GO
/****** Object:  StoredProcedure [dbo].[spSkomCFEdit]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Edit skema cf
*/

CREATE PROCEDURE [dbo].[spSkomCFEdit]
	 @Nomor int
	,@Nama varchar (100) = ''
	,@Inaktif bit
	,@SalesTipe int
	,@Dari datetime
	,@Sampai datetime
	,@Rumus varchar(50)
	,@DasarHitung varchar(50)
AS

UPDATE REF_SKOM_CF
SET
	 Nama = @Nama
	,Inaktif = @Inaktif
	,SalesTipe = @SalesTipe
	,Dari = @Dari
	,Sampai = @Sampai
	,Rumus = @Rumus
	,DasarHitung = @DasarHitung
WHERE NoSkema = @Nomor







GO
/****** Object:  StoredProcedure [dbo].[spSkomCFTambah]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Tambah satu buah baris rumus cf
*/

CREATE PROCEDURE [dbo].[spSkomCFTambah]
	 @Nomor int
	,@SalesLevel int
	,@TipeTarif varchar (5) = ''
	,@Nominal money = 0
	,@Potong bit
AS

-- Nomor urut terbesar ditambahkan satu
DECLARE @Baris int
SELECT @Baris = ISNULL(MAX(SN),0) + 1 FROM REF_SKOM_CF_DETAIL
WHERE NoSkema = @Nomor

INSERT INTO REF_SKOM_CF_DETAIL
(
	 NoSkema
	,SN
	,SalesLevel
	,TipeTarif
	,Nilai
	,PotongKomisi
)
VALUES
(
	 @Nomor
	,@Baris
	,@SalesLevel
	,@TipeTarif
	,@Nominal
	,@Potong
)










GO
/****** Object:  StoredProcedure [dbo].[spSkomCFTambah2]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Tambah satu buah baris rumus cf
*/

CREATE PROCEDURE [dbo].[spSkomCFTambah2]
	 @Nomor int
	,@SalesLevel int
	,@TipeTarget varchar(50) = ''
	,@TargetBawah money = 0
	,@TargetAtas money = 0
	,@TipeTarif varchar (5) = ''
	,@Nominal money = 0
	,@Potong bit
AS

-- Nomor urut terbesar ditambahkan satu
DECLARE @Baris int
SELECT @Baris = ISNULL(MAX(SN),0) + 1 FROM REF_SKOM_CF_DETAIL2
WHERE NoSkema = @Nomor

INSERT INTO REF_SKOM_CF_DETAIL2
(
	 NoSkema
	,SN
	,SalesLevel
	,TipeTarget
	,TargetBawah
	,TargetAtas
	,TipeTarif
	,Nilai
	,PotongKomisi
)
VALUES
(
	 @Nomor
	,@Baris
	,@SalesLevel
	,@TipeTarget
	,@TargetBawah
	,@TargetAtas
	,@TipeTarif
	,@Nominal
	,@Potong
)










GO
/****** Object:  StoredProcedure [dbo].[spSkomDel]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Delete skema komisi
*/

CREATE PROCEDURE [dbo].[spSkomDel]
	@Nomor int
AS

DELETE FROM REF_SKOM WHERE NoSkema = @Nomor










GO
/****** Object:  StoredProcedure [dbo].[spSkomEdit]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Edit skema komisi
*/

CREATE PROCEDURE [dbo].[spSkomEdit]
	 @Nomor int
	,@Nama varchar (100) = ''
	,@Inaktif bit
	,@SalesTipe int
	,@Dari datetime
	,@Sampai datetime
	,@Rumus varchar(50)
	,@DasarHitung varchar(50)
AS

UPDATE REF_SKOM
SET
	 Nama = @Nama
	,Inaktif = @Inaktif
	,SalesTipe = @SalesTipe
	,Dari = @Dari
	,Sampai = @Sampai
	,Rumus = @Rumus
	,DasarHitung = @DasarHitung
WHERE NoSkema = @Nomor







GO
/****** Object:  StoredProcedure [dbo].[spSkomRewardBaru]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Penambahan skema reward baru
*/

CREATE PROCEDURE [dbo].[spSkomRewardBaru]
	 @Nama varchar (100) = ''
	 ,@SalesTipe int
	 ,@Dari datetime
	 ,@Sampai datetime
	 ,@Rumus varchar(50)
AS

-- Nomor urut terbesar ditambahkan satu
DECLARE @Nomor int
SELECT @Nomor = ISNULL(MAX(NoSkema),0) + 1 FROM REF_SKOM_REWARD

INSERT INTO REF_SKOM_REWARD
(
	 NoSkema
	,SalesTipe
	,Nama	
	,Dari
	,Sampai
	,Rumus
)
VALUES
(
	 @Nomor
	,@SalesTipe
	,@Nama
	,@Dari
	,@Sampai
	,@Rumus
)










GO
/****** Object:  StoredProcedure [dbo].[spSkomRewardDel]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Delete skema reward
*/

CREATE PROCEDURE [dbo].[spSkomRewardDel]
	@Nomor int
AS

DELETE FROM REF_SKOM_REWARD WHERE NoSkema = @Nomor










GO
/****** Object:  StoredProcedure [dbo].[spSkomRewardEdit]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Edit skema reward
*/

CREATE PROCEDURE [dbo].[spSkomRewardEdit]
	 @Nomor int
	,@Nama varchar (100) = ''
	,@Inaktif bit
	,@SalesTipe int
	,@Dari datetime
	,@Sampai datetime
	,@Rumus varchar(50)
AS

UPDATE REF_SKOM_REWARD
SET
	 Nama = @Nama
	,Inaktif = @Inaktif
	,SalesTipe = @SalesTipe
	,Dari = @Dari
	,Sampai = @Sampai
	,Rumus = @Rumus
WHERE NoSkema = @Nomor







GO
/****** Object:  StoredProcedure [dbo].[spSkomRewardTambah]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Tambah satu buah baris rumus reward
*/

CREATE PROCEDURE [dbo].[spSkomRewardTambah]
	 @Nomor int
	,@SalesLevel int
	,@Penjualan money = 0
	,@Reward varchar (100) = ''
AS

-- Nomor urut terbesar ditambahkan satu
DECLARE @Baris int
SELECT @Baris = ISNULL(MAX(SN),0) + 1 FROM REF_SKOM_REWARD_DETAIL
WHERE NoSkema = @Nomor

INSERT INTO REF_SKOM_REWARD_DETAIL
(
	 NoSkema
	,SN
	,SalesLevel
	,Penjualan
	,Reward
)
VALUES
(
	 @Nomor
	,@Baris
	,@SalesLevel
	,@Penjualan
	,@Reward
)










GO
/****** Object:  StoredProcedure [dbo].[spSkomRewardTambah2]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Tambah satu buah baris rumus reward
*/

CREATE PROCEDURE [dbo].[spSkomRewardTambah2]
	 @Nomor int
	,@SalesLevel int
	,@TipeTarget varchar(50) = ''
	,@TargetBawah money = 0
	,@TargetAtas money = 0
	,@Reward varchar (100) = ''
AS

-- Nomor urut terbesar ditambahkan satu
DECLARE @Baris int
SELECT @Baris = ISNULL(MAX(SN),0) + 1 FROM REF_SKOM_REWARD_DETAIL2
WHERE NoSkema = @Nomor

INSERT INTO REF_SKOM_REWARD_DETAIL2
(
	 NoSkema
	,SN
	,SalesLevel
	,TipeTarget
	,TargetBawah
	,TargetAtas
	,Reward
)
VALUES
(
	 @Nomor
	,@Baris
	,@SalesLevel
	,@TipeTarget
	,@TargetBawah
	,@TargetAtas
	,@Reward
)










GO
/****** Object:  StoredProcedure [dbo].[spSkomTambah]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Tambah satu buah baris rumus komisi
*/

CREATE PROCEDURE [dbo].[spSkomTambah]
	 @Nomor int
	,@SalesLevel int
	,@TipeTarif varchar (5) = ''
	,@Nominal money = 0
AS

-- Nomor urut terbesar ditambahkan satu
DECLARE @Baris int
SELECT @Baris = ISNULL(MAX(SN),0) + 1 FROM REF_SKOM_DETAIL
WHERE NoSkema = @Nomor

INSERT INTO REF_SKOM_DETAIL
(
	 NoSkema
	,SN
	,SalesLevel
	,TipeTarif
	,Nilai
)
VALUES
(
	 @Nomor
	,@Baris
	,@SalesLevel
	,@TipeTarif
	,@Nominal
)










GO
/****** Object:  StoredProcedure [dbo].[spSkomTambah2]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Tambah satu buah baris rumus komisi
*/

CREATE PROCEDURE [dbo].[spSkomTambah2]
	 @Nomor int
	,@SalesLevel int
	,@TipeTarget varchar(50) = ''
	,@TargetBawah money = 0
	,@TargetAtas money = 0
	,@TipeTarif varchar (5) = ''
	,@Nominal money = 0
AS

-- Nomor urut terbesar ditambahkan satu
DECLARE @Baris int
SELECT @Baris = ISNULL(MAX(SN),0) + 1 FROM REF_SKOM_DETAIL2
WHERE NoSkema = @Nomor

INSERT INTO REF_SKOM_DETAIL2
(
	 NoSkema
	,SN
	,SalesLevel
	,TipeTarget
	,TargetBawah
	,TargetAtas
	,TipeTarif
	,Nilai
)
VALUES
(
	 @Nomor
	,@Baris
	,@SalesLevel
	,@TipeTarget
	,@TargetBawah
	,@TargetAtas
	,@TipeTarif
	,@Nominal
)










GO
/****** Object:  StoredProcedure [dbo].[spSkomTermBaru]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Penambahan termin komisi baru
*/

CREATE PROCEDURE [dbo].[spSkomTermBaru]
	 @Nama varchar (100) = ''
	,@CaraBayar  varchar(50) = ''
	,@SalesTipe int
AS

-- Nomor urut terbesar ditambahkan satu
DECLARE @Nomor int
SELECT @Nomor = ISNULL(MAX(NoTermin),0) + 1 FROM REF_SKOM_TERM

INSERT INTO REF_SKOM_TERM
(
	 NoTermin
	,Nama	
	,CaraBayar
	,SalesTipe
)
VALUES
(
	 @Nomor
	,@Nama
	,@CaraBayar
	,@SalesTipe
)




GO
/****** Object:  StoredProcedure [dbo].[spSkomTermDel]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Delete termin komisi
*/

CREATE PROCEDURE [dbo].[spSkomTermDel]
	@Nomor int
AS

DELETE FROM REF_SKOM_TERM WHERE NoTermin = @Nomor










GO
/****** Object:  StoredProcedure [dbo].[spSkomTermEdit]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spSkomTermEdit]
	 @Nomor int
	 ,@Nama varchar (100) = ''
	 ,@Status bit
	 ,@CaraBayar varchar (50) = ''
	 ,@SalesTipe int
AS

UPDATE REF_SKOM_TERM
SET	  
      Nama = @Nama
	 ,Inaktif = @Status
	 ,CaraBayar = @CaraBayar
	 ,SalesTipe = @SalesTipe
WHERE 
	NoTermin = @Nomor



GO
/****** Object:  StoredProcedure [dbo].[spSkomTermTambah]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[spSkomTermTambah]
	  @Nomor int
	 ,@Nama varchar (100) = ''
	 ,@PersenCair money
	 ,@Lunas bit
	 ,@PersenLunas money
	 ,@BF bit
	 ,@PersenBF money
	 ,@DP bit
	 ,@PersenDP money
	 ,@ANG bit
	 ,@PersenANG money
	 ,@PPJB bit
	 ,@AJB bit
	 ,@AKAD bit
	 ,@TipeCair bit
	 ,@SalesLevel int
AS

-- Nomor urut terbesar ditambahkan satu
declare @baris int = (Select ISNULL(Max(SN),0) + 1 from REF_SKOM_TERM_DETAIL where NoTermin=@Nomor)
INSERT INTO REF_SKOM_TERM_DETAIL
(
	   [NoTermin]
	  ,[SN]
      ,[Nama]
      ,[PersenCair]
      ,[Lunas]
      ,[PersenLunas]
      ,[BF]
      ,[PersenBF]
      ,[DP]
      ,[PersenDP]
      ,[ANG]
      ,[PersenANG]
      ,[PPJB]
	  ,[AJB]
      ,[Akad]
      ,[TipeCair]
	  ,[SalesLevel]
)
VALUES
(
	  @Nomor
	 ,@baris
	 ,@Nama
	 ,@PersenCair
	 ,@Lunas 
	 ,@PersenLunas
	 ,@BF
	 ,@PersenBF
	 ,@DP
	 ,@PersenDP
	 ,@ANG
	 ,@PersenANG 
	 ,@PPJB 
	 ,@AJB
	 ,@AKAD 
	 ,@TipeCair
	 ,@SalesLevel
)




GO
/****** Object:  StoredProcedure [dbo].[spST]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Prosedur serah terima
*/

CREATE PROCEDURE [dbo].[spST]
	 @NoKontrak varchar(50)
	,@NoST varchar(20) = ''
	,@TglST datetime
AS

	INSERT INTO MS_BAST
	(
		NoKontrak
		,NoST
		,TglST
	)
	VALUES
	(
		@NoKontrak
		,@NoST
		,@TglST 
	)
	




GO
/****** Object:  StoredProcedure [dbo].[spTagihanBalance]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Proses untuk menghitung selisih out of balance
*/

CREATE PROCEDURE [dbo].[spTagihanBalance]
	@NoKontrak varchar (50)
AS

DECLARE @NilaiTagihan money

SELECT @NilaiTagihan = ISNULL(SUM(NilaiTagihan),0)
FROM MS_TAGIHAN WHERE NoKontrak = @NoKontrak
AND Tipe IN ('BF','DP','ANG')

UPDATE MS_KONTRAK SET
	OutBalance = NilaiKontrak - @NilaiTagihan
WHERE NoKontrak = @NoKontrak









GO
/****** Object:  StoredProcedure [dbo].[spTagihanDaftar]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Pendaftaran satu buah tagihan baru
*/

CREATE PROCEDURE [dbo].[spTagihanDaftar]
	 @NoKontrak varchar (50)
	,@NamaTagihan varchar (50)
	,@TglJT datetime
	,@NilaiTagihan money
	,@Tipe varchar (3)
AS

DECLARE @NoUrut int
SELECT @NoUrut = ISNULL(MAX(NoUrut),0) + 1 FROM MS_TAGIHAN
WHERE NoKontrak = @NoKontrak

INSERT INTO MS_TAGIHAN
(
	 NoKontrak
	,NoUrut
	,NamaTagihan
	,TglJT
	,NilaiTagihan
	,Tipe
)
VALUES
(
	 @NoKontrak
	,@NoUrut
	,@NamaTagihan
	,CONVERT(datetime, @TglJT, 101)
	,@NilaiTagihan
	,@Tipe
)

-- MS_KONTRAK.OutBalance
EXEC spTagihanBalance @NoKontrak

-- MS_KONTRAK.PersenLunas
EXEC spProsentasePelunasan @NoKontrak

-- Customer
DECLARE @NoCustomer int
SELECT @NoCustomer = NoCustomer FROM MS_KONTRAK WHERE NoKontrak = @NoKontrak
EXEC spTglTransaksi @NoCustomer









GO
/****** Object:  StoredProcedure [dbo].[spTagihanDaftar_Laporan]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
Pendaftaran satu buah tagihan baru untuk laporan reschedule
*/

CREATE PROCEDURE [dbo].[spTagihanDaftar_Laporan]
	 @NoApproval varchar (20)
	,@NoKontrak varchar (50)
	,@NamaTagihan varchar (50)
	,@TglJT datetime
	,@NilaiTagihan money
	,@Tipe varchar (3)
	,@TagihanKe int
	,@UserInput varchar(100)
	,@TglInput datetime
	,@Skema varchar(150)
	,@NilaiKontrak money
AS

DECLARE @NoUrut int
SELECT @NoUrut = ISNULL(MAX(NoUrut),0) + 1 FROM MS_TAGIHAN_LAPORAN
WHERE NoKontrak = @NoKontrak and TagihanKe = @TagihanKe

INSERT INTO MS_TAGIHAN_LAPORAN
(
	 NoApproval
	,NoKontrak
	,NoUrut
	,NamaTagihan
	,TglJT
	,NilaiTagihan
	,Tipe
	,TagihanKe
	,UserInput
	,TglInput
	,Skema
	,NilaiKontrak
)
VALUES
(
	 @NoApproval
	,@NoKontrak
	,@NoUrut
	,@NamaTagihan
	,CONVERT(datetime, @TglJT, 101)
	,@NilaiTagihan
	,@Tipe
	,@TagihanKe
	,@UserInput
	,CONVERT(datetime, @TglInput, 101)
	,@Skema
	,@NilaiKontrak
)







GO
/****** Object:  StoredProcedure [dbo].[spTagihanDaftarKPA]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Pendaftaran satu buah tagihan baru
*/

CREATE PROCEDURE [dbo].[spTagihanDaftarKPA]
	 @NoKontrak varchar (50)
	,@NamaTagihan varchar (50)
	,@TglJT datetime
	,@NilaiTagihan money
	,@Tipe varchar (10)
	,@NilaiTagihanTipe varchar (20)
	,@NilaiTagihanPersen money
AS

DECLARE @NoUrut int
SELECT @NoUrut = ISNULL(MAX(NoUrut),0) + 1 FROM MS_TAGIHAN_KPA
WHERE NoKontrak = @NoKontrak

INSERT INTO MS_TAGIHAN_KPA
(
	 NoKontrak
	,NoUrut
	,NamaTagihan
	,TglJT
	,NilaiTagihan
	,Tipe
	,NilaiTagihanTipe
	,NilaiTagihanPersen
)
VALUES
(
	 @NoKontrak
	,@NoUrut
	,@NamaTagihan
	,CONVERT(datetime, @TglJT, 101)
	,@NilaiTagihan
	,@Tipe
	,@NilaiTagihanTipe
	,@NilaiTagihanPersen
)










GO
/****** Object:  StoredProcedure [dbo].[spTagihanDaftarM]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*  
Pendaftaran satu buah tagihan baru (migrate)
*/  
  
CREATE PROCEDURE [dbo].[spTagihanDaftarM]  
  @NoKontrak varchar (50)  
 ,@NamaTagihan varchar (50)  
 ,@TglJT datetime  
 ,@NilaiTagihan money  
 ,@Denda money  
 ,@Tipe varchar (3)  
 ,@KPR bit
AS  
  
DECLARE @NoUrut int  
SELECT @NoUrut = ISNULL(MAX(NoUrut),0) + 1 FROM MS_TAGIHAN  
WHERE NoKontrak = @NoKontrak   
  
INSERT INTO MS_TAGIHAN  
(  
  NoKontrak  
 ,NoUrut  
 ,NamaTagihan  
 ,TglJT  
 ,NilaiTagihan  
 ,Denda
 ,Tipe  
 ,KPR
)  
VALUES  
(  
  @NoKontrak  
 ,@NoUrut  
 ,@NamaTagihan  
 ,CONVERT(datetime, @TglJT, 101)  
 ,@NilaiTagihan  
 ,@Denda
 ,@Tipe  
 ,@KPR
)  
  
-- MS_KONTRAK.OutBalance  
EXEC spTagihanBalance @NoKontrak  
  
-- MS_KONTRAK.PersenLunas  
EXEC spProsentasePelunasan @NoKontrak  
  
-- Customer  
DECLARE @NoCustomer int  
SELECT @NoCustomer = NoCustomer FROM MS_KONTRAK WHERE NoKontrak = @NoKontrak  
EXEC spTglTransaksi @NoCustomer








GO
/****** Object:  StoredProcedure [dbo].[spTagihanDaftarTEMP]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
Pendaftaran satu buah tagihan baru
*/

CREATE PROCEDURE [dbo].[spTagihanDaftarTEMP]
	 @NoKontrak varchar (50)
	,@NamaTagihan varchar (50)
	,@TglJT datetime
	,@NilaiTagihan money
	,@Tipe varchar (3)
	--,@SN int
AS

DECLARE @NoUrut int
SELECT @NoUrut = ISNULL(MAX(NoUrut),0) + 1 FROM MS_TAGIHAN_TEMP
WHERE NoKontrak = @NoKontrak

INSERT INTO MS_TAGIHAN_TEMP
(
	 NoKontrak
	,NoUrut
	,NamaTagihan
	,TglJT
	,NilaiTagihan
	,Tipe
	--,SN
)
VALUES
(
	 @NoKontrak
	,@NoUrut
	,@NamaTagihan
	,CONVERT(datetime, @TglJT, 101)
	,@NilaiTagihan
	,@Tipe
	--,@SN
)

------ MS_KONTRAK.OutBalance
--EXEC spTagihanBalance @NoKontrak

------ MS_KONTRAK.PersenLunas
--EXEC spProsentasePelunasan @NoKontrak

---- Customer
DECLARE @NoCustomer int
SELECT @NoCustomer = NoCustomer FROM MS_KONTRAK WHERE NoKontrak = @NoKontrak
EXEC spTglTransaksi @NoCustomer








GO
/****** Object:  StoredProcedure [dbo].[spTagihanDaftarTempRE]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/*
Pendaftaran satu buah tagihan baru
*/

CREATE PROCEDURE [dbo].[spTagihanDaftarTempRE]
	 @NoApproval varchar(20)
	,@NoKontrak varchar(50)
	,@TglPengajuan datetime
	,@SkemaBef varchar (150)
	,@SkemaAft varchar (150)
	,@CaraBayarBef varchar (50)
	,@CaraBayarAft varchar (50)
AS

INSERT INTO MS_APPROVAL_RESCHEDULE
(
	 NoApproval
	,NoKontrak
	,TglPengajuan
	,SkemaBef
	,SkemaAft
	,CaraBayarBef
	,CaraBayarAft
)
VALUES
(
	 @NoApproval
	,@NoKontrak
	,@TglPengajuan
	,@SkemaBef
	,@SkemaAft
	,@CaraBayarBef
	,@CaraBayarAft
)
GO
/****** Object:  StoredProcedure [dbo].[spTagihanDel]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Hapus tagihan PERMANEN
*/

CREATE PROCEDURE [dbo].[spTagihanDel]
	 @NoKontrak varchar (50)
	,@NoUrut int
AS

DELETE FROM MS_TAGIHAN
WHERE NoKontrak = @NoKontrak AND NoUrut = @NoUrut

-- Reorganize nomor urut
UPDATE MS_TAGIHAN SET
	NoUrut = NoUrut - 1
WHERE
	NoKontrak = @NoKontrak
	AND NoUrut > @NoUrut
	
-- MS_KONTRAK.OutBalance
EXEC spTagihanBalance @NoKontrak

-- MS_KONTRAK.PersenLunas
EXEC spProsentasePelunasan @NoKontrak

-- Unallocated
UPDATE MS_PELUNASAN SET NoTagihan = 0
WHERE NoKontrak = @NoKontrak AND NoTagihan = @NoUrut
UPDATE MS_PELUNASAN SET NoTagihan = NoTagihan - 1
WHERE NoKontrak = @NoKontrak AND NoTagihan > @NoUrut

-- Customer
DECLARE @NoCustomer int
SELECT @NoCustomer = NoCustomer FROM MS_KONTRAK WHERE NoKontrak = @NoKontrak
EXEC spTglTransaksi @NoCustomer









GO
/****** Object:  StoredProcedure [dbo].[spTagihanDelKPA]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Hapus tagihan PERMANEN
*/

CREATE PROCEDURE [dbo].[spTagihanDelKPA]
	 @NoKontrak varchar (50)
	,@NoUrut int
AS

DELETE FROM MS_TAGIHAN_KPA
WHERE NoKontrak = @NoKontrak AND NoUrut = @NoUrut

-- Reorganize nomor urut
UPDATE MS_TAGIHAN_KPA SET
	NoUrut = NoUrut - 1
WHERE
	NoKontrak = @NoKontrak
	AND NoUrut > @NoUrut
	


-- Unallocated
UPDATE MS_PELUNASAN_KPA SET NoTagihan = 0
WHERE NoKontrak = @NoKontrak AND NoTagihan = @NoUrut
UPDATE MS_PELUNASAN_KPA SET NoTagihan = NoTagihan - 1
WHERE NoKontrak = @NoKontrak AND NoTagihan > @NoUrut












GO
/****** Object:  StoredProcedure [dbo].[spTagihanEdit]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Edit satu buah baris tagihan
*/

CREATE PROCEDURE [dbo].[spTagihanEdit]
	 @NoKontrak varchar (50)
	,@NoUrut int
	,@NamaTagihan varchar (50)
	,@TglJT datetime
	,@NilaiTagihan money
	,@Tipe varchar (3)
AS

UPDATE MS_TAGIHAN
SET
	 NamaTagihan = @NamaTagihan
	,TglJT = CONVERT(datetime, @TglJT, 101)
	,NilaiTagihan = @NilaiTagihan
	,Tipe = @Tipe
WHERE NoKontrak = @NoKontrak AND NoUrut = @NoUrut

-- MS_KONTRAK.OutBalance
EXEC spTagihanBalance @NoKontrak

-- MS_KONTRAK.PersenLunas
EXEC spProsentasePelunasan @NoKontrak

-- Customer
DECLARE @NoCustomer int
SELECT @NoCustomer = NoCustomer FROM MS_KONTRAK WHERE NoKontrak = @NoKontrak
EXEC spTglTransaksi @NoCustomer









GO
/****** Object:  StoredProcedure [dbo].[spTagihanEditKPA]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Edit satu buah baris tagihan
*/

CREATE PROCEDURE [dbo].[spTagihanEditKPA]
	 @NoKontrak varchar (50)
	,@NoUrut int
	,@NamaTagihan varchar (50)
	,@TglJT datetime
	,@NilaiTagihan money
	,@Tipe varchar (10)
	,@NilaiTagihanTipe varchar (20)
	,@NilaiTagihanPersen money

AS

UPDATE MS_TAGIHAN_KPA
SET
	 NamaTagihan = @NamaTagihan
	,TglJT = CONVERT(datetime, @TglJT, 101)
	,NilaiTagihan = @NilaiTagihan
	,Tipe = @Tipe
	,NilaiTagihanTipe = @NilaiTagihanTipe
	,NilaiTagihanPersen = @NilaiTagihanPersen
WHERE NoKontrak = @NoKontrak AND NoUrut = @NoUrut











GO
/****** Object:  StoredProcedure [dbo].[spTagihanHeaderTemp]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
Pendaftaran header tagihan
*/

CREATE PROCEDURE [dbo].[spTagihanHeaderTemp]
	 @NoKontrak varchar (50)
	,@Skema varchar(150)
	,@NoCustomer int
	,@NoUnit varchar(20)
	,@NoAgent int
	,@CaraBayar varchar(50)
	--,@Revisi int
	,@RefSkema int
	--,@TglReschedule datetime
	--,@TglApproval datetime
AS

INSERT INTO MS_TAGIHAN_HEADER
(
	 NoKontrak
	,Skema
	,NoCustomer
	,NoUnit
	,NoAgent
	,CaraBayar
	--,Revisi
	,RefSkema
	--,TglReschedule
	--,TglApproval
)

VALUES
(
	 @NoKontrak
	,@Skema
	,@NoCustomer
	,@NoUnit
	,@NoAgent
	,@CaraBayar
	--,@Revisi
	,@RefSkema
	--,@TglReschedule
	--,@TglApproval	 
)




GO
/****** Object:  StoredProcedure [dbo].[spTagihanReset]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Hapus semua tagihan PERMANEN
*/

CREATE PROCEDURE [dbo].[spTagihanReset]
	@NoKontrak varchar(50)
AS

-- validasi status
DECLARE @Status varchar(1)
SELECT @Status = Status
FROM MS_KONTRAK WHERE NoKontrak = @NoKontrak
IF @Status <> 'A'
	RETURN
	
DELETE FROM MS_TAGIHAN
WHERE NoKontrak = @NoKontrak

-- MS_KONTRAK.OutBalance
EXEC spTagihanBalance @NoKontrak

-- MS_KONTRAK.PersenLunas
EXEC spProsentasePelunasan @NoKontrak

-- Unallocated
UPDATE MS_PELUNASAN SET NoTagihan = 0 WHERE NoKontrak = @NoKontrak

-- Customer
DECLARE @NoCustomer int
SELECT @NoCustomer = NoCustomer FROM MS_KONTRAK WHERE NoKontrak = @NoKontrak
EXEC spTglTransaksi @NoCustomer









GO
/****** Object:  StoredProcedure [dbo].[spTglTransaksi]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Prosedur untuk memperbaharui tanggal transaksi seorang customer
setiap kali terjadi transaksi marketing
*/

CREATE PROCEDURE [dbo].[spTglTransaksi]
	@NoCustomer int
AS

UPDATE MS_CUSTOMER
	SET TglTransaksi = GETDATE()
WHERE NoCustomer = @NoCustomer









GO
/****** Object:  StoredProcedure [dbo].[spTTREdit]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spTTREdit]
	 @NoTTR VARCHAR(20)
	,@TglTTR datetime
	,@Unit varchar(100)
	,@Customer varchar(100)
	,@Ket varchar(200)
	,@NoBG varchar(20)
	,@TglBG datetime
	,@ManualTTR VARCHAR(50)
AS

UPDATE MS_TTR
SET
	 TglTTR = CONVERT(DATETIME, @TglTTR, 101)
	,Unit = @Unit
	,Customer = @Customer
	,Ket = @Ket
	,NoBG = @NoBG
	,TglBG = CONVERT(DATETIME, @TglBG, 101)
	,ManualTTR = @ManualTTR
WHERE
NoTTR = @NoTTR









GO
/****** Object:  StoredProcedure [dbo].[spTTRRegistrasi]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spTTRRegistrasi]
	@NoTTR VARCHAR(20)
	, @NoReservasi VARCHAR(20)
	, @UserID VARCHAR(20)
	, @IP VARCHAR(50)
	, @Unit VARCHAR(100)
	, @Customer VARCHAR(100)
	, @CaraBayar VARCHAR(2)
	, @Ket VARCHAR(200)
	, @Total MONEY
	, @NoBG VARCHAR(20)
	, @TglBG DATETIME
AS
	INSERT INTO MS_TTR
	(
		NoTTR
		, NoReservasi
		, UserID
		, IP
		, Unit
		, Customer
		, CaraBayar
		, Ket
		, Total
		, NoBG
		, TglBG
	)
	VALUES
	(
		@NoTTR
		, @NoReservasi
		, @UserID
		, @IP
		, @Unit
		, @Customer
		, @CaraBayar
		, @Ket
		, @Total
		, @NoBG
		, @TglBG
	)









GO
/****** Object:  StoredProcedure [dbo].[spTTRVoid]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spTTRVoid]
	 @NoTTR VARCHAR(20)
AS

DECLARE
	 @Status varchar(4)
SELECT
	 @Status = Status
FROM MS_TTR WHERE NoTTR = @NoTTR

-- validasi status
IF @Status <> 'BARU'
	RETURN

UPDATE MS_TTR SET
	 Total = 0
	,Status = 'VOID'
	, NoReservasi = ''
WHERE NoTTR = @NoTTR









GO
/****** Object:  StoredProcedure [dbo].[spUnitDaftar]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Pendaftaran unit baru (PEMILIK)
*/

CREATE PROCEDURE [dbo].[spUnitDaftar]
	 @NoStock varchar(20)
	,@Jenis varchar(20) = ''
	,@Lokasi varchar(20) = ''
	,@NoUnit varchar(20) = ''
	,@Luas money = 0
	,@Lantai varchar(20) = ''
	,@Nomor varchar(20) = ''
	,@Project varchar(20) = ''
AS

-- kontrol duplikat
IF EXISTS(SELECT * FROM MS_UNIT WHERE NoUnit = @NoUnit)
	RETURN

INSERT INTO MS_UNIT
(
	 NoStock
	,Jenis
	,Lokasi
	,NoUnit
	,Luas
	,Lantai
	,Nomor
	,Project
)
VALUES
(
	 @NoStock
	,@Jenis
	,@Lokasi
	,@NoUnit
	,@Luas
	,@Lantai
	,@Nomor
	,@Project
)




GO
/****** Object:  StoredProcedure [dbo].[spUnitDaftar2]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[spUnitDaftar2]
	 @NoUnit Varchar(20)
AS
--IF NOT EXISTS(SELECT * FROM ISC064_MARKETINGJUAL..MS_UNIT
--      WHERE NoStock  = @NoUnit)
--INSERT ISC064_MARKETINGJUAL..MS_UNIT SELECT * FROM ISC064_MARKETINGJUAL..MS_UNIT 
--WHERE ISC064_MARKETINGJUAL..MS_UNIT.NoStock  = @NoUnit
--SET QUOTED_IDENTIFIER OFF









GO
/****** Object:  StoredProcedure [dbo].[spUnitDel]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Hapus unit PERMANEN
*/

CREATE PROCEDURE [dbo].[spUnitDel]
	@NoStock varchar(20)
AS

-- Unit tidak boleh dihapus jika sudah pernah dikontrak
IF EXISTS (SELECT NoStock FROM MS_RESERVASI WHERE NoStock = @NoStock)
	RETURN
IF EXISTS (SELECT NoStock FROM MS_KONTRAK WHERE NoStock = @NoStock)
	RETURN

DELETE FROM MS_UNIT WHERE NoStock = @NoStock









GO
/****** Object:  StoredProcedure [dbo].[spUnitEdit]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Edit data unit
*/

CREATE PROCEDURE [dbo].[spUnitEdit]
	 @NoStock varchar(20)
	,@Jenis varchar(20) = ''
	,@Lokasi varchar(20) = ''
	,@NoUnit varchar(20) = ''
	,@Luas money = 0
	,@Lantai varchar(20) = ''
	,@Nomor varchar(20) = ''
AS

-- kontrol duplikat
IF EXISTS(SELECT * FROM MS_UNIT WHERE NoUnit = @NoUnit AND NoStock <> @NoStock)
	RETURN

UPDATE MS_UNIT
SET
	 Jenis = @Jenis
	,Lokasi = @Lokasi
	,NoUnit = @NoUnit
	,Luas = @Luas
	,Lantai = @Lantai
	,Nomor = @Nomor
	,TglEdit = GETDATE()
WHERE NoStock = @NoStock

-- Price list
DECLARE @FlagSPL int
SELECT @FlagSPL = FlagSPL FROM MS_UNIT WHERE NoStock = @NoStock
IF @FlagSPL = 1
	UPDATE MS_UNIT SET FlagSPL = 2 WHERE NoStock = @NoStock









GO
/****** Object:  StoredProcedure [dbo].[spUnitEditKoordinat]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Edit data koordinat
*/

CREATE PROCEDURE [dbo].[spUnitEditKoordinat]
	 @NoUnit varchar(20)
	,@Peta varchar(100) = ''
	,@Koordinat varchar(255) = ''
AS

UPDATE MS_UNIT
SET
	 Peta = @Peta
	,Koordinat = @Koordinat
WHERE NoUnit = @NoUnit










GO
/****** Object:  StoredProcedure [dbo].[spUnitEditSpek]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
Edit spesifikasi
*/

CREATE PROCEDURE [dbo].[spUnitEditSpek]
	 @NoStock varchar(20)
	,@Zoning varchar(100) = ''
	,@Panjang money = 0
	,@Lebar money = 0
	,@Tinggi money = 0
	,@LuasSG money = 0
	,@LuasNett money = 0
	,@HadapAtrium bit = 0
	,@HadapEntrance bit = 0
	,@HadapEskalator bit = 0
	,@HadapLift bit = 0
	,@HadapParkir bit = 0
	,@HadapAxis bit = 0
	,@Hook bit = 0
	,@LebarJalan money = 0
	,@Outdoor bit = 0
	,@ArahHadap varchar(50) = ''
	,@Panorama varchar(100) = ''
	,@JenisProperti varchar(50) = ''
	,@Kategori varchar(100) = ''
AS

UPDATE MS_UNIT
SET
	 Zoning = @Zoning
	,Panjang = @Panjang
	,Lebar = @Lebar
	,Tinggi = @Tinggi
	,LuasSG = @LuasSG
	,LuasNett = @LuasNett
	,HadapAtrium = @HadapAtrium
	,HadapEntrance = @HadapEntrance
	,HadapEskalator = @HadapEskalator
	,HadapLift = @HadapLift
	,HadapParkir = @HadapParkir
	,HadapAxis = @HadapAxis
	,Hook = @Hook
	,LebarJalan = @LebarJalan
	,Outdoor = @Outdoor
	,ArahHadap = @ArahHadap
	,Panorama = @Panorama
	,JenisProperti = @JenisProperti
	,Kategori = @Kategori
WHERE NoStock = @NoStock










GO
/****** Object:  StoredProcedure [dbo].[spUnitEditStatus]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Edit status
*/

CREATE PROCEDURE [dbo].[spUnitEditStatus]
	 @NoStock varchar(20)
	,@Status varchar(1)
AS

-- sudah terjual tidak bisa edit status
IF EXISTS(SELECT * FROM MS_KONTRAK WHERE NoStock = @NoStock AND Status = 'A')
	RETURN

UPDATE MS_UNIT
SET
	 Status = @Status
WHERE NoStock = @NoStock









GO
/****** Object:  StoredProcedure [dbo].[spUnitGantiKey]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Prosedur ganti primary key
*/

CREATE PROCEDURE [dbo].[spUnitGantiKey]
	 @Lama varchar(20)
	,@Baru varchar(20)
AS

UPDATE MS_UNIT SET
	 NoStock = @Baru
	,TglEdit = GETDATE()
WHERE
	NoStock = @Lama

-- Normalisasi primary key
UPDATE MS_UNIT_LOG SET Pk = @Baru WHERE Pk = @Lama









GO
/****** Object:  StoredProcedure [dbo].[spUnitPriceList]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Update price list unit
*/

CREATE PROCEDURE [dbo].[spUnitPriceList]
	 @NoStock varchar(20)
	,@PriceListMin money
	,@PriceList money
AS

UPDATE MS_UNIT SET
	 PriceListMin = @PriceListMin
	,PriceList = @PriceList
	,FlagSPL = 1
	,TglEdit = GETDATE()
WHERE NoStock = @NoStock









GO
/****** Object:  StoredProcedure [dbo].[spUnitPriceListSkema]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/*  
Create by : Ronald (8 Jan 2013)
Function : Insert / Update Price List Unit Mengikat Skema
*/  
  
CREATE PROCEDURE [dbo].[spUnitPriceListSkema]  
(
  @NoStock varchar(20)  
 ,@NoSkema int  
 ,@PriceList money  
)

AS  

DECLARE @FlagNoStock INT = 0
DECLARE @FlagNoSkema INT = 0
DECLARE @NomorID INT

IF EXISTS ( SELECT * FROM MS_UNIT WHERE NoStock = @NoStock ) BEGIN SET @FlagNoStock = 1 END
IF EXISTS ( SELECT * FROM REF_SKEMA WHERE Nomor = @NoSkema ) BEGIN SET @FlagNoSkema = 1 END

IF (@FlagNoStock = 1) AND (@FlagNoSkema = 1)
BEGIN
IF EXISTS ( SELECT * FROM MS_PRICELIST WHERE NoStock = @NoStock AND NoSkema = @NoSkema )
BEGIN
    UPDATE MS_PRICELIST SET  
      PriceList = @PriceList 
     ,Tgl = GETDATE()  
    WHERE NoStock = @NoStock AND NoSkema = @NoSkema  
END
ELSE
BEGIN
    SET @NomorID = ( SELECT ISNULL(MAX(Nomor)+1,1) FROM MS_PRICELIST )

    INSERT INTO MS_PRICELIST (Nomor, NoStock, NoSkema, PriceList, Tgl) VALUES
    (  
      @NomorID
     ,@NoStock
     ,@NoSkema
     ,@PriceList
     ,GETDATE()
    )
END
END

/*
GRANT EXECUTE ON AM073_MARKETINGJUAL..[spUnitPriceListSkema] TO [batavianet]
*/





GO
/****** Object:  StoredProcedure [dbo].[spValidasiDaftar]    Script Date: 05/04/2019 15.51.15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Logika untuk mengecek apakah satu buah unit boleh disewakan atau tidak
Sifat : INSERT
*/

CREATE PROCEDURE [dbo].[spValidasiDaftar]
	 @NoStock varchar(20)
	,@Validasi varchar(3) OUTPUT
AS

DECLARE
	@Jml int

-- Periksa setiap kontrak yang aktif -> apakah ada double booking?
SELECT @Jml = COUNT(NoKontrak) FROM MS_KONTRAK 
WHERE NoStock = @NoStock AND Status <> 'B'

IF @Jml = 0
	SET @Validasi = 'OK'
ELSE -- Sudah ada orang lain
	SET @Validasi = 'DBL'









GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0=Cimahi;1 = Jakarta' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MS_KONTRAK', @level2type=N'COLUMN',@level2name=N'JenisPenjualan'
GO
USE [master]
GO
ALTER DATABASE [ISC064_MARKETINGJUAL] SET  READ_WRITE 
GO
