<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.NUP.NUPDaftarCustomer" CodeFile="NUPDaftarCustomer.aspx.cs" %>

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
        .sm TD {
            font-weight: normal;
            font-size: 8pt;
            line-height: normal;
            font-style: normal;
            font-variant: normal;
        }
    </style>
</head>
<body class="body-padding" onkeyup="if(document.getElementById('cancel')){if(event.keyCode==27)document.getElementById('cancel').click()}">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <div style="display: none">
            <asp:CheckBox ID="dariReservasi" runat="server"></asp:CheckBox>
        </div>
        <h1>Registrasi NUP</h1>
        <p style="font-size: 8pt; color: #666;">Halaman 1 dari 2</p>
        <br>
        <table cellspacing="0">
            <tr valign="top">
                <td>
                    <table cellspacing="5" style="width: 470px; float: left;">
                        <tr>
                            <td colspan="3">
                                <p style="font-size: 12pt">
                                    <b>Identitas Customer</b>
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td>Project</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="project" runat="server" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Pilih Project :</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>No. Customer
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="nocustomer" runat="server" CssClass="txt" Width="75" Text="#AUTO#"
                                    ReadOnly="true" Font-Bold="True"></asp:TextBox>
                                &nbsp;
                            <input class="btn" id="btnpop" runat="server" show-modal='#ModalPopUp' modal-title='Daftar Customer' type="button" value="..." />
                            </td>
                        </tr>
                        <tr>
                            <td>Nama
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="nama" runat="server" CssClass="txt" Width="250" MaxLength="100"></asp:TextBox>
                                <asp:Label ID="namac" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>NPWP
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="npwp" runat="server" CssClass="txt igroup" Width="200" MaxLength="15"></asp:TextBox>
                                <asp:Label ID="npwpc" runat="server" CssClass="err" Width="250"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>No. Telp / HP
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="ctelp" runat="server" CssClass="txt" Width="90" MaxLength="100"></asp:TextBox>
                                <asp:Label ID="ctelpc" runat="server" CssClass="err"></asp:Label>
                                &nbsp;/&nbsp;
                            <asp:TextBox ID="chp" runat="server" CssClass="txt" Width="90" MaxLength="100"></asp:TextBox>
                                <asp:Label ID="chpc" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Email
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="email" runat="server" CssClass="txt" Width="140" MaxLength="50"></asp:TextBox>
                                <asp:Label ID="emailc" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>No. KTP
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="noktp" runat="server" CssClass="txt" Width="140" MaxLength="50"></asp:TextBox>
                                <asp:Label ID="noktpc" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top;">Alamat KTP
                            </td>
                            <td style="vertical-align: top;">:
                            </td>
                            <td>
                                <asp:TextBox ID="ktp1" runat="server" CssClass="txt" Width="200" MaxLength="100"></asp:TextBox>&nbsp;<font
                                    style="font-size: 7pt">(alamat)</font>
                                <br />
                                <asp:TextBox ID="ktp2" runat="server" CssClass="txt" Width="120" MaxLength="50"></asp:TextBox>&nbsp;<font
                                    style="font-size: 7pt">(RT/RW)</font>
                                <br />
                                <asp:TextBox ID="ktp5" runat="server" CssClass="txt" Width="120" MaxLength="50"></asp:TextBox>&nbsp;<font
                                    style="font-size: 7pt">(Kelurahan)</font>
                                <br />
                                <asp:TextBox ID="ktp3" runat="server" CssClass="txt" Width="120" MaxLength="50"></asp:TextBox>&nbsp;<font
                                    style="font-size: 7pt">(Kecamatan)</font>
                                <br />
                                <asp:TextBox ID="ktp4" runat="server" CssClass="txt" Width="120" MaxLength="50"></asp:TextBox>&nbsp;<font
                                    style="font-size: 7pt">(Kotamadya)</font>
                            </td>
                        </tr>
                        <tr>
                            <td>Alamat Korespondensi
                            </td>
                            <td>:
                            </td>
                            <td>
                                <label>
                                    <input type="radio" id="rbBeda" name="pilihcara" onclick="javascript: bedaisi()" />BEDA dengan KTP</label>
                                <label>
                                    <input type="radio" id="rbSama" name="pilihcara" onclick="javascript: samaisi()" />SAMA dengan KTP</label>
                            </td>
                        </tr>
                        <tr id="trKoresponden" runat="server">
                            <td colspan="2"></td>
                            <td>
                                <asp:TextBox ID="Korespon1" runat="server" CssClass="txt" Width="200" MaxLength="100"></asp:TextBox>&nbsp;<font
                                    style="font-size: 7pt">(alamat)</font>
                                <br />
                                <asp:TextBox ID="Korespon2" runat="server" CssClass="txt" Width="120" MaxLength="50"></asp:TextBox>&nbsp;<font
                                    style="font-size: 7pt">(RT/RW)</font>
                                <br />
                                <asp:TextBox ID="Korespon5" runat="server" CssClass="txt" Width="120" MaxLength="50"></asp:TextBox>&nbsp;<font
                                    style="font-size: 7pt">(Kelurahan)</font>
                                <br />
                                <asp:TextBox ID="Korespon3" runat="server" CssClass="txt" Width="120" MaxLength="50"></asp:TextBox>&nbsp;<font
                                    style="font-size: 7pt">(Kecamatan)</font>
                                <br />
                                <asp:TextBox ID="Korespon4" runat="server" CssClass="txt" Width="120" MaxLength="50"></asp:TextBox>&nbsp;<font
                                    style="font-size: 7pt">(Kotamadya)</font>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top">Rek. untuk Refund
                            </td>
                            <td style="vertical-align: top">:
                            </td>
                            <td>
                                <asp:TextBox ID="bank" runat="server" CssClass="txt" Width="140" MaxLength="100"></asp:TextBox>&nbsp;<font
                                    style="font-size: 7pt">(Bank)</font>&nbsp;<asp:Label ID="bankc" runat="server" CssClass="err"></asp:Label>
                                <br />
                                <asp:TextBox ID="cabang" runat="server" CssClass="txt" Width="140" MaxLength="50"></asp:TextBox>&nbsp;<font
                                    style="font-size: 7pt">(Cabang)</font>&nbsp;<asp:Label ID="cabangc" runat="server" CssClass="err"></asp:Label>
                                <br />
                                <asp:TextBox ID="rek" runat="server" CssClass="txt" Width="140" MaxLength="50"></asp:TextBox>&nbsp;<font
                                    style="font-size: 7pt">(No. Rekening)</font>&nbsp;<asp:Label ID="rekc" runat="server" CssClass="err" Style="width: 100px"></asp:Label>
                                <br />
                                <asp:TextBox ID="reknama" runat="server" CssClass="txt" Width="140" MaxLength="50"></asp:TextBox>&nbsp;<font
                                    style="font-size: 7pt">(Atas Nama)</font>&nbsp;<asp:Label ID="reknamac" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table cellspacing="5" style="width: 430px; float: right;">
                        <tr>
                            <td colspan="3">
                                <p style="font-size: 12pt">
                                    <b>Data NUP</b>
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td>No. NUP
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="nomorNUP" runat="server" CssClass="txt" Width="120" ReadOnly="True"
                                    Font-Bold="True" Text="#AUTO#"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Sales
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:DropDownList ID="agent" runat="server" Width="280" CssClass="ddl">
                                    <asp:ListItem Value="0">Pilih Agent :</asp:ListItem>
                                </asp:DropDownList>
                                <asp:Label ID="agentc" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Jenis Properti
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:DropDownList ID="jenisproperti" runat="server" Width="280" CssClass="ddl">
                                    <asp:ListItem Value="">Pilih Jenis Properti :</asp:ListItem>
                                </asp:DropDownList>
                                <asp:Label ID="jenispropertic" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:LinkButton ID="save" runat="server" Width="75" CssClass="btn btn-blue" OnClick="save_Click"><i class="fa fa-share"></i> OK</asp:LinkButton></td>
                </td>
                <td colspan="2">
                    <input id="cancel" runat="server" class="btn btn-red" onclick="location.href = 'NUPDaftarCustomer.aspx'; return false;"
                        style="width: 75px" type="button" value="Cancel">
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
            function call(no, nm, hp, telp, email, tgllahir, noktp, npwp, ktp1, ktp2, ktp3, ktp4, ktp5, sifat, kor1, kor2, kor3, kor4, kor5, nama,rekb, rekc, rekno, reknam) {
                document.getElementById('nocustomer').value = no;
                document.getElementById('nama').value = nm;
                document.getElementById('ctelp').value = telp;
                document.getElementById('chp').value = hp;
                document.getElementById('noktp').value = noktp;
                document.getElementById('ktp1').value = ktp1;
                document.getElementById('ktp2').value = ktp2;
                document.getElementById('ktp3').value = ktp4;
                document.getElementById('ktp4').value = ktp5;
                document.getElementById('ktp5').value = ktp3;
                document.getElementById('Korespon1').value = kor1;
                document.getElementById('Korespon2').value = kor2;
                document.getElementById('Korespon3').value = kor4;
                document.getElementById('Korespon4').value = kor5;
                document.getElementById('Korespon5').value = kor3;
                //document.getElementById('bank').value = rekb;
                //document.getElementById('cabang').value = rekc;
                //document.getElementById('rek').value = rekno;
                //document.getElementById('reknama').value = reknam;
            }

            function samaisi() {
                document.getElementById('Korespon1').value = document.getElementById('ktp1').value;
                document.getElementById('Korespon2').value = document.getElementById('ktp2').value;
                document.getElementById('Korespon3').value = document.getElementById('ktp3').value;
                document.getElementById('Korespon4').value = document.getElementById('ktp4').value;
                document.getElementById('Korespon5').value = document.getElementById('ktp5').value;
            }

            function bedaisi() {
                document.getElementById('Korespon1').value = '';
                document.getElementById('Korespon2').value = '';
                document.getElementById('Korespon3').value = '';
                document.getElementById('Korespon4').value = '';
                document.getElementById('Korespon5').value = '';
            }
        </script>

    </form>
</body>
</html>
