<%@ Reference Page="~/Skema.aspx" %>
<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.ReservasiEdit" CodeFile="ReservasiEdit.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="HeadReservasi" Src="HeadReservasi.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavReservasi" Src="NavReservasi.ascx" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Edit Reservasi</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="5">
		<meta name="sec" content="Reservasi - Edit Reservasi">
	</head>
	<body onkeyup="if(event.keyCode==27) document.getElementById('cancel').click()">
		<form id="Form1" method="post" runat="server">
			<div class="content-header">
				<uc1:navreservasi id="NavReservasi1" runat="server" aktif="1"></uc1:navreservasi>				
			</div>
			<div class="tabdata">
				<div class="pad">
					<uc1:headreservasi id="HeadReservasi1" runat="server"></uc1:headreservasi>
					<table cellspacing="5">
						<tr>
							<td>Print:</td>
							<td class="printhref"><a id="printWL" runat="server"><b>Kartu Waiting List</b></a></td>
							<td class="printhref"><a id="printJadwalTagihan" runat="server"><b>Jadwal Pembayaran</b></a></td>
							<td class="printhref"><a id="printbform" runat="server"><b>Booking Form</b></a></td>
                            <td class="printhref"><a id="printtts" runat="server"><b>Tanda Terima Sementara</b></a></td>
							<td width="100%"></td>
							<td>
								<label class="ibtn ibtn-file">
									<input type="button" class="btn btn-blue btn-ico" value="Log" id="btnlog" runat="server" name="btnlog"
									accesskey="l">
								</label>
							</td>
							<td>
								<label class="ibtn ibtn-remove">
									<input type="button" class="btn btn-red btn-ico" value="Delete" id="btndel" runat="server" name="btndel"
									accesskey="d">
								</label>
							</td>
						</tr>
					</table>
					<table>
						<tr>
							<td class="stamp">
								Input :
								<asp:label id="tglInput" runat="server"></asp:label>
							</td>
							<td class="stamp">
								Edit :
								<asp:label id="tglEdit" runat="server"></asp:label>
							</td>
						</tr>
					</table>
					<table cellspacing="5">
						<tr>
							<td colspan="3">
								<input type="button" id="wl" runat="server" class="btn" value="Waiting List" name="wl"><br>
								<asp:label id="nourut" runat="server" font-bold="True" font-size="30pt"></asp:label>
								<i>
									<asp:label id="status" runat="server" font-bold="True" font-size="14"></asp:label></i>
							</td>
						</tr>
						<tr>
							<td colspan="3">
								<br>
								<p><b>Dokumen</b></p>
							</td>
						</tr>
						<tr style="display:none">
							<td>No. Sistem</td>
							<td>:</td>
							<td>
								<asp:textbox id="noreservasi" runat="server" cssclass="txt" width="150" readonly="True"></asp:textbox>
							</td>
						</tr>
                        <tr>
							<td>No. Reservasi</td>
							<td>:</td>
							<td>
								<asp:textbox id="noreservasifull" runat="server" cssclass="txt" width="200" readonly="True"></asp:textbox>
							</td>
						</tr>
						<tr>
							<td>Tgl. Reservasi</td>
							<td>:</td>
							<td>
								<asp:textbox id="tgl" runat="server" cssclass="txt_center" width="85"></asp:textbox>
								<label for="tgl" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
								<asp:label id="tglc" runat="server" cssclass="err"></asp:label>
							</td>
						</tr>
						<tr>
							<td>Batas Waktu</td>
							<td>:</td>
							<td>
								<asp:textbox id="batas" runat="server" cssclass="txt_center" width="150"></asp:textbox>
								<label for="batas" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
								<asp:label id="batasc" runat="server" cssclass="err"></asp:label>
							</td>
						</tr>
                        <tr>
							<td>Sisa Waktu</td>
							<td>:</td>
							<td>
								<asp:label id="sisawaktu" runat="server"></asp:label> Hari
							</td>
						</tr>
						<tr style="display:none">
							<td>NUP</td>
							<td>:</td>
							<td>
								<asp:textbox id="noqueue" runat="server" width="50" cssclass="txt_center"></asp:textbox>
								<asp:label id="noqueuec" runat="server" cssclass="err"></asp:label>
								<i style="font-size:7.5pt">Prioritas pemesanan atau sistem queueing</i>
							</td>
						</tr>
						<tr>
							<td colspan="3">
								<br>
								<p><b>Perhitungan Harga</b></p>
							</td>
						</tr>
						<tr>
							<td>Nilai Pengikatan</td>
							<td>:</td>
							<td>
								<asp:textbox id="nilai" runat="server" cssclass="txt_num" ReadOnly="true"></asp:textbox>
								<asp:label id="nilaic" runat="server" cssclass="err"></asp:label>
							</td>
						</tr>
						<tr>
							<td>Skema</td>
							<td>:</td>
							<td>
								<asp:dropdownlist id="skema" runat="server" cssclass="txt" width="300"></asp:dropdownlist>
							</td>
						</tr>
						<tr>
							<td>Sales</td>
							<td>:</td>
							<td>
								<asp:dropdownlist id="agent" runat="server" cssclass="txt" width="300" AutoPostBack="true" OnSelectedIndexChanged="GantiTipeSales"></asp:dropdownlist>
							</td>
						</tr>
						<tr>
							<td>Supervisor</td>
							<td>:</td>
							<td>
								<asp:textbox id="supervisor" runat="server" cssclass="txt" width="300" maxlength="150"></asp:textbox>
							    <asp:Label runat="server" ID="spv" cssclass="err"></asp:Label>
							</td>
						</tr>
						<tr>
							<td>Manager</td>
							<td>:</td>
							<td>
								<asp:textbox id="manager" runat="server" cssclass="txt" width="300" maxlength="150"></asp:textbox>
							    <asp:Label runat="server" ID="mgr" cssclass="err"></asp:Label>
							</td>
						</tr>
					</table>
					<table height="50">
						<tr>
							<td>
								<asp:LinkButton id="ok" runat="server" cssclass="btn btn-blue" width="75" onclick="ok_Click">
									<i class="fa fa-share"></i> OK
								</asp:LinkButton>
							</td>
							<td>
								<asp:LinkButton id="save" runat="server" cssclass="btn btn-orange" width="75" accesskey="a" onclick="save_Click">
									<i class="fa fa-check"></i> Apply
								</asp:LinkButton>
							</td>
							<td>
								<input id="cancel" type="button" onclick="window.close()" class="btn btn-red" value="Cancel">
							</td>
							<td style="padding-left:10px">
								<p class="feed">
									<asp:label id="feed" runat="server"></asp:label>
								</p>
							</td>
						</tr>
					</table>
				</div>
			</div>
		</form>
	</body>
</html>
