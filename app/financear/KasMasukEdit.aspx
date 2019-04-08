<%@ Reference Page="~/Acc.aspx" %>
<%@ Page language="c#" Inherits="ISC064.FINANCEAR.KasMasukEdit" CodeFile="KasMasukEdit.aspx.cs" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Edit Kas Masuk</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
    	<link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
		<meta name="ctrl" content="5">
		<meta name="sec" content="Tanda Terima Sementara - Edit Kas Masuk">
		<script type="text/javascript" src="/Js/NumberFormat.js"></script>
	</head>
	<body onkeyup="if(event.keyCode==27)window.close()">
		<form id="Form1" method="post" runat="server">
			<div class="pad">
				<table cellspacing="5">
					<tr valign="top">
						<td style="white-space:nowrap">No. : <b id="no" runat="server" style="font-size:11pt"></b>
						</td>
						<td>Print :</td>
						<td class="printhref">
							<p><a id="printKM" runat="server"><b>Voucher Kas Masuk</b></a></p>
						</td>
						<td width="100%"></td>
						<td>
							<input type="button" class="btn" value="  Log  " id="btnlog" runat="server" name="btnlog"
								accesskey="l">
						</td>
					</tr>
				</table>
				<table cellspacing="5">
					<tr>
						<td>Tgl. Input</td>
						<td>:</td>
						<td>
							<asp:label id="tglinput" runat="server" font-italic="True"></asp:label>
						</td>
					</tr>
					<tr>
						<td>Tanggal</td>
						<td>:</td>
						<td>
							<asp:textbox id="tgl" runat="server" cssclass="txt_center" width="85"></asp:textbox>
							<label for="tgl" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
							<asp:label id="tglc" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
					<tr>
						<td>Rekening Bank</td>
						<td>:</td>
						<td>
							<asp:dropdownlist id="acc" runat="server" cssclass="ddl" width="300"></asp:dropdownlist>
							<asp:label id="accc" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
					<tr>
						<td colspan="3">
							<br>
						</td>
					</tr>
					<tr>
						<td>Cara Bayar</td>
						<td>:</td>
						<td>
							<asp:radiobuttonlist id="carabayar" runat="server" repeatdirection="Horizontal">
								<asp:listitem value="TN">Tunai</asp:listitem>
								<asp:listitem value="BG">Cek Giro</asp:listitem>
							</asp:radiobuttonlist>
						</td>
					</tr>
					<tr>
						<td>Alat Bayar</td>
						<td>:</td>
						<td>
							<asp:textbox id="alatbayar" runat="server" cssclass="txt" width="100" maxlength="50"></asp:textbox>
						</td>
					</tr>
					<tr>
						<td colspan="3">
							<br>
						</td>
					</tr>
					<tr>
						<td>Diterima Dari</td>
						<td>:</td>
						<td>
							<asp:textbox id="diterimadari" runat="server" cssclass="txt" width="200" maxlength="50"></asp:textbox>
						</td>
					</tr>
					<tr>
						<td>Keterangan</td>
						<td>:</td>
						<td>
							<asp:textbox id="keterangan" runat="server" cssclass="txt" width="400" maxlength="200"></asp:textbox>
						</td>
					</tr>
					<tr>
						<td colspan="3">
							<br>
						</td>
					</tr>
					<tr>
						<td>Nilai</td>
						<td>:</td>
						<td>
							<asp:textbox id="nilai" runat="server" cssclass="txt_num"></asp:textbox>
							<asp:label id="nilaic" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
				</table>
				<table style="height:50px">
					<tr>
						<td>
							<asp:button id="ok" runat="server" cssclass="btn" text="OK" width="75" onclick="ok_Click"></asp:button>
						</td>
						<td>
							<input id="cancel" type="button" onclick="window.close()" class="btn btn-red" value="Cancel">
						</td>
						<td>
							<asp:Button ID="save" runat="server" CssClass="btn btn-blue" text="Apply" width="75" accesskey="a" onclick="save_Click"></asp:button>
						</td>
						<td style="PADDING-LEFT:10px">
							<p class="feed">
								<asp:label id="feed" runat="server"></asp:label>
							</p>
						</td>
					</tr>
				</table>
			</div>
		</form>
	</body>
</html>
