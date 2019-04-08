<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.Laporan.LaporanSalesPerformance"
    CodeFile="PDFLaporanSalesPerformance.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Laporan Sales Performance</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Sales Performance">
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
                    <h1 id="judul" runat="server" class="title title-line">Laporan Sales Performance
                    </h1>
                    <br />
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
                <asp:TableHeaderCell HorizontalAlign="center" RowSpan="3" BackColor="Gray" ForeColor="white">No. Sales</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" RowSpan="3" BackColor="Gray" ForeColor="white">Nama Sales</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" RowSpan="3" BackColor="Gray" ForeColor="white">Principal</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" ColumnSpan="4" BackColor="Gray" ForeColor="white">Jan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" ColumnSpan="4" BackColor="Gray" ForeColor="white">Feb</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" ColumnSpan="4" BackColor="Gray" ForeColor="white">Mar</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" ColumnSpan="4" BackColor="Gray" ForeColor="white">Apr</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" ColumnSpan="4" BackColor="Gray" ForeColor="white">May</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" ColumnSpan="4" BackColor="Gray" ForeColor="white">Jun</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" ColumnSpan="4" BackColor="Gray" ForeColor="white">Jul</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" ColumnSpan="4" BackColor="Gray" ForeColor="white">Aug</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" ColumnSpan="4" BackColor="Gray" ForeColor="white">Sep</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" ColumnSpan="4" BackColor="Gray" ForeColor="white">Oct</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" ColumnSpan="4" BackColor="Gray" ForeColor="white">Nov</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" ColumnSpan="4" BackColor="Gray" ForeColor="white">Dec</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" RowSpan="2" BackColor="Gray" ForeColor="white"
                    ColumnSpan="2">Total</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" RowSpan="2" BackColor="Gray" ForeColor="white"
                    ColumnSpan="2">Batal</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" RowSpan="2" BackColor="Gray" ForeColor="white"
                    ColumnSpan="2">Grand Total</asp:TableHeaderCell>
            </asp:TableRow>
            <asp:TableRow VerticalAlign="Middle">
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white"
                    RowSpan="2">Target</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white"
                    ColumnSpan="2">Riil</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white"
                    RowSpan="2">Persentase</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white"
                    RowSpan="2">Target</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white"
                    ColumnSpan="2">Riil</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white"
                    RowSpan="2">Persentase</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white"
                    RowSpan="2">Target</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white"
                    ColumnSpan="2">Riil</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white"
                    RowSpan="2">Persentase</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white"
                    RowSpan="2">Target</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white"
                    ColumnSpan="2">Riil</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white"
                    RowSpan="2">Persentase</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white"
                    RowSpan="2">Target</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white"
                    ColumnSpan="2">Riil</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white"
                    RowSpan="2">Persentase</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white"
                    RowSpan="2">Target</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white"
                    ColumnSpan="2">Riil</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white"
                    RowSpan="2">Persentase</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white"
                    RowSpan="2">Target</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white"
                    ColumnSpan="2">Riil</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white"
                    RowSpan="2">Persentase</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white"
                    RowSpan="2">Target</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white"
                    ColumnSpan="2">Riil</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white"
                    RowSpan="2">Persentase</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white"
                    RowSpan="2">Target</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white"
                    ColumnSpan="2">Riil</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white"
                    RowSpan="2">Persentase</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white"
                    RowSpan="2">Target</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white"
                    ColumnSpan="2">Riil</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white"
                    RowSpan="2">Persentase</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white"
                    RowSpan="2">Target</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white"
                    ColumnSpan="2">Riil</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white"
                    RowSpan="2">Persentase</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white"
                    RowSpan="2">Target</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white"
                    ColumnSpan="2">Riil</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white"
                    RowSpan="2">Persentase</asp:TableHeaderCell>
            </asp:TableRow>
            <asp:TableRow VerticalAlign="Middle">
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white">Kuantitas</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white">Rupiah</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white">Kuantitas</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white">Rupiah</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white">Kuantitas</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white">Rupiah</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white">Kuantitas</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white">Rupiah</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white">Kuantitas</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white">Rupiah</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white">Kuantitas</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white">Rupiah</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white">Kuantitas</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white">Rupiah</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white">Kuantitas</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white">Rupiah</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white">Kuantitas</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white">Rupiah</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white">Kuantitas</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white">Rupiah</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white">Kuantitas</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white">Rupiah</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white">Kuantitas</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white">Rupiah</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white">Kuantitas</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white">Rupiah</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white">Kuantitas</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white">Rupiah</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white">Kuantitas</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="Gray" ForeColor="white">Rupiah</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
        <br />
        <br />
        <%--    <asp:Chart ID="Chart1" runat="server">
        <Series>
            <asp:Series Name="Series1">
            </asp:Series>
        </Series>
        <ChartAreas>
            <asp:ChartArea Name="ChartArea1">
            </asp:ChartArea>
        </ChartAreas>
    </asp:Chart>--%>
        <%--</div>--%>
    </form>
</body>
</html>
