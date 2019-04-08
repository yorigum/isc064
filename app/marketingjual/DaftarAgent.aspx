<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.DaftarAgent" CodeFile="DaftarAgent.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Daftar Sales</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="(Pop-Up) Daftar Sales">
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
                        <asp:TextBox ID="keyword" runat="server" CssClass="txt" Width="300"></asp:TextBox>
                    </td>
                    <td>
                        <asp:DropDownList ID="metode" runat="server" CssClass="ddl form-control" Style='width: 100%'>
                            <asp:ListItem>Semua</asp:ListItem>
                            <asp:ListItem>Sales Aktif</asp:ListItem>
                            <asp:ListItem>Sales Inaktif</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="search" runat="server" CssClass="btn" Text="Search" AccessKey="s" OnClick="search_Click"></asp:Button>
                    </td>
                </tr>
            </table>
        </div>
        <br>
        <asp:GridView ID="tb" runat="server" PageSize="5" OnPageIndexChanging="tb_PageIndexChanging" SkinID="pager">
            <Columns>
                <asp:BoundField DataField="Nama" HeaderText="Nama" ItemStyle-Width="350" />
                <asp:BoundField DataField="Principal" HeaderText="Principal" ItemStyle-Width="350" />
            </Columns>
        </asp:GridView>
        <input type="text" style="display: none;">
        <script type="text/javascript">

            function call(nomor) {
                window.parent.call(nomor);
                window.parent.globalclose();
            }

            function callSource(nomor, source) {
                window.parent.callSource(nomor, source);
                window.parent.globalclose();
            }
        </script>
    </form>
</body>
</html>
