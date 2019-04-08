<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Project2.aspx.cs" Inherits="ISC064.SECURITY.Project2" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>

<html lang="en">
<head>
    <title>Project</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Project">
</head>
<body class="body-padding">
    <form id="form1" runat="server">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Edit Project</h1>
        <table>
            <tr>
                <td>Kode Project</td>
                <td>:</td>
                <td><asp:TextBox ID="project" runat="server" CssClass="txt igroup" Width="300" ReadOnly="true"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Nama Project</td>
                <td>:</td>
                <td><asp:TextBox ID="nama" runat="server" CssClass="txt igroup" Width="300" ReadOnly="true"></asp:TextBox></td>
            </tr>
        </table>
    </form>
</body>
</html>