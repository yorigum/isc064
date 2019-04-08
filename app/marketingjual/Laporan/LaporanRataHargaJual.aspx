<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.Laporan.LaporanRataHargaJual" CodeFile="LaporanRataHargaJual.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Laporan Rata-Rata Harga Jual Per Tower</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Report.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Laporan Rata-Rata Harga Jual Per Tower">
	</head>
	<body onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
		<form id="Form1" method="post" runat="server">
			<div style="DISPLAY:none"><uc1:head id="Head1" runat="server"></uc1:head></div>
			<table id="param" runat="server" width="100%" cellspacing="3">
				<tr>
					<td>
						<p class="comp" id="comp" runat="server"></p>
						<h1 id="judul" runat="server">
							Laporan Rata-Rata Harga Jual Per Tower
						</h1>
						<br />
						<div class="ins">
							<table>
								<tr>
									<td>
										<asp:button id="scr" accesskey="s" runat="server" text="Screen Preview" width="100" cssclass="btn" onclick="scr_Click"></asp:button>
										<asp:button id="xls" accesskey="e" runat="server" text="Download Excel" width="100" cssclass="btn" onclick="xls_Click"></asp:button>
									</td>
								</tr>
							</table>
						</div>
					</td>
				</tr>
			</table>
			<asp:table id="rpt" runat="server" cssclass="tb" cellspacing="3">
				<asp:tablerow>
					<asp:tablecell columnspan="7">
						<asp:label id="lblHeader" runat="server" font-size="12pt" font-bold="True"></asp:label>
					</asp:tablecell>
				</asp:tablerow>
				<asp:tablerow verticalalign="Middle">
					<asp:tableheadercell horizontalalign="Left" backcolor="Gray" forecolor="White" rowspan="2">Tower</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Center" backcolor="Gray" forecolor="White" columnspan="2">Total</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Center" backcolor="Gray" forecolor="White">Rata-Rata</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Center" backcolor="Gray" forecolor="White" columnspan="2">Sold</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Center" backcolor="Gray" forecolor="White">Rata-Rata</asp:tableheadercell>
				</asp:tablerow>
				<asp:tablerow verticalalign="Middle">
					<asp:tableheadercell horizontalalign="Right" backcolor="Gray" forecolor="White">Nilai Transaksi</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Right" backcolor="Gray" forecolor="White">Luas</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Right" backcolor="Gray" forecolor="White">Harga Jual</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Right" backcolor="Gray" forecolor="White">Nilai Transaksi</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Right" backcolor="Gray" forecolor="White">Luas</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Right" backcolor="Gray" forecolor="White">Harga Jual</asp:tableheadercell>
				</asp:tablerow>
			</asp:table>
		</form>
	</body>
</html>
