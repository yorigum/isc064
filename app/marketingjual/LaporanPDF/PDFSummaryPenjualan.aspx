<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.Laporan.SummaryPenjualan" CodeFile="PDFSummaryPenjualan.aspx.cs" %>

<!DOCTYPE html>
<html>
<head>
    <title>Laporan Rekapitulasi Kontrak dan Pembayaran</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Summary Penjualan">
</head>
<body onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server">
        <div style="display: none">
            <uc1:Head ID="Head1" runat="server"></uc1:Head>
        </div>
        <table id="param" cellspacing="3" width="100%" runat="server">
            <tr>
                <td>
                    <p class="comp" id="comp" runat="server"></p>
                    <h1 id="judul" runat="server" class="title title-line">Laporan Rekapitulasi Kontrak dan Pembayaran
                    </h1>
                    
                </td>
            </tr>
        </table>
        <div id="headReport" runat="server"></div>
        <br />
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow VerticalAlign="Bottom">
                <asp:TableHeaderCell HorizontalAlign="Left" RowSpan="2">No</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" RowSpan="2">No Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" RowSpan="2">Tgl Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" RowSpan="2">No Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" RowSpan="2">Luas SG</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" RowSpan="2">Customer</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" RowSpan="2">Nilai Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" RowSpan="2">View</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" RowSpan="2">Biaya Admin</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" RowSpan="2">Total</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="5">Pembayaran</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="5">Outstanding</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" RowSpan="2">Project</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>
