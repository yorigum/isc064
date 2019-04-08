<%@ Reference Page="~/Customer.aspx" %>
<%@ Reference Page="~/Unit.aspx" %>
<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.TTREdit" CodeFile="TTREdit.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="HeadTTR" Src="HeadTTR.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavTTR" Src="NavTTR.ascx" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Edit Tanda Terima Reservasi</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="5">
		<meta name="sec" content="Tanda Terima Reservasi - Edit TTR">
	</head>
	<body onkeyup="if(event.keyCode==27)window.close()">
		<form id="Form1" method="post" runat="server">
            <div class="content-header">
			    <uc1:navttr id="NavTTR1" runat="server" aktif="1"></uc1:navttr>
            </div>
			<div class="tabdata">
				<div class="pad"><uc1:headttr id="HeadTTR1" runat="server"></uc1:headttr>
					<table cellspacing="5">
						<tr valign="top">
							<td>Print </td>
							<td class="printhref">
								<p><a id="printTTR" runat="server"><b>Tanda Terima Reservasi</b></a></p>
							</td>
							<td width="100%"></td>
							<td>
                                <label class="ibtn ibtn-file">
                                    <input class="btn btn-blue btn-ico" id="btnlog" accesskey="l" type="button" value="  Log  " name="btnlog" runat="server">
                                </label>
							</td>
							<td>
								<input class="btn btn-red" id="btnvoid" type="button" value="Void" name="btnvoid"
										runat="server">
                            </td>
                            <td>
								<input class="btn" id="btnvoid2" type="button" value="Void Reimburse"
										name="btnvoid2" runat="server">
							</td>
						</tr>
					</table>
					<table>
						<tr>
							<td class="stamp">Kasir :
								<asp:label id="kasir" runat="server"></asp:label>,
								<asp:label id="ip" runat="server"></asp:label>&nbsp;<asp:label id="tglInput" runat="server"></asp:label>
							</td>
						</tr>
					</table>
					<table cellspacing="0" cellpadding="0">
						<tr valign="top">
							<td>
								<div style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px; PADDING-BOTTOM: 5px; PADDING-TOP: 5px">
									<table cellspacing="0" cellpadding="0">
										<tr>
											<td>
												<p><b>Status</b></p>
											</td>
											<td>
												<p><b>Cara Bayar</b></p>
											</td>
										</tr>
										<tr valign="top">
											<td width="240">
												<p><asp:label id="status" runat="server" font-size="20" font-bold="True"></asp:label></p>
											</td>
											<td>
												<p><asp:label id="carabayar" runat="server" font-size="20" font-bold="True"></asp:label></p>
											</td>
										</tr>
									</table>
								</div>
								<table cellspacing="5">
									<tr>
										<td colspan="3">
											<p><b>Data Tanda Terima Reservasi</b></p>
										</td>
									</tr>
									<tr>
										<td>Tgl. TTR</td>
										<td>:</td>
										<td>
                                            <asp:textbox id="tglttr" runat="server" width="85" cssclass="txt_center"></asp:textbox>
                                            <label for="tglttr" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
											<asp:label id="tglttrc" runat="server" cssclass="err"></asp:label>
										</td>
									</tr>
									<tr>
										<td>No. Manual</td>
										<td>:</td>
										<td>TTR
											<asp:textbox id="manualttr" runat="server" width="60" cssclass="txt_center"></asp:textbox></td>
									</tr>
									<tr>
										<td>Keterangan</td>
										<td>:</td>
										<td><asp:textbox id="ket" runat="server" width="425" cssclass="txt" maxlength="200"></asp:textbox></td>
									</tr>
									<tr>
										<td colspan="3"><br>
											<p><b>Informasi Cek Giro</b></p>
										</td>
									</tr>
									<tr>
										<td>No. BG</td>
										<td>:</td>
										<td><asp:textbox id="nobg" runat="server" width="150" cssclass="txt" maxlength="20"></asp:textbox><asp:label id="nobgc" runat="server" cssclass="err"></asp:label></td>
									</tr>
									<tr>
										<td>Tgl. BG</td>
										<td>:</td>
										<td>
                                            <asp:textbox id="tglbg" runat="server" width="85" cssclass="txt_center"></asp:textbox>
                                            <label for="tglbg" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
											<asp:label id="tglbgc" runat="server" cssclass="err"></asp:label>
										</td>
									</tr>
									<tr>
										<td colspan="3"><br>
											<p><b>Identitas Customer</b></p>
										</td>
									</tr>
									<tr>
										<td>Unit</td>
										<td>:</td>
										<td><asp:textbox id="unit" runat="server" width="200" cssclass="txt" maxlength="100"></asp:textbox><asp:label id="unitc" runat="server" cssclass="err"></asp:label></td>
									</tr>
									<tr>
										<td>Customer</td>
										<td>:</td>
										<td><asp:textbox id="customer" runat="server" width="300" cssclass="txt" maxlength="100"></asp:textbox><asp:label id="customerc" runat="server" cssclass="err"></asp:label></td>
									</tr>
								</table>
								<table height="50">
									<tr>
										<td><asp:LinkButton id="ok" runat="server" width="75" cssclass="btn btn-blue" onclick="ok_Click"><i class="fa fa-share"></i> OK</asp:LinkButton></td>
										<td><asp:LinkButton id="save" accesskey="a" runat="server" width="75" cssclass="btn btn-orange" onclick="save_Click"><i class="fa fa-check"></i>Apply</asp:LinkButton></td>
                                        <td><input class="btn btn-red" id="cancel" style="width: 75px" onclick="window.close()" type="button"
												value="Cancel">
										</td>										
										<td style="PADDING-LEFT: 10px">
											<p class="feed"><asp:label id="feed" runat="server"></asp:label></p>
										</td>
									</tr>
								</table>
							</td>
							<td width="20"></td>
							<td>Nilai :
								<p><asp:label id="nilai" runat="server" font-size="18" font-bold="True"></asp:label></p>
								<br>
							</td>
						</tr>
					</table>
				</div>
			</div>
		</form>
	</body>
</html>
