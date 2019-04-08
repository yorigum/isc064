<%@ Page language="c#" Inherits="ISC064.FINANCEAR.BGTolak" CodeFile="BGTolak.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Tolakan Cek Giro</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
    	<link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Cek Giro - Tolakan Cek Giro">
	</head>
	<body onkeyup="if(document.getElementById('cancel')){if(event.keyCode==27)document.getElementById('cancel').click()}">
		<form id="Form1" method="post" runat="server" class="cnt">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<input type="text" style="display:none">
			<div id="pilih" runat="server">
				<h1>Tolakan Cek Giro</h1>
				<p>Halaman 1 dari 2</p>
				<br>
				<table style="border:1px solid #DCDCDC" cellspacing="5">
					<tr>
						<td>No. BG :</td>
						<td>
							<asp:textbox id="nobg" runat="server" width="100" cssclass="txt"></asp:textbox>
							<input type="button" value="..." class="btn" onclick="popDaftarBG('ok')" id="btnpop" runat="server"
								name="btnpop">
						</td>
						<td>
							<asp:button id="next" runat="server" text="Next..." cssclass="btn" onclick="next_Click"></asp:button>
						</td>
					</tr>
				</table>
				<p class="feed">
					<asp:label id="feed" runat="server"></asp:label>
				</p>
			</div>
			<div id="frm" runat="server">
				<h1>Tolakan Cek Giro</h1>
				<p>Halaman 2 dari 2</p>
				<br>
				<asp:table id="rpt" runat="server" cssclass="tb" cellspacing="3">
					<asp:tablerow horizontalalign="Left">
						<asp:tableheadercell width="60">No. TTS</asp:tableheadercell>
						<asp:tableheadercell width="110">Tgl. / Kasir</asp:tableheadercell>
						<asp:tableheadercell width="200">Customer</asp:tableheadercell>
						<asp:tableheadercell width="200">Keterangan</asp:tableheadercell>
						<asp:tableheadercell width="130">Cara Bayar</asp:tableheadercell>
						<asp:tableheadercell width="90" horizontalalign="Right">Total</asp:tableheadercell>
					</asp:tablerow>
				</asp:table>
				<br>
				<table cellspacing="5">
					<tr>
						<td>
							Alasan Penolakan :
							<br>
							<asp:radiobuttonlist id="alasan" runat="server">
								<asp:listitem selected="True">DANA OVERDRAFT</asp:listitem>
								<asp:listitem>TANDA TANGAN BERBEDA</asp:listitem>
								<asp:listitem>CEK GIRO RUSAK/SOBEK</asp:listitem>
								<asp:listitem>PEMALSUAN</asp:listitem>
								<asp:listitem>LAINNYA</asp:listitem>
							</asp:radiobuttonlist>
						</td>
					</tr>
				</table>
				<table style="height:50px">
					<tr>
						<td>
							<asp:Button ID="save" runat="server" CssClass="btn btn-blue" text="OK" width="75" onclick="save_Click"></asp:button>
						</td>
						<td>
							<input type="button" onclick="location.href='?'" class="btn btn-red" value="Cancel"
								id="cancel" runat="server">
						</td>
					</tr>
				</table>
			</div>
			<script type="text/javascript">
			function call(no)
			{
				document.getElementById('nobg').value = no;
				document.getElementById('next').click();
			}
			</script>
		</form>
	</body>
</html>
