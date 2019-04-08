<%@ Page Language="c#" Inherits="ISC064.LaporanApi.LapBatalKontrakApi" CodeFile="LapBatalKontrakApi.aspx.cs" %>

<!DOCTYPE html>
<html>
<head>
    <title>Laporan Pembatalan</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Pengalihan Hak">
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <div id="headReport" runat="server">
        </div>
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow BackColor="LightGray">
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">No.</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Tgl. Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">No. Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">No. Surat Pesanan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Customer</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Sales</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Tipe</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Luas SG</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Price List</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Diskon</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Bunga</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Tgl. Batal</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Nilai Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Pembayaran</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Pengembalian</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Sisa</asp:TableHeaderCell>
                <asp:TableHeaderCell ColumnSpan="2">Perincian Kontrak</asp:TableHeaderCell>
            </asp:TableRow>
            <asp:TableRow VerticalAlign="Bottom" BackColor="LightGray">
                <asp:TableHeaderCell HorizontalAlign="Center">DPP</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">PPN</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
        <asp:PlaceHolder ID="rp" runat="server"></asp:PlaceHolder>
    </form>
</body>
</html>
