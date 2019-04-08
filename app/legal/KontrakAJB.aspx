<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<%@ Page language="c#" Inherits="ISC064.LEGAL.KontrakAJB" CodeFile="KontrakAJB.aspx.cs" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Akte Jual Beli</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Kontrak - Akte Jual Beli">
	</head>
	<body class="body-padding" onkeyup="&#13;&#10;&#9;if(document.getElementById('backbtn')){if(event.keyCode==27)document.getElementById('backbtn').click()};&#13;&#10;&#9;if(document.getElementById('cancel')){if(event.keyCode==27)document.getElementById('cancel').click()}">
		<form id="Form1" method="post" runat="server" class="cnt">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<input type="text" style="DISPLAY:none">
			<div style="DISPLAY:none">
				<asp:checkbox id="dariReminder" runat="server"></asp:checkbox>
			</div>
			<div id="pilih" runat="server">
				<h1 class="title title-line">Akte Jual Beli</h1>
				<p><b><i>Halaman 1 dari 2</i></b></p>
				<br>
				<table style="BORDER-RIGHT:#dcdcdc 1px solid; BORDER-TOP:#dcdcdc 1px solid; BORDER-LEFT:#dcdcdc 1px solid; BORDER-BOTTOM:#dcdcdc 1px solid"
					cellspacing="5">
					<tr>
						<td><b>No. Kontrak :</b></td>
						<td>
							<asp:textbox id="nokontrak" runat="server" width="100" cssclass="txt"></asp:textbox>
                            <input class="btn btn-orange" id="btnpop" show-modal='#ModalPopUp' modal-title='Daftar Kontrak' modal-url='DaftarKontrak.aspx?status=a&ajb=B' type="button" value="&#xf002;" style="font-family: 'fontawesome'"
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
				<h1 class="title title-line">Akte Jual Beli</h1>
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
					<tr>
						<td>No. AJB</td>
						<td>:</td>
						<td>
							<asp:textbox id="noajb" runat="server" cssclass="txt" width="100" maxlength="20" style="text-align:center" readonly="True">#AUTO#</asp:textbox>
							<asp:label id="noajbc" runat="server" cssclass="err"></asp:label>
						</td>
                        <td rowspan="2">
                            <asp:RadioButtonList ID="ajbused" runat="server" RepeatDirection="Vertical" CellPadding="10" >
                                <asp:ListItem Value="0" Selected="True">Auto</asp:ListItem>
                                <asp:ListItem Value="1">Manual</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
					</tr>
                    <tr>
						<td>No. AJB Manual</td>
						<td>:</td>
						<td>
							<asp:textbox id="noajbm" runat="server" cssclass="txt" width="200" maxlength="50" ></asp:textbox>
							<asp:label id="noajbmc" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
					<tr>
						<td>Tanggal AJB</td>
						<td>:</td>
						<td>
							<nobr>
								<asp:textbox id="tglajb" runat="server" cssclass="txt_center tgl" width="85"></asp:textbox>
								<label for="tglajb" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                            </nobr>
							<asp:label id="tglajbc" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
					<tr>
						<td>Notaris</td>
						<td>:</td>
						<td>
							<asp:textbox id="notaris" runat="server" cssclass="txt" width="100" maxlength="50" ></asp:textbox>
							<asp:label id="notarisc" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
					<tr>
						<td>Biaya AJB</td>
						<td>:</td>
						<td>
							<asp:textbox id="nilaibiaya" runat="server" cssclass="txt_num" width="100">0</asp:textbox>
							<asp:label id="nilaibiayac" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
                    <tr>
                        <td>Keterangan</td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="keterangan" runat="server" TextMode="MultiLine" Width="200" Height="75"></asp:TextBox>
                        </td>
                    </tr>
				</table>
				<table height="50">
					<tr>
						<td>
							<asp:Linkbutton id="save" runat="server" cssclass="btn btn-blue" width="75" onclick="save_Click">
								<i class="fa fa-share"></i> OK
							</asp:Linkbutton> 
						</td>
						<td>
							<input type="button" onclick="location.href='?'" class="btn btn-red" value="Cancel" style="WIDTH:100px"
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
