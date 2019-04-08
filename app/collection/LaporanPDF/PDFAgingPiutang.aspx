<%@ Page Language="c#" Inherits="ISC064.COLLECTION.Laporan.AgingPiutang" CodeFile="PDFAgingPiutang.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Laporan Aging Tagihan</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Aging Piutang">
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
                    <h1 id="judul" runat="server" class="title title-line">Laporan Aging Piutang
                    </h1>
                </td>
            </tr>
        </table>
        <div id="headReport" runat="server">
            <asp:Label ID="lblHeader" runat="server" Font-Size="12pt" Font-Bold="True"></asp:Label>
            <br />
            <asp:Label ID="lblSubHeader" runat="server" Font-Size="9pt" Font-Bold="True"></asp:Label>
        </div>
        <br />
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow VerticalAlign="Middle">
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="3" Wrap="false">No.</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="3" Wrap="false">No. Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="3" Wrap="false">No. Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="3" Wrap="false">Customer</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="3" Wrap="false">Sales</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="3" Wrap="false">Total</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="3" Wrap="false">Rincian Tagihan</asp:TableHeaderCell>
                <asp:TableHeaderCell ColumnSpan="8" Wrap="false">AGING TAGIHAN</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" ColumnSpan="2" Wrap="false">Keterangan</asp:TableHeaderCell>
            </asp:TableRow>
            <asp:TableRow VerticalAlign="Middle">
                <asp:TableHeaderCell ColumnSpan="2" Wrap="false">1 - 30 hari</asp:TableHeaderCell>
                <asp:TableHeaderCell ColumnSpan="2" Wrap="false">31 - 60 hari</asp:TableHeaderCell>
                <asp:TableHeaderCell ColumnSpan="2" Wrap="false">61 - 90 hari</asp:TableHeaderCell>
                <asp:TableHeaderCell ColumnSpan="2" Wrap="false">> 91 hari</asp:TableHeaderCell>
            </asp:TableRow>
            <asp:TableRow VerticalAlign="Middle">
                <asp:TableHeaderCell Wrap="false">Nominal</asp:TableHeaderCell>
                <asp:TableHeaderCell Wrap="false">Telat (Hari)</asp:TableHeaderCell>
                <asp:TableHeaderCell Wrap="false">Nominal</asp:TableHeaderCell>
                <asp:TableHeaderCell Wrap="false">Telat (Hari)</asp:TableHeaderCell>
                <asp:TableHeaderCell Wrap="false">Nominal</asp:TableHeaderCell>
                <asp:TableHeaderCell Wrap="false">Telat (Hari)</asp:TableHeaderCell>
                <asp:TableHeaderCell Wrap="false">Nominal</asp:TableHeaderCell>
                <asp:TableHeaderCell Wrap="false">Telat (Hari)</asp:TableHeaderCell>
                <asp:TableHeaderCell Wrap="false">Info Terakhir</asp:TableHeaderCell>
                <asp:TableHeaderCell Wrap="false">Aktvitas Telepon</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>
