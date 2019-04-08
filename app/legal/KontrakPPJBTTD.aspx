<%@ Page language="c#" Inherits="ISC064.LEGAL.KontrakPPJBTTD" CodeFile="KontrakPPJBTTD.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Tanda Tangan PPJB</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Kontrak - Tanda Tangan PPJB">
        <%--<style>
            .tdppjb{

            }
        </style>--%>
	</head>
	<body class="body-padding" onkeyup="&#13;&#10;&#9;if(document.getElementById('backbtn')){if(event.keyCode==27)document.getElementById('backbtn').click()};&#13;&#10;&#9;if(document.getElementById('cancel')){if(event.keyCode==27)document.getElementById('cancel').click()}">
		<form id="Form1" method="post" runat="server" class="cnt">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<input type="text" style="DISPLAY:none">
			<div style="DISPLAY:none">
				<asp:checkbox id="dariReminder" runat="server"></asp:checkbox>
			</div>
			<div id="pilih" runat="server">
				<h1 class="title title-line">Tanda Tangan PPJB</h1>
				<p><b><i>Halaman 1 dari 2</i></b></p>
				<br>
				<table style="BORDER-RIGHT:#dcdcdc 1px solid; BORDER-TOP:#dcdcdc 1px solid; BORDER-LEFT:#dcdcdc 1px solid; BORDER-BOTTOM:#dcdcdc 1px solid"
					cellspacing="5" cellpadding="1">
					<tr>
						<td><b>No. Kontrak :</b></td>
						<td>
							<asp:textbox id="nokontrak" runat="server" width="100" cssclass="txt"></asp:textbox>
                        <input class="btn btn-orange" id="Button1" show-modal='#ModalPopUp' modal-title='Daftar Kontrak' modal-url='DaftarKontrak.aspx?status=a&ppjb=B' type="button" value="&#xf002;" style="font-family: 'fontawesome'"
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
				<h1 class="title title-line">Tanda Tangan PPJB</h1>
				<p><b><i>Halaman 2 dari 2</i></b></p>
				<br>
				<table cellspacing="5" cellpadding="1">
					<tr>
						<td style="width:100px">No. Kontrak</td>
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
						<td style="width:110px">No. PPJB</td>
						<td>:</td>
						<td>
							<asp:textbox id="noppjb" runat="server" cssclass="txt" width="150" maxlength="20" readonly="True" style="text-align:center">#AUTO#</asp:textbox>
							<asp:label id="noppjbc" runat="server" cssclass="err"></asp:label>
						</td>
                        <td rowspan="2">
                            <asp:RadioButtonList ID="ppjbused" runat="server" RepeatDirection="Vertical" CellPadding="10" >
                                <asp:ListItem Value="0" Selected="True">Auto</asp:ListItem>
                                <asp:ListItem Value="1">Manual</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
					</tr>
					<tr>
						<td>No. PPJB Manual</td>
						<td>:</td>
						<td>
							<asp:textbox id="noppjbm" runat="server" cssclass="txt" width="200" maxlength="50" ></asp:textbox>
							<asp:label id="noppjbmc" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
					<tr>
						<td>Tanggal PPJB</td>
						<td>:</td>
						<td>
							<nobr>
								<asp:textbox id="tglppjb" runat="server" cssclass="txt_center tgl" width="85" readonly="True"></asp:textbox>
								<input type="button" value="&#xf073;" style="font-family: 'fontawesome'" class="btn" onclick="openCalendar('tglppjb')" disabled="disabled"/>
							</nobr>
							<asp:label id="tglppjbc" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
					<tr>
						<td colspan="3"><br>
						</td>
					</tr>
					<tr>
						<td>Biaya PPJB</td>
						<td>:</td>
						<td>
							<asp:textbox id="nilaibiaya" runat="server" cssclass="txt_num" readonly="True" width="100">0</asp:textbox>
							<asp:label id="nilaibiayac" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
                    <tr>
                        <td>Keterangan</td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="keterangan" runat="server" TextMode="MultiLine" readonly="True" Width="200" Height="75"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>    
                        <td>Tgl. Tanda Tangan</td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="tglttd" runat="server" Width="85" CssClass="txt_center tgl"></asp:TextBox>
                            <label for="tglttd" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                            <asp:label id="tglttdc" runat="server" cssclass="err"></asp:label>
                        </td>
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
