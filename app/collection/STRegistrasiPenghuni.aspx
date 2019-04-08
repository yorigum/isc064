<%@ Reference Page="~/Customer.aspx" %>
<%@ Page language="c#" Inherits="ISC064.COLLECTION.STRegistrasiPenghuni" CodeFile="STRegistrasiPenghuni.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Registrasi Surat Peringatan</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Surat Peringatan - Registrasi Surat Peringatan (Tenant)">
	</head>
	<body onkeyup="if(event.keyCode==27&&confirm('Apakah anda ingin membatalkan proses registrasi?')) document.getElementById('cancel').click();">
		<form id="Form1" method="post" runat="server" class="cnt">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<input type="text" style="display:none">
			<h1>Registrasi Surat Peringatan</h1>
			<p>Halaman 2 dari 2</p>
			<br>
			<table cellspacing="5">
				<tr>
					<td>Tipe</td>
					<td>:</td>
					<td width="100">
						<asp:label id="tipe" runat="server" font-bold="True"></asp:label>
					</td>
					<td>Unit</td>
					<td>:</td>
					<td>
						<asp:label id="unit" runat="server" font-bold="True"></asp:label>
					</td>
				</tr>
				<tr>
					<td>Ref.</td>
					<td>:</td>
					<td>
						<asp:label id="referensi" runat="server" font-bold="True"></asp:label>
					</td>
					<td>Customer</td>
					<td>:</td>
					<td>
						<asp:label id="customer" runat="server" font-bold="True"></asp:label>
					</td>
				</tr>
			</table>
			<hr size="1" noshade color="silver">
			<table cellspacing="5">
				<tr>
					<td>Tanggal</td>
					<td>:</td>
					<td>
                        <div class="input-group input-medium">
                            <asp:TextBox ID="tgl" runat="server" type="text" CssClass="form-control" style="width:50%" Height="20"></asp:TextBox>
                            <span class="input-group-btn" style="height: 34px; display: block">
                                <button class="btn-a default" runat="server" type="button" style="height: 100%">
                                    <i class="fa fa-calendar"></i>
                                </button>
                            </span>
                        </div>
						<asp:label id="tglc" runat="server" cssclass="err"></asp:label>
					</td>
				</tr>
				<tr>
					<td>No. Telp:</td>
					<td>:</td>
					<td>
						<asp:textbox id="notelp" runat="server" cssclass="input-text" width="300" maxlength="50"></asp:textbox>
					</td>
				</tr>
				<tr valign="top">
					<td>Alamat:</td>
					<td>:</td>
					<td>
						<p><asp:textbox id="alamat1" runat="server" cssclass="input-text" width="300" maxlength="50"></asp:textbox></p>
						<p><asp:textbox id="alamat2" runat="server" cssclass="input-text" width="300" maxlength="50"></asp:textbox></p>
						<p><asp:textbox id="alamat3" runat="server" cssclass="input-text" width="150" maxlength="50"></asp:textbox></p>
					</td>
				</tr>
			</table>
			<br>
			<table class="tb blue-skin" cellspacing="0">
				<tr align="left" valign="bottom">
					<th width="50">
						No.</th>
					<th width="280">
						Tagihan</th>
					<th width="75">
						Jatuh Tempo</th>
					<th align="right" width="120">
						Sisa Tagihan
					</th>
				</tr>
				<asp:placeholder id="list" runat="server"></asp:placeholder>
				<tr>
					<td colspan="3">
						<b id="gtc" runat="server">Grand Total</b>
					</td>
					<td>
						<asp:textbox id="gt" runat="server" cssclass="txt_num" readonly="True" width="120"></asp:textbox>
					</td>
				</tr>
			</table>
			<table height="50">
				<tr>
					<td><asp:button id="save" runat="server" width="75" cssclass="btn btn-blue" text="OK" onclick="save_Click"></asp:button></td>
					<td><input class="btn btn-red" id="cancel" style="width: 75px" onclick="location.href='STRegistrasi.aspx'"
							type="button" value="Cancel" name="cancel" runat="server">
					</td>
				</tr>
			</table>
			<script language="javascript">
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
