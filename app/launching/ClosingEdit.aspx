<%@ Reference Page="~/Customer.aspx" %>
<%@ Reference Page="~/Unit.aspx" %>
<%@ Reference Page="~/Skema.aspx" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.LAUNCHING.ClosingEdit" CodeFile="ClosingEdit.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Pendaftaran Closing Kontrak</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Closing Langsung - Pendaftaran Closing Langsung (Hal. 2)">
    <style type="text/css">
        .style1 {
            height: 26px;
        }
    </style>
</head>
<body class="body-padding" onkeyup="if(document.getElementById('cancel')){if(event.keyCode==27)document.getElementById('cancel').click()}">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <input type="text" style="display: none">
        <table cellpadding="0" cellspacing="0">
            <tr valign="top">
                <td width="550">
                    <div id="frm" runat="server">
                        <h1>Closing Kontrak</h1>
                        <p>
                            Halaman 2 dari 3
                        </p>
                        <br />
                        <div id="dclosing" runat="server">
                            <table cellpadding="2">
                                <tr>
                                    <td colspan="3">
                                        <h3>
                                            <span style="width: 30px">1.</span>UNIT YANG DIPESAN</h3>
                                    </td>
                                </tr>
                                <tr>
                                    <td>NUP
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="noqueue" runat="server" Width="150" CssClass="txt_center" Enabled="false"></asp:TextBox>
                                        <asp:Label ID="noqueuec" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Tanggal Kontrak
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tglKontrak" runat="server" Width="100px" CssClass="txt_center" ReadOnly="False"
                                            Height="20px"></asp:TextBox>&nbsp;
                                        <%--<input class="btn" onclick="openCalendar('tglKontrak')"
                                                type="button" value="...">--%>
                                        </nobr>
                                    <asp:Label ID="tglkontrakc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Kode Unit
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="unit" runat="server" Width="100" CssClass="txt" Enabled="false"
                                            ReadOnly="true"></asp:TextBox>
                                        <div style="display: none">
                                            <asp:TextBox ID="nostock" runat="server"></asp:TextBox>
                                            <asp:TextBox ID="nokontrak" runat="server"></asp:TextBox>
                                            <asp:TextBox ID="nokontrakmanual" runat="server"></asp:TextBox>
                                        </div>
                                        <input visible="false" class="btn" id="btnUnit" onclick="popDaftarUnit2('a')" type="button"
                                            value="..." name="btnUnit" runat="server" />
                                        <asp:Label ID="unitc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td>Price List
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="Pricelist" runat="server" Width="100" Enabled="false"></asp:TextBox>&nbsp
                                    rupiah
                                    <asp:Label ID="pricec" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr valign="top" id="trsurcharge" runat="server" style="display: none">
                                    <td>Surcharge
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="Surcharge" runat="server" CssClass="txt_num" OnTextChanged="Surcharge_TextChanged">0</asp:TextBox>
                                        <asp:Label ID="surchargec" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td class="style1">Skema
                                    </td>
                                    <td class="style1">:
                                    </td>
                                    <td class="style1">
                                        <asp:DropDownList Enabled="true" ID="skema" runat="server" Width="400" CssClass="ddl"
                                            AutoPostBack="True" OnSelectedIndexChanged="skema_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td>Sifat PPN
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="sifatppn" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" OnSelectedIndexChanged="sifatppn_SelectedIndexChanged">
                                            <asp:ListItem Selected="False">Tanpa PPN</asp:ListItem>
                                            <asp:ListItem Selected="True">Dengan PPN</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr id="trppn" runat="server" style="display: none">
                                    <td colspan="2">&nbsp;
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="includeppn" runat="server" Text="Nilai Transaksi adalah Include PPN"
                                            Checked="True"></asp:CheckBox><br />
                                        <asp:CheckBox ID="roundppn" runat="server" Checked="True" Text="Nilai PPN Dibulatkan"></asp:CheckBox>
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td valign="top">Diskon Skema
                                    </td>
                                    <td valign="top">:
                                    </td>
                                    <td>
                                        <%--onselectedindexchanged="sifatppn_SelectedIndexChanged">--%>
                                        <div id="lumsum" runat="server" style="display: none">
                                            <asp:TextBox ID="diskon" runat="server" CssClass="txt_num">0</asp:TextBox><asp:Label
                                                ID="diskonc" runat="server" CssClass="err"></asp:Label>
                                        </div>
                                        <div id="persentingkat" runat="server">
                                            <input class="btn" onclick="popdiskon('diskon2', 'diskonket')" type="button" value="..."
                                                id="btnBertingkat" runat="server" visible="false" />
                                            <asp:TextBox ID="diskon2" runat="server" Width="60px" MaxLength="100" CssClass="txt_num"
                                                AutoPostBack="true" OnTextChanged="diskon2_TextChanged">0</asp:TextBox>&nbsp;
                                        <div style="display: none">
                                            <asp:TextBox ID="diskonket" runat="server"></asp:TextBox>
                                        </div>
                                            <asp:TextBox ID="nilaiDiskon" runat="server" CssClass="txt_num" ReadOnly="True" Width="108px">0</asp:TextBox>
                                            <%--onselectedindexchanged="sifatppn_SelectedIndexChanged">--%>
                                            <asp:Label ID="diskon2c" runat="server" CssClass="err"></asp:Label>
                                        </div>
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td valign="top">Diskon Harga Jual
                                    </td>
                                    <td valign="top">:
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="jenisDiskon" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
                                            OnSelectedIndexChanged="jenisDiskon_SelectedIndexChanged" Enabled="false">
                                            <asp:ListItem Selected="True">Rp</asp:ListItem>
                                            <asp:ListItem>% Bertingkat</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <div id="divLumpSum" runat="server">
                                            <asp:TextBox ID="diskonLumpSum" runat="server" CssClass="txt_num">0</asp:TextBox>
                                            <asp:Label ID="diskonLumpSumc" runat="server" CssClass="err" />
                                        </div>
                                        <div id="divPersenBertingkat" runat="server">
                                            <asp:TextBox ID="diskontambahPersen" runat="server" Width="150" MaxLength="100" CssClass="txt">0</asp:TextBox>
                                            <input class="btn" onclick="popdiskon('diskontambahPersen', 'diskontambahKet')" type="button"
                                                value="..." />
                                            <div style="display: none;">
                                                <asp:TextBox ID="diskontambahKet" runat="server"></asp:TextBox>
                                            </div>
                                            <asp:Label ID="diskontambahPersenc" runat="server" CssClass="err" />
                                        </div>
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td valign="top">Bunga
                                    </td>
                                    <td valign="top">:
                                    </td>
                                    <td>
                                        <div id="persentingakat" runat="server">
                                            <input class="btn" onclick="popbunga('bunga2', 'bungaket')" type="hidden" value="..."
                                                id="btnBertingkat2" runat="server">
                                            <asp:TextBox ID="bunga2" runat="server" CssClass="txt" Width="50px" ReadOnly="true"
                                                MaxLength="100" AutoPostBack="true" OnTextChanged="bunga2_TextChanged" Text='0'>0</asp:TextBox>&nbsp;
                                        <div style="display: none">
                                            <asp:TextBox ID="bungaket" runat="server"></asp:TextBox>
                                        </div>
                                            <asp:TextBox ID="nilaiBunga" runat="server" CssClass="txt_num" ReadOnly="True" Text='0'>0</asp:TextBox>
                                            <asp:Label ID="bunga2c" runat="server" CssClass="err"></asp:Label>
                                        </div>
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td valign="top">Sumber Dana
                                    </td>
                                    <td valign="top">:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlSumberDana" runat="server" OnSelectedIndexChanged="ddlSumberDana_SelectedIndexChanged"
                                            AutoPostBack="true">
                                            <asp:ListItem Value="0">Gaji</asp:ListItem>
                                            <asp:ListItem Value="1">Warisan</asp:ListItem>
                                            <asp:ListItem Value="2">Pinjaman</asp:ListItem>
                                            <asp:ListItem Value="3">Usaha</asp:ListItem>
                                            <asp:ListItem Value="4">Lainnya</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr id="trLainnya" runat="server" visible="false">
                                    <td valign="top">&nbsp;
                                    </td>
                                    <td valign="top">&nbsp;
                                    </td>
                                    <td>
                                        <asp:TextBox ID="lainnya" runat="server" Width="400px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td valign="top">Tujuan Transaksi
                                    </td>
                                    <td valign="top">:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlTujuan" runat="server" AutoPostBack="true">
                                            <asp:ListItem Value="0">Investasi</asp:ListItem>
                                            <asp:ListItem Value="1">Dihuni</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td valign="top">PPN Ditanggung
                                    </td>
                                    <td valign="top">:
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="JenisPPN" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="False">PEMERINTAH</asp:ListItem>
                                            <asp:ListItem Selected="True">KONSUMEN</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:Label ID="JenisPPNc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">Cara Bayar
                                    </td>
                                    <td valign="top">:
                                    </td>
                                    <td>
                                        <table id="tablecarabayar" cellspacing="1" cellpadding="1" width="100%" border="0">
                                            <tr>
                                                <td>
                                                    <asp:RadioButtonList Enabled="false" ID="carabayar2" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Selected="True" Value="CASH KERAS">CASH KERAS</asp:ListItem>
                                                        <asp:ListItem Value="CASH BERTAHAP">CASH BERTAHAP</asp:ListItem>
                                                        <asp:ListItem Value="KPR">KPR</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td>
                                                    <asp:Label ID="carabayarc" runat="server" CssClass="err"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <br />
                                        <h3>
                                            <span style="width: 30px">2.</span>SALES PERSON</h3>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Kode Sales
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:DropDownList Enabled="false" ID="agent" runat="server" Width="400" CssClass="ddl"
                                            AutoPostBack="true" OnSelectedIndexChanged="GantiTipeSales">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr id="reff" runat="server" visible="false">
                                    <td>Refferator
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="agentreff" runat="server" Width="300" CssClass="ddl">
                                            <asp:ListItem>Refferator:</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="agentreffc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <br />
                                        <h3>
                                            <span>3.</span>Gimmick</h3>
                                    </td>
                                </tr>

                                <tr>
                                    <td>Type Gimmick
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="gimmick" runat="server" Width="100" CssClass="txt"></asp:TextBox>
                                        <input type="button" value="&#xf002;" style="font-family: 'fontawesome'" class="btn btn-orange" show-modal='#ModalPopUp' modal-title='Daftar Gimmick' modal-url='DaftarGimmick.aspx' id="btnpop"
                                            runat="server" name="btnpop">
                                        <asp:Label ID="gimmickc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>


                                <tr style="display: none">
                                    <td colspan="3">
                                        <br />
                                        <h3>
                                            <span style="display: none">4.</span>BOOKING FEE</h3>
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td>Rekening Bank
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlAcc" runat="server" CssClass="ddl" Width="300">
                                            <%--<asp:listitem selected="True">- Pilih Rekening Bank -</asp:listitem>--%>
                                        </asp:DropDownList>
                                        <asp:Label ID="ddlAccErr" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td>Nilai
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="carabayar" runat="server" CssClass="ddl">
                                            <asp:ListItem Value="TN">TN = Tunai</asp:ListItem>
                                            <asp:ListItem Value="KK">KK = Kartu Kredit</asp:ListItem>
                                            <asp:ListItem Value="KD">KD = Kartu Debit</asp:ListItem>
                                            <asp:ListItem Value="TR">TR = Transfer Bank</asp:ListItem>
                                            <asp:ListItem Value="BG">BG = Cek Giro</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="nilai" runat="server" CssClass="txt_num">0</asp:TextBox><asp:Label
                                            ID="nilaic" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td>Keterangan TTS
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="kettts" runat="server" Width="200" MaxLength="200" CssClass="txt"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td>Bank BG
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="bankbg" runat="server" Width="125" MaxLength="50" CssClass="txt"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td>No. BG
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="nobg" runat="server" Width="125" MaxLength="20" CssClass="txt"></asp:TextBox>&nbsp;
                                    Tgl :
                                    <asp:TextBox ID="tglbg" runat="server" Width="85" CssClass="txt_center"></asp:TextBox><input
                                        class="btn" onclick="openCalendar('tglbg')" type="button" value="...">
                                        <asp:Label ID="bgc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td>No. Kartu
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="nokk" runat="server" Width="125" CssClass="txt" MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td>Bank Kartu
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="bankkk" runat="server" Width="125" CssClass="txt" MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td colspan="3">
                                        <br>
                                        <h3>
                                            <span style="width: 30px">3.</span>KATEGORISASI</h3>
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td>Fitting Out
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="focounter" runat="server" Width="40" CssClass="txt_center">1</asp:TextBox>&nbsp;x&nbsp;Angsuran
                                    <asp:TextBox ID="fo" runat="server" CssClass="txt_num">0</asp:TextBox><asp:Label
                                        ID="foc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <div id="oto" runat="server">
                                <table>
                                    <tr>
                                        <td colspan="3">
                                            <b>Otorisasi Atasan</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <table>
                                                <tr>
                                                    <td>Username
                                                    </td>
                                                    <td>:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="username" runat="server" CssClass="txt" Width="120"></asp:TextBox>
                                                        &nbsp;
                                        <asp:Label ID="usernamec" runat="server" CssClass="err"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Password
                                                    </td>
                                                    <td>:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="password" TextMode="Password" runat="server" CssClass="txt" Width="120"></asp:TextBox>
                                                        &nbsp;
                                        <asp:Label ID="passc" runat="server" CssClass="err"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top">Ket</td>
                                                    <td>:</td>
                                                    <td>
                                                        <asp:TextBox ID="ket" runat="server" TextMode="MultiLine" Height="70px" />
                                                    </td>
                                                </tr>
                                                <%--<tr>
                                                    <td colspan="2">
                                                        <asp:Button ID="otorisasi" runat="server" CssClass="btn" Text="Otorisasi" Width="75"
                                                            OnClick="otorisasi_Click"></asp:Button>
                                                    </td>
                                                    <td>
                                                        <input id="cancel2" runat="server" class="btn" onclick="location.href = 'NUP.aspx'; return false;"
                                                            style="width: 75px" type="button" value="Cancel">
                                                    </td>
                                                </tr>--%>
                                                <td colspan="3">
                                                    <asp:Label ID="lblotorisasi" runat="server" CssClass="err"></asp:Label>
                                                </td>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <table height="50">
                                <tr>
                                    <td>
                                        <asp:Button ID="ok" runat="server" Width="75" CssClass="btn btn-blue" Text="OK" OnClick="ok_Click"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:Button ID="cancel" runat="server" Width="75" CssClass="btn btn-red" Text="Cancel" OnClick="cancel_Click"></asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </td>
                <td style="padding-right: 10px; padding-left: 10px; padding-bottom: 10px; padding-top: 70px">
                    <img src="/Media/line_vert.gif">
                </td>
                <td style="padding-top: 60px">
                    <table cellspacing="5">
                        <tr>
                            <td>No NUP :
                            <br>
                                <asp:Label ID="nonup" runat="server" Font-Bold="True" Font-Size="16"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Unit :
                            <br>
                                <asp:Label ID="nounit" runat="server" Font-Bold="True" Font-Size="16"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Tipe Unit :
                            <br>
                                <asp:Label ID="tipeunit" runat="server" Font-Bold="True" Font-Size="16"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Customer :
                            <br>
                                <asp:Label ID="customer" runat="server" Font-Bold="True" Font-Size="16"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

        <script language="javascript">
            function call(nomor) {
                document.getElementById('gimmick').value = nomor;
            }
            function call2(nomor, nounit) {
                document.getelementbyid('nostock').value = nomor;
                document.getelementbyid('unit').value = nounit;
            }
            function kalk(foo) {
                nomor = foo.options[foo.selectedIndex].value;
                if (nomor != 0) {
                    pl = document.getElementById('pl').value;
                    tgl = document.getElementById('tgl').value;

                    popSkema(nomor, pl, tgl);
                }
            }
            function popdiskon(d1, d2) {
                foo1 = document.getElementById(d1);
                foo2 = document.getElementById(d2);
                openModal('SkemaDiskon.aspx?t1=' + foo1.value + '&t2=' + foo2.value + '&d1=' + d1 + '&d2=' + d2, '450', '360');
            }

            function popbunga(d1, d2) {
                foo1 = document.getElementById(d1);
                foo2 = document.getElementById(d2);
                openModal('SkemaBunga.aspx?t1=' + foo1.value + '&t2=' + foo2.value + '&d1=' + d1 + '&d2=' + d2, '450', '360');
            }
            function recaldisc(discTxt) {
                disc = discTxt.value.split("+");

                discTxt.value = "";

                for (i = 0; i < disc.length; i++) {
                    if (!isNaN(disc[i]) && disc[i] != "") {
                        if (discTxt.value != "") discTxt.value = discTxt.value + "+";
                        discTxt.value = discTxt.value + disc[i];
                    }
                }
            }
            function cvtnum(foo) {
                return foo.replace(/,/gi, "");
            }
            function recal() {

            }
        </script>

    </form>
</body>
</html>
