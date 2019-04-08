<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.Laporan.MasterStock" CodeFile="PDFMasterStock.aspx.cs" %>
<!DOCTYPE html>
<HTML>
	<HEAD>
		<title>Laporan Master Stock</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Report.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Laporan Master Stock">
	</HEAD>
	<body onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
		<form id="Form1" method="post" runat="server">
			<div style="DISPLAY:none"><uc1:head id="Head1" runat="server"></uc1:head></div>
		
			<asp:Label ID="headJudul" Runat="server"></asp:Label>
			<asp:table id="rpt" runat="server" cssclass="tb blue-skin" cellspacing="1">
				<asp:tablerow>
					<asp:tableheadercell horizontalalign="Center" RowSpan="2" Width="50px">Tower</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Center" RowSpan="2" Width="50px">Lantai</asp:tableheadercell>
                    <asp:tableheadercell horizontalalign="Center" RowSpan="2" Width="50px">Project</asp:tableheadercell>
					<asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="2">Available</asp:TableHeaderCell>
					<asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="2">Sold</asp:TableHeaderCell>
					<asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="2">Hold Internal</asp:TableHeaderCell>
					<asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="2">Total</asp:TableHeaderCell>
				</asp:tablerow>
				<asp:tablerow verticalalign="Bottom">
					<asp:tableheadercell horizontalalign="Center">Unit</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Center">%</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Center">Unit</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Center">%</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Center">Unit</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Center">%</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Center">Unit</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Center">%</asp:tableheadercell>
				</asp:tablerow>
			</asp:table>
			<asp:PlaceHolder ID="rp" Runat="server"></asp:PlaceHolder>
		</form>
	</body>
</HTML>
