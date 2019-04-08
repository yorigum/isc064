<%@ Page Language="c#" Inherits="ISC064.FINANCEAR.VAEksporSinarmas" CodeFile="VAEksporSinarmas.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Virtual Account</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Virtual Account - Export Data">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <input type="text" style="display: none" />
        <h1 class="title title-line">Export Data Virtual Account</h1>
        <br />
        <div id="cust" runat="server">
            <br />
            <table>
                <tr>
                    <th colspan="7" style="padding: 1px;">DATA Virtual Account Bank Sinarmas</th>
                </tr>
                <tr>
                    <td>Periode :</td>
                    <td><asp:Label ID="dari" runat="server"></asp:Label></td>
                    <td>&nbsp; s/d &nbsp;</td>
                    <td><asp:Label ID="sampai" runat="server"></asp:Label></td>
                </tr>
            </table>
            <table class="tb blue-skin">
                <tr>
                    <th style="padding: 1px;">No.</th>
                    <th>Nama</th>
                    <th>No. VA</th>
                    <th>Nama PT</th>
                    <th>Alamat PT</th>
                    <th>Nama Tagihan</th>
                    <th>Nilai Tagihan</th>
                </tr>
                <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
                <tr>
                    <td colspan="7">
                        <asp:Button ID="save" runat="server" Text="Save" Width="75" OnClick="save_Click" CssClass="btn-submit button-submit2" AccessKey="s" />
                        <input class="btn btn-red" id="cancel" style="width: 75px" onclick="location.href='VAEkspor.aspx'"
							type="button" value="Cancel" name="cancel" runat="server">
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
