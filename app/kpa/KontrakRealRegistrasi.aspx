<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KontrakRealRegistrasi.aspx.cs"
    Inherits="ISC064.KPA.KontrakRealRegistrasi" %>

<!DOCTYPE html>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Realisasi Tagihan KPR</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Tagihan KPR - Realisasi Tagihan">

    <script type="text/javascript">
        function keyup() {
            if (event.keyCode == 27 && confirm('Apakah anda ingin membatalkan proses registrasi?')) {
                if (document.getElementById('cancel'))
                    document.getElementById('cancel').click();
                else if (document.getElementById('cancel2'))
                    document.getElementById('cancel2').click();
            }
        }
        function tagihan(no, nilai, foo) {
            if (foo.checked)
                document.getElementById('lunas_' + no).value = nilai;
            else
                document.getElementById('lunas_' + no).value = "";

            hitunggt();
        }
        function hitunggt() {
            foogt = document.getElementById('total');
            grandtotal = 0 * 1;

            eof = false;
            i = 0 * 1;
            while (!eof) {
                foo = document.getElementById('lunas_' + i);
                if (!foo) {
                    eof = true;
                    break;
                }
                else {
                    total = cvtnum(foo.value);
                    if (!isNaN(total))
                        grandtotal = grandtotal + (total * 1);
                    i++;
                }
            }

            finalnet = Math.round(100 * grandtotal) / 100;
            eval("foogt.value = FinalFormat('" + finalnet + "')");
        }
        function cvtnum(foo) {
            return foo.replace(/,/gi, "");
        }
        function call(nomor, tglcekgiro) {
            document.getElementById('nobg').value = nomor;
            document.getElementById('tglbg').value = tglcekgiro;
        }
        function hitungtotal() {
            var gt = parseFloat(document.getElementById('total').value.replace(/,/g, ""));
            var adm = parseFloat(document.getElementById('admBank').value.replace(/,/g, ""));
            var bulat = parseFloat(document.getElementById('lebihbayar').value.replace(/,/g, ""));
            var lebih = parseFloat(document.getElementById('lb').value.replace(/,/g, ""));

            document.getElementById('total').value = gt + bulat + lebih;
            CalcBlur(document.getElementById('grandtotal'));
        }
    </script>

</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt" style="padding-left: 10px; padding-top: 10px;">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <input type="text" style="display: none">
        <h1 class="title title-line">Realisasi Tagihan KPR</h1>
        <p>
            Halaman 2 dari 2
        </p>
        <br />

        <p class="feed">
            <asp:Label ID="feed" runat="server"></asp:Label>
        </p>
        <div id="transaksi" runat="server">
            <table>
                <tr>
                    <td>Tanggal
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:TextBox ID="tgl" runat="server" CssClass="txt_center tgl" Width="75" Font-Size="8"></asp:TextBox>
                        <label for="tgl" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                        <asp:Label ID="tglc" runat="server" CssClass="err"></asp:Label>
                    </td>
                </tr>
                <tr style="display: none">
                    <td valign="top">Cara Bayar
                    </td>
                    <td valign="top">:
                    </td>
                    <td>
                        <asp:RadioButtonList ID="carabayar" runat="server" RepeatColumns="2" RepeatDirection="Vertical">
                            <asp:ListItem Value="TN">TN = Tunai</asp:ListItem>
                            <asp:ListItem Value="KK">KK = Kartu Kredit</asp:ListItem>
                            <asp:ListItem Value="KD">KD = Kartu Debit</asp:ListItem>
                            <asp:ListItem Value="TR" Selected="True">TR = Transfer Bank</asp:ListItem>
                            <asp:ListItem Value="BG">BG = Cek Giro</asp:ListItem>
                            <asp:ListItem Value="MB">MB = Merchant Banking</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>Rekening Bank
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlAcc" runat="server" CssClass="ddl" Width="300">
                            <asp:ListItem Selected="True">- Pilih Rekening Bank -</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="ddlAccErr" runat="server" CssClass="err"></asp:Label>
                    </td>
                </tr>
                <tr style="display: none">
                    <td colspan="3">
                        <br>
                        <p>
                            <b>Khusus Cek Giro</b>&nbsp;&nbsp;&nbsp;<i>(data akan otomatis masuk ke master cek giro)</i>
                        </p>
                    </td>
                </tr>
                <tr style="display: none">
                    <td>Bank BG
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:TextBox ID="bankbg" runat="server" Width="125" MaxLength="20" CssClass="txt"></asp:TextBox>
                    </td>
                </tr>
                <tr style="display: none">
                    <td>No. BG
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:TextBox ID="nobg" runat="server" Width="125" CssClass="input-text" MaxLength="20"></asp:TextBox>
                        <input type="button" value="..." class="btn" onclick="popDaftarBG()" id="btnpop"
                            runat="server" name="btnpop">
                        <asp:Label ID="nobgc" runat="server" CssClass="err"></asp:Label>
                    </td>
                </tr>
                <tr style="display: none">
                    <td>Tanggal BG
                    </td>
                    <td>:
                    </td>
                    <td>
                        <div class="input-group input-medium">
                            <asp:TextBox ID="tglbg" runat="server" type="text" CssClass="form-control" Style="width: 50%" Height="34"></asp:TextBox>
                            <span class="input-group-btn" style="height: 34px; display: block">
                                <button class="btn-a default" runat="server" onclick="openCalendar('tglbg');" type="button" style="height: 100%">
                                    <i class="fa fa-calendar"></i>
                                </button>
                            </span>
                        </div>
                        <asp:Label ID="tglbgc" runat="server" CssClass="err"></asp:Label>
                    </td>
                </tr>
                <tr style="display: none">
                    <td>&nbsp;
                    </td>
                </tr>
                <tr style="display: none">
                    <td colspan="3">
                        <p>
                            <b>Khusus Kartu Kredit</b>
                        </p>
                    </td>
                </tr>
                <tr style="display: none">
                    <td>No. Kartu
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:TextBox ID="nokk" runat="server" Width="125" CssClass="input-text" MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                <tr style="display: none">
                    <td>Bank
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:TextBox ID="bankkk" runat="server" Width="125" CssClass="input-text" MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                <tr style="display: none">
                    <td>&nbsp;
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">Sumber Dana
                    </td>
                    <td valign="top">:
                    </td>
                    <td>
                        <asp:RadioButtonList ID="sumberdana" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0">Dari Customer</asp:ListItem>
                            <asp:ListItem Value="1" Selected="True">Dari Bank</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:Label ID="sumberdanac" runat="server" CssClass="err"></asp:Label>
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">Keterangan
                    </td>
                    <td valign="top">:
                    </td>
                    <td>
                        <asp:TextBox ID="ket" runat="server" CssClass="input-text" Width="400" TextMode="MultiLine"
                            Height="70"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <br />
            <table class="tb blue-skin" cellspacing="1">
                <tr>
                    <th>No. Kontrak
                    </th>
                    <th>No. Tagihan
                    </th>
                    <th>Nama Tagihan
                    </th>
                    <th>Customer
                    </th>
                    <th>No. Unit
                    </th>
                    <th>Nilai Pengajuan
                    </th>
                    <th>Nilai Realisasi
                    </th>
                    <th></th>
                </tr>
                <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
                <tr>
                    <td colspan="6">Total
                    </td>
                    <td>
                        <asp:TextBox ID="total" runat="server" Width="150" CssClass="txt_num"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="7">
                        <asp:Label ID="err" runat="server" CssClass="err"></asp:Label>
                        <asp:LinkButton ID="ok" runat="server" CssClass="btn btn-blue" Width="75" OnClick="save_Click">
                        <i class="fa fa-share"></i> OK
                        </asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
