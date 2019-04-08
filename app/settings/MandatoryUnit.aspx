<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MandatoryUnit.aspx.cs" Inherits="ISC064.SETTINGS.MandatoryUnit" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>

<html>
<head>
    <title>Mandatori</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Mandatori Pendaftaran Unit">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <h1 class="title title-line">Setup Mandatory Unit</h1>
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <div>
            <p class="feed">
                <asp:Label ID="feed" runat="server"></asp:Label>
            </p>
            <asp:DropDownList ID="project" runat="server" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged"></asp:DropDownList>
            <table class="tb blue-skin">
                <tr>
                    <th>No
                    </th>
                    <th>Data
                    </th>
                    <th>Harus Isi
                    </th>
                </tr>
                <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
                <tr>
                    <td colspan="3">
                            <asp:LinkButton id="ok" runat="server" cssclass="btn btn-blue" width="75" onclick="ok_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
