<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.FINANCEAR.Laporan.LaporanDetilPembayaran" CodeFile="PDFLaporanDetilPembayaran.aspx.cs" %>

<!DOCTYPE html>
<html>
<head>
    <title>Laporan Pembayaran Detail</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Pembayaran Detail">
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
                    <h1 id="judul" runat="server" class="title title-line">Laporan Detail Pembayaran
                    </h1>
                </td>
            </tr>
        </table>
        <div id="headReport" runat="server"></div>
        <br />
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow VerticalAlign="Bottom">
                <asp:TableHeaderCell HorizontalAlign="Center" Wrap="false">NO</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" Wrap="false">KONTRAK</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" Wrap="false">UNIT</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" Wrap="false">CUSTOMER</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" Wrap="false">BF</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" Wrap="false">BANK (BF)</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" Wrap="false">DP</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" Wrap="false">BANK (DP)</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" Wrap="false">ANG</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" Wrap="false">BANK (ANG)</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" Wrap="false">ADM</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" Wrap="false">BANK (ADM)</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" Wrap="false">SALDO AWAL</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" Wrap="false">MEMO</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" Wrap="false">AKUMULASI PEMBAYARAN</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" Wrap="false">AKUMULASI PEMBAYARAN (SALDO AWAL)</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" Wrap="false">AKUMULASI PEMBAYARAN (MEMO)</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" Wrap="false">HARGA JUAL INC PPN</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" Wrap="false">%</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>
