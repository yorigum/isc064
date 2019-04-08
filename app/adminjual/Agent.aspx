<%@ Page Language="c#" Inherits="ISC064.ADMINJUAL.Agent" CodeFile="Agent.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Master Sales</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Sales">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Master Sales</h1>
        <br>
        <table cssclass="blue-skin" style="border: 1px solid #DCDCDC" cellspacing="5">
            <tr>
                <td>
                    <input type="button" class="btn btn-blue" value="Search" show-modal="#ModalPopUp" modal-title="Daftar Agent" modal-url="DaftarAgent.aspx" id="search"
                        runat="server" name="search" accesskey="s">
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="project" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged"></asp:DropDownList>
                </td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <td><a href="" runat="server" id="abcd" style="font: 10pt"><b>a b c d</b></a></td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <td><a href="" runat="server" id="efgh" style="font: 10pt"><b>e f g h</b></a></td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <td><a href="" runat="server" id="ijkl" style="font: 10pt"><b>i j k l</b></a></td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <td><a href="" runat="server" id="mnop" style="font: 10pt"><b>m n o p</b></a></td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <td><a href="" runat="server" id="qrst" style="font: 10pt"><b>q r s t</b></a></td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <td><a href="" runat="server" id="uvwx" style="font: 10pt"><b>u v w x</b></a></td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <td><a href="" runat="server" id="yz09" style="font: 10pt"><b>y z 0 9</b></a></td>
            </tr>
        </table>
        <br>
        <div style="padding-left: 5">
            <asp:Label ID="list" runat="server"></asp:Label>
        </div>
        <script type="text/javascript">
            function call(nomor) {
                popEditAgent(nomor);
            }
        </script>
    </form>
</body>
</html>
