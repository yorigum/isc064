<%@ Page Language="c#" Inherits="ISC064.KOMISI.Reminder" CodeFile="Reminder.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Reminder</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Reminder">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Reminder</h1>
        <br>
        <asp:DropDownList ID="project" runat="server" AutoPostBack="true">
        </asp:DropDownList>
        <br />
        <table class="blue-list-skin">
            <tr>
                <td class="remind-td-num">
                    <a href="" id="Kom" runat="server">
                        <asp:Label ID="countKom" runat="server"></asp:Label>
                    </a>
                </td>
                <td class="remind-td-text">
                    <a href="" id="Kom2" runat="server">Komisi Belum Generate</a>
                    <p class="remind-span">
                        Daftar kontrak yang komisinya belum digenerate.
                    </p>
                </td>
            </tr>
            <tr>
                <td class="remind-td-num">
                    <a href="" id="CairKom" runat="server">
                        <asp:Label ID="countCairKom" runat="server"></asp:Label>
                    </a>
                </td>
                <td class="remind-td-text">
                    <a href="" id="CairKom2" runat="server">Komisi Sudah Memenuhi Syarat Cair</a>
                    <p class="remind-span">
                        Daftar kontrak yang perhitungan komisinya Sudah Memenuhi Syarat Cair.
                    </p>
                </td>
            </tr>
            <tr>
                <td class="remind-td-num">
                    <a href="" id="KomP" runat="server">
                        <asp:Label ID="countKomP" runat="server"></asp:Label>
                    </a>
                </td>
                <td class="remind-td-text">
                    <a href="" id="KomP2" runat="server">Komisi Yang Belum Diajukan</a>
                    <p class="remind-span">
                        Daftar kontrak yang komisinya belum diajukan.
                    </p>
                </td>
            </tr>
            
        </table>
    </form>
</body>
</html>
