<%@ Reference Page="~/SecLevel.aspx" %>

<%@ Page Language="c#" Inherits="ISC064.SECURITY.EditUser" CodeFile="EditUser.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="HeadUser" Src="HeadUser.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavUser" Src="NavUser.ascx" %>
<!doctype html>
<html>
<head>
    <title>Edit User</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="5">
    <meta name="sec" content="Username - Edit User">
</head>
<body onkeyup="if(event.keyCode==27)window.close()">
    <form id="Form1" method="post" runat="server">
        <div class="content-header">
            <uc1:NavUser ID="NavUser1" runat="server" Aktif="1"></uc1:NavUser>
        </div>
        <div class="tabdata">
            <div class="pad">
                <uc1:HeadUser ID="HeadUser1" runat="server"></uc1:HeadUser>
                <table cellspacing="5">
                    <tr>
                        <td width="100%"></td>
                        <td>
                            <label class="ibtn ibtn-file">
                                <input type="button" class="btn btn-blue btn-ico" value="Log" id="btnlog" runat="server" name="btnlog" accesskey="l">
                            </label>
                        </td>
                        <td>
                            <label class="ibtn ibtn-remove">
                                <input type="button" class="btn btn-red btn-ico" value="Delete" id="btndel" runat="server" name="btndel" accesskey="d">
                            </label>
                        </td>
                    </tr>
                </table>
                <hr size="1" noshade style="color: silver; margin: 0; border-bottom: 1px solid #ededed">
                <table>
                    <tr>
                        <td class="stamp">Input :
								<asp:Label ID="tglinput" runat="server"></asp:Label>
                        </td>
                        <td class="stamp">Password :
								<asp:Label ID="tglpass" runat="server"></asp:Label>
                        </td>
                        <td class="stamp">Log-In :
								<asp:Label ID="tgllogin" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table cellspacing="5">
                    <tr>
                        <td colspan="3">
                            <p><b>Identitas Username</b></p>
                        </td>
                    </tr>
                    <tr>
                        <td class="igroup-label"><a id="aKey" runat="server">Kode</a></td>
                        <td>
                            <asp:TextBox ID="userid" runat="server" CssClass="txt igroup igroup-label" Width="75" MaxLength="20" ReadOnly="True"
                                Font-Bold="True"></asp:TextBox>
                            <asp:Label ID="status" runat="server" Font-Bold="True" Font-Size="14"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="igroup-label">Nama Lengkap</td>
                        <td>
                            <asp:TextBox ID="nama" runat="server" CssClass="txt igroup" Width="300" MaxLength="50"></asp:TextBox>
                            <asp:Label ID="namac" runat="server" CssClass="err"></asp:Label>
                        </td>
                    </tr>                    
                    <tr>
                        <td class="igroup-label">Email</td>
                        <td>
                            <asp:TextBox ID="email" runat="server" CssClass="txt igroup" Width="300" MaxLength="50"></asp:TextBox>
                            <asp:Label ID="emailc" runat="server" CssClass="err"></asp:Label>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="igroup-label">Security Level</td>
                        <td>
                            <asp:ListBox ID="seclevel" runat="server" CssClass="ddl" Width="250"></asp:ListBox>
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td>Kode Sales</td>
                        <td>
                            <asp:DropDownList ID="agent" runat="server" Width="400" CssClass="ddl">
                                <asp:ListItem Value="0">Sales :</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <br>
                            <p><b>Aturan Password</b></p>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:CheckBox Checked="True" ID="gantipass" runat="server" Text="User harus rubah password pada saat Log-In berikutnya"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">User diwajbkan ganti password setiap
								<asp:TextBox ID="rotasipass" runat="server" CssClass="txt_center" Width="50">6</asp:TextBox>
                            bulan sekali. &nbsp;
								<asp:Label ID="rotasipassc" runat="server" CssClass="err"></asp:Label>
                            <br>
                            (0 = Password tidak pernah expire)
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
