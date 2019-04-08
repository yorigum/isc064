<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.ADMINJUAL.Laporan.PriceList" CodeFile="PDFSummaryStockPerTipe.aspx.cs" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Summary Stock Per Tipe</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Price List">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server">
        <div id="headReport" runat="server">
            <table id="param" runat="server" width="100%" cellspacing="3">

                <tr>
                    <td colspan="2">
                        <p class="comp" id="comp" runat="server"></p>
                        <h1 id="judul" runat="server" class="title title-line">Laporan Summary Stock Per Tipe
                        </h1>
                           <asp:Label ID="lblHeader" runat="server" Font-Size="12pt" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow VerticalAlign="Middle">
                <asp:TableHeaderCell HorizontalAlign="Left" RowSpan="3" BackColor="#1E90FF" ForeColor="White">Tower</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" RowSpan="3" BackColor="#1E90FF" ForeColor="White">Tipe</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="2" BackColor="#1E90FF" ForeColor="White">Available</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="2" BackColor="#1E90FF" ForeColor="White">Sold</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="2" BackColor="#1E90FF" ForeColor="White">Hold Internal</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="2" BackColor="#1E90FF" ForeColor="White">Total</asp:TableHeaderCell>
            </asp:TableRow>
            <asp:TableRow VerticalAlign="Middle">
                <asp:TableHeaderCell HorizontalAlign="Right" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" RowSpan="2" BackColor="#1E90FF" ForeColor="White">%</asp:TableHeaderCell>                
                <asp:TableHeaderCell HorizontalAlign="Right" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" RowSpan="2" BackColor="#1E90FF" ForeColor="White">%</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" RowSpan="2" BackColor="#1E90FF" ForeColor="White">%</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" RowSpan="2" BackColor="#1E90FF" ForeColor="White">%</asp:TableHeaderCell>
            </asp:TableRow>
            <asp:TableRow VerticalAlign="Middle">
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>
