<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.PDFSkema" CodeFile="PDFSkema.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Print Skema Cara Bayar</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Print.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Print Skema Cara Bayar">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27&&confirm('Apakah anda ingin menutup layar print ini?')) window.close();">
    <form id="Form1" method="post" runat="server">
        <h1 class="title title-line">Kalkulator Skema Cara Bayar</h1>
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
           <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
            <br />
            <p class="lbl" style="font-size:16px">JADWAL TAGIHAN</p>            
            <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">                
<%--                <asp:TableRow HorizontalAlign="Left">
                    <asp:TableHeaderCell ColumnSpan="5">JADWAL TAGIHAN</asp:TableHeaderCell>
                </asp:TableRow>--%>
                <asp:TableRow HorizontalAlign="Left">
                    <asp:TableHeaderCell>No.</asp:TableHeaderCell>
                    <asp:TableHeaderCell Width="300">Keterangan</asp:TableHeaderCell>
                    <asp:TableHeaderCell Width="100">Jatuh Tempo</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Right" Width="120">Jumlah</asp:TableHeaderCell>
                    <asp:TableHeaderCell></asp:TableHeaderCell>
                </asp:TableRow>
            </asp:Table>
    </form>
</body>
</html>
