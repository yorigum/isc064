<%@ Page Language="c#" Inherits="ISC064.SECURITY.BackupDatabase" CodeFile="BackupDatabase.aspx.cs" %>

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
                    <asp:TextBox ID="tglbackup" runat="server" CssClass="txt_center" Width="85"></asp:TextBox>
                    <label for="tglbackup" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                    <asp:Label ID="tglbackupc" runat="server" CssClass="err"></asp:Label>
                </td>
                <td>
                    <asp:Button ID="backup" runat="server" CssClass="btn" Text="Backup" OnClick="backup_Click"></asp:Button>
                </td>
            </tr>
        </table>
        <p class="feed">
            <asp:Label ID="feed" runat="server"></asp:Label>
        </p>
    </form>
</body>
</html>
