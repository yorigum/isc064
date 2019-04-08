<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetupApproval.aspx.cs" Inherits="ISC064.SETTINGS.SetupApproval" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>

<html>
<head>
    <title>Setting Approval On Off</title>
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
        <h1 class="title title-line">Setting Approval On Off</h1>
        <div>
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
                    <th>Approval
                    </th>
                    <th>On / Off
                    </th>
                </tr>
                <tr>
                    <td>Approval Ganti Unit</td>
                    <td class="center">
                        <%--<asp:CheckBox ID="GU" runat="server" ToolTip="Checklist untuk mengaktifkan fitur approval Ganti Unit"></asp:CheckBox>--%>
                        <asp:RadioButtonList ID="GU" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="True">ON</asp:ListItem>
                            <asp:ListItem Value="False" Selected="True">OFF</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>Approval Pengalihan Hak</td>
                    <td class="center" style="border-style:none">
                        <%--<asp:CheckBox ID="GN" runat="server" ToolTip="Checklist untuk mengaktifkan fitur approval Pengalihan Hak"></asp:CheckBox>--%>
                        <asp:RadioButtonList ID="GN" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="True">ON</asp:ListItem>
                            <asp:ListItem Value="False" Selected="True">OFF</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>Approval Pembatalan Kontrak</td>
                    <td class="center">
                        <%--<asp:CheckBox ID="batal" runat="server" ToolTip="Checklist untuk mengaktifkan fitur approval Pembatalan Kontrak"></asp:CheckBox>--%>
                        <asp:RadioButtonList ID="batal" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="True">ON</asp:ListItem>
                            <asp:ListItem Value="False" Selected="True">OFF</asp:ListItem>
                        </asp:RadioButtonList>

                    </td>
                </tr>
                <tr>
                    <td>Approval Adjustment Kontrak</td>
                    <td class="center">
                        <%--<asp:CheckBox ID="adjust" runat="server" ToolTip="Checklist untuk mengaktifkan fitur approval Adjustment Kontrak"></asp:CheckBox>--%>
                        <asp:RadioButtonList ID="adjust" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="True">ON</asp:ListItem>
                            <asp:ListItem Value="False" Selected="True">OFF</asp:ListItem>
                        </asp:RadioButtonList>

                    </td>
                </tr>
                <tr>
                    <td>Approval Reschedule Tagihan</td>
                    <td class="center">
                        <%--<asp:CheckBox ID="resc" runat="server" ToolTip="Checklist untuk mengaktifkan fitur approval Reschedule Tagihan"></asp:CheckBox>--%>
                        <asp:RadioButtonList ID="resc" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="True">ON</asp:ListItem>
                            <asp:ListItem Value="False" Selected="True">OFF</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>Approval Customize Tagihan</td>
                    <td class="center">
                        <%--<asp:CheckBox ID="custom" runat="server" ToolTip="Checklist untuk mengaktifkan fitur approval Customize Tagihan"></asp:CheckBox>--%>
                        <asp:RadioButtonList ID="custom" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="True">ON</asp:ListItem>
                            <asp:ListItem Value="False" Selected="True">OFF</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>Approval Diskon</td>
                    <td class="center" style="border-style:hidden">
                        <%--<asp:CheckBox ID="diskon" runat="server" ToolTip="Checklist untuk mengaktifkan fitur approval Diskon"></asp:CheckBox>--%>
                        <asp:RadioButtonList ID="diskon" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="True">ON</asp:ListItem>
                            <asp:ListItem Value="False" Selected="True">OFF</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:LinkButton ID="ok" runat="server" CssClass="btn btn-blue" Width="75" OnClick="ok_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
