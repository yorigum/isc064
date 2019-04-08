<%@ Register TagPrefix="uc1" TagName="HeadKontrak" Src="HeadKontrak.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavKontrakEdit" Src="NavKontrakEdit.ascx" %>
<%@ Page language="c#" Inherits="ISC064.KPA.KontrakProses" CodeFile="KontrakProses.aspx.cs" %>
<!doctype html>
<html>
	<head>
		<title>Kontrak KPA</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Kontrak - Kontrak KPA">
		<script language="javascript" src="/Js/Common.js" type="text/javascript"></script>
	</head>
	<body onkeyup="if(event.keyCode==27) window.close();">
		<form id="Form1" method="post" runat="server">
            <div class="content-header">
                <uc1:NavKontrakEdit ID="NavKontrak1" runat="server" Aktif="2"></uc1:NavKontrakEdit>
            </div>
			<div class="tabdata">
				<div class="pad"><input style="DISPLAY: none">
					<uc1:headkontrak id="HeadKontrak1" runat="server"></uc1:headkontrak>
					<div style="FLOAT: left">
					    <h2 class="title">CHECKLIST BERKAS</h2>
					    <table cellspacing="5">
							<tr>
								<td>Status</td>
								<td>:</td>
								<td><asp:label id="lblStatusBerkas" runat="server"></asp:label></td>
							</tr>
							<tr>
								<td>Tgl. Selesai Berkas</td>
								<td>:</td>
								<td><asp:label id="lblTglSelesaiBerkas" runat="server"></asp:label></td>
							</tr>
							<tr>
								<td>Checklist Dokumen</td>
								<td>:</td>
								<td><asp:label id="lblChecklistDokumen" runat="server"></asp:label></td>
							</tr>
							</table>
					    <hr color="silver" noshade size="1">
						<h2 class="title">WAWANCARA</h2>
						<table cellspacing="5">
							<tr>
								<td>Bank KPA</td>
								<td>:</td>
								<td><asp:label id="lblBankKPA" runat="server"></asp:label></td>
							</tr>
							<tr>
								<td>Status</td>
								<td>:</td>
								<td><asp:label id="lblStatusWawancara" runat="server"></asp:label></td>
							</tr>
							<tr>
								<td>Target Wawancara</td>
								<td>:</td>
								<td><asp:label id="lblTargetWawancara" runat="server"></asp:label></td>
							</tr>
							<tr>
								<td>Tgl. Wawancara</td>
								<td>:</td>
								<td><asp:label id="lblTglWawancara" runat="server"></asp:label></td>
							</tr>
							<tr>
								<td>Lokasi Wawancara</td>
								<td>:</td>
								<td><asp:label id="lblLokasiWawancara" runat="server"></asp:label></td>
							</tr>
							<tr>
								<td valign="top">Keterangan Wawancara</td>
								<td valign="top">:</td>
								<td><asp:label id="lblKetWawancara" runat="server"></asp:label></td>
							</tr>
							<tr>
								<td>Nilai Pengajuan</td>
								<td>:</td>
								<td><asp:label id="nilaipengajuan" runat="server"></asp:label></td>
							</tr>
						</table>
						<hr color="silver" noshade size="1">
						<h2 class="title">OTS</h2>
						<table cellspacing="5">
							<tr>
								<td>Status</td>
								<td>:</td>
								<td><asp:label id="statusots" runat="server"></asp:label></td>
							</tr>
							<tr>
								<td>Target OTS</td>
								<td>:</td>
								<td><asp:label id="targetots" runat="server"></asp:label></td>
							</tr>
							<tr>
								<td>Tgl. OTS</td>
								<td>:</td>
								<td><asp:label id="tglots" runat="server"></asp:label></td>
							</tr>
							<tr>
								<td>Hasil OTS</td>
								<td>:</td>
								<td><asp:label id="hasilots" runat="server"></asp:label></td>
							</tr>
							<tr>
								<td valign="top">Keterangan OTS</td>
								<td valign="top">:</td>
								<td><asp:label id="ketots" runat="server"></asp:label></td>
							</tr>
						</table>
						<hr color="silver" noshade size="1">
						<h2 class="title">LPA</h2>
						<table cellspacing="5">
							<tr>
								<td>Status</td>
								<td>:</td>
								<td><asp:label id="lblStatusLPA" runat="server"></asp:label></td>
							</tr>
							<tr>
								<td>Target LPA</td>
								<td>:</td>
								<td><asp:label id="lblTargetLPA" runat="server"></asp:label></td>
							</tr>
							<tr>
								<td>Tgl. LPA</td>
								<td>:</td>
								<td><asp:label id="lblTglLPA" runat="server"></asp:label></td>
							</tr>
							<tr>
								<td>No. LPA</td>
								<td>:</td>
								<td><asp:label id="lblNoLPA" runat="server"></asp:label></td>
							</tr>
							<tr>
								<td valign="top">Keterangan LPA</td>
								<td valign="top">:</td>
								<td><asp:label id="lblKetLPA" runat="server"></asp:label></td>
							</tr>
						</table>
						<hr color="silver" noshade size="1">
						<h2 class="title">SP3K</h2>
						<table cellspacing="5">
							<tr>
								<td>Status</td>
								<td>:</td>
								<td><asp:label id="lblStatusSP3K" runat="server"></asp:label></td>
							</tr>
							<tr>
								<td>Target SP3K</td>
								<td>:</td>
								<td><asp:label id="lblTargetSP3K" runat="server"></asp:label></td>
							</tr>
							<tr>
								<td>Tgl. Pengajuan SP3K</td>
								<td>:</td>
								<td><asp:label id="lblTglPengajuanSP3K" runat="server"></asp:label></td>
							</tr>
							<tr>
								<td>Tgl. Hasil SP3K</td>
								<td>:</td>
								<td><asp:label id="lblTglHasilSP3K" runat="server"></asp:label></td>
							</tr>
							<tr>
								<td>No. SP3K</td>
								<td>:</td>
								<td><asp:label id="lblNoSP3K" runat="server"></asp:label></td>
							</tr>
							<tr>
								<td>Hasil SP3K</td>
								<td>:</td>
								<td><asp:label id="lblHasilSP3K" runat="server"></asp:label></td>
							</tr>
							<tr>
								<td valign="top">Keterangan SP3K</td>
								<td valign="top">:</td>
								<td><asp:label id="lblKetSP3K" runat="server"></asp:label></td>
							</tr>
							<tr>
								<td valign="top">Nilai KPA Disetujui</td>
								<td valign="top">:</td>
								<td><asp:label id="approvalkpr" runat="server"></asp:label></td>
							</tr>
							<tr>
								<td valign="top">Tambahan Uang Muka</td>
								<td valign="top">:</td>
								<td><asp:label id="tambahum" runat="server"></asp:label></td>
							</tr>
						</table>
						<hr color="silver" noshade size="1">
						<h2 class="title">AKAD</h2>
						<table cellspacing="5">
							<tr>
								<td>Status</td>
								<td>:</td>
								<td><asp:label id="lblStatusAkad" runat="server"></asp:label></td>
							</tr>
							<tr>
								<td>Target Akad</td>
								<td>:</td>
								<td><asp:label id="lblTargetAkad" runat="server"></asp:label></td>
							</tr>
							<tr>
								<td>Tgl. Akad</td>
								<td>:</td>
								<td><asp:label id="lblTglAkad" runat="server"></asp:label></td>
							</tr>
							<tr>
								<td>No. Akad</td>
								<td>:</td>
								<td><asp:label id="lblNoAkad" runat="server"></asp:label></td>
							</tr>
							<tr>
								<td valign="top">Keterangan Akad</td>
								<td valign="top">:</td>
								<td><asp:label id="lblKetAkad" runat="server"></asp:label></td>
							</tr>
							<tr>
								<td>Realisasi Akad</td>
								<td>:</td>
								<td><asp:label id="realisasiakad" runat="server"></asp:label></td>
							</tr>
						</table>
						<hr color="silver" noshade size="1" style="display:none">
						<h2 class="title" style="display:none">AJB</h2>
						<table cellspacing="5" style="display:none">
							<tr>
								<td>Status</td>
								<td>:</td>
								<td><asp:label id="lblStatusAJB" runat="server"></asp:label></td>
							</tr>
							<tr>
								<td>Tgl. AJB</td>
								<td>:</td>
								<td><asp:label id="lblTglAJB" runat="server"></asp:label></td>
							</tr>
							<tr>
								<td>No. AJB</td>
								<td>:</td>
								<td><asp:label id="lblNoAJB" runat="server"></asp:label></td>
							</tr>
							<tr>
								<td>Notaris</td>
								<td>:</td>
								<td><asp:label id="lblNamaNotaris" runat="server"></asp:label></td>
							</tr>
							<tr>
								<td valign="top">Keterangan AJB</td>
								<td valign="top">:</td>
								<td><asp:label id="lblKetAJB" runat="server"></asp:label></td>
							</tr>
						</table>
						<hr color="silver" noshade size="1" style="display:none">
						<h2 class="title" style="display:none">SERTIFIKAT</h2>
						<table cellspacing="5" style="display:none">
							<tr>
								<td>Status</td>
								<td>:</td>
								<td><asp:label id="lblStatusSertifikat" runat="server"></asp:label></td>
							</tr>
							<tr id="atasnama" runat="server">
								<td>Atas Nama</td>
								<td>:</td>
								<td><asp:label id="namaperusahaan" runat="server"></asp:label></td>
							</tr>
							<tr>
								<td>Tgl. Sertifikat</td>
								<td>:</td>
								<td><asp:label id="lblTglSertifikat" runat="server"></asp:label></td>
							</tr>
							<tr>
								<td>No. Sertifikat</td>
								<td>:</td>
								<td><asp:label id="lblNoSertifikat" runat="server"></asp:label></td>
							</tr>
							<tr id="sedangproses1" runat="server">
								<td>Nomor Ukur</td>
								<td>:</td>
								<td><asp:label id="nomorukur" runat="server"></asp:label></td>
							</tr>
							<tr id="sedangproses2" runat="server">
								<td>Tanggal Pengukuran</td>
								<td>:</td>
								<td><asp:label id="tanggalukur" runat="server"></asp:label></td>
							</tr>
							<tr id="sedangproses3" runat="server">
								<td>No Surat Ukur</td>
								<td>:</td>
								<td><asp:label id="nosuratukur" runat="server"></asp:label></td>
							</tr>
							<tr id="sedangproses4" runat="server">
								<td>Tanggal Surat Ukur</td>
								<td>:</td>
								<td><asp:label id="tanggalsuratukur" runat="server"></asp:label></td>
							</tr>
							<tr id="sedangproses5" runat="server">
								<td>Jumlah Bidang</td>
								<td>:</td>
								<td><asp:label id="jumlahbidang" runat="server"></asp:label></td>
							</tr>
							<tr id="sertifikat3" runat="server">
								<td>Status Sertifikat</td>
								<td>:</td>
								<td><asp:label id="statushak" runat="server"></asp:label></td>
							</tr>
							<tr id="sertifikat2" runat="server">
								<td>Jangka Waktu</td>
								<td>:</td>
								<td><asp:label id="jangkawaktu" runat="server" cssclass="txt"></asp:label>tahun</td>
							</tr>
							<tr id="sertifikat1" runat="server">
								<td>Tgl. Berakhir Sertifikat</td>
								<td>:</td>
								<td><asp:label id="tglakhir" runat="server" cssclass="txt"></asp:label></td>
							</tr>
						</table>
						<hr color="silver" noshade size="1" style="display:none">
						<h2 class="title" style="display:none">IMB</h2>
						<table cellspacing="5" style="display:none">
							<tr>
								<td>Status</td>
								<td>:</td>
								<td><asp:label id="statusimb" runat="server"></asp:label></td>
							</tr>
							<tr>
								<td>Tgl. IMB</td>
								<td>:</td>
								<td><asp:label id="tglimb" runat="server"></asp:label></td>
							</tr>
							<tr>
								<td>No. IMB</td>
								<td>:</td>
								<td><asp:label id="noimb" runat="server"></asp:label></td>
							</tr>
							<tr>
								<td>Keterangan</td>
								<td>:</td>
								<td><asp:label id="keteranganimb" runat="server"></asp:label></td>
							</tr>
						</table>
						</div>
				</div>
			</div>
		</form>
	</body>
</html>
