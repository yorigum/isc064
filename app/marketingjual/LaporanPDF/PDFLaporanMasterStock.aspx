<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.Laporan.LaporanMasterStock"
    CodeFile="PDFLaporanMasterStock.aspx.cs" %>

<!DOCTYPE html>
<html>
<head>
    <title>Laporan Master Stock Per Tipe Property</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Master Stock Per Tipe Property">
</head>
<body onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server">
        <div style="display: none">
            <uc1:Head ID="Head1" runat="server"></uc1:Head>
        </div>
        <table id="param" runat="server" width="100%" cellspacing="3">
            <tr>
                <td>
                    <p class="comp" id="comp" runat="server">
                    </p>
                    <h1 id="judul" runat="server" class="title title-line">Laporan Master Stock Per Tipe Property</h1>
                </td>
            </tr>
        </table>
        <div id="headReport" runat="server">
            <h2 id="project" runat="server"></h2>
            <asp:Label ID="filter" runat="server"></asp:Label>
        </div>
        <br />
        <asp:Table ID="rpt" runat="server" CssClass="blue-skin" CellSpacing="1">
            <asp:TableRow VerticalAlign="Bottom">
                <asp:TableHeaderCell HorizontalAlign="Center" VerticalAlign="Middle" RowSpan="3">Tipe</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="4">Stock</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="4">Sold</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="3" VerticalAlign="Middle">Deviasi STU Sold</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="4">Available</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="3" VerticalAlign="Middle">Project</asp:TableHeaderCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="2">Total Luas</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Nilai (excl. ppn)</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="2">Total Luas</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Nilai (excl. ppn)</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="2">Total Luas</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2">Nilai (excl. ppn)</asp:TableHeaderCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableHeaderCell HorizontalAlign="Center">Net</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">SGA</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Net</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">SGA</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Net</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">SGA</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>
