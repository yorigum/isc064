<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.Alokasi" CodeFile="Alokasi.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Edit Alokasi Pelunasan</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Tagihan - Edit Alokasi Pelunasan">
	</head>
	<body>
		<form id="Form1" method="post" runat="server" class="cnt">
			<h1>Edit Alokasi Pelunasan</h1>
			<br>
			<p style="padding:3;font:8pt">
				Cara Bayar : TN = Tunai / KD = Kartu Debit / KK = Kartu Kredit / TR = Transfer 
				Bank / BG = Cek Giro / DN = Diskon
			</p>
			<table cellspacing="3" class="tb">
				<tr align="left" valign="bottom">
					<th width="130">
						Tagihan</th>
					<th width="42">
						TTS</th>
					<th>
						Cara<br>
						Bayar</th>
					<th width="240">
						Keterangan</th>
					<th width="70">
						Tgl</th>
					<th width="90" align="right">
						Nilai</th>
				</tr>
				<asp:placeholder id="list" runat="server"></asp:placeholder>
			</table>
			<table height="50">
				<tr>
					<td>
						<asp:button id="ok" runat="server" cssclass="btn" text="OK" width="75" onclick="ok_Click"></asp:button>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>
