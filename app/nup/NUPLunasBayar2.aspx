<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<%@ Page language="c#" Inherits="ISC064.NUP.NUPLunasBayar2" CodeFile="NUPLunasBayar2.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>NUP - Pelunasan Registrasi NUP</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="NUP - Pelunasan Registrasi NUP (Hal. 2)">
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
        <h1>Pelunasan Registrasi NUP</h1>
        <p style="font-size: 8pt; color: #666;">Halaman 2 dari 3</p>
        <br />
        <br />
        <table cellspacing="0">
            <tr valign="top">
                <td>
                    <table cellspacing="5" style="width: 330px; float: left;">
                        <tr>
                            <td colspan="3">
                                <p style="font-size: 12pt">
                                    <b>Data Customer</b>
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td>Nama Customer
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:Label ID="namacs" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>No. KTP
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:Label ID="ktpcs" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>No. Telp/HP
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:Label ID="nokontak" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top">Rek. Refund
                            </td>
                            <td style="vertical-align: top">:
                            </td>
                            <td>
                                <asp:Label ID="refund" runat="server"></asp:Label>
                            </td>
                        </tr>
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
                                <asp:Label ID="nonup" runat="server"></asp:Label>
                            </td>
                        </tr>
                       
                        <tr>
                            <td>Nama Sales/Agent
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:Label ID="namaag" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Pembayaran Pertama
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:Label ID="bayar1" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table cellspacing="5" style="width: 420px; float: right;">
                        <tr>
                            <td colspan="3">
                                <p style="font-size: 12pt">
                                    <b>Pembayaran</b>
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td>Tanggal Penerimaan
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="tbTglTerima" CssClass="tgl" Width="80px" runat="server"></asp:TextBox>
                                <label for="tbTglTerima" class="btn btn-cal"><i class="fa fa-calendar"></i></label>  
                                <asp:Label ID="tbTglTerimac" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Sisa yang harus dibayar
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:DropDownList ID="valueNUP" runat="server" CssClass="ddl" Width="120">
                                </asp:DropDownList>
                                <asp:Label ID="valueNUPc" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top">Cara Bayar
                            </td>
                            <td style="vertical-align: top">:
                            </td>
                            <td>
                                <asp:RadioButtonList ID="carabayar" runat="server" RepeatColumns="2" RepeatDirection="Vertical">
                                    <asp:ListItem Value="KK">KK = Kartu Kredit</asp:ListItem>
                                    <asp:ListItem Value="KD">KD = Kartu Debit</asp:ListItem>
                                    <asp:ListItem Value="TR" Selected="True">TR = Transfer Bank</asp:ListItem>
				    <asp:ListItem Value="TN" Style="Display:none">TN = Tunai</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:Label ID="carabayarc" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Rekening Bank
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlAcc" runat="server" CssClass="ddl" Width="280">
                                    <asp:ListItem Value="" Selected="True">Pilih Rekening :</asp:ListItem>
                                </asp:DropDownList>
                                <asp:Label ID="ddlAccErr" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td>No. Kartu
                            </td>
                            <td>:
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
                            <td>Keterangan
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="ket" CssClass="txt" Width="350px" Rows="4" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:LinkButton ID="save" runat="server" Width="75" CssClass="btn btn-blue" OnClick="save_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
                            </td>
                            <td colspan="2">
                                <input id="cancel" runat="server" class="btn btn-red" onclick="location.href = 'NUPLunasBayar.aspx'; return false;"
                                    style="width: 75px" type="button" value="Cancel">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Label ID="note" runat="server" CssClass="err"></asp:Label>
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