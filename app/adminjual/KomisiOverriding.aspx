<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KomisiOverriding.aspx.cs"
    Inherits="ISC064.ADMINJUAL.KomisiOverriding" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Komisi Overriding</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Komisi - Overriding">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Penerima Overriding</h1>
        <br />
        <asp:Table ID="rpt" runat="server" CssClass="blue-skin" CellSpacing="1">
            <asp:TableHeaderRow>
                <asp:TableHeaderCell>Jabatan</asp:TableHeaderCell>
                <asp:TableHeaderCell>Overriding Project (%)</asp:TableHeaderCell>
                <asp:TableHeaderCell>Overriding Cross Selling (%)</asp:TableHeaderCell>
                <asp:TableHeaderCell></asp:TableHeaderCell>
            </asp:TableHeaderRow>
        </asp:Table>

    </form>
</body>
</html>
