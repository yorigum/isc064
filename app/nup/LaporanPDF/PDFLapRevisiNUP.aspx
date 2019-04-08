<%@ Page Language="c#" Inherits="ISC064.NUP.Laporan.LapRevisiNUP" CodeFile="PDFLapRevisiNUP.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Laporan NUP</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="LaporanSummary NUP">
</head>
<body onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server">
        <div style="display: none">
            <div style="display: none">
                <uc1:Head ID="Head1" runat="server"></uc1:Head>
            </div>
        </div>
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="3">
            <asp:TableRow>
                <asp:TableCell ColumnSpan="18">
                    <asp:Label ID="lblHeader" runat="server" Font-Size="12pt" Font-Bold="True"></asp:Label>
                    <br />
                    <asp:Label ID="lblSubHeader" runat="server" Font-Size="9pt" Font-Bold="True"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow VerticalAlign="Bottom">
                <asp:TableHeaderCell HorizontalAlign="center"  BackColor="#1E90FF" ForeColor="White">No.</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center"  BackColor="#1E90FF" ForeColor="White">Tanggal<br/>Daftar</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center"  BackColor="#1E90FF" ForeColor="White">Nomor NUP</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center"  BackColor="#1E90FF" ForeColor="White">Tanggal<br/>Ganti Nama</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center"  BackColor="#1E90FF" ForeColor="White">Nama Pemesan - Lama</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center"  BackColor="#1E90FF" ForeColor="White">Nama Pemesan - Baru</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>
