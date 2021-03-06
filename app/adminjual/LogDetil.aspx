<%@ Page Language="c#" Inherits="ISC064.ADMINJUAL.LogDetil" CodeFile="LogDetil.aspx.cs" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Log File</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="4">
    <meta name="sec" content="Log File Detil">
    <meta http-equiv="pragma" content="no-cache">
    <base target="_self">
</head>
<body class="body-padding pop" onkeyup="if(event.keyCode==27)window.close()"
    onload="document.getElementById('ket').focus();">
    <form id="Form1" method="post" runat="server">
        <asp:DropDownList ID="akt1" runat="server" Visible="False">
            <asp:ListItem Value="DAFTAR">DAFTAR = Pendaftaran Unit</asp:ListItem>
            <asp:ListItem Value="SPL">SPL = Set Price List</asp:ListItem>
            <asp:ListItem Value="EDIT">EDIT = Edit Unit</asp:ListItem>
            <asp:ListItem Value="STATUS">STATUS = Edit Status Unit</asp:ListItem>
            <asp:ListItem Value="DELETE">DELETE = Delete Unit (Permanen)</asp:ListItem>
            <asp:ListItem Value="FP">FP = Edit Floor Plan</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="akt2" runat="server" Visible="False">
            <asp:ListItem Value="DAFTAR">DAFTAR = Pendaftaran Sales</asp:ListItem>
            <asp:ListItem Value="EDIT">EDIT = Edit Sales</asp:ListItem>
            <asp:ListItem Value="SSK">SSK = Set Skema Komisi</asp:ListItem>
            <asp:ListItem Value="FOTO">FOTO = Edit Foto</asp:ListItem>
            <asp:ListItem Value="DELETE">DELETE = Delete Sales (Permanen)</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="akt3" runat="server" Visible="False">
            <asp:ListItem Value="DAFTAR">DAFTAR = Daftar Skema Cara Bayar</asp:ListItem>
            <asp:ListItem Value="EDIT">EDIT = Edit Skema Cara Bayar</asp:ListItem>
            <asp:ListItem Value="DELETE">DELETE = Delete Skema Cara Bayar (Permanen)</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="akt4" runat="server" Visible="False">
            <asp:ListItem Value="DAFTAR">DAFTAR = Daftar Tipe Sales</asp:ListItem>
            <asp:ListItem Value="EDIT">EDIT = Edit Tipe Sales</asp:ListItem>
            <asp:ListItem Value="DELETE">DELETE = Delete Tipe Sales</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="akt5" runat="server" Visible="False">
            <asp:ListItem Value="DAFTAR">DAFTAR = Daftar Level Sales</asp:ListItem>
            <asp:ListItem Value="EDIT">EDIT = Edit Level Sales</asp:ListItem>
            <asp:ListItem Value="DELETE">DELETE = Delete Level Sales</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="akt6" runat="server" Visible="False">
            <asp:ListItem Value="DAFTAR">DAFTAR = Daftar Gimmick</asp:ListItem>
            <asp:ListItem Value="EDIT">EDIT = Edit Gimmick</asp:ListItem>
            <asp:ListItem Value="DELETE">DELETE = Delete Gimmick</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="akt7" runat="server" Visible="False">
            <asp:ListItem Value="DAFTAR">DAFTAR = Daftar Tipe Gimmick</asp:ListItem>
            <asp:ListItem Value="EDIT">EDIT = Edit Tipe Gimmick</asp:ListItem>
            <asp:ListItem Value="DELETE">DELETE = Delete Tipe Gimmick</asp:ListItem>
        </asp:DropDownList>
        <script type="text/javascript">
            preva = MM_preloadImages('/Media/icon_prev_a.gif');
            prevo = MM_preloadImages('/Media/icon_prev_o.gif');
            prevc = MM_preloadImages('/Media/icon_prev_c.gif');
            nexta = MM_preloadImages('/Media/icon_next_a.gif');
            nexto = MM_preloadImages('/Media/icon_next_o.gif');
            nextc = MM_preloadImages('/Media/icon_next_c.gif');
            function MM_preloadImages() {
                x = new Image;
                x.src = MM_preloadImages.arguments[0];
                return x
            }
            function sc(foo, imgnew) {
                if (document.images) { foo.src = eval(imgnew + ".src"); }
            }
        </script>
        <table cellspacing="5">
            <tr>
                <td><a id="prev" runat="server">
                    <img src="/Media/icon_prev_a.gif" onmouseover="sc(this,'prevo')" onmousedown="sc(this,'prevc')"
                        onmouseout="sc(this,'preva')" border="0"></a></td>
                <td><a id="next" runat="server">
                    <img src="/Media/icon_next_a.gif" onmouseover="sc(this,'nexto')" onmousedown="sc(this,'nextc')"
                        onmouseout="sc(this,'nexta')" border="0"></a></td>
                <td>
                    <asp:Button ID="ok" runat="server" CssClass="btn btn-blue" Text="Approve" AccessKey="a" OnClick="ok_Click"></asp:Button></td>
                <td>Approval :
						<asp:Label ID="approveinfo" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table cellspacing="5">
            <tr>
                <td>Sumber</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="sumber" runat="server" CssClass="txt" ReadOnly="True" Width="190"></asp:TextBox>
                </td>
                <td width="10" rowspan="3"></td>
                <td>User</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="user" runat="server" CssClass="txt" ReadOnly="True" Width="65"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Nomor</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="logid" runat="server" CssClass="txt" ReadOnly="True" Width="75"></asp:TextBox>
                </td>
                <td>IP Addr.</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="ip" runat="server" CssClass="txt" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Referensi
                </td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="pk" runat="server" CssClass="txt" ReadOnly="True" Width="150"></asp:TextBox>
                </td>
                <td>Tanggal</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="tgl" runat="server" CssClass="txt" ReadOnly="True" Width="150"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Aktivitas
                </td>
                <td>:</td>
                <td colspan="5">
                    <asp:DropDownList ID="aktivitas" runat="server" CssClass="ddl" Width="480"></asp:DropDownList>
                </td>
            </tr>
        </table>
        <table cellspacing="5">
            <tr>
                <td><label style="vertical-align:top"> Deskripsi :</label>
						<asp:TextBox ReadOnly="True" ID="ket" runat="server" TextMode="MultiLine" Width="550" Height="300"
                            CssClass="txt"></asp:TextBox>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
