<%@ Reference Page="~/Skema.aspx" %>
<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.TagihanReset" CodeFile="TagihanReset.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<HTML>
	<HEAD>
		<title>Reset Tagihan</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Tagihan - Reset Tagihan">
	</HEAD>
	<body class="body-padding" onkeyup="&#13;&#10;&#9;if(document.getElementById('backbtn')){if(event.keyCode==27)document.getElementById('backbtn').click()};&#13;&#10;&#9;if(document.getElementById('cancel')){if(event.keyCode==27)document.getElementById('cancel').click()}">
		<form id="Form1" method="post" runat="server" class="cnt">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<input type="text" style="DISPLAY:none">
			<div style="DISPLAY:none">
				<asp:checkbox id="dariDaftar" runat="server"></asp:checkbox>
			</div>
			<div id="pilih" runat="server">
				<h1 class="title title-line">Reset Tagihan</h1>
				<p><b><i>Halaman 1 dari 2</i></b></p>
				<br>
				<table style="BORDER-RIGHT:#dcdcdc 1px solid; BORDER-TOP:#dcdcdc 1px solid; BORDER-LEFT:#dcdcdc 1px solid; BORDER-BOTTOM:#dcdcdc 1px solid"
					cellspacing="5">
					<tr>
						<td>No. Kontrak :</td>
						<td>
							<asp:textbox id="nokontrak" runat="server" width="100" cssclass="txt"></asp:textbox>
							<input type="button" value="..." class="btn" onclick="popDaftarKontrak('a')" id="btnpop"
								runat="server" name="btnpop">
						</td>
						<td>
							<asp:button id="next" runat="server" text="Next..." cssclass="btn" onclick="next_Click"></asp:button>
						</td>
					</tr>
				</table>
				<p class="feed">
					<asp:label id="feed" runat="server"></asp:label>
				</p>
				<input type="button" id="backbtn" runat="server" onclick="history.back(-1)" value="Cancel"
					class="btn" style="MARGIN:5px" name="backbtn">
			</div>
			<div id="frm" runat="server">
				<h1 class="title title-line">Reset Tagihan</h1>
				<p><b><i>Halaman 2 dari 2</i></b></p>
				<br>
				<table cellpadding="0" cellspacing="0">
					<tr valign="top">
						<td width="440">
							<div style="DISPLAY:none">
								<asp:textbox id="pl" runat="server" readonly="True" cssclass="txt"></asp:textbox>
							</div>
							<table cellspacing="5">
								<tr>
									<td colspan="3">
										<p><b>Kondisi Sekarang</b></p>
									</td>
								</tr>
								<tr>
									<td>Skema</td>
									<td>:</td>
									<td>
										<asp:label id="skema" runat="server" font-bold="True"></asp:label>
									</td>
								</tr>
								<tr>
									<td>Tanggal Kontrak</td>
									<td>:</td>
									<td>
										<asp:textbox id="tglkontrak" runat="server" readonly="True" width="85"></asp:textbox>
									</td>
								</tr>
								<tr>
									<td colspan="3">
										<br>
										<p><b>Jadwal Tagihan</b></p>
									</td>
								</tr>
								<tr>
									<td colspan="3">
										<table>
											<tr valign="top">
												<td width="200">
													<asp:radiobutton id="pakaigross" runat="server" groupname="angka" text="Sebelum Diskon" font-size="12"></asp:radiobutton>
													<p style="PADDING-LEFT:20px"><asp:label id="gross" runat="server" font-bold="True"></asp:label></p>
													<p style="PADDING-LEFT:20px;FONT-WEIGHT:normal;FONT-SIZE:8pt;LINE-HEIGHT:normal;FONT-STYLE:normal;FONT-VARIANT:normal">Diskon 
														dihitung ulang</p>
												</td>
												<td width="200">
													<asp:radiobutton id="pakainett" runat="server" groupname="angka" text="Setelah Diskon" font-size="12"
														checked="True"></asp:radiobutton>
													<p style="PADDING-LEFT:20px"><asp:label id="netto" runat="server" font-bold="True"></asp:label></p>
													<p style="PADDING-LEFT:20px;FONT-WEIGHT:normal;FONT-SIZE:8pt;LINE-HEIGHT:normal;FONT-STYLE:normal;FONT-VARIANT:normal">Diskon 
														tidak berubah</p>
												</td>
											</tr>
										</table>
									</td>
								</tr>
								<tr valign="top">
									<td>
										Cara Bayar Baru
										<br>
										<br>
										<p style="FONT-WEIGHT:normal; FONT-SIZE:8pt; LINE-HEIGHT:normal; FONT-STYLE:normal; FONT-VARIANT:normal">Double-click 
											untuk<br>
											membuka kalkulator<br>
											cara bayar</p>
									</td>
									<td>:</td>
									<td>
										<asp:listbox id="carabayar" runat="server" cssclass="ddl" rows="10" width="300"></asp:listbox>
									</td>
								</tr>
								<tr>
									<td colspan="3"><br>
									</td>
								</tr>
								<tr>
									<td>Biaya Administrasi</td>
									<td>:</td>
									<td>
										<asp:textbox id="nilaibiaya" runat="server" cssclass="txt_num" width="100">0</asp:textbox>
										<asp:label id="nilaibiayac" runat="server" cssclass="err"></asp:label>
									</td>
								</tr>
							</table>
						</td>
						<td style="PADDING-RIGHT:10px; PADDING-LEFT:10px; PADDING-BOTTOM:10px; PADDING-TOP:10px"><img src="/Media/line_vert.gif"></td>
						<td>
							<table cellspacing="5">
								<tr>
									<td>
										No. Kontrak :<br>
										<asp:label id="nokontrakl" runat="server" font-bold="True"></asp:label>
									</td>
								</tr>
								<tr>
									<td>
										Unit :
										<br>
										<asp:label id="unit" runat="server" font-bold="True"></asp:label>
									</td>
								</tr>
								<tr>
									<td>
										Customer :
										<br>
										<asp:label id="customer" runat="server" font-bold="True"></asp:label>
									</td>
								</tr>
								<tr>
									<td>
										Sales :
										<br>
										<asp:label id="agent" runat="server" font-bold="True"></asp:label>
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
				<asp:checkbox id="alokasi" runat="server" text="Auto Alokasi TTS" checked="True"></asp:checkbox>
				<br>
				<asp:label id="lblAkunting" runat="server" font-bold="True" font-size="12pt" forecolor="Red"></asp:label>
				<table height="50">
					<tr>
						<td><asp:button id="save" runat="server" width="75" cssclass="btn btn-blue" text="OK" onclick="save_Click"></asp:button></td>
						<td><input class="btn btn-red" id="cancel" style="width: 75px" onclick="location.href='?'" type="button"
								value="Cancel" name="cancel" runat="server">
						</td>
					</tr>
				</table>
			</div>
			<script language="javascript">
			function call(no)
			{
				document.getElementById('nokontrak').value = no;
				document.getElementById('next').click();
			}
			function kalk(foo) {
				nomor = foo.options[foo.selectedIndex].value;
				if(nomor!=0) {
					pl = document.getElementById('pl').value;
					tgl = document.getElementById('tglkontrak').value;
					
					popSkema(nomor,pl,tgl);
				}
			}
			</script>
		</form>
	</body>
</HTML>
