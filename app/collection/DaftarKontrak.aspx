<%@ Page language="c#" Inherits="ISC064.COLLECTION.DaftarKontrak" CodeFile="DaftarKontrak.aspx.cs" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Daftar Kontrak</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="(Pop-Up) Daftar Kontrak">
    <meta http-equiv="pragma" content="no-cache">
    <base target="_self">
</head>
<body class="body-padding pop" onkeyup="if(event.keyCode==27){window.close()}"
    onload="document.getElementById('keyword').select()">
    <form id="Form1" method="post" runat="server">
        <div class="pad">
            <table>
                <tr>
                    <td>
                        <asp:TextBox ID="keyword" runat="server" CssClass="input-text" Width="400"></asp:TextBox>
                    </td>
                    <td>
                        <asp:DropDownList ID="metode" runat="server" CssClass="ddl form-control">
                            <asp:ListItem>Semua</asp:ListItem>
                            <asp:ListItem>Kontrak Aktif</asp:ListItem>
                            <asp:ListItem>Kontrak Batal</asp:ListItem>
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
        <p>
            <asp:Label ID="info" runat="server"><br>
            </asp:Label>
        </p>
        <asp:GridView ID="tb" runat="server" SkinID="pager" OnPageIndexChanging="tb_PageIndexChanging">
            <Columns>
                <asp:BoundField HeaderText="No. Kontrak" DataField="NoKontrak" />
                <asp:BoundField HeaderText="Unit" DataField="Unit" />
                <asp:BoundField HeaderText="Tgl." DataField="Tanggal" />
                <asp:BoundField HeaderText="Customer / Sales" DataField="Customer" />
                <asp:BoundField HeaderText="Keterangan" DataField="Keterangan" />
                <asp:BoundField HeaderText="Project" DataField="Project" />
            </Columns>
        </asp:GridView>
        <input type="text" style="display: none">
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
