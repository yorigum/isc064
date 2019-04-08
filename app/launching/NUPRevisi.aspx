<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.LAUNCHING.NUPRevisi" CodeFile="NUPRevisi.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>NUP - Revisi</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="NUP - Revisi (Hal. 1)">
    <style type="text/css">
        .sm TD
        {
            font-weight: normal;
            font-size: 8pt;
            line-height: normal;
            font-style: normal;
            font-variant: normal;
        }
    </style>
</head>
<body onkeyup="if(document.getElementById('cancel')){if(event.keyCode==27)document.getElementById('cancel').click()}">
    <form id="Form1" method="post" runat="server" class="cnt">
    <uc1:Head ID="Head1" runat="server"></uc1:Head>
    <div style="display: none">
        <asp:CheckBox ID="dariReservasi" runat="server"></asp:CheckBox>
    </div>
    <h1>
        Revisi NUP</h1>
    <p style="font-size: 8pt; color: #666;">
        Halaman 1 dari 2</p>
    <br>
    <table cellspacing="0">
        <tr valign="top">
            <td>
                <table cellspacing="5" style="width: 400px; float: left;">
                    <tr>
                        <td colspan="3">
                            <p style="font-size: 12pt">
                                <b>Identitas Customer</b></p>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            No. Customer
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="nocustomer" runat="server" CssClass="txt" Width="75" Text="#AUTO#"
                                ReadOnly="true" Font-Bold="True"></asp:TextBox>
                            &nbsp;
                            <input class="btn" id="btnpop" onclick="popDaftarCustomer2('a')" type="button" value="..." />
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
                            <asp:Label ID="namac" runat="server" CssClass="err"></asp:Label>
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
                            <asp:Label ID="ctelpc" runat="server" CssClass="err"></asp:Label>
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
                            <asp:Label ID="noktpc" runat="server" CssClass="err"></asp:Label>
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
                            <asp:RadioButton ID="beda" runat="server" Text="BEDA dengan KTP" GroupName="tipe2"
                                Checked="True" OnCheckedChanged="beda_CheckedChanged" AutoPostBack="true"></asp:RadioButton>
                            <asp:RadioButton ID="sama" runat="server" Text="SAMA dengan KTP" GroupName="tipe2"
                                OnCheckedChanged="sama_CheckedChanged" AutoPostBack="true"></asp:RadioButton>
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
                                style="font-size: 7pt">(Bank)</font>&nbsp;<asp:Label ID="bankc" runat="server" CssClass="err"></asp:Label>
                            <br />
                            <asp:TextBox ID="cabang" runat="server" CssClass="txt" Width="140" MaxLength="50"></asp:TextBox>&nbsp;<font
                                style="font-size: 7pt">(Cabang)</font>&nbsp;<asp:Label ID="cabangc" runat="server"
                                    CssClass="err"></asp:Label>
                            <br />
                            <asp:TextBox ID="rek" runat="server" CssClass="txt" Width="140" MaxLength="50"></asp:TextBox>&nbsp;<font
                                style="font-size: 7pt">(No. Rekening)</font>&nbsp;<asp:Label ID="rekc" runat="server"
                                    CssClass="err"></asp:Label>
                            <br />
                            <asp:TextBox ID="reknama" runat="server" CssClass="txt" Width="140" MaxLength="50"></asp:TextBox>&nbsp;<font
                                style="font-size: 7pt">(Atas Nama)</font>&nbsp;<asp:Label ID="reknamac" runat="server"
                                    CssClass="err"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table cellspacing="5" style="width: 430px; float: right;">
                    <tr>
                        <td colspan="3">
                            <p style="font-size: 12pt">
                                <b>Data NUP</b></p>
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
                            Sales
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:DropDownList ID="agent" runat="server" Width="200" CssClass="ddl">
                                <asp:ListItem Value="0">Pilih Agent :</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="agentc" runat="server" CssClass="err"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Tipe yang diminati
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:DropDownList ID="tipe" runat="server" CssClass="ddl" Width="120">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="trsave" runat="server" visible="false">
                        <td>
                            <asp:Button ID="save" runat="server" CssClass="btn" Text="OK" Width="75" OnClick="save_Click">
                            </asp:Button>
                        </td>
                        <td colspan="2">
                            <input id="cancel" runat="server" class="btn" onclick="location.href='NUP.aspx'; return false;"
                                style="width: 75px" type="button" value="Cancel">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="height:50px;"></td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <b>Otorisasi Atasan</b>
                        </td>
                    </tr>
                    <tr id="trotorisasi" runat="server">
                        <td colspan="3">
                            <table>
                                <tr>
                                    <td>
                                        Username
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="username" runat="server" CssClass="txt" Width="120"></asp:TextBox>
                                        &nbsp;
                                        <asp:Label ID="usernamec" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Password
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="password" TextMode="Password" runat="server" CssClass="txt" Width="120"></asp:TextBox>
                                        &nbsp;
                                        <asp:Label ID="passc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Button ID="otorisasi" runat="server" CssClass="btn" Text="Otorisasi" Width="75"
                                            OnClick="otorisasi_Click"></asp:Button>
                                    </td>
                                    <td>
                                        <input id="cancel2" runat="server" class="btn" onclick="location.href='NUP.aspx'; return false;"
                                            style="width: 75px" type="button" value="Cancel">
                                    </td>
                                </tr>
                                <td colspan="3">
                                    <asp:Label ID="lblotorisasi" runat="server" CssClass="err"></asp:Label>
                                </td>
                            </table>
                        </td>
                    </tr>
                    <tr id="trWarn" runat="server" visible="false">
                        <td colspan="3">
                            <font class="err">Tombol 'OK' dikunci, harap melengkapi data customer di NUP</font>
                        </td>
                    </tr>
                    <p class="feed">
                        <asp:Label ID="feed" runat="server"></asp:Label>
                    </p>
                </table>
            </td>
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
    </script>

    </form>
</body>
</html>
