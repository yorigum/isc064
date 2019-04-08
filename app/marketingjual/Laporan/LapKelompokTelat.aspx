<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.Laporan.LapKelompokTelat" CodeFile="LapKelompokTelat.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Laporan Kelompok Hari Keterlambatan</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Report.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Laporan Kelompok Hari Keterlambatan">
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<table id="param" runat="server" width="100%" cellspacing="3">
				<tr>
					<td>
						<p class="comp" id="comp" runat="server"></p>
						<h1 id="judul" runat="server">
							Laporan Kelompok Hari Keterlambatan
						</h1>
						<table cellspacing="0" cellpadding="0">
							<tr valign="top">
								<td>
									<p class="pparam">
										<b>As of:</b>
									</p>
									<asp:textbox id="tgl" runat="server" width="85" cssclass="txt_center"></asp:textbox>
									<label for="tgl" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
									<asp:label id="tglc" runat="server" cssclass="err"></asp:label>
								</td>
							</tr>
							<tr>
								<td>
									<p class="pparam">
										<strong>Lantai:</strong>
										<br>
										<asp:dropdownlist id="lt" runat="server" cssclass="ddl" width="200">
											<asp:listitem selected="True" value="SEMUA"></asp:listitem>
										</asp:dropdownlist>
									</p>
								</td>
							</tr>
							<tr>
								<td>
									<p class="pparam">
										<strong>Tower:</strong>
										<br>
										<asp:dropdownlist id="tower" runat="server" cssclass="ddl" width="200">
											<asp:listitem selected="True" value="SEMUA"></asp:listitem>
										</asp:dropdownlist>
									</p>
								</td>
							</tr>
							<tr>
								<td>
									<p class="pparam">
										<strong>Sales:</strong>
										<br>
										<asp:dropdownlist id="sales" runat="server" cssclass="ddl" width="200">
											<asp:listitem selected="True" value="SEMUA"></asp:listitem>
										</asp:dropdownlist>
									</p>
								</td>
							</tr>
						</table>
						<br>
						<div class="ins">
							<table>
								<tr>
									<td>
										<asp:button id="scr" accesskey="s" runat="server" text="Screen Preview" width="100" cssclass="btn"></asp:button>
										<asp:button id="xls" accesskey="e" runat="server" text="Download Excel" width="100" cssclass="btn"></asp:button>
									</td>
								</tr>
							</table>
						</div>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>
