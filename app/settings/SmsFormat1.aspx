<%--<%@ Page Language="c#" Inherits="ISC064.SETTINGS.SmsFormat1" CodeFile="SmsFormat1.aspx.cs" %>--%>

<%@ Page Language="c#" Inherits="ISC064.SETTINGS.SmsFormat1" CodeFile="SmsFormat1.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Setup Format SMS</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Setup - Format SMS">
</head>
<body class="body-padding">
    <form id="Form2" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Setup Format SMS</h1>
        <br>
        <asp:Table ID="tb" runat="server">
            <asp:TableHeaderRow>
                <asp:TableHeaderCell>Tipe</asp:TableHeaderCell>
            </asp:TableHeaderRow>
        </asp:Table>
    </form>
</body>
</html>
