<%@ Page Language="c#" Inherits="ISC064.SECURITY.BackupDatabase2" CodeFile="BackupDatabase2.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Backup Database</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Backup Database">
</head>
<body>
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1>Backup Database</h1>
        <br>
        <table style="border: 1px solid #DCDCDC" cellspacing="5">
            <tr>
                <td>Backup Database :</td>
                <td>Tanggal Backup 
                </td>
                <td>&nbsp;</td>
                <td>
                    <asp:Label runat="server" ID="tglbackup"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Table ID="rpt" runat="server" CssClass="tb" CellSpacing="3">
                        <asp:TableRow HorizontalAlign="Left">
                            <asp:TableHeaderCell Width="175">Nama</asp:TableHeaderCell>
                            <asp:TableHeaderCell Width="50">Link</asp:TableHeaderCell>
                        </asp:TableRow>
                        <asp:TableRow HorizontalAlign="Left">
                            <asp:TableCell Width="50">Security</asp:TableCell>
                            <asp:TableCell Width="175 " ID="linksec" runat="server">
                                <label ></label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow HorizontalAlign="Left">
                            <asp:TableCell Width="50">Marketing</asp:TableCell>
                            <asp:TableCell Width="175" ID="linkmkt" runat="server"><label></label></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow HorizontalAlign="Left">
                            <asp:TableCell Width="50">Finance</asp:TableCell>
                            <asp:TableCell Width="175" ID="linkfin" runat="server"><label ></label></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>

                </td>
            </tr>

        </table>
    </form>
</body>
</html>
