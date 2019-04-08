<%@ Reference Page="~/Skema.aspx" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.KontrakDaftar2" CodeFile="KontrakDaftar2.aspx.cs" %>

<!DOCTYPE html>
<html>
<head>
    <title>Pendaftaran Surat Pesanan</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Kontrak - Pendaftaran Surat Pesanan (Hal. 2)">
</head>
<body class="body-padding" onkeyup="if(document.getElementById('cancel')){if(event.keyCode==27)document.getElementById('cancel').click()}">

    <script type="text/javascript" src="/Js/NumberFormat.js"></script>

    <form class="cnt" id="Form1" method="post" runat="server">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <input style="display: none" type="text">
        <div id="nolanjut" runat="server">
            <h1>Pendaftaran Surat Pesanan Tidak Dapat Dilanjutkan</h1>
            <br>
            <div class="plike">
                <h2>Kemungkinan Terjadi Karena:</h2>
                <ul>
                    <li>
                    Unit tidak terdaftar atau sudah dihapus.
                    <li>
                    Unit sudah dibatalkan.
                        <li>Price list belum di-set. </li>
                </ul>
            </div>
        </div>
        <div id="lanjut" runat="server">
            <table cellspacing="0" cellpadding="0">
                <tr valign="top">
                    <td width="750">
                        <div id="pilih" runat="server">
                            <h1 class="title title-line">Pendaftaran Surat Pesanan</h1>
                            <p><b><i>Halaman 2 dari 4</i></b></p>
                            <h2 style="padding-left: 3px">Reservasi</h2>
                            <p style="padding-left: 3px; font-weight: normal; font-size: 8pt; line-height: normal; font-style: normal; font-variant: normal">
                                Daftar diurutkan berdasarkan nomor urut.
                            </p>
                            <asp:Table ID="rpt" runat="server" CellSpacing="0" CssClass="tb blue-skin">
                                <asp:TableRow HorizontalAlign="Left">
                                    <asp:TableHeaderCell>No. Urut</asp:TableHeaderCell>
                                    <asp:TableHeaderCell>No. Reservasi</asp:TableHeaderCell>
                                    <asp:TableHeaderCell>Tgl. Reservasi</asp:TableHeaderCell>
                                    <asp:TableHeaderCell>Customer / Sales</asp:TableHeaderCell>
                                    <asp:TableHeaderCell>Batas Waktu</asp:TableHeaderCell>
                                </asp:TableRow>
                            </asp:Table>
                            <table height="50">
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="next" runat="server" CssClass="btn btn-blue" OnClick="next_Click">
                                        Next <i class="fa fa-arrow-right"></i>
                                        </asp:LinkButton>
                                    </td>
                                    <td>
                                        <p style="padding-right: 3px; padding-left: 3px; font-size: 8pt; padding-bottom: 3px; padding-top: 3px">
                                            <asp:Label ID="expireinfo" runat="server">Reservasi yang bisa menjadi surat pesanan adalah <u>
														nomor urut satu</u>.</asp:Label>
                                        </p>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="frm" runat="server">
                            <asp:TextBox ID="noreservasi" runat="server"></asp:TextBox>
                            <h1 class="title title-line">Pendaftaran Surat Pesanan</h1>
                            <p><b><i>Halaman 3 dari 4</i></b></p>
                            <br>
                            <table cellspacing="5">
                                <tr>
                                    <td colspan="3">
                                        <p>
                                            <b>Dokumen</b>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td>No. Kontrak
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="nokontrak" runat="server" CssClass="txt" Text="#AUTO#" Font-Bold="True"
                                            ReadOnly="True" Width="65"></asp:TextBox>&nbsp;&nbsp; Tanggal :
                                    <nobr>
										<asp:textbox id="tglkontrak" runat="server" cssclass="txt_center" width="85"></asp:textbox>
                                        <label for="tglkontrak" class="btn btn-cal"><i class="fa fa-calendar"></i></label>                                            
									</nobr>
                                        <asp:Label ID="tglkontrakc" runat="server" CssClass="err"></asp:Label>
                                        <div style="display: none;">
                                            <asp:TextBox ID="nokontrakmanual" runat="server"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Lokasi
                                    </td>
                                    <td>:</td>
                                    <td>
                                        <asp:DropDownList ID="lokasi" runat="server" Enabled="false"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Jadwal Serah Terima
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="targetst" runat="server" CssClass="txt_center" Width="85"></asp:TextBox>
                                        <label for="targetst" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                        <asp:Label ID="targetstc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <br>
                                        <p>
                                            <b>Perhitungan Harga</b>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Luas
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>Bangunan</td>
                                                <td><asp:TextBox ID="luasbangunan" runat="server" ReadOnly="true" Width="50" CssClass="txt_center"></asp:TextBox> m<sup>2</sup></td>
                                                <td>&nbsp; &nbsp; &nbsp;</td>
                                                <td>Tanah</td>
                                                <td><asp:TextBox ID="luastanah" runat="server" ReadOnly="true" Width="50" CssClass="txt_center"></asp:TextBox> m<sup>2</sup></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr style="vertical-align: top" id="pldefault" runat="server">
                                    <td>Price List Berdasarkan
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="pldef" AutoPostBack="true" OnSelectedIndexChanged="Pricelist_SelectedIndexChanged">
                                            <asp:ListItem Value="0">-- Pilih Salah Satu</asp:ListItem>
                                            <asp:ListItem Value="1">Pricelist Rumah</asp:ListItem>
                                            <asp:ListItem Value="2">Pricelist Kavling</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="defaultc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Price List
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="pl" runat="server" CssClass="txt" ReadOnly="true"></asp:TextBox>rupiah
                                    <asp:Label ID="pricec" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td>Cara Bayar
                                    <br>
                                        <br>
                                        <p style="font-weight: normal; font-size: 8pt; line-height: normal; font-style: normal; font-variant: normal">
                                            Double-click untuk<br>
                                            membuka kalkulator<br>
                                            cara bayar
                                        </p>
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:ListBox ID="carabayar" runat="server" CssClass="ddl" Width="300" Rows="8" AutoPostBack="True"
                                            OnSelectedIndexChanged="skema_SelectedIndexChanged"></asp:ListBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Sifat PPN
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="sifatppn" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
                                            OnSelectedIndexChanged="sifatppn_SelectedIndexChanged">
                                            <asp:ListItem>Tanpa PPN</asp:ListItem>
                                            <asp:ListItem Selected="True">Dengan PPN</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr id="trppn" runat="server">
                                    <td colspan="2"></td>
                                    <td>
                                        <asp:CheckBox ID="roundppn" runat="server" Text="Nilai PPN Dibulatkan" Checked="True"></asp:CheckBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">PPN Ditanggung
                                    </td>
                                    <td valign="top">:
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="JenisPPN" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem>PEMERINTAH</asp:ListItem>
                                            <asp:ListItem Selected="True">KONSUMEN</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:Label ID="JenisPPNc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">Sumber Dana
                                    </td>
                                    <td valign="top">:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlSumberDana" runat="server" OnSelectedIndexChanged="ddlSumberDana_SelectedIndexChanged"
                                            AutoPostBack="true">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem Value="0">Dana Sendiri</asp:ListItem>
                                            <asp:ListItem Value="1">Pinjaman Bank</asp:ListItem>
                                            <asp:ListItem Value="2">Warisan/Hibah</asp:ListItem>
                                            <asp:ListItem Value="3">Lainnya</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="ddlSumberDanac" runat="server" CssClass="err"></asp:Label>
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
                                <tr>
                                    <td valign="top">Tujuan Pembelian
                                    </td>
                                    <td valign="top">:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlTujuan" runat="server" AutoPostBack="true">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem Value="0">Investasi</asp:ListItem>
                                            <asp:ListItem Value="1">Jual Kembali</asp:ListItem>
                                            <asp:ListItem Value="2">Dipakai Sendiri</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="ddlTujuanc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">Diskon Harga Jual
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
                                                id="btnBertingkat" runat="server" />
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
                                <tr>
                                    <td valign="top">Diskon Tambahan
                                    </td>
                                    <td valign="top">:
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="jenisDiskon" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
                                            OnSelectedIndexChanged="jenisDiskon_SelectedIndexChanged">
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
                                <tr>
                                    <td valign="top">Bunga
                                    </td>
                                    <td valign="top">:
                                    </td>
                                    <td>
                                        <div id="persentingakat" runat="server">
                                            <input class="btn" onclick="popbunga('bunga2', 'bungaket')" type="button" value="..."
                                                id="btnBertingkat2" runat="server">
                                            <asp:TextBox ID="bunga2" runat="server" CssClass="txt" Width="50px" MaxLength="100"
                                                AutoPostBack="true" OnTextChanged="bunga2_TextChanged" Text='0'>0</asp:TextBox>&nbsp;
                                        <div style="display: none">
                                            <asp:TextBox ID="bungaket" runat="server"></asp:TextBox>
                                        </div>
                                            <asp:TextBox ID="nilaiBunga" runat="server" CssClass="txt_num" ReadOnly="True" Text='0'>0</asp:TextBox>
                                            <asp:Label ID="bunga2c" runat="server" CssClass="err"></asp:Label>
                                        </div>
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td valign="top">Jenis KPR
                                    </td>
                                    <td valign="top">:
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="jeniskpr" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="True" Value="0">KPR</asp:ListItem>
                                            <asp:ListItem Value="1">NON-KPR</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:Label ID="jeniskprc" runat="server" CssClass="err"></asp:Label>
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
                                                <td style="width: 275px">
                                                    <asp:RadioButtonList ID="carabayar2" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem>KPR</asp:ListItem>
                                                        <asp:ListItem>CASH BERTAHAP</asp:ListItem>
                                                        <asp:ListItem>CASH KERAS</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td>
                                                    <asp:Label ID="carabayarc" runat="server" CssClass="err"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td>Fitting Out
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="focounter" runat="server" CssClass="txt_center" Width="40">1</asp:TextBox>&nbsp;x&nbsp;Angsuran
                                    <asp:TextBox ID="fo" runat="server" CssClass="txt_num">0</asp:TextBox><asp:Label
                                        ID="foc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr >
                                    <td valign="top">Note
                                    </td>
                                    <td valign="top">:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="note" runat="server" Width="350" Height="150" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td valign="top">Status Titip Jual
                                    </td>
                                    <td valign="top">:
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="titipjual" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="False" Value="1">Titip Jual</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="0">Non Titip Jual</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:Label ID="titipjualc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td valign="top">Status Paket Investasi
                                    </td>
                                    <td valign="top">:
                                    </td>
                                    <td valign="top">
                                        <asp:CheckBox ID="paketinvest" runat="server" Text="Merupakan paket investasi" />
                                        <br />
                                        Tgl Berakhir Paket Investasi :
                                    <asp:TextBox ID="tglinv" runat="server" Width="100px" CssClass="txt_center" ReadOnly="False"
                                        Height="20px"></asp:TextBox>&nbsp;
                                        <label for="tglinv" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                        <br />
                                        <asp:Label ID="tglinvc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <table height="50">
                                <tr>
                                    <td>
                                        <asp:LinkButton id="save" runat="server" cssclass="btn btn-blue" width="75" onclick="save_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
                                    </td>
                                    <td>
                                        <input class="btn btn-red" id="cancel" style="width: 75px" type="button" value="Cancel" name="cancel"
                                            runat="server">
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                    <td style="padding-right: 10px; padding-left: 10px; padding-bottom: 10px; padding-top: 70px">
                        <img src="/Media/line_vert.gif">
                    </td>
                    <td style="padding-top: 60px">
                        <table cellspacing="5">
                            <tr>
                                <td>Unit :
                                <br>
                                    <asp:Label ID="unit" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Customer :
                                <br>
                                    <asp:Label ID="customer" runat="server" Font-Bold="True">
											<br>
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Sales :
                                <br>
                                    <asp:Label ID="agent" runat="server" Font-Bold="True">
											<br>
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Skema :
                                <br>
                                    <asp:Label ID="skema" runat="server" Font-Bold="True">
											<br>
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Nilai Pengikatan :
                                <br>
                                    <asp:Label ID="nettorsv" runat="server" Font-Bold="True">
											<br>
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" id="trNoTTR">
                                <td>
                                    Nomor TTR :
                                    <br>
                                    <asp:Label ID="infoNoTTR" runat="server" Font-Bold="True">
											    <br>
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" id="trNilaiTTR">
                                <td>
                                    Nilai TTR :
                                    <br>
                                    Rp.&nbsp;<asp:Label ID="infoNilaiTTR" runat="server" Font-Bold="True">
											    <br>
                                    </asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>

        <script type="text/javascript">
            function kalk(foo) {
                nomor = foo.options[foo.selectedIndex].value;
                if (nomor != 0) {
                    pl = document.getElementById('pl').value;
                    tgl = document.getElementById('tglkontrak').value;

                    popSkema(nomor, pl, tgl);
                }
            }
            function call(nomor, nounit) {
                document.getElementById('nostock').value = nomor;
                document.getElementById('unit').value = nounit;
            }
            function callSource(nomor, source) {
                document.getElementById('spgabungan').value = nomor;
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
