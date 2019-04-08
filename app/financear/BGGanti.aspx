<%@ Page language="c#" Inherits="ISC064.FINANCEAR.BGGanti" CodeFile="BGGanti.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Penggantian Cek Giro</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
    	<link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Cek Giro - Penggantian Cek Giro">
	</head>
	<body onkeyup="if(document.getElementById('cancel')){if(event.keyCode==27)document.getElementById('cancel').click()}">
		<form id="Form1" method="post" runat="server" class="cnt">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<input type="text" style="display:none">
			<div style="display:none">
				<asp:checkbox id="dariReminder" runat="server"></asp:checkbox>
			</div>
			<div id="pilih" runat="server">
				<h1>Penggantian Cek Giro</h1>
				<p>Halaman 1 dari 2</p>
				<br>
				<table style="border:1px solid #DCDCDC" cellspacing="5">
					<tr>
						<td>No. BG :</td>
						<td>
							<asp:textbox id="nobg" runat="server" width="100" cssclass="txt"></asp:textbox>
							<input type="button" value="..." class="btn" onclick="popDaftarBG('bad')" id="btnpop" runat="server"
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
				<input type="button" id="backbtn" runat="server" onclick="history.back(-1)" value="Cancel"
					class="btn" style="margin:5px" name="backbtn">
			</div>
			<div id="frm" runat="server">
				<h1>Penggantian Cek Giro</h1>
				<p>Halaman 2 dari 2</p>
				<br>
				<asp:table id="rpt" runat="server" cssclass="tb" cellspacing="3">
					<asp:tablerow horizontalalign="Left">
						<asp:tableheadercell width="60px">No. TTS</asp:tableheadercell>
						<asp:tableheadercell width="110px">Tgl. / Kasir</asp:tableheadercell>
						<asp:tableheadercell width="200px">Customer</asp:tableheadercell>
						<asp:tableheadercell width="200px">Keterangan</asp:tableheadercell>
						<asp:tableheadercell width="130px">Cara Bayar</asp:tableheadercell>
						<asp:tableheadercell width="90px" horizontalalign="Right">Total</asp:tableheadercell>
					</asp:tablerow>
				</asp:table>
				<br>
				<table cellspacing="5">
					<tr>
						<td colspan="3">
							<p><b>Cek Giro Pengganti</b></p>
						</td>
					</tr>
					<tr>
						<td>No. BG</td>
						<td>:</td>
						<td>
							<asp:textbox id="nobgbaru" runat="server" width="125" cssclass="txt" maxlength="20"></asp:textbox>
							<asp:label id="nobgc" runat="server" cssclass="err"></asp:label>
							<font style="font:8pt">Nama bank diikuti nomor cek giro</font>
						</td>
					</tr>
					<tr>
						<td>Tgl. BG</td>
						<td>:</td>
						<td>
							<asp:textbox id="tglbg" runat="server" cssclass="txt_center" width="85"></asp:textbox>
							<label for="tglbg" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
							<asp:label id="tglbgc" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
				</table>
				<table height="50">
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
			<script language="javascript">
			function call(no)
			{
				document.getElementById('nobg').value = no;
				document.getElementById('next').click();
			}
			</script>
		</form>
	</body>
</html>
