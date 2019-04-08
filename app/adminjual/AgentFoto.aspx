<%@ Page Language="c#" Inherits="ISC064.ADMINJUAL.AgentFoto" CodeFile="AgentFoto.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="HeadAgent" Src="HeadAgent.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavAgent" Src="NavAgent.ascx" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Foto Sales</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Sales - Foto Sales">
</head>
<body onkeyup="if(event.keyCode==27) window.close()">
    <form id="Form1" method="post" runat="server">
        <div class="content-header">
            <uc1:NavAgent ID="NavAgent1" runat="server" Aktif="3"></uc1:NavAgent>
        </div>
        <div class="tabdata">
            <div class="pad">
                <uc1:HeadAgent ID="HeadAgent1" runat="server"></uc1:HeadAgent>
                <table cellspacing="5">
                    <tr>
                        <td>File Foto</td>
                        <td>:</td>
                        <td>
                            <input type="file" id="file" runat="server" class="btn" style="width: 600px; text-align:left" name="file">
                        </td>
                    </tr>
                </table>
                <table height="50">
                    <tr>
                        <td>
                            <asp:LinkButton ID="upload" runat="server" CssClass="btn btn-blue" Width="75" OnClick="upload_Click">
                                <i class="fa fa-share"></i> OK
                            </asp:LinkButton>
                        </td>
                        <td style="padding-left: 10px">
                            <p class="feed">
                                <asp:Label ID="feed" runat="server"></asp:Label>
                            </p>
                        </td>
                    </tr>
                </table>
                <br>
                <table cellspacing="5">
                    <tr>
                        <td>
                            <asp:Image ID="foto" runat="server"></asp:Image>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
