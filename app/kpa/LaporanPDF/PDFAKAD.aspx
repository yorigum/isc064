<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.KPA.Laporan.AKAD" CodeFile="PDFAKAD.aspx.cs" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Laporan AKAD</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan AKAD">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server">
        <div id="headReport" runat="server">
            <table id="param" runat="server" width="100%" cellspacing="3">
                <tr>
                    <td colspan="2">
                        <p class="comp" id="comp" runat="server"></p>
                        <h1 id="judul" runat="server" class="title title-line">Laporan AKAD
                        </h1>
                    </td>
                </tr>
            </table>
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
                <asp:TableHeaderCell HorizontalAlign="Left">Agent</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right">Luas</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Jenis</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Bank KPA</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Harga Jual Awal</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Diskon Harga Jual</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Nilai Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Potensi KPA</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="left">Realisasi Akad</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>
