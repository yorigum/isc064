<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.KontrakGantiUnit" CodeFile="KontrakGantiUnit.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Pindah Unit</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Kontrak - Pindah Unit">
	</head>
	<body class="body-padding" onkeyup="if(document.getElementById('cancel')){if(event.keyCode==27)document.getElementById('cancel').click()}">
		<form id="Form1" method="post" runat="server" class="cnt">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<input type="text" style="DISPLAY:none">
			<div id="pilih" runat="server">
				<h1 class="title title-line">Pindah Unit</h1>
				<p><b><i>Halaman 1 dari 2</i></b></p>
				<br>
				<table style="BORDER-RIGHT:#dcdcdc 1px solid; BORDER-TOP:#dcdcdc 1px solid; BORDER-LEFT:#dcdcdc 1px solid; BORDER-BOTTOM:#dcdcdc 1px solid"
					cellspacing="5">
					<tr>
						<td><b>No. Kontrak :</b></td>
						<td>
							<asp:textbox id="nokontrak" runat="server" width="100" cssclass="txt"></asp:textbox>
							<input type="button" value="&#xf002;" style="font-family: 'fontawesome'" class="btn btn-orange" show-modal='#ModalPopUp' modal-title='Daftar Kontrak' modal-url='DaftarKontrak.aspx?status=a' id="btnpop"
								runat="server" name="btnpop">
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
                <p class="feed1">
					<asp:label id="feed1" runat="server"></asp:label>
				</p>
			</div>
			<div id="frm" runat="server">
				<h1 class="title title-line">Pindah Unit</h1>
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
						<td>Sales</td>
						<td>:</td>
						<td>
							<asp:label id="agent" runat="server" font-bold="True"></asp:label>
						</td>
					</tr>
				</table>
				<asp:label id="lblValid" runat="server" cssclass="err" font-bold="True"></asp:label>
				&nbsp;<br>
				<br>
				<table cellspacing="5">
					<tr>
						<td colspan="3">
							<p><b>Unit Lama</b></p>
						</td>
					</tr>
					<tr>
						<td>No. Stock</td>
						<td>:</td>
						<td><asp:label id="nostockl" runat="server" font-bold="True"></asp:label></td>
					</tr>
					<tr>
						<td colspan="3"><br/>
							<p><b>Unit Baru</b></p>
						</td>
					</tr>
					<tr>
						<td>Tanggal Pindah Unit</td>
						<td>:</td>
						<td>
							<nobr>
								<asp:textbox id="tglgu" runat="server" cssclass="txt_center" width="85"></asp:textbox>
								<label for="tglgu" class="btn btn-cal"><i class="fa fa-calendar"></i></label> </nobr>
							<asp:label id="tglguc" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
					<tr>
						<td>No. Stock</td>
						<td>:</td>
						<td>
							<asp:textbox id="nostock" runat="server" cssclass="txt"></asp:textbox>
							<input id="btnpop2" type="button" value="&#xf002;" style="font-family: 'fontawesome'" class="btn btn-orange" show-modal='#ModalPopUp' modal-title='Daftar Unit'
								runat="server" name="btnpop2">
                            <%--<input runat="server" id="tbstok" class="btn btn-blue" value="Pilih Table Stok" type="button" />--%>
							<asp:label id="nostockc" runat="server" cssclass="err"></asp:label>                            
						</td>
					</tr>
					<tr>
						<td colspan="3"><br/>
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
						<td colspan="3"><br>
						</td>
					</tr>
					<tr>
						<td colspan="3"><strong>Jurnal Kontrak</strong></td>
					</tr>
					<tr>
						<td>Keterangan Tambahan</td>
						<td>:</td>
						<td>
							<asp:textbox id="baru" runat="server" cssclass="txt" width="500"></asp:textbox>
						</td>
					</tr>
					<tr>
						<td>File Hasil Scan</td>
						<td>:</td>
						<td>
							<input type="file" id="file" runat="server" class="txt" style="WIDTH:568px" name="file">
						</td>
					</tr>
				</table>
				<table height="50">
					<tr>
						<td>
                            <asp:LinkButton id="save" runat="server" cssclass="btn btn-blue" width="75" onclick="save_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
						</td>
						<td><input class="btn btn-red" id="cancel" style="width: 75px" onclick="location.href='?'" type="button"
								value="Cancel" name="cancel" runat="server" />
						</td>
					</tr>
				</table>
			</div>
			<script type="text/javascript">
			function call(no)
			{
				if(document.getElementById('pilih')){
					document.getElementById('nokontrak').value = no;
					document.getElementById('next').click();
				}
				else if(document.getElementById('frm'))
				{
					document.getElementById('nostock').value = no;
					//document.getElementById('save').click();
				}
			}
			function call3(no)
			{
			    document.getElementById('nostock').value = no;
			}
			</script>
		</form>
	</body>
</html>
