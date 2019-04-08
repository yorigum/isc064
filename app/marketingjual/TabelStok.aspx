<%@ Reference Page="~/TabelStok2.aspx" %>

<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.TabelStok" CodeFile="TabelStok.aspx.cs" %>

<!DOCTYPE html>
<html>
<head>
    <title>TABLE STOCK</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <meta http-equiv="Refresh" content="60">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <style type="text/css">
        A {
            COLOR: blue;
        }
            A:hover {
                COLOR: black;
            }
    </style>
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server">
        <span class="title">Tabel Stock</span><br />
        <asp:DropDownList runat="server" ID="project" AutoPostBack="true"></asp:DropDownList>
        <div style="font-size: large; margin: 5px; padding: 20px">
            <asp:Table ID="rpt" runat="server" CssClass="blue-skin tb" CellSpacing="1">
                <asp:TableRow HorizontalAlign="Left">
                </asp:TableRow>
            </asp:Table>
        </div>
    </form>
</body>
</html>
