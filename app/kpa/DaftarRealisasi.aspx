<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DaftarRealisasi.aspx.cs" Inherits="ISC064.KPA.DaftarRealisasi" %>

<html>
<head>
    <title>Daftar Realisasi</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="(Pop-Up) Daftar Realisasi">
    <meta http-equiv="pragma" content="no-cache">
    <base target="_self">
</head>
<body class="body-padding pop" onkeyup="if(event.keyCode==27){window.close()}"
    onload="document.getElementById('keyword').select()">
    <form id="Form1" method="post" runat="server">
        <div class="pad">
            <table>
                <tr>
                    <td colspan="2">
                        <asp:TextBox ID="keyword" runat="server" CssClass="txt" Width="400"></asp:TextBox>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="project"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="search" runat="server" CssClass="btn btn-blue" Text="Search" AccessKey="s" OnClick="search_Click"></asp:Button>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <p>
            <asp:Label ID="info" runat="server"><br />
            </asp:Label>
        </p>
        <asp:GridView ID="tb" runat="server" SkinID="pager" OnPageIndexChanging="tb_PageIndexChanging">
            <Columns>
                <asp:BoundField HeaderText="No. Realisasi" DataField="Realisasi" Visible="false" />
                <asp:BoundField HeaderText="No. Realisasi" DataField="NoRealisasi" />
                <asp:BoundField HeaderText="Tgl." DataField="Tgl" />
                <asp:BoundField HeaderText="Keterangan" DataField="Keterangan" />
                <asp:BoundField HeaderText="Total" DataField="Total" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField HeaderText="Project" DataField="Project" />
            </Columns>
        </asp:GridView>
        <input type="text" style="display: none" />
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
