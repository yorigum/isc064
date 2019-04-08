<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.Laporan.LaporanRealisasiSales" CodeFile="LaporanRealisasiSales.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<HTML>
	<HEAD>
		<title>Laporan Pembayaran Detail</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Report.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Laporan Pembayaran Detail">
	</HEAD>
	<body onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
		<form id="Form1" method="post" runat="server">
			<table id="param" runat="server" width="100%" cellspacing="3">
				<tr>
					<td>
						<p class="comp" id="comp" runat="server"></p>
						<h1 id="judul" runat="server">
							REALISASI SALES & CASH IN
						</h1>
						<br />
						Dari
						<br />
						<asp:DropDownList ID="bulandari" runat="server">
						    <asp:ListItem Value="1">Januari</asp:ListItem>
						    <asp:ListItem Value="2">Februari</asp:ListItem>
						    <asp:ListItem Value="3">Maret</asp:ListItem>
						    <asp:ListItem Value="4">April</asp:ListItem>
						    <asp:ListItem Value="5">Mei</asp:ListItem>
						    <asp:ListItem Value="6">Juni</asp:ListItem>
						    <asp:ListItem Value="7">Juli</asp:ListItem>
						    <asp:ListItem Value="8">Agustus</asp:ListItem>
						    <asp:ListItem Value="9">September</asp:ListItem>
						    <asp:ListItem Value="10">Oktober</asp:ListItem>
						    <asp:ListItem Value="11">November</asp:ListItem>
						    <asp:ListItem Value="12">Desember</asp:ListItem>
						</asp:DropDownList>
						<asp:TextBox ID="tahundari" runat="server"></asp:TextBox>
						<br />
							Sampai
						<br />
						<asp:DropDownList ID="bulansampai" runat="server">
						    <asp:ListItem Value="1">Januari</asp:ListItem>
						    <asp:ListItem Value="2">Februari</asp:ListItem>
						    <asp:ListItem Value="3">Maret</asp:ListItem>
						    <asp:ListItem Value="4">April</asp:ListItem>
						    <asp:ListItem Value="5">Mei</asp:ListItem>
						    <asp:ListItem Value="6">Juni</asp:ListItem>
						    <asp:ListItem Value="7">Juli</asp:ListItem>
						    <asp:ListItem Value="8">Agustus</asp:ListItem>
						    <asp:ListItem Value="9">September</asp:ListItem>
						    <asp:ListItem Value="10">Oktober</asp:ListItem>
						    <asp:ListItem Value="11">November</asp:ListItem>
						    <asp:ListItem Value="12">Desember</asp:ListItem>
						</asp:DropDownList>
						<asp:TextBox ID="tahunsampai" runat="server"></asp:TextBox>
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
			<asp:Label ID="lblHeader" runat="server"></asp:Label>
			<asp:table id="rpt" runat="server" cssclass="tb" Width="100%" BorderStyle="Solid" GridLines="Both" BorderColor="Black" BorderWidth="1" CellPadding="10">
				<%--<asp:tablerow BackColor="LightGray">
					<asp:tableheadercell horizontalalign="Center" RowSpan="2">NO</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Center" RowSpan="2">CUSTOMER</asp:tableheadercell>
					<asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">TGL BF</asp:TableHeaderCell>
					<asp:tableheadercell horizontalalign="Center" ColumnSpan="4">TYPE</asp:tableheadercell>
					<asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Harga</asp:TableHeaderCell>
					<asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">PPN</asp:TableHeaderCell>
					<asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">TOTAL HARGA (Rp.)</asp:TableHeaderCell>
					<asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">TYPE OF PAYMENT</asp:TableHeaderCell>
				</asp:tablerow>
				<asp:TableRow BackColor="LightGray">
				    <asp:TableHeaderCell HorizontalAlign="Center">UNIT</asp:TableHeaderCell>
					<asp:TableHeaderCell HorizontalAlign="Center">TOWER</asp:TableHeaderCell>
					<asp:TableHeaderCell HorizontalAlign="Center">LANTAI</asp:TableHeaderCell>
					<asp:TableHeaderCell HorizontalAlign="Center">LUAS</asp:TableHeaderCell>
				</asp:TableRow>--%>
			</asp:table>
		</form>
	</body>
</HTML>
