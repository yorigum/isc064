<%@ Reference Page="~/Customer.aspx" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.FINANCEAR.TTSRegistrasiMarketing" AutoEventWireup="true" CodeFile="TTSRegistrasiMarketing.aspx.cs" %>

<!DOCTYPE html>
<html>
<head>
    <title>Registrasi Tanda Terima Sementara</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Tanda Terima Sementara - Registrasi TTS (Marketing)">
    <style type="text/css">
        #nilaitr TD {
            PADDING-TOP: 20px;
        }
    </style>
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <input type="text" style="display: none">
        <h1 class="title title-line">Registrasi Tanda Terima Sementara</h1>
        <p><b><i>Halaman 2 dari 2</i></b></p>
        <br>
        <table cellspacing="5">
            <tr>
                <td>Tipe</td>
                <td>:</td>
                <td width="200">
                    <asp:Label ID="tipe" runat="server" Font-Bold="True"></asp:Label>
                </td>
                <td>Unit</td>
                <td>:</td>
                <td>
                    <asp:Label ID="unit" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Ref.</td>
                <td>:</td>
                <td>
                    <asp:Label ID="referensi" runat="server" Font-Bold="True"></asp:Label>
                </td>
                <td>Customer</td>
                <td>:</td>
                <td>
                    <asp:Label ID="customer" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
        </table>
        <hr size="1" noshade color="silver">
        <br>
        <asp:RadioButtonList ID="carabayar" runat="server" RepeatColumns="2" RepeatDirection="Vertical" AutoPostBack="true" OnSelectedIndexChanged="carabayar_SelectedIndexChanged">
            <asp:ListItem class="radio" Value="TN">TN = Tunai</asp:ListItem>
            <asp:ListItem class="radio" Value="KK">KK = Kartu Kredit</asp:ListItem>
            <asp:ListItem class="radio" Value="KD">KD = Kartu Debit</asp:ListItem>
            <asp:ListItem class="radio" Value="TR">TR = Transfer Bank</asp:ListItem>
            <asp:ListItem class="radio" Value="BG">BG = Cek Giro</asp:ListItem>
        </asp:RadioButtonList>
        <table cellspacing="5">
            <tr>
                <td><b>Tanggal</b></td>
                <td>
                    <div class="input-group input-medium" style="margin-top: 0px; margin-left: 0px;">
                        <asp:TextBox ID="tgl" runat="server" type="text" CssClass="form-control" Style="width: 65%; height: 20px"></asp:TextBox>
                        <span class="input-group-btn" style="height: 34px; display: block; width: 408px;">
                            <label for="tgl" class="btn-a default btn-cal"><i class="fa fa-calendar"></i></label>
                        </span>
                    </div>
                    <asp:Label ID="tglc" runat="server" CssClass="err"></asp:Label>
                </td>
            </tr>
            <tr>
                <td><b>Keterangan</b></td>
                <td>
                    <asp:TextBox ID="ket" runat="server" Width="400" MaxLength="200"></asp:TextBox>
                </td>
            </tr>
            <%--<tr>
                <td><b>Nama Perusahaan</b></td>
                <td>
                    <asp:DropDownList ID="ddlpt" runat="server" Width="300">
                        <asp:ListItem Selected="True">- Pilih Perusahaan -</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="ddlpterr" runat="server" CssClass="err"></asp:Label>
                </td>
            </tr>--%>
            <tr>
                <td><b>Rekening Bank</b></td>
                <td>
                    <asp:DropDownList ID="ddlAcc" runat="server" Width="300" AutoPostBack="true" OnSelectedIndexChanged="ddlAcc_SelectedIndexChanged">
                        <asp:ListItem Selected="True">- Pilih Rekening Bank -</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="ddlAccErr" runat="server" CssClass="err"></asp:Label>
                </td>
            </tr>
            <tr>
                <td><b>Transfer Anonim</b></td>
                <td>
                    <asp:DropDownList ID="anonim" runat="server" Width="300" AutoPostBack="true" OnSelectedIndexChanged="anonim_SelectedIndexChanged">
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="trgiro1" runat="server" visible="false">
                <td colspan="3">
                    <br>
                    <p>
                        <b>Khusus Cek Giro</b>&nbsp;&nbsp;&nbsp;<i>(data akan otomatis masuk ke master cek 
								giro)</i>
                    </p>
                </td>
            </tr>
            <tr id="trgiro2" runat="server" visible="false">
                <td><b>Bank BG</b></td>
                <td>
                    <asp:TextBox ID="bankbg" runat="server" Width="125" MaxLength="20"></asp:TextBox></td>
            </tr>
            <tr id="trgiro3" runat="server" visible="false">
                <td><b>No. BG</b></td>
                <td>
                    <div class="input-group input-medium" style="margin-top: 0px; margin-left: 0px;">
                        <asp:TextBox ID="nobg" runat="server" type="text" CssClass="form-control" Style="width: 65%; height: 20px"></asp:TextBox>
                        <span class="input-group-btn" style="height: 34px; display: block">
                            <button name="btnpop" class="btn btn-orange" runat="server" id="btnpop" onclick="popDaftarBG()" type="button" style="height: 100%">
                                <i class="fa fa-search"></i>
                            </button>
                        </span>
                    </div>
                    <asp:Label ID="nobgc" runat="server" CssClass="err"></asp:Label>
                </td>
            </tr>
            <tr id="trgiro4" runat="server" visible="false">
                <td><b>Tanggal BG</b></td>
                <td>
                    <div class="input-group input-medium" style="margin-top: 0px; margin-left: 0px;">
                        <asp:TextBox ID="tglbg" runat="server" type="text" CssClass="tgl form-control" Style="width: 65%; height: 20px"></asp:TextBox>
                        <span class="input-group-btn" style="height: 34px; display: block">
                            <label for="tglbg" class="btn-a default btn-cal"><i class="fa fa-calendar"></i></label>
                        </span>
                    </div>
                    <asp:Label ID="tglbgc" runat="server" CssClass="err"></asp:Label>
                </td>
            </tr>
            <tr id="trgiro5" runat="server" visible="false">
                <td><b>Tanggal Jatuh Tempo</b></td>
                <td>
                    <div class="input-group input-medium" style="margin-top: 0px; margin-left: 0px;">
                        <asp:TextBox ID="tgljtbg" runat="server" type="text" CssClass="tgl form-control" Style="width: 65%; height: 20px"></asp:TextBox>
                        <span class="input-group-btn" style="height: 34px; display: block">
                            <label for="tgljtbg" class="btn-a default btn-cal"><i class="fa fa-calendar"></i></label>
                        </span>
                    </div>
                    <asp:Label ID="tgljtbgc" runat="server" CssClass="err"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr id="trkredit1" runat="server" visible="false">
                <td colspan="3">
                    <p><b>Khusus Kartu Kredit</b></p>
                </td>
            </tr>
            <tr id="trkredit2" runat="server" visible="false">
                <td><b>No. Kartu</b></td>
                <td>
                    <asp:TextBox ID="nokk" runat="server" Width="125" MaxLength="50"></asp:TextBox>&nbsp;
                    <asp:Label ID="nokkc" runat="server" CssClass="err"></asp:Label>
                </td>
            </tr>
            <tr id="trkredit3" runat="server" visible="false">
                <td><b>Bank</b></td>
                <td>
                    <asp:TextBox ID="bankkk" runat="server" Width="125" MaxLength="50"></asp:TextBox>&nbsp;
                    <asp:Label ID="bankkkc" runat="server" CssClass="err"></asp:Label>
                </td>
            </tr>
            <tr id="trkredit4" runat="server" visible="false">
                <td>
                    <b>Biaya Admin Bank</b>
                </td>
                <td>
                    <asp:TextBox ID="biayaadmin" runat="server" Width="125" CssClass="txt_num">0</asp:TextBox>
                    <asp:RadioButton ID="adminpersen" runat="server" GroupName="tipebeban" CssClass="radio" Text="%" Value="1" Checked="true" />
                    <asp:RadioButton ID="adminls" runat="server" GroupName="tipebeban" CssClass="radio" Text="Lump Sum" Value="2" />
                </td>
            </tr>
            <tr id="trkredit5" runat="server" visible="false">
                <td>
                    <b>Biaya Admin Dibebankan Kepada</b>
                </td>
                <td>
                    <asp:RadioButtonList ID="bebanbiayaadmin" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem class="radio" Value="1" Selected="True">Customer</asp:ListItem>
                        <asp:ListItem class="radio" Value="2">Perusahaan</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td valign="top"><b>Sumber Dana</b></td>
                <td>
                    <asp:RadioButtonList ID="sumberdana" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem class="radio" Selected="True" Value="0">Dari Customer</asp:ListItem>
                        <asp:ListItem class="radio" Value="1">Dari Bank</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:Label ID="sumberdanac" runat="server" CssClass="err"></asp:Label>
                </td>
            </tr>
            <tr id="nilaitr" runat="server">
                <td><b>Nilai</b></td>
                <td>
                    <asp:TextBox ID="nilai" runat="server" Width="150" CssClass="txt_num"></asp:TextBox>
                    <asp:LinkButton ID="next" runat="server" CssClass="btn btn-blue" OnClick="next_Click">Next <i class="fa fa-arrow-right"></i></asp:LinkButton>
                    <input class="btn" id="cancel2" style="width: 75px" onclick="location.href = 'TTSRegistrasi.aspx'"
                        type="button" value="Cancel" name="cancel2" runat="server">
                </td>
            </tr>
        </table>
        <br>
        <div id="detildiv" runat="server">
            <table class="tb blue-skin" cellspacing="1">
                <tr align="left" valign="bottom">
                    <th>No. Tagihan</th>
                    <th width="150">Tagihan</th>
                    <th>Tipe</th>
                    <th width="75">Jatuh Tempo</th>
                    <th align="right" width="120">Sisa Tagihan
                    </th>
                    <th align="right">Nilai Pembayaran</th>
                    <th></th>
                </tr>
                <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
                <tr>
                    <td colspan="5">
                        <b id="gtc" runat="server">Total</b>
                    </td>
                    <td>
                        <asp:TextBox ID="gt" runat="server" CssClass="txt_num" Width="100"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="4"></td>
                    <td>Administrasi Bank</td>
                    <td>
                        <asp:TextBox ID="admBank" runat="server" CssClass="txt_num" Width="100px">0</asp:TextBox>
                        <asp:Label ID="admBankc" runat="server" CssClass="err"></asp:Label>
                    </td>
                    <td></td>
                </tr>
                <tr style="display:none">
                    <td colspan="4"></td>
                    <td>Pembulatan</td>
                    <td>
                        <asp:TextBox ID="lebihBayar" runat="server" CssClass="txt_num" Width="100px">0</asp:TextBox>
                        <asp:Label ID="lebihBayarc" runat="server" CssClass="err"></asp:Label>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="4"></td>
                    <td>Lebih Bayar</td>
                    <td>
                        <asp:TextBox ID="lb" runat="server" CssClass="txt_num" Width="100px">0</asp:TextBox>
                        <asp:Label ID="lbc" runat="server" CssClass="err"></asp:Label>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="4"></td>
                    <td>Grand Total</td>
                    <td>
                        <asp:TextBox ID="grandtotal" runat="server" CssClass="txt_num" Width="100px" Font-Bold="true"></asp:TextBox></td>
                    <td></td>
                </tr>
            </table>
            <table height="50">
                <tr>
                    <td>
                        <asp:LinkButton ID="save" runat="server" Width="75" CssClass="btn btn-blue" Text="OK" OnClick="save_Click"><i class="fa fa-share"></i> OK</asp:LinkButton></td>
                    <td>
                        <input class="btn btn-red" id="cancel" style="width: 75px" onclick="location.href='TTSRegistrasi.aspx'"
                            type="button" value="Cancel" name="cancel" runat="server">
                    </td>
                </tr>
            </table>
        </div>
        <script type="text/javascript">
            
            function tagihan(no, nilai, foo) {
                if (foo.checked)
                    document.getElementById('lunas_' + no).value = nilai;
                else
                    document.getElementById('lunas_' + no).value = "";

                hitunggt();
            }
            function hitunggt() {
                foogt = document.getElementById('gt');
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

                hitungbiayaadmin();
            }
            function cvtnum(foo) {
                return foo.replace(/,/gi, "");
            }
            function call(nomor, tglcekgiro) {
                document.getElementById('nobg').value = nomor;
                document.getElementById('tglbg').value = tglcekgiro;
            }
            function hitungbiayaadmin() {
                try{
                    var gt = parseFloat(document.getElementById('gt').value.replace(/,/g, ""));
                    var adm = parseFloat(document.getElementById('biayaadmin').value.replace(/,/g, ""));
                    var tipe = $("input[name=tipebeban]:checked").val();
                    var carabayar = $("input[name=carabayar]:checked").val();
                    var nilaiadmin = 0;

                    if(carabayar == "KK")
                    {
                        if(tipe == 1)
                        {
                            nilaiadmin = gt * adm / 100;
                        }
                        else
                        {
                            nilaiadmin = adm;
                        }

                        document.getElementById('admBank').value = nilaiadmin;
                        CalcBlur(document.getElementById('admBank'));

                    }
                }
                catch(err){
                    console.log(err);
                }
                hitungtotal();
            }
            function hitungtotal() {
                var gt = parseFloat(document.getElementById('gt').value.replace(/,/g, ""));
                var adm = parseFloat(document.getElementById('admBank').value.replace(/,/g, ""));
                var bulat = parseFloat(document.getElementById('lebihBayar').value.replace(/,/g, ""));
                var lebih = parseFloat(document.getElementById('lb').value.replace(/,/g, ""));

                document.getElementById('grandtotal').value = gt + adm + bulat + lebih;
                CalcBlur(document.getElementById('grandtotal'));
            }
            $(function () {
                $("input[name=tipebeban]").click(function () {
                    hitungbiayaadmin();
                })
            })
            $(function () {
                $("input[name=carabayar]").click(function () {
                    hitungbiayaadmin();
                })
            })
        </script>
    </form>
</body>
</html>
