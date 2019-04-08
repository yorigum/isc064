<%@ Page Language="c#" Inherits="ISC064.ADMINJUAL.ComplainEdit" CodeFile="ComplainEdit.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="HeadUnit" Src="HeadUnit.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavUnit" Src="NavUnit.ascx" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Edit Lokasi Unit</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="5">
    <meta name="sec" content="Lokasi Unit - Edit Lokasi Unit">
</head>
<body onkeyup="if(event.keyCode==27) window.close()">
    <form id="Form1" method="post" runat="server">
        <%--<uc1:navunit id="NavUnit1" runat="server" aktif="1"></uc1:navunit>--%>
        <div class="tabdata">
            <div class="pad">
                <%--<uc1:headunit id="HeadUnit1" runat="server"></uc1:headunit>--%>
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
                <table>
                    <tr>
                        <td>Judul Complain
                        </td>
                        <td>:
                        </td>
                        <td>
                            <asp:TextBox ID="judul" runat="server" Width="200" CssClass="txt" MaxLength="100"></asp:TextBox>
                            <asp:Label ID="judulc" runat="server" CssClass="err"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>PIC
                        </td>
                        <td>:
                        </td>
                        <td>
                            <asp:TextBox ID="pic" runat="server" CssClass="txt" Width="200" MaxLength="100"></asp:TextBox>
                            <asp:Label ID="picc" runat="server" CssClass="err"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table height="50">
                    <tr>
                        <td>
                            <asp:LinkButton ID="ok" runat="server" CssClass="btn btn-blue" Width="75" OnClick="ok_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
                        </td>
                        <td>
                            <input id="cancel" type="button" onclick="window.close()" class="btn btn-red" value="Cancel"
                                style="width: 75px">
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
            </div>
        </div>
    </form>
</body>
</html>
