<%@ Page Language="c#" Inherits="ISC064.COLLECTION.Laporan.LaporanKartuPiutang" CodeFile="PDFLaporanKartuPiutang.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Laporan Kartu Piutang</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Kartu Piutang - Hal.1">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server">
        <div style="display: none">
            <uc1:Head ID="Head1" runat="server"></uc1:Head>
        </div>
        <table id="param" cellspacing="3" width="100%" runat="server">
            <tr>
                <td>
                    <p class="comp" id="comp" runat="server"></p>
                    <h1 id="judul" runat="server" class="title">Laporan Kartu Piutang
                    </h1>
                </td>
            </tr>
        </table>
        <asp:PlaceHolder ID="report" runat="server"></asp:PlaceHolder>
        <asp:Table ID="rpt" runat="server"></asp:Table>
        <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
        <asp:PlaceHolder ID="rp" runat="server"></asp:PlaceHolder>
    </form>
</body>
</html>
