﻿<%@ Page language="c#" Inherits="ISC064.LEGAL.KontrakSTTarget" CodeFile="KontrakSTTarget.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Target Serah Terima</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Kontrak - Target Serah Terima">
	</head>
	<body class="body-padding" onkeyup="&#13;&#10;&#9;if(document.getElementById('backbtn')){if(event.keyCode==27)document.getElementById('backbtn').click()};&#13;&#10;&#9;if(document.getElementById('cancel')){if(event.keyCode==27)document.getElementById('cancel').click()}">
		<form id="Form1" method="post" runat="server" class="cnt">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<input type="text" style="DISPLAY:none">
			<div style="DISPLAY:none">
				<asp:checkbox id="dariReminder" runat="server"></asp:checkbox>
			</div>
			<div id="pilih" runat="server">
				<h1 class="title title-line">Target Serah Terima</h1>
				<p><b><i>Halaman 1 dari 2</i></b></p>
				<br>
				<table style="BORDER-RIGHT:#dcdcdc 1px solid; BORDER-TOP:#dcdcdc 1px solid; BORDER-LEFT:#dcdcdc 1px solid; BORDER-BOTTOM:#dcdcdc 1px solid"
					cellspacing="5">
					<tr>
						<td><b>No. Kontrak :</b></td>
						<td>
							<asp:textbox id="nokontrak" runat="server" width="100" cssclass="txt"></asp:textbox>
                        <input class="btn btn-orange" id="Button1" show-modal='#ModalPopUp' modal-title='Daftar Kontrak' modal-url='DaftarKontrak.aspx?status=a&st=1' type="button" value="&#xf002;" style="font-family: 'fontawesome'"
								name="btnpop" runat="server" />
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
				<h1 class="title title-line">Serah Terima</h1>
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
						<td>Jadwal Serah Terima</td>
						<td>:</td>
						<td>
							<asp:label id="targetst" runat="server" font-bold="True"></asp:label>
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
				<p style="PADDING-RIGHT:5px;PADDING-LEFT:5px;FONT-WEIGHT:normal;FONT-SIZE:8pt;PADDING-BOTTOM:5px;LINE-HEIGHT:normal;PADDING-TOP:5px;FONT-STYLE:normal;FONT-VARIANT:normal">
					Perbedaan Luas Serah Terima dengan Luas Kontrak akan tercatat sebagai <u>MUTASI 
						PRICE LIST</u>
				</p>
				<table cellspacing="5">
                    <tr>    
                        <td>Tgl. Target</td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="tgltarget" runat="server" Width="85" CssClass="txt_center tgl"></asp:TextBox>
                            <label for="tgltarget" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                            <asp:label id="tgltargetc" runat="server" cssclass="err"></asp:label>
                        </td>
                    </tr>
				</table>
				<table height="50">
					<tr>
						<td>
							<asp:LinkButton ID="save" runat="server" CssClass="btn btn-blue" Width="75" OnClick="save_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
						<td>
							<input type="button" onclick="location.href='?'" class="btn btn-red" value="Cancel" style="WIDTH:100px"
								id="cancel" runat="server">
						</td>
					</tr>
				</table>
			</div>
			<script type="text/javascript">
			function call(nokontrak)
			{
				document.getElementById('nokontrak').value = nokontrak;
				document.getElementById('next').click();
			}
			</script>
		</form>
	</body>
</html>

