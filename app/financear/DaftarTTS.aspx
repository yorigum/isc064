<%@ Page language="c#" Inherits="ISC064.FINANCEAR.DaftarTTS" CodeFile="DaftarTTS.aspx.cs" %>
<!DOCTYPE html>
<html>
<head>
    <title>Daftar TTS</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="(Pop-Up) Daftar TTS">
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
                        <asp:TextBox ID="keyword" runat="server" Width="400"></asp:TextBox>
                    </td>
                    <td>
                        <asp:DropDownList ID="metode" runat="server" class="ddl form-control">
                            <asp:ListItem>Semua</asp:ListItem>
                            <asp:ListItem>TTS Baru</asp:ListItem>
                            <asp:ListItem>TTS Post</asp:ListItem>
                            <asp:ListItem>TTS Void</asp:ListItem>
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
        <asp:GridView ID="tb" runat="server" SkinID="pager" OnPageIndexChanging="tb_PageIndexChanging">
            <Columns>
                <asp:BoundField HeaderText="No. TTS" DataField="TTS" />
                <asp:BoundField HeaderText="Tgl. " DataField="Tgl" />
                <asp:BoundField HeaderText="Customer" DataField="Customer" />
                <asp:BoundField HeaderText="Keterangan" DataField="Keterangan" />
                <asp:BoundField HeaderText="Cara Bayar" DataField="CaraBayar" />
                <asp:BoundField HeaderText="Total" DataField="Total" />
                <asp:BoundField HeaderText="Project" DataField="Project" />
            </Columns>
        </asp:GridView>
        <input type="text" style="display: none;">
        <script type="text/javascript">
            function call(nomor, ctrl1) {
                window.parent.call(nomor, ctrl1);
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
