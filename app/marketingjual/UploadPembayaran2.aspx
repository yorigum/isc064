<%@ Page Language="c#" Inherits="ISC064.marketingjual.UploadPembayaran2" CodeFile="UploadPembayaran2.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
	<title>Upload Jadwal Pembayaran</title>
	<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
	<meta content="C#" name="CODE_LANGUAGE">
	<meta content="JavaScript" name="vs_defaultClientScript">
	<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	<link href="/Media/Style.css" type="text/css" rel="stylesheet">
	<meta name="ctrl" content="1">
	<meta name="sec" content="Migrate - Upload Pembayaran (Hal. 2)">
</head>
<body>
	<form class="cnt" id="Form1" method="post" runat="server">
	<uc1:Head ID="Head1" runat="server"></uc1:Head>
	<h1>
		Upload Pembayaran</h1>
	<br />
	<p class="feed">
		<asp:Label ID="feed" runat="server" />
	</p>
	<div id="div1" runat="server">
		<table cellspacing="5" class="tb">
			<tr>
				<th align="center">
					#
				</th>
				<th align="left" width="80">
					No. Kontrak
				</th>
				<th align="left" width="75">
					No. TTS
				</th>
				<th align="left" width="75">
					No. TTS Manual
				</th>
				<th align="left" width="80">
					Tgl. TTS
				</th>
				<th align="left" width="200">
					No. Kwitansi 
				</th>
				<th align="left" width="80">
					Tgl Kwitansi
				</th>
				<th align="right" width="90">
					Cara Bayar
				</th>
				<th align="left" width="150">
					Nama Tagihan
				</th>
				<th>
					Rekening
				</th>
				<th width="75">
					Nilai Pelunasan
				</th>				
			</tr>
			<asp:PlaceHolder ID="ph" runat="server" />
		</table>
		<table style="height: 50">
			<tr>
				<td>
					<asp:Button ID="save" runat="server" Width="75" CssClass="btn btn-blue" Text="OK" OnClick="save_Click" />
				</td>
				<td>
					<input class="btn btn-red" id="cancel" style="width: 75px" onclick="location.href='VAUpload.aspx'"
						type="button" value="Cancel" name="cancel" runat="server" />
				</td>
			</tr>
		</table>		
	</div>
	
	<div id="div2" runat="server">
		<asp:Table ID="rpt" runat="server" CssClass="tb" CellSpacing="3">
			<asp:TableRow HorizontalAlign="Left">
				<asp:TableHeaderCell Width="80">No. Kontrak</asp:TableHeaderCell>
				<asp:TableHeaderCell Width="80">No. TTS</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="80">No. TTS Manual</asp:TableHeaderCell>
				<asp:TableHeaderCell Width="80">Tgl. TTS</asp:TableHeaderCell>
				<asp:TableHeaderCell Width="80">No. Kwitansi</asp:TableHeaderCell>
				<asp:TableHeaderCell Width="80">Tgl. Kwitansi</asp:TableHeaderCell>
				<asp:TableHeaderCell Width="90" HorizontalAlign="Right">Total</asp:TableHeaderCell>
			</asp:TableRow>
		</asp:Table>
	</div>

	<script language="javascript">
		function tagihan(no, nilai, foo) {
			if (foo.checked)
				document.getElementById('lunas_' + no).value = nilai;
			else
				document.getElementById('lunas_' + no).value = "";
		}
		function call(nomor) {
			popEditTTS(nomor);
		}
	</script>

	</form>
</body>
</html>
