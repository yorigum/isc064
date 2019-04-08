<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.Laporan.MasterKontrak" CodeFile="PDFMasterKontrak.aspx.cs" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Laporan Master Kontrak</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<link href="/Media/Report.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Laporan Master Kontrak">
	</head>
	<body onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
		<form id="Form1" method="post" runat="server">
			<div style="DISPLAY: none"><uc1:head id="Head1" runat="server"></uc1:head></div>
			<table id="param" cellspacing="3" width="100%" runat="server">
				<tr>
					<td>
						<p class="comp" id="comp" runat="server"></p>
						<h1 id="judul" class="title title-search" runat="server">Laporan Master Kontrak
						</h1>
					</td>
				</tr>
			</table>
            <div id="headReport" runat="server">

            </div>
            <br />
			<asp:table id="rpt" runat="server" cssclass="tb blue-skin" cellspacing="1">
				<asp:tablerow verticalalign="Bottom">
				<asp:tableheadercell horizontalalign="Left">No. Urut</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">No. Kontrak</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Tgl. Kontrak</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Tgl. Input</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Status</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Customer</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Lokasi</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Tipe</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">No. Unit</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">NUP</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Right">Luas SG (M2)</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Skema Cara Bayar</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Cara Bayar</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">No. VA</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Right">Price List</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Right">Diskon</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Right">Bunga</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Right">Diskon Tambahan</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Right">Nilai Kontrak</asp:tableheadercell>
                    <asp:tableheadercell horizontalalign="Right">Biaya Administrasi</asp:tableheadercell>					
					<asp:tableheadercell horizontalalign="Right">Fitting Out</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Right">Nominal Bayar</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Right">Sisa Belum Bayar</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Persentase Lunas</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Sales</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Principal</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Tgl. PPJB</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">No. PPJB</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Tgl. BAST</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">No. BAST</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Tgl. AJB</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">No. AJB</asp:tableheadercell>
                    <asp:tableheadercell horizontalalign="Left">Project</asp:tableheadercell>
				</asp:tablerow>
			</asp:table></form>
	</body>
</html>
