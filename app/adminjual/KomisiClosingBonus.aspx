<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KomisiClosingBonus.aspx.cs"
    Inherits="ISC064.ADMINJUAL.KomisiClosingBonus" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Setup Closing Fee</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Sales - Setup Closing Fee">
</head>
<body>
    <form id="Form1" method="post" runat="server" class="cnt" style="padding: 10px;">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Setup Closing Fee</h1>
        <br />
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableHeaderRow>
                <asp:TableHeaderCell>Level</asp:TableHeaderCell>
                <asp:TableHeaderCell>Nilai Bawah (IDR)</asp:TableHeaderCell>
                <asp:TableHeaderCell>Nilai Atas (IDR)</asp:TableHeaderCell>
                <asp:TableHeaderCell>Nilai General Manager (IDR)</asp:TableHeaderCell>
                <asp:TableHeaderCell>Nilai Sales Manager (IDR)</asp:TableHeaderCell>
                <asp:TableHeaderCell>Nilai Marketing (IDR)</asp:TableHeaderCell>
                <asp:TableHeaderCell></asp:TableHeaderCell>
            </asp:TableHeaderRow>
        </asp:Table>
    </form>
</body>
</html>
