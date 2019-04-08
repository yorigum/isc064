<%@ Reference Page="~/Skema.aspx" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.MigrateKontrak2" CodeFile="MigrateKontrak2.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Approval Kontrak</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Migrate - Approval Kontrak (Hal. 2)">
		<style type="text/css">
		    h3 { font-size: 11pt; text-transform:uppercase; }
		    tr, td { vertical-align:top; }
	    </style>
	</head>
	<body onkeyup="if(document.getElementById('cancel')){if(event.keyCode==27)document.getElementById('cancel').click()}">
		<script language="javascript" src="/Js/NumberFormat.js"></script>
		<form class="cnt" id="Form1" method="post" runat="server">
			<uc1:head id="Head1" runat="server"></uc1:head><input style="DISPLAY: none" type="text">
			<div>
				<table cellspacing="0" cellpadding="0">
					<tr>
						<td width="500">
							<div id="frm" runat="server">
								<h1>Approval Kontrak</h1>
								<p>Halaman 2 dari 3</p>
								<br />
								<table cellspacing="5">
									<tr>
										<td colspan="3">
											<h3>DATA KONTRAK</h3>
										</td>
									</tr>
									<tr>
										<td>No. Kontrak</td>
										<td>:</td>
										<td>
										    <asp:textbox id="nokontrak" runat="server" cssclass="txt" font-bold="True" 
												width="125" MaxLength="20" />
												<asp:label id="nokontrakc" runat="server" cssclass="err" />
												&nbsp;&nbsp; Tanggal : <nobr>
												<asp:textbox id="tglkontrak" runat="server" cssclass="txt_center" width="85" />
												<label for="tglKontrak" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
											</nobr>
											<asp:label id="tglkontrakc" runat="server" cssclass="err" />
											<asp:Label ID="mnokontrak" runat="server" Visible="false"></asp:Label>
										</td>
									</tr>									
									<tr>
									    <td>Status</td>
									    <td>:</td>
									    <td>
									        <asp:RadioButtonList ID="statuskontrak" runat="server" RepeatDirection="Horizontal">
									            <asp:ListItem Value="A">Aktif</asp:ListItem>
									            <asp:ListItem Value="B">Batal</asp:ListItem>
									        </asp:RadioButtonList>
									        <asp:Label ID="mstatuskontrak" runat="server"></asp:Label>
									        <asp:label id="statuskontrakc" runat="server" cssclass="err" />
									    </td>
									</tr>
									<tr>
									    <td>Agent</td>
									    <td>:</td>
									    <td>
									        <asp:DropDownList ID="agent" runat="server" Width="300" CssClass="ddl">
                                            </asp:DropDownList>
                                            <asp:Label ID="magent" runat="server"></asp:Label>
                                            <asp:label id="agentc" runat="server" cssclass="err" />                                            
									    </td>
									</tr>
									<tr>
									    <td>No. Unit</td>
									    <td>:</td>
									    <td>
									        <asp:TextBox ID="nounit" runat="server" Width="100" CssClass="txt"></asp:TextBox>
                                            <asp:label id="nounitc" runat="server" cssclass="err" />
									    </td>
									</tr>
									<tr>
										<td>Gross</td>
										<td>:</td>
										<td><asp:textbox id="pl" runat="server" cssclass="txt" />&nbsp;rupiah
											<asp:label id="pricec" runat="server" cssclass="err" /></td>
									</tr>
									<tr>
										<td>Diskon Rupiah</td>
										<td>:</td>
										<td><asp:textbox id="diskonrupiah" runat="server" cssclass="txt" />&nbsp;rupiah
											<asp:label id="diskonrupiahc" runat="server" cssclass="err" /></td>
									</tr>
									<tr>
										<td>Nilai Kontrak</td>
										<td>:</td>
										<td><asp:textbox id="nilaikontrak" runat="server" cssclass="txt" />&nbsp;rupiah
											<asp:label id="nilaikontrakc" runat="server" cssclass="err" /></td>
									</tr>
									<tr>
										<td>Skema Cara Bayar</td>
										<td>:</td>
										<td>
										    <asp:textbox id="skema" runat="server" cssclass="txt" Width="250" MaxLength="150" />
										    <asp:label id="skemac" runat="server" cssclass="err" />
										</td>
									</tr>
									<tr>
									    <td>Tipe Skema</td>
									    <td>:</td>
									    <td>
									        <asp:RadioButtonList ID="tipeSkema" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="CASH TUNAI">CASH TUNAI</asp:ListItem>
                                                <asp:ListItem Value="CASH BERTAHAP">CASH BERTAHAP</asp:ListItem>
                                                <asp:ListItem Value="KPR">KPR</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <asp:label id="mtipeSkema" runat="server" />
                                            <asp:label id="tipeSkemac" runat="server" cssclass="err" />
									    </td>
									</tr>
									<tr>
									    <td>Sifat PPN</td>
									    <td>:</td>
									    <td>
									        <asp:RadioButtonList ID="sifatppn" runat="server" AutoPostBack="True" RepeatDirection="Horizontal">
									            <asp:ListItem Value="PEMERINTAH">Tanpa PPN</asp:ListItem>
									            <asp:ListItem value="KONSUMEN">Dengan PPN</asp:ListItem>
								            </asp:RadioButtonList>
								            <asp:label id="msifatppn" runat="server" />
								            <asp:label id="sifatppnc" runat="server" cssclass="err" />
									    </td>
									</tr>
									<tr>
										<td>NilaiPPN</td>
										<td>:</td>
										<td><asp:textbox id="nilaippn" runat="server" cssclass="txt" />&nbsp;rupiah
											<asp:label id="nilaippnc" runat="server" cssclass="err" /></td>
									</tr>
									<tr>
										<td>No. VA</td>
										<td>:</td>
										<td>
										    <asp:textbox id="nova" runat="server" cssclass="txt" MaxLength="50" />
										</td>
									</tr>
									<tr>
										<td>Tgl. BAST</td>
										<td>:</td>
										<td><asp:textbox id="tglst" runat="server" cssclass="txt_center" width="85"></asp:textbox>
											<label for="tglst" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
											<asp:label id="tglstc" runat="server" cssclass="err" /></td>
									</tr>
									<tr>
										<td>No. BAST</td>
										<td>:</td>
										<td>
										    <asp:textbox id="nost" runat="server" cssclass="txt" MaxLength="20" />
										</td>
									</tr>
									<tr>
										<td>Target BAST</td>
										<td>:</td>
										<td><asp:textbox id="targetst" runat="server" cssclass="txt_center" width="85"></asp:textbox>
											<label for="targetst" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
											<asp:label id="targetstc" runat="server" cssclass="err" /></td>
									</tr>
									<tr>
										<td>Tgl. PPJB</td>
										<td>:</td>
										<td><asp:textbox id="tglppjb" runat="server" cssclass="txt_center" width="85"></asp:textbox>
											<label for="tglppjb" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
											<asp:label id="tglppjbc" runat="server" cssclass="err" /></td>
									</tr>
									<tr>
										<td>No. PPJB</td>
										<td>:</td>
										<td>
										    <asp:textbox id="noppjb" runat="server" cssclass="txt" MaxLength="20" />
										</td>
									</tr>
									<tr>
										<td>Tgl. AJB</td>
										<td>:</td>
										<td><asp:textbox id="tglajb" runat="server" cssclass="txt_center" width="85"></asp:textbox>
											<label for="tglajb" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
											<asp:label id="tglajbc" runat="server" cssclass="err" /></td>
									</tr>
									<tr>
										<td>No. AJB</td>
										<td>:</td>
										<td>
										    <asp:textbox id="noajb" runat="server" cssclass="txt" MaxLength="20" />
										</td>
									</tr>
									<tr>
										<td>Tgl. Batal</td>
										<td>:</td>
										<td><asp:textbox id="tglbatal" runat="server" cssclass="txt_center" width="85"></asp:textbox>
											<label for="tglbatal" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
											<asp:label id="tglbatalc" runat="server" cssclass="err" /></td>
									</tr>
									<tr>
										<td>Alasan Batal</td>
										<td>:</td>
										<td>
										    <asp:textbox id="alasanbatal" runat="server" cssclass="txt" MaxLength="100" />
										</td>
									</tr>
									<tr>
										<td>Nilai Pembayaran</td>
										<td>:</td>
										<td><asp:textbox id="batalmasuk" runat="server" cssclass="txt" />&nbsp;rupiah
											<asp:label id="batalmasukc" runat="server" cssclass="err" /></td>
									</tr>
									<tr>
										<td>Nilai Klaim Batal</td>
										<td>:</td>
										<td><asp:textbox id="nilaiklaim" runat="server" cssclass="txt" />&nbsp;rupiah
											<asp:label id="nilaiklaimc" runat="server" cssclass="err" /></td>
									</tr>
									<tr>
										<td>Nilai Pengembalian Batal</td>
										<td>:</td>
										<td><asp:textbox id="nilaipulang" runat="server" cssclass="txt" />&nbsp;rupiah
											<asp:label id="nilaipulangc" runat="server" cssclass="err" /></td>
									</tr>
									<tr>
									    <td>Rekening Pembatalan</td>
									    <td>:</td>
									    <td>
									        <asp:DropDownList ID="acc" runat="server" CssClass="ddl" Width="300">
                                                <asp:ListItem Selected="True">- Pilih Rekening Bank -</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:Label ID="macc" runat="server"></asp:Label>
                                            <asp:Label ID="accerr" runat="server" CssClass="err"></asp:Label>
									    </td>
									</tr>
									<tr>
										<td colspan="3">
											&nbsp;
										</td>
									</tr>
									<tr>
										<td colspan="3">
											<h3>DATA KONSUMEN</h3>
										</td>
									</tr>
									<tr>
									    <td>Nama</td>
									    <td>:</td>
									    <td>
									        <asp:textbox id="nama" runat="server" cssclass="txt" width="250" maxlength="100"></asp:textbox>
								            <asp:label id="namac" runat="server" cssclass="err"></asp:label>
								            <asp:Label ID="nocustomer" runat="server" Visible="false"></asp:Label>
									    </td>
									</tr>
									<tr>
									    <td>No. KTP</td>
									    <td>:</td>
									    <td>
									        <asp:TextBox ID="ktp" runat="server" CssClass="txt" MaxLength="50"></asp:TextBox>
									        &nbsp;<asp:label id="ktpc" runat="server" cssclass="err"></asp:label>
									    </td>
									</tr>
									<tr>
								        <td valign="top">Alamat (Sesuai Identitas)</td>
								        <td valign="top">:</td>
								        <td>
						                    <asp:textbox id="ktp1" runat="server" width="250" maxlength="50" cssclass="txt"></asp:textbox>
						                    <asp:label id="ktp1c" runat="server" cssclass="err"></asp:label>
                                            <br />
							                <asp:textbox id="ktp2" runat="server" width="150" maxlength="50" cssclass="txt"></asp:textbox>
							                <br />
							                <asp:textbox id="ktp3" runat="server" width="150" maxlength="50" cssclass="txt"></asp:textbox>
								            <br />
							                <asp:textbox id="ktp4" runat="server" width="150" maxlength="50" cssclass="txt"></asp:textbox>
								        </td>
							        </tr>
							        <tr>
								        <td valign="top">Tempat Tanggal Lahir</td>
								        <td valign="top">:</td>
								        <td>
						                    <asp:textbox id="tempatlahir" runat="server" maxlength="50" cssclass="txt"></asp:textbox>
                                            <asp:textbox id="tgllahir" runat="server" maxlength="50" cssclass="txt_center" Width="88px"></asp:textbox>
                                            <label for="tgllahir" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                            <asp:Label ID="tgllahirc" runat="server" cssclass="err"></asp:Label>
                                        </td>
							        </tr>
							        <tr>
								        <td valign="top">Status</td>
								        <td valign="top">:</td>
								        <td>
                                            <asp:RadioButtonList ID="marital" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem>MENIKAH</asp:ListItem>
                                                <asp:ListItem>BELUM MENIKAH</asp:ListItem>
                                                <asp:ListItem>CERAI</asp:ListItem>
                                                <asp:ListItem>LAIN-LAIN</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <asp:Label ID="mmarital" runat="server"></asp:Label>
                                            <asp:Label ID="maritalc" runat="server" cssclass="err"></asp:Label>
                                        </td>
							        </tr>
							        <tr>
								        <td valign="top">Agama</td>
								        <td valign="top">:</td>
								        <td>
						                    <asp:RadioButtonList ID="agama" runat="server" RepeatDirection="Horizontal" 
                                                RepeatColumns="2">
                                                <asp:ListItem>ISLAM</asp:ListItem>
                                                <asp:ListItem>KRISTEN</asp:ListItem>
                                                <asp:ListItem>KATOLIK</asp:ListItem>
                                                <asp:ListItem>BUDHA</asp:ListItem>
                                                <asp:ListItem>HINDU</asp:ListItem>
                                                <asp:ListItem>KONGHUCU</asp:ListItem>
                                                <asp:ListItem>LAINNYA</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <asp:Label ID="magama" runat="server"></asp:Label>
                                            <asp:Label ID="agamac" runat="server" cssclass="err"></asp:Label>
								        </td>
							        </tr>
							        <tr>
								        <td valign="top">No. Telp</td>
								        <td valign="top">:</td>
								        <td>
						                    <asp:textbox id="telp" runat="server" maxlength="50" cssclass="txt"></asp:textbox>
						                    <asp:label id="telpc" runat="server" cssclass="err"></asp:label>
						                </td>
							        </tr>
							        <tr>
								        <td valign="top">No. Fax</td>
								        <td valign="top">:</td>
								        <td>
						                    <asp:textbox id="fax" runat="server" maxlength="50" cssclass="txt"></asp:textbox>
						                </td>
							        </tr>
							        <tr>
								        <td valign="top">NPWP</td>
								        <td valign="top">:</td>
								        <td>
                                            <asp:TextBox ID="npwp" runat="server" CssClass="txt" MaxLength="50" 
                                                Width="150px"></asp:TextBox>
                                        </td>
							        </tr>
							        <tr>
								        <td valign="top">Alamat Surat Menyurat </td>
								        <td valign="top">:</td>
								        <td>
                                            <asp:textbox id="alamat1" runat="server" width="250" maxlength="50" cssclass="txt"></asp:textbox>
                                            <asp:label id="suratc" runat="server" cssclass="err"></asp:label>
                                            <br />
							                <asp:textbox id="alamat2" runat="server" width="150" maxlength="50" cssclass="txt"></asp:textbox>
							                <br />
							                <asp:textbox id="alamat3" runat="server" width="150" maxlength="50" cssclass="txt"></asp:textbox>
                                        </td>
							        </tr>
								</table>
								<br />
								<table>
									<tr>
										<td><asp:Button ID="save" runat="server" CssClass="btn btn-blue" text="OK" width="75" onclick="save_Click"></asp:button></td>
										<td>
										    <input class="btn btn-red" id="cancel" style="width: 75px" type="button" value="Cancel" name="cancel"
												runat="server">
											<asp:Label ID="cek" runat="server" CssClass="err"></asp:Label>	
										</td>
									</tr>
								</table>
							</div>
						</td>
					</tr>
				</table>
			</div>
			<script type="text/javascript" language="javascript">

		        function callEditTTR(nomor) {
			        popEditTTR(nomor);
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
			
			    function popWaitingList(d1) {
			        kontrol = document.getElementById(d1);
			        openModal('UnitWL.aspx?NoStock=' + kontrol.value , '770', '520');
			    }
			    function kalk(foo) {
			        nomor = foo.options[foo.selectedIndex].value;
			        if (nomor != 0) {
			            pl = document.getElementById('pl').value;
			            tgl = document.getElementById('tglKontrak').value;

			            popSkema(nomor, pl, tgl);
			        }
			    }
			</script>
		</form>
	</body>
</html>
