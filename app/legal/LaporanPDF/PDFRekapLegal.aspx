<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.LEGAL.Laporan.LaporanPPJB" CodeFile="PDFRekapLegal.aspx.cs" %>

<!DOCTYPE html>
<html>
<head>
    <title>Rekap Legal</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan PPJB">
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
                    <h1 id="judul" runat="server" class="title title-line">Rekap Legal
                    </h1>
                </td>
            </tr>
        </table>
        <div id="headReport" runat="server"></div>
        <%--<asp:Label ID="lblHeader" runat="server"></asp:Label>--%>
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellPadding="10" CellSpacing="1">
           <asp:TableRow VerticalAlign="Bottom" BackColor="#5c9bd1" ForeColor="white">
                <asp:TableHeaderCell HorizontalAlign="Left" VerticalAlign="Middle" RowSpan="2">No</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" VerticalAlign="Middle" RowSpan="2">No Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" VerticalAlign="Middle" RowSpan="2">Customer</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" VerticalAlign="Middle" RowSpan="2">Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="4">PPJB</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="5">AJB</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="4">BAST</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="4">IMB</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="4">Sertifikat</asp:TableHeaderCell>
            </asp:TableRow>
            <asp:TableRow BackColor="#5c9bd1" ForeColor="white">
                <asp:TableHeaderCell HorizontalAlign="Center">Kelengkapan Berkas</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Target PPJB</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">PPJB</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">No PPJB</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Tanda Tangan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Target AJB</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">AJB</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">No AJB</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Tanda Tangan AJB</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Target BAST</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">BAST</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">No BAST</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Tanda Tangan BAST</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Target IMB</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Proses IMB</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">IMB</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">No IMB</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Target Sertifikat</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Proses Sertifikat</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">Sertifikat</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center">No Sertifikat</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>
