<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.COLLECTION.Laporan.Col" CodeFile="PDFCol.aspx.cs" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Laporan Tagihan</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Proyeksi Penerimaan">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server">
        <div style="display: none">
            <uc1:Head ID="Head1" runat="server"></uc1:Head>
        </div>
        <table id="param" runat="server" width="100%" cellspacing="3">
            <tr>
                <td>
                    <p class="comp" id="comp" runat="server"></p>
                    <h1 id="judul" runat="server" class="title title-line">Laporan Proyeksi Penerimaan
                    </h1>
                </td>
            </tr>
        </table>
        <asp:Label ID="headJudul" runat="server"></asp:Label>
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow BackColor="LightGray">
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="3">No.</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="3">Customer</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="3">No. Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="3">Harga<br />Pokok</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="3">PPN 10%</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="3">Total<br />Harga</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="3">Cara<br />Pembayaran</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="3">DP</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Rencana Pembayaran</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="3">Total<br />Pembayaran</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="3">Sisa<br />Pembayaran</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
        <asp:PlaceHolder ID="rp" runat="server"></asp:PlaceHolder>
    </form>
</body>
</html>
