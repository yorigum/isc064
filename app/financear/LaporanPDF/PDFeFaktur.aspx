<%@ Page Language="c#" Inherits="ISC064.FINANCEAR.Laporan.eFaktur" CodeFile="PDFeFaktur.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE html >
<html>
<head>
    <title>Laporan Master Tanda Terima Sementara</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan e-Faktur">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27&&confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server">
        <div style="display: none">
            <uc1:Head ID="Head1" runat="server"></uc1:Head>
        </div>
        <table id="param" runat="server" width="100%" cellspacing="3">
            <tr>
                <td>
                    <div class="underline">
                        <p class="comp" id="comp" runat="server"></p>
                        <h1 id="judul" runat="server">Laporan e-Faktur
                        </h1>
                    </div>
                </td>
            </tr>
        </table>
        <div id="headReport" runat="server"></div>
        <br />
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow VerticalAlign="Bottom" CssClass="tb blue-skin">
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false">FK<br /> LT<br />OF<br /></asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false">KD_JENIS_TRANSAKSI<br />NPWP<br />KODE_OBJEK<br /></asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false">FG_PENGGANTI<br />NAMA<br />NAMA<br /></asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false">NOMOR_FAKTUR<br />JALAN<br />HARGA_SATUAN<br /></asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false">MASA_PAJAK<br />BLOK<br />JUMLAH_BARANG<br /></asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false">TAHUN_PAJAK<br />NOMOR<br />HARGA_TOTAL<br /></asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false">TANGGAL_FAKTUR<br />RT<br />DISKON<br /></asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false">NPWP<br />RW<br />DPP<br /></asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false">NAMA<br />KECAMATAN<br />PPN<br /></asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false">ALAMAT_LENGKAP<br />KELURAHAN<br />TARIF_PPNBM<br /></asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false">JUMLAH_DPP<br />KABUPATEN<br />PPNBM<br /></asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false">JUMLAH_PPN<br />PROPINSI<br /><br /></asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false">JUMLAH_PPNBM<br />KODE_POS<br /><br /></asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false">ID_KETERANGAN_TAMBAHAN<br />NOMOR_TELEPON<br /><br /></asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false">FG_UANG_MUKA<br /><br /><br /></asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false">UANG_MUKA_DPP<br /><br /><br /></asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false">UANG_MUKA_PPN<br /><br /><br /></asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false">UANG_MUKA_PPNBM<br /><br /><br /></asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false">REFERENSI<br /><br /><br /></asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>
