<%@ Reference Page="~/Skema.aspx" %>
<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.KomisiReset" CodeFile="KomisiReset.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Reset Komisi</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Komisi - Reset Komisi">
	</head>
	<body onkeyup="if(document.getElementById('cancel')){if(event.keyCode==27)document.getElementById('cancel').click()}">
		<form id="Form1" method="post" runat="server" class="cnt">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<input type="text" style="DISPLAY:none">
			<div id="pilih" runat="server">
				<h1>Reset Komisi</h1>
				<p>Halaman 1 dari 2</p>
				<br>
				<table style="border:1px solid #DCDCDC" cellspacing="5">
					<tr>
						<td>No. Kontrak :</td>
						<td>
							<asp:textbox id="nokontrak" runat="server" width="100" cssclass="txt"></asp:textbox>
							<input type="button" value="..." class="btn" onclick="popDaftarKontrak('a')" id="btnpop" runat="server"
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
				<h1>Reset Komisi</h1>
				<p>Halaman 2 dari 2</p>
				<br>
				<table cellpadding="0" cellspacing="0">
					<tr valign="top">
						<td width="440">
							<table cellspacing="5">
								<tr>
									<td colspan="3">
										<p><b>Kondisi Sekarang</b></p>
									</td>
								</tr>
								<tr>
									<td>Principal</td>
									<td>:</td>
									<td>
										<asp:label id="principal" runat="server" font-bold="True"></asp:label>
									</td>
								</tr>
								<tr>
									<td>Nilai Kontrak</td>
									<td>:</td>
									<td>
										<asp:label id="netto" runat="server" font-bold="True"></asp:label>
									</td>
								</tr>
								<tr>
									<td>Skema Komisi</td>
									<td>:</td>
									<td>
										<asp:label id="skemal" runat="server" font-bold="True"></asp:label>
									</td>
								</tr>
								<tr>
									<td colspan="3">
										<br>
										<p><b>Kondisi Baru</b></p>
									</td>
								</tr>
								<tr valign="top">
									<td>
										Skema Komisi
									</td>
									<td>:</td>
									<td>
										<asp:listbox id="skema" runat="server" cssclass="ddl" rows="10" width="300"></asp:listbox>
									</td>
								</tr>
							</table>
						</td>
						<td style="padding:10"><img src="/Media/line_vert.gif"></td>
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
				<table height="50">
					<tr>
						<td><asp:button id="save" runat="server" width="75" cssclass="btn" text="OK" onclick="save_Click"></asp:button></td>
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
			</script>
		</form>
	</body>
</html>
