<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.Laporan.LaporanGantiNama" CodeFile="PDFLaporanGantiUnit.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Laporan Pindah Unit</title>
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
        <div style="display: none">
            <uc1:Head ID="Head1" runat="server"></uc1:Head>
        </div>
        <table id="param" runat="server" width="100%" cellspacing="3">
            <tr>
                <td>
                    <p class="comp" id="comp" runat="server"></p>
                    <h1 id="judul" class="title title-line" runat="server">Laporan Pindah Unit
                    </h1>

                </td>
            </tr>
        </table>
        <div id="headReport" runat="server">
            <asp:Label ID="headJudul" runat="server"></asp:Label>
        </div>
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow VerticalAlign="Middle" BackColor="LightGray">
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">No.</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Tgl. Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">No. Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Customer</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Sales</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="6">Awal</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="7">Pindah</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Project</asp:TableHeaderCell>
            </asp:TableRow>
            <asp:TableRow BackColor="LightGray">
                <asp:TableHeaderCell HorizontalAlign="Center">Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Tipe</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Luas SG</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Price List</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Diskon</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Total</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Tipe</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Luas SG</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Price List</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Diskon</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Total</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Tgl</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
		</form>
	</body>
</html>
