<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KomisiAgent.aspx.cs" Inherits="ISC064.ADMINJUAL.KomisiAgent" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Setup Komisi Pokok</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Sales - Setup Komisi Pokok">
</head>
<body>
    <form id="Form1" method="post" runat="server" class="cnt" style="padding: 10px;">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Setup Komisi Pokok</h1>
        <br />
        <div style="padding: 10px">
            <table>
                <tr>
                    <td colspan="3"><strong>Tanpa Refferal</strong>
                    </td>
                </tr>
                <tr>
                    <td>Agent
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:TextBox ID="agent" runat="server" Style="text-align: right"></asp:TextBox>
                    </td>
                    <td>%
                    </td>
                </tr>
                <tr>
                    <td colspan="3"></td>
                </tr>
                <tr>
                    <td colspan="3"></td>
                </tr>
            </table>
            <br />
            <table>
                <tr>
                    <td colspan="3"><strong>Dengan Refferal</strong>
                    </td>
                </tr>
                <tr>
                    <td>Agent
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:TextBox ID="AgentReff" runat="server" Style="text-align: right"></asp:TextBox>
                    </td>
                    <td>%
                    </td>
                </tr>
                <tr>
                    <td>Refferal
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:TextBox ID="refferal" runat="server" Style="text-align: right"></asp:TextBox>
                    </td>
                    <td>%
                    </td>
                </tr>
                <tr>
                    <td colspan="4"></td>
                </tr>
                <tr>
                    <td colspan="4"></td>
                </tr>
                <tr>
                    <td>
                        <asp:LinkButton ID="save" runat="server" CssClass="btn btn-orange" Width="75" AccessKey="a" OnClick="save_Click"><i class="fa fa-check"></i> Apply </asp:LinkButton>
                    </td>
                    <td colspan="3">
                        <asp:Label ID="feed" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
