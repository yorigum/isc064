<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.Laporan.LaporanPenjualan3" CodeFile="PDFLaporanPenjualan3.aspx.cs" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Laporan Penjualan Tahunan</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<link href="/Media/Report.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Laporan Penjualan Tahunan">
	</head>
	<body onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
		<form id="Form1" method="post" runat="server">
			
			<asp:Label ID="headJudul" runat="server"></asp:Label>
			<asp:table id="rpt" runat="server" cssclass="tb blue-skin" cellspacing="1">
				<asp:tablerow>
					<asp:tablecell columnspan="23" font-size="8pt">
						Status : A = Aktif / B = Batal.
					</asp:tablecell>
				</asp:tablerow>
				<asp:tablerow verticalalign="Bottom" BackColor="LightGray">
				    <asp:tableheadercell ColumnSpan="5">Total Sales</asp:tableheadercell>
				    <asp:TableHeaderCell></asp:TableHeaderCell>
				    <asp:TableHeaderCell ColumnSpan="4">Total Canceled</asp:TableHeaderCell>
				    <asp:TableHeaderCell></asp:TableHeaderCell>
				    <asp:TableHeaderCell ColumnSpan="4">Total Net Sales</asp:TableHeaderCell>
				</asp:tablerow>
				<asp:tablerow verticalalign="Bottom" BackColor="LightGray">
				    <asp:TableHeaderCell RowSpan="2">Tahun</asp:TableHeaderCell>
				    <asp:TableHeaderCell RowSpan="2">Unit</asp:TableHeaderCell>
				    <asp:TableHeaderCell ColumnSpan="2">Total Luas</asp:TableHeaderCell>
				    <asp:TableHeaderCell RowSpan="2">Nilai<br />( incl ppn )</asp:TableHeaderCell>
				    <asp:TableHeaderCell RowSpan="2"></asp:TableHeaderCell>
				    <asp:TableHeaderCell RowSpan="2">Unit</asp:TableHeaderCell>
				    <asp:TableHeaderCell ColumnSpan="2">Total Luas</asp:TableHeaderCell>
				    <asp:TableHeaderCell RowSpan="2">Nilai<br />( incl ppn )</asp:TableHeaderCell>
				    <asp:TableHeaderCell RowSpan="2"></asp:TableHeaderCell>
				    <asp:TableHeaderCell RowSpan="2">Unit</asp:TableHeaderCell>
				    <asp:TableHeaderCell ColumnSpan="2">Total Luas</asp:TableHeaderCell>
				    <asp:TableHeaderCell RowSpan="2">Nilai<br />( incl ppn )</asp:TableHeaderCell>
				</asp:tablerow>
				<asp:TableRow BackColor="LightGray">
				    <asp:TableHeaderCell>Net</asp:TableHeaderCell>
				    <asp:TableHeaderCell>SGA</asp:TableHeaderCell>
				    <asp:TableHeaderCell>Net</asp:TableHeaderCell>
				    <asp:TableHeaderCell>SGA</asp:TableHeaderCell>
				    <asp:TableHeaderCell>Net</asp:TableHeaderCell>
				    <asp:TableHeaderCell>SGA</asp:TableHeaderCell>
				</asp:TableRow>
			</asp:table>
			<asp:PlaceHolder ID="rp" runat="server"></asp:PlaceHolder>
		</form>
	</body>
</html>
