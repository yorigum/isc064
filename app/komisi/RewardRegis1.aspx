<%@ Page Language="c#" Inherits="ISC064.KOMISI.RewardRegis1" CodeFile="RewardRegis1.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Generate Reward</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Reward - Generate Reward">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Generate Reward</h1>
        <br>
        <table style="border: 1px solid #DCDCDC" cellspacing="5">
            <tr>
                <td>
                    <asp:DropDownList ID="skema" runat="server" Width="200">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="project" runat="server" Width="200" OnSelectedIndexChanged="project_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="display" runat="server" CssClass="btn btn-blue" Text="Display" OnClick="display_Click"></asp:Button>
                </td>
            </tr>
        </table>
        <br>
        <h4 id="periode" runat="server"></h4>
        <%--<div class="peach">
            Status : A = Aktif / B = Batal
        </div>--%>
        <table class="tb blue-skin" cellspacing="1">
            <tr align="left" valign="bottom">
                <th>Sales</th>
                <th class="right">Jumlah Unit</th>
                <th class>Nilai Penjualan</th>
                <th>Reward</th>
            </tr>
            <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
            <tr>
                <td colspan="4">
                    <asp:LinkButton ID="save" runat="server" CssClass="btn btn-blue" Width="75" OnClick="save_Click"><i class="fa fa-share"></i> OK
                    </asp:LinkButton>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
