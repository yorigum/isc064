<%@ Reference Page="~/SecLevel.aspx" %>

<%@ Page Language="c#" Inherits="ISC064.SECURITY.Laporan.TabelAbsensi" CodeFile="PDFTabelAbsensi.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE html >
<html>
<head>
    <title>Laporan Tabel Absensi</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Tabel Absensi">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==13)document.getElementById('scr').click();if(event.keyCode==27&&confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server">
        <div id="headReport" runat="server">
            <table id="param" runat="server" width="100%" cellspacing="3">
                <tr>
                    <td colspan="2">
                        <p class="comp" id="comp" runat="server"></p>
                        <h1 id="judul" runat="server" class="title title-line">Laporan Security Log
                        </h1>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false">ID</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" Width="150px">Tgl. Jam Masuk</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" Width="150px">Tgl. Jam Keluar</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" Width="75px">Kode User</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" Width="200px">Nama User</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" Width="75px">Security Level</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false">IP Address</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>
