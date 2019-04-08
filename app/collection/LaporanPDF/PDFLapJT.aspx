<%@ Page Language="c#" Inherits="ISC064.COLLECTION.Laporan.LapJT" CodeFile="PDFLapJT.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Laporan Jatuh Tempo</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Jatuh Tempo">
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
                    <h1 id="judul" runat="server" class="title title-line">Laporan Jatuh Tempo
                    </h1>
                    <br />
                </td>
            </tr>
        </table>
        <div id="headReport" runat="server"></div><br />
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow VerticalAlign="Bottom">
                <asp:TableHeaderCell HorizontalAlign="Center" Wrap="false">No.</asp:TableHeaderCell>
                <asp:TableHeaderCell Wrap="false">No. Tagihan</asp:TableHeaderCell>
                <asp:TableHeaderCell Wrap="false">Customer</asp:TableHeaderCell>
                <asp:TableHeaderCell Wrap="false">No. Tlp</asp:TableHeaderCell>
                <asp:TableHeaderCell Wrap="false">No. HP</asp:TableHeaderCell>
                <asp:TableHeaderCell Wrap="false">No. Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell Wrap="false">Nama Tagihan</asp:TableHeaderCell>
                <asp:TableHeaderCell Wrap="false">Tgl. Jatuh Tempo</asp:TableHeaderCell>
                <asp:TableHeaderCell Wrap="false">Nilai Tagihan</asp:TableHeaderCell>
                <asp:TableHeaderCell Wrap="false">Nilai Pelunasan</asp:TableHeaderCell>
                <asp:TableHeaderCell Wrap="false">Sisa Tagihan</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>
