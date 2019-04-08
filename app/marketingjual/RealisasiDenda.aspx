<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.RealisasiDenda" CodeFile="RealisasiDenda.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Realisasi Denda</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Kontrak - Realisasi Denda">
	</head>
	<body class="body-padding" onkeyup="if(document.getElementById('cancel')){if(event.keyCode==27)document.getElementById('cancel').click()}">
		<form class="cnt" id="Form1" method="post" runat="server">
			<uc1:head id="Head1" runat="server"></uc1:head><input style="DISPLAY: none" type="text">
			<div id="pilih" runat="server">
				<h1 class="title title-line">Realisasi Denda</h1>
				<p><b><i>Halaman 1 dari 2</i></b></p>
				<br>
				<table style="BORDER-RIGHT: #dcdcdc 1px solid; BORDER-TOP: #dcdcdc 1px solid; BORDER-LEFT: #dcdcdc 1px solid; BORDER-BOTTOM: #dcdcdc 1px solid"
					cellspacing="5">
					<tr>
						<td>No. Kontrak :</td>
						<td>
							<asp:textbox id="nokontrak" runat="server" cssclass="txt" width="100"></asp:textbox>
							<input class="btn btn-orange" id="btnpop" show-modal='#ModalPopUp' modal-title='Daftar Kontrak' modal-url='DaftarKontrak.aspx?status=a' type="button" value="&#xf002;" style="font-family: 'fontawesome'"
								name="btnpop" runat="server">
						</td>
						<td>
                            <asp:LinkButton id="next" runat="server" cssclass="btn btn-blue" onclick="next_Click">
                                Next <i class="fa fa-arrow-right"></i>
							</asp:LinkButton>
						</td>
					</tr>
				</table>
				<p class="feed"><asp:label id="feed" runat="server"></asp:label></p>
			</div>
			<div id="frm" runat="server">
				<h1 class="title title-line">Realisasi Denda</h1>
				<p><b><i>Halaman 2 dari 2</i></b></p>
				<br>
				<table cellspacing="5">
					<tr>
						<td>No. Kontrak</td>
						<td>:</td>
						<td><asp:label id="nokontrakl" runat="server" font-bold="True"></asp:label></td>
					</tr>
					<tr>
						<td>Unit</td>
						<td>:</td>
						<td><asp:label id="unit" runat="server" font-bold="True"></asp:label></td>
					</tr>
					<tr>
						<td>Customer</td>
						<td>:</td>
						<td><asp:label id="customer" runat="server" font-bold="True"></asp:label></td>
					</tr>
					<tr>
						<td>Sales</td>
						<td>:</td>
						<td><asp:label id="agent" runat="server" font-bold="True"></asp:label></td>
					</tr>
				</table>
				<br>
				<asp:table id="rpt" runat="server" cellspacing="1" CssClass="tb blue-skin" enableviewstate="True">
					<asp:tablerow>
						<asp:tableheadercell>No.</asp:tableheadercell>
						<asp:tableheadercell horizontalalign="Left">Nama Tagihan</asp:tableheadercell>
						<asp:tableheadercell horizontalalign="Left">Tipe</asp:tableheadercell>
						<asp:tableheadercell horizontalalign="Left" width="75">Jatuh Tempo</asp:tableheadercell>
						<asp:tableheadercell horizontalalign="Right" width="120">Nilai</asp:tableheadercell>
						<asp:tableheadercell horizontalalign="Right" width="120">Denda</asp:tableheadercell>
						<asp:tableheadercell horizontalalign="Right" width="120">Realisasi Denda</asp:tableheadercell>
						<asp:tableheadercell horizontalalign="Right" width="120">Sisa</asp:tableheadercell>
					</asp:tablerow>
				</asp:table>
				<br>
				<table height="50">
					<tr>
						<td>Tagihan Denda</td>
						<td>:</td>
						<td><asp:textbox id="tagihandenda" runat="server" cssclass="txt_num" width="120"></asp:textbox><asp:label id="tagihandendac" runat="server" cssclass="err"></asp:label></td>
					</tr>
					<tr>
						<td>Tgl. Jatuh Tempo</td>
						<td>:</td>
						<td><asp:textbox id="tgl" runat="server" cssclass="txt_center" width="85"></asp:textbox>
                            <label for="tgl" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
							<asp:label id="tglc" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
					<tr>
						<td colspan="3"><br>
						</td>
					</tr>
					<tr>
						<td colspan="3"><strong>Jurnal Kontrak</strong></td>
					</tr>
					<tr>
						<td>Keterangan Tambahan</td>
						<td>:</td>
						<td>
							<asp:textbox id="baru" runat="server" cssclass="txt" width="500"></asp:textbox>
						</td>
					</tr>
					<tr>
						<td>File Hasil Scan</td>
						<td>:</td>
						<td>
							<input type="file" id="file" runat="server" class="txt" style="WIDTH:568px" name="file">
						</td>
					</tr>
					<tr>
						<td colspan="3"><asp:button id="save" runat="server" cssclass="btn btn-blue" width="75" text="OK" onclick="save_Click"></asp:button>
							<input type="button" onclick="location.href='?'" class="btn btn-red" value="Cancel"
								id="cancel">
						</td>
					</tr>
				</table>
			</div>
			<asp:label id="warning" runat="server" cssclass="err" font-bold="True" font-size="12pt"></asp:label>
			<script language="javascript">
			function call(nokontrak)
			{
				document.getElementById('nokontrak').value = nokontrak;
				document.getElementById('next').click();
			}
			</script>
		</form>
	</body>
</html>
