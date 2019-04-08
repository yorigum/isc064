<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.Laporan.MasterReservasi" CodeFile="PDFMasterReservasi.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Laporan Master Reservasi</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Master Reservasi">
</head>
<body onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server">
        <div style="display: none">
            <uc1:Head ID="Head1" runat="server"></uc1:Head>
        </div>
        <table id="param" runat="server" width="100%" cellspacing="3">
            <tr>
                <td>
                    <p class="comp" id="comp" runat="server"></p>
                    <h1 id="judul" runat="server">Laporan Master Reservasi
                    </h1>
                </td>
            </tr>
        </table>
        <div id="headReport" runat="server"></div>
        <br />
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow>
                <asp:TableCell ColumnSpan="12" Font-Size="8pt">
					Status : A = Aktif / E = Expire.<br>
					Luas dalam meter persegi. Price List dalam rupiah per meter persegi per bulan. 
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow VerticalAlign="Bottom">
                <asp:TableHeaderCell HorizontalAlign="Left">No.</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Status</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Tgl</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Customer</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Sales</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Principal</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">No. Urut</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Batas Waktu</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">NUP</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Skema</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right">Nilai</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>
