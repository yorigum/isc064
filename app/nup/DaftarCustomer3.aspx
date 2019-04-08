<%@ Page Language="c#" Inherits="ISC064.NUP.DaftarCustomer3" CodeFile="DaftarCustomer3.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Daftar Customer</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="(Pop-Up) Daftar Customer">
    <meta http-equiv="pragma" content="no-cache">
    <base target="_self">
</head>
<body class="pop" onkeyup="if(event.keyCode==27){window.close()}"
    onload="document.getElementById('keyword').select()">
    <form id="Form1" method="post" runat="server">
        <div class="pad">
            <table>
                <tr>
                    <td>
                        <asp:TextBox ID="keyword" runat="server" CssClass="txt" Width="400"></asp:TextBox>
                    </td>
                    <td>
                        <asp:DropDownList ID="metode" runat="server" CssClass="ddl">
                            <asp:ListItem>Semua</asp:ListItem>
                            <asp:ListItem>Customer Aktif</asp:ListItem>
                            <asp:ListItem>Customer Inaktif</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="search" runat="server" CssClass="btn" Text="Search" AccessKey="s" OnClick="search_Click"></asp:Button>
                    </td>
                </tr>
            </table>
        </div>
        <br>
        <asp:Table ID="rpt" runat="server" CssClass="tb" CellSpacing="3">
            <asp:TableRow HorizontalAlign="Left">
                <asp:TableHeaderCell Width="350">Nama</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="350">Alamat</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
        <input type="text" style="display: none;">
        <script language="javascript">
            function call(no, nm, hp, telp, noktp, ktp1, ktp2, ktp3, ktp4, kor1, kor2, kor3, kor4, rekb, rekc, rekno, reknam) {
                window.close();
                
                window.opener.call(no, nm, hp, telp, noktp, ktp1, ktp2, ktp3, ktp4, kor1, kor2, kor3, kor4, rekb, rekc, rekno, reknam);
            }
        </script>
    </form>
</body>
</html>
