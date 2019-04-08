<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetupEmail.aspx.cs" Inherits="ISC064.SETTINGS.SetupEmail" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>

<html>
<head>
    <title>Setup Email</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Setup Email">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Setup Email</h1>
        <div>
            <p class="feed">
                <asp:Label ID="feed" runat="server"></asp:Label>
            </p>
            <table>
                <tr>
                    <td>Project</td>
                    <td>:</td>
                    <td>
                        <asp:DropDownList ID="project" runat="server" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Email Pengirim
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:TextBox ID="emailfrom" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Password
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:TextBox ID="password" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Nama yang ditampilkan
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:TextBox ID="displayname" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        SMTP
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:TextBox ID="smtp" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Port
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:TextBox ID="port" runat="server" CssClass="txt_num"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                    <td>
                        <asp:LinkButton ID="ok" runat="server" CssClass="btn btn-blue" Width="75" OnClick="ok_Click" ><i class="fa fa-share"></i> OK</asp:LinkButton>                        
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
