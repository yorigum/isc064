<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FormatUnit.aspx.cs" Inherits="ISC064.SETTINGS.FormatUnit" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>

<html>
<head>
    <title>Format Nomor Unit</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Format Unit">
    <style>
        #btn:hover
        {
            color:black;
        }
    </style>
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <div>
            <h1 class="title title-line">Format No.Unit</h1>
            <br>
            <asp:DropDownList ID="project" runat="server" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged"></asp:DropDownList>
            <br />
            <table>
                <tr>
                    <td>Lokasi
                    </td>
                    <td>
                        <asp:TextBox ID="batas" runat="server" CssClass="txt_num" style="text-align:center" Width="40px"></asp:TextBox>
                    </td>
                    <td>Blok</td>
                    <td>
                        <asp:TextBox ID="batas2" runat="server" CssClass="txt_num" style="text-align:center" Width="40px"></asp:TextBox>
                    </td>
                    <td>
                        No.Unit
                    </td>
                </tr>
                <tr style="height:10px">
                    <td/>
                </tr>
                <tr>
                    <td colspan="3">
                            <asp:LinkButton id="ok" runat="server" cssclass="btn btn-blue" width="75" onclick="save_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
                    </td>
                </tr>
            </table>
            <p class="feed">
                <asp:Label ID="feed" runat="server"></asp:Label>
            </p>
        </div>
    </form>
</body>
</html>
