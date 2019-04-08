<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.ReservasiDaftar3" CodeFile="ReservasiDaftar3.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Pendaftaran Reservasi</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Reservasi - Pendaftaran Reservasi (Hal. 3)">
	</head>
	<body class="body-padding" onkeyup="if(event.keyCode==27)document.getElementById('cancel').click()">
		<form id="Form1" method="post" runat="server" class="cnt">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<h1 class="title title-line">Pendaftaran Reservasi</h1>
			<p><b><i>Halaman 4 dari 4</i></b></p>
			<br>
			<br>
			<h2 style="color:brown;border:1px solid silver;padding:10px">
				Reservasi Selesai
			</h2>
			<br>
			<table cellspacing="5">
				<tr>
					<td>No. Reservasi</td>
					<td>:</td>
					<td><asp:label id="no" runat="server" font-bold="True"></asp:label></td>
					<td rowspan="4" valign="top" style="padding-left:20px">
						No. Urut :<br>
						<asp:label id="nourut" runat="server" font-bold="True" font-size="30"></asp:label>
					</td>
				</tr>
				<tr>
					<td>Unit</td>
					<td>:</td>
					<td>
						<asp:label id="unit" runat="server" font-bold="True"></asp:label>
					</td>
				</tr>
				<tr>
					<td>Customer</td>
					<td>:</td>
					<td>
						<asp:label id="customer" runat="server" font-bold="True"></asp:label>
					</td>
				</tr>
				<tr>
					<td>Sales</td>
					<td>:</td>
					<td>
						<asp:label id="agent" runat="server" font-bold="True"></asp:label>
					</td>
				</tr>
			</table>
			<br />
			<h1><a id="attr" runat="server">Print TTR</a></h1>
            <br />
            <h1><a id="atts" runat="server">Print TTS</a></h1>
			<br />
			<h1><a id="boform" runat="server">Print Booking Form</a></h1>
			<br>
			<p style="padding:5px"><b>Prosedur Lanjutan :</b></p>
			<ul>
				<li>
					<a style="font:bold 10 pt" id="aKontrak" runat="server">Surat Pesanan</a>
					<br>
					Mutasi reservasi menjadi surat pesanan / kontrak
				</li>
			</ul>
			<br>
			<table height="50">
				<tr>
					<td>
						<input id="cancel" onclick="location.href='ReservasiDaftar.aspx'" type="button" class="btn btn-blue"
							value="OK" style="width:75px">
					</td>
					<td>
						<p class="feed">
							<asp:label id="feed" runat="server"></asp:label>
						</p>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>
