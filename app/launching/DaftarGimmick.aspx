<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DaftarGimmick.aspx.cs" Inherits="ISC064.LAUNCHING.DaftarGimmick" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Daftar Gimmick</title>
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="(Pop-Up) Daftar Gimmick">
    <meta http-equiv="pragma" content="no-cache">
    <base target="_self">
</head>
<body class="body-padding pop" onkeyup="if(event.keyCode==27){window.close()}" onload="document.getElementById('keyword').select()">
    <form id="Form1" method="post" runat="server">
        <div class="pad">
            <table>
                <tr>
                    <td>
                        <asp:TextBox ID="keyword" runat="server" CssClass="txt" Width="400"></asp:TextBox>
                    </td>
                    <td>
                        <asp:DropDownList ID="project" runat="server" CssClass="ddl form-control" Style='width: 100%'>
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
                <asp:BoundField HeaderText="Type Gimmick" DataField="NoGimmick" />
                <%--<asp:BoundField HeaderText="Unit" DataField="Unit" />
                <asp:BoundField HeaderText="Tgl." DataField="Tanggal" />
                <asp:BoundField HeaderText="Customer / Sales" DataField="Customer" />
                <asp:BoundField HeaderText="Keterangan" DataField="Keterangan" />
                <asp:BoundField HeaderText="Project" DataField="Project" />--%>
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
