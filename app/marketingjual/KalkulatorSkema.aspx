<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.KalkulatorSkema" CodeFile="KalkulatorSkema.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Kalkulator Skema</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Kalkulator Skema">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Kalkulator Skema Cara Bayar</h1>
        <br>
        <table cellspacing="0" cellpadding="0" id="pilih" runat="server">
            <tr valign="top">
                <td>
                    <table cellspacing="5">
                        <tr valign="top">
                            <td>Cara Bayar :
                            <br>
                                <asp:ListBox ID="carabayar" runat="server" CssClass="ddl" Rows="12" Width="300" OnSelectedIndexChanged="carabayar_SelectedIndexChanged"
                                    AutoPostBack="true"></asp:ListBox><asp:Label ID="carabayarc" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Tanggal Kontrak:
                                <asp:TextBox ID="tglkontrak" runat="server" CssClass="txt_center igroup" Width="85"></asp:TextBox>
                                <input type="button" value="&#xf073;" style="font-family: 'fontawesome'" class="btn">
                                <asp:Label ID="tglkontrakc" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr style="display:none;">
                            <td>
                                <asp:Label ID="inclppn" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="20"></td>
                <td>
                    <table cellspacing="5" id="tb1" runat="server">
                        <tr>
                            <td>Project</td>
                            <td>:</td>
                            <td><asp:DropDownList runat="server" ID="project" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>No. Stock
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="nostock" Font-Bold="true"></asp:TextBox>
							    <input type="button" value="&#xf002;" style="font-family: 'fontawesome'" class="btn btn-orange" show-modal='#ModalPopUp' modal-title='Daftar Unit' id="btnpop" runat="server" name="btnpop">
                                <%--<input id="btnpop" onclick="popDaftarUnit4('a')" value="&#xf002;" style="font-family: 'fontawesome'" class="btn btn-orange" type="button" name="btnpop" runat="server">--%>
                            </td>
                        </tr>
                        <tr>
                            <td>No. Unit
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="nounit" Font-Bold></asp:TextBox>

                            </td>
                        </tr>
                        <tr>
                        <td>
                        Metode
                        </td>
                        <td>:</td>
                        <td colspan="2">
                            <asp:RadioButtonList ID="metode" runat="server" RepeatDirection="Horizontal" CssClass="igroup-radio" autopostback="True" onselectedindexchanged="metode_SelectedIndexChanged">
                                <asp:ListItem Value="0" Selected="True">Skema Cara Bayar</asp:ListItem>
                                <asp:ListItem Value="1">Customize</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                        <tr>
                            <td>Price List
                            </td>
                            <td>:
                            </td>
                            <td align="right">
                                <asp:TextBox ID="gross" runat="server" CssClass="txt_num" Width="125" Enabled="false"></asp:TextBox>
                            </td>
                            <td style="font: 8pt; padding-left: 10">rupiah
                            </td>
                        </tr>
                        <tr>
                            <td>Present Value
                            </td>
                            <td>:
                            </td>
                            <td align="right">
                                <asp:TextBox ID="persenbunga" runat="server" CssClass="txt_num" Width="30px" Enabled="false"></asp:TextBox>
                                &nbsp;
                            <asp:TextBox ID="nilaiBunga" runat="server" CssClass="txt_num" Width="125px" Enabled="false"></asp:TextBox>
                            </td>
                            <td style="font: 8pt; padding-left: 10">%
                            </td>
                        </tr>
                        <tr>
                            <td>Discount
                            </td>
                            <td>:
                            </td>
                            <td align="right">
                                <asp:TextBox ID="disc" runat="server" CssClass="txt_num" Width="30px" Enabled="false"></asp:TextBox>
                                &nbsp;
                            <asp:TextBox ID="nilaiDiskon" runat="server" CssClass="txt_num" Width="125px" Enabled="false"></asp:TextBox>
                            </td>
                            <td style="font: 8pt; padding-left: 10">%
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td>Additional Discount
                            </td>
                            <td>:
                            </td>
                            <td align="right">
                                <asp:TextBox ID="diskontambahan" runat="server" CssClass="txt_num" Width="125px"
                                    Text="0"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td colspan="3" align="right">
                                <hr size="1" noshade>
                            </td>
                            <td style="font: bold">-
                            </td>
                        </tr>
                        <tr>
                            <td>DPP
                            </td>
                            <td>:
                            </td>
                            <td align="right">
                                <asp:TextBox ID="dpp" runat="server" CssClass="txt_num" Width="125" Enabled="false"></asp:TextBox>
                            </td>
                            <td style="font: 8pt; padding-left: 10">&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>PPN
                            </td>
                            <td>:
                            </td>
                            <td align="right">
                                <asp:TextBox ID="ppn" runat="server" CssClass="txt_num" Width="125" Enabled="false"></asp:TextBox>
                            </td>
                            <td style="font: 8pt; padding-left: 10">&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>Nilai Kontrak
                            </td>
                            <td>:
                            </td>
                            <td align="right">
                                <asp:TextBox ID="nilai" CssClass="txt_num" Width="125" Enabled="false" runat="server" TabIndex="99"></asp:TextBox>
                            </td>
                            <td style="font: 8pt; padding-left: 10">rupiah
                            <asp:Label ID="nilaic" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>

                    </table>
                    <div id="tb2" runat="server">
                    <table cellspacing="5">
                        <tr>
                            <td colspan="8">
                                <p><b>Rumus Nilai</b></p>
                            </td>
                        </tr>
                        <tr>
                            <td>Booking Fee</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="bfkali" runat="server" Width="40" CssClass="txt_center"></asp:TextBox>&nbsp;x&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:RadioButton ID="bfrupiah" runat="server" Text="Rp" GroupName="bftipe"></asp:RadioButton></td>
                            <td>
                                <asp:RadioButton ID="bfpersen" runat="server" Text="%" GroupName="bftipe"></asp:RadioButton></td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:TextBox ID="bfjumlah" runat="server" CssClass="txt_num"></asp:TextBox>
                                <asp:Label ID="bfc" runat="server" CssClass="err"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>DP</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="dpkali" runat="server" Width="40" CssClass="txt_center"></asp:TextBox>&nbsp;x&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:RadioButton ID="dprupiah" runat="server" Text="Rp" GroupName="dptipe"></asp:RadioButton></td>
                            <td>
                                <asp:RadioButton ID="dppersen" runat="server" Text="%" GroupName="dptipe"></asp:RadioButton></td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:TextBox ID="dpjumlah" runat="server" CssClass="txt_num"></asp:TextBox>
                                <asp:Label ID="dpc" runat="server" CssClass="err"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>Angsuran</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="angkali" runat="server" Width="40" CssClass="txt_center"></asp:TextBox>&nbsp;x&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:RadioButton ID="angrupiah" runat="server" Text="Rp" GroupName="angtipe"></asp:RadioButton></td>
                            <td>
                                <asp:RadioButton ID="angpersen" runat="server" Text="%" GroupName="angtipe"></asp:RadioButton></td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:TextBox ID="angjumlah" runat="server" CssClass="txt_num"></asp:TextBox>
                                <asp:Label ID="angc" runat="server" CssClass="err"></asp:Label></td>
                        </tr>
                    </table>
                    <br>
                    <table cellspacing="5">
                        <tr>
                            <td colspan="9">
                                <p><b>Rumus Jadwal</b></p>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">&nbsp;</td>
                            <td colspan="3"><u>Interval</u></td>
                            <td>&nbsp;</td>
                            <td colspan="3"><u>Pertama</u></td>
                        </tr>
                        <tr>
                            <td>Booking Fee</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="bflama1" runat="server" Width="40" CssClass="txt_center">7</asp:TextBox></td>
                            <td>
                                <asp:RadioButton ID="bfbln1" runat="server" Text="Bln" GroupName="bf1"></asp:RadioButton></td>
                            <td>
                                <asp:RadioButton ID="bfhari1" runat="server" Text="Hari" GroupName="bf1" Checked="True"></asp:RadioButton></td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:TextBox ID="bflama2" runat="server" Width="40" CssClass="txt_center">0</asp:TextBox></td>
                            <td>
                                <asp:RadioButton ID="bfbln2" runat="server" Text="Bln" GroupName="bf2"></asp:RadioButton></td>
                            <td>
                                <asp:RadioButton ID="bfhari2" runat="server" Text="Hari" GroupName="bf2" Checked="True"></asp:RadioButton>
                                <asp:Label ID="bf2c" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>DP</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="dplama1" runat="server" Width="40" CssClass="txt_center">1</asp:TextBox></td>
                            <td>
                                <asp:RadioButton ID="dpbln1" runat="server" Text="Bln" GroupName="dp1" Checked="True"></asp:RadioButton></td>
                            <td>
                                <asp:RadioButton ID="dphari1" runat="server" Text="Hari" GroupName="dp1"></asp:RadioButton></td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:TextBox ID="dplama2" runat="server" Width="40" CssClass="txt_center">1</asp:TextBox></td>
                            <td>
                                <asp:RadioButton ID="dpbln2" runat="server" Text="Bln" GroupName="dp2" Checked="True"></asp:RadioButton></td>
                            <td>
                                <asp:RadioButton ID="dphari2" runat="server" Text="Hari" GroupName="dp2"></asp:RadioButton>
                                <asp:Label ID="dp2c" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Angsuran</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="anglama1" runat="server" Width="40" CssClass="txt_center">1</asp:TextBox></td>
                            <td>
                                <asp:RadioButton ID="angbln1" runat="server" Text="Bln" GroupName="ang1" Checked="True"></asp:RadioButton></td>
                            <td>
                                <asp:RadioButton ID="anghari1" runat="server" Text="Hari" GroupName="ang1"></asp:RadioButton></td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:TextBox ID="anglama2" runat="server" Width="40" CssClass="txt_center">1</asp:TextBox></td>
                            <td>
                                <asp:RadioButton ID="angbln2" runat="server" Text="Bln" GroupName="ang2" Checked="True"></asp:RadioButton></td>
                            <td>
                                <asp:RadioButton ID="anghari2" runat="server" Text="Hari" GroupName="ang2"></asp:RadioButton>
                                <asp:Label ID="ang2c" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <br>
                    <table cellspacing="5">
                        <tr>
                            <td valign="top">Booking Fee dipotong di :
									<br>
                                <asp:RadioButton ID="dp1potong" runat="server" GroupName="bfpotong" Text="DP 1" Checked="True"></asp:RadioButton>
                                &nbsp;&nbsp;
									<asp:RadioButton ID="ang1potong" runat="server" GroupName="bfpotong" Text="Angsuran 1"></asp:RadioButton>
                                &nbsp;&nbsp;
									<asp:RadioButton ID="dpspotong" runat="server" GroupName="bfpotong" Text="DP disebar"></asp:RadioButton>
                                &nbsp;&nbsp;
									<asp:RadioButton ID="angspotong" runat="server" GroupName="bfpotong" Text="Angsuran disebar"></asp:RadioButton>
                                &nbsp;&nbsp;
									<asp:RadioButton ID="bftdkdisebar" runat="server" GroupName="bfpotong" Text="BF Tidak disebar"></asp:RadioButton>
                                <asp:Label ID="cc" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <%--<tr>
                            <td colspan="3">
                                <asp:Button ID="hitung" runat="server" CssClass="btn" Text="Hitung" OnClick="hitung_Click"></asp:Button>
                                <asp:Button ID="back" runat="server" CssClass="btn" Text="Cancel" OnClick="back_Click" />
                            </td>
                        </tr>--%>
                    </table>
                    </div>
                    <table cellspacing="5">
                        <tr>
                            <td colspan="3">
                                <asp:Button ID="hitung" runat="server" CssClass="btn btn-blue" Text="Hitung" OnClick="hitung_Click"></asp:Button>
                                <asp:Button ID="back" runat="server" CssClass="btn" Text="Cancel" OnClick="back_Click" />
                                <asp:Button ID="pdf" runat="server" CssClass="btn btn-orange" Text="Download PDF" OnClick="pdf_Click" />
                            </td>
                        </tr>
                    </table>
                    <%--<table cellspacing="5" id="tb2" runat="server">
                        <tr>
                            <td>No. Stock
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="TextBox1" Font-Bold></asp:TextBox>
                                <input id="Button1" onclick="popDaftarUnit4('a')" value="&#xf002;" style="font-family: 'fontawesome'" class="btn btn-orange" type="button" name="btnpop" runat="server">
                            </td>
                        </tr>
                        <tr>
                            <td>No. Unit
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="TextBox2" Font-Bold></asp:TextBox>

                            </td>
                        </tr>
                        <tr>
                        <td>
                        Metode
                        </td>
                        <td>:</td>
                        <td colspan="2">
                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" autopostback="True" onselectedindexchanged="metode_SelectedIndexChanged">
                            <asp:ListItem Value="0" Selected="True">Skema Cara Bayar</asp:ListItem>
                            <asp:ListItem Value="1">Customize</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                        <tr>
                            <td>Price List
                            </td>
                            <td>:
                            </td>
                            <td align="right">
                                <asp:TextBox ID="TextBox3" runat="server" CssClass="txt_num" Width="125" Enabled="false"></asp:TextBox>
                            </td>
                            <td style="font: 8pt; padding-left: 10">rupiah
                            </td>
                        </tr>
                        <tr>
                            <td>Discount
                            </td>
                            <td>:
                            </td>
                            <td align="right">
                                <asp:TextBox ID="TextBox4" runat="server" CssClass="txt_num" Width="30px" Enabled="false"></asp:TextBox>
                                &nbsp;
                            <asp:TextBox ID="TextBox5" runat="server" CssClass="txt_num" Width="125px" Enabled="false"></asp:TextBox>
                            </td>
                            <td style="font: 8pt; padding-left: 10">%
                            </td>
                        </tr>
                        <tr>
                            <td>Present Value
                            </td>
                            <td>:
                            </td>
                            <td align="right">
                                <asp:TextBox ID="TextBox6" runat="server" CssClass="txt_num" Width="30px" Enabled="false"></asp:TextBox>
                                &nbsp;
                            <asp:TextBox ID="TextBox7" runat="server" CssClass="txt_num" Width="125px" Enabled="false"></asp:TextBox>
                            </td>
                            <td style="font: 8pt; padding-left: 10">%
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td>Additional Discount
                            </td>
                            <td>:
                            </td>
                            <td align="right">
                                <asp:TextBox ID="TextBox8" runat="server" CssClass="txt_num" Width="125px"
                                    Text="0"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td colspan="3" align="right">
                                <hr size="1" noshade>
                            </td>
                            <td style="font: bold">-
                            </td>
                        </tr>
                        <tr>
                            <td>DPP
                            </td>
                            <td>:
                            </td>
                            <td align="right">
                                <asp:TextBox ID="TextBox9" runat="server" CssClass="txt_num" Width="125" Enabled="false"></asp:TextBox>
                            </td>
                            <td style="font: 8pt; padding-left: 10">&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>PPN
                            </td>
                            <td>:
                            </td>
                            <td align="right">
                                <asp:TextBox ID="TextBox10" runat="server" CssClass="txt_num" Width="125" Enabled="false"></asp:TextBox>
                            </td>
                            <td style="font: 8pt; padding-left: 10">&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>Nilai Kontrak
                            </td>
                            <td>:
                            </td>
                            <td align="right">
                                <asp:TextBox ID="TextBox11" CssClass="txt_num" Width="125" Enabled="false" runat="server" TabIndex="99"></asp:TextBox>
                            </td>
                            <td style="font: 8pt; padding-left: 10">rupiah
                            <asp:Label ID="Label1" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        </table>--%>
                </td>
            </tr>
        </table>
        <div id="hasil" runat="server">
            <p style="padding: 5">
                <input type="button" value="Back..." class="btn btn-blue" name="btnback" 
                    onclick="javascript: history.back(-1)" style="width:75">
            </p>
            <asp:Table ID="rpt" runat="server" CssClass="tb" CellSpacing="5">
                <asp:TableRow HorizontalAlign="Left">
                    <asp:TableHeaderCell>No.</asp:TableHeaderCell>
                    <asp:TableHeaderCell Width="300">Keterangan</asp:TableHeaderCell>
                    <asp:TableHeaderCell Width="100">Jatuh Tempo</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Right" Width="120">Jumlah</asp:TableHeaderCell>
                </asp:TableRow>
            </asp:Table>
        </div>
        <div id="noskema" runat="server" style="padding: 5px; margin: 40px">
            <h2>Skema Belum Tersedia</h2>
            <p style="font: 8pt">
                Silakan menghubungi administrasi untuk melakukan setup skema cara bayar.
            </p>
        </div>

        <script language="javascript">
            function load() {

                Tgl = document.getElementById('tglkontrak').value;
                PriceList = cvtnum(document.getElementById('gross').value);
                Tambahan = cvtnum(document.getElementById('diskontambahan').value);

                foo = document.getElementById('carabayar');
                nounitk = document.getElementById('nostock');
                Nomor = foo.options[foo.selectedIndex].value;

                location.href = '?NoStock=' + nounitk;
            }
            function recaldisc() {
                var pl = parseFloat(document.getElementById('gross').value.replace(/,/g, ""));
                var bunga = parseFloat(document.getElementById('nilaiBunga').value.replace(/,/g, ""));

                nett = pl + bunga;
                disc = document.getElementById('disc').value.split("+");
                nilaidisc = 0;

                document.getElementById('disc').value = "";

                for (i = 0; i < disc.length; i++) {
                    if (!isNaN(disc[i]) && disc[i] != "") {
                        nilaidisc = Math.round(100 * nett * (disc[i] / 100) * 1) / 100;
                        nett = nett - nilaidisc;
                        if (document.getElementById('disc').value != "") document.getElementById('disc').value = document.getElementById('disc').value + "+";
                        document.getElementById('disc').value = document.getElementById('disc').value + disc[i];
                    }
                }

                n = nett;
                eval("document.getElementById('nilai').value = FinalFormat('" + n + "')");
                eval("document.getElementById('nilaiDiskon').value = FinalFormat('" + nilaidisc + "')");

                hitungtotal();
            }
            function recalbunga(baseTxt, bungaTxt, nettTxt, nilaibungaTxt) {
                base = cvtnum(baseTxt.value);
                nett = base;
                bunga = bungaTxt.value.split("+");
                nilaibunga = 0;

                bungaTxt.value = "";

                for (i = 0; i < bunga.length; i++) {
                    if (!isNaN(bunga[i]) && bunga[i] != "") {
                        nilaibunga = Math.round(100 * nett * (bunga[i] / 100) * 1) / 100;
                        nett = nett - nilaibunga;
                        if (bungaTxt.value != "") bungaTxt.value = bungaTxt.value + "+";
                        bungaTxt.value = bungaTxt.value + bunga[i];
                    }
                }

                n = nett;
                eval("nettTxt.value = FinalFormat('" + n + "')");
                eval("nilaibungaTxt.value = FinalFormat('" + nilaibunga + "')");

                //recaldisc();
                hitungtotal();
            }
            function hitungtotal() {
                var pl = parseFloat(document.getElementById('gross').value.replace(/,/g, ""));
                var diskon = parseFloat(document.getElementById('nilaiDiskon').value.replace(/,/g, ""));
                var bunga = parseFloat(document.getElementById('nilaiBunga').value.replace(/,/g, ""));
                
                if (document.getElementById('inclppn').innerText == "True") {
                    ndpp = Math.round((pl - diskon + bunga) / 1.1);
                    nppn = pl - diskon + bunga - ndpp;
                }
                else {
                    ndpp = Math.round(pl - diskon + bunga);
                    nppn = ndpp * 0.1;
                }

                document.getElementById('dpp').value = ndpp;
                document.getElementById('ppn').value = nppn;
                document.getElementById('nilai').value = ndpp + nppn;

                CalcBlur(document.getElementById('dpp'));
                CalcBlur(document.getElementById('ppn'));
                CalcBlur(document.getElementById('nilai'));                
            }
            function nohitung() {
                document.getElementById('hitung').disabled = true;
            }
            function okhitung() {
                document.getElementById('hitung').disabled = false;
            }
            function cvtnum(foo) {
                return foo.replace(/,/gi, "");
            }

            function call3(nostock, nounit, pricelist) {
                document.getElementById('nostock').value = nostock;
                document.getElementById('nounit').value = nounit;
                document.getElementById('gross').value = pricelist;

            }
        </script>

    </form>
</body>
</html>
