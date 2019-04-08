<%@ Reference Page="~/Customer.aspx" %>
<%@ Page language="c#" Inherits="ISC064.COLLECTION.PJTRegistrasiMarketing" CodeFile="PJTRegistrasiMarketing.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html lang="en">
	<head>
		<title>Registrasi Pemberitahuan Jatuh Tempo</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="P. Jatuh Tempo - Registrasi Pemberitahuan Jatuh Tempo (Marketing)">
	</head>
	<body class="body-padding" onkeyup="if(event.keyCode==27&&confirm('Apakah anda ingin membatalkan proses registrasi?')) document.getElementById('cancel').click();">
		<form id="Form1" method="post" runat="server" class="cnt">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<input type="text" style="display:none">
			<h1 class="title title-line">Registrasi Pemberitahuan Jatuh Tempo </h1>
			<p>Halaman 2 dari 2</p>
			<br>
			<table cellspacing="5">
				<tr>
					<td><b>Tipe</b></td>
					<td><b>:</b></td>
					<td style="width:200px;">
						<asp:label id="tipe" runat="server" font-bold="True"></asp:label>
					</td>
					<td><b>Unit</b></td>
					<td><b>:</b></td>
					<td>
						<asp:label id="unit" runat="server" font-bold="True"></asp:label>
					</td>
				</tr>
				<tr>
					<td><b>Ref.</b></td>
					<td><b>:</b></td>
					<td>
						<asp:label id="referensi" runat="server" font-bold="True"></asp:label>
					</td>
					<td><b>Customer</b></td>
					<td><b>:</b></td>
					<td>
						<asp:label id="customer" runat="server" font-bold="True"></asp:label>
					</td>
				</tr>
			</table>
			<hr size="1" noshade color="silver">
			<table cellspacing="5">
				<tr>
					<td><b>Tgl. Cetak</b></td>
					<td><b>:</b></td>
					<td>
                        <asp:textbox id="tgl" runat="server" type="text" cssclass="txt_center"></asp:textbox>
                        <label for="tgl" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                        <asp:label id="tglc" runat="server" cssclass="err"></asp:label>
					</td>
				</tr>
				<tr>
					<td><b>Tgl. Jatuh Tempo</b></td>
					<td><b>:</b></td>
					<td>
                            <asp:textbox id="tgljt" runat="server" type="text" cssclass="txt_center"></asp:textbox>
                            <label for="tgljt" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
						<asp:label id="tgljtc" runat="server" cssclass="err"></asp:label>
					</td>
				</tr>
				<tr>
					<td><b>No. Telp:</b></td>
					<td><b>:</b></td>
					<td>
						<asp:textbox id="notelp" runat="server" cssclass="input-text" width="300" maxlength="50"></asp:textbox>
					</td>
				</tr>
				<tr valign="top">
					<td rowspan="3"><b>Alamat</b></td>
					<td rowspan="3"><b>:</b></td>
					<td>
						<p><asp:textbox id="alamat1" runat="server" cssclass="input-text" width="300" maxlength="50"></asp:textbox></p>
					</td>
                </tr>
                <tr>
                    <td>
                        <p><asp:textbox id="alamat2" runat="server" cssclass="input-text" width="300" maxlength="50"></asp:textbox></p>
				    </td>
                </tr>
                <tr>
                    <td>		
                        <p><asp:textbox id="alamat3" runat="server" cssclass="input-text" width="150" maxlength="50"></asp:textbox></p>
					</td>
				</tr>
			</table>
			<br>
			<table class="tb blue-skin" cellspacing="1">
				<tr align="left" valign="bottom">
					<th>
						No.</th>
					<th>
						Tagihan</th>
					<th>
						Tipe</th>
					<th>
						Jatuh Tempo</th>
					<th align="right">
						Sisa Tagihan
					</th>
                    <th></th>
				</tr>
				<asp:placeholder id="list" runat="server"></asp:placeholder>
				<tr>
					<td colspan="4">
						<b id="gtc" runat="server">Grand Total</b>
					</td>
					<td>
						<asp:textbox id="gt" runat="server" cssclass="txt_num" readonly="True" width="120"></asp:textbox>
					</td>
				</tr>
			</table>
			<table height="50">
				<tr>
					<td><asp:LinkButton id="save" runat="server" width="75" cssclass="btn btn-blue" onclick="save_Click"><i class="fa fa-share"></i> OK</asp:LinkButton></td>
					<td>
                        <br />
					</td>
                    <td><input class="btn btn-red" id="cancel" style="width: 75px" onclick="location.href='PJTRegistrasi.aspx'"
							type="button" value="Cancel" name="cancel" runat="server">
					</td>
				</tr>
			</table>
			<script type="text/javascript">
			function tagihan(no,nilai,foo)
			{
				if(foo.checked)
					document.getElementById('lunas_'+no).value = nilai;
				else
					document.getElementById('lunas_'+no).value = "";
					
				hitunggt();
			}
			function hitunggt()
			{
				foogt = document.getElementById('gt');
				grandtotal = 0*1;
				
				eof = false;
				i = 0*1;
				while(!eof) {
					foo = document.getElementById('lunas_'+i);
					if(!foo)
					{
						eof = true;
						break;
					}
					else
					{
						total = cvtnum(foo.value);
						if(!isNaN(total))
							grandtotal = grandtotal + (total*1);
						i++;
					}
				}
				
				finalnet = Math.round(100*grandtotal)/100;
				eval("foogt.value = FinalFormat('"+finalnet+"')");
			}
			function cvtnum(foo){
				return foo.replace(/,/gi ,"");
			}
			</script>
		</form>
	</body>
</html>
