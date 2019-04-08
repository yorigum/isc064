<%@ Page Language="c#" Inherits="ISC064.SETTINGS.HtmlEditor" CodeFile="HtmlEditor.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Html Editor</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1" />
    <meta name="CODE_LANGUAGE" content="C#" />
    <meta name="vs_defaultClientScript" content="JavaScript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
    <link href="/Media/Style.css" type="text/css" rel="stylesheet" />
    <meta name="ctrl" content="1">
    <meta name="sec" content="Html Editor">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Html Editor</h1>
        <asp:DropDownList ID="project" runat="server" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged">
            <asp:ListItem>Pilih</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="modul" runat="server" AutoPostBack="true" OnSelectedIndexChanged="modul_SelectedIndexChanged">
            <asp:ListItem Value="MARKETINGJUAL" Selected="True">Sales</asp:ListItem>
            <asp:ListItem Value="COLLECTION">Collection</asp:ListItem>
            <asp:ListItem Value="FINANCEAR">Finance & AR</asp:ListItem>
        </asp:DropDownList>
        <a href="#" onclick="PopHelpHtmlEditor()" class="btn btn-green" style="color: white"><i class="fa fa-question"></i></a>
        <asp:Table ID="list" runat="server" CssClass="tb blue-skin" CellSpacing="1" Style="min-width: 80%">
            <asp:TableRow HorizontalAlign="Left" VerticalAlign="Bottom">
                <asp:TableHeaderCell>Halaman</asp:TableHeaderCell>
                <asp:TableHeaderCell>Modul</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>
