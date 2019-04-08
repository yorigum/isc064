<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.KontrakGantiNama" CodeFile="KontrakGantiNama.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Pengalihan Hak</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Kontrak - Pengalihan Hak">
	</head>
	<body class="body-padding" onkeyup="if(document.getElementById('cancel')){if(event.keyCode==27)document.getElementById('cancel').click()}">
		<form id="Form1" method="post" runat="server" class="cnt">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<input type="text" style="DISPLAY:none">
			<div id="pilih" runat="server">
				<h1 class="title title-line">Pengalihan Hak</h1>
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
				<h1 class="title title-line">Pengalihan Hak</h1>
				<p><b><i>Halaman 2 dari 2</i></b></p>
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
				<br>
				<br>
				<table cellspacing="5">
					<tr>
						<td colspan="3">
							<p><b>Kondisi Sekarang</b></p>
						</td>
					</tr>
					<tr>
						<td>Customer</td>
						<td>:</td>
						<td>
							<asp:label id="namaskr" runat="server" font-bold="True"></asp:label>
							(<asp:label id="noskr" runat="server" font-bold="True"></asp:label>)
						</td>
					</tr>
					<tr>
						<td colspan="3"><br>
							<p><b>Kondisi Baru</b></p>
						</td>
					</tr>
					<tr>
						<td>Tanggal Pengalihan Hak</td>
						<td>:</td>
						<td>
							<nobr>
								<asp:textbox id="tglgn" runat="server" cssclass="txt_center" width="85"></asp:textbox>
								<label for="tglgn" class="btn btn-cal"><i class="fa fa-calendar"></i></label></nobr>
							<asp:label id="tglgnc" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
					<tr>
						<td>No. Customer</td>
						<td>:</td>
						<td>
							<asp:textbox id="nocustomer" runat="server" cssclass="txt"></asp:textbox>
							<input class="btn btn-orange" id="btnpop2" value="&#xf002;" style="font-family: 'fontawesome'" show-modal='#ModalPopUp' modal-title='Daftar Customer' type="button"
								name="btnpop2" runat="server">
                        <input runat="server" id="btnbaru" class="btn btn-blue" value="Baru" type="button" />
						</td>
					</tr>
                <tr>
                    <td>Customer Baru</td>
                    <td>:</td>
                    <td>
                        <b><asp:Label runat="server" ID="namacustomer"></asp:Label></b>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <br>
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
					<tr style="display:none;">
						<td>Biaya PPH Pengalihan Hak</td>
						<td>:</td>
						<td>
							<asp:textbox id="nilaipph" runat="server" cssclass="txt_num" width="100">0</asp:textbox>
							<asp:label id="nilaipphc" runat="server" cssclass="err"></asp:label>
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
                            <asp:LinkButton id="ok" runat="server" cssclass="btn btn-blue" width="75" onclick="save_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
						</td>
						<td><input class="btn btn-red" id="cancel" style="width: 75px" onclick="location.href='?'" type="button"
								value="Cancel" name="cancel" runat="server">
						</td>
					</tr>
				</table>
			</div>
        <script type="text/javascript">
            function call(no, nama) {
                if (document.getElementById('pilih')) {
                    document.getElementById('nokontrak').value = no;
                    document.getElementById('next').click();
                }
                else if (document.getElementById('frm')) {
                    document.getElementById('nocustomer').value = no;
                    document.getElementById('namacustomer').innerText = nama;
                    //document.getElementById('save').click();
                }
            }
        </script>
		</form>
	</body>
</html>
