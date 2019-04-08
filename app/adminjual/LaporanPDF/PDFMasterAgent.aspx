<%@ Page Language="c#" Inherits="ISC064.ADMINJUAL.Laporan.MasterAgent" CodeFile="PDFMasterAgent.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Laporan Master Sales</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Master Sales">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27&&confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
   <form id="Form1" method="post" runat="server">
        <div id="headReport" runat="server">
            <table id="param" runat="server" width="100%" cellspacing="3">
                <tr>
                    <td colspan="2">
                        <p class="comp" id="comp" runat="server"></p>
                        <h1 id="judul" runat="server" class="title title-line">Laporan Master Agent
                        </h1>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
             <asp:TableRow VerticalAlign="Bottom">
                <asp:TableHeaderCell HorizontalAlign="Left">No.</asp:TableHeaderCell>
                <%--<asp:TableHeaderCell HorizontalAlign="Left">Tgl. Input</asp:TableHeaderCell>--%>
                <asp:TableHeaderCell HorizontalAlign="Left">Tipe</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Level</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Atasan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Kode Sales</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Nama Sales</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Alamat</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Email</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Telepon</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Handphone</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Whatsapp</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">NPWP</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Rekening</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Bank</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" style="width:200px">Atas Nama Rekening</asp:TableHeaderCell>
                <%--<asp:tableheadercell horizontalalign="Left">Skema Komisi</asp:tableheadercell>--%>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>
