<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PDFLaporanAkad.aspx.cs" Inherits="ISC064.MARKETINGJUAL.Laporan.Laporan_LaporanAkad" %>

<!DOCTYPE html>
<html>
<head>
    <title>Laporan Akad</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Akad">
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
                    <h1 id="judul" runat="server" class="title title-line">Laporan Akad
                    </h1>
                    
                </td>
            </tr>
        </table>
        <div id="headReport" runat="server">

        </div>
        <br />
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow VerticalAlign="Bottom">
                <asp:TableHeaderCell HorizontalAlign="Left">#</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">No. Akad</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Tgl. Akad</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">No. Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Customer</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">NPWP</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Alamat</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Sales</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right">Luas SG</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" ID="tower" runat="server">Tower</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Bank KPR</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Harga Jual Awal</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Diskon Harga Jual</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Nilai Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Potensi KPR</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="left">Realisasi Akad</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>
