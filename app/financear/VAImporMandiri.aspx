<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VAImporMandiri.aspx.cs" Inherits="ISC064.FINANCEAR.VAImporMandiri" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
	<title>Impor Data Transaksi Virtual Account Mandiri</title>
	<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
	<meta content="C#" name="CODE_LANGUAGE">
	<meta content="JavaScript" name="vs_defaultClientScript">
	<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	<link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
	<meta name="ctrl" content="1">
	<meta name="sec" content="Virtual Account - Impor Data Transaksi Mandiri">
</head>
<body class="body-padding">
	<form class="cnt" id="Form1" method="post" runat="server">
	<uc1:Head ID="Head1" runat="server"></uc1:Head>
	<h1 class="title title-line">Import Data Transaksi Virtual Account Mandiri</h1>
	<br />
        <asp:table id="rule" runat="server" cssclass="tb blue-skin" cellspacing="1">
				<asp:tablerow horizontalalign="Left">
					<asp:tableheadercell>No.</asp:tableheadercell>
					<asp:tableheadercell width="200">Kolom</asp:tableheadercell>
					<asp:tableheadercell width="75">Format</asp:tableheadercell>
					<asp:tableheadercell>Panjang</asp:tableheadercell>
					<asp:tableheadercell width="300">Keterangan</asp:tableheadercell>
				</asp:tablerow>
				<asp:tablerow>
					<asp:tablecell>1.</asp:tablecell>
					<asp:tablecell>No. Kontrak</asp:tablecell>
					<asp:tablecell>TEKS</asp:tablecell>
					<asp:tablecell>50</asp:tablecell>
					<asp:tablecell cssclass="sm"></asp:tablecell>
				</asp:tablerow>
				<asp:tablerow>
					<asp:tablecell>2.</asp:tablecell>
					<asp:tablecell>No. TTS</asp:tablecell>
					<asp:tablecell>TEKS</asp:tablecell>
					<asp:tablecell>50</asp:tablecell>
					<asp:tablecell cssclass="sm">Nomor Yang Duplikat Tidak Di-Upload</asp:tablecell>
				</asp:tablerow>
                
				<asp:tablerow>
					<asp:tablecell>3.</asp:tablecell>
					<asp:tablecell>No. TTS Manual</asp:tablecell>
					<asp:tablecell>TEKS</asp:tablecell>
					<asp:tablecell>50</asp:tablecell>
					<asp:tablecell cssclass="sm"></asp:tablecell>
				</asp:tablerow>
				<asp:tablerow>
					<asp:tablecell>4.</asp:tablecell>
					<asp:tablecell>Tgl. TTS</asp:tablecell>
					<asp:tablecell>TANGGAL</asp:tablecell>
					<asp:tablecell></asp:tablecell>
					<asp:tablecell cssclass="sm">Tanggal Tanda Terima Sementara</asp:tablecell>
				</asp:tablerow>
				<asp:tablerow>
					<asp:tablecell>5.</asp:tablecell>
					<asp:tablecell>No. Kwitansi</asp:tablecell>
					<asp:tablecell>TEKS</asp:tablecell>
					<asp:tablecell>50</asp:tablecell>
					<asp:tablecell cssclass="sm"></asp:tablecell>
				</asp:tablerow>
				<asp:tablerow>
					<asp:tablecell>6.</asp:tablecell>
					<asp:tablecell>Tgl. Kwitansi</asp:tablecell>
					<asp:tablecell>TANGGAL</asp:tablecell>
					<asp:tablecell></asp:tablecell>
					<asp:tablecell cssclass="sm"></asp:tablecell>
				</asp:tablerow>
				<asp:tablerow>
					<asp:tablecell>7.</asp:tablecell>
					<asp:tablecell>Cara Bayar</asp:tablecell>
					<asp:tablecell>RANGE</asp:tablecell>
					<asp:tablecell></asp:tablecell>
					<asp:tablecell cssclass="sm">TN/TR/KK/BG/KD/MB</asp:tablecell>
				</asp:tablerow>
				<asp:tablerow>
					<asp:tablecell>8.</asp:tablecell>
					<asp:tablecell>Nilai Pelunasan</asp:tablecell>
					<asp:tablecell>ANGKA</asp:tablecell>
					<asp:tablecell></asp:tablecell>
					<asp:tablecell cssclass="sm"></asp:tablecell>
				</asp:tablerow>
				<asp:tablerow>
					<asp:tablecell>9.</asp:tablecell>
					<asp:tablecell>Nama Tagihan</asp:tablecell>
					<asp:tablecell>TEKS</asp:tablecell>
					<asp:tablecell>50</asp:tablecell>
					<asp:tablecell cssclass="sm">Harus Sesuai Dengan Jadwal Tagihan</asp:tablecell>
				</asp:tablerow>
				<asp:tablerow>
					<asp:tablecell>10.</asp:tablecell>
					<asp:tablecell>Rekening</asp:tablecell>
					<asp:tablecell>RANGE</asp:tablecell>
					<asp:tablecell></asp:tablecell>
					<asp:tablecell cssclass="sm">01/02</asp:tablecell>
				</asp:tablerow>
			</asp:table>
	<table cellspacing="5">
		<tr>
			<td>
				<b>Bank</b>
			</td>
			<td>:</td>
			<td>
				<asp:DropDownList ID="bank" runat="server" Width="300">
					<asp:ListItem Selected="True">- Pilih Rekening Bank -</asp:ListItem>
				</asp:DropDownList>
				<asp:Label ID="bankc" runat="server" CssClass="err"></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				<b>File</b>
			</td>
			<td>:</td>
			<td>
                <input id="txt"  type = "text" value ="" style="width:40%" />
                <button type="button" class="btn-submit button-submit2" onclick ="javascript:document.getElementById('file').click();">Upload</button>
                <input runat="server" name="file" id = "file" type="file" style='visibility: hidden;' onchange="ChangeText(this, 'txt');"/>				
			</td>
		</tr>
	</table>
	<table style="height: 50px">
		<tr>
			<td>
                <asp:LinkButton ID="upload" runat="server" Width="75" CssClass="btn btn-blue" OnClick="upload_Click">Next <i class="fa fa-arrow-right"></i>
				</asp:LinkButton>  
			</td>
		</tr>
	</table>
	</form>
</body>
    <script type="text/javascript">
    function ChangeText(oFileInput, sTargetID) {
        document.getElementById(sTargetID).value = oFileInput.value;
    }
</script>

</html>

