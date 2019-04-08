<%@ Page language="c#" Inherits="ISC064.SECURITY.Lap" CodeFile="Lap.aspx.cs" %>
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
		</style>
	</head>
	<body class="body-padding">
		<form id="Form1" method="post" runat="server" class="cnt">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<h1 class="title title-line">Laporan</h1>
			<br>
			<div>
				<div class="box-list">
					<a href='javascript:openLaporan("Laporan/SecurityLog.aspx")'>
						<div class="box-content">
							<span>Laporan</span>
							<p>Security Log</p>
						</div>
					</a>
				</div>
				<div class="box-list">
					<a href='javascript:openLaporan("Laporan/TabelAbsensi.aspx")'>
						<div class="box-content">
							<span>Laporan</span>
							<p>Tabel Absensi</p>
						</div>
					</a>
				</div>
				<div class="box-list">
					<a href='javascript:openLaporan("Laporan/DaftarUserName.aspx")'>
						<div class="box-content">
							<span>Laporan</span>
							<p>Daftar User</p>
						</div>
					</a>
				</div>

			</div>
		</form>
	</body>
</html>
