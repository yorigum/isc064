<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NUPRefundFin.aspx.cs" Inherits="ISC064.NUP.NUPRefundFin" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>NUP - Refund NUP</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="NUP - Refund NUP (Hal. 2)">
</head>
<body>
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1>Refund NUP</h1>
        <p style="font-size: 8pt; color: #666;">Halaman 2 dari 2</p>
        <br />
        <br />
        <h2 style="color: Brown; border: 1 solid silver; padding: 10">Refund NUP Berhasil
        </h2>
        <br />
        <table cellspacing="5">
            <tr>
                <td>NUP</td>
                <td>:</td>
                <td>
                    <asp:Label ID="nonup" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Customer</td>
                <td>:</td>
                <td>
                    <asp:Label ID="customer" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Agent</td>
                <td>:</td>
                <td>
                    <asp:Label ID="agent" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
        </table>
        <br>
        <h1><a id="asp" runat="server">Print Refund</a></h1>
    </form>
</body>
</html>
