<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Numerator.aspx.cs" Inherits="ISC064.SETTINGS.Numerator" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="~/Head.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>Numerator</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1" />
    <meta name="CODE_LANGUAGE" content="C#" />
    <meta name="vs_defaultClientScript" content="JavaScript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
    <link href="/Media/Style.css" type="text/css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css" />
    <meta name="ctrl" content="1">
    <meta name="sec" content="Numerator">
    <meta http-equiv="pragma" content="no-cache">
    <base target="_self">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27){window.close()}"
    onload="document.getElementById('keyword').select()">
    <form id="Form1" method="post" runat="server">
        <uc1:Head ID="Head" runat="server" />
        <h1 class="title title-line">Numerator</h1>
        <br />
        <asp:DropDownList ID="project" runat="server" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged">            
        </asp:DropDownList>
        <table class="tb blue-skin">
            <tr>
                <th>Nama
                </th>
                <th>Modul
                </th>
                <th>Numerator
                </th>
            </tr>
            <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
        </table>
        <p class="feed">
            <asp:Label ID="feed" runat="server"></asp:Label>
        </p>
    </form>
</body>
</html>
