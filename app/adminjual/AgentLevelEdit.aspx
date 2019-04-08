<%@ Page Language="c#" Inherits="ISC064.ADMINJUAL.AgentLevelEdit" CodeFile="AgentLevelEdit.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="HeadUnit" Src="HeadUnit.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavUnit" Src="NavUnit.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Edit Jenis Unit</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="5">
    <meta name="sec" content="Jenis Unit - Edit Jenis Unit">
</head>
<body onkeyup="if(event.keyCode==27) window.close()">
    <form id="Form1" method="post" runat="server">
    <%--<uc1:navunit id="NavUnit1" runat="server" aktif="1"></uc1:navunit>--%>
    <div class="tabdata">
        <div class="pad">
            <%--<uc1:headunit id="HeadUnit1" runat="server"></uc1:headunit>--%>
            <table cellspacing="5">
                <tr>
                    <td width="100%">
                    </td>
                    <td>
                        <input type="button" class="btn" value="  Log  " id="btnlog" runat="server" name="btnlog"
                            accesskey="l" style="display: none">
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
            <table>
                <tr style="display: none">
                    <td class="stamp">
                        Input :
                        <asp:Label ID="tglInput" runat="server"></asp:Label>
                    </td>
                    <td class="stamp">
                        Edit :
                        <asp:Label ID="tglEdit" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <table cellpadding="0" cellspacing="0">
                <tr valign="top">
                    <td width="400">
                        <table cellspacing="5">
                            <tr>
                                <td colspan="3">
                                    <p>
                                        <b>Dokumen</b></p>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    No. Jenis
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="LevelID" runat="server" CssClass="txt" Width="150" ReadOnly="True"></asp:TextBox>
                                    <asp:Label ID="nojenisc" runat="server" CssClass="err"></asp:Label>
                                </td>
                            </tr>
                           
                            <tr>
                                <td>
                                    Nama Jenis Unit
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="nama" runat="server" Width="200" CssClass="txt" MaxLength="100"></asp:TextBox>
                                    <asp:Label ID="namac" runat="server" CssClass="err"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table height="50">
                            <tr>
                                <td>
                                    <asp:LinkButton ID="ok" runat="server" CssClass="btn btn-blue"  Width="75" OnClick="ok_Click">
                                        <i class="fa fa-share"></i> OK
                                    </asp:LinkButton>
                                </td>
                                <td>
                                    <input id="cancel" type="button" onclick="window.close()" class="btn btn-red" value="Cancel"
                                        style="width: 75px">
                                </td>
                                <td>
                                    <asp:Button ID="save" runat="server" CssClass="btn btn-blue" Text="Apply" Width="75" AccessKey="a"
                                        OnClick="save_Click"></asp:Button>
                                </td>
                                <td style="padding-left: 10px">
                                    <p class="feed">
                                        <asp:Label ID="feed" runat="server"></asp:Label>
                                    </p>
                                </td>
                            </tr>
                        </table>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
