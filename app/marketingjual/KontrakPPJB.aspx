<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.KontrakPPJB" CodeFile="KontrakPPJB.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Perjanjian Pengikatan Jual Beli</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Kontrak - Perjanjian Pengikatan Jual Beli">
	</head>
	<body class="body-padding" onkeyup="&#13;&#10;&#9;if(document.getElementById('backbtn')){if(event.keyCode==27)document.getElementById('backbtn').click()};&#13;&#10;&#9;if(document.getElementById('cancel')){if(event.keyCode==27)document.getElementById('cancel').click()}">
		<form id="Form1" method="post" runat="server" class="cnt">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<input type="text" style="DISPLAY:none">
			<div style="DISPLAY:none">
				<asp:checkbox id="dariReminder" runat="server"></asp:checkbox>
			</div>
			<div id="pilih" runat="server">
				<h1 class="title title-line">Perjanjian Pengikatan Jual Beli</h1>
				<p><b><i>Halaman 1 dari 2</i></b></p>
				<br>
				<table style="BORDER-RIGHT:#dcdcdc 1px solid; BORDER-TOP:#dcdcdc 1px solid; BORDER-LEFT:#dcdcdc 1px solid; BORDER-BOTTOM:#dcdcdc 1px solid"
					cellspacing="5" cellpadding="1">
					<tr>
						<td><b>No. Kontrak :</b></td>
						<td>
							<asp:textbox id="nokontrak" runat="server" width="100" cssclass="txt"></asp:textbox>
							<input type="button" value="&#xf002;" style="font-family: 'fontawesome'" class="btn btn-orange" show-modal='#ModalPopUp' modal-title='Daftar Kontrak' modal-url='DaftarKontrak.aspx?status=a&ppjb=B' id="btnpop" runat="server" name="btnpop" />
						</td>
						<td>
							<asp:LinkButton id="next" runat="server" cssclass="btn btn-blue" onclick="next_Click">
                                Next <i class="fa fa-arrow-right"></i>
							</asp:LinkButton>
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
				<h1 class="title title-line">Perjanjian Pengikatan Jual Beli</h1>
				<p><b><i>Halaman 2 dari 2</i></b></p>
				<br>
				<table cellspacing="5" cellpadding="1">
					<tr>
						<td>No. Kontrak</td>
						<td>:</td>
						<td>
							<asp:label id="nokontrakl" runat="server" font-bold="True"></asp:label>
						</td>
					</tr>
					<tr>
						<td>Unit</td>
						<td>:</td>
						<td>
							<asp:label id="unit" runat="server" font-bold="True"></asp:label>
						</td>
					</tr>
					<tr>
						<td>Customer</td>
						<td>:</td>
						<td>
							<asp:label id="customer" runat="server" font-bold="True"></asp:label>
						</td>
					</tr>
					<tr>
						<td>Sales</td>
						<td>:</td>
						<td>
							<asp:label id="agent" runat="server" font-bold="True"></asp:label>
						</td>
					</tr>
					<tr>
						<td>Pelunasan</td>
						<td>:</td>
						<td>
							<asp:label id="persenlunas" runat="server" font-bold="True"></asp:label>% 
							&nbsp;&nbsp;&nbsp;&nbsp;
							<asp:label id="lunasinfo" runat="server" cssclass="err" font-bold="True"></asp:label>
						</td>
					</tr>
				</table>
				<br>
				<table cellspacing="5">
					<tr style="display:none;">
						<td>No. PPJB</td>
						<td>:</td>
						<td>
							<asp:textbox id="noppjb" runat="server" cssclass="txt" width="120" maxlength="20" readonly="True">#AUTO#</asp:textbox>
							<asp:label id="noppjbc" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
					<tr>
						<td>No. PPJB Manual</td>
						<td>:</td>
						<td>
							<asp:textbox id="noppjbm" runat="server" cssclass="txt" width="120" maxlength="50" ></asp:textbox>
							<asp:label id="noppjbmc" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
					<tr>
						<td>Tanggal PPJB</td>
						<td>:</td>
						<td>
							<nobr>
								<asp:textbox id="tglppjb" runat="server" cssclass="txt_center" width="85"></asp:textbox>
								<label for="tglppjb" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
							</nobr>
							<asp:label id="tglppjbc" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
					<tr>
						<td colspan="3"><br>
						</td>
					</tr>
					<tr>
						<td>Biaya Administrasi</td>
						<td>:</td>
						<td>
							<asp:textbox id="nilaibiaya" runat="server" cssclass="txt_num" width="100">0</asp:textbox>
							<asp:label id="nilaibiayac" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
					<tr>
						<td>&nbsp;</td>
					</tr>
					<tr style="display:none">
						<td>Nilai Realisasi KPR</td>
						<td>:</td>
						<td>
							<asp:textbox id="nilaikpa" runat="server" cssclass="txt_num" width="100">0</asp:textbox>
							<asp:label id="nilaikpac" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
					<tr  style="display:none">
						<td>Rekening Cair KPR</td>
						<td>:</td>
						<td>
							<asp:dropdownlist id="rekcair" runat="server" cssclass="ddl" width="250">
								<asp:listitem value="1.102.100.01">1.102.100.01 - BCA 491 032 0080</asp:listitem>
								<asp:listitem value="1.102.100.02">1.102.100.02 - BCA 491 032 3500</asp:listitem>
								<asp:listitem value="1.102.100.03">1.102.100.03 - BTN BANDUNG</asp:listitem>
								<asp:listitem value="1.102.100.04">1.102.100.04 - BANK MANDIRI</asp:listitem>
								<asp:listitem value="1.102.100.05">1.102.100.05 - VICTORIA GIRO</asp:listitem>
								<asp:listitem value="1.102.100.06">1.102.100.06 - VICTORIA TABUNGAN</asp:listitem>
							</asp:dropdownlist>
						</td>
					</tr>
				</table>
				<table height="50">
					<tr>
						<td>
							<asp:LinkButton id="save" runat="server" cssclass="btn btn-blue" width="75" onclick="save_Click">
								<i class="fa fa-share"></i> OK
							</asp:LinkButton>
						</td>
						<td>
							<input type="button" onclick="location.href='?'" class="btn btn-red" value="Cancel"
								id="cancel" runat="server">
						</td>
					</tr>
				</table>
			</div>
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
