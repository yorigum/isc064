<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.Laporan.LaporanGantiNama" CodeFile="PDFLaporanGantiNama.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Laporan Pengalihan Hak</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Report.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Laporan Pengalihan Hak">
	</head>
	<body onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
		<form id="Form1" method="post" runat="server">
			<div style="DISPLAY:none"><uc1:head id="Head1" runat="server"></uc1:head></div>
			<table id="param" runat="server" width="100%" cellspacing="3">
				<tr>
					<td>
						<p class="comp" id="comp" runat="server"></p>
						<h1 id="judul" class="title title-line" runat="server">
							Laporan Pengalihan Hak
						</h1>
					
					</td>
				</tr>
			</table>
            <div id="headReport" runat="server">

            </div>
			<asp:table id="rpt" runat="server" cssclass="tb blue-skin" cellspacing="1">
				<asp:tablerow verticalalign="Bottom">
					<asp:tableheadercell horizontalalign="Center">No.</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Tgl. Pengalihan Hak</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">No. Kontrak</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Unit</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Luas SG</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Customer Lama</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Customer Baru</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Sales</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Project</asp:tableheadercell>
				</asp:tablerow>
			</asp:table>
		</form>
	</body>
</html>
