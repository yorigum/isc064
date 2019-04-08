<%@ Page language="c#" Inherits="ISC064.FINANCEAR.Accounting" CodeFile="Accounting.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Posting Accounting</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
    	<link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Posting Accounting">
	</head>
	<body>
		<form id="Form1" method="post" runat="server" class="cnt">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<h1>Posting Accounting</h1>
			<br>
			<div id="mnpenjualan" runat="server">
				<table>
					<tr>
						<td>
							Penjualan :
						</td>
						<td>
							<asp:dropdownlist id="penjualan" runat="server" width="200" cssclass="ddl">
								<asp:listitem>Surabaya</asp:listitem>
								<asp:listitem>Jakarta</asp:listitem>
							</asp:dropdownlist>
						</td>
						<td>
							<asp:button id="search" runat="server" cssclass="btn" text="Search" accesskey="s" onclick="search_Click"></asp:button>
						</td>
					</tr>
				</table>
			</div>
			<div id="isi" runat="server">
				<p class="feed">
					<asp:label id="feed" runat="server"></asp:label>
				</p>
				<p>
					Posting Accounting adalah sebuah proses untuk mengirimkan data-data keuangan ke 
					modul akunting.<br>
					Komputer akan mencari semua transaksi kwitansi dengan status belum pernah 
					di-posting sebelumnya.
				</p>
				<p>Berikut adalah data-data yang akan di-posting:</p>
				<br>
				<asp:table id="list" runat="server" cssclass="tb">
					<asp:tablerow>
						<asp:tableheadercell horizontalalign="Left" width="60">No. TTS</asp:tableheadercell>
						<asp:tableheadercell horizontalalign="Left" width="60">No. BKM</asp:tableheadercell>
						<asp:tableheadercell horizontalalign="Left">Rekening</asp:tableheadercell>
						<asp:tableheadercell horizontalalign="Left" width="200">Customer</asp:tableheadercell>
						<asp:tableheadercell horizontalalign="Left" width="100">Unit</asp:tableheadercell>
						<asp:tableheadercell horizontalalign="Right" width="90">Nilai</asp:tableheadercell>
						<asp:tableheadercell horizontalalign="Right" width="90">% Lunas</asp:tableheadercell>
					</asp:tablerow>
				</asp:table>
				<br>
				<asp:button id="ok" runat="server" cssclass="btn" text="Generate" width="75" onclick="ok_Click"></asp:button>
				<br>
				<br>
				<asp:label id="err" runat="server"></asp:label>
			</div>
		</form>
	</body>
</html>
