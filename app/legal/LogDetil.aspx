<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.LogDetil" CodeFile="LogDetil.aspx.cs" %>

<!DOCTYPE html>
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
<body class="body-padding pop" onkeyup="if(event.keyCode==27)window.close()"
    onload="document.getElementById('ket').focus();">
    <form id="Form1" method="post" runat="server">
        <asp:DropDownList ID="akt1" runat="server" Visible="False">
            <asp:ListItem Value="DAFTAR">DAFTAR = Pendaftaran Customer</asp:ListItem>
            <asp:ListItem Value="EDIT">EDIT = Edit Customer</asp:ListItem>
            <asp:ListItem Value="FOTO">FOTO = Edit Foto</asp:ListItem>
            <asp:ListItem Value="DELETE">DELETE = Delete Customer (Permanen)</asp:ListItem>
            <asp:ListItem Value="GABUNG">GABUNG = Prosedur Gabung Nomor</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="akt2" runat="server" Visible="False">
            <asp:ListItem Value="DAFTAR">DAFTAR = Pendaftaran Reservasi</asp:ListItem>
            <asp:ListItem Value="P-WL">P-WL = Print Kartu Waiting List</asp:ListItem>
            <asp:ListItem Value="R-WL">R-WL = Otorisasi Reprint Kartu Waiting List</asp:ListItem>
            <asp:ListItem Value="EDIT">EDIT = Edit Reservasi</asp:ListItem>
            <asp:ListItem Value="PWL">PWL = Promote Waiting List</asp:ListItem>
            <asp:ListItem Value="DELETE">DELETE = Delete Reservasi (Permanen)</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="akt3" runat="server" Visible="False">
            <asp:ListItem Value="DAFTAR">DAFTAR = Pendaftaran Surat Pesanan</asp:ListItem>
            <asp:ListItem Value="P-SP">P-SP = Print Surat Pesanan</asp:ListItem>
            <asp:ListItem Value="R-SP">R-SP = Otorisasi Reprint Surat Pesanan</asp:ListItem>
            <asp:ListItem Value="DISKON">DISKON = Prosedur Diskon</asp:ListItem>
            <asp:ListItem Value="RT">RT = Reset Tagihan</asp:ListItem>
            <asp:ListItem Value="CUSTOM">CUSTOM = Customize Tagihan</asp:ListItem>
            <asp:ListItem Value="RESCHE">RESCHE = Reschedule Tagihan</asp:ListItem>
            <asp:ListItem Value="KOMISI">KOMISI = Generate Komisi</asp:ListItem>
            <asp:ListItem Value="RK">RK = Reset Komisi</asp:ListItem>
            <asp:ListItem Value="P-RKOM">P-RKOM = Print Rencana Komisi</asp:ListItem>
            <asp:ListItem Value="R-RKOM">R-RKOM = Otorisasi Reprint Rencana Komisi</asp:ListItem>
            <asp:ListItem Value="PPJB">PPJB = Perjanjian Pengikatan Jual Beli</asp:ListItem>
            <asp:ListItem Value="P-PPJB">P-PPJB = Print PPJB</asp:ListItem>
            <asp:ListItem Value="R-PPJB">R-PPJB = Otorisasi Reprint PPJB</asp:ListItem>
            <asp:ListItem Value="AJB">AJB = Akte Jual Beli</asp:ListItem>
            <asp:ListItem Value="P-AJB">P-AJB = Print AJB</asp:ListItem>
            <asp:ListItem Value="R-AJB">R-AJB = Otorisasi Reprint AJB</asp:ListItem>
            <asp:ListItem Value="ST">ST = Serah Terima</asp:ListItem>
            <asp:ListItem Value="P-BAST">P-BAST = Print BAST</asp:ListItem>
            <asp:ListItem Value="R-BAST">R-BAST = Otorisasi Reprint BAST</asp:ListItem>
            <asp:ListItem Value="GN">GN = Pengalihan Hak</asp:ListItem>
            <asp:ListItem Value="GU">GU = Pindah Unit</asp:ListItem>
            <asp:ListItem Value="BATAL">BATAL = Pembatalan Kontrak</asp:ListItem>
            <asp:ListItem Value="EDIT">EDIT = Edit Kontrak</asp:ListItem>
            <asp:ListItem Value="STATUS">STATUS = Edit Status Kontrak</asp:ListItem>
            <asp:ListItem Value="EJT">EJT = Edit Jadwal Tagihan</asp:ListItem>
            <asp:ListItem Value="EJK">EJK = Edit Jadwal Komisi</asp:ListItem>
            <asp:ListItem Value="EAP">EAP = Edit Alokasi Pelunasan</asp:ListItem>
            <asp:ListItem Value="REF">REF = Refresh Data Unit</asp:ListItem>
            <asp:ListItem Value="DELETE">DELETE = Delete Kontrak (Permanen)</asp:ListItem>
            <asp:ListItem Value="SK">SK = Solve Komisi</asp:ListItem>
            <asp:ListItem Value="RD">RD = Realisasi Denda</asp:ListItem>
            <asp:ListItem Value="T-PPJB">T-PPJB = Target PPJB</asp:ListItem>
            <asp:ListItem Value="PPJB">PPJB = Registrasi PPJB</asp:ListItem>
            <asp:ListItem Value="TTD-PPJB">TTD-PPJB = Tanda Tangan PPJB</asp:ListItem>
            <asp:ListItem Value="T-AJB">T-AJB = Target AJB</asp:ListItem>
            <asp:ListItem Value="AJB">AJB = Registrasi AJB</asp:ListItem>
            <asp:ListItem Value="TTD-AJB">TTD-AJB = Tanda Tangan AJB</asp:ListItem>
            <asp:ListItem Value="T-BAST">T-BAST = Target BAST</asp:ListItem>
            <asp:ListItem Value="BAST">BAST = Registrasi BAST</asp:ListItem>
            <asp:ListItem Value="TTD-BAST">TTD-BAST = Tanda Tangan BAST</asp:ListItem>
            <asp:ListItem Value="T-IMB">T-IMB = Target IMB</asp:ListItem>
            <asp:ListItem Value="PRO-IMB">PRO-IMB = Proses IMB</asp:ListItem>
            <asp:ListItem Value="IMB">IMB = IMB</asp:ListItem>
            <asp:ListItem Value="T-STT">T-STT = Target Sertifikat</asp:ListItem>
            <asp:ListItem Value="PRO-STT">PRO-STT = Proses Sertifikat</asp:ListItem>
            <asp:ListItem Value="STT">STT = Registrasi Sertifikat</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="akt4" runat="server" Visible="False">
            <asp:ListItem Value="REGIS">REGIS = Registrasi Tanda Terima Reservasi</asp:ListItem>
            <asp:ListItem Value="POST">POST = Posting Tanda Terima Reservasi</asp:ListItem>
            <asp:ListItem Value="VOID">VOID = Void Tanda Terima Reservasi</asp:ListItem>
            <asp:ListItem Value="P-TTR">P-TTR = Print Tanda Terima Reservasi</asp:ListItem>
            <asp:ListItem Value="R-TTR">R-TTR = Otorisasi Reprint Tanda Terima Reservasi</asp:ListItem>
            <asp:ListItem Value="EDIT">EDIT = Edit Tanda Terima Reservasi</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="akt5" runat="server" Visible="False"></asp:DropDownList>
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
                <td>Deskripsi :
						<asp:TextBox ReadOnly="True" ID="ket" runat="server" TextMode="MultiLine" Width="550" Height="300"
                            CssClass="txt"></asp:TextBox>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
