<%--<%@ Reference Page="~/Laporan/PotensiKPR.aspx" %>--%>
<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.Laporan.Laporan_LaporanSP3k" CodeFile="PDFLaporanSP3K.aspx.cs" %>

<!DOCTYPE html>
<html>
<head>
    <title>Laporan SP3K</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan SP3K">
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
                    <h1 id="judul" runat="server" class="title title-line">Laporan SP3K
                    </h1>
                </td>
            </tr>
        </table>
        <div id="headReport" runat="server"></div>
        <br />
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow VerticalAlign="middle">
                <asp:TableHeaderCell HorizontalAlign="Left">#</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">No. Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Customer</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Luas SG</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Bank KPR</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Status SP3K</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Target SP3K</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Tgl. Pengajuan SP3K</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Tgl. Hasil SP3K</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">No. SP3K</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Hasil SP3K</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Nilai Yang Disetujui</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>

