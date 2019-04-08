<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.CustomerGabung" CodeFile="CustomerGabung.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Gabung Nomor Customer</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Customer - Gabung Nomor Customer">
	</head>
	<body class="body-padding" onkeyup="if(document.getElementById('cancel')){if(event.keyCode==27)document.getElementById('cancel').click()}">
		<form id="Form1" method="post" runat="server" class="cnt">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<input type="text" style="display:none">
			<div id="pilih" runat="server">
				<h1 class="title title-line">Gabung Nomor Customer</h1>
				<p>Halaman 1 dari 2</p>
				<br>
				<ul>
					<li>
						Proses akan memindahkan daftar kontrak customer A ke dalam daftar kontrak 
						customer B.
						<h2>Daftar Kontrak A --> Daftar Kontrak B</h2>
					</li>
				</ul>
				<br>
				<table>
					<tr onclick="document.getElementById('pilihA').checked=true;">
						<td>
							<asp:radiobutton id="pilihA" runat="server" groupname="pil"></asp:radiobutton>
						</td>
						<td>Customer A</td>
						<td>:</td>
						<td>
							<asp:textbox id="del" runat="server" cssclass="txt igroup"></asp:textbox>
							<input type="button" value="&#xf002;" style="font-family: 'fontawesome'" class="btn btn-orange" onclick="document.getElementById('pilihA').checked=true;" show-modal='#ModalPopUp' modal-title='Daftar Customer' modal-url='DaftarCustomer.aspx'>
						</td>
						<td>
							<b style="color:Red">DIHAPUS</b>
						</td>
					</tr>
					<tr onclick="document.getElementById('pilihB').checked=true;">
						<td>
							<asp:radiobutton id="pilihB" runat="server" groupname="pil"></asp:radiobutton>
						</td>
						<td>Customer B</td>
						<td>:</td>
						<td>
							<asp:textbox id="simpan" runat="server" cssclass="txt igroup"></asp:textbox>
							<input type="button" value="&#xf002;" style="font-family: 'fontawesome'" class="btn btn-orange" onclick="document.getElementById('pilihB').checked=true;" show-modal='#ModalPopUp' modal-title='Daftar Customer' modal-url='DaftarCustomer.aspx'>
						</td>
					</tr>
					<tr>
						<td colspan="5">
							<br>
							<asp:LinkButton id="next" runat="server" cssclass="btn btn-blue" onclick="next_Click">NEXT <i class="fa fa-arrow-right"></i></asp:LinkButton>
						</td>
					</tr>
				</table>
				<p class="feed">
					<asp:label id="feed" runat="server"></asp:label>
				</p>
			</div>
			<div id="frm" runat="server">
				<h1 class="title title-line">Gabung Nomor Customer</h1>
				<p>Halaman 2 dari 2</p>
				<br>
				<b>Daftar Kontrak A <font style="color:red">DIHAPUS</font></b>
				<h2>
					<asp:label id="nama1" runat="server"></asp:label>
				</h2>
				<asp:table id="rpta" runat="server" cssclass="tb blue-skin" cellspacing="1">
					<asp:tablerow horizontalalign="Left">
						<asp:tableheadercell width="100">No.</asp:tableheadercell>
						<asp:tableheadercell width="100">Tgl.</asp:tableheadercell>
						<asp:tableheadercell width="300">Keterangan</asp:tableheadercell>
						<asp:tableheadercell width="120" horizontalalign="Right">Nilai</asp:tableheadercell>
					</asp:tablerow>
				</asp:table>
				<br>
				<br>
				&nbsp;&nbsp;&nbsp;AKAN DIPINDAHKAN KE
				<br>
				<br>
				<br>
				<b>Daftar Kontrak B</b>
				<h2>
					<asp:label id="nama2" runat="server"></asp:label>
				</h2>
				<asp:table id="rptb" runat="server" cssclass="tb blue-skin" cellspacing="1">
					<asp:tablerow horizontalalign="Left">
						<asp:tableheadercell width="100">No.</asp:tableheadercell>
						<asp:tableheadercell width="100">Tgl.</asp:tableheadercell>
						<asp:tableheadercell width="300">Keterangan</asp:tableheadercell>
						<asp:tableheadercell width="120" horizontalalign="Right">Nilai</asp:tableheadercell>
					</asp:tablerow>
				</asp:table>
				<table height="50">
					<tr>
						<td>
							<asp:LinkButton ID="save" runat="server" CssClass="btn btn-blue" Width="75" OnClick="save_Click"><i class="fa fa-share"></i> OK
                                        </asp:LinkButton>
						</td>
						<td>
							<input type="button" onclick="location.href='?'" class="btn btn-red" value="Cancel"
								id="cancel">
						</td>
					</tr>
				</table>
			</div>
			<script type="text/javascript">
			function call(nocustomer)
			{
				if(document.getElementById('pilihA').checked)
					document.getElementById('del').value = nocustomer;
				else
					document.getElementById('simpan').value = nocustomer;
			}
			</script>
		</form>
	</body>
</html>
