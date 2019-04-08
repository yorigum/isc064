<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.Laporan.LaporanComplain" CodeFile="PDFLaporanComplain.aspx.cs" %>

<!DOCTYPE html>
<html>
<head>
    <title>Laporan Complain</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Complain">
</head>
<body onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server">
        <table id="param" runat="server" width="100%" cellspacing="3">
            <tr>
                <td>
                    <p class="comp" id="comp" runat="server"></p>
                    <h1 id="judul" class="title title-line" runat="server">Laporan Master Customer
                    </h1>

                </td>
            </tr>
        </table>
        <div id="headReport" runat="server"></div>
        <br />
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow VerticalAlign="Bottom">
                <asp:TableHeaderCell HorizontalAlign="Center">No. Urut</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Tgl. Complain</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Jenis Complain</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">PIC</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">No. Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Customer</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Keterangan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Solusi</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Tgl. Solved</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Status</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Project</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
<script type="text/javascript">
    function call(nomor) {
        document.getElementById('customer').value = nomor;
    }
</script>
</html>
