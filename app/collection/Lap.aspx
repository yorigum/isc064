<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<%@ Page language="c#" Inherits="ISC064.COLLECTION.Lap" CodeFile="Lap.aspx.cs" %>
<!DOCTYPE html>
<html lang="en">
	<head>
		<title>Laporan</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
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
			<h1 class="title">Laporan</h1>
			<br />
            <div class="box-list">
                <a href='javascript:openLaporan("Laporan/MasterTagihan.aspx")'>
                    <div class="box-content">
                        <span>Laporan</span>
                        <p> Master Tagihan</p>
                    </div>
                </a>
            </div>
            <div class="box-list">
                <a href='javascript:openLaporan("Laporan/PenerimaanCustomer.aspx")'>
                    <div class="box-content">
                        <span>Laporan</span>
                        <p>Penerimaan Customer</p>
                    </div>
                </a>
            </div>
            <div class="box-list">
                <a href='javascript:openLaporan("Laporan/LapTunggakan.aspx")'>
                    <div class="box-content">
                        <span>Laporan</span>
                        <p>Tunggakan</p>
                    </div>
                </a>
            </div>
            <div class="box-list">
                <a href='javascript:openLaporan("Laporan/LapJT.aspx")'>
                    <div class="box-content">
                        <span>Laporan</span>
                        <p>Jatuh Tempo</p>
                    </div>
                </a>
            </div>
            <div class="box-list">
                <a href='javascript:openLaporan("Laporan/OutstandingTagihan.aspx")'>
                    <div class="box-content">
                        <span>Laporan</span>
                        <p>Outstanding Tagihan</p>
                    </div>
                </a>
            </div>
            <div class="box-list">
                <a href='javascript:openLaporan("Laporan/AgingPiutang.aspx")'>
                    <div class="box-content">
                        <span>Laporan</span>
                        <p>Aging Tagihan</p>
                    </div>
                </a>
            </div>
            <div class="box-list">
                <a href='javascript:openLaporan("Laporan/LaporanKartuPiutang2.aspx")'>
                    <div class="box-content">
                        <span>Laporan</span>
                        <p>Kartu Piutang</p>
                    </div>
                </a>
            </div>
            <div class="box-list">
                <a href='javascript:openLaporan("Laporan/Col.aspx")'>
                    <div class="box-content">
                        <span>Laporan</span>
                        <p>Proyeksi Penerimaan</p>
                    </div>
                </a>
            </div>
            <div class="box-list">
                <a href='javascript:openLaporan("Laporan/Col2.aspx")'>
                    <div class="box-content">
                        <span>Laporan</span>
                        <p>Collection</p>
                    </div>
                </a>
            </div>
            <div class="box-list">
                <a href='javascript:openLaporan("Laporan/DaftarTagihan.aspx")'>
                    <div class="box-content">
                        <span>Laporan</span>
                        <p>Daftar Piutang</p>
                    </div>
                </a>
            </div>
            <div class="box-list">
                <a href='javascript:openLaporan("Laporan/DendaCustomer.aspx")'>
                    <div class="box-content">
                        <span>Laporan</span>
                        <p>Denda Customer</p>
                    </div>
                </a>
            </div>
		</form>
	</body>
</html>
