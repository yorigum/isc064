<%@ Page Language="c#" Inherits="ISC064.FINANCEAR.Slip" CodeFile="Slip.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html >
<html>
<head>
    <title>Slip Setoran</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Slip Setoran">
</head>
<body>
    <form id="Form1" method="post" runat="server" class="cnt">
        <input type="text" style="display: none">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
			<h1>Slip Setoran</h1>
        <br />
        <asp:Table ID="tb" runat="server" CssClass="tb" CellSpacing="3">
            <asp:TableRow>
                <asp:TableHeaderCell HorizontalAlign="Center" Width="60">No. Slip</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="80">Jumlah TTS</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="80">Jumlah Giro</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="90">Total</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>
