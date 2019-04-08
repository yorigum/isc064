<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.ClosingLangsungApprov" CodeFile="ClosingLangsungApprov.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Closing Langsung Approval</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Closing Langsung Approval">
</head>
<body>
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Closing Langsung Approval</h1>
        <p><b><i>Halaman 3 dari 3 </i></b></p>
        <br />
        <br />
        <h2 style="color: Brown; border: 1 solid silver; padding: 10">Closing Langsung Approval
        </h2>
        <br />
        <table cellspacing="5">
            <tr>
                <td>No. Kontrak Approval</td>
                <td>:</td>
                <td>
                    <asp:Label ID="nokontrak" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Unit</td>
                <td>:</td>
                <td>
                    <asp:Label ID="unit" runat="server" Font-Bold="True"></asp:Label>
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
                <td>Sales</td>
                <td>:</td>
                <td>
                    <asp:Label ID="agent" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Tgl Kontrak</td>
                <td>:</td>
                <td>
                    <asp:Label ID="tglkontrak" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
                    </table>
    </form>
</body>
</html>
