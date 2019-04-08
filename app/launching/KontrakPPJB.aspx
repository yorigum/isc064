<%@ Page language="c#" Inherits="ISC064.LAUNCHING.KontrakPPJB" CodeFile="KontrakPPJB.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Perjanjian Pengikatan Jual Beli</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Kontrak - Perjanjian Pengikatan Jual Beli">
	</head>
	<body onkeyup="&#13;&#10;&#9;if(document.getElementById('backbtn')){if(event.keyCode==27)document.getElementById('backbtn').click()};&#13;&#10;&#9;if(document.getElementById('cancel')){if(event.keyCode==27)document.getElementById('cancel').click()}">
		<form id="Form1" method="post" runat="server" class="cnt">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<input type="text" style="DISPLAY:none">
			<div style="DISPLAY:none">
				<asp:checkbox id="dariReminder" runat="server"></asp:checkbox>
			</div>
			<div id="pilih" runat="server">
				<h1>Perjanjian Pengikatan Jual Beli</h1>
				<p>Halaman 1 dari 2</p>
				<br>
				<table style="BORDER-RIGHT:#dcdcdc 1px solid; BORDER-TOP:#dcdcdc 1px solid; BORDER-LEFT:#dcdcdc 1px solid; BORDER-BOTTOM:#dcdcdc 1px solid"
					cellspacing="5">
					<tr>
						<td>No. Kontrak :</td>
						<td>
							<asp:textbox id="nokontrak" runat="server" width="100" cssclass="txt"></asp:textbox>
							<input type="button" value="..." class="btn" onclick="popDaftarKontrak('a&amp;ppjb=1')"
								id="btnpop" runat="server" name="btnpop">
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
				<h1>Perjanjian Pengikatan Jual Beli</h1>
				<p>Halaman 2 dari 2</p>
				<br>
				<table cellspacing="5">
					<tr>
						<td>No. Kontrak</td>
						<td>:</td>
						<td>
							<asp:label id="nokontrakl" runat="server" font-bold="True"></asp:label>
						</td>
					</tr>
					<tr>
						<td>Unit</td>
						<td>:</td>
						<td>
							<asp:label id="unit" runat="server" font-bold="True"></asp:label>
						</td>
					</tr>
					<tr>
						<td>Jenis</td>
						<td>:</td>
						<td>
							<asp:label id="jenis" runat="server" font-bold="True"></asp:label>
						</td>
					</tr>
					<tr>
						<td>Customer</td>
						<td>:</td>
						<td>
							<asp:label id="customer" runat="server" font-bold="True"></asp:label>
						</td>
					</tr>
					<tr>
						<td>Sales</td>
						<td>:</td>
						<td>
							<asp:label id="agent" runat="server" font-bold="True"></asp:label>
						</td>
					</tr>
					<tr>
						<td>Pelunasan</td>
						<td>:</td>
						<td>
							<asp:label id="persenlunas" runat="server" font-bold="True"></asp:label>% 
							&nbsp;&nbsp;&nbsp;&nbsp;
							<asp:label id="lunasinfo" runat="server" cssclass="err" font-bold="True"></asp:label>
						</td>
					</tr>
				</table>
				<br>
				<table cellspacing="5">
					<tr>
						<td>No. PPJB</td>
						<td>:</td>
						<td>
							<asp:textbox id="noppjb" runat="server" cssclass="txt" width="120" maxlength="20" readonly="True">#AUTO#</asp:textbox>
							<asp:label id="noppjbc" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
					<tr>
						<td>Tanggal PPJB</td>
						<td>:</td>
						<td>
							<nobr>
								<asp:textbox id="tglppjb" runat="server" cssclass="txt_center" width="85"></asp:textbox>
								<input type="button" value="..." class="btn" onclick="openCalendar('tglppjb')"> </nobr>
							<asp:label id="tglppjbc" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
					<tr>
						<td colspan="3"><br>
						</td>
					</tr>
					<tr>
						<td colspan="3">
						    <b>Pengalihan Kuasa</b>
						</td>
					</tr>
					<tr>
						<td>Sebagai</td>
						<td>:</td>
						<td>
							<%--<asp:dropdownlist id="Dropdownlist1" runat="server" cssclass="ddl" width="250" OnChange="javascript:ubah()">--%>
							<asp:dropdownlist id="seb" runat="server" cssclass="ddl" width="250">
								<asp:listitem>DIRI SENDIRI</asp:listitem>
								<asp:listitem>DIREKTUR</asp:listitem>
								<asp:listitem>KUASA</asp:listitem>
							</asp:dropdownlist>
						</td>
					</tr>
					<tr id="trSrtKuasa" runat="server" style="display:none">
						<td>Tgl. Surat Kuasa</td>
						<td>:</td>
						<td>
							<asp:textbox id="tglskuasa" runat="server" cssclass="txt_center" width="85"></asp:textbox>
							<input type="button" value="..." class="btn" onclick="openCalendar('tglskuasa')">
							&nbsp;<asp:label id="tglskuasac" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
					<tr id="trSrtPersetujuan" runat="server" style="display:none">
						<td>Tgl. Surat Persetujuan</td>
						<td>:</td>
						<td>
							<asp:textbox id="tglspersetujuan" runat="server" cssclass="txt_center" width="85"></asp:textbox>
							<input type="button" value="..." class="btn" onclick="openCalendar('tglspersetujuan')">
							&nbsp;<asp:label id="tglspersetujuanc" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
					<tr id="trNoAkta" runat="server" style="display:none">
						<td>No. Akta</td>
						<td>:</td>
						<td>
							<asp:textbox id="akta" runat="server" width="180"></asp:textbox>
							&nbsp;<asp:label id="aktac" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
					<tr id="trNoSK" runat="server" style="display:none">
						<td>No. SK/MENKUMHAM</td>
						<td>:</td>
						<td>
							<asp:textbox id="nosk" runat="server" width="180"></asp:textbox>
							&nbsp;<asp:label id="noskc" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
					<tr id="trNama" runat="server" style="display:none">
						<td>Atas nama</td>
						<td>:</td>
						<td>
							<asp:textbox id="nama" runat="server" width="180"></asp:textbox>
							&nbsp;<asp:label id="namac" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
					<tr id="trKedudukan" runat="server" style="display:none">
						<td>Berkedudukan di</td>
						<td>:</td>
						<td>
							<asp:textbox id="kedudukan" runat="server" width="180"></asp:textbox>
							&nbsp;<asp:label id="kedudukanc" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
					<tr id="trAlamat" runat="server" style="display:none">
						<td>Alamat</td>
						<td>:</td>
						<td>
							<asp:textbox id="alamat" runat="server" width="300"></asp:textbox>
							&nbsp;<asp:label id="alamatc" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
					<tr id="trRTRW" runat="server" style="display:none">
						<td>RT / RW</td>
						<td>:</td>
						<td>
							<asp:textbox id="rt" runat="server" width="35" MaxLength="3"></asp:textbox>
							&nbsp;/&nbsp;
							<asp:textbox id="rw" runat="server" width="35" MaxLength="3"></asp:textbox>
							&nbsp;<asp:label id="rtrwc" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
					<tr id="trKel" runat="server" style="display:none">
						<td>Kelurahan</td>
						<td>:</td>
						<td>
							<asp:textbox id="kel" runat="server" width="100"></asp:textbox>
							&nbsp;<asp:label id="kelc" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
					<tr id="trKec" runat="server" style="display:none">
						<td>Kecamatan</td>
						<td>:</td>
						<td>
							<asp:textbox id="kec" runat="server" width="100"></asp:textbox>
							&nbsp;<asp:label id="kecc" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
					<tr id="trKomad" runat="server" style="display:none">
						<td>Kotamadya</td>
						<td>:</td>
						<td>
							<asp:textbox id="komad" runat="server" width="100"></asp:textbox>
							&nbsp;<asp:label id="komadc" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
					<tr id="trProv" runat="server" style="display:none">
						<td>Provinsi</td>
						<td>:</td>
						<td>
							<asp:textbox id="prov" runat="server" width="100"></asp:textbox>
							&nbsp;<asp:label id="provc" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
					<tr style="display:none">
						<td>Biaya Administrasi</td>
						<td>:</td>
						<td>
							<asp:textbox id="nilaibiaya" runat="server" cssclass="txt_num" width="100">0</asp:textbox>
							<asp:label id="nilaibiayac" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
					<tr>
						<td>&nbsp;</td>
					</tr>
					<tr style="display:none">
						<td>Nilai Realisasi KPR</td>
						<td>:</td>
						<td>
							<asp:textbox id="nilaikpa" runat="server" cssclass="txt_num" width="100">0</asp:textbox>
							<asp:label id="nilaikpac" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
					<tr  style="display:none">
						<td>Rekening Cair KPR</td>
						<td>:</td>
						<td>
							<asp:dropdownlist id="rekcair" runat="server" cssclass="ddl" width="250">
								<asp:listitem value="1.102.100.01">1.102.100.01 - BCA 491 032 0080</asp:listitem>
								<asp:listitem value="1.102.100.02">1.102.100.02 - BCA 491 032 3500</asp:listitem>
								<asp:listitem value="1.102.100.03">1.102.100.03 - BTN BANDUNG</asp:listitem>
								<asp:listitem value="1.102.100.04">1.102.100.04 - BANK MANDIRI</asp:listitem>
								<asp:listitem value="1.102.100.05">1.102.100.05 - VICTORIA GIRO</asp:listitem>
								<asp:listitem value="1.102.100.06">1.102.100.06 - VICTORIA TABUNGAN</asp:listitem>
							</asp:dropdownlist>
						</td>
					</tr>
				</table>
				<table height="50">
					<tr>
						<td>
							<asp:button id="save" runat="server" cssclass="btn" text="OK" width="75" onclick="save_Click"></asp:button>
						</td>
						<td>
							<input type="button" onclick="location.href='?'" class="btn btn-red" value="Cancel"
								id="cancel" runat="server">
						</td>
					</tr>
				</table>
			</div>
			<script language="javascript">
			function call(nokontrak)
			{
				document.getElementById('nokontrak').value = nokontrak;
				document.getElementById('next').click();
            }

            function ubah()
			{
			    var indexseb = document.getElementById('seb').selectedIndex;

			    if (indexseb == "0") {
			        document.getElementById("trSrtKuasa").style.display = "none";
			        document.getElementById("trSrtPersetujuan").style.display = "none";
			        document.getElementById("trNoAkta").style.display = "none";
			        document.getElementById("trNoSK").style.display = "none";
			        document.getElementById("trNama").style.display = "none";
			        document.getElementById("trKedudukan").style.display = "none";
			        document.getElementById("trAlamat").style.display = "none";
			        document.getElementById("trRTRW").style.display = "none";
			        document.getElementById("trKel").style.display = "none";
			        document.getElementById("trKec").style.display = "none";
			        document.getElementById("trKomad").style.display = "none";
			        document.getElementById("trProv").style.display = "none";
			    }
			    else if (indexseb == "1") {
			        document.getElementById("trSrtKuasa").style.display = "inline";
			        document.getElementById("trSrtPersetujuan").style.display = "none";
			        document.getElementById("trNoAkta").style.display = "inline";
			        document.getElementById("trNoSK").style.display = "inline";
			        document.getElementById("trNama").style.display = "inline";
			        document.getElementById("trKedudukan").style.display = "inline";
			        document.getElementById("trAlamat").style.display = "inline";
			        document.getElementById("trRTRW").style.display = "inline";
			        document.getElementById("trKel").style.display = "inline";
			        document.getElementById("trKec").style.display = "inline";
			        document.getElementById("trKomad").style.display = "inline";
			        document.getElementById("trProv").style.display = "inline";
			    }
			    else if (indexseb == "2") {
			        document.getElementById("trSrtKuasa").style.display = "inline";
			        document.getElementById("trSrtPersetujuan").style.display = "inline";
			        document.getElementById("trNoAkta").style.display = "none";
			        document.getElementById("trNoSK").style.display = "none";
			        document.getElementById("trNama").style.display = "inline";
			        document.getElementById("trKedudukan").style.display = "inline";
			        document.getElementById("trAlamat").style.display = "inline";
			        document.getElementById("trRTRW").style.display = "inline";
			        document.getElementById("trKel").style.display = "inline";
			        document.getElementById("trKec").style.display = "inline";
			        document.getElementById("trKomad").style.display = "inline";
			        document.getElementById("trProv").style.display = "inline";
			    }
			}
			</script>
		</form>
	</body>
</html>
