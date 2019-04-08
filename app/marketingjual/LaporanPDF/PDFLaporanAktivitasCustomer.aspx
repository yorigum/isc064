<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.Laporan.LaporanGantiNama" CodeFile="PDFLaporanAktivitasCustomer.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Laporan Aktivitas Customer</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Pengalihan Hak">
</head>
<body onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server">
        <div style="display: none">
            <uc1:Head ID="Head1" runat="server"></uc1:Head>
        </div>
        <table id="param" runat="server" width="100%" cellspacing="3">
           
        </table>
        <div id="headReport" runat="server">
            <asp:Label ID="headJudul" runat="server"></asp:Label>
        </div>
        <asp:Label ID="lblA" runat="server"></asp:Label>
        <asp:Table ID="rptA" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow VerticalAlign="Bottom" BackColor="LightGray" HorizontalAlign="Center">
                <asp:TableHeaderCell HorizontalAlign="Center">No.</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Status</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Tgl. Reservasi</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Customer</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Sales</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">No. Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Luas SG</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">No. Urut Reservasi</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Batas Waktu</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">No. Urut Prioritas</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Nilai Pengikatan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">PIC</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Project</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
        <asp:Label ID="lblB" runat="server"></asp:Label>
        <asp:Table ID="rptB" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow VerticalAlign="Bottom" BackColor="LightGray" HorizontalAlign="Center">
                <asp:TableHeaderCell HorizontalAlign="Center">No.</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Tgl. Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">No. Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Customer</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Sales</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">No. Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Luas SG</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Tgl. Batal</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Alasan Pembatalan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Nilai Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Biaya Administrasi</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">PIC</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Project</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
        <asp:Label ID="lblC" runat="server"></asp:Label>
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow VerticalAlign="Bottom" BackColor="LightGray" HorizontalAlign="Center">
                <asp:TableHeaderCell HorizontalAlign="Center">No.</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Tgl. Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">No. Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Customer</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Sales</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Tgl. Pindah Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Unit Sebelumnya</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Luas SG</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Unit Sekarang</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Luas SG</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Nilai Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Biaya Administrasi</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">PIC</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Project</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
        <asp:Label ID="lblD" runat="server"></asp:Label>
        <asp:Table ID="rptD" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow VerticalAlign="Bottom" BackColor="LightGray" HorizontalAlign="Center">
                <asp:TableHeaderCell HorizontalAlign="Center">No.</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Tgl. Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">No. Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">No. Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Luas SG</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Sales</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Tgl. Pengalihan Hak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Nama Sebelumnya</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Nama Sekarang</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Nilai Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Biaya Administrasi</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">PIC</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Project</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
        <asp:PlaceHolder ID="rp" runat="server"></asp:PlaceHolder>
    </form>
</body>
</html>
