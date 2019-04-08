<%@ Reference Page="~/Unit.aspx" %>

<%@ Page Language="c#" Inherits="ISC064.ADMINJUAL.Laporan.SummaryStock" CodeFile="SummaryStock.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Laporan Summary Stock</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Summary Stock">
</head>
<body onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server">
        <div style="display: none">
            <uc1:Head ID="Head1" runat="server"></uc1:Head>
        </div>
        <table id="param" runat="server" width="100%" cellspacing="3">
            <tr>
                <td>
                    <p class="comp" id="comp" runat="server"></p>
                    <h1 id="judul" runat="server">Laporan Summary Stock
                    </h1>
                    <br />
                    <div class="ins">
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="scr" AccessKey="s" runat="server" Text="Screen Preview" Width="100" CssClass="btn btn-blue" OnClick="scr_Click"></asp:Button>
                                    <asp:Button ID="xls" AccessKey="e" runat="server" Text="Download Excel" Width="100" CssClass="btn btn-green" OnClick="xls_Click"></asp:Button>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <asp:Table ID="rpt" runat="server" CssClass="blue-skin tb" CellSpacing="0">
            <asp:TableRow>
                <asp:TableCell ColumnSpan="4">
                    <asp:Label ID="lblHeader" runat="server" Font-Size="12pt" Font-Bold="True"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>
