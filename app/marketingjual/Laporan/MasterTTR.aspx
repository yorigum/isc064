<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.Laporan.MasterTTR" CodeFile="MasterTTR.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Laporan Master Tanda Terima Reservasi</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Report.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Laporan Master Tanda Terima Reservasi">
	</head>
	<body onkeyup="if(event.keyCode==27&&confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
		<form id="Form1" method="post" runat="server">
			<div style="display:none"><uc1:head id="Head1" runat="server"></uc1:head></div>
			<table id="param" runat="server" width="100%" cellspacing="3">
				<tr>
					<td>
						<p class="comp" id="comp" runat="server"></p>
						<h1 id="judul" runat="server">
							Laporan Master Tanda Terima Reservasi
						</h1>
						<table cellspacing="0" cellpadding="0">
							<tr valign="top">
								<td width="200">
									<p class="pparam">
										<asp:checkbox id="carabayarCheck" runat="server" text="<b>Cara Bayar :</b>" checked="True" autopostback="True" oncheckedchanged="carabayarCheck_CheckedChanged"></asp:checkbox>
										<asp:label id="carabayarc" runat="server" cssclass="err"></asp:label>
									</p>
									<asp:checkboxlist id="carabayar" runat="server">
										<asp:listitem selected="True" value="TN">TN = Tunai</asp:listitem>
										<asp:listitem selected="True" value="KK">KK = Kartu Kredit</asp:listitem>
										<asp:listitem selected="True" value="KD">KD = Kartu Debit</asp:listitem>
										<asp:listitem selected="True" value="TR">TR = Transfer Bank</asp:listitem>
										<asp:listitem selected="True" value="BG">BG = Cek Giro</asp:listitem>
									</asp:checkboxlist>
								</td>
								<td width="20"></td>
								<td>
									<p class="pparam">
										<asp:radiobutton id="tglttr" runat="server" text="Tanggal TTR" font-bold="True" font-size="10"
											groupname="tgl" checked="True"></asp:radiobutton>
										:
										<br>
										<asp:radiobutton id="tglinput" runat="server" text="Tanggal Input" font-bold="True" font-size="10"
											groupname="tgl"></asp:radiobutton>
										:
										<br>
										<asp:radiobutton id="tglbg" runat="server" text="Tanggal BG" font-bold="True" font-size="10"
											groupname="tgl"></asp:radiobutton>
										:
									</p>
									<table>
										<tr>
											<td>dari</td>
											<td>
												<asp:textbox id="dari" runat="server" width="85" cssclass="txt_center"></asp:textbox>
												<label for="dari" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
											</td>
											<td rowspan="2">&nbsp;&nbsp;</td>
											<td>sampai</td>
											<td>
												<asp:textbox id="sampai" runat="server" width="85" cssclass="txt_center"></asp:textbox>
												<label for="sampai" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
											</td>
										</tr>
										<tr>
											<td colspan="3"><asp:label id="daric" runat="server" cssclass="err"></asp:label></td>
											<td colspan="3"><asp:label id="sampaic" runat="server" cssclass="err"></asp:label></td>
										</tr>
									</table>
									<div style="width: 100%;">
										<div style="float: left;">
											<p class="pparam">
												<b>Kasir :</b>
												<br>
												<asp:listbox id="kasir" runat="server" cssclass="ddl" width="300" rows="12">
													<asp:listitem>SEMUA</asp:listitem>
												</asp:listbox>
											</p>
											<p class="pparam">
												<b>Status :</b>
												<asp:radiobutton id="statusS" runat="server" groupname="status" font-size="14" text="SEMUA" checked="True"></asp:radiobutton>
												<asp:radiobutton id="statusB" runat="server" groupname="status" font-size="14" text="BARU"></asp:radiobutton>
												<asp:radiobutton id="statusP" runat="server" groupname="status" font-size="14" text="POST"></asp:radiobutton>
												<asp:radiobutton id="statusV" runat="server" groupname="status" font-size="14" text="VOID"></asp:radiobutton>
											</p>
										</div>
									</div>
									<br>
								</td>
							</tr>
						</table>
						<br>
						<div class="ins">
							<table>
								<tr>
									<td>
										<asp:button id="scr" accesskey="s" runat="server" text="Screen Preview" width="100" cssclass="btn" onclick="scr_Click"></asp:button>
										<asp:button id="xls" accesskey="e" runat="server" text="Download Excel" width="100" cssclass="btn" onclick="xls_Click"></asp:button>
									</td>
								</tr>
							</table>
						</div>
					</td>
				</tr>
			</table>
			<asp:table id="rpt" runat="server" cssclass="tb" cellspacing="3">
				<asp:tablerow>
					<asp:tablecell columnspan="14" font-size="8pt">
						Cara Bayar : TN = Tunai / KD = Kartu Debit / KK = Kartu Kredit / TR = Transfer Bank / BG = Cek Giro.<br>
					</asp:tablecell>
				</asp:tablerow>
				<asp:tablerow verticalalign="Bottom">
					<asp:tableheadercell horizontalalign="Left">No. TTR</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Status</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Tanggal</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Kasir</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">IP Address</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Reservasi</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Unit</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Customer</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Cara Bayar</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Keterangan</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">No. BG</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Tgl. BG</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Right">Total</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Right">Reimburse</asp:tableheadercell>
				</asp:tablerow>
			</asp:table>
		</form>
	</body>
</html>
