<%@ Page Language="c#" Inherits="ISC064.KPA.LogDetil" CodeFile="LogDetil.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Log File</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="4">
    <meta name="sec" content="Log File Detil">
    <meta http-equiv="pragma" content="no-cache">
    <base target="_self">
</head>
<body class="pop" onkeyup="if(event.keyCode==27)window.close()" onload="document.getElementById('ket').focus();">
    <form id="Form1" method="post" runat="server">
        <asp:DropDownList ID="akt1" runat="server" Visible="False">
            <asp:ListItem Value="KPACLB">KPACLB = KPR - Check List Berkas</asp:ListItem>
            <asp:ListItem Value="KPAWAN">KPAWAN = KPR - Edit Wawancara</asp:ListItem>
            <asp:ListItem Value="KPAOTS">KPAOTS = KPR - Edit OTS</asp:ListItem>
            <asp:ListItem Value="KPALPA">KPALPA = KPR - Edit LPA</asp:ListItem>
            <asp:ListItem Value="KPASP3">KPASP3 = KPR - Edit SP3K</asp:ListItem>
            <asp:ListItem Value="KPAAKD">KPAAKD = KPR - Edit Akad</asp:ListItem>
            <asp:ListItem Value="KPAAJB">KPAAJB = KPR - Edit AJB</asp:ListItem>
            <asp:ListItem Value="KPASFT">KPASFT = KPR - Edit Sertifikat</asp:ListItem>
            <asp:ListItem Value="KPAIMB">KPAIMB = KPR - Edit IMB</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="akt2" runat="server" Visible="False">
            <asp:ListItem Value="REGIS">REGIS = Registrasi Bank KPR</asp:ListItem>
            <asp:ListItem Value="EDIT">EDIT = Edit Bank KPR</asp:ListItem>
            <asp:ListItem Value="DELETE">DELETE = Delete Bank KPR</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="akt3" runat="server" Visible="False">
            <asp:ListItem Value="REGIS">REGIS = Registrasi Retensi KPR</asp:ListItem>
            <asp:ListItem Value="EDIT">EDIT = Edit Retensi KPR</asp:ListItem>
            <asp:ListItem Value="DELETE">DELETE = Delete Retensi KPR</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="akt4" runat="server" Visible="False">
            <asp:ListItem Value="REGIS">REGIS = Registrasi Pengajuan KPR</asp:ListItem>
            <asp:ListItem Value="EDIT">EDIT = Edit Pengajuan KPR</asp:ListItem>
            <asp:ListItem Value="DELETE">DELETE = Delete Pengajuan KPR</asp:ListItem>
            <asp:ListItem Value="BATAL">BATAL = BATAL Pengajuan KPR</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="akt5" runat="server" Visible="False">
            <asp:ListItem Value="REGIS">REGIS = Registrasi Realisasi KPR</asp:ListItem>
            <asp:ListItem Value="EDIT">EDIT = Edit Realisasi KPR</asp:ListItem>
            <asp:ListItem Value="DELETE">DELETE = Delete Realisasi KPR</asp:ListItem>
            <asp:ListItem Value="VOID">VOID = VOID Realisasi KPR</asp:ListItem>
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
                <td><a id="prev" runat="server" class="abtn"><i class="fa fa-long-arrow-left"></i><b>Prev</b></a></td>
                <td><a id="next" runat="server" class="abtn"><b>Next</b> <i class="fa fa-long-arrow-right"></i></a></td>
                <td>
                    <asp:Button ID="ok" runat="server" CssClass="btn btn-green" Text="Approve" AccessKey="a" OnClick="ok_Click"></asp:Button></td>
                <td>Approval :
                <asp:Label ID="approveinfo" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table cellspacing="5">
            <tr>
                <td>Sumber
                </td>
                <td>:
                </td>
                <td>
                    <asp:TextBox ID="sumber" runat="server" CssClass="txt" ReadOnly="True" Width="190"></asp:TextBox>
                </td>
                <td width="10" rowspan="3"></td>
                <td>User
                </td>
                <td>:
                </td>
                <td>
                    <asp:TextBox ID="user" runat="server" CssClass="txt" ReadOnly="True" Width="65"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Nomor
                </td>
                <td>:
                </td>
                <td>
                    <asp:TextBox ID="logid" runat="server" CssClass="txt" ReadOnly="True" Width="75"></asp:TextBox>
                </td>
                <td>IP Addr.
                </td>
                <td>:
                </td>
                <td>
                    <asp:TextBox ID="ip" runat="server" CssClass="txt" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Referensi
                </td>
                <td>:
                </td>
                <td>
                    <asp:TextBox ID="pk" runat="server" CssClass="txt" ReadOnly="True" Width="150"></asp:TextBox>
                </td>
                <td>Tanggal
                </td>
                <td>:
                </td>
                <td>
                    <asp:TextBox ID="tgl" runat="server" CssClass="txt" ReadOnly="True" Width="150"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Aktivitas
                </td>
                <td>:
                </td>
                <td colspan="5">
                    <asp:DropDownList ID="aktivitas" runat="server" CssClass="ddl" Width="480">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <table cellspacing="5">
            <tr>
                <td>Deskripsi :
                <asp:TextBox ReadOnly="True" ID="ket" runat="server" TextMode="MultiLine" Width="550"
                    Height="300" CssClass="txt"></asp:TextBox>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
