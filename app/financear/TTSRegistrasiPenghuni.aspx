<%@ Reference Page="~/Customer.aspx" %>

<%@ Page Language="c#" Inherits="ISC064.FINANCEAR.TTSRegistrasiPenghuni" CodeFile="TTSRegistrasiPenghuni.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
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
    <meta name="sec" content="Tanda Terima Sementara - Registrasi TTS (Tenant)">
    <style type="text/css">
        #nilaitr td {
            padding-top: 20px;
        }
    </style>
</head>
<body onkeyup="keyup();">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <input type="text" style="display: none">
        <h1>Registrasi Tanda Terima Sementara</h1>
        <p>Halaman 2 dari 2</p>
        <br>
        <table cellspacing="5">
            <tr>
                <td>Tipe</td>
                <td>:</td>
                <td width="100">
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
        <asp:RadioButtonList ID="carabayar" runat="server" RepeatColumns="2" RepeatDirection="Vertical">
            <asp:ListItem Value="TN">TN = Tunai</asp:ListItem>
            <asp:ListItem Value="KK">KK = Kartu Kredit</asp:ListItem>
            <asp:ListItem Value="KD">KD = Kartu Debit</asp:ListItem>
            <asp:ListItem Value="TR">TR = Transfer Bank</asp:ListItem>
            <asp:ListItem Value="BG">BG = Cek Giro</asp:ListItem>
            <asp:ListItem Value="UJ">UJ = Uang Jaminan</asp:ListItem>
        </asp:RadioButtonList>
        <table cellspacing="5">
            <tr>
                <td>Tanggal</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="tgl" runat="server" CssClass="txt_center" Width="85"></asp:TextBox>
                    <label for="tgl" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                    <asp:Label ID="tglc" runat="server" CssClass="err"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Keterangan</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="ket" runat="server" CssClass="txt" Width="400" MaxLength="200"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Rekening Bank</td>
                <td>:</td>
                <td>
                    <asp:DropDownList ID="ddlAcc" runat="server" CssClass="ddl" Width="300">
                        <asp:ListItem Selected="True">- Pilih Rekening Bank -</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="ddlAccErr" runat="server" CssClass="err"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Transfer Anonim</td>
                <td>:</td>
                <td>
                    <asp:DropDownList ID="anonim" runat="server" CssClass="ddl" Width="600">
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <br>
                    <p>
                        <b>Khusus Cek Giro</b>&nbsp;&nbsp;&nbsp;<i>(data akan otomatis masuk ke master cek 
								giro)</i>
                    </p>
                </td>
            </tr>
            <tr>
                <td>No. BG</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="nobg" runat="server" Width="125" CssClass="txt" MaxLength="20"></asp:TextBox>
                    <input type="button" value="..." class="btn" onclick="popDaftarBG()" id="btnpop" runat="server"
                        name="btnpop">
                    <asp:Label ID="nobgc" runat="server" CssClass="err"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Tanggal BG</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="tglbg" runat="server" CssClass="txt_center" Width="85"></asp:TextBox>
                    <label for="tglbg" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                    <asp:Label ID="tglbgc" runat="server" CssClass="err"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <br>
                    <p><b>Khusus Uang Jaminan</b></p>
                </td>
            </tr>
            <tr>
                <td>Sisa Deposit</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="sisadeposit" runat="server" Font-Bold="True" Font-Size="10" ReadOnly="True"
                        Width="100"></asp:TextBox>
                    <asp:Label ID="sisadepositc" runat="server" CssClass="err"></asp:Label>
                </td>
            </tr>
            <tr id="nilaitr" runat="server">
                <td>Nilai</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="nilai" runat="server" CssClass="txt_num" Width="150"></asp:TextBox>
                    <asp:Button ID="next" runat="server" CssClass="btn" Text="Next..." OnClick="next_Click"></asp:Button>
                    <input class="btn" id="cancel2" style="width: 75px" onclick="location.href='TTSRegistrasi.aspx'"
                        type="button" value="Cancel" name="cancel2" runat="server">
                </td>
            </tr>
        </table>
        <br>
        <div id="detildiv" runat="server">
            <table class="tb" cellspacing="5">
                <tr align="left" valign="bottom">
                    <th width="50">No.</th>
                    <th width="280">Tagihan</th>
                    <th width="75">Jatuh Tempo</th>
                    <th align="right" width="120">Sisa Tagihan
                    </th>
                    <th align="right">Nilai Pembayaran</th>
                </tr>
                <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
                <tr>
                    <td colspan="4">
                        <b id="gtc" runat="server">Grand Total</b>
                    </td>
                    <td>
                        <asp:TextBox ID="gt" runat="server" CssClass="txt_num" ReadOnly="True" Width="100"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table height="50">
                <tr>
                    <td>
                        <asp:Button ID="save" runat="server" Width="75" CssClass="btn" Text="OK" OnClick="save_Click"></asp:Button></td>
                    <td>
                        <input class="btn btn-red" id="cancel" style="width: 75px" onclick="location.href='TTSRegistrasi.aspx'"
                            type="button" value="Cancel" name="cancel" runat="server">
                    </td>
                </tr>
            </table>
        </div>
        <script type="text/javascript">
			function keyup()
			{
				if(event.keyCode==27&&confirm('Apakah anda ingin membatalkan proses registrasi?'))
				{
					if(document.getElementById('cancel'))
						document.getElementById('cancel').click();
					else if(document.getElementById('cancel2'))
						document.getElementById('cancel2').click();
				}
			}
			function tagihan(no,nilai,foo)
			{
				if(foo.checked)
					document.getElementById('lunas_'+no).value = nilai;
				else
					document.getElementById('lunas_'+no).value = "";
					
				hitunggt();
			}
			function hitunggt()
			{
				foogt = document.getElementById('gt');
				grandtotal = 0*1;
				
				eof = false;
				i = 0*1;
				while(!eof) {
					foo = document.getElementById('lunas_'+i);
					if(!foo)
					{
						eof = true;
						break;
					}
					else
					{
						total = cvtnum(foo.value);
						if(!isNaN(total))
							grandtotal = grandtotal + (total*1);
						i++;
					}
				}
				
				finalnet = Math.round(100*grandtotal)/100;
				eval("foogt.value = FinalFormat('"+finalnet+"')");
			}
			function cvtnum(foo){
				return foo.replace(/,/gi ,"");
			}
			function call(nomor,tglcekgiro) {
				document.getElementById('nobg').value = nomor;
				document.getElementById('tglbg').value = tglcekgiro;
			}
        </script>
    </form>
</body>
</html>
