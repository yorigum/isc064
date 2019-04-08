<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.Laporan.MasterKomisiBayar" CodeFile="PDFMasterKomisiBayar.aspx.cs" %>

<!DOCTYPE html>
<html>
<head>
    <title>Laporan Komisi dalam Proses Pembayaran </title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Komisi dalam Proses Pembayaran">
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
                    <h1 id="judul" runat="server" class="title title-line">Laporan Komisi dalam Proses Pembayaran
                    </h1>
                </td>
            </tr>
        </table>
        <div id="headReport" runat="server"></div>
        <br />
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow VerticalAlign="Bottom">
                <asp:TableHeaderCell HorizontalAlign="Left">No Urut</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Status</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">No. Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Tgl. Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Customer</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Luas SG</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Nama Sales</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">NPWP</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">No. Rekening</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Nilai DPP</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Komisi</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>

        <script language="javascript" type="text/javascript">
            function popJadwalKomisi(NoKontrak) {
                openModal('../KontrakEdit.aspx?NoKontrak=' + NoKontrak, '800', '600');
            }
        </script>
    </form>
</body>
</html>
