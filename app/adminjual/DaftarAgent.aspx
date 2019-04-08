<%@ Page Language="c#" Inherits="ISC064.ADMINJUAL.DaftarAgent" CodeFile="DaftarAgent.aspx.cs" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Daftar Sales</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="(Pop-Up) Daftar Sales">
    <meta http-equiv="pragma" content="no-cache">
    <base target="_self">
</head>
<body class="body-adding pop" onkeyup="if(event.keyCode==27){window.close()}"
    onload="document.getElementById('keyword').select()">
    <form id="Form1" method="post" runat="server">
        <div class="pad">
            <table>
                <tr>
                    <td>
                        <asp:TextBox ID="keyword" runat="server" CssClass="txt" Width="300"></asp:TextBox>
                    </td>
                    <td>
                        <asp:DropDownList ID="metode" runat="server" CssClass="ddl form-control">
                            <asp:ListItem>Semua</asp:ListItem>
                            <asp:ListItem>Sales Aktif</asp:ListItem>
                            <asp:ListItem>Sales Inaktif</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="project" runat="server" CssClass="ddl form-control">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="search" runat="server" CssClass="btn btn-blue" Text="Search" AccessKey="s" OnClick="search_Click"></asp:Button>
                    </td>
                </tr>
            </table>
        </div>
        <br>
        <div class="pad">
        <asp:GridView ID="tb" runat="server" SkinID="pager" OnPageIndexChanging="tb_PageIndexChanging">
            <Columns>
                <asp:BoundField HeaderText="Nama" DataField="Nama" HeaderStyle-Width="100px" />
                <asp:BoundField HeaderText="Principal" DataField="Principal" HeaderStyle-Width="200px" />
                <asp:BoundField HeaderText="Project" DataField="Project" />
            </Columns>
        </asp:GridView>
        </div>
        <input type="text" style="display: none;">
        <script type="text/javascript">
            function call(nomor) {
                window.parent.call(nomor);
                window.parent.globalclose();
            }
        </script>
    </form>
</body>
</html>
