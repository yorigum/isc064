<%@ Page language="c#" Inherits="ISC064.COLLECTION.Laporan.PJT" CodeFile="PJT.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Laporan Pemberitahuan Jatuh Tempo</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Report.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Laporan Pemberitahuan Jatuh Tempo">
	</head>
	<body onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
		<form id="Form1" method="post" runat="server">
			<div style="DISPLAY:none"><uc1:head id="Head1" runat="server"></uc1:head></div>
			<table id="param" runat="server" width="100%" cellspacing="3">
				<tr>
					<td>
						<p class="comp" id="comp" runat="server"></p>
						<h1 id="judul" runat="server">
							Laporan Pemberitahuan Jatuh Tempo
						</h1>
						<table cellspacing="0" cellpadding="0">
							<tr valign="top">
								<td width="200">
									<p class="pparam">
										<asp:checkbox id="tipeCheck" runat="server" text="<b>Tipe :</b>" checked="True" autopostback="True" oncheckedchanged="tipeCheck_CheckedChanged"></asp:checkbox>
										<asp:label id="tipec" runat="server" cssclass="err"></asp:label>
									</p>
									<asp:checkboxlist id="tipe" runat="server"></asp:checkboxlist>
								</td>
								<td width="20"></td>
								<td>
									<p class="pparam">
										<b>Tanggal :</b>
									</p>
									<table>
										<tr>
											<td>dari</td>
											<td>
												<asp:textbox id="dari" runat="server" type="text"></asp:textbox>
												<label for="dari" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
											</td>
											<td rowspan="2">&nbsp;&nbsp;</td>
											<td>sampai</td>
											<td>
												<asp:textbox id="sampai" runat="server" type="text"></asp:textbox>
												<label for="sampai" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
											</td>
										</tr>
										<tr>
											<td colspan="3"><asp:label id="daric" runat="server" cssclass="err"></asp:label></td>
											<td colspan="3"><asp:label id="sampaic" runat="server" cssclass="err"></asp:label></td>
										</tr>
									</table>
									<asp:checkbox id="detil" runat="server" text="Tampilkan detil tagihan"></asp:checkbox>
								<p class="pparam">
										<b>Status :</b>
										<asp:radiobutton id="statusS" runat="server" groupname="status" font-size="14" text="SEMUA"></asp:radiobutton>
										<asp:radiobutton id="statusA" runat="server" groupname="status" font-size="14" text="AKTIF" checked="True"></asp:radiobutton>
										<asp:radiobutton id="statusB" runat="server" groupname="status" font-size="14" text="BATAL"></asp:radiobutton>
									</p>
								</td>
								
							</tr>
						</table>
						<br>
						<div>
							<table>
							    <tr>
								    <td style="min-width:auto; padding-right:10px">
									    <asp:LinkButton id="scr" accesskey="s" runat="server" cssclass="btn btn-blue" onclick="scr_Click"><i class="fa fa-search"></i> Preview</asp:LinkButton>
									</td>
                                    <td>
                                        <asp:button id="xls" accesskey="e" runat="server" text="Download Excel" cssclass="btn btn-green" onclick="xls_Click"></asp:button>
								    </td>
							    </tr>
						    </table>
						</div>
					</td>
				</tr>
			</table>
			<asp:table id="rpt" runat="server" cssclass="tb blue-skin" cellspacing="0">
				<asp:tablerow>
					<asp:tablecell columnspan="7" font-size="8pt"></asp:tablecell>
				</asp:tablerow>
				<asp:tablerow verticalalign="Bottom">
					<asp:tableheadercell horizontalalign="Left">No. PJT</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Tanggal</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Tipe</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">No. SP</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Unit</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Customer</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Status</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Right">Total</asp:tableheadercell>
				</asp:tablerow>
			</asp:table>
		</form>
	</body>
</html>
