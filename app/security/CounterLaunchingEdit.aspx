<%@ Reference Page="~/SecLevel.aspx" %>

<%@ Page Language="c#" Inherits="ISC064.SECURITY.EditUser" CodeFile="CounterLaunchingEdit.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="HeadCounterLaunching" Src="HeadCounterLaunching.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavCounterLaunching" Src="NavCounterLaunching.ascx" %>
<!doctype html>
<html>
<head>
    <title>Security - Counter Launching Edit</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="5">
    <meta name="sec" content="Security - Edit Counter Launching">
</head>
<body onkeyup="if(event.keyCode==27)window.close()">
    <form id="Form1" method="post" runat="server">
        <div class="content-header">
            <uc1:NavCounterLaunching ID="NavCounterLaunching" runat="server" Aktif="1"></uc1:NavCounterLaunching>
        </div>
        <div class="tabdata">
            <div class="pad">
                <uc1:HeadCounterLaunching ID="HeadCounterLaunching" runat="server"></uc1:HeadCounterLaunching>
                <table cellspacing="5">
                    <tr>
                        <td width="100%"></td>
                        <td>
                            <label class="ibtn ibtn-remove">
                                <input type="button" class="btn btn-red btn-ico" value="Delete" id="btndel" runat="server" name="btndel" accesskey="d">
                            </label>
                        </td>
                    </tr>
                </table>
                <hr size="1" noshade style="color: silver; margin: 0; border-bottom: 1px solid #ededed">
               <table cellspacing="5">
                        <tr>
                            <td >Nama </td>
                            <td>
                                <asp:TextBox ID="nama" runat="server" CssClass="txt igroup" Width="300" MaxLength="50"></asp:TextBox>
                                <asp:Label ID="namac" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td >Nomor </td>

                            <td>
                                <asp:TextBox ID="nomor" runat="server" CssClass="txt igroup" Width="100" MaxLength="50"></asp:TextBox>
                                <asp:Label ID="nomorc" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>User</td>
                            <td>
                                <asp:DropDownList runat="server" ID="username"></asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                <table style="height: 50px;">
                    <tr>
                        <td>
                            <asp:LinkButton ID="ok" runat="server" CssClass="btn btn-blue" Width="75" OnClick="ok_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
                        </td>
                        <td>
                            <asp:LinkButton ID="save" runat="server" CssClass="btn btn-orange" Width="75" AccessKey="a" OnClick="save_Click"><i class="fa fa-check"></i> Apply </asp:LinkButton>
                        </td>
                        <td>
                            <input id="cancel" type="button" onclick="window.close()" class="btn btn-red" value="Cancel" style="width: 75px">
                        </td>
                        <td style="padding-left: 10px">
                            <p class="feed">
                                <asp:Label ID="feed" runat="server"></asp:Label>
                            </p>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
