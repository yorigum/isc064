<%@ Reference Page="~/Acc.aspx" %>

<%@ Page Language="c#" Inherits="ISC064.SETTINGS.AlamatEmailEdit" CodeFile="AlamatEmailEdit.aspx.cs" %>

<!DOCTYPE html>
<html>
<head>
    <title>Edit Email</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="5">
    <meta name="sec" content="Set Up - Edit Email">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27) window.close()">
    <script type="text/javascript" src="/Js/Common.js"></script>
    <script type="text/javascript" src="/Js/NumberFormat.js"></script>
    <form id="Form1" method="post" runat="server">
        <h1 class="title title-line">Edit Email</h1>
        <table cellspacing="5">
            <tr>
                <td width="100%"></td>
                <td>
                    <label class="ibtn ibtn-file">
                        <input type="button" class="btn btn-blue btn-ico" value="  Log  " id="btnlog" runat="server" name="btnlog"
                            accesskey="l">
                    </label>
                </td>
                <td>
                    <label class="ibtn ibtn-remove">
                        <input type="button" class="btn btn-red btn-ico" value="Delete" id="btndel" runat="server" name="btndel"
                            accesskey="d">
                    </label>
                </td>
            </tr>
        </table>
        <table cellspacing="5">
            <tr>
                <td><b>ID</b></td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="id" runat="server" Width="160" MaxLength="20" CssClass="txt" ReadOnly="true"></asp:TextBox>
                    <asp:Label ID="idc" runat="server" CssClass="err"></asp:Label>
                </td>
                <tr>
                    <td><b>Email</b></td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID="email" runat="server" Width="150" CssClass="txt" MaxLength="50"></asp:TextBox>
                        <asp:Label ID="emailc" runat="server" CssClass="err"></asp:Label>
                    </td>
                </tr>
            <tr>
                <td><b>Project</b></td>                
                <td>:</td>
                <td>
                    <asp:DropDownList ID="project" runat="server"></asp:DropDownList>
                </td>
            </tr>
        </table>
        <table style="height: 50px">
            <tr>
                <td>
                    <asp:LinkButton ID="ok" runat="server" CssClass="btn btn-blue" Width="75" OnClick="ok_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
                </td>
                <td>
                    <asp:LinkButton ID="save" runat="server" CssClass="btn btn-orange" Width="75" AccessKey="a" OnClick="save_Click"><i class="fa fa-check"></i> Apply </asp:LinkButton>
                </td>
                <td>
                    <input id="cancel" type="button" onclick="window.close()" class="btn btn-red" value="Cancel">
                </td>
                <td style="padding-left: 10px">
                    <p class="feed">
                        <asp:Label ID="feed" runat="server"></asp:Label>
                    </p>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
