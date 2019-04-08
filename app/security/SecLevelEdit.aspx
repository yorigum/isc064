<%@ Page Language="c#" Inherits="ISC064.SECURITY.SecLevelEdit" CodeFile="SecLevelEdit.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="HeadSecLevel" Src="HeadSecLevel.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavSecLevel" Src="NavSecLevel.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Edit Security Level</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="5">
    <meta name="sec" content="Setup Security Level - Edit Security Level">
</head>
<body onkeyup="if(event.keyCode==27) window.close()">
    <form id="Form1" method="post" runat="server">
        <div class="content-header">
            <uc1:NavSecLevel ID="NavSecLevel1" runat="server" Aktif="1"></uc1:NavSecLevel>
        </div>
        <div class="tabdata">
            <div class="pad">
                <uc1:HeadSecLevel ID="HeadSecLevel1" runat="server"></uc1:HeadSecLevel>
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
                <table cellpadding="0" cellspacing="0">
                    <tr valign="top">
                        <td style="width: 400px">
                            <table cellspacing="5">
                                <tr>
                                    <td class="igroup-label">Kode</td>
                                    
                                    <td>
                                        <asp:TextBox ID="kode" runat="server" Width="100" MaxLength="10" CssClass="txt"></asp:TextBox>
                                        <asp:Label ID="kodec" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="igroup-label">Nama</td>
                                    
                                    <td>
                                        <asp:TextBox ID="nama" runat="server" Width="220" CssClass="txt" MaxLength="50"></asp:TextBox>
                                        <asp:Label ID="namac" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <table height="50">
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="ok" runat="server" CssClass="btn btn-blue" Width="75" OnClick="ok_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
                                    </td>
                                    <td>
                                        <input id="cancel" type="button" onclick="window.close()" class="btn btn-red" value="Cancel" style="width: 75px">
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="save" runat="server" CssClass="btn btn-orange" Width="75" AccessKey="a" OnClick="save_Click"><i class="fa fa-check"></i> Apply </asp:LinkButton>
                                    </td>
                                    <td style="padding-left: 10px">
                                        <p class="feed">
                                            <asp:Label ID="feed" runat="server"></asp:Label>
                                        </p>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <p style="padding: 5px">
                                Daftar Username :
                            </p>
                            <ul id="daftaruser" runat="server" class="plike">
                            </ul>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
