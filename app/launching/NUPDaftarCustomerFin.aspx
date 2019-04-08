<%@ Page language="c#" Inherits="ISC064.LAUNCHING.NUPDaftarCustomerFin" CodeFile="NUPDaftarCustomerFin.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>NUP - Registrasi Customer</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="NUP - Registrasi Customer (Hal. 2)">
	</head>
	<body>
		<form id="Form1" method="post" runat="server" class="cnt">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<h1>Registrasi NUP</h1>
            <p style="font-size:8pt; color:#666;">Halaman 2 dari 2</p>
			<br/>
			<br/>
			<h2 style="color:Brown;border:1 solid silver;padding:10">
				Pendaftaran NUP Berhasil
			</h2>
			<br/>
			<table cellspacing="5">
				<tr>
					<td>NUP</td>
					<td>:</td>
					<td>
						<asp:label id="nonup" runat="server" font-bold="True"></asp:label>
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
					<td>Agent</td>
					<td>:</td>
					<td>
						<asp:label id="agent" runat="server" font-bold="True"></asp:label>
					</td>
				</tr>
			</table>
			<br />
			<h1><a id="asp" runat="server">Print Form NUP</a></h1>
			<br />
			<p style="padding:5px">
			    <b>Daftar NUP Baru :</b>
			</p>
			<ul>
			    <li>
					<a style="FONT-WEIGHT:bold; FONT-SIZE:10pt; LINE-HEIGHT:normal; FONT-STYLE:normal; FONT-VARIANT:normal" id="adaftar" runat="server">Daftar</a>
					<br/>
					    Registrasi NUP baru.
					<br/>
					<br/>
				</li>
				<%--<li>
					<a style="FONT-WEIGHT:bold; FONT-SIZE:10pt; LINE-HEIGHT:normal; FONT-STYLE:normal; FONT-VARIANT:normal"
						id="aTagihan" runat="server">Customize Tagihan</a>
					<br>
					Membuat jadwal tagihan diluar skema cara bayar yang berlaku. Prosedur tidak 
					tersedia untuk surat pesanan yang sudah memiliki jadwal tagihan.
				</li>--%>
			</ul>
		</form>
	</body>
</html>
