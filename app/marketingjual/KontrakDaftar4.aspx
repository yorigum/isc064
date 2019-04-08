<%@ Reference Page="~/Skema.aspx" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.KontrakDaftar4" CodeFile="KontrakDaftar4.aspx.cs" %>
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
		<script language="javascript" src="/Js/NumberFormat.js"></script>
		<form class="cnt" id="Form1" method="post" runat="server">
			<uc1:head id="Head1" runat="server"></uc1:head><input style="DISPLAY: none" type="text">
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
						<li>
							Price list belum di-set.
						</li>
					</ul>
				</div>
			</div>
			<div id="lanjut" runat="server">
				<table cellspacing="0" cellpadding="0">
					<tr valign="top">
						<td width="500">
							<div id="pilih" runat="server">
								<h1>Pendaftaran Surat Pesanan</h1>
								<p>Halaman 1 dari 3</p>
								<br>
								<h2 style="PADDING-LEFT: 3px">Reservasi</h2>
								<p style="PADDING-LEFT: 3px; FONT-WEIGHT: normal; FONT-SIZE: 8pt; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal">Daftar 
									diurutkan berdasarkan nomor urut.
								</p>
								<asp:table id="rpt" runat="server" cellspacing="3" cssclass="tb">
									<asp:tablerow horizontalalign="Left">
										<asp:tableheadercell>No. Urut</asp:tableheadercell>
										<asp:tableheadercell>No.</asp:tableheadercell>
										<asp:tableheadercell width="120">Batas Waktu</asp:tableheadercell>
										<asp:tableheadercell width="200">Customer / Sales</asp:tableheadercell>
									</asp:tablerow>
								</asp:table>
								<table height="50">
									<tr>
										<td><asp:button id="next" runat="server" cssclass="btn" text="Next..." onclick="next_Click"></asp:button></td>
										<td>
											<p style="PADDING-RIGHT: 3px; PADDING-LEFT: 3px; FONT-SIZE: 8pt; PADDING-BOTTOM: 3px; PADDING-TOP: 3px"><asp:label id="expireinfo" runat="server">Reservasi yang bisa menjadi surat pesanan adalah <u>
														nomor urut satu</u>.</asp:label></p>
										</td>
									</tr>
								</table>
							</div>
							<div id="frm" runat="server"><asp:textbox id="noreservasi" runat="server"></asp:textbox>
								<h1>Pendaftaran Surat Pesanan</h1>
								<p>Halaman 2 dari 3</p>
								<br>
								<table cellspacing="5">
									<tr>
										<td colspan="3">
											<p><b>Dokumen</b></p>
										</td>
									</tr>
									<tr>
										<td>No. Kontrak</td>
										<td>:</td>
										<td><asp:textbox id="nokontrak" runat="server" cssclass="txt" text="#AUTO#" font-bold="True" readonly="True"
												width="65"></asp:textbox>&nbsp;&nbsp; Tanggal : <nobr>
												<asp:textbox id="tglkontrak" runat="server" cssclass="txt_center" width="85"></asp:textbox><label for="tglkontrak" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
											</nobr>
											<asp:label id="tglkontrakc" runat="server" cssclass="err"></asp:label></td>
									</tr>
									<tr>
										<td>Jadwal Serah Terima</td>
										<td>:</td>
										<td><asp:textbox id="targetst" runat="server" cssclass="txt_center" width="85">1 Jan 2010</asp:textbox><label for="targetst" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
											<asp:label id="targetstc" runat="server" cssclass="err"></asp:label></td>
									</tr>
									<tr>
										<td colspan="3"><br>
											<p><b>Perhitungan Harga</b></p>
										</td>
									</tr>
									<tr>
										<td>Luas</td>
										<td>:</td>
										<td><asp:textbox id="luas" runat="server" cssclass="txt" readonly="True" width="75"></asp:textbox>m2
										</td>
									</tr>
									<tr>
										<td>Price List</td>
										<td>:</td>
										<td><asp:textbox id="pl" runat="server" cssclass="txt" readonly="True"></asp:textbox>rupiah
											<asp:label id="pricec" runat="server" cssclass="err"></asp:label></td>
									</tr>
									<tr valign="top" id="trsurcharge" runat="server">
					                    <td>Surcharge</td>
					                    <td>:</td>
					                    <td><asp:TextBox ID="Surcharge" runat="server" CssClass="txt_num" 
                                                ontextchanged="Surcharge_TextChanged"></asp:TextBox>
					                    <asp:label id="surchargec" runat="server" cssclass="err"></asp:label>
					                    </td>
				                    </tr>
									<tr valign="top">
										<td>Skema
											<br>
											<br>
											<p style="FONT-WEIGHT: normal; FONT-SIZE: 8pt; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal">Double-click 
												untuk<br>
												membuka kalkulator<br>
												skema
												cara bayar</p>
										</td>
										<td>:</td>
										<td><asp:listbox id="carabayar" runat="server" cssclass="ddl" width="300" rows="8" 
                                                AutoPostBack="True" onselectedindexchanged="carabayar_SelectedIndexChanged"></asp:listbox></td>
									</tr>
								<%--	<tr>
										<td>Sifat PPN</td>
										<td>:</td>
										<td><asp:radiobuttonlist id="sifatppn" runat="server" autopostback="True" 
                                                repeatdirection="Horizontal" 
                                                onselectedindexchanged="sifatppn_SelectedIndexChanged">
												<asp:listitem Selected="False">Tanpa PPN</asp:listitem>
												<asp:listitem selected="True">Dengan PPN</asp:listitem>
											</asp:radiobuttonlist></td>
									</tr>
									<tr id="trppn" runat="server">
										<td colspan="2"></td>
										<td><asp:checkbox id="includeppn" runat="server" text="Nilai Transaksi adalah Include PPN" Checked="true"></asp:checkbox><br>
											<asp:checkbox id="roundppn" runat="server" text="Nilai PPN Dibulatkan" checked="True"></asp:checkbox></td>
									</tr>--%>
									<tr>
										<td valign="top">PPN Ditanggung</td>
										<td valign="top">:</td>
										<td><asp:radiobuttonlist id="JenisPPN" runat="server" repeatdirection="Horizontal">
												<asp:listitem>PEMERINTAH</asp:listitem>
												<asp:listitem selected="True">KONSUMEN</asp:listitem>
											</asp:radiobuttonlist><asp:label id="JenisPPNc" runat="server" cssclass="err"></asp:label></td>
									</tr>
									<tr>
										<td valign="top">Diskon</td>
										<td valign="top">:</td>
										<td><%--<asp:radiobuttonlist id="jenisdiskon" runat="server" autopostback="True" repeatdirection="Horizontal" onselectedindexchanged="jenisdiskon_SelectedIndexChanged">
												<asp:listitem selected="True">lum sum</asp:listitem>
												<asp:listitem>% bertingkat</asp:listitem>
											</asp:radiobuttonlist>--%>
											<div id="lumsum" runat="server"><%--<asp:textbox id="diskon" runat="server" cssclass="txt_num">0</asp:textbox><asp:label id="diskonc" runat="server" cssclass="err"></asp:label>--%></div>
											<div id="persentingkat" runat="server">
											<input class="btn" onclick="popdiskon('diskon2','diskonket')" type="button" value="..." id="btnBertingkat" runat="server">
											<asp:textbox id="diskon2" runat="server" cssclass="txt" width="50px" 
                                                    maxlength="100" AutoPostBack="true" ontextchanged="diskon2_TextChanged"></asp:textbox>&nbsp;
                                            <div style="DISPLAY: none"><asp:textbox id="diskonket" runat="server"></asp:textbox></div>
											    <asp:TextBox ID="nilaiDiskon" runat="server" CssClass="txt_num" ReadOnly="True"></asp:TextBox>
											    <asp:label id="diskon2c" runat="server" cssclass="err"></asp:label>
											</div>
										</td>
									</tr>
									<tr style="display:none">
										<td valign="top">Bunga</td>
										<td valign="top">:</td>
										<td>
											<div style="FLOAT: left"><asp:textbox id="lsbunga" runat="server" cssclass="txt_num">0</asp:textbox><asp:textbox id="persenBunga" runat="server" width="50"></asp:textbox></div>
											<div style="FLOAT: left"><asp:radiobuttonlist id="rdBunga" runat="server" autopostback="True" repeatdirection="Horizontal" onselectedindexchanged="rdBunga_SelectedIndexChanged">
													<asp:listitem selected="True">Lum Sum</asp:listitem>
													<asp:listitem selected="False">Persen</asp:listitem>
												</asp:radiobuttonlist></div>
										</td>
									</tr>
									<tr style="DISPLAY: none">
										<td valign="top">Jenis KPR</td>
										<td valign="top">:</td>
										<td><asp:radiobuttonlist id="jeniskpr" runat="server" repeatdirection="Horizontal">
												<asp:listitem selected="True" value="0">KPR</asp:listitem>
												<asp:listitem value="1">NON-KPR</asp:listitem>
											</asp:radiobuttonlist><asp:label id="jeniskprc" runat="server" cssclass="err"></asp:label></td>
									</tr>
									<tr>
										<td valign="top">Cara Bayar</td>
										<td valign="top">:</td>
										<td>
											<table id="tablecarabayar" cellspacing="1" cellpadding="1" width="100%" border="0">
												<tr>
													<td style="WIDTH: 275px"><asp:radiobuttonlist id="carabayar2" runat="server" repeatdirection="Horizontal">
															<asp:listitem>CASH KERAS</asp:listitem>
															<asp:ListItem>CASH BERTAHAP</asp:ListItem>
                                                            <asp:ListItem>KPR</asp:ListItem>
														</asp:radiobuttonlist></td>
													<td><asp:label id="carabayarc" runat="server" cssclass="err"></asp:label></td>
												</tr>
											</table>
										</td>
									</tr>
									<tr style="display:none">
										<td>Fitting Out</td>
										<td>:</td>
										<td><asp:textbox id="focounter" runat="server" cssclass="txt_center" width="40">1</asp:textbox>&nbsp;x&nbsp;Angsuran
											<asp:textbox id="fo" runat="server" cssclass="txt_num">0</asp:textbox><asp:label id="foc" runat="server" cssclass="err"></asp:label></td>
									</tr>
								</table>
								<table height="50">
									<tr>
										<td><asp:Button ID="save" runat="server" CssClass="btn btn-blue" text="OK" width="75" onclick="save_Click"></asp:button></td>
										<td><input class="btn btn-red" id="cancel" style="width: 75px" type="button" value="Cancel" name="cancel"
												runat="server">
										</td>
									</tr>
								</table>
							</div>
						</td>
						<td style="PADDING-RIGHT: 10px; PADDING-LEFT: 10px; PADDING-BOTTOM: 10px; PADDING-TOP: 70px"><img src="/Media/line_vert.gif"></td>
						<td style="PADDING-TOP: 60px">
							<table cellspacing="5">
								<tr>
									<td>Unit :
										<br>
										<asp:label id="unit" runat="server" font-bold="True"></asp:label></td>
								</tr>
								<tr>
									<td>Customer :
										<br>
										<asp:label id="customer" runat="server" font-bold="True">
											<br>
										</asp:label></td>
								</tr>
								<tr>
									<td>Sales :
										<br>
										<asp:label id="agent" runat="server" font-bold="True">
											<br>
										</asp:label></td>
								</tr>
								<tr>
									<td>Skema :
										<br>
										<asp:label id="skema" runat="server" font-bold="True">
											<br>
										</asp:label></td>
								</tr>
								<tr>
									<td>Nilai Pengikatan :
										<br>
										<asp:label id="nettorsv" runat="server" font-bold="True">
											<br>
										</asp:label></td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</div>
			<script type="text/javascript">

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
