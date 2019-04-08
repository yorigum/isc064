<%@ Page Language="c#" Inherits="ISC064.ADMINJUAL.Reminder" CodeFile="Reminder.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Reminder</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Reminder">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Reminder</h1>
        <br />
        <asp:DropDownList runat="server" ID="project" AutoPostBack="true"></asp:DropDownList>
        <br />
        <table class="blue-list-skin">
            <tr>
                <td class="remind-td-num">
                    <a href="" id="pending" runat="server">
                        <asp:Label ID="countPL1" runat="server"></asp:Label></a>
                </td>
                <td class="remind-td-text">
                    <a href="" id="pending2" runat="server">Price List - Pending</a>
                    <p class="remind-span">
                        Unit yang belum memiliki price list.
                    </p>
                </td>
            </tr>
            <tr>
                <td class="remind-td-num">
                    <a href="" id="edit" runat="server">
                        <asp:Label ID="countPL2" runat="server"></asp:Label></a>
                </td>
                <td class="remind-td-text">
                    <a href="" id="edit2" runat="server">Price List - Edit Unit</a>
                    <p class="remind-span">
                        Price list sudah ditentukan tetapi unit di-edit.
                    </p>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
