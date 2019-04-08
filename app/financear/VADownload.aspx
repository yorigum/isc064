<%@ Page Language="c#" Inherits="ISC064.FINANCEAR.VADownload" CodeFile="VADownload.aspx.cs" %>

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
        <asp:Button ID="dlcust" runat="server" Text="Data Customer" Visible="false" />
        <asp:Button ID="dltag" runat="server" Text="Data Tagihan" Visible="false" />
        <br />
        <div id="cust" runat="server" visible="false">
            <table class="tb blue-skin">
                <tr>
                    <th colspan="6" style="padding:1px;">DATA CUSTOMER</th>
                </tr>
                <tr>
                    <th style="padding:1px;">No.</th>
                    <th>Nama</th>
                    <th>Status</th>
                    <th>NoUnit</th>
                    <th>No. VA</th>
                    <th>Perintah</th>
                </tr>
                <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
                <tr>
                    <td colspan="5">
                        <asp:Button ID="save" runat="server" Text="Save" Width="75" OnClick="save_Click" CssClass="btn-submit button-submit2" AccessKey="s" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="tag" runat="server">
            <table class="tb blue-skin">
                <tr>
                    <th colspan="7">DATA TAGIHAN</th>
                </tr>
                <tr>
                    <th>No.</th>
                    <th>No. Kontrak</th>
                    <th>No. Unit</th>
                    <th>Status</th>
                    <th>Customer</th>
                    <th>No. VA</th>
                    <th width="200px;">Sisa Tagihan</th>
                </tr>
                <asp:PlaceHolder ID="list2" runat="server"></asp:PlaceHolder>
                <tr>
                    <td colspan="7">
                        <asp:LinkButton ID="save2" CssClass="btn btn-blue t-white" runat="server" Width="75" OnClick="save2_Click"
                            AccessKey="s">
                            <i class="fa fa-share"></i> OK    
                        </asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
