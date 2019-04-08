<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RumusDenda.aspx.cs" Inherits="ISC064.SETTINGS.RumusDenda" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>

<html>
<head>
    <title>Rumus Denda</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Rumus Denda">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Rumus Denda</h1>
        <div>
            <asp:DropDownList ID="project" runat="server" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged"></asp:DropDownList>
            <table>
                <tr>
                    <td>Grace Period
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:TextBox ID="grace" runat="server" CssClass="txt_num" Width="115"></asp:TextBox> Hari
                    </td>
                </tr>
                <tr>
                    <td>Tarif
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:TextBox ID="rumus1" runat="server" CssClass="txt_num" Width="50"></asp:TextBox> / 
                        <asp:TextBox ID="rumus2" runat="server" CssClass="txt_num" Width="50"></asp:TextBox> / Hari
                    </td>
                </tr>
                <tr>
                    <td>
                        Denda Dihitung
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:TextBox ID="berlaku" runat="server" CssClass="txt_num" Width="115"></asp:TextBox> Hari setelah hari telat
                    </td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                    <td>
                            <asp:LinkButton id="ok" runat="server" cssclass="btn btn-blue" width="75" onclick="ok_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
                    </td>
                </tr>
            </table>
            <p class="feed">
                <asp:Label ID="feed" runat="server"></asp:Label>
            </p>
        </div>
    </form>
</body>
</html>
