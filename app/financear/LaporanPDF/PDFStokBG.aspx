<%@ Page Language="c#" Inherits="ISC064.FINANCEAR.Laporan.StokBG" CodeFile="PDFStokBG.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Laporan Stok Cek Giro</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Stok Cek Giro">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27&&confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server">
        <div style="display: none">
            <uc1:Head ID="Head1" runat="server"></uc1:Head>
        </div>
        <table id="param" runat="server" width="100%" cellspacing="3">
            <tr>
                <td>
                    <p class="comp" id="comp" runat="server"></p>
                    <h1 id="judul" runat="server" class="title title-line">Laporan Stok Cek Giro
                    </h1>
                </td>
            </tr>
        </table>
        <div id="headReport" runat="server"></div>
        <br />
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow CssClass="tb blue-skin">
                <asp:TableHeaderCell HorizontalAlign="Left">Bulan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Total</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50">1</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50">2</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50">3</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50">4</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50">5</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50">6</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50">7</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50">8</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50">9</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50">10</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50">11</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50">12</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50">13</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50">14</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50">15</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50">16</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50">17</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50">18</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50">19</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50">20</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50">21</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50">22</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50">23</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50">24</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50">25</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50">26</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50">27</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50">28</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50">29</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50">30</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50">31</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>
