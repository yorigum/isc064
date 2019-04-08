<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.Laporan.LaporanProyeksiTagihan" CodeFile="LaporanProyeksiTagihan.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Laporan Proyeksi Tagihan</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Report.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Laporan Master Kontrak">
	</head>
	<body onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
		<form id="Form1" method="post" runat="server">
			<div style="DISPLAY:none"><uc1:head id="Head1" runat="server"></uc1:head></div>
			<table id="param" runat="server" width="100%" cellspacing="3">
				<tr>
					<td>
						<p class="comp" id="comp" runat="server"></p>
						<h1 id="judul" runat="server">
							Laporan Proyeksi Tagihan
						</h1>
						<table cellspacing="0" cellpadding="0">
							<tr valign="top">
								<td>
									<p class="pparam">
										<b>Periode tahun :</b>
										<br />
										<asp:dropdownlist id="thn" runat="server" cssclass="ddl"></asp:dropdownlist>
									</p>
								</td>
							</tr>
						</table>
						<br>
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
					<asp:tablecell columnspan="2" font-size="8pt"></asp:tablecell>
				</asp:tablerow>
				<asp:tablerow verticalalign="Bottom">
					<asp:tableheadercell horizontalalign="Left">Bulan</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Right">Total</asp:tableheadercell>
				</asp:tablerow>
			</asp:table>
		</form>
	</body>
</html>
