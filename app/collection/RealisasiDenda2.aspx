<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RealisasiDenda2.aspx.cs" Inherits="ISC064.COLLECTION.RealisasiDenda2" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Realisasi Denda 2</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Kontrak - Realisasi Denda">
	</head>
	<body class="body-padding" onkeyup="if(document.getElementById('cancel')){if(event.keyCode==27)document.getElementById('cancel').click()}"
		ms_positioning="GridLayout">
		<script type="text/javascript" src="/Js/Common.js"></script>
		<script type="text/javascript" src="/Js/NumberFormat.js"></script>
		<form class="cnt" id="Form1" method="post" runat="server">
			<uc1:head id="Head1" runat="server"></uc1:head><input style="DISPLAY: none">
			<div id="frm" runat="server">
				<h1 class="title title-line">Realisasi Denda</h1>
				<p>Halaman 2 dari 2</p>
				<br>
				<table cellspacing="5">
					<tr>
						<td>No. Kontrak</td>
						<td>:</td>
						<td><asp:label id="nokontrakl" runat="server" font-bold="True"></asp:label></td>
					</tr>
					<tr>
						<td>Unit</td>
						<td>:</td>
						<td><asp:label id="unit" runat="server" font-bold="True"></asp:label></td>
					</tr>
					<tr>
						<td>Customer</td>
						<td>:</td>
						<td><asp:label id="customer" runat="server" font-bold="True"></asp:label></td>
					</tr>
					<tr>
						<td>Marketing</td>
						<td>:</td>
						<td><asp:label id="agent" runat="server" font-bold="True"></asp:label></td>
					</tr>
                    <tr>
						<td>Benefit</td>
						<td>:</td>
						<td><asp:label id="benefit" runat="server" font-bold="True"></asp:label></td>
					</tr>
                    <tr>
						<td></td>
						<td></td>
						<td>
						    <%--<input type="checkbox" id="aa" runat="server" />--%>
						    <asp:CheckBox ID="cek" style="display:none" runat="server" OnCheckedChanged="gantirealisasi" AutoPostBack="true" />
						    <%--Realisasi Benefit--%>
						</td>
					</tr>
				</table>
				<br>
				<table cellspacing="1" class="tb blue-skin">
					<tr align="left">
						<th>
							No.</th>
						<th>
							Nama Tagihan</th>
						<th>
							Tipe</th>
						<th>
							Jatuh Tempo</th>
						<th>
							Nilai</th>
						<th>
							Denda</th>
						<th>
							Realisasi Denda</th>
						<th>
							Sisa</th>
<%--						<th>
							Action</th>--%>
					</tr>
					<asp:placeholder id="list" runat="server"></asp:placeholder>
				</table>
				<br>
				<table height="50">
					<tr>
						<td>Tgl. Jatuh Tempo</td>
						<td>:</td>
						<td>
                            <asp:textbox id="tgl" runat="server" type="text" cssclass="txt_center"></asp:textbox>
                            <label for="tgl" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                            <asp:label id="tglc" runat="server" cssclass="err"></asp:label></td>
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
							<asp:textbox id="baru" runat="server" cssclass="input-text" width="500"></asp:textbox>
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
                <br />
                <table>
                    <tr>
						<td colspan="3"><asp:LinkButton id="save" runat="server" cssclass="btn btn-blue" width="75" 
                                 onclick="save_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
                        </td>
                        <td>
                            <br />
                        </td>
                        <td>
							<input type="button" onclick="javascript:history.go(-1)" class="btn btn-red" value="Cancel" id="cancel"/>
						</td>
                    </tr>
                </table>
			</div>
			<asp:label id="warning" runat="server" cssclass="err" font-bold="True" font-size="12pt"></asp:label>
			<script type="text/javascript">
			    function call(nokontrak) {
			        document.getElementById('nokontrak').value = nokontrak;
			        document.getElementById('next').click();
			    }
			    function realisasi(foo, benefit, jum) {
			        var i = 0; var nilai = 0; var abc = 0;
			        if (foo.checked) {
			            while (i < jum) {
			                var sd = document.getElementById('sisa_' + i).value;
			                var r = document.getElementById('real_' + i);

			                nilai = (benefit <= cvtnum(sd)) ? benefit : cvtnum(sd);

			                r.disabled = true;
			                eval("r.value = FinalFormat('" + nilai + "')");

			                benefit = cvtnum(benefit) - cvtnum(nilai);
			                i++;
			            }
			        }
			        else {
			            while (i < jum) {
			                var r = document.getElementById('real_' + i);
			                r.value = 0;
			                r.disabled = false;

			                i++;
			            }
			        }
			    }
			    function cvtnum(foo) {
			        return foo.replace(/,/gi, "");
			    }
			</script>
		</form>
	</body>
</html>
