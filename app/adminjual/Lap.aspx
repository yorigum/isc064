<%@ Page language="c#" Inherits="ISC064.ADMINJUAL.Lap" CodeFile="Lap.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
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
				/*background: #5c9bd1;*/
				margin-top:5px;
			}
		</style>
	</head>
	<body class="body-padding">
		<form id="Form1" method="post" runat="server" class="cnt">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<h1 class="title title-line">Laporan</h1>
			<br>
			<div>
				<div class="box-list">
					<a href='javascript:openLaporan("Laporan/MasterAgent.aspx")'>
						<div class="box-content">
							<span>Laporan</span>
							<p>Master Marketing</p>
						</div>
					</a>
				</div>
<%--				<div class="box-list">
					<a href='javascript:openLaporan("Laporan/MasterKomisi.aspx")'>
						<div class="box-content">
							<span>Laporan</span>
							<p>Laporan Master Komisi</p>
						</div>
					</a>
				</div>--%>
				<div class="box-list">
					<a href='javascript:openLaporan("Laporan/PriceList.aspx")'>
						<div class="box-content">
							<span>Laporan</span>
							<p>Price List</p>
						</div>
					</a>
				</div>
				<div class="box-list">
					<a href='javascript:openLaporan("Laporan/HistoryPriceList.aspx")'>
						<div class="box-content">
							<span>Laporan</span>
							<p>History Price List</p>
						</div>
					</a>
				</div>
				<div class="box-list">
					<a href='javascript:openLaporan("Laporan/SummaryStockPerTipe.aspx")'>
						<div class="box-content">
							<span>Laporan</span>
							<p>Summary Stock Per Tipe</p>
						</div>
					</a>
				</div>
                <div class="box-list">
					<a href='javascript:openLaporan("Laporan/LaporanMasterUnit.aspx")'>
						<div class="box-content">
							<span>Laporan</span>
							<p>Master Unit</p>                            
						</div>
					</a>
				</div>
			</div>
		</form>
	</body>
</html>
