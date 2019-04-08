<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.UnitInfo" CodeFile="UnitInfo.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="HeadUnit" Src="HeadUnit.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavUnit" Src="NavUnit.ascx" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Informasi Unit</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Unit - Informasi Unit">
	</head>
	<body onkeyup="if(event.keyCode==27) window.close()">
		<form id="Form1" method="post" runat="server">
            <div class="content-header">
			    <uc1:navunit id="NavUnit1" runat="server" aktif="1"></uc1:navunit>
            </div>
			<div class="tabdata">
				<div class="pad">
					<uc1:headunit id="HeadUnit1" runat="server"></uc1:headunit>
					<table cellspacing="3">
						<tr>
							<td>
								<input type="button" id="lokasi" runat="server" value="Lokasi" class="btn btn-blue" name="lokasi"
									style="width:75">
							</td>
							<td>
								<input type="button" id="kalk" runat="server" class="btn btn-blue" value="Skema" name="kalk" style="width:75">
							</td>
						</tr>
					</table>
					<table cellpadding="0" cellspacing="0">
						<tr valign="top">
							<td width="300">
								<table cellspacing="5">
									<tr>
									<tr>
										<td>Jenis</td>
										<td>:</td>
										<td>
											<asp:label id="jenis" runat="server" font-bold="True"></asp:label>
										</td>
									</tr>
									<tr>
										<td>Price List</td>
										<td>:</td>
										<td>
											<asp:label id="pl" runat="server" font-bold="True"></asp:label>
										</td>
									</tr>
									<tr>
										<td>Gross / Nett</td>
										<td>:</td>
										<td>
											<asp:label id="luassg" runat="server"></asp:label>
											m2 /
											<asp:label id="luasnett" runat="server"></asp:label>
											m2
										</td>
									</tr>
								</table>
							</td>
							<td>
								<table cellspacing="5">
									<tr>
										<td>Zoning</td>
										<td>:</td>
										<td>
											<asp:label id="zoning" runat="server"></asp:label>
										</td>
									</tr>
									<tr>
										<td>Arah Hadap</td>
										<td>:</td>
										<td>
											<asp:label id="arahhadap" runat="server"></asp:label>
										</td>
									</tr>
									<tr>
										<td>Panorama</td>
										<td>:</td>
										<td>
											<asp:label id="panorama" runat="server"></asp:label>
										</td>
									</tr>
									<tr>
										<td>Nilai Strategis</td>
										<td>:</td>
										<td>
											<asp:label id="nilaistrategis" runat="server"></asp:label>
										</td>
									</tr>
								</table>
							</td>
						</tr>
					</table>
					<div style="padding:5">
						<br>
                        <div class="peach">
						    <p style="font:8pt;padding-left:3">
							    Status : A = Aktif / B = Batal / E = Expire
						    </p>
                        </div>
						<asp:table id="rpt" runat="server" cssclass="tb blue-skin" cellspacing="1">
							<asp:tablerow horizontalalign="Left">
								<asp:tableheadercell columnspan="2">No.</asp:tableheadercell>
								<asp:tableheadercell width="75">Tgl.</asp:tableheadercell>
								<asp:tableheadercell width="150">Customer</asp:tableheadercell>
								<asp:tableheadercell width="150">Sales</asp:tableheadercell>
								<asp:tableheadercell width="120">Keterangan</asp:tableheadercell>
							</asp:tablerow>
						</asp:table>
					</div>
				</div>
			</div>
		</form>
	</body>
</html>
