<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetupLaunching.aspx.cs" Inherits="ISC064.NUP.SetupLaunching" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>

<html>
<head>
    <title>Setup Aktivasi Launching</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Setting Approval On Off">

    <style>
        .center {
            text-align: center;
        }
    </style>
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Setup Aktivasi Launching</h1>
        <div>
            <asp:ScriptManager runat="server" ID="scriptmanager" EnablePartialRendering="true"></asp:ScriptManager>
            <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                <ContentTemplate>
            <table>
                <tr>
                    <td>
                        <asp:DropDownList ID="project" runat="server" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                </tr>
            </table>
            <br />
             <p class="feed">
                <asp:Label ID="feed" runat="server"></asp:Label>
            </p>
            <table class="tb blue-skin">
                <tr>
                    <th>No
                    </th>
                    <th>Data
                    </th>
                    <th>Aktivasi / Cancel Aktivasi
                    </th>
                </tr>
                <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
                <tr>
                    <td colspan="3">
                        <asp:LinkButton ID="ok" runat="server" CssClass="btn btn-blue" Width="75" OnClick="ok_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
                    </td>
                </tr>
            </table>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="ok" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
