<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KontrakIncludeBiaya.aspx.cs" Inherits="ISC064.SETTINGS.KontrakIncludeBiaya" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>

<html>
<head>
    <title>Mandatori</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Kontrak Include Biaya">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <h1 class="title title-line">Setup Nilai Kontrak Include Biaya</h1>
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <div>
            <p class="feed">
                <asp:Label ID="feed" runat="server"></asp:Label>
            </p>
            <asp:DropDownList ID="project" runat="server" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged"></asp:DropDownList>
            <table>
                <tr>
                    <td>Nilai Kontrak include Biaya
                    </td>
                    <td>:
                    </td>
                    <td>
                        <p class="pparam">
                            <asp:CheckBox ID="include" runat="server" Text="Ya" />
                        </p>
                    </td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                    <td>
                        <asp:LinkButton ID="ok" runat="server" CssClass="btn btn-blue" Width="75" OnClick="ok_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
