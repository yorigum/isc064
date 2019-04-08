<%@ Control Language="c#" Inherits="ISC064.NUP.PrintNUPTemplate" CodeFile="PrintNUPTemplate.ascx.cs" %>
<link href="/Media/Style.css" type="text/css" rel="stylesheet">
<style type="text/css">
   .header 
   {
       text-align:center;
       font-size:16px;
       margin-bottom:30px;
   }
   #header
   {
       text-align:center;
       font-size:16px;
       margin-bottom:30px;
   }
   .normalTXT
   {
       font-size:12px;
       font-family:arial;
       text-align:justify;
   }
   .customerTB
   {
      margin:10px 0px 10px 10px;
      border-collapse:collapse;
	  border-color:black;
   }
   .customerTB td
   {
       padding:8px 8px 8px 12px;
   }
   .tableTXT
   {
       font-size:12px;
   }
   .tableASP
   {
       font-size:12px;
   }
   .ol
   {
       margin-bottom:15px;
   }
</style>
<div id="divPrint" runat="server" visible="false">
	<h3> <b><u>NOMOR URUT PEMILIHAN (NUP) </u></b></h3>
	<br /><br />
	<table border=0 style="margin:-20px 0px 0px 0px; float:left;" width="55%">
		<tr>
			<td width="320px" colspan=3><b style="font-size:15pt;">Batavianet</b></td>
		</tr>
		<tr>
			<td width="35%"> <b>Tanggal</b> </td>
			<td width="1%"> <b>:</b> </td>
			<td> <b><asp:Label ID="tgl" runat="server"></asp:Label></b></td>
		</tr>
		<tr>
			<td> <b>Pembayaran NUP</b> </td>
			<td> <b>:</b> </td>
			<td> <b><asp:Label ID="nilainup" runat="server"></asp:Label></b></td>
		</tr>
	</table>
	<div style="text-align:right;">
	<table border=0 width="" style="border:solid 1px #000; border-collapse:collapse; margin-right:50px">
	    <tr>
	        <td width="120px" style="font-weight:bold;"> No NUP  &nbsp; :</td>
	    </tr>
	    <tr>
	        <td height="50px" align="right" style="font-weight:bold; font-size:23pt;">
	            <asp:Label ID="nonup" runat="server"></asp:Label>
	        </td>
	    </tr>
	</table>
	</div>
	<br /><br /><br />
	<div>
	Jenis yang diminati : <asp:Label ID="jenispilihan1" runat="server"></asp:Label>
	
	</div>
	<br /><br />
	<table width="100%" border=1 style="border-collapse:collapse; border-color:black;">
		<tr valign="top">
			<td width="2%" style="padding:0px 15px 10px 6px;" >1</td>
			<td width="40%">Nama Pemesan</td>
			<td><asp:Label ID="nama" runat="server"></asp:Label></td>
		</tr>
		<tr valign="top">
			<td style="padding:0px 15px 10px 6px;" >2</td>
			<td>Alamat Korespondensi Pemesan</td>
			<td><asp:Label ID="alamat" runat="server"></asp:Label></td>
		</tr>
		<tr valign="top">
			<td style="padding:0px 15px 10px 6px;" >3</td>
			<td>No. KTP Pemesan</td>
			<td><asp:Label ID="noktp" runat="server"></asp:Label></td>
		</tr>
		<tr valign="top">
			<td style="padding:0px 15px 10px 6px;" >4</td>
			<td>Alamat KTP Pemesan</td>
			<td><asp:Label ID="alamatktp" runat="server"></asp:Label></td>
		</tr>
		<tr valign="top">
			<td style="padding:0px 15px 10px 6px;" >5</td>
			<td>No. Telepon/ HP Pemesan</td>
			<td><asp:Label ID="telp" runat="server"></asp:Label></td>
		</tr>
		<tr valign="top">
			<td style="padding:0px 15px 10px 6px;" >6</td>
			<td>Nama Sales/ Agen</td>
			<td><asp:Label ID="agent" runat="server"></asp:Label></td>
		</tr>
		<tr valign="top">
			<td style="padding:0px 15px 10px 6px;" >7</td>
			<td>Type yang diminati</td>
			<td><asp:Label ID="tiperumah" runat="server"></asp:Label></td>
		</tr>
		<tr valign="top">
			<td style="padding:0px 15px 10px 6px;" >8</td>
			<td>No. Telepon /HP Sales/ Agen</td>
			<td><asp:Label ID="telpagent" runat="server"></asp:Label></td>
		</tr>
		<tr valign="top">
			<td style="padding:0px 15px 10px 6px;" >9</td>
			<td>No. Rekening atas nama Pemesan<br>
			(Khusus Refund)</td>
			<td>
				<table width="100%" border=0>
					<tr>
						<td width="35%"> Nama </td>
						<td width="1%"> : </td>
						<td> <asp:Label ID="namarek" runat="server"></asp:Label></td>
					</tr>
					<tr>
						<td> Bank </td>
						<td> : </td>
						<td> <asp:Label ID="bank" runat="server"></asp:Label></td>
					</tr>
					<tr>
						<td> Cabang </td>
						<td> : </td>
						<td> <asp:Label ID="cabang" runat="server"></asp:Label></td>
					</tr>
					<tr>
						<td> No. Rekening </td>
						<td> : </td>
						<td><asp:Label ID="norek" runat="server"></asp:Label> </td>
					</tr>
				</table>
			</td>
		</tr>
	</table><br>
	<b><u>Syarat dan Ketentuan</u></b><br>
	<ol type=1>
		<li><p align="justify">
			Nama pemesan adalah nama yang akan tercantum di Surat Pesanan (SP) dan Perjanjian Pengikatan Jual Beli (PPJB) rumah/kavling/ruko, sedangkan cara bayar yang akan dipilih wajib ditentukan pada saat pemilihan unit dilakukan.</p>
		</li>
		<li><p align="justify">
			Penggantian nama pemesan akan diberlakukan ketentuan standar untuk pengalihan hak yang berlaku di SP dan PPJB.</p>
		</li>
		<li><p align="justify">
			Pendaftaran ke-ikutsertaan pemesanan pada tanggal 23 Mei 2015, mulai jam 08. 30 WIB. Pendaftaran maksimal 15 unit (rumah/kavling/ruko), ditandai dengan penyerahan Formulir NUP yang setiap formnya berlaku untuk 1 (satu) unit, dan telah diisi lengkap serta dilampirkan:
			<ol type=a>
				<li>Fotocopy KTP dan NPWP pemesan yang diperbesar 2 (dua) kali dan jelas.</li>
				<li>1 (satu) KTP maksimal untuk pemesanan 5 (lima) unit.</li>
				<li>
					Bukti pembayaran pemesanan sementara sebesar Rp. 5,000,000,- (lima juta rupiah)/Unit,dengan ketentuan :<br>
					Rp. 2.000.000,- (dua juta rupiah) pada saat NUP tanggal 23 Mei 2015 ;<br>
					Rp. 3.000.000,- (tiga juta rupiah) pada saat Launching tanggal 31 Mei 2015
				</li>
			</ol>
		</p></li>
		<li><p align="justify">
			Pembayaran biaya NUP dapat dilakukan dengan cara cash, debit atau pun kartu kredit (di kasir Batavianet). Pembayaran akan dianggap sah apabila TRANS PROPERTY telah menerbitkan 
            Tanda Terima Sementara Atas nama Pemesan.</p>
		</li>
		<li><p align="justify">
			Pada saat pemilihan unit, dokumen yang wajib dibawa pemesan/kuasa yang sah adalah:
			<ol type="a">
				<li>
					Fotocopy formulir NUP dengan melampirkan fotocopy KTP (pemesan), NPWP, dan Surat Kuasa (bila ada), fotocopy KTP Penerima Kuasa.
				</li>
				<li>Asli Tanda Terima Sementara</li>
			</ol>
		</p></li>
		<li><p align="justify">
			Uang NUP hangus dan tidak dapat dikembalikan apabila:
			<ol type="a">
				<li>
					Pemesan atau kuasa yang sah tidak hadir pada saat hari pemilihan unit (31 Mei 2015) atau tidak hadir pada saat namanya dipanggil.
				</li>
				<li>
					Sesudah acara pemilihan unit Pemesan atau kuasa yang sah membatalkan pembelian unit tersebut.
				</li>
			</ol>
		</p></li>
		<li><p align="justify">
			Proses Refund untuk uang NUP akan dikembalikan berdasarkan nama pemesan yang tertera di formulir NUP ini (Batavianet akan mengembalikan uang pemesanan hanya atas nama pemesan saja). Adapun proses refund akan dilakukan dengan ketentuan sbb:
			<ol type="a">
				<li>
					Mengembalikan atau menyerahkan kembali Asli Tanda Terima Sementara kepada Batavianet pada tanggal 31 Mei 2015, setelah acara pemilihan unit selesai
				</li>
				<li>Proses refund akan dilakukan dalam waktu 14 (empat belas) hari kerja.</li>
				<li>Pembayaran dengan kartu kredit, pengembalian uang NUP akan dipotong sebesar 3% (tiga persen) </li>
			</ol>
		</p></li>
		<li><p align="justify">
			Kuasa yang sah adalah orang yang memperoleh kuasa dari Pemberi Kuasa (Pemesan) berdasarkan surat kuasa, yang dibuat dibawah tangan dengan bermaterai cukup (form kuasa dari developer) dengan melampirkan fotocopy KTP yang masih berlaku dari Pemberi kuasa (Pemesan) dan Penerima Kuasa yang diperbesar 2 (dua) kali dengan jelas, dan wajib diserahkan kepada developer Batavianet pada saat launching.</p>
		</p></li>
	</ol>
	Dengan ini saya mengetahui dan menyetujui syarat dan ketentuan tersebut di atas.<br><br><br>
	<table border=0 width="100%">
		<tr>
			<td> PEMESAN/KUASA </td>
			<td> SALES In HOUSE/AGENT </td>
		</tr>
		<tr>
			<td valign="bottom"> 
			<table width="100%" border=0>
					<tr>
						<td width="25%">Nama Customer</td>
						<td width="1%">:</td>
						<td><asp:Label ID="cus" runat="server"></asp:Label></td>
					</tr>
					
				</table>
			</td>
			<td>
				<br><br /><br />
				<table width="100%" border=0>
					<tr>
						<td width="25%">Nama Sales</td>
						<td width="1%">:</td>
						<td><asp:Label ID="agent2" runat="server"></asp:Label></td>
					</tr>
					<tr>
						<td>Nama Kantor</td>
						<td>:</td>
						<td> <asp:Label ID="kantor" runat="server"></asp:Label></td>
					</tr>
				</table>
			</td>
	</table>
    </div>
 
<div id="divInfo" runat="server" visible="false">
    <h1>Tidak dapat mencetak Form NUP</h1>
    <p>Karena :</p>
    <p>1. Data wajib customer untuk NUP belum lengkap</p>
    <p>2. Customer belum melakukan pembayaran NUP pertama</p>
</div>