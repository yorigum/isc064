<%@ Reference Page="~/Acc.aspx" %>
<%@ Register TagPrefix="uc1" TagName="NavVA" Src="NavVA.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeadVA" Src="HeadVA.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.FINANCEAR.VAEdit" CodeFile="VAEdit.aspx.cs" %>

<!DOCTYPE html>
<html>
<head>
    <title>Edit Virtual Account</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="5">
    <meta name="sec" content="Virtual Account - Edit VA">
</head>
<body onkeyup="if(event.keyCode==27) window.close()">
    <script language="javascript" src="/Js/Common.js"></script>
    <form id="Form1" method="post" runat="server">
        <div class="content-header">
            <uc1:NavVA ID="NavVA1" runat="server" Aktif="1"></uc1:NavVA>
        </div>
        <div class="tabdata">
            <div class="pad">
                <uc1:HeadVA ID="HeadVA1" runat="server"></uc1:HeadVA>
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
                        <td>
                            <b>Bank</b>
                        </td>
                        <td>:
                        </td>
                        <td>
                            <asp:TextBox ID="bank" runat="server" MaxLength="50" />
                            <asp:Label ID="bankc" runat="server" CssClass="err" />
                        </td>
                    </tr>
                </table>
                <table style="height:50px">
                    <tr>
                        <td>
                            <asp:LinkButton ID="ok" runat="server" CssClass="btn btn-blue" Width="75" OnClick="ok_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
                        </td>
                        <td>
                            <asp:LinkButton ID="save" runat="server" CssClass="btn btn-orange" Width="75" AccessKey="a" OnClick="save_Click"><i class="fa fa-check"></i> Apply </asp:LinkButton>
                        </td>
                        <td>
                            <input id="cancel" type="button" onclick="window.close()" class="btn btn-red" value="Cancel"
                                style="width: 75px">
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
