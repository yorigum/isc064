<%@ Reference Page="~/SecLevel.aspx" %>

<%@ Page Language="c#" Inherits="ISC064.SECURITY.EditFoto" CodeFile="EditFoto.aspx.cs" %>

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
    <meta name="sec" content="Username - Edit Foto">
</head>
<body onkeyup="if(event.keyCode==27)window.close()">
    <form id="Form1" method="post" runat="server">
        <div class="content-header">
            <uc1:NavUser ID="NavUser1" runat="server" Aktif="4"></uc1:NavUser>
        </div>
        <div class="tabdata">
            <div>

            </div>
            <div class="pad">
                <uc1:HeadUser ID="HeadUser1" runat="server"></uc1:HeadUser>
                    <table>
                        <tr>
                            <td colspan="2">
                                <img href="" runat="server" id="foto" style="margin-left:10%;margin-top:5%;margin-bottom:5%; border-radius:100%; width:200px;height:200px" />
                            </td>
                        </tr>
                        <tr>
                            <td>Upload Gambar :</td>
<%--                            <td>:</td>--%>
                            <td><input type="file" id="file" class="txt" runat="server" style="WIDTH:568px" name="file" /></td>
                        </tr>
                    </table>
                <table style="height: 50px;">
                    <tr>
                        <td>
                            <asp:LinkButton ID="ok" runat="server" CssClass="btn btn-blue" Width="75" OnClick="ok_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
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
