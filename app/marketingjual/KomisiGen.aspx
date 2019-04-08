<%@ Reference Page="~/Skema.aspx" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.KomisiGen" CodeFile="KomisiGen.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Generate Komisi</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Komisi - Generate Komisi">
	</head>
	<body onkeyup="&#13;&#10;&#9;if(document.getElementById('backbtn')){if(event.keyCode==27)document.getElementById('backbtn').click()};&#13;&#10;&#9;if(document.getElementById('cancel')){if(event.keyCode==27)document.getElementById('cancel').click()}">
		<form id="Form1" method="post" runat="server" class="cnt">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<input type="text" style="DISPLAY:none">
			<div style="DISPLAY:none">
				<asp:checkbox id="dariDaftar" runat="server"></asp:checkbox>
				<asp:checkbox id="dariReminder" runat="server"></asp:checkbox>
			</div>
			<div id="pilih" runat="server">
				<h1>Generate Komisi</h1>
				<p>Halaman 1 dari 2</p>
				<br>
				<table style="BORDER-RIGHT:#dcdcdc 1px solid; BORDER-TOP:#dcdcdc 1px solid; BORDER-LEFT:#dcdcdc 1px solid; BORDER-BOTTOM:#dcdcdc 1px solid"
					cellspacing="5">
					<tr>
						<td>No. Kontrak :</td>
						<td>
							<asp:textbox id="nokontrak" runat="server" width="100" cssclass="txt"></asp:textbox>
							<input type="button" value="..." class="btn" onclick="popDaftarKontrak('a&amp;kom=1')"
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
				<h1>Generate Komisi</h1>
				<p>Halaman 2 dari 2</p>
				<br>
				<asp:dropdownlist id="daftarskema" runat="server" visible="False"></asp:dropdownlist>
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
					<tr>
						<td>
							Principal :
							<br>
							<asp:label id="principal" runat="server" font-bold="True"></asp:label>
						</td>
					</tr>
				</table>
				<hr size="1">
				<table cellspacing="5">
					<tr>
						<td>Nilai Kontrak</td>
						<td>:</td>
						<td>
							<asp:label id="netto" runat="server" font-bold="True"></asp:label>
						</td>
					</tr>
					<tr>
						<td>Nilai Kumulatif</td>
						<td>:</td>
						<td>
							<asp:label id="kumulatif" runat="server" font-bold="True"></asp:label>
						</td>
					</tr>
					<tr>
						<td>Skema Berlaku</td>
						<td>:</td>
						<td>
							<asp:label id="skemaid" runat="server" visible="False"></asp:label>
							<asp:label id="skema" runat="server" font-bold="True"></asp:label>
						</td>
					</tr>
					<tr>
						<td colspan="3"><asp:checkbox id="special" runat="server" text="Special Event" font-size="15" font-bold="True"></asp:checkbox></td>
					</tr>
				</table>
				<table height="50">
					<tr>
						<td>
							<asp:button id="save" runat="server" width="75" cssclass="btn" text="OK" onclick="save_Click"></asp:button>
						</td>
						<td>
							<input class="btn btn-red" id="cancel" style="width: 75px" onclick="location.href='?'" type="button"
								value="Cancel" name="cancel" runat="server">
						</td>
						<td style="PADDING-LEFT:7px">
							<asp:label id="noskema" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
				</table>
				<br>
				<h2>Track Record Marketing</h2>
				<asp:table id="rpt" runat="server" cssclass="tb" cellspacing="3">
					<asp:tablerow horizontalalign="Left">
						<asp:tableheadercell>No. Kontrak</asp:tableheadercell>
						<asp:tableheadercell>Tgl. Kontrak</asp:tableheadercell>
						<asp:tableheadercell width="100">Unit</asp:tableheadercell>
						<asp:tableheadercell width="200">Customer</asp:tableheadercell>
						<asp:tableheadercell horizontalalign="Right" width="120">Nilai</asp:tableheadercell>
						<asp:tableheadercell horizontalalign="Right" width="120">Kumulatif</asp:tableheadercell>
					</asp:tablerow>
				</asp:table>
			</div>
			<script language="javascript">
			    function call(no) {
			        document.getElementById('nokontrak').value = no;
			        document.getElementById('next').click();
			    }
			</script>
		</form>
	</body>
</html>
