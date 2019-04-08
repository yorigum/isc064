<%@ Reference Page="~/Customer.aspx" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<%@ Page language="c#" Inherits="ISC064.LAUNCHING.TTSRegistrasiMarketing" CodeFile="TTSRegistrasiMarketing.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Registrasi Pembayaran</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Tanda Terima Sementara - Registrasi Pembayaran">
		<style type="text/css">
		#nilaitr TD { PADDING-TOP: 20px }
		</style>
	</HEAD>
	<body onkeyup="keyup();" style="text-align:center">
		<form id="Form1" method="post" runat="server" class="cnt">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<input type="text" style="DISPLAY:none">
			<h1>Registrasi Pembayaran</h1>
			<p>Halaman 2 dari 2</p>
			<br>
			<table cellspacing="5">
				<tr>
					<td>Tipe</td>
					<td>:</td>
					<td width="100">
						<asp:label id="tipe" runat="server" font-bold="True"></asp:label>
					</td>
					<td>Unit</td>
					<td>:</td>
					<td>
						<asp:label id="unit" runat="server" font-bold="True"></asp:label>
					</td>
				</tr>
				<tr>
					<td>Ref.</td>
					<td>:</td>
					<td>
						<asp:label id="referensi" runat="server" font-bold="True"></asp:label>
					</td>
					<td>Customer</td>
					<td>:</td>
					<td>
						<asp:label id="customer" runat="server" font-bold="True"></asp:label>
					</td>
				</tr>
			</table>
			<hr size="1" noshade color="silver">
			<br>
			<asp:radiobuttonlist id="carabayar" runat="server" repeatcolumns="2" repeatdirection="Vertical">
				<asp:listitem value="TN">TN = Tunai</asp:listitem>
				<asp:listitem value="KK">KK = Kartu Kredit</asp:listitem>
				<asp:listitem value="KD">KD = Kartu Debit</asp:listitem>
				<asp:listitem value="TR">TR = Transfer Bank</asp:listitem>
				<asp:listitem value="BG">BG = Cek Giro</asp:listitem>
				<asp:listitem value="MB">MB = Merchant Banking</asp:listitem>
			</asp:radiobuttonlist>
			<table cellspacing="5">
				<tr>
					<td>Tanggal</td>
					<td>:</td>
					<td>
						<asp:textbox id="tgl" runat="server" cssclass="txt_center" width="85"></asp:textbox>
						<input type="button" value="..." class="btn" onclick="openCalendar('tgl')">
						<asp:label id="tglc" runat="server" cssclass="err"></asp:label>
					</td>
				</tr>
				<tr>
					<td>Keterangan</td>
					<td>:</td>
					<td>
						<asp:textbox id="ket" runat="server" cssclass="txt" width="400" maxlength="200"></asp:textbox>
					</td>
				</tr>
				<tr>
					<td>Rekening Bank</td>
					<td>:</td>
					<td>
						<asp:dropdownlist id="ddlAcc" runat="server" cssclass="ddl" width="300"
						    autopostback="True" onselectedindexchanged="rekening_SelectedIndexChanged">
							<asp:listitem selected="True">- Pilih Rekening Bank -</asp:listitem>
						</asp:dropdownlist>
						<asp:label id="ddlAccErr" runat="server" cssclass="err"></asp:label>
					</td>
				</tr>
				<tr>
					<td>Transfer Anonim</td>
					<td>:</td>
					<td>
						<asp:dropdownlist id="anonim" runat="server" cssclass="ddl" width="600">
							<asp:listitem></asp:listitem>
						</asp:dropdownlist>
					</td>
				</tr>
				<tr>
					<td colspan="3">
						<br>
						<p><b>Khusus Cek Giro</b>&nbsp;&nbsp;&nbsp;<i>(data akan otomatis masuk ke master cek 
								giro)</i></p>
					</td>
				</tr>
				<tr>
					<td>Bank BG</td>
					<td>:</td>
					<td><asp:textbox id="bankbg" runat="server" width="125" maxlength="20" cssclass="txt"></asp:textbox></td>
				</tr>
				<tr>
					<td>No. BG</td>
					<td>:</td>
					<td>
						<asp:textbox id="nobg" runat="server" width="125" cssclass="txt" maxlength="20"></asp:textbox>
						<input type="button" value="..." class="btn" onclick="popDaftarBG()" id="btnpop" runat="server"
							name="btnpop">
						<asp:label id="nobgc" runat="server" cssclass="err"></asp:label>
					</td>
				</tr>
				<tr>
					<td>Tanggal BG</td>
					<td>:</td>
					<td>
						<asp:textbox id="tglbg" runat="server" cssclass="txt_center" width="85"></asp:textbox>
						<input type="button" value="..." class="btn" onclick="openCalendar('tglbg')">
						<asp:label id="tglbgc" runat="server" cssclass="err"></asp:label>
					</td>
				</tr>
				<tr>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="3">
						<p><b>Khusus Kartu Kredit</b></p>
					</td>
				</tr>
				<tr>
					<td>No. Kartu</td>
					<td>:</td>
					<td>
						<asp:textbox id="nokk" runat="server" width="125" cssclass="txt" maxlength="50"></asp:textbox>
					</td>
				</tr>
				<tr>
					<td>Bank</td>
					<td>:</td>
					<td>
						<asp:textbox id="bankkk" runat="server" width="125" cssclass="txt" maxlength="50"></asp:textbox>
					</td>
				</tr>
				<tr>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td valign="top">Sumber Dana</td>
					<td valign="top">:</td>
					<td>
						<asp:radiobuttonlist id="sumberdana" runat="server" repeatdirection="Horizontal">
							<asp:listitem selected="True" value="0">Dari Customer</asp:listitem>
							<asp:listitem value="1">Dari Bank</asp:listitem>
						</asp:radiobuttonlist>
						<asp:label id="sumberdanac" runat="server" cssclass="err"></asp:label>
					</td>
				</tr>
				<tr id="nilaitr" runat="server">
					<td>Nilai</td>
					<td>:</td>
					<td>
						<asp:textbox id="nilai" runat="server" cssclass="txt_num" width="150"></asp:textbox>
						<asp:button id="next" runat="server" cssclass="btn" text="Next..." onclick="next_Click"></asp:button>
						<input class="btn" id="cancel2" style="WIDTH: 75px" onclick="location.href='TTSRegistrasi.aspx'"
							type="button" value="Cancel" name="cancel2" runat="server">
					</td>
				</tr>
			</table>
			<br>
			<div id="detildiv" runat="server">
				<table class="tb" cellspacing="5">
					<tr align="left" valign="bottom">
						<th width="100">
							No. Tagihan</th>
						<th width="150">
							Tagihan</th>
						<th>
							Tipe</th>
						<th width="75">
							Jatuh Tempo</th>
						<th align="right" width="120">
							Sisa Tagihan
						</th>
						<th align="right">
							Nilai Pembayaran</th>
					</tr>
					<asp:placeholder id="list" runat="server"></asp:placeholder>
					<tr>
						<td colspan="5">
							<b id="gtc" runat="server">Grand Total</b>
						</td>
						<td>
							<asp:textbox id="gt" runat="server" cssclass="txt_num" readonly="True" width="100"></asp:textbox>
						</td>
					</tr>
						<tr>
						<td colspan="4">
						</td>
						<td>Administrasi Bank</td>
						<td><asp:TextBox ID="admBank" runat="server" CssClass="txt_num" Width="100px">0</asp:TextBox>
						<asp:label id="admBankc" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
					<tr>
						<td colspan="4">
						</td>
						<td>Pembulatan</td>
						<td><asp:TextBox ID="lebihBayar" runat="server" CssClass="txt_num" Width="100px">0</asp:TextBox>
						<asp:label id="lebihBayarc" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
					<tr>
						<td colspan="4">
						</td>
						<td>Lebih Bayar</td>
						<td><asp:TextBox ID="lb" runat="server" CssClass="txt_num" Width="100px">0</asp:TextBox>
						<asp:label id="lbc" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
					<tr>
						<td colspan="4">
						</td>
						<td>Total</td>
						<td><asp:TextBox ID="grandtotal" runat="server" CssClass="txt_num" Width="100px" ReadOnly=true Font-Bold=true></asp:TextBox></td>
					</tr>
				</table>
				<table height="50">
					<tr>
						<td><asp:button id="save" runat="server" width="75" cssclass="btn" text="OK" onclick="save_Click"></asp:button></td>
						<td><input class="btn" id="cancel" style="WIDTH: 75px" onclick="location.href='TTSRegistrasi.aspx'"
								type="button" value="Cancel" name="cancel" runat="server">
						</td>
					</tr>
				</table>
			</div>
			<script language="javascript">
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
            function hitungtotal() {
                var gt = parseFloat(document.getElementById('gt').value.replace(/,/g,""));
                var adm = parseFloat(document.getElementById('admBank').value.replace(/,/g, ""));
                var bulat = parseFloat(document.getElementById('lebihbayar').value.replace(/,/g, ""));
                var lebih = parseFloat(document.getElementById('lb').value.replace(/,/g, ""));

                document.getElementById('grandtotal').value = gt + bulat + lebih;
                CalcBlur(document.getElementById('grandtotal'));
            }
			</script>
		</form>
	</body>
</HTML>
