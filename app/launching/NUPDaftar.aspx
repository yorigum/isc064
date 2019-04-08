<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.LAUNCHING.NUPDaftar" CodeFile="NUPDaftar.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>NUP - Registrasi Customer</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="NUP - Registrasi Customer (Hal. 1)">
    <style type="text/css">
        .sm TD
        {
            font-weight: normal;
            font-size: 8pt;
            line-height: normal;
            font-style: normal;
            font-variant: normal;
        }
        .nav, .navsub
        {
            border: 0px;
            background-color: #EEEEEE;
            font: 8pt Trebuchet MS;
            padding-left: 7;
            text-align: left;
            width: 190;
            height: 18px;
        }
        .nav2
        {
            border: 0px;
            background-color: #EEEEEE;
            font: 14pt Trebuchet MS;
            padding-left: 7;
            text-align: left;
            width: 200;
            height: 30px;
        }
    </style>
</head>
<body style="height: 700px;">
    <form id="Form1" method="post" runat="server" class="cnt">
    <uc1:Head ID="Head1" runat="server"></uc1:Head>
    <div style="float: left; width: 40%;">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="width: 20px">
                    <img src="/Media/icon_prev_c.gif" style="width: 30px; height: 30px;">
                </td>
                <td>
                    <input type="button" value="Back" class="nav2" style="width: 80px;" onmouseover="over(this)"
                        onclick="window.location='Nup.aspx';" onmouseout="out(this)">
                </td>
            </tr>
        </table>
    </div>
    <div style="float: right; width: 10%;">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="width: 20px">
                    <img src="/Media/icon_gateway.gif" style="width: 30px; height: 30px;">
                </td>
                <td>
                    <input type="button" value="Gateway" class="nav2" onclick="top.location.href='/Gateway.aspx'"
                        style="width: 100px" onmouseover="over(this)" onmouseout="out(this)">
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 20px">
                    <img src="/Media/icon_out.gif" style="width: 30px; height: 30px;">
                </td>
                <td>
                    <input type="button" value="Sign-Out" class="nav2" onclick="if(confirm('Apakah anda ingin melakukan sign-out?\nProgram dan absensi aktif anda akan ditutup.')){top.location.href='SignOut.aspx'}"
                        style="width: 100px" onmouseover="over(this)" onmouseout="out(this)">
                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
    <br />
    <h1>
        Registrasi NUP</h1>
    <p style="font-size: 8pt; color: #666;">
        Halaman 1 dari 2</p>
    <br>
    <table cellspacing="0" cellpadding="20">
        <tr valign="top">
            <td style="width: 50%;">
                <table>
                    <tr>
                        <td colspan="3">
                            <p style="font-size: 12pt">
                                <b>Identitas Customer</b></p>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            No. NUP
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="nomorNUP" runat="server" CssClass="txt" Width="120" ReadOnly="True"
                                Font-Bold="True" Text="#AUTO#"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Tanggal NUP
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="tglNUP" runat="server" Width="100px" CssClass="txt_center" ReadOnly="false"
                                Height="20px"></asp:TextBox>&nbsp;<input class="btn" onclick="openCalendar('tglNUP')"
                                    type="button" value="...">
                            <asp:Label ID="tglNUPc" runat="server" CssClass="err">*</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Sumber Data
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:DropDownList ID="sumberdata" runat="server" CssClass="ddl" Width="130">
                                <asp:ListItem Value="0">Pilih Sumber Data :</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="sumberdatac" runat="server" CssClass="err">*</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Nama
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="nama" runat="server" CssClass="txt" Width="250" MaxLength="100"></asp:TextBox>
                            <asp:Label ID="namac" runat="server" CssClass="err">*</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            No. Telp / HP
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="ctelp" runat="server" CssClass="txt" Width="90" MaxLength="100"></asp:TextBox>
                            &nbsp;/&nbsp;
                            <asp:TextBox ID="chp" runat="server" CssClass="txt" Width="90" MaxLength="100"></asp:TextBox>
                            <asp:Label ID="ctelpc" runat="server" CssClass="err">*</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            No. KTP
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="noktp" runat="server" CssClass="txt" Width="140" MaxLength="50"></asp:TextBox>
                            <asp:Label ID="noktpc" runat="server" CssClass="err">*</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            No. NPWP
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="nonpwp" runat="server" CssClass="txt" Width="200" MaxLength="100"></asp:TextBox>
                            <asp:Label ID="nonpwpc" runat="server" CssClass="err">*</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Email
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="email" runat="server" CssClass="txt" Width="200" MaxLength="100"></asp:TextBox>
                            <asp:Label ID="emailc" runat="server" CssClass="err">*</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top;">
                            Alamat KTP
                        </td>
                        <td style="vertical-align: top;">
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="ktp1" runat="server" CssClass="txt" Width="200" MaxLength="100"></asp:TextBox>&nbsp;<font
                                style="font-size: 7pt">(alamat)</font>
                            <br />
                            <asp:TextBox ID="ktp2" runat="server" CssClass="txt" Width="120" MaxLength="50"></asp:TextBox>&nbsp;<font
                                style="font-size: 7pt">(RT/RW)</font>
                            <br />
                            <asp:TextBox ID="ktp3" runat="server" CssClass="txt" Width="120" MaxLength="50"></asp:TextBox>&nbsp;<font
                                style="font-size: 7pt">(Kecamatan)</font>
                            <br />
                            <asp:TextBox ID="ktp4" runat="server" CssClass="txt" Width="120" MaxLength="50"></asp:TextBox>&nbsp;<font
                                style="font-size: 7pt">(Kotamadya)</font>
                            <asp:Label ID="ktpc" runat="server" CssClass="err">*</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Alamat Korespondensi
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <label>
                                <input type="radio" id="rbBeda" name="pilihcara" onclick="javascript:bedaisi()" />BEDA
                                dengan KTP</label>
                            <label>
                                <input type="radio" id="rbSama" name="pilihcara" onclick="javascript:samaisi()" />SAMA
                                dengan KTP</label>
                        </td>
                    </tr>
                    <tr id="trKoresponden" runat="server">
                        <td colspan="2">
                        </td>
                        <td>
                            <asp:TextBox ID="Korespon1" runat="server" CssClass="txt" Width="200" MaxLength="100"></asp:TextBox>&nbsp;<font
                                style="font-size: 7pt">(alamat)</font>
                            <br />
                            <asp:TextBox ID="Korespon2" runat="server" CssClass="txt" Width="120" MaxLength="50"></asp:TextBox>&nbsp;<font
                                style="font-size: 7pt">(RT/RW)</font>
                            <br />
                            <asp:TextBox ID="Korespon3" runat="server" CssClass="txt" Width="120" MaxLength="50"></asp:TextBox>&nbsp;<font
                                style="font-size: 7pt">(Kecamatan)</font>
                            <br />
                            <asp:TextBox ID="Korespon4" runat="server" CssClass="txt" Width="120" MaxLength="50"></asp:TextBox>&nbsp;<font
                                style="font-size: 7pt">(Kotamadya)</font>
                            <asp:Label ID="Koresponc" runat="server" CssClass="err">*</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top">
                            Rek. untuk Refund
                        </td>
                        <td style="vertical-align: top">
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="bank" runat="server" CssClass="txt" Width="140" MaxLength="100"></asp:TextBox>&nbsp;<font
                                style="font-size: 7pt">(Bank)</font>&nbsp;<asp:Label ID="bankc" runat="server" CssClass="err">*</asp:Label>
                            <br />
                            <asp:TextBox ID="cabang" runat="server" CssClass="txt" Width="140" MaxLength="50"></asp:TextBox>&nbsp;<font
                                style="font-size: 7pt">(Cabang)</font>&nbsp;<asp:Label ID="cabangc" runat="server"
                                    CssClass="err">*</asp:Label>
                            <br />
                            <asp:TextBox ID="rek" runat="server" CssClass="txt" Width="140" MaxLength="50"></asp:TextBox>&nbsp;<font
                                style="font-size: 7pt">(No. Rekening)</font>&nbsp;<asp:Label ID="rekc" runat="server"
                                    CssClass="err">*</asp:Label>
                            <br />
                            <asp:TextBox ID="reknama" runat="server" CssClass="txt" Width="140" MaxLength="50"></asp:TextBox>&nbsp;<font
                                style="font-size: 7pt">(Atas Nama)</font>&nbsp;<asp:Label ID="reknamac" runat="server"
                                    CssClass="err">*</asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%;">
                <table>
                    <tr>
                        <td>
                            Sales
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:DropDownList ID="agent" runat="server" Width="280" CssClass="ddl">
                                <asp:ListItem Value="0">Pilih Agent :</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="agentc" runat="server" CssClass="err">*</asp:Label>
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td>
                            Tipe yang diminati
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:DropDownList ID="tipe" runat="server" CssClass="ddl" Width="120">
                                <asp:ListItem Value="0">Pilih Tipe :</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="tipec" runat="server" CssClass="err">*</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <p style="font-size: 12pt">
                                <b>Pembayaran</b></p>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            No. Tanda Terima
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="nottnup" runat="server" CssClass="txt" Width="120" ReadOnly="True"
                                Font-Bold="True" Text="#AUTO#"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Nilai Pembayaran
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="nilaibayar" runat="server" CssClass="txt" Width="140" MaxLength="50"
                                ReadOnly="true"></asp:TextBox>
                            <asp:Label ID="nilaibayarc" runat="server" CssClass="err">*</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top">
                            Cara Bayar
                        </td>
                        <td style="vertical-align: top">
                            :
                        </td>
                        <td>
                            <asp:RadioButtonList ID="carabayar" runat="server" RepeatColumns="3" RepeatDirection="Vertical">
                                <%--OnSelectedIndexChanged="carabayar_SelectedIndexChanged" AutoPostBack="true">--%>
                                <asp:ListItem Value="TN">TN = Tunai</asp:ListItem>
                                <asp:ListItem Value="KK">KK = Kartu Kredit</asp:ListItem>
                                <asp:ListItem Value="KD">KD = Kartu Debit</asp:ListItem>
                                <asp:ListItem Value="TR">TR = Transfer Bank</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:Label ID="carabayarc" runat="server" CssClass="err">*</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Rekening Bank
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlAcc" runat="server" CssClass="ddl" Width="280">
                                <asp:ListItem Value="" Selected="True">Pilih Rekening :</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="ddlAccErr" runat="server" CssClass="err">*</asp:Label>
                        </td>
                    </tr>
                    <div id="ketkartu" runat="server" visible="false">
                        <tr>
                            <td colspan="3">
                                <p>
                                    <b>Isi Data Kartu Debit / Kredit</b></p>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                No Kartu Debit/Kredit
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="nokk1" CssClass="txt" Width="45px" MaxLength="4" runat="server"></asp:TextBox>
                                &nbsp;-&nbsp;<asp:TextBox ID="nokk2" CssClass="txt" Width="45px" MaxLength="4" runat="server"></asp:TextBox>
                                &nbsp;-&nbsp;<asp:TextBox ID="nokk3" CssClass="txt" Width="45px" MaxLength="4" runat="server"></asp:TextBox>
                                &nbsp;-&nbsp;<asp:TextBox ID="nokk4" CssClass="txt" Width="45px" MaxLength="4" runat="server"></asp:TextBox>
                                &nbsp;<asp:Label ID="nokkc" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Bank Penerbit Kartu
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="txtNamaBank" runat="server" Width="125" CssClass="txt" MaxLength="20"></asp:TextBox>
                                <asp:Label ID="txtNamaBankc" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                    </div>
                    <br />
                    <div id="tranfer" runat="server" visible="false">
                        <tr>
                            <td colspan="3">
                                <p>
                                    <b>Isi Data Transfer</b></p>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Transfer Dari Bank
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="txtNamaBankTransfer" runat="server" Width="125" CssClass="txt"></asp:TextBox>
                                <asp:Label ID="txtNamaBankTransferc" runat="server" CssClass="err"></asp:Label>
                                <asp:TextBox ID="anonim" runat="server" Width="125" CssClass="txt" Visible="false"></asp:TextBox>
                                <div style="display: none">
                                    <asp:TextBox ID="noanonim" runat="server"></asp:TextBox><</div>
                                <input class="btn" id="btnAno" onclick="popDaftarAnonim('open')" type="button" value="..."
                                    name="btnAno" runat="server" visible="false" />
                                <asp:Label ID="anonimc" runat="server" CssClass="err" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Tanggal Transfer
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="tglTransfer" runat="server" CssClass="txt_center" Width="85"></asp:TextBox>
                                <input type="button" value="..." class="btn" onclick="openCalendar('tglTransfer')">
                                <asp:Label ID="tglTransferc" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                    </div>
                    <div id="CaraBG" runat="server" visible="false">
                        <tr>
                            <td colspan="3">
                                <br>
                                <p>
                                    <b>Khusus Cek Giro</b>&nbsp;&nbsp;&nbsp;<i>(data akan otomatis masuk ke master cek giro)</i></p>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                No. BG
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="nobg" runat="server" Width="125" CssClass="txt" MaxLength="20"></asp:TextBox>
                                <input type="button" value="..." class="btn" onclick="popDaftarBG()" id="btnpop"
                                    runat="server" name="btnpop" visible="false">
                                <asp:Label ID="nobgc" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Tanggal BG
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="tglbg" runat="server" CssClass="txt_center" Width="85"></asp:TextBox>
                                <input type="button" value="..." class="btn" onclick="openCalendar('tglbg')">
                                <asp:Label ID="tglbgc" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Dari Bank
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="txtBankGiro" runat="server" Width="125" CssClass="txt" MaxLength="20"></asp:TextBox>
                                <asp:Label ID="txtBankGiroc" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                    </div>
                    <tr>
                        <td>
                            Keterangan
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="ket" runat="server" CssClass="txt" Width="200" MaxLength="100"></asp:TextBox>
                            <asp:Label ID="ketc" runat="server" CssClass="err"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="save" runat="server" CssClass="btn" Text="OK" Width="75" OnClick="save_Click">
                            </asp:Button>
                        </td>
                        <td colspan="2">
                            <input id="cancel" runat="server" class="btn" onclick="location.href='Nup.aspx'; return false;"
                                style="width: 75px" type="button" value="Cancel">
                        </td>
                        <p class="feed">
                            <asp:Label ID="feed" runat="server"></asp:Label>
                        </p>
                    </tr>
                </table>

                <script language="javascript">
        function call(no, nm, hp, telp, noktp, ktp1, ktp2, ktp3, ktp4, kor1, kor2, kor3, kor4, rekb, rekc, rekno, reknam) {
            //document.getElementById('nocustomer').value = no;
            document.getElementById('nama').value = nm;
            document.getElementById('ctelp').value = telp;
            document.getElementById('chp').value = hp;
            document.getElementById('noktp').value = noktp;
            document.getElementById('ktp1').value = ktp1;
            document.getElementById('ktp2').value = ktp2;
            document.getElementById('ktp3').value = ktp3;
            document.getElementById('ktp4').value = ktp4;
            document.getElementById('Korespon1').value = kor1;
            document.getElementById('Korespon2').value = kor2;
            document.getElementById('Korespon3').value = kor3;
            document.getElementById('Korespon4').value = ktp4;
            document.getElementById('bank').value = rekb;
            document.getElementById('cabang').value = rekc;
            document.getElementById('rek').value = rekno;
            document.getElementById('reknama').value = reknam;
        }

        function samaisi() {
            document.getElementById('Korespon1').value = document.getElementById('ktp1').value;
            document.getElementById('Korespon2').value = document.getElementById('ktp2').value;
            document.getElementById('Korespon3').value = document.getElementById('ktp3').value;
            document.getElementById('Korespon4').value = document.getElementById('ktp4').value;
        }

        function bedaisi() {
            document.getElementById('Korespon1').value = '';
            document.getElementById('Korespon2').value = '';
            document.getElementById('Korespon3').value = '';
            document.getElementById('Korespon4').value = '';
        }
                </script>

    </form>
</body>
</html>
