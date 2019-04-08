<%@ Page Language="c#" Inherits="ISC064.FINANCEAR.SlipLaporan" CodeFile="SlipLaporan.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Laporan Slip Setoran</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Slip Setoran">
</head>
<body>
    <form id="Form1" method="post" runat="server" class="cnt">
        <input type="text" style="display: none">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        </UC1:HEAD>
			<h1>Laporan Slip Setoran</h1>
        <br />
        <table cellspacing="3">
            <tr>
                <td>No. Slip</td>
                <td>:</td>
                <td>
                    <asp:Label ID="noslip" runat="server" Font-Size="14pt" Font-Bold="true"></asp:Label></td>
            </tr>
            <tr>
                <td>Tgl. Slip</td>
                <td>:</td>
                <td>
                    <asp:Label ID="tglslip" runat="server"></asp:Label></td>
            </tr>
        </table>
        <asp:Table ID="rpt" runat="server" CssClass="tb" CellSpacing="3">
            <asp:TableRow>
                <asp:TableHeaderCell HorizontalAlign="Left" Width="60">No. BG</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Width="70">Tgl. BG</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Width="60">No. TTS</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Width="200">Customer</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Width="100">Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="90">Nilai</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>
