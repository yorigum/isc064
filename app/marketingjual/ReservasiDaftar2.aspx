<%@ Reference Page="~/Skema.aspx" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.ReservasiDaftar2" CodeFile="ReservasiDaftar2.aspx.cs" %>

<!DOCTYPE html>
<html>
<head>
    <title>Pendaftaran Reservasi</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Reservasi - Pendaftaran Reservasi (Hal. 2)">
</head>
<body class="body-padding" onkeyup="if(document.getElementById('cancel')){if(event.keyCode==27)document.getElementById('cancel').click()}">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <input type="text" style="display: none">
        <h1 class="title title-line">Pendaftaran Reservasi</h1>
        <table cellpadding="0" cellspacing="0">
            <tr valign="top">
                <td width="450">
                    <div id="pilih" runat="server">
                        <p><b><i>Halaman 2 dari 4</i></b></p>
                        <br />
                        <table cellspacing="5">
                            <tr>
                                <td>
                                    <b>Customer</b>
                                </td>
                                <td>:
                                </td>
                                <td>
                                    <asp:TextBox ID="nocustomer" runat="server" Width="100" CssClass="txt"></asp:TextBox>
                                    <button class="btn btn-orange" id="btnpop" show-modal="#ModalPopUp" modal-title="Daftar Customer" type="button" value="..."
                                        name="btnpop" runat="server" style="height: 32px">
                                        <i class="fa fa-search"></i>
                                    </button>
                                    <input class="btn btn-blue" id="btnbaru" type="button" value="Baru" name="btnbaru" runat="server">
                                </td>
                                <td>&nbsp;&nbsp;
                                </td>
                                <td>
                                    <asp:LinkButton ID="next" runat="server" CssClass="btn btn-blue" OnClick="next_Click">
                                     Next <i class="fa fa-arrow-right"></i>
                                    </asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="frm" runat="server">
                        <p><b><i>Halaman 3 dari 4</i></b></p>
                        <br />
                        <table cellspacing="5">
                            <tr>
                                <td colspan="3">
                                    <p>
                                        <b>Waiting List</b>
                                    </p>
                                </td>
                            </tr>
                            <tr>
                                <td>No. Urut
                                </td>
                                <td>:
                                </td>
                                <td>
                                    <asp:TextBox ID="nourut" runat="server" Font-Bold="True" Width="75" CssClass="txt"
                                        Text="#AUTO#" ReadOnly="True"></asp:TextBox>
                                    <div style="display: none">
                                        <asp:TextBox ID="nostock" runat="server"></asp:TextBox>
                                        <asp:TextBox runat="server" ID="nostock2"></asp:TextBox>
                                        <asp:TextBox ID="noreservasifull" runat="server"></asp:TextBox>
                                    </div>
                                    &nbsp;&nbsp; Tanggal :
                                <asp:TextBox ID="tgl" runat="server" Width="85" CssClass="txt_center"></asp:TextBox>
                                    <label for="tgl" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                    <asp:Label ID="tglc" runat="server" CssClass="err"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Lokasi
                                </td>
                                <td>:</td>
                                <td>
                                    <asp:DropDownList ID="lokasi" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>Batas Waktu
                                </td>
                                <td>:
                                </td>
                                <td>
                                    <asp:TextBox ID="batas" runat="server" Width="150" CssClass="txt_center"></asp:TextBox>
                                    <asp:Label ID="batasc" runat="server" CssClass="err"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Keterangan</td>
                                <td>:</td>
                                <td>
                                    <asp:TextBox ID="alasan1" runat="server" Width="300" CssClass="txt_center" /></td>
                            </tr>
                            <tr style="display: none;">
                                <td>NUP
                                </td>
                                <td>:
                                </td>
                                <td>
                                    <asp:TextBox ID="noqueue" runat="server" Width="50" CssClass="txt_center">0</asp:TextBox>
                                    <asp:Label ID="noqueuec" runat="server" CssClass="err"></asp:Label>
                                    <i style="font-size: 7.5pt">Prioritas pemesanan atau sistem queueing</i>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <br />
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
                                            <td>
                                                <asp:TextBox ID="luasbangunan" runat="server" ReadOnly="true" Width="50" CssClass="txt_center"></asp:TextBox>
                                                m<sup>2</sup></td>
                                            <td>&nbsp; &nbsp; &nbsp;</td>
                                            <td>Tanah</td>
                                            <td>
                                                <asp:TextBox ID="luastanah" runat="server" ReadOnly="true" Width="50" CssClass="txt_center"></asp:TextBox>
                                                m<sup>2</sup></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>Price List
                                </td>
                                <td>:
                                </td>
                                <td>
                                    <asp:TextBox ID="pl" runat="server" CssClass="txt_num" ReadOnly="true">0</asp:TextBox>
                                    rupiah
                                <asp:Label ID="plc" runat="server" CssClass="err"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">Cara Bayar
                                <br />
                                    <br />
                                    <p style="font-weight: normal; font-size: 8pt; line-height: normal; font-style: normal; font-variant: normal">
                                        Double-click untuk<br />
                                        membuka kalkulator<br />
                                        cara bayar
                                    </p>
                                </td>
                                <td valign="top">:
                                </td>
                                <td>
                                    <asp:ListBox ID="skema" runat="server" Width="300" Rows="8" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="skema_SelectedIndexChanged"></asp:ListBox>
                                    <asp:Label ID="skemac" runat="server" CssClass="err"></asp:Label>
                                </td>
                            </tr>
                            <tr style="display: none;">
                                <td>Sifat PPN
                                </td>
                                <td>:
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="sifatppn" runat="server" AutoPostBack="True" RepeatDirection="Horizontal">
                                        <asp:ListItem class="igroup-radio">Tanpa PPN</asp:ListItem>
                                        <asp:ListItem class="igroup-radio" Selected="True">Dengan PPN</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">Diskon Harga Jual
                                </td>
                                <td valign="top">:
                                </td>
                                <td>
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
                            <tr>
                                <td style="vertical-align: top">Cara Bayar
                                </td>
                                <td style="vertical-align: top">:
                                </td>
                                <td>
                                    <table id="tablecarabayar" cellspacing="1" cellpadding="1" width="100%" border="0">
                                        <tr>
                                            <td>
                                                <asp:RadioButtonList ID="carabayar2" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="CASH KERAS">CASH KERAS</asp:ListItem>
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
                                    <p>
                                        <b>Reservasi</b>
                                    </p>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:RadioButtonList ID="rbCaraBayar" runat="server" RepeatColumns="3" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="TN">TN = Tunai</asp:ListItem>
                                        <asp:ListItem Value="KK">KK = Kartu Kredit</asp:ListItem>
                                        <asp:ListItem Value="KD">KD = Kartu Debit</asp:ListItem>
                                        <asp:ListItem Value="TR">TR = Transfer Bank</asp:ListItem>
                                        <asp:ListItem Value="BG">BG = Cek Giro</asp:ListItem>
                                        <asp:ListItem Value="MB">MB = Merchant Banking</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:Label ID="cb" runat="server" CssClass="err"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Rekening Bank
                                </td>
                                <td>:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlAcc" runat="server" Width="300">
                                        <asp:ListItem Selected="True">- Pilih Rekening Bank -</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Label ID="ddlAccErr" runat="server" CssClass="err" Width="250"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Keterangan TTS
                                </td>
                                <td>:
                                </td>
                                <td>
                                    <asp:TextBox ID="ketttr" runat="server" CssClass="txt" MaxLength="200" Width="200"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>No. BG
                                </td>
                                <td>:
                                </td>
                                <td>
                                    <asp:TextBox ID="nobg" runat="server" Width="125" CssClass="txt" MaxLength="20"></asp:TextBox>
                                    &nbsp; Tgl :
                                <asp:TextBox ID="tglbg" runat="server" CssClass="txt_center" Width="85"></asp:TextBox>
                                    <label for="tglbg" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                    <asp:Label ID="bgc" runat="server" CssClass="err"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Nilai Pengikatan
                                </td>
                                <td>:
                                </td>
                                <td>
                                    <asp:TextBox ID="nilai" runat="server" CssClass="txt_num" ReadOnly="False" Text="0"></asp:TextBox>
                                    Estimasi
                                <asp:Label ID="nilaic" runat="server" CssClass="err"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">Sales
                                </td>
                                <td valign="top">:
                                </td>
                                <td>
                                    <asp:DropDownList ID="agent" runat="server" Width="300" AutoPostBack="true"
                                        OnSelectedIndexChanged="GantiTipeSales">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr style="display: none;">
                                <td>Refferator
                                </td>
                                <td>:
                                </td>
                                <td>
                                    <asp:TextBox ID="reffcust" runat="server" Width="300"></asp:TextBox>

                                </td>
                            </tr>
                            <tr style="display: none;">
                                <td>Bank Refferator
                                </td>
                                <td>:
                                </td>
                                <td>
                                    <asp:TextBox ID="bankreff" runat="server" Width="300"></asp:TextBox>

                                </td>
                            </tr>
                            <tr style="display: none;">
                                <td>No Rek. Refferator
                                </td>
                                <td>:
                                </td>
                                <td>
                                    <asp:TextBox ID="norekreff" runat="server" Width="300"></asp:TextBox>

                                </td>
                            </tr>
                            <tr style="display: none;">
                                <td>A/N Bank Reff
                                </td>
                                <td>:
                                </td>
                                <td>
                                    <asp:TextBox ID="anreff" runat="server" Width="300"></asp:TextBox>

                                </td>
                            </tr>

                            <tr style="display: none;">
                                <td>NPWP Refferator
                                </td>
                                <td>:
                                </td>
                                <td>
                                    <asp:TextBox ID="npwpreff" runat="server" Width="300"></asp:TextBox>

                                </td>
                            </tr>
                            <tr style="display: none;">
                                <td valign="top">Supervisor
                                </td>
                                <td valign="top">:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="supervisor" Width="300"></asp:TextBox>
                                    <asp:Label runat="server" ID="spv" CssClass="err"></asp:Label>
                                </td>
                            </tr>
                            <tr style="display: none;">
                                <td valign="top">Manager
                                </td>
                                <td valign="top">:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="manager" Width="300"></asp:TextBox>
                                    <asp:Label runat="server" ID="mgr" CssClass="err"></asp:Label>
                                </td>
                            </tr>
                            <tr id="reff" runat="server" visible="false">
                                <td>Refferator
                                </td>
                                <td>:
                                </td>
                                <td>
                                    <asp:DropDownList ID="agentreff" runat="server" Width="300">
                                        <asp:ListItem>Refferator:</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="agentreffc" runat="server" CssClass="err"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table height="50">
                            <tr>
                                <td>
                                    <asp:LinkButton ID="ok" runat="server" CssClass="btn btn-blue" Width="75" OnClick="ok_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
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
                            <br />
                                <asp:Label ID="nounit" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Customer :
                            <br />
                                <asp:Label ID="customer" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

        <script src="/Js/jquery.signalR-2.2.3.min.js"></script>
        <script src="signalr/hubs" type="text/javascript"></script>
        <script type="text/javascript">
            ////ini untuk booking saat closing
            $(function () {
                var userid = "<%=ISC064.Act.UserID %>";
                var nostock = $("#nostock").val();
                var test = $.connection.closingHub;
                $.connection.hub.qs = "UserID=" + userid + "&NoStock=" + nostock;

                test.client.broadcastMsg = function (user, nostock) {
                    console.log(user);
                    console.log(nostock);
                };

                $.connection.hub.start().done(function () {
                    test.server.hello(userid, nostock);
                });
            });
            function call(nomor) {
                document.getElementById('nocustomer').value = nomor;
                document.getElementById('next').click();
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
        </script>

    </form>
</body>
</html>
