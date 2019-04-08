<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>

<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.KontrakDendaBASTRealisasi" CodeFile="KontrakDendaBASTRealisasi.aspx.cs" %>

<!DOCTYPE html>
<html>
	<head>
		<title>Realisasi Denda BAST</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Kontrak - Realisasi Denda BAST">
	</head>
<body class="body-padding" onkeyup="if(document.getElementById('cancel')){if(event.keyCode==27)document.getElementById('cancel').click()}">
		<form id="Form1" method="post" runat="server" class="cnt">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<input type="text" style="DISPLAY:none">
			<div style="DISPLAY:none">
				<asp:checkbox id="dariReminder" runat="server"></asp:checkbox>
			</div>
			<div id="pilih" runat="server">
				<h1 class="title title-line">Realisasi Denda BAST</h1>
				<p><i><b>Halaman 1 dari 2</b></i></p>
				<br>
				<table style="BORDER-RIGHT:#dcdcdc 1px solid; BORDER-TOP:#dcdcdc 1px solid; BORDER-LEFT:#dcdcdc 1px solid; BORDER-BOTTOM:#dcdcdc 1px solid"
					cellspacing="5">
					<tr>
						<td><b>No. Kontrak :</b></td>
						<td>
                    <asp:TextBox ID="nokontrak" runat="server" Width="100" CssClass="txt"></asp:TextBox>
                    <input type="button" value="&#xf002;" style="font-family: 'fontawesome'" class="btn btn-orange" show-modal='#ModalPopUp' modal-title='Daftar Kontrak' modal-url='DaftarKontrak.aspx?status=a' id="btnpop"
                        runat="server" name="btnpop">
						</td>
						<td>
							<asp:button id="next" runat="server" text="Next" cssclass="btn btn-blue" onclick="next_Click"></asp:button>
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
				<h1 class="title title-line">Realisasi Denda BAST</h1>
				<p>Halaman 2 dari 2</p>
				<br>
				<table cellspacing="5">
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
						<td>Agent</td>
						<td>:</td>
						<td>
							<asp:label id="agent" runat="server" font-bold="True"></asp:label>
						</td>
					</tr>
				</table>
				<br>
				<table cellspacing="5">
					<tr>
                        <td>
                            Target BAST
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:Label ID="targetst" runat="server"></asp:Label>
                        </td>
                    </tr>
					<tr>
						<td colspan="3"><br>
						</td>
					</tr>
					<tr>
						<td>Denda BAST</td>
						<td>:</td>
						<td>
							<asp:Label ID="dendabast" runat="server">0</asp:Label>
						</td>
					</tr>
					<tr>
						<td>Realisasi Denda BAST</td>
						<td>:</td>
						<td>
							<asp:textbox id="realisasi" runat="server" cssclass="txt_num" width="100">0</asp:textbox>
							<asp:label id="realisasic" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
				</table>
				<table height="50">
					<tr>
						<td>
                                <asp:LinkButton ID="save" runat="server" CssClass="btn btn-blue" Width="75" OnClick="save_Click">
                                <i class="fa fa-share"></i> OK
                                </asp:LinkButton>
						</td>
						<td>
                                <input type="button" id="cancel" onclick="history.back(-1)" class="btn btn-red" value="Cancel" runat="server" style="width: 75px">
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
