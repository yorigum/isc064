<%@ Page Language="c#" Inherits="ISC064.ADMINJUAL.DaftarUnit" CodeFile="DaftarUnit.aspx.cs" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Daftar Unit</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="(Pop-Up) Daftar Unit">
    <meta http-equiv="pragma" content="no-cache">
    <base target="_self">
</head>
<body class="body-padding pop" onkeyup="if(event.keyCode==27){window.close()}"
    onload="document.getElementById('keyword').select()">
    <form id="Form1" method="post" runat="server">
        <div>
            <table>
                <tr>
                    <td>
                        <asp:TextBox ID="keyword" runat="server" CssClass="txt" Width="400"></asp:TextBox>
                    </td>
                    <td>
                        <asp:DropDownList ID="metode" runat="server" class="ddl form-control">
                            <asp:ListItem>Semua</asp:ListItem>
                            <asp:ListItem>Unit Aktif</asp:ListItem>
                            <asp:ListItem>Unit Blokir</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="project" runat="server" class="ddl form-control">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="search" runat="server" CssClass="btn btn-blue" Text="Search" AccessKey="s" OnClick="search_Click"></asp:Button>
                    </td>
                </tr>
            </table>
        </div>
        <br>
        <p style="font: 8pt; padding: 3px">
            Harga price list adalah dalam rupiah
        </p>
        <asp:GridView ID="tb" runat="server" SkinID="pager" OnPageIndexChanging="tb_PageIndexChanging">
            <Columns>
                <asp:BoundField HeaderText="No. Stock" DataField="NoStock" />
                <asp:BoundField HeaderText="Unit" DataField="NoUnit" />
                <asp:BoundField HeaderText="Keterangan" DataField="Keterangan" />
                <asp:BoundField HeaderText="Luas Semi Gross" DataField="LuasSG" />
                <asp:BoundField HeaderText="Luas Nett" DataField="LuasNett" />
                <asp:BoundField HeaderText="Price List" DataField="PL" />
                <asp:BoundField HeaderText="Project" DataField="Project" />
            </Columns>
        </asp:GridView>
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
