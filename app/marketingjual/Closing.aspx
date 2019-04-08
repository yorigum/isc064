<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.Closing" CodeFile="Closing.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Closing Kontrak</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Closing Kontrak">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Closing Kontrak</h1>
        <table style="border: 1px solid #DCDCDC" cellspacing="5">
            <tr>
                <td></td>
                <td>
                    <asp:DropDownList ID="status" runat="server" Width="200">
                        <asp:ListItem Value="">Status Closing :</asp:ListItem>
                        <asp:ListItem Value="A">Aktif</asp:ListItem>
                        <asp:ListItem Value="E">Expired</asp:ListItem>
                        <asp:ListItem Value="C">Closing</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="display" runat="server" CssClass="btn btn-blue" Text="Display" OnClick="display_Click"></asp:Button>
                </td>
            </tr>
        </table>
        <br>
        <div class="peach">
            Status : A = Aktif / E = Expired / C = Closing
        </div>
        <asp:Table ID="rpt" runat="server" CellSpacing="0" CssClass="tb blue-skin">
            <asp:TableRow HorizontalAlign="Left">
                <asp:TableHeaderCell>No. Closing</asp:TableHeaderCell>
                <asp:TableHeaderCell>Status. </asp:TableHeaderCell>
                <asp:TableHeaderCell>Tgl. </asp:TableHeaderCell>
                <asp:TableHeaderCell>Customer </asp:TableHeaderCell>
                <asp:TableHeaderCell>Sales </asp:TableHeaderCell>
                <asp:TableHeaderCell>Unit </asp:TableHeaderCell>
                <asp:TableHeaderCell>Cara Bayar </asp:TableHeaderCell>
                <asp:TableHeaderCell>Batas Waktu </asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>
