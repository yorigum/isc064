<%@ Page language="c#" Inherits="ISC064.FINANCEAR.Lap" CodeFile="Lap.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Laporan</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
    	<link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Laporan">
		<style type="text/css">
			.list td
			{
				width:220px;
				padding-left:50px;
			}
			h2{font:bold 10pt}
			.title:after{
				content: '';
				display: block;
				height: 2px;
				width: 100%;
				background: #5c9bd1;
				margin-top:5px;
			}
		</style>
	</head>
	<body class="body-padding">
		<form id="Form1" method="post" runat="server" class="cnt">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<h1 class="title title-line">Laporan</h1>
			<br>
            <div class="box-list">
                <a href='javascript:openLaporan("Laporan/MasterTTS.aspx")' >
                    <div class="box-content">
                        <span>Laporan</span>
                        <p>Master TTS</p>
                    </div>
                </a>
            </div>
            <div class="box-list">
                <a href='javascript:openLaporan("Laporan/LaporanKasir.aspx")' >
                    <div class="box-content">
                        <span>Laporan</span>
                        <p>Kasir</p>
                    </div>
                </a>
            </div>
            <div class="box-list">
                <a href='javascript:openLaporan("Laporan/VoidHarian.aspx")' >
                    <div class="box-content">
                        <span>Laporan</span>
                        <p>Void Harian</p>
                    </div>
                </a>
            </div>
            <div class="box-list">
                <a href='javascript:openLaporan("Laporan/StokBG.aspx")' >
                    <div class="box-content">
                        <span>Laporan</span>
                        <p>Stok Cek Giro</p>
                    </div>
                </a>
            </div>
            <div class="box-list">
                <a href='javascript:openLaporan("Laporan/StokBGAll.aspx")' >
                    <div class="box-content">
                        <span>Laporan</span>
                        <p>Stok Cek Giro Tahunan</p>
                    </div>
                </a>
            </div>
            <div class="box-list">
                <a href='javascript:openLaporan("Laporan/LaporanPajak.aspx")' >
                    <div class="box-content">
                        <span>Laporan</span>
                        <p>Pajak</p>
                    </div>
                </a>
            </div>
            <div class="box-list">
                <a href='javascript:openLaporan("Laporan/LaporanDetilPembayaran.aspx")' >
                    <div class="box-content">
                        <span>Laporan</span>
                        <p>Detail Pembayaran</p>
                    </div>
                </a>
            </div>
            <div class="box-list">
                <a href='javascript:openLaporan("Laporan/LaporanTransferAnonim.aspx")' >
                    <div class="box-content">
                        <span>Laporan</span>
                        <p>Transfer Anonim</p>
                    </div>
                </a>
            </div>
            <div class="box-list">
                <a href='javascript:openLaporan("Laporan/LaporanRealisasiSales.aspx")' >
                    <div class="box-content">
                        <span>Laporan</span>
                        <p>Realisasi Sales & Cash In</p>
                    </div>
                </a>
            </div>
            <div class="box-list">
                <a href='javascript:openLaporan("Laporan/eFaktur.aspx")' >
                    <div class="box-content">
                        <span>Laporan</span>
                        <p>e-Faktur</p>
                    </div>
                </a>
            </div>			
		</form>
	</body>
</html>
