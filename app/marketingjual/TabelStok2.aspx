<%@ Reference Page="~/Customer.aspx" %>
<%@ Reference Page="~/Unit.aspx" %>
<%@ Reference Page="~/Skema.aspx" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.TabelStok2" CodeFile="TabelStok2.aspx.cs" %>

<!DOCTYPE html>
<html>
<head>
    <title>Reservasi Launching</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Unit - Reservasi Launching (Hal. 1)">
    <script src="/Js/Jquery.min.js"></script>
    <script src="/Js/jquery.signalR-2.2.3.min.js"></script>
    <script src="signalr/hubs" type="text/javascript"></script>
    <style type="text/css">
        H3 {
            FONT-SIZE: 9pt;
        }
    </style>
</head>
<body class="body-padding">
    <script type="text/javascript" src="/Js/NumberFormat.js">
    </script>
    <script type="text/javascript">
        function kaliLuas(gimmick, lainlain, pricelist) {
            var vargimmick = parseFloat(gimmick.value.replace(/,/g, ""));
            var varlainlain = parseFloat(lainlain.value.replace(/,/g, ""));
            var varpricelist = parseFloat(pricelist.value.replace(/,/g, ""));

            var result = vargimmick + varlainlain + varpricelist;

            hitungHarga(result);
        }

        function hitungHarga(x) {
            if (x == 0) { x = 0; }
            //alert(x);
            document.getElementById("totalharga").value = x;
            CalcBlur(document.getElementById("totalharga"));
        }
    </script>
    <form class="cnt" id="Form1" method="post" runat="server">
        <%--<uc1:Head ID="Head1" runat="server"></uc1:Head>--%>
        <h1 class="title title-line">Closing Langsung</h1>
        <p><b><i>Halaman 1 dari 2</i></b></p>
        <br>
        <div>
            <input class="btn btn-blue" id="ilus" type="button" value="Ilustrasi" name="ilus" runat="server">
            <input class="btn btn-blue" id="reserv" type="button" value="Reservasi" name="reserv" runat="server" visible="false">
            <input class="btn btn-blue" id="closing" type="button" value="Closing Langsung" name="closing" runat="server">
            <asp:TextBox ID="nostock" runat="server" Style="display: none"></asp:TextBox>
        </div>
        <br>
        <div id="dclosing" runat="server">
            <table cellpadding="2">
                <tr>
                    <td colspan="3">
                        <h3><span style="width: 30px">1.</span> DATA PEMESAN</h3>
                    </td>
                </tr>
                <tr>
                    <td>Nama Lengkap</td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID="nama" runat="server" Width="300" MaxLength="100" CssClass="txt"></asp:TextBox><asp:Label ID="namac" runat="server" CssClass="err"></asp:Label></td>
                </tr>
                <tr valign="top">
                    <td>Alamat Korespondensi</td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID="alamat1" runat="server" Width="250" MaxLength="50" CssClass="txt"></asp:TextBox>
                        <asp:Label ID="alamat1c" runat="server" CssClass="err"></asp:Label><br>
                        <asp:TextBox ID="alamat2" runat="server" Width="150" MaxLength="50" CssClass="txt"></asp:TextBox>
                        <asp:Label ID="alamat2c" runat="server" CssClass="err"></asp:Label><br>
                        <asp:TextBox ID="alamat3" runat="server" Width="150" MaxLength="50" CssClass="txt"></asp:TextBox>
                        <asp:Label ID="alamat3c" runat="server" CssClass="err"></asp:Label></td>
                </tr>
                <tr valign="top">
                    <td>Alamat KTP</td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID="ktp1" runat="server" Width="250" MaxLength="50" CssClass="txt"></asp:TextBox>
                        <asp:Label ID="ktp1c" runat="server" CssClass="err"></asp:Label><br>
                        <asp:TextBox ID="ktp2" runat="server" Width="150" MaxLength="50" CssClass="txt"></asp:TextBox>RT/RW
							<asp:Label ID="ktp2c" runat="server" CssClass="err"></asp:Label>
                        <br>
                        <asp:TextBox ID="ktp3" runat="server" Width="150" MaxLength="50" CssClass="txt"></asp:TextBox>KECAMATAN
							<asp:Label ID="ktp3c" runat="server" CssClass="err"></asp:Label>&nbsp;<br>
                        <asp:TextBox ID="ktp4" runat="server" Width="150" MaxLength="50" CssClass="txt"></asp:TextBox>KOTAMADYA
						    <asp:Label ID="ktp4c" runat="server" CssClass="err"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>No. KTP</td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID="noktp" runat="server" Width="200" MaxLength="50" CssClass="txt"></asp:TextBox>
                        <asp:Label ID="noktpc" runat="server" CssClass="err"></asp:Label></td>
                </tr>
                <tr>
                    <td>No. Telp / HP</td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID="telp" runat="server" MaxLength="50" CssClass="txt"></asp:TextBox>/
							<asp:TextBox ID="hp" runat="server" MaxLength="50" CssClass="txt"></asp:TextBox><asp:Label ID="telpc" runat="server" CssClass="err"></asp:Label></td>
                </tr>
                <tr>
                    <td>NPWP</td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID="npwp" runat="server" Width="200" MaxLength="50" CssClass="txt"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="3">
                        <br>
                        <h3><span style="width: 30px">2.</span> UNIT YANG DIPESAN</h3>
                    </td>
                </tr>
                <tr>
                    <td>Tanggal Kontrak</td>
                    <td>&nbsp;</td>
                    <td>
                        <asp:TextBox ID="tglKontrak" runat="server" Width="100px" CssClass="txt"
                            ReadOnly="False" Height="20px"></asp:TextBox>&nbsp;<label for="tglKontrak" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                        <asp:Label ID="tglkontrakc" runat="server" CssClass="err"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Kode Unit</td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID="unit" runat="server" Width="100" CssClass="txt" ReadOnly="True"></asp:TextBox>
                        <div style="display: none">
                            <asp:TextBox ID="nokontrak" runat="server"></asp:TextBox>
                        </div>
                        <input class="btn" id="btnpop" onclick="popDaftarUnit2('a')" type="button" value="..."
                            name="btnpop" runat="server">
                        <asp:Label ID="unitc" runat="server" CssClass="err"></asp:Label></td>
                </tr>
                <tr valign="top">
                    <td>Skema</td>
                    <td>:</td>
                    <td>
                        <asp:DropDownList ID="skema" runat="server" Width="400" CssClass="ddl" AutoPostBack="True" OnSelectedIndexChanged="skema_SelectedIndexChanged"></asp:DropDownList></td>
                </tr>
                <tr style="display: none" valign="top">
                    <td>Netto</td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID="netto" runat="server" CssClass="txt_num"></asp:TextBox><asp:Label ID="nettoc" runat="server" CssClass="err"></asp:Label></td>
                </tr>
                <tr valign="top">
                    <td>Price List</td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID="Pricelist" runat="server" CssClass="txt_num" ReadOnly="true"></asp:TextBox>
                        <asp:Label ID="pricec" runat="server" CssClass="err"></asp:Label></td>
                </tr>
                <tr valign="top">
                    <td>Gimmick</td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID="gimmick" runat="server" CssClass="txt_num"></asp:TextBox>
                        <asp:Label ID="gimmickc" runat="server" CssClass="err"></asp:Label></td>
                </tr>
                <tr valign="top">
                    <td>Harga Tambahan</td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID="hargatambahan" runat="server" CssClass="txt_num"></asp:TextBox>
                        + 
							<asp:Label ID="hargatambahanc" runat="server" CssClass="err"></asp:Label></td>
                </tr>
                <tr>
                    <td align="right" colspan="3">
                        <hr noshade size="1">
                    </td>
                    <td></td>
                </tr>
                <tr valign="top">
                    <td>Total Harga</td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID="totalharga" runat="server" CssClass="txt_num" ReadOnly="true">0</asp:TextBox>
                    </td>
                </tr>
                <!--
					<tr>
						<td valign="top">Diskon Harga Jual</td>
						<td valign="top">:</td>
						<td><asp:radiobuttonlist id="jenisdiskon" runat="server" autopostback="True" repeatdirection="Horizontal" onselectedindexchanged="jenisdiskon_SelectedIndexChanged">
								<asp:listitem selected="True">lum sum</asp:listitem>
								<asp:listitem>% bertingkat</asp:listitem>
							</asp:radiobuttonlist>
							<div id="lumsum" runat="server"><asp:textbox id="diskon" runat="server" cssclass="txt_num">0</asp:textbox><asp:label id="diskonc" runat="server" cssclass="err"></asp:label></div>
							<div id="persentingkat" runat="server"><asp:textbox id="diskon2" runat="server" width="150" maxlength="100" cssclass="txt" readonly="True"></asp:textbox><input class="btn" onclick="popdiskon('diskon2','diskonket')" type="button" value="...">
								<div style="DISPLAY: none"><asp:textbox id="diskonket" runat="server"></asp:textbox></div>
							</div>
						</td>
					</tr>
					-->
                <tr>
                    <td colspan="2">Bunga</td>
                    <td>
                        <div style="float: left">
                            <asp:TextBox ID="lsbunga" runat="server" CssClass="txt_num">0</asp:TextBox><asp:TextBox ID="persenBunga" runat="server" Width="50"></asp:TextBox>
                        </div>
                        <div style="float: left">
                            <asp:RadioButtonList ID="rdBunga" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdBunga_SelectedIndexChanged">
                                <asp:ListItem Selected="true">Rupiah</asp:ListItem>
                                <asp:ListItem>% (Persen)</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td valign="top">Diskon Harga Jual</td>
                    <td valign="top">:</td>
                    <td>
                        <asp:RadioButtonList ID="jenisdiskon2" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" OnSelectedIndexChanged="jenisdiskon_SelectedIndexChanged">
                            <asp:ListItem Selected="True">(Rp) Lump Sum</asp:ListItem>
                            <asp:ListItem>(%) Persen Bertingkat</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:Label ID="diskonbaruc" runat="server" CssClass="err"></asp:Label>
                        <div id="lumsum2" runat="server">
                            <asp:TextBox ID="diskonlamsam" runat="server" CssClass="txt_num">0</asp:TextBox>
                            <asp:Label ID="diskonlamsamc" runat="server" CssClass="err"></asp:Label>
                        </div>
                        <div id="persentingkat2" runat="server">
                            <asp:TextBox ID="diskon3" runat="server" Width="150" MaxLength="100" CssClass="txt">0</asp:TextBox>
                            <input class="btn" onclick="popdiskon('diskon3', 'diskonket2')" type="button" value="..." />
                            <div style="display: none">
                                <asp:TextBox ID="diskonket2" runat="server"></asp:TextBox>
                            </div>
                            (%) Persen &nbsp;&nbsp;
                                <asp:CheckBox ID="bulat" runat="server" Text="Pembulatan Nilai" Checked="true"></asp:CheckBox>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td valign="top">Diskon Tambahan</td>
                    <td valign="top">:</td>
                    <td>
                        <asp:TextBox ID="diskontambahan" runat="server" CssClass="txt_num">0</asp:TextBox>
                        <asp:Label ID="diskontambahanc" runat="server" CssClass="err"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Sifat PPN</td>
                    <td>:</td>
                    <td>
                        <asp:RadioButtonList ID="sifatppn" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" OnSelectedIndexChanged="sifatppn_SelectedIndexChanged">
                            <asp:ListItem Selected="False">Tanpa PPN</asp:ListItem>
                            <asp:ListItem Selected="True">Dengan PPN</asp:ListItem>
                        </asp:RadioButtonList></td>
                </tr>
                <tr id="trppn" runat="server">
                    <td colspan="2">&nbsp;</td>
                    <td>
                        <asp:CheckBox ID="includeppn" runat="server" Text="Nilai Transaksi adalah Include PPN" OnCheckedChanged="includeppn_CheckedChanged"></asp:CheckBox><br>
                        <asp:CheckBox ID="roundppn" runat="server" Checked="True" Text="Nilai PPN Dibulatkan"></asp:CheckBox></td>
                </tr>
                <tr>
                    <td colspan="3">
                        <br>
                        <h3><span style="width: 30px">3.</span>SALES PERSON</h3>
                    </td>
                </tr>
                <tr>
                    <td>Kode Sales</td>
                    <td>:</td>
                    <td>
                        <asp:DropDownList ID="agent" runat="server" Width="400" CssClass="ddl"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>
                        <asp:CheckBox ID="special" runat="server" Text="Special Event" Font-Bold="True" Font-Size="15"></asp:CheckBox></td>
                </tr>
                <tr style="display: none">
                    <td colspan="3">
                        <br>
                        <h3><span style="display: none">4.</span> BOOKING FEE</h3>
                    </td>
                </tr>
                <tr style="display: none">
                    <td>Nilai</td>
                    <td>:</td>
                    <td>
                        <asp:DropDownList ID="carabayar" runat="server" CssClass="ddl">
                            <asp:ListItem Value="TN">TN = Tunai</asp:ListItem>
                            <asp:ListItem Value="KK">KK = Kartu Kredit</asp:ListItem>
                            <asp:ListItem Value="KD">KD = Kartu Debit</asp:ListItem>
                            <asp:ListItem Value="TR">TR = Transfer Bank</asp:ListItem>
                            <asp:ListItem Value="BG">BG = Cek Giro</asp:ListItem>
                        </asp:DropDownList><asp:TextBox ID="nilai" runat="server" CssClass="txt_num">0</asp:TextBox><asp:Label ID="nilaic" runat="server" CssClass="err"></asp:Label></td>
                </tr>
                <tr style="display: none">
                    <td>Keterangan TTS</td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID="kettts" runat="server" Width="200" MaxLength="200" CssClass="txt"></asp:TextBox></td>
                </tr>
                <tr style="display: none">
                    <td>No. BG</td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID="nobg" runat="server" Width="125" MaxLength="20" CssClass="txt"></asp:TextBox>&nbsp; 
							Tgl :
							<asp:TextBox ID="tglbg" runat="server" Width="85" CssClass="txt_center"></asp:TextBox>
                        <label for="tglbg" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                        <asp:Label ID="bgc" runat="server" CssClass="err"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="3">
                        <br>
                        <h3><span style="width: 30px">4.</span> KATEGORISASI</h3>
                    </td>
                </tr>
                <tr>
                    <td valign="top">PPN Ditanggung</td>
                    <td valign="top">:</td>
                    <td>
                        <asp:RadioButtonList ID="JenisPPN" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="False">PEMERINTAH</asp:ListItem>
                            <asp:ListItem Selected="True">KONSUMEN</asp:ListItem>
                        </asp:RadioButtonList><asp:Label ID="JenisPPNc" runat="server" CssClass="err"></asp:Label></td>
                </tr>
                <tr style="display: none">
                    <td valign="top">Jenis KPR</td>
                    <td valign="top">:</td>
                    <td>
                        <asp:RadioButtonList ID="jeniskpr" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Value="0">KPR</asp:ListItem>
                            <asp:ListItem Value="1">NON-KPR</asp:ListItem>
                        </asp:RadioButtonList><asp:Label ID="jeniskprc" runat="server" CssClass="err"></asp:Label></td>
                </tr>
                <tr>
                    <td valign="top">Cara Bayar</td>
                    <td valign="top">:</td>
                    <td>
                        <table id="tablecarabayar" cellspacing="1" cellpadding="1" width="100%" border="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="carabayar2" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem>KPR</asp:ListItem>
                                        <asp:ListItem>CASH BERTAHAP</asp:ListItem>
                                        <asp:ListItem>CASH KERAS</asp:ListItem>
                                    </asp:RadioButtonList></td>
                                <td>
                                    <asp:Label ID="carabayarc" runat="server" CssClass="err"></asp:Label></td>
                            </tr>
                        </table>
                    </td>
                    <tr>
                        <td>Fitting Out</td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="focounter" runat="server" Width="40" CssClass="txt_center">1</asp:TextBox>&nbsp;x&nbsp;Angsuran
							<asp:TextBox ID="fo" runat="server" CssClass="txt_num">0</asp:TextBox><asp:Label ID="foc" runat="server" CssClass="err"></asp:Label></td>
                    </tr>
                <tr>
                    <td colspan="3">
                        <br>
                        <h3><span style="width: 30px">5.</span> Lain - Lain</h3>
                    </td>
                </tr>
                <tr>
                    <td valign="top">Note</td>
                    <td valign="top">:</td>
                    <td>
                        <asp:TextBox ID="note" runat="server" Width="350" Height="150" TextMode="MultiLine"></asp:TextBox></td>
                </tr>
            </table>
            <table height="50">
                <tr>
                    <td>
                        <asp:Button ID="ok" runat="server" Width="75" CssClass="btn" Text="OK" OnClick="ok_Click"></asp:Button></td>
                </tr>
            </table>
        </div>
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
            ///
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
