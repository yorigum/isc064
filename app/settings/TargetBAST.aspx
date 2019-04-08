<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TargetBAST.aspx.cs" Inherits="ISC064.SETTINGS.TargetBAST" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>

<html>
<head>
    <title>Target BAST</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Target BAST">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <div>
            <h1 class="title title-line">Target BAST</h1>
            <br>
            <table>
                <tr>
                    <td>
                        <asp:DropDownList runat="server" ID="project" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged"></asp:DropDownList>                        
                    </td>
                </tr>
                <tr>    
                <td>Tgl. Target</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="tgltarget" runat="server" Width="85" CssClass="tgl"></asp:TextBox>
                    <label for="tgltarget" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                    <asp:label id="tgltargetc" runat="server" cssclass="err"></asp:label>
                </td>
                </tr>
                <tr>
                    <td>Lokasi</td>
                    <td>:</td>
                    <td><asp:DropDownList runat="server" ID="lokasi" AutoPostBack="true" OnSelectedIndexChanged="lokasi_SelectedIndexChanged"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;</td>
                    <td><asp:CheckBox runat="server" ID="update" />Update All Kontrak</td>
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

