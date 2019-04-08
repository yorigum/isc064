<%@ Page Language="c#" Inherits="ISC064.LaporanApi.LapPenjualanApi" CodeFile="LapPenjualanApi.aspx.cs" %>
<!DOCTYPE html>
<html>
<head>
    <title>Laporan Penjualan</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Penjualan">
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <div>
            <asp:Label ID="headJudul" runat="server"></asp:Label>
        </div>
        <br>
        <asp:Table ID="graph" runat="server">
        </asp:Table>
        <br>
        <table>
            <tr>
                <td>
                    <img src='/Media/g2.jpg' height='15px' width='15px' />
                    Total Penjualan
                </td>
                <td>:
                </td>
                <td>
                    <asp:Label ID="sumall" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <img src='/Media/g1.jpg' height='15px' width='15px' />
                    Batal
                </td>
                <td>:
                </td>
                <td>
                    <asp:Label ID="sumbatal" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <img src='/Media/g3.jpg' height='15px' width='15px' />
                    Netto
                </td>
                <td>:
                </td>
                <td>
                    <asp:Label ID="sumnetto" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:Label ID="lblA" Style="float: left" onclick="switchDetail(rptA, sA)" runat="server"></asp:Label><asp:Label
            ID="sA" Style="display: none" runat="server"></asp:Label><asp:Table ID="rptA" Style="clear: both"
                runat="server" CssClass="tb blue-skin" CellSpacing="1">
                <asp:TableRow VerticalAlign="Middle" onclick="switchDetail(sA, rptA)">
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">No.</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Tgl. Kontrak</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">No. Kontrak</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Customer</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Sales</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Unit</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Tipe</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Tipe Property</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Luas SG</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Arah hadap</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Price List</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Diskon</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Diskon Tambahan</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Tambahan Harga Gimmick</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Tambahan Harga Lain - Lain</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Bunga</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Nilai Kontrak (Include PPN)</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="3">Perincian Kontrak</asp:TableHeaderCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableHeaderCell HorizontalAlign="Center">Nilai Kontrak (Exclude PPN)</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center">PPN</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center">Skema Cara Bayar</asp:TableHeaderCell>
                </asp:TableRow>
            </asp:Table>
        <br>
        <asp:Label ID="lblB" Style="float: left" onclick="switchDetail(rptB, sumB)" runat="server"></asp:Label><asp:Label
            ID="sumB" Style="display: none" runat="server"></asp:Label><asp:Table ID="rptB" Style="clear: both"
                runat="server" CssClass="tb blue-skin" CellSpacing="1">
                <asp:TableRow VerticalAlign="Middle" onclick="switchDetail(sumB, rptB)">
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">No.</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Tgl. Kontrak</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">No. Kontrak</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Customer</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Sales</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">No. Unit</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Tipe Property</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Luas SG</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Tanggal Batal</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Diskon</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Diskon Tambahan</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Tambahan Harga Gimmick</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Tambahan Harga Lain - Lain</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Bunga</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Nilai Kontrak</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Biaya Administrasi</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Pembayaran</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Nilai Pengembalian</asp:TableHeaderCell>
                </asp:TableRow>
                <asp:TableRow>
                </asp:TableRow>
            </asp:Table>
        <br />
        <asp:Label ID="lblC" Style="float: left" onclick="switchDetail(rptC, sumC)" runat="server"></asp:Label><asp:Label
            ID="sumC" Style="display: none" onclick="switchDetail(rptC, sumC)" runat="server"></asp:Label>
        <asp:Table ID="rptC" runat="server" Style="clear: both" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow VerticalAlign="Middle" onclick="switchDetail(sumC, rptC)">
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">No.</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Tgl. Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">No. Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Customer</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Sales</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Tipe</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Tipe Property</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Luas SG</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Cara Bayar</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Price List</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Diskon</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Diskon Tambahan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Tambahan Harga Gimmick</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Tambahan Harga Lain - Lain</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Bunga</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Nilai Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="2">Perincian Kontrak</asp:TableHeaderCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableHeaderCell HorizontalAlign="Center">DPP</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">PPN</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
        <br />
        <asp:Label ID="lblD" Style="float: left" onclick="switchDetail(rptD, sumD)" runat="server"></asp:Label><asp:Label
            ID="sumD" Style="display: none" onclick="switchDetail(rptD, sumD)" runat="server"></asp:Label>
        <asp:Table ID="rptD" runat="server" Style="clear: both" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow VerticalAlign="Middle" onclick="switchDetail(sA, rptA)">
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">No.</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Tgl. Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">No. Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Customer</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Sales</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Tipe</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Tipe Property</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Luas SG</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Price List</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Diskon</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Diskon Tambahan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Tambahan Harga Gimmick</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Tambahan Harga Lain - Lain</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Bunga</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Nilai Kontrak (Include PPN)</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="2">Perincian Kontrak</asp:TableHeaderCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableHeaderCell HorizontalAlign="Center">Nilai Kontrak (Exclude PPN)</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">PPN</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
        <br />
        <asp:PlaceHolder ID="rpt" runat="server"></asp:PlaceHolder>
    </form>
</body>
</html>
