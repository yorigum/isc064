<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.COLLECTION.Laporan.LaporanOutstandingTagihan" CodeFile="PDFOutstandingTagihan.aspx.cs" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Laporan Outstanding Tagihan</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Outstanding Tagihan">
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
                        <h1 id="judul" runat="server" class="title title-line">Laporan Outstanding Tagihan
                        </h1>
                </td>
            </tr>
        </table>
        <div id="headReport" runat="server"></div><br />
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow VerticalAlign="Middle">
                <asp:TableHeaderCell HorizontalAlign="Left" RowSpan="2" Wrap="false">Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" RowSpan="2" Wrap="false">No. Surat Pesanan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" RowSpan="2" Wrap="false">Tgl Surat Pesanan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" RowSpan="2" Wrap="false">Customer</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" RowSpan="2" Wrap="false">No.Telp</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" RowSpan="2" Wrap="false">No.Hp</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" RowSpan="2" Wrap="false">Sales</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" RowSpan="2" Wrap="false">Nett Price</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" RowSpan="2" Wrap="false">Tagihan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" RowSpan="2" Wrap="false">Tgl Jatuh Tempo</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="3" Wrap="false">Billing</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" RowSpan="2" Wrap="false">Tgl Lunas</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="2" Wrap="false">Settlement</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
        <asp:Label ID="total1" runat="server" Visible="False">0</asp:Label>
        <asp:Label ID="total2" runat="server" Visible="False">0</asp:Label>
        <asp:Label ID="total3" runat="server" Visible="False">0</asp:Label>
        <asp:Label ID="total4" runat="server" Visible="False">0</asp:Label>
        <asp:Label ID="total5" runat="server" Visible="False">0</asp:Label>
    </form>
</body>
</html>
